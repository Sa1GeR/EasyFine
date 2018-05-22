using FluentAssertions;
using PrivateForum.IntegrationTests.Fixtures;
using System.Threading.Tasks;
using System.Net;
using Xunit;

namespace PrivateForum.IntegrationTests.Scenarios
{
    [CollectionDefinition("SystemCollection")]
    public class PingTests
    {
        private readonly TestContext _sut;

        public PingTests(TestContext sut)
        {
            _sut = sut;
        }

        [Fact]
        public async Task PingReturnsOkResponse()
        {
            var response = await _sut.Client.GetAsync("/ping");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
