using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.LibraryModels;

public class PointCalculatorVer01 : IPointCalculator
{
    public int CalculatePoints(int totalTime, int spendTime, int questionCount, int correctQuesionCount)
    {
        const float BASE_FOR_CORRECT = 1000.0F;
        const float BASE_FOR_TIME = 10.0F;

        float persentOfCorrect = (float)correctQuesionCount / (float)questionCount;

        float bonusForTime = ((float)totalTime - (float)spendTime) * BASE_FOR_TIME * persentOfCorrect;

        return (int)((BASE_FOR_CORRECT * persentOfCorrect) + bonusForTime);
    }
}
