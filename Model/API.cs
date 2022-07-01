using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PublicAPIs
{
    public partial class Api
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("entries")]
        public List<Entry> Entries { get; set; }
    }

    public partial class Entry
    {
        [JsonProperty("API")]
        public string Api { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Auth")]
        public string Auth { get; set; }

        [JsonProperty("HTTPS")]
        public bool Https { get; set; }

        [JsonProperty("Cors")]
        public string Cors { get; set; }

        [JsonProperty("Link")]
        public Uri Link { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }
    }

    public partial class Api
    {
        public static Api FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Api>(json, PublicAPIs.Converter.Settings);
        }
    }

    public static partial class Serialize
    {
        public static string ToJson(Api api)
        {
            return JsonConvert.SerializeObject(api, PublicAPIs.Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}