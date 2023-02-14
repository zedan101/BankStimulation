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

        public bool TransferFunds(double amount, string recieverAccNum, string recieverBankId,Enums.TransferType transferType)
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
                newTransaction.TransactionType = transferType;
                newTransaction.TransectionNum = "TXN" + accHldr.BankId + accHldr.AccNumber + DateTime.Now.ToString("ddMMyyyy");
                
                accHldr.AccountBalance -= (amount + amount * GetCharges(transferType.ToString(), recieverBankId == GlobalDataStorage.yesBank.BankId) / 100);
                if (transferType.ToString() == "IMPS")
                {
                    if(recieverBankId == GlobalDataStorage.yesBank.BankId)
                    {
                        GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == recieverAccNum).AccountBalance += amount;
                    }
                    newTransaction.TransactionStatus = Enums.TransactionStatus.Success;

                }
                else
                {
                    newTransaction.TransactionStatus = Enums.TransactionStatus.Pending;
                }
                GlobalDataStorage.Transactions.Add(newTransaction);
                return true;
            }
            else
            {
                return false;
            }
        }

        double GetCharges(string transferType,bool isSameBank)
        {
            if (transferType == "RTGS")
            {
                if (isSameBank)
                {
                    return GlobalDataStorage.yesBank.SameRtgsCharges;
                }
                else
                {
                    return GlobalDataStorage.yesBank.OtherRtgsCharges;
                }
            }
            else
            {
                if (isSameBank)
                {
                    return GlobalDataStorage.yesBank.SameImpsCharges;
                }
                else
                {
                    return GlobalDataStorage.yesBank.OtherImpsCharges;
                }
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
            return GlobalDataStorage.AccHolder.Any(accHldr => accHldr.AccNumber == accNum);
        }

        public bool ValidateAccHolder(string accPin , string accNum)
        {
            if (GlobalDataStorage.AccHolder.Any(accHldr =>accHldr.AccPin == accPin) && GlobalDataStorage.AccHolder.Any(accHldr => accHldr.AccNumber == accNum))
            {
                loggedInUserAccNum = accNum;
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
