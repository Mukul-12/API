using API.Data;
using API.Models;
using API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbset;
        
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbset = _context.Set<T>();
        }
        public async Task Create(T student)
        {
            await dbset.AddAsync(student);
            await Save();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> student = dbset;  // _context.Students;
            if (filter != null)
            {
                student = student.Where(filter);
            }
            return await student.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> student = dbset;
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

        public async Task Remove(T student)
        {
            dbset.Remove(student);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
