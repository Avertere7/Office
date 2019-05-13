using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BiuroRachunkowe.Models;

namespace BiuroRachunkowe.Controllers
{
    public class OfficeController : Controller
    {
        private OfficeModel db = new OfficeModel();


	
        // GET: Office
        public ActionResult Index()
        {
            return View(db.InvoiceHeader.ToList());
        }

        // GET: Office/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceHeader invoiceHeader = db.InvoiceHeader.Find(id);
            if (invoiceHeader == null)
            {
                return HttpNotFound();
            }
            return View(invoiceHeader);
        }

        // GET: Office/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Office/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InvoiceNumber,InvoiceDate,InvoiceStatus,Voucher,Remarks,Currency,ExchangeRate,InvoiceValue,TransportCost_,AdditionalCost,Supplier,ShipFrom,TypeOfTranosport")] InvoiceHeader invoiceHeader)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceHeader.Add(invoiceHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoiceHeader);
        }

        // GET: Office/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceHeader invoiceHeader = db.InvoiceHeader.Find(id);
            if (invoiceHeader == null)
            {
                return HttpNotFound();
            }
            return View(invoiceHeader);
        }

        // POST: Office/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,InvoiceNumber,InvoiceDate,InvoiceStatus,Voucher,Remarks,Currency,ExchangeRate,InvoiceValue,TransportCost_,AdditionalCost,Supplier,ShipFrom,TypeOfTranosport")] InvoiceHeader invoiceHeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoiceHeader);
        }

        // GET: Office/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceHeader invoiceHeader = db.InvoiceHeader.Find(id);
            if (invoiceHeader == null)
            {
                return HttpNotFound();
            }
            return View(invoiceHeader);
        }

        // POST: Office/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InvoiceHeader invoiceHeader = db.InvoiceHeader.Find(id);
            db.InvoiceHeader.Remove(invoiceHeader);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
