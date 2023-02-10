using BankStimulation.Services;
using BankStimulation.Model;

namespace BankStimulation
{
    internal class Program
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
            Console.WriteLine("Bank Stimulation");
            bool exit = false;
            do
            {
                Console.WriteLine("For Bank Employee LogIn Enter 0");
                Console.WriteLine("For Account Holder LogIn Enter 1");
                int userInput = Int32.Parse(Console.ReadLine());
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
                else if(userInput == 0)
                {
                    exit=true;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
                Console.WriteLine("\n =========||||||||========== \n");
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
                    int userInput = Int32.Parse(Console.ReadLine());
                    switch ((MenuAccHolder)userInput)
                    {
                        case MenuAccHolder.DepositeMoney:
                            Console.WriteLine("\n Please enter the Amount");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            accHolderService.DepositeFund(amount);
                            break;

                        case MenuAccHolder.WithdrawMoney:
                            Console.WriteLine("\n Please enter amount to Withdraw");
                            amount = Convert.ToDouble(Console.ReadLine());
                            accHolderService.WithdrawFund(amount);
                            break;

                        case MenuAccHolder.transactMoney:
                            Console.WriteLine("Enter reciever Bank Id");
                            string rcvBankId = Console.ReadLine();
                            Console.WriteLine("Enter Reciever bank Account Number");
                            string rcvAccNum = Console.ReadLine();
                            Console.WriteLine("Enter Amiount To tranfer");
                            amount = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Transfet Type |t For RTGS->1 \t For IMPS->2 ");
                            userInput = Int32.Parse(Console.ReadLine());
                            if (userInput == 1)
                            {
                                accHolderService.TransferFundsRtgs(amount, rcvAccNum, rcvBankId);
                                break;
                            }
                            else if (userInput == 2)
                            {
                                accHolderService.TransferFundImps(amount, rcvAccNum, rcvBankId);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please Enter Valid Option");
                                break;
                            }

                        case MenuAccHolder.transactionHistory:
                            foreach (Transactions item in accHolderService.ViewTransectionHistory())
                            {
                                Console.WriteLine("Transaction Number:-" + item.transectionNum);
                                Console.WriteLine("Transaction Amount:-" + item.transactionAmount);
                                Console.WriteLine("Recievers Account Number:-" + item.recieversAccNum);
                                Console.WriteLine("Recievers Bank Id:-" + item.sendersBankId);
                            }
                            break;

                        case MenuAccHolder.Exit:
                            exit=true;
                            break;

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
                    int userInput = Int32.Parse(Console.ReadLine());
                    switch((MenuBankEmp)userInput)
                    {
                        case MenuBankEmp.CreateNewAcc:
                            Console.WriteLine("Enter Account Holder Name");
                            string accHldrName = Console.ReadLine();
                            Console.WriteLine("Enter Initial Account balance");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Pin");
                            string accPin = Console.ReadLine();
                            bankEmployeeService.CreatNewAccount(new AccountHolder() {accHolderName = accHldrName, 
                            accNumber = accHldrName.Substring(0,3) + DateTime.Now.ToString("ddMMyyyy") , 
                            AccountBalance=amount , AccPin=accPin});
                            break;

                        case MenuBankEmp.UpdateBankAccount:
                            Console.WriteLine("Enter Account Number of Account To Be Updated");
                            string accNum= Console.ReadLine();
                            Console.WriteLine("Enter Updated Name");
                            accHldrName = Console.ReadLine();
                            Console.WriteLine("Enter Updated Pin");
                            accPin= Console.ReadLine();
                            bankEmployeeService.UpdateBankAccount(accNum, accPin,accHldrName);
                            break;

                        case MenuBankEmp.UpdateCurrency:
                            Console.WriteLine("Enter Updated Currency");
                            string updatedCurrency = Console.ReadLine();
                            Console.WriteLine("Enter Updated Exchange rates");
                            string updatedExchangeRates= Console.ReadLine();
                            bankEmployeeService.UpdateCurrency(updatedCurrency, updatedExchangeRates);
                            break;

                        case MenuBankEmp.UpdateRtgs:
                            Console.WriteLine("Enter Updated Same RTGS");
                            double updatedSameRtgs = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Updated Other RTGS");
                            double updatedOtherRtgs = Convert.ToDouble(Console.ReadLine());
                            bankEmployeeService.UpdateRtgs(updatedSameRtgs, updatedOtherRtgs);
                            break;

                        case MenuBankEmp.UpdateImps:
                            Console.WriteLine("Enter Updated Same IMPS");
                            double updatedSameImps = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Updated Other IMPS");
                            double updatedOtherImps = Convert.ToDouble(Console.ReadLine());
                            bankEmployeeService.UpdateImps(updatedSameImps, updatedOtherImps);
                            break;

                        case MenuBankEmp.ViewTransactionHistory:
                            Console.WriteLine("Enter account Number");
                            accNum= Console.ReadLine();
                            bankEmployeeService.ViewTransectionHistory(accNum);
                            break;

                        case MenuBankEmp.RevertTransaction:
                            Console.WriteLine("Enter Transaction Number");
                            string transactionNum = Console.ReadLine();
                            bankEmployeeService.RevertTransection(transactionNum);
                            break;

                        case MenuBankEmp.DisplayAccdetails:
                            Console.WriteLine("Enter Account number ");
                            accNum= Console.ReadLine();
                            AccountHolder accountDetail = new AccountHolder();
                            accountDetail = bankEmployeeService.DisplayAccountDetails(accNum);
                            Console.WriteLine("Account Number :-" + accountDetail.accNumber);
                            Console.WriteLine("Account Holder Name :-" + accountDetail.accHolderName);
                            Console.WriteLine("Account Balance :-" + accountDetail.AccountBalance);
                            Console.WriteLine("BankId :-" + accountDetail.bankId);
                            break;

                        case MenuBankEmp.Exit:
                            exit = true;
                            break;
                    }
                }
                while (!exit);
            }
        }
    }
}