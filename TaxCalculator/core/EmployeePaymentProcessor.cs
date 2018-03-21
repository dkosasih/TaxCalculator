using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public class EmployeePaymentProcessor : IEmployeePaymentProcessor
    {
        private IPayCalculator _payCalc;

        public EmployeePaymentProcessor(IPayCalculator payCalculator)
        {
            _payCalc = payCalculator;
        }

        public IList<OutputData> GeneratePaymentSummary(IList<InputData> data)
        {
            var resultData = data.Select(x =>
                new OutputData()
                {
                    Name = $"{x.Firstname} {x.Lastname}",
                    PayPeriod = x.PayPeriod,
                    GrossIncome = _payCalc.CalculateGrossIncome(x.AnnualSalary),
                    IncomeTax = _payCalc.CalculateIncomeTax(x.AnnualSalary),
                    NetIncome =
                        _payCalc.CalculateNetIncome(_payCalc.CalculateGrossIncome(x.AnnualSalary),
                            _payCalc.CalculateIncomeTax(x.AnnualSalary)),
                    SuperAnnuationEarned =
                        _payCalc.CalculateSuper(_payCalc.CalculateGrossIncome(x.AnnualSalary), x.SuperRate)
                }).ToList();

            return resultData;
        }

    }
}
