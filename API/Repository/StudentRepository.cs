using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(Student student)
        {
            await _context.Students.AddAsync(student);
            await Save();
        }

        public async Task<List<Student>> GetAll(Expression<Func<Student, bool>> filter = null)
        {
            IQueryable<Student> student = _context.Students;
            if(filter != null)
            {
                student = student.Where(filter);
            }
            return await student.ToListAsync();
        }

        public async Task<Student> GetById(Expression<Func<Student, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Student> student = _context.Students;
            if (!tracked)
            {
                student = student.AsNoTracking();
            }
            if (filter != null)
            {
                student = student.Where(filter);
            }
            return await student.FirstOrDefaultAsync();
        }

        public async Task Remove(Student student)
        {
            _context.Students.Remove(student);
            await Save();
        }

        public async Task Update(Student student)
        {
            _context.Students.Update(student);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
