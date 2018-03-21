﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Dto;

namespace TaxCalculator.Core
{
    public class BiWeeklyPayCalculator : PayCalculator
    {
        public override int CalculateGrossIncome(uint grossIncomeYearly)
        {
            var monthlyIncome = (int)Math.Round(((double)grossIncomeYearly / 26), MidpointRounding.AwayFromZero);
            return monthlyIncome;
        }

        public override int CalculateIncomeTax(uint grossIncomeYearly)
        {
            var taxBracket = TaxParameters2017(grossIncomeYearly);
            var incomeTax =
                (int)Math.Round((taxBracket.PreviousTaxBracket + (grossIncomeYearly - taxBracket.LowerLimit) * taxBracket.TaxRate) / 26,
                    MidpointRounding.AwayFromZero);

            return incomeTax;
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

        private TaxBracket TaxParameters2017(uint income)
        {
            var taxBrackets = GetParameterSet();
            return taxBrackets.OrderByDescending(x => x.LowerLimit).First(x => income > x.LowerLimit);
        }
    }
}