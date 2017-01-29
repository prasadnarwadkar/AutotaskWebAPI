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
    public class ResourceController : ApiController
    {
        private AutotaskAPI api = null;

        public ResourceController()
        {
            api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"], ConfigurationManager.AppSettings["APIPassword"]);
        }

        // GET api/resource/GetById?id={}
        [HttpGet]
        public HttpResponseMessage GetById(string id)
        {
            string errorMsg = string.Empty;

            var result = api.GetResourceById(id, out errorMsg);

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

        // GET api/resource/GetByName?firstName={}&lastName={}
        [HttpGet]
        public HttpResponseMessage GetByName(string firstName, string lastName)
        {
            string errorMsg = string.Empty;

            var result = api.GetResourceByName(firstName, lastName, out errorMsg);

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

        // GET api/resource/GetByEmail?email={}
        [HttpGet]
        public HttpResponseMessage GetByEmail(string email)
        {
            string errorMsg = string.Empty;

            var result = api.GetResourceByEmail(email, out errorMsg);

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

        // GET api/resource/GetByUsername?userName={}
        [HttpGet]
        public HttpResponseMessage GetByUsername(string userName)
        {
            string errorMsg = string.Empty;

            var result = api.GetResourceByUsername(userName, out errorMsg);

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

        // GET api/resource/GetByLocationId?locationId={}
        [HttpGet]
        public HttpResponseMessage GetByLocationId(string locationId)
        {
            string errorMsg = string.Empty;

            var result = api.GetResourceByLocationId(locationId, out errorMsg);

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
