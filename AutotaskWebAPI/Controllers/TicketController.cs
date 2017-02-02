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
    /// <summary>
    /// Provides API for Ticket entity in Autotask.
    /// </summary>
    public class TicketController : BaseApiController
    {
        /// <summary>
        /// Get Ticket(s) by account id.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [Route("api/ticket/GetByAccountId/{accountId}")]
        [HttpGet]
        public HttpResponseMessage GetByAccountId(string accountId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = ticketsApi.GetTicketByAccountId(accountId, out errorMsg);

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
        /// Get Ticket by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/ticket/GetById/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(string id)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = ticketsApi.GetTicketById(id, out errorMsg);

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
        /// Get Ticket(s) by creator resource id.
        /// </summary>
        /// <param name="creatorResourceId">Id of the resource who created the Ticket(s).</param>
        /// <returns></returns>
        [Route("api/ticket/GetByCreatorResourceId/{creatorResourceId}")]
        [HttpGet]
        public HttpResponseMessage GetByCreatorResourceId(string creatorResourceId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = ticketsApi.GetTicketByCreatorResourceId(creatorResourceId, out errorMsg);

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
        /// Get Ticket(s) which have activity in them after the date passed-in as argument.
        /// </summary>
        /// <param name="lastActivityDate"></param>
        /// <returns></returns>
        [Route("api/ticket/GetByLastActivityDate/{lastActivityDate}")]
        [HttpGet]
        public HttpResponseMessage GetByLastActivityDate(string lastActivityDate)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = ticketsApi.GetTicketByLastActivityDate(lastActivityDate, out errorMsg);

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
        [Route("api/ticket/GetByAccountIdAndStatus/{accountId}/{status}")]
        [HttpGet]
        public HttpResponseMessage GetByAccountIdAndStatus(string accountId, string status)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

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
                    var result = ticketsApi.GetTicketByAccountIdAndStatus(accountId, statusInt, out errorMsg);

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

        /// <summary>
        /// Get tickets by account id and priority. e.g. Priority of 6 means "Normal".
        /// </summary>
        /// <param name="accountId">Account id to which the ticket(s) belong.</param>
        /// <param name="priority">Priority as an integer passed as string. e.g. "6".</param>
        /// <returns>List of tickets.</returns>
        [Route("api/ticket/GetByAccountIdAndPriority/{accountId}/{priority}")]
        [HttpGet]
        public HttpResponseMessage GetByAccountIdAndPriority(string accountId, string priority)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            if (string.IsNullOrEmpty(priority))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Priority cannot be null or empty. Please pass the priority as an integer. e.g. '6'");
            }

            int priorityInt = 0;

            bool parseResult = int.TryParse(priority, out priorityInt);

            if (parseResult)
            {
                int accountIdInt = -1;

                parseResult = int.TryParse(accountId, out accountIdInt);

                if (parseResult)
                {
                    var result = ticketsApi.GetTicketByAccountIdAndPriority(accountId, priorityInt, out errorMsg);

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

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Priority or account id passed is not an integer.");
        }

        /// <summary>
        /// Create a Ticket in Autotask.
        /// </summary>
        /// <param name="details">Details of Ticket to create.</param>
        /// <returns></returns>
        [Route("api/ticket/PostTicket")]
        [HttpPost]
        public HttpResponseMessage PostTicket([FromBody] TicketDetails details)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            try
            {
                if (details == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No ticket details are passed");
                }

                string errorMsg = string.Empty;

                Ticket ticket = ticketsApi.CreateTicket(details.AccountID, details.DueDateTime,
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

        /// <summary>
        /// Update a ticket in Autotask.
        /// </summary>
        /// <param name="details">Details of Ticket to update.</param>
        /// <returns></returns>
        [Route("api/ticket/UpdateTicket")]
        [HttpPost]
        public HttpResponseMessage UpdateTicket([FromBody] TicketUpdateDetails details)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            try
            {
                if (details == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No ticket details are passed");
                }

                string errorMsg = string.Empty;

                var id = ticketsApi.UpdateTicket(details, out errorMsg);

                if (id > 0)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted, id);
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                }
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
