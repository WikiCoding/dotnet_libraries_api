using Library_Rest_API.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace LibraryIntegrationTests
{
    public class LibraryControllerTest : IClassFixture<ControllerTestSetup<Program>>
    {
        private readonly ControllerTestSetup<Program> _factory;
        private readonly HttpClient _client;
        private const string BASE_URL = "api/v1/libraries";

        public LibraryControllerTest(ControllerTestSetup<Program> factory)
        {
            _factory = factory;
            _client  = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }

        [Theory]
        [InlineData(BASE_URL)]
        public async void CreateLibrary_Success(string url)
        {
            // Arrange
            var libName = "lib1";
            var libReq = new LibraryRequest()
            {
                Name = libName
            };

            var serialized = JsonSerializer.Serialize(libReq);

            StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync(url, content);
            var responseJson = await response.Content.ReadFromJsonAsync<LibraryRequest>();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(libName, responseJson.Name);
        }
    }
}