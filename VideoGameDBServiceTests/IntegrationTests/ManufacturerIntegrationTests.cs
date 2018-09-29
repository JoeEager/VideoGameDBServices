using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Models;
using System.Collections.Generic;

namespace VideoGameDBServiceTests.IntegrationTests
{
    [TestClass]
    public class ManufacturerIntegrationTests
    {
        private readonly HttpClient _client;

        public ManufacturerIntegrationTests()
        {
            var testEnv = new VideoGameDBServiceTests.IntegrationTests.IntegrationTestBuilder();
            _client = testEnv.ConfigureTests();
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestManufacturer_GetAll()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Manufacturers/");

            // Act
            var response = _client.SendAsync(request).Result;
            var content = (response.Content.ReadAsAsync<List<Manufacturers>>()).Result;


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Count > 0);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestManufacturer_GetSingle()
        {
            //Arrange 
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Manufacturers/1");

            //Act
            var response = _client.SendAsync(request).Result;
            var content = (response.Content.ReadAsAsync<Manufacturers>()).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Id == 1);
        }
    }
}
