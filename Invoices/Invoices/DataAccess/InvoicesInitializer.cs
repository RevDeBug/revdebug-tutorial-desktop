using System;
using System.Collections.Generic;
using System.Linq;
using Invoices.Models;

namespace Invoices.DataAccess
{
    public class InvoicesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<InvoicesContext>
    {
        protected override void Seed(InvoicesContext context)
        {
            var accounts = new List<Account>
            {
                new Account {FirstName = "Exceptional", LastName = "LLC", Balance = 1024},
                new Account {FirstName = "BadData", LastName = "Inc.", Balance = 2048},
                new Account {FirstName = "Upright", LastName = "S corp", Balance = 4096}
            };

            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product{Label = "Nimbus2000", Description = "BroomStick", UnitPrice = 3000, Tax = 23},
                new Product{Label = "T1000", Description = "Housework helping machine", UnitPrice = 5999, Tax = 0},
                new Product{Label = "The One Ring", Description = "Makes you dissappear", UnitPrice = 1999, Tax = 0},
                new Product{Label = "Volley ball", Description = "Wilson??", UnitPrice = 19, Tax = 5},
                new Product{Label = "Mjolnir", Description = "Nailed it", UnitPrice = 12335, Tax = 15}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            var invoices = new List<Invoice>
            {
                new Invoice {InvoiceId = "222/ST/" + DateTime.Now.Year, AccountId = 1, IssueDate = DateTime.Now.AddDays(-25).AddHours(-4).AddMinutes(20), DueDate = DateTime.Now.AddDays(24).AddHours(-4).AddMinutes(20)},
                new Invoice {InvoiceId = "88/SU/05/" + DateTime.Now.Year, AccountId = 3, IssueDate = DateTime.Now.AddDays(-12).AddHours(-2).AddMinutes(45), DueDate = DateTime.Now.AddDays(15).AddHours(-4).AddMinutes(20)},
                new Invoice {InvoiceId = "135/ST/GD/05/" + DateTime.Now.Year, AccountId = 2, IssueDate = DateTime.Now.AddDays(-16).AddHours(-9).AddMinutes(24), DueDate = DateTime.Now.AddDays(8).AddHours(-4).AddMinutes(20)}
            };

            invoices.ForEach(i => context.Invoices.Add(i));
            context.SaveChanges();
            
            var invoiceEntries = new List<InvoiceEntry>
            {
                new InvoiceEntry{InvoiceId = "222/ST/" + DateTime.Now.Year, ProductId = 1, Quantity = 5},
                new InvoiceEntry{InvoiceId = "222/ST/" + DateTime.Now.Year, ProductId = 2, Quantity = 20},
                new InvoiceEntry{InvoiceId = "222/ST/" + DateTime.Now.Year, ProductId = 5, Quantity = 1},
                new InvoiceEntry{InvoiceId = "88/SU/05/" + DateTime.Now.Year, ProductId = 3, Quantity = 7},
                new InvoiceEntry{InvoiceId = "88/SU/05/" + DateTime.Now.Year, ProductId = 5, Quantity = 5},
                new InvoiceEntry{InvoiceId = "88/SU/05/" + DateTime.Now.Year, ProductId = 4, Quantity = 8},
                new InvoiceEntry{InvoiceId = "135/ST/GD/05/" + DateTime.Now.Year, ProductId = 2, Quantity = 10},
                new InvoiceEntry{InvoiceId = "135/ST/GD/05/" + DateTime.Now.Year, ProductId = 3, Quantity = 1},
                new InvoiceEntry{InvoiceId = "135/ST/GD/05/" + DateTime.Now.Year, ProductId = 4, Quantity = 50}
            };

            invoiceEntries.ForEach(ie => context.InvoiceEntries.Add(ie));
            context.SaveChanges();

        }
    }
}