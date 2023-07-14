using Newtonsoft.Json;
using System.Text;
using Utility;
using Web.Models;
using Web.Services.IServices;

namespace Web.Services
{
    public class BaseService : IBaseServices
    {
        public ApiResponses responseModel {
            get; set; 
        }

        public IHttpClientFactory httpClient{ get; set; }
        public BaseService(IHttpClientFactory client)
        {
            this.responseModel = new ApiResponses();
            this.httpClient = client;
        }
        public async Task<T> sendAsync<T>(ApiRequest request)
        {
            try
            {
                var client = httpClient.CreateClient("StudentApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(request.Url);
                if(request.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                    Encoding.UTF8, "application/json");
                }
                switch (request.apiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync(); 
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception ex)
            {
                var dto = new ApiResponses
                {
                    ErrorMessages = new List<string>() { Convert.ToString(ex.Message) },
                    IsSuccess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T> (res);
                return APIResponse;
            }
        }
    }
}
