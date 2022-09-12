using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    public class Weather
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "main")]
        public string? Main  { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string? Description { get; set; }
    }
}
