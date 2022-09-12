using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    public class WeatherResponse
    {
        [JsonProperty(PropertyName = "temperature")]
        public string? Temperature { get; set; }

        [JsonProperty(PropertyName = "weather_conditions")]
        public string? WeatherConditions { get; set; }

        [JsonProperty(PropertyName = "wind")]
        public string? Wind { get; set; }

        [JsonProperty(PropertyName = "wind_direction")]
        public string? WindDirection { get; set; }

        [JsonProperty(PropertyName = "pressure")]
        public int? Pressure { get; set; }

        [JsonProperty(PropertyName = "humidity")]
        public int? Humidity { get; set; }
    }
}
