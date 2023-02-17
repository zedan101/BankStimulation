using BankStimulation.Model;

namespace BankStimulation.Services
{
    public class AccountHolderService
    {
        string loggedInUserAccNum;
        string loggedInUserbankId;

        public bool DepositeFund(double amount)
        {
            var accHldr = GlobalDataStorage. AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber== loggedInUserAccNum && accHldr.BankId == loggedInUserbankId);
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
            var accHldr = GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum && accHldr.BankId == loggedInUserbankId);
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
            var accHldr = GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == loggedInUserAccNum && accHldr.BankId == loggedInUserbankId);
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
                
                accHldr.AccountBalance -= (amount + amount * GetCharges(transferType.ToString(), recieverBankId == loggedInUserbankId) / 100);
                if (transferType.ToString() == "IMPS")
                {
                    if(recieverBankId == loggedInUserbankId)
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
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserbankId).SameRtgsCharges;
                }
                else
                {
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserbankId).OtherRtgsCharges;
                }
            }
            else
            {
                if (isSameBank)
                {
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserbankId).SameImpsCharges;
                }
                else
                {
                    return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == loggedInUserbankId).OtherImpsCharges;
                }
            }
        }

        public bool SetAccHolderData(Accounts accountHolder)
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
            var validateCred = GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.Password == accPin && accHldr.AccNumber == accNum);
            if (validateCred != null)
            {
                loggedInUserAccNum = accNum;
                loggedInUserbankId = validateCred.BankId;
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
