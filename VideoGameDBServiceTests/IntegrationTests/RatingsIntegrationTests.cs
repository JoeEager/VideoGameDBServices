using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VideoGameDBServiceTests.IntegrationTests
{
    [TestClass]
    public class RatingsIntegrationTests
    {
        private readonly HttpClient _client;

        public RatingsIntegrationTests()
        {
            var testEnv = new VideoGameDBServiceTests.IntegrationTests.IntegrationTestBuilder();
            _client = testEnv.ConfigureTests();
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestRatings_GetAll()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Ratings/");

            // Act
            var response = _client.SendAsync(request).Result;
            var content = JsonConvert.DeserializeObject<List<Ratings>>(response.Content.ReadAsStringAsync().Result);


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Count > 0);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestRatings_GetSingle()
        {
            //Arrange 
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Ratings/1");

            //Act
            var response = _client.SendAsync(request).Result;
            var content = JsonConvert.DeserializeObject<Ratings>(response.Content.ReadAsStringAsync().Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Id == 1);
        }
    }
}
