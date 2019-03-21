using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CreditCards.IntegrationTests
{
    public class ValuesApiShould : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public ValuesApiShould(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetValidValue()
        {
            var response = await _fixture.Client.GetAsync("/api/values/1");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Value 1", responseString);
        }

        [Fact]
        public async Task ErrorOnInvalidValue()
        {
            var response = await _fixture.Client.GetAsync("/api/values/0");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task StartJob()
        {
            var response = await _fixture.Client.PostAsync("/api/values/startjob", null);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Batch Job Started", responseString);
        }

    }
}
