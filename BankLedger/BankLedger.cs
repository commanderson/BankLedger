using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLedger
{
    class Ledger
    {
        Account currentLoadedAccount = null;
        List<Account> accountList = new List<Account>();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Chrisbank!");
        }
    }




}
