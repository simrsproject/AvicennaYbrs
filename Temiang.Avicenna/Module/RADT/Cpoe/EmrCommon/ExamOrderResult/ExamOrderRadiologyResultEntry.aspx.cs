using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Core;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Text.RegularExpressions;
using System.Web; // pastikan namespace ini ada di atas

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ExamOrderRadiologyResultEntry : BasePageDialogEntry
    {
        protected string TransactionNo
        {
            get
            {
                return Request.QueryString["trno"];
            }
        }
        protected string SequenceNo
        {
            get
            {
                return Request.QueryString["seqno"];
            }
        }

        protected string TransType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? "" : Request.QueryString["type"];
            }
        }

        // fungsi kecil untuk membersihkan semua tag HTML
        string CleanHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // decode entity seperti &nbsp;, &amp;, &lt;, dll
            input = HttpUtility.HtmlDecode(input);

            // ganti <br> / <br /> jadi newline dulu
            input = input.Replace("<br />", Environment.NewLine)
                         .Replace("<br>", Environment.NewLine);

            // hapus semua tag HTML lain seperti <p>, <b>, <i>, <span>, dll
            input = Regex.Replace(input, "<.*?>", string.Empty);

            // hilangkan spasi kosong berlebih
            return input.Trim();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

            // Program Fiture
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.DeleteVisible = false;
            ToolBar.NavigationVisible = false;
            ToolBar.PrintVisible = true;
            // -------------------

            if (!IsPostBack)
            {
                var tc = new TransCharges();
                tc.LoadByPrimaryKey(TransactionNo);
                txtNotes.Text = tc.Notes;

                //var tr = new TestResult();
                //tr.LoadByPrimaryKey(TransactionNo);
                //txtClinicalInfo.Text = tc.Notes;

                // From table EpisodeSOAPE
                var soapColl = new EpisodeSOAPECollection();
                soapColl.Query.Where(
                    soapColl.Query.RegistrationNo == tc.RegistrationNo &&
                    soapColl.Query.IsVoid == false,
                    soapColl.Query.Or(soapColl.Query.Imported.IsNull(), soapColl.Query.Imported == false)
                    );
                soapColl.LoadAll();

                foreach (var soap in soapColl)
                {
                    txtS.Text = string.IsNullOrEmpty(soap.Subjective?.Trim())
                        ? txtS.Text
                        : txtS.Text + CleanHtml(soap.Subjective) + Environment.NewLine;

                    txtO.Text = string.IsNullOrEmpty(soap.Objective?.Trim())
                        ? txtO.Text
                        : txtO.Text + CleanHtml(soap.Objective) + Environment.NewLine;

                    txtA.Text = string.IsNullOrEmpty(soap.Assesment?.Trim())
                        ? txtA.Text
                        : txtA.Text + CleanHtml(soap.Assesment) + Environment.NewLine;

                    txtP.Text = string.IsNullOrEmpty(soap.Planning?.Trim())
                        ? txtP.Text
                        : txtP.Text + CleanHtml(soap.Planning) + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(txtS.Text) && string.IsNullOrEmpty(txtO.Text) && string.IsNullOrEmpty(txtA.Text) && string.IsNullOrEmpty(txtP.Text))
                {
                    //From Table RegistrationInfoMedic
                    var rimColl = new RegistrationInfoMedicCollection();
                    rimColl.Query.Where(
                        rimColl.Query.RegistrationNo == tc.RegistrationNo,
                        rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
                        );
                    rimColl.LoadAll();

                    foreach (var rim in rimColl)
                    {
                        txtS.Text = string.IsNullOrEmpty(rim.Info1?.Trim())
                            ? txtS.Text
                            : txtS.Text + CleanHtml(rim.Info1) + Environment.NewLine;

                        txtO.Text = string.IsNullOrEmpty(rim.Info2?.Trim())
                            ? txtO.Text
                            : txtO.Text + CleanHtml(rim.Info2) + Environment.NewLine;

                        txtA.Text = string.IsNullOrEmpty(rim.Info3?.Trim())
                            ? txtA.Text
                            : txtA.Text + CleanHtml(rim.Info3) + Environment.NewLine;

                        txtP.Text = string.IsNullOrEmpty(rim.Info4?.Trim())
                            ? txtP.Text
                            : txtP.Text + CleanHtml(rim.Info4) + Environment.NewLine;
                    }
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        private void SetEntityValue(TestResult entity, TransCharges tc)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.ItemID = ViewState["ItemID" + Request.UserHostName].ToString();
            entity.ParamedicID = ViewState["vpid"].ToString();
            entity.ClinicalInfo = txtClinicalInfo.Text;
            entity.TestResultDateTime = DateTime.Parse(txtEntryDate.SelectedDate.Value.ToShortDateString() + " " + txtEntryTime.TextWithLiterals);

            entity.TestResult = txtTestResult.Content;
            entity.TestSummary = txtTestSummary.Content;
            entity.TestSuggest = txtTestSuggest.Content;
            entity.TestResultOtherLang = txtTestResultOtherLang.Content;
            entity.TestSummaryOtherLang = txtTestSummaryOtherLang.Content;
            entity.TestSuggestOtherLang = txtTestSuggestOtherLang.Content;

            if (entity.es.IsModified)
            {

                entity.TestResultHistory = string.Format("[{0}]\n{1}\n\n{2}", (new DateTime()).NowAtSqlServer(), txtTestResult.Text, entity.TestResultHistory);
                entity.TestSummaryHistory = string.Format("[{0}]\n{1}\n\n{2}", (new DateTime()).NowAtSqlServer(), txtTestSummary.Text, entity.TestSummaryHistory);
                entity.TestSuggestHistory = string.Format("[{0}]\n{1}\n\n{2}", (new DateTime()).NowAtSqlServer(), txtTestSuggest.Text, entity.TestSuggestHistory);
                entity.TestResultOtherLangHistory = string.Format("[{0}]\n{1}\n\n{2}", (new DateTime()).NowAtSqlServer(), txtTestResultOtherLang.Text, entity.TestResultOtherLangHistory);
                entity.TestSummaryOtherLangHistory = string.Format("[{0}]\n{1}\n\n{2}", (new DateTime()).NowAtSqlServer(), txtTestSummaryOtherLang.Text, entity.TestSummaryOtherLangHistory);
                entity.TestSuggestOtherLangHistory = string.Format("[{0}]\n{1}\n\n{2}", (new DateTime()).NowAtSqlServer(), txtTestSuggestOtherLang.Text, entity.TestSuggestOtherLangHistory);
            }
            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            if (tc.LoadByPrimaryKey(entity.TransactionNo))
            {
                tc.PhysicianSenders = cboPhysicianSender.Text;
            }
        }
        #region Override Method & Function

        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            TestResult entity = new TestResult();
            TransChargesItem chargesItem = new TransChargesItem();
            if (!string.IsNullOrWhiteSpace(TransactionNo))
            {

                if (chargesItem.LoadByPrimaryKey(TransactionNo, SequenceNo))
                {
                    var isnew = !entity.LoadByPrimaryKey(TransactionNo, chargesItem.ItemID);
                    entity.TransactionNo = TransactionNo;
                    entity.ItemID = chargesItem.ItemID;

                    if (Request.QueryString["replace"] == null || string.IsNullOrEmpty(Request.QueryString["replace"]) || Request.QueryString["replace"] == "no" || string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
                    {
                        var comps = new TransChargesItemCompCollection();

                        var compq = new TransChargesItemCompQuery("a");
                        var tc = new TariffComponentQuery("b");
                        compq.InnerJoin(tc).On(compq.TariffComponentID.Equal(tc.TariffComponentID))
                            .Where(
                                compq.TransactionNo == TransactionNo &&
                                compq.SequenceNo == SequenceNo &&
                                tc.IsTariffParamedic.Equal(true)
                            ).Select(compq)
                            .OrderBy(comps.Query.TariffComponentID.Descending);
                        comps.Load(compq);

                        foreach (var comp in comps)
                        {
                            // update kalau ada perubahan dokter
                            if (!isnew && entity.ParamedicID != comp.ParamedicID)
                            {
                                entity.ParamedicID = comp.ParamedicID;
                                entity.Save();
                            }
                            else entity.ParamedicID = comp.ParamedicID;
                        }

                        if (entity.ParamedicID != null)
                        {
                            var par = new Paramedic();
                            if (par.LoadByPrimaryKey(entity.ParamedicID))
                            {
                                chargesItem.ParamedicCollectionName = par.ParamedicName;
                                chargesItem.Save();
                            }
                        }
                    }
                    else
                    {
                        entity.ParamedicID = AppSession.UserLogin.ParamedicID;

                        var par = new Paramedic();
                        if (par.LoadByPrimaryKey(entity.ParamedicID))
                        {
                            chargesItem.ParamedicCollectionName = par.ParamedicName;
                            chargesItem.Save();
                        }
                    }
                    if (string.IsNullOrEmpty(entity.ParamedicID))
                    {
                        if (!string.IsNullOrEmpty(chargesItem.ParamedicID)) entity.ParamedicID = chargesItem.ParamedicID;
                        else
                        {
                            if (!string.IsNullOrEmpty(chargesItem.ParamedicCollectionName))
                            {
                                var par = new Paramedic();
                                par.Query.Where(par.Query.ParamedicName == chargesItem.ParamedicCollectionName);
                                if (par.Query.Load()) entity.ParamedicID = par.ParamedicID;
                            }
                        }
                    }

                    //}
                }
                OnPopulateEntryControl(entity);
            }
        }

        protected void OnPopulateEntryControl(esEntity entity)
        {
            var testResult = (TestResult)entity;
            txtTransactionNo.Text = testResult.TransactionNo;
            if (testResult.TestResultDateTime.HasValue)
            {
                txtEntryDate.SelectedDate = testResult.TestResultDateTime;
                txtEntryTime.Text = testResult.TestResultDateTime.Value.ToString("HH:mm");
            }
            else
            {
                var time = (new DateTime()).NowAtSqlServer();
                txtEntryDate.SelectedDate = time;
                txtEntryTime.Text = time.ToString("HH:mm");
            }
            txtClinicalInfo.Text = testResult.ClinicalInfo;

            ViewState["ItemID" + Request.UserHostName] = testResult.ItemID;
            if (!string.IsNullOrEmpty(testResult.ParamedicID))
            {
                ViewState["vpid"] = testResult.ParamedicID;
            }
            else
            {
                ViewState["vpid"] = Page.Request.QueryString["pid"];
                testResult.ParamedicID = Page.Request.QueryString["pid"];
            }

            var tc = new TransCharges();
            if (tc.LoadByPrimaryKey(testResult.TransactionNo))
            {
                cboPhysicianSender.Text = tc.PhysicianSenders;
                txtRegistrationNo.Text = tc.RegistrationNo;
            }
            ///munculin reg,rm,nama
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(txtRegistrationNo.Text))
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.str.PatientID);
                txtPatientName.Text = pat.PatientName;
                txtMedicalNo.Text = pat.MedicalNo;

                PopulatePatientImage(pat.PatientID);
            }
            txtTestResult.Content = (DataModeCurrent == AppEnum.DataMode.Edit || testResult.TestResult == null) ?
                testResult.TestResult :
                ReplaceKeyword(testResult.TestResult, cboPhysicianSender.Text);
            txtTestSummary.Content = testResult.TestSummary;
            txtTestSuggest.Content = testResult.TestSuggest;
            txtTestResultOtherLang.Content = testResult.TestResultOtherLang;
            txtTestSummaryOtherLang.Content = testResult.TestSummaryOtherLang;
            txtTestSuggestOtherLang.Content = testResult.TestSuggestOtherLang;

            var item = new Item();
            txtItemID.Text = testResult.ItemID;
            lblItemName.Text = item.LoadByPrimaryKey(testResult.ItemID) ? item.ItemName : string.Empty;

            this.Title = "Result of Item: " + lblItemName.Text;

            var paramedic = new Paramedic();
            txtParamedicName.Text = paramedic.LoadByPrimaryKey(testResult.ParamedicID) ? paramedic.ParamedicName : string.Empty;

            cboTestResultTemplateID.DataSource = null;
            cboTestResultTemplateID.DataBind();
            cboTestResultTemplateID.SelectedValue = string.Empty;
            cboTestResultTemplateID.Text = string.Empty;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            lbtnDocumentImageAdd.Visible = newVal != AppEnum.DataMode.Read;
        }
        private string ReplaceKeyword(string testResult, string PhysicianSenderName)
        {
            return testResult.Replace("[sender]", PhysicianSenderName);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            printJobParameters.AddNew("p_SequenceNo", SequenceNo);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = String.Format("TransactionNo='{0}' AND ItemID={1}'", txtTransactionNo.Text.Trim(), ViewState["ItemID" + Request.UserHostName]);
            auditLogFilter.TableName = "TestResult";
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            //ToolBarMenuAdd.Enabled = false;
            //ToolBarMenuDelete.Enabled = false;
            ToolBarMenuAdd.Visible = false;
            ToolBarMenuDelete.Visible = false;

            ToolBarMenuPrint.Enabled = (TransType != "oth");

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsTestResultAllowModifDate))
            {
                txtEntryDate.DateInput.ReadOnly = false;
                txtEntryDate.DatePopupButton.Enabled = true;
            }
        }


        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            args.IsCancel = true;

            var entity = new TestResult();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, ViewState["ItemID" + Request.UserHostName].ToString());
            entity.MarkAsDeleted();
            SaveEntity(entity, null);

            OnPopulateEntryControl(args);
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtTestResult.Text) && string.IsNullOrEmpty(txtTestResultOtherLang.Text))
            {
                args.MessageText = "Result required.";
                args.IsCancel = true;
                return;
            }
            var entity = new TestResult();
            entity.AddNew();
            var tc = new TransCharges();
            SetEntityValue(entity, tc);
            SaveEntity(entity, tc);

            if (TransType == "oth" && AppParameter.IsYes(AppParameter.ParameterItem.IsAutoSaveOtherExamResultToDocFolderAfterSave))
            {
                SaveResultToSepFolder();
            }
        }

        private static void SaveEntity(TestResult entity, TransCharges tc)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                if (tc != null) tc.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtTestResult.Text) && string.IsNullOrEmpty(txtTestResultOtherLang.Text))
            {
                args.MessageText = "Result required.";
                args.IsCancel = true;
                return;
            }

            var entity = new TestResult();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text, ViewState["ItemID" + Request.UserHostName].ToString()))
            {
                entity = new TestResult();
            }
            var tc = new TransCharges();
            SetEntityValue(entity, tc);
            SaveEntity(entity, tc);

            if (TransType == "oth" && AppParameter.IsYes(AppParameter.ParameterItem.IsAutoSaveOtherExamResultToDocFolderAfterSave))
            {
                SaveResultToSepFolder();
            }
        }

        private void SaveResultToSepFolder()
        {
            var isSaveResultToSepFolder = false;
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                var gr = new Guarantor();
                gr.LoadByPrimaryKey(reg.GuarantorID);
                var isBpjsPatient = gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeBpjs));
                if (isBpjsPatient && !string.IsNullOrEmpty(reg.BpjsSepNo))
                {
                    isSaveResultToSepFolder = true;
                }
                else
                {
                    isSaveResultToSepFolder = !gr.SRGuarantorType.Equals(AppParameter.GetParameterValue(AppParameter.ParameterItem.GuarantorTypeSelf)); ;
                }
            }

            if (isSaveResultToSepFolder)
            {
                // Save cetakan MDS
                var printJobParameters = new PrintJobParameterCollection();
                printJobParameters.AddNew("p_TransactionNo", TransactionNo);
                printJobParameters.AddNew("p_SequenceNo", SequenceNo);
                var path = Module.Reports.ReportViewer.SaveFileToGuarantorDocument(AppSession.Parameter.HealthcareInitial, AppSession.Parameter.ProgramIdPrintExamOrderOtherResult, printJobParameters); 
            }
        }

        #endregion

        #region ComboBox TestResultTemplateID
        protected void cboTestResultTemplateID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.TestResultTemplateItemsRequested((RadComboBox)sender, ViewState["vpid"].ToString(), txtItemID.Text, e.Text);
        }

        protected void cboTestResultTemplateID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.TestResultTemplateItemDataBound(e);
        }
        protected void cboTestResultTemplateID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboTestResultTemplateID.SelectedValue == string.Empty) return;

            TestResultTemplate resultTemplate = new TestResultTemplate();
            if (resultTemplate.LoadByPrimaryKey(Convert.ToInt32(cboTestResultTemplateID.SelectedValue)))
            {
                txtTestResult.Content = resultTemplate.TestResult;
                txtTestSuggest.Content = resultTemplate.TestSuggest;
                txtTestSummary.Content = resultTemplate.TestSummary;
                txtTestResultOtherLang.Content = resultTemplate.TestResultOtherLang;
                txtTestSuggestOtherLang.Content = resultTemplate.TestSuggestOtherLang;
                txtTestSummaryOtherLang.Content = resultTemplate.TestSummaryOtherLang;
            }
        }

        #endregion

        protected void cboPhysicianSender_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((string)e.Item.DataItem);
            e.Item.Value = ((string)e.Item.DataItem);
        }

        protected void cboPhysicianSender_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            // ambil dari master dokter
            var pcoll = new ParamedicCollection();
            pcoll.Query.Where(
                pcoll.Query.ParamedicName.Like("%" + e.Text + "%"),
                pcoll.Query.IsActive == true,
                pcoll.Query.SRParamedicType.In(new string[]{
                    "ParamedicType-001",
                    "ParamedicType-002",
                    "ParamedicType-003",
                    "ParamedicType-004"}));
            pcoll.LoadAll();

            // ambil dari master sender
            var scoll = new AppStandardReferenceItemCollection();
            scoll.Query.Where(
                scoll.Query.StandardReferenceID == "PhysicianSender",
                scoll.Query.ItemName.Like("%" + e.Text + "%"),
                scoll.Query.IsActive == true);
            scoll.LoadAll();

            var strs = (pcoll.Select(x => x.ParamedicName).Union(scoll.Select(x => x.ItemName)).Distinct()).OrderBy(x => x);

            //foreach (var item in strs)
            //{
            //    comboBox.Items.Add(new RadComboBoxItem(item.ItemName, item.ItemID));
            //}
            cboPhysicianSender.DataSource = strs;
            cboPhysicianSender.DataBind();
        }

        #region PatientImage
        private void PopulatePatientImage(string patientID)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;

            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                    Convert.ToBase64String(patientImg.Photo));
            }

        }
        #endregion

        #region Document Image

        protected void lvItemDocumentImage_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var qr = new TransChargesItemImageQuery();
            qr.Where(qr.TransactionNo == TransactionNo, qr.SequenceNo == SequenceNo);
            var dtb = qr.LoadDataTable();
            lvItemDocumentImage.DataSource = dtb;
        }
        #endregion

        protected void lbtnDocumentImageDelete_OnClick(object sender, EventArgs e)
        {
            var lbtn = (LinkButton) sender;
            var nmd = new TransChargesItemImage();
            nmd.LoadByPrimaryKey(TransactionNo, SequenceNo, lbtn.CommandArgument.ToInt());
            nmd.MarkAsDeleted();
            nmd.Save();
            lvItemDocumentImage.Rebind();
        }
    }
}
