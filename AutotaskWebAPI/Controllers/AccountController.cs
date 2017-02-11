using AutotaskWebAPI.Autotask.Net.Webservices;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides API for Account entity in Autotask.
    /// </summary>
    public class AccountController : BaseApiController
    {
        /// <summary>
        /// Get account by name.
        /// </summary>
        /// <param name="name">Account name begins with this parameter.</param>
        /// <returns>All accounts whose names begin with passed name.</returns>
        [Route("api/accounts/{name}")]
        [SwaggerResponse(typeof(List<Account>))]
        public HttpResponseMessage GetByName(string name)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = accountsApi.GetAccountByName(name, out errorMsg);

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
        /// Get account(s) given date of last activity in the account.
        /// </summary>
        /// <param name="lastActivityDate">Date string in the format YYYY-MM-DD. e.g. 2017-01-12</param>
        /// <returns></returns>
        [Route("api/accounts/{lastActivityDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [SwaggerResponse(typeof(List<Account>))]
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

            var result = accountsApi.GetAccountByLastActivityDate(lastActivityDate, out errorMsg);

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
        /// Get account(s) given an account number. Uses 'Like' search.
        /// </summary>
        /// <param name="accountNumber">Account Number</param>
        /// <returns></returns>
        [Route("api/accounts/number/{accountNumber}")]
        [SwaggerResponse(typeof(List<Account>))]
        [HttpGet]
        public HttpResponseMessage GetByNumber(string accountNumber)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = accountsApi.GetAccountByNumber(accountNumber, out errorMsg);

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
        /// Get account given its id. Uses exact match to the passed id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/accounts/{id:int}")]
        [SwaggerResponse(typeof(Account))]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;
            var result = accountsApi.GetAccountById(id, out errorMsg);

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