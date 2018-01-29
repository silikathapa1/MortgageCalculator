using Mortgage_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mortgage_Calculator.Utility
{
    public class amortChart
    {

        //Monthly installment amount calculation
        protected double calcPaymentAmount(double principalAmt, double noOfPayment, double iRate)
        {
            double monthlyInterest;
            double intRate = (iRate / 100) / 12;
            monthlyInterest = (principalAmt * (Math.Pow((1 + intRate), noOfPayment)) * intRate / (Math.Pow((1 + intRate), noOfPayment) - 1));
            return Convert.ToDouble(monthlyInterest);
        }

        public List<AmortTable> calcAmortTable(CalculationView cv)
        {
            AmortTable amortTable = new AmortTable();
                   
            //calculate interest
            double dblConvertInterest = cv.interestRate / 100 * 1 / 12;

            //calculate the total number of payments (n * 12)
            int intYears = (int)cv.amortizationPeriod;     //for years calculation
                                                  
            int intNumOfPayments = intYears *12;  // for monthly calculation

            if (cv.downpayment != 0)
            {
               cv.mortgageAmount = cv.mortgageAmount - cv.downpayment;
            }
            amortTable.MonthlyPayAmount = calcPaymentAmount(cv.mortgageAmount, intNumOfPayments, cv.interestRate);

            double interest; 
            double DeductedPrincipal;       // new principle after each month amount payment
            double decNewBalance; 
           List <AmortTable> amortList = new List<AmortTable>();

            decNewBalance = cv.mortgageAmount;
            int pNo = 1;

            //extra payment calculation
            if (cv.extraPayment != 0)
            {
                if (string.Equals(cv.extraPaymentOption.ToString(), "Monthly", StringComparison.OrdinalIgnoreCase))
                {
                   amortTable.MonthlyPayAmount += cv.extraPayment;
                }else if (string.Equals(cv.extraPaymentOption.ToString(), "Yearly", StringComparison.OrdinalIgnoreCase))
                {
                    cv.extraPayment = cv.extraPayment / 12;
                    amortTable.MonthlyPayAmount += cv.extraPayment;
                }
            }
        
           for (int x=1; x<=intNumOfPayments; x++)
            {

                AmortTable am = new AmortTable();
               
                am.PaymentNo = x;
                am.MonthlyPayAmount = Math.Round(amortTable.MonthlyPayAmount,3);
              
                //Adjustment for final installment payment
                if (decNewBalance < am.MonthlyPayAmount)
                {
                    am.MonthlyPayAmount = decNewBalance;
                }

                am.interest = Math.Round(decNewBalance * dblConvertInterest,3);
           
                am.DeductedPrincipal = Math.Round(am.MonthlyPayAmount - am.interest,3);
                //Balancing balance for final transaction.
                if (decNewBalance == am.MonthlyPayAmount)
                {
                    am.balance = 0;
                }
                else
                {
                    am.balance = Math.Round(decNewBalance - am.DeductedPrincipal, 3);
                }
                am.extraPayment = cv.extraPayment;
            
                amortList.Add(am);
                if (am.balance < 1)
                {
                    break;
                }

                DeductedPrincipal = am.DeductedPrincipal;
                decNewBalance = am.balance;
            }
       
            return amortList;
        }
    }
}