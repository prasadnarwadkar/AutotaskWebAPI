using WrapperLib.Autotask.Net.Webservices;
using System.Collections.Generic;
using System.Text;

namespace WrapperLib.Models
{
    /// <summary>
    /// API for Contact entity.
    /// </summary>
    public class ContactsAPI : ApiBase
    {
        /// <summary>
        /// Get contact by email. Uses exact match to passed email address.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Contact> GetContactByEmail(string email, out string errorMsg)
        {
            List<Contact> list = new List<Contact>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContact = new StringBuilder();
            strContact.Append("<queryxml version=\"1.0\">");
            strContact.Append("<entity>Contact</entity>");
            strContact.Append("<query>");
            strContact.Append("<field>EMailAddress<expression op=\"equals\">");
            strContact.Append(email);
            strContact.Append("</expression></field>");
            strContact.Append("</query></queryxml>");

            ATWSResponse respContact = _atwsServices.query(strContact.ToString(), out errorMsg);

            if (respContact.ReturnCode > 0 && respContact.EntityResults.Length > 0)
            {
                foreach (Entity entity in respContact.EntityResults)
                {
                    list.Add((Contact)entity);
                }
            }
            else if (respContact.Errors != null &&
                    respContact.Errors.Length > 0)
            {
                errorMsg = respContact.Errors[0].Message;
            }

            return list;
        }

        public Contact GetContactById(long id, out string errorMsg)
        {
            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContact = new StringBuilder();
            strContact.Append("<queryxml version=\"1.0\">");
            strContact.Append("<entity>Contact</entity>");
            strContact.Append("<query>");
            strContact.Append("<field>id<expression op=\"equals\">");
            strContact.Append(id);
            strContact.Append("</expression></field>");
            strContact.Append("</query></queryxml>");

            ATWSResponse respContact = _atwsServices.query(strContact.ToString(), out errorMsg);

            if (respContact.ReturnCode > 0 && respContact.EntityResults.Length > 0)
            {
                return (Contact)respContact.EntityResults[0];
            }
            else if (respContact.Errors != null &&
                    respContact.Errors.Length > 0)
            {
                errorMsg = respContact.Errors[0].Message;

                return null;
            }

            return null;
        }

        public List<Contact> GetContactByAccountId(long accountId, out string errorMsg)
        {
            List<Contact> list = new List<Contact>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContact = new StringBuilder();
            strContact.Append("<queryxml version=\"1.0\">");
            strContact.Append("<entity>Contact</entity>");
            strContact.Append("<query>");
            strContact.Append("<field>AccountID<expression op=\"equals\">");
            strContact.Append(accountId);
            strContact.Append("</expression></field>");
            strContact.Append("</query></queryxml>");

            ATWSResponse respContact = _atwsServices.query(strContact.ToString(), out errorMsg);

            if (respContact.ReturnCode > 0 && respContact.EntityResults.Length > 0)
            {
                foreach (Entity entity in respContact.EntityResults)
                {
                    list.Add((Contact)entity);
                }
            }
            else if (respContact.Errors != null &&
                    respContact.Errors.Length > 0)
            {
                errorMsg = respContact.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get contact by name. Uses 'beginswith' operator to match passed
        /// first name and last name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Contact> GetContactByName(string firstName, string lastName,
                                                out string errorMsg)
        {
            List<Contact> list = new List<Contact>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strContact = new StringBuilder();
            strContact.Append("<queryxml version=\"1.0\">");
            strContact.Append("<entity>Contact</entity>");
            strContact.Append("<query>");
            strContact.Append("<condition><field>FirstName<expression op=\"beginswith\">");
            strContact.Append(firstName);
            strContact.Append("</expression></field></condition>");
            strContact.Append("<condition><field>LastName<expression op=\"beginswith\">");
            strContact.Append(lastName);
            strContact.Append("</expression></field></condition>");
            strContact.Append("</query></queryxml>");

            ATWSResponse respContact = _atwsServices.query(strContact.ToString(), out errorMsg);

            if (respContact.ReturnCode > 0 && respContact.EntityResults.Length > 0)
            {
                foreach (Entity entity in respContact.EntityResults)
                {
                    list.Add((Contact)entity);
                }
            }
            else if (respContact.Errors != null &&
                    respContact.Errors.Length > 0)
            {
                errorMsg = respContact.Errors[0].Message;
            }

            return list;
        }
    }


}