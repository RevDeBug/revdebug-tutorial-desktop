using System;
using System.Xml;
using System.Xml.Linq;

#region NoTimeTravel
namespace Starter.Examples.CarsEconomy
{
    public static class RdbExtensions
    {
        public static string RdbSerialize(this System.Xml.Linq.XElement obj)
        {
            return obj.Name+": "+obj.Value;
        }
    }
}
#endregion
