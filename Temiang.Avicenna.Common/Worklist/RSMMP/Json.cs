using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temiang.Avicenna.Common.Worklist.RSMMP
{
    public class Json
    {
        public class Order
        {
            public class Request
            {
                public class Patient
                {
                    [JsonProperty("Address")]
                    public string Address;

                    [JsonProperty("Alerts")]
                    public string Alerts;

                    [JsonProperty("Allergies")]
                    public string Allergies;

                    [JsonProperty("BirthDate")]
                    public string BirthDate;

                    [JsonProperty("BirthPlace")]
                    public string BirthPlace;

                    [JsonProperty("Diagnosis")]
                    public string Diagnosis;

                    [JsonProperty("Email")]
                    public string Email;

                    [JsonProperty("FirstName")]
                    public string FirstName;

                    [JsonProperty("Gender")]
                    public string Gender;

                    [JsonProperty("Height")]
                    public int? Height;

                    [JsonProperty("LastName")]
                    public string LastName;

                    [JsonProperty("MedicalNo")]
                    public string MedicalNo;

                    [JsonProperty("MiddleName")]
                    public string MiddleName;

                    [JsonProperty("Phone")]
                    public string Phone;

                    [JsonProperty("Pregnancy")]
                    public string Pregnancy;

                    [JsonProperty("Ssn")]
                    public string Ssn;

                    [JsonProperty("Weight")]
                    public int? Weight;
                }

                public class Payor
                {
                    [JsonProperty("Code")]
                    public string Code;

                    [JsonProperty("Name")]
                    public string Name;
                }

                public class ParamedicPic
                {
                    [JsonProperty("Code")]
                    public string Code;
                }

                public class ParamedicRef
                {
                    [JsonProperty("Code")]
                    public string Code;

                    [JsonProperty("Name")]
                    public string Name;
                }

                public class Study
                {
                    [JsonProperty("Code")]
                    public string Code;
                }

                public class UnitPic
                {
                    [JsonProperty("Code")]
                    public string Code;
                }

                public class UnitRef
                {
                    [JsonProperty("Code")]
                    public string Code;

                    [JsonProperty("Name")]
                    public string Name;
                }

                public class Exam
                {
                    [JsonProperty("Datetime")]
                    public string Datetime;

                    [JsonProperty("HisRefId")]
                    public string HisRefId;

                    [JsonProperty("Payor")]
                    public Payor Payor;

                    [JsonProperty("ParamedicPic")]
                    public ParamedicPic ParamedicPic;

                    [JsonProperty("ParamedicRef")]
                    public ParamedicRef ParamedicRef;

                    [JsonProperty("Study")]
                    public Study Study;

                    [JsonProperty("CitoStat")]
                    public bool CitoStat;

                    [JsonProperty("UnitPic")]
                    public UnitPic UnitPic;

                    [JsonProperty("UnitRef")]
                    public UnitRef UnitRef;

                    [JsonProperty("UserId")]
                    public string UserId;
                }

                public class Root
                {
                    [JsonProperty("Patient")]
                    public Patient Patient;

                    [JsonProperty("Exams")]
                    public List<Exam> Exams;
                }
            }

            public class Response
            {
                public class Exams
                {
                    [JsonProperty("ExamNo")]
                    public string ExamNo;

                    [JsonProperty("HisRefId")]
                    public string HisRefId;

                    [JsonProperty("FilmNo")]
                    public string FilmNo;

                    [JsonProperty("PacsStudyUid")]
                    public string PacsStudyUid;
                }

                public class Metadata
                {
                    [JsonProperty("Code")]
                    public int Code;

                    [JsonProperty("Message")]
                    public string Message;
                }

                public class Root
                {
                    [JsonProperty("Metadata")]
                    public Metadata Metadata;

                    [JsonProperty("Exams")]
                    public List<Exams> Exams;
                }
            }
        }

        public class Login
        {
            public class Request
            {
                public class Root
                {
                    [JsonProperty("siteId")]
                    public int SiteId { get; set; }
                    [JsonProperty("code")]
                    public string Code { get; set; }
                    [JsonProperty("key")]
                    public string Key { get; set; }
                }
            }

            public class Response
            {
                public class MetaData
                {
                    [JsonProperty("code")]
                    public int Code;

                    [JsonProperty("message")]
                    public string Message;
                }

                public class TResponse
                {
                    [JsonProperty("siteName")]
                    public string SiteName;

                    [JsonProperty("code")]
                    public string Code;

                    [JsonProperty("token")]
                    public string Token;

                    [JsonProperty("fullToken")]
                    public string FullToken;

                    [JsonProperty("issuer")]
                    public string Issuer;

                    [JsonProperty("issueDateTime")]
                    public string IssueDateTime;

                    [JsonProperty("expiredDateTime")]
                    public string ExpiredDateTime;
                }

                public class Root
                {
                    [JsonProperty("metaData")]
                    public MetaData MetaData;

                    [JsonProperty("response")]
                    public TResponse Response;
                }
            }
        }

        public class ImageStatus
        {
            public class Response
            {
                public class Root
                {
                    [JsonProperty("statusCode")]
                    public int? StatusCode;

                    [JsonProperty("pacsStudyUid")]
                    public string PacsStudyUid;
                }
            }
        }
    }
}
