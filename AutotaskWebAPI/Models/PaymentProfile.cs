using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class PaymentProfile
    {
        public string PaymentProfileId { get; set; }
        
        public creditCardMaskedType CCItem { get; set; }
        public bankAccountMaskedType BankAcctItem { get; set; }
        public string CustomerProfileId { get; set; }
    }

    public class CustomerProfileDto
    {
        public string Email { get; set; }
        public string MerchantCustomerID { get; set; }
        public string CustomerProfileId { get; set; }
        public List<PaymentProfile> PaymentProfiles { get; set; }
        public bool ProfileCreatedOnAuthorizeNet { get; set; }
        public string ANetCustomerProfileId { get; set; }
        public int AutotaskAccountId { get; set; }
        public bool IsSubscriptionCreate { get; set; }
        public int SubscriptionId { get; set; }
        public Nullable<int> AutotaskContactId { get; set; }
        public Nullable<int> AutotaskResourceId { get; set; }
    }
}