using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace TravelAgent.DTO.Common
{
    public class StripeDTO
    {
        [JsonPropertyName("data")]
        public DataStripe Data { get; set; }
        public string Type { get; set; }

        public class DataStripe
        {
            [JsonProperty("object")]
            public Object Object { get; set; }
        }

        public class Object
        {
            public string Id { get; set; }
            [JsonProperty("payment_intent")]
            public string PaymentIntent { get; set; }
            [JsonProperty("payment_status")]
            public string Paid { get; set; }
        }
    }
}
