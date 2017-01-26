using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string AccounName { get; set; }
        public string AccountNumber { get; set; }
        public Nullable<int> AccountType { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string AdditionalAddressInformation { get; set; }
        public string BillToAddress1 { get; set; }
        public string BillToAddress2 { get; set; }
        public string BillToAdditionalAddressInformation { get; set; }
        public Nullable<int> BillToAddressToUse { get; set; }
        public string BillToAttention { get; set; }
        public string BillToCity { get; set; }
        public Nullable<int> BillToCountryID { get; set; }
        public string BillToState { get; set; }
        public string BillSoZipCode { get; set; }
        public string AltenatePhone1 { get; set; }
        public string AlternatePhone2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string TaxID { get; set; }
        public Nullable<int> ParentAccountID { get; set; }
        public Nullable<System.DateTime> LastActivityDate { get; set; }
        public string Country { get; set; }
    }
}