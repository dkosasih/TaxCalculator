using System.Collections.Generic;
using System.Linq;
using Autofac.Features.Indexed;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public class EmployeePaymentProcessor : IEmployeePaymentProcessor
    {
        private PayCalculator _payCalc;
        public string Period { get; set; }

        public EmployeePaymentProcessor(IIndex<string, PayCalculator> payCalculator)
        {
            Period = "Monthly"; //default to monthly
            _payCalc = payCalculator[Period];
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
