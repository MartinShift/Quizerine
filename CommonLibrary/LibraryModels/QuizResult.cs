using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.LibraryModels
{
    public class QuizResult
    {
        public string ClientName { get; set; }
        public Quiz Quiz { get; set; }
        public int SecondsSpent { get; set; }
        public int Points { get; set; }
    }
}
