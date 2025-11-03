using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public class BaseAssessmentCtl : System.Web.UI.UserControl
    {
        #region properties Current Registration
        protected string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string ReferFromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        public string RegistrationType
        {
            get
            {
                return Request.QueryString["rt"];
            }
        }
        public string ModeType
        {
            get
            {
                return Request.QueryString["mod"];
            }
        }
        protected virtual List<string> MergeRegistrations
        {
            get
            {
                return AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
            }
        }
        protected string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        protected bool IsClosed
        {
            get { return Convert.ToBoolean(ViewState["isClosed"]); }
            set { ViewState["isClosed"] = value; }
        }
        private string _patientID;
        protected string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    _patientID = reg.PatientID;
                }
                else
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }
        protected string AssessmentType
        {
            get
            {
                return Request.QueryString["astp"];
            }
        }
        protected string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }
        public string RegistrationInfoMedicID
        {
            get
            {
                if (!IsPostBack)
                    hdnRegistrationInfoMedicID.Value = Request.QueryString["rimid"] == "undefined" ? string.Empty : Request.QueryString["rimid"];
                return hdnRegistrationInfoMedicID.Value;
            }
            set { hdnRegistrationInfoMedicID.Value = value; }
        }

        private HiddenField _hdnRimID;
        private HiddenField hdnRegistrationInfoMedicID
        {
            get
            {
                return _hdnRimID ??
                       (_hdnRimID = (HiddenField)Helper.FindControlRecursive(this.Page, "hdnRegistrationInfoMedicID"));
            }
        }
        private List<string> _patientRelateds;
        protected List<string> PatientRelateds
        {
            get
            {
                if (_patientRelateds == null)
                {
                    _patientRelateds = Patient.PatientRelateds(PatientID);
                }
                return _patientRelateds;
            }
        }
        public string PastMedicalHistRefId
        {
            get { return Convert.ToString(ViewState["_mc_PastMedicalHistRefId"]); }
            set { ViewState["_mc_PastMedicalHistRefId"] = value; }
        }
        public string FamilyMedHistRefId
        {
            get { return Convert.ToString(ViewState["_mc_FamilyMedHistRefId"]); }
            set { ViewState["_mc_FamilyMedHistRefId"] = value; }
        }
        public string ReferenceValue
        {
            get { return Convert.ToString(ViewState["_mc_RefVal"]); }
            set { ViewState["_mc_RefVal"] = value; }
        }
        public string Description
        {
            get { return Convert.ToString(ViewState["_mc_Descr"]); }
            set { ViewState["_mc_Descr"] = value; }
        }
        public bool IsPanelCollapse
        {
            get { return Convert.ToBoolean(ViewState["_mc_ispc"]); }
            set { ViewState["_mc_ispc"] = value; }
        }
        public bool IsUserEditAble
        {
            get { return Convert.ToBoolean(ViewState["_mc_isueditable"]); }
            set { ViewState["_mc_isueditable"] = value; }
        }
        public bool IsUserAddAble
        {
            get { return Convert.ToBoolean(ViewState["_mc_isuaddable"]); }
            set { ViewState["_mc_isuaddable"] = value; }
        }

        public string ValidationGroup
        {
            get { return Convert.ToString(ViewState[this.ID + "_valgrp"]); }
            set { ViewState[this.ID + "_valgrp"] = value; }
        }

        protected bool IsUserCanNotEdit()
        {
            if (IsUserEditAble.Equals(false)) return true;

            if (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
            {
                return true;
            }
            if (AppSession.UserLogin.ParamedicID.Equals(ParamedicID))
                return false;

            return !IsUserInParamedicTeam();
        }
        protected bool IsUserCanNotAdd()
        {
            if (this.IsUserAddAble.Equals(false)) return true;

            if (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
            {
                // User selain dokter bisa tambah record dan hak aksesnya diset di entriannya page yg dipanggil
                return false;
            }
            else
            {
                if (AppSession.UserLogin.ParamedicID.Equals(ParamedicID))
                    return false;
            }


            return !IsUserInParamedicTeam();
        }
        private bool IsUserInParamedicTeam()
        {
            // Jika user paramedic cek apakah termasuk Paramedic Team nya
            if (IsPostBack)
            {
                if (Session["IsUserInParamedicTeam"] != null)
                    return (bool)Session["IsUserInParamedicTeam"];
            }

            var qrPt = new ParamedicTeamQuery("pt");
            qrPt.Where(qrPt.RegistrationNo == RegistrationNo && qrPt.ParamedicID == AppSession.UserLogin.ParamedicID &&
                       (qrPt.EndDate.IsNull() || qrPt.EndDate >= DateTime.Today));
            var dtbPt = qrPt.LoadDataTable();
            bool retval = dtbPt != null && dtbPt.Rows.Count > 0;

            Session["IsUserInParamedicTeam"] = retval;
            return retval;
        }
        #endregion

        public enum EntryGroupEnum
        {
            None,
            Anamnesis,
            PhysicalExam
        }
        public virtual EntryGroupEnum EntryGroup { get; set; }

        public enum ColumnEnum
        {
            Left,
            Right
        }
        public virtual ColumnEnum Column { get; set; }


        private Label _handleTime;
        protected Label HandleTime
        {
            get
            {
                return _handleTime ?? (_handleTime = (Label)Helper.FindControlRecursive(this.Page, "lblHandleTime"));
            }
        }        
        
        protected DateTime AssessmentDateTime
        {
            get
            {
                var today = (new DateTime()).NowAtSqlServer(); 
                var times = this.HandleTime.Text.Split(':');
                var assessmentDateTime = new DateTime(today.Year, today.Month, today.Day, times[0].ToInt(), times[1].ToInt(), 0);
                return assessmentDateTime;
            }
        }

        #region override method
        protected virtual void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
        }

        protected virtual void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            throw new Exception("The method OnSetEntityValue is not implemented.");
        }


        protected virtual void OnBeforeMenuEditClick(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
        }
        protected virtual void OnDataModeChanged(bool isEdited)
        {
        }
        public virtual void OnMenuNewClick()
        {
        }
        #endregion


        public void SetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            OnSetEntityValue(args, assessment, rim);
        }

        public void PopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            OnPopulateEntryControl(assessment, rim);
        }

        public void DataModeChanged(bool isEdited)
        {
            OnDataModeChanged(isEdited);
        }

        protected bool IsEdited
        {
            get
            {
                // Krn ada penggabungan tipe assessement
                try
                {
                    var basePage = (AssessmentEntry) this.Page;
                    return (basePage.DataModeCurrent == AppEnum.DataMode.Edit || basePage.DataModeCurrent == AppEnum.DataMode.New);

                }
                catch (Exception e)
                {
                }

                try
                {
                    var basePage = (AssessmentEntry)this.Page;
                    return (basePage.DataModeCurrent == AppEnum.DataMode.Edit || basePage.DataModeCurrent == AppEnum.DataMode.New);

                }
                catch (Exception e)
                {
                    
                }
                return false;
            }
        }

        protected AppEnum.DataMode DataModeCurrent
        {
            get
            {

                // Krn ada penggabungan tipe assessement
                try
                {
                    var basePage = (AssessmentEntry)this.Page;
                    return basePage.DataModeCurrent;
                }
                catch (Exception e)
                {
                }

                try
                {
                    var basePage = (AssessmentEntry)this.Page;
                    return basePage.DataModeCurrent;

                }
                catch (Exception e)
                {

                }
                return AppEnum.DataMode.Read;
            }
        }

        protected void SoapObjectivePopulateWith(StringBuilder strBuilder, PhysicalExamMetod pem, string label)
        {
            string notNormalNotes = null;
            if (!string.IsNullOrEmpty(pem.AbNormalAndNotes.Notes))
            {
                notNormalNotes = string.Format(": {0}", pem.AbNormalAndNotes.Notes);
            }

            // String Builder
            if (pem.AbNormalAndNotes.IsAbNormal
                || !string.IsNullOrEmpty(pem.AbNormalAndNotes.Notes)
                || !string.IsNullOrEmpty(pem.Auskultasi)
                || !string.IsNullOrEmpty(pem.Inspeksi)
                || !string.IsNullOrEmpty(pem.Palpasi)
                || !string.IsNullOrEmpty(pem.Perkusi))
            {
                strBuilder.AppendFormat("{0}: ", label);
                if (pem.AbNormalAndNotes.IsAbNormal)
                {
                    strBuilder.AppendFormat("Abnormal{0}", notNormalNotes);
                    strBuilder.AppendLine(string.Empty);
                }
                else
                {
                    strBuilder.AppendFormat("{0}", notNormalNotes);
                    strBuilder.AppendLine(string.Empty);
                }

                if (!string.IsNullOrEmpty(pem.Inspeksi))
                {
                    strBuilder.AppendFormat("   • Inspeksi: {0}", pem.Inspeksi);
                    strBuilder.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(pem.Palpasi))
                {
                    strBuilder.AppendFormat("   • Palpasi: {0}", pem.Palpasi);
                    strBuilder.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(pem.Perkusi))
                {
                    strBuilder.AppendFormat("   • Perkusi: {0}", pem.Perkusi);
                    strBuilder.AppendLine(string.Empty);
                }
                if (!string.IsNullOrEmpty(pem.Auskultasi))
                {
                    strBuilder.AppendFormat("   • Auskultasi: {0}", pem.Auskultasi);
                    strBuilder.AppendLine(string.Empty);
                }
                strBuilder.AppendLine(string.Empty);
            }
        }

        public bool SetIsAbnormalValue(int selectedIndex, string notes)
        {
            if (selectedIndex == 1) return true;

            if (!string.IsNullOrWhiteSpace(notes)) return true;

            return false;
        }
    }
}