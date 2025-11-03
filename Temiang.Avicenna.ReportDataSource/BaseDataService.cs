using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Newtonsoft.Json;
using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;
using System.Data.SqlClient;

namespace Temiang.Avicenna.ReportDataSource
{
    public class BaseDataService : JsonRetWS
    {
        protected const string ErrUnspecified = "100|Unspecified error";
        protected const string ErrDataNotFound = "200|Data not found";
        protected const string ErrDataApptSlotNotFound = "201|Appointment slot not found";
        protected const string ErrDataScheNotFound = "202|Schedule not found";
        protected const string ErrDataMultipleFound = "210|Multiple data found, expect one";
        protected const string ErrDataMultipleApptSlotFound = "211|Multiple slot found, expect one";
        protected const string ErrDataMultipleScheFound = "212|Multiple schedule found, expect one";
        protected const string ErrFieldRequired = "300|Field value required";
        protected const string ErrDataApptConflict = "400|Appointment time slot has been taken";
        protected const string ErrKeyInvalid = "900|Key access invalid";
        protected const string ErrKeyExpired = "901|Key access expired";

        protected void ResponseWrite(object jsonField)
        {
            var json = JsonConvert.SerializeObject(jsonField, Formatting.Indented);
            Context.Response.Write(json);
        }

        protected static string GetErrorCode(string errorConst)
        {
            var strs = errorConst.Split('|');
            if (strs.Length > 1)
            {
                return strs[0];
            }
            else
            {
                return GetErrorCode(ErrUnspecified);
            }
        }

        protected static string GetErrorMessage(string errorConst)
        {
            var strs = errorConst.Split('|');
            if (strs.Length > 1)
            {
                return strs[1];
            }
            else
            {
                return strs[0];
            }
        }

        public class FieldDB
        {
            public string FieldName;
            public string FieldValue;
        }

        protected object HeaderField(Patient pat, Registration reg, DateTime? realizationTime)
        {
            var su = new ServiceUnit();
            su.LoadByPrimaryKey(reg.ServiceUnitID);

            var dpjp = ParamedicTeam.DPJP(reg.RegistrationNo);

            var alergiobat = new PatientAllergy();
            alergiobat.LoadByPrimaryKey("DrugAllergen", "DrugAllergen-001", pat.PatientID);

            var alergihewan = new PatientAllergy();
            alergihewan.LoadByPrimaryKey("AnimalAllergen", "AnimalAllergen-001", pat.PatientID);

            var alergidebu =  new PatientAllergy();
            alergidebu.LoadByPrimaryKey("DustAllergen", "DustAllergen-001", pat.PatientID);

            var alergimakan = new PatientAllergy();
            alergimakan.LoadByPrimaryKey("FoodAllergen", "FoodAllergen-001", pat.PatientID);

            var alergiHabit = new PatientAllergy();
            alergiHabit.LoadByPrimaryKey("Habit", "Habit-001", pat.PatientID);

            var healthcare = new Healthcare();
            var par = new AppParameter();
            par.LoadByPrimaryKey(AppParameter.ParameterItem.HealthcareID.ToString());
            healthcare.LoadByPrimaryKey(par.ParameterValue);

            var obj = new
            {
                TglKunjungan = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.DateShortMonth),
                JamDatang = reg.RegistrationTime,
                JamPeriksa = realizationTime == null ? String.Empty : Convert.ToDateTime(realizationTime).ToString("HH:mm"),
                alergiobat = alergiobat.DescAndReaction,
                alergihewan = alergihewan.DescAndReaction,
                alergidebu = alergidebu.DescAndReaction,
                alergimakan = alergimakan.DescAndReaction,
                alergiHabit = alergiHabit.DescAndReaction,
                NamaPasien = pat.PatientName,
                Salutation = StandardReference.GetItemName(AppEnum.StandardReference.Title, pat.SRSalutation),
                Pekerjaan = StandardReference.GetItemName(AppEnum.StandardReference.Occupation, pat.SROccupation),
                NoRM = pat.MedicalNo,
                JnsKel = pat.Sex == "F" ? "Perempuan" : "Laki-laki",
                Agama = StandardReference.GetItemName(AppEnum.StandardReference.Religion, pat.SRReligion),
                TglLahir = Convert.ToDateTime(pat.DateOfBirth).ToString(AppConstant.DisplayFormat.DateShortMonth),
                TempatLahir = pat.CityOfBirth,
                Umur = reg.AgeInYear > 0 ? String.Format("{0} Thn", reg.AgeInYear) : (reg.AgeInMonth > 0 ? String.Format("{0} Bln", reg.AgeInMonth) : String.Format("{0} Hr", reg.AgeInDay)),
                UmurLengkap = String.Format("{0} Thn {1} Bln {2} Hr", reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay),
                Alamat = pat.Address,
                Kecamatan = pat.County,
                Kota = pat.City,
                RuangRawat = su.ServiceUnitName,
                title   = su.SRRegistrationType== "OPR"? "ASESMEN AWAL RAWAT JALAN" : "ASESMEN RAWAT INAP TERINTEGRASI",
                NilaiKepercayaan = String.Empty,
                Ruang = su.ServiceUnitName,
                HospitalCity = healthcare.City,
                HospitalName = healthcare.HealthcareName,
                hospitalAddres = healthcare.AddressLine1,
                NamaDokter = dpjp.ParamedicName,
                SIP = dpjp.LicenseNo,
                reg.RegistrationNo,
                pat.Ssn,
                ServiceUnit = su.ServiceUnitName,
                Diagnosamasuk=reg.Anamnesis
            };
            return obj;
        }

        protected string FixParameter(string value)
        {
            if (value.Contains(","))
            {
                var pars = value.Split(',');
                if (pars.Length == 2 && pars[0] == pars[1])
                {
                    value = pars[0];
                }
            }

            return value;
        }

        protected string FixParameterV2(string value)
        {
            if (value.Length % 2 == 1)
            {
                if (value.Contains(","))
                {
                    return value.Substring(0, (value.Length - 1) / 2);
                }
            }
            return value;
        }

        #region Logging

        protected void ValidateAccessKey(string sKey)
        {
            if (sKey == "sciadmin88") return; // key sakti

            if (String.IsNullOrEmpty(sKey)) throw new Exception(
                ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), String.Format("{0} required", "AccessKey")));

            var keyColl = new WebServiceAccessKeyCollection();
            var query = new WebServiceAccessKeyQuery();
            query.Where(query.AccessKey == sKey);
            if (keyColl.Load(query))
            {
                var key = keyColl.First();
                if (key.StartDate.Value > DateTime.Now)
                {
                    throw new Exception(ErrKeyInvalid);
                }
                if (key.EndDate.Value < DateTime.Now)
                {
                    throw new Exception(ErrKeyInvalid);
                }
            }
            else
            {
                throw new Exception(ErrKeyInvalid);
            }
        }
        #endregion

        #region General Private

        protected void InspectOneResult(DataTable dtb)
        {
            switch (dtb.Rows.Count)
            {
                case 0:
                    {
                        //throw new Exception(ErrDataNotFound); 
                        var newRow = dtb.NewRow();
                        dtb.Rows.Add(newRow);
                        break;
                    }
                case 1: { break; }
                default: { throw new Exception(ErrDataMultipleFound); }
            }
        }
        protected void InspectStringRequired(string FieldName, string FieldValue)
        {
            if (String.IsNullOrEmpty(FieldValue))
                throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), String.Format("{0} required", FieldName)));
        }

        protected void SetListParameters(esDynamicQuery Query, string FieldName, string FieldValue)
        {
            if (!String.IsNullOrEmpty(FieldValue))
            {
                Query.Where(String.Format("<{0} like '{1}'>", FieldName, Helper.EscapeQuery(FieldValue)));
            }
        }

        protected void SetListParametersOR(esDynamicQuery Query, FieldDB[] Fields)
        {

            string w = String.Empty;
            foreach (var field in Fields)
            {
                w += String.Format("{2}{0} like '%{1}%'", field.FieldName, Helper.EscapeQuery(field.FieldValue), (w.Length > 0) ? "OR " : "");
            }
            Query.Where(String.Format("<{0}>", w));
        }
        #endregion

        #region General Protected FastJson

        protected List<Dictionary<string, object>> JsonGetNodeArrayRequired(string FieldName, Dictionary<string, object> jSonObj)
        {
            var oNode = JsonGetNodeArray(FieldName, jSonObj);
            if (oNode.Count == 0)
            {
                throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("{0} required", FieldName)));
            }
            return oNode;
        }
        protected List<Dictionary<string, object>> JsonGetNodeArray(string FieldName, Dictionary<string, object> jSonObj)
        {
            var objs = jSonObj.Where(x => x.Key.ToLower() == FieldName.ToLower()).Select(x => (Dictionary<string, object>)x.Value);
            return objs.ToList();
        }
        protected Dictionary<string, object> JsonGetNodeRequired(string FieldName, Dictionary<string, object> jSonObj)
        {
            var oNode = JsonGetNode(FieldName, jSonObj);
            if (oNode == null)
            {
                throw new Exception(
                    ErrFieldRequired.Replace(GetErrorMessage(ErrFieldRequired), string.Format("{0} required", FieldName)));
            }
            return oNode;
        }
        protected Dictionary<string, object> JsonGetNode(string FieldName, Dictionary<string, object> jSonObj)
        {
            var fields = jSonObj.Where(x => x.Key.ToLower() == FieldName.ToLower()).Select(x => x.Value);
            var y = fields.FirstOrDefault();
            return (Dictionary<string, object>)y;
        }
        #endregion

        #region connect
        #region Static
        public static DataTable GetDataTableDirect(string query)
        {
            string[] sForbid = new string[] { "insert ", "update ", "delete ", "drop ", "alter " };

            foreach (var sf in sForbid)
            {
                if (query.ToLower().Contains(sf.ToLower())) throw new Exception(
                    string.Format("One of the following syntax is forbidden: {0}", string.Join(", ", sForbid)));
            }

            var table = new DataTable();

            using (SqlConnection conn = new SqlConnection(esConfigSettings.ConnectionInfo.Connections[esConfigSettings.ConnectionInfo.Default].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = conn.ConnectionTimeout;
                    cmd.CommandType = CommandType.Text;

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(table);
                    }
                }
            }

            return table;
        }
        #endregion
        #endregion
    }
}
