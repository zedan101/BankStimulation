using BankStimulation.Model;

namespace BankStimulation.Services
{
    public class SuperAdminService
    {
        Users _superAdmin = new Users() { UserId = "superAdminXX", UserName = "Super", Password = "super@123" };

        public bool CreateBank(Bank bank)
        {
            GlobalDataStorage.Banks.Add(bank);
            return true;
        }

        public bool EditBankAdminCredentials(string bankId , Users admin)
        {
            var bankToBeEdited = GlobalDataStorage.Banks.First(bank => bank.BankId== bankId);
            bankToBeEdited.Admin.UserId = admin.UserId;
            bankToBeEdited.Admin.UserName = admin.UserName;
            bankToBeEdited.Admin.Password = admin.Password;
            return true;
        }

        public Bank DisplayBank(string bankId)
        {
            return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId);
        }

        public bool ValidateSuperAdmin(string superAdminId , string superAdminPassword)
        {
            return (superAdminId == _superAdmin.UserId) && (superAdminPassword == _superAdmin.Password);
        }

    }
}
