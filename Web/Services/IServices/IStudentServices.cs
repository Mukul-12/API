using Web.Models.DTO;

namespace Web.Services.IServices
{
    public interface IStudentServices
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(createStudentDTO dto);
        Task<T> UpdateAsync<T>(updateStudentDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
