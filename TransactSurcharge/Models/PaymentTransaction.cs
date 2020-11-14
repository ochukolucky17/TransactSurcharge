using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransactSurcharge.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        [Display(Name = "Ref No.")]
        public string TransRef { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Payment Type")]
        public subType SubscriptionType { get; set; }
        [Display(Name = "Trans. Date")]
        public DateTime SubscriptionDate { get; set; }
        [Display(Name = "Payment Amount")]
        public double PaymentAmount { get; set; }
        [Display(Name = "Transfer Amount")]
        public double TransferAmount { get; set; }
        [Display(Name = "Charge")]
        public double Charge { get; set; }
        [Display(Name = "Debit Amount")]
        public double DebitAmount { get; set; }
        public DateTime TimeStamp { get; set; }
        
    }

     public enum subType
    {
        Starter,
        Premium,
        Enterprise
    }
}