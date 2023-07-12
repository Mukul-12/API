using API.Models;
using System.Linq.Expressions;

namespace API.Repository.IRepository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll(Expression<Func<Student, bool>> filter = null);
        Task<Student> GetById(Expression<Func<Student, bool>> filter = null, bool tracked=true);
        Task Create(Student student);
        Task Update(Student student);
        Task Remove(Student student);
        Task Save();
    }
}
