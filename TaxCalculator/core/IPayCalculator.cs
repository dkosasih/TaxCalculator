namespace TaxCalculator.Core
{
    public interface IPayCalculator
    {
        uint CalculateIncomeTax(uint grossIncomeYearly);
        int CalculateNetIncome(uint grossIncomeYearly, int incomeTax);
        int CalculateSuper(int superRate);
    }
}
