namespace TaxCalculator.Core
{
    public interface IPayCalculator
    {
        int CalculateGrossIncome(uint grossIncomeYearly);
        int CalculateIncomeTax(uint grossIncomeYearly);
        int CalculateNetIncome(int grossIncomeMonthly, int incomeTax);
        int CalculateSuper(int grossIncomeMonthly, uint superRate);
    }
}
