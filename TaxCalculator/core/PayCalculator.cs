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

            return 0;
        }

        public  int CalculateNetIncome(uint grossIncomeYearly, int incomeTax)
        {
            return 0;
        }

        public  int CalculateSuper(int superRate)
        {
            return 0;
        }
    }
}
