using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BankLedger.Tests
{
    [TestClass()]
    public class AccountTests
    {
        [TestMethod()]
        public void AccountConstructorTest()
        {
            Account myAccount = new Account("UN", "PW");
            Assert.IsNotNull(myAccount);
        }

        [TestMethod()]
        public void AccountInitializationTest()
        {
            Account myAccount = new Account("UN", "PW");
            Assert.AreEqual("UN", myAccount.GetUsername());
            Assert.IsTrue(myAccount.CheckPassword("PW"));
            Assert.AreEqual(0.0m, myAccount.GetBalance());
            Assert.AreEqual(0, myAccount.GetHistory().Count);
        }

        [TestMethod()]
        public void GetBalanceTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeDeposit(580.08m);
            myAccount.MakeWithdrawal(566.71m);
            Assert.AreEqual(13.37m, myAccount.GetBalance());
        }

        [TestMethod()]
        public void GetHistoryTest()
        {
            Account myAccount = new Account("UN", "PW");
            DateTime start = DateTime.Now;
            myAccount.MakeDeposit(580.08m);
            myAccount.MakeWithdrawal(566.71m);
            DateTime end = DateTime.Now;
            List<Transaction> myHistory = myAccount.GetHistory();
            Assert.AreEqual(2, myHistory.Count);
            Assert.AreEqual(580.08m, myHistory[0].amount);
            Assert.AreEqual(-566.71m, myHistory[1].amount);
            Assert.IsTrue(start <= myHistory[0].time);
            Assert.IsTrue(myHistory[0].time <= myHistory[1].time);
            Assert.IsTrue(myHistory[1].time <= end);
        }

        [TestMethod()]
        public void MakeDepositGoodTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeDeposit(580.08m);
            Assert.AreEqual(580.08m, myAccount.GetBalance());
            List<Transaction> myHistory = myAccount.GetHistory();
            Assert.AreEqual(1, myHistory.Count);
            Assert.AreEqual(580.08m, myHistory[0].amount);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeDepositInvalidZeroTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeDeposit(0.00m);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeDepositInvalidNegativeTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeDeposit(-10.00m);
        }

        [TestMethod()]
        public void MakeWithdrawalGoodTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeDeposit(100.00m);
            myAccount.MakeWithdrawal(60.00m);
            Assert.AreEqual(40.00m, myAccount.GetBalance());
            List<Transaction> myHistory = myAccount.GetHistory();
            Assert.AreEqual(2, myHistory.Count);
            Assert.AreEqual(100.00m, myHistory[0].amount);
            Assert.AreEqual(-60.00m, myHistory[1].amount);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeWithdrawalInvalidTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeWithdrawal(-60.00m);
        }

        [TestMethod()]
        public void MakeWithdrawalOverdraftGoodTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeDeposit(20.00m);
            myAccount.MakeWithdrawal(30.00m);
            Assert.AreEqual(-10.00m, myAccount.GetBalance());
            List<Transaction> myHistory = myAccount.GetHistory();
            Assert.AreEqual(2, myHistory.Count);
            Assert.AreEqual(20.00m, myHistory[0].amount);
            Assert.AreEqual(-30.00m, myHistory[1].amount);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MakeWithdrawalOverdraftInvalidTest()
        {
            Account myAccount = new Account("UN", "PW");
            myAccount.MakeWithdrawal(0.01m);
        }

    }
}