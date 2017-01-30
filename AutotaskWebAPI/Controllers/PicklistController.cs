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
    public class PicklistController : ApiController
    {
        private AutotaskAPI api = null;

        public PicklistController()
        {
            api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"], ConfigurationManager.AppSettings["APIPassword"]);
        }

        [Route("api/picklist/GetLabel/{entityType}/{fieldName}/{valueToSearch}")]
        [HttpGet]
        public HttpResponseMessage GetLabel(string entityType, string fieldName,
                                        string valueToSearch)
        {
            string errorMsg = string.Empty;

            var result = api.GetPickListLabel(entityType, fieldName, valueToSearch, out errorMsg);

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