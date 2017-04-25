using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Starter.Examples.CarsEconomy
{
    public class Csv2Xml
    {
        public void ConvertCsv2Xml(string csvInputFile, string xmlOutputFile)
        {
            var lines = File.ReadAllLines(csvInputFile, Encoding.GetEncoding(1252));


            string[] headers = lines[0].Split(';').Select(x => x.Trim('\"')).ToArray();

            var xml = new XElement("CarsList",
                lines.Where((line, index) => index > 0).Select(line => new XElement("CarDetails",
                    line.Split(';').Select((column, index) => new XElement(headers[index], column)))));

            xml.Save(xmlOutputFile);
        }
    }
}
