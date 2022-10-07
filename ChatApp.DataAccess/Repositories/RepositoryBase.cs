using ChatApp.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatApp.DataAccess.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().AsNoTracking() :
            _context.Set<T>();
        }
            

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition,
            bool trackChanges)
        {
            return !trackChanges ? _context.Set<T>().Where(condition).AsNoTracking() :
            _context.Set<T>().Where(condition);
        }
            
        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity) => _context?.Set<T>().Remove(entity);
    }
}
