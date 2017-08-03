using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.helper
{
    public interface IFileWrapper
    {
        string[] ReadAllLines(string path);
        void WriteAllLines(string path, IEnumerable<string> contents);
        bool FileExists(string path);
        bool DirectoryExists(string path);
        string GetDirectoryName(string path);
    }
}
