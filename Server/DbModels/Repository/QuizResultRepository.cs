using CommonLibrary.LibraryModels;
using DbModels.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels.Repository
{
    internal class QuizResultRepository : BaseRepository<DbQuizResult>
    {
        public QuizResultRepository(QuizerineDbContext context) : base(context)
        {

        }
    }
}
