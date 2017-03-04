using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Starter.Examples.CarsEconomy
{
    public class CarsEconomy
    {
        //private const string FileCsvInput = @"..\..\Csv\cars.csv";
        private const string FileXmlOutput = @"..\..\Xml\cars.xml";
        private const string FileXmlResult = @"..\..\Xml\cars_result.xml";

        public bool Calculate()
        {
           // var csv2Xml = new Csv2Xml();
           // csv2Xml.ConvertCsv2Xml(FileCsvInput, FileXmlOutput);

            File.Copy(FileXmlOutput, FileXmlResult, true);

            var doc = new XmlDocument();
            doc.Load(FileXmlResult);
            var root = doc.DocumentElement;
            if (root == null) return false;

            //select nodes
            var nodes = root.SelectNodes("CarDetails");
            
            //set culture info for proper reading float values
            var englishCulture = new CultureInfo("en-EN");

            if (nodes == null) return false;

            var xDocument = new XDocument();
            var rootElement = new XElement("CarsList");
            xDocument.Add(rootElement);

            foreach (XmlNode node in nodes)
            {
                var carName = node["Car"].InnerText;
                var country = node["Origin"].InnerText;
                var mpg = node["MPG"].InnerText.ToString(new CultureInfo("en-EN"));

                double value;
                Double.TryParse(mpg, NumberStyles.Float, englishCulture, out value);
                double lvalue;
                lvalue = FuelUsageConverter.Mpg2Liters(value);
                 
                var carElement = new XElement("CarDetails"); 
                carElement.Add(new XElement("Car", carName));
                carElement.Add(new XElement("L100", lvalue.ToString("0.##", englishCulture)));
                carElement.Add(new XElement("Country", country));
                rootElement.Add(carElement);
            }
            //save result to disk
            xDocument.Save(FileXmlResult);

            return true;
        }
    }
}
