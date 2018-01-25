using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mortgage_Calculator.Models
{
    public enum extraPaymentType
    {
        Monthly,
        Yearly
    };

    public class CalculationView
    {
        public double mortgageAmount { get; set; }
        public int amortizationPeriod { get; set; }
        public double downpayment { get; set; }
        public double interestRate { get; set; }

        public double extraPayment { get; set; }
       
        public extraPaymentType extraPaymentOption
        {
            get;
            set;
        }

    }
}