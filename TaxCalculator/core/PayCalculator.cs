using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.core
{
    public  class PayCalculator : IPayCalculator
    {
        public  uint CalculateIncomeTax(uint grossIncomeYearly)
        {
            // TODO: Implement
            return 0;
        }

        public  int CalculateNetIncome(uint grossIncomeYearly, int incomeTax)
        {
            // TODO: Implement
            return 0;
        }

        public  int CalculateSuper(int superRate)
        {
            // TODO: Implement
            return 0;
        }
    }
}
