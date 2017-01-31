﻿using AutotaskWebAPI.Autotask.Net.Webservices;
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