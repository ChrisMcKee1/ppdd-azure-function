using Microsoft.AspNetCore.Mvc;
using Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicAPIs
{
    public interface IShared
    {
        Task<IActionResult> SendRequest(string endpointType, RequestModel model);
        Task<IActionResult> SendRequest(string endpointType);
    }

    public class Shared : IShared
    {
        private readonly HttpClient _client;

        public Shared(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }
        public async Task<IActionResult> SendRequest(string endpointType, RequestModel model)
        {
            string url = $"https://api.publicapis.org/{endpointType}?";

            url = model.title is null ? url : url + "title=" + model.title.Trim().ToLower() + "&";
            url = model.description is null ? url : url + "description=" + model.description.Trim().ToLower() + "&";
            url = model.auth is null ? url : url + "auth=" + model.auth.Trim().ToLower() + "&";
            url = model.https is null ? url : url + "https=" + model.https.Trim().ToLower() + "&";
            url = model.cors is null ? url : url + "cors=" + model.cors.Trim().ToLower() + "&";
            url = model.category is null ? url : url + "category=" + model.category.Trim().ToLower() + "&";
            url = url[..^1];

            HttpResponseMessage response = await _client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(result);
        }
        public async Task<IActionResult> SendRequest(string endpointType)
        {
            string url = $"https://api.publicapis.org/{endpointType}";

            HttpResponseMessage response = await _client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(result);
        }
    }
}