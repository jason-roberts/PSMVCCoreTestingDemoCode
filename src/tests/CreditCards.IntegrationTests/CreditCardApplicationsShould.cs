using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Xunit;

namespace CreditCards.IntegrationTests
{
    public class CreditCardApplicationsShould : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public CreditCardApplicationsShould(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task RenderApplicationForm()
        {
            var response = await _fixture.Client.GetAsync("/Apply");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("New Credit Card Application", responseString);
        }

        [Fact]
        public async Task NotAcceptPostedApplicationDetailsWithMissingFrequentFlyerNumber()
        {     
            // Get initial response that contains anti forgery tokens
            HttpResponseMessage initialResponse = await _fixture.Client.GetAsync("/Apply");
            var antiForgeryValues = await _fixture.ExtractAntiForgeryValues(initialResponse);          


            // Create POST request, adding anti forgery cookie and form field
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Apply");

            postRequest.Headers.Add("Cookie", 
                new CookieHeaderValue(TestServerFixture.AntiForgeryCookieName,
                                      antiForgeryValues.cookieValue).ToString());

            var formData = new Dictionary<string, string>
            {
                {TestServerFixture.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"FirstName", "Sarah"},
                {"LastName", "Smith"},
                {"Age", "18"},
                {"GrossAnnualIncome", "100000"}
                // Frequent flyer number omitted
            };

            postRequest.Content = new FormUrlEncodedContent(formData);

            var postResponse = await _fixture.Client.SendAsync(postRequest);
            postResponse.EnsureSuccessStatusCode();

            var responseString = await postResponse.Content.ReadAsStringAsync();
            Assert.Contains("Please provide a frequent flyer number", responseString);
        }
    }
}
