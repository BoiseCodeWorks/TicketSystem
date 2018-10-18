using System;
using System.Collections.Generic;
using System.Data;
using TicketSystem.Entities;

namespace TicketSystem.Data
{
    public interface ITicketRepo
    {
        Ticket GetTicketById(int id);
        List<Ticket> GetTickets();
        void UpdateTicket(Ticket ticket);
    }

    public class TicketRepo : ITicketRepo
    {
        private readonly IDbConnection _db;

        public TicketRepo(IDbConnection db)
        {
            _db = db;
        }

        public void UpdateTicket(Ticket ticket)
        {
            //
            Console.WriteLine("ticket updated");
        }

        public Ticket GetTicketById(int id)
        {
            //DO NOT DO THIS OBVIOUSLY
            return new Ticket
            {
                Id = id,
                Title = $"My Ticket {id}",
                Description = "Blah",
                CreatedAt = DateTimeOffset.Now.AddDays(-1),
            };
        }

        public List<Ticket> GetTickets()
        {
            return new List<Ticket>
            {
                GetTicketById(1),
                GetTicketById(2),
                GetTicketById(3),
            };
        }

    }
}
