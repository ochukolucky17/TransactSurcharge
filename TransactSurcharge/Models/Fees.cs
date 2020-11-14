using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactSurcharge.Models
{
    
    public class Fees
    {
       
        public double minAmount { get; set; }
        public double maxAmount { get; set; }
        public double feeAmount { get; set; }
    }
}