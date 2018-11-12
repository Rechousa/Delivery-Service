using DeliveryService.API.Controllers;
using DeliveryService.Common;
using DeliveryService.Database;
using DeliveryService.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace DeliveryService.Tests
{
    [TestClass]
    public class RouteFakeRepositoryTests
    {
        RoutesController _controller;
        IRouteRepository _repository;

        public RouteFakeRepositoryTests()
        {
            _repository = new RouteRepositoryFake();
            _controller = new RoutesController(_repository);
        }

        [TestMethod]
        public void RouteFakeRepositoryGetAllTest()
        {
            // Arrange:

            // Act:
            var request = _controller.GetRoutes().Result;
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<RouteVM>));

            var items = okResult.Value as List<RouteVM>;
            Assert.AreEqual(4, items.Count);
        }

        [TestMethod]
        [DataRow(1, 2)]
        public void RouteFakeRepositoryGetExistingItemTest(int locationA, int locationB)
        {
            // Arrange:

            // Act:
            var request = _controller.GetRoute(locationA, locationB).Result;
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(RouteVM));

            var item = okResult.Value as RouteVM;
            Assert.AreEqual(1, item.LocationA);
            Assert.AreEqual(2, item.LocationB);
            Assert.AreEqual(300, item.Cost);
        }


        [TestMethod]
        [DataRow(0, 0)]
        public void RouteFakeRepositoryGetNonExistingItemTest(int locationA, int locationB)
        {
            // Arrange:

            // Act:
            var request = _controller.GetRoute(locationA, locationB).Result;
            var notFoundResult = request as NotFoundResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)notFoundResult.StatusCode);
        }


        [TestMethod]
        [DataRow(1, 2, 300)]
        [DataRow(1, 3, 800)]
        [DataRow(1, 4, 410)]
        [DataRow(1, 5, 920)]
        public void RouteFakeRepositoryGetMultipleExistingItemsTest(int locationA, int locationB, int cost)
        {
            var request = _controller.GetRoute(locationA, locationB).Result;
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(RouteVM));

            var item = okResult.Value as RouteVM;
            Assert.AreEqual(locationA, item.LocationA);
            Assert.AreEqual(locationB, item.LocationB);
            Assert.AreEqual(cost, item.Cost);
        }


        [TestMethod]
        [DataRow(-1, -1)]
        [DataRow(-2, -2)]
        [DataRow(-3, -3)]
        [DataRow(int.MinValue, int.MinValue)]
        [DataRow(int.MaxValue, int.MaxValue)]
        public void RouteFakeRepositoryGetMultipleNonExistingItemsTest(int locationA, int locationB)
        {
            // Arrange:

            // Act:
            var request = _controller.GetRoute(locationA, locationB).Result;
            var notFoundResult = request as NotFoundResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)notFoundResult.StatusCode);
        }


        [TestMethod]
        public void RouteFakeRepositoryCreateItem()
        {
            // Arrange:
            var route = new RouteVM { LocationA = 2, LocationB = 4, Distance = 1000, Cost = 212 };

            // Act:
            var request = _controller.PostRoute(route).Result;
            var result = request as CreatedAtActionResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
        }


        [TestMethod]
        public void RouteFakeRepositoryUpdateItem()
        {
            // Arrange:
            var route = new RouteVM { LocationA = 1, LocationB = 2, Distance = 800, Cost = 299 };

            // Act:
            var request = _controller.PutRoute(1, 2, route).Result;
            var result = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }


        [TestMethod]
        public void RouteFakeRepositoryDeleteItem()
        {
            // Arrange:

            // Act:
            var request = _controller.DeleteRoute(1, 2).Result;
            var result = request as OkResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }
    }
}
