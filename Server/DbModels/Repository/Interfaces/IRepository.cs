using System.Linq.Expressions;

namespace DbModels.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int Id);
    }
}
