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
using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace OKPO
{
    public static class GetSupportedDestinations
    {
        [FunctionName("GetSupportedDestinations")]
        [OpenApiOperation(Description = "Return available translation destination languages for source language")]
        [OpenApiParameter("code", In = Microsoft.OpenApi.Models.ParameterLocation.Query, Description = "Code of source language", Required = true, Type = typeof(string))]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(List<LanguageDetailsModel>), Description = "Available translation destination languages")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            TelemetryClient telemetry = new TelemetryClient();
            telemetry.InstrumentationKey = System.Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");
            var sw = Stopwatch.StartNew();
            Language sourceLanguage;
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                string code = req.Query["code"];
                sourceLanguage = (Language)int.Parse(code);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                throw;
            }
            var supportedDestinations = new TranslationService().GetSupportedDestinations(sourceLanguage);
            telemetry.GetMetric("GetSupportedDestinationsTime").TrackValue(sw.ElapsedMilliseconds);

            return new OkObjectResult(supportedDestinations);
        }
    }
}
