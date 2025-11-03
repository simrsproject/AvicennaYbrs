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

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterileItemsRequestDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "SterileItemsRequestSearch.aspx";
            UrlPageList = "SterileItemsRequestList.aspx";

            ProgramID = AppConstant.Program.CssdSterileItemsRequest;

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
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromServiceUnitID);
            ajax.AddAjaxSetting(cboFromServiceUnitID, cboFromRoomID);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CssdSterileItemsRequest());

            txtRequestNo.Text = GetNewRequestNo();
            txtReceivedDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            txtSenderBy.Text = AppSession.UserLogin.UserName;
            
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            //var entity = new CssdSterileItemsRequest();
            //if (entity.LoadByPrimaryKey(txtReceivedNo.Text))
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

            if (CssdSterileItemsRequestItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterileItemsRequest();
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

            if (CssdSterileItemsRequestItems.Count == 0)
            {
                args.MessageText = AppConstant.Message.RecordDetailEmpty;
                args.IsCancel = true;
                return;
            }

            var entity = new CssdSterileItemsRequest();
            if (entity.LoadByPrimaryKey(txtRequestNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("RequestNo='{0}'", txtRequestNo.Text.Trim());
            auditLogFilter.TableName = "CssdSterileItemsRequest";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsRequest();
            if (entity.LoadByPrimaryKey(txtRequestNo.Text))
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

        private bool IsApprovedOrVoid(CssdSterileItemsRequest entity, ValidateArgs args)
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
            printJobParameters.AddNew("p_RequestNo", txtRequestNo.Text);
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
            var entity = new CssdSterileItemsRequest();
            if (parameters.Length > 0)
            {
                var reqNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(reqNo);
            }
            else
                entity.LoadByPrimaryKey(txtRequestNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var received = (CssdSterileItemsRequest)entity;

            txtRequestNo.Text = received.RequestNo;
            txtReceivedDate.SelectedDate = received.RequestDate;
            if (!string.IsNullOrEmpty(received.FromServiceUnitID))
            {
                var q = new ServiceUnitQuery();
                q.Where(q.ServiceUnitID == received.FromServiceUnitID);
                cboFromServiceUnitID.DataSource = q.LoadDataTable();
                cboFromServiceUnitID.DataBind();
                cboFromServiceUnitID.SelectedValue = received.FromServiceUnitID;

                PopulateRoomList(received.FromServiceUnitID, false);
                cboFromRoomID.SelectedValue = received.FromRoomID;
            }
            else
            {
                cboFromServiceUnitID.Items.Clear();
                cboFromServiceUnitID.Text = string.Empty;
                cboFromRoomID.Items.Clear();
                cboFromRoomID.Text = string.Empty;
            }

            txtSenderBy.Text = received.SenderBy;

            PopulateItemGrid();

            ViewState["IsApproved"] = received.IsApproved ?? false;
            ViewState["IsVoid"] = received.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CssdSterileItemsRequest();
            if (!entity.LoadByPrimaryKey(txtRequestNo.Text))
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
            var entity = new CssdSterileItemsRequest();
            if (!entity.LoadByPrimaryKey(txtRequestNo.Text))
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

            var received = new CssdSterileItemsReceivedCollection();
            received.Query.Where(received.Query.ProductionNo == txtRequestNo.Text, received.Query.IsVoid == false);
            received.LoadAll();
            if (received.Count > 0)
            {
                args.MessageText = "Sterilization request data has been processed.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(CssdSterileItemsRequest entity, bool isApproval, ValidateArgs args)
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
            var entity = new CssdSterileItemsRequest();
            if (!entity.LoadByPrimaryKey(txtRequestNo.Text))
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
            var entity = new CssdSterileItemsRequest();
            if (!entity.LoadByPrimaryKey(txtRequestNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(CssdSterileItemsRequest entity, bool isVoid)
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

        private void SetEntityValue(CssdSterileItemsRequest entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtRequestNo.Text = GetNewRequestNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.RequestNo = txtRequestNo.Text;
            entity.RequestDate = txtReceivedDate.SelectedDate;
            entity.FromServiceUnitID = cboFromServiceUnitID.SelectedValue;
            entity.FromRoomID = cboFromRoomID.SelectedValue;
            entity.SenderByID = AppSession.Parameter.CssdSenderByOtherUnit;
            entity.SenderBy = txtSenderBy.Text;
            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in CssdSterileItemsRequestItems)
            {
                item.RequestNo = txtRequestNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(CssdSterileItemsRequest entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                CssdSterileItemsRequestItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CssdSterileItemsRequestQuery("a");
            var usr = new AppUserServiceUnitQuery("b");
            que.InnerJoin(usr).On(usr.ServiceUnitID == que.FromServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);

            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where
                    (
                        que.RequestNo > txtRequestNo.Text
                    );
                que.OrderBy(que.RequestNo.Ascending);
            }
            else
            {
                que.Where
                    (
                        que.RequestNo < txtRequestNo.Text
                    );
                que.OrderBy(que.RequestNo.Descending);
            }

            var entity = new CssdSterileItemsRequest();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of SterileItemsReceived

        private CssdSterileItemsRequestItemCollection CssdSterileItemsRequestItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCssdSterileItemsRequestItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((CssdSterileItemsRequestItemCollection)(obj));
                    }
                }

                var coll = new CssdSterileItemsRequestItemCollection();
                var query = new CssdSterileItemsRequestItemQuery("a");
                var iq = new ItemQuery("b");
                var unitq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        iq.ItemName.As("refToCssdItem_ItemName"),
                        unitq.ItemName.As("refToAppStandardReferenceItem_CssdItemUnit")

                    );
                query.InnerJoin(iq).On(query.ItemID == iq.ItemID);
                query.InnerJoin(unitq).On(query.SRCssdItemUnit == unitq.ItemID &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.RequestNo == txtRequestNo.Text);
                query.OrderBy(query.RequestSeqNo.Ascending);
                coll.Load(query);

                Session["collCssdSterileItemsRequestItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collCssdSterileItemsRequestItem" + Request.UserHostName] = value;
            }
        }

        private void RefreshCommandItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;
            grdItem.Columns[grdItem.Columns.Count - 1].Visible = isVisible;

            grdItem.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private void PopulateItemGrid()
        {
            //Display Data Detail
            CssdSterileItemsRequestItems = null; //Reset Record Detail
            grdItem.DataSource = CssdSterileItemsRequestItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private CssdSterileItemsRequestItem FindItem(String seqNo)
        {
            CssdSterileItemsRequestItemCollection coll = CssdSterileItemsRequestItems;
            CssdSterileItemsRequestItem retEntity = null;
            foreach (CssdSterileItemsRequestItem rec in coll)
            {
                if (rec.RequestSeqNo.Equals(seqNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = CssdSterileItemsRequestItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String seqNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][CssdSterileItemsRequestItemMetadata.ColumnNames.RequestSeqNo]);
            CssdSterileItemsRequestItem entity = FindItem(seqNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String seqNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CssdSterileItemsRequestItemMetadata.ColumnNames.RequestSeqNo]);
            CssdSterileItemsRequestItem entity = FindItem(seqNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            CssdSterileItemsRequestItem entity = CssdSterileItemsRequestItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(CssdSterileItemsRequestItem entity, GridCommandEventArgs e)
        {
            var userControl = (SterileItemsRequestDetailItem)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.RequestSeqNo = userControl.RequestSeqNo;
                entity.ItemID = userControl.ItemID;
                entity.ItemName = userControl.ItemName;
                entity.Qty = userControl.Qty;
                entity.SRCssdItemUnit = userControl.SRCssdItemUnit;
                entity.CssdItemUnit = userControl.CssdItemUnitName;
                entity.Notes = userControl.Notes;
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
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            DataTable dtb = query.LoadDataTable();

            cboFromServiceUnitID.DataSource = dtb;
            cboFromServiceUnitID.DataBind();
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboFromServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
                PopulateRoomList(e.Value, true);
            else
            {
                cboFromRoomID.Items.Clear();
                cboFromRoomID.Text = string.Empty;
            }
        }

        private void PopulateRoomList(string unitId, bool isNew)
        {
            cboFromRoomID.Items.Clear();

            var sr = new ServiceRoomCollection();
            var srQ = new ServiceRoomQuery("a");

            srQ.Select(srQ.RoomID, srQ.RoomName);
            srQ.Where(srQ.ServiceUnitID == unitId);
            if (isNew)
                srQ.Where(srQ.IsActive == true);
            srQ.OrderBy(srQ.RoomID.Ascending);
            srQ.es.Distinct = true;

            sr.Load(srQ);

            cboFromRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceRoom entity in sr)
            {
                cboFromRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
            }
        }
        #endregion

        private string GetNewRequestNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CssdRequestNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}