namespace TaxCalculator.Core
{
    public interface IPayCalculator
    {
        int CalculateGrossIncomeMonthly(uint grossIncomeYearly);
        int CalculateIncomeTax(uint grossIncomeYearly);
        int CalculateNetIncome(uint grossIncomeYearly, int incomeTax);
        int CalculateSuper(int superRate);
    }
}
