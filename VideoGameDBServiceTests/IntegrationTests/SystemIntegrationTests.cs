using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VideoGameDBServiceTests.IntegrationTests
{
    [TestClass]
    public class SystemIntegrationTests
    {
        private readonly HttpClient _client;

        public SystemIntegrationTests()
        {
            var testEnv = new VideoGameDBServiceTests.IntegrationTests.IntegrationTestBuilder();
            _client = testEnv.ConfigureTests();
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestSystems_GetAll()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Systems/");

            // Act
            var response = _client.SendAsync(request).Result;
            var content = JsonConvert.DeserializeObject<List<Systems>>(response.Content.ReadAsStringAsync().Result);


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Count > 0);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestSystems_GetSingle()
        {
            //Arrange 
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Systems/1");

            //Act
            var response = _client.SendAsync(request).Result;
            var content = JsonConvert.DeserializeObject<Systems>(response.Content.ReadAsStringAsync().Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Id == 1);
        }
    }
}
