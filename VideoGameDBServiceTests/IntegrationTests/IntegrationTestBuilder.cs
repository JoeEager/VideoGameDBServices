using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore;
using VideoGameDBServices;
using System.Text;


namespace VideoGameDBServiceTests.IntegrationTests
{
    public class IntegrationTestBuilder
    {
        public HttpClient ConfigureTests()
        {

            var builder = WebHost.CreateDefaultBuilder()
                .UseEnvironment("Production")
                .UseStartup<Startup>();
            TestServer server = new TestServer(builder);
            return (server.CreateClient());
        }
    }
}
