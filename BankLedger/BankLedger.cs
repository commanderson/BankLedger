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
        static Hashtable Accounts = new Hashtable();
        
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
                        Console.WriteLine("Log in to existing account");
                        break;
                    case 9:
                        Console.WriteLine("Exiting");
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            }
            
            /*
             Create a new account [username, password]
	        Login (load an account object) [username, password]
	        (w/Loaded account object:)
		        check balance (get_balance)[]
		        record a deposit (add_balance)[positive double,format]
		        record a withdrawal (subtract_balance) [positive double,format]
		        view transaction history []
		        Logout(unload an account object) []
             */
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
    }




}
