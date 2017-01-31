using AutotaskWebAPI.Autotask.Net.Webservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AutotaskWebAPI.Models
{
    public class NotesAPI
    {
        private AutotaskAPI api = null;

        public NotesAPI(AutotaskAPI apiInstance)
        {
            api = apiInstance;
        }

        /// <summary>
        /// Get a note by its ticket id.
        /// </summary>
        /// <param name="ticketId"></param>
        /// <param name="errorMsg">Out parameter to set error from AT SOAP API.</param>
        /// <returns></returns>
        public List<TicketNote> GetNoteByTicketId(string ticketId, out string errorMsg)
        {
            List<TicketNote> list = new List<TicketNote>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>TicketNote</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>TicketID<expression op=\"equals\">");
            strResource.Append(ticketId);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((TicketNote)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<TicketNote> GetNoteByLastActivityDate(string lastActivityDate,
                                                          out string errorMsg)
        {
            List<TicketNote> list = new List<TicketNote>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>TicketNote</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>LastActivityDate<expression op=\"greaterthan\">");
            strResource.Append(lastActivityDate);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((TicketNote)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public List<TicketNote> GetNoteById(string id, out string errorMsg)
        {
            List<TicketNote> list = new List<TicketNote>();

            string ret = string.Empty;
            errorMsg = string.Empty;

            // Query
            StringBuilder strResource = new StringBuilder();
            strResource.Append("<queryxml version=\"1.0\">");
            strResource.Append("<entity>TicketNote</entity>");
            strResource.Append("<query>");
            strResource.Append("<field>id<expression op=\"equals\">");
            strResource.Append(id);
            strResource.Append("</expression></field>");
            strResource.Append("</query></queryxml>");

            ATWSResponse respResource = api._atwsServices.query(strResource.ToString());

            if (respResource.ReturnCode > 0 && respResource.EntityResults.Length > 0)
            {
                foreach (Entity entity in respResource.EntityResults)
                {
                    list.Add((TicketNote)entity);
                }
            }
            else if (respResource.Errors != null &&
                    respResource.Errors.Length > 0)
            {
                errorMsg = respResource.Errors[0].Message;
            }

            return list;
        }

        public TicketNote CreateTicketNote(long ticketId, string title,
                                        string description, long creatorResourceID,
                                        long noteType, long publish)
        {
            TicketNote retNote = null;

            // Time to create the Note.
            TicketNote noteAct = new TicketNote();

            noteAct.TicketID = ticketId;
            noteAct.CreatorResourceID = creatorResourceID;
            noteAct.Title = title;
            noteAct.Description = description;
            noteAct.NoteType = noteType;
            noteAct.Publish = publish;

            Entity[] entNote = new Entity[] { noteAct };

            ATWSResponse respNote = api._atwsServices.create(entNote);
            if (respNote.ReturnCode > 0 && respNote.EntityResults.Length > 0)
            {
                retNote = (TicketNote)respNote.EntityResults[0];
            }
            else
            {
                if (respNote.EntityReturnInfoResults.Length > 0)
                {
                    throw new Exception("Could not create the Contact: " + respNote.EntityReturnInfoResults[0].Message);
                }
            }

            return retNote;
        }
    }


}