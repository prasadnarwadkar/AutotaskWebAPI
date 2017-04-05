using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WrapperLib.Models;

namespace AutotaskWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public abstract class BaseApiController : ApiController
    {
        protected static bool apiInitialized = false;

        protected static ResourcesAPI resourcesApi = null;
        protected static TicketsAPI ticketsApi = null;
        protected static AccountsAPI accountsApi = null;
        protected static NotesAPI notesApi = null;
        protected static AttachmentsAPI attachmentsApi = null;
        protected static ResourceRolesAPI resourceRolesApi = null;
        protected static ContactsAPI contactsApi = null;
        protected static TasksAPI tasksApi = null;
        protected static ContractsAPI contractsApi = null;
        protected static GenericAPI genericApi = null;
        protected static PicklistAPI picklistApi = null;

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
            string username = string.Empty;
            string password = string.Empty;

            try
            {
                if (!apiInitialized)
                {
                    string[] authHeaders = HttpContext.Current.Request.Headers.GetValues("Authorization");

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
                            username = userNameAndPasword.Item1;
                            password = userNameAndPasword.Item2;
                        }
                        else
                        {
                            // Are the username and password set in web.config appSettings keys?
                            username = ConfigurationManager.AppSettings["APIUsername"];
                            password = ConfigurationManager.AppSettings["APIPassword"];
                        }

                        resourcesApi = new ResourcesAPI(username, password);

                        if (!resourcesApi.IsApiInitialized())
                        {
                            apiInitialized = false;
                        }
                        else
                        {
                            apiInitialized = true;
                        }

                        ticketsApi = new TicketsAPI(username, password);
                        accountsApi = new AccountsAPI(username, password);
                        notesApi = new NotesAPI(username, password);
                        attachmentsApi = new AttachmentsAPI(username, password);
                        resourceRolesApi = new ResourceRolesAPI(username, password);
                        contactsApi = new ContactsAPI(username, password);
                        tasksApi = new TasksAPI(username, password);
                        contractsApi = new ContractsAPI(username, password);
                        genericApi = new GenericAPI(username, password);
                        picklistApi = new PicklistAPI(username, password);
                    }
                    else
                    {
                        // Are the username and password set in web.config appSettings keys?
                        username = ConfigurationManager.AppSettings["APIUsername"];
                        password = ConfigurationManager.AppSettings["APIPassword"];
                    }
                }
                else
                {
                    resourcesApi = new ResourcesAPI(username, password);

                    if (!resourcesApi.IsApiInitialized())
                    {
                        apiInitialized = false;
                    }
                    else
                    {
                        apiInitialized = true;
                    }

                    ticketsApi = new TicketsAPI(username, password);
                    accountsApi = new AccountsAPI(username, password);
                    notesApi = new NotesAPI(username, password);
                    attachmentsApi = new AttachmentsAPI(username, password);
                    resourceRolesApi = new ResourceRolesAPI(username, password);
                    contactsApi = new ContactsAPI(username, password);
                    tasksApi = new TasksAPI(username, password);
                    contractsApi = new ContractsAPI(username, password);
                    genericApi = new GenericAPI(username, password);
                    picklistApi = new PicklistAPI(username, password);
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API is not initialized. It needs valid API username and password. Please ensure you have set a valid authorization header on the request.");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "Please specify action and parameters.");
            }
        }
    }
}