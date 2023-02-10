using BankStimulation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStimulation.Services
{
    public class AccountHolderService
    {
        public List<AccountHolder> accHolder = new List<AccountHolder>();
        string loggedInUserAccNum;
        BankingService bankService = new BankingService();

        public bool DepositeFund(double amount)
        {
            var accHldr = accHolder.FirstOrDefault(accHldr => accHldr.accNumber== loggedInUserAccNum);
            if(amount != 0 )
            {
                accHldr.AccountBalance += amount;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool WithdrawFund(double amount)
        {
            var accHldr = accHolder.FirstOrDefault(accHldr => accHldr.accNumber == loggedInUserAccNum);
            if(accHldr.AccountBalance >= amount && amount != 0 )
            {
                accHldr.AccountBalance -= amount;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void TransferFundImps(double amount, string recieverAccNum, string recieverBankId)
        {
            var accHldr = accHolder.FirstOrDefault(accHldr => accHldr.accNumber == loggedInUserAccNum);
            if (recieverBankId == bankService.yesBank.bankId)
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transactions newTransaction = new Transactions();
                    newTransaction.sendersBankId = accHldr.bankId;
                    newTransaction.recieversBankId = recieverBankId;
                    newTransaction.recieversAccNum = recieverAccNum;
                    newTransaction.senderAccNum = accHldr.accNumber;
                    newTransaction.transactionAmount = amount;
                    newTransaction.transectionNum = "TXN" + accHldr.bankId + accHldr.accNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * bankService.yesBank.sameImps / 100);
                }
            }
            else
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transactions newTransaction = new Transactions();
                    newTransaction.sendersBankId = accHldr.bankId;
                    newTransaction.recieversBankId = recieverBankId;
                    newTransaction.recieversAccNum = recieverAccNum;
                    newTransaction.senderAccNum = accHldr.accNumber;
                    newTransaction.transactionAmount = amount;
                    newTransaction.transectionNum = "TXN" + accHldr.bankId + accHldr.accNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * bankService.yesBank.otherImps / 100);
                }
            }
        }

        public void TransferFundsRtgs(double amount , string recieverAccNum,string recieverBankId)
        {
            var accHldr = accHolder.FirstOrDefault(accHldr => accHldr.accNumber == loggedInUserAccNum);
            if (recieverBankId == bankService.yesBank.bankId)
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transactions newTransaction = new Transactions();
                    newTransaction.sendersBankId = accHldr.bankId;
                    newTransaction.recieversBankId = recieverBankId;
                    newTransaction.recieversAccNum = recieverAccNum;
                    newTransaction.senderAccNum = accHldr.accNumber;
                    newTransaction.transactionAmount = amount;
                    newTransaction.transectionNum = "TXN" + accHldr.bankId + accHldr.accNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * bankService.yesBank.sameRtgs/100);
                }
            }
            else
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transactions newTransaction = new Transactions();
                    newTransaction.sendersBankId = accHldr.bankId;
                    newTransaction.recieversBankId = recieverBankId;
                    newTransaction.recieversAccNum = recieverAccNum;
                    newTransaction.senderAccNum = accHldr.accNumber;
                    newTransaction.transactionAmount = amount;
                    newTransaction.transectionNum = "TXN" + accHldr.bankId + accHldr.accNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * bankService.yesBank.otherRtgs / 100);
                }           
            }
        }

        public List<Transactions> ViewTransectionHistory()
        {
            List<Transactions> txn= new List<Transactions>();
            txn = bankService.transactions.Where(txn=> txn.recieversAccNum == loggedInUserAccNum || txn.senderAccNum == loggedInUserAccNum).ToList();
            return txn;
        }

        public bool SetAccHolderData(AccountHolder accountHolder)
        {
            if(accountHolder!=null)
            {
                accHolder.Add(accountHolder);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ValidateAccNum(string accNum)
        {
            if (accHolder.Any(accHldr => accHldr.accNumber == accNum))
            {
                loggedInUserAccNum= accNum;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateAccPin(string accPin)
        {
            if (accHolder.Any(accHldr => accHldr.AccPin == accPin))
            {
                return true;
            }
            else
            {
                loggedInUserAccNum= "";
                return false;
            }
        }
    }
}
