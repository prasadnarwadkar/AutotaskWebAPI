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
        /// Get role by id.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public RoleDto GetRoleById(long roleId, out string errorMsg)
        {
            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Role</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(roleId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                var role = (Role)respResource.EntityResults[0];

                return new RoleDto {
                    Id = role.id,
                    Description = role.Description.ToString(),
                    Name = role.Name.ToString()
                };
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return null;
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<RoleDto> GetAllRoles(out string errorMsg)
        {
            List<RoleDto> list = new List<RoleDto>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>Role</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"greaterthan\">");
            strResource.Append(0);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    var role = (Role)entity;

                    list.Add(new RoleDto
                    {
                        Id = role.id,
                        Description = role.Description.ToString(),
                        Name = role.Name.ToString()
                    });
                }

                return list;
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;

                return list;
            }

            return list;           
        }

        /// <summary>
        /// Get resource role by resource id.
        /// </summary>
        /// <param name="resourceID"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public List<ResourceRoleDto> GetRoleByResourceId(long resourceID, out string errorMsg)
        {
            List<ResourceRoleDto> list = new List<ResourceRoleDto>();

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

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    var resourceRole = (ResourceRole)entity;

                    if (resourceRole.RoleID != null &&
                        !string.IsNullOrEmpty(resourceRole.RoleID.ToString()))
                    {
                        var role = GetRoleById(Convert.ToInt32(resourceRole.RoleID), out errorMsg);

                        if (role != null)
                        {
                            list.Add(new ResourceRoleDto {
                                ResourceId = Convert.ToInt32(resourceID),
                                RoleDescription = role.Description,
                                RoleId = role.Id,
                                RoleName = role.Name
                            });
                        }
                    }
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