using BankStimulation.Model;

namespace BankStimulation.Services
{
    public class SuperAdminService
    {
        string _superAdminId = "SupAdmin";
        string _superAdminPassword = "Super@123";
        public bool CreateBank(Bank bank)
        {
            GlobalDataStorage.Banks.Add(bank);
            return true;
        }

        public bool EditBankAdminCredentials(string bankId , Users admin)
        {
            var bankToBeEdited = GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId== bankId);
            bankToBeEdited.UserId = admin.UserId;
            bankToBeEdited.UserName = admin.UserName;
            bankToBeEdited.Password = admin.Password;
            return true;
        }

        public Bank DisplayBank(string bankId)
        {
            return GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId);
        }

        public bool ValidateSuperAdmin(string superAdminId , string superAdminPassword)
        {
            if(superAdminId  == _superAdminId && superAdminPassword== _superAdminPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
