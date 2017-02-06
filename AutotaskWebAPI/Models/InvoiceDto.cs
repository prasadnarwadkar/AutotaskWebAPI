using System;

namespace AutotaskWebAPI.Models
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int AccountID { get; set; }
        public int BatchID { get; set; }
        public Nullable<System.DateTime> CreateDatetime { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> InvoiceDatetime { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<System.DateTime> Duedate { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public Nullable<decimal> InvoiceTotal { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<decimal> TotalTaxValue { get; set; }
        public Nullable<bool> Isvoided { get; set; }
        public Nullable<System.DateTime> VoidedDate { get; set; }
        public Nullable<System.DateTime> WebServiceDate { get; set; }
        public Nullable<int> InvoiceEditorTemplateID { get; set; }
        public string Customer { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public Nullable<int> ANetSubscriptionId { get; set; }
        public Nullable<int> CreatorResourceId { get; set; }
    }
}