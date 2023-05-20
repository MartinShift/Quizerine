using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels
{
    internal class DbAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; }
        public int QuestionId { get; set; }
        public DbQuestion Question { get; set; }
    }
}
