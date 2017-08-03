using System.Collections.Generic;
using TaxCalculator.dto;

namespace TaxCalculator.core
{
    public interface IEmployeePaymentProcessor
    {
        IList<OutputData> GeneratePaymentSummary(IList<InputData> data);
    }
}