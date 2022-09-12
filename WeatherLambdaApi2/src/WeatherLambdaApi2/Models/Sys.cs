using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    public class Sys
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string? Country { get; set; }

        [JsonProperty(PropertyName = "sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty(PropertyName = "sunset")]
        public long Sunset { get; set; }
    }
}
