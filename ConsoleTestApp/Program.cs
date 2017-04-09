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
            ApiBase.Init("YOUR Autotask API username here",
                          "YOUR Autotask API password here");
            string errorMsg = string.Empty;

            if (ApiBase.IsApiInitialized())
            {
                // out parameter 'errorMsg' contains the error(s) thrown by SOAP API.
                
                // Replace the dummy ticket id below with real ticket id.
                var notes = new NotesAPI().GetNoteByTicketId(12345, out errorMsg);

                if (string.IsNullOrEmpty(errorMsg))
                {
                    foreach (TicketNote note in notes)
                    {
                        Console.WriteLine(string.Format("Note details=> Note id: {0} Note Title: {1} Note Description: {2}", note.id, note.Title, note.Description));
                    }
                }

                // out parameter 'errorMsg' contains the error(s) thrown by SOAP API.

                // Replace the dummy ticket id below with real ticket id.
                var ticket = new TicketsAPI().GetTicketById(12345, out errorMsg);

                if (string.IsNullOrEmpty(errorMsg))
                {
                    Console.WriteLine(string.Format("Ticket details=> Ticket id: {0} Ticket Title: {1} Ticket Description: {2}", ticket.id, ticket.Title, ticket.Description));
                }
            }

            Console.ReadLine();
        }
    }
}
