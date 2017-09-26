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
            DontTryAtHome();
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

        // Find Anything you want in your code, with advanced search
        private void DontTryAtHome()
        {
            //$BaseState.Kind == StateKind.Catch && !$CatchState.IsUnhandled && $CatchState.TypeName == "System.FormatException"
            try
            {
                string str = "IAmNotANumber";
                int.Parse(str);
            }
            catch { }

            //$VariableState.TypeName == "System.String" && !String.IsNullOrWhiteSpace($VariableState.Value) && $VariableState.Value.All(Char.IsDigit)
            string IAmANumber = "1235123";
            if (IAmANumber == "12345312")
            {
                //do things
            }
        }
    }
}
