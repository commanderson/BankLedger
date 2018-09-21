using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger
{
    class Ledger
    {
        static Account CurrentLoadedAccount = null;
        static Dictionary<string, Account> Accounts = new Dictionary<string, Account>();
        
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
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        LoginToAccount();
                        break;
                    case 9:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
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
            Console.WriteLine("Input desired username.");
            string username = Console.ReadLine();
            if (!Accounts.ContainsKey(username))
            {
                Console.WriteLine("No account by the name \"" + username + "\" was found");
                return;
            }

            Console.WriteLine("Enter the password for user \"" + username + "\".");
            string password = Console.ReadLine();
            int attempts = 4;
            while ((!Accounts[username].CheckPassword(password)) && (attempts>0))
            {
                attempts--;
                Console.WriteLine("Incorrect password entered for user \"" + username + "\"; " + attempts + " attempts remaining.");
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
                    case 1:
                        Console.WriteLine("Current balance is $" + CurrentLoadedAccount.GetBalance());
                        break;
                    case 2:
                        
                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    case 9:
                        Console.WriteLine("Logging out \"" + CurrentLoadedAccount.GetUsername() + "\".");
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }

         /*       (w / Loaded account object:)
	        check balance(get_balance)[]
            record a deposit(add_balance)decmial,format]
	        record a withdrawal(subtract_balance) [positive double,format]
	        view transaction history[]
            Logout(unload an account object) []*/
    }
    }




}
