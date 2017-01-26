using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    public class NoteController : ApiController
    {
        private AutotaskAPI api = null;

        public NoteController()
        {
            api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"], ConfigurationManager.AppSettings["APIPassword"]);
        }

        // GET api/note/GetByTicketId?ticketId={}
        [HttpGet]
        public List<Autotask.Net.Webservices.TicketNote> GetByTicketId(string ticketId)
        {
            return api.GetNoteByTicketId(ticketId);
        }

        // GET api/note/GetById?id={}
        [HttpGet]
        public List<Autotask.Net.Webservices.TicketNote> GetById(string id)
        {
            return api.GetNoteById(id);
        }

        [HttpPost]
        public long Post([FromBody] TicketNote details)
        {
            try
            {
                if (details == null)
                {
                    return -3;
                }

                AutotaskAPI api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                                            ConfigurationManager.AppSettings["APIPassword"]);

                Autotask.Net.Webservices.TicketNote note = api.CreateTicketNote(Convert.ToInt32(details.TicketID), 
                                            details.Title.ToString(),
                                            details.Description.ToString(), Convert.ToInt32(details.CreatorResourceID),
                                            Convert.ToInt32(details.NoteType), Convert.ToInt32(details.Publish));

                if (note != null)
                {
                    return note.id;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -2;
            }
        }
    }
}
