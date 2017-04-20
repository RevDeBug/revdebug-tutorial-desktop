using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Invoices.DataAccess;
using Invoices.Models;
using System.Xml;

namespace Invoices.Controllers
{
    public class InvoicesController : Controller
    {
        private InvoicesContext db = new InvoicesContext();

        // GET: Invoices
        public ActionResult Index()
        {
            var invoices = db.Invoices.Include(i => i.Contractor);
            return View(invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(string id)
        {
            ReconcileData.Init();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "FirstName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,IssueDate,DueDate,Accepted,AccountId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "FirstName", invoice.AccountId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "FirstName", invoice.AccountId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,IssueDate,DueDate,Accepted,AccountId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "AccountId", "FirstName", invoice.AccountId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Reconsile(string id)
        {
            Invoice invoice = db.Invoices.Find(id);

            invoice.Reconciled = CalculateReconcilation(id);
            db.Entry(invoice).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details",new { id = id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private double CalculateReconcilation(string id)
        {
            double reconsiled = 0;
            var rows = ReconcileData.InvoiceEntries.Select($"InvoiceId ='{id}'");
            var Products = ReconcileData.Products;
            foreach (var row in rows)
            {
                var product = Products.Select($"ProductId = '{row["ProductId"]}'").FirstOrDefault();
                var productName = product["Label"];
                var unitPrice = double.Parse((string)product["UnitPrice"]);
                var taxRate = Int32.Parse((string)product["Tax"]);
                var quantity = int.Parse((string)row["quantity"]);
                var tax = unitPrice * taxRate / 100;

                reconsiled += (unitPrice + tax) * quantity;
            }
            return reconsiled;
        }
    }
}
