using BankStimulation.Model;


namespace BankStimulation.Services
{
    public class BankAdminService
    {
        public string bankId;
        public bool AddEmployees(BankEmployee emp )
        {
            GlobalDataStorage.BankEmp.Add(emp);

            return true;
        }

        public bool EditEmployees(BankEmployee  emp)
        {
            var empToBeEdited = GlobalDataStorage.BankEmp.FirstOrDefault(employee => employee.UserId== emp.UserId);
            empToBeEdited.UserName = emp.UserName;
            empToBeEdited.Password = emp.Password;
            return true;
        }

        public bool ApproveTransaction(string txnNum)
        {
            var txnToBeApproved = GlobalDataStorage.Transactions.FirstOrDefault(txn => txn.TransectionNum== txnNum);
            if(txnToBeApproved.RecieversBankId == bankId)
            {
                GlobalDataStorage.AccHolder.FirstOrDefault(acc => acc.AccNumber == txnToBeApproved.RecieversAccNum && acc.BankId == bankId).AccountBalance += txnToBeApproved.TransactionAmount;
                txnToBeApproved.TransactionStatus = Enums.TransactionStatus.Success;
            }
            return true;
        }

        public List<Transaction> GetTransactionsToApprove()
        {
            return GlobalDataStorage.Transactions.Where(txn => txn.TransactionStatus == Enums.TransactionStatus.Pending && (txn.RecieversBankId == bankId || txn.SendersBankId == bankId)).ToList();
        }

        public bool ValidateBankAdmin(string userId , string password)
        {
            var validateCred = GlobalDataStorage.Banks.FirstOrDefault(bank => bank.UserId == userId && bank.Password == password);
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
    }
}
