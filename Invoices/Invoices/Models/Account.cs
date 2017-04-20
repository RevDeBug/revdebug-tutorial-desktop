using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Invoices.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Balance { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}