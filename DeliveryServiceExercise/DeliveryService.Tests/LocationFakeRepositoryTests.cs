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
    public class LocationFakeRepositoryTests
    {
        LocationsController _controller;
        ILocationRepository _repository;

        public LocationFakeRepositoryTests()
        {
            _repository = new LocationRepositoryFake();
            _controller = new LocationsController(_repository);
        }

        [TestMethod]
        public void LocationFakeRepositoryGetAllTest()
        {
            // Arrange:

            // Act:
            var request = _controller.GetLocation();
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Location>));

            var items = okResult.Value as List<Location>;
            Assert.AreEqual(5, items.Count);
        }

        [TestMethod]
        [DataRow(1)]
        public void LocationFakeRepositoryGetExistingItemTest(int id)
        {
            // Arrange:

            // Act:
            var request = _controller.GetLocation(id).Result;
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Location));

            var item = okResult.Value as Location;
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual("Lisboa", item.Name);
        }


        [TestMethod]
        [DataRow(0)]
        public void LocationFakeRepositoryGetNonExistingItemTest(int id)
        {
            // Arrange:

            // Act:
            var request = _controller.GetLocation(id).Result;
            var notFoundResult = request as NotFoundResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)notFoundResult.StatusCode);
        }


        [TestMethod]
        [DataRow(1, "Lisboa")]
        [DataRow(2, "Madrid")]
        [DataRow(3, "London")]
        [DataRow(4, "Paris")]
        [DataRow(5, "New York")]
        public void LocationFakeRepositoryGetMultipleExistingItemsTest(int id, string name)
        {
            var request = _controller.GetLocation(id).Result;
            var okResult = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)okResult.StatusCode);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Location));

            var item = okResult.Value as Location;
            Assert.AreEqual(id, item.Id);
            Assert.AreEqual(name, item.Name);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(-2)]
        [DataRow(-3)]
        [DataRow(int.MinValue)]
        [DataRow(int.MaxValue)]
        public void LocationFakeRepositoryGetMultipleNonExistingItemsTest(int id)
        {
            // Arrange:

            // Act:
            var request = _controller.GetLocation(id).Result;
            var notFoundResult = request as NotFoundResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)notFoundResult.StatusCode);
        }


        [TestMethod]
        public void LocationFakeRepositoryCreateItem()
        {
            // Arrange:
            var location = new Location { Name = "Berlin" };

            // Act:
            var request = _controller.PostLocation(location).Result;
            var result = request as CreatedAtActionResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
        }


        [TestMethod]
        public void LocationFakeRepositoryUpdateItem()
        {
            // Arrange:
            var location = new Location { Id = 1, Name = "Porto" };

            // Act:
            var request = _controller.PutLocation(1, location).Result;
            var result = request as OkObjectResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }


        [TestMethod]
        public void LocationFakeRepositoryDeleteItem()
        {
            // Arrange:

            // Act:
            var request = _controller.DeleteLocation(4).Result;
            var result = request as OkResult;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }
    }
}
