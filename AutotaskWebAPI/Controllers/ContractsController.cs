﻿using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides API for Contract entity in Autotask.
    /// </summary>
    public class ContractController : BaseApiController
    {
        /// <summary>
        /// Get a Contract by its id.
        /// </summary>
        /// <param name="id">Contract id</param>
        /// <returns>Contract</returns>
        [Route("api/contract/GetById/{id}")]
        [SwaggerResponse(typeof(Contract))]
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

            var result = contractsApi.GetContractById(id, out errorMsg);

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
        /// Get Contract(s) given an opportunity id.
        /// </summary>
        /// <param name="opportunityId"></param>
        /// <returns>Contract(s)</returns>
        [Route("api/contract/GetByOpportunityId/{opportunityId}")]
        [SwaggerResponse(typeof(List<Contract>))]
        [HttpGet]
        public HttpResponseMessage GetByOpportunityId(long opportunityId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = contractsApi.GetContractByOpportunityId(opportunityId, out errorMsg);

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
        /// Get contract(s) given a contact id.
        /// </summary>
        /// <param name="contactId">Contact id</param>
        /// <returns>Contract(s)</returns>
        [Route("api/contract/GetByContactId/{contactId}")]
        [SwaggerResponse(typeof(List<Contract>))]
        [HttpGet]
        public HttpResponseMessage GetByContactId(long contactId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = contractsApi.GetContractByContactId(contactId, out errorMsg);

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
        /// Get contract(s) given an account id.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [Route("api/contract/GetByAccountId/{accountId}")]
        [SwaggerResponse(typeof(List<Contract>))]
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

            var result = contractsApi.GetContractByAccountId(accountId, out errorMsg);

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
