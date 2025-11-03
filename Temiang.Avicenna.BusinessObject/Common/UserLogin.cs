using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Temiang.Avicenna.BusinessObject;
using RestSharp;
using System.Net;

namespace Temiang.Avicenna.BusinessObject.Common
{
    public class UserLogin
    {
        public UserLogin()
        {

        }

        public UserLogin(AppUser user)
        {
            UserID = user.UserID;
            UserName = user.UserName;
            Password = user.Password;
            SRLanguage = user.SRLanguage;
            SRUserType = user.SRUserType ?? String.Empty;
            ActiveDate = user.ActiveDate ?? DateTime.Now;
            ExpireDate = user.ExpireDate ?? DateTime.Now;
            ParamedicID = user.ParamedicID ?? string.Empty;
            ServiceUnitID = user.ServiceUnitID;
            LicenseNo = user.LicenseNo;
            PersonID = user.PersonID;

            if (!string.IsNullOrEmpty(user.ParamedicID))
            {
                var par = new Paramedic();
                if (par.LoadByPrimaryKey(user.ParamedicID))
                {
                    ParamedicName = par.ParamedicName;
                    SmfID = par.SRParamedicRL1;
                }
                else
                {
                    ParamedicName = string.Empty;
                    SmfID = string.Empty;
                }

                // Reset jika tidak ada di Paramedic
                if (string.IsNullOrEmpty(ParamedicName))
                    ParamedicID = string.Empty;
                else
                {
                    // Check Paramedic in Radiology Service Unit 
                    var appPar = new AppParameter();
                    if (appPar.LoadByPrimaryKey(AppParameter.ParameterItem.ServiceUnitRadiologyID.ToString()))
                    {
                        var su = new ServiceUnitParamedic();
                        IsRadilogyParamedic = (su.LoadByPrimaryKey(appPar.ParameterValue, ParamedicID));
                    }

                    if (IsRadilogyParamedic == false)
                    {
                        appPar = new AppParameter();
                        if (appPar.LoadByPrimaryKey(AppParameter.ParameterItem.ServiceUnitRadiologyID2.ToString()))
                        {
                            var su = new ServiceUnitParamedic();
                            IsRadilogyParamedic = (su.LoadByPrimaryKey(appPar.ParameterValue, ParamedicID));
                        }
                    }


                }
            }
        }
        public UserLogin(AppUser user, int logID, string userHostName)
            : this(user)
        {
            UserLogID = logID;
            UserHostName = userHostName;
        }

        public KeyValuePair<string, string> Validate(AppUser user, string passwdEncrypted, ref Int32 attempt)
        {
            if (user.IsLocked ?? false)
            {
                return new KeyValuePair<string, string>("2", "User id is locked, contact administrator");
            }
            else if (!user.Password.Equals(passwdEncrypted))
            {
                attempt = attempt + 1;

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes" && attempt >= Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin)))
                {
                    user.IsLocked = true;
                    user.Save();
                    return new KeyValuePair<string, string>("1", string.Format("Failed login attempt is {0} of {1}, user id will deactivated or locked by system, contact administrator", attempt, Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin))));
                }
                else if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingPasswordPolicy).ToLower() == "yes" && attempt < Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin)))
                {
                    return new KeyValuePair<string, string>("2", string.Format("Password is not accepted or false, Failed login attempt is {0} of {1}", attempt, Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.MaxAttemptFailedLogin))));
                }
                else
                {
                    return new KeyValuePair<string, string>("2", "Password is not accepted or false");
                }
            }
            else if (user.ExpireDate.Value.AddDays(1) < DateTime.Now)
            {
                return new KeyValuePair<string, string>("1", "User id has expired, contact administrator");
            }
            else if (AppParameter.GetParameterValue(AppParameter.ParameterItem.HealthcareInitial).ToLower() == "rsi" &&
                AppParameter.GetParameterValue(AppParameter.ParameterItem.IsParamedicAbsentByIMMADokter).ToLower() == "yes" &&
                user.SRUserType == AppUser.UserType.Doctor)
            {
                var userdctr = user.UserID;
                var url = "https://app.rsimmanuel.net:9099/checkAbsent";

                var client = new RestClient(url);
                var request = new RestRequest();
                request.Method = Method.GET;
                request.AddParameter("user_id", userdctr, ParameterType.QueryString);

                request.AddHeader("AccessKey", "SimRSI25");

                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NotFound)
                {
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);

                    if (result.status == "ERR" && response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return new KeyValuePair<string, string>("1", $"{result.message.ToString()} (IMMA DOKTER)");
                    }

                    bool isAbsen = result.isAbsen;
                    string message = result.message;
                    string parname = result.paramedic_name;

                    if (!isAbsen)
                    {
                        return new KeyValuePair<string, string>("1", $"{message}-{parname} (IMMA DOKTER)");
                    }
                }
                else
                {
                    return new KeyValuePair<string, string>("1", "Unable to check absent status. Please try again.");
                }
            }
            return new KeyValuePair<string, string>("0", "");
        }

        #region Properties
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserHostName { get; set; }
        public string SRLanguage { get; set; }
        public string SRUserType { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string ParamedicID { get; set; }
        public string ParamedicName { get; set; }
        public bool IsRadilogyParamedic { get; set; }
        public string ServiceUnitID { get; set; }
        public string LicenseNo { get; set; }
        public Int64? UserLogID { get; set; }
        public System.Int32? PersonID { get; set; }
        public string SmfID { get; set; }

        // Untuk keperluan pengecekan pass-code (secure program), selama belum pindah ke program lain maka tidak perlu isi pass-code (Handono 2303)
        public string LastAccessSecureProgramID { get; set; }
        // End
        #endregion

        public static class UserType
        {
            public static string Administration = "ADM";
            public static string Doctor = "DTR";
            public static string Nurse = "NRS";

        }

    }
}
