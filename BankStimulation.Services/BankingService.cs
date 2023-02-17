using BankStimulation.Model;
using System.Text.RegularExpressions;


namespace BankStimulation.Services
{
    public class BankingService
    {   

        public bool SetRtgs(double sameRtgs, double otherRtgs , string bankId)
        {
            var refBank = GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId);
            refBank.OtherRtgsCharges = otherRtgs;
            refBank.SameRtgsCharges = sameRtgs;
            return true;
            
        }

        public bool SetImps(double sameImps, double otherImps , string bankId)
        {
            var refBank = GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId);
            refBank.OtherImpsCharges = otherImps;
            refBank.SameImpsCharges = sameImps;
            return true;
        
        }

        public bool ValidateIdAndAccNum(string rcvAccNum, string rcvBankId)
        {
            return Regex.IsMatch(rcvAccNum, "^[a-zA-Z]{3}[0-9]{8}$") && Regex.IsMatch(rcvBankId, "^[a-zA-Z]{3}[0-9]{8}$");
            
        }

        public bool ValidateTransactionNumber(string txnNum)
        {
            return GlobalDataStorage.Transactions.Any(txn => txn.TransectionNum == txnNum);
        }

        public bool ValidateBankId(string bankId)
        {
            return GlobalDataStorage.Banks.Any(bnk => bnk.BankId == bankId);
            
        }
    }
}
