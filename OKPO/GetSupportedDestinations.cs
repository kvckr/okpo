using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OKPO
{
    public static class GetSupportedDestinations
    {
        [FunctionName("GetSupportedDestinations")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string code = req.Query["code"];
            Language sourceLanguage = (Language)int.Parse(code);
            var supportedDestinations = new TranslationService().GetSupportedDestinations(sourceLanguage);
            return new OkObjectResult(supportedDestinations);
        }
    }
}
