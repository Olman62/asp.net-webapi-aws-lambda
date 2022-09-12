using Enyim.Caching;
using WeatherLambdaApi.Models;
using Newtonsoft.Json;
using Enyim.Caching.Configuration;

namespace WeatherLambdaApi.Helpers
{
    public class ElasticCacheHelper
    {
        MemcachedClient client;

        public ElasticCacheHelper(ILoggerFactory _loggerFactory, IMemcachedClientConfiguration _memcachedClientConfiguration)
        {
            this.client = new MemcachedClient(_loggerFactory, _memcachedClientConfiguration);
                        
        }

        public void StoreWeatherData(string city, WeatherResponse weatherResponse, out string errorString)
        {
            errorString = "";

            try
            {
                var serializer = new JsonSerializer();
                var jsonText = JsonConvert.SerializeObject(weatherResponse);
                var result = this.client.Store(Enyim.Caching.Memcached.StoreMode.Set, city, weatherResponse, DateTime.Now.AddMinutes(60));
            }
            catch (Exception ex)
            {
                errorString = $"Can not store current weather for the city {city} {Environment.NewLine} {ex.Message}";
            }
        }

        public WeatherResponse? GetWeatherData(string city, out string errorString)
        {
            errorString = "";

            try
            {
                var result = this.client.Get<WeatherResponse>(city);
                return result;
            }
            catch (Exception ex)
            {
                errorString = $"Can not retrieve current weather for the city {city} {Environment.NewLine} {ex.Message}";
                return null;
            }
            
        }

    }
}
