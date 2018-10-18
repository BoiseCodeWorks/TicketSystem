using System;
using System.Collections.Generic;
using TicketSystem.Data;
using TicketSystem.Entities;

namespace TicketSystem.Business
{
    public class TicketService
    {
        private readonly ITicketRepo _ticketRepo;

        public TicketService(ITicketRepo ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public List<Ticket> GetAllTickets()
        {
            return _ticketRepo.GetTickets();
        }

        public Ticket GetTicketById(int id)
        {
            // TODO security....
            return _ticketRepo.GetTicketById(id);
        } 

        public Ticket CloseTicket(Ticket ticket)
        {
            var currentTicket = GetTicketById(ticket.Id);

            if (currentTicket == null) {throw new Exception("Ticket not found"); }

            if(currentTicket.ClosedAt != null)
            {
                throw new Exception("Ticket Already Closed");
            }

            currentTicket.ClosedAt = ticket.ClosedAt ?? DateTimeOffset.Now;
            _ticketRepo.UpdateTicket(currentTicket);

            return currentTicket;
        }
    }
}
