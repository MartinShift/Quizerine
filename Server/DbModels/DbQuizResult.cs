using CommonLibrary.LibraryModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels
{
    public class DbQuizResult
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DbQuiz Quiz { get; set; }
        public int SecondsSpent { get; set; }
        public int Points { get; set; }
        public int QuizId { get; set; }
    }
}
