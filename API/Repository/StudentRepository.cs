using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Student> Update(Student student)
        {
            student.updatedDate = DateTime.Now;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }

        


    }
}
