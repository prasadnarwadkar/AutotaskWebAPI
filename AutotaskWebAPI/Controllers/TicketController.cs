using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides API for Ticket entity in Autotask.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TicketController : BaseApiController
    {
        /// <summary>
        /// Get Ticket(s) by account id.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [Route("api/tickets/account/{accountId:int}")]
        [SwaggerResponse(typeof(List<Ticket>))]
        [HttpGet]
        public HttpResponseMessage GetByAccountId(long accountId)
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
        [Route("api/tickets/{id:int}")]
        [SwaggerResponse(typeof(Ticket))]
        [HttpGet]
        public HttpResponseMessage GetById(long id)
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
        [Route("api/tickets/creator/{creatorResourceId:int}")]
        [SwaggerResponse(typeof(List<Ticket>))]
        [HttpGet]
        public HttpResponseMessage GetByCreatorResourceId(long creatorResourceId)
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
        [Route("api/tickets/lastactivitydate/{lastActivityDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [SwaggerResponse(typeof(List<Ticket>))]
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
        /// Get tickets by account id and status. e.g. Status of 8 means "In Progress".
        /// </summary>
        /// <param name="accountId">Account id to which the ticket(s) belong.</param>
        /// <param name="status">Status as an integer passed as string. e.g. "8".</param>
        /// <returns>List of tickets.</returns>
        [Route("api/tickets/account/{accountId:int}/status/{status:int}")]
        [SwaggerResponse(typeof(List<Ticket>))]
        [HttpGet]
        public HttpResponseMessage GetByAccountIdAndStatus(long accountId, long status)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = ticketsApi.GetByAccountIdAndStatus(accountId, status, out errorMsg);

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
        /// Get tickets by account id and priority. e.g. Priority of 6 means "Normal".
        /// </summary>
        /// <param name="accountId">Account id to which the ticket(s) belong.</param>
        /// <param name="priority">Priority as an integer e.g. 6.</param>
        /// <returns>List of tickets.</returns>
        [Route("api/tickets/account/{accountId:int}/priority/{priority:int}")]
        [SwaggerResponse(typeof(List<Ticket>))]
        [HttpGet]
        public HttpResponseMessage GetByAccountIdAndPriority(long accountId, long priority)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = ticketsApi.GetByAccountIdAndPriority(accountId, priority, out errorMsg);

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
}
