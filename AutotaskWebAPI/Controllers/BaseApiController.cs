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
        protected ResourcesAPI resourcesApi = null;
        protected TicketsAPI ticketsApi = null;
        protected AccountsAPI accountsApi = null;
        protected NotesAPI notesApi = null;
        protected AttachmentsAPI attachmentsApi = null;
        protected ResourceRolesAPI resourceRolesApi = null;

        public BaseApiController()
        {
            try
            {
                api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                        ConfigurationManager.AppSettings["APIPassword"]);
                apiInitialized = true;
                resourcesApi = new ResourcesAPI(api);
                ticketsApi = new TicketsAPI(api);
                accountsApi = new AccountsAPI(api);
                notesApi = new NotesAPI(api);
                attachmentsApi = new AttachmentsAPI(api);
                resourceRolesApi = new ResourceRolesAPI(api);
            }
            catch (ArgumentException)
            {
                apiInitialized = false;
            }
        }

        [HttpGet]
        public HttpResponseMessage NotInitialized()
        {
            if (!apiInitialized)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API is not initialized. It needs valid API username and password. Please update web.config appSettings keys.");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "Please specify action and parameters.");
            }
        }
    }
}