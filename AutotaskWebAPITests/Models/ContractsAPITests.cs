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
    public class ContractsAPITests
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
        public void GetContractByIdTest()
        {
            ContractsAPI testInstance = new ContractsAPI(api);

            string errorMsg = string.Empty;

            var contract = testInstance.GetContractById(29685870, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContractByIdTestInvalidId()
        {
            ContractsAPI testInstance = new ContractsAPI(api);

            string errorMsg = string.Empty;

            var contract = testInstance.GetContractById(0, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContractByAccountIdTest()
        {
            ContractsAPI testInstance = new ContractsAPI(api);

            string errorMsg = string.Empty;

            var contract = testInstance.GetContractByAccountId(29714238, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContractByAccountIdTestInvalidAccountId()
        {
            ContractsAPI testInstance = new ContractsAPI(api);

            string errorMsg = string.Empty;

            var contract = testInstance.GetContractByAccountId(-1, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContractByContactIdTest()
        {
            ContractsAPI testInstance = new ContractsAPI(api);

            string errorMsg = string.Empty;

            var contract = testInstance.GetContractByContactId(29685869, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContractByContactIdTestInvalidContactId()
        {
            ContractsAPI testInstance = new ContractsAPI(api);

            string errorMsg = string.Empty;

            var contract = testInstance.GetContractByContactId(0, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetContractByOpportunityIdTest()
        {
            ContractsAPI testInstance = new ContractsAPI(api);

            string errorMsg = string.Empty;

            var contract = testInstance.GetContractByOpportunityId(29712482, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }
    }
}