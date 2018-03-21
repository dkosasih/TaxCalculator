using System;
using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public  class MonthlyPayCalculator : IPayCalculator
    {
        private readonly ITaxBracketProcessor _taxBracketProcessor;

        public MonthlyPayCalculator(ITaxBracketProcessor taxBracketProcessor)
        {
            _taxBracketProcessor = taxBracketProcessor;
        }
        public int CalculateGrossIncome(decimal grossIncomeYearly)
        {
            var monthlyIncome = (int)Math.Round(((double)grossIncomeYearly/12), MidpointRounding.AwayFromZero);
            return monthlyIncome;
        }

        public  int CalculateIncomeTax(decimal grossIncomeYearly)
        {
            var taxBracket = _taxBracketProcessor.GetTaxBracketSet(grossIncomeYearly);
            var incomeTax =
                (int)Math.Round((taxBracket.PreviousTaxBracket + (grossIncomeYearly - taxBracket.LowerLimit)*(decimal)taxBracket.TaxRate)/12,
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

       
    }
}
