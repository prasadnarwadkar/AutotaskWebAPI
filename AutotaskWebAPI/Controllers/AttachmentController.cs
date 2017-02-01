using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    public class AttachmentController : BaseApiController
    {
        [Route("api/attachment/GetInfoByParentId/{parentId}")]
        [HttpGet]
        public HttpResponseMessage GetInfoByParentId(string parentId)
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

        [Route("api/attachment/GetById/{id}")]
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

            int idInt = 0;

            bool parseResult = int.TryParse(id, out idInt);

            if (parseResult)
            {
                var result = attachmentsApi.GetAttachmentById(idInt, out errorMsg);

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
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Passed id is not an integer.");
            }            
        }

        [Route("api/attachment/GetInfoByAttachDate/{attachDate}")]
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

        [Route("api/attachment/GetInfoByParentIdAndAttachDate/{parentId}/{attachDate}")]
        [HttpGet]
        public HttpResponseMessage GetInfoByParentIdAndAttachDate(string parentId, string attachDate)
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

        // POST api/ticketattachment/post/
        // Body:
        // Form-data
        // File1: file chosen by user from client side.
        // name: file name
        // TicketId: parent ticket id.
        [Route("api/attachment/PostTicketAttachment")]
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
                        attachment.Info.ParentType = 4;

                        attachment.Info.FullPath = HttpContext.Current.Request.Form["name"];
                        attachment.Info.ParentID = HttpContext.Current.Request.Form["TicketId"];
                        attachment.Info.Publish = 1;
                        attachment.Info.Title = HttpContext.Current.Request.Form["name"];
                        attachment.Info.Type = "FILE_ATTACHMENT";
                        byte[] buffer = new byte[httpPostedFile.ContentLength];

                        httpPostedFile.InputStream.Read(buffer, 0, httpPostedFile.ContentLength);
                        attachment.Data = buffer;

                        AutotaskAPI api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                                        ConfigurationManager.AppSettings["APIPassword"]);

                        string errorMsg = string.Empty;

                        long id = attachmentsApi.CreateAttachment(attachment, out errorMsg);

                        if (id > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, id);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMsg);
                        }
                    }
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No files were attached.");
        }

    }
}
