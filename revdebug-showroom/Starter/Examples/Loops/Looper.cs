using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Starter.Examples.Loops
{
    class Looper
    {
        private const string CarsXml = @"..\..\Xml\cars.xml";

        public List<string> CollectCars()
        {
            List<string> myCars = new List<string>();

            var doc = new XmlDocument();
            doc.Load(CarsXml);
            var root = doc.DocumentElement;
            if (root == null) return myCars;
            
            var nodes = root.SelectNodes("CarDetails");
            
            var englishCulture = new CultureInfo("en-EN");

            if (nodes == null) return myCars;

            var xDocument = new XDocument();
            var rootElement = new XElement("CarsList");
            xDocument.Add(rootElement);

            foreach (XmlNode node in nodes)
            {
                var carName = node["Car"].InnerText;
                myCars.Add(carName);
            }

            return myCars;
        }
    }
}
