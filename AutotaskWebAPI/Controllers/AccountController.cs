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
    public class AccountController : ApiController
    {
        private AutotaskAPI api = null;

        public AccountController()
        {
            api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"], 
                                    ConfigurationManager.AppSettings["APIPassword"]);
        }

        // GET api/account/GetByName?name={}
        [HttpGet]
        public HttpResponseMessage GetByName(string name)
        {
            string errorMsg = string.Empty;

            var result = api.GetAccountByName(name, out errorMsg);

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

        // GET api/account/GetByLastActivityDate?lastActivityDate={}
        [HttpGet]
        public HttpResponseMessage GetByLastActivityDate(string lastActivityDate)
        {
            string errorMsg = string.Empty;

            var result = api.GetAccountByLastActivityDate(lastActivityDate, out errorMsg);

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

        // GET api/account/GetByNumber?accountNumber={}
        [HttpGet]
        public HttpResponseMessage GetByNumber(string accountNumber)
        {
            string errorMsg = string.Empty;

            var result = api.GetAccountByNumber(accountNumber, out errorMsg);

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

        // GET api/account/GetById?id={}
        [HttpGet]
        public HttpResponseMessage GetById(string id)
        {
            string errorMsg = string.Empty;
            var accountId = -1;

            bool parseResult = int.TryParse(id, out accountId);

            if (parseResult)
            {
                var result = api.GetAccountById(id, out errorMsg);

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