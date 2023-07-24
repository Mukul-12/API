using Newtonsoft.Json.Linq;
using Utility;
using Web.Models;
using Web.Models.DTO;
using Web.Services.IServices;

namespace Web.Services
{
    public class StudentService : BaseService, IStudentServices
    {
        private readonly IHttpClientFactory _clientFactory;
        private string studentUrl;
        public StudentService(IHttpClientFactory client, IConfiguration configuration) : base(client)
        {
            _clientFactory = client;
            studentUrl = configuration.GetValue<string>("ServiceUrls:StudentApi");
        }

        public Task<T> CreateAsync<T>(createStudentDTO dto)
        {
            return sendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                Url = studentUrl + "/api/Student"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return sendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.DELETE,
                Url = studentUrl + "/api/Student/" +id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return sendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                Url = studentUrl + "/api/Student"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return sendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,
                Url = studentUrl + "/api/Student/" + id
            }) ;
        }

        public Task<T> UpdateAsync<T>(updateStudentDTO dto)
        {
            return sendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                Data = dto,
                Url = studentUrl + "/api/Student/" + dto.Id
            }) ;
        }
    }
}
