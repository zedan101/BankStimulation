namespace BankStimulation.Model
{
    public class GlobalDataStorage
    {
        static public List<AccountHolder> AccHolder = new List<AccountHolder>();

        static public List<BankEmployee> BankEmp = new List<BankEmployee>()
        {
            new BankEmployee() {EmpId = "ram101" , EmpName = "Ramesh Verma" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "ani102" , EmpName = "Anish Singh" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "nav103" , EmpName = "Naveen Gour" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "shi104" , EmpName = "Shiva Gupta" , Password = "qwerty@123"},
            new BankEmployee() {EmpId = "moh105" , EmpName = "Mohan Raj" , Password = "qwerty@123"},
        };

        static public Bank yesBank = new Bank()
        {
            BankId = "yes100223",
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