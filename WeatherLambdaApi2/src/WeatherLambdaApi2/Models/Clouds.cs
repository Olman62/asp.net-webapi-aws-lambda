using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    public class Clouds
    {
        [JsonProperty(PropertyName = "all")]
        public int? All { get; set; }
    }
}
