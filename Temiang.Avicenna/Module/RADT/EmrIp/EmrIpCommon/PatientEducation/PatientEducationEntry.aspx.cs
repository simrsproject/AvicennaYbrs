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
using Newtonsoft.Json;
using DevExpress.XtraPrinting.Export.Pdf;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
//using Telerik.Reporting;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    [Obsolete("Sudah tidak dikembangkan, gunakan PatientEducationDetail.aspx untuk kondisi terakhir (Handono 230317)",true)]
    public partial class PatientEducationEntry : BasePageDialogHistEntry
    {
        private string EducationType
        {
            get { return Request.QueryString["edutype"]; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            //IsSingleRecordMode = true; //Save then close
            //ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.PrintVisible = false;
            ToolBar.DeleteVisible = true;

            //ToolBar.EditVisible = false;
            //ToolBar.AddVisible = false;
            // -------------------

            Splitter.Orientation = Orientation.Horizontal;
            Splitter.Items[0].Height = Unit.Pixel(400);

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Patient Education of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }

                if (!string.IsNullOrWhiteSpace(EducationType))
                {
                    txtEducationType.Text = EducationType;
                    cboEducationByUserID.AutoPostBack = false;
                }
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
            if (ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
            {
                txtSeqNo.Text = string.Format("{0:00000}", ent.SeqNo);
                txtEducationDateTime.SelectedDate = ent.EducationDateTime;
                ComboBox.PopulateWithOneRow(cboEducationByUserID, ent.EducationByUserID, Enums.EntityClassName.AppUser, "UserID", "UserName");
                ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationEvaluation,
                    AppEnum.StandardReference.PatientEducationEvaluation.ToString(), ent.SRPatientEducationEvaluation);
                ComboBox.PopulateWithOneStandardReference(cboSREducationProblem,
                    AppEnum.StandardReference.PatientEducationProblem.ToString(), ent.SRPatientEducationProblem);
                ComboBox.PopulateWithOneStandardReference(cboSREducationMethod,
                    AppEnum.StandardReference.PatientEducationMethod.ToString(), ent.SRPatientEducationMethod);
                ComboBox.PopulateWithOneStandardReference(cboSRPatientEducationRecipient,
                    AppEnum.StandardReference.PatientEducationRecipient.ToString(), ent.SRPatientEducationRecipient);
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
                if (ent.FmSign != null)
                {
                    var val = (byte[])ent.FmSign;
                    fmImage.DataValue = val;
                    var mstream = new MemoryStream(val);
                    Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                    hdnImage1.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
                }
                else
                {
                    fmImage.DataValue = null;
                    hdnImage1.Value = String.Empty;
                }

                if (ent.PsSign != null)
                {
                    var val = (byte[])ent.PsSign;
                    psImage.DataValue = val;
                    var mstream = new MemoryStream(val);
                    Telerik.Web.UI.ImageEditor.EditableImage img = new Telerik.Web.UI.ImageEditor.EditableImage(mstream);
                    hdnImage2.Value = imgHelper.ToBase64String(img.Image, ImageFormat.Png);
                }
                else
                {
                    psImage.DataValue = null;
                    hdnImage2.Value = String.Empty;
                }

            }

            grdPatientEducation.Rebind();
        }
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            grdPatientEducationHist.Columns[0].Visible = newVal == AppEnum.DataMode.Read;
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
            ClearEntry();
            txtSeqNo.Text = string.Format("{0:00000}", NewSeqNo());
            txtEducationDateTime.SelectedDate = DateTime.Now;
            ComboBox.PopulateWithOneRow(cboEducationByUserID, AppSession.UserLogin.UserID, Enums.EntityClassName.AppUser, "UserID", "UserName");
            ApplyEducationListByUserType(string.IsNullOrWhiteSpace(EducationType) ? AppSession.UserLogin.SRUserType : EducationType);
        }

        private void ClearEntry()
        {
            txtSeqNo.Text = string.Empty;
            txtEducationDateTime.Clear();
            txtRecipientName.Text = string.Empty;
            txtMethodOther.Text = string.Empty;
            txtDuration.Value = 0;
            txtPatientEducationEvaluationOth.Text = string.Empty;
            txtPatientEducationGoalOth.Text = string.Empty;
            txtVerificator.Text = string.Empty;

            cboEducationByUserID.SelectedIndex = -1;
            cboEducationByUserID.Text = string.Empty;
            cboSRPatientEducationEvaluation.SelectedIndex = 0;
            cboSREducationProblem.SelectedIndex = 0;
            cboSREducationMethod.SelectedIndex = 0;
            cboSRPatientEducationRecipient.SelectedIndex = 0;
            cboSRPatientEducationGoal.SelectedIndex = 0;

            grdPatientEducation.Rebind();
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, true);
            if (!args.IsCancel)
            {
                grdPatientEducationHist.Rebind();
            }
        }

        private bool Save(ValidateArgs args, bool isNewRecord = false)
        {
            var ent = new PatientEducation();
            if (string.IsNullOrEmpty(txtSeqNo.Text) || !ent.LoadByPrimaryKey(RegistrationNo, txtSeqNo.Text.ToInt()))
            {
                ent.RegistrationNo = RegistrationNo;
                ent.SeqNo = NewSeqNo();
            }

            var imgHelper = new ImageHelper();
            //SIGN
            if (!string.IsNullOrWhiteSpace(hdnImage1.Value))
            {

                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage1.Value), new Size(332, 185));
                ent.FmSign = imgHelper.ToByteArray(resized, ImageFormat.Png);
            }
            else
                ent.FmSign = null;

            if (!string.IsNullOrWhiteSpace(hdnImage2.Value))
            {
                var resized = imgHelper.ResizeImage(imgHelper.ToImage(hdnImage2.Value), new Size(332, 185));
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

            ent.Save();

            SavePatientEducation();

            txtSeqNo.Text = string.Format("{0:00000}", ent.SeqNo);
            grdPatientEducation.Rebind();
            return true;
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            args.IsCancel = !Save(args, false);
            if (!args.IsCancel)
            {
                grdPatientEducationHist.Rebind();
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
            return !string.IsNullOrEmpty(txtSeqNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text);
        }

        public override bool OnGetStatusMenuDelete()
        {
            return !string.IsNullOrEmpty(txtSeqNo.Text) && string.IsNullOrEmpty(txtReferenceNo.Text);
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

        #region History
        protected void grdPatientEducationHist_ItemCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            if (e.CommandName == "View")
            {
                txtSeqNo.Text = string.Format("{0:00000}", item.OwnerTableView.DataKeyValues[item.ItemIndex]["SeqNo"]);
                OnPopulateEntryControl(new ValidateArgs());
                RefreshMenuStatus();
            }
            else if (e.CommandName == "PrintLabel1")
            {
                var jobParameters = new PrintJobParameterCollection();
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ';' });

                PrintJobParameter jobParameter;
                PrintJobParameter jobParameter2;


                jobParameter = jobParameters.AddNew();
                jobParameter2 = jobParameters.AddNew();


                jobParameter.Name = "RegistrationNo";
                jobParameter2.Name = "SeqNo";
                jobParameter.ValueString = commandArgs[0];
                jobParameter2.ValueString = commandArgs[1];

                AppSession.PrintJobReportID = AppConstant.Report.CetakanPasienEdukasi;
                AppSession.PrintJobParameters = jobParameters;


                string script = @"var oWnd = $find('" + winProcess.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        protected void grdPatientEducationHist_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPatientEducationHist.DataSource = EducationHistDataTable();
        }


        private DataTable EducationHistDataTable()
        {
            var query = new PatientEducationQuery("a");
            var eval = new AppStandardReferenceItemQuery("eval");
            query.LeftJoin(eval).On(query.SRPatientEducationEvaluation == eval.ItemID &&
                                    eval.StandardReferenceID == "PatientEducationEvaluation");

            var problem = new AppStandardReferenceItemQuery("problem");
            query.LeftJoin(problem).On(query.SRPatientEducationProblem == problem.ItemID &&
                                       problem.StandardReferenceID == "PatientEducationProblem");

            var method = new AppStandardReferenceItemQuery("method");
            query.LeftJoin(method).On(query.SRPatientEducationMethod == method.ItemID &&
                                      method.StandardReferenceID == "PatientEducationMethod");

            var recip = new AppStandardReferenceItemQuery("recip");
            query.LeftJoin(recip).On(query.SRPatientEducationRecipient == recip.ItemID &&
                                     recip.StandardReferenceID == "PatientEducationRecipient");

            var userType = new AppStandardReferenceItemQuery("ut");
            query.LeftJoin(userType).On(query.SRUserType == userType.ItemID &&
                                        userType.StandardReferenceID == "UserType");

            var ppa = new AppUserQuery("ppa");
            query.LeftJoin(ppa).On(query.EducationByUserID == ppa.UserID);

            query.Select
                (query, eval.ItemName.As("SRPatientEducationEvaluationName"),
                problem.ItemName.As("SRPatientEducationProblemName"),
                recip.ItemName.As("SRPatientEducationRecipientName"),
                method.ItemName.As("SRPatientEducationMethodName"),
                userType.ItemName.As("SRUserTypeName"), ppa.UserName.As("EducationByUserName"));

            query.Where(query.RegistrationNo == RegistrationNo);

            query.OrderBy(query.SeqNo.Descending);
            var dtb = query.LoadDataTable();

            return dtb;
        }

        protected string PatientEducationLineHtml(GridItem container)
        {
            var seqNo = DataBinder.Eval(container.DataItem, "SeqNo").ToInt();
            var eduType = DataBinder.Eval(container.DataItem, "EducationType").ToString();

            var dtb = PatientEducationDataTable(RegistrationNo, seqNo, eduType, true);
            var strb = new StringBuilder();
            strb.AppendLine("<table id='educationLine'>");
            strb.AppendLine("<tr>");
            strb.AppendLine("<th style = 'width: 250px'>Education</th><th>Notes</th>");
            strb.AppendLine("</tr>");

            foreach (DataRow row in dtb.Rows)
            {
                strb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", row["ItemName"], row["EducationNotes"]);
            }
            strb.AppendLine("</table>");

            return strb.ToString();

        }
        #endregion

    }
}
