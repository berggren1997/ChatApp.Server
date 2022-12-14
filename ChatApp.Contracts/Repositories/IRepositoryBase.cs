using System.Linq.Expressions;

namespace ChatApp.Contracts.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition,
            bool trackChanges);

        void Create(T entity);
        void Delete(T entity);
    }
}
