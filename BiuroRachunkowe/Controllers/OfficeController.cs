﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BiuroRachunkowe.Models;

namespace BiuroRachunkowe.Controllers
{
    public class OfficeController : Controller
    {
        private OfficeModel db = new OfficeModel();


	
        // GET: Office
        public ActionResult InvoiceList(string SearchString, string sort, long? id, string btnsubmit = null)
        {

			if (!String.IsNullOrEmpty(sort))
			{
				Session["sortII"] = sort;
				ViewBag.Pos = null;
				ViewBag.Details = null;
				Session["PositionInvoice"] = null;
				ViewBag.Editable = null;
				id = null;

			}
			else
			{
				if (Session["sortII"] != null)
					sort = Session["sortII"].ToString();
			}
			ViewBag.ID = sort == "id" ? "id desc" : "id";
			ViewBag.Date = sort == "Date" ? "Date desc" : "Date";
			ViewBag.InvoiceNumber = sort == "Number" ? "Number desc" : "Number";
			ViewBag.Supp = sort == "Supp" ? "Supp desc" : "Supp";
			ViewBag.Ship = sort == "Ship" ? "Ship desc" : "Ship";
			ViewBag.Country = sort == "Country" ? "Country desc" : "Country";
			ViewBag.Type = sort == "Type" ? "Type desc" : "Type";
			var all = db.InvoiceHeader.AsQueryable();
			if (id != null)
			{
				ViewBag.Details = db.InvoiceHeader.FirstOrDefault(x => x.Id == id);
				Session["PositionInvoice"] = id;

				

			}
			else if (id == null && Session["PositionInvoice"] != null && btnsubmit != "Szukaj")
			{
				long idd = Convert.ToInt32(Session["PositionInvoice"]);
				ViewBag.Details = db.InvoiceHeader.FirstOrDefault(x => x.Id== idd);
			}

			if (btnsubmit == "Szukaj")
			{
				ViewBag.Pos = null;
				ViewBag.Details = null;
				Session["PositionInvoice"] = null;
				ViewBag.Editable = null;
			}



			if (SearchString == null)
			{
				if (Session["SearchStringInvoice"] != null)
					SearchString = Session["SearchStringInvoice"].ToString();
			}
			else if (SearchString == "")
			{
				Session["SearchStringInvoice"] = "";
			}

			if (!String.IsNullOrEmpty(SearchString))
			{
				Session["SearchStringInvoice"] = SearchString.Trim();
				all = all.Where(x => x.InvoiceNumber.ToLower().Contains(SearchString.Trim().ToLower()) || x.Voucher.ToLower().Contains(SearchString.Trim().ToLower()) || x.Id.ToString().Contains(SearchString.Trim()));
				
					if (all == null)//nie ma takiej faktury
					{
						TempData["msg"] = "<script>alert('Brak faktury');</script>";
						ViewBag.InvoicesW = null;
					}
					
				
			}

			switch (sort)
			{
				case "id desc":
					all = all.OrderBy(x => x.Id);
					break;
				case "id":
					all = all.OrderByDescending(x => x.Id);
					break;
				case "Date":
					all = all.OrderBy(x => x.InvoiceDate);
					break;
				case "Date desc":
					all = all.OrderByDescending(x => x.InvoiceDate);
					break;
				case "Number":
					all = all.OrderBy(x => x.InvoiceNumber);
					break;
				case "Number desc":
					all = all.OrderByDescending(x => x.InvoiceNumber);
					break;
				case "Supp":
					all = all.OrderBy(x => x.Supplier);
					break;
				case "Supp desc":
					all = all.OrderByDescending(x => x.Supplier);
					break;
			
				case "Ship":
					all = all.OrderBy(x => x.ShipFrom);
					break;
				case "Ship desc":
					all = all.OrderByDescending(x => x.ShipFrom);
					break;
				case "Type":
					all = all.OrderBy(x => x.TypeOfTranosport);
					break;
				case "Type desc":
					all = all.OrderByDescending(x => x.TypeOfTranosport);
					break;
				default:
					all = all.OrderBy(x => x.Id);
					break;
			}
			
			if (Session["PositionInvoice"] == null)
			{
				if (all.Any())
				{
					id = all.First().Id;
					ViewBag.Details = db.InvoiceHeader.FirstOrDefault(x => x.Id == id);
					Session["PositionInvoice"] = id;


				}
			}
			return View(all.ToList());
        }

        // GET: Office/Details/5
        public ActionResult InvoiceDetails(long? id)
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
        public ActionResult InvoiceCreate()
        {
			InvoiceHeaderViewModel IH = new InvoiceHeaderViewModel();
			IH.InvoiceDate = DateTime.Now;
            return View(IH);
        }

        // POST: Office/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceCreate(InvoiceHeaderViewModel invoiceHeaderView)
        {
            if (ModelState.IsValid)
            {
				InvoiceHeader newIH = FromViewToModel(invoiceHeaderView);
                db.InvoiceHeader.Add(newIH);
                db.SaveChanges();
                return RedirectToAction("InvoiceList");
            }

            return View(invoiceHeaderView);
        }

        // GET: Office/Edit/5
        public ActionResult InvoiceEdit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceHeader invoiceHeader = db.InvoiceHeader.Find(id);
			InvoiceHeaderViewModel model = FromModelToView(invoiceHeader);
            if (invoiceHeader == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Office/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceEdit( InvoiceHeaderViewModel invoiceHeader)
        {
            if (ModelState.IsValid)
            {
				InvoiceHeader IH = FromViewToModel(invoiceHeader);
                db.Entry(IH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("InvoiceList");
            }
            return View(invoiceHeader);
        }

     
        [HttpPost, ActionName("InvoiceDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult InvoiceDeleteConfirmed(long id)
        {
            InvoiceHeader invoiceHeader = db.InvoiceHeader.Find(id);
			if (db.InvoicePosition.Any(x => x.InvoiceId == id))
			{
				var positions = db.InvoicePosition.Where(x => x.InvoiceId == id);
				foreach (var del in positions)
				{
					db.InvoicePosition.Remove(del);
				}
			}
			db.InvoiceHeader.Remove(invoiceHeader);
            db.SaveChanges();
			Session["PositionInvoice"] = null;
			TempData["msg"] = "<script>alert('Faktura została usunięta');</script>";
			return RedirectToAction("InvoiceList");
        }
		public ActionResult ExportInvoice(long id)
		{

			var invH= db.InvoiceHeader.Find(id);
			string csv = "InvNumber;InvDate;Voucher;Currency;ExRate;InvoiceValue;Supplier;PositionNumber;PositionItemNumber;PositionHSCode;PositionWeight;PositionPrice;\n"+ InvoiceToString(invH);
			return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv",
				"Invoice" + invH.InvoiceNumber + ".csv");
		}

		public ActionResult ExportAllInvoice()
		{
			ViewBag.Date = DateTime.Now.ToString("yyyy-MM-dd");
			return View();
		}
		[HttpPost]
		public ActionResult ExportAllInvoice(importInvoiceDate model)
		{
			if (model.from > model.to)
			{
				ModelState.AddModelError("", "Data od nie może byc większa od daty do");
				return View(model);
			}

			var inv = db.InvoiceHeader.Where(x => x.InvoiceDate > DbFunctions.TruncateTime(model.from) && x.InvoiceDate < DbFunctions.TruncateTime(model.to)).ToList();

			string csv = "InvNumber;InvDate;Voucher;Currency;ExRate;InvoiceValue;Supplier;PositionNumber;PositionItemNumber;PositionHSCode;PositionWeight;PositionPrice;\n";

			foreach (InvoiceHeader invoice in inv)
			{
				csv += InvoiceToString(invoice);
			}
			return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv",
				"Invoicelist.csv");
		}
		private string InvoiceToString(InvoiceHeader invH)
		{
			StringBuilder csv = new StringBuilder();

			if (db.InvoicePosition.Any(x => x.InvoiceId == invH.Id))
			{
				var positions = db.InvoicePosition.Where(x => x.InvoiceId == invH.Id);
				foreach (var p in positions)
				{
					csv.AppendLine(invH.InvoiceNumber + ";" + invH.InvoiceDate + ";" + invH.Voucher + ";" + invH.Currency + ";" + invH.ExchangeRate + ";" + invH.InvoiceValue + ";" + invH.Supplier + ";" + p.PositionNumber + ";" + p.Itemnumber + ";" + p.HSCode + ";" + p.Weight + ";" + p.Price);
				}
			}
			else
			{
				csv.AppendLine(invH.InvoiceNumber + ";" + invH.InvoiceDate + ";"  + invH.Voucher + ";" + invH.Currency + ";" + invH.ExchangeRate + ";" + invH.InvoiceValue + ";" + invH.Supplier);
			}
			return csv.ToString();
		}

		public ActionResult ImportInvoicePos(long? id, long? idpos)
		{
			if (id == 0 || id == null)
			{
				if (Session["PositionInvoice"] != null)
					id = Convert.ToInt32(Session["PositionInvoice"]);
			}

			ViewBag.Details = db.InvoiceHeader.Find(id);
			
			
			List<InvoicePosition> all = db.InvoicePosition.Where(x => x.InvoiceId == id).ToList();


			if (idpos.HasValue && idpos != 0)
			{
				Session["IvnoicePositionCount"] = db.InvoicePosition.Find(idpos).Quantity;
				Session["InvoicePositionSelected"] = idpos;

			}
			else if (all.Any())
				Session["InvoicePositionSelected"] = all.OrderBy(x => x.PositionNumber).First().Id;





			return View(all);
		}

		public InvoiceHeaderViewModel FromModelToView(InvoiceHeader invoiceHeader)
		{
			InvoiceHeaderViewModel invoiceHeaderView = new InvoiceHeaderViewModel()
			{
				Id = invoiceHeader.Id,
				InvoiceDate = invoiceHeader.InvoiceDate,
				InvoiceNumber = invoiceHeader.InvoiceNumber,
				InvoiceValue = invoiceHeader.InvoiceValue,
				Voucher = invoiceHeader.Voucher,
				Remarks = invoiceHeader.Remarks,
				Currency = invoiceHeader.Currency,
				ExchangeRate = invoiceHeader.ExchangeRate,
				TransportCost = invoiceHeader.TransportCost,
				AdditionalCost = invoiceHeader.AdditionalCost,
				Supplier = invoiceHeader.Supplier,
				ShipFrom = invoiceHeader.ShipFrom,
				TypeOfTranosport = invoiceHeader.TypeOfTranosport
			};
			return invoiceHeaderView;
		}
		public InvoiceHeader FromViewToModel(InvoiceHeaderViewModel invoiceHeaderView)
		{
			InvoiceHeader newIH = new InvoiceHeader()
			{
				Id = invoiceHeaderView.Id,
				InvoiceDate = invoiceHeaderView.InvoiceDate,
				InvoiceNumber = invoiceHeaderView.InvoiceNumber,
				InvoiceValue = invoiceHeaderView.InvoiceValue,
				Voucher = invoiceHeaderView.Voucher,
				Remarks = invoiceHeaderView.Remarks,
				Currency = invoiceHeaderView.Currency,
				ExchangeRate = invoiceHeaderView.ExchangeRate,
				TransportCost = invoiceHeaderView.TransportCost,
				AdditionalCost = invoiceHeaderView.AdditionalCost,
				Supplier = invoiceHeaderView.Supplier,
				ShipFrom = invoiceHeaderView.ShipFrom,
				TypeOfTranosport = invoiceHeaderView.TypeOfTranosport
			};
			return newIH;
		}



    }

}
