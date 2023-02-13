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
        static public List<AccountHolder> AccHolder = new List<AccountHolder>();
        string loggedInUserAccNum;

        public bool DepositeFund(double amount)
        {
            var accHldr = AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber== loggedInUserAccNum);
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
            var accHldr = AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum);
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

        public bool TransferFundImps(double amount, string recieverAccNum, string recieverBankId)
        {
            var accHldr = AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum);
            if (recieverBankId == BankingService.yesBank.BankId)
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transaction newTransaction = new Transaction();
                    newTransaction.SendersBankId = accHldr.BankId;
                    newTransaction.RecieversBankId = recieverBankId;
                    newTransaction.RecieversAccNum = recieverAccNum;
                    newTransaction.SenderAccNum = accHldr.AccNumber;
                    newTransaction.TransactionAmount = amount;
                    newTransaction.TransectionNum = "TXN" + accHldr.BankId + accHldr.AccNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * BankingService.yesBank.SameImpsCharges / 100);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transaction newTransaction = new Transaction();
                    newTransaction.SendersBankId = accHldr.BankId;
                    newTransaction.RecieversBankId = recieverBankId;
                    newTransaction.RecieversAccNum = recieverAccNum;
                    newTransaction.SenderAccNum = accHldr.AccNumber;
                    newTransaction.TransactionAmount = amount;
                    newTransaction.TransectionNum = "TXN" + accHldr.BankId + accHldr.AccNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * BankingService.yesBank.OtherImpsCharges / 100);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool TransferFundsRtgs(double amount , string recieverAccNum,string recieverBankId)
        {
            var accHldr = AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum);
            if (recieverBankId == BankingService.yesBank.BankId)
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transaction newTransaction = new Transaction();
                    newTransaction.SendersBankId = accHldr.BankId;
                    newTransaction.RecieversBankId = recieverBankId;
                    newTransaction.RecieversAccNum = recieverAccNum;
                    newTransaction.SenderAccNum = accHldr.AccNumber;
                    newTransaction.TransactionAmount = amount;
                    newTransaction.TransectionNum = "TXN" + accHldr.BankId + accHldr.AccNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * BankingService.yesBank.SameRtgsCharges/100);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (accHldr.AccountBalance >= amount && amount != 0)
                {
                    Transaction newTransaction = new Transaction();
                    newTransaction.SendersBankId = accHldr.BankId;
                    newTransaction.RecieversBankId = recieverBankId;
                    newTransaction.RecieversAccNum = recieverAccNum;
                    newTransaction.SenderAccNum = accHldr.AccNumber;
                    newTransaction.TransactionAmount = amount;
                    newTransaction.TransectionNum = "TXN" + accHldr.BankId + accHldr.AccNumber + DateTime.Now.ToString("ddMMyyyy");
                    accHldr.AccountBalance -= (amount + amount * BankingService.yesBank.OtherRtgsCharges / 100);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Transaction> ViewTransectionHistory()
        {
            
            var txn = BankingService.Transactions.Where(txn=> txn.RecieversAccNum == loggedInUserAccNum || txn.SenderAccNum == loggedInUserAccNum).ToList();
            return txn;
        }

        public bool SetAccHolderData(AccountHolder accountHolder)
        {
            if(accountHolder!=null)
            {
                AccHolder.Add(accountHolder);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ValidateAccNum(string accNum)
        {
            if (AccHolder.Any(accHldr => accHldr.AccNumber == accNum))
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
            if (AccHolder.Any(accHldr => accHldr.AccPin == accPin))
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
