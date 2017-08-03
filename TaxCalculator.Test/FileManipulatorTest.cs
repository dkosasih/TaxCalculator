using System;
using System.Collections.Generic;
using System.IO;
using AutofacContrib.NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;
using TaxCalculator.core;
using TaxCalculator.dto;
using TaxCalculator.helper;

namespace TaxCalculator.Test
{
    [TestClass]
    public class FileManipulatorTest
    {
        private AutoSubstitute _autoSubstitute;
        private FileManipulator _fileManipulator;
        private IFileWrapper _fileWrapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _autoSubstitute = new AutoSubstitute();

            _fileWrapper = _autoSubstitute.Resolve<IFileWrapper>();
            _fileManipulator = _autoSubstitute.Resolve<FileManipulator>();
        }

        [TestMethod]
        public void GetData_WhenCorrectData_ShouldReturnRequestedObject()
        {
            // Arrange
            var sampleFileReadResult = new string[]
            {
                "Firstname,Lastname,AnnualSalary,SuperRate,PayPeriod",
                "David,Rudd,60500,9,01 March - 31 March"
            };

            var filePath = @"C:\temp\InputData.csv";

            _fileWrapper.FileExists(filePath).Returns(true);
            _fileWrapper.ReadAllLines(filePath).Returns(sampleFileReadResult);

            // Act
            var result = _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert
            Assert.AreEqual(result[0].Firstname , "David", "firstname does not match");
            Assert.AreEqual(result[0].Lastname , "Rudd", "lastname does not match");
            Assert.AreEqual(result[0].AnnualSalary , (uint)60500, "Annual Salary does not match");
            Assert.AreEqual(result[0].SuperRate , (uint)9, "Super Rate does not match " );
            Assert.AreEqual(result[0].PayPeriod , "01 March - 31 March", "Pay Period does not match");
        }

        [TestMethod]
        [ExpectedException(typeof (NotSupportedException))]
        public void GetData_WhenSuperRateAbove50_ShouldThrowNotSupportedException()
        {
            // Arrange
            var sampleFileReadResult = new string[]
            {
                "Firstname,Lastname,AnnualSalary,SuperRate,PayPeriod",
                "David,Rudd,60500,51,01 March - 31 March"
            };

            var filePath = @"C:\temp\InputData.csv";

            _fileWrapper.FileExists(filePath).Returns(true);
            _fileWrapper.ReadAllLines(filePath).Returns(sampleFileReadResult);
            
            // Act
            var result = _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void GetData_WhenSuperRateBelow0_ShouldThrowNotSupportedException()
        {
            // Arrange
            var sampleFileReadResult = new string[]
            {
                "Firstname,Lastname,AnnualSalary,SuperRate,PayPeriod",
                "David,Rudd,60500,-1,01 March - 31 March"
            };

            var filePath = @"C:\temp\InputData.csv";

            _fileWrapper.FileExists(filePath).Returns(true);
            _fileWrapper.ReadAllLines(filePath).Returns(sampleFileReadResult);

            // Act
            var result = _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void GetData_WhenIncorrectPathIsPassed_ShouldThrowFileNotFoundException()
        {
            // Arrange
            var filePath = @"C:\temp\InputData.csv";
            _fileWrapper.FileExists(filePath).Returns(false);

            // Act
            var result = _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert Expect Exception
        }
    }
}
