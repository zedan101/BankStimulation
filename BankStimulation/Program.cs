using BankStimulation.Services;
using BankStimulation.Model;
namespace BankStimulation
{
    public class Program
    {
        static void Main(string[] args)
        {
            AccountHolderService accHolderService = new AccountHolderService();
            BankEmployeeService bankEmployeeService = new BankEmployeeService();
            BankingService bankingService = new BankingService();
            Console.WriteLine("Bank Stimulation");
            bool exit = false;
            do
            {
                Console.WriteLine("To exist enter " + (int)Enums.LogInType.Exit);
                Console.WriteLine("For Bank Employee LogIn Enter " + (int)Enums.LogInType.BankEmployeeLogIn);
                Console.WriteLine("For Account Holder LogIn Enter " + (int)Enums.LogInType.AccountHolderLogIn);
                try 
                {
                    int input;
                    if(int.TryParse(Console.ReadLine(),out input))
                    {
                        if (Enums.LogInType.BankEmployeeLogIn == (Enums.LogInType)input)
                        {
                            Console.WriteLine("Enter Employee Id");
                            string empId = Console.ReadLine();
                            Console.WriteLine("Enter Password");
                            string empPass = Console.ReadLine();
                            if (bankEmployeeService.ValidateEmpCredentials(empId, empPass))
                            {
                                UiBankEmployee();
                            }
                            else
                            {
                                Console.WriteLine("Invalid Credentials");

                            }

                        }
                        else if (Enums.LogInType.AccountHolderLogIn == (Enums.LogInType)input)
                        {
                            Console.WriteLine("Enter Account Number");
                            string accNum = Console.ReadLine();
                            Console.WriteLine("Enter Password");
                            string accPin = Console.ReadLine();
                            if (accHolderService.ValidateAccHolder(accPin , accNum))
                            {
                                UiAccountHolder();
                            }
                            else
                            {
                                Console.WriteLine("Invalid Credentials");
                            }
                        }
                        else if (Enums.LogInType.Exit == (Enums.LogInType)input)
                        {
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input");
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

            void UiAccountHolder()
            {
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
                                    if (GlobalDataStorage.AcceptedCurrency.Any(e => e.AcceptedCurrency == currency))
                                    {
                                        IsSuccess(accHolderService.DepositeFund(amount * GlobalDataStorage.AcceptedCurrency.FirstOrDefault(e => e.AcceptedCurrency == currency).ExchangeRate));
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
                                if (accHolderService.ViewTransectionHistory().Any())
                                {
                                    foreach (Transaction item in accHolderService.ViewTransectionHistory())
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
            void UiBankEmployee()
            {
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
                                if(GlobalDataStorage.AcceptedCurrency.Any(e => e.AcceptedCurrency == currency))
                                {
                                    if (double.TryParse(amtInput, out amount) && amount>0)
                                    {
                                        accHolder.AccNumber = accHolder.UserName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy");
                                        accHolder.AccountBalance = amount * GlobalDataStorage.AcceptedCurrency.FirstOrDefault(e => e.AcceptedCurrency == currency).ExchangeRate;
                                        accHolder.BankId = GlobalDataStorage.yesBank.BankId;
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
                                    if (bankEmployeeService.ViewTransectionHistory(accNum).Any())
                                    {
                                        foreach(Transaction item in bankEmployeeService.ViewTransectionHistory(accNum))
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
                                    accountDetail = bankEmployeeService.DisplayAccountDetails(accNum);
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
        }
    }
}