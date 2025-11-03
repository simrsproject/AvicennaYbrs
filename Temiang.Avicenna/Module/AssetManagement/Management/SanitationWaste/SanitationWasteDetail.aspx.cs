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

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationWasteDetail : BasePageDetail
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SanitationWasteSearch.aspx?type=" + getPageID;
            UrlPageList = "SanitationWasteList.aspx?type=" + getPageID;

            ProgramID = getPageID == "rec" ? AppConstant.Program.SanitationWasteReceipt : AppConstant.Program.SanitationWasteDisposal;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                trFromServiceUnitID.Visible = getPageID == "rec";
                trSupplierID.Visible = getPageID == "dis";
                trGetPicklist.Visible = getPageID == "dis";

                grdItem.Columns.FindByUniqueName("SRWasteType").Visible = getPageID == "rec";
                grdItem.Columns.FindByUniqueName("ReferenceNo").Visible = getPageID == "dis";
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
            //ajax.AddAjaxSetting(grdItem, grdItem);
            //ajax.AddAjaxSetting(AjaxManager, grdItem);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new SanitationWasteTrans());

            txtTransactionNo.Text = GetNewProcessNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (SanitationWasteTransItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new SanitationWasteTrans();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (SanitationWasteTransItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new SanitationWasteTrans();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "SanitationWasteTrans";
        }

        #endregion

        #region ToolBar Menu Support

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
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
            var entity = new SanitationWasteTrans();
            if (parameters.Length > 0)
            {
                var transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var process = (SanitationWasteTrans)entity;

            txtTransactionNo.Text = process.TransactionNo;
            txtTransactionDate.SelectedDate = process.TransactionDate;
            if (!string.IsNullOrEmpty(process.FromServiceUnitID))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitID == process.FromServiceUnitID);
                cboFromServiceUnitID.DataSource = q.LoadDataTable();
                cboFromServiceUnitID.DataBind();
                cboFromServiceUnitID.SelectedValue = process.FromServiceUnitID;
            }
            else
            {
                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Text = string.Empty;
                cboFromServiceUnitID.SelectedValue = string.Empty;
            }
            if (!string.IsNullOrEmpty(process.SupplierID))
            {
                var q = new SupplierQuery();
                q.Where(q.SupplierID == process.SupplierID);
                cboSupplierID.DataSource = q.LoadDataTable();
                cboSupplierID.DataBind();
                cboSupplierID.SelectedValue = process.SupplierID;
            }
            else
            {
                cboSupplierID.Items.Clear();
                cboSupplierID.Text = string.Empty;
                cboSupplierID.SelectedValue = string.Empty;
            }

            txtNotes.Text = process.Notes;

            PopulateItemGrid();

            ViewState["IsApproved"] = process.IsApproved ?? false;
            ViewState["IsVoid"] = process.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new SanitationWasteTrans();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

            if (getPageID == "dis" && !AppSession.Parameter.IsAllowSanitationWasteBalanceMinus)
            {
                var x = GetItemWithInsufficientBalance(entity.TransactionNo);
                if (!string.IsNullOrEmpty(x))
                {
                    args.MessageText = "Insufficient balance of item : " + x;
                    args.IsCancel = true;
                    return;
                }
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            var entity = new SanitationWasteTrans();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

        private void SetApproval(SanitationWasteTrans entity, bool isApproval, ValidateArgs args)
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

                var balances = new SanitationWasteItemBalanceCollection();
                var movements = new SanitationWasteItemMovementCollection();

                SanitationWasteItemMovement.PrepareSanitationWasteItemMovement(entity, SanitationWasteTransItems, AppSession.UserLogin.UserID,
                   ref balances, ref movements);

                if (balances != null)
                    balances.Save();
                if (movements != null)
                    movements.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new SanitationWasteTrans();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            var entity = new SanitationWasteTrans();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(SanitationWasteTrans entity, bool isVoid)
        {
            using (var trans = new esTransactionScope())
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

                trans.Complete();
            }
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(SanitationWasteTrans entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewProcessNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.TransactionCode = getPageID == "rec" ? "R" : "D";
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.SupplierID = cboSupplierID.SelectedValue;
            entity.Notes = txtNotes.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in SanitationWasteTransItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(SanitationWasteTrans entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                SanitationWasteTransItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new SanitationWasteTransQuery();
            que.es.Top = 1; // SELECT TOP 1 ..

            if (isNextRecord)
            {
                que.Where
                    (
                        que.TransactionNo > txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.TransactionNo < txtTransactionNo.Text
                    );
                que.OrderBy(que.TransactionNo.Descending);
            }
            if (getPageID == "rec")
                que.Where(que.TransactionCode == "R");
            else
                que.Where(que.TransactionCode == "D");

            var entity = new SanitationWasteTrans();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function of SanitationWasteTransItem

        private SanitationWasteTransItemCollection SanitationWasteTransItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collSanitationWasteTransItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((SanitationWasteTransItemCollection)(obj));
                    }
                }

                var coll = new SanitationWasteTransItemCollection();
                var query = new SanitationWasteTransItemQuery("a");
                var typeq = new AppStandardReferenceItemQuery("b");
                var refq = new SanitationWasteTransQuery("c");
                var unitq = new ServiceUnitQuery("d");
               
                query.Select
                    (
                        query,
                        typeq.ItemName.As("refToStdRef_WasteTypeName"),
                        unitq.ServiceUnitName.As("refToUnit_ServiceUnitName")
                    );
                query.InnerJoin(typeq).On(typeq.StandardReferenceID == AppEnum.StandardReference.WasteType && typeq.ItemID == query.SRWasteType);
                query.LeftJoin(refq).On(refq.TransactionNo == query.ReferenceNo);
                query.LeftJoin(unitq).On(unitq.ServiceUnitID == refq.FromServiceUnitID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.SRWasteType.Ascending, query.ReferenceNo.Ascending);
                coll.Load(query);

                Session["collSanitationWasteTransItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collSanitationWasteTransItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            if (getPageID == "dis")
            {
                grdItem.Columns[0].Visible = false;
                grdItem.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
                
            else
            {
                grdItem.Columns[0].Visible = isVisible;
                grdItem.MasterTableView.CommandItemDisplay = isVisible
                                                             ? GridCommandItemDisplay.Top
                                                             : GridCommandItemDisplay.None;
            }
                
            btnGetPickList.Enabled = isVisible;
            btnResetItem.Enabled = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            SanitationWasteTransItems = null; //Reset Record Detail
            grdItem.DataSource = SanitationWasteTransItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private SanitationWasteTransItem FindItem(String refNo, String id)
        {
            SanitationWasteTransItemCollection coll = SanitationWasteTransItems;
            SanitationWasteTransItem retEntity = null;
            foreach (SanitationWasteTransItem rec in coll)
            {
                if (rec.ReferenceNo.Equals(refNo) && rec.SRWasteType.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = SanitationWasteTransItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String refNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SanitationWasteTransItemMetadata.ColumnNames.ReferenceNo]);
            String id =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][SanitationWasteTransItemMetadata.ColumnNames.SRWasteType]);
            
            SanitationWasteTransItem entity = FindItem(refNo,id);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            String refNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SanitationWasteTransItemMetadata.ColumnNames.ReferenceNo]);
            String id =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][SanitationWasteTransItemMetadata.ColumnNames.SRWasteType]);
            SanitationWasteTransItem entity = FindItem(refNo, id);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            SanitationWasteTransItem entity = SanitationWasteTransItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(SanitationWasteTransItem entity, GridCommandEventArgs e)
        {
            var userControl = (SanitationWasteItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.SRWasteType = userControl.SRWasteType;
                entity.WasteTypeName = userControl.WasteTypeName;
                entity.Qty = userControl.Qty;
                entity.ReferenceNo = userControl.ReferenceNo;
            }
        }

        #endregion

        private string GetNewProcessNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, getPageID == "rec" ? AppEnum.AutoNumber.SanitationWasteReceipt : AppEnum.AutoNumber.SanitationWasteDisposal);

            return _autoNumber.LastCompleteNumber;
        }

        protected string GetItemWithInsufficientBalance(string transNo)
        {
            var retVal = string.Empty;

            var query = new SanitationWasteTransItemQuery("a");
            var ib = new SanitationWasteItemBalanceQuery("b");
            var i = new AppStandardReferenceItemQuery("c");
            query.LeftJoin(ib).On(ib.SRWasteType == query.SRWasteType);
            query.InnerJoin(i).On(i.StandardReferenceID == "WasteType" && i.ItemID == query.SRWasteType);
            query.Where(query.TransactionNo == transNo);
            query.Where(@"<a.Qty > ISNULL(b.Balance, 0)>");

            query.Select(i.ItemName, @"<a.Qty - ISNULL(b.Balance, 0) AS Balance>");
            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (retVal == string.Empty)
                        retVal = row["ItemName"].ToString() + " (- " + String.Format("{0:N2}", Convert.ToDecimal(row["Balance"])) + ")";
                    else
                        retVal += ", " + row["ItemName"].ToString() + " (- " + String.Format("{0:N2}", Convert.ToDecimal(row["Balance"])) + ")";
                }
            }

            return retVal;
        }

        protected void cboFromServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboSupplierID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.SupplierItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (sourceControl is RadGrid)
            {
                if (eventArgument == "rebind")
                    grdItem.Rebind();
            }
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (SanitationWasteTransItems.Count > 0)
                SanitationWasteTransItems.MarkAllAsDeleted();
            grdItem.DataSource = SanitationWasteTransItems;
            grdItem.DataBind();
        }
    }
}