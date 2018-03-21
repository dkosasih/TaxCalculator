using System;

namespace TaxCalculator.Core
{
    public abstract class PayCalculator : IPayCalculator
    {
        public abstract int CalculateGrossIncome(uint grossIncomeYearly);
        public abstract int CalculateIncomeTax(uint grossIncomeYearly);

        public virtual int CalculateNetIncome(int grossIncomeMonthly, int incomeTax)
        {
            return grossIncomeMonthly - incomeTax;
        }

        public virtual int CalculateSuper(int grossIncomeMonthly, uint superRate)
        {
            var superValue =
                (int)Math.Round((grossIncomeMonthly * ((double)superRate / 100)), MidpointRounding.AwayFromZero);
            return superValue;
        }
    }
}