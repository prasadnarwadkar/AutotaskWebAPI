using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace AutotaskWebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        protected AutotaskAPI api = null;
        protected bool apiInitialized = false;

        public BaseApiController()
        {
            try
            {
                api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                        ConfigurationManager.AppSettings["APIPassword"]);
                apiInitialized = true;
            }
            catch (ArgumentException)
            {
                apiInitialized = false;
            }
        }

        [HttpGet]
        public HttpResponseMessage NotInitialized()
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API is not initialized. It needs valid API username and password. Please update web.config appSettings keys.");
        }
    }
}