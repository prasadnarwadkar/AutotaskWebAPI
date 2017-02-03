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
    public class ContactsAPITests
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
        public void GetContactByEmailTest()
        {
            ContactsAPI testInstance = new ContactsAPI(api);

            string errorMsg = string.Empty;

            var contact = testInstance.GetContactByEmail("pmponzi@tceglobal.com", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContactByEmailTestInvalidEmail()
        {
            ContactsAPI testInstance = new ContactsAPI(api);

            string errorMsg = string.Empty;

            var contact = testInstance.GetContactByEmail("test@example.com", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContactByIdTest()
        {
            ContactsAPI testInstance = new ContactsAPI(api);

            string errorMsg = string.Empty;

            var contact = testInstance.GetContactById(31716773, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContactByIdTestInvalidId()
        {
            ContactsAPI testInstance = new ContactsAPI(api);

            string errorMsg = string.Empty;

            var contact = testInstance.GetContactById(0, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContactByAccountIdTest()
        {
            ContactsAPI testInstance = new ContactsAPI(api);

            string errorMsg = string.Empty;

            var contact = testInstance.GetContactByAccountId(29714238, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContactByAccountIdTestInvalidAccountId()
        {
            ContactsAPI testInstance = new ContactsAPI(api);

            string errorMsg = string.Empty;

            var contact = testInstance.GetContactByAccountId(-1, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContactByNameTest()
        {
            ContactsAPI testInstance = new ContactsAPI(api);

            string errorMsg = string.Empty;

            var contact = testInstance.GetContactByName("Mukumadi", "Tshibuabua", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }
    }
}