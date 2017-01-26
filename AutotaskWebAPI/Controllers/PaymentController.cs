using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    public class SubscriptionData
    {
        public short IntervalLength { get; set; }
        public ARBSubscriptionUnitEnum Unit { get; set; }
        public DateTime StartDate { get; set; }
        public short TotalOccurrences { get; set; }
        public short TrialOccurrences { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string BillToFName { get; set; }
        public string BillToLName { get; set; }
        public decimal Amount { get; set; }
        public decimal TrialAmount { get; set; }
        public int AutotaskAccountId { get; set; }
        public string Email { get; set; }
    }   

    public class PaymentController : ApiController
    {
        [HttpGet]
        public bool Pay(string cpId, string cppId, decimal amount, int invoiceId)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = Constants.API_LOGIN_ID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = Constants.TRANSACTION_KEY
            };

            //create a customer payment profile
            customerProfilePaymentType profileToCharge = new customerProfilePaymentType();
            profileToCharge.customerProfileId = cpId;
            profileToCharge.paymentProfile = new paymentProfile { paymentProfileId = cppId };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // refund type
                amount = amount,
                profile = profileToCharge
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the collector that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            //validate
            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                }

                // Update the paid date in AT db as well.
                AutotaskAPI api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                                        ConfigurationManager.AppSettings["APIPassword"]);

                api.UpdateInvoice(invoiceId.ToString());
                return true;
            }
            else if (response != null)
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                }

                return false;
            }

            return false;
        }

        [HttpPost]
        public string CreateSubscription([FromBody]SubscriptionData subscriptionObj)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = Constants.API_LOGIN_ID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = Constants.TRANSACTION_KEY
            };

            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

            interval.length = subscriptionObj.IntervalLength;
            interval.unit = subscriptionObj.Unit;

            paymentScheduleType schedule = new paymentScheduleType
            {
                interval = interval,
                startDate = subscriptionObj.StartDate,      // start date should be tomorrow
                totalOccurrences = subscriptionObj.TotalOccurrences,                          // 999 indicates no end date
                trialOccurrences = subscriptionObj.TrialOccurrences
            };

            #region Payment Information
            var creditCard = new creditCardType
            {
                cardNumber = subscriptionObj.CreditCardNumber,
                expirationDate = subscriptionObj.ExpiryDate
            };

            //standard api call to retrieve response
            paymentType cc = new paymentType { Item = creditCard };
            #endregion

            nameAndAddressType addressInfo = new nameAndAddressType()
            {
                firstName = subscriptionObj.BillToFName,
                lastName = subscriptionObj.BillToLName
            };

            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = subscriptionObj.Amount,
                trialAmount = subscriptionObj.TrialAmount,
                paymentSchedule = schedule,
                billTo = addressInfo,
                payment = cc
            };

            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };

            var controller = new ARBCreateSubscriptionController(request);          // instantiate the contoller that will call the service
            controller.Execute();

            ARBCreateSubscriptionResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)

            //validate
            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response != null && response.messages.message != null)
                {
                    Console.WriteLine("Success, Subscription ID : " + response.subscriptionId.ToString());

                    return response.subscriptionId.ToString();
                }
            }
            else if (response != null)
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                return "Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text;
            }

            return string.Empty;
        }

        private string createCustomerProfileOnAuthorizeNet(CustomerProfile profile)
        {
            string apiLoginId = Constants.API_LOGIN_ID;
            string transactionKey = Constants.TRANSACTION_KEY;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = apiLoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = transactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = profile.CreditCardNumber,
                expirationDate = profile.ExpiryDate
            };

            //standard api call to retrieve response
            paymentType cc = new paymentType { Item = creditCard };

            List<customerPaymentProfileType> paymentProfileList = new List<customerPaymentProfileType>();
            customerPaymentProfileType ccPaymentProfile = new customerPaymentProfileType();
            ccPaymentProfile.payment = cc;

            paymentProfileList.Add(ccPaymentProfile);

            customerProfileType customerProfile = new customerProfileType();
            customerProfile.merchantCustomerId = profile.MerchantCustomerID;
            customerProfile.email = profile.Email;
            customerProfile.paymentProfiles = paymentProfileList.ToArray();

            var request = new createCustomerProfileRequest { profile = customerProfile, validationMode = validationModeEnum.none };

            var controller = new createCustomerProfileController(request);          // instantiate the contoller that will call the service
            controller.Execute();

            createCustomerProfileResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)

            //validate
            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response != null && response.messages.message != null)
                {
                    return response.customerProfileId;
                }
            }
            else if (response != null)
            {
                
                return string.Empty;
            }

            

            return string.Empty;
        }

        // POST api/payment/post/
        [HttpPost]
        public HttpResponseMessage Post([FromBody]CustomerProfile profile)
        {
            if (profile == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No profile was sent.");
            }
            else
            {
                // Todo retrieve merchant cusotmer id from db or from account entity in Autotask.
                // Add profile to our db after creating it on Authorize.net servers.
                
                var result = createCustomerProfileOnAuthorizeNet(profile);

                if (string.IsNullOrEmpty(result))
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }

            }
        }
    }

    public class CustomerProfile
    {
        public string CreditCardNumber { get; set; }
        public string Email { get; set; }
        public string ExpiryDate { get; set; }
        public string MerchantCustomerID { get; set; }
        public int AutotaskContactId { get; set; }
        public bool IsSubscriptionCreate { get; set; }
        public int SubscriptionId { get; set; }
        public int AutotaskResourceId { get; set; }
    }
}
