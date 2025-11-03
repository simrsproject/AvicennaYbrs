using System;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using Temiang.Dal.Core;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    /// <summary>
    /// Add, View, Edit, Delete, and Verif Patient Education 
    /// </summary>
    /// Create by: Handono
    /// Create date: 23 Maret
    /// Modified Hist:
    /// ==============
    /// 23-Maret-20 Handono
    /// Tambah fitur untuk Education Resep yang dipanggil dari page PrescriptionSalesDetail
    /// 
    /// ==============

    public partial class PatientEducationDetail : BasePageDetail
    {
        private string PrescriptionNo
        {
            get
            {
                return Request.QueryString["prescno"];
            }
        }

        private string EducationTypeDefault
        {
            get
            {
                if (!string.IsNullOrEmpty(PrescriptionNo))
                    return "RSP";

                return string.Empty;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            this.Title = "Patient Education";

            IsSingleRecordMode = true; //For popup windows
            IsMedicalRecordEntry = true; //Activate deadline edit & add

            // Program Fiture
            ToolBar.UnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBarMenuApproval.Text = "Verify";
            ToolBar.DeleteVisible = true;


            if (!IsPostBack)
            {
                
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            // Tidak di OnGetStatusMenuEdit karena OnGetStatusMenuEdit oleh framework digunakan juga
            // utk status disable tombol Approve yg disini dipakai untuk verif (Handono 230411)
            if (ToolBarMenuEdit.Enabled && DataModeCurrent == AppEnum.DataMode.Read)
                ToolBarMenuEdit.Enabled = !string.IsNullOrEmpty(txtSeqNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text);
        }

        #region override method

        // Tidak jadi menggunakan TTD proses verif nya
        //protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        //{
        //    base.RaisePostBackEvent(sourceControl, eventArgument);

        //    if (eventArgument == "verify")
        //    {
        //        var ent = new PatientEducation();
        //        if (ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
        //        {
        //            ent.VerifyByUserID = AppSession.UserLogin.UserID;
        //            ent.VerifyDateTime = DateTime.Now;

        //            if (!string.IsNullOrWhiteSpace(hdnVerificatorSign.Value))
        //            {
        //                var imgHelper = new ImageHelper();
        //                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnVerificatorSign.Value), new Size(332, 185));
        //                ent.VerifySign = imgHelper.ToByteArray(resized, ImageFormat.Png);
        //            }
        //            else
        //                ent.VerifySign = null;

        //            ent.Save();

        //            PopulateVerifyInfo(ent);
        //            RefreshMenuStatus();
        //        }
        //    }
        //}

        private void PopulateVerifyInfo(PatientEducation ent)
        {
            //// Verificator Sign
            //if (ent.VerifySign != null)
            //{
            //    var imgHelper = new ImageHelper();
            //    var val = (byte[])ent.VerifySign;
            //    imgVerificatorSign.DataValue = val;
            //    var mstream = new MemoryStream(val);
            //    Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
            //    hdnVerificatorSign.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
            //}
            //else
            //{
            //    imgVerificatorSign.DataValue = null;
            //    hdnVerificatorSign.Value = String.Empty;
            //}

            //if (!string.IsNullOrWhiteSpace(ent.VerifyByUserID))
            //    lblVerifyInfo.Text = string.Format("By: {0}, Time: {1}", AppUser.GetUserName(ent.VerifyByUserID),
            //        ent.VerifyDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute));
            //else
            //    lblVerifyInfo.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(ent.VerifyByUserID))
                txtVerifyDateTime.Text = ent.VerifyDateTime.Value.ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute);
            else
                txtVerifyDateTime.Text = string.Empty;
        }
        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var seqNo = 0;
            // Jika dipangggil dari PrescriptionSales page, ambil seqNo dari hist Edu nya
            if (!string.IsNullOrEmpty(PrescriptionNo))
            {
                var presc = new TransPrescription();
                presc.LoadByPrimaryKey(PrescriptionNo);
                seqNo = presc.PatientEducationSeqNo ?? 0;
            }
            else
                seqNo = Request.QueryString["sno"].ToInt();

            if (seqNo == 0)
                OnMenuNewClick();
            else
            {
                var ent = new PatientEducation();
                if (ent.LoadByPrimaryKey(RegistrationNo, seqNo))
                {
                    txtSeqNo.Text = string.Format("{0:00000}", ent.SeqNo);
                    txtEducationDateTime.SelectedDate = ent.EducationDateTime;
                    ComboBox.PopulateWithOneRow(cboEducationByUserID, ent.EducationByUserID,
                        Enums.EntityClassName.AppUser, "UserID", "UserName");
                    ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationEvaluation,
                        AppEnum.StandardReference.PatientEducationEvaluation.ToString(),
                        ent.SRPatientEducationEvaluation);
                    ComboBox.PopulateWithOneStandardReference(cboSREducationProblem,
                        AppEnum.StandardReference.PatientEducationProblem.ToString(), ent.SRPatientEducationProblem);
                    ComboBox.PopulateWithOneStandardReference(cboSREducationMethod,
                        AppEnum.StandardReference.PatientEducationMethod.ToString(), ent.SRPatientEducationMethod);
                    ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationRecipient,
                        AppEnum.StandardReference.PatientEducationRecipient.ToString(),
                        ent.SRPatientEducationRecipient);
                    ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationGoal,
                        AppEnum.StandardReference.PatientEducationGoal.ToString(), ent.SRPatientEducationGoal);

                    txtMethodOther.Text = ent.MethodOther;
                    txtRecipientName.Text = ent.RecipientName;
                    txtDuration.Value = ent.Duration;
                    txtEducationType.Text = ent.EducationType;
                    txtReferenceNo.Text = ent.ReferenceNo;
                    txtPatientEducationEvaluationOth.Text = ent.PatientEducationEvaluationOth;
                    txtPatientEducationGoalOth.Text = ent.PatientEducationGoalOth;
                    txtVerificator.Text = ent.Verificator;


                    //SIGN
                    var imgHelper = new ImageHelper();

                    // Patient
                    if (ent.FmSign != null)
                    {
                        var val = (byte[])ent.FmSign;
                        imgPatientSign.DataValue = val;
                        var mstream = new MemoryStream(val);
                        Telerik.Web.UI.ImageEditor.EditableImage img =
                            new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                        hdnPatientSign.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
                    }
                    else
                    {
                        imgPatientSign.DataValue = null;
                        hdnPatientSign.Value = String.Empty;
                    }

                    // Educator
                    if (ent.PsSign != null)
                    {
                        var val = (byte[])ent.PsSign;
                        imgEducator.DataValue = val;
                        var mstream = new MemoryStream(val);
                        Telerik.Web.UI.ImageEditor.EditableImage img =
                            new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                        hdnEducatorSign.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
                    }
                    else
                    {
                        imgEducator.DataValue = null;
                        hdnEducatorSign.Value = String.Empty;
                    }

                    PopulateVerifyInfo(ent);
                }

                grdPatientEducation.Rebind();
            }
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            if (newVal != AppEnum.DataMode.Read)
            {
                StandardReference.InitializeIncludeSpace(cboSRPatientEducationEvaluation, AppEnum.StandardReference.PatientEducationEvaluation);
                StandardReference.InitializeIncludeSpace(cboSREducationProblem, AppEnum.StandardReference.PatientEducationProblem);
                StandardReference.InitializeIncludeSpace(cboSREducationMethod, AppEnum.StandardReference.PatientEducationMethod);
                StandardReference.InitializeIncludeSpace(cboSRPatientEducationRecipient, AppEnum.StandardReference.PatientEducationRecipient);
                StandardReference.InitializeIncludeSpace(cboSRPatientEducationGoal, AppEnum.StandardReference.PatientEducationGoal);
            }

            var isEdited = newVal != AppEnum.DataMode.Read;
            grdPatientEducation.Columns[0].Display = isEdited; // Selected
            grdPatientEducation.Columns[1].Display = !isEdited; // IsSelected
            grdPatientEducation.Columns[3].Display = !isEdited; // Notes
            grdPatientEducation.Columns[4].Display = isEdited; // Notes Edit

            //SIGN
            var isVisible = newVal != AppEnum.DataMode.Read;
            btnFmSign.Enabled = isVisible;
            btnPsSign.Enabled = isVisible;
        }
        protected override void OnMenuNewClick()
        {
            txtSeqNo.Text = string.Format("{0:00000}", NewSeqNo());
            txtEducationDateTime.SelectedDate = DateTime.Now;
            txtDuration.Value = 5;
            ComboBox.PopulateWithOneRow(cboEducationByUserID, AppSession.UserLogin.UserID, Enums.EntityClassName.AppUser, "UserID", "UserName");
            ApplyEducationListByUserType(string.IsNullOrWhiteSpace(EducationTypeDefault) ? AppSession.UserLogin.SRUserType : EducationTypeDefault);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            Save(args, true);
        }

        private bool Save(ValidateArgs args, bool isNewRecord = false)
        {
            var ent = new PatientEducation();

            if (!string.IsNullOrWhiteSpace(PrescriptionNo))
            {
                // Check recordnya untuk pemanggilan dari PrescriptionSales
                if (!ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
                {
                    var newSeqNo = NewSeqNo();
                    txtSeqNo.Text = txtSeqNo.Text = string.Format("{0:00000}", newSeqNo);
                    ent.RegistrationNo = RegistrationNo;
                    ent.SeqNo = newSeqNo;
                }
            }
            else
            {
                if (isNewRecord)
                {
                    var newSeqNo = NewSeqNo();
                    txtSeqNo.Text = txtSeqNo.Text = string.Format("{0:00000}", newSeqNo);
                    ent.RegistrationNo = RegistrationNo;
                    ent.SeqNo = newSeqNo;

                }
                else
                {
                    if (!ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
                    {
                        args.IsCancel = true;
                        args.MessageText = AppMessage.GetMessageText(AppMessage.MessageIdEnum.RecordDoesNotExist);
                        return false;
                    }
                }
            }

            var imgHelper = new ImageHelper();

            // Patient / Family Sign
            if (!string.IsNullOrWhiteSpace(hdnPatientSign.Value))
            {
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnPatientSign.Value), new Size(332, 185));
                ent.FmSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }
            else
                ent.FmSign = null;

            // Educator Sign
            if (!string.IsNullOrWhiteSpace(hdnEducatorSign.Value))
            {
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnEducatorSign.Value), new Size(332, 185));
                ent.PsSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }
            else
                ent.PsSign = null;

            ent.SRUserType = AppSession.UserLogin.SRUserType;
            ent.EducationType = txtEducationType.Text;
            ent.EducationByUserID = cboEducationByUserID.SelectedValue;
            ent.EducationDateTime = txtEducationDateTime.SelectedDate;
            ent.SRPatientEducationEvaluation = cboSRPatientEducationEvaluation.SelectedValue;
            ent.SRPatientEducationMethod = cboSREducationMethod.SelectedValue;
            ent.SRPatientEducationProblem = cboSREducationProblem.SelectedValue;
            ent.SRPatientEducationRecipient = cboSRPatientEducationRecipient.SelectedValue;
            ent.RecipientName = txtRecipientName.Text;
            ent.MethodOther = txtMethodOther.Text;
            ent.Duration = txtDuration.Value.ToInt();

            ent.PatientEducationEvaluationOth = txtPatientEducationEvaluationOth.Text;
            ent.SRPatientEducationGoal = cboSRPatientEducationGoal.SelectedValue;
            ent.PatientEducationGoalOth = txtPatientEducationGoalOth.Text;
            ent.Verificator = txtVerificator.Text;


            // Update Prescription jika dipangggil dari PrescriptionSales page
            if (!string.IsNullOrEmpty(PrescriptionNo))
            {
                var presc = new TransPrescription();
                presc.LoadByPrimaryKey(PrescriptionNo);
                presc.PatientEducationSeqNo = ent.SeqNo;
                presc.Save();

                ent.ReferenceNo = PrescriptionNo;
                ent.ReferenceType = "PRESC";

                // Return value to parent page
                hdfReturnValue.Value = string.Format("{0} @{1}", AppSession.UserLogin.UserName, ent.EducationDateTime.Value.ToString(AppConstant.DisplayFormat.DateTimeSecond));

            }

            ent.Save();

            SavePatientEducation();

            txtSeqNo.Text = string.Format("{0:00000}", ent.SeqNo);
            grdPatientEducation.Rebind();
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            // Return info jika dipangggil dari PrescriptionSales page
            if (!string.IsNullOrEmpty(PrescriptionNo))
                return string.Format("oArg.value = document.getElementById('{0}').value;", hdfReturnValue.ClientID);

            return string.Empty;
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            Save(args, false);
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
            grdPatientEducation.Rebind();
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
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {
        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var ent = new PatientEducation();
            if (ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
            {
                ent.Verificator = AppUser.GetUserName(AppSession.UserLogin.UserID);
                ent.VerifyByUserID = AppSession.UserLogin.UserID;
                ent.VerifyDateTime = DateTime.Now;
                ent.Save();

                PopulateVerifyInfo(ent);
            }
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
            // Dipindah ke OnLoadComplete karena oleh framework digunakan juga utk status disable tombol Approve yg dipakai untuk verif (Handono 230411)
            //return !string.IsNullOrEmpty(txtSeqNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text);
            return true;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !string.IsNullOrEmpty(txtSeqNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            // Disable jika sudah diverify
            //return string.IsNullOrWhiteSpace(lblVerifyInfo.Text);

            return string.IsNullOrWhiteSpace(txtVerifyDateTime.Text);
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


        protected void grdPatientEducation_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var grd = ((RadGrid)sender);
            var eduType = string.IsNullOrEmpty(txtEducationType.Text) ? "OTH" : txtEducationType.Text;
            grd.DataSource = PatientEducationDataTable(RegistrationNo, txtSeqNo.Text.ToInt(), eduType);
        }

        private DataTable PatientEducationDataTable(string registrationNo, int seqNo, string eduType, bool isJustSelected = false)
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

            que.Where(que.ReferenceID.Like(string.Format("{0}%", eduType)));
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
            var usr = new AppUser();
            if (usr.LoadByPrimaryKey(cboEducationByUserID.SelectedValue))
                ApplyEducationListByUserType(usr.SRUserType);
        }

        private void ApplyEducationListByUserType(string userType)
        {
            txtEducationType.Text = userType;
            grdPatientEducation.DataSource = null; // Agar Rebind yakin dijalankan
            grdPatientEducation.Rebind();
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtDuration.Value == null || txtDuration.Value == 0)
            {
                args.IsValid = false;
                customValidator.ErrorMessage = @"Duration not valid";
            }
        }
    }
}
