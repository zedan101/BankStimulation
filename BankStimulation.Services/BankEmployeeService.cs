using BankStimulation.Model;


namespace BankStimulation.Services
{
    public class BankEmployeeService
    {
        string currentBankId;
        AccountHolderService _accHldrService = new AccountHolderService();
        BankingService _bankingService= new BankingService();

        public Accounts DisplayAccountDetails(string accNum)
        {
            return GlobalDataStorage.AccHolder.FirstOrDefault(e => e.AccNumber == accNum);
        }

        public bool CreatNewAccount(Accounts accHolder)
        {
           
           if (_accHldrService.SetAccHolderData(accHolder))
           {
               return true;
           }
           else
           {
               return false;
           }
           
        }

        public bool UpdateBankAccount(string accNumber,string accPin,string accHldrName)
        {
            
            var currentDetails = GlobalDataStorage.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == accNumber);
            if (currentDetails != null)
            {
                if(accHldrName != null)
                {
                    currentDetails.UserName = accHldrName;
                }
                if(accPin != null)
                {
                    currentDetails.Password = accPin;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCurrency(Currency updatedCurrency)
        {
            if (updatedCurrency.AcceptedCurrency != null && updatedCurrency.ExchangeRate != 0)
            {
                GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == currentBankId).AcceptedCurrency.Add(updatedCurrency);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateRtgs(double updatedSameRtgs,double updatedOtherRtgs )
        {
            return _bankingService.SetRtgs(updatedSameRtgs, updatedOtherRtgs , currentBankId);
            
        }

        public bool UpdateImps(double updatedSameImps, double updatedOtherImps)
        {
           return _bankingService.SetImps(updatedSameImps, updatedOtherImps , currentBankId);
            
        }

        public List<Transaction> ViewTransectionHistory(string accNumber)
        {
            List<Transaction> txn = new List<Transaction>();
            txn = GlobalDataStorage.Transactions.Where(txn => txn.SenderAccNum == accNumber || txn.RecieversAccNum == accNumber).ToList();
            return txn;
        }

        public bool RevertTransection(string txnNum)
        {
            
            var transactionToRevert = GlobalDataStorage.Transactions.FirstOrDefault(txnToRevert => txnToRevert.TransectionNum == txnNum);
            if (transactionToRevert.TransactionType == Enums.TransferType.RTGS) 
            {
                if (transactionToRevert.SendersBankId == transactionToRevert.RecieversBankId)
                {
                    var senderAccToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.SenderAccNum);
                    var recieverAccToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.RecieversAccNum);
                    recieverAccToRevertTxn.AccountBalance -= transactionToRevert.TransactionAmount;
                    senderAccToRevertTxn.AccountBalance += transactionToRevert.TransactionAmount;
                    transactionToRevert.TransactionStatus = Enums.TransactionStatus.Success;
                    return true;
                }
                else
                {
                    if (transactionToRevert.SendersBankId == currentBankId)
                    {
                        var accToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.SenderAccNum);
                        transactionToRevert.TransactionStatus = Enums.TransactionStatus.Pending;
                        return true;

                    }
                    else if (transactionToRevert.RecieversBankId == currentBankId)
                    {
                        var accToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.RecieversAccNum);
                        accToRevertTxn.AccountBalance -= transactionToRevert.TransactionAmount;
                        transactionToRevert.TransactionStatus = Enums.TransactionStatus.Pending;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            else
            {
                if (transactionToRevert.SendersBankId==transactionToRevert.RecieversBankId)
                {
                    var senderAccToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.SenderAccNum);
                    var recieverAccToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.RecieversAccNum);
                    senderAccToRevertTxn.AccountBalance += transactionToRevert.TransactionAmount;
                    recieverAccToRevertTxn.AccountBalance -= transactionToRevert.TransactionAmount;
                    transactionToRevert.TransactionStatus = Enums.TransactionStatus.Success;
                    return true;    
                }
                else
                {
                    if(transactionToRevert.SendersBankId == currentBankId)
                    {
                        var accToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.SenderAccNum);
                        accToRevertTxn.AccountBalance += transactionToRevert.TransactionAmount;
                        transactionToRevert.TransactionStatus = Enums.TransactionStatus.Success;
                        return true;

                    }
                    else if(transactionToRevert.RecieversBankId == currentBankId){
                        var accToRevertTxn = GlobalDataStorage.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.RecieversAccNum);
                        accToRevertTxn.AccountBalance -= transactionToRevert.TransactionAmount;
                        transactionToRevert.TransactionStatus = Enums.TransactionStatus.Success;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool ValidateEmpCredentials(string empId, string empPass)
         {
            var validateCred = GlobalDataStorage.BankEmp.FirstOrDefault(bnkEmp => bnkEmp.UserId == empId && bnkEmp.Password == empPass);
            if (validateCred != null)
            {
                currentBankId = validateCred.BankId;
                return true;
            }
            else
            {
                return false;
            }
         }
    }
}