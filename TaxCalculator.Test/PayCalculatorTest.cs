using System;
using AutofacContrib.NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.core;
using TaxCalculator.helper;

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

            _payCalculator = _autoSubstitute.Resolve<IPayCalculator>();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
