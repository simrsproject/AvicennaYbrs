using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Master.Location
{
    public class PostPutResponse : Resource
    {
    }

    public class LocationSearchResponse
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

    public class Resource
    {
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("identifier")]
        public List<Identifier> Identifier { get; set; }

        [JsonProperty("managingOrganization")]
        public ManagingOrganization ManagingOrganization { get; set; }

        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public Meta Meta { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("physicalType")]
        public PhysicalType PhysicalType { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("telecom")]
        public List<Telecom> Telecom { get; set; }
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

    public class Meta
    {
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("versionId")]
        public string VersionId { get; set; }
    }

    public class Search
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }
    }


    public class Address
    {
        [JsonProperty("use")]
        public string Use { get; set; }

        [JsonProperty("line")]
        public List<string> Line { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("extension")]
        public List<ExtensionInfo> ExtensionInfo { get; set; }
    }

    public class Coding
    {
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }

    public class ExtensionInfo
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("extension")]
        public List<Extension> Extension { get; set; }

        //[JsonProperty("valueCode")]
        //public string ValueCode { get; set; }
    }

    public class Extension
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
    }

    public class ManagingOrganization
    {
        [JsonProperty("reference")]
        public string Reference { get; set; }
    }

    public class PhysicalType
    {
        [JsonProperty("coding")]
        public List<Coding> Coding { get; set; }
    }

    public class Position
    {
        [JsonProperty("altitude")]
        public int Altitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }


    public class Telecom
    {
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }
    }


}
