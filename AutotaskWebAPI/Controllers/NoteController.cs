﻿using WrapperLib.Autotask.Net.Webservices;
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
    /// Provides API for TicketNote entity in Autotask.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NoteController : BaseApiController
    {
        /// <summary>
        /// Get TicketNote(s) given parent id (ticket id).
        /// </summary>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [Route("api/notes/ticket/{ticketId:int}")]
        [SwaggerResponse(typeof(List<TicketNote>))]
        [HttpGet]
        public HttpResponseMessage GetByTicketId(long ticketId)
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
        [Route("api/notes/{id:int}")]
        [SwaggerResponse(typeof(TicketNote))]
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
        /// <param name="lastActivityDate">Date in the format yyyy-mm-dd</param>
        /// <returns>Returns all notes which have activity in them after the given date.</returns>
        [Route("api/notes/lastactivitydate/{lastActivityDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [SwaggerResponse(typeof(List<TicketNote>))]
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
    }
}
