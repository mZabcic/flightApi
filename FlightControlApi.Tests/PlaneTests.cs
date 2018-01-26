using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FlightControlApi;
using NHibernate;

namespace FlightControlApi.Tests
{
    [TestClass]
    public class PlaneTests
    {
        [TestMethod]
        public void TestCreate()
        {
            var sessionMock = new Mock<ISession>();
            var queryMock = new Mock<IQuery>();
            var transactionMock = new Mock<ITransaction>();

            sessionMock.SetupGet(x => x.Transaction).Returns(transactionMock.Object);
            sessionMock.Setup(session => session.CreateQuery("from User")).Returns(queryMock.Object);
            queryMock.Setup(x => x.List<User>()).Returns(userList);
        }
    }
}
