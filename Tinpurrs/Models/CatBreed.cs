using System.Text.Json.Serialization;

namespace Tinpurrs.Models
{
    public class CatBreed
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("temperament")]
        public string Temperament { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("reference_image_id")]
        public string ReferenceImageId { get; set; }
    }
}
