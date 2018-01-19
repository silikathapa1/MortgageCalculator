using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mortgage_Calculator.Models
{
    public class AmortTable
    {
             

        public int PaymentNo { get; set; }

        public double MonthlyPayAmount { get; set; }
        public double DeductedPrincipal { get; set; }
        public double interest { get; set; }
      
        public double balance { get; set; }

      
    }
}