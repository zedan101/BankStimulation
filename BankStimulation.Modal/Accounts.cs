namespace BankStimulation.Model
{
    public class Accounts : Users
    {
      // public string AccHolderName;
       public string AccNumber;
      // string _accPin;
       public string BankId;
       double _accountBalance;

        public double AccountBalance
        {
            get { return _accountBalance; }
            set { _accountBalance = value; }
        }

        
    }
}