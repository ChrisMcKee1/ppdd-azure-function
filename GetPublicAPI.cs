using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Model;
using System.Net;
using System.Threading.Tasks;

namespace PublicAPIs
{
    public class GetPublicAPIs
    {
        private readonly IShared _shared;

        public GetPublicAPIs(IShared shared)
        {
            _shared = shared;
        }

        [FunctionName("GetPublicAPIs")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Public APIs" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "title", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **title** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "Application/json", bodyType: typeof(Api), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req
        )
        {
            RequestModel model = new()
            {
                title = req.Query["title"],
                description = req.Query["description"],
                auth = req.Query["auth"],
                https = req.Query["https"],
                cors = req.Query["cors"],
                category = req.Query["category"]
            };
            return await _shared.SendRequest("entries", model);
        }

    }
}

