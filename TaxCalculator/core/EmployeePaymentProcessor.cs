using System.Collections.Generic;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public class EmployeePaymentProcessor:IEmployeePaymentProcessor
    {
        private IPayCalculator _payCalc;

        public EmployeePaymentProcessor(IPayCalculator payCalculator)
        {
            _payCalc = payCalculator;
        }

        public IList<OutputData> GeneratePaymentSummary(IList<InputData> data)
        {
            // TODO: Implement
            var resultData = new List<OutputData>
            {
                new OutputData()
                {
                    Name = "d k",
                    IncomeTax = 1000,
                    NetIncome = 590,
                    GrossIncome = 150000
                }
            };
            return resultData;
        }

    }
}
