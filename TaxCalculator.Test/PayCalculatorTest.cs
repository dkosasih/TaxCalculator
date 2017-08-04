using AutofacContrib.NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Core;

namespace TaxCalculator.Test
{
    [TestClass]
    public class PayCalculatorTest
    {
        private AutoSubstitute _autoSubstitute;
        private IPayCalculator _payCalculator;

        [TestInitialize]
        public void Init()
        {
            _autoSubstitute = new AutoSubstitute();

            _payCalculator = _autoSubstitute.Resolve<PayCalculator>();
        }

        [TestMethod]
        public void CalculateIncomeTax_YearlyIncome_ReturnRoundedIntergerValue()
        {
            // Arrange
            int income = 60050;

            // Act
            var result = _payCalculator.CalculateGrossIncomeMonthly((uint)income);

            // Assert
            Assert.AreEqual(result, 5004);
        }
    }
}
