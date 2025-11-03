using LZStringCSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Temiang.Avicenna.Common.BPJS.Apotek.Obat;
using Temiang.Avicenna.Common.BPJS.Apotek.PelayananObat;
using Temiang.Avicenna.Common.BPJS.Apotek.Referensi;
using Temiang.Avicenna.Common.BPJS.Apotek.Resep;
using static Temiang.Avicenna.Common.BPJS.Apotek.Resep.DaftarResep.Response;

namespace Temiang.Avicenna.Common.BPJS.Apotek
{
    public class Service
    {
        private string _baseUrl = ConfigurationManager.AppSettings["ApotekServiceUrlLocation"];
        private string _consKey = ConfigurationManager.AppSettings["ApotekConsumerID"];
        private string _ppkKey = ConfigurationManager.AppSettings["ApotekHospitalID"];
        private string _salt = ConfigurationManager.AppSettings["ApotekSaltConsumerID"];
        private string _encrypted = ConfigurationManager.AppSettings["BPJSResponseIsEncrypted"];
        private string _userKey = ConfigurationManager.AppSettings["BPJSApotekUserKey"];

        public class JsonHeader
        {
            [JsonProperty("x-cons-id")]
            public string cons_id { get; set; }
            [JsonProperty("x-timestamp")]
            public string timestamp { get; set; }
            [JsonProperty("x-signature")]
            public string signature { get; set; }
            [JsonProperty("user_key")]
            public string user_key { get; set; }
        }

        public string GetHeaderValue()
        {
            var timeStamp = BPJS.Helper.GetUnixTimeStamp();

            var str = new JsonHeader
            {
                cons_id = _consKey,
                timestamp = timeStamp,
                signature = BPJS.Helper.GetEncodedKey(timeStamp, _consKey, _salt, false),
                user_key = _userKey
            };
            return JsonConvert.SerializeObject(str);
        }

        public HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method, Helper.WebRequestContentType contentType, string parameter, out string timeStamp)
        {
            Helper.IgnoreBadCertificates();

            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = method.ToString();

            if (method != Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            webrequest.Headers.Add("x-cons-id", _consKey);
            timeStamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("x-timestamp", timeStamp);
            webrequest.Headers.Add("x-signature", BPJS.Helper.GetEncodedKey(timeStamp, _consKey, _salt, false));
            webrequest.Headers.Add("user_key", _userKey);
            if (method != Helper.WebRequestMethod.GET)
            {
                byte[] formData = Encoding.UTF8.GetBytes(parameter.ToString());
                webrequest.ContentLength = formData.Length;

                using (var post = webrequest.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }
            }

            return webrequest;
        }

        public HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method, out string timeStamp)
        {
            Helper.IgnoreBadCertificates();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            webrequest.Method = method.ToString();

            webrequest.Headers.Add("x-cons-id", _consKey);
            timeStamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("x-timestamp", timeStamp);
            webrequest.Headers.Add("x-signature", BPJS.Helper.GetEncodedKey(timeStamp, _consKey, _salt, false));
            webrequest.Headers.Add("user_key", _userKey);

            return webrequest;
        }

        public string DecryptResponse(string timeStamp, string data)
        {
            byte[] key;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                key = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(_consKey, _salt, timeStamp)));
            }
            byte[] iv = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                iv[i] = key[i];
            }

            string plaintext = string.Empty;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(data)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        #region referensi
        public Referensi.Dpho.Root GetObatDpho()
        {
            _baseUrl += $"referensi/dpho";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.Dpho.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.Dpho.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.Dpho.Response>(decryptResponse)

                        };

                        return entity;
                    }
                    else return new Referensi.Dpho.Root
                    {
                        MetaData = new Metadata()
                        {
                            Code = encryptedResponse.MetaData.Code,
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public Referensi.Poliklinik.Root GetPoli(string kode)
        {
            _baseUrl += $"referensi/poli/{kode}";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.Poliklinik.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.Poliklinik.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.Poliklinik.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Referensi.Poliklinik.Root
                    {
                        MetaData = new Metadata()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public Referensi.FasilitasKes.Root GetFaskes(string jenisFaskes, string namaFaskes)
        {
            _baseUrl += $"referensi/ppk/{jenisFaskes}/{namaFaskes}";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.FasilitasKes.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.FasilitasKes.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.FasilitasKes.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Referensi.FasilitasKes.Root
                    {
                        MetaData = new Metadata()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public string SettingApotek(string KodeApotek)
        {
            _baseUrl += $"referensi/settingppk/read/{KodeApotek}";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                    return sr.ReadToEnd();
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        return decryptResponse;
                    }
                    else return sr.ReadToEnd();
                }
            }
        }

        public Referensi.Spesialistik.Root GetSpesialistik()
        {
            _baseUrl += $"referensi/spesialistik";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.Spesialistik.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.Spesialistik.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.Spesialistik.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Referensi.Spesialistik.Root
                    {
                        MetaData = new Metadata()
                        {
                            Code = encryptedResponse.MetaData.Code,
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public Referensi.Obat.Root GetObat(string kodeJenisObat, DateTime tglResep, string filterPencarian)
        {
            _baseUrl += $"referensi/obat/{kodeJenisObat}/{tglResep:yyyy-MM-dd}/{filterPencarian}";
            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var responseData = sr.ReadToEnd();

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                {
                    return JsonConvert.DeserializeObject<Referensi.Obat.Root>(responseData);
                }
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(responseData);
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.Obat.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.Obat.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else
                    {
                        return new Referensi.Obat.Root
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = null
                        };
                    }
                }
            }
        }
        #endregion

        #region obat

        public Obat.NonRacikan.Response InsertNonRacikan(Obat.NonRacikan.Request.Root root)
        {
            _baseUrl += $"obatnonracikan/v3/insert";

            using (var resp = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.TEXT, JsonConvert.SerializeObject(root), out var ts).GetResponse() as HttpWebResponse)
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Server error (HTTP {resp.StatusCode}: {resp.StatusDescription}).");

                using (var sr = new StreamReader(resp.GetResponseStream()))
                {
                    var raw = sr.ReadToEnd();

                    if (string.IsNullOrEmpty(_encrypted) || _encrypted.Equals("false", StringComparison.OrdinalIgnoreCase))
                        return JsonConvert.DeserializeObject<Obat.NonRacikan.Response>(raw);

                    var enc = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(raw);
                    var code = enc?.MetaData?.Code?.ToString() ?? "";
                    var msg = enc?.MetaData?.Message ?? "";

                    var cipher = enc?.Response;
                    if (string.IsNullOrWhiteSpace(cipher))
                        return new Obat.NonRacikan.Response { Code = code, Message = msg };

                    var json = LZString.DecompressFromEncodedURIComponent(DecryptResponse(ts, cipher));
                    if (string.IsNullOrWhiteSpace(json))
                        return new Obat.NonRacikan.Response { Code = code, Message = msg };

                    var obj = JsonConvert.DeserializeObject<Obat.NonRacikan.Response>(json) ?? new Obat.NonRacikan.Response();
                    if (string.IsNullOrWhiteSpace(obj.Code)) obj.Code = code;
                    if (string.IsNullOrWhiteSpace(obj.Message)) obj.Message = msg;
                    return obj;
                }
            }
        }

        public Obat.Racikan.Response InsertRacikan(Obat.Racikan.Request.Root root)
        {
            _baseUrl += $"obatracikan/v3/insert";

            using (var resp = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.TEXT, JsonConvert.SerializeObject(root), out var ts).GetResponse() as HttpWebResponse)
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Server error (HTTP {resp.StatusCode}: {resp.StatusDescription}).");

                using (var sr = new StreamReader(resp.GetResponseStream()))
                {
                    var raw = sr.ReadToEnd();

                    if (string.IsNullOrEmpty(_encrypted) || _encrypted.Equals("false", StringComparison.OrdinalIgnoreCase))
                        return JsonConvert.DeserializeObject<Obat.Racikan.Response>(raw);

                    var enc = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(raw);
                    var code = enc?.MetaData?.Code?.ToString() ?? "";
                    var msg = enc?.MetaData?.Message ?? "";

                    var cipher = enc?.Response;
                    if (string.IsNullOrWhiteSpace(cipher))
                        return new Obat.Racikan.Response { Code = code, Message = msg };

                    var json = LZString.DecompressFromEncodedURIComponent(DecryptResponse(ts, cipher));
                    if (string.IsNullOrWhiteSpace(json))
                        return new Obat.Racikan.Response { Code = code, Message = msg };

                    var obj = JsonConvert.DeserializeObject<Obat.Racikan.Response>(json) ?? new Obat.Racikan.Response();
                    if (string.IsNullOrWhiteSpace(obj.Code)) obj.Code = code;
                    if (string.IsNullOrWhiteSpace(obj.Message)) obj.Message = msg;
                    return obj;
                }
            }
        }

        #endregion

        #region pelayanan obat
        public MetadataResponse HapusPelayananObat(HapusPelayananObat.Request.Root root)
        {
            _baseUrl += "pelayanan/obat/hapus";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.DELETE, Helper.WebRequestContentType.TEXT, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<MetadataResponse>(sr.ReadToEnd());
            }
        }

        public PelayananObat.DaftarPelayananObat.Root GetPelayananObat(string NoSEP)
        {
            _baseUrl += $"pelayanan/obat/daftar/{NoSEP}";
            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var responseData = sr.ReadToEnd();

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                {
                    return JsonConvert.DeserializeObject<PelayananObat.DaftarPelayananObat.Root>(responseData);
                }
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(responseData);
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new PelayananObat.DaftarPelayananObat.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<PelayananObat.DaftarPelayananObat.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else
                    {
                        return new PelayananObat.DaftarPelayananObat.Root
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = null
                        };
                    }
                }
            }
        }

        public PelayananObat.RiwayatPelayananObat.Root GetRiwayatPelayanan(DateTime TglAwal, DateTime TglAkhir, string NoKartu)
        {
            _baseUrl += $"riwayatobat/{TglAwal:yyyy-MM-dd}/{TglAkhir:yyyy-MM-dd}/{NoKartu}";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var responseData = sr.ReadToEnd();

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                {
                    return JsonConvert.DeserializeObject<PelayananObat.RiwayatPelayananObat.Root>(responseData);
                }
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(responseData);
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new PelayananObat.RiwayatPelayananObat.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<PelayananObat.RiwayatPelayananObat.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else
                    {
                        return new PelayananObat.RiwayatPelayananObat.Root
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = null
                        };
                    }
                }
            }
        }
        #endregion

        #region Resep

        public SimpanResep.Root SimpanResep(SimpanResep.Request root)
        {
            _baseUrl += "sjpresep/v3/insert";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.TEXT, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Resep.SimpanResep.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Resep.SimpanResep.Root
                        {
                            MetaData = new MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            ResponseData = JsonConvert.DeserializeObject<SimpanResep.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Resep.SimpanResep.Root
                    {
                        MetaData = new MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code,
                            Message = encryptedResponse.MetaData.Message
                        },
                        ResponseData = null
                    };
                }
            }
        }

        public MetadataResponse HapusResep(HapusResep.Request.Root root)
        {
            _baseUrl += "/hapusresep";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.DELETE, Helper.WebRequestContentType.TEXT, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                return JsonConvert.DeserializeObject<MetadataResponse>(sr.ReadToEnd());
            }
        }

        public Resep.DaftarResep.Response.Root GetDaftarResep(Resep.DaftarResep.Request.Root root)
        {
            _baseUrl += "/daftarresep";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.TEXT, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Resep.DaftarResep.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Resep.DaftarResep.Response.Root
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<List<Resep.DaftarResep.Response.Resep>>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Resep.DaftarResep.Response.Root
                    {
                        Metadata = new Metadata()
                        {
                            Code = encryptedResponse.MetaData.Code,
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }
        #endregion

        public Referensi.klaim.Root GetDataKlaim(string Bulan, string Tahun, string jenisObat, string statusVerif)
        {
            _baseUrl += $"monitoring/klaim/{Bulan}/{Tahun}/{jenisObat}/{statusVerif}";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());
                var responseData = sr.ReadToEnd();

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                {
                    return JsonConvert.DeserializeObject<Referensi.klaim.Root>(responseData);
                }
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(responseData);
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.klaim.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.klaim.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else
                    {
                        return new Referensi.klaim.Root
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = null
                        };
                    }
                }
            }
        }

        public Referensi.Sep.Root GetSep(string nosep)
        {
            _baseUrl += $"sep/{nosep}";

            using (var response = PopulateWebRequest(_baseUrl, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                {
                    return JsonConvert.DeserializeObject<Referensi.Sep.Root>(sr.ReadToEnd());
                }
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsApolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.Sep.Root()
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.Sep.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else
                    {
                        return new Referensi.Sep.Root
                        {
                            MetaData = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = null
                        };
                    }
                }
            }
        }
    }
}
