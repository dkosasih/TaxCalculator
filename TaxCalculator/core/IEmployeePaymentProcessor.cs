using System.Collections.Generic;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public interface IEmployeePaymentProcessor
    {
        IList<OutputData> GeneratePaymentSummary(IList<InputData> data);
    }
}