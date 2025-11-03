using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace Temiang.Avicenna.Common.Worklist.RSTJ
{
    public class Service
    {
        public string SendJsonOrder(Json.Request.Root root)
        {
            var webrequest = (HttpWebRequest)WebRequest.Create("http://10.10.10.38/reqpasien");
            webrequest.Method = "POST";
            webrequest.ContentType = "application/json";

            byte[] formData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(root));
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            var response = webrequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.OK) return null;
            var sr = new StreamReader(response.GetResponseStream());
            //return JsonConvert.DeserializeObject<Json.Response.Root>(sr.ReadToEnd());
            return sr.ReadToEnd();
        }

        public Json.PdfResultResponse GetJsonResult(string notransaksi)
        {
            try
            {
                var webrequest = (HttpWebRequest)WebRequest.Create($"http://10.10.10.38/resbynotransaksi/{notransaksi}");
                webrequest.Method = "GET";
                webrequest.ContentType = "application/json";

                var response = webrequest.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.OK) return null;
                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Json.PdfResultResponse>(sr.ReadToEnd());
            }
            catch (Exception ex) { return null; }
        }

        public DataTable GetDataAbsensi(string nip, int bulan, int tahun)
        {
            var table = new DataTable();
            var str = $@"select nip, nama, tanggal, hari, kodewaktukerja, ka2, keterangan, jam_masuk, jam_pulang, jamtelat, menittelat, jamcepatpulang, menitcepatpulang, jamovertime, menitovertime, jamactual, menitactual, waktukerja, jadwalmasuk, jadwalkeluar from v_absen va where nip='{nip}' and month(tanggal)={bulan} and year(tanggal)={tahun} order by tanggal;";
            var connection = new MySqlConnection("Server=192.168.6.89;Port=3306;User ID=trk_read;Password=trk!@#;Database=siknet_rs");
            connection.Open();
            using (var command = new MySqlCommand(str, connection))
            {
                table.Load(command.ExecuteReader());
            }
            if (connection.State == ConnectionState.Open) connection.Close();
            return table;
        }
    }
}
