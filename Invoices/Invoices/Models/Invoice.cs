using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Invoices.Models
{
    public class Invoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InvoiceId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime IssueDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; }
        public double Reconciled { get; set; }

        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Contractor { get; set; }

        public virtual ICollection<InvoiceEntry> InvoiceEntries { get; set; }
    }
}