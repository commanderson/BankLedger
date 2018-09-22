using System;
using System.Collections.Generic;



namespace BankLedger
{
    class Ledger
    {
        static Account CurrentLoadedAccount = null;
        static Dictionary<string, Account> Accounts = new Dictionary<string, Account>();
        static readonly string CurrencyFormat = "{0:$#,##0.00}";

        static void Main(string[] args)
        {
            int choice = -1;
            while (choice != 9)
            {
                Console.WriteLine("Welcome to Chrisbank!\n"
                + "Enter 1 to create new account.\n"
                + "Enter 2 to Log in to an account.\n"
                + "Enter 9 to Exit");
                String input = Console.ReadLine();
                int.TryParse(input, out choice);
                switch (choice)
                {
                    case 1://create account
                        CreateAccount();
                        break;
                    case 2://login to an account
                        LoginToAccount();
                        break;
                    case 9://exit program
                        Console.WriteLine("Exiting...");
                        break;
                    default://invalid option
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }
        }

        public static void CreateAccount()
        {
            Console.WriteLine("Create an account.");
            Console.WriteLine("Input desired username.");
            string username = Console.ReadLine();
            while (username.Length<=0)//minimal username validation, could be beefed up to whatever requirements we want
            {
                Console.WriteLine("Username can't be empty; input desired username.");
                username = Console.ReadLine();
            }
            while (Accounts.ContainsKey(username))
            {
                Console.WriteLine("That username is taken; input another desired username.");
                username = Console.ReadLine();
            }
            Console.WriteLine("Input desired password.");
            string password = Console.ReadLine();
            while (password.Length <= 0)//minimal password validation, could be beefed up to whatever requirements we want
            {
                Console.WriteLine("Password can't be empty; input desired password.");
                password = Console.ReadLine();
            }
            Accounts.Add(username, new Account(username, password));
            Console.WriteLine("Account for user " + username + " created");
        }

        public static void LoginToAccount()
        {
            Console.WriteLine("Log in to existing account.");
            Console.WriteLine("Input account username.");
            string username = Console.ReadLine();
            if (!Accounts.ContainsKey(username))
            {
                Console.WriteLine("No account by the name \"" + username + "\" was found");
                return;
            }

            Console.WriteLine("Enter the password for user \"" + username + "\".");
            string password = Console.ReadLine();
            int attempts = 3;
            while ((!Accounts[username].CheckPassword(password)) && (attempts>0))
            {
                Console.WriteLine("Incorrect password entered for user \"" + username + "\";\n" + attempts + " attempts remaining.");
                attempts--;
                password = Console.ReadLine();
            }
            if (attempts<1)
            {
                Console.WriteLine("Too many failed pasword attempts; halting login.");
                return;
            }
            CurrentLoadedAccount = Accounts[username];
            try
            {
                LoggedInInteractions();
            }
            // we do not expect to throw exceptions in normal behavior,
            //so this is for debugging as much as anything else
            catch (Exception ex)
            {
                Console.WriteLine("While logged in, encountered exception with message:\n" + ex.Message);
            }
            // since our only form of account protection is access control, 
            //let's at least make sure we clear the loaded account
            finally
            {
                CurrentLoadedAccount = null;
            }
            
        }

        public static void LoggedInInteractions()
        {
            Console.Clear();
            int choice = -1;
            while (choice != 9)
            {
                Console.WriteLine("Welcome user " + CurrentLoadedAccount.GetUsername() + ".\n"
                + "Enter 1 to check balance.\n"
                + "Enter 2 to record a deposit.\n"
                + "Enter 3 to record a withdrawal.\n"
                + "Enter 4 to view transaction history.\n"
                + "Enter 9 to logout");
                String input = Console.ReadLine();
                int.TryParse(input, out choice);
                switch (choice)
                {
                    case 1://check balance
                        Console.WriteLine("Current balance is " + string.Format(CurrencyFormat, CurrentLoadedAccount.GetBalance()));
                        break;
                    case 2://record deposit
                        Console.WriteLine("Enter the amount of the deposit (without dollar sign)");
                        decimal deposit = -1;
                        //we do validate entries here, so exceptions exist to tell us something went wrong
                        //and are not part of normal operations.
                        while((!decimal.TryParse(Console.ReadLine(), out deposit)) || (deposit <= 0.0m))
                        {
                            Console.WriteLine("Please enter a valid, positive decimal monetary quantity \n(without a currency symbol).");
                        }
                        Console.WriteLine("Confirm a deposit of " + string.Format(CurrencyFormat, deposit) + "?");
                        Console.WriteLine("Type y to confirm, anything else to cancel.");
                        String confirmDeposit = Console.ReadLine();
                        if (confirmDeposit == "y")
                        {
                            CurrentLoadedAccount.MakeDeposit(deposit);
                            Console.WriteLine("Deposit of " + string.Format(CurrencyFormat, deposit) + " confirmed.");
                        }
                        break;
                    case 3://record withdrawal
                        if (CurrentLoadedAccount.GetBalance() <=0.0m)
                        {
                            Console.WriteLine("The current account balance is at or below $0.00,\n"
                                + "so you may not make any withdrawals at this time.");
                            break;
                        }
                        Console.WriteLine("Enter the amount of the withdrawal (without dollar sign)");
                        decimal withdrawal = -1;
                        //we do validate entries here, so the exceptions exist to tell us something went wrong
                        //and are not part of normal operations.
                        while ((!decimal.TryParse(Console.ReadLine(), out withdrawal)) || (withdrawal <= 0.0m))
                        {
                            Console.WriteLine("Please enter a valid, positive decimal monetary quantity \n(without a currency symbol).");
                        }
                        Console.WriteLine("Confirm a withdrawal of " + string.Format(CurrencyFormat, withdrawal) + "?");
                        Console.WriteLine("Type y to confirm, anything else to cancel.");
                        String confirmWithdrawal = Console.ReadLine();
                        if (confirmWithdrawal == "y")
                        {
                            CurrentLoadedAccount.MakeWithdrawal(withdrawal);
                            Console.WriteLine("Withdrawal of " + string.Format(CurrencyFormat, withdrawal) + " confirmed.");
                        }
                        break;
                    case 4://view transaction history
                        Console.WriteLine("Transaction history for " + CurrentLoadedAccount.GetUsername() + ":");
                        Console.WriteLine("Timestamp\t\tAmount\tTransaction Type");
                        List<Transaction> myHistory = CurrentLoadedAccount.GetHistory();
                        for (var i = 0; i < myHistory.Count; i++)
                        {
                            Console.Write(myHistory[i].time + "\t" + myHistory[i].amount + "\t");
                            //call it a deposit if it's positive and a withdrawal otherwise
                            Console.WriteLine((myHistory[i].amount > 0.00m) ? "Desposit" : "Withdrawal");
                        }
                        if (myHistory.Count == 0)
                        {
                            Console.WriteLine("(No transactions to display)");
                        }

                        break;
                    case 9://logout
                        Console.WriteLine("Logging out \"" + CurrentLoadedAccount.GetUsername() + "\".");
                        Console.Clear();
                        break;
                    default://invalid option
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }
        }
    }
}
