using CommonLibrary.LibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels
{
    internal class DbQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DbAnswer> Answers { get; set; }
        public byte?[] Image { get; set; }
        public int QuizId { get; set; }
        public DbQuiz Quiz { get; set; }
    }
}
