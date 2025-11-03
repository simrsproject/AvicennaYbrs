using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Bridging.SatuSehat.BusinessObject.Kfa
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ActiveIngredient
    {
        [JsonProperty("kfa_code", NullValueHandling = NullValueHandling.Ignore)]
        public string KfaCode { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("zat_aktif", NullValueHandling = NullValueHandling.Ignore)]
        public string ZatAktif { get; set; }

        [JsonProperty("kekuatan_zat_aktif", NullValueHandling = NullValueHandling.Ignore)]
        public string KekuatanZatAktif { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedAt { get; set; }
    }

    public class Data
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("kfa_code", NullValueHandling = NullValueHandling.Ignore)]
        public string KfaCode { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public object Image { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedAt { get; set; }

        [JsonProperty("farmalkes_type", NullValueHandling = NullValueHandling.Ignore)]
        public FarmalkesType FarmalkesType { get; set; }

        [JsonProperty("produksi_buatan", NullValueHandling = NullValueHandling.Ignore)]
        public string ProduksiBuatan { get; set; }

        [JsonProperty("nie", NullValueHandling = NullValueHandling.Ignore)]
        public string Nie { get; set; }

        [JsonProperty("nama_dagang", NullValueHandling = NullValueHandling.Ignore)]
        public string NamaDagang { get; set; }

        [JsonProperty("manufacturer", NullValueHandling = NullValueHandling.Ignore)]
        public string Manufacturer { get; set; }

        [JsonProperty("registrar", NullValueHandling = NullValueHandling.Ignore)]
        public string Registrar { get; set; }

        [JsonProperty("generik", NullValueHandling = NullValueHandling.Ignore)]
        public bool Generik { get; set; }

        [JsonProperty("fix_price", NullValueHandling = NullValueHandling.Ignore)]
        public object FixPrice { get; set; }

        [JsonProperty("het_price", NullValueHandling = NullValueHandling.Ignore)]
        public double HetPrice { get; set; }

        [JsonProperty("farmalkes_hscode", NullValueHandling = NullValueHandling.Ignore)]
        public object FarmalkesHscode { get; set; }

        [JsonProperty("tayang_lkpp", NullValueHandling = NullValueHandling.Ignore)]
        public object TayangLkpp { get; set; }

        [JsonProperty("kode_lkpp", NullValueHandling = NullValueHandling.Ignore)]
        public object KodeLkpp { get; set; }

        [JsonProperty("net_weight", NullValueHandling = NullValueHandling.Ignore)]
        public object NetWeight { get; set; }

        [JsonProperty("net_weight_uom_name", NullValueHandling = NullValueHandling.Ignore)]
        public string NetWeightUomName { get; set; }

        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public object Volume { get; set; }

        [JsonProperty("volume_uom_name", NullValueHandling = NullValueHandling.Ignore)]
        public string VolumeUomName { get; set; }

        [JsonProperty("uom", NullValueHandling = NullValueHandling.Ignore)]
        public Uom Uom { get; set; }

        [JsonProperty("product_template", NullValueHandling = NullValueHandling.Ignore)]
        public ProductTemplate ProductTemplate { get; set; }

        [JsonProperty("active_ingredients", NullValueHandling = NullValueHandling.Ignore)]
        public List<ActiveIngredient> ActiveIngredients { get; set; }

        [JsonProperty("rxterm", NullValueHandling = NullValueHandling.Ignore)]
        public string Rxterm { get; set; }

        [JsonProperty("dose_per_unit", NullValueHandling = NullValueHandling.Ignore)]
        public int DosePerUnit { get; set; }

        [JsonProperty("dosage_form", NullValueHandling = NullValueHandling.Ignore)]
        public DosageForm DosageForm { get; set; }

        [JsonProperty("replacement", NullValueHandling = NullValueHandling.Ignore)]
        public Replacement Replacement { get; set; }

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<Tag> Tags { get; set; }
    }

    public class DosageForm
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public class FarmalkesType
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public string Group { get; set; }
    }

    public class ProductTemplate
    {
        [JsonProperty("kfa_code", NullValueHandling = NullValueHandling.Ignore)]
        public string KfaCode { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedAt { get; set; }
    }

    public class Replacement
    {
        [JsonProperty("product", NullValueHandling = NullValueHandling.Ignore)]
        public object Product { get; set; }

        [JsonProperty("template", NullValueHandling = NullValueHandling.Ignore)]
        public object Template { get; set; }
    }

    public class Root
    {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public Data Data { get; set; }
    }

    public class Tag
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }
    }

    public class Uom
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }


}
