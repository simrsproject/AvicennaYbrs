using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryDistributionDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LaundryDistributionSearch.aspx";
            UrlPageList = "LaundryDistributionList.aspx";

            ProgramID = AppConstant.Program.LaundryDistribution;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            //ToolBarMenuSearch.Visible = false;
            //ToolBarMenuAdd.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new LaundryDistribution());

            txtDistributionNo.Text = GetNewDistributionNo();
            txtDistributionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtDistributionTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new LaundryDistribution();
            //if (entity.LoadByPrimaryKey(txtDistributionNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                args.MessageText = "To Unit required.";
                args.IsCancel = true;
                return;
            }

            if (LaundryDistributionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryDistribution();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                args.MessageText = "To Unit required.";
                args.IsCancel = true;
                return;
            }

            if (LaundryDistributionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryDistribution();
            if (entity.LoadByPrimaryKey(txtDistributionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
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
            auditLogFilter.PrimaryKeyData = string.Format("DistributionNo='{0}'", txtDistributionNo.Text.Trim());
            auditLogFilter.TableName = "LaundryDistribution";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new LaundryDistribution();
            if (entity.LoadByPrimaryKey(txtDistributionNo.Text))
            {
                if (!IsApprovedOrVoid(entity, args))
                    return;
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
            }
        }

        private bool IsApprovedOrVoid(LaundryDistribution entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_DistributionNo", txtDistributionNo.Text);
            printJobParameters.AddNew("p_UserID", AppSession.UserLogin.UserID);
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return ViewState["IsApproved"] == null ? false : !(bool)ViewState["IsApproved"];
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !(bool)ViewState["IsVoid"];
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItem(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LaundryDistribution();
            if (parameters.Length > 0)
            {
                var receivedNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(receivedNo);
            }
            else
                entity.LoadByPrimaryKey(txtDistributionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var dist = (LaundryDistribution)entity;

            txtDistributionNo.Text = dist.DistributionNo;
            txtDistributionDate.SelectedDate = dist.DistributionDate;
            txtDistributionTime.Text = dist.DistributionTime;
            if (!string.IsNullOrEmpty(dist.ToServiceUnitID))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitID == dist.ToServiceUnitID);
                cboToServiceUnitID.DataSource = q.LoadDataTable();
                cboToServiceUnitID.DataBind();
                cboToServiceUnitID.SelectedValue = dist.ToServiceUnitID;
            }
            else
            {
                cboToServiceUnitID.Items.Clear();
                cboToServiceUnitID.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(dist.HandedByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == dist.HandedByUserID);
                cboHandedByUserID.DataSource = usr.LoadDataTable();
                cboHandedByUserID.DataBind();
                cboHandedByUserID.SelectedValue = dist.HandedByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboHandedByUserID.DataSource = usr.LoadDataTable();
                cboHandedByUserID.DataBind();
                cboHandedByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }
            txtReceivedBy.Text = dist.ReceivedBy;

            PopulateItemGrid();
            
            ViewState["IsApproved"] = dist.IsApproved ?? false;
            ViewState["IsVoid"] = dist.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new LaundryDistribution();
            if (!entity.LoadByPrimaryKey(txtDistributionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new LaundryDistribution();
            if (!entity.LoadByPrimaryKey(txtDistributionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            if (entity.IsApproved == false)
            {
                args.MessageText = AppConstant.Message.RecordHasNotApproved;
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(LaundryDistribution entity, bool isApproval, ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                entity.IsApproved = isApproval;
                if (isApproval)
                {
                    entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                    entity.ApprovedDateTime = (new DateTime()).NowAtSqlServer();
                }
                else
                {
                    entity.ApprovedByUserID = null;
                    entity.ApprovedDateTime = null;
                }

                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                entity.Save();

                if (AppSession.Parameter.IsCentralizedLaundrie)
                {
                    var balancesFrom = new LaundryItemBalanceCollection();
                    var balancesTo = new LaundryItemBalanceCollection();
                    var movements = new LaundryItemMovementCollection();

                    if (isApproval)
                        LaundryItemBalance.PrepareBalancesForDistribution(LaundryDistributionItems, AppSession.Parameter.ServiceUnitLaundryID, entity.ToServiceUnitID, AppSession.UserLogin.UserID,
                            ref balancesFrom, ref balancesTo, ref movements);
                    else
                        LaundryItemBalance.PrepareBalancesForDistributionReverse(LaundryDistributionItems, AppSession.Parameter.ServiceUnitLaundryID, entity.ToServiceUnitID, AppSession.UserLogin.UserID,
                            ref balancesFrom, ref balancesTo, ref movements);

                    if (balancesFrom != null)
                        balancesFrom.Save();
                    if (balancesTo != null)
                        balancesTo.Save();
                    if (movements != null)
                        movements.Save();
                }

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new LaundryDistribution();
            if (!entity.LoadByPrimaryKey(txtDistributionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }
            if (entity.IsVoid ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, true);
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            var entity = new LaundryDistribution();
            if (!entity.LoadByPrimaryKey(txtDistributionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(LaundryDistribution entity, bool isVoid)
        {
            //header
            entity.IsVoid = isVoid;
            if (isVoid)
            {
                entity.VoidByUserID = AppSession.UserLogin.UserID;
                entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            }
            else
            {
                entity.VoidByUserID = null;
                entity.VoidDateTime = null;
            }
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(LaundryDistribution entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtDistributionNo.Text = GetNewDistributionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.DistributionNo = txtDistributionNo.Text;
            entity.DistributionDate = txtDistributionDate.SelectedDate;
            entity.DistributionTime = txtDistributionTime.TextWithLiterals;
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.HandedByUserID = cboHandedByUserID.SelectedValue;
            entity.ReceivedBy = txtReceivedBy.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in LaundryDistributionItems)
            {
                item.DistributionNo = txtDistributionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(LaundryDistribution entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                LaundryDistributionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundryDistributionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.DistributionNo > txtDistributionNo.Text
                    );
                que.OrderBy(que.DistributionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.DistributionNo < txtDistributionNo.Text
                    );
                que.OrderBy(que.DistributionNo.Descending);
            }

            var entity = new LaundryDistribution();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of LaundryDistributionItem

        private LaundryDistributionItemCollection LaundryDistributionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryDistributionItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryDistributionItemCollection)(obj));
                    }
                }

                var coll = new LaundryDistributionItemCollection();
                var query = new LaundryDistributionItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")

                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.DistributionNo == txtDistributionNo.Text);
                query.OrderBy(query.SeqNo.Ascending);
                coll.Load(query);

                Session["collLaundryDistributionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaundryDistributionItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LaundryDistributionItems = null; //Reset Record Detail
            grdItem.DataSource = LaundryDistributionItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LaundryDistributionItem FindItem(String seqNo)
        {
            LaundryDistributionItemCollection coll = LaundryDistributionItems;
            LaundryDistributionItem retEntity = null;
            foreach (LaundryDistributionItem rec in coll)
            {
                if (rec.SeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LaundryDistributionItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundryDistributionItemMetadata.ColumnNames.SeqNo]);
            LaundryDistributionItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryDistributionItemMetadata.ColumnNames.SeqNo]);
            LaundryDistributionItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundryDistributionItem entity = LaundryDistributionItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(LaundryDistributionItem entity, GridCommandEventArgs e)
        {
            var userControl = (LaundryDistributionDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SeqNo = userControl.SeqNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRItemUnit = userControl.SRItemUnit;
                entity.ItemUnit = userControl.ItemUnitName;
            }
        }

        #endregion

        #region Combobox
        protected void cboToServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboToServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void HandedByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitLaundryID, e.Text);
        }

        protected void HandedByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }
        #endregion

        private string GetNewDistributionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.LaundryDistributionNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}