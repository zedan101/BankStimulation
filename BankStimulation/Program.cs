using BankStimulation.Services;
using BankStimulation.Model;
using System.Net.Http.Headers;

namespace BankStimulation
{
    public class Program
    {
        static void Main(string[] args)
        {
            BankAdminService bankAdminService = new BankAdminService();
            SuperAdminService superAdminService= new SuperAdminService();
            AccountHolderService accHolderService = new AccountHolderService();
            BankEmployeeService bankEmployeeService = new BankEmployeeService();
            BankingService bankingService = new BankingService();
            CommonService commonService = new CommonService();
            Console.WriteLine("Bank Stimulation");
            bool exit = false;
            do
            {

                Users user = new();
                Console.WriteLine("To exist enter " + (int)Enums.LogInType.Exit);
                Console.WriteLine("For Bank Employee LogIn Enter " + (int)Enums.LogInType.BankEmployeeLogIn);
                Console.WriteLine("For Account Holder LogIn Enter " + (int)Enums.LogInType.AccountHolderLogIn);
                Console.WriteLine("For Super Admin LogIn Enter " + (int)Enums.LogInType.SuperAdminLogIn);
                Console.WriteLine("For Bank Admin LogIn Enter " + (int)Enums.LogInType.BankAdminLogIn);
                try 
                {
                    int input;
                    if(int.TryParse(Console.ReadLine(),out input))
                    {
                        switch ((Enums.LogInType)input)
                        {
                            case Enums.LogInType.BankEmployeeLogIn:
                                Console.WriteLine("Enter Employee Id");
                                user.UserId = Console.ReadLine();
                                Console.WriteLine("Enter Password");
                                user.Password = Console.ReadLine();
                                if (bankEmployeeService.ValidateEmpCredentials(user.UserId, user.Password))
                                {
                                    UiBankEmployee(user.UserId);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;

                                }

                            case Enums.LogInType.AccountHolderLogIn:
                                Console.WriteLine("Enter Account Number");
                                user.UserId = Console.ReadLine();
                                Console.WriteLine("Enter Password");
                                user.Password = Console.ReadLine();
                                if (accHolderService.ValidateAccHolder(user.Password, user.UserId))
                                {
                                    UiAccountHolder(user.UserId);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }

                            case Enums.LogInType.SuperAdminLogIn:
                                Console.WriteLine("Enter Id");
                                user.UserId = Console.ReadLine();
                                Console.WriteLine("Enter Password");
                                user.Password = Console.ReadLine();
                                if (bankAdminService.ValidateBankAdmin(user.UserId, user.Password))
                                {
                                    UiSuperAdmin();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }

                            case Enums.LogInType.BankAdminLogIn:
                                Console.WriteLine("Enter Id");
                                user.UserId = Console.ReadLine();
                                Console.WriteLine("Enter Password");
                                user.Password = Console.ReadLine();
                                if (bankAdminService.ValidateBankAdmin(user.UserId, user.Password))
                                {
                                    UiBankAdmin(GlobalDataStorage.Banks.FirstOrDefault(bank => bank.UserId == user.UserId).BankId);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }

                            case Enums.LogInType.Exit:
                                exit = true;
                                break;
                        }
                    }
                    else
                    {
                        throw new InvalidInputException("Invalid Input");
                    }  
                   
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine(ex); 
                }
                
            }
            while (!exit);

            void UiAccountHolder(string accNm)
            {
                string bankId = GlobalDataStorage.AccHolder.FirstOrDefault(acchldr => acchldr.AccNumber == accNm).BankId;
                bool exit = false;
                do
                {
                    Console.WriteLine("\n To Deposite Money Enter 1");
                    Console.WriteLine("To Withdraw Money Enter 2");
                    Console.WriteLine("To Transact Money Enter 3");
                    Console.WriteLine("To View Transaction history Enter 4");
                    Console.WriteLine("To Exit Enter 0");
                    try
                    {
                        int input = Int32.Parse(Console.ReadLine());

                        switch ((Enums.MenuAccHolder)input)
                        {
                            case Enums.MenuAccHolder.DepositeMoney:
                                Console.WriteLine("Enter Currency");
                                string currency = Console.ReadLine();
                                Console.WriteLine("\n Please enter the Amount");
                                try
                                {
                                    double amount = Convert.ToDouble(Console.ReadLine());
                                    if (GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId).AcceptedCurrency.Any(e => e.AcceptedCurrency == currency))
                                    {
                                        IsSuccess(accHolderService.DepositeFund(amount * GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId).AcceptedCurrency.FirstOrDefault(e => e.AcceptedCurrency == currency).ExchangeRate));
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Currency");
                                        break;
                                    }

                                }
                                catch (InvalidInputException)
                                {
                                    Console.WriteLine("Invalid Amount");
                                    break;
                                }
                                

                            case Enums.MenuAccHolder.WithdrawMoney:
                                Console.WriteLine("\n Please enter amount to Withdraw");
                                try
                                {
                                    double amount = Convert.ToDouble(Console.ReadLine());
                                    IsSuccess(accHolderService.WithdrawFund(amount));
                                    break;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid amount");
                                    break;
                                }

                            case Enums.MenuAccHolder.transactMoney:
                                Console.WriteLine("Enter reciever Bank Id");
                                string rcvBankId = Console.ReadLine();
                                Console.WriteLine("Enter Reciever bank Account Number");
                                string rcvAccNum = Console.ReadLine();
                                if(bankingService.ValidateIdAndAccNum(rcvAccNum, rcvBankId))
                                {
                                    Console.WriteLine("Enter Amiount To tranfer");
                                    try
                                    {
                                        double amount = Convert.ToDouble(Console.ReadLine());
                                        Console.WriteLine("Enter Transfet Type \t For RTGS->1 \t For IMPS->2 ");
                                        try
                                        {
                                            input = Int32.Parse(Console.ReadLine());
                                            IsSuccess(accHolderService.TransferFunds(amount, rcvAccNum, rcvBankId,input==1?Enums.TransferType.RTGS:Enums.TransferType.IMPS));
                                            break;
                                            
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Invalid Input");
                                            break;
                                        }

                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("Invalid Amount");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Credentials");
                                    break;
                                }
                                


                            case Enums.MenuAccHolder.transactionHistory:
                                if (commonService.ViewTransectionHistory(accNm, bankId).Any())
                                {
                                    foreach (Transaction item in commonService.ViewTransectionHistory(accNm, bankId))
                                    {
                                        Console.WriteLine("Transaction Number:-" + item.TransectionNum);
                                        Console.WriteLine("Transaction Amount:-" + item.TransactionAmount);
                                        Console.WriteLine("Recievers Account Number:-" + item.RecieversAccNum);
                                        Console.WriteLine("Recievers Bank Id :- " + item.RecieversBankId);
                                        Console.WriteLine("Senders Account Number:- " + item.SenderAccNum);
                                        Console.WriteLine("Recievers Bank Id:-" + item.SendersBankId);
                                        Console.WriteLine("Transaction Type:- " + item.TransactionType);
                                        Console.WriteLine("Transaction Status :-" + item.TransactionStatus);
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("No Transactions Yet");
                                    break;
                                }


                            case Enums.MenuAccHolder.Exit:
                                exit = true;
                                break;

                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ivalid Input");
                    }
                }
                while (!exit);
                
                

            }
            void UiBankEmployee(string empId)
            { 
                string bankId = GlobalDataStorage.BankEmp.FirstOrDefault(emp => emp.UserId == empId).BankId;
                bool exit = false;
                do
                {
                    Console.WriteLine("\n To Create a New Account Enter 1");
                    Console.WriteLine("To Update Bank Account Enter 2");
                    Console.WriteLine("To Update Currency Enter 3");
                    Console.WriteLine("To Update RTGS Enter 4");
                    Console.WriteLine("To Update IMPS Enter 5");
                    Console.WriteLine("To View Transaction History Enter 6");
                    Console.WriteLine("To Revert A Transaction Enter 7");
                    Console.WriteLine("To Display Account Details Enter 8");
                    Console.WriteLine("To Exit Enter 0");
                    try
                    {
                        int input = Int32.Parse(Console.ReadLine());
                        switch ((Enums.MenuBankEmp)input)
                        {
                            case Enums.MenuBankEmp.CreateNewAcc:
                                Accounts accHolder = new ();
                                Console.WriteLine("Enter Account Holder Name");
                                accHolder.UserName = Console.ReadLine();
                                Console.WriteLine("Enter Pin");
                                accHolder.Password = Console.ReadLine();
                                Console.WriteLine("Enter Initial Account balance");
                                string amtInput = Console.ReadLine();
                                double amount;
                                Console.WriteLine("Enter Currency");
                                string currency= Console.ReadLine();
                                if(GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId).AcceptedCurrency.Any(e => e.AcceptedCurrency == currency))
                                {
                                    if (double.TryParse(amtInput, out amount) && amount>0)
                                    {
                                        accHolder.AccNumber = accHolder.UserName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                                        accHolder.AccountBalance = amount * GlobalDataStorage.Banks.FirstOrDefault(bank => bank.BankId == bankId).AcceptedCurrency.FirstOrDefault(e => e.AcceptedCurrency == currency).ExchangeRate;
                                        accHolder.BankId = bankId;
                                        IsSuccess(bankEmployeeService.CreatNewAccount(accHolder));
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Amount");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Currency");
                                    break;
                                }
                                
                               

                            case Enums.MenuBankEmp.UpdateBankAccount:
                                Console.WriteLine("Enter Account Number of Account To Be Updated");
                                string accNum = Console.ReadLine();
                                if(accHolderService.ValidateAccNum(accNum))
                                {
                                    Console.WriteLine("Enter Updated Name");
                                    string accHldrName = Console.ReadLine();
                                    Console.WriteLine("Enter Updated Pin");
                                    string accPin = Console.ReadLine();
                                    IsSuccess(bankEmployeeService.UpdateBankAccount(accNum, accPin, accHldrName));
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }
                                

                            case Enums.MenuBankEmp.UpdateCurrency:
                                Currency updateCurrency = new();
                                Console.WriteLine("Enter Updated Currency");
                                updateCurrency.AcceptedCurrency = Console.ReadLine();
                                Console.WriteLine("Enter Updated Exchange rates");
                                updateCurrency.ExchangeRate = Convert.ToDouble(Console.ReadLine());
                                IsSuccess(bankEmployeeService.UpdateCurrency(updateCurrency));
                                break;

                            case Enums.MenuBankEmp.UpdateRtgs:
                                Console.WriteLine("Enter Updated Same RTGS");
                                int updatedSameRtgs; 
                                string sameRtgsInput =Console.ReadLine();
                                if(int.TryParse(sameRtgsInput, out updatedSameRtgs) && updatedSameRtgs <= 100)
                                {
                                    Console.WriteLine("Enter Updated Other RTGS");
                                    int updatedOtherRtgs;
                                    string otherRtgsInput = Console.ReadLine();
                                    if(int.TryParse(otherRtgsInput, out updatedOtherRtgs) && updatedOtherRtgs<=100)
                                    {
                                        IsSuccess(bankEmployeeService.UpdateRtgs(updatedSameRtgs, updatedOtherRtgs));
                                        break;   
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    break;
                                }
                                

                            case Enums.MenuBankEmp.UpdateImps:
                                Console.WriteLine("Enter Updated Same IMPS");
                                int updatedSameImps;
                                string sameImpsInput= Console.ReadLine();
                                if (int.TryParse(sameImpsInput, out updatedSameImps) && updatedSameImps<=100)
                                {
                                    Console.WriteLine("Enter Updated Other IMPS");
                                    int updatedOtherImps;
                                    string otherImpsInput = Console.ReadLine();
                                    if (int.TryParse(otherImpsInput, out updatedOtherImps) && updatedOtherImps <=100)
                                    {
                                        IsSuccess(bankEmployeeService.UpdateImps(updatedSameImps, updatedOtherImps));
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    break;
                                }
                               
                                

                            case Enums.MenuBankEmp.ViewTransactionHistory:
                                Console.WriteLine("Enter account Number");
                                accNum = Console.ReadLine();
                                if(accHolderService.ValidateAccNum(accNum))
                                {
                                    if (commonService.ViewTransectionHistory(accNum, bankId).Any())
                                    {
                                        foreach(Transaction item in commonService.ViewTransectionHistory(accNum, bankId))
                                        {
                                            Console.WriteLine("Transaction Number:-" + item.TransectionNum);
                                            Console.WriteLine("Transaction Amount:-" + item.TransactionAmount);
                                            Console.WriteLine("Recievers Account Number:-" + item.RecieversAccNum);
                                            Console.WriteLine("Recievers Bank Id :- " + item.RecieversBankId);
                                            Console.WriteLine("Senders Account Number:- " + item.SenderAccNum);
                                            Console.WriteLine("Recievers Bank Id:-" + item.SendersBankId);
                                            Console.WriteLine("Transaction Type:- " + item.TransactionType);
                                            Console.WriteLine("Transaction Status :-" + item.TransactionStatus);
                                        }
                                        break;
                                    }
                                    else 
                                    {
                                        Console.WriteLine("No Transactions Yet");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }
                                

                            case Enums.MenuBankEmp.RevertTransaction:
                                Console.WriteLine("Enter Transaction Number");
                                string transactionNum = Console.ReadLine();
                                if (bankingService.ValidateTransactionNumber(transactionNum))
                                {
                                    IsSuccess(bankEmployeeService.RevertTransection(transactionNum));
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Transaction Number");
                                    break;
                                }
                                

                            case Enums.MenuBankEmp.DisplayAccdetails:
                                Console.WriteLine("Enter Account number ");
                                accNum = Console.ReadLine();
                                if (accHolderService.ValidateAccNum(accNum))
                                {
                                    Accounts accountDetail = new Accounts();
                                    accountDetail = commonService.DisplayAccountDetails(accNum, bankId);
                                    Console.WriteLine("Account Number :-" + accountDetail.AccNumber);
                                    Console.WriteLine("Account Holder Name :-" + accountDetail.UserName);
                                    Console.WriteLine("Account Balance :-" + accountDetail.AccountBalance);
                                    Console.WriteLine("BankId :-" + accountDetail.BankId);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }
                                

                            case Enums.MenuBankEmp.Exit:
                                exit = true;
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                while (!exit);
            }


            void IsSuccess(bool isSuccess)
            {
                if (isSuccess)
                {
                    Console.WriteLine("Successful");
                }
                else
                {
                    Console.WriteLine("Unsuccessful");
                }
            }

            void UiSuperAdmin()
            {
                bool exit = false;
                do
                {
                    Users admin = new ();
                    Bank bank = new();
                    Console.WriteLine("\n To Create a New Bank Enter 1");
                    Console.WriteLine("To Edit Bank Admin Credentials Enter 2");
                    Console.WriteLine("To Display Bank Details Enter 3");
                    Console.WriteLine("To Exit Enter 0");
                    try
                    {
                        int input = Int32.Parse(Console.ReadLine());
                        switch ((Enums.MenuSuperAdmin)input)
                        {
                            case Enums.MenuSuperAdmin.CreateBank:
                                Console.WriteLine("Enter Bank Admin Id");
                                bank.UserId = Console.ReadLine();
                                Console.WriteLine("Enter Bank Admin Name");
                                bank.UserName = Console.ReadLine();
                                Console.WriteLine("Enter Bank Admin PassWord");
                                bank.Password = Console.ReadLine();
                                Console.WriteLine("Enter Bank Name");
                                bank.BankName = Console.ReadLine();
                                Console.WriteLine("Enter RTGS Charges For Same Bank");
                                bank.SameRtgsCharges = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter IMPS Charges For Same Bank");
                                bank.SameImpsCharges = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter RTGS Charges For Different Bank");
                                bank.OtherRtgsCharges = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter IMPS Charges For Different Bank");
                                bank.OtherImpsCharges = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter Default Currency");
                                bank.DefaultCurrency = Console.ReadLine();
                                Console.WriteLine("Enter ExchangeRate");
                                bank.CurrencyExchangeRates= Convert.ToDouble(Console.ReadLine());
                                IsSuccess(superAdminService.CreateBank(bank));
                                break;

                            case Enums.MenuSuperAdmin.EditBankAdmin:
                                Console.WriteLine("Enter Bank Id");
                                string bankId = Console.ReadLine();
                                if (bankingService.ValidateBankId(bankId))
                                {
                                    Console.WriteLine("Enter Admin Id");
                                    admin.UserId = Console.ReadLine();
                                    if (bankAdminService.ValidateBankAdmin(admin.UserId))
                                    {
                                        Console.WriteLine("Enter Admin Name");
                                        admin.UserName = Console.ReadLine();
                                        Console.WriteLine("Enter New Password");
                                        admin.Password = Console.ReadLine();
                                        IsSuccess(superAdminService.EditBankAdminCredentials(bankId, admin));
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Admin Id");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid BankId");
                                    break;
                                }
                                
                                

                            case Enums.MenuSuperAdmin.DisplayBankDetails:
                                Console.WriteLine("Enter Bank ID");
                                bankId= Console.ReadLine();
                                if(bankingService.ValidateBankId(bankId))
                                {
                                    if (superAdminService.DisplayBank(bankId) != null)
                                    {
                                        bank = superAdminService.DisplayBank(bankId);

                                        Console.WriteLine("\n Bank Admin Id:-" + bank.UserId);
                                        Console.WriteLine("Bank Admin Name:-" + bank.UserName);
                                        Console.WriteLine("Bank Name:-" + bank.BankName);
                                        Console.WriteLine("Bank Id :- " + bank.BankId);
                                        Console.WriteLine("RTGS Charges For Same Bank:- " + bank.SameRtgsCharges);
                                        Console.WriteLine("IMPS Charges For Same Bank:-" + bank.SameImpsCharges);
                                        Console.WriteLine("RTGS Charges For Different Banks:- " + bank.OtherRtgsCharges);
                                        Console.WriteLine("IMPS Charges For Different Banks :-" + bank.OtherImpsCharges);
                                        Console.WriteLine("Default Currency:-" + bank.DefaultCurrency);
                                        Console.WriteLine("Currency Exchange Rate:-" + bank.CurrencyExchangeRates + "\n");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("No Banks Added Yet");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid BankId");
                                    break;
                                }
                                

                            case Enums.MenuSuperAdmin.Exit:
                                exit = true;
                                break;


                        }    
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                while (!exit);
            }

            void UiBankAdmin(string bnkId)
            {

                bool exit = false;
                do
                {
                    BankEmployee emp = new();
                    Console.WriteLine("\n To Add A New Employee Enter 1");
                    Console.WriteLine("To Edit Employee Credentials Enter 2");
                    Console.WriteLine("To Approve Transactions Enter 3");
                    Console.WriteLine("To View Transaction Enter 4");
                    Console.WriteLine("To View Employee Details Enter 5");
                    Console.WriteLine("To View Account Details Enter 6");
                    Console.WriteLine("To Exit Enter 0");
                    try
                    {
                        int input = Int32.Parse(Console.ReadLine());
                        switch ((Enums.MenuBankAdmin)input)
                        {
                            case Enums.MenuBankAdmin.AddEmployees:
                                Console.WriteLine("Enter Bank Employee Id");
                                emp.UserId = Console.ReadLine();
                                Console.WriteLine("Enter Bank Employee Name");
                                emp.UserName = Console.ReadLine();
                                Console.WriteLine("Enter Bank Employee PassWord");
                                emp.Password = Console.ReadLine();
                                emp.BankId = bnkId;
                                IsSuccess(bankAdminService.AddEmployees(emp));
                                break;

                            case Enums.MenuBankAdmin.EditEmployees:
                                Console.WriteLine("Enter Bank Employee Id");
                                emp.UserId = Console.ReadLine();
                                if (bankEmployeeService.ValidateEmpCredentials(emp.UserId))
                                {
                                    Console.WriteLine("Enter New Employee Name");
                                    emp.UserName = Console.ReadLine();
                                    Console.WriteLine("Enter New Password");
                                    emp.Password = Console.ReadLine();
                                    IsSuccess(bankAdminService.EditEmployees(emp));
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Employee Id");
                                    break;
                                }
                                

                            case Enums.MenuBankAdmin.ApproveTransactions:
                                if (bankAdminService.GetTransactionsToApprove().Any())
                                {
                                    foreach(Transaction txn in bankAdminService.GetTransactionsToApprove())
                                    {
                                        Console.WriteLine("Transaction Number:-" + txn.TransectionNum);
                                        Console.WriteLine("Transaction Amount:-" + txn.TransactionAmount);
                                        Console.WriteLine("Recievers Account Number:-" + txn.RecieversAccNum);
                                        Console.WriteLine("Recievers Bank Id :- " + txn.RecieversBankId);
                                        Console.WriteLine("Senders Account Number:- " + txn.SenderAccNum);
                                        Console.WriteLine("Recievers Bank Id:-" + txn.SendersBankId);
                                        Console.WriteLine("Transaction Type:- " + txn.TransactionType);
                                        Console.WriteLine("Transaction Status :-" + txn.TransactionStatus);
                                    }
                                    
                                    
                                    Console.WriteLine("Enter Transaction Number To Approve");
                                    string txnNum = Console.ReadLine();
                                    if (bankAdminService.ValidateTransactionNumber(txnNum))
                                    {
                                        IsSuccess(bankAdminService.ApproveTransaction(txnNum));
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Transaction Number");
                                        break;
                                    }

                                    
                                }
                                else
                                {
                                    Console.WriteLine("Nothing to Approve");
                                    break;
                                }

                            case Enums.MenuBankAdmin.DisplayTransactionHistory:
                                Console.WriteLine("Enter account Number");
                                string accNum = Console.ReadLine();
                                if (accHolderService.ValidateAccNum(accNum))
                                {
                                    if (commonService.ViewTransectionHistory(accNum, bnkId).Any())
                                    {
                                        foreach (Transaction item in commonService.ViewTransectionHistory(accNum, bnkId))
                                        {
                                            Console.WriteLine("Transaction Number:-" + item.TransectionNum);
                                            Console.WriteLine("Transaction Amount:-" + item.TransactionAmount);
                                            Console.WriteLine("Recievers Account Number:-" + item.RecieversAccNum);
                                            Console.WriteLine("Recievers Bank Id :- " + item.RecieversBankId);
                                            Console.WriteLine("Senders Account Number:- " + item.SenderAccNum);
                                            Console.WriteLine("Recievers Bank Id:-" + item.SendersBankId);
                                            Console.WriteLine("Transaction Type:- " + item.TransactionType);
                                            Console.WriteLine("Transaction Status :-" + item.TransactionStatus);
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("No Transactions Yet");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }

                            case Enums.MenuBankAdmin.DisplayAccountDetails:
                                Console.WriteLine("Enter Account number ");
                                accNum = Console.ReadLine();
                                if (accHolderService.ValidateAccNum(accNum))
                                {
                                    Accounts accountDetail = new Accounts();
                                    accountDetail = commonService.DisplayAccountDetails(accNum, bnkId);
                                    Console.WriteLine("Account Number :-" + accountDetail.AccNumber);
                                    Console.WriteLine("Account Holder Name :-" + accountDetail.UserName);
                                    Console.WriteLine("Account Balance :-" + accountDetail.AccountBalance);
                                    Console.WriteLine("BankId :-" + accountDetail.BankId);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }

                            case Enums.MenuBankAdmin.Exit:
                                exit = true;
                                break;


                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                while (!exit);

            }
        }
    }
}