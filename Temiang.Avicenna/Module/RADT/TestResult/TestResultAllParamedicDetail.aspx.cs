using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Dal.Core;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT
{
    public partial class TestResultAllParamedicDetail : BasePageDetail
    {
        private void SetEntityValue(TestResult entity, TransCharges tc)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.ItemID = ViewState["ItemID" + Request.UserHostName].ToString();
            entity.ParamedicID = ViewState["ParamedicID" + Request.UserHostName].ToString();
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

        private void MoveRecord(bool isNextRecord)
        {
            TransChargesItemQuery que = new TransChargesItemQuery("a");
            ItemQuery qItem = new ItemQuery("c");
            que.InnerJoin(qItem).On(que.ItemID == qItem.ItemID & qItem.SRItemType == ItemType.Radiology);
            que.Where(que.IsOrderRealization == true);
            que.Select(que.TransactionNo, que.SequenceNo, que.ParamedicID, que.ItemID);
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TransactionNo > txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where(que.TransactionNo < txtTransactionNo.Text);
                que.OrderBy(que.TransactionNo.Descending);
            }
            DataTable dtb = que.LoadDataTable();

            if (dtb.Rows.Count > 0 && dtb.Rows[0][0] != DBNull.Value)
            {
                TestResult entity = new TestResult();
                string paramedicID = dtb.Rows[0]["ParamedicID"].ToString();
                string itemID = dtb.Rows[0]["ItemID"].ToString();
                string transactionNo = dtb.Rows[0]["TransactionNo"].ToString();
                string ClinicalInfo = dtb.Rows[0]["ClinicalInfo"].ToString();

                if (!entity.LoadByPrimaryKey(transactionNo, itemID))
                {
                    entity = new TestResult();
                    entity.TransactionNo = transactionNo;
                    entity.ClinicalInfo = ClinicalInfo;
                    entity.ItemID = itemID;
                    entity.ParamedicID = paramedicID;
                }

                OnPopulateEntryControl(entity);
            }
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            TestResult entity = new TestResult();
            TransChargesItem chargesItem = new TransChargesItem();
            if (parameters.Length > 0)
            {
                string seqNo = parameters[1];
                string transactionNo = parameters[0];

                if (chargesItem.LoadByPrimaryKey(transactionNo, seqNo))
                {
                    var isnew = !entity.LoadByPrimaryKey(transactionNo, chargesItem.ItemID);
                    //if (!entity.LoadByPrimaryKey(transactionNo, chargesItem.ItemID))
                    //{}
                    entity.TransactionNo = transactionNo;
                    entity.ItemID = chargesItem.ItemID;

                    if (Request.QueryString["replace"] == null || string.IsNullOrEmpty(Request.QueryString["replace"]) || Request.QueryString["replace"] == "no" || string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
                    {
                        var comps = new TransChargesItemCompCollection();

                        var compq = new TransChargesItemCompQuery("a");
                        var tc = new TariffComponentQuery("b");
                        compq.InnerJoin(tc).On(compq.TariffComponentID.Equal(tc.TariffComponentID))
                            .Where(
                                compq.TransactionNo == transactionNo &&
                                compq.SequenceNo == seqNo &&
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
                    }
                    else
                    {
                        entity.ParamedicID = AppSession.UserLogin.ParamedicID;
                        
                        var par = new Paramedic();
                        par.LoadByPrimaryKey(entity.ParamedicID);

                        chargesItem.ParamedicCollectionName = par.ParamedicName; 
                        chargesItem.Save();
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

        protected override void OnPopulateEntryControl(esEntity entity)
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
                txtEntryDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtEntryTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            }
            txtClinicalInfo.Text = testResult.ClinicalInfo;

            ViewState["ItemID" + Request.UserHostName] = testResult.ItemID;
            if (!string.IsNullOrEmpty(testResult.ParamedicID))
            {
                ViewState["ParamedicID" + Request.UserHostName] = testResult.ParamedicID;
            }
            else
            {
                ViewState["ParamedicID" + Request.UserHostName] = Page.Request.QueryString["pid"];
                testResult.ParamedicID = Page.Request.QueryString["pid"];
            }

            var tc = new TransCharges();
            if (tc.LoadByPrimaryKey(testResult.TransactionNo))
            {
                cboPhysicianSender.Text = tc.PhysicianSenders;
            }

            txtTestResult.Content = (DataModeCurrent == AppEnum.DataMode.Edit || testResult.TestResult == null) ?
                testResult.TestResult :
                Common.TestResultHelper.ReplaceKeyword(testResult.TestResult, cboPhysicianSender.Text);
            txtTestSummary.Content = testResult.TestSummary;
            txtTestSuggest.Content = testResult.TestSuggest;
            txtTestResultOtherLang.Content = testResult.TestResultOtherLang;
            txtTestSummaryOtherLang.Content = testResult.TestSummaryOtherLang;
            txtTestSuggestOtherLang.Content = testResult.TestSuggestOtherLang;

            var item = new Item();
            txtItemID.Text = testResult.ItemID;
            lblItemName.Text = item.LoadByPrimaryKey(testResult.ItemID) ? item.ItemName : string.Empty;

            var paramedic = new Paramedic();
            txtParamedicName.Text = paramedic.LoadByPrimaryKey(testResult.ParamedicID) ? paramedic.ParamedicName : string.Empty;

            cboTestResultTemplateID.DataSource = null;
            cboTestResultTemplateID.DataBind();
            cboTestResultTemplateID.SelectedValue = string.Empty;
            cboTestResultTemplateID.Text = string.Empty;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            var entity = new TestResult();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text, ViewState["ItemID" + Request.UserHostName].ToString()))
            {
                OnPopulateEntryControl(entity);
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            //switch (programID)
            //{
            //    case AppConstant.Report.TestResultNative:
            //        printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            //        break;
            //    case AppConstant.Report.TestResultEnglish:
            //        printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            //        break;
            //    case AppConstant.Report.EtiketRadiology:
            //        printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            //        break;
            //}


            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
            printJobParameters.AddNew("p_SequenceNo", Request.QueryString["seqno"]);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = String.Format("TransactionNo='{0}' AND ItemID={1}'", txtTransactionNo.Text.Trim(), ViewState["ItemID" + Request.UserHostName]);
            auditLogFilter.TableName = "TestResult";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "TestResultSearch.aspx";
            UrlPageList = "TestResultList.aspx";

            ProgramID = AppConstant.Program.TestResult;

            if (!IsPostBack)
            {
                var tc = new TransCharges();
                tc.LoadByPrimaryKey(Request.QueryString["trno"]);
                txtNotes.Text = tc.Notes;

                //var tr = new TestResult();
                //tr.LoadByPrimaryKey(Request.QueryString["trno"]);
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
                    txtS.Text = string.IsNullOrEmpty(soap.Subjective.Trim()) ? txtS.Text : txtS.Text + soap.Subjective + System.Environment.NewLine;
                    txtO.Text = string.IsNullOrEmpty(soap.Objective.Trim()) ? txtO.Text : txtO.Text + soap.Objective + System.Environment.NewLine;
                    txtA.Text = string.IsNullOrEmpty(soap.Assesment.Trim()) ? txtA.Text : txtA.Text + soap.Assesment + System.Environment.NewLine;
                    txtP.Text = string.IsNullOrEmpty(soap.Planning.Trim()) ? txtP.Text : txtP.Text + soap.Planning.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
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
                        txtS.Text = string.IsNullOrEmpty(rim.Info1.Trim()) ? txtS.Text : txtS.Text + rim.Info1 + System.Environment.NewLine;
                        txtO.Text = string.IsNullOrEmpty(rim.Info2.Trim()) ? txtO.Text : txtO.Text + rim.Info2 + System.Environment.NewLine;
                        txtA.Text = string.IsNullOrEmpty(rim.Info3.Trim()) ? txtA.Text : txtA.Text + rim.Info3 + System.Environment.NewLine;
                        txtP.Text = string.IsNullOrEmpty(rim.Info4.Trim()) ? txtP.Text : txtP.Text + rim.Info4.Replace("<br />", Environment.NewLine) + System.Environment.NewLine;
                    }
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuAdd.Enabled = false;
            ToolBarMenuDelete.Enabled = false;
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //}

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            args.IsCancel = true;

            var entity = new TestResult();
            entity.LoadByPrimaryKey(txtTransactionNo.Text, ViewState["ItemID" + Request.UserHostName].ToString());
            entity.MarkAsDeleted();
            SaveEntity(entity, null);

            OnPopulateEntryControl(new string[2] { Request.QueryString["trno"], Request.QueryString["seqno"] });
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
        }

        #endregion

        #region ComboBox TestResultTemplateID

        protected void cboTestResultTemplateID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.TestResultTemplateItemsRequested((RadComboBox)sender, ViewState["ParamedicID" + Request.UserHostName].ToString(), txtItemID.Text, e.Text);
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
    }
}
