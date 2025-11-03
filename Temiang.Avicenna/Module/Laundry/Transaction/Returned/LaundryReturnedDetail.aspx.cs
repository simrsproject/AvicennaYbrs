using System;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaundryReturnedDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "LaundryReturnedSearch.aspx";
            UrlPageList = "LaundryReturnedList.aspx";

            ProgramID = AppConstant.Program.LaundryReturned;

            this.WindowSearch.Height = 400;
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
            OnPopulateEntryControl(new LaundryReturned());

            txtReturnNo.Text = GetNewReturnNo();
            txtReturnDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtReturnTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");

            btnGetPickList.Enabled = true;
            btnResetItem.Enabled = true;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new LaundryReturned();
            //if (entity.LoadByPrimaryKey(txtReturnNo.Text))
            //{
            //    entity.MarkAsDeleted();

            //    SaveEntity(entity);
            //}
            //else
            //    args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            if (LaundryReturnedItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryReturned();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (LaundryReturnedItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new LaundryReturned();
            if (entity.LoadByPrimaryKey(txtReturnNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.TableName = "LaundryReturned";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new LaundryReturned();
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

        private bool IsApprovedOrVoid(LaundryReturned entity, ValidateArgs args)
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
            btnGetPickList.Enabled = newVal != AppEnum.DataMode.Read;
            btnResetItem.Enabled = newVal != AppEnum.DataMode.Read;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new LaundryReturned();
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
            var ret = (LaundryReturned)entity;

            txtReturnNo.Text = ret.ReturnNo;
            txtReturnDate.SelectedDate = ret.ReturnDate;
            txtReturnTime.Text = ret.ReturnTime;

            if (!string.IsNullOrEmpty(ret.ToServiceUnitID))
            {
                var u = new ServiceUnitQuery();
                u.Where(u.ServiceUnitID == ret.ToServiceUnitID);
                cboToServiceUnitID.DataSource = u.LoadDataTable();
                cboToServiceUnitID.DataBind();
                cboToServiceUnitID.SelectedValue = ret.ToServiceUnitID;
            }
            else
            {
                cboToServiceUnitID.Items.Clear();
                cboToServiceUnitID.Text = string.Empty;
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

            btnGetPickList.Enabled = false;
            btnResetItem.Enabled = false;

            PopulateItemGrid();

            ViewState["IsApproved"] = ret.IsApproved ?? false;
            ViewState["IsVoid"] = ret.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new LaundryReturned();
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
            var entity = new LaundryReturned();
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

        private void SetApproval(LaundryReturned entity, bool isApproval, ValidateArgs args)
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

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new LaundryReturned();
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
            var entity = new LaundryReturned();
            if (!entity.LoadByPrimaryKey(txtReturnNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(LaundryReturned entity, bool isVoid)
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

        private void SetEntityValue(LaundryReturned entity)
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
            entity.ToServiceUnitID = cboToServiceUnitID.SelectedValue;
            entity.HandedByUserID = cboHandedByUserID.SelectedValue;
            entity.ReceivedBy = txtReceivedBy.Text;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in LaundryReturnedItems)
            {
                item.ReturnNo = txtReturnNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(LaundryReturned entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                LaundryReturnedItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new LaundryReturnedQuery();
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

            var entity = new LaundryReturned();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Blood Bank Transaction Item

        private LaundryReturnedItemCollection LaundryReturnedItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLaundryReturnedItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LaundryReturnedItemCollection)(obj));
                    }
                }

                var coll = new LaundryReturnedItemCollection();
                var query = new LaundryReturnedItemQuery("a");
                var iq = new ItemQuery("d");
                var inm = new ItemProductNonMedicQuery("f");
                var unitq = new AppStandardReferenceItemQuery("e");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_ItemUnit")
                    );
                query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
                query.InnerJoin(inm).On(inm.ItemID == query.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == inm.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ReturnNo == txtReturnNo.Text);
                query.OrderBy(query.ReturnSeqNo.Ascending);
                coll.Load(query);

                Session["collLaundryReturnedItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLaundryReturnedItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            LaundryReturnedItems = null; //Reset Record Detail
            grdItem.DataSource = LaundryReturnedItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private LaundryReturnedItem FindItem(String seqNo)
        {
            LaundryReturnedItemCollection coll = LaundryReturnedItems;
            LaundryReturnedItem retEntity = null;
            foreach (LaundryReturnedItem rec in coll)
            {
                if (rec.ReturnSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = LaundryReturnedItems;
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][LaundryReturnedItemMetadata.ColumnNames.ReturnSeqNo]);
            LaundryReturnedItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        #endregion

        #region Combobox
        protected void cboHandedByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitLaundryID, e.Text);
        }

        protected void cboHandedByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }

        protected void cboToServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboToServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ServiceUnitItemDataBound(e);
        }
        #endregion

        private string GetNewReturnNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.LaundryReturnNo);

            return _autoNumber.LastCompleteNumber;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid))
                return;

            RadGrid grd = (RadGrid)sourceControl;
            switch (grd.ID)
            {
                case "grdItem":
                    grdItem.Rebind();
                    break;
            }
        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (LaundryReturnedItems.Count > 0)
                LaundryReturnedItems.MarkAllAsDeleted();
            grdItem.DataSource = LaundryReturnedItems;
            grdItem.DataBind();
        }
    }
}
