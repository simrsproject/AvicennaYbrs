using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI.Calendar;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.DynamicQuery;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using System.Web.Util;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Display,Edit, Add PPA Notes Respond / Result Template base on PpaNoteEntry Function
    /// </summary>
    /// Create By: Handono 
    /// Date: 2023 Nov 29
    public partial class QuestionRespondEntry : BasePageDialogEntry
    {
        private Dictionary<string, esEntityWAuditLog> _othRelatedEntities = new Dictionary<string, esEntityWAuditLog>();
        private string TransactionNoPhr => Request.QueryString["trNo"];
        private int TemplateID => Request.QueryString["tid"].ToInt();

        private NursingDiagnosaTransDT _currentNursingDiagnosaTransDT;
        private NursingDiagnosaTransDT CurrentNursingDiagnosaTransDT
        {
            get
            {
                if (_currentNursingDiagnosaTransDT == null)
                {
                    var ndt = new NursingDiagnosaTransDT();
                    ndt.Query.Where(ndt.Query.ReferenceToPhrNo == TransactionNoPhr);
                    ndt.Query.es.Top = 1;
                    ndt.Query.Load();

                    _currentNursingDiagnosaTransDT = ndt;
                }
                return _currentNursingDiagnosaTransDT;
            }
        }

        private Patient _patientCurrent;
        private Patient PatientCurrent
        {
            get
            {
                if (_patientCurrent == null)
                {
                    _patientCurrent = new Patient();
                    _patientCurrent.LoadByPrimaryKey(RegistrationCurrent.PatientID);
                }

                return _patientCurrent;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsMedicalRecordEntry = true; //Activate deadline edit & add
            ToolBar.AutoSaveVisible = false;
            ToolBar.SaveAndEditVisible = false;
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    var nt = new NursingDiagnosaTemplate();
                    nt.LoadByPrimaryKey(TemplateID);

                    this.Title = string.Format("{0} : {1} (MRN: {2})", !string.IsNullOrEmpty(nt.TemplateText) ? nt.TemplateText : nt.TemplateName, pat.PatientName, pat.MedicalNo);
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            phrCtl.InitializedQuestion(TemplateID.ToString(), true);
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            phrCtl.SetReadOnlyPatientHealthRecordLine(newVal == AppEnum.DataMode.Read, PatientCurrent, RegistrationCurrent);
        }

        private NursingTransHD CreateNursingTransHD()
        {
            AppAutoNumberLast autoNumberND;
            autoNumberND = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.NursingCareNo);
            // save autonumber immediately to decrease time gap between create and save
            autoNumberND.Save();

            var hd = new NursingTransHD();
            hd.TransactionNo = autoNumberND.LastCompleteNumber;
            hd.NursingTransDateTime = txtDateTimeImplementation.SelectedDate;
            hd.RegistrationNo = RegistrationNo;
            //Last Update Status

            hd.CreateByUserID = AppSession.UserLogin.UserID;
            hd.CreateDateTime = DateTime.Now;
            hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
            hd.LastUpdateDateTime = DateTime.Now;

            return hd;
        }
        private void Save(ValidateArgs args)
        {
            if (AppSession.Parameter.IsUsingValidationImplementationDateTimeOnPpaNotes)
            {
                if (txtDateTimeImplementation.SelectedDate > DateTime.Now)
                {
                    args.MessageText = "Implementation Date Time must not exceed System Date Time.";
                    return;
                }
            }

            var nursingTxNo = string.Empty;
            var ndt = new NursingDiagnosaTransDT();
            var hd = new NursingTransHD();
            if (!string.IsNullOrEmpty(TransactionNoPhr))
            {
                ndt.Query.Where(ndt.Query.ReferenceToPhrNo == TransactionNoPhr);
                ndt.Query.es.Top = 1;
                if (!ndt.Query.Load())
                {
                    // Anggap TransactionNo yg dikirim tidak valid jadi batalkan pengeditan
                    return;
                }
                nursingTxNo = ndt.TransactionNo;

                hd = new NursingTransHD();
                hd.Query.Where(hd.Query.RegistrationNo == RegistrationNo, hd.Query.TransactionNo == nursingTxNo);
                hd.Query.es.Top = 1;
                hd.Query.Load();
            }
            else
            {
                // Mode New
                // Check NursingTransHD
                hd = new NursingTransHD();
                hd.Query.Where(hd.Query.RegistrationNo == RegistrationNo);
                hd.Query.es.Top = 1;
                if (!hd.Query.Load())
                {
                    hd = CreateNursingTransHD();
                }
                nursingTxNo = hd.TransactionNo;
            }

            ndt.TmpNursingDiagnosaID = string.Empty;

            ndt.TransactionNo = nursingTxNo;
            ndt.NursingDiagnosaID = string.Empty;
            ndt.NursingDiagnosaName = string.Empty;
            ndt.SRNursingDiagnosaLevel = "31";
            //if (!string.IsNullOrEmpty(hfNicInt.Value))
            //{
            //    Int64 id = System.Convert.ToInt64(hfNicInt.Value);
            //    ndt.ParentID = id;
            //}
            //ndt.NursingDiagnosaParentID = hfNicCode.Value;
            ndt.Priority = 0;
            ndt.EvalPeriod = 0;
            ndt.PeriodConversionInHour = 24;
            ndt.Skala = 1;
            ndt.Target = 0;
            ndt.Evaluasi = 1;
            ndt.Respond = string.Empty;
            ndt.Reexamine = false;
            ndt.ExecuteDateTime = txtDateTimeImplementation.SelectedDate;

            ndt.S = string.Empty;
            ndt.O = string.Empty;
            ndt.A = string.Empty;
            ndt.P = string.Empty;
            ndt.Info5 = string.Empty;
            ndt.PpaInstruction = string.Empty;
            ndt.ParamedicID = string.Empty;
            ndt.SubmitBy = string.Empty;
            ndt.ReceiveBy = string.Empty;

            ndt.TmpNursingDiagnosaParentID = string.Empty;
            ndt.TemplateID = TemplateID;

            if (ndt.es.IsAdded || ndt.es.IsModified)
            {
                ndt.LastUpdateByUserID = AppSession.UserLogin.UserID;
                ndt.LastUpdateDateTime = DateTime.Now;
            }
            if (ndt.es.IsAdded)
            {
                ndt.CreateByUserID = AppSession.UserLogin.UserID;
                ndt.CreateDateTime = DateTime.Now;
            }

            // PHR
            var phr = new PatientHealthRecord();
            if (!string.IsNullOrEmpty(TransactionNoPhr))
            {
                phr.Query.Where(phr.Query.TransactionNo == TransactionNoPhr,
                    phr.Query.RegistrationNo == RegistrationNo);
                if (!phr.Load(phr.Query))
                    phr = new PatientHealthRecord();
            }

            AppAutoNumberLast autoNumberPHR;
            //if (phr.es.IsAdded) // Perintah phr = new PatientHealthRecord() tdak mengakibatkan phr.es.IsAdded == true sebelum propertiesnya ada yg diisi
            if (phr.TransactionNo == null)
            {
                autoNumberPHR = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.PatientHealthRecord);
                phr.TransactionNo = autoNumberPHR.LastCompleteNumber;
                autoNumberPHR.Save();

                ndt.ReferenceToPhrNo = phr.TransactionNo;

                phr.CreateByUserID = AppSession.UserLogin.UserID;
                phr.CreateDateTime = DateTime.Now;
            }

            // Set PHR Date
            var phrDate = txtDateTimeImplementation.SelectedDate.Value;
            phr.RecordDate = phrDate;
            phr.RecordTime = phrDate.ToString("HH:mm");

            phr.RegistrationNo = RegistrationNo;
            phr.QuestionFormID = TemplateID.ToString();

            phr.EmployeeID = AppSession.UserLogin.UserID;
            phr.IsComplete = true;
            phr.ReferenceNo = string.Empty;
            phr.ExaminerID = AppSession.UserLogin.UserID;
            phr.ServiceUnitID = string.Empty;

            if (phr.es.IsAdded || phr.es.IsModified)
            {
                phr.LastUpdateByUserID = AppSession.UserLogin.UserID;
                phr.LastUpdateDateTime = DateTime.Now;
            }

            var phrlColl = new PatientHealthRecordLineCollection();
            if (!string.IsNullOrEmpty(TransactionNoPhr))
            {
                phrlColl.Query.Where(phrlColl.Query.TransactionNo == phr.TransactionNo,
                    phrlColl.Query.RegistrationNo == RegistrationNo, phrlColl.Query.QuestionFormID == TemplateID.ToString());
                phrlColl.LoadAll();
                foreach (var phrl in phrlColl)
                {
                    phrl.MarkAsDeleted();
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                phrCtl.SetEntityValue(PatientCurrent, RegistrationCurrent, _othRelatedEntities, phr, phrlColl, LastRegistrationNo);

                phr.Save();
                phrlColl.Save();

                if (PatientCurrent.es.IsModified)
                    PatientCurrent.Save();

                if (RegistrationCurrent.es.IsModified)
                    RegistrationCurrent.Save();

                // othRelatedEntities
                foreach (var othRelatedEntity in _othRelatedEntities.Values)
                {
                    if (othRelatedEntity.es.IsModified || othRelatedEntity.es.IsAdded)
                        othRelatedEntity.Save();
                }

                // Simpan rangkuman di Objective field untuk kecepatan nanti saat menampilkan data rangkumannya 
                var questions = new QuestionCollection();
                ndt.O = RADT.Emr.NursingImplementationEntry.ParsePhrlRespond(phrlColl, questions);

                ndt.Save();
                hd.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args);
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args);
        }
        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
            txtDateTimeImplementation.SelectedDate = DateTime.Now;
        }

        private string _lastRegistrationNo = null;
        private string LastRegistrationNo
        {
            get
            {
                if (_lastRegistrationNo == null)
                {
                    var lastReg = PatientCurrent.LastRegistration();
                    if (lastReg != null)
                        _lastRegistrationNo = lastReg.RegistrationNo;
                    else
                        _lastRegistrationNo = string.Empty;
                }

                return _lastRegistrationNo;
            }

        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            txtDateTimeImplementation.SelectedDate = CurrentNursingDiagnosaTransDT.ExecuteDateTime ?? DateTime.Now;
            txtDateTimeImplementation.Enabled = AppSession.Parameter.IsAllowEditDateTimeImplementation;

            var phr = new PatientHealthRecord();
            phr.LoadByPrimaryKey(
                TransactionNoPhr,
                RegistrationNo,
                TemplateID.ToString());
            phrCtl.PopulateValue(PatientCurrent, RegistrationCurrent, _othRelatedEntities, phr, LastRegistrationNo);
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                // delete PatientHealthRecordLine
                var phrlColl = new PatientHealthRecordLineCollection();
                phrlColl.Query.Where(phrlColl.Query.TransactionNo == TransactionNoPhr,
                    phrlColl.Query.RegistrationNo == RegistrationNo, phrlColl.Query.QuestionFormID == TemplateID.ToString());
                if (phrlColl.LoadAll())
                {
                    phrlColl.MarkAllAsDeleted();
                    phrlColl.Save();
                }

                // delete PatientHealthRecord
                var phr = new PatientHealthRecord();
                if (phr.LoadByPrimaryKey(TransactionNoPhr, RegistrationNo, TemplateID.ToString()))
                {
                    phr.MarkAsDeleted();
                    phr.Save();
                }

                // delete PatientHealthRecordLine
                if (CurrentNursingDiagnosaTransDT != null)
                {
                    CurrentNursingDiagnosaTransDT.MarkAsDeleted();
                    CurrentNursingDiagnosaTransDT.Save();
                }
                trans.Complete();
            }
        }
        public override bool OnGetStatusMenuEdit()
        {
            return CurrentNursingDiagnosaTransDT.CreateByUserID == AppSession.UserLogin.UserID;
        }
        public override bool OnGetStatusMenuDelete()
        {
            return CurrentNursingDiagnosaTransDT.CreateByUserID == AppSession.UserLogin.UserID;
        }
    }
}
