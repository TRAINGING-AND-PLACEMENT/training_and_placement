using Demo.Models;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Converters;
using CsvHelper.Configuration.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Controllers.Json
{
    public partial class JsonDecode
    {
        [JsonProperty("success")]
        [AllowNull]
        public bool Success { get; set; }

        [JsonProperty("user")]
        [AllowNull]
        public Dictionary<string, string>[] User { get; set; }

        [JsonProperty("sessions")]
        [AllowNull]
        public Dictionary<string, string>[] Sessions { get; set; }

        [JsonProperty("session")]
        public List<Sessions> session { get; set; }

        [JsonProperty("departments")]
        public List<Department> departments { get; set; }

        [JsonProperty("sectors")]
        public List<Sector> sectors { get; set; }

        [JsonProperty("student")]
        [AllowNull]
        public Dictionary<string, string>[] Student { get; set; }

        [JsonProperty("studentInfo")]
        public Student[] studentInfo { get; set; }

        [JsonProperty("companies")]
        [AllowNull]
        public List<Company> Companies { get; set; }

        [JsonProperty("company")]
        public Company[] Company { get; set; }

        [JsonProperty("hirings")]
        [AllowNull]
        public List<Hiring> hirings { get; set; }

    }
    public partial class JsonDecode
    {
        public static JsonDecode FromJson(string json) => JsonConvert.DeserializeObject<JsonDecode>(json, Demo.Controllers.Json.Converter.Settings);
    }
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
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