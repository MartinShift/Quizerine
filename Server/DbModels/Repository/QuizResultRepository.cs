using CommonLibrary.LibraryModels;
using DbModels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels.Repository
{
    internal class QuizResultRepository : BaseRepository<DbQuizResult>
    {
        public QuizResultRepository(QuizerineDbContext context) : base(context)
        {
            
        }
        public async Task<bool> HasPassed(string client) => await this.Entities.FirstAsync(qr=>qr.ClientName == client).ConfigureAwait(false) != null;
    }
}
