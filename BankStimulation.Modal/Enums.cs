using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Exit , BankEmployeeLogIn , AccountHolderLogIn
        }

        public enum TransactionStatus
        {
            Success , Pending , Failed
        }

        public enum TransferType
        {
            RTGS , IMPS
        }

    }
}
