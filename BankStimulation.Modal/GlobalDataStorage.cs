namespace BankStimulation.Model
{
    public class GlobalDataStorage
    {
        static public List<Accounts> AccHolder = new List<Accounts>();

        static public List<BankEmployee> BankEmp = new List<BankEmployee>()
        {
            new BankEmployee() {UserId = "ram101" , UserName = "Ramesh Verma" , Password = "qwerty@123" , BankId = "yes10022023"},
            new BankEmployee() {UserId = "ani102" , UserName = "Anish Singh" , Password = "qwerty@123" ,  BankId = "yes10022023"},
            new BankEmployee() {UserId = "nav103" , UserName = "Naveen Gour" , Password = "qwerty@123", BankId = "yes10022023"},
            new BankEmployee() {UserId = "shi104" , UserName = "Shiva Gupta" , Password = "qwerty@123", BankId = "yes10022023"},
            new BankEmployee() {UserId = "moh105" , UserName = "Mohan Raj" , Password = "qwerty@123", BankId = "yes10022023"},
        };

        static public Bank yesBank = new Bank()
        {
            BankId = "yes10022023",
            SameRtgsCharges = 0,
            SameImpsCharges = 5,
            OtherRtgsCharges = 2,
            OtherImpsCharges = 6,
            DefaultCurrency = "INR",
            CurrencyExchangeRates = "1"
        };

        static public List<Transaction> Transactions = new List<Model.Transaction>();

        static public List<Currency> AcceptedCurrency = new List<Currency>()
        {
            new Currency()
            {
                AcceptedCurrency = "INR",
                ExchangeRate= 1,
            }
        };

    }
}