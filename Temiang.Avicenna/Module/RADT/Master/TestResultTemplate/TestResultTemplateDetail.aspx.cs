using System;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class TestResultTemplateDetail : BasePageDetail
    {
        private Int32 NewID()
        {
            TestResultTemplateQuery query = new TestResultTemplateQuery();
            query.Select(query.TestResultTemplateID.Max().As("MaxID"));
            DataTable dtb = query.LoadDataTable();
            Int32 newID = 1;
            if (dtb.Rows.Count > 0 && dtb.Rows[0][0] != DBNull.Value)
                newID = Convert.ToInt32(dtb.Rows[0][0]) + 1;
            return newID;
        }

        private void SetEntityValue(TestResultTemplate entity)
        {
            if (entity.es.IsAdded)
            {
                entity.TestResultTemplateID = NewID();
                txtTestResultTemplateID.Text = Convert.ToString(entity.TestResultTemplateID);
            }
            else
                entity.TestResultTemplateID = Convert.ToInt32(txtTestResultTemplateID.Text);

            entity.TestResultTemplateName = txtTestResultTemplateName.Text;
            entity.ItemID = cboItemID.SelectedValue;
            entity.str.ParamedicID = cboParamedicID.Text != string.Empty ? cboParamedicID.SelectedValue : string.Empty;
            entity.TestResult = txtTestResult.Content;
            //entity.TestSummary = txtTestSummary.Content;
            //entity.TestSuggest = txtTestSuggest.Content;
            entity.TestResultOtherLang = txtTestResultOtherLang.Content;
            //entity.TestSummaryOtherLang = txtTestSummaryOtherLang.Content;
            //entity.TestSuggestOtherLang = txtTestSuggestOtherLang.Content;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            TestResultTemplateQuery que = new TestResultTemplateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TestResultTemplateID > txtTestResultTemplateID.Text);
                que.OrderBy(que.TestResultTemplateID.Ascending);
            }
            else
            {
                que.Where(que.TestResultTemplateID < txtTestResultTemplateID.Text);
                que.OrderBy(que.TestResultTemplateID.Descending);
            }
            TestResultTemplate entity = new TestResultTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        #region Override Method & Function

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            TestResultTemplate entity = new TestResultTemplate();
            if (parameters.Length > 0)
            {
                Int32 testResultTemplateID = Convert.ToInt32(parameters[0]);
                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(testResultTemplateID);
            }
            else
                entity.LoadByPrimaryKey(Convert.ToInt32(txtTestResultTemplateID.Text));
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var testResultTemplate = (TestResultTemplate)entity;

            txtTestResultTemplateName.Text = testResultTemplate.TestResultTemplateName;
            txtTestResult.Content = testResultTemplate.TestResult;
            //txtTestSummary.Content = testResultTemplate.TestSummary;
            //txtTestSuggest.Content = testResultTemplate.TestSuggest;
            txtTestResultOtherLang.Content = testResultTemplate.TestResultOtherLang;
            //txtTestSummaryOtherLang.Content = testResultTemplate.TestSummaryOtherLang;
            //txtTestSuggestOtherLang.Content = testResultTemplate.TestSuggestOtherLang;

            if (!string.IsNullOrEmpty(testResultTemplate.ItemID))
            {
                var query = new ItemQuery("a");
                query.Where(query.ItemID == testResultTemplate.ItemID);
                cboItemID.DataSource = query.LoadDataTable();
                cboItemID.DataBind();
                cboItemID.SelectedValue = testResultTemplate.ItemID;
            }

            if (!string.IsNullOrEmpty(testResultTemplate.ItemID))
            {
                var query = new ParamedicQuery("a");
                query.Where(query.ParamedicID == testResultTemplate.ParamedicID);
                cboParamedicID.DataSource = query.LoadDataTable();
                cboParamedicID.DataBind();
                cboParamedicID.SelectedValue = testResultTemplate.ParamedicID;
            }
            
            txtTestResultTemplateID.Text = testResultTemplate.es.IsAdded ? Convert.ToString(NewID()) : Convert.ToString(testResultTemplate.TestResultTemplateID);
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new TestResultTemplate());
            cboItemID.Items.Clear();
            cboItemID.Text = string.Empty;
            cboParamedicID.Items.Clear();
            cboParamedicID.Text = string.Empty;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = "TestResultTemplateID='" + txtTestResultTemplateID.Text.Trim() + "'";
            auditLogFilter.TableName = "TestResultTemplate";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            txtTestResultTemplateID.Enabled = (newVal == AppEnum.DataMode.New);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Url Search & List
            UrlPageSearch = "TestResultTemplateSearch.aspx";
            UrlPageList = "TestResultTemplateList.aspx";

            ProgramID = AppConstant.Program.TestResultTemplate;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            TestResultTemplate entity = new TestResultTemplate();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtTestResultTemplateID.Text)))
            {
                entity.MarkAsDeleted();
                using (var trans = new esTransactionScope())
                {
                    entity.Save();
                    //SaveEntity(entity);
                    trans.Complete();
                }
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var item = new Item();
            if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                args.MessageText = "Selected item not valid, please select exist item.";
                args.IsCancel = true;
                return;
            }

            var paramedic = new Paramedic();
            if (!paramedic.LoadByPrimaryKey(cboParamedicID.SelectedValue))
            {
                args.MessageText = "Selected paramedic not valid, please select exist item.";
                args.IsCancel = true;
                return;
            }

            var entity = new TestResultTemplate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        private void SaveEntity(TestResultTemplate entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
            txtTestResultTemplateID.Text = entity.TestResultTemplateID.ToString();
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var item = new Item();
            if (!item.LoadByPrimaryKey(cboItemID.SelectedValue))
            {
                args.MessageText = "Selected item not valid, please select exist item.";
                args.IsCancel = true;
                return;
            }

            var paramedic = new Paramedic();
            if (!paramedic.LoadByPrimaryKey(cboParamedicID.SelectedValue))
            {
                args.MessageText = "Selected paramedic not valid, please select exist item.";
                args.IsCancel = true;
                return;
            }

            var entity = new TestResultTemplate();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtTestResultTemplateID.Text)))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
        }

        #endregion

        #region ComboBox ItemID

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            //var rad = new ItemRadiologyQuery("b");
            query.Where
                (
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true, 
                    query.IsHasTestResults == true
                );
            query.Select
                        (
                            query.ItemID,
                            query.ItemName
                        );
            //query.InnerJoin(rad).On(query.ItemID == rad.ItemID);

            query.es.Top = 20;
            query.OrderBy(query.ItemID.Ascending);

            var dtb = query.LoadDataTable();
            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }


        #endregion
        
        #region ComboBox ParamedicID

        protected void cboParamedicID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.Where
                (
                    query.Or
                        (
                            query.ParamedicID == e.Text,
                            query.ParamedicName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            query.Select(query.ParamedicID, query.ParamedicName);
            DataTable dtb = query.LoadDataTable();

            cboParamedicID.DataSource = dtb;
            cboParamedicID.DataBind();
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ParamedicItemDataBound(e);
        }


        #endregion
    }
}