using Aliencube.AzureFunctions.Extensions.OpenApi;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using Aliencube.AzureFunctions.Extensions.OpenApi.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OKPO
{
    public static class RenderSwaggerDocument
    {
        [FunctionName(nameof(RenderSwaggerDocument))]
        [OpenApiIgnore]
        public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger.json")] HttpRequest req,
    ILogger log)
        {
            var settings = new AppSettings();
            var filter = new RouteConstraintFilter();
            var helper = new DocumentHelper(filter);
            var document = new Document(helper);
            var result = await document.InitialiseDocument()
                                       .AddMetadata(settings.OpenApiInfo)
                                       .AddServer(req, settings.HttpSettings.RoutePrefix)
                                       .Build(Assembly.GetExecutingAssembly(), new CamelCaseNamingStrategy())
                                       .RenderAsync(OpenApiSpecVersion.OpenApi2_0, OpenApiFormat.Json)
                                       .ConfigureAwait(false);
            var response = new ContentResult()
            {
                Content = result,
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
        }
    }
}
