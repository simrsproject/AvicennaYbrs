using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.ReportDataSource.Common;

namespace Temiang.Avicenna.ReportDataSource.RSMM.Emr
{
    /// <summary>
    /// Summary description for Assessment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NosocomialMon :  BaseDataService
    {

        #region Shared Method
        private bool ValidateParameterAndLoadMonitoring(string accessKey, ref string p_RegistrationNo,
            ref string p_MonitoringNo, ref NosocomialMonitoring mon, ref Patient pat, ref Registration reg)
        {
            accessKey = FixParameter(accessKey);
            p_RegistrationNo = FixParameter(p_RegistrationNo);
            p_MonitoringNo = FixParameter(p_MonitoringNo);

            mon = new NosocomialMonitoring();
            mon.LoadByPrimaryKey(p_RegistrationNo, p_MonitoringNo.ToInt());

            reg = new Registration();
            reg.LoadByPrimaryKey(p_RegistrationNo);

            pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            return true;
        }

        #endregion

        /// <summary>
        /// Bed Rest Nosocomial Monitoring
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BedRest(string accessKey, string p_RegistrationNo, string p_MonitoringNo, string p_UserName)
        {
            try
            {
                p_UserName = FixParameter(p_UserName);
                var reg = new Registration();
                var pat = new Patient();
                var nm = new NosocomialMonitoring();
                if (ValidateParameterAndLoadMonitoring(accessKey, ref p_RegistrationNo, ref p_MonitoringNo, ref nm, ref pat, ref reg))
                {
                    // Row Detail
                    var mon = new NosocomialMonitoringBedRestQuery("mon");
                    var ppa = new AppUserQuery("ppa");
                    mon.InnerJoin(ppa).On(mon.MonitoringByUserID == ppa.UserID);
                    mon.Select("<MonitoringDate = CONVERT(VARCHAR, mon.MonitoringDateTime, 103)>",
                        "<DayNo = DATEDIFF(d, '" + nm.InstallationDateTime.Value.ToString("yyyyMMdd") + "', mon.MonitoringDateTime)>",
                        mon.Mobilization,
                        mon.IsMobilization,
                        mon.IsInjuryCare,
                        mon.SkinCondition,
                        mon.InjuryCondition,
                        mon.Fisiotherapi,
                        ppa.UserName.As("Petugas"),
                        mon.Note);
                    mon.Where(mon.RegistrationNo == p_RegistrationNo, mon.MonitoringNo == p_MonitoringNo);

                    var dtbMon = mon.LoadDataTable();

                    var additionalField = new
                    {
                        DecubitusDate = Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute),
                        Problem = nm.Problem,
                        Diagnosa = EpisodeDiagnose.MainDiagnose(p_RegistrationNo),
                        IsDecubitusRoom = nm.DecubitusFromType == "R",
                        IsDecubitusRS = nm.DecubitusFromType == "H",
                        IsDecubitusHome = nm.DecubitusFromType == "R",
                        DecubitusFromRS = nm.DecubitusFromType == "H" ? nm.DecubitusFrom : string.Empty,
                        DecubitusFromRoom = nm.DecubitusFromType == "R" ? nm.DecubitusFrom : string.Empty,
                        nm.Location,
                        MonitoringItems = ConvertDataTabletoObject(dtbMon),
                        PrintByUserName = p_UserName

                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// Surgery Nosocomial Monitoring
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Surgery(string accessKey, string p_RegistrationNo, string p_MonitoringNo, string p_UserName)
        {
            try
            {
                p_UserName = FixParameter(p_UserName);
                var reg = new Registration();
                var pat = new Patient();
                var nm = new NosocomialMonitoring();
                if (ValidateParameterAndLoadMonitoring(accessKey, ref p_RegistrationNo, ref p_MonitoringNo, ref nm, ref pat, ref reg))
                {
                    // Row Detail
                    var mon = new NosocomialMonitoringSurgeryQuery("mon");
                    var ppa = new AppUserQuery("ppa");
                    mon.InnerJoin(ppa).On(mon.MonitoringByUserID == ppa.UserID);
                    mon.Select("<MonitoringDate = CONVERT(VARCHAR, mon.MonitoringDateTime, 103)>",
                        "<DayNo = DATEDIFF(d, '" + nm.InstallationDateTime.Value.ToString("yyyyMMdd") + "', mon.MonitoringDateTime)>",
                        mon.InjuryCondition,
                        mon.IsAfDrain,
                        mon.IsAfSuture,
                        mon.IsTempAbove38,
                        mon.IsRedness,
                        mon.IsSwollen,
                        mon.IsPain,
                        mon.IsFeelingHot,
                        mon.IsPus,
                        ppa.UserName.As("Petugas"),
                        mon.Note);
                    mon.Where(mon.RegistrationNo == p_RegistrationNo, mon.MonitoringNo == p_MonitoringNo);
                    var dtbMon = mon.LoadDataTable();


                    var sur = new ServiceUnitBooking();
                    sur.LoadByPrimaryKey(nm.ReferenceNo);

                    var ps = new PpiProcedureSurveillance();
                    ps.LoadByPrimaryKey(nm.ReferenceNo);


                    var additionalField = new
                    {
                        SurgeryDate = Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute),
                        Problem = nm.Problem,
                        Diagnosa = EpisodeDiagnose.MainDiagnose(p_RegistrationNo),
                        nm.Location,

                        Antibiotic = nm.Antibiotic.Replace(';',','),
                        OtherDrugs = nm.OtherDrugs.Replace(';', ','),
                        SurgeryByName = Paramedic.GetParamedicName(sur.ParamedicID),

                        // WoundClassification
                        //01	Bersih
                        //02	Bersih Tercemar
                        //03	Tercemar
                        //04	Kotor
                        IsOperasiB = ps.SRWoundClassification == "01",
                        IsOperasiT = ps.SRWoundClassification == "03",
                        IsOperasiBT = ps.SRWoundClassification == "02",
                        IsOperasiK = ps.SRWoundClassification == "04",

                        // ASA Score
                        //01	ASA I	Seorang pasien yang normal dan sehat, selain penyakit yang akan dioperasi.
                        //02	ASA II	Seorang pasien dengan penyakit sistemik ringan sampai sedang.
                        //03	ASA III	Seorang pasien dengan penyakit sistemik berat yang belum mengancam jiwa.
                        //04	ASA IV	Seorang pasien dengan penyakit sistemik berat yang mengancam jiwa.
                        //05	ASA V	Penderita sekarat yang mungkin tidak bertahan dalam waktu 24 jam dengan atau tanpa pembedahan,
                        //kategori ini meliputi penderita yang sebelumnya sehat, disertai dengan perdarahan yang tidak terkontrol, begitu juga penderita usia lanjutdengan penyakit terminal.
                        IsAsa01 = ps.SRAsaScore == "01",
                        IsAsa02 = ps.SRAsaScore == "02",
                        IsAsa03 = ps.SRAsaScore == "03",
                        IsAsa04 = ps.SRAsaScore == "04",
                        IsAsa05 = ps.SRAsaScore == "05",
                        MonitoringItems = ConvertDataTabletoObject(dtbMon),
                        PrintByUserName = p_UserName

                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// Mechanic Ventilator / ETT  Monitoring
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MechanicVent(string accessKey, string p_RegistrationNo, string p_MonitoringNo, string p_UserName)
        {
            try
            {
                p_UserName = FixParameter(p_UserName);
                var reg = new Registration();
                var pat = new Patient();
                var nm = new NosocomialMonitoring();
                if (ValidateParameterAndLoadMonitoring(accessKey, ref p_RegistrationNo, ref p_MonitoringNo, ref nm, ref pat, ref reg))
                {
                    // Row Detail
                    var mon = new NosocomialMonitoringEttQuery("mon");
                    var ppa = new AppUserQuery("ppa");
                    mon.InnerJoin(ppa).On(mon.MonitoringByUserID == ppa.UserID);
                    mon.Select(mon,
                        "<MonitoringDate = CONVERT(VARCHAR, mon.MonitoringDateTime, 103)>",
                        "<DayNo = DATEDIFF(d, '" + nm.InstallationDateTime.Value.ToString("yyyyMMdd") + "', mon.MonitoringDateTime)>",
                        ppa.UserName.As("Petugas"));
                    mon.Where(mon.RegistrationNo == p_RegistrationNo, mon.MonitoringNo == p_MonitoringNo);
                    var dtbMon = mon.LoadDataTable();

                    var additionalField = new
                    {
                        Diagnosa = EpisodeDiagnose.MainDiagnose(p_RegistrationNo),
                        InstallationDate = Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonth),
                        InstallationTime = Convert.ToDateTime(nm.InstallationDateTime).ToString("hh:mm"),
                        ReleaseDate = nm.ReleaseDateTime == null || nm.ReleaseDateTime.Value.Year == 1900 ? string.Empty : nm.ReleaseDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        ReleaseTime = nm.ReleaseDateTime == null || nm.ReleaseDateTime.Value.Year == 1900 ? string.Empty : nm.ReleaseDateTime.Value.ToString("hh:mm"),
                        InstallationBy = AppUser.GetUserName(nm.InstallationByUserID),
                        InstallationRoom = ServiceRoom.GetRoomName(nm.RoomID),
                        EttType = StandardReference.GetItemName(AppEnum.StandardReference.EttType,nm.SREttType),
                        nm.BodyWeight,
                        nm.FiO2,
                        nm.VentilationMode,
                        nm.Peep,
                        nm.TidalVolume,
                        nm.PeakFlow,
                        nm.RespirationRate,
                        nm.Sensitivity,
                        InstallationReason = nm.Problem,
                        nm.Location,
                        MonitoringItems = ConvertDataTabletoObject(dtbMon),
                        PrintByUserName = p_UserName

                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// Dower Catheter  Monitoring
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DowerCatheter(string accessKey, string p_RegistrationNo, string p_MonitoringNo, string p_UserName)
        {
            try
            {
                p_UserName = FixParameter(p_UserName);
                var reg = new Registration();
                var pat = new Patient();
                var nm = new NosocomialMonitoring();
                if (ValidateParameterAndLoadMonitoring(accessKey, ref p_RegistrationNo, ref p_MonitoringNo, ref nm, ref pat, ref reg))
                {
                    // Row Detail
                    var mon = new NosocomialMonitoringCatheterQuery("mon");
                    var ppa = new AppUserQuery("ppa");
                    mon.InnerJoin(ppa).On(mon.MonitoringByUserID == ppa.UserID);
                    mon.Select(mon,
                        "<MonitoringDate = CONVERT(VARCHAR, mon.MonitoringDateTime, 103)>",
                        "<DayNo = DATEDIFF(d, '" + nm.InstallationDateTime.Value.ToString("yyyyMMdd") + "', mon.MonitoringDateTime)>",
                        ppa.UserName.As("Petugas"));
                    mon.Where(mon.RegistrationNo == p_RegistrationNo, mon.MonitoringNo == p_MonitoringNo);
                    var dtbMon = mon.LoadDataTable();

                    var additionalField = new
                    {
                        Diagnosa = EpisodeDiagnose.MainDiagnose(p_RegistrationNo),
                        InstallationDate = Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonth),
                        InstallationTime = Convert.ToDateTime(nm.InstallationDateTime).ToString("hh:mm"),
                        ReleaseDate = nm.ReleaseDateTime == null || nm.ReleaseDateTime.Value.Year == 1900 ? string.Empty : nm.ReleaseDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        ReleaseTime = nm.ReleaseDateTime == null || nm.ReleaseDateTime.Value.Year == 1900 ? string.Empty : nm.ReleaseDateTime.Value.ToString("hh:mm"),
                        InstallationBy = AppUser.GetUserName(nm.InstallationByUserID),
                        InstallationRoom = ServiceRoom.GetRoomName(nm.RoomID),
                        nm.TypeOfCatheter,
                        nm.Problem,
                        nm.Location,
                        MonitoringItems = ConvertDataTabletoObject(dtbMon),
                        PrintByUserName = p_UserName

                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// Dower Catheter  Monitoring
        /// </summary>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Infus(string accessKey, string p_RegistrationNo, string p_MonitoringNo, string p_UserName)
        {
            try
            {
                p_UserName = FixParameter(p_UserName);
                var reg = new Registration();
                var pat = new Patient();
                var nm = new NosocomialMonitoring();
                if (ValidateParameterAndLoadMonitoring(accessKey, ref p_RegistrationNo, ref p_MonitoringNo, ref nm, ref pat, ref reg))
                {
                    // Row Detail
                    var mon = new NosocomialMonitoringInfusQuery("mon");
                    var ppa = new AppUserQuery("ppa");
                    mon.InnerJoin(ppa).On(mon.MonitoringByUserID == ppa.UserID);
                    var stdi = new AppStandardReferenceItemQuery("stdi");
                    mon.LeftJoin(stdi).On(mon.SRInfusSet == stdi.ItemID & stdi.StandardReferenceID == "InfusSet");
                    mon.Select(mon,
                        "<MonitoringDate = CONVERT(VARCHAR, mon.MonitoringDateTime, 103)>",
                        "<DayNo = DATEDIFF(d, '" + nm.InstallationDateTime.Value.ToString("yyyyMMdd") + "', mon.MonitoringDateTime)>",
                        ppa.UserName.As("Petugas"), stdi.ItemName.As("InfusSet"));
                    mon.Where(mon.RegistrationNo == p_RegistrationNo, mon.MonitoringNo == p_MonitoringNo);
                    var dtbMon = mon.LoadDataTable();

                    var additionalField = new
                    {
                        Diagnosa = EpisodeDiagnose.MainDiagnose(p_RegistrationNo),
                        InstallationDate = Convert.ToDateTime(nm.InstallationDateTime).ToString(AppConstant.DisplayFormat.DateShortMonth),
                        InstallationTime = Convert.ToDateTime(nm.InstallationDateTime).ToString("hh:mm"),
                        ReleaseDate = nm.ReleaseDateTime == null || nm.ReleaseDateTime.Value.Year == 1900 ? string.Empty : nm.ReleaseDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonth),
                        ReleaseTime = nm.ReleaseDateTime == null || nm.ReleaseDateTime.Value.Year == 1900 ? string.Empty : nm.ReleaseDateTime.Value.ToString("hh:mm"),
                        InstallationBy = AppUser.GetUserName(nm.InstallationByUserID),
                        InstallationRoom = ServiceRoom.GetRoomName(nm.RoomID),
                        InfusLocation = nm.Location,
                        JenisCairan = nm.TypeOfInfus,
                        Antibiotic = nm.Antibiotic.Replace(';', ','),
                        OtherDrugs = nm.OtherDrugs.Replace(';', ','),
                        nm.Problem,
                        MonitoringItems = ConvertDataTabletoObject(dtbMon),
                        PrintByUserName = p_UserName
                    };

                    var retField = MergeJsonData(HeaderField(pat, reg, null), additionalField);

                    ResponseWrite(retField);
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write(ex.Message);
            }
        }

    }
}
