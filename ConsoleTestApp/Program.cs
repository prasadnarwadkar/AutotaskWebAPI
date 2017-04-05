using System;
using System.Configuration;
using WrapperLib.Autotask.Net.Webservices;
using WrapperLib.Models;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var notesApi = new NotesAPI(ConfigurationManager.AppSettings["APIUsername"],
                                        ConfigurationManager.AppSettings["APIPassword"]);
            string errorMsg = string.Empty;

            if (notesApi.IsApiInitialized())
            {
                // out parameter 'errorMsg' contains the error(s) thrown by SOAP API.
                
                // Replace the dummy ticket id below with real ticket id.
                var notes = notesApi.GetNoteByTicketId(12345, out errorMsg);

                if (string.IsNullOrEmpty(errorMsg))
                {
                    foreach (TicketNote note in notes)
                    {
                        Console.WriteLine(string.Format("Note details=> Note id: {0} Note Title: {1} Note Description: {2}", note.id, note.Title, note.Description));
                    }
                }                
            }

            var ticketApi = new TicketsAPI(ConfigurationManager.AppSettings["APIUsername"],
                                        ConfigurationManager.AppSettings["APIPassword"]);

            if (ticketApi.IsApiInitialized())
            {
                // out parameter 'errorMsg' contains the error(s) thrown by SOAP API.

                // Replace the dummy ticket id below with real ticket id.
                var ticket = ticketApi.GetTicketById(12345, out errorMsg);

                if (string.IsNullOrEmpty(errorMsg))
                {
                    Console.WriteLine(string.Format("Ticket details=> Ticket id: {0} Ticket Title: {1} Ticket Description: {2}", ticket.id, ticket.Title, ticket.Description));
                }
            }

            Console.ReadLine();
        }
    }
}
