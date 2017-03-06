using AutotaskWebAPI.Autotask.Net.Webservices;
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
    /// Provides API for Contact entity in Autotask.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactController : BaseApiController
    {
        /// <summary>
        /// Get a contact by id.
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>Contact</returns>
        [Route("api/contacts/{id:int}")]
        [SwaggerResponse(typeof(Contact))]
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

            var result = contactsApi.GetContactById(id, out errorMsg);

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
        /// Get a contact given first name and last name.
        /// Uses 'beginswith' search.
        /// </summary>
        /// <param name="firstName">Contact first name</param>
        /// <param name="lastName">Contact last name</param>
        /// <returns>Contact(s)</returns>
        [Route("api/contacts/name/{firstName}/{lastName}")]
        [SwaggerResponse(typeof(List<Contact>))]
        [HttpGet]
        public HttpResponseMessage GetByName(string firstName, string lastName)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = contactsApi.GetContactByName(firstName, lastName, out errorMsg);

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
        /// Get a contact given email address.
        /// Uses exact match to the email address passed in.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("api/contacts/email")]
        [SwaggerResponse(typeof(List<Contact>))]
        [HttpGet]
        public HttpResponseMessage GetByEmail(string email)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = contactsApi.GetContactByEmail(email, out errorMsg);

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
        /// Get contact(s) given account id.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>Contact(s)</returns>
        [Route("api/contacts/account/{accountId:int}")]
        [SwaggerResponse(typeof(List<Contact>))]
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

            var result = contactsApi.GetContactByAccountId(accountId, out errorMsg);

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
