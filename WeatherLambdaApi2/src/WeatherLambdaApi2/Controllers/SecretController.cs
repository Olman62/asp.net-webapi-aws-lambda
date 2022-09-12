using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Mvc;

namespace WeatherLambdaApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretController : ControllerBase
    {
        private readonly IAmazonSecretsManager _secretsManager;

        public SecretController(IAmazonSecretsManager secretsManager)
        {
            _secretsManager = secretsManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetOpenWeatherKey()
        {
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = "weatherApiKey";
            request.VersionStage = "AWSCURRENT";
            GetSecretValueResponse response = await _secretsManager.GetSecretValueAsync(request);
            return Ok(new { Secret = response.SecretString });
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateOpenWeatherKey([FromBody]string secret)
        {
            CreateSecretRequest request = new CreateSecretRequest();
            request.Name = "weatherApiKey";
            request.SecretString = secret;
            
            request.ForceOverwriteReplicaSecret = true;

            CreateSecretResponse? response = await _secretsManager.CreateSecretAsync(request);

            return Ok(new { secretResponse = response });
        }

        [HttpPut("weatherApiKey")]
        public async Task<IActionResult> UpdateOpenWeatherKey([FromBody] string secret)
        {
            UpdateSecretRequest request = new UpdateSecretRequest();
            request.SecretId = "weatherApiKey";
            request.SecretString = secret;

            UpdateSecretResponse? response = await _secretsManager.UpdateSecretAsync(request);

            return Ok(new { secretResponse = response });
        }

        [HttpDelete("weatherApiKey")]
        public async Task<IActionResult> DeleteOpenWeatherKey()
        {
            DeleteSecretRequest request = new DeleteSecretRequest();
            request.SecretId= "weatherApiKey";
            request.ForceDeleteWithoutRecovery = true;

            DeleteSecretResponse? response = await _secretsManager.DeleteSecretAsync(request);

            return Ok(new { secretResponse = response });
        }

    }
}
