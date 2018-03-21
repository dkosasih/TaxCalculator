using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Dto
{
    public class TaxBracket
    {
        public int LowerLimit { get; set; }
        public int UpperLimit { get; set; }
        public double TaxRate { get; set; }
        public int PreviousTaxBracket { get; set; }

    }
}
