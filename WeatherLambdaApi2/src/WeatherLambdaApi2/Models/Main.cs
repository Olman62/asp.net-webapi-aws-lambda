using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    public class Main
    {
        [JsonProperty(PropertyName = "temp")]
        public float Temperature { get; set; }

        [JsonProperty(PropertyName = "feels_like")]
        public float FeelsLike { get; set; }

        [JsonProperty(PropertyName = "temp_min")]
        public float TemperatureMinimum { get; set; }

        [JsonProperty(PropertyName = "temp_max")]
        public float TemperatureMaximum { get; set; }

        [JsonProperty(PropertyName = "pressure")]
        public int? Pressure { get; set; }

        [JsonProperty(PropertyName = "humidity")]
        public int? Humidity { get; set; }
    }
}
