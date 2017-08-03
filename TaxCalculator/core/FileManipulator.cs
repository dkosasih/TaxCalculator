using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace TaxCalculator.core
{
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

        public void SetData<T>(T dataToSave, string outputPath)
        {
            var jsonString = JsonConvert.SerializeObject(dataToSave);
            jsonStringToCSV(jsonString, outputPath);
        }

        private  void jsonStringToCSV(string jsonContent, string outputFile)
        {
            //used NewtonSoft json nuget package
            var xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonContent + "}}");
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);

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

            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            File.WriteAllLines(outputFile, lines);
        }
    }
}