using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public interface ITaxBracketProcessor
    {
        TaxBracket GetTaxBracketSet(decimal income);
    }
}