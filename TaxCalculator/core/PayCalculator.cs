using System;

namespace TaxCalculator.Core
{
    public  class PayCalculator : IPayCalculator
    {

        public int CalculateGrossIncomeMonthly(uint grossIncomeYearly)
        {
            var monthlyIncome = (int)Math.Round(((double)grossIncomeYearly/12), MidpointRounding.AwayFromZero);
            return monthlyIncome;
        }

        public  int CalculateIncomeTax(uint grossIncomeYearly)
        {
            return (int)grossIncomeYearly - 2;
        }

        public  int CalculateNetIncome(uint grossIncomeYearly, int incomeTax)
        {
            // TODO: Implement
            return 1;
        }

        public  int CalculateSuper(int superRate)
        {
            // TODO: Implement
            return 0;
        }
    }
}
