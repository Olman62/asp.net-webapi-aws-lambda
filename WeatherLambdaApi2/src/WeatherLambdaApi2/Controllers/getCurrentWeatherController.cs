using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherLambdaApi.Helpers;
using WeatherLambdaApi.Models;
using Microsoft.Extensions.Options;
using Enyim.Caching.Configuration;

namespace WeatherLambdaApi2.Controllers
{
    [Route("v1/[controller]")]
    public class getCurrentWeatherController : ControllerBase
    {
        Dictionary<float, string> windDirections = new Dictionary<float, string>()
        {
            {0f, "N (North)" },
            {22.5f, "NNE (North Nort East)" },
            {45f, "NE (Nort East)" },
            {67.5f, "ENE (East North East" },
            {90f, "E (East)" },
            {112.5f, "ESE (East South East)" },
            {135f, "SE (South East)" },
            {157.5f, "SSE (South South East)" },
            {180f, "S (South)" },
            {202.5f, "SSW (South South West)" },
            {225f, "SW (South West)" },
            {247.5f, "WSW (West South West)" },
            {270f, "W (West)" },
            {292.5f, "WNW (West North West)" },
            {315f, "NW (North West)" },
            {337.5f, "NNW (North North West)" },
            {360f, "N (North)" }
        };

        private readonly ILogger<getCurrentWeatherController> _logger;

        private IAmazonSecretsManager? secretsManager;

        private ElasticCacheHelper elasticCacheHelper;

        public getCurrentWeatherController(ILogger<getCurrentWeatherController> logger, 
            IServiceProvider serviceProvider,
            ILoggerFactory _loggerFactory,
            IMemcachedClientConfiguration _memcachedClientConfiguration)
        {
            _logger = logger;
            secretsManager = serviceProvider.GetService<IAmazonSecretsManager>();
            elasticCacheHelper = new ElasticCacheHelper(_loggerFactory, _memcachedClientConfiguration);
        }

        // GET v1/getCurrentWeather
        [HttpGet]
        public async Task<IActionResult> GetAsync(string city)
        {
            try
            {
                var cityWeather = elasticCacheHelper.GetWeatherData(city, out string errorString);
                if (cityWeather!=null && errorString.Length<1 && cityWeather.GetType()==typeof(WeatherResponse))
                {
                    return Ok(cityWeather);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var getSecretResult = async () =>
                    {
                        GetSecretValueRequest request = new GetSecretValueRequest();
                        request.SecretId = "weatherApiKey";
                        request.VersionStage = "AWSCURRENT";
                        GetSecretValueResponse response = await secretsManager.GetSecretValueAsync(request);
                        return response.SecretString;
                    };

                    string weatherApiKey = await getSecretResult();

                    if (weatherApiKey == null)
                    {
                        return NotFound();
                    }


                    if (weatherApiKey == null)
                    {
                        return NotFound();
                    }

                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid={weatherApiKey}&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<WeatherForecast>(stringResult);
                    
                    string windDirection(float? direct)
                    {
                        var dirKey = windDirections.Keys.Where(x => (direct >= x - 11.25 && direct < x + 11.25)).FirstOrDefault();
                        return dirKey!=default(float) ? windDirections[dirKey] : "";
                    }

                    var weatherResponse = new WeatherResponse()
                    {
                        Temperature = $"{rawWeather?.Main?.Temperature ?? float.NaN} °C",
                        WeatherConditions = rawWeather != null ? String.Join(", ", rawWeather.Weather.Select(x => x.Main)) : "",
                        Wind = rawWeather != null ? $"{rawWeather.Wind?.Speed ?? float.NaN} km/h" : "",
                        WindDirection = windDirection(rawWeather?.Wind?.Degree ?? float.NaN),
                        Pressure = rawWeather?.Main?.Pressure ?? default(int),
                        Humidity = rawWeather?.Main?.Humidity ?? default(int)

                    };

                    elasticCacheHelper.StoreWeatherData(city, weatherResponse, out string errorString);

                    if (errorString.Length > 0) _logger.LogError(errorString);

                    return Ok(weatherResponse);
                }
                catch (HttpRequestException httpRequestException)
                {
                    _logger.LogError($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }

            
        }

    }
}