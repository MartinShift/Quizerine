using DbModels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels.Repository
{
    internal class QuizRepository : BaseRepository<DbQuiz>
    {
        public QuizRepository(QuizerineDbContext context) : base(context)
        {
        }
        public virtual async Task<IList<DbQuiz>> FindByConditionAsync(Expression<Func<DbQuiz, bool>> expression) => await this.Entities.Where(expression).Include(x => x.Questions).ToListAsync().ConfigureAwait(false);
        public virtual async Task<IList<DbQuiz>> GetAllAsync() => await this.Entities.ToListAsync().ConfigureAwait(false);

    }
}
