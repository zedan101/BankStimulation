using BankStimulation.Model;

namespace BankStimulation.Services
{
    public class AccountHolderService
    {
        string loggedInUserAccNum;
        string loggedInUserBankId;

        public bool DepositeFund(double amount)
        {
            var accHldr = GlobalDataStorage.Banks.First(bank => bank.BankId == loggedInUserBankId).AccHolder.First(accHldr => accHldr.AccNumber == loggedInUserAccNum);
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
            var accHldr = GlobalDataStorage.Banks.First(bank => bank.BankId == loggedInUserBankId).AccHolder.First(accHldr => accHldr.AccNumber == loggedInUserAccNum);
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
            var accHldr = GlobalDataStorage.Banks.First(bank => bank.BankId == loggedInUserBankId).AccHolder.First(accHldr => accHldr.AccNumber == loggedInUserAccNum);
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
                
                accHldr.AccountBalance -= (amount + amount * GetCharges(transferType.ToString(), recieverBankId == loggedInUserBankId) / 100);
                if (transferType.ToString() == "IMPS")
                {
                    if(recieverBankId == loggedInUserBankId)
                    {
                        GlobalDataStorage.Banks.First(bank => bank.BankId == loggedInUserBankId).AccHolder.First(accHldr => accHldr.AccNumber == recieverAccNum).AccountBalance += amount;
                    }
                    newTransaction.TransactionStatus = Enums.TransactionStatus.Success;

                }
                else
                {
                    newTransaction.TransactionStatus = Enums.TransactionStatus.Pending;
                }
                GlobalDataStorage.Banks.First(bank => bank.BankId == loggedInUserBankId).Transactions.Add(newTransaction);
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
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserBankId).SameRtgsCharges;
                }
                else
                {
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserBankId).OtherRtgsCharges;
                }
            }
            else
            {
                if (isSameBank)
                {
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserBankId).SameImpsCharges;
                }
                else
                {
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserBankId).OtherImpsCharges;
                }
            }
        }

        public bool SetAccHolderData(Accounts accountHolder)
        {
            if(accountHolder!=null)
            {
                GlobalDataStorage.Banks.First(bank => bank.BankId == accountHolder.BankId).AccHolder.Add(accountHolder);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ValidateAccNum(string accNum , string bankId)
        {
            return GlobalDataStorage.Banks.First(bank => bank.BankId == bankId).AccHolder.Any(accHldr => accHldr.AccNumber == accNum);
        }

        public bool ValidateAccHolder(string accPin , string accNum , string bankId)
        {
           
            if (GlobalDataStorage.Banks.Any(bank => bank.BankId == bankId) && GlobalDataStorage.Banks.First(bank => bank.BankId == bankId).AccHolder.Any(accHldr => accHldr.Password == accPin && accHldr.AccNumber == accNum))
            {
                loggedInUserAccNum = accNum;
                loggedInUserBankId = bankId;
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
