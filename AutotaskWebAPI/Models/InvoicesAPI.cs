using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class InvoicesAPI
    {
        private AutotaskAPI api = null;

        public InvoicesAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        public Invoice FindInvoiceById(string invoiceId)
        {
            Invoice invoice = null;

            if (invoiceId.Length > 0)
            {
                // Query Contact to see if the contact is already in the system
                StringBuilder strResource = new StringBuilder();
                strResource.Append("<queryxml version=\"1.0\">");
                strResource.Append("<entity>Invoice</entity>");
                strResource.Append("<query>");
                strResource.Append("<field>id<expression op=\"equals\">");
                strResource.Append(invoiceId);
                strResource.Append("</expression></field>");
                strResource.Append("</query></queryxml>");

                ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    invoice = (Invoice)respResource.EntityResults[0];
                }
            }

            return invoice;
        }

        public bool UpdateInvoice(string invoiceId)
        {
            Invoice retInvoice = null;

            retInvoice = FindInvoiceById(invoiceId);

            retInvoice.PaidDate = DateTime.Now;

            Entity[] entityArray = new Entity[] { retInvoice };
            ATWSResponse respUpdate = api._atwsServices.update(entityArray);

            if (respUpdate.ReturnCode == -1)
            {
                throw new Exception("Could not update the invoice: " + respUpdate.EntityReturnInfoResults[0].Message);
            }
            if (respUpdate.ReturnCode > 0 && respUpdate.EntityResults.Length > 0)
            {
                retInvoice = (Invoice)respUpdate.EntityResults[0];
            }

            return true;
        }
    }
}