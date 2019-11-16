using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OKPO
{
    public static class Translate
    {
        [FunctionName("Translate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            TranslationRequestModel data = JsonConvert.DeserializeObject<TranslationRequestModel>(requestBody);
            var translatedText = new TranslationService().Translate(data.Text);
            return translatedText != null
                ? (ActionResult)new OkObjectResult(translatedText)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
