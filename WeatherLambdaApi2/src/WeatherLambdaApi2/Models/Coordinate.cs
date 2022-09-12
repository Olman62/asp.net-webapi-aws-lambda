using Newtonsoft.Json;

namespace WeatherLambdaApi.Models
{
    
    public class Coordinate
    {
        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }
    }
}
