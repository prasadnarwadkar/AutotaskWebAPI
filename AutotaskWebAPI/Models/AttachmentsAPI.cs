using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;

namespace AutotaskWebAPI.Models
{
    public class AttachmentsAPI
    {
        private AutotaskAPI api = null;

        public AttachmentsAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        public Attachment GetAttachmentById(long id, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                var result = api._atwsServices.GetAttachment(id);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (SoapException ex)
            {
                errorMsg = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Get attachment info by parent id. Usually parent is a ticket.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<AttachmentInfo> GetAttachmentInfoByParentId(long parentId,
                                                                out string errorMsg)
        {
            List<AttachmentInfo> list = new List<AttachmentInfo>();
            errorMsg = string.Empty;

            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>AttachmentInfo</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>ParentID<expression op=\"equals\">");
            strResource.Append(parentId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString(), out errorMsg);

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((AttachmentInfo)entity);
                }
            }
            else if (respResource.Errors != null &&
                respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        /// <summary>
        /// Get attachment info by attach date. 
        /// This is the date at or after which it was attached to its parent.
        /// </summary>
        /// <param name="attachDate"></param>
        /// <returns></returns>
        public List<AttachmentInfo> GetAttachmentInfoByAttachDate(string attachDate,
                                                                out string errorMsg)
        {
            List<AttachmentInfo> list = new List<AttachmentInfo>();
            errorMsg = string.Empty;

            if (attachDate.Length > 0)
            {
                StringBuilder strResource = new StringBuilder();
                strResource.Append("<queryxml version=\"1.0\">");
                strResource.Append("<entity>AttachmentInfo</entity>");
                strResource.Append("<query>");
                strResource.Append("<field>AttachDate<expression op=\"GreaterThanorEquals\">");
                strResource.Append(attachDate);
                strResource.Append("</expression></field>");
                strResource.Append("</query></queryxml>");

                ATWSResponse respResource = api._atwsServices.query(strResource.ToString(), out errorMsg);

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    foreach (Entity entity in respResource.EntityResults)
                    {
                        list.Add((AttachmentInfo)entity);
                    }
                }
                else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
                {
                    errorMsg = respResource.Errors[0].Message;
                }
            }

            return list;
        }

        public List<AttachmentInfo> GetAttachmentInfoByParentIdAndAttachDate(long parentId,
                                                                string attachDate,
                                                                out string errorMsg)
        {
            List<AttachmentInfo> list = new List<AttachmentInfo>();
            errorMsg = string.Empty;

            if (attachDate.Length > 0)
            {
                StringBuilder strResource = new StringBuilder();
                strResource.Append("<queryxml version=\"1.0\">");
                strResource.Append("<entity>AttachmentInfo</entity>");
                strResource.Append("<query>");
                strResource.Append("<condition><field>ParentID<expression op=\"Equals\">");
                strResource.Append(parentId);
                strResource.Append("</expression></field></condition>");
                strResource.Append("<condition><field>AttachDate<expression op=\"GreaterThanorEquals\">");
                strResource.Append(attachDate);
                strResource.Append("</expression></field></condition>");
                strResource.Append("</query></queryxml>");

                ATWSResponse respResource = api._atwsServices.query(strResource.ToString(), out errorMsg);

                if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
                {
                    foreach (Entity entity in respResource.EntityResults)
                    {
                        list.Add((AttachmentInfo)entity);
                    }
                }
                else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
                {
                    errorMsg = respResource.Errors[0].Message;
                }
            }

            return list;
        }

        public long CreateAttachment(Attachment attachment, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                return api._atwsServices.CreateAttachment(attachment);
            }
            catch (SoapException ex)
            {
                errorMsg = ex.Message;

                return -1;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;

                return -1;
            }

        }
    }
}