using System;

namespace TaxCalculator.Dto
{
    public class InputData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public uint AnnualSalary { get; set; }
        public uint SuperRate { get; set; }
        public string PayPeriod { get; set; }

        public InputData(uint superRate)
        {
            if (superRate > 50)
            {
                throw new NotSupportedException("Maximum super annuaiton rate is 50%");
            }

            SuperRate = superRate;
        }
    }
}