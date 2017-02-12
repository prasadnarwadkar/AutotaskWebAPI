using AutotaskWebAPI.Autotask.Net.Webservices;
using Newtonsoft.Json.Linq;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides Generic API for any entity and any of its fields.
    /// You can use this API for getting any entity by any of its fields.
    /// </summary>
    public class GenericController : BaseApiController
    {
        /// <summary>
        /// Get entity by entity name, field name and field value.
        /// You can use this API for getting any entity by any of its fields.
        /// </summary>
        /// <param name="entityName">Name of the entity e.g. Ticket</param>
        /// <param name="fieldName">Field name. e.g. id</param>
        /// <param name="fieldValue">Field value e.g. 12345. If you pass a date, it should be in the format 'yyyy-mm-dd'.</param>
        /// <returns>List of entities for which field value of the field name matches the passed-in value. e.g. id value matches the passed-in id value.</returns>
        [Route("api/generics")]
        [SwaggerResponse(typeof(List<Entity>))]
        [HttpGet]
        public HttpResponseMessage GetByEntityNameFieldNameAndValue(string entityName, string fieldName, 
                                                                    string fieldValue)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            if (string.IsNullOrEmpty(entityName))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Entity Name is null or empty.");
            }

            if (string.IsNullOrEmpty(fieldName))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Field Name is null or empty.");
            }

            if (fieldValue == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Field Value is null or empty.");
            }

            string errorMsg = string.Empty;

            var result = genericApi.GetEntityByFieldEquals(entityName, fieldName, fieldValue.ToString(), out errorMsg);

            if (errorMsg.Length > 0)
            {
                // There is an error.
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// Create an entity.
        /// Returns id of the entity created. Please query the entity with this id again to retrieve created entity.
        /// Pass entity details to create. E.g.
        /// 
        /// {"EntityType":"Task","EntityObj":
        ///    {
        ///      "projectid": 12345,
        ///      "creatorResourceId": 12345,
        ///      "title": "string",
        ///      "description": "string"
        ///    }}
        /// 
        /// Change entity type to the type you would like to create.
        /// As of now, it supports Task, TicketNote, Contact, Contract and Resource entities.
        /// </summary>
        /// <param name="details">Entity details</param>
        /// <returns>id of the entity created</returns>
        [Route("api/generics")]
        [HttpPost]
        public HttpResponseMessage PostEntity([FromBody] JObject details)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            try
            {
                if (details == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request: Entity details passed are null or invalid");
                }

                string errorMsg = string.Empty;

                Entity entity = null;

                if (details["EntityType"] != null)
                {
                    if (details["EntityObj"] == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "EntityObj key and value is not sent in JSON body. It is required for creating an entity.");
                    }

                    switch (details["EntityType"].ToString())
                    {
                        case "Task":
                            entity = genericApi.CreateEntity(details["EntityObj"].ToObject<Task>(), out errorMsg);
                            break;
                        case "Contact":
                            entity = genericApi.CreateEntity(details["EntityObj"].ToObject<Contact>(), out errorMsg);
                            break;
                        case "TicketNote":
                            entity = genericApi.CreateEntity(details["EntityObj"].ToObject<TicketNote>(), out errorMsg);
                            break;
                        case "Contract":
                            entity = genericApi.CreateEntity(details["EntityObj"].ToObject<Contract>(), out errorMsg);
                            break;
                        case "Resource":
                            entity = genericApi.CreateEntity(details["EntityObj"].ToObject<Resource>(), out errorMsg);
                            break;
                        default:
                            return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "Entity type not supported yet.");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "EntityType key is not sent in JSON body");
                }            

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.id);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update an entity.
        /// Returns id of the entity updated. Please query the entity with this id again to retrieve updated entity.
        /// Pass entity details to update. E.g.
        /// 
        /// {"EntityType":"Task","EntityObj":
        ///    {
        ///      "id":123,
        ///      "projectid": 12345,
        ///      "creatorResourceId": 12345,
        ///      "title": "string",
        ///      "description": "string"
        ///    }}
        ///    
        /// Change entity type to the type you would like to update.
        /// As of now, it supports Task, TicketNote, Contact, Contract and Resource entities.
        /// </summary>
        /// <param name="details">Entity details</param>
        /// <returns>Returns id of the entity updated. Please query the entity with this id again to retrieve updated entity.</returns>
        [Route("api/generics")]
        [HttpPut]
        public HttpResponseMessage UpdateEntity([FromBody] JObject details)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            try
            {
                if (details == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request: Entity details passed are null or invalid. Please make sure that the JSON you sent is well-formed.");
                }

                string errorMsg = string.Empty;

                Entity entity = null;

                if (details["EntityType"] != null)
                {
                    if (details["EntityObj"] == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "EntityObj key and value is not sent in JSON body. It is required for updating an entity.");
                    }

                    switch (details["EntityType"].ToString())
                    {
                        case "Task":
                            entity = genericApi.UpdateEntity(details["EntityObj"].ToObject<Task>(), out errorMsg);
                            break;
                        case "Contact":
                            entity = genericApi.UpdateEntity(details["EntityObj"].ToObject<Contact>(), out errorMsg);
                            break;
                        case "Contract":
                            entity = genericApi.UpdateEntity(details["EntityObj"].ToObject<Contract>(), out errorMsg);
                            break;
                        case "Resource":
                            entity = genericApi.UpdateEntity(details["EntityObj"].ToObject<Resource>(), out errorMsg);
                            break;
                        case "TicketNote":
                            entity = genericApi.UpdateEntity(details["EntityObj"].ToObject<TicketNote>(), out errorMsg);
                            break;
                        default:
                            return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "Entity type not supported yet.");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "EntityType key is not sent in JSON body");
                }

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity.id);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}