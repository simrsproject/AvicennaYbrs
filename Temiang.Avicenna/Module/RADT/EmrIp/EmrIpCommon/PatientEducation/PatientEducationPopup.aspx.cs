using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    [Obsolete("Sudah tidak dikembangkan, gunakan PatientEducationDetail.aspx untuk kondisi terakhir (Handono 230317)",true)]
    public partial class PatientEducationPopup : BasePageDialogEntry
    {
        public string EducationTypeDefault
        {
            get
            {
                if (!string.IsNullOrEmpty(PrescriptionNo))
                    return "RSP";

                return string.Empty;
            }
        }
        public string ReferenceType
        {
            get
            {
                if (!string.IsNullOrEmpty(PrescriptionNo))
                    return "PRESC";

                return string.Empty;
            }
        }
        private int _seqNo = -1;
        public int SeqNo
        {
            get
            {
                if (_seqNo == -1 && !string.IsNullOrEmpty(PrescriptionNo))
                {
                    // Selalu ambil dari PatientEducation spy uptodate jika ada user lain yg sudah entry
                    var edu = new PatientEducation();
                    edu.Query.Where(edu.Query.RegistrationNo == RegistrationNo, edu.Query.ReferenceNo == PrescriptionNo,
                        edu.Query.ReferenceType == ReferenceType);
                    edu.Query.es.Top = 1;
                    if (edu.Query.Load())
                    {
                        _seqNo = edu.SeqNo ?? 0;
                    }
                }
                else if (string.IsNullOrEmpty(Request.QueryString["seqno"]))
                {
                    _seqNo = Request.QueryString["seqno"].ToInt();
                }
                else
                    _seqNo = 0;

                return _seqNo;
            }
        }
        public string PrescriptionNo
        {
            get
            {
                return Request.QueryString["prescno"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            IsSingleRecordMode = true; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            if (!IsPostBack)
            {
                var patientID = PatientID;
                if (string.IsNullOrEmpty(patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    patientID = reg.PatientID;
                }
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(patientID))
                {
                    this.Title = "Patient Education of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                txtRegistrationNo.Text = RegistrationNo;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        #region override method
        public override void OnServerValidate(ValidateArgs args)
        {
            if (txtDuration.Value == 0)
            {
                args.IsCancel = true;
                args.MessageText = @"Duration not valid";
            }
        }
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var ent = new PatientEducation();
            if (ent.LoadByPrimaryKey(RegistrationNo, SeqNo))
            {
                txtSeqNo.Text = string.Format("{0:00000}", ent.SeqNo);
                txtEducationDateTime.SelectedDate = ent.EducationDateTime;
                ComboBox.PopulateWithOneRow(cboEducationByUserID, AppSession.UserLogin.UserID, Enums.EntityClassName.AppUser, "UserID", "UserName");
                ComboBox.PopulateWithOneStandardReference(cboSREducationEvaluation,
                    AppEnum.StandardReference.PatientEducationEvaluation.ToString(), ent.SRPatientEducationEvaluation);
                ComboBox.PopulateWithOneStandardReference(cboSREducationProblem,
                    AppEnum.StandardReference.PatientEducationProblem.ToString(), ent.SRPatientEducationProblem);
                ComboBox.PopulateWithOneStandardReference(cboSREducationMethod,
                    AppEnum.StandardReference.PatientEducationMethod.ToString(), ent.SRPatientEducationMethod);
                ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationRecipient,
                    AppEnum.StandardReference.PatientEducationRecipient.ToString(), ent.SRPatientEducationRecipient);

                txtMethodOther.Text = ent.MethodOther;
                txtRecipientName.Text = ent.RecipientName;
                txtDuration.Value = ent.Duration;
                txtEducationType.Text = ent.EducationType;
            }

            grdPatientEducation.DataSource = PatientEducationDataTable(RegistrationNo, txtSeqNo.Text.ToInt(), txtEducationType.Text, false);
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (newVal != AppEnum.DataMode.Read)
            {
                StandardReference.InitializeIncludeSpace(cboSREducationEvaluation, AppEnum.StandardReference.PatientEducationEvaluation);
                StandardReference.InitializeIncludeSpace(cboSREducationProblem, AppEnum.StandardReference.PatientEducationProblem);
                StandardReference.InitializeIncludeSpace(cboSREducationMethod, AppEnum.StandardReference.PatientEducationMethod);
                StandardReference.InitializeIncludeSpace(cboSRPatientEducationRecipient, AppEnum.StandardReference.PatientEducationRecipient);
            }

            var isEdited = newVal != AppEnum.DataMode.Read;
            grdPatientEducation.Columns[0].Display = isEdited; // Selected
            grdPatientEducation.Columns[1].Display = !isEdited; // IsSelected
            grdPatientEducation.Columns[3].Display = !isEdited; // Notes
            grdPatientEducation.Columns[4].Display = isEdited; // Notes Edit
        }
        protected override void OnMenuNewClick()
        {
            ClearEntry();
            txtSeqNo.Text = string.Format("{0:00000}", NewSeqNo());
            txtEducationDateTime.SelectedDate = DateTime.Now;
            ComboBox.PopulateWithOneRow(cboEducationByUserID, AppSession.UserLogin.UserID, Enums.EntityClassName.AppUser, "UserID", "UserName");
            if (!string.IsNullOrEmpty(EducationTypeDefault))
                txtEducationType.Text = EducationTypeDefault;

            grdPatientEducation.DataSource = PatientEducationDataTable(RegistrationNo, txtSeqNo.Text.ToInt(), txtEducationType.Text, false);
        }

        private void ClearEntry()
        {
            txtSeqNo.Text = string.Empty;
            txtEducationDateTime.Clear();
            txtRecipientName.Text = string.Empty;
            txtMethodOther.Text = string.Empty;
            txtDuration.Value = 0;

            cboEducationByUserID.SelectedIndex = -1;
            cboEducationByUserID.Text = string.Empty;
            cboSREducationEvaluation.SelectedIndex = 0;
            cboSREducationProblem.SelectedIndex = 0;
            cboSREducationMethod.SelectedIndex = 0;
            cboSRPatientEducationRecipient.SelectedIndex = 0;

            grdPatientEducation.DataSource = PatientEducationDataTable(RegistrationNo, txtSeqNo.Text.ToInt(), txtEducationType.Text, false);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
        }

        private bool Save(ValidateArgs args)
        {
            var ent = new PatientEducation();
            if (string.IsNullOrEmpty(txtSeqNo.Text) || !ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.SeqNo = NewSeqNo();
            }
            ent.SRUserType = AppSession.UserLogin.SRUserType;
            ent.EducationType = txtEducationType.Text;
            ent.ReferenceType = ReferenceType;
            ent.EducationByUserID = cboEducationByUserID.SelectedValue;
            ent.EducationDateTime = txtEducationDateTime.SelectedDate;
            ent.SRPatientEducationEvaluation = cboSREducationEvaluation.SelectedValue;
            ent.SRPatientEducationMethod = cboSREducationMethod.SelectedValue;
            ent.SRPatientEducationProblem = cboSREducationProblem.SelectedValue;
            ent.SRPatientEducationRecipient = cboSRPatientEducationRecipient.SelectedValue;
            ent.RecipientName = txtRecipientName.Text;
            ent.MethodOther = txtMethodOther.Text;
            ent.Duration = txtDuration.Value.ToInt();

            if (!string.IsNullOrEmpty(PrescriptionNo))
            {
                // Update Prescription
                var presc = new TransPrescription();
                presc.LoadByPrimaryKey(PrescriptionNo);
                presc.PatientEducationSeqNo = ent.SeqNo;
                presc.Save();

                ent.ReferenceNo = PrescriptionNo;
            }

            ent.Save();

            SavePatientEducation();

            txtSeqNo.Text = string.Format("{0:00000}", ent.SeqNo);

            // Return value to parent page
            hdfReturnValue.Value = string.Format("{0} @{1}", AppSession.UserLogin.UserName, ent.EducationDateTime.Value.ToString(AppConstant.DisplayFormat.DateHourMinute));
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Format("oArg.value = document.getElementById('{0}').value;", hdfReturnValue.ClientID);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args);
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
            if (string.IsNullOrEmpty(txtSeqNo.Text))
                OnMenuNewClick();
            else
                grdPatientEducation.DataSource = PatientEducationDataTable(RegistrationNo, txtSeqNo.Text.ToInt(), txtEducationType.Text, false);
        }
        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var seqNo = txtSeqNo.Text.ToInt();

            var line = new PatientEducationLineCollection();
            line.Query.Where(line.Query.RegistrationNo == RegistrationNo, line.Query.SeqNo == seqNo);
            line.LoadAll();
            line.MarkAllAsDeleted();
            line.Save();

            var nmd = new PatientEducation();
            nmd.LoadByPrimaryKey(RegistrationNo, seqNo);
            nmd.MarkAsDeleted();
            nmd.Save();

            if (!string.IsNullOrEmpty(PrescriptionNo))
            {
                // Update Prescription
                var presc = new TransPrescription();
                presc.LoadByPrimaryKey(PrescriptionNo);
                presc.str.PatientEducationSeqNo = string.Empty;
                presc.Save();
            }

            ClearEntry();
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
            return !string.IsNullOrEmpty(txtSeqNo.Text);
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


        #region Entry
        private int NewSeqNo()
        {
            var qr = new PatientEducationQuery("a");
            var fb = new PatientEducation();
            qr.es.Top = 1;
            qr.Where(qr.RegistrationNo == RegistrationNo);
            qr.OrderBy(qr.SeqNo.Descending);

            if (fb.Load(qr))
            {
                return fb.SeqNo.ToInt() + 1;
            }
            return 1;
        }
        private void SavePatientEducation()
        {
            using (var trans = new esTransactionScope())
            {
                // PatientEducationLine
                var medColl = new PatientEducationLineCollection();
                if (DataModeCurrent != AppEnum.DataMode.Read)
                {
                    medColl.Query.Where(medColl.Query.RegistrationNo == RegistrationNo, medColl.Query.SeqNo == txtSeqNo.Text.ToInt());
                    medColl.LoadAll();
                    medColl.MarkAllAsDeleted();
                    medColl.Save();
                }

                medColl = new PatientEducationLineCollection();

                foreach (GridDataItem item in grdPatientEducation.MasterTableView.Items)
                {
                    var chkIsSelected = ((CheckBox)item.FindControl("chkIsSelected"));

                    if (chkIsSelected.Checked)
                    {
                        var txtNotes = ((RadTextBox)item.FindControl("txtNotes"));
                        var med = medColl.AddNew();
                        med.RegistrationNo = RegistrationNo;
                        med.SeqNo = txtSeqNo.Text.ToInt();
                        med.SRPatientEducation = item.GetDataKeyValue("ItemID").ToString();
                        med.EducationNotes = txtNotes.Text;
                    }
                }

                medColl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }



        private DataTable PatientEducationDataTable(string registrationNo, int seqNo, string referenceID, bool isJustSelected)
        {
            var que = new AppStandardReferenceItemQuery("sri");
            var qrFam = new PatientEducationLineQuery("a");

            if (isJustSelected)
                que.InnerJoin(qrFam)
                    .On(que.ItemID == qrFam.SRPatientEducation && qrFam.RegistrationNo == registrationNo && qrFam.SeqNo == seqNo);
            else
                que.LeftJoin(qrFam)
                .On(que.ItemID == qrFam.SRPatientEducation && qrFam.RegistrationNo == registrationNo && qrFam.SeqNo == seqNo);

            que.Where(que.StandardReferenceID == "PatientEducation");

            que.Where(que.ReferenceID.Like(string.Format("{0}%", referenceID)));
            que.OrderBy(que.LineNumber.Ascending);
            que.Select(que.ItemID, que.ItemName, qrFam.EducationNotes, "<CONVERT(BIT,CASE WHEN a.SRPatientEducation IS NULL THEN 0 ELSE 1 END) as IsSelected>");
            return que.LoadDataTable();
        }

        protected void grdPatientEducation_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.Read)
                return;

            if (e.Item is GridDataItem)
            {
                var dataItem = (GridDataItem)e.Item;
                var chkIsSelected = ((CheckBox)dataItem.FindControl("chkIsSelected"));
                chkIsSelected.Checked = ((CheckBox)(dataItem["IsSelected"].Controls[0])).Checked;

                var txtNotes = ((RadTextBox)dataItem.FindControl("txtNotes"));
                var notes = dataItem["EducationNotes"].Text;
                if (notes == "&nbsp;")
                    notes = string.Empty;
                txtNotes.Text = notes;

            }
        }

        protected void cboEducationByUserID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            // Jika tidak ditentukan tipe edukasinya maka ambil dari tipe user
            if (string.IsNullOrEmpty(EducationTypeDefault))
                PopulateEducationList(cboEducationByUserID.SelectedValue);
        }

        private void PopulateEducationList(string userID)
        {
            var user = new AppUser();
            if (user.LoadByPrimaryKey(userID))
            {
                //var sri = new AppStandardReferenceItem();
                //sri.LoadByPrimaryKey("UserType", user.SRUserType);
                txtEducationType.Text = user.SRUserType;
            }
            else
                txtEducationType.Text = "NRS";

            grdPatientEducation.DataSource = PatientEducationDataTable(RegistrationNo, txtSeqNo.Text.ToInt(), txtEducationType.Text, false);
        }

        #endregion
    }
}
