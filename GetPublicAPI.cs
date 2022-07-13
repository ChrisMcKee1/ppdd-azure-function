using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Model;
using System.Net;
using System.Net.Http;
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
        [OpenApiOperation(operationId: "List-APIs", tags: new[] { "Public APIs" })]
        [OpenApiParameter(name: "title", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **title** parameter")]
        [OpenApiParameter(name: "description", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **description** parameter")]
        [OpenApiParameter(name: "auth", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **auth** parameter")]
        [OpenApiParameter(name: "https", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **https** parameter")]
        [OpenApiParameter(name: "cors", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **cors** parameter")]
        [OpenApiParameter(name: "category", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **category** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "Application/json", bodyType: typeof(Api), Description = "The OK response")]
        public async Task<HttpResponseMessage> Run(
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

