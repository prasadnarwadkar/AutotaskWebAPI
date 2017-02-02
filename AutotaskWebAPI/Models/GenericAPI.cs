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

        public Entity UpdateEntity(Entity entityToUpdate, out string errorMsg)
        {
            try
            {
                errorMsg = string.Empty;

                if (entityToUpdate == null)
                {
                    throw new ArgumentNullException("Entity to Update is null.");
                }

                if (entityToUpdate.id < 1)
                {
                    throw new ArgumentException("Id of the entity to update is invalid.");
                }

                if (errorMsg.Length == 0)
                {
                    Entity[] entityArray = new Entity[] { entityToUpdate };
                    ATWSResponse respUpdate = api._atwsServices.update(entityArray);

                    if (respUpdate.ReturnCode > 0 && respUpdate.EntityResults.Length > 0)
                    {
                        return respUpdate.EntityResults[0];
                    }
                    else if (respUpdate.Errors != null &&
                            respUpdate.Errors.Length > 0)
                    {
                        errorMsg = respUpdate.Errors[0].Message;

                        return null;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }

        public Entity CreateEntity(Entity entityToCreate, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (entityToCreate == null)
            {
                throw new ArgumentNullException("Entity to create is null.");
            }

            Entity[] entityArray = new Entity[] { entityToCreate };

            ATWSResponse response = api._atwsServices.create(entityArray);

            if (response.ReturnCode > 0 && response.EntityResults.Length > 0)
            {
                return response.EntityResults[0];
            }
            else
            {
                if (response != null && response.Errors != null
                    && response.Errors.Length > 0)
                {
                    errorMsg = response.Errors[0].Message;
                    return null;
                }
            }

            return null;
        }
    }
}