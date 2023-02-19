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
            return GlobalDataStorage.Banks.First(bank => bank.BankId == bankId).AccHolder.FirstOrDefault(e => e.AccNumber == accNum);
        }

        public List<Transaction> ViewTransectionHistory(string accNumber, string bankId)
        {
            List<Transaction> txn = new List<Transaction>();
            return GlobalDataStorage.Banks.First(bank => bank.BankId == bankId).Transactions.Where(txn => (txn.SenderAccNum == accNumber || txn.RecieversAccNum == accNumber)).ToList();
            
        }


    }
}
