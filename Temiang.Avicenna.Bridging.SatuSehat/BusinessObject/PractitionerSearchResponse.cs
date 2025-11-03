using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PractitionerSearchResponse
{
    public class Address
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("extension")]
        public List<Extension> Extension { get; set; }

        [JsonProperty("line")]
        public List<string> Line { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }
    }

    public class Code
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Coding
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }
    }

    public class Entry
    {
        [JsonProperty("fullUrl")]
        public string FullUrl { get; set; }

        [JsonProperty("resource")]
        public Resource Resource { get; set; }

        [JsonProperty("search")]
        public Search Search { get; set; }
    }

    public class Extension
    {
        [JsonProperty("extension")]
        public List<ExtensionItem> ExtensionItem { get; set; }

    }
    public class ExtensionItem
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("valueCode")]
        public string ValueCode { get; set; }
    }

    public class Identifier
    {
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }
    }

    public class Meta
    {
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("versionId")]
        public string VersionId { get; set; }
    }

    public class Name
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }
    }

    public class Period
    {
        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }
    }

    public class Qualification
    {
        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("period")]
        public Period Period { get; set; }
    }

    public class Resource
    {
        [JsonProperty("address")]
        public List<Address> Address { get; set; }

        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("name")]
        public List<Name> Name { get; set; }

        [JsonProperty("qualification")]
        public List<Qualification> Qualification { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }
    }

    public class Root
    {
        [JsonProperty("entry")]
        public List<Entry> Entry { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Search
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }
    }




}