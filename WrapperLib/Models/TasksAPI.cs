using WrapperLib.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Text;

namespace WrapperLib.Models
{
    /// <summary>
    /// API for Task entity.
    /// </summary>
    public class TasksAPI : ApiBase
    {
        public List<Task> GetOpenTasksByAssignedResourceId(long assignedResourceId, out string errorMsg)
        {
            List<Task> list = new List<Task>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>AssignedResourceID<expression op=\"equals\">");
            strResource.Append(assignedResourceId);
            strResource.Append("</expression></field>");
            strResource.Append("<field>Status<expression op=\"NotEqual\">");
            strResource.Append(5);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Task)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Task> GetDueOrOverdue(long assignedResourceId,
                                            string date, out string errorMsg)
        {
            List<Task> list = new List<Task>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>EndDateTime<expression op=\"LessThanOrEquals\">");
            strResource.Append(date);
            strResource.Append("</expression></field>");
            strResource.Append("<field>AssignedResourceID<expression op=\"equals\">");
            strResource.Append(assignedResourceId);
            strResource.Append("</expression></field>");
            strResource.Append("<field>Status<expression op=\"NotEqual\">");
            strResource.Append(5);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Task)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public Task CreateTask(long projectId, 
                                long status, long taskType,
                                string title, long creatorResourceID,
                                long assignedResourceID,
                                long assignedResourceRoleID, out string error)
        {
            error = string.Empty;

            // Time to create the Task.
            Task task = new Task();

            if (projectId < 1)
            {
                throw new ArgumentException("Project ID sent is invalid.");
            }

            // Bare-minimum number of fields needed to create the task.
            task.ProjectID = projectId;
            task.Status = status;
            task.TaskType = taskType;
            task.Title = title;

            if (creatorResourceID > 0)
            {
                task.CreatorResourceID = creatorResourceID;
            }

            if (assignedResourceID > 0)
            {
                task.AssignedResourceID = assignedResourceID;
                task.AssignedResourceRoleID = assignedResourceRoleID;
            }
            
            Entity[] entityToCreate = new Entity[] { task };

            ATWSResponse response = _atwsServices.create(entityToCreate, out error);

            if (response.ReturnCode > 0 && response.EntityResults.Length > 0)
            {
                return (Task)response.EntityResults[0];
            }
            else
            {
                if (response != null && response.Errors != null
                    && response.Errors.Length > 0)
                {
                    error = response.Errors[0].Message;
                    return null;
                }
            }

            return null;
        }

        public List<Task> GetByProjectIdAndPriority(long projectId, long priority, out string errorMsg)
        {
            List<Task> list = new List<Task>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<condition><field>Priority<expression op=\"Equals\">");
            strResource.Append(priority);
            strResource.Append("</expression></field></condition>");
            strResource.Append("<condition><field>ProjectID<expression op=\"Equals\">");
            strResource.Append(projectId);
            strResource.Append("</expression></field></condition>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Task)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Task> GetTaskByProjectId(long projectId, out string errorMsg)
        {
            List<Task> list = new List<Task>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>ProjectID<expression op=\"equals\">");
            strResource.Append(projectId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Task)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Task> GetByProjectIdAndStatus(long projectId, long status, out string errorMsg)
        {
            List<Task> list = new List<Task>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<condition><field>Status<expression op=\"Equals\">");
            strResource.Append(status);
            strResource.Append("</expression></field></condition>");
            strResource.Append("<condition><field>ProjectID<expression op=\"Equals\">");
            strResource.Append(projectId);
            strResource.Append("</expression></field></condition>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Task)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<Task> GetTaskByCreatorResourceId(long creatorResourceId, out string errorMsg)
        {
            List<Task> list = new List<Task>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>CreatorResourceID<expression op=\"equals\">");
            strResource.Append(creatorResourceId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Task)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public Task GetTaskById(long id, out string errorMsg)
        {
            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(id);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                return (Task)respResource.EntityResults[0];
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;

                return null;
            }

            return null;
        }

        public List<Task> GetTaskByLastActivityDate(string lastActivityDateTime, out string errorMsg)
        {
            List<Task> list = new List<Task>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Task</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LastActivityDateTime<expression op=\"greaterthan\">");
            strResource.Append(lastActivityDateTime);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = _atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((Task)entity);
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