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
        public Bank yesBank = new Bank()
        {
            bankId = "yes100223" ,
            sameRtgs = 0,
            sameImps = 5,
            otherRtgs = 2,
            otherImps = 6,
            currency = "INR",
            currencyExchangeRates = "1"
        };

        public List<Transactions> transactions = new List<Transactions>();     

        public bool SetCurrency(string currency, string currencyExchangeRates)
        {
            if(currency!=null && currencyExchangeRates != null)
            {
                yesBank.currency = currency;
                yesBank.currencyExchangeRates = currencyExchangeRates;
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
                yesBank.otherRtgs = otherRtgs;
                yesBank.sameRtgs = sameRtgs;
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
                yesBank.otherImps = otherImps;
                yesBank.sameImps = sameImps;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateTransactionNumber(string txnNum)
        {
            if (transactions.Any(txn => txn.transectionNum == txnNum))
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
