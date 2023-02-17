

namespace BankStimulation
{
    public class Enums
    {

        public enum MenuAccHolder
        {
            Exit, DepositeMoney, WithdrawMoney, transactMoney, transactionHistory
        }
        public enum MenuBankEmp
        {
            Exit, CreateNewAcc, UpdateBankAccount, UpdateCurrency, UpdateRtgs, UpdateImps, ViewTransactionHistory, RevertTransaction, DisplayAccdetails
        }

        public enum LogInType
        {
            Exit , BankEmployeeLogIn , AccountHolderLogIn , SuperAdminLogIn , BankAdminLogIn
        }

        public enum TransactionStatus
        {
            Success , Pending , Failed
        }

        public enum TransferType
        {
            RTGS , IMPS
        }

        public enum MenuSuperAdmin
        {
           Exit , CreateBank , EditBankAdmin , DisplayBankDetails
        }

        public enum MenuBankAdmin
        {
            Exit, AddEmployees , EditEmployees , ApproveTransactions , DisplayTransactionHistory , DisplayAccountDetails
        }
    }
}
