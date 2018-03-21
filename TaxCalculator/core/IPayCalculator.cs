namespace TaxCalculator.Core
{
    public interface IPayCalculator
    {
        int CalculateNetIncome(int grossIncomeMonthly, int incomeTax);
        int CalculateSuper(int grossIncomeMonthly, uint superRate);
    }
}
