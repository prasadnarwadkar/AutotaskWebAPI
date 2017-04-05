using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WrapperLib.Autotask.Net.Webservices;

namespace WrapperLib.Models
{
    /// <summary>
    /// Serves as the abstract base class for API classes
    /// for specific entities.
    /// Initializes the instance of the ATWS class on which we 
    /// invoke the query, create and update methods for specific
    /// entities.
    /// </summary>
    public abstract class ApiBase : IDisposable
    {
        protected static bool apiInitialized = false;

        public static ATWS _atwsServices = null;
        private static int utcOffsetInMins = Convert.ToInt32(ConfigurationManager.AppSettings["utcOffsetInMins"]);
        private static string _webServiceBaseAPIURL = ConfigurationManager.AppSettings["APIServiceURLZoneInfo"];

        public ApiBase(string user, string password)
        {
            try
            {
                // Are the username and password set correctly in 
                // app.config appSettings keys?

                if (!apiInitialized)
                {
                    // Initialize AT db context.
                    string zoneURL = string.Empty;

                    _atwsServices = new ATWS();
                    _atwsServices.Url = _webServiceBaseAPIURL;

                    CredentialCache cache = new CredentialCache();
                    cache.Add(new Uri(_atwsServices.Url), "BASIC", new NetworkCredential(user, password));
                    _atwsServices.Credentials = cache;

                    ATWSZoneInfo zoneInfo = new ATWSZoneInfo();
                    zoneInfo = _atwsServices.getZoneInfo(user);

                    if (zoneInfo.ErrorCode >= 0)
                    {
                        zoneURL = zoneInfo.URL;
                        _atwsServices = new ATWS();
                        _atwsServices.Url = zoneInfo.URL;
                        cache = new CredentialCache();
                        cache.Add(new Uri(_atwsServices.Url), "BASIC", new NetworkCredential(user, password));
                        _atwsServices.Credentials = cache;
                    }

                    apiInitialized = true;
                }
            }
            catch (ArgumentException)
            {
                // Autotask API username and/or password might be incorrect.
                // Please check app.config for these keys and their values.
                
                // Other reason for failure could be one of the following: 
                // 1. API service uri cannot be reached on the network.
                // 2. API service uri is incorrect. Please check the Autotask API doc
                //    and Autotask API webpage.

                apiInitialized = false;
            }
            catch (Exception)
            {
                // Autotask API username and/or password might be incorrect.
                // Please check app.config for these keys and their values.

                // Other reason for failure could be one of the following: 
                // 1. API service uri cannot be reached on the network.
                // 2. API service uri is incorrect. Please check the Autotask API doc
                //    and Autotask API webpage.

                apiInitialized = false;
            }
        }

        /// <summary>
        /// Returns true if API is initialized, false otherwise.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsApiInitialized()
        {
            return apiInitialized;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose _atwsServices
                _atwsServices.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
