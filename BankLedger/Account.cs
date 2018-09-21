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
        public decimal amount;
    }

    public class Account
    {
        //balance is represented as decimal type because it makes formatting intuitive,
        //and the increased precision is appropriate for financial applications.

        private decimal balance;
        private List<Transaction> history;
        //I'm assuming for sake of this exercise, since it's just a local console app,
        //that proper encryption is outside of the scope of the exercise.
        //If I were going to properly encrypt, I'd use something like the method detailed at 
        //http://csharptest.net/470/another-example-of-how-to-store-a-salted-password-hash/
        //to keep a secure password store.
        private string username;
        private string password;

        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.balance = 0.0m;
        }

        //ChangeBalance will be used for both deposits and withdrawals;
        //amounts will be validated in public MakeDeposit and MakeWithdrawal functions
        private void ChangeBalance(decimal amount)
        {
            this.balance += amount;
        }

        private void Add_to_history(Transaction act)
        {
            this.history.Add(act);
        }

        public decimal GetBalance()
        {
            return this.balance;
        }

        public List<Transaction> GetHistory()
        {
            return this.history;
        }

        //MakeDeposit validates for only positive decimals
        public void MakeDeposit(decimal amount)
        {
            if (amount <= 0.0m)
            {
                throw new System.ArgumentException("Deposit amount must be greater than $0.00");
            }
            else
            {
                this.ChangeBalance(amount);
            }
        }

        //MakeWithdrawal validates for only positive decimals 
        //and also prevents withdrawal from an overdrawn account;
        //it will, however, let you overdraw.
        public void MakeWithdrawal(decimal amount)
        {
            if (amount <= 0.0m)
            {
                throw new System.ArgumentException("Withdrawal amount must be greater than $0.00");
            }
            else if (this.balance < 0)
            {
                throw new System.InvalidOperationException("Can't withdraw from an account which is overdrawn!");
            }
            else
            {
                this.ChangeBalance(-1 * amount);
            }
        }
    }
}
