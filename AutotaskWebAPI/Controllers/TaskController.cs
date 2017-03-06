using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides API for a Task entity in Autotask.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : BaseApiController
    {
        /// <summary>
        /// Get Task(s) given project id.
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <returns>All Task(s) with project id matching passed-in project id.</returns>
        [Route("api/tasks/project/{projectId:int}")]
        [SwaggerResponse(typeof(List<Task>))]
        [HttpGet]
        public HttpResponseMessage GetByProjectId(long projectId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = tasksApi.GetTaskByProjectId(projectId, out errorMsg);

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
        /// Get Task given its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/tasks/{id:int}")]
        [SwaggerResponse(typeof(Task))]
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

            var result = tasksApi.GetTaskById(id, out errorMsg);

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
        /// Get Task(s) given its creator resource id.
        /// </summary>
        /// <param name="creatorResourceId"></param>
        /// <returns>All Task(s) with creator resource id matching passed-in creator resource id.</returns>
        [Route("api/tasks/creator/{creatorResourceId:int}")]
        [SwaggerResponse(typeof(List<Task>))]
        [HttpGet]
        public HttpResponseMessage GetByCreatorResourceId(long creatorResourceId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = tasksApi.GetTaskByCreatorResourceId(creatorResourceId, out errorMsg);

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
        /// Get Task(s) which have last activity date that occurs after 
        /// passed-in date.
        /// </summary>
        /// <param name="lastActivityDate">Date in the format yyyy-mm-dd. This is the 
        /// date after which there is activity in the returned tasks.</param>
        /// <returns></returns>
        [Route("api/tasks/lastactivitydate/{lastActivityDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [SwaggerResponse(typeof(List<Task>))]
        [HttpGet]
        public HttpResponseMessage GetByLastActivityDate(string lastActivityDate)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = tasksApi.GetTaskByLastActivityDate(lastActivityDate, out errorMsg);

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
        /// Get tasks by status. e.g. Status of 8 means "In Progress".
        /// </summary>
        /// <param name="projectId">Project id to which the task(s) belong.</param>
        /// <param name="status">Status as an integer e.g. 8.</param>
        /// <returns>List of tasks.</returns>
        [Route("api/tasks/project/{projectId:int}/status/{status:int}")]
        [SwaggerResponse(typeof(List<Task>))]
        [HttpGet]
        public HttpResponseMessage GetByProjectIdAndStatus(long projectId, long status)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = tasksApi.GetByProjectIdAndStatus(projectId, status, out errorMsg);

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
        /// Get tasks by account id and priority. e.g. Priority of 6 means "Normal".
        /// </summary>
        /// <param name="projectId">Project id to which the task(s) belong.</param>
        /// <param name="priority">Priority as an integer e.g. 6.</param>
        /// <returns>List of tasks.</returns>
        [Route("api/tasks/project/{projectId:int}/priority/{priority:int}")]
        [SwaggerResponse(typeof(List<Task>))]
        [HttpGet]
        public HttpResponseMessage GetByProjectIdAndPriority(long projectId, long priority)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = tasksApi.GetByProjectIdAndPriority(projectId, priority, out errorMsg);

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
        /// Create a Task in Autotask.
        /// </summary>
        /// <param name="details"></param>
        /// <returns>id of the created task</returns>
        [Route("api/tasks/")]
        [SwaggerResponse(typeof(Int32))]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] TaskDetails details)
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
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No task details are passed");
                }

                string errorMsg = string.Empty;

                Task task = tasksApi.CreateTask(details.ProjectID, 
                                                details.Status,
                                                details.TaskType,
                                                details.Title,
                                                details.CreatorResourceID,
                                                details.AssignedResourceID,
                                                details.AssignedResourceRoleID,
                                                out errorMsg);

                if (task != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, task.id);
                    return response;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                }
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
