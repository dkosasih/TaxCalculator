using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using TaxCalculator.core;
using TaxCalculator.dto;

namespace TaxCalculator
{
    public class Runner
    {
        private string _inputPath;
        private string _outputPath;
        private FileManipulator _manipulator;
        private IEmployeePaymentProcessor _paymentProcessor;

        public Runner(string inputPath, string outputPath, FileManipulator manipulator, IEmployeePaymentProcessor paymentProcessor)
        {
            _inputPath = inputPath;
            _outputPath = outputPath;
            _manipulator = manipulator;
            _paymentProcessor = paymentProcessor;
        }
        
        public async Task<bool> Run()
        {
            
                try
                {
                    var inputData = await _manipulator.GetData<IList<InputData>>(_inputPath);
                
                    var outputFile =  _paymentProcessor.GeneratePaymentSummary(inputData);

                    if (outputFile.Count > 0)
                    {
                        _manipulator.SetData(outputFile, _outputPath);
                    }
                    else
                    {
                        throw new Exception("Empty result no csv generated");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error executing the Runner. ex: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}