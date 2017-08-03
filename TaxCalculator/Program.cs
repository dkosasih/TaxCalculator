using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json;
using TaxCalculator.dto;

namespace TaxCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IocConfig.RegisterDependencies();

            if (args.Contains("/?") || args.Contains("-?"))
            {
                PrintUsageInfo();
                //return;
            }

            string inputPath, outputPath;
            if (ArgumentsValid(args, out inputPath, out outputPath))
            {
                var runner = new Runner(inputPath, outputPath, IocConfig.Container.Resolve<FileManipulator>());
                runner.Run().Wait();
            }
            else
            {
                PrintUsageInfo();
                //return;
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

                Console.WriteLine(outputPath);
                Console.WriteLine(inputPath);

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
                    var inputData = _manipulator.GetData<IEnumerable<InputData>>(_inputPath);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error executing the Runner. ex: {ex.Message}");
                }
            }
            return Task.FromResult(0);
        }
    }


    public class FileManipulator
    {
        public T GetData<T>(string filePath)
        {
            var csv = File.ReadAllLines(filePath).Select(l => l.Split(',')).ToList(); 

            var headers = csv[0];
            var dicts = csv.Skip(1).Select(row => headers.Zip(row, Tuple.Create).ToDictionary(p => p.Item1, p => p.Item2)).ToArray();

            var jsonString = JsonConvert.SerializeObject(dicts);


            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
