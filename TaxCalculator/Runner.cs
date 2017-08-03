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

        public Runner(string inputPath, string outputPath, FileManipulator manipulator)
        {
            _inputPath = inputPath;
            _outputPath = outputPath;
            _manipulator = manipulator;
        }


        public Task Run()
        {
            using (ILifetimeScope scope = IocConfig.Container.BeginLifetimeScope())
            {
                try
                {
                    var inputData = _manipulator.GetData<IList<InputData>>(_inputPath);

                    var processor = scope.Resolve<IEmployeePaymentProcessor>();

                    var outputFile = processor.GeneratePaymentSummary(inputData);

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
                }
            }
            return Task.FromResult(0);
        }
    }
}