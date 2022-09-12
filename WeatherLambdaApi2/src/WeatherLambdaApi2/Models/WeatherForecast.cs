using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    public class WeatherForecast
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "coord")]
        public Coordinate? Coord { get; set; }

        [JsonProperty(PropertyName = "weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty(PropertyName = "base")]
        public string? Base { get; set; }

        [JsonProperty(PropertyName = "main")]
        public Main? Main { get; set; }

        [JsonProperty(PropertyName = "visibility")]
        public int? Visibility { get; set; }

        [JsonProperty(PropertyName = "wind")]
        public Wind? Wind { get; set; }

        [JsonProperty(PropertyName = "clouds")]
        public Clouds? Clouds { get; set; }

        [JsonProperty(PropertyName = "dt")]
        public long? DateTime { get; set; }

        [JsonProperty(PropertyName = "sys")]
        public Sys? Sys { get; set; }

        [JsonProperty(PropertyName = "timezone")]
        public int? Timezone { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "cod")]
        public int? Code { get; set; }
    }

    
}