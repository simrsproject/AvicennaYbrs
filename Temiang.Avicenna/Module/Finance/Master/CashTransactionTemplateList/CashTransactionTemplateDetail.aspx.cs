using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class CashTransactionTemplateDetail : BasePageDetail
    {
        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "CashTransactionTemplateSearch.aspx";
            UrlPageList = "CashTransactionTemplateList.aspx";

            ProgramID = AppConstant.Program.CASH_TRANSACTION_LIST;

            //if (!IsPostBack)
            //    StandardReference.InitializeIncludeSpace(cboCashType, AppEnum.StandardReference.CashManagementType);

            ////PopUp Search
            //if (!IsCallback)
            //{
            //}
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CashTransactionTemplate());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //Temiang.Avicenna.Module.Finance.Master.CashTransactionTemplate.
            CashTransactionTemplate entity = new CashTransactionTemplate();
            if (entity.LoadByPrimaryKey(TemplateID))
            {
                entity.MarkAsDeleted();
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            CashTransactionTemplate entity = new CashTransactionTemplate();

            if (entity.LoadByPrimaryKey(TemplateID))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }

            entity = new CashTransactionTemplate();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            CashTransactionTemplate entity = new CashTransactionTemplate();
            if (entity.LoadByPrimaryKey(TemplateID))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
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

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("TemplateId='{0}'", TemplateID);
            auditLogFilter.TableName = "CashTransactionTemplate";
        }
        #endregion

        #region ToolBar Menu Support

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            CashTransactionTemplate entity = new CashTransactionTemplate();
            if (parameters.Length > 0)
            {
                String sTemplateId = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(System.Convert.ToInt32(sTemplateId));
            }
            else
            {
                entity.LoadByPrimaryKey(TemplateID);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            CashTransactionTemplate cs = (CashTransactionTemplate)entity;
            txtTemplateId.Text = cs.TemplateId.ToString();
            txtTemplateName.Text = cs.TemplateName;
            chkIsActive.Checked = cs.IsActive ?? false;

            if (txtTemplateId.Text != string.Empty)
            {
                
            }
            else
            {

            }

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(CashTransactionTemplate entity)
        {
            entity.TemplateName = txtTemplateName.Text;
            entity.IsActive = chkIsActive.Checked;

            //Last Update Status
            if (entity.es.IsAdded)
            {
                entity.CreateByUserID = AppSession.UserLogin.UserID;
                entity.CreateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
            if (entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(CashTransactionTemplate entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();

                foreach (var tDetail in CashTransactionTemplateDetails) {
                    if (tDetail.es.IsAdded)
                    {
                        tDetail.TemplateId = entity.TemplateId;

                        tDetail.CreateByUserID = AppSession.UserLogin.UserID;
                        tDetail.CreateDateTime = DateTime.Now;
                        tDetail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        tDetail.LastUpdateDateTime = DateTime.Now;
                    }
                    if (tDetail.es.IsModified)
                    {
                        tDetail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        tDetail.LastUpdateDateTime = DateTime.Now;
                    }
                }
                CashTransactionTemplateDetails.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            CashTransactionTemplateQuery que = new CashTransactionTemplateQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.TemplateName > txtTemplateName.Text);
                que.OrderBy(que.TemplateName.Ascending);
            }
            else
            {
                que.Where(que.TemplateName < txtTemplateName.Text);
                que.OrderBy(que.TemplateName.Descending);
            }
            CashTransactionTemplate entity = new CashTransactionTemplate();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }


        #endregion

        #region Method & Event TextChanged
        
        #endregion

        private void PopulateGridDetail()
        {
            //Display Data Detail
            CashTransactionTemplateDetails = null; //Reset Record Detail
            grdListItem.DataSource = CashTransactionTemplateDetails;
            grdListItem.MasterTableView.IsItemInserted = false;
            grdListItem.MasterTableView.ClearEditItems();
            grdListItem.DataBind();
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdListItem.Columns[0].Visible = isVisible;
            grdListItem.Columns[grdListItem.Columns.Count - 1].Visible = isVisible;

            grdListItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read) CashTransactionTemplateDetails = null;

            //Perbaharui tampilan dan data
            grdListItem.Rebind();
        }

        private int TemplateID { 
            get {
                return string.IsNullOrEmpty(txtTemplateId.Text) ? -1 : System.Convert.ToInt32(txtTemplateId.Text);
            } 
        }

        private CashTransactionTemplateDetailCollection CashTransactionTemplateDetails
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCashTransactionTemplateDetail"];
                    if (obj != null) return ((CashTransactionTemplateDetailCollection)(obj));
                }

                CashTransactionTemplateDetailCollection coll = new CashTransactionTemplateDetailCollection();
                CashTransactionTemplateDetailQuery query = new CashTransactionTemplateDetailQuery("a");
                ChartOfAccountsQuery coa = new ChartOfAccountsQuery("b");
                SubLedgersQuery sub = new SubLedgersQuery("c");

                query.Select(query,
                    coa.ChartOfAccountCode.As("refToChartOfAccounts_ChartOfAccountCode"),
                    coa.ChartOfAccountName.As("refToChartOfAccounts_ChartOfAccountName"),
                    sub.Description.As("refToSubLedgers_SubLedgerName"));
                query.InnerJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
                query.LeftJoin(sub).On(query.SubLedgerId == sub.SubLedgerId);
                query.Where(query.TemplateId == TemplateID);
                query.OrderBy(query.TemplateDetailId.Ascending);

                coll.Load(query);

                Session["collCashTransactionTemplateDetail"] = coll;
                return coll;
            }
            set { Session["collCashTransactionTemplateDetail"] = value; }
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListItem.DataSource = CashTransactionTemplateDetails;
        }

        protected void grdListItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;
            int detailId = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CashTransactionTemplateDetailMetadata.ColumnNames.TemplateDetailId]);
            BusinessObject.CashTransactionTemplateDetail entity = FindItemGrid(detailId);
            if (entity != null) SetEntityValue(entity, e);
        }

        private BusinessObject.CashTransactionTemplateDetail FindItemGrid(int detailId)
        {
            return CashTransactionTemplateDetails.Where(d => d.TemplateDetailId == detailId).FirstOrDefault();
        }

        protected void grdListItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int detailId = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][CashTransactionTemplateDetailMetadata.ColumnNames.TemplateDetailId]);
            BusinessObject.CashTransactionTemplateDetail entity = FindItemGrid(detailId);
            if (entity != null) entity.MarkAsDeleted();
        }

        protected void grdListItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            BusinessObject.CashTransactionTemplateDetail entity = CashTransactionTemplateDetails.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdListItem.Rebind();
        }

        private void SetEntityValue(BusinessObject.CashTransactionTemplateDetail entity, GridCommandEventArgs e)
        {
            CashTransactionTemplateItem userControl = (CashTransactionTemplateItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TemplateDetailId = userControl.TemplateDetailId;
                //entity.ListId = txtListId.Text;
                entity.ChartOfAccountId = userControl.ChartOfAccountId;
                entity.ChartOfAccountCode = userControl.ChartOfAccountCode;
                entity.ChartOfAccountName = userControl.ChartOfAccountName;
                entity.SubLedgerId = userControl.SubLedgerId;
                entity.SubLedgerName = userControl.SubLedgerName;
                entity.AmountVariablePercentage = userControl.AmountVariablePctg;
                entity.AmountFixed = userControl.AmountFixed;
            }
        }
    }
}
