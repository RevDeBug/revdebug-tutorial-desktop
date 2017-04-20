using Invoices.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Invoices.DataAccess
{
    public class InvoicesContext : DbContext
    {
        public InvoicesContext() : base("InvoiceContext")
        {

        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceEntry> InvoiceEntries { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products{ get; set; }
    }
}