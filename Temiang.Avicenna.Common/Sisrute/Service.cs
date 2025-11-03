using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Common.Sisrute
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["InacbgServiceUrlLocation"];
        private string _consKey = ConfigurationManager.AppSettings["SisruteConsumerID"];
        private string _ppkKey = ConfigurationManager.AppSettings["SisruteHospitalID"];
        private string _salt = ConfigurationManager.AppSettings["SisruteSaltConsumerID"];

        private HttpWebRequest PopulateWebRequest(string url, BPJS.Helper.WebRequestMethod method, BPJS.Helper.WebRequestContentType contentType, string parameter)
        {
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != BPJS.Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            var formData = Encoding.UTF8.GetBytes(parameter.ToString());
            webrequest.ContentLength = formData.Length;

            using (var post = webrequest.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            return webrequest;
        }

        public Sisrute.Rujukan.Get GetRujukan(string nomor, string tanggal)
        {
            var param = string.Concat(new string[]
                {
                    "consid=",_consKey,
                    "&salt=",_salt,
                    "&nomor=", nomor,
                    "&tanggal=", tanggal
                });

            var sb = new StringBuilder();
            sb.Append(param);

            _url += "/sisrute_get_rujukan";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Sisrute.Rujukan.Get>(sr.ReadToEnd());
            }
        }

        public Sisrute.Rujukan.Post.Data GetRujukanByNo(string nomor)
        {
            var response = GetRujukan(nomor, string.Empty);
            return response.Data[0];
        }

        public string GetReferensi(string kode, int type)
        {
            var param = string.Concat(new string[]
                {
                    "consid=",_consKey,
                    "&salt=",_salt,
                    "&kode=", kode
                });

            var sb = new StringBuilder();
            sb.Append(param);

            if (type == 0) _url += "/sisrute_get_diagnosa";
            else if (type == 1) _url += "/sisrute_get_alasan";
            else _url += "/sisrute_get_faskes";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return sr.ReadToEnd();
            }
        }

        public Sisrute.Rujukan.Post SetRujukan(Rujukan.SetRujukan.RootObject entity, BPJS.Helper.WebRequestMethod method)
        {
            var param = string.Concat(new string[]
                {
                    "consid=",_consKey,
                    "&salt=",_salt,
                    "&NORM=", entity.PASIEN.NORM,
                    "&NIK=", entity.PASIEN.NIK,
                    "&NO_KARTU_JKN=", entity.PASIEN.NO_KARTU_JKN,
                    "&NAMA=", entity.PASIEN.NAMA,
                    "&JENIS_KELAMIN=", entity.PASIEN.JENIS_KELAMIN,
                    "&TANGGAL_LAHIR=", entity.PASIEN.TANGGAL_LAHIR,
                    "&TEMPAT_LAHIR=", entity.PASIEN.TEMPAT_LAHIR,
                    "&ALAMAT=", entity.PASIEN.ALAMAT,
                    "&KONTAK=", entity.PASIEN.KONTAK,
                    "&JENIS_RUJUKAN=", entity.RUJUKAN.JENIS_RUJUKAN,
                    "&TANGGAL=", entity.RUJUKAN.TANGGAL,
                    "&FASKES_TUJUAN=", entity.RUJUKAN.FASKES_TUJUAN,
                    "&ALASAN=", entity.RUJUKAN.ALASAN,
                    "&ALASAN_LAINNYA=", entity.RUJUKAN.ALASAN_LAINNYA,
                    "&DIAGNOSA=", entity.RUJUKAN.DIAGNOSA,
                    "&NIK_DOKTER=", entity.RUJUKAN.DOKTER.NIK,
                    "&NAMA_DOKTER=", entity.RUJUKAN.DOKTER.NAMA,
                    "&NIK_PETUGAS=", entity.RUJUKAN.PETUGAS.NIK,
                    "&NAMA_PETUGAS=", entity.RUJUKAN.PETUGAS.NAMA,
                    "&ANAMNESIS_DAN_PEMERIKSAAN_FISIK=", entity.KONDISI_UMUM.ANAMNESIS_DAN_PEMERIKSAAN_FISIK,
                    "&KESADARAN=", entity.KONDISI_UMUM.KESADARAN,
                    "&TEKANAN_DARAH=", entity.KONDISI_UMUM.TEKANAN_DARAH,
                    "&FREKUENSI_NADI=", entity.KONDISI_UMUM.FREKUENSI_NADI,
                    "&SUHU=", entity.KONDISI_UMUM.SUHU,
                    "&PERNAPASAN=", entity.KONDISI_UMUM.PERNAPASAN,
                    "&KEADAAN_UMUM=", entity.KONDISI_UMUM.KEADAAN_UMUM,
                    "&NYERI=", entity.KONDISI_UMUM.NYERI,
                    "&ALERGI=", entity.KONDISI_UMUM.ALERGI,
                    "&LABORATORIUM=", entity.PENUNJANG.LABORATORIUM,
                    "&RADIOLOGI=", entity.PENUNJANG.RADIOLOGI,
                    "&TERAPI_ATAU_TINDAKAN=", entity.PENUNJANG.TERAPI_ATAU_TINDAKAN,
                    "&method=", method.ToString()
                });

            var sb = new StringBuilder();
            sb.Append(param);

            _url += "/sisrute_set_rujukan";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Sisrute.Rujukan.Post>(sr.ReadToEnd());
            }
        }

        public Sisrute.Common.Response BatalRujukan(string nomor, Rujukan.SetRujukan.PETUGAS petugas)
        {
            var param = string.Concat(new string[]
                {
                    "consid=",_consKey,
                    "&salt=",_salt,
                    "&kode=",nomor,
                    "&NIK_PETUGAS=",petugas.NIK,
                    "&NAMA_PETUGAS=",petugas.NAMA
                });

            var sb = new StringBuilder();
            sb.Append(param);

            _url += "/sisrute_batal_rujukan";

            using (var response = PopulateWebRequest(_url, BPJS.Helper.WebRequestMethod.POST, BPJS.Helper.WebRequestContentType.FORM, param).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<Sisrute.Common.Response>(sr.ReadToEnd());
            }
        }

    }
}
