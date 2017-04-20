using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invoices.Models
{
    public class InvoiceEntry
    {
        public int InvoiceEntryId { get; set; }
        public int Quantity { get; set; }
        
        public int ProductId { get; set; }
        public string InvoiceId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
    }
}