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
    /// Provides API for Picklist in Autotask.
    /// </summary>
    public class PicklistController : BaseApiController
    {
        /// <summary>
        /// Get a picklist label given an entity name, field name and 
        /// value to search.
        /// </summary>
        /// <param name="entityType">e.g. Ticket</param>
        /// <param name="fieldName">e.g. Status</param>
        /// <param name="valueToSearch">String representation of an object value. e.g. "8"</param>
        /// <returns></returns>
        [Route("api/picklist/GetLabel/{entityType}/{fieldName}/{valueToSearch}")]
        [HttpGet]
        public HttpResponseMessage GetLabel(string entityType, string fieldName,
                                        string valueToSearch)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            if (string.IsNullOrEmpty(entityType))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Entity Type is null or empty.");
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Field name is null or empty.");
            }

            if (valueToSearch == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value to search is null or empty.");
            }

            string errorMsg = string.Empty;

            var result = api.GetPickListLabel(entityType, fieldName, valueToSearch.ToString(), out errorMsg);

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
        /// Get all picklist labels given an entity name and field name.
        /// </summary>
        /// <param name="entityType">Ticket</param>
        /// <param name="fieldName">Status</param>
        /// <returns>All status labels to display which represent integer values.</returns>
        [Route("api/picklist/GetLabels/{entityType}/{fieldName}")]
        [HttpGet]
        public HttpResponseMessage GetLabels(string entityType, string fieldName)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            if (string.IsNullOrEmpty(entityType))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Entity Type is null or empty.");
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Field name is null or empty.");
            }

            string errorMsg = string.Empty;

            var result = api.GetPickListLabelsByField(entityType, fieldName, out errorMsg);

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