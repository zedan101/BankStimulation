using BankStimulation.Model;


namespace BankStimulation.Services
{
    public class BankAdminService
    {
        public string bankId;
        public bool AddEmployees(BankEmployee emp )
        {
            GlobalDataStorage.Banks.First(bank  => bank.BankId == emp.BankId).BankEmp.Add(emp);

            return true;
        }

        public bool EditEmployees(BankEmployee  emp)
        {
            var empToBeEdited = GlobalDataStorage.Banks.First(bank => bank.BankId == emp.BankId).BankEmp.First(employee => employee.UserId== emp.UserId);
            empToBeEdited.UserName = emp.UserName;
            empToBeEdited.Password = emp.Password;
            return true;
        }

        public bool ApproveTransaction(string txnNum)
        {
            var txnToBeApproved = GlobalDataStorage.Banks.First(bank => bank.BankId == bankId).Transactions.First(txn => txn.TransectionNum== txnNum);
            if(txnToBeApproved.RecieversBankId == bankId)
            {
                GlobalDataStorage.Banks.First(bank => bank.BankId == bankId).AccHolder.FirstOrDefault(acc => acc.AccNumber == txnToBeApproved.RecieversAccNum && acc.BankId == bankId).AccountBalance += txnToBeApproved.TransactionAmount;
                txnToBeApproved.TransactionStatus = Enums.TransactionStatus.Success;
            }
            return true;
        }

        public List<Transaction> GetTransactionsToApprove()
        {
            return GlobalDataStorage.Banks.First(bank => bank.BankId == bankId).Transactions.Where(txn => txn.TransactionStatus == Enums.TransactionStatus.Pending && (txn.RecieversBankId == bankId || txn.SendersBankId == bankId)).ToList();
        }

        public bool ValidateBankAdmin(string userId , string password , string idBank)
        {
            var validateCred = GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == idBank && bank.Admin.Password == password && bank.Admin.UserId == userId);
            if (validateCred != null)
            {
                bankId = validateCred.BankId;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateTransactionNumber(string transactionNumber)
        {
            return (GetTransactionsToApprove().Any(txn => txn.TransectionNum == transactionNumber));
        }

        public bool ValidateBankAdmin(string userId , string idBank)
        {
            return GlobalDataStorage.Banks.Any(bank => bank.Admin.UserId == userId && bank.BankId == idBank);
            
            
        }
    }
}
