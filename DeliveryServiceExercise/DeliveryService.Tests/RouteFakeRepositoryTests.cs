using DeliveryService.API.Controllers;
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
            var request = _controller.GetRoute();
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Route>));

            var items = okResult.Value as List<Route>;
            Assert.AreEqual(4, items.Count);
        }

        [TestMethod]
        [DataRow(1)]
        public void RouteFakeRepositoryGetExistingItemTest(int id)
        {
            // Arrange:

            // Act:
            var request = _controller.GetRoute(id).Result;
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Route));

            var item = okResult.Value as Route;
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual(300, item.Cost);
        }


        [TestMethod]
        [DataRow(0)]
        public void RouteFakeRepositoryGetNonExistingItemTest(int id)
        {
            // Arrange:

            // Act:
            var request = _controller.GetRoute(id).Result;
            var notFoundResult = request as NotFoundResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)notFoundResult.StatusCode);
        }


        [TestMethod]
        [DataRow(1, 300)]
        [DataRow(2, 800)]
        [DataRow(3, 410)]
        [DataRow(4, 920)]
        public void RouteFakeRepositoryGetMultipleExistingItemsTest(int id, int cost)
        {
            var request = _controller.GetRoute(id).Result;
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Route));

            var item = okResult.Value as Route;
            Assert.AreEqual(id, item.Id);
            Assert.AreEqual(cost, item.Cost);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(-2)]
        [DataRow(-3)]
        [DataRow(int.MinValue)]
        [DataRow(int.MaxValue)]
        public void RouteFakeRepositoryGetMultipleNonExistingItemsTest(int id)
        {
            // Arrange:

            // Act:
            var request = _controller.GetRoute(id).Result;
            var notFoundResult = request as NotFoundResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)notFoundResult.StatusCode);
        }


        [TestMethod]
        public void RouteFakeRepositoryCreateItem()
        {
            // Arrange:
            var route = new Route { LocationA = 2, LocationB = 4, Distance = 1000, Cost = 212 };

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
            var route = new Route { Id = 1, LocationA = 1, LocationB = 2, Distance = 800, Cost = 299 };

            // Act:
            var request = _controller.PutRoute(1, route).Result;
            var result = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }


        [TestMethod]
        public void RouteFakeRepositoryDeleteItem()
        {
            // Arrange:

            // Act:
            var request = _controller.DeleteRoute(4).Result;
            var result = request as OkResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }
    }
}
