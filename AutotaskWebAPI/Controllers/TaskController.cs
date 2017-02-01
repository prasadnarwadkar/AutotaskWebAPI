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
    public class TaskController : BaseApiController
    {
        [Route("api/task/GetByProjectId/{accountId}")]
        [HttpGet]
        public HttpResponseMessage GetByProjectId(string accountId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = tasksApi.GetTaskByProjectId(accountId, out errorMsg);

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

        [Route("api/task/GetByProjectIdAndStatus/{accountId}/{status}")]
        /// <summary>
        /// Get tasks by status. e.g. Status of 8 means "In Progress".
        /// </summary>
        /// <param name="accountId">Project id to which the task(s) belong.</param>
        /// <param name="status">Status as an integer passed as string. e.g. "8".</param>
        /// <returns>List of tasks.</returns>
        [HttpGet]
        public HttpResponseMessage GetByProjectIdAndStatus(string accountId, string status)
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
                int accountIdInt = -1;

                parseResult = int.TryParse(accountId, out accountIdInt);

                if (parseResult)
                {
                    var result = tasksApi.GetTaskByProjectIdAndStatus(accountId, statusInt, out errorMsg);

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
        /// <param name="accountId">Project id to which the task(s) belong.</param>
        /// <param name="priority">Priority as an integer passed as string. e.g. "6".</param>
        /// <returns>List of tasks.</returns>
        [Route("api/task/GetByProjectIdAndPriority/{accountId}/{priority}")]
        [HttpGet]
        public HttpResponseMessage GetByProjectIdAndPriority(string accountId, string priority)
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
                int accountIdInt = -1;

                parseResult = int.TryParse(accountId, out accountIdInt);

                if (parseResult)
                {
                    var result = tasksApi.GetTaskByProjectIdAndPriority(accountId, priorityInt, out errorMsg);

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
