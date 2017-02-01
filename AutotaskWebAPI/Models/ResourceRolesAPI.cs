using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class ResourceRolesAPI
    {
        private AutotaskAPI api = null;

        public ResourceRolesAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        /// <summary>
        /// Get resource role by resource id.
        /// </summary>
        /// <param name="resourceID"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<ResourceRole> GetRoleByResourceId(string resourceID, out string errorMsg)
        {
            List<ResourceRole> list = new List<ResourceRole>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>ResourceRole</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>ResourceID<expression op=\"equals\">");
            strResource.Append(resourceID);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((ResourceRole)entity);
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