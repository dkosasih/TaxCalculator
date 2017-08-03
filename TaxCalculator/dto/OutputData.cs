namespace TaxCalculator.dto
{
    public class OutputData
    {
        public string Name { get; set; }
        public uint Pay { get; set; }
        public string PayPeriod { get; set; }
        public uint GrossIncome { get; set; }
        public int IncomeTax { get; set; }
        public int NetIncome { get; set; }
        public int SuperAnnuationEarned { get; set; }
    }
}