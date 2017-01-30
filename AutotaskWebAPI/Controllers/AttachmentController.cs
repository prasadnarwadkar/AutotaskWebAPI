using AutotaskWebAPI.Models;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    public class AttachmentController : ApiController
    {
        private AutotaskAPI api = null;

        public AttachmentController()
        {
            api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"], ConfigurationManager.AppSettings["APIPassword"]);
        }

        [Route("api/attachment/GetInfoByParentId/{parentId}")]
        [HttpGet]
        public HttpResponseMessage GetInfoByParentId(string parentId)
        {
            string errorMsg = string.Empty;

            var result = api.GetAttachmentInfoByParentId(parentId, out errorMsg);

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
            string errorMsg = string.Empty;

            int idInt = 0;

            bool parseResult = int.TryParse(id, out idInt);

            if (parseResult)
            {
                var result = api.GetAttachmentById(idInt, out errorMsg);

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
            string errorMsg = string.Empty;

            var result = api.GetAttachmentInfoByAttachDate(attachDate, out errorMsg);

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
            string errorMsg = string.Empty;

            var result = api.GetAttachmentInfoByParentIdAndAttachDate(parentId,
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
    }
}
