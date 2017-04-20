using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invoices.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public double Tax { get; set; }

        public virtual ICollection<InvoiceEntry> Entries { get; set; }
    }
}