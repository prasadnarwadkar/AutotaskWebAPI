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
    /// Details of a TicketNote to create it.
    /// </summary>
    public class NoteDetails
    {
        /// <summary>
        /// Ticket id. Parent of the note.
        /// </summary>
        public long TicketId { get; set; }

        /// <summary>
        /// Note creator resource id.
        /// </summary>
        public long CreatorResourceId { get; set; }

        /// <summary>
        /// Note title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Note description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Note type
        /// </summary>
        public long NoteType { get; set; }

        /// <summary>
        /// Publish the note?. Set 1 to publish.
        /// </summary>
        public long Publish { get; set; }
    }

    /// <summary>
    /// Provides API for TicketNote entity in Autotask.
    /// </summary>
    public class NoteController : BaseApiController
    {
        /// <summary>
        /// Get a TicketNote by its parent id (ticket id).
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [Route("api/note/GetByTicketId/{ticketId}")]
        [HttpGet]
        public HttpResponseMessage GetByTicketId(string ticketId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = notesApi.GetNoteByTicketId(ticketId, out errorMsg);

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
        /// Get a TicketNote by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/note/GetById/{id}")]
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
            var result = notesApi.GetNoteById(id, out errorMsg);

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
        /// Get notes which have last activity dates that occur after
        /// the passed-in date.
        /// </summary>
        /// <param name="lastActivityDate"></param>
        /// <returns></returns>
        [Route("api/note/GetByLastActivityDate/{lastActivityDate}")]
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

            var result = notesApi.GetNoteByLastActivityDate(lastActivityDate, out errorMsg);

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
        /// Create a ticket note.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [Route("api/note/PostTicketNote")]
        [HttpPost]
        public HttpResponseMessage PostTicketNote([FromBody] NoteDetails details)
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
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request: Note details passed are null or invalid");
                }

                TicketNote note = notesApi.CreateTicketNote(Convert.ToInt32(details.TicketId), 
                                            details.Title.ToString(),
                                            details.Description.ToString(), Convert.ToInt32(details.CreatorResourceId),
                                            Convert.ToInt32(details.NoteType), Convert.ToInt32(details.Publish));

                if (note != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, note.id);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not create note due to internal server error");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
