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
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using System.Net;

namespace OKPO
{
    public static class GetLanguagesDetails
    {
        [FunctionName("GetLanguagesDetails")]
        [OpenApiOperation(Description = "Return list of all languages and their codes")]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(List<LanguageDetailsModel>), Description = "List of all languages and their codes")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var languageCodes = new TranslationService().GetLanguagesDetails();
            return new OkObjectResult(languageCodes);
        }
    }
}
