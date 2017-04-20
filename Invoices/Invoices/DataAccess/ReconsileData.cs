using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace Invoices.DataAccess
{
    public class ReconcileData
    {

        public static DataTable Accounts, Products, Invoices, InvoiceEntries;

        public static void Init()
        {
            if (Accounts == null)
            {
                InitColumns();
                InitRows();
            }
        }

        private static void InitRows()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            XmlDocument doc = new XmlDocument();
            XmlTextReader reader = new XmlTextReader(asm.GetManifestResourceStream("Invoices.DataAccess.InvoiceData.xml"));
            doc.Load(reader);

            XmlNodeList parentNode = doc.GetElementsByTagName("InvoiceData");

            DataRow dr;

            foreach (XmlNode node in parentNode.Item(0).ChildNodes.Item(0))
            {
                dr = Products.NewRow();
                for(int i = 0; i < Products.Columns.Count; i++)
                {
                    if(node.ChildNodes.Item(i).ChildNodes.Item(0) != null)
                    {
                        dr[i] = node.ChildNodes.Item(i).ChildNodes.Item(0).Value;
                    }
                }
                Products.Rows.Add(dr);
            }

            foreach (XmlNode node in parentNode.Item(0).ChildNodes.Item(1))
            {
                dr = Accounts.NewRow();
                for (int i = 0; i < Accounts.Columns.Count; i++)
                {
                    if (node.ChildNodes.Item(i).ChildNodes.Item(0) != null)
                    {
                        dr[i] = node.ChildNodes.Item(i).ChildNodes.Item(0).Value;
                    }
                }
                Accounts.Rows.Add(dr);
            }

            foreach (XmlNode node in parentNode.Item(0).ChildNodes.Item(2))
            {
                dr = Invoices.NewRow();
                dr[0] = node.ChildNodes.Item(0).ChildNodes.Item(0).Value + DateTime.Now.Year;
                dr[1] = node.ChildNodes.Item(1).ChildNodes.Item(0).Value;
                Invoices.Rows.Add(dr);
            }

            foreach (XmlNode node in parentNode.Item(0).ChildNodes.Item(3))
            {
                dr = InvoiceEntries.NewRow();
                dr[0] = node.ChildNodes.Item(0).ChildNodes.Item(0).Value;
                dr[1] = node.ChildNodes.Item(1).ChildNodes.Item(0).Value + DateTime.Now.Year;
                dr[2] = node.ChildNodes.Item(2).ChildNodes.Item(0).Value;
                dr[3] = node.ChildNodes.Item(3).ChildNodes.Item(0).Value;
                InvoiceEntries.Rows.Add(dr);
            }
        }

        private static void InitColumns()
        {
            Accounts = new DataTable("Accounts");
            DataColumn id = new DataColumn("AccountId");
            DataColumn firstName = new DataColumn("FirstName");
            DataColumn lastName = new DataColumn("LastName");
            Accounts.Columns.AddRange(new DataColumn[] { id, firstName, lastName });

            Products = new DataTable("Products");
            id = new DataColumn("ProductId");
            DataColumn label = new DataColumn("Label");
            DataColumn description = new DataColumn("Description");
            DataColumn unitPrice = new DataColumn("UnitPrice");
            DataColumn tax = new DataColumn("Tax");
            Products.Columns.AddRange(new DataColumn[] { id, label, description, unitPrice, tax });

            Invoices = new DataTable("Invoices");
            id = new DataColumn("InvoiceId");
            DataColumn accId = new DataColumn("AccountId");
            Invoices.Columns.AddRange(new DataColumn[] { id, accId });

            InvoiceEntries = new DataTable("InvoiceEntries");
            id = new DataColumn("InvoiceEntryId");
            DataColumn invoiceId = new DataColumn("InvoiceId");
            DataColumn productId = new DataColumn("ProductId");
            DataColumn quantity = new DataColumn("Quantity");
            InvoiceEntries.Columns.AddRange(new DataColumn[] { id, invoiceId, productId, quantity });
        }
    }
}