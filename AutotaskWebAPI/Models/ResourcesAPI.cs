using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class ResourcesAPI
    {
        private AutotaskAPI api = null;

        public ResourcesAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        /// <summary>
        /// Get resource by email. Uses exact match to passed email address.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByEmail(string email, out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>Email<expression op=\"equals\">");
            strResource.Append(email);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
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
        /// Get resource by user name. Uses exact match to passed user name.
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByUsername(string userName, out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>UserName<expression op=\"equals\">");
            strResource.Append(userName);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Resource> GetResourceById(long id, out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(id);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
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
        /// Get resource by location id. Uses exact match to passed location id.
        /// Location id is a picklist field.
        /// </summary>
        /// <param name="locationId">Location id of the resource</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByLocationId(long locationId,
                                                    out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LocationID<expression op=\"equals\">");
            strResource.Append(locationId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
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
        /// Get resource by name. Uses 'beginswith' operator to match passed
        /// first name and last name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Resource> GetResourceByName(string firstName, string lastName,
                                                out string errorMsg)
        {
            List<Resource> list = new List<Resource>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Resource</entity>");
            strResource.Append("<query>");
            strResource.Append("<condition><field>FirstName<expression op=\"beginswith\">");
            strResource.Append(firstName);
            strResource.Append("</expression></field></condition>");
            strResource.Append("<condition><field>LastName<expression op=\"beginswith\">");
            strResource.Append(lastName);
            strResource.Append("</expression></field></condition>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Resource)entity);
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