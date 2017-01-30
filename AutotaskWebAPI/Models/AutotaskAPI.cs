using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web.Services.Protocols;

namespace AutotaskWebAPI.Models
{
    public class TicketDetails
    {
        public long AccountID { get; set; }
        public string Title { get; set; }
        public long id { get;  set; }
        public string Description { get;  set; }
        public int Status { get;  set; }
        public string DueDateTime { get; set; }
        public long AssignedResourceID  { get; set; }
        public long AssignedResourceRoleID { get; set; }
        public long CreatorResourceID { get; set; }
        public int Priority { get; set; }
    }

    /// <summary>
    /// Public Class AutotaskAPI.
    /// </summary>
    public class AutotaskAPI
	{
		public ATWS _atwsServices = null;
        private int utcOffsetInMins = Convert.ToInt32(ConfigurationManager.AppSettings["utcOffsetInMins"]);
        private string _webServiceBaseAPIURL = ConfigurationManager.AppSettings["APIServiceURLZoneInfo"];
        
		/// <summary>
		/// Public Constuctor.
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

        public List<Ticket> GetTicketByCreatorResourceId(string creatorResourceId, out string errorMsg)
        {
            List<Ticket> list = new List<Ticket>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Ticket</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>CreatorResourceID<expression op=\"equals\">");
            strResource.Append(creatorResourceId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Ticket)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get pick list label given its value.
        /// </summary>
        /// <param name="entityType">e.g. Account</param>
        /// <param name="fieldName">e.g. AccountType which follows a picklist.</param>
        /// <param name="valueToSearch">e.g. 1 which should return label "Customer".</param>
        /// <returns>Label matching the passed value.</returns>
        public string GetPickListLabel(string entityType, string fieldName,
                                        string valueToSearch, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                var fields = this._atwsServices.GetFieldInfo(entityType);

                return AutotaskAPI.PickListLabelFromValue(fields, fieldName, valueToSearch);
            }

            catch (SoapException ex)
            {
                errorMsg = ex.Message;

                // This is sort of fatal exception. The entity name or field name or 
                // both are incorrect and might not exist.
                return string.Empty;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;

                // This is sort of fatal exception. The entity name or field name or 
                // both are incorrect and might not exist.
                return string.Empty;
            }
        }

        /// <summary>
        /// Get account given its name. Uses Like search.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Account> GetAccountByName(string accountName, out string errorMsg)
        {
            List<Account> list = new List<Account>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>AccountName<expression op=\"BeginsWith\">");
            strResource.Append(accountName);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Account)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get account given its number.
        /// </summary>
        /// <param name="num">Account Number</param>
        /// <param name="errorMsg">Error message</param>
        /// <returns>List of accounts (actually one account matching the passed number)</returns>
        public List<Account> GetAccountByNumber(string num, out string errorMsg)
        {
            List<Account> list = new List<Account>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>AccountNumber<expression op=\"Like\">");
            strResource.Append(num);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Account)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get account by id
        /// </summary>
        /// <param name="num"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Account> GetAccountById(string id, out string errorMsg)
        {
            List<Account> list = new List<Account>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
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
                    list.Add((Account)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Used to find a specific Field in an array based on the name
        /// </summary>
        /// <param name="field">array containing Fields to search from</param>
        /// <param name="name">contains the name of the Field to search for</param>
        /// <returns>Field match</returns>
        protected static Field FindField(Field[] field, string name)
        {
            return Array.Find(field, element => element.Name == name);
        }

        /// <summary>
        /// Returns the label of a picklist when the value is sent
        /// </summary>
        /// <param name="fields">entity fields</param>
        /// <param name="strField">picklick to choose from</param>
        /// <param name="strPickListValue">value ("id") of picklist</param>
        /// <returns>picklist label</returns>
        protected static string PickListLabelFromValue(Field[] fields, string strField, string strPickListValue)
        {
            string strRet = string.Empty;

            Field fldFieldToFind = FindField(fields, strField);
            if (fldFieldToFind == null)
            {
                throw new Exception("Could not get the " + strField + " field from the collection");
            }
            PickListValue plvValueToFind = FindPickListValue(fldFieldToFind.PicklistValues, strPickListValue);
            if (plvValueToFind != null)
            {
                strRet = plvValueToFind.Label;
            }

            return strRet;
        }

        /// <summary>
        /// Used to find a specific value in a picklist
        /// </summary>
        /// <param name="pickListValue">array of PickListsValues to search from</param>
        /// <param name="valueID">contains the value of the PickListValue to search for</param>
        /// <returns>PickListValue match</returns>
        protected static PickListValue FindPickListValue(PickListValue[] pickListValue, string valueID)
        {
            return Array.Find(pickListValue, element => element.Value == valueID);
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

        public Autotask.Net.Webservices.Invoice FindInvoiceById(string invoiceId)
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

            retInvoice = FindInvoiceById(invoiceId);

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

        public TicketNote CreateTicketNote(long ticketId, string title,
                                        string description, long creatorResourceID,
                                        long noteType, long publish)
        {
            TicketNote retNote = null;

            // Time to create the Note.
            TicketNote noteAct = new TicketNote();

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
                retNote = (TicketNote)respNote.EntityResults[0];
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

        public Ticket CreateTicket(long accountId, string dueDateTime,
                                                            string title,
                                        string description, long creatorResourceID,
                                        long priority, long status,
                                        long assignedResourceID,
                                        long assignedResourceRoleID, out string error)
        {
            error = string.Empty;

            // Time to create the Ticket.
            Ticket ticket = new Autotask.Net.Webservices.Ticket();

            // Bare-minimum number of fields needed to create the ticket.
            ticket.AccountID = accountId;
            ticket.CreatorResourceID = creatorResourceID;
            ticket.Title = title;
            ticket.Description = description;
            ticket.Status = status;
            ticket.Priority = priority;
            ticket.AssignedResourceID = assignedResourceID;
            ticket.AssignedResourceRoleID = assignedResourceRoleID;
            ticket.DueDateTime = Convert.ToDateTime(dueDateTime);

            Entity[] entityToCreate = new Entity[] { ticket };

            ATWSResponse response = this._atwsServices.create(entityToCreate);

            if (response.ReturnCode > 0 && response.EntityResults.Length > 0)
            {
                return (Ticket)response.EntityResults[0];
            }
            else
            {
                if (response != null && response.Errors != null 
                    && response.Errors.Length > 0)
                {
                    error = response.Errors[0].Message;
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Get client portal user contact id.
        /// </summary>
        /// <param name="email">Client Portal username/email.</param>
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

        public List<Ticket> GetTicketByAccountId(string accountId, out string errorMsg)
        {
            List<Ticket> list = new List<Ticket>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Ticket</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>AccountID<expression op=\"equals\">");
            strResource.Append(accountId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Ticket)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get a note by its ticket id.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="errorMsg">Out parameter to set error from AT SOAP API.</param>
        /// <returns></returns>
        public List<TicketNote> GetNoteByTicketId(string ticketId, out string errorMsg)
        {
            List<TicketNote> list = new List<TicketNote>();

            string ret = string.Empty;
            errorMsg = string.Empty;

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
                    list.Add((TicketNote)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<TicketNote> GetNoteById(string id, out string errorMsg)
        {
            List<TicketNote> list = new List<TicketNote>();

            string ret = string.Empty;
            errorMsg = string.Empty;

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
                    list.Add((TicketNote)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Ticket> GetTicketById(string id, out string errorMsg)
        {
            List<Ticket> list = new List<Ticket>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Ticket</entity>");
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
                    list.Add((Ticket)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
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

        public List<TicketNote> GetNoteByLastActivityDate(string lastActivityDate,
                                                          out string errorMsg)
        {
            List<TicketNote> list = new List<TicketNote>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>TicketNote</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LastActivityDate<expression op=\"greaterthan\">");
            strResource.Append(lastActivityDate);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((TicketNote)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Account> GetAccountByLastActivityDate(string lastActivityDate, out string errorMsg)
        {
            List<Account> list = new List<Account>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LastActivityDate<expression op=\"greaterthan\">");
            strResource.Append(lastActivityDate);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Account)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Ticket> GetTicketByLastActivityDate(string lastActivityDate, out string errorMsg)
        {
            List<Ticket> list = new List<Ticket>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Ticket</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LastActivityDate<expression op=\"greaterthan\">");
            strResource.Append(lastActivityDate);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Ticket)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Ticket> GetTicketByAccountIdAndStatus(string accountId, long status, out string errorMsg)
        {
            List<Ticket> list = new List<Ticket>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Ticket</entity>");
            strResource.Append("<query>");
            strResource.Append("<condition><field>Status<expression op=\"Equals\">");
            strResource.Append(status);
            strResource.Append("</expression></field></condition>");
            strResource.Append("<condition><field>AccountID<expression op=\"Equals\">");
            strResource.Append(accountId);
            strResource.Append("</expression></field></condition>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Ticket)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Ticket> GetTicketByAccountIdAndPriority(string accountId, long priority, out string errorMsg)
        {
            List<Ticket> list = new List<Ticket>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Ticket</entity>");
            strResource.Append("<query>");
            strResource.Append("<condition><field>Priority<expression op=\"Equals\">");
            strResource.Append(priority);
            strResource.Append("</expression></field></condition>");
            strResource.Append("<condition><field>AccountID<expression op=\"Equals\">");
            strResource.Append(accountId);
            strResource.Append("</expression></field></condition>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Ticket)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Resource> GetResourceById(string id, out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
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
                    list.Add((Resource)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get resource by email. Uses exact match to passed email address.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByEmail(string email, out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
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
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get resource by user name. Uses exact match to passed user name.
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByUsername(string userName, out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>UserName<expression op=\"equals\">");
            strResource.Append(userName);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public Attachment GetAttachmentById(long id, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                var result = this._atwsServices.GetAttachment(id);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (SoapException ex)
            {
                errorMsg = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Get attachment info by parent id. Usually parent is a ticket.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<AttachmentInfo> GetAttachmentInfoByParentId(string parentId,
                                                                out string errorMsg)
        {
            List<AttachmentInfo> list = new List<AttachmentInfo>();
            errorMsg = string.Empty;

            if (parentId.Length > 0)
            {
                StringBuilder strResource = new StringBuilder();
                strResource.Append("<queryxml version=\"1.0\">");
                strResource.Append("<entity>AttachmentInfo</entity>");
                strResource.Append("<query>");
                strResource.Append("<field>ParentID<expression op=\"equals\">");
                strResource.Append(parentId);
                strResource.Append("</expression></field>");
                strResource.Append("</query></queryxml>");

                ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    foreach (Entity entity in respResource.EntityResults)
                    {
                        list.Add((AttachmentInfo)entity);
                    }
                }
                else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
                {
                    errorMsg = respResource.Errors[0].Message;
                }
            }

            return list;
        }

        /// <summary>
        /// Get attachment info by attach date. 
        /// This is the date at or after which it was attached to its parent.
        /// </summary>
        /// <param name="attachDate"></param>
        /// <returns></returns>
        public List<AttachmentInfo> GetAttachmentInfoByAttachDate(string attachDate,
                                                                out string errorMsg)
        {
            List<AttachmentInfo> list = new List<AttachmentInfo>();
            errorMsg = string.Empty;

            if (attachDate.Length > 0)
            {
                StringBuilder strResource = new StringBuilder();
                strResource.Append("<queryxml version=\"1.0\">");
                strResource.Append("<entity>AttachmentInfo</entity>");
                strResource.Append("<query>");
                strResource.Append("<field>AttachDate<expression op=\"GreaterThanorEquals\">");
                strResource.Append(attachDate);
                strResource.Append("</expression></field>");
                strResource.Append("</query></queryxml>");

                ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    foreach (Entity entity in respResource.EntityResults)
                    {
                        list.Add((AttachmentInfo)entity);
                    }
                }
                else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
                {
                    errorMsg = respResource.Errors[0].Message;
                }
            }

            return list;
        }

        public List<AttachmentInfo> GetAttachmentInfoByParentIdAndAttachDate(string parentId,
                                                                string attachDate,
                                                                out string errorMsg)
        {
            List<AttachmentInfo> list = new List<AttachmentInfo>();
            errorMsg = string.Empty;

            if (attachDate.Length > 0)
            {
                StringBuilder strResource = new StringBuilder();
                strResource.Append("<queryxml version=\"1.0\">");
                strResource.Append("<entity>AttachmentInfo</entity>");
                strResource.Append("<query>");
                strResource.Append("<condition><field>ParentID<expression op=\"Equals\">");
                strResource.Append(parentId);
                strResource.Append("</expression></field></condition>");
                strResource.Append("<condition><field>AttachDate<expression op=\"GreaterThanorEquals\">");
                strResource.Append(attachDate);
                strResource.Append("</expression></field></condition>");
                strResource.Append("</query></queryxml>");

                ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    foreach (Entity entity in respResource.EntityResults)
                    {
                        list.Add((AttachmentInfo)entity);
                    }
                }
                else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
                {
                    errorMsg = respResource.Errors[0].Message;
                }
            }

            return list;
        }

        /// <summary>
        /// Get resource by location id. Uses exact match to passed location id.
        /// Location id is a picklist field.
        /// </summary>
        /// <param name="locationId">Location id of the resource</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByLocationId(string locationId, 
                                                    out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LocationID<expression op=\"equals\">");
            strResource.Append(locationId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get resource by name. Uses 'beginswith' operator to match passed
        /// first name and last name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByName(string firstName, string lastName, 
                                                out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<condition><field>FirstName<expression op=\"beginswith\">");
            strResource.Append(firstName);
            strResource.Append("</expression></field></condition>");
            strResource.Append("<condition><field>LastName<expression op=\"beginswith\">");
            strResource.Append(lastName);
            strResource.Append("</expression></field></condition>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = this._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }
    }
}
