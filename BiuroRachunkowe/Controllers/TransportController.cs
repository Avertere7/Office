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
    public class TransportController : Controller
    {
        private OfficeModel db = new OfficeModel();

        // GET: Transport
        public ActionResult SADList( long? id, string sort,string SearchString)
        {
			ViewBag.idd = sort == "F03002" ? "F03002 desc" : "F03002";
			ViewBag.nr = sort == "F03003" ? "F03003 desc" : "F03003";
			ViewBag.date = sort == "F03004" ? "F03004 desc" : "F03004";


			var all = db.SAD.AsQueryable();




			switch (sort)
			{
				case "F03002":
					all = all.OrderBy(x => x.Id);
					break;
				case "F03003":
					all = all.OrderBy(x => x.SadNumber);
					break;
				case "F03003 desc":
					all = all.OrderByDescending(x => x.SadNumber);
					break;
				case "F03004":
					all = all.OrderBy(x => x.SadDate);
					break;
				case "F03002 desc":
					all = all.OrderByDescending(x => x.Id);
					break;
				case "F03004 desc":
					all = all.OrderByDescending(x => x.SadDate);
					break;
				default:
					all = all.OrderByDescending(x => x.SadDate);
					break;
			}


			if (SearchString == null)
			{
				if (Session["SearchSADIS"] != null)
					SearchString = Session["SearchSADIS"].ToString();
			}
			else if (SearchString == "")
			{
				if (Session["SearchSADIS"] != null)
				{
					if (Session["SearchSADIS"].ToString() != SearchString)
					{
						Session["SelectedIdIS"] = null;
						id = null;
					}
				}
				else
				{
					Session["SelectedIdIS"] = null;
					id = null;
				}
				Session["SearchSADIS"] = "";
			}

			if (!String.IsNullOrEmpty(SearchString))
			{

				if (Session["SearchSADIS"] != null)
				{
					if (Session["SearchSADIS"].ToString() != SearchString)
					{
						Session["SelectedIdIS"] = null;
						id = null;
					}
				}
				else
				{
					Session["SelectedIdIS"] = null;
					id = null;
				}
				Session["SearchSADIS"] = SearchString;
				string SearchString2 = SearchString;
				if (SearchString.Length > 20)
				{

					if (SearchString.Contains("/"))
					{
						SearchString2 = SearchString2.Replace("/", string.Empty);
					}
					else
					{

						SearchString2 = SearchString2.Insert(3, "/");
						SearchString2 = SearchString2.Insert(10, "/");
						SearchString2 = SearchString2.Insert(13, "/");
						SearchString2 = SearchString2.Insert(20, "/");
					}
				}
				all = all.Where(x => x.Id.ToString().Contains(SearchString.Trim()) || x.SadNumber.ToLower().Contains(SearchString.ToLower().Trim()) || x.SadNumber.ToLower().Contains(SearchString2.ToLower().Trim()) || x.SadDate.ToString().Contains(SearchString.Trim()));
			}

			if (Session["SelectedIdIS"] != null)
			{
				long jd = Convert.ToInt32(Session["SelectedIdIS"]);
				if (!all.Any(x => x.Id== jd))
				{
					Session["SelectedIdIS"] = null;
					id = null;
				}
			}
			if (!all.Any())
			{
				Session["SelectedIdIS"] = null;
				id = null;
			}
			if (id != null)
			{
				Session["SelectedIdIS"] = id;
				ViewBag.Details = db.SAD.Where(x => x.Id == id).ToList();
			}
			else if (Session["SelectedIdIS"] != null)
			{
				id = Convert.ToInt32(Session["SelectedIdIS"]);
				ViewBag.Details = db.SAD.Where(x => x.Id == id).ToList();
			}
			else
			{
				if (all.Any())
				{
					id = all.First().Id;
					Session["SelectedIdIS"] = id;
					ViewBag.Details = db.SAD.Where(x => x.Id == id).ToList();
				}
			}


			return View(all.ToList());
        }

        // GET: Transport/Details/5
        public ActionResult SADDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAD sAD = db.SAD.Find(id);
            if (sAD == null)
            {
                return HttpNotFound();
            }
            return View(sAD);
        }

        // GET: Transport/Create
        public ActionResult SADCreate()
        {
            return View();
        }

        // POST: Transport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SADCreate([Bind(Include = "Id,SadNumber,SadDate,Currency,ExchangeRate,SadStatus,Paid,PaidDate,Remarks")] SAD sAD)
        {
            if (ModelState.IsValid)
            {
				if (sAD.SadNumber.Contains("/"))
					sAD.SadNumber = sAD.SadNumber.Replace("/", String.Empty);
				db.SAD.Add(sAD);
                db.SaveChanges();
                return RedirectToAction("SADList",new {id=sAD.Id });
            }

            return View(sAD);
        }

        // GET: Transport/Edit/5
        public ActionResult SADEdit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAD sAD = db.SAD.Find(id);
            if (sAD == null)
            {
                return HttpNotFound();
            }
            return View(sAD);
        }

        // POST: Transport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SADEdit([Bind(Include = "Id,SadNumber,SadDate,Currency,ExchangeRate,SadStatus,Paid,PaidDate,Remarks")] SAD sAD)
        {
            if (ModelState.IsValid)
            {
				if (sAD.SadNumber.Contains("/"))
					sAD.SadNumber = sAD.SadNumber.Replace("/", String.Empty);
                db.Entry(sAD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SADList",new { id = sAD.Id });
            }
            return View(sAD);
        }

      

        // POST: Transport/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SADDelete(long id)
        {
            SAD sAD = db.SAD.Find(id);
            db.SAD.Remove(sAD);
            db.SaveChanges();
            return RedirectToAction("SADList");
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
