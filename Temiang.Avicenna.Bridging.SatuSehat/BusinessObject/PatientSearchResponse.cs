using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.PatientSearch
{
   public class PatientSearchResponse
    {
        [JsonProperty("entry")]
        public List<Entry> Entry;

        [JsonProperty("resourceType")]
        public string ResourceType;

        [JsonProperty("total")]
        public int Total;

        [JsonProperty("type")]
        public string Type;
    }

   public class Entry
   {
       [JsonProperty("fullUrl")]
       public string FullUrl;

       [JsonProperty("resource")]
       public Resource Resource;

       [JsonProperty("search")]
       public Search Search;
   }
   public class Search
   {
       [JsonProperty("mode")]
       public string Mode;
   }
   public class Resource
   {
       [JsonProperty("active")]
       public bool Active;

       [JsonProperty("communication")]
       public List<Communication> Communication;

       [JsonProperty("deceasedBoolean")]
       public bool DeceasedBoolean;

       [JsonProperty("gender")]
       public string Gender;

       [JsonProperty("id")]
       public string Id;

       [JsonProperty("identifier")]
       public List<Identifier> Identifier;

       [JsonProperty("meta")]
       public Meta Meta;

       [JsonProperty("multipleBirthBoolean")]
       public bool MultipleBirthBoolean;

       [JsonProperty("name")]
       public List<Name> Name;

       [JsonProperty("resourceType")]
       public string ResourceType;
   }
   public class Name
   {
       [JsonProperty("text")]
       public string Text;

       [JsonProperty("use")]
       public string Use;
   }
   public class Communication
   {
       [JsonProperty("language")]
       public Language Language;

       [JsonProperty("preferred")]
       public bool Preferred;
   }
   public class Language
   {
       [JsonProperty("coding")]
       public List<Coding> Coding;

       [JsonProperty("text")]
       public string Text;
   }
}
