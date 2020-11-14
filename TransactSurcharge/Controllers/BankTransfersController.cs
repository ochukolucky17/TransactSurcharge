using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransactSurcharge.Models;

namespace TransactSurcharge.Controllers
{
    public class BankTransfersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankTransfers
        public ActionResult Index()
        {
            return View(db.BankTransfers.ToList());
        }

        // GET: BankTransfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransfer bankTransfer = db.BankTransfers.Find(id);
            if (bankTransfer == null)
            {
                return HttpNotFound();
            }
            return View(bankTransfer);
        }

        // GET: BankTransfers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BankTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TransRef,BeneficiaryName,BankName,AccountNumber,Narration,Amount,Charge,DebitAmount,TimeStamp")] BankTransfer bankTransfer)
        {
            var myAmt = bankTransfer.Amount;
            string file = Server.MapPath("~/Content/fees.config.json");
            bankTransfer.TimeStamp = DateTime.Now;
            bankTransfer.TransRef = RandomString(8);
            string Json = System.IO.File.ReadAllText(file);
            var kk = (JsonConvert.DeserializeObject<IDictionary<string, object>>(Json))["fees"];
            var feemanager = JsonConvert.DeserializeObject<ObservableCollection<Fees>>(kk.ToString());
            var feesort = feemanager.Where(x => x.minAmount <= myAmt && x.maxAmount >= myAmt).FirstOrDefault();
            var myCharge = feesort.feeAmount;
            bankTransfer.Charge = myCharge;
            bankTransfer.DebitAmount = bankTransfer.Amount + bankTransfer.Charge;
            if (ModelState.IsValid)
            {
                db.BankTransfers.Add(bankTransfer);
                db.SaveChanges();
                int Id = bankTransfer.Id;
                return RedirectToAction("Details", new { Id });
            }

            return View(bankTransfer);
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: BankTransfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransfer bankTransfer = db.BankTransfers.Find(id);
            if (bankTransfer == null)
            {
                return HttpNotFound();
            }
            return View(bankTransfer);
        }

        // POST: BankTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TransRef,BeneficiaryName,BankName,AccountNumber,Narration,Amount,Charge,DebitAmount,TimeStamp")] BankTransfer bankTransfer)
        {
            var myAmt = bankTransfer.Amount;
            string file = Server.MapPath("~/Content/fees.config.json");
            bankTransfer.TimeStamp = DateTime.Now;
            bankTransfer.TransRef = RandomString(8);
            string Json = System.IO.File.ReadAllText(file);
            var kk = (JsonConvert.DeserializeObject<IDictionary<string, object>>(Json))["fees"];
            var feemanager = JsonConvert.DeserializeObject<ObservableCollection<Fees>>(kk.ToString());
            var feesort = feemanager.Where(x => x.minAmount <= myAmt && x.maxAmount >= myAmt).FirstOrDefault();
            var myCharge = feesort.feeAmount;
            bankTransfer.Charge = myCharge;
            bankTransfer.DebitAmount = bankTransfer.Amount + bankTransfer.Charge;
            if (ModelState.IsValid)
            {
                db.Entry(bankTransfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bankTransfer);
        }

        // GET: BankTransfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankTransfer bankTransfer = db.BankTransfers.Find(id);
            if (bankTransfer == null)
            {
                return HttpNotFound();
            }
            return View(bankTransfer);
        }

        // POST: BankTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankTransfer bankTransfer = db.BankTransfers.Find(id);
            db.BankTransfers.Remove(bankTransfer);
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
