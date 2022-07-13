using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicAPIs
{
    public class GetAPICategory
    {
        private readonly IShared _shared;

        public GetAPICategory(IShared shared)
        {

            _shared = shared;
        }

        [FunctionName("GetAPICategory")]
        [OpenApiOperation(operationId: "Category", tags: new[] { "Public APIs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "Application/json", bodyType: typeof(Categories), Description = "The OK response")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            return await _shared.SendRequest("categories");
        }
    }
}
