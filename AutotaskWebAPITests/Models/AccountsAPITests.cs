using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AutotaskWebAPI.Models.Tests
{
    [TestClass()]
    public class AccountsAPITests
    {
        private AutotaskAPI api = null;
        private bool apiInitialized;

        [TestInitialize()]
        public void Init()
        {
            try
            {
                api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                        ConfigurationManager.AppSettings["APIPassword"]);

                apiInitialized = true;
            }
            catch (ArgumentException)
            {
                apiInitialized = false;
            }
        }

        [TestMethod()]
        public void GetAccountByLastActivityDateTest()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountByLastActivityDate("2017-02-01", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAccountByLastActivityDateTestInvalidDateString()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountByLastActivityDate("abcdef", out errorMsg);

            Console.WriteLine(errorMsg);
            Assert.IsTrue(errorMsg.Length > 0);
        }

        [TestMethod()]
        public void GetAccountByIdTest()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountById(0, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAccountByIdTestInvalidId()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountById(12345, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAccountByNameTest()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountByName("standard meat", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAccountByNameTestInvalidName()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountByName("abcdef", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0 &&
                accounts.Count == 0);
        }

        [TestMethod()]
        public void GetAccountByNumberTest()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountByNumber("0", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0
                        && accounts.Count == 1);
        }

        [TestMethod()]
        public void GetAccountByNumberTestInvalidNumber()
        {
            AccountsAPI testInstance = new AccountsAPI(api);

            string errorMsg = string.Empty;

            var accounts = testInstance.GetAccountByNumber("ABCDEF", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0
                        && accounts.Count == 0);
        }
    }
}