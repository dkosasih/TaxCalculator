using System;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using TaxCalculator.core;

namespace TaxCalculator
{
    internal class Program
    {
        private static async void Main(string[] args)
        {
            IocConfig.RegisterDependencies();

            if (args.Contains("/?") || args.Contains("-?"))
            {
                PrintUsageInfo();
                //return;
            }

            var successRun = false;
            string inputPath, outputPath;

            using (var scope = IocConfig.Container.BeginLifetimeScope())
            {
                if (ArgumentsValid(args, out inputPath, out outputPath))
                {
                    var runner = new Runner(inputPath, outputPath, scope.Resolve<FileManipulator>(), scope.Resolve<IEmployeePaymentProcessor>());
                    successRun = await  runner.Run();
                }
                else
                {
                    PrintUsageInfo();
                    successRun = false;
                }
            }

            if (successRun)
            {
                Console.WriteLine($"Payment calculation successful. Result can be found in: {outputPath}");
            }
            else
            {
                Console.WriteLine($"Payment calculation error. Check log for details");
            }

            Console.ReadKey();
        }

        private static void PrintUsageInfo()
        {
            Console.WriteLine("Usage: TaxCalculator -i [Input Path] -o [Output Path]");
        }

        private static bool ArgumentsValid(string[] args, out string inputPath, out string outputPath)
        {
            if (args.Contains("-i") && args.Contains(("-o")) && args.Length == 4)
            {
                inputPath = args[Array.IndexOf(args, "-i") + 1];
                outputPath = args[Array.IndexOf(args, "-o") + 1];

                if (!File.Exists(inputPath))
                {
                    Console.WriteLine("Input file does not exists");
                    return false;
                }
                else if (!Directory.Exists(Path.GetDirectoryName(outputPath)))
                {
                    Console.WriteLine("Output directory does not exists");
                    return false;
                }
                

                return true;
            }
            else
            {
                inputPath = null;
                outputPath = null;
                return false;
            }
        }

    }
}
