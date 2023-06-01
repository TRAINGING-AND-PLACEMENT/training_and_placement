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

        [JsonProperty("students")]
        [AllowNull]
        public List<Student> students { get; set; }

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

        [JsonProperty("hiring")]
        public Hiring[] Hiring { get; set; }

        [JsonProperty("hiringdepartments")]
        public Hiring_Departments[] Hiring_Departments { get; set; }

        [JsonProperty("hiringsectors")]
        public Hiring_sectors[] Hiring_sectors { get; set; }

        [JsonProperty("applications")]
        [AllowNull]
        public List<StudentApplication> applications { get; set; }

        [JsonProperty("application")]
        public StudentApplication[] StudentApplicationss { get; set; }

        [JsonProperty("internships")]
        [AllowNull]
        public List<Internships> internships { get; set; }

        [JsonProperty("internship")]
        public Internships[] internship { get; set; }

        [JsonProperty("additionalQualifications")]
        [AllowNull]
        public List<AdditionalQualif> additionalQualifications { get; set; }

        [JsonProperty("additionalQualification")]
        public AdditionalQualif[] additionalQualification { get; set; }


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