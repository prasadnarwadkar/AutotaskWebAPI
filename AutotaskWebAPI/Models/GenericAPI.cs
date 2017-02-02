using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class GenericAPI
    {
        private AutotaskAPI api = null;

        public GenericAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        /// <summary>
        /// Get any entity by simple entity name and a field name
        /// and field value. e.g. Ticket entity, field id and value 12345.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<Entity> GetEntityByFieldEquals(string entityName, string fieldName,
                                                   string fieldValue, out string errorMsg)
        {
            List<Entity> list = new List<Entity>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append(string.Format("<entity>{0}</entity>", entityName));
            strResource.Append("<query>");
            strResource.Append(string.Format("<field>{0}<expression op=\"equals\">", fieldName));
            strResource.Append(fieldValue);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add(entity);
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