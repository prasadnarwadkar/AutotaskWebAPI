using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class TicketsAPI
    {
        private AutotaskAPI api = null;

        public TicketsAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        public long UpdateTicket(TicketUpdateDetails ticketToUpdate, out string errorMsg)
        {
            try
            {
                errorMsg = string.Empty;

                if (ticketToUpdate == null)
                {
                    throw new ArgumentNullException("Ticket to Update is null.");
                }

                if (ticketToUpdate.ID < 1)
                {
                    throw new ArgumentException("Id of the ticket to update is invalid.");
                }

                var tickets = GetTicketById(ticketToUpdate.ID, out errorMsg);

                if (errorMsg.Length == 0)
                {
                    var ticket = tickets[0];

                    ticket.AccountID = ticketToUpdate.AccountID;
                    ticket.AllocationCodeID = ticketToUpdate.AllocationCodeID;
                    ticket.AssignedResourceID = ticketToUpdate.AssignedResourceID;
                    ticket.AssignedResourceRoleID = ticketToUpdate.AssignedResourceRoleID;
                    ticket.ContactID = ticketToUpdate.ContactID;
                    ticket.ContractID = ticketToUpdate.ContractID;
                    ticket.Description = ticketToUpdate.Description;
                    ticket.DueDateTime = ticketToUpdate.DueDateTime;
                    ticket.EstimatedHours = ticketToUpdate.EstimatedHours;
                    ticket.IssueType = ticketToUpdate.IssueType;
                    ticket.OpportunityId = ticketToUpdate.OpportunityId;
                    ticket.QueueID = ticketToUpdate.QueueID;
                    ticket.Resolution = ticketToUpdate.Resolution;
                    ticket.ProblemTicketId = ticketToUpdate.ProblemTicketId;
                    ticket.PurchaseOrderNumber = ticketToUpdate.PurchaseOrderNumber;
                    ticket.ServiceLevelAgreementID = ticketToUpdate.ServiceLevelAgreementID;
                    ticket.Source = ticketToUpdate.Source;
                    ticket.Status = ticketToUpdate.Status;
                    ticket.SubIssueType = ticketToUpdate.SubIssueType;
                    ticket.TicketType = ticketToUpdate.TicketType;
                    ticket.Title = ticketToUpdate.Title;

                    ticket.InstalledProductID = ticketToUpdate.InstalledProductID;
                    ticket.ChangeApprovalBoard = ticketToUpdate.ChangeApprovalBoard;
                    ticket.ChangeApprovalStatus = ticketToUpdate.ChangeApprovalStatus;
                    ticket.ChangeApprovalType = ticketToUpdate.ChangeApprovalType;
                    ticket.ChangeInfoField1 = ticketToUpdate.ChangeInfoField1;
                    ticket.ChangeInfoField2 = ticketToUpdate.ChangeInfoField2;
                    ticket.ChangeInfoField3 = ticketToUpdate.ChangeInfoField3;
                    ticket.ChangeInfoField4 = ticketToUpdate.ChangeInfoField4;
                    ticket.ChangeInfoField5 = ticketToUpdate.ChangeInfoField5;

                    ticket.Priority = ticketToUpdate.Priority;
                    ticket.ContractServiceID = ticketToUpdate.ContractServiceID;
                    ticket.ContractServiceBundleID = ticketToUpdate.ContractServiceBundleID;

                    Entity[] entityArray = new Entity[] { ticket };
                    ATWSResponse respUpdate = api._atwsServices.update(entityArray);

                    if (respUpdate.ReturnCode > 0 && respUpdate.EntityResults.Length > 0)
                    {
                        return ((Ticket)respUpdate.EntityResults[0]).id;
                    }
                    else if (respUpdate.Errors != null &&
                            respUpdate.Errors.Length > 0)
                    {
                        errorMsg = respUpdate.Errors[0].Message;

                        return 0;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return 0;
            }
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

            ATWSResponse response = api._atwsServices.create(entityToCreate);

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

        public List<Ticket> GetByAccountIdAndPriority(long accountId, long priority, out string errorMsg)
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

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

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

        public List<Ticket> GetTicketByAccountId(long accountId, out string errorMsg)
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

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

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

        public List<Ticket> GetByAccountIdAndStatus(long accountId, long status, out string errorMsg)
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

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

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

        public List<Ticket> GetTicketByCreatorResourceId(long creatorResourceId, out string errorMsg)
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

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

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

        public List<Ticket> GetTicketById(long id, out string errorMsg)
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

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

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

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

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
    }


}