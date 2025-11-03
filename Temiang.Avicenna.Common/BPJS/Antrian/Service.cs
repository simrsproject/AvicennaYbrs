using LZStringCSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common.BPJS.VClaim;

namespace Temiang.Avicenna.Common.BPJS.Antrian
{
    public class Service
    {
        private string _url = ConfigurationManager.AppSettings["BPJSAntrianServiceUrlLocation"];
        private string _consKey = ConfigurationManager.AppSettings["BPJSAntrianConsumerID"];
        private string _ppkKey = ConfigurationManager.AppSettings["BPJSAntrianHospitalID"];
        private string _salt = ConfigurationManager.AppSettings["BPJSAntrianSaltConsumerID"];
        private string _encrypted = ConfigurationManager.AppSettings["BPJSResponseIsEncrypted"];
        private string _userKey = ConfigurationManager.AppSettings["BPJSAntrianUserKey"];

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

        public Referensi.Poli.Root GetReferensiPoli()
        {
            _url += "ref/poli";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.Poli.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.Poli.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Referensi.Poli.Response()
                            {
                                List = JsonConvert.DeserializeObject<List<Referensi.Poli.List>>(decryptResponse)
                            }
                        };

                        return entity;
                    }
                    else return new Referensi.Poli.Root
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

        public Referensi.Dokter.Root GetReferensiDokter()
        {
            _url += "ref/dokter";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.Dokter.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.Dokter.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Referensi.Dokter.Response()
                            {
                                List = JsonConvert.DeserializeObject<List<Referensi.Dokter.List>>(decryptResponse)
                            }
                        };

                        return entity;
                    }
                    else return new Referensi.Dokter.Root
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

        public Referensi.JadwalDokter.Root GetJadwalDokter(string kodePoli, string tanggal)
        {
            _url += $"jadwaldokter/kodepoli/{kodePoli}/tanggal/{tanggal}";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.JadwalDokter.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.JadwalDokter.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Referensi.JadwalDokter.Response()
                            {
                                List = JsonConvert.DeserializeObject<List<Referensi.JadwalDokter.List>>(decryptResponse)
                            }
                        };

                        return entity;
                    }
                    else return new Referensi.JadwalDokter.Root
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

        public MetadataResponse UpdateJadwalDokter(Update.JadwalDokter.Request.Root root)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/Update
            _url += "jadwaldokter/updatejadwaldokter";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<MetadataResponse>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsAntrolValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        return new Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code,
                //            Message = encryptedResponse.MetaData.Message
                //        };
                //    }
                //    else return new Metadata()
                //    {
                //        Code = encryptedResponse.MetaData.Code,
                //        Message = encryptedResponse.MetaData.Message
                //    };
                //}
            }
        }

        public MetadataResponse TambahAntrian(Tambah.Request.Root root)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/Update
            _url += "antrean/add";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<MetadataResponse>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsAntrolValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        return new Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code,
                //            Message = encryptedResponse.MetaData.Message
                //        };
                //    }
                //    else return new Metadata()
                //    {
                //        Code = encryptedResponse.MetaData.Code,
                //        Message = encryptedResponse.MetaData.Message
                //    };
                //}
            }
        }

        public MetadataResponse UpdateWaktuAntrian(Update.WaktuAntrian.Request.Root root)
        {
            //var ws = new WebServiceAPILog();
            //var str = "<Params LIKE '%{\"kodebooking\":\"XXX\",\"taskid\":YYY%'>";
            //str = str.Replace("XXX", root.Kodebooking);
            //str = str.Replace("YYY", root.Taskid.ToString());
            //ws.Query.Where(str);
            //if (ws.Query.Load())
            //{
            //    var metadata = JsonConvert.DeserializeObject<MetadataResponse>(ws.Response);
            //    if (metadata.Metadata.IsAntrolValid)
            //    {
            //        return new MetadataResponse()
            //        {
            //            Metadata = new Metadata()
            //            {
            //                Code = "200",
            //                Message = $"TaskId {root.Taskid} untuk kode booking {root.Kodebooking} sudah ada"
            //            }
            //        };
            //    }
            //}

            _url = ConfigurationManager.AppSettings["BPJSAntrianServiceUrlLocation"];
            _url += "antrean/updatewaktu";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 

                var metadata = JsonConvert.DeserializeObject<MetadataResponse>(sr.ReadToEnd());
                if (!metadata.Metadata.IsAntrolValid && metadata.Metadata.Message.Trim() == "Kode Booking tidak ditemukan")
                {
                    var apt = root.Kodebooking.Split('-');
                    if (apt.Length == 3)
                    {
                        var param = new Update.WaktuAntrian.Request.Root()
                        {
                            Kodebooking = $"{apt[0]}/{apt[1]}-{apt[2]}",
                            Taskid = root.Taskid,
                            Waktu = root.Waktu,
                            Jenisresep = root.Jenisresep
                        };
                        return UpdateWaktuAntrian(param);
                    }
                }
                return metadata;
                //    else
                //    {
                //        var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //        if (encryptedResponse.MetaData.IsAntrolValid)
                //        {
                //            var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //            return new Metadata()
                //            {
                //                Code = encryptedResponse.MetaData.Code,
                //                Message = encryptedResponse.MetaData.Message
                //            };
                //        }
                //        else return new Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code,
                //            Message = encryptedResponse.MetaData.Message
                //        };
                //    }
            }
        }

        public MetadataResponse BatalAntrian(Update.BatalAntrian.Request.Root root)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/Update
            _url += "antrean/batal";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<MetadataResponse>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsAntrolValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        return new Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code,
                //            Message = encryptedResponse.MetaData.Message
                //        };
                //    }
                //    else return new Metadata()
                //    {
                //        Code = encryptedResponse.MetaData.Code,
                //        Message = encryptedResponse.MetaData.Message
                //    };
                //}
            }
        }

        public List.TaskId.Response.Root GetListWaktuTaskId(List.TaskId.Request.Root root)
        {
            _url += $"antrean/getlisttask";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<List.TaskId.Response.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new List.TaskId.Response.Root
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<List<List.TaskId.Response.List>>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new List.TaskId.Response.Root
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

        public List.Dashboard.PerTanggal.Root DashboardPerTanggal(string tanggal, string waktu)
        {
            _url += $"dashboard/waktutunggu/tanggal/{tanggal}/waktu/{waktu}";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<List.Dashboard.PerTanggal.Root>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsAntrolValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        var entity = new List.Dashboard.PerTanggal.Root()
                //        {
                //            Metadata = new Metadata()
                //            {
                //                Code = encryptedResponse.MetaData.Code,
                //                Message = encryptedResponse.MetaData.Message
                //            },
                //            Response = JsonConvert.DeserializeObject<List.Dashboard.PerTanggal.Response>(decryptResponse)
                //        };

                //        return entity;
                //    }
                //    else return new List.Dashboard.PerTanggal.Root
                //    {
                //        Metadata = new Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code,
                //            Message = encryptedResponse.MetaData.Message
                //        },
                //        Response = null
                //    };
                //}
            }
        }

        public List.Dashboard.PerBulan.Root DashboardPerBulan(string bulan, string tahun, string waktu)
        {
            _url += $"dashboard/waktutunggu/bulan/{bulan}/tahun/{tahun}/waktu/{waktu}";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<List.Dashboard.PerBulan.Root>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsAntrolValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        var entity = new List.Dashboard.PerBulan.Root()
                //        {
                //            Metadata = new Metadata()
                //            {
                //                Code = encryptedResponse.MetaData.Code,
                //                Message = encryptedResponse.MetaData.Message
                //            },
                //            Response = JsonConvert.DeserializeObject<List.Dashboard.PerBulan.Response>(decryptResponse)
                //        };

                //        return entity;
                //    }
                //    else return new List.Dashboard.PerBulan.Root
                //    {
                //        Metadata = new Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code,
                //            Message = encryptedResponse.MetaData.Message
                //        },
                //        Response = null
                //    };
                //}
            }
        }

        public Referensi.PoliFingerPrint.Root GetReferensiPoliFingerPrint()
        {
            _url += "ref/poli/fp";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.PoliFingerPrint.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.PoliFingerPrint.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Referensi.PoliFingerPrint.Response()
                            {
                                List = JsonConvert.DeserializeObject<List<Referensi.PoliFingerPrint.List>>(decryptResponse)
                            }
                        };

                        return entity;
                    }
                    else return new Referensi.PoliFingerPrint.Root
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

        public Referensi.PasienFingerPrint.Root GetReferensiPasienFingerPrint(VClaim.Enum.Identitas identitas, string text)
        {
            _url += $"ref/pasien/fp/identitas/{identitas.ToString()}/noidentitas/{text}";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<Referensi.PasienFingerPrint.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new Referensi.PasienFingerPrint.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = JsonConvert.DeserializeObject<Referensi.PasienFingerPrint.Response>(decryptResponse)
                        };

                        return entity;
                    }
                    else return new Referensi.PasienFingerPrint.Root
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

        public List.Antrean.PerTanggal.Root GetAntreanPerTanggal(string tanggal)
        {
            _url += $"antrean/pendaftaran/tanggal/{tanggal}";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<List.Antrean.PerTanggal.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new List.Antrean.PerTanggal.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Antrian.List.Antrean.PerTanggal.Response()
                            {
                                List = JsonConvert.DeserializeObject<List<Antrian.List.Antrean.PerTanggal.List>>(decryptResponse)
                            }
                        };

                        return entity;
                    }
                    else return new List.Antrean.PerTanggal.Root
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

        public List.Antrean.BelumDilayani.Root GetAntreanBelumDilayani(string tanggal)
        {
            _url += $"antrean/pendaftaran/aktif";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<List.Antrean.BelumDilayani.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new List.Antrean.BelumDilayani.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Antrian.List.Antrean.BelumDilayani.Response()
                            {
                                List = JsonConvert.DeserializeObject<List<List.Antrean.BelumDilayani.List>>(decryptResponse)
                            }
                        };

                        return entity;
                    }
                    else return new List.Antrean.BelumDilayani.Root
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

        public MetadataResponse TambahAntrianFarmasi(Farmasi.Request.Root root)
        {
            //{BASE URL}/{Service Name}/SEP/1.1/Update
            _url += "antrean/farmasi/add";

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.POST, Helper.WebRequestContentType.FORM, JsonConvert.SerializeObject(root), out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                //if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") 
                return JsonConvert.DeserializeObject<MetadataResponse>(sr.ReadToEnd());
                //else
                //{
                //    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                //    if (encryptedResponse.MetaData.IsAntrolValid)
                //    {
                //        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                //        return new Metadata()
                //        {
                //            Code = encryptedResponse.MetaData.Code,
                //            Message = encryptedResponse.MetaData.Message
                //        };
                //    }
                //    else return new Metadata()
                //    {
                //        Code = encryptedResponse.MetaData.Code,
                //        Message = encryptedResponse.MetaData.Message
                //    };
                //}
            }
        }

        public List.Antrean.PerKodeBooking.Root GetAntreanPerKodeBooking(string kodeBooking)
        {
            _url += "antrean/pendaftaran/kodebooking/" + kodeBooking;

            using (var response = PopulateWebRequest(_url, Helper.WebRequestMethod.GET, out var timeStamp).GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK) throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                var sr = new StreamReader(response.GetResponseStream());

                if (string.IsNullOrEmpty(_encrypted) || _encrypted == "false") return JsonConvert.DeserializeObject<List.Antrean.PerKodeBooking.Root>(sr.ReadToEnd());
                else
                {
                    var encryptedResponse = JsonConvert.DeserializeObject<Helper.EncryptedResponse.Root>(sr.ReadToEnd());
                    if (encryptedResponse.MetaData.IsAntrolValid)
                    {
                        var decryptResponse = LZString.DecompressFromEncodedURIComponent(DecryptResponse(timeStamp, encryptedResponse.Response));
                        var entity = new List.Antrean.PerKodeBooking.Root()
                        {
                            Metadata = new Metadata()
                            {
                                Code = encryptedResponse.MetaData.Code,
                                Message = encryptedResponse.MetaData.Message
                            },
                            Response = new Antrian.List.Antrean.PerKodeBooking.Response()
                            {
                                List = JsonConvert.DeserializeObject<List<List.Antrean.PerKodeBooking.List>>(decryptResponse)
                            }
                        };

                        return entity;
                    }
                    else return new Antrian.List.Antrean.PerKodeBooking.Root()
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
    }
}