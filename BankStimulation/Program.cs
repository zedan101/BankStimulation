using BankStimulation.Services;
using BankStimulation.Model;
using System.Security.AccessControl;

namespace BankStimulation
{
    public class Program
    {
        enum MenuAccHolder
        {
            Exit, DepositeMoney, WithdrawMoney, transactMoney, transactionHistory 
        }
        enum MenuBankEmp
        {
            Exit, CreateNewAcc , UpdateBankAccount , UpdateCurrency , UpdateRtgs , UpdateImps , ViewTransactionHistory , RevertTransaction , DisplayAccdetails
        }

        static void Main(string[] args)
        {
            AccountHolderService accHolderService = new AccountHolderService();
            BankEmployeeService bankEmployeeService = new BankEmployeeService();
            BankingService bankingService = new BankingService();
            Console.WriteLine("Bank Stimulation");
            bool exit = false;
            do
            {
                Console.WriteLine("For Bank Employee LogIn Enter 0");
                Console.WriteLine("For Account Holder LogIn Enter 1");
                string input =Console.ReadLine();
                int userInput;
                if(int.TryParse(input,out userInput))
                {
                    if (userInput == 0)
                    {
                        Console.WriteLine("Enter Employee Id");
                        string empId = Console.ReadLine();
                        if (bankEmployeeService.ValidateEmpId(empId))
                        {
                            Console.WriteLine("Enter Password");
                            string empPass = Console.ReadLine();
                            if (bankEmployeeService.ValidateEmpPass(empPass))
                            {
                                UiBankEmployee();
                            }
                            else
                            {
                                Console.WriteLine("Invalid Password");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id");
                        }

                    }
                    else if (userInput == 1)
                    {
                        Console.WriteLine("Enter Employee Id");
                        string accNum = Console.ReadLine();
                        if (accHolderService.ValidateAccNum(accNum))
                        {
                            Console.WriteLine("Enter Password");
                            string accPin = Console.ReadLine();
                            if (accHolderService.ValidateAccPin(accPin))
                            {
                                UiAccountHolder();
                            }
                            else
                            {
                                Console.WriteLine("Invalid Password");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Id");
                        }
                    }
                    else if (userInput == 0)
                    {
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                    Console.WriteLine("\n =========||||||||========== \n");
                }
                else
                {
                    Console.WriteLine("Invalid Input");
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
                    string input = Console.ReadLine();
                    int userInput;
                    if(int.TryParse(input, out userInput))
                    {
                        switch ((MenuAccHolder)userInput)
                        {
                            case MenuAccHolder.DepositeMoney:
                                Console.WriteLine("\n Please enter the Amount");
                                string amtInput = Console.ReadLine();
                                int amount;
                                if (int.TryParse(amtInput, out amount))
                                {
                                    if (accHolderService.DepositeFund(Convert.ToDouble(amount)))
                                    {
                                        Console.WriteLine("Deposite Successful");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unsuccessful");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    break;
                                }

                            case MenuAccHolder.WithdrawMoney:
                                Console.WriteLine("\n Please enter amount to Withdraw");
                                amtInput = Console.ReadLine();
                                if (int.TryParse(amtInput, out amount))
                                {
                                    if (accHolderService.WithdrawFund(amount))
                                    {
                                        Console.WriteLine("Withdraw Successful");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unsuccessful");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Input");
                                    break;
                                }

                            case MenuAccHolder.transactMoney:
                                Console.WriteLine("Enter reciever Bank Id");
                                string rcvBankId = Console.ReadLine();
                                Console.WriteLine("Enter Reciever bank Account Number");
                                string rcvAccNum = Console.ReadLine();
                                Console.WriteLine("Enter Amiount To tranfer");
                                amtInput = Console.ReadLine();
                                if (int.TryParse(amtInput, out amount))
                                {
                                    Console.WriteLine("Enter Transfet Type \t For RTGS->1 \t For IMPS->2 ");
                                    input = Console.ReadLine();
                                    if (int.TryParse(input, out userInput))
                                    {
                                        if (userInput == 1)
                                        {
                                            if(accHolderService.TransferFundsRtgs(amount, rcvAccNum, rcvBankId))
                                            {
                                                Console.WriteLine("Transfer Successful");
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Transfer Unsuccessful");
                                                break;
                                            }   
                                        }
                                        else if (userInput == 2)
                                        {
                                            if(accHolderService.TransferFundImps(amount, rcvAccNum, rcvBankId))
                                            {
                                                Console.WriteLine("Transfer Successful");
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Transfer Unsuccessful");
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please Enter Valid Option");
                                            break;
                                        }
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


                            case MenuAccHolder.transactionHistory:
                                if (accHolderService.ViewTransectionHistory().Any())
                                {
                                    foreach (Transactions item in accHolderService.ViewTransectionHistory())
                                    {
                                        Console.WriteLine("Transaction Number:-" + item.transectionNum);
                                        Console.WriteLine("Transaction Amount:-" + item.transactionAmount);
                                        Console.WriteLine("Recievers Account Number:-" + item.recieversAccNum);
                                        Console.WriteLine("Recievers Bank Id:-" + item.sendersBankId);
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("No Transactions Yet");
                                    break;
                                }
                                

                            case MenuAccHolder.Exit:
                                exit = true;
                                break;

                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
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
                    string input = Console.ReadLine();
                    int userInput;
                    if (int.TryParse(input, out userInput))
                    {
                        switch ((MenuBankEmp)userInput)
                        {
                            case MenuBankEmp.CreateNewAcc:
                                Console.WriteLine("Enter Account Holder Name");
                                string accHldrName = Console.ReadLine();
                                Console.WriteLine("Enter Pin");
                                string accPin = Console.ReadLine();
                                Console.WriteLine("Enter Initial Account balance");
                                string amtInput = Console.ReadLine();
                                int amount;
                                if(int.TryParse(amtInput, out amount))
                                {
                                    if (bankEmployeeService.CreatNewAccount(new AccountHolder()
                                    {
                                        accHolderName = accHldrName,
                                        accNumber = accHldrName.Substring(0, 3) + DateTime.Now.ToString("ddMMyyyy"),
                                        AccountBalance = Convert.ToDouble(amount),
                                        AccPin = accPin,
                                        bankId = BankingService.yesBank.bankId
                                    }))
                                    {
                                        Console.WriteLine("Account Created Successfully");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unsuccessful");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Amount");
                                    break;
                                }
                               

                            case MenuBankEmp.UpdateBankAccount:
                                Console.WriteLine("Enter Account Number of Account To Be Updated");
                                string accNum = Console.ReadLine();
                                if(accHolderService.ValidateAccNum(accNum))
                                {
                                    Console.WriteLine("Enter Updated Name");
                                    accHldrName = Console.ReadLine();
                                    Console.WriteLine("Enter Updated Pin");
                                    accPin = Console.ReadLine();
                                    if(bankEmployeeService.UpdateBankAccount(accNum, accPin, accHldrName))
                                    {
                                        Console.WriteLine("Account Updated Successfully");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unsuccessful");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }
                                

                            case MenuBankEmp.UpdateCurrency:
                                Console.WriteLine("Enter Updated Currency");
                                string updatedCurrency = Console.ReadLine();
                                Console.WriteLine("Enter Updated Exchange rates");
                                string updatedExchangeRates = Console.ReadLine();
                                if(bankEmployeeService.UpdateCurrency(updatedCurrency, updatedExchangeRates))
                                {
                                    Console.WriteLine("Update Successful");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Update Unsuccesful");
                                    break;
                                }

                            case MenuBankEmp.UpdateRtgs:
                                Console.WriteLine("Enter Updated Same RTGS");
                                int updatedSameRtgs; 
                                string sameRtgsInput =Console.ReadLine();
                                if(int.TryParse(sameRtgsInput, out updatedSameRtgs))
                                {
                                    Console.WriteLine("Enter Updated Other RTGS");
                                    int updatedOtherRtgs;
                                    string otherRtgsInput = Console.ReadLine();
                                    if(int.TryParse(otherRtgsInput, out updatedOtherRtgs))
                                    {
                                        if(bankEmployeeService.UpdateRtgs(updatedSameRtgs, updatedOtherRtgs))
                                        {
                                            Console.WriteLine("Update Successful");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Update Unsuccessful");
                                            break;
                                        }
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
                                

                            case MenuBankEmp.UpdateImps:
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
                                        if(bankEmployeeService.UpdateImps(updatedSameImps, updatedOtherImps))
                                        {
                                            Console.WriteLine("Update Successful");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Update Unsuccessful");
                                            break;
                                        }
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
                               
                                

                            case MenuBankEmp.ViewTransactionHistory:
                                Console.WriteLine("Enter account Number");
                                accNum = Console.ReadLine();
                                if(accHolderService.ValidateAccNum(accNum))
                                {
                                    if (bankEmployeeService.ViewTransectionHistory(accNum).Any())
                                    {
                                        foreach(Transactions item in bankEmployeeService.ViewTransectionHistory(accNum))
                                        {
                                            Console.WriteLine("Transaction Number:-" + item.transectionNum);
                                            Console.WriteLine("Transaction Amount:-" + item.transactionAmount);
                                            Console.WriteLine("Recievers Account Number:-" + item.recieversAccNum);
                                            Console.WriteLine("Recievers Bank Id:-" + item.sendersBankId);
                                        }
                                        break;
                                    }
                                    else 
                                    {
                                        Console.WriteLine("No Transactions Yet");
                                        break;
                                    }

                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }
                                

                            case MenuBankEmp.RevertTransaction:
                                Console.WriteLine("Enter Transaction Number");
                                string transactionNum = Console.ReadLine();
                                if (bankingService.ValidateTransactionNumber(transactionNum))
                                {
                                    if (bankEmployeeService.RevertTransection(transactionNum))
                                    {
                                        Console.WriteLine("Transaction Revert Successful");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Revert Unsuccessful");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Transaction Number");
                                    break;
                                }
                                

                            case MenuBankEmp.DisplayAccdetails:
                                Console.WriteLine("Enter Account number ");
                                accNum = Console.ReadLine();
                                if (accHolderService.ValidateAccNum(accNum))
                                {
                                    AccountHolder accountDetail = new AccountHolder();
                                    accountDetail = bankEmployeeService.DisplayAccountDetails(accNum);
                                    Console.WriteLine("Account Number :-" + accountDetail.accNumber);
                                    Console.WriteLine("Account Holder Name :-" + accountDetail.accHolderName);
                                    Console.WriteLine("Account Balance :-" + accountDetail.AccountBalance);
                                    Console.WriteLine("BankId :-" + accountDetail.bankId);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Account Number");
                                    break;
                                }
                                

                            case MenuBankEmp.Exit:
                                exit = true;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
                while (!exit);
            }
        }
    }
}