

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
