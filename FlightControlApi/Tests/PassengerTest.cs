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

//<< Testiranje repozitorija na live bazi za model Passenger >> 
namespace FlightControlApi.Tests
{
    [TestClass]
    public class PassengerTests
    {

        private Repository<Passenger> repo;
        private Repository<PassengerVM> repoVM;

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







            Mock<Repository<Passenger>> mockRepository = new Mock<Repository<Passenger>>();
            Mock<Repository<PassengerVM>> mockRepositoryVM = new Mock<Repository<PassengerVM>>();




            mockRepository.Setup(mr => mr.FindAll()).Returns(passengers);

            mockRepositoryVM.Setup(mr => mr.FindAll()).Returns(passengersVM);


            mockRepository.Setup(mr => mr.GetById(It.IsAny<Int64>())).Returns((Int64 i) => passengers.Where(x => x.Id == i).Single());

            mockRepositoryVM.Setup(mr => mr.GetById(It.IsAny<Int64>())).Returns((Int64 i) => passengersVM.Where(x => x.Id == i).Single());




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
        public void TestPassengerControllerGet()
        {
            PassengerController pc = new PassengerController(repo, repoVM);
            IHttpActionResult actionResult = pc.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<PassengerVM>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Id);
        }


        [TestMethod]
        public void TestPassengerControllerGetAll()
        {
            PassengerController pc = new PassengerController(repo, repoVM);
            IEnumerable<PassengerVM> actionResult = pc.Get();
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(3, actionResult.Count());
        }


        [TestMethod]
        public void TestPassengerControllerPost()
        {
            PassengerController pc = new PassengerController(repo, repoVM);
            int beforeInsert = pc.Get().Count();
            IHttpActionResult actionResult = pc.Post(new Passenger { Email = "mocking@mocki.com", CountryId = 5, Identifier = "01923", Name = "Đuro" });
            int afterInsert = pc.Get().Count();
            var contentResult = actionResult as OkNegotiatedContentResult<Passenger>; ;
            Assert.IsNotNull(actionResult);
            Assert.IsTrue(beforeInsert == afterInsert);
        }


    }
}
