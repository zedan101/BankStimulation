using BankStimulation.Model;


namespace BankStimulation.Services
{
    public class BankAdminService
    {
        public string bankId;
        public bool AddEmployees(string empId , string empName , string empPassword )
        {
            GlobalDataStorage.BankEmp.Add(new BankEmployee()
            {
                UserId= empId,
                Password= empPassword,
                BankId= bankId
            });

            return true;
        }

        public bool EditEmployees(string empId, string empName, string empPassword)
        {
            var empToBeEdited = GlobalDataStorage.BankEmp.FirstOrDefault(emp => emp.UserId== empId);
            empToBeEdited.UserName = empName;
            empToBeEdited.Password = empPassword;
            return true;
        }

        public bool ApproveTransaction(string txnNum)
        {
            var txnToBeApproved = GlobalDataStorage.Transactions.FirstOrDefault(txn => txn.TransectionNum== txnNum);
            //txnToBeApproved.
                return true;
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
