

namespace BankStimulation.Model
{
    public class Bank : Users
    {
        public string BankName;
       public string BankId;
       public double SameRtgsCharges;
       public double SameImpsCharges;
       public double OtherRtgsCharges;
       public double OtherImpsCharges;
       public string DefaultCurrency;
       public double CurrencyExchangeRates;

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
