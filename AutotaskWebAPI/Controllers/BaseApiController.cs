using AutotaskWebAPI.Models;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
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
        protected ContactsAPI contactsApi = null;
        protected TasksAPI tasksApi = null;
        protected ContractsAPI contractsApi = null;
        protected GenericAPI genericApi = null;

        private static Tuple<string, string> ExtractUserNameAndPassword(string authorizationParameter)
        {
            byte[] credentialBytes;

            try
            {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (String.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            int colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            string userName = decodedCredentials.Substring(0, colonIndex);
            string password = decodedCredentials.Substring(colonIndex + 1);
            return new Tuple<string, string>(userName, password);
        }

        /// <summary>
        /// Base class of All API controllers for various entities in 
        /// Autotask in this Web API.
        /// </summary>
        public BaseApiController()
        {
            try
            {
                string[] authHeaders= HttpContext.Current.Request.Headers.GetValues("Authorization");
                
                if (authHeaders != null &&
                    authHeaders.Count() > 0)
                {
                    // Extract the actual base64 string by stripping off the Basic and space.
                    string authValue = authHeaders[0];
                    int indexOfBasic = authValue.IndexOf("Basic ");
                    authValue = authValue.Substring(indexOfBasic + 6);

                    Tuple<string, string> userNameAndPasword = ExtractUserNameAndPassword(authValue);

                    if (!string.IsNullOrEmpty(userNameAndPasword.Item1)
                        && !string.IsNullOrEmpty(userNameAndPasword.Item2))
                    {
                        // Username and password are available from Request's Authorization header.
                        api = new AutotaskAPI(userNameAndPasword.Item1, userNameAndPasword.Item2);
                    }
                    else
                    {
                        // Are the username and password set in web.config appSettings keys?
                        api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                                ConfigurationManager.AppSettings["APIPassword"]);
                    }

                    apiInitialized = true;

                    resourcesApi = new ResourcesAPI(api);
                    ticketsApi = new TicketsAPI(api);
                    accountsApi = new AccountsAPI(api);
                    notesApi = new NotesAPI(api);
                    attachmentsApi = new AttachmentsAPI(api);
                    resourceRolesApi = new ResourceRolesAPI(api);
                    contactsApi = new ContactsAPI(api);
                    tasksApi = new TasksAPI(api);
                    contractsApi = new ContractsAPI(api);
                    genericApi = new GenericAPI(api);
                }
                else
                {
                    // Are the username and password set in web.config appSettings keys?
                    api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                            ConfigurationManager.AppSettings["APIPassword"]);
                }
            }
            catch (ArgumentException)
            {
                apiInitialized = false;
            }
        }

        /// <summary>
        /// This is the operation that is invoked if API is not initialized because 
        /// 1. API user name and password were not set in web.config.
        /// 2. API user name and password were also not set as part of Request Authorization header.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage NotInitialized()
        {
            if (!apiInitialized)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API is not initialized. It needs valid API username and password. Please update web.config appSettings keys. Alternatively, you can set a valid authorization header with Basic scheme (e.g. Basic xxxxxx) with the request.");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "Please specify action and parameters.");
            }
        }
    }
}