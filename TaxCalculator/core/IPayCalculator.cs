namespace TaxCalculator.Core
{
    public interface IPayCalculator
    {
        int CalculateGrossIncome(decimal grossIncomeYearly);
        int CalculateIncomeTax(decimal grossIncomeYearly);
        int CalculateNetIncome(int grossIncomeMonthly, int incomeTax);
        int CalculateSuper(int grossIncomeMonthly, uint superRate);
    }
}
