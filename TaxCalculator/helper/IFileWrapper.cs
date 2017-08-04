using System.Collections.Generic;

namespace TaxCalculator.Helper
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
