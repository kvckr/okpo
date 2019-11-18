using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;

namespace OKPO
{
    public class AppSettings : OpenApiAppSettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        public AppSettings() : base()
        {
        }
    }

    public static class RenderSwaggerUI
    {
        [FunctionName(nameof(RenderSwaggerUI))]
        [OpenApiIgnore]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequest req,
    ILogger log)
        {
            var settings = new AppSettings();
            var ui = new SwaggerUI();
            var result = await ui.AddMetadata(settings.OpenApiInfo)
                                 .AddServer(req, settings.HttpSettings.RoutePrefix)
                                 .BuildAsync()
                                 .RenderAsync("swagger.json", settings.SwaggerAuthKey)
                                 .ConfigureAwait(false);
            var response = new ContentResult()
            {
                Content = result,
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
        }
    }
}
