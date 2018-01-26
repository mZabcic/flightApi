using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FlightControlApi;
using NHibernate;
using System.Collections.Generic;
using FlightControlApi.Models;
using FlightControlApi.Repository;

namespace FlightControlApi.Tests
{
    [TestClass]
    public class PassengerTests
    {

        private IRepository<Passenger> repo;
        private List<Int64> passengerIds;
        
        [TestInitialize]
        public void Initialize()
        {

            Passenger passenger1 = new Passenger { Email = "mocking@mocking.com", CountryId = 5, Identifier = "01923", Name = "Đuro"};
            Passenger passenger2 = new Passenger { Email = "mocking1@mocking.com", CountryId = 7, Identifier = "545", Name = "Pero" };
            Passenger passenger3 = new Passenger { Email = "mocking2@mocking.com", CountryId = 5, Identifier = "011254923", Name = "Štef" };
            passengerIds = new List<Int64>();
            repo = new Repository<Passenger>();
            passengerIds.Add(repo.Add(passenger1).Id);
            passengerIds.Add(repo.Add(passenger2).Id);
            passengerIds.Add(repo.Add(passenger3).Id);
        }

        [TestCleanup]
        public void Cleanup()
        {
            passengerIds.ForEach(r =>
            {
                repo.Delete(r);
            });
        }

        [TestMethod]
        public void CanReturnNewPassenger()
        {
            Passenger passenger1 = repo.GetById(passengerIds[0]);

            Assert.IsNotNull(passenger1); // Test if null
            Assert.IsTrue(passenger1.Identifier.Equals("01923") && passenger1.Email.Equals("mocking@mocking.com"));
        }
    }
}
