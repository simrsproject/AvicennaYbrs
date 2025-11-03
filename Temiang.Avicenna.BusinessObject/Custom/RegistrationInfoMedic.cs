using System;
using System.Linq;
using System.Web;
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RegistrationInfoMedic
    {
        private string formatedinfo;
        public string UserName
        {
            get { return GetColumn("refToAppUser_UserName").ToString(); }
            set { SetColumn("refToAppUser_UserName", value); }
        }
        public string FormattedInfo
        {
            get { return formatedinfo; }
            set { formatedinfo = value; }
        }

        public void PopulatePrescriptionCurrentDay()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            PopulatePrescriptionCurrentDay(reg.ParamedicID, reg.RegistrationNo, reg.FromRegistrationNo);
        }

        public void PopulatePrescriptionCurrentDay(string paramedicID, string registrationNo, string fromRegistrationNo)
        {
            // Update histori resep dari dokter bersangkutan
            var updateDate = DateTimeInfo.Value.Date;
            var prescHist = string.Empty;
            if (string.IsNullOrEmpty(fromRegistrationNo))
                prescHist = TransPrescription.PrescriptionHist(paramedicID, registrationNo, updateDate);
            else
                prescHist = string.Format("{0}{1}{1}{2}", TransPrescription.PrescriptionHist(paramedicID, fromRegistrationNo, updateDate), Environment.NewLine, TransPrescription.PrescriptionHist(paramedicID, registrationNo, updateDate));

            if (!string.IsNullOrEmpty(prescHist.Replace("\r\n", ""))) //Jangan diisi jika hanya berisi baris kosong (Handono 231209)
                PrescriptionCurrentDay = prescHist;
            else
                PrescriptionCurrentDay = string.Empty;
        }

        public static class Last
        {
            public static RegistrationInfoMedic RegistrationInfoMedicInfo2(string registrationNo, string fromRegistrationNo)
            {
                var qra = new RegistrationInfoMedicQuery("a");
                if (!String.IsNullOrWhiteSpace(fromRegistrationNo))
                    qra.Where(qra.Or(qra.RegistrationNo == registrationNo, qra.RegistrationNo == fromRegistrationNo));
                else
                    qra.Where(qra.RegistrationNo == registrationNo,qra.SRMedicalNotesInputType== "SOAP");
                //qra.Where(qra.RegistrationNo.In(mergeRegistrations));

                qra.OrderBy(qra.RegistrationInfoMedicID.Descending);
                qra.es.Top = 1;
                var assesment = new RegistrationInfoMedic();
                if (assesment.Load(qra))
                {
                    return assesment;
                }
                else
                {
                    return new RegistrationInfoMedic();
                }
            }
        }

        public override void Save()
        {
            if (this.es.IsAdded)
            {
                str.SRUserType = string.Empty;
                if (string.IsNullOrEmpty(CreatedByUserID))
                {
                    var userLogin = new UserLogin();
                    if (HttpContext.Current.Session["_UserLogin"] != null)
                    {
                        userLogin = ((UserLogin)HttpContext.Current.Session["_UserLogin"]);
                        CreatedByUserID = userLogin.UserID;

                    }
                }

                // User dokter DPJP
                var user = new AppUser();
                user.LoadByPrimaryKey(CreatedByUserID);
                if (!string.IsNullOrEmpty(user.ParamedicID))
                    IsCreatedByUserDpjp =
                        ParamedicTeam.IsParamedicTeamStatusDpjp(RegistrationNo, user.ParamedicID, CreatedDateTime ?? DateTime.Now);

                SRUserType = user.SRUserType;

            }

            // Save Log
            if (this.es.IsModified)
            {
                var log = string.Empty;
                if (this.Info1 == null)
                    this.Info1 = ""; //TODO: Cari bugs nya dimana

                if (this.Info2 == null)
                    this.Info2 = ""; //TODO: Cari bugs nya dimana

                if (this.Info3 == null)
                    this.Info3 = ""; //TODO: Cari bugs nya dimana

                if (this.Info4 == null)
                    this.Info4 = ""; //TODO: Cari bugs nya dimana

                if (this.Info1 != null && !this.Info1.Equals(this.GetOriginalColumnValue("Info1")))
                {
                    log = string.Format("{0}{1}{1}[{2}] {3}", this.Info1Log, Environment.NewLine,
                        this.GetOriginalColumnValue("LastUpdateDateTime"), this.GetOriginalColumnValue("Info1"));
                    this.SetColumn("Info1Log", log);
                }

                if (this.Info2 != null && !this.Info2.Equals(this.GetOriginalColumnValue("Info2")))
                {
                    log = string.Format("{0}{1}{1}[{2}] {3}", this.Info2Log, Environment.NewLine,
                        this.GetOriginalColumnValue("LastUpdateDateTime"), this.GetOriginalColumnValue("Info2"));
                    this.SetColumn("Info2Log", log);
                }
                if (this.Info3 != null && !this.Info3.Equals(this.GetOriginalColumnValue("Info3")))
                {
                    log = string.Format("{0}{1}{1}[{2}] {3}", this.Info2Log, Environment.NewLine,
                        this.GetOriginalColumnValue("LastUpdateDateTime"), this.GetOriginalColumnValue("Info3"));
                    this.SetColumn("Info3Log", log);
                }
                if (this.Info4 != null && !this.Info4.Equals(this.GetOriginalColumnValue("Info4")))
                {
                    log = string.Format("{0}{1}{1}[{2}] {3}", this.Info4Log, Environment.NewLine,
                        this.GetOriginalColumnValue("LastUpdateDateTime"), this.GetOriginalColumnValue("Info4"));
                    this.SetColumn("Info4Log", log);
                }
            }

            base.Save();
        }
    }

    public partial class RegistrationInfoMedicCollection
    {
        public override void Save()
        {
            foreach (var entity in this)
            {
                if (entity.es.IsAdded)
                {
                    entity.str.SRUserType = string.Empty;
                    if (string.IsNullOrEmpty(entity.CreatedByUserID))
                    {
                        var userLogin = new UserLogin();
                        if (HttpContext.Current.Session["_UserLogin"] != null)
                        {
                            userLogin = ((UserLogin)HttpContext.Current.Session["_UserLogin"]);
                            entity.CreatedByUserID = userLogin.UserID;

                        }
                    }

                    // User dokter DPJP
                    var user = new AppUser();
                    user.LoadByPrimaryKey(entity.CreatedByUserID);
                    if (!string.IsNullOrEmpty(user.ParamedicID))
                        entity.IsCreatedByUserDpjp =
                            ParamedicTeam.IsParamedicTeamStatusDpjp(entity.RegistrationNo, user.ParamedicID, entity.CreatedDateTime ?? DateTime.Now);

                    entity.SRUserType = user.SRUserType;

                }

                // Save Log
                if (entity.es.IsModified)
                {
                    var log = string.Empty;

                    if (!entity.Info1.Equals(entity.GetOriginalColumnValue("Info1")))
                    {
                        log = string.Format("{0}{1}{1}[{2}] {3}", entity.Info1Log, Environment.NewLine,
                            entity.GetOriginalColumnValue("LastUpdateDateTime"), entity.GetOriginalColumnValue("Info1"));
                        entity.SetColumn("Info1Log", log);
                    }

                    if (!entity.Info2.Equals(entity.GetOriginalColumnValue("Info2")))
                    {
                        log = string.Format("{0}{1}{1}[{2}] {3}", entity.Info2Log, Environment.NewLine,
                            entity.GetOriginalColumnValue("LastUpdateDateTime"), entity.GetOriginalColumnValue("Info2"));
                        entity.SetColumn("Info2Log", log);
                    }
                    if (!entity.Info3.Equals(entity.GetOriginalColumnValue("Info3")))
                    {
                        log = string.Format("{0}{1}{1}[{2}] {3}", entity.Info2Log, Environment.NewLine,
                            entity.GetOriginalColumnValue("LastUpdateDateTime"), entity.GetOriginalColumnValue("Info3"));
                        entity.SetColumn("Info3Log", log);
                    }
                    if (!entity.Info4.Equals(entity.GetOriginalColumnValue("Info4")))
                    {
                        log = string.Format("{0}{1}{1}[{2}] {3}", entity.Info4Log, Environment.NewLine,
                            entity.GetOriginalColumnValue("LastUpdateDateTime"), entity.GetOriginalColumnValue("Info4"));
                        entity.SetColumn("Info4Log", log);
                    }
                }
            }

            base.Save();
        }
    }



}
