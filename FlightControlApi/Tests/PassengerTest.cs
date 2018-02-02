using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightControlApi;
using NHibernate;
using System.Collections.Generic;
using FlightControlApi.Models;
using FlightControlApi.Repository;
using NHibernate.Criterion;
using Moq;
using System.Linq;
using FlightControlApi.Controllers;
using System.Web.Http;
using System.Web.Http.Results;


namespace FlightControlApi.Tests
{
    [TestClass]
    public class PassengerTests
    {

        private IRepository<Passenger> repo;
        private IRepository<PassengerVM> repoVM;
        private IRepository<Ticket> repoTicket;
        private IRepository<Country> repoCountry;

        [TestInitialize]
        public void Initialize()
        {

      
            



            IList<Passenger> passengers = new List<Passenger>

                {

                     new Passenger { Id = 1, Email = "mocking@mocking.com", CountryId = 5, Identifier = "01923", Name = "Đuro" },
                   new Passenger { Id = 2, Email = "mocking1@mocking.com", CountryId = 7, Identifier = "545", Name = "Pero" },
                   new Passenger  { Id = 3, Email = "mocking2@mocking.com", CountryId = 5, Identifier = "011254923", Name = "Štef" }

        };

            IList<PassengerVM> passengersVM = new List<PassengerVM>

                {

                     new PassengerVM { Id = 1, Email = "mocking@mocking.com", CountryId = 5, Identifier = "01923", Name = "Đuro", Country = null },
                   new PassengerVM { Id = 2, Email = "mocking1@mocking.com", CountryId = 7, Identifier = "545", Name = "Pero" , Country = null},
                   new PassengerVM  { Id = 3, Email = "mocking2@mocking.com", CountryId = 5, Identifier = "011254923", Name = "Štef", Country = null }

        };


            IList<Country> country = new List<Country>

                {

                    new Country { Id = 1, name = "Croatia", iso="HR", iso3="CRO", numcode=385, printable_name="Republika Hrvatska", airports = null },
                    new Country { Id = 2, name = "Slovenia", iso="SV", iso3="SLO", numcode=386, printable_name="Republika Slovenija", airports = null },
                    new Country { Id = 3, name = "Bosnia and Herzegovina", iso="BH", iso3="BIH", numcode=384, printable_name="Bosna i Hercegovina", airports = null }
        };


            IList<Ticket> ticket = new List<Ticket>

                {

                     new Ticket { Id = 1, FlightId=1, PassengerId=2, Price=500, Revoked=false, SeatId=2, StoreId=1},
                      new Ticket { Id = 2, FlightId=1, PassengerId=1, Price=500, Revoked=false, SeatId=3, StoreId=1},
                       new Ticket { Id = 3, FlightId=4, PassengerId=2, Price=1000, Revoked=false, SeatId=2, StoreId=1}
        };






            Mock<IRepository<Passenger>> mockRepository = new Mock<IRepository<Passenger>>();
            Mock<IRepository<PassengerVM>> mockRepositoryVM = new Mock<IRepository<PassengerVM>>();
            Mock<IRepository<Ticket>> mockRepositoryTicket= new Mock<IRepository<Ticket>>();
            Mock<IRepository<Country>> mockRepositoryCountry = new Mock<IRepository<Country>>();



            mockRepository.Setup(mr => mr.FindAll()).Returns(passengers);

            mockRepositoryVM.Setup(mr => mr.FindAll()).Returns(passengersVM);

            mockRepositoryTicket.Setup(mr => mr.FindAll()).Returns(ticket);

            mockRepositoryCountry.Setup(mr => mr.FindAll()).Returns(country);


            mockRepository.Setup(mr => mr.GetById(It.IsAny<Int64>())).Returns((Int64 i) => passengers.Where(x => x.Id == i).SingleOrDefault());

            mockRepositoryVM.Setup(mr => mr.GetById(It.IsAny<Int64>())).Returns((Int64 i) => passengersVM.Where(x => x.Id == i).SingleOrDefault());

            mockRepositoryTicket.Setup(mr => mr.GetById(It.IsAny<Int64>())).Returns((Int64 i) => ticket.Where(x => x.Id == i).SingleOrDefault());

            mockRepositoryCountry.Setup(mr => mr.GetById(It.IsAny<Int64>())).Returns((Int64 i) => country.Where(x => x.Id == i).SingleOrDefault());

            mockRepositoryTicket.Setup(mr => mr.FindBy(It.IsAny<String>(), It.IsAny<Int64>())).Returns((String i, Int64 v) => ticket.Where(x => x.PassengerId == v));

            mockRepository.Setup(mr => mr.FindBy(It.IsAny<String>(), It.IsAny<String>())).Returns((String i, String v) => passengers.Where(x => x.Email == v));

            mockRepository.Setup(mr => mr.Add(It.IsAny<Passenger>())).Returns(

                (Passenger target) =>

                {

                        target.Id = passengers.Count() + 1;

                        passengers.Add(target);

                        return target;

                });

            mockRepositoryVM.Setup(mr => mr.Add(It.IsAny<PassengerVM>())).Returns(

             (PassengerVM target) =>

             {

                 target.Id = passengersVM.Count() + 1;

                 passengersVM.Add(target);

                 return target;

             });


            mockRepository.Setup(mr => mr.Delete(It.IsAny<Int64>())).Returns(

              (Int64 id) =>

              {

                  Passenger i = passengers.Where(p => p.Id == id).FirstOrDefault();
                  passengers.Remove(i);

                  return true;

              });

            mockRepositoryVM.Setup(mr => mr.Delete(It.IsAny<Int64>())).Returns(

            (Int64 id) =>

            {

                PassengerVM i = passengersVM.Where(p => p.Id == id).FirstOrDefault();
                passengersVM.Remove(i);

                return true;

            });



            mockRepository.Setup(mr => mr.Update(It.IsAny<Passenger>(), It.IsAny<Int64>())).Returns(

             (Passenger target, Int64 id) =>

             {
                 var original = passengers.Where(q => q.Id == id).FirstOrDefault();
                 if (original == null)

                 {

                     return false;

                 }

                 else
                 {
                     original.Name = target.Name;

                     original.Identifier = target.Identifier;

                     original.Email = target.Email;

                     original.CountryId = target.CountryId;

                     original.Id = target.Id;


                     return true;
                 }







             });


      mockRepositoryVM.Setup(mr => mr.Update(It.IsAny<PassengerVM>(), It.IsAny<Int64>())).Returns(

       (PassengerVM target, Int64 id) =>

       {
           PassengerVM original = passengersVM.Where(q => q.Id == id).FirstOrDefault();
           if (original == null)

           {

               return false;

           }

           else
           {
               original.Name = target.Name;

               original.Identifier = target.Identifier;

               original.Email = target.Email;

               original.CountryId = target.CountryId;

               original.Id = target.Id;


               return true;
           }

       });





            this.repo = mockRepository.Object;
            this.repoVM = mockRepositoryVM.Object;
            this.repoCountry = mockRepositoryCountry.Object;
            this.repoTicket = mockRepositoryTicket.Object;

        }

      

        [TestMethod]
        public void CanReturnNewPassenger()
        {
            Passenger passenger1 = repo.GetById(2);

            Assert.IsNotNull(passenger1); // Test if null
            Assert.AreEqual(passenger1.Id, 2);
        }


        [TestMethod]
        public void CanAddAndDeleteNewPassenger()
        {
            int beforeInsert = ((List<Passenger>)repo.FindAll()).Count;
            Passenger passengerNew = new Passenger { Email = "mocking4@mocking.com", CountryId = 7, Identifier = "1111", Name = "Joža" };
            passengerNew = repo.Add(passengerNew);
            int afterInsert = ((List<Passenger>)repo.FindAll()).Count;

            Assert.IsTrue(afterInsert > beforeInsert);
            Assert.IsTrue(passengerNew.Id == 4);

            repo.Delete(passengerNew.Id);

            int afterDelete = ((List<Passenger>)repo.FindAll()).Count;

            Assert.IsTrue(afterDelete == beforeInsert);
        }




        [TestMethod]
        public void UpdatePassenger()
        {
            Passenger beforeUpdate = repo.GetById(1);
            Assert.IsNotNull(beforeUpdate);


            beforeUpdate.Email = "test";
            repo.Update(beforeUpdate, 1);

           Passenger afterUpdate = repo.GetById(1);
            Assert.AreEqual(beforeUpdate.Email, "test");

        }

        [TestMethod]
        public void PassengerControllerGet()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            IHttpActionResult actionResult = pc.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<PassengerVM>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Id);
        }


        [TestMethod]
        public void PassengerControllerGetAll()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            IEnumerable<PassengerVM> actionResult = pc.Get();
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(3, actionResult.Count());
        }


        [TestMethod]
        public void PassengerControllerPost()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            IHttpActionResult actionResult = pc.Post(new Passenger { Email = "mocking123@mocking.com", CountryId = 1, Identifier = "01923", Name = "Đuro" });
            Assert.IsTrue(repo.FindAll().Count() == 4);
            var contentResult = actionResult as OkNegotiatedContentResult<Passenger>; ;
            Assert.IsNotNull(actionResult);
        }


        [TestMethod]
        public void PassengerControllerDelete()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            IHttpActionResult actionResult = pc.Delete(3);
            Assert.IsTrue(repo.FindAll().Count() == 2);
            var contentResult = actionResult as OkNegotiatedContentResult<Passenger>; ;
            Assert.IsNotNull(actionResult);
        }



        [TestMethod]
        public void PassengerControllerUpdate()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            Assert.IsTrue(repo.GetById(1).Name.Equals("Đuro"));
            Passenger newPassenger = new Passenger { Email = "mocking1234@mocking.com", CountryId = 2, Identifier = "01923", Name = "Zdenko" };
            IHttpActionResult actionResult = pc.Put(1, newPassenger);
            Assert.IsTrue(repo.GetById(1).Name.Equals("Zdenko"));
            var contentResult = actionResult as OkNegotiatedContentResult<Passenger>; ;
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void PassengerControllerPostValidationsEmailUnique()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            //Email vec postoji
            IHttpActionResult actionResult = pc.Post(new Passenger { Email = "mocking@mocking.com", CountryId = 1, Identifier = "01923", Name = "Đuro" });
            Assert.IsTrue(repo.FindAll().Count() == 3);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            var contentResult = actionResult as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message == "User with this email already exists");
        }


        [TestMethod]
        public void PassengerControllerPostValidationsEmailFormat()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            //Email vec postoji
            IHttpActionResult actionResult = pc.Post(new Passenger { Email = "mockin", CountryId = 1, Identifier = "01923", Name = "Đuro" });
            Assert.IsTrue(repo.FindAll().Count() == 3);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            var contentResult = actionResult as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message == "Email format is wrong");
        }



        [TestMethod]
        public void PassengerControllerPostValidationsCountry()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            //Email vec postoji
            IHttpActionResult actionResult = pc.Post(new Passenger { Email = "mocking123@mocking.com", CountryId = 5, Identifier = "01923", Name = "Đuro" });
            Assert.IsTrue(repo.FindAll().Count() == 3);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            var contentResult = actionResult as BadRequestErrorMessageResult;
            Assert.IsTrue(contentResult.Message == "CountryId is wrong");
        }


        [TestMethod]
        public void PassengerControllerDeleteTestValidation()
        {
            PassengerController pc = new PassengerController(repo, repoVM, repoCountry, repoTicket);
            IHttpActionResult actionResult = pc.Delete(1);
            Assert.IsTrue(repo.FindAll().Count() == 3);
            var contentResult = actionResult as OkNegotiatedContentResult<Passenger>; ;
            Assert.IsNotNull(actionResult);
        }



    }
}
