using Web.Models;

namespace Web.Services.IServices
{
    public interface IBaseServices
    {
        ApiResponses responseModel { get; set; }    
        Task<T> sendAsync<T>(ApiRequest request);
    }
}
