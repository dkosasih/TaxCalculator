using System.Collections.Generic;
using System.Linq;
using AutofacContrib.NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TaxCalculator.Core;
using TaxCalculator.Dto;

namespace TaxCalculator.Test
{
    [TestClass]
    public class EmployeePaymentProcessorTest
    {
        private AutoSubstitute _autoSubstitute;
        private IEmployeePaymentProcessor _employeePaymentProcessor;
        private IPayCalculator _payCalculator;

        [TestInitialize]
        public void Init()
        {
            _autoSubstitute = new AutoSubstitute();

            _employeePaymentProcessor = _autoSubstitute.Resolve<EmployeePaymentProcessor>();
            _payCalculator = _autoSubstitute.Resolve<IPayCalculator>();
        }

        [TestMethod]
        public void GeneratePaymentSummary_InputDataset_ReturnOutputDataset()
        {
            // Arrange
            var sampleInputDataset = new List<InputData>()
            {
                new InputData(9)
                {
                    Firstname = "David",
                    Lastname = "Rudd",
                    AnnualSalary = 60050,
                    PayPeriod = "01 March - 31 March"
                }
            };

            _payCalculator.CalculateGrossIncome(Arg.Any<uint>()).Returns(5004);
            _payCalculator.CalculateIncomeTax(Arg.Any<uint>()).Returns(922);
            _payCalculator.CalculateNetIncome(Arg.Any<int>(), Arg.Any<int>()).Returns(4082);
            _payCalculator.CalculateSuper(Arg.Any<int>(), Arg.Any<uint>()).Returns(450);

            // Act 
            var result = _employeePaymentProcessor.GeneratePaymentSummary(sampleInputDataset);

            // Assert
            Assert.AreEqual($"{sampleInputDataset.First().Firstname} {sampleInputDataset.First().Lastname}", result.First().Name, "Name does not match the input");
            Assert.AreEqual(sampleInputDataset.First().PayPeriod, result.First().PayPeriod, "Pay period does not match the input");
            Assert.AreEqual(5004, result.First().GrossIncome);
            Assert.AreEqual(922, result.First().IncomeTax);
            Assert.AreEqual(4082, result.First().NetIncome);
            Assert.AreEqual(450, result.First().SuperAnnuationEarned);
        }
    }
}
