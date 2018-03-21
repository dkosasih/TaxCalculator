using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public class TaxBracketProcessor: ITaxBracketProcessor
    {
        private FileManipulator _fileManipulator;
        public TaxBracketProcessor(FileManipulator fileManipulator)
        {
            _fileManipulator = fileManipulator;
        }

        public TaxBracket GetTaxBracketSet(decimal income)
        {
            var taxBrackets = GetParameterSet();
            return taxBrackets.OrderByDescending(x => x.LowerLimit).First(x => income > x.LowerLimit);
        }
        private IEnumerable<TaxBracket> GetParameterSet()
        {
            var taxParameters = new List<TaxBracket>()
            {
                new TaxBracket()
                {
                    LowerLimit = 0,
                    UpperLimit = 0,
                    TaxRate = 0,
                    PreviousTaxBracket = 0
                },
                new TaxBracket()
                {
                    LowerLimit = 0,
                    UpperLimit = 18200,
                    TaxRate = 0,
                    PreviousTaxBracket = 0
                },
                new TaxBracket()
                {
                    LowerLimit = 18200,
                    UpperLimit = 37000,
                    TaxRate = 0.19,
                    PreviousTaxBracket = 0
                },
                new TaxBracket()
                {
                    LowerLimit = 37000,
                    UpperLimit = 80000,
                    TaxRate = 0.325,
                    PreviousTaxBracket = 3572
                },
                new TaxBracket()
                {
                    LowerLimit = 80000,
                    UpperLimit = 180000,
                    TaxRate = 0.37,
                    PreviousTaxBracket = 17547
                },
                new TaxBracket()
                {
                    LowerLimit = 180000,
                    UpperLimit = int.MaxValue,
                    TaxRate = 0.45,
                    PreviousTaxBracket = 54547
                }

            };
            return taxParameters;
        }
    }
}