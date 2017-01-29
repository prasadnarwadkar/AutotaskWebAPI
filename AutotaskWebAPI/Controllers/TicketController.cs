using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    public class TicketController : ApiController
    {
        private AutotaskAPI api = null;

        public TicketController()
        {
            api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"], ConfigurationManager.AppSettings["APIPassword"]);
        }

        // GET api/ticket/GetByAccountId?accountId={}
        [HttpGet]
        public HttpResponseMessage GetByAccountId(string accountId)
        {
            string errorMsg = string.Empty;

            var result = api.GetTicketByAccountId(accountId, out errorMsg);

            if (errorMsg.Length > 0)
            {
                // There is an error.
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        // GET api/ticket/GetById?id={}
        [HttpGet]
        public HttpResponseMessage GetById(string id)
        {
            string errorMsg = string.Empty;

            var result = api.GetTicketById(id, out errorMsg);

            if (errorMsg.Length > 0)
            {
                // There is an error.
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        // GET api/ticket/GetByCreatorResourceId?creatorResourceId={}
        [HttpGet]
        public HttpResponseMessage GetByCreatorResourceId(string creatorResourceId)
        {
            string errorMsg = string.Empty;

            var result = api.GetTicketByCreatorResourceId(creatorResourceId, out errorMsg);

            if (errorMsg.Length > 0)
            {
                // There is an error.
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetByLastActivityDate(string lastActivityDate)
        {
            string errorMsg = string.Empty;

            var result = api.GetTicketByLastActivityDate(lastActivityDate, out errorMsg);

            if (errorMsg.Length > 0)
            {
                // There is an error.
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        /// <summary>
        /// Get tickets by status. e.g. Status of 8 means "In Progress".
        /// </summary>
        /// <param name="accountId">Account id to which the ticket(s) belong.</param>
        /// <param name="status">Status as an integer passed as string. e.g. "8".</param>
        /// <returns>List of tickets.</returns>
        // GET api/ticket/GetByAccountIdAndStatus?accountId={}&status={}
        [HttpGet]
        public HttpResponseMessage GetByAccountIdAndStatus(string accountId, string status)
        {
            string errorMsg = string.Empty;

            if (string.IsNullOrEmpty(status))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Status cannot be null or empty. Please pass the status as an integer. e.g. '8'");
            }

            int statusInt = 0;

            bool parseResult = int.TryParse(status, out statusInt);

            if (parseResult)
            {
                int accountIdInt = -1;

                parseResult = int.TryParse(accountId, out accountIdInt);

                if (parseResult)
                {
                    var result = api.GetTicketByAccountIdAndStatus(accountId, statusInt, out errorMsg);

                    if (errorMsg.Length > 0)
                    {
                        // There is an error.
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Status or account id passed is not an integer.");
        }

        [HttpPost]
        public HttpResponseMessage PostTicket([FromBody] TicketDetails details)
        {
            try
            {
                if (details == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No ticket details are passed");
                }

                AutotaskAPI api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                                            ConfigurationManager.AppSettings["APIPassword"]);

                string errorMsg = string.Empty;

                Ticket ticket = api.CreateTicket(details.AccountID, details.DueDateTime,
                                                details.Title, details.Description,
                                                details.CreatorResourceID, details.Priority,
                                                details.Status, details.AssignedResourceID,
                                                details.AssignedResourceRoleID, out errorMsg);

                if (ticket != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ticket.id);
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
