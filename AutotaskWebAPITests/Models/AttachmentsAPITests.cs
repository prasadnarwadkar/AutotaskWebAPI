using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace AutotaskWebAPI.Models.Tests
{
    [TestClass()]
    public class AttachmentsAPITests
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
        public void GetAttachmentByIdTest()
        {
            AttachmentsAPI testInstance = new AttachmentsAPI(api);

            string errorMsg = string.Empty;

            var attachment = testInstance.GetAttachmentById(363, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0
                        && attachment.Info.id == 363);
        }

        [TestMethod()]
        public void GetAttachmentByIdTestInvalidId()
        {
            AttachmentsAPI testInstance = new AttachmentsAPI(api);

            string errorMsg = string.Empty;

            var attachment = testInstance.GetAttachmentById(0, out errorMsg);

            Assert.IsFalse(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAttachmentInfoByParentIdTestInvalidParentId()
        {
            AttachmentsAPI testInstance = new AttachmentsAPI(api);

            string errorMsg = string.Empty;

            var attachment = testInstance.GetAttachmentInfoByParentId(0, out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAttachmentInfoByParentIdTest()
        {
            AttachmentsAPI testInstance = new AttachmentsAPI(api);

            string errorMsg = string.Empty;

            var attachment = testInstance.GetAttachmentInfoByParentId(14146, out errorMsg);

            Assert.IsNotNull(attachment);
            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAttachmentInfoByAttachDateTest()
        {
            AttachmentsAPI testInstance = new AttachmentsAPI(api);

            string errorMsg = string.Empty;

            var attachment = testInstance.GetAttachmentInfoByAttachDate("2017-02-03", out errorMsg);

            Assert.IsNotNull(attachment);
            Assert.IsTrue(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAttachmentInfoByAttachDateTestInvalidDate()
        {
            AttachmentsAPI testInstance = new AttachmentsAPI(api);

            string errorMsg = string.Empty;

            var attachment = testInstance.GetAttachmentInfoByAttachDate("abcdef", out errorMsg);

            Assert.IsFalse(errorMsg.Length == 0);
        }

        [TestMethod()]
        public void GetAttachmentInfoByParentIdAndAttachDateTest()
        {
            AttachmentsAPI testInstance = new AttachmentsAPI(api);

            string errorMsg = string.Empty;

            var attachment = testInstance.GetAttachmentInfoByParentIdAndAttachDate(14141, "2017-02-03", out errorMsg);

            Assert.IsTrue(errorMsg.Length == 0);
        }
    }
}