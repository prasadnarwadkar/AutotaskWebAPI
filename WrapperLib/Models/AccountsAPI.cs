using WrapperLib.Autotask.Net.Webservices;
using System.Collections.Generic;
using System.Text;
using WrapperLib.Models;

namespace WrapperLib.Models
{
    /// <summary>
    /// API for Account entity
    /// </summary>
    public class AccountsAPI :ApiBase
    {
        public List<Account> GetAccountByLastActivityDate(string lastActivityDate, out string errorMsg)
        {
            List<Account> list = new List<Account>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LastActivityDate<expression op=\"greaterthan\">");
            strResource.Append(lastActivityDate);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Account)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public AccountsAPI(string user, string password):base(user, password)
        {

        }
        /// <summary>
        /// Get account by id
        /// </summary>
        /// <param name="id">Account id</param>
        /// <param name="errorMsg">Error message from API</param>
        /// <returns></returns>
        public Account GetAccountById(long id, out string errorMsg)
        {
            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(id);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                return (Account)respResource.EntityResults[0];
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;

                return null;
            }

            return null;
        }

        /// <summary>
        /// Get account given its name. Uses BeginsWith search.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Account> GetAccountByName(string accountName, out string errorMsg)
        {
            List<Account> list = new List<Account>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>AccountName<expression op=\"BeginsWith\">");
            strResource.Append(accountName);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Account)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get account given its number.
        /// </summary>
        /// <param name="num">Account Number</param>
        /// <param name="errorMsg">Error message</param>
        /// <returns>List of accounts (actually one account matching the passed number)</returns>
        public List<Account> GetAccountByNumber(string num, out string errorMsg)
        {
            List<Account> list = new List<Account>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Account</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>AccountNumber<expression op=\"Like\">");
            strResource.Append(num);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Account)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }
    }


}