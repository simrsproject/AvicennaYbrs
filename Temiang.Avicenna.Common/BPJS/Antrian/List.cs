using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.BPJS.Antrian.List
{
    public class TaskId
    {
        public class Request
        {
            public class Root
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;
            }
        }

        public class Response : Metadata
        {
            public class List
            {
                [JsonProperty("wakturs")]
                public string Wakturs { get; set; }

                [JsonProperty("waktu")]
                public string Waktu { get; set; }

                [JsonProperty("taskname")]
                public string Taskname { get; set; }

                [JsonProperty("taskid")]
                public int Taskid { get; set; }

                [JsonProperty("kodebooking")]
                public string Kodebooking { get; set; }
            }

            public class TResponse
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("response")]
                public List<List> Response;

                [JsonProperty("metadata")]
                public Metadata Metadata;
            }
        }
    }

    public class Dashboard
    {
        public class PerTanggal : Metadata
        {
            public class List
            {
                [JsonProperty("kdppk")]
                public string Kdppk { get; set; }

                [JsonProperty("waktu_task1")]
                public string WaktuTask1 { get; set; }

                [JsonProperty("avg_waktu_task4")]
                public string AvgWaktuTask4 { get; set; }

                [JsonProperty("jumlah_antrean")]
                public int JumlahAntrean { get; set; }

                [JsonProperty("avg_waktu_task3")]
                public string AvgWaktuTask3 { get; set; }

                [JsonProperty("namapoli")]
                public string Namapoli { get; set; }

                [JsonProperty("avg_waktu_task6")]
                public string AvgWaktuTask6 { get; set; }

                [JsonProperty("avg_waktu_task5")]
                public string AvgWaktuTask5 { get; set; }

                [JsonProperty("nmppk")]
                public string Nmppk { get; set; }

                [JsonProperty("avg_waktu_task2")]
                public string AvgWaktuTask2 { get; set; }

                [JsonProperty("avg_waktu_task1")]
                public string AvgWaktuTask1 { get; set; }

                [JsonProperty("kodepoli")]
                public string Kodepoli { get; set; }

                [JsonProperty("waktu_task5")]
                public string WaktuTask5 { get; set; }

                [JsonProperty("waktu_task4")]
                public string WaktuTask4 { get; set; }

                [JsonProperty("waktu_task3")]
                public string WaktuTask3 { get; set; }

                [JsonProperty("insertdate")]
                public string Insertdate { get; set; }

                [JsonProperty("tanggal")]
                public string Tanggal { get; set; }

                [JsonProperty("waktu_task2")]
                public string WaktuTask2 { get; set; }

                [JsonProperty("waktu_task6")]
                public string WaktuTask6 { get; set; }

                [JsonIgnore]
                public int JumlahPasien { get; set; }

                [JsonIgnore]
                public double Persentase { get; set; }
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata;

                [JsonProperty("response")]
                public Response Response;
            }
        }

        public class PerBulan : Metadata
        {
            public class List
            {
                [JsonProperty("kdppk")]
                public string Kdppk { get; set; }

                [JsonProperty("waktu_task1")]
                public string WaktuTask1 { get; set; }

                [JsonProperty("avg_waktu_task4")]
                public string AvgWaktuTask4 { get; set; }

                [JsonProperty("jumlah_antrean")]
                public string JumlahAntrean { get; set; }

                [JsonProperty("avg_waktu_task3")]
                public string AvgWaktuTask3 { get; set; }

                [JsonProperty("namapoli")]
                public string Namapoli { get; set; }

                [JsonProperty("avg_waktu_task6")]
                public string AvgWaktuTask6 { get; set; }

                [JsonProperty("avg_waktu_task5")]
                public string AvgWaktuTask5 { get; set; }

                [JsonProperty("nmppk")]
                public string Nmppk { get; set; }

                [JsonProperty("avg_waktu_task2")]
                public string AvgWaktuTask2 { get; set; }

                [JsonProperty("avg_waktu_task1")]
                public string AvgWaktuTask1 { get; set; }

                [JsonProperty("kodepoli")]
                public string Kodepoli { get; set; }

                [JsonProperty("waktu_task5")]
                public string WaktuTask5 { get; set; }

                [JsonProperty("waktu_task4")]
                public string WaktuTask4 { get; set; }

                [JsonProperty("waktu_task3")]
                public string WaktuTask3 { get; set; }

                [JsonProperty("insertdate")]
                public string Insertdate { get; set; }

                [JsonProperty("tanggal")]
                public string Tanggal { get; set; }

                [JsonProperty("waktu_task2")]
                public string WaktuTask2 { get; set; }

                [JsonProperty("waktu_task6")]
                public string WaktuTask6 { get; set; }

                [JsonIgnore]
                public int JumlahPasien { get; set; }

                [JsonIgnore]
                public double Persentase { get; set; }
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("metadata")]
                public Metadata Metadata;

                [JsonProperty("response")]
                public Response Response;
            }
        }
    }

    public class Antrean
    {
        public class PerTanggal : Metadata
        {
            public class List
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking { get; set; }

                [JsonProperty("tanggal")]
                public string Tanggal { get; set; }

                [JsonProperty("kodepoli")]
                public string Kodepoli { get; set; }

                [JsonProperty("kodedokter")]
                public int? Kodedokter { get; set; }

                [JsonProperty("jampraktek")]
                public string Jampraktek { get; set; }

                [JsonProperty("nik")]
                public string Nik { get; set; }

                [JsonProperty("nokapst")]
                public string Nokapst { get; set; }

                [JsonProperty("nohp")]
                public string Nohp { get; set; }

                [JsonProperty("norekammedis")]
                public string Norekammedis { get; set; }

                [JsonProperty("jeniskunjungan")]
                public int? Jeniskunjungan { get; set; }

                [JsonProperty("nomorreferensi")]
                public string Nomorreferensi { get; set; }

                [JsonProperty("sumberdata")]
                public string Sumberdata { get; set; }

                [JsonProperty("ispeserta")]
                public bool Ispeserta { get; set; }

                [JsonProperty("noantrean")]
                public string Noantrean { get; set; }

                [JsonProperty("estimasidilayani")]
                public long? Estimasidilayani { get; set; }

                [JsonProperty("createdtime")]
                public long? Createdtime { get; set; }

                [JsonProperty("status")]
                public string Status { get; set; }
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("response")]
                public Response Response;

                [JsonProperty("metadata")]
                public Metadata Metadata;
            }
        }

        public class PerKodeBooking : Metadata
        {
            public class List
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("tanggal")]
                public string Tanggal;

                [JsonProperty("kodepoli")]
                public string Kodepoli;

                [JsonProperty("kodedokter")]
                public int? Kodedokter;

                [JsonProperty("jampraktek")]
                public string Jampraktek;

                [JsonProperty("nik")]
                public string Nik;

                [JsonProperty("nokapst")]
                public string Nokapst;

                [JsonProperty("nohp")]
                public string Nohp;

                [JsonProperty("norekammedis")]
                public string Norekammedis;

                [JsonProperty("jeniskunjungan")]
                public int? Jeniskunjungan;

                [JsonProperty("nomorreferensi")]
                public string Nomorreferensi;

                [JsonProperty("sumberdata")]
                public string Sumberdata;

                [JsonProperty("ispeserta")]
                public bool Ispeserta;

                [JsonProperty("noantrean")]
                public string Noantrean;

                [JsonProperty("estimasidilayani")]
                public long? Estimasidilayani;

                [JsonProperty("createdtime")]
                public long? Createdtime;

                [JsonProperty("status")]
                public string Status;
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("response")]
                public Response Response;

                [JsonProperty("metadata")]
                public Metadata Metadata;
            }
        }

        public class BelumDilayani : Metadata
        {
            public class List
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking { get; set; }

                [JsonProperty("tanggal")]
                public string Tanggal { get; set; }

                [JsonProperty("kodepoli")]
                public string Kodepoli { get; set; }

                [JsonProperty("kodedokter")]
                public int? Kodedokter { get; set; }

                [JsonProperty("jampraktek")]
                public string Jampraktek { get; set; }

                [JsonProperty("nik")]
                public string Nik { get; set; }

                [JsonProperty("nokapst")]
                public string Nokapst { get; set; }

                [JsonProperty("nohp")]
                public string Nohp { get; set; }

                [JsonProperty("norekammedis")]
                public string Norekammedis { get; set; }

                [JsonProperty("jeniskunjungan")]
                public int? Jeniskunjungan { get; set; }

                [JsonProperty("nomorreferensi")]
                public string Nomorreferensi { get; set; }

                [JsonProperty("sumberdata")]
                public string Sumberdata { get; set; }

                [JsonProperty("ispeserta")]
                public bool? Ispeserta { get; set; }

                [JsonProperty("noantrean")]
                public string Noantrean { get; set; }

                [JsonProperty("estimasidilayani")]
                public long? Estimasidilayani { get; set; }

                [JsonProperty("createdtime")]
                public long? Createdtime { get; set; }

                [JsonProperty("status")]
                public string Status { get; set; }

                [JsonIgnore]
                public string Namapoli { get; set; }

                [JsonIgnore]
                public string Namadokter { get; set; }

                [JsonIgnore]
                public string Namapasien { get; set; }
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("response")]
                public Response Response;

                [JsonProperty("metadata")]
                public Metadata Metadata;
            }
        }

        public class BelumDilayaniPerPoliPerDokterPerHariPerJamPraktek : Metadata
        {
            public class List
            {
                [JsonProperty("kodebooking")]
                public string Kodebooking;

                [JsonProperty("tanggal")]
                public string Tanggal;

                [JsonProperty("kodepoli")]
                public string Kodepoli;

                [JsonProperty("kodedokter")]
                public int? Kodedokter;

                [JsonProperty("jampraktek")]
                public string Jampraktek;

                [JsonProperty("nik")]
                public string Nik;

                [JsonProperty("nokapst")]
                public string Nokapst;

                [JsonProperty("nohp")]
                public string Nohp;

                [JsonProperty("norekammedis")]
                public string Norekammedis;

                [JsonProperty("jeniskunjungan")]
                public int? Jeniskunjungan;

                [JsonProperty("nomorreferensi")]
                public string Nomorreferensi;

                [JsonProperty("sumberdata")]
                public string Sumberdata;

                [JsonProperty("ispeserta")]
                public int? Ispeserta;

                [JsonProperty("noantrean")]
                public string Noantrean;

                [JsonProperty("estimasidilayani")]
                public long? Estimasidilayani;

                [JsonProperty("createdtime")]
                public long? Createdtime;

                [JsonProperty("status")]
                public string Status;
            }

            public class Response
            {
                [JsonProperty("list")]
                public List<List> List;
            }

            public class Root
            {
                [JsonProperty("response")]
                public Response Response;

                [JsonProperty("metadata")]
                public Metadata Metadata;
            }
        }
    }
}