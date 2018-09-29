using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Models;
using System.Collections.Generic;

namespace VideoGameDBServiceTests.IntegrationTests
{
    [TestClass]
    public class GameIntegrationTests
    {
        private readonly HttpClient _client;

        public GameIntegrationTests()
        {
            var testEnv = new VideoGameDBServiceTests.IntegrationTests.IntegrationTestBuilder();
            _client = testEnv.ConfigureTests();
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestGames_GetAll()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Games/");

            // Act
            var response = _client.SendAsync(request).Result;
            var content = (response.Content.ReadAsAsync<List<Games>>()).Result;


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Count > 0);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestGames_GetSingle()
        {
            //Arrange 
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Games/472");

            //Act
            var response = _client.SendAsync(request).Result;
            var content = (response.Content.ReadAsAsync<Games>()).Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Id == 472);
        }
    }
}
