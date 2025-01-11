using MakeSurpriseProject.Models;
using System.Text.Json;
using System.Text;

namespace MakeSurpriseProject.ApiServices
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> PostAsync(string url, ApiRequestDataModel requestData)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(requestData);

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<ApiResponseModel>(responseBody);

                return apiResponse?.PredictedInterest ?? string.Empty; 
            }
            catch (Exception ex)
            {
                throw new Exception($"API isteği başarısız: {ex.Message}", ex);
            }
        }
    }
}
