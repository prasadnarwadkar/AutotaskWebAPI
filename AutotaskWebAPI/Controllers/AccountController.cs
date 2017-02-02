using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        /// <param name="name">Account name begins with</param>
        /// <returns>All accounts whose names begin with passed name.</returns>
        [Route("api/account/GetByName/{name}")]
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
        /// <param name="lastActivityDate">Date in the format YYYY-MM-DD. e.g. 2017-01-12</param>
        /// <returns></returns>
        [Route("api/account/GetByLastActivityDate/{lastActivityDate}")]
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
        /// Get account(s) given an account number.
        /// </summary>
        /// <param name="accountNumber">Account Number</param>
        /// <returns></returns>
        [Route("api/account/GetByNumber/{accountNumber}")]
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
        /// Get account(s) given an account id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/account/GetById/{id}")]
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
            var accountId = -1;

            bool parseResult = int.TryParse(id, out accountId);

            if (parseResult)
            {
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

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "id passed is not an integer.");
        }
    }
}