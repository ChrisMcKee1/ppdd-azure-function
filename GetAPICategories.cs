using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Http;
using System.Text;
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
        [OpenApiOperation(operationId: "Run", tags: new[] { "Public APIs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "Application/json", bodyType: typeof(Categories), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log
        )
        {
            IActionResult result = await _shared.SendRequest("categories");
            StringContent json = new(result.ToString(), Encoding.UTF8, "application/json");

            return new OkObjectResult(json);
        }
    }
}
