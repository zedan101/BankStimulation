

namespace BankStimulation.Model
{
    public class Bank
    {
       public string BankName;
       public string BankId;
       public double SameRtgsCharges;
       public double SameImpsCharges;
       public double OtherRtgsCharges;
       public double OtherImpsCharges;
       public string DefaultCurrency;
       public double CurrencyExchangeRates;

        public Users Admin = new Users();

        public List<Accounts> AccHolder = new List<Accounts>();

        public List<BankEmployee> BankEmp = new List<BankEmployee>();

        public List<Transaction> Transactions = new List<Model.Transaction>();

        public List<Currency> AcceptedCurrency = new List<Currency>()
        {
            new Currency()
            {
                AcceptedCurrency = "INR",
                ExchangeRate= 1,
            }
        };
    }
}
