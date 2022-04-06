using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoGameDBServices.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

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
            var content = JsonConvert.DeserializeObject<List<YearIntegrationTests>>(response.Content.ReadAsStringAsync().Result);


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
            var content = JsonConvert.DeserializeObject<Years>(response.Content.ReadAsStringAsync().Result);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.Id == 1);
        }
    }
}
