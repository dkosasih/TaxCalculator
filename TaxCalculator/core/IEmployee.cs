namespace TaxCalculator.core
{
    public interface IEmployee
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        uint AnnualSalary { get; set; }
        uint Super { get; set; }
        string PayPeriod { get; set; }
    }
}