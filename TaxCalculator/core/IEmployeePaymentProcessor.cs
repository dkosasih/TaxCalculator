using System.Collections.Generic;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public interface IEmployeePaymentProcessor
    {
        string Period { get; set; }
        IList<OutputData> GeneratePaymentSummary(IList<InputData> data);
    }
}