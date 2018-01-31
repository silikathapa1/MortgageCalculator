using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public double mortgageAmount { get; set; }
        [Required]
        public int amortizationPeriod { get; set; }
        public double downpayment { get; set; }
        [Required]
        public double interestRate { get; set; }

        public double extraPayment { get; set; }
       
        public extraPaymentType extraPaymentOption
        {
            get;
            set;
        }

    }
}