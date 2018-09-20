using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger
{
    //I decided to make my own transaction struct instead of using ValueTuples
    //Because I didn't want to worry about NuGet packages for this project
    public struct Transaction
    {
        public DateTime time;
        public Int64 amount;
    }

    public class Account
    {
        /*
            data members:
	        balance
	        transaction history
	        password (hashed)
        */
        private Int64 balance;
        private List<Transaction> history;
        //I'm assuming for sake of this exercise, since it's just a local console app,
        //that proper encryption is outside of the scope of the exercise.
        private string password;


        /*
        functions:
	        private:
		        set balance [integer]
		        add_to_history [string]
	        public:
		        get_balance[]
		        get_history[]
		        make_deposit(checks for value)[+int]
			        -calls set_balance and then add_to_history
		        make_withdrawal (checks for value)[+int]
			        -calls set_balance and then add_to_history
         */
    }
}
