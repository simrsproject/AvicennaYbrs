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
    public partial class ReturnDistributionDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ReturnDistributionSearch.aspx";
            UrlPageList = "ReturnDistributionList.aspx";

            ProgramID = AppConstant.Program.LaundryReturnDistribution;

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
            OnPopulateEntryControl(new LaundryReturnDistribution());

            txtReturnNo.Text = GetNewReturnNo();
            txtReturnDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtReturnTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new LaundryReturnDistribution();
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
            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "From Unit required.";
                args.IsCancel = true;
                return;
            }

            if (LaundryReturnDistributionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryReturnDistribution();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                args.MessageText = "From Unit required.";
                args.IsCancel = true;
                return;
            }

            if (LaundryReturnDistributionItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryReturnDistribution();
            if (entity.LoadByPrimaryKey(txtReturnNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("ReturnNo='{0}'", txtReturnNo.Text.Trim());
            auditLogFilter.TableName = "LaundryReturnDistribution";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new LaundryReturnDistribution();
            if (entity.LoadByPrimaryKey(txtReturnNo.Text))
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

        private bool IsApprovedOrVoid(LaundryReturnDistribution entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_ReturnNo", txtReturnNo.Text);
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
            var entity = new LaundryReturnDistribution();
            if (parameters.Length > 0)
            {
                var receivedNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(receivedNo);
            }
            else
                entity.LoadByPrimaryKey(txtReturnNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var ret = (LaundryReturnDistribution)entity;

            txtReturnNo.Text = ret.ReturnNo;
            txtReturnDate.SelectedDate = ret.ReturnDate;
            txtReturnTime.Text = ret.ReturnTime;
            if (!string.IsNullOrEmpty(ret.FromServiceUnitID))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitID == ret.FromServiceUnitID);
                cboFromServiceUnitID.DataSource = q.LoadDataTable();
                cboFromServiceUnitID.DataBind();
                cboFromServiceUnitID.SelectedValue = ret.FromServiceUnitID;
            }
            else
            {
                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Text = string.Empty;
            }
            if (!string.IsNullOrEmpty(ret.HandedByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == ret.HandedByUserID);
                cboHandedByUserID.DataSource = usr.LoadDataTable();
                cboHandedByUserID.DataBind();
                cboHandedByUserID.SelectedValue = ret.HandedByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboHandedByUserID.DataSource = usr.LoadDataTable();
                cboHandedByUserID.DataBind();
                cboHandedByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }
            txtReceivedBy.Text = ret.ReceivedBy;

            PopulateItemGrid();

            ViewState["IsApproved"] = ret.IsApproved ?? false;
            ViewState["IsVoid"] = ret.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new LaundryReturnDistribution();
            if (!entity.LoadByPrimaryKey(txtReturnNo.Text))
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
            var entity = new LaundryReturnDistribution();
            if (!entity.LoadByPrimaryKey(txtReturnNo.Text))
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

        private void SetApproval(LaundryReturnDistribution entity, bool isApproval, ValidateArgs args)
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
                        LaundryItemBalance.PrepareBalancesForReturn(LaundryReturnDistributionItems, entity.FromServiceUnitID, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
                            ref balancesFrom, ref balancesTo, ref movements);
                    else
                        LaundryItemBalance.PrepareBalancesForReturnReverse(LaundryReturnDistributionItems, entity.FromServiceUnitID, AppSession.Parameter.ServiceUnitLaundryID, AppSession.UserLogin.UserID,
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
            var entity = new LaundryReturnDistribution();
            if (!entity.LoadByPrimaryKey(txtReturnNo.Text))
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
            var entity = new LaundryReturnDistribution();
            if (!entity.LoadByPrimaryKey(txtReturnNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(LaundryReturnDistribution entity, bool isVoid)
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

        private void SetEntityValue(LaundryReturnDistribution entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtReturnNo.Text = GetNewReturnNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.ReturnNo = txtReturnNo.Text;
            entity.ReturnDate = txtReturnDate.SelectedDate;
            entity.ReturnTime = txtReturnTime.TextWithLiterals;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.HandedByUserID = cboHandedByUserID.SelectedValue;
            entity.ReceivedBy = txtReceivedBy.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in LaundryReturnDistributionItems)
            {
                item.ReturnNo = txtReturnNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(LaundryReturnDistribution entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                LaundryReturnDistributionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundryReturnDistributionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.ReturnNo > txtReturnNo.Text
                    );
                que.OrderBy(que.ReturnNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.ReturnNo < txtReturnNo.Text
                    );
                que.OrderBy(que.ReturnNo.Descending);
            }

            var entity = new LaundryReturnDistribution();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of LaundryReturnDistributionItem

        private LaundryReturnDistributionItemCollection LaundryReturnDistributionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryReturnDistributionItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryReturnDistributionItemCollection)(obj));
                    }
                }

                var coll = new LaundryReturnDistributionItemCollection();
                var query = new LaundryReturnDistributionItemQuery("a");
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
                query.Where(query.ReturnNo == txtReturnNo.Text);
                query.OrderBy(query.SeqNo.Ascending);
                coll.Load(query);

                Session["collLaundryReturnDistributionItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaundryReturnDistributionItem" + Request.UserHostName] = value;
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
            LaundryReturnDistributionItems = null; //Reset Record Detail
            grdItem.DataSource = LaundryReturnDistributionItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LaundryReturnDistributionItem FindItem(String seqNo)
        {
            LaundryReturnDistributionItemCollection coll = LaundryReturnDistributionItems;
            LaundryReturnDistributionItem retEntity = null;
            foreach (LaundryReturnDistributionItem rec in coll)
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
            grdItem.DataSource = LaundryReturnDistributionItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][LaundryReturnDistributionItemMetadata.ColumnNames.SeqNo]);
            LaundryReturnDistributionItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryReturnDistributionItemMetadata.ColumnNames.SeqNo]);
            LaundryReturnDistributionItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            LaundryReturnDistributionItem entity = LaundryReturnDistributionItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(LaundryReturnDistributionItem entity, GridCommandEventArgs e)
        {
            var userControl = (ReturnDistributionDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
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
        protected void cboFromServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery("a");
            var usr = new AppUserServiceUnitQuery("b");
            query.InnerJoin(usr).On(usr.ServiceUnitID == query.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);

            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.Where
                (
                    query.Or
                        (
                            query.ServiceUnitID == e.Text,
                            query.ServiceUnitName.Like(searchTextContain)
                        ),
                    query.IsActive == true
                );

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();

            cboFromServiceUnitID.DataSource = dtb;
            cboFromServiceUnitID.DataBind();
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void HandedByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppUserQuery("a");

            query.Select(query.UserID, query.UserName);
            query.Where(query.UserID == AppSession.UserLogin.UserID);

            query.es.Top = 20;
            
            DataTable dtb = query.LoadDataTable();

            cboHandedByUserID.DataSource = dtb;
            cboHandedByUserID.DataBind();
        }

        protected void HandedByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }
        #endregion

        private string GetNewReturnNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.LaundryReturnNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}