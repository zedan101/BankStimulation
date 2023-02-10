namespace BankStimulation.Model
{
    public class AccountHolder
    {
       public string accHolderName;
       public string accNumber;
       string _accPin;
       public string bankId;
       double _accountBalance;

        public double AccountBalance
        {
            get { return _accountBalance; }
            set { _accountBalance = value; }
        }

        public string AccPin
        {
            get { return _accPin; }
            set { _accPin = value; }
        }
    }
}