using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStimulation.Model
{
    public class Transaction
    {
        public string SenderAccNum;
        public string RecieversAccNum;
        public string SendersBankId;
        public string RecieversBankId;
        public string TransectionNum;
        public double TransactionAmount;
        public Enums.TransactionStatus TransactionStatus;
        public Enums.TransferType TransactionType;

    }
}
