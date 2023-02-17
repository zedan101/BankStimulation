using BankStimulation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStimulation.Services
{
    public class CommonService
    {
        public Accounts DisplayAccountDetails(string accNum , string bankId)
        {
            return GlobalDataStorage.AccHolder.FirstOrDefault(e => e.AccNumber == accNum && e.BankId==bankId);
        }

        public List<Transaction> ViewTransectionHistory(string accNumber, string bankId)
        {
            List<Transaction> txn = new List<Transaction>();
            txn = GlobalDataStorage.Transactions.Where(txn => (txn.SenderAccNum == accNumber || txn.RecieversAccNum == accNumber) && (txn.SendersBankId == bankId || txn.RecieversBankId == bankId)).ToList();
            return txn;
        }


    }
}
