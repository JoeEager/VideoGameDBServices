using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Models;
using System.Collections.Generic;

namespace VideoGameDBServiceTests.IntegrationTests
{
    [TestClass]
    public class YearIntegrationTests
    {
        private readonly HttpClient _client;

        public YearIntegrationTests()
        {
            var testEnv = new VideoGameDBServiceTests.IntegrationTests.IntegrationTestBuilder();
            _client = testEnv.ConfigureTests();
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestYears_GetAll()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Years/");

            // Act
            var response = _client.SendAsync(request).Result;
            var content = (response.Content.ReadAsAsync<List<Years>>()).Result;


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Count > 0);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestYears_GetSingle()
        {
            //Arrange 
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Years/1");

            //Act
            var response = _client.SendAsync(request).Result;
            var content = (response.Content.ReadAsAsync<Years>()).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Id == 1);
        }
    }
}
