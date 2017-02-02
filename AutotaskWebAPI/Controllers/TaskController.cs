using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides API for a Task entity in Autotask.
    /// </summary>
    public class TaskController : BaseApiController
    {
        /// <summary>
        /// Get Task(s) given project id.
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <returns>All Task(s) with project id matching passed-in project id.</returns>
        [Route("api/task/GetByProjectId/{projectId}")]
        [HttpGet]
        public HttpResponseMessage GetByProjectId(string projectId)
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
        [Route("api/task/GetById/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(string id)
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
        [Route("api/task/GetByCreatorResourceId/{creatorResourceId}")]
        [HttpGet]
        public HttpResponseMessage GetByCreatorResourceId(string creatorResourceId)
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
        /// <param name="lastActivityDate">Date after which there is activity in the returned tasks.</param>
        /// <returns></returns>
        [Route("api/task/GetByLastActivityDate/{lastActivityDate}")]
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
        /// <param name="status">Status as an integer passed as string. e.g. "8".</param>
        /// <returns>List of tasks.</returns>
        [Route("api/task/GetByProjectIdAndStatus/{projectId}/{status}")]
        [HttpGet]
        public HttpResponseMessage GetByProjectIdAndStatus(string projectId, string status)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            if (string.IsNullOrEmpty(status))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Status cannot be null or empty. Please pass the status as an integer. e.g. '8'");
            }

            int statusInt = 0;

            bool parseResult = int.TryParse(status, out statusInt);

            if (parseResult)
            {
                int projectIdInt = -1;

                parseResult = int.TryParse(projectId, out projectIdInt);

                if (parseResult)
                {
                    var result = tasksApi.GetTaskByProjectIdAndStatus(projectId, statusInt, out errorMsg);

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

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Status or account id passed is not an integer.");
        }

        /// <summary>
        /// Get tasks by account id and priority. e.g. Priority of 6 means "Normal".
        /// </summary>
        /// <param name="projectId">Project id to which the task(s) belong.</param>
        /// <param name="priority">Priority as an integer passed as string. e.g. "6".</param>
        /// <returns>List of tasks.</returns>
        [Route("api/task/GetByProjectIdAndPriority/{projectId}/{priority}")]
        [HttpGet]
        public HttpResponseMessage GetByProjectIdAndPriority(string projectId, string priority)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            if (string.IsNullOrEmpty(priority))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Priority cannot be null or empty. Please pass the priority as an integer. e.g. '6'");
            }

            int priorityInt = 0;

            bool parseResult = int.TryParse(priority, out priorityInt);

            if (parseResult)
            {
                int projectIdInt = -1;

                parseResult = int.TryParse(projectId, out projectIdInt);

                if (parseResult)
                {
                    var result = tasksApi.GetTaskByProjectIdAndPriority(projectId, priorityInt, out errorMsg);

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

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Priority or account id passed is not an integer.");
        }

        /// <summary>
        /// Create a Task in Autotask.
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [Route("api/task/PostTask")]
        [HttpPost]
        public HttpResponseMessage PostTask([FromBody] TaskDetails details)
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
