﻿using AutotaskWebAPI.Autotask.Net.Webservices;
using AutotaskWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutotaskWebAPI.Controllers
{
    public class NoteDetails
    {
        public long TicketId { get; set; }
        public long CreatorResourceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long NoteType { get; set; }
        public long Publish { get; set; }
    }

    public class NoteController : ApiController
    {
        private AutotaskAPI api = null;

        public NoteController()
        {
            api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"], ConfigurationManager.AppSettings["APIPassword"]);
        }

        // GET api/note/GetByTicketId?ticketId={}
        [HttpGet]
        public List<TicketNote> GetByTicketId(string ticketId)
        {
            return api.GetNoteByTicketId(ticketId);
        }

        // GET api/note/GetById?id={}
        [HttpGet]
        public List<TicketNote> GetById(string id)
        {
            return api.GetNoteById(id);
        }

        [HttpGet]
        public List<TicketNote> GetByLastActivityDate(string lastActivityDate)
        {
            return api.GetNoteByLastActivityDate(lastActivityDate);
        }

        [HttpPost]
        public long Post([FromBody] NoteDetails details)
        {
            try
            {
                if (details == null)
                {
                    return -3;
                }

                AutotaskAPI api = new AutotaskAPI(ConfigurationManager.AppSettings["APIUsername"],
                                                            ConfigurationManager.AppSettings["APIPassword"]);

                TicketNote note = api.CreateTicketNote(Convert.ToInt32(details.TicketId), 
                                            details.Title.ToString(),
                                            details.Description.ToString(), Convert.ToInt32(details.CreatorResourceId),
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
