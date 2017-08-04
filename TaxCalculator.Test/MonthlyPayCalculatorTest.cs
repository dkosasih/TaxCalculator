using AutofacContrib.NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Core;

namespace TaxCalculator.Test
{
    [TestClass]
    public class MonthlyPayCalculatorTest
    {
        private AutoSubstitute _autoSubstitute;
        private IPayCalculator _payCalculator;

        [TestInitialize]
        public void Init()
        {
            _autoSubstitute = new AutoSubstitute();

            _payCalculator = _autoSubstitute.Resolve<MonthlyPayCalculator>();
        }

        [TestMethod]
        public void CalculateGrossIncomeMonthly_YearlyIncome_ReturnRoundedIntergerValue()
        {
            // Arrange
            int income = 60050;

            // Act
            var result = _payCalculator.CalculateGrossIncome((uint)income);

            // Assert
            Assert.AreEqual(5004, result);
        }

        [TestMethod]
        public void CalculateIncomeTax_YearlyIncome_ReturnMonthlyTaxValue()
        {
            // Arrange
            int income = 60050;

            // Act
            var result = _payCalculator.CalculateIncomeTax((uint)income);

            // Assert
            Assert.AreEqual(922, result);
        }

        [TestMethod]
        public void CalculateNetIncome_MonthlyIncomeAndMonthlyTax_ReturnMonthlyNetIncome()
        {
            // Arrange
            int monthlyIncome = 5004;
            int monthlyTax = 922;

            // Act
            var result = _payCalculator.CalculateNetIncome(monthlyIncome, monthlyTax);

            // Assert
            Assert.AreEqual(4082, result);
        }


        [TestMethod]
        public void CalculateSuper_MonthlyIncomeAndSuperRate_ReturnSuperValue()
        {
            // Arrange
            int monthlyIncome = 5004;
            uint superRate = 9;

            // Act
            var result = _payCalculator.CalculateSuper(monthlyIncome, superRate);

            // Assert
            Assert.AreEqual(450, result);
        }
    }
}
