using Mortgage_Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mortgage_Calculator.Utility
{
    public class amortChart
    {
       
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
        

            //convert interest rate to decimal form
            double dblInterestToDecimal = cv.interestRate / 100;
            //calculate interest
            double dblConvertInterest = cv.interestRate / 100 * 1 / 12;

            //calculate the total number of payments (n * 12)
            int intYears = (int)cv.amortizationPeriod;     //years
                                                   //int intNumOfPayments = intYears * 12;    //In Years (with Month wise)
            int intNumOfPayments = intYears *12;  // Only number of installments basis like (2 Installment or 3 Installment)

            if (cv.downpayment != 0)
            {
               cv.mortgageAmount = cv.mortgageAmount - cv.downpayment;
            }
            amortTable.MonthlyPayAmount = calcPaymentAmount(cv.mortgageAmount, intNumOfPayments, cv.interestRate);

            double interest; //= loanPrincipal * dblConvertInterest;
            double DeductedPrincipal; //= amortTable.MonthlyPayAmount - amortTable.interest;       // new principle after each month amount payment
            double decNewBalance; // = loanPrincipal - DeductedPrincipal;
           List <AmortTable> amortList = new List<AmortTable>();

            decNewBalance = cv.mortgageAmount;
            int pNo = 1;

            while (pNo <= intNumOfPayments)
            {

                AmortTable am = new AmortTable();
                am.PaymentNo = pNo;
                am.MonthlyPayAmount = Math.Round(amortTable.MonthlyPayAmount,3);
                am.interest = Math.Round(decNewBalance * dblConvertInterest,3);
                am.DeductedPrincipal = Math.Round(amortTable.MonthlyPayAmount - am.interest,3);
                am.balance = Math.Round(decNewBalance - am.DeductedPrincipal,3);
                if (am.balance < 1)
                {
                    am.balance = 0.00;
                }
                System.Diagnostics.Debug.WriteLine("interest Paid  " + am.interest+ "deducted p  " + am.DeductedPrincipal + "new balance  " + am.balance);
                interest = am.interest;
                DeductedPrincipal = am.DeductedPrincipal;
                decNewBalance = am.balance;
             
                
                amortList.Add(am);

                pNo += 1;

            }
       

            return amortList;
        }
    }
}