using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.core
{
    public interface IPayCalculator
    {
        uint CalculateIncomeTax(uint grossIncomeYearly);
        int CalculateNetIncome(uint grossIncomeYearly, int incomeTax);
        int CalculateSuper(int superRate);
    }
}
