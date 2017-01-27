using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace AutotaskWebAPI.Models
{
    public class ClientPortalDisplayTicket
    {
        // AEMAlertID
        // AllocationCodeID
        // AssignedResourceID
        // AssignedResourceRoleID
        // ContractID
        // Priority
        // Source
        public string Number { get; set; }
        public string AccountID { get; set; }
        public string Title { get; set; }
        public string AccountName { get; set; }
        public string ID { get;  set; }
        public string Description { get;  set; }
        public string RequestType { get;  set; }
        public string Status { get;  set; }
        public string TicketContactName { get;  set; }
        public string CreateDate { get; set; }
        public string DueDateTime { get; set; }
        public string Resolution { get; set; }
        public string CreatorResourceId { get; set; }
    }

    /// <summary>
    /// Public Class DbApi.
    /// </summary>
    public class AutotaskAPI
	{
		public ATWS _atwsServices = null;
        private int utcOffsetInMins = Convert.ToInt32(ConfigurationManager.AppSettings["utcOffsetInMins"]);
        private string _webServiceBaseAPIURL = ConfigurationManager.AppSettings["APIServiceURLZoneInfo"];
        
		/// <summary>
		/// Public Constuctor for API Tests.
		/// </summary>
		public AutotaskAPI(string user, string pass)
		{
            // Initialize db context.
			string zoneURL = string.Empty;

			this._atwsServices = new ATWS();
			this._atwsServices.Url = this._webServiceBaseAPIURL;

			CredentialCache cache = new CredentialCache();
			cache.Add(new Uri(this._atwsServices.Url), "BASIC", new NetworkCredential(user, pass));
			this._atwsServices.Credentials = cache;

            try
            {
                ATWSZoneInfo zoneInfo = new ATWSZoneInfo();
                zoneInfo = this._atwsServices.getZoneInfo(user);
                if (zoneInfo.ErrorCode >= 0)
                {
                    zoneURL = zoneInfo.URL;
                    this._atwsServices = new ATWS();
                    this._atwsServices.Url = zoneInfo.URL;
                    cache = new CredentialCache();
                    cache.Add(new Uri(this._atwsServices.Url), "BASIC", new NetworkCredential(user, pass));
                    this._atwsServices.Credentials = cache;
                }
                else
                {
                    //throw new Exception("Error with getZoneInfo()");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error with getZoneInfo()- error: " + ex.Message);
            }
        }

        public string GetCountryDisplayNameById(int countryId)
        {
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Country</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(countryId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    Country country = (Country)entity;

                    return country.DisplayName.ToString();
                }
            }

            return string.Empty;
         }

        

        public List<NotificationHistory> FindHistoryOfTicketsById(string ticketId)
        {
            List<NotificationHistory> ticketList = new List<NotificationHistory>();

            if (ticketId.Length > 0)
            {
                StringBuilder strResource = new StringBuilder();
                strResource.Append("<queryxml version=\"1.0\">");
                strResource.Append("<entity>NotificationHistory</entity>");
                strResource.Append("<query>");
                strResource.Append("<field>TicketID<expression op=\"equals\">");
                strResource.Append(ticketId);
                strResource.Append("</expression></field>");
                strResource.Append("</query></queryxml>");

                ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    foreach (Entity entity in respResource.EntityResults)
                    {
                        ticketList.Add((NotificationHistory)entity);
                    }
                }
            }

            return ticketList;
        }

        public Autotask.Net.Webservices.Invoice FindInvoice(string invoiceId)
        {
            Autotask.Net.Webservices.Invoice invoice = null;

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

                ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    invoice = (Autotask.Net.Webservices.Invoice)respResource.EntityResults[0];
                }
            }

            return invoice;
        }

        public bool UpdateInvoice(string invoiceId)
        {
            Autotask.Net.Webservices.Invoice retInvoice = null;

            retInvoice = FindInvoice(invoiceId);

            retInvoice.PaidDate = DateTime.Now;



            Entity[] entityArray = new Entity[] { retInvoice };
            ATWSResponse respUpdate = this._atwsServices.update(entityArray);

            if (respUpdate.ReturnCode == -1)
            {
                throw new Exception("Could not update the invoice: " + respUpdate.EntityReturnInfoResults[0].Message);
            }
            if (respUpdate.ReturnCode > 0 && respUpdate.EntityResults.Length > 0)
            {
                retInvoice = (Autotask.Net.Webservices.Invoice)respUpdate.EntityResults[0];
            }

            return true;
        }

        public Autotask.Net.Webservices.TicketNote CreateTicketNote(long ticketId, string title,
                                        string description, long creatorResourceID,
                                        long noteType, long publish)
        {
            Autotask.Net.Webservices.TicketNote retNote = null;

            // Time to create the Note.
            Autotask.Net.Webservices.TicketNote noteAct = new Autotask.Net.Webservices.TicketNote();

            noteAct.TicketID = ticketId;
            noteAct.CreatorResourceID = creatorResourceID;
            noteAct.Title = title;
            noteAct.Description = description;
            noteAct.NoteType = noteType;
            noteAct.Publish = publish;

            Entity[] entNote = new Entity[] { noteAct };

            ATWSResponse respNote = this._atwsServices.create(entNote);
            if (respNote.ReturnCode > 0 && respNote.EntityResults.Length > 0)
            {
                retNote = (Autotask.Net.Webservices.TicketNote)respNote.EntityResults[0];
            }
            else
            {
                if (respNote.EntityReturnInfoResults.Length > 0)
                {
                    throw new Exception("Could not create the Contact: " + respNote.EntityReturnInfoResults[0].Message);
                }
            }

            return retNote;
        }

        /// <summary>
        /// Get account contact id.
        /// </summary>
        /// <param name="email">Client Portal username.</param>
        /// <returns></returns>
        public string FindClientPortalUserContactIdByEmail(string email)
        {
            string ret = string.Empty;

            // Query ClientPortalUser by their email.
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>ClientPortalUser</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>UserName<expression op=\"equals\">");
            strResource.Append(email);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");
            
            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                // Get the Contact ID for the ClientPortalUser
                ret = ((ClientPortalUser)respResource.EntityResults[0]).ContactID.ToString();
            }
            else
            {
                Debug.WriteLine("FindClientPortalUserContactIdByEmail returned: " + respResource.ReturnCode + " " + respResource.Errors);
                ret = "FindClientPortalUserContactIdByEmail returned: " + respResource.ReturnCode + " " + respResource.Errors;
            }
            return ret;
        }

        /// <summary>
        /// Get contact from id
        /// </summary>
        /// <param name="id">Contact id.</param>
        /// <returns>Contact</returns>
        public Autotask.Net.Webservices.Contact FindContactById(string id)
        {
            string ret = string.Empty;

            // Query ClientPortalUser by their email.
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Contact</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(id);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                return ((Autotask.Net.Webservices.Contact)respResource.EntityResults[0]);
            }

            return null;
        }

        public List<Autotask.Net.Webservices.TicketNote> GetNoteByTicketId(string ticketId)
        {
            List<Autotask.Net.Webservices.TicketNote> list = new List<Autotask.Net.Webservices.TicketNote>();

            string ret = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>TicketNote</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>TicketID<expression op=\"equals\">");
            strResource.Append(ticketId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Autotask.Net.Webservices.TicketNote)entity);
                }
            }

            return list;
        }

        public List<Autotask.Net.Webservices.TicketNote> GetNoteById(string id)
        {
            List<Autotask.Net.Webservices.TicketNote> list = new List<Autotask.Net.Webservices.TicketNote>();

            string ret = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>TicketNote</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(id);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Autotask.Net.Webservices.TicketNote)entity);
                }
            }

            return list;
        }

        /// <summary>
        /// Get resource by email id.
        /// </summary>
        /// <param name="email">Resource email id.</param>
        /// <returns></returns>
        public Resource FindResourceIdByEmail(string email)
        {
            // Query Resource by their email.
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>Email<expression op=\"equals\">");
            strResource.Append(email);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                // Get the Id.
                return ((Resource)respResource.EntityResults[0]);
            }

            return null;
        }

        public long CreateAttachment(Attachment attachment)
        {
            return this._atwsServices.CreateAttachment(attachment);
        }

        internal List<InvoiceDto> FindAllInvoicesByCreatorResourceId(int creatorResourceId, string searchString, int daysEarlier)
        {
            throw new NotImplementedException();
        }
    }
}
