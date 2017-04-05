using WrapperLib.Autotask.Net.Webservices;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WrapperLib.Models;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides Complex query support for any entity.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ComplexQueryController : BaseApiController
    {
        /// <summary>
        /// Although this method uses POST, it actually returns results of
        /// complex query performed on the dataset. POST is used here simply to
        /// make it easy to pass a complex query containing simple or nested conditions.
        ///
        /// e.g. 
        /// (
        ///     firstname = ‘Joe’
        ///     OR(
        ///         (firstname = ‘Larry’ and lastname = ‘Brown’)
        ///         OR
        ///         (firstname = ‘Mary’ and lastname = ‘Smith’)
        ///     )
        /// )
        /// 
        /// </summary>
        /// <param name="complexQuery">Complex query containing a list of Conditions.</param>
        /// <returns>List of entities matching the given condition(s).</returns>
        [Route("api/complexquery")]
        [SwaggerResponse(typeof(List<Entity>))]
        [HttpPost]
        public HttpResponseMessage ComplexQuery([FromBody] ComplexQuery complexQuery)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            try
            {
                if (complexQuery == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request: Complex query sent is null or invalid.");
                }

                string errorMsg = string.Empty;

                List<Entity> entities = genericApi.GetEntityByListOfConditions(complexQuery.EntityName, complexQuery.Conditions, out errorMsg);

                if (errorMsg.Length > 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                }
                else {
                    return Request.CreateResponse(HttpStatusCode.OK, entities.ToList());
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}