using System.Text.Json.Serialization;

namespace MakeSurpriseProject.Models
{
    public class ApiResponseModel
    {
        [JsonPropertyName("predicted_interest")]
        public string PredictedInterest { get; set; }
    }
}
