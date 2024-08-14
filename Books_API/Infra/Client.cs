using Books_API.Dto;
using Confluent.Kafka;

namespace Books_API.Infra
{
    public sealed class Client(HttpClient httpClient)
    {
        private const string LIBRARY_SERVICE_BASE_URL = "http://localhost:5199/api/v1/libraries";
        // uncomment after containerizing the whole app
        //private const string LIBRARY_SERVICE_BASE_URL = "http://library-service";

        public async Task<LibrariesResponse?> GetAsync(string libraryId)
        {
            var res = await httpClient.GetFromJsonAsync<LibrariesResponse>($"{LIBRARY_SERVICE_BASE_URL}/{libraryId}");

            return res;
        }
    }
}
