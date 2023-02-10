using BankStimulation.Model;
using System.Net.Sockets;

namespace BankStimulation.Services
{
    public class BankEmployeeService
    {
        List<BankEmployee> bankEmp = new List<BankEmployee>()
        {
            new BankEmployee() {empId = "ram101" , empName = "Ramesh Verma" , Password = "qwerty@123"},
            new BankEmployee() {empId = "ani102" , empName = "Anish Singh" , Password = "qwerty@123"},
            new BankEmployee() {empId = "nav103" , empName = "Naveen Gour" , Password = "qwerty@123"},
            new BankEmployee() {empId = "shi104" , empName = "Shiva Gupta" , Password = "qwerty@123"},
            new BankEmployee() {empId = "moh105" , empName = "Mohan Raj" , Password = "qwerty@123"},
        };

        AccountHolderService accHldrService = new AccountHolderService();
        BankingService bankingService= new BankingService();

        public AccountHolder DisplayAccountDetails(string accNum)
        {
            return accHldrService.accHolder.FirstOrDefault(e => e.accNumber == accNum);
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
            
            var currentDetails = accHldrService.accHolder.FirstOrDefault(accHldr => accHldr.accNumber == accNumber);
            if (currentDetails != null)
            {
                if(accHldrName != null)
                {
                    currentDetails.accHolderName = accHldrName;
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

        public List<Transactions> ViewTransectionHistory(string accNumber)
        {
            List<Transactions> txn = new List<Transactions>();
            txn = bankingService.transactions.Where(txn => txn.senderAccNum == accNumber || txn.recieversAccNum == accNumber).ToList();
            return txn;
        }

        public void RevertTransection(string txnNum)
        {
            Transactions transactionToRevert = new Transactions();
            transactionToRevert = bankingService.transactions.FirstOrDefault(txnToRevert => txnToRevert.transectionNum == txnNum);
            if(transactionToRevert.sendersBankId == bankingService.yesBank.bankId)
            {
                var accToRevertTxn = accHldrService.accHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.accNumber == transactionToRevert.senderAccNum);
                accToRevertTxn.AccountBalance += transactionToRevert.transactionAmount;

            }
            else if(transactionToRevert.recieversBankId == bankingService.yesBank.bankId){
                var accToRevertTxn = accHldrService.accHolder.FirstOrDefault(accToRvrtTxn => accToRvrtTxn.accNumber == transactionToRevert.recieversAccNum);
                accToRevertTxn.AccountBalance -= transactionToRevert.transactionAmount;
            }
        }

        public bool ValidateEmpId(string empId)
         {
            if (bankEmp.Any(bnkEmp => bnkEmp.empId == empId))
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
            if (bankEmp.Any(bnkEmp => bnkEmp.Password == empPass))
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