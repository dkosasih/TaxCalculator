using System;

namespace TaxCalculator.Core
{
    public  class MonthlyPayCalculator : IPayCalculator
    {

        public int CalculateGrossIncome(uint grossIncomeYearly)
        {
            var monthlyIncome = (int)Math.Round(((double)grossIncomeYearly/12), MidpointRounding.AwayFromZero);
            return monthlyIncome;
        }

        public  int CalculateIncomeTax(uint grossIncomeYearly)
        {
            var taxParameter = TaxParameters2017(grossIncomeYearly);
            var incomeTax =
                (int)Math.Round((taxParameter.Item2 + (grossIncomeYearly - taxParameter.Item1)*taxParameter.Item3)/12,
                    MidpointRounding.AwayFromZero);

            return incomeTax;
        }

        public  int CalculateNetIncome(int grossIncomeMonthly, int incomeTax)
        {
            return grossIncomeMonthly - incomeTax;
        }

        public int CalculateSuper(int grossIncomeMonthly, uint superRate)
        {
            var superValue =
                (int) Math.Round((grossIncomeMonthly*((double)superRate/100)), MidpointRounding.AwayFromZero);
            return superValue;
        }

        private Tuple<int, int, double> TaxParameters2017(uint income)
        {
            const int bracketA = 18200,
                bracketB = 37000,
                bracketC = 80000,
                bracketD = 180000;

            const int preBracketAValue = 0,
                preBracketBValue = 3572,
                preBracketCValue = 17547,
                preBracketDValue = 54547;

            const double bracketARate = 0.19,
                bracketBRate = 0.325,
                bracketCRate = 0.37,
                bracketDRate = 0.45;


            if (income > bracketD)
            {
                return new Tuple<int, int, double>(bracketD, preBracketDValue, bracketDRate);
            }
            else if (income > bracketC)
            {
                return new Tuple<int, int, double>(bracketC, preBracketCValue, bracketCRate);
            }
            else if (income > bracketB)
            {
                return new Tuple<int, int, double>(bracketB, preBracketBValue, bracketBRate);
            }
            else if (income > bracketA)
            {
                return new Tuple<int, int, double>(bracketA, preBracketAValue, bracketARate);
            }
            else // below 18200
            {
                return new Tuple<int, int, double>(0, 0, 0);
            }
        }
    }
}
