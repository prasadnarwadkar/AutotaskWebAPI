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
    /// Provides Generic API for any entity and any of its fields.
    /// You can use this API for getting any entity by any of its fields.
    /// </summary>
    public class GenericController : BaseApiController
    {
        /// <summary>
        /// Get entity by entity name, field name and field value.
        /// You can use this API for getting any entity by any of its fields.
        /// </summary>
        /// <param name="entityName">Name of the entity e.g. Ticket</param>
        /// <param name="fieldName">Field name. e.g. id</param>
        /// <param name="fieldValue">Field value e.g. 12345</param>
        /// <returns></returns>
        [Route("api/generic/GetByEntityNameFieldNameAndValue/{entityName}/{fieldName}/{fieldValue}")]
        public HttpResponseMessage GetByEntityNameFieldNameAndValue(string entityName, string fieldName, 
                                                                    string fieldValue)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = genericApi.GetEntityByFieldEquals(entityName, fieldName, fieldValue, out errorMsg);

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