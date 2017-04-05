using AutotaskWebAPI.Autotask.Net.Webservices;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WrapperLib.Models;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides API for Resource entity in Autotask.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ResourceController : BaseApiController
    {
        /// <summary>
        /// Get a resource by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/resources/{id:int}")]
        [SwaggerResponse(typeof(Resource))]
        [HttpGet]
        public HttpResponseMessage GetById(long id)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourcesApi.GetResourceById(id, out errorMsg);

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
        /// Get Resource(s) by first name and last name.
        /// Uses 'beginswith' search.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [Route("api/resources/name/{firstName}/{lastName}")]
        [SwaggerResponse(typeof(List<Resource>))]
        [HttpGet]
        public HttpResponseMessage GetByName(string firstName, string lastName)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourcesApi.GetResourceByName(firstName, lastName, out errorMsg);

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
        /// Get a resource given email address.
        /// Uses exact match to given email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("api/resources/email")]
        [SwaggerResponse(typeof(List<Resource>))]
        [HttpGet]
        public HttpResponseMessage GetByEmail(string email)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourcesApi.GetResourceByEmail(email, out errorMsg);

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
        /// Get a resource given username.
        /// Uses exact match to given username.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [Route("api/resources/username/{userName}")]
        [SwaggerResponse(typeof(List<Resource>))]
        [HttpGet]
        public HttpResponseMessage GetByUsername(string userName)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourcesApi.GetResourceByUsername(userName, out errorMsg);

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
        /// Get a resource given location id.
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        [Route("api/resources/location/{locationId:int}")]
        [SwaggerResponse(typeof(List<Resource>))]
        [HttpGet]
        public HttpResponseMessage GetByLocationId(long locationId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourcesApi.GetResourceByLocationId(locationId, out errorMsg);

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
        /// Get roles of a resource given a resource id.
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns>List of roles for a resource.</returns>
        [Route("api/resources/roles/{resourceId:int}")]
        [SwaggerResponse(typeof(List<ResourceRoleDto>))]
        [HttpGet]
        public HttpResponseMessage GetRoleByResourceId(long resourceId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourceRolesApi.GetRoleByResourceId(resourceId, out errorMsg);

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
        /// Get role name, and description given its id.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Role details object.</returns>
        [Route("api/resources/role/{roleId:int}")]
        [SwaggerResponse(typeof(RoleDto))]
        [HttpGet]
        public HttpResponseMessage GetRoleById(long roleId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourceRolesApi.GetRoleById(roleId, out errorMsg);

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
        /// Get role details of all roles.
        /// </summary>
        /// <returns>List of all Role details objects. Each details object has Role id, name
        /// and description.</returns>
        [Route("api/resources/roles")]
        [SwaggerResponse(typeof(List<RoleDto>))]
        [HttpGet]
        public HttpResponseMessage GetAllRoles()
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = resourceRolesApi.GetAllRoles(out errorMsg);

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
    }
}
