using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Author: RefAndDisplay
    {
    }
    public class Custodian: RefAndDisplay
    {
    }

    public class CompositionPost
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("identifier")]
        public Identifier Identifier { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public Code Type { get; set; }

        [JsonProperty("category")]
        public List<Category> Category { get; set; }

        [JsonProperty("subject")]
        public RefAndDisplay Subject { get; set; }

        [JsonProperty("encounter")]
        public RefAndDisplay Encounter { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("author")]
        public List<Author> Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("custodian")]
        public Custodian Custodian { get; set; }

        [JsonProperty("section")]
        public List<Section> Section { get; set; }
    }

    public class Section
    {
        [JsonProperty("code")]
        public Code Code { get; set; }

        [JsonProperty("text")]
        public Text Text { get; set; }
    }

    public class Text
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("div")]
        public string Div { get; set; }
    }



}
