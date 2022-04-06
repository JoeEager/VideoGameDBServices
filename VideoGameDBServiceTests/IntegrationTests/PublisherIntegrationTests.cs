using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VideoGameDBServiceTests.IntegrationTests
{
    [TestClass]
    public class PublisherIntegrationTests
    {
        private readonly HttpClient _client;

        public PublisherIntegrationTests()
        {
            var testEnv = new VideoGameDBServiceTests.IntegrationTests.IntegrationTestBuilder();
            _client = testEnv.ConfigureTests();
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestPublishers_GetAll()
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Publishers/");

            // Act
            var response = _client.SendAsync(request).Result;
            var content = JsonConvert.DeserializeObject<List<Publishers>>(response.Content.ReadAsStringAsync().Result);


            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Count > 0);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void IntegrationTestPublishers_GetSingle()
        {
            //Arrange 
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/Publishers/1");

            //Act
            var response = _client.SendAsync(request).Result;
            var content = JsonConvert.DeserializeObject<Publishers>(response.Content.ReadAsStringAsync().Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Id == 1);
        }
    }
}
