using Library_Rest_API.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace LibraryTest.Controllers
{
    [TestClass]
    public class LibraryControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public LibraryControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/v1/libraries")]
        public async Task Post_CreateLibrary_ReturnsCreated(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            var request = new LibraryRequest { Name = "lib1" };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode();
            Xunit.Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var createdLibrary = JsonConvert.DeserializeObject<LibrariesResponse>(responseString);
            Xunit.Assert.Equal("lib1", createdLibrary.Name);
        }
    }
}
