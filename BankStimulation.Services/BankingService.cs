using BankStimulation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankStimulation.Services
{
    public class BankingService
    {
        static public Bank yesBank = new Bank()
        {
            BankId = "yes100223" ,
            SameRtgsCharges = 0,
            SameImpsCharges = 5,
            OtherRtgsCharges = 2,
            OtherImpsCharges = 6,
            DefaultCurrency = "INR",
            CurrencyExchangeRates = "1"
        };

        static public List<Model.Transaction> Transactions = new List<Model.Transaction>();     

        public bool SetCurrency(string currency, string currencyExchangeRates)
        {
            if(currency!=null && currencyExchangeRates != null)
            {
                yesBank.DefaultCurrency = currency;
                yesBank.CurrencyExchangeRates = currencyExchangeRates;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetRtgs(double sameRtgs, double otherRtgs)
        {
            if (sameRtgs != null && otherRtgs != null)
            {
                yesBank.OtherRtgsCharges = otherRtgs;
                yesBank.SameRtgsCharges = sameRtgs;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetImps(double sameImps, double otherImps)
        {
            if (sameImps != null && otherImps != null)
            {
                yesBank.OtherImpsCharges = otherImps;
                yesBank.SameImpsCharges = sameImps;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateTransactionNumber(string txnNum)
        {
            if (Transactions.Any(txn => txn.TransectionNum == txnNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
