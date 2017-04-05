using WrapperLib.Autotask.Net.Webservices;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AutotaskWebAPI.Controllers
{
    /// <summary>
    /// Provides API for Attachments in Autotask.
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AttachmentController : BaseApiController
    {
        /// <summary>
        /// Get attachment(s) by parent id. A parent could be a Ticket for example.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [Route("api/attachments/parent/{parentId:int}")]
        [SwaggerResponse(typeof(List<AttachmentInfo>))]
        [HttpGet]
        public HttpResponseMessage GetInfoByParentId(long parentId)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = attachmentsApi.GetAttachmentInfoByParentId(parentId, out errorMsg);

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
        /// Get attachment by id.
        /// </summary>
        /// <param name="id">attachment id</param>
        /// <returns></returns>
        [Route("api/attachments/{id:int}")]
        [SwaggerResponse(typeof(ByteArrayContent))]
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

            var result = attachmentsApi.GetAttachmentById(id, out errorMsg);

            if (errorMsg.Length > 0)
            {
                // There is an error.
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(result.Data, 0, result.Data.Length - 1);
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(result.Info.ContentType.ToString());

                return response;
            }
        }

        /// <summary>
        /// Get attachment(s) by date.
        /// </summary>
        /// <param name="attachDate">Date Format: YYYY-MM-DD</param>
        /// <returns>All attachments attached to their parents respectively after given date.</returns>
        [Route("api/attachments/attachdate/{attachDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [SwaggerResponse(typeof(List<AttachmentInfo>))]
        [HttpGet]
        public HttpResponseMessage GetInfoByAttachDate(string attachDate)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = attachmentsApi.GetAttachmentInfoByAttachDate(attachDate, out errorMsg);

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
        /// Get attachment(s) by the date they were attached and their parent id. 
        /// A parent could be a Ticket for example.
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="attachDate"></param>
        /// <returns>All attachments attached to their parents respectively after given date.</returns>
        [Route("api/attachments/parent/{parentId:int}/attachdate/{attachDate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [SwaggerResponse(typeof(List<AttachmentInfo>))]
        [HttpGet]
        public HttpResponseMessage GetInfoByParentIdAndAttachDate(long parentId, string attachDate)
        {
            if (!apiInitialized)
            {
                var response = Request.CreateResponse(HttpStatusCode.Found);
                response.Headers.Location = new Uri(Url.Route("NotInitialized", null), UriKind.Relative);
                return response;
            }

            string errorMsg = string.Empty;

            var result = attachmentsApi.GetAttachmentInfoByParentIdAndAttachDate(parentId,
                                                                    attachDate, 
                                                                    out errorMsg);

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
        /// Create an attachment.
        /// e.g.
        ///    POST /api/attachments HTTP/1.1
        ///    Host: {base url}
        ///    Authorization: Basic {auth token here}
        ///
        ///    Content-Type: multipart/form-data; 
        ///    Content-Disposition: form-data; name="file1"; filename=""
        ///    Content-Type: 
        ///    Content-Disposition: form-data; name="ParentID"
        ///    12345
        ///    Content-Disposition: form-data; name="Title"
        ///    myFile.txt
        ///    Content-Disposition: form-data; name="FullPath"
        ///    myFile.txt
        ///    Content-Disposition: form-data; name="Publish"
        ///    2
        ///    Content-Disposition: form-data; name="ParentType"
        ///    4
        /// </summary>
        /// <returns>id of the attachment created</returns>
        [Route("api/attachments")]
        [SwaggerResponse(typeof(Int32))]
        [HttpPost]
        public HttpResponseMessage PostTicketAttachment()
        {
            if (HttpContext.Current.Request != null)
            {
                // Get the uploaded file from the Files collection
                if (HttpContext.Current.Request.Files != null
                    && HttpContext.Current.Request.Files.Count > 0)
                {
                    var httpPostedFile = HttpContext.Current.Request.Files[0];

                    if (httpPostedFile != null)
                    {
                        Attachment attachment = new Attachment();
                        attachment.Info = new AttachmentInfo();

                        // Parent type for a ticket parent is 4.
                        attachment.Info.ParentType = HttpContext.Current.Request.Form["ParentType"];

                        attachment.Info.FullPath = HttpContext.Current.Request.Form["FullPath"];
                        attachment.Info.ParentID = HttpContext.Current.Request.Form["ParentID"];
                        attachment.Info.Publish = HttpContext.Current.Request.Form["Publish"];
                        attachment.Info.Title = HttpContext.Current.Request.Form["Title"];
                        attachment.Info.Type = "FILE_ATTACHMENT";
                        byte[] buffer = new byte[httpPostedFile.ContentLength];

                        httpPostedFile.InputStream.Read(buffer, 0, httpPostedFile.ContentLength);
                        attachment.Data = buffer;

                        string errorMsg = string.Empty;

                        long id = attachmentsApi.CreateAttachment(attachment, out errorMsg);

                        if (id > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, id);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, errorMsg);
                        }
                    }
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No files were attached.");
        }

    }
}
