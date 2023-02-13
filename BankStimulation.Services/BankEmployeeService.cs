using BankStimulation.Model;
using System.Net.Sockets;

namespace BankStimulation.Services
{
    public class BankEmployeeService
    {
        static List<BankEmployee> BankEmp = new List<BankEmployee>()
        {
            new BankEmployee() {EmpId = "ram101" , EmpName = "Ramesh Verma" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "ani102" , EmpName = "Anish Singh" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "nav103" , EmpName = "Naveen Gour" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "shi104" , EmpName = "Shiva Gupta" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "moh105" , EmpName = "Mohan Raj" , Password = "qwerty@123"},
        };

        AccountHolderService accHldrService = new AccountHolderService();
        BankingService bankingService= new BankingService();

        public AccountHolder DisplayAccountDetails(string accNum)
        {
            return AccountHolderService.AccHolder.FirstOrDefault(e => e.AccNumber == accNum);
        }

        public bool CreatNewAccount(AccountHolder accHolder)
        {
           
           if (accHldrService.SetAccHolderData(accHolder))
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
            
            var currentDetails = AccountHolderService.AccHolder.FirstOrDefault(accHldr => accHldr.AccNumber == accNumber);
            if (currentDetails != null)
            {
                if(accHldrName != null)
                {
                    currentDetails.AccHolderName = accHldrName;
                }
                if(accPin != null)
                {
                    currentDetails.AccPin = accPin;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCurrency(string updatedCurrency , string updatedExchangeRates)
        {
            if (bankingService.SetCurrency(updatedCurrency,updatedExchangeRates))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateRtgs(double updatedSameRtgs,double updatedOtherRtgs)
        {
            if (bankingService.SetRtgs(updatedSameRtgs, updatedOtherRtgs))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateImps(double updatedSameImps, double updatedOtherImps)
        {
            if (bankingService.SetImps(updatedSameImps, updatedOtherImps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Transaction> ViewTransectionHistory(string accNumber)
        {
            List<Transaction> txn = new List<Transaction>();
            txn = BankingService.Transactions.Where(txn => txn.SenderAccNum == accNumber || txn.RecieversAccNum == accNumber).ToList();
            return txn;
        }

        public bool RevertTransection(string txnNum)
        {
            Transaction transactionToRevert = new Transaction();
            transactionToRevert = BankingService.Transactions.FirstOrDefault(txnToRevert => txnToRevert.TransectionNum == txnNum);
            if(transactionToRevert.SendersBankId == BankingService.yesBank.BankId)
            {
                var accToRevertTxn = AccountHolderService.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.SenderAccNum);
                accToRevertTxn.AccountBalance += transactionToRevert.TransactionAmount;
                return true;

            }
            else if(transactionToRevert.RecieversBankId == BankingService.yesBank.BankId){
                var accToRevertTxn = AccountHolderService.AccHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.AccNumber == transactionToRevert.RecieversAccNum);
                accToRevertTxn.AccountBalance -= transactionToRevert.TransactionAmount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateEmpId(string empId)
         {
            if (BankEmp.Any(bnkEmp => bnkEmp.EmpId == empId))
            {
                return true;
            }
            else
            {
                return false;
            }
         }

        public bool ValidateEmpPass(string empPass)
        {
            if (BankEmp.Any(bnkEmp => bnkEmp.Password == empPass))
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