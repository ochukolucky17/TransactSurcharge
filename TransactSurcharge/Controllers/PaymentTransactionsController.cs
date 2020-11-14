using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransactSurcharge.Models;
using System.Web.Script.Serialization; 
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace TransactSurcharge.Controllers
{
    public class PaymentTransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentTransactions
        public ActionResult Index()
        {
            return View(db.PaymentTransactions.ToList());
        }

        // GET: PaymentTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTransaction paymentTransaction = db.PaymentTransactions.Find(id);
            if (paymentTransaction == null)
            {
                return HttpNotFound();
            }
            return View(paymentTransaction);
        }

        // GET: PaymentTransactions/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PaymentTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TransRef,CustomerName,SubscriptionType,SubscriptionDate,PaymentAmount,TransferAmount,Charge,DebitAmount,TimeStamp")] PaymentTransaction paymentTransaction)
        {
            var myAmt = paymentTransaction.PaymentAmount;
            string file = Server.MapPath("~/Content/fees.config.json");
            paymentTransaction.TimeStamp = DateTime.Now;
            paymentTransaction.TransRef = RandomString(8);
            string Json = System.IO.File.ReadAllText(file);
            var kk = (JsonConvert.DeserializeObject<IDictionary<string, object>>(Json))["fees"];
            var feemanager = JsonConvert.DeserializeObject<ObservableCollection<Fees>>(kk.ToString());
            var feesort = feemanager.Where(x => x.minAmount <= myAmt && x.maxAmount >= myAmt).FirstOrDefault();
            var myCharge = feesort.feeAmount;
            paymentTransaction.TransferAmount = myAmt - myCharge;
            paymentTransaction.Charge = myCharge;
            paymentTransaction.DebitAmount = paymentTransaction.TransferAmount + paymentTransaction.Charge;

            if (ModelState.IsValid)
            {

                db.PaymentTransactions.Add(paymentTransaction);
                db.SaveChanges();
                int Id = paymentTransaction.Id;
                return RedirectToAction("Details", new { Id });
            }

            return View(paymentTransaction);
        }

        // GET: PaymentTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTransaction paymentTransaction = db.PaymentTransactions.Find(id);
            if (paymentTransaction == null)
            {
                return HttpNotFound();
            }
            return View(paymentTransaction);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // POST: PaymentTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TransRef,CustomerName,SubscriptionType,SubscriptionDate,PaymentAmount,TransferAmount,Charge,DebitAmount,TimeStamp")] PaymentTransaction paymentTransaction)
        {
            var myAmt = paymentTransaction.PaymentAmount;
            string file = Server.MapPath("~/Content/fees.config.json");
            paymentTransaction.TimeStamp = DateTime.Now;
            paymentTransaction.TransRef = RandomString(8);
            string Json = System.IO.File.ReadAllText(file);
            var kk = (JsonConvert.DeserializeObject<IDictionary<string, object>>(Json))["fees"];
            var feemanager = JsonConvert.DeserializeObject<ObservableCollection<Fees>>(kk.ToString());
            var feesort = feemanager.Where(x => x.minAmount <= myAmt && x.maxAmount >= myAmt).FirstOrDefault();
            var myCharge = feesort.feeAmount;
            paymentTransaction.TransferAmount = myAmt - myCharge;
            paymentTransaction.Charge = myCharge;
            paymentTransaction.DebitAmount = paymentTransaction.TransferAmount + paymentTransaction.Charge;

            if (ModelState.IsValid)
            {
                db.Entry(paymentTransaction).State = EntityState.Modified;
                db.SaveChanges();
                int Id = paymentTransaction.Id;
                return RedirectToAction("Details", new { Id });
            }
            return View(paymentTransaction);
        }

        // GET: PaymentTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentTransaction paymentTransaction = db.PaymentTransactions.Find(id);
            if (paymentTransaction == null)
            {
                return HttpNotFound();
            }
            return View(paymentTransaction);
        }

        // POST: PaymentTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentTransaction paymentTransaction = db.PaymentTransactions.Find(id);
            db.PaymentTransactions.Remove(paymentTransaction);
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
