using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using PrivateForum.App.Web;

namespace PrivateForum.IntegrationTests.Fixtures
{
    public class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public TestContext()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            Client = _server.CreateClient();
        }
    }
}
