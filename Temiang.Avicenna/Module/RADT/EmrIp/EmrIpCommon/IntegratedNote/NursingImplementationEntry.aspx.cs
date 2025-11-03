using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class NursingImplementationEntry : BasePageDialogEntry
    {
        private AppAutoNumberLast _autoNumberND, _autoNumberPHR;

        private string GetNewPHRTransactionNo()
        {
            _autoNumberPHR = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.PatientHealthRecord);
            return _autoNumberPHR.LastCompleteNumber;
        }
        public string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }        
        public string RegistrationInfoMedicID
        {
            get
            {
                if (!IsPostBack)
                {
                    hdnRegistrationInfoMedicID.Value = Request.QueryString["rimid"];
                }
                return hdnRegistrationInfoMedicID.Value;
            }
            set
            {
                hdnRegistrationInfoMedicID.Value = value;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //List<string> keys = Request.Form.AllKeys.Where(key => key.Contains("referAnswerCtl")).ToList();
            //foreach (string key in keys)
            //{
            //    AddReferAnswerCtl();
            //    break; // hanya buat 1 ctl
            //}
            
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {

            }
            else
            {

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.EditVisible = false;
            ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Nursing notes of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                var hd = new NursingTransHDCollection();
                hd.Query.Where(hd.Query.RegistrationNo == RegistrationNo);
                hd.LoadAll();
                if (hd.Count > 0)
                {
                    NursingTransNo = hd[0].TransactionNo;
                }
                else {
                    NursingTransNo = string.Empty;
                }

                NewOrEditedPhrLine = null;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {

        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            NewOrEditedNursingImplementations = null;
            IdImplementationDeleted = null;

            // grid implementasi
            gridListImplementasi.Columns[0].Visible = (newVal != AppEnum.DataMode.Read);
            gridListImplementasi.Columns[gridListImplementasi.Columns.Count - 1].Visible = (newVal != AppEnum.DataMode.Read);
            gridListImplementasi.MasterTableView.CommandItemDisplay = (newVal != AppEnum.DataMode.Read)
                                                                             ? GridCommandItemDisplay.Top
                                                                             : GridCommandItemDisplay.None;
            //gridListImplementasi
            gridListImplementasi.Rebind();
        }
        protected override void OnMenuNewClick()
        {
            
        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var hd = SetHeader();
            SetImplementation();
            SaveImplementation();
        }

        private NursingTransHD SetHeader()
        {
            NursingTransHD hd;
            var hdColl = new NursingTransHDCollection();
            hdColl.Query.Where(hdColl.Query.TransactionNo == NursingTransNo,
                    hdColl.Query.RegistrationNo == RegistrationNo);
            hdColl.LoadAll();

            if (hdColl.Count == 0)
            {
                //hd = new NursingTransHD();
                hd = hdColl.AddNew();
                //hd.AddNew();
                PopulateNewNo(true);


                hd.TransactionNo = NursingTransNo;
                hd.NursingTransDateTime = DateTime.Now;// txtNursingTransDateTime.SelectedDate;
                hd.RegistrationNo = RegistrationNo;
                //Last Update Status

                hd.CreateByUserID = AppSession.UserLogin.UserID;
                hd.CreateDateTime = DateTime.Now;
                hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hd.LastUpdateDateTime = DateTime.Now;
            }
            else
            {
                hd = hdColl[0];

                hd.LastUpdateByUserID = AppSession.UserLogin.UserID;
                hd.LastUpdateDateTime = DateTime.Now;


            }
            hdColl.Save();
            return hd;
        }

        private void PopulateNewNo(bool TobeSave)
        {
            _autoNumberND = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.NursingCareNo);
            NursingTransNo = _autoNumberND.LastCompleteNumber;
            if (TobeSave)
            {
                // save autonumber immediately to decrease time gap between create and save
                _autoNumberND.Save();
            }
        }

        private void SetImplementation()
        {
            // tersimpan di session NursingImplementations
            // update transno jika belum ada
            foreach (var x in NewOrEditedNursingImplementations)
            {
                if (string.IsNullOrEmpty(x.TransactionNo)) x.TransactionNo = NursingTransNo;
            }
        }

        private void SaveImplementation()
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                var phrColl = new PatientHealthRecordCollection();

                //var newPHRList = NewOrEditedPhrLine.Where(x => x.TransactionNo == string.Empty)
                //    .Select(x => x.QuestionGroupID).Distinct();
                var newPHRList = NewOrEditedPhrLine.Where(x => x.TransactionNo == string.Empty && x.QuestionFormID != string.Empty);
                if(newPHRList.Count() > 0)
                {
                    foreach (var groupIDDiag in newPHRList.Select(x => x.QuestionFormID).Distinct()) {
                        var phr = phrColl.AddNew();

                        phr.TransactionNo = GetNewPHRTransactionNo();
                        _autoNumberPHR.Save();
                        phr.RegistrationNo = RegistrationNo;
                        phr.QuestionFormID = string.Empty;

                        phr.EmployeeID = AppSession.UserLogin.UserID;
                        phr.IsComplete = true;
                        phr.ReferenceNo = string.Empty;
                        phr.ExaminerID = AppSession.UserLogin.UserID;
                        phr.ServiceUnitID = string.Empty;

                        //Last Update Status
                        if (phr.es.IsAdded || phr.es.IsModified)
                        {
                            phr.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            phr.LastUpdateDateTime = DateTime.Now;
                        }
                        if (phr.es.IsAdded)
                        {
                            phr.CreateByUserID = AppSession.UserLogin.UserID;
                            phr.CreateDateTime = DateTime.Now;
                        }

                        var phrlItems = newPHRList.Where(x => x.QuestionGroupID == groupIDDiag);
                        foreach (var phrl in phrlItems)
                        {
                            phrl.TransactionNo = phr.TransactionNo;
                            phrl.QuestionFormID = phr.QuestionFormID;
                            phrl.QuestionGroupID = string.Empty;

                            // Set PHR Date
                            var phrDate = phrl.LastUpdateDateTime.Value;
                            phr.RecordDate = phrDate;
                            phr.RecordTime = phrDate.ToString("HH:mm");
                        }

                        var impList = NewOrEditedNursingImplementations.Where(x => x.ID == System.Convert.ToInt64(groupIDDiag));
                        foreach (var imp in impList)
                        {
                            imp.ReferenceToPhrNo = phr.TransactionNo;
                        }
                    }
                }

                newPHRList = NewOrEditedPhrLine.Where(x => x.TransactionNo == string.Empty && x.QuestionFormID == string.Empty && x.QuestionGroupID != string.Empty);
                if (newPHRList.Count() > 0)
                {
                    foreach (var groupTmpIDDiag in newPHRList.Select(x => x.QuestionGroupID).Distinct())
                    {
                        var phr = phrColl.AddNew();

                        phr.TransactionNo = GetNewPHRTransactionNo();
                        _autoNumberPHR.Save();
                        phr.RegistrationNo = RegistrationNo;
                        phr.QuestionFormID = string.Empty;
                        phr.EmployeeID = AppSession.UserLogin.UserID;
                        phr.IsComplete = true;
                        phr.ReferenceNo = string.Empty;
                        phr.ExaminerID = AppSession.UserLogin.UserID;
                        phr.ServiceUnitID = string.Empty;

                        //Last Update Status
                        if (phr.es.IsAdded || phr.es.IsModified)
                        {
                            phr.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            phr.LastUpdateDateTime = DateTime.Now;
                        }
                        if (phr.es.IsAdded)
                        {
                            phr.CreateByUserID = AppSession.UserLogin.UserID;
                            phr.CreateDateTime = DateTime.Now;
                        }

                        var phrlItems = newPHRList.Where(x => x.QuestionGroupID == groupTmpIDDiag);
                        foreach (var phrl in phrlItems)
                        {
                            phrl.TransactionNo = phr.TransactionNo;
                            phrl.QuestionFormID = phr.QuestionFormID;
                            phrl.QuestionGroupID = string.Empty;

                            // Set PHR Date
                            var phrDate = phrl.LastUpdateDateTime.Value;
                            phr.RecordDate = phrDate;
                            phr.RecordTime = phrDate.ToString("HH:mm");
                        }

                        var impList = NewOrEditedNursingImplementations.Where(x => x.TmpNursingDiagnosaID == groupTmpIDDiag);
                        foreach (var imp in impList)
                        {
                            imp.ReferenceToPhrNo = phr.TransactionNo;
                        }
                    }
                }

                // Update RecordDate di PHR
                var phrLines = NewOrEditedPhrLine;
                var trno = string.Empty;
                foreach (PatientHealthRecordLine line in phrLines)
                {
                    if (trno != line.TransactionNo)
                    {
                        trno = line.TransactionNo;
                        var isExist = false;
                        foreach (PatientHealthRecord phr in phrColl)
                        {
                            if (trno == phr.TransactionNo)
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            var phrUpd = new PatientHealthRecord();
                            if (phrUpd.LoadByPrimaryKey(trno, RegistrationNo, string.Empty))
                            {
                                var phrDate = line.LastUpdateDateTime.Value;
                                phrUpd.RecordDate = phrDate;
                                phrUpd.RecordTime = phrDate.ToString("HH:mm");
                                phrUpd.Save();
                            }
                        }
                    }
                }

                NewOrEditedNursingImplementations.Save();
                phrColl.Save();
                NewOrEditedPhrLine.Save();

                foreach (var phrNo in NewOrEditedPhrLine.Select(x => x.TransactionNo).Distinct())
                {
                    Question.ImportFromAssessmentForm(
                        RegistrationNo, phrNo, NursingTransNo, AppSession.UserLogin.UserID);
                }
                //Commit if success, Rollback if failed
                trans.Complete();

                NewOrEditedPhrLine = null;
            }
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Implementasi
        public DataTable ImplementationCountPerIntervention
        {
            get
            {
                if (ViewState["ImplementationCountPerIntervention" + RegistrationNo] != null)
                {
                    return (DataTable)ViewState["ImplementationCountPerIntervention" + RegistrationNo];
                }

                return null;
            }
            set
            {
                ViewState["ImplementationCountPerIntervention" + RegistrationNo] = value;
            }
        }

        public string SelectedImplementationNIC
        {
            get
            {
                if (Session["SelectedImplementationNIC" + RegistrationNo] != null)
                {
                    return Session["SelectedImplementationNIC" + RegistrationNo].ToString();
                }

                return string.Empty;
            }
            set
            {
                Session["SelectedImplementationNIC" + RegistrationNo] = value;
            }
        }

        public string NursingTransNo
        {
            get
            {
                if (Session["NursingTransNo" + RegistrationNo] != null)
                {
                    return Session["NursingTransNo" + RegistrationNo].ToString();
                }

                return string.Empty;
            }
            set
            {
                Session["NursingTransNo" + RegistrationNo] = value;
            }
        }

        private NursingDiagnosaTransDTCollection NewOrEditedNursingImplementations
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["collNursingImplementation" + RegistrationNo];
                if (obj != null)
                {
                    return ((NursingDiagnosaTransDTCollection)(obj));
                }
                //}

                //var coll = NursingDiagnosaTransDT.Implementation(NursingTransNo, 500);
                var coll = new NursingDiagnosaTransDTCollection();
                Session["collNursingImplementation" + RegistrationNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "collNursingImplementation" + RegistrationNo;
                Session[sessionName] = value;
            }
        }

        private PatientHealthRecordLineCollection NewOrEditedPhrLine
        {
            get
            {
                object obj = Session["collNewOrEditedPhrLine" + RegistrationNo];
                if (obj != null)
                {
                    return ((PatientHealthRecordLineCollection)(obj));
                }

                var coll = new PatientHealthRecordLineCollection();
                coll.Query.Select(coll.Query, "<GETDATE() as refToPatientHealthRecord_RecordDate>");
                Session["collNewOrEditedPhrLine" + RegistrationNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "collNewOrEditedPhrLine" + RegistrationNo;
                Session[sessionName] = value;
            }
        }

        private List<long> IdImplementationDeleted
        {
            get
            {
                //if (IsPostBack)
                //{
                object obj = Session["listIdImplementationDeleted" + RegistrationNo];
                if (obj != null)
                {
                    return ((List<long>)(obj));
                }
                //}

                //var coll = NursingDiagnosaTransDT.Implementation(NursingTransNo, 500);
                var coll = new List<long>();
                Session["listIdImplementationDeleted" + RegistrationNo] = coll;
                return coll;
            }
            set
            {
                string sessionName = "listIdImplementationDeleted" + RegistrationNo;
                Session[sessionName] = value;
            }
        }

        private DataTable NursingImplementasi(bool OpenOnly)
        {
            NursingDiagnosaQuery query = new NursingDiagnosaQuery("a");
            NursingDiagnosaQuery prDiag = new NursingDiagnosaQuery("b");
            NursingDiagnosaTransDTQuery dt = new NursingDiagnosaTransDTQuery("c");
            NursingDiagnosaTransDTQuery dtDiag = new NursingDiagnosaTransDTQuery("d");

            query.es.Distinct = true;

            query.InnerJoin(prDiag).On(query.NursingDiagnosaParentID == prDiag.NursingDiagnosaID
                & query.SRNursingDiagnosaLevel == "30");
            query.InnerJoin(dtDiag).On(prDiag.NursingDiagnosaID == dtDiag.NursingDiagnosaID
                & dtDiag.TransactionNo == NursingTransNo);
            query.InnerJoin(dt).On(query.NursingDiagnosaID == dt.NursingDiagnosaID
                & dt.TransactionNo == NursingTransNo);

            if (OpenOnly)
            {
                query.Where("<ISNULL(c.SRNursingCarePlanning,'') <> '01'>", "<ISNULL(d.SRNursingCarePlanning,'') <> '01'>");
            }

            query.Select(
                query,
                prDiag.NursingDiagnosaID.As("DiagID"),
                prDiag.NursingDiagnosaName.As("DiagName"),
                dt.ID,
                dt.NursingDiagnosaID.As("TransNursingDiagnosaID"),
                "<ISNULL(c.NursingDiagnosaName, a.NursingDiagnosaName) NursingDiagnosaNameEdited>",
                dt.Priority,
                dt.EvalPeriod,
                dt.PeriodConversionInHour,
                dt.Skala,
                dt.Target,
                dt.Evaluasi,
                dt.SRNursingCarePlanning, dtDiag.Priority.As("DiagnosaPriority")
                )
                .OrderBy(dtDiag.Priority.Ascending);

            var dttbl = query.LoadDataTable();
            return dttbl;
        }

        protected void gridListImplementasiDiagnosa_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var dt = NursingImplementasi(true);
            // tambah row ALL
            var nRow = dt.NewRow();
            nRow["ID"] = 0;
            nRow["NursingDiagnosaID"] = "";
            nRow["NursingDiagnosaNameEdited"] = "Kegiatan Rutin";
            nRow["DiagID"] = "";
            nRow["DiagName"] = "";
            nRow["SRNursingCarePlanning"] = "";

            dt.Rows.InsertAt(nRow, 0);

            //var dr = dt.AsEnumerable().Where(x => x.Field<string>("SRNursingCarePlanning") != "01");
            ImplementationCountPerIntervention = (new NursingDiagnosaTransDTCollection()).ImplementationCountPerIntervention(NursingTransNo);

            ((RadGrid)source).DataSource = dt;
        }

        protected void gridListImplementasiDiagnosa_PreRender(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedImplementationNIC))
                foreach (GridDataItem item in gridListImplementasiDiagnosa.MasterTableView.Items)
                {
                    if (item.GetDataKeyValue("NursingDiagnosaID").ToString() == SelectedImplementationNIC)
                    {
                        item.Selected = true;
                    }
                }
        }

        protected void gridListImplementasiDiagnosa_ItemDataBound(object source, GridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case (GridItemType.AlternatingItem):
                case (GridItemType.Item):
                    {
                        GridDataItem i = (GridDataItem)e.Item;
                        var IDNIC = i.GetDataKeyValue("NursingDiagnosaID").ToString();

                        var ds = ImplementationCountPerIntervention.AsEnumerable()
                            .Where(x => x.Field<string>("NursingDiagnosaID") == IDNIC);
                        if ((int)ds.First()["cID31"] == 0)
                        {
                            e.Item.ForeColor = System.Drawing.Color.Red;
                            e.Item.Font.Bold = true;
                        }
                        break;
                    }
            }
        }

        protected void gridListImplementasiDiagnosa_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem x = (GridDataItem)e.Item;
            SelectedImplementationNIC = x.GetDataKeyValue("NursingDiagnosaID").ToString();
            gridListImplementasi.Columns[3].HeaderText = "Implementation of " + x["NursingDiagnosaNameEdited"].Text;
            lblInterventionName.Text = x["NursingDiagnosaNameEdited"].Text;
            gridListImplementasi.Rebind();
        }

        protected string GetFullImplementationNameFormatted(string nursingDiagnosaName,
            string S, string O, string A, string P, object info5, object submitBy, object receiveBy)
        {

            if (Equals(nursingDiagnosaName, "S B A R"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "B: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "R: " + P);
            }

            if (Equals(nursingDiagnosaName, "S O A P"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "O: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + P );
            }

            if (Equals(nursingDiagnosaName, "ADIME"))
            {
                return (
                    "A: " + S + System.Environment.NewLine +
                    "D: " + O + System.Environment.NewLine +
                    "I: " + A + System.Environment.NewLine +
                    "M: " + P + System.Environment.NewLine +
                    "E: " +  (info5?? string.Empty) );
            }

            if (Equals(nursingDiagnosaName, "Handover Patient"))
            {
                return (
                    "S: " + S + System.Environment.NewLine +
                    "O: " + O + System.Environment.NewLine +
                    "A: " + A + System.Environment.NewLine +
                    "P: " + P+ System.Environment.NewLine +
                    "Submit By: "+ (submitBy?? string.Empty) + System.Environment.NewLine +
                    "Receive By: " + (receiveBy?? string.Empty)
                    );

            }
            return nursingDiagnosaName;
        }
        public static string ParsePhrlRespond(IEnumerable<PatientHealthRecordLine> phrls, QuestionCollection questions)
        {
            if (phrls.Count() == 0) return string.Empty;

            var respond = string.Empty;

            foreach (var phrl in phrls)
            {
                var quest = questions.Where(q => q.QuestionID == phrl.QuestionID).FirstOrDefault();
                if (quest == null)
                {
                    quest = new Question();
                    quest.LoadByPrimaryKey(phrl.QuestionID);
                    questions.AttachEntity(quest);
                }

                var ass = string.Empty;
                if (quest != null)
                {
                    var Ret = NursingDiagnosaTransDT.GetAssessmentValueList(
                        quest.SRAnswerType, phrl.QuestionAnswerText + " " + phrl.QuestionAnswerText2,
                        phrl.QuestionAnswerNum ?? 0, quest.QuestionText,
                        quest.AnswerDecimalDigit ?? 0, quest.AnswerSuffix);
                    foreach (var r in Ret)
                    {
                        ass += ((ass.Length == 0) ? "" : ", ") + r.AssessmentName;
                    }
                }
                respond = respond + (respond.Length > 0 ? ", " : "") + ass;
            }
            return respond;
        }

        public static string parsePhrlRespond(IEnumerable<PatientHealthRecordLine> phrls)
        {
            if (phrls.Count() == 0) return string.Empty;

            var respond = string.Empty;

            //var phrColl = new PatientHealthRecordCollection();
            //phrColl.Query.Where(phrColl.Query.TransactionNo == phrls.First().TransactionNo,
            //    phrColl.Query.RegistrationNo == phrls.First().RegistrationNo);
            //if (phrColl.LoadAll()) {
            //    var phr = phrColl.First();
                foreach (var phrl in phrls)
                {
                    var Ass = string.Empty;
                    var q = new Question();
                    if (q.LoadByPrimaryKey(phrl.QuestionID))
                    {
                    var Ret = NursingDiagnosaTransDT.GetAssessmentValueList(
                        q.SRAnswerType, phrl.QuestionAnswerText + " " + phrl.QuestionAnswerText2,
                        phrl.QuestionAnswerNum ?? 0, q.QuestionText,
                        q.AnswerDecimalDigit ?? 0, q.AnswerSuffix);
                        foreach (var r in Ret)
                        {
                            Ass += ((Ass.Length == 0) ? "" : ", ") + r.AssessmentName;
                        }
                    }
                    respond = respond + (respond.Length > 0 ? ", " : "") + Ass;
                }
            //}
            return respond;
        }
        public static string parsePhrlRespond_(IEnumerable<PatientHealthRecordLine> phrls) {
            var respond = string.Empty;
            foreach (var phrl in phrls)
            {
                var resp = string.Empty;
                var q = new Question();
                if (q.LoadByPrimaryKey(phrl.QuestionID))
                {
                    if (phrl.QuestionAnswerNum.HasValue)
                    {
                        resp = phrl.QuestionAnswerPrefix + " " + phrl.QuestionAnswerNum.Value.ToString("##0.##") + " " + phrl.QuestionAnswerSuffix;
                    }
                    else if (!string.IsNullOrEmpty(phrl.QuestionAnswerSelectionLineID))
                    {
                        var qasl = new QuestionAnswerSelectionLine();
                        qasl.Query.Where(qasl.Query.QuestionAnswerSelectionID == q.QuestionAnswerSelectionID,
                            qasl.Query.QuestionAnswerSelectionLineID == phrl.QuestionAnswerSelectionLineID);
                        if (qasl.Load(qasl.Query))
                        {
                            resp = qasl.QuestionAnswerSelectionLineText;
                        }
                    }
                    else {
                        resp = phrl.QuestionAnswerText;
                    }
                }
                resp = q.QuestionText + " " + resp;

                respond = respond + (respond.Length > 0 ? ", " : "") + resp;
            }

            return respond;
        }
        protected void gridListImplementasi_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //var ds = NursingImplementations/*.Where(x => x.NursingDiagnosaParentID == SelectedImplementationNIC)*/
            //    .OrderByDescending(x => x.ExecuteDateTime);
            //gridListImplementasi.DataSource = ds;

            //gridListImplementasi.DataSource = NursingDiagnosaTransDT.Implementation(NursingTransNo, 0);
            var prColl = new NursingDiagnosaTransDTCollection();
            var regnos = Common.NursingCare.GetRelatedRegistrationsByNsTransNo(NursingTransNo);
            var ni = prColl.ImplementationByPage(regnos, false,
                    ((gridListImplementasi.CurrentPageIndex * gridListImplementasi.PageSize) + 1),
                    ((gridListImplementasi.CurrentPageIndex + 1) * gridListImplementasi.PageSize));

            // yang delete buang saja dari ni
            if (IdImplementationDeleted.Count > 0)
            {
                var nid = ni.AsEnumerable().Where(x => IdImplementationDeleted.Contains(x.Field<long>("ID")));
                foreach (var n1 in nid)
                {
                    n1.Delete();
                }
                ni.AcceptChanges();
            }

            var nRespond = ni.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.Field<string>("ReferenceToPhrNo")));
            foreach (var n in nRespond)
            {
                var phrlColl = new PatientHealthRecordLineCollection();
                phrlColl.Query.Where(phrlColl.Query.TransactionNo == n["ReferenceToPhrNo"], 
                    phrlColl.Query.RegistrationNo == n["RegistrationNo"]);
                if (phrlColl.LoadAll()) { 
                    n["Respond2"] = parsePhrlRespond(phrlColl);
                }
            }

            // yang sudah diedit di session diupdate ke ni
            var nis = ni.AsEnumerable().Where(x =>
                NewOrEditedNursingImplementations.Select(y => y.ID).Contains(x.Field<long>("ID")));
            foreach (var n1 in nis)
            {
                var n2 = NewOrEditedNursingImplementations.Where(x => x.ID == n1.Field<long>("ID")).First();
                n1["NursingDiagnosaName"] = n2.NursingDiagnosaName;
                n1["Respond"] = n2.Respond;
                n1["ExecuteDateTime"] = n2.ExecuteDateTime;
                n1["S"] = n2.S;
                n1["O"] = n2.O;
                n1["A"] = n2.A;
                n1["P"] = n2.P;
                n1["Info5"] = n2.Info5;
                n1["SubmitBy"] = n2.SubmitBy;
                n1["ReceiveBy"] = n2.ReceiveBy;
                //n1.AcceptChanges();

                var phrl = NewOrEditedPhrLine.Where(x => x.QuestionFormID == n2.ID.ToString());
                if (phrl.Count() > 0) {
                    n1["Respond2"] = parsePhrlRespond(phrl);
                }
            }
            // yang baru ditambahin saja ke ni
            foreach (var n1 in NewOrEditedNursingImplementations.Where(x => !x.ID.HasValue))
            {
                var row = ni.NewRow();
                row["NursingDiagnosaName"] = n1.NursingDiagnosaName;
                row["Respond"] = n1.Respond;
                row["ExecuteDateTime"] = n1.ExecuteDateTime;
                row["S"] = n1.S;
                row["O"] = n1.O;
                row["A"] = n1.A;
                row["P"] = n1.P;
                row["Info5"] = n1.Info5;
                row["CreateByUserID"] = AppSession.UserLogin.UserID;
                row["RefToUserName"] = AppSession.UserLogin.UserName;
                row["TmpNursingDiagnosaID"] = n1.TmpNursingDiagnosaID;
                row["NursingDiagnosaParentID"] = n1.NursingDiagnosaParentID;
                row["SubmitBy"] = n1.SubmitBy;
                row["ReceiveBy"] = n1.ReceiveBy;
                ni.Rows.InsertAt(row, 0);

                var phrl = NewOrEditedPhrLine.Where(x => x.QuestionGroupID == n1.TmpNursingDiagnosaID);
                if (phrl.Count() > 0)
                {
                    row["Respond2"] = parsePhrlRespond(phrl);
                }
            }

            ni.AcceptChanges();

            gridListImplementasi.VirtualItemCount = prColl.ImplementationCount(regnos, false);

            gridListImplementasi.DataSource = ni;
        }

        protected void gridListImplementasi_ItemDataBound(object source, GridItemEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.Edit)
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = e.Item as GridDataItem;
                    // find user create 
                    var UserCreate = DataBinder.Eval(e.Item.DataItem, "CreateByUserID");
                    var IsDeleted = DataBinder.Eval(e.Item.DataItem, "IsDeleted") is DBNull ? false : (bool)DataBinder.Eval(e.Item.DataItem, "IsDeleted");
                    if (string.Equals(UserCreate, AppSession.UserLogin.UserID) && !IsDeleted)
                    {
                        // allow edit and delete

                    }
                    else
                    {
                        // edit not allowed
                        ImageButton lnke = item["EditColumn"].Controls[0] as ImageButton;
                        item["EditColumn"].Controls.Remove(lnke);

                        ImageButton lnkd = item["DeleteColumn"].Controls[0] as ImageButton;
                        item["DeleteColumn"].Controls.Remove(lnkd);

                        if (IsDeleted) {
                            item.Font.Strikeout = true;
                            item.ForeColor = System.Drawing.Color.Gray;
                        }
                    }
                }
            }
        }

        protected void gridListImplementasi_InsertCommand(object source, GridCommandEventArgs e)
        {
            SetEntityValue(e);
            //Stay in insert mode
            e.Canceled = false;
            gridListImplementasi.Rebind();
            gridListImplementasi.CurrentPageIndex = 0;
        }

        protected void gridListImplementasi_UpdateCommand(object source, GridCommandEventArgs e)
        {
            SetEntityValue(e);
            //Stay in insert mode
            e.Canceled = false;
            gridListImplementasi.Rebind();
        }

        protected void gridListImplementasi_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            try
            {
                String SID = item[NursingDiagnosaTransDTMetadata.ColumnNames.ID].Text;
                if (Equals(SID, "&nbsp;")) throw new Exception("No ID");

                // hapus juga di session kalau ada
                var entity2Coll = NewOrEditedNursingImplementations.Where(x => x.ID == Int64.Parse(SID));
                if (entity2Coll.Count() > 0)
                {
                    foreach (var ent in entity2Coll)
                    {
                        ent.MarkAsDeleted();
                    }
                }
                else
                {
                    NursingDiagnosaTransDT entity = new NursingDiagnosaTransDT();
                    if (entity.LoadByPrimaryKey(Int64.Parse(SID)))
                    {
                        NewOrEditedNursingImplementations.AttachEntity(entity);
                        entity.MarkAsDeleted();
                    }
                }
                IdImplementationDeleted.Add(Int64.Parse(SID));
            }
            catch (Exception ex)
            {
                // find by TmpNursingDiagnosaID
                String TmpNursingDiagnosaID = item[NursingDiagnosaTransDTMetadata.ColumnNames.TmpNursingDiagnosaID].Text;
                if (!string.IsNullOrEmpty(TmpNursingDiagnosaID))
                {
                    NursingDiagnosaTransDT entity = FindNursingDiagnosaTransDTByTmpNursingDiagnosaID(TmpNursingDiagnosaID);
                    if (entity != null)
                        entity.MarkAsDeleted();
                }
            }

            gridListImplementasi.Rebind();
        }

        private NursingDiagnosaTransDT FindNursingDiagnosaTransDTByTmpNursingDiagnosaID(String TmpNursingDiagnosaID)
        {
            NursingDiagnosaTransDTCollection coll = NewOrEditedNursingImplementations;
            return coll.Where(
                rec => rec.TmpNursingDiagnosaID.Equals(TmpNursingDiagnosaID) &&
                rec.TransactionNo == NursingTransNo).First();
        }

        private void SetEntityValue(GridCommandEventArgs e)
        {
            NursingCareStandardTransDTDetail userControl = (NursingCareStandardTransDTDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                if (!string.IsNullOrEmpty(userControl.NursingDiagnosaID) ||
                    !string.IsNullOrEmpty(userControl.NursingDiagnosaCustomText))
                {
                    NursingDiagnosaTransDT entity;

                    var id = userControl.RecordID;
                    var tmpNursingDiagnosaID = userControl.TmpNursingDiagnosaID;
                    if (!string.IsNullOrEmpty(id))
                    {
                        // edit yang sudah pernah disimpan di database, 
                        // untuk mengurangi beban memory karena session menyimpan data terlalu banyak maka
                        // hanya data baru atau data yang diedit saya yang di-store ke session, gitu ya....!!

                        // step 1, cari di session NewOrEditedNursingImplementations ada data yang dimaksud atau tidak
                        // jika ada ya sudah pakai yang itu
                        var entityColl = NewOrEditedNursingImplementations.Where(x => x.ID == System.Convert.ToInt64(id));
                        if (entityColl.Count() > 0)
                        {
                            entity = entityColl.First();
                        }
                        else
                        {
                            // jika belum ada di session berarti belum diedit, ambil dari database kemudian store ke session
                            entity = new NursingDiagnosaTransDT();
                            if (!entity.LoadByPrimaryKey(System.Convert.ToInt64(id)))
                            {
                                throw new Exception("Data not found");
                            }
                            NewOrEditedNursingImplementations.AttachEntity(entity);
                        }

                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        entity.LastUpdateDateTime = DateTime.Now;
                    }
                    else
                    {
                        // ini edit yang baru tersimpan di session saja
                        var lEntity = NewOrEditedNursingImplementations.Where(x => x.TmpNursingDiagnosaID == tmpNursingDiagnosaID);
                        if (lEntity.Count() > 0)
                        {
                            entity = lEntity.First();
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;
                        }
                        else
                        {
                            // ini data baru coy
                            entity = NewOrEditedNursingImplementations.AddNew();

                            entity.TmpNursingDiagnosaID = tmpNursingDiagnosaID;

                            entity.CreateByUserID = AppSession.UserLogin.UserID;
                            entity.CreateDateTime = DateTime.Now;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.RefToUserName = AppSession.UserLogin.UserName;
                            entity.LastUpdateDateTime = DateTime.Now;
                        }
                    }

                    entity.TransactionNo = NursingTransNo;
                    entity.NursingDiagnosaID = userControl.NursingDiagnosaID;
                    entity.NursingDiagnosaName = userControl.NursingDiagnosaCustomText;
                    entity.SRNursingDiagnosaLevel = "31";

                    var NursingDiagnosaParentID = string.Empty;
                    if (gridListImplementasiDiagnosa.SelectedItems.Count > 0)
                    {
                        GridDataItem item = (GridDataItem)gridListImplementasiDiagnosa.MasterTableView.Items[gridListImplementasiDiagnosa.SelectedItems[0].ItemIndex];

                        NursingDiagnosaParentID = item.GetDataKeyValue(NursingDiagnosaTransDTMetadata.ColumnNames.NursingDiagnosaID).ToString();

                        // ini error kalau kegiatan rutin, gak ada ID-nya bro
                        if (item.GetDataKeyValue("ID") != null)
                        {
                            var idr = System.Convert.ToInt64(item.GetDataKeyValue("ID"));
                            if (idr == 0) { entity.ParentID = null; }
                            else { entity.ParentID = idr; }
                        }
                    }
                    entity.NursingDiagnosaParentID = NursingDiagnosaParentID;
                    entity.Priority = 0;
                    entity.EvalPeriod = 0;
                    entity.PeriodConversionInHour = 24;
                    entity.Skala = 1;
                    entity.Target = 0;
                    entity.Evaluasi = 1;
                    entity.Respond = userControl.Respond;
                    entity.Reexamine = false;
                    entity.ExecuteDateTime = userControl.ImplementationDateTime.Value;

                    entity.S = userControl.S;
                    entity.O = userControl.O;
                    entity.A = userControl.A;
                    entity.P = userControl.P;
                    entity.Info5 = userControl.Info5;
                    entity.PpaInstruction = userControl.PpaInstruction;
                    entity.SubmitBy = userControl.SubmitBy;
                    entity.ReceiveBy = userControl.ReceiveBy;

                    entity.TmpNursingDiagnosaParentID = string.Empty;

                    //if (entity.es.IsAdded)
                    //{
                    //    var tdl = NewOrEditedPhrLine.Where(x => x.TransactionNo == string.Empty && x.QuestionGroupID == entity.TmpNursingDiagnosaID);
                    //    foreach (var d in tdl) d.MarkAsDeleted();
                    //}
                    //else {
                    //    var tdl = NewOrEditedPhrLine.Where(x => x.TransactionNo == entity.ReferenceToPhrNo);
                    //    foreach (var d in tdl) d.MarkAsDeleted();
                    //}

                    var tid = userControl.TemplateID;
                    if (tid != "0" && tid != string.Empty) {
                        entity.TemplateID = System.Convert.ToInt32(tid);
                    }

                    userControl.UpdatePHRLine();
                }
                else
                {
                    // nothing to be processed
                }
            }
        }
        #endregion

        protected void gridListImplementasi_OnPreRender(object sender, EventArgs e)
        {
            //// IsEdit
            //if (!string.IsNullOrEmpty(Request.QueryString["rimid"]))
            //{
            //    GridDataItem dataItem = gridListImplementasi.MasterTableView.FindItemByKeyValue("ID", Request.QueryString["rimid"]) as GridDataItem;
                

            //    int pageIndex = dataItem.ItemIndex / gridListImplementasi.MasterTableView.PageSize + 1;
            //    int itemIndex = dataItem.ItemIndex % gridListImplementasi.MasterTableView.PageSize;

            //    gridListImplementasi.MasterTableView.AllowPaging = true;
            //    gridListImplementasi.MasterTableView.Rebind();

            //    GridPagerItem pageritem = gridListImplementasi.MasterTableView.GetItems(GridItemType.Pager)[0] as GridPagerItem;
            //    pageritem.FireCommandEvent("Page", pageIndex.ToString());

            //    dataItem = gridListImplementasi.MasterTableView.Items[itemIndex] as GridDataItem;
            //    dataItem.FireCommandEvent("ExpandCollapse", String.Empty);

            //    GridItem[] nestedViewItems = gridListImplementasi.MasterTableView.GetItems(GridItemType.NestedView);
            //    GridTableView newDetailTable = (nestedViewItems[itemIndex] as GridNestedViewItem).NestedTableViews[0] as GridTableView;
            //    newDetailTable.IsItemInserted = true;
            //    newDetailTable.Rebind();
            //}
        }
    }
}
