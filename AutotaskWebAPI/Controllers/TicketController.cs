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
