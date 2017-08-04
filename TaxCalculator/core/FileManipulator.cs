using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using TaxCalculator.helper;

namespace TaxCalculator.core
{
    public class FileManipulator
    {
        private IFileWrapper _fileWrapper;

        public  FileManipulator(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        public async Task<T> GetData<T>(string filePath)
        {
            if (!_fileWrapper.FileExists(filePath))
            {
               throw new FileNotFoundException("Csv input file not found");
            }
            var csv = _fileWrapper.ReadAllLines(filePath).Select(l => l.Split(',')).ToList(); 

            var headers = csv[0];
            var dicts = csv.Skip(1).Select(row => headers.Zip(row, Tuple.Create).ToDictionary(p => p.Item1, p => p.Item2)).ToArray();

            var jsonString = await Task.Factory.StartNew(()=>JsonConvert.SerializeObject(dicts));
            
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public async void SetData<T>(T dataToSave, string outputPath)
        {
            if (!_fileWrapper.DirectoryExists(_fileWrapper.GetDirectoryName(outputPath)))
            {
                throw new DirectoryNotFoundException("Output directory does not exists");
            }

            var jsonString = await Task.Factory.StartNew(()=> JsonConvert.SerializeObject(dataToSave));
            var csvList = TransformJson(jsonString);
            
            _fileWrapper.WriteAllLines(outputPath, csvList);
        }

        private  List<string> TransformJson(string jsonContent)
        {
            // convert to xml so it can be converted to datatable
            var xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonContent + "}}");
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);

            // convert to datatable so it can get the headers and rows
            var xmlReader = new XmlNodeReader(xml);
            var dataSet = new DataSet();

            dataSet.ReadXml(xmlReader);

            var dataTable = dataSet.Tables[0];

            var lines = new List<string>();
            var columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);

            // parse it to list of string
            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);

            return lines;
        }
    }
}