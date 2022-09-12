using Newtonsoft.Json;


namespace WeatherLambdaApi.Models
{
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public float? Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public float? Degree { get; set; }
    }
}
