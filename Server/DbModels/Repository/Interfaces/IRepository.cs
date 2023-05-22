using System.Linq.Expressions;

namespace DbModels.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<ICollection<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int Id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
