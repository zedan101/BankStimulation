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

        public bool SetRtgs(double sameRtgs, double otherRtgs)
        {
                GlobalDataStorage.yesBank.OtherRtgsCharges = otherRtgs;
                GlobalDataStorage.yesBank.SameRtgsCharges = sameRtgs;
                return true;
            
        }

        public bool SetImps(double sameImps, double otherImps)
        {
                GlobalDataStorage.yesBank.OtherImpsCharges = otherImps;
                GlobalDataStorage.yesBank.SameImpsCharges = sameImps;
                return true;
        
        }

        public bool ValidateTransactionNumber(string txnNum)
        {
            if (GlobalDataStorage.Transactions.Any(txn => txn.TransectionNum == txnNum))
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
