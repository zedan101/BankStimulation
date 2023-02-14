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
        string loggedInUserAccNum;

        public bool DepositeFund(double amount)
        {
            var accHldr = GlobalDataStorage. AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber== loggedInUserAccNum);
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
            var accHldr = GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum);
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

        public bool setTransection(double Charges)
        {

        }

        public bool TransferFundImps(double amount, string recieverAccNum, string recieverBankId)
        {
            var accHldr = GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum);
            if (accHldr.AccountBalance >= amount && amount != 0)
            {
                Transaction newTransaction = new Transaction();
                newTransaction.SendersBankId = accHldr.BankId;
                newTransaction.RecieversBankId = recieverBankId;
                newTransaction.RecieversAccNum = recieverAccNum;
                newTransaction.SenderAccNum = accHldr.AccNumber;
                newTransaction.TransactionAmount = amount;
                newTransaction.TransactionType = "IMPS";
                newTransaction.TransectionNum = "TXN" + accHldr.BankId + accHldr.AccNumber + DateTime.Now.ToString("ddMMyyyy");
                newTransaction.TransactionStatus = "Success";
                if (recieverBankId == GlobalDataStorage.yesBank.BankId)
                {
                    accHldr.AccountBalance -= (amount + amount * GlobalDataStorage.yesBank.SameImpsCharges / 100);
                  
                }
                else
                {
                    accHldr.AccountBalance -= (amount + amount * GlobalDataStorage.yesBank.OtherImpsCharges / 100);
                    
                }
                GlobalDataStorage.Transactions.Add(newTransaction);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TransferFundsRtgs(double amount , string recieverAccNum,string recieverBankId)
        {
            var accHldr = GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum);
            if (accHldr.AccountBalance >= amount && amount != 0)
            {

                Transaction newTransaction = new Transaction();
                newTransaction.SendersBankId = accHldr.BankId;
                newTransaction.RecieversBankId = recieverBankId;
                newTransaction.RecieversAccNum = recieverAccNum;
                newTransaction.SenderAccNum = accHldr.AccNumber;
                newTransaction.TransactionAmount = amount;
                newTransaction.TransactionType = "RTGS";
                newTransaction.TransectionNum = "TXN" + accHldr.BankId + accHldr.AccNumber + DateTime.Now.ToString("ddMMyyyy");
                newTransaction.TransactionStatus = "Pending";
                if (recieverBankId == GlobalDataStorage.yesBank.BankId)
                {
                    accHldr.AccountBalance -= (amount + amount * GlobalDataStorage.yesBank.SameRtgsCharges/100);
                    
                }
                else
                {
                    accHldr.AccountBalance -= (amount + amount * GlobalDataStorage.yesBank.OtherRtgsCharges / 100);
                 
                }
                GlobalDataStorage.Transactions.Add(newTransaction);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Transaction> ViewTransectionHistory()
        {
            
            var txn = GlobalDataStorage.Transactions.Where(txn=> txn.RecieversAccNum == loggedInUserAccNum || txn.SenderAccNum == loggedInUserAccNum).ToList();
            return txn;
        }

        public bool SetAccHolderData(AccountHolder accountHolder)
        {
            if(accountHolder!=null)
            {
                GlobalDataStorage.AccHolder.Add(accountHolder);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ValidateAccNum(string accNum)
        {
            if (GlobalDataStorage.AccHolder.Any(accHldr => accHldr.AccNumber == accNum))
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
            if (GlobalDataStorage.AccHolder.Any(accHldr => accHldr.AccPin == accPin))
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
