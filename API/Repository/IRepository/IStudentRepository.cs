using API.Models;
using System.Linq.Expressions;

namespace API.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        
        Task<Student> Update(Student student);
    }
}
