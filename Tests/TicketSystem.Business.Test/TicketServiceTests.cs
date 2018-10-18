using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using TicketSystem.Data;
using TicketSystem.Entities;

namespace TicketSystem.Business.Test
{
    [TestClass]
    public class TicketServiceTests
    {
        [TestMethod]
        public void CloseTicket_Cannot_Close_Already_Closed_Ticket()
        {
            //arrange
            var ticket = new Ticket
            {
                Id = 1,
                ClosedAt = DateTimeOffset.Now
            };
            var mockTicketRepo = Substitute.For<ITicketRepo>();
            mockTicketRepo.GetTicketById(ticket.Id).Returns(ticket);

            var ticketService = new TicketService(mockTicketRepo);

            //act
            try
            {
                var expectedTicket = ticketService.CloseTicket(ticket);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Ticket Already Closed", ex.Message);
                return;
            }
            //assert
            Assert.Fail("Shouldn't have gotten here");
        }

        [TestMethod]
        public void CloseTicket_Can_Close_Ticket()
        {
            //arrange
            var userTicket = new Ticket
            {
                Id = 1
            };

            var mockTicketRepo = Substitute.For<ITicketRepo>();
            mockTicketRepo.GetTicketById(userTicket.Id).Returns(new Ticket
            {
                Id = 1,
                ClosedAt = null
            });

            var ticketService = new TicketService(mockTicketRepo);

            //act
            var expectedTicket = ticketService.CloseTicket(userTicket);

            //assert
            Assert.IsNotNull(expectedTicket.ClosedAt,"ClosedAt was not set");
            mockTicketRepo.Received(1).GetTicketById(userTicket.Id);
            mockTicketRepo.ReceivedWithAnyArgs(1).UpdateTicket(expectedTicket);
        }
    }
}
