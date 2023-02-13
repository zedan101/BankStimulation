using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStimulation
{
    internal class Enums
    {

        public enum MenuAccHolder
        {
            Exit, DepositeMoney, WithdrawMoney, transactMoney, transactionHistory
        }
        public enum MenuBankEmp
        {
            Exit, CreateNewAcc, UpdateBankAccount, UpdateCurrency, UpdateRtgs, UpdateImps, ViewTransactionHistory, RevertTransaction, DisplayAccdetails
        }

    }
}
