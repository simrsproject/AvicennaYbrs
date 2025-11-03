using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MPI;
using Temiang.Avicenna.BusinessObject.Mpi;
using Temiang.Avicenna.BusinessObject;


namespace Temiang.Avicenna.Common
{
    public class Mpi
    {
        private const string _apiKey = "fa7d1528df8e20e0276d0692c7dfb0f7";
        private const string _secret = "06c219e5bc8378f3a8a3f83b4b7e4649";

        private static string MpiUrlApi
        {
            get
            {
                return AppParameter.GetParameterValue(AppParameter.ParameterItem.MpiUrlApi);
            }
        }

        public static StreamReader SendData(string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(MpiUrlApi);

            request.Method = "POST";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/x-www-form-urlencoded";
            Encoding utf8 = new UTF8Encoding();
            byte[] content = utf8.GetBytes(postData);
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(content, 0, content.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr;
        }
        private static MemoryStream ConvertToStream(string data)
        {
            // convert string to stream
            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;

        }

        private static string MD5Hash(string data)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();

            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }

        //private static string Token()
        //{
        //    string method = "Auth";
        //    string rand = new Random().Next().ToString();
        //    string auth_sig = MD5Hash(string.Concat(_secret, _apiKey, rand));
        //    string postData = string.Format("method={0}&api_key={1}&rand={2}&auth_sig={3}", method, _apiKey, rand,
        //                                    auth_sig);

        //    StreamReader sr = SendData(postData);
        //    XmlSerializer serial = new XmlSerializer(typeof(Auth));
        //    Auth result = (Auth)serial.Deserialize(sr);
        //    sr.Close();

        //    return result.Token;
        //}


        public static DataTable SearchPatientByKeywords(string search, string tanggal)
        {
            XmlDocument xmlDoc = new XmlDocument();
            using (StreamReader sr = SendData("method=pasien_cari&keywords=" + search + "&dob=" + tanggal))
            {
                MemoryStream memoryStream = ConvertToStream(sr.ReadToEnd());
                xmlDoc.Load(memoryStream);
                sr.Close();
                memoryStream.Dispose();
            }
            XmlNodeList nodeList = xmlDoc.SelectNodes("/response/patients/patient");
            DataTable dtb = XmlHelper.GetDataTable(nodeList);
            return dtb;
        }

        public static DataTable SearchPatient(string search)
        {
            XmlDocument xmlDoc = new XmlDocument();
            using (StreamReader sr = SendData("method=pasien_cari&keywords=" + search))
            {
                MemoryStream memoryStream = ConvertToStream(sr.ReadToEnd());
                xmlDoc.Load(memoryStream);
                sr.Close();
                memoryStream.Dispose();
            }
            XmlNodeList nodeList = xmlDoc.SelectNodes("/response/patients/patient");
            DataTable dtb = XmlHelper.GetDataTable(nodeList);
            return dtb;
        }

        public static Hashtable PatientDetil(string id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            using (StreamReader sr = SendData("method=pasien_detil&patient_id=" + id))
            {
                MemoryStream memoryStream = ConvertToStream(sr.ReadToEnd());
                xmlDoc.Load(memoryStream);
                sr.Close();
                memoryStream.Dispose();
            }
            Hashtable patient = new Hashtable();
            XmlNode node = xmlDoc.LastChild.SelectSingleNode("patient");
            if (node != null)
                foreach (XmlElement childNode in node.ChildNodes)
                {
                    patient.Add(childNode.Name, childNode.InnerText);
                }
            return patient;

        }

        //public static PatientSearch SearchPatient(string search)
        //{
        //    ////string token = Token();
        //    //string method = "patient_cari";
        //    //string rand = new Random().Next().ToString();
        //    ////-- Search Arg
        //    ////fullname = "Agus";
        //    ////mrn = "";
        //    //string gender = "";
        //    //string address = "";
        //    //string phone = "";
        //    //string regional_cd = "";
        //    //// string keywords = "";

        //    //string uname = "admin";
        //    //string uid = "admin";

        //    //ArrayList arrayList = new ArrayList();
        //    //arrayList.Add("method" + method);
        //    //arrayList.Add("rand" + rand);

        //    //if (keywords != string.Empty)
        //    //    arrayList.Add("keywords" + keywords);

        //    ////if (mrn != string.Empty)
        //    ////    arrayList.Add("mrn" + mrn);

        //    //arrayList.Add("uname" + uname);
        //    //arrayList.Add("uid" + uid);
        //    //arrayList.Add("v1.0");
        //    //arrayList.Sort();
        //    //string apiSig = "";//token;
        //    //foreach (string item in arrayList)
        //    //{
        //    //    apiSig = string.Concat(apiSig, item);
        //    //}

        //    //apiSig = MD5Hash(apiSig);
        //    //string postData =
        //    //    string.Format(
        //    //        "method={0}&api_key={1}&rand={2}&v=v1.0&fullname={3}&mrn={4}&uname={5}&uid={6}&api_sig={7}",
        //    //        method, _apiKey, rand, fullname, mrn, uname, uid, apiSig);

        //    StreamReader sr = SendData("method=pasien_cari&keywords="+search);
        //    MemoryStream memoryStream = ConvertToStream(sr.ReadToEnd());
        //    //string content = sr.ReadToEnd();
        //    XmlSerializer serial = new XmlSerializer(typeof(PatientSearch));
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.Load(memoryStream);


        //    PatientSearch result = (PatientSearch)serial.Deserialize(memoryStream);
        //    sr.Close();

        //    return result;
        //}
    }
}
