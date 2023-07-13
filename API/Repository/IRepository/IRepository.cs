using API.Models;
using System.Linq.Expressions;

namespace API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task<T> GetById(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task Create(T student);
        Task Remove(T student);
        Task Save();
    }
}
