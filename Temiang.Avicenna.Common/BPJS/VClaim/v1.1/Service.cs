using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Security;
using Temiang.Avicenna.Common.BPJS.VClaim.v11.Sep;
using LZStringCSharp;
using System.Security.Cryptography;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Common.BPJS.VClaim.v11
{
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

    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["BPJSServiceUrlLocation"];
        private string _consKey = ConfigurationManager.AppSettings["BPJSConsumerID"];
        private string _ppkKey = ConfigurationManager.AppSettings["BPJSHospitalID"];
        private string _salt = ConfigurationManager.AppSettings["BPJSSaltConsumerID"];
        private string _encrypted = ConfigurationManager.AppSettings["BPJSResponseIsEncrypted"];
        private string _userKey = ConfigurationManager.AppSettings["BPJSUserKey"];

        //private HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method, Helper.WebRequestContentType contentType, string parameter)
        //{
        //    Helper.IgnoreBadCertificates();

        //    var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
        //    webrequest.Method = method.ToString();

        //    if (method != Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

        //    webrequest.Headers.Add("X-cons-id", _consKey);
        //    string stamp = BPJS.Helper.GetUnixTimeStamp();
        //    webrequest.Headers.Add("X-timestamp", stamp);
        //    webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(stamp, _consKey, _salt, false));

        //    if (method != Helper.WebRequestMethod.GET)
        //    {
        //        byte[] formData = Encoding.UTF8.GetBytes(parameter.ToString());
        //        webrequest.ContentLength = formData.Length;

        //        using (var post = webrequest.GetRequestStream())
        //        {
        //            post.Write(formData, 0, formData.Length);
        //        }
        //    }

        //    return webrequest;
        //}

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

            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = method.ToString();

            if (method != Helper.WebRequestMethod.GET) webrequest.ContentType = contentType.ToString();

            webrequest.Headers.Add("X-cons-id", _consKey);
            timeStamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("X-timestamp", timeStamp);
            webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(timeStamp, _consKey, _salt, false));
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

        //private HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method)
        //{
        //    Helper.IgnoreBadCertificates();

        //    HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
        //    webrequest.Method = method.ToString();

        //    webrequest.Headers.Add("X-cons-id", _consKey);
        //    string stamp = BPJS.Helper.GetUnixTimeStamp();
        //    webrequest.Headers.Add("X-timestamp", stamp);
        //    webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(stamp, _consKey, _salt, false));

        //    return webrequest;
        //}

        public HttpWebRequest PopulateWebRequest(string url, Helper.WebRequestMethod method, out string timeStamp)
        {
            Helper.IgnoreBadCertificates();

            HttpWebRequest webrequest = (HttpWebRequest)System.Net.WebRequest.Create(_url);
            webrequest.Method = method.ToString();

            webrequest.Headers.Add("X-cons-id", _consKey);
            timeStamp = BPJS.Helper.GetUnixTimeStamp();
            webrequest.Headers.Add("X-timestamp", timeStamp);
            webrequest.Headers.Add("X-signature", BPJS.Helper.GetEncodedKey(timeStamp, _consKey, _salt, false));
            webrequest.Headers.Add("user_key", _userKey);

            return webrequest;
        }

        public v11.Diagnosa.Diagnosa GetDiagnosa(string text)
        {
            //{Base URL}/{Service Name}/referensi/diagnosa/{parameter} 
            _url += "referensi/diagnosa/" + text;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Diagnosa.Diagnosa>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Diagnosa.Diagnosa
                        {
                            MetaData = new Diagnosa.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Diagnosa.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Diagnosa.Diagnosa()
                    {
                        MetaData = new Diagnosa.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Poli.Poli GetPoli(string text)
        {
            //{Base URL}/{Service Name}referensi/poli/{Parameter}
            _url += "referensi/poli/" + text;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Poli.Poli>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Poli.Poli
                        {
                            MetaData = new Poli.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Poli.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Poli.Poli()
                    {
                        MetaData = new Poli.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Faskes.Faskes GetFaskes(string text, Enum.JenisFaskes jenisFaskes)
        {
            //{Base URL}/{Service Name}/referensi/faskes/{Parameter 1}/{Parameter 2}
            _url += string.Format("referensi/faskes/{0}/{1}", text, jenisFaskes.ToString());

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Faskes.Faskes>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Faskes.Faskes
                        {
                            MetaData = new Faskes.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Faskes.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Faskes.Faskes()
                    {
                        MetaData = new Faskes.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Procedure.Procedure GetProcedure(string text)
        {
            //{Base URL}/{Service Name}/referensi/procedure/{Parameter}
            _url += "referensi/procedure/" + text;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Procedure.Procedure>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Procedure.Procedure
                        {
                            MetaData = new Procedure.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Procedure.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Procedure.Procedure()
                    {
                        MetaData = new Procedure.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.KelasRawat.KelasRawat GetKelasRawat()
        {
            //{Base URL}/{Service Name}/referensi/kelasrawat
            _url += "referensi/kelasrawat";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.KelasRawat.KelasRawat>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.KelasRawat.KelasRawat
                        {
                            MetaData = new KelasRawat.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.KelasRawat.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new KelasRawat.KelasRawat()
                    {
                        MetaData = new KelasRawat.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.DokterDpjp.DokterDpjp GetDokterDpjp(Enum.JenisPelayanan jenisPelayanan, DateTime tglPelayanan, string kodeSpesialis)
        {
            //{Base URL}/{Service Name}/referensi/dokter/pelayanan/{Parameter 1}/tglPelayanan/{Parameter 2}/Spesialis/{Parameter 3}
            _url += string.Format("referensi/dokter/pelayanan/{0}/tglPelayanan/{1}/Spesialis/{2}", jenisPelayanan.ToString(), tglPelayanan.ToString("yyyy-MM-dd"), kodeSpesialis);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.DokterDpjp.DokterDpjp>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.DokterDpjp.DokterDpjp
                        {
                            MetaData = new DokterDpjp.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.DokterDpjp.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new DokterDpjp.DokterDpjp()
                    {
                        MetaData = new DokterDpjp.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.DokterKlaim.DokterKlaim GetDokterKlaim(string namaDokter)
        {
            //{Base URL}/{Service Name}/referensi/dokter/{Parameter}
            _url += string.Format("referensi/dokter/{0}", namaDokter);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.DokterKlaim.DokterKlaim>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.DokterKlaim.DokterKlaim
                        {
                            MetaData = new DokterKlaim.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.DokterKlaim.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new DokterKlaim.DokterKlaim()
                    {
                        MetaData = new DokterKlaim.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Spesialistik.Spesialistik GetSpesialistik()
        {
            //{Base URL}/{Service Name}/referensi/spesialistik
            _url += "referensi/spesialistik";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Spesialistik.Spesialistik>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Spesialistik.Spesialistik
                        {
                            MetaData = new Spesialistik.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Spesialistik.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Spesialistik.Spesialistik()
                    {
                        MetaData = new Spesialistik.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RuangRawat.RuangRawat GetRuangRawat()
        {
            //{Base URL}/{Service Name}/referensi/ruangrawat
            _url += "referensi/ruangrawat";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RuangRawat.RuangRawat>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RuangRawat.RuangRawat
                        {
                            MetaData = new RuangRawat.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RuangRawat.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RuangRawat.RuangRawat()
                    {
                        MetaData = new RuangRawat.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.CaraKeluar.CaraKeluar GetCaraKeluar()
        {
            //{Base URL}/{Service Name}/referensi/carakeluar
            _url += "referensi/carakeluar";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.CaraKeluar.CaraKeluar>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.CaraKeluar.CaraKeluar
                        {
                            MetaData = new CaraKeluar.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.CaraKeluar.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new CaraKeluar.CaraKeluar()
                    {
                        MetaData = new CaraKeluar.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.PascaPulang.PascaPulang GetPascaPulang()
        {
            //{Base URL}/{Service Name}/referensi/pascapulang
            _url += "referensi/pascapulang";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.PascaPulang.PascaPulang>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.PascaPulang.PascaPulang
                        {
                            MetaData = new PascaPulang.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.PascaPulang.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new PascaPulang.PascaPulang()
                    {
                        MetaData = new PascaPulang.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Propinsi.Propinsi GetPropinsi()
        {
            //{Base URL}/{Service Name}/referensi/propinsi
            _url += "referensi/propinsi";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Propinsi.Propinsi>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Propinsi.Propinsi
                        {
                            MetaData = new Propinsi.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Propinsi.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Propinsi.Propinsi()
                    {
                        MetaData = new Propinsi.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Kabupaten.Kabupaten GetKabupaten(string kodePropinsi)
        {
            //{Base URL}/{Service Name}/referensi/kabupaten/propinsi/{paramater 1}
            _url += "referensi/kabupaten/propinsi/" + kodePropinsi;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Kabupaten.Kabupaten>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Kabupaten.Kabupaten
                        {
                            MetaData = new Kabupaten.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Kabupaten.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Kabupaten.Kabupaten()
                    {
                        MetaData = new Kabupaten.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Kecamatan.Kecamatan GetKecamatan(string kodeKabupaten)
        {
            //{Base URL}/{Service Name}/referensi/kecamatan/kabupaten/{paramater 1}
            _url += "referensi/kecamatan/kabupaten/" + kodeKabupaten;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Kecamatan.Kecamatan>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Kecamatan.Kecamatan
                        {
                            MetaData = new Kecamatan.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Kecamatan.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Kecamatan.Kecamatan()
                    {
                        MetaData = new Kecamatan.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Peserta.Root GetPeserta(Enum.SearchPeserta searchPeserta, string text, DateTime tglSEP)
        {
            //no kartu : {BASE URL}/{Service Name}/Peserta/nokartu/{parameter 1}/tglSEP/{parameter 2}
            //nik : {BASE URL}/{Service Name}/Peserta/nik/{parameter 1}/tglSEP/{parameter 2}
            _url += string.Format("Peserta/{0}/{1}/tglSEP/{2}", searchPeserta.ToString(), text, tglSEP.ToString("yyyy-MM-dd"));
            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Peserta.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Peserta.Root
                        {
                            MetaData = new Peserta.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Peserta.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Peserta.Root()
                    {
                        MetaData = new Peserta.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
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

        public v11.Sep.InsertResponse.Insert Insert(v11.Sep.InsertRequest.TSep tsep)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/insert
            _url += "SEP/1.1/insert";

            var root = new Common.BPJS.VClaim.v11.Sep.InsertRequest.RootObject();
            root.request = new Common.BPJS.VClaim.v11.Sep.InsertRequest.Request { t_sep = tsep };

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Sep.InsertResponse.Insert>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.InsertResponse.Insert
                        {
                            MetaData = new Sep.InsertResponse.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Sep.InsertResponse.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Sep.InsertResponse.Insert()
                    {
                        MetaData = new Sep.InsertResponse.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v20.Sep.InsertResponse.Root Insert(v20.Sep.InsertRequest.TSep tsep)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/insert
            _url += "SEP/2.0/insert";

            var root = new Common.BPJS.VClaim.v20.Sep.InsertRequest.Root();
            root.Request = new Common.BPJS.VClaim.v20.Sep.InsertRequest.TRequest { TSep = tsep };

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v20.Sep.InsertResponse.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v20.Sep.InsertResponse.Root
                        {
                            MetaData = new v20.Sep.InsertResponse.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v20.Sep.InsertResponse.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new v20.Sep.InsertResponse.Root()
                    {
                        MetaData = new v20.Sep.InsertResponse.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.UpdateResponse.Update Update(v11.Sep.UpdateRequest.Update.TSep tsep)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/Update
            _url += "Sep/1.1/Update";

            var root = new Common.BPJS.VClaim.v11.Sep.UpdateRequest.Update.RootObject();
            root.request = new Common.BPJS.VClaim.v11.Sep.UpdateRequest.Update.Request { t_sep = tsep };

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Sep.UpdateResponse.Update>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.UpdateResponse.Update
                        {
                            MetaData = new Sep.UpdateResponse.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new Sep.UpdateResponse.Update()
                    {
                        MetaData = new Sep.UpdateResponse.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.UpdateResponse.Update Update(v20.Sep.UpdateRequest.Update.TSep tsep)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/Update
            _url += "SEP/2.0/update";

            var root = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.Root()
            {
                Request = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.Update.Request { TSep = tsep }
            };

            {
                var log = new WebServiceAPILog()
                {
                    DateRequest = DateTime.Now,
                    IPAddress = string.Empty,
                    UrlAddress = string.Empty,
                    Params = JsonConvert.SerializeObject(root),
                    Response = string.Empty,
                    Totalms = 0
                };
                log.Save();
            }

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                    return JsonConvert.DeserializeObject<v11.Sep.UpdateResponse.Update>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.UpdateResponse.Update
                        {
                            MetaData = new Sep.UpdateResponse.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new Sep.UpdateResponse.Update()
                    {
                        MetaData = new Sep.UpdateResponse.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.UpdateResponse.Update Delete(v11.Sep.DeleteRequest.TSep tsep)
        {
            //{BASE URL}/{Service Name}/SEP/Delete
            _url += "SEP/2.0/delete";

            var root = new Common.BPJS.VClaim.v11.Sep.DeleteRequest.Root();
            root.Request = new Common.BPJS.VClaim.v11.Sep.DeleteRequest.Request { TSep = tsep };

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.DELETE, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false")
                    return JsonConvert.DeserializeObject<v11.Sep.UpdateResponse.Update>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        try
                        {
                            var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                            var entity = new v11.Sep.UpdateResponse.Update
                            {
                                MetaData = new Sep.UpdateResponse.MetaData()
                                {
                                    Code = encryptedResponse.MetaData.Code.ToString(),
                                    Message = encryptedResponse.MetaData.Message
                                },
                                Response = decryptResponse
                            };

                            return entity;
                        }
                        catch (Exception e)
                        {
                            var entity = new v11.Sep.UpdateResponse.Update
                            {
                                MetaData = new Sep.UpdateResponse.MetaData()
                                {
                                    Code = encryptedResponse.MetaData.Code.ToString(),
                                    Message = encryptedResponse.MetaData.Message
                                },
                                Response = encryptedResponse.Response
                            };

                            return entity;
                        }
                    }
                    else return new Sep.UpdateResponse.Update()
                    {
                        MetaData = new Sep.UpdateResponse.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.Cari.Cari GetSep(string noSep)
        {
            //{BASE URL}/{Service Name}/SEP/{parameter}
            _url += "SEP/" + noSep;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Sep.Cari.Cari>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.Cari.Cari
                        {
                            MetaData = new Sep.Cari.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Sep.Cari.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Sep.Cari.Cari()
                    {
                        MetaData = new Sep.Cari.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        //public Sep.Approve.Feedback Approve(Enum.ApproveType approveType, Sep.Approve.TSep tsep)
        //{
        //    _url += string.Format("Sep/{0}", approveType.ToString());

        //    var root = new Common.BPJS.VClaim.Sep.Approve.RootObject();
        //    root.request = new Common.BPJS.VClaim.Sep.Approve.Request { t_sep = tsep };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Sep.Approve.Feedback>(sr.ReadToEnd());
        //    }
        //}

        public v11.Sep.UpdateResponse.Update UpdateTglPulang(v11.Sep.UpdateRequest.UpdateTanggalPulang.TSep tsep)
        {
            //{BASE URL}/{Service Name}/Sep/updtglplg
            _url += "Sep/updtglplg";

            var root = new Common.BPJS.VClaim.v11.Sep.UpdateRequest.UpdateTanggalPulang.RootObject();
            root.request = new Common.BPJS.VClaim.v11.Sep.UpdateRequest.UpdateTanggalPulang.Request { t_sep = tsep };

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Sep.UpdateResponse.Update>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.UpdateResponse.Update
                        {
                            MetaData = new Sep.UpdateResponse.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new Sep.UpdateResponse.Update()
                    {
                        MetaData = new Sep.UpdateResponse.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.UpdateResponse.Update UpdateTglPulang(v20.Sep.UpdateRequest.UpdateTanggalPulang.TSep tsep)
        {
            //{BASE URL}/{Service Name}/Sep/updtglplg
            _url += "SEP/2.0/updtglplg";

            var root = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.UpdateTanggalPulang.Root();
            root.Request = new Common.BPJS.VClaim.v20.Sep.UpdateRequest.UpdateTanggalPulang.Request { TSep = tsep };

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Sep.UpdateResponse.Update>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        //var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.UpdateResponse.Update
                        {
                            MetaData = new Sep.UpdateResponse.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = encryptedResponse.Response
                        };

                        return entity;
                    }
                    else return new Sep.UpdateResponse.Update()
                    {
                        MetaData = new Sep.UpdateResponse.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        //public Klaim.Insert.Response Insert(Klaim.Insert.TLpk tlpk)
        //{
        //    _url += "LPK/insert";

        //    var root = new Common.BPJS.VClaim.Klaim.Insert.RootObject();
        //    root.request = new Common.BPJS.VClaim.Klaim.Insert.Request { t_lpk = tlpk };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Klaim.Insert.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Klaim.Update.Response Update(Klaim.Update.TLpk tlpk)
        //{
        //    _url += "LPK/update";

        //    var root = new Common.BPJS.VClaim.Klaim.Update.RootObject();
        //    root.request = new Common.BPJS.VClaim.Klaim.Update.Request { t_lpk = tlpk };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Klaim.Update.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Klaim.Delete.Response Delete(Klaim.Delete.TLpk tlpk)
        //{
        //    _url += "LPK/delete";

        //    var root = new Common.BPJS.VClaim.Klaim.Delete.RootObject();
        //    root.request = new Common.BPJS.VClaim.Klaim.Delete.Request { t_lpk = tlpk };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Klaim.Delete.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Klaim.Search.Response GetKlaim(DateTime tglMasuk, Enum.JenisPelayanan jenisPelayanan)
        //{
        //    _url += string.Format("LPK/TglMasuk/{0}/JnsPelayanan/{1}", tglMasuk.ToString("yyyy-MM-dd"), jenisPelayanan.ToString());

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Klaim.Search.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Monitoring.DataKunjungan.Response GetMonitoring(DateTime tglMasuk, Enum.JenisPelayanan jenisPelayanan)
        //{
        //    _url += string.Format("Monitoring/Kunjungan/Tanggal/{0}/JnsPelayanan/{1}", tglMasuk.ToString("yyyy-MM-dd"), jenisPelayanan.ToString());

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Monitoring.DataKunjungan.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Monitoring.DataKlaim.Response GetMonitoring(DateTime tglMasuk, Enum.JenisPelayanan jenisPelayanan, Enum.StatusKlaim statusKlaim)
        //{
        //    _url += string.Format("Monitoring/Klaim/Tanggal/{0}/JnsPelayanan/{1}/Status/{2}", tglMasuk.ToString("yyyy-MM-dd"), jenisPelayanan.ToString(), statusKlaim.ToString());

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Monitoring.DataKlaim.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Rujukan.Insert.Response Insert(Rujukan.Insert.TRujukan trujukan)
        //{
        //    _url += "Rujukan/insert";

        //    var root = new Common.BPJS.VClaim.Rujukan.Insert.RootObject();
        //    root.request = new Common.BPJS.VClaim.Rujukan.Insert.Request { t_rujukan = trujukan };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Rujukan.Insert.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Rujukan.Update.Response Update(Rujukan.Update.TRujukan trujukan)
        //{
        //    _url += "Rujukan/update";

        //    var root = new Common.BPJS.VClaim.Rujukan.Update.RootObject();
        //    root.request = new Common.BPJS.VClaim.Rujukan.Update.Request { t_rujukan = trujukan };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Rujukan.Update.Response>(sr.ReadToEnd());
        //    }
        //}

        //public Rujukan.Update.Response Delete(Rujukan.Update.TRujukan trujukan)
        //{
        //    _url += "Rujukan/delete";

        //    var root = new Common.BPJS.VClaim.Rujukan.Update.RootObject();
        //    root.request = new Common.BPJS.VClaim.Rujukan.Update.Request { t_rujukan = trujukan };

        //    using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, fastJSON.JSON.ToJSON(root, new fastJSON.JSONParameters() { UseExtensions = false })).GetResponse() as HttpWebResponse)
        //    {
        //        if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

        //        var sr = new StreamReader(response.GetResponseStream());
        //        return JsonConvert.DeserializeObject<Rujukan.Update.Response>(sr.ReadToEnd());
        //    }
        //}

        public v11.Rujukan.Select.Rujukan GetRujukan(bool isByNoRujukan, string text, Enum.JenisFaskes faskes)//, Enum.JenisFaskes jenisFaskes)
        {
            if (isByNoRujukan)
            {
                //1 : {BASE URL}/{Service Name}/Rujukan/{parameter}
                //2 : {BASE URL}/{Service Name}/Rujukan/RS/{parameter}
                //if (jenisFaskes == Enum.JenisFaskes.Faskes_1) _url += string.Format("Rujukan/{0}", text);
                //else
                if (faskes == Enum.JenisFaskes.Faskes_1) _url += string.Format("Rujukan/{0}", text);
                else _url += string.Format("Rujukan/RS/{0}", text);
            }
            else
            {
                //if (jenisFaskes == Enum.JenisFaskes.Faskes_1) _url += string.Format("Rujukan/Peserta/{0}", text);
                //else 
                if (faskes == Enum.JenisFaskes.Faskes_1) _url += string.Format("Rujukan/Peserta/{0}", text);
                else _url += string.Format("Rujukan/RS/Peserta/{0}", text);
            }

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Rujukan.Select.Rujukan>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Rujukan.Select.Rujukan
                        {
                            MetaData = new Rujukan.Select.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Rujukan.Select.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Rujukan.Select.Rujukan()
                    {
                        MetaData = new Rujukan.Select.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Rujukan.Select.RujukanList GetRujukan(string text, Enum.JenisFaskes jenisFaskes)
        {
            //1 : {BASE URL}/{Service Name}/Rujukan/{parameter}
            //2 : {BASE URL}/{Service Name}/Rujukan/RS/{parameter}
            if (jenisFaskes == Enum.JenisFaskes.Faskes_1) _url += string.Format("Rujukan/List/Peserta/{0}", text);
            else _url += string.Format("Rujukan/RS/List/Peserta/{0}", text);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Rujukan.Select.RujukanList>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Rujukan.Select.RujukanList
                        {
                            MetaData = new Rujukan.Select.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Rujukan.Select.ResponseList>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Rujukan.Select.RujukanList()
                    {
                        MetaData = new Rujukan.Select.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Rujukan.Select.Rujukan GetRujukan(string text)
        {
            _url += string.Format("Rujukan/{0}", text);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Rujukan.Select.Rujukan>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Rujukan.Select.Rujukan
                        {
                            MetaData = new Rujukan.Select.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Rujukan.Select.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Rujukan.Select.Rujukan()
                    {
                        MetaData = new Rujukan.Select.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Monitoring.HistoriPelayananPeserta.DataHistoriPelayananPeserta GetDataHistoriPelayananPeserta(string noKartu, DateTime tglMulai, DateTime tglAkhir)
        {
            //{Base URL}/{Service Name}/monitoring/HistoriPelayanan/NoKartu/{Parameter 1}/tglMulai/{Parameter 2}/tglAkhir/{Parameter 3}
            _url += string.Format("monitoring/HistoriPelayanan/NoKartu/{0}/tglMulai/{1}/tglAkhir/{2}", noKartu, tglMulai.ToString("yyyy-MM-dd"), tglAkhir.ToString("yyyy-MM-dd"));

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Monitoring.HistoriPelayananPeserta.DataHistoriPelayananPeserta>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Monitoring.HistoriPelayananPeserta.DataHistoriPelayananPeserta
                        {
                            MetaData = new Monitoring.HistoriPelayananPeserta.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Monitoring.HistoriPelayananPeserta.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Monitoring.HistoriPelayananPeserta.DataHistoriPelayananPeserta()
                    {
                        MetaData = new Monitoring.HistoriPelayananPeserta.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Monitoring.HistoriPelayananPeserta.DataHistoriPelayananPeserta GetDataHistoriPelayananPeserta(string noKartu, string tglMulai, DateTime tglAkhir)
        {
            string format = "yyyy-MM-dd";
            DateTime parsed;
            DateTime.TryParseExact(tglMulai, format, null, System.Globalization.DateTimeStyles.None, out parsed);

            return GetDataHistoriPelayananPeserta(noKartu, parsed, tglAkhir);
        }

        public v11.Monitoring.Kunjungan.DataKunjungan GetDataKunjungan(DateTime tglPelayanan, Enum.JenisPelayanan jenisPelayanan)
        {
            //{Base URL}/{Service Name}/Monitoring/Kunjungan/Tanggal/{Parameter 1}/JnsPelayanan/{Parameter 2}
            _url += string.Format("Monitoring/Kunjungan/Tanggal/{0}/JnsPelayanan/{1}", tglPelayanan.ToString("yyyy-MM-dd"), jenisPelayanan.ToString());

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Monitoring.Kunjungan.DataKunjungan>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Monitoring.Kunjungan.DataKunjungan
                        {
                            MetaData = new Monitoring.Kunjungan.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Monitoring.Kunjungan.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Monitoring.Kunjungan.DataKunjungan()
                    {
                        MetaData = new Monitoring.Kunjungan.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Monitoring.Klaim.DataKlaim GetDataKlaim(DateTime tglPelayanan, Enum.JenisPelayanan jenisPelayanan, Enum.StatusKlaim statusKlaim)
        {
            //{Base URL}/{Service Name}/Monitoring/Kunjungan/Tanggal/{Parameter 1}/JnsPelayanan/{Parameter 2}
            _url += string.Format("/Monitoring/Klaim/Tanggal/{0}/JnsPelayanan/{1}/Status/{2}", tglPelayanan.ToString("yyyy-MM-dd"), jenisPelayanan.ToString(), statusKlaim.ToString());

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Monitoring.Klaim.DataKlaim>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Monitoring.Klaim.DataKlaim
                        {
                            MetaData = new Monitoring.Klaim.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Monitoring.Klaim.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Monitoring.Klaim.DataKlaim()
                    {
                        MetaData = new Monitoring.Klaim.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.SuplesiJasaRaharja.SuplesiJasaRaharja GetSuplesiJasaRaharja(string noPeserta, DateTime tglPelayanan)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("sep/JasaRaharja/Suplesi/{0}/tglPelayanan/{1}", noPeserta, tglPelayanan.ToString("yyyy-MM-dd"));

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Sep.SuplesiJasaRaharja.SuplesiJasaRaharja>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.SuplesiJasaRaharja.SuplesiJasaRaharja
                        {
                            MetaData = new Sep.SuplesiJasaRaharja.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Sep.SuplesiJasaRaharja.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Sep.SuplesiJasaRaharja.SuplesiJasaRaharja()
                    {
                        MetaData = new Sep.SuplesiJasaRaharja.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Rujukan.Insert.Response.Root Insert(Rujukan.Insert.Request.Root root)
        {
            //{BASE URL}/{Service Name}/Rujukan/insert
            _url += "Rujukan/insert";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Rujukan.Insert.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Rujukan.Insert.Response.Root
                        {
                            MetaData = new Rujukan.Select.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.Rujukan.Insert.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Rujukan.Insert.Response.Root()
                    {
                        MetaData = new Rujukan.Select.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v20.Rujukan.Insert.Response.Root Insert(v20.Rujukan.Insert.Request.Root root)
        {
            //{BASE URL}/{Service Name}/Rujukan/insert
            _url += "Rujukan/2.0/insert";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v20.Rujukan.Insert.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v20.Rujukan.Insert.Response.Root
                        {
                            MetaData = new v20.Rujukan.Insert.Response.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v20.Rujukan.Insert.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new v20.Rujukan.Insert.Response.Root()
                    {
                        MetaData = new v20.Rujukan.Insert.Response.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Rujukan.Update.Response.Root Update(Rujukan.Update.Request.Root root)
        {
            //{BASE URL}/{Service Name}/Rujukan/update
            _url += "Rujukan/update";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Rujukan.Update.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Rujukan.Update.Response.Root
                        {
                            MetaData = new Rujukan.Select.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new Rujukan.Update.Response.Root()
                    {
                        MetaData = new Rujukan.Select.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Rujukan.Update.Response.Root Update(v20.Rujukan.Update.Request.Root root)
        {
            //{BASE URL}/{Service Name}/Rujukan/update
            _url += "Rujukan/2.0/Update";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Rujukan.Update.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Rujukan.Update.Response.Root()
                        {
                            MetaData = new Rujukan.Select.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new v11.Rujukan.Update.Response.Root()
                    {
                        MetaData = new Rujukan.Select.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Rujukan.Update.Response.Root Delete(Rujukan.Delete.Request.Root root)
        {
            //{BASE URL}/{Service Name}/Rujukan/delete
            _url += "Rujukan/delete";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.DELETE, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Rujukan.Update.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Rujukan.Update.Response.Root
                        {
                            MetaData = new Rujukan.Select.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new Rujukan.Update.Response.Root()
                    {
                        MetaData = new Rujukan.Select.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.Pengajuan.Response.Root Insert(Sep.Pengajuan.Request.Root root)
        {
            //{BASE URL}/{Service Name}/Rujukan/insert
            _url += "Sep/pengajuanSEP";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.Sep.Pengajuan.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.Sep.Pengajuan.Response.Root
                        {
                            Metadata = new Pengajuan.Response.Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new Pengajuan.Response.Root()
                    {
                        Metadata = new Pengajuan.Response.Metadata()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.Sep.Pengajuan.Response.Root Update(Sep.Approval.Request.Root root)
        {
            //{BASE URL}/{Service Name}/Rujukan/insert
            _url += "Sep/aprovalSEP";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<v11.Sep.Pengajuan.Response.Root>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        var entity = new v11.Sep.Pengajuan.Response.Root
                //        {
                //            Metadata = new Pengajuan.Response.Metadata()
                //            {
                //                Code = encryptedResponse.MetaData.Code.ToString(),
                //                Message = encryptedResponse.MetaData.Message
                //            },
                //            Response = decryptResponse
                //        };

                //        return entity;
                //    }
                //    else return new Pengajuan.Response.Root()
                //    {
                //        Metadata = new Pengajuan.Response.Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code.ToString(),
                //            Message = encryptedResponse.MetaData.Message
                //        },
                //        Response = null
                //    };
                //}
            }
        }

        public v11.RujukanBalik.Insert.Response.Root Insert(RujukanBalik.Insert.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "PRB/insert";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RujukanBalik.Insert.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RujukanBalik.Insert.Response.Root
                        {
                            MetaData = new RujukanBalik.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RujukanBalik.Insert.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RujukanBalik.Insert.Response.Root()
                    {
                        MetaData = new RujukanBalik.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RujukanBalik.Update.Response.Root Update(RujukanBalik.Update.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "PRB/Update";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RujukanBalik.Update.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RujukanBalik.Update.Response.Root
                        {
                            MetaData = new RujukanBalik.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new RujukanBalik.Update.Response.Root()
                    {
                        MetaData = new RujukanBalik.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RujukanBalik.Delete.Response.Root Delete(RujukanBalik.Delete.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "PRB/Delete";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.DELETE, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RujukanBalik.Delete.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RujukanBalik.Delete.Response.Root
                        {
                            MetaData = new RujukanBalik.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = decryptResponse
                        };

                        return entity;
                    }
                    else return new RujukanBalik.Delete.Response.Root()
                    {
                        MetaData = new RujukanBalik.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.DiagnosaPRB.Root GetDiagnosaPrb()
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += "referensi/diagnosaprb";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.DiagnosaPRB.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.DiagnosaPRB.Root
                        {
                            MetaData = new DiagnosaPRB.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.DiagnosaPRB.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new DiagnosaPRB.Root()
                    {
                        MetaData = new DiagnosaPRB.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.ObatPRB.Root GetObatPrb(string namaObatGenerik)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("referensi/obatprb/{0}", namaObatGenerik);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.ObatPRB.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.ObatPRB.Root
                        {
                            MetaData = new ObatPRB.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.ObatPRB.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new ObatPRB.Root()
                    {
                        MetaData = new ObatPRB.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RujukanBalik.Select.Response.Root GetPrbByNo(string noPrb, string noSep)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("prb/{0}/nosep/{1}", noPrb, noSep);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RujukanBalik.Select.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RujukanBalik.Select.Response.Root
                        {
                            MetaData = new RujukanBalik.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RujukanBalik.Select.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RujukanBalik.Select.Response.Root()
                    {
                        MetaData = new RujukanBalik.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RujukanBalik.Select.ResponseList.Root GetPrbByTanggal(DateTime tglAwal, DateTime tglAkhir)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("prb/tglMulai/{0}/tglAkhir/{1}", tglAwal.ToString("yyyy-MM-dd"), tglAkhir.ToString("yyyy-MM-dd"));

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RujukanBalik.Select.ResponseList.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RujukanBalik.Select.ResponseList.Root
                        {
                            MetaData = new RujukanBalik.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RujukanBalik.Select.ResponseList.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RujukanBalik.Select.ResponseList.Root()
                    {
                        MetaData = new RujukanBalik.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Insert.Response.Root Insert(RencanaKontrol.Insert.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "RencanaKontrol/insert";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Insert.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Insert.Response.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Insert.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Insert.Response.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Update.Response.Root Update(RencanaKontrol.Update.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "RencanaKontrol/Update";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Update.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Update.Response.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Update.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Update.Response.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Delete.Response.Root Delete(RencanaKontrol.Delete.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "RencanaKontrol/Delete";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.DELETE, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<v11.RencanaKontrol.Delete.Response.Root>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        var entity = new v11.RencanaKontrol.Delete.Response.Root
                //        {
                //            MetaData = new RencanaKontrol.MetaData
                //            {
                //                Code = encryptedResponse.MetaData.Code.ToString(),
                //                Message = encryptedResponse.MetaData.Message
                //            },
                //            Response = decryptResponse
                //        };

                //        return entity;
                //    }
                //    else return new RencanaKontrol.Delete.Response.Root()
                //    {
                //        MetaData = new RencanaKontrol.MetaData
                //        {
                //            Code = encryptedResponse.MetaData.Code.ToString(),
                //            Message = encryptedResponse.MetaData.Message
                //        },
                //        Response = null
                //    };
                //}
            }
        }

        public v11.RencanaKontrol.Select.ResponseSuratKontrolList.Root GetRencanaKontrolByNoPeserta(string bulan, string tahun, string noPeserta, Enum.FilterRencanaKontrol filterRencanaKontrol)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("RencanaKontrol/ListRencanaKontrol/Bulan/{0}/Tahun/{1}/Nokartu/{2}/filter/{3}", bulan, tahun, noPeserta, filterRencanaKontrol.ToString());

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSuratKontrolList.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Select.ResponseSuratKontrolList.Root()
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSuratKontrolList.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Select.ResponseSuratKontrolList.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Select.ResposeSep.Root GetRencanaKontrolByNoSep(string noSep)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("RencanaKontrol/nosep/{0}", noSep);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResposeSep.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Select.ResposeSep.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResposeSep.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Select.ResposeSep.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Select.ResponseSuratKontrol.Root GetRencanaKontrolByNoSuratKontrol(string noSuratKontrol)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("RencanaKontrol/noSuratKontrol/{0}", noSuratKontrol);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSuratKontrol.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Select.ResponseSuratKontrol.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSuratKontrol.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Select.ResponseSuratKontrol.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Select.ResponseSuratKontrolList.Root GetDataRencanaKontrolByTanggal(DateTime tglAwal, DateTime tglAkhir, Enum.FilterRencanaKontrol filterRencanaKontrol)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("RencanaKontrol/ListRencanaKontrol/tglAwal/{0}/tglAkhir/{1}/filter/{2}", tglAwal.ToString("yyyy-MM-dd"), tglAkhir.ToString("yyyy-MM-dd"), filterRencanaKontrol.ToString());

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSuratKontrolList.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Select.ResponseSuratKontrolList.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSuratKontrolList.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Select.ResponseSuratKontrolList.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Select.ResponseSpesialistikList.Root GetDataRencanaKontrolSpesialistik(Enum.JenisKontrol jenisKontrol, string nomor, DateTime tglKontrol)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("RencanaKontrol/ListSpesialistik/JnsKontrol/{0}/nomor/{1}/TglRencanaKontrol/{2}", jenisKontrol.ToString(), nomor, tglKontrol.ToString("yyyy-MM-dd"));

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSpesialistikList.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Select.ResponseSpesialistikList.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseSpesialistikList.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Select.ResponseSpesialistikList.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.Select.ResponseJadwalPraktekDokterList.Root GetDataRencanaKontrolJadwalDokter(Enum.JenisKontrol jenisKontrol, string kodePoli, DateTime tglKontrol)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("RencanaKontrol/JadwalPraktekDokter/JnsKontrol/{0}/KdPoli/{1}/TglRencanaKontrol/{2}", jenisKontrol.ToString(), kodePoli, tglKontrol.ToString("yyyy-MM-dd"));

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseJadwalPraktekDokterList.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.Select.ResponseJadwalPraktekDokterList.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.Select.ResponseJadwalPraktekDokterList.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.Select.ResponseJadwalPraktekDokterList.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.InsertSpri.Response.Root Insert(RencanaKontrol.InsertSpri.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "RencanaKontrol/InsertSPRI";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.InsertSpri.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.InsertSpri.Response.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.InsertSpri.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.InsertSpri.Response.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.RencanaKontrol.UpdateSpri.Response.Root Update(RencanaKontrol.UpdateSpri.Request.Root root)
        {
            //{BASE URL}/{Service Name}/PRB/insert
            _url += "RencanaKontrol/UpdateSPRI";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.PUT, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.RencanaKontrol.UpdateSpri.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.RencanaKontrol.UpdateSpri.Response.Root
                        {
                            MetaData = new RencanaKontrol.MetaData
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.RencanaKontrol.UpdateSpri.Response.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new RencanaKontrol.UpdateSpri.Response.Root()
                    {
                        MetaData = new RencanaKontrol.MetaData
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.InternalSep.Select.Root GetInternalSep(string noSep)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("SEP/Internal/{0}", noSep);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v11.InternalSep.Select.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v11.InternalSep.Select.Root
                        {
                            MetaData = new InternalSep.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v11.InternalSep.Select.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new InternalSep.Select.Root()
                    {
                        MetaData = new InternalSep.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v11.InternalSep.Delete.Response.Root Delete(InternalSep.Delete.Request.Root root)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("SEP/Internal/delete");

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.DELETE, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<v11.InternalSep.Delete.Response.Root>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        var entity = new v11.InternalSep.Delete.Response.Root
                //        {
                //            MetaData = new InternalSep.MetaData
                //            {
                //                Code = encryptedResponse.MetaData.Code.ToString(),
                //                Message = encryptedResponse.MetaData.Message
                //            },
                //            Response = decryptResponse
                //        };

                //        return entity;
                //    }
                //    else return new InternalSep.Delete.Response.Root()
                //    {
                //        MetaData = new InternalSep.MetaData
                //        {
                //            Code = encryptedResponse.MetaData.Code.ToString(),
                //            Message = encryptedResponse.MetaData.Message
                //        },
                //        Response = null
                //    };
                //}
            }
        }

        public v20.Fingerprint.Select.Root GetFingerprints(string tglPelayanan)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("SEP/FingerPrint/List/Peserta/TglPelayanan/{0}", tglPelayanan);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v20.Fingerprint.Select.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v20.Fingerprint.Select.Root
                        {
                            MetaData = new v20.Fingerprint.Select.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v20.Fingerprint.Select.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new v20.Fingerprint.Select.Root()
                    {
                        MetaData = new v20.Fingerprint.Select.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v20.Rujukan.SelectDataJumlahSEPRujukan.Root GetDataJumlahSEPRujukan(Enum.JenisFaskes jenisRujukan, string noRujukan)
        {
            //{Base URL}/{Service Name}/sep/JasaRaharja/Suplesi/{Parameter 1}/tglPelayanan/{Parameter 2}
            _url += string.Format("Rujukan/JumlahSEP/{0}/{1}", jenisRujukan.ToString(), noRujukan);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v20.Rujukan.SelectDataJumlahSEPRujukan.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v20.Rujukan.SelectDataJumlahSEPRujukan.Root
                        {
                            MetaData = new v20.Rujukan.SelectDataJumlahSEPRujukan.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v20.Rujukan.SelectDataJumlahSEPRujukan.TResponse>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new v20.Rujukan.SelectDataJumlahSEPRujukan.Root()
                    {
                        MetaData = new v20.Rujukan.SelectDataJumlahSEPRujukan.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }

        public v20.RujukanKhusus.Select.Root GetRujukanKhusus(int bulan, int tahun)
        {
            //{Base URL}/{Service Name}/Rujukan/Khusus/List/Bulan/{parameter 1}/Tahun/{parameter 2}
            _url += string.Format("Rujukan/Khusus/List/Bulan/{0}/Tahun/{1}", bulan, tahun);

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<v20.RujukanKhusus.Select.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new v20.RujukanKhusus.Select.Root()
                        {
                            MetaData = new v20.RujukanKhusus.MetaData()
                            {
                                Code = encryptedResponse.MetaData.Code.ToString(),
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<v20.RujukanKhusus.Select.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new v20.RujukanKhusus.Select.Root()
                    {
                        MetaData = new v20.RujukanKhusus.MetaData()
                        {
                            Code = encryptedResponse.MetaData.Code.ToString(),
                            Message = encryptedResponse.MetaData.Message
                        },
                        Response = null
                    };
                }
            }
        }
    }
}