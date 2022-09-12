using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    public class SecretResponse
    {
        [JsonProperty(PropertyName = "secret")]
        public string? Secret { get; set; }
    }
}
