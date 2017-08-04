using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutofacContrib.NSubstitute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;
using TaxCalculator.Core;
using TaxCalculator.Dto;
using TaxCalculator.Helper;

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
        public async Task GetData_WhenCorrectData_ShouldReturnRequestedObject()
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
            var result = await _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert
            Assert.AreEqual(result[0].Firstname, "David", "firstname does not match");
            Assert.AreEqual(result[0].Lastname, "Rudd", "lastname does not match");
            Assert.AreEqual(result[0].AnnualSalary, (uint) 60500, "Annual Salary does not match");
            Assert.AreEqual(result[0].SuperRate, (uint) 9, "Super Rate does not match ");
            Assert.AreEqual(result[0].PayPeriod, "01 March - 31 March", "Pay Period does not match");
        }

        [TestMethod]
        [ExpectedException(typeof (NotSupportedException))]
        public async Task GetData_WhenSuperRateAbove50_ShouldThrowNotSupportedException()
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
            var result = await _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof (JsonSerializationException))]
        public async Task GetData_WhenSuperRateBelow0_ShouldThrowNotSupportedException()
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
            var result = await _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof (FileNotFoundException))]
        public async Task GetData_WhenIncorrectPathIsPassed_ShouldThrowFileNotFoundException()
        {
            // Arrange
            var filePath = @"C:\temp\InputData.csv";
            _fileWrapper.FileExists(filePath).Returns(false);

            // Act
            var result = await _fileManipulator.GetData<IList<InputData>>(filePath);

            // Assert Expect Exception
        }


        [TestMethod]
        [ExpectedException(typeof (DirectoryNotFoundException))]
        public async Task SetData_WrongDirectory_ThrowDirectoryNotFoundException()
        {
            // Arrange
            var filePath = @"C:\temp\InputData.csv";
            _fileWrapper.DirectoryExists(filePath).Returns(false);
            _fileWrapper.GetDirectoryName(filePath).Returns(@"C:\temp\");

            // Act
            await _fileManipulator.SetData(Arg.Any<IList<InputData>>(), filePath);

            // Assert Expect Exception
        }

        [TestMethod]
        public async Task SetData_CorrectFile_SetFileShouldReceiveCall()
        {
            // Arrange
            var filePath = @"C:\temp\InputData.csv";
            _fileWrapper.GetDirectoryName(Arg.Any<string>()).Returns(string.Empty);
            _fileWrapper.DirectoryExists(Arg.Any<string>()).Returns(true);

            // Act
            await _fileManipulator.SetData(Arg.Any<IList<InputData>>(), filePath);

            // Assert
            _fileWrapper.Received().WriteAllLines(filePath, Arg.Any<IEnumerable<string>>());
        }
    }
}
