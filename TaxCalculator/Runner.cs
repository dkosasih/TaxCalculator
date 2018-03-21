using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Core;
using TaxCalculator.Dto;

namespace TaxCalculator
{
    public class Runner
    {
        private FileManipulator _manipulator;
        private IEmployeePaymentProcessor _paymentProcessor;

        public Runner(FileManipulator manipulator, IEmployeePaymentProcessor paymentProcessor)
        {
            _manipulator = manipulator;
            _paymentProcessor = paymentProcessor;
        }
        
        public async Task<bool> Run(string inputPath, string outputPath)
        {
                try
                {
                    var inputData = await _manipulator.GetData<IList<InputData>>(inputPath);
                
                    var outputFile =  _paymentProcessor.GeneratePaymentSummary(inputData);

                    if (outputFile.Count > 0)
                    {
                        await _manipulator.SetData(outputFile, outputPath);
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