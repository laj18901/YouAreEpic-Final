using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YouAreEpic.Backend.Dtos
{
    public class TwitterOEmbedResponse
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("author_name")]
        public string AuthorName { get; set; }

        [JsonPropertyName("author_url")]
        public string AuthorUrl { get; set; }

        [JsonPropertyName("html")]
        public string Html { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public object Height { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("cache_age")]
        public string CacheAge { get; set; }

        [JsonPropertyName("provider_name")]
        public string ProviderName { get; set; }

        [JsonPropertyName("provider_url")]
        public string ProviderUrl { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
