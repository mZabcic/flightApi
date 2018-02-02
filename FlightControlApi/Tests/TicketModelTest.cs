using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightControlApi;
using NHibernate;
using System.Collections.Generic;
using FlightControlApi.Models;
using FlightControlApi.Repository;
using Moq;
using System.Linq;
using FlightControlApi.Controllers;
using System.Web.Http;
using System.Web.Http.Results;

namespace FlightControlApi.Tests
{
    [TestClass]
    public class TicketModelTest
    {
        Ticket ticket;
        TicketCreator ticketCreator;

        [TestInitialize]
        public void Initialize()
        {
            ticket = new Ticket { Id = 3, FlightId = 4, PassengerId = 2, Price = 1000, Revoked = false, SeatId = 2, StoreId = 1 };
            ticketCreator = new TicketCreator { Id = 3, FlightId = 4, PassengerId = 2, Price = 1000, SeatClassId = 3, StoreId = 1 };
        }

        [TestMethod]
        public void TicketCreate()
        {
            Ticket ticket = new Ticket { Id = 1, FlightId = 1, PassengerId = 2, Price = 500, Revoked = false, SeatId = 2, StoreId = 1 };
            Assert.IsNotNull(ticket);
            Assert.IsTrue(ticket.Id == 1);
        }

        [TestMethod]
        public void TicketCreatorCreate()
        {
            TicketCreator ticket = new TicketCreator { Id = 1, FlightId = 1, PassengerId = 2, Price = 500, SeatClassId = 1, StoreId = 1 };
            Assert.IsNotNull(ticket);
            Assert.IsTrue(ticket.Id == 1);
        }

        [TestMethod]
        public void TicketSet()
        {
            this.ticket.Id = 1;
            this.ticket.PassengerId = 2;
            this.ticket.Price = (Decimal) 20.25;
            this.ticket.Revoked = true;
            this.ticket.StoreId = 2;
            Assert.IsTrue(ticket.Id == 1);
            Assert.IsTrue(ticket.PassengerId == 2);
            Assert.IsTrue(ticket.Price == (Decimal)20.25);
            Assert.IsTrue(ticket.Revoked);
            Assert.IsTrue(ticket.StoreId == 2);
        }


        [TestMethod]
        public void TicketCreatorSet()
        {
            this.ticketCreator.Id = 1;
            this.ticketCreator.PassengerId = 2;
            this.ticketCreator.Price = (Decimal)20.25;
            this.ticketCreator.StoreId = 2;
            this.ticketCreator.SeatClassId = 1;
            Assert.IsTrue(ticketCreator.Id == 1);
            Assert.IsTrue(ticketCreator.PassengerId == 2);
            Assert.IsTrue(ticketCreator.Price == (Decimal)20.25);
            Assert.IsTrue(ticketCreator.SeatClassId == 1);
            Assert.IsTrue(ticketCreator.StoreId == 2);
        }


        [TestMethod]
        public void TicketCreatorCheckPassengerId()
        {
            Assert.IsFalse(ticketCreator.CheckPassengerId());
            this.ticketCreator.PassengerId = 0;
            Assert.IsTrue(ticketCreator.CheckPassengerId());
        }


        [TestMethod]
        public void TicketCreatorCheckFlightId()
        {
            Assert.IsFalse(ticketCreator.CheckFlightId());
            this.ticketCreator.FlightId = 0;
            Assert.IsTrue(ticketCreator.CheckFlightId());
        }

        [TestMethod]
        public void TicketCreatorCheckStoreId()
        {
            Assert.IsFalse(ticketCreator.CheckStoreId());
            this.ticketCreator.StoreId = 0;
            Assert.IsTrue(ticketCreator.CheckStoreId());
        }


        [TestMethod]
        public void TicketCreatorCheckSeatClass()
        {
            Assert.IsFalse(ticketCreator.CheckSeatClass());
            this.ticketCreator.SeatClassId = 0;
            Assert.IsTrue(ticketCreator.CheckSeatClass());
            this.ticketCreator.SeatClassId = 4;
            Assert.IsTrue(ticketCreator.CheckSeatClass());
        }


    }
}