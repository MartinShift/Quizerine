using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.LibraryModels
{
    public class Question
    {
        public Question()
        {
            Answers = new List<Answer>();
        }
        public int? Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public byte?[] Image { get; set; }
    }
}
