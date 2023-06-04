using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.LibraryModels
{
    public class PointCalculatorVer02 : IPointCalculator
    {
        public int CalculatePoints(int totalTime, int spendTime, int questionCount, int correctQuesionCount)
        {
            var points = correctQuesionCount * 100;
            var timeScore = (double)totalTime / spendTime;
            return (int)(points * timeScore);
        }
    }
}
