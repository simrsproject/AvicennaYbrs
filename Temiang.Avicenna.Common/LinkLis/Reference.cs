using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.LinkLis.Reference
{
    public class KodePemeriksaan
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Root
        {
            [JsonProperty("Status")]
            public string Status;

            [JsonProperty("kode_pemeriksaan")]
            public string KodePemeriksaan;
        }
    }

    public class Ruangan
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("kode_ruangan")]
            public string KodeRuangan;

            [JsonProperty("nama")]
            public string Nama;
        }

        public class Root
        {
            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }

    public class Dokter
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("id_dokter")]
            public string IdDokter;

            [JsonProperty("nama")]
            public string Nama;
        }

        public class Root
        {
            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }

    public class Analis
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("id_analis")]
            public string IdAnalis;

            [JsonProperty("nama")]
            public string Nama;
        }

        public class Root
        {
            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }

    public class DokterPK
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("id_dokterpk")]
            public string IdDokterpk;

            [JsonProperty("nama")]
            public string Nama;
        }

        public class Root
        {
            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }

    public class Status
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("id_status")]
            public string IdStatus;

            [JsonProperty("nama_status")]
            public string NamaStatus;
        }

        public class Root
        {
            [JsonProperty("data")]
            public List<Datum> Data;
        }
    }

    public class ListPemeriksaan
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("list_pemeriksaan")]
            public string ListPemeriksaan;

            [JsonProperty("nama_pemeriksaan")]
            public string NamaPemeriksaan;
        }

        public class Root
        {
            [JsonProperty("list_pemeriksaan")]
            public List<Datum> ListPemeriksaan;
        }
    }

    public class ListParameter
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Datum
        {
            [JsonProperty("kode")]
            public string Kode;

            [JsonProperty("nama_pemeriksaan")]
            public string NamaPemeriksaan;
        }

        public class Root
        {
            [JsonProperty("kode")]
            public List<Datum> ListParameter;
        }
    }

    public class Response
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Root
        {
            [JsonProperty("Status")]
            public string Status;

            [JsonProperty("Pesan")]
            public string Pesan;
        }
    }
}
