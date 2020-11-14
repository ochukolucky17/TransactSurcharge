using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransactSurcharge.Models
{
    public class BankTransfer
    {
        public int Id { get; set; }
        [Display(Name = "Ref No.")]
        public string TransRef { get; set; }
        [Display(Name = "Beneficiary Name")]
        public string BeneficiaryName { get; set; }
        [Display(Name = "Beneficiary Bank")]
        public bnkName BankName { get; set; }
        [Display(Name = "Beneficiary Account Number")]
        public string AccountNumber { get; set; }
        [Display(Name = "Narration")]
        public string Narration { get; set; }
        [Display(Name = "Transfer Amount")]
        public double Amount { get; set; }
        [Display(Name = "Charge")]
        public double Charge { get; set; }
        [Display(Name = "Debit Amount")]
        public double DebitAmount { get; set; }
        public DateTime TimeStamp { get; set; }
        
    }

     public enum bnkName
    {
        
        [Description("Access Bank Plc")]
        Access,
        [Description("Fidelity Bank Plc")]
        Fidelity,
        [Description("First City Monument Bank Limited")]
        FCMB,
        [Description("First Bank of Nigeria Limited")]
        FBN,
        [Description("Guaranty Trust Bank Plc")]
        GTB,
        [Description("Union Bank of Nigeria Plc")]
        Union,
        [Description("United Bank for Africa Plc")]
        UBA,
        [Description("Zenith Bank Plc")]
        Zenith,
        [Description("Citibank Nigeria Limited")]
        Citibank,
[Description("Ecobank Nigeria Plc")]
Ecobank,
[Description("Heritage Banking Company Limited")]
Heritage,
[Description("Keystone Bank Limited")]
Keystone,
[Description("Polaris Bank Limited")]
Polaris,
        [Description("Stanbic IBTC Bank Plc")]
        Stanbic,
[Description("Standard Chartered")]
StandardChartered,
[Description("Sterling Bank Plc")]
Sterling,
[Description("Titan Trust Bank Limited")]
Titan,
[Description("Unity Bank Plc")]
Unity,
[Description("Wema Bank Plc")]
Wema
    }
}