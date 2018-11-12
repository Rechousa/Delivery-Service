using DeliveryService.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;

namespace DeliveryService.Tests
{
    [TestClass]
    public class LocationIntegrationTests
    {
        private readonly HttpClient _client;

        public LocationIntegrationTests()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                //.UseContentRoot(projectDir)
                .UseConfiguration(new ConfigurationBuilder()
                    //.SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        [TestMethod]
        public void LocationGetAllTest()
        {
            // Arrange:
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/locations");

            // Act:
            var response = _client.SendAsync(request).Result;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void LocationGetExistingItemTest(int id)
        {
            // Arrange:
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/locations/{id}");

            // Act:
            var response = _client.SendAsync(request).Result;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        [DataRow(0)]
        public void LocationGetNonExistingItemTest(int id)
        {
            // Arrange:
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/locations/{id}");

            // Act:
            var response = _client.SendAsync(request).Result;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void LocationGetMultipleExistingItemsTest(int id)
        {
            // Arrange:
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/locations/{id}");

            // Act:
            var response = _client.SendAsync(request).Result;

            // Assert:
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        [DataRow(-1)]
        [DataRow(-2)]
        [DataRow(-3)]
        [DataRow(int.MinValue)]
        [DataRow(int.MaxValue)]
        public void LocationGetMultipleNonExistingItemsTest(int id)
        {
            // Arrange:
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"/api/locations/{id}");

            // Act:
            var response = _client.SendAsync(request).Result;

            // Assert:
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }


        [TestMethod]
        public void LocationPostEmptyObjectTest()
        {
            // Arrange:
            var request = new HttpRequestMessage(new HttpMethod("POST"), "/api/locations/");

            // Act:
            var response = _client.SendAsync(request).Result;

            // Assert:
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
