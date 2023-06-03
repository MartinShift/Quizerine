using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.LibraryModels
{
    public interface IPointCalculator
    {
        public int CalculatePoints(int totalTime, int spendTime, int questionCount, int correctQuesionCount);
    }
}
