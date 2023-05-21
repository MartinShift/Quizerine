﻿using Microsoft.EntityFrameworkCore;
using Server.DbModels;
using System.Linq.Expressions;

namespace DbModels.Repository.Interfaces
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _entities;
        protected DbSet<TEntity> Entities => this._entities ??= _context.Set<TEntity>();
        protected QuizerineDbContext _context;
        public BaseRepository(QuizerineDbContext context) => _context = context;
        public async Task CreateAsync(TEntity entity) => await this.Entities.AddAsync(entity).ConfigureAwait(false);
        public virtual async Task<IList<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression) => await this.Entities.Where(expression).ToListAsync().ConfigureAwait(false);
        public virtual async Task<IList<TEntity>> GetAllAsync() => await this.Entities.ToListAsync().ConfigureAwait(false);
        public virtual async Task UpdateAsync(TEntity entity) => this.Entities.Update(entity);
        public virtual async Task DeleteAsync(int Id) => await DeleteAsync(Id).ConfigureAwait(false);
    }
}
