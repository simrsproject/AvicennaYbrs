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

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class StockReceivedDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "StockReceivedSearch.aspx";
            UrlPageList = "StockReceivedList.aspx";

            ProgramID = AppConstant.Program.BloodStockReceived;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRBloodSource, AppEnum.StandardReference.BloodSource);
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
            ajax.AddAjaxSetting(cboSRBloodSource, cboSRBloodSource);
            ajax.AddAjaxSetting(cboSRBloodSource, cboSRBloodSourceFrom);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new BloodReceived());

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            
            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new BloodReceived();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new BloodReceived();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "BloodReceived";
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
            var entity = new BloodReceived();
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
            var trans = (BloodReceived)entity;

            txtTransactionNo.Text = trans.TransactionNo;
            txtTransactionDate.SelectedDate = trans.TransactionDate;
            if (!string.IsNullOrEmpty(trans.SRBloodSource))
            {
                cboSRBloodSource.SelectedValue = trans.SRBloodSource;
                StandardReference.InitializeIncludeSpace(cboSRBloodSourceFrom, AppEnum.StandardReference.BloodSourceFrom, trans.SRBloodSource);
                if (!string.IsNullOrEmpty(trans.SRBloodSourceFrom))
                    cboSRBloodSourceFrom.SelectedValue = trans.SRBloodSourceFrom;
                else
                {
                    cboSRBloodSourceFrom.SelectedValue = string.Empty;
                    cboSRBloodSourceFrom.Text = string.Empty; 
                }
            }
            else
            {
                cboSRBloodSource.SelectedValue = string.Empty;
                cboSRBloodSource.Text = string.Empty;

                cboSRBloodSourceFrom.Items.Clear();
                cboSRBloodSourceFrom.Text = string.Empty;
            }
            txtNotes.Text = trans.Notes;

            PopulateItemGrid();

            ViewState["IsApproved"] = trans.IsApproved ?? false;
            ViewState["IsVoid"] = trans.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new BloodReceived();
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
            if (string.IsNullOrEmpty(entity.SRBloodSource))
            {
                args.MessageText = "Blood Source required.";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(entity.SRBloodSourceFrom))
            {
                args.MessageText = "Blood Source From required.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        private void SetApproval(BloodReceived entity, bool isApproval, ValidateArgs args)
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

                var bagnos = (BloodReceivedItems.Select(i => i.BagNo)).Distinct();

                var bloodBagnos = new BloodBagNoCollection();
                bloodBagnos.Query.Where(bloodBagnos.Query.BagNo.In(bagnos));
                bloodBagnos.LoadAll();

                foreach (var item in BloodReceivedItems)
                {
                    var bagno = bloodBagnos.SingleOrDefault(ib => ib.BagNo == item.BagNo);
                    if (bagno != null)
                    {
                        bagno.SRBloodType = item.SRBloodType;
                        bagno.BloodRhesus = item.BloodRhesus;
                        bagno.SRBloodGroup = item.SRBloodGroup;
                        bagno.ExpiredDateTime = item.ExpiredDateTime;
                        bagno.IsExtermination = false;
                        bagno.IsCrossMatching = false;
                        bagno.VolumeBag = item.VolumeBag;
                        bagno.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bagno.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    else
                    {
                        bagno = bloodBagnos.AddNew();
                        bagno.BagNo = item.BagNo;
                        bagno.SRBloodType = item.SRBloodType;
                        bagno.BloodRhesus = item.BloodRhesus;
                        bagno.SRBloodGroup = item.SRBloodGroup;
                        bagno.ExpiredDateTime = item.ExpiredDateTime;
                        bagno.IsExtermination = false;
                        bagno.IsCrossMatching = false;
                        bagno.VolumeBag = item.VolumeBag;
                        bagno.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bagno.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                }

                bloodBagnos.Save();

                var bloodBalanceBefores = new BloodBalanceCollection();
                bloodBalanceBefores.Query.Where(bloodBalanceBefores.Query.BagNo.In(bagnos),
                                                bloodBalanceBefores.Query.Balance > 0,
                                                bloodBalanceBefores.Query.Or(
                                                    bloodBalanceBefores.Query.SRBloodSource != entity.SRBloodSource,
                                                    bloodBalanceBefores.Query.SRBloodSourceFrom != entity.SRBloodSourceFrom));
                bloodBalanceBefores.LoadAll();

                var bloodBalances = new BloodBalanceCollection();
                bloodBalances.Query.Where
                (
                    bloodBalances.Query.SRBloodSource == entity.SRBloodSource,
                    bloodBalances.Query.SRBloodSourceFrom == entity.SRBloodSourceFrom,
                    bloodBalances.Query.BagNo.In(bagnos)
                );
                bloodBalances.LoadAll();

                foreach (var item in BloodReceivedItems)
                {
                    var bloodBalanceBefore =
                        bloodBalanceBefores.SingleOrDefault(
                            ib => ib.BagNo == item.BagNo);
                    if (bloodBalanceBefore != null)
                    {
                        bloodBalanceBefore.Balance = 0;
                        bloodBalanceBefore.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        bloodBalanceBefore.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    }
                    
                    var balance =
                        bloodBalances.SingleOrDefault(
                            ib =>
                            ib.SRBloodSource == entity.SRBloodSource && ib.SRBloodSourceFrom == entity.SRBloodSourceFrom &&
                            ib.BagNo == item.BagNo);
                    if (balance == null)
                    {
                        balance = bloodBalances.AddNew();
                        balance.SRBloodSource = entity.SRBloodSource;
                        balance.SRBloodSourceFrom = entity.SRBloodSourceFrom;
                        balance.BagNo = item.BagNo;
                    }
                    balance.Balance = 1;
                    balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    balance.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }
                bloodBalanceBefores.Save();
                bloodBalances.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new BloodReceived();
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
        }

        private void SetVoid(BloodReceived entity, bool isVoid)
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

        private void SetEntityValue(BloodReceived entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.SRBloodSource = cboSRBloodSource.SelectedValue;
            entity.SRBloodSourceFrom = cboSRBloodSourceFrom.SelectedValue;
            entity.Notes = txtNotes.Text;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in BloodReceivedItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(BloodReceived entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                BloodReceivedItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BloodReceivedQuery();
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

            var entity = new BloodReceived();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Blood Bank Transaction Item

        private BloodReceivedItemCollection BloodReceivedItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBloodReceivedItem" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((BloodReceivedItemCollection)(obj));
                    }
                }

                var coll = new BloodReceivedItemCollection();
                var query = new BloodReceivedItemQuery("a");
                var btq = new AppStandardReferenceItemQuery("b");
                var bgq = new AppStandardReferenceItemQuery("c");

                query.Select
                    (
                        query,
                        btq.ItemName.As("refToAppStandardReferenceItem_BloodType"),
                        bgq.ItemName.As("refToAppStandardReferenceItem_BloodGroup")

                    );
                query.InnerJoin(btq).On(query.SRBloodType == btq.ItemID &&
                                        btq.StandardReferenceID == AppEnum.StandardReference.BloodType);
                query.InnerJoin(bgq).On(query.SRBloodGroup == bgq.ItemID &&
                                        bgq.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.TransactionNo.Ascending);
                coll.Load(query);

                Session["collBloodReceivedItem" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collBloodReceivedItem" + Request.UserHostName] = value;
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
            BloodReceivedItems = null; //Reset Record Detail
            grdItem.DataSource = BloodReceivedItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private BloodReceivedItem FindItem(String bagNo)
        {
            BloodReceivedItemCollection coll = BloodReceivedItems;
            BloodReceivedItem retEntity = null;
            foreach (BloodReceivedItem rec in coll)
            {
                if (rec.BagNo.Equals(bagNo))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = BloodReceivedItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String bagNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BloodReceivedItemMetadata.ColumnNames.BagNo]);
            BloodReceivedItem entity = FindItem(bagNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String bagNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BloodReceivedItemMetadata.ColumnNames.BagNo]);
            BloodReceivedItem entity = FindItem(bagNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            BloodReceivedItem entity = BloodReceivedItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(BloodReceivedItem entity, GridCommandEventArgs e)
        {
            var userControl = (StockReceivedItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BagNo = userControl.BagNo;
                entity.SRBloodType = userControl.SrBloodType;
                entity.BloodType = userControl.BloodType;
                entity.BloodRhesus = userControl.BloodRhesus;
                entity.SRBloodGroup = userControl.SrBloodGroup;
                entity.BloodGroup = userControl.BloodGroup;
                entity.VolumeBag = userControl.VolumeBag;
                entity.ExpiredDateTime = userControl.ExpiredDateTime;
            }
        }

        #endregion

        protected void cboSRBloodSource_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRBloodSourceFrom.Items.Clear();
            StandardReference.InitializeIncludeSpace(cboSRBloodSourceFrom, AppEnum.StandardReference.BloodSourceFrom, e.Value);
            cboSRBloodSourceFrom.Text = string.Empty;
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.BloodReceivedNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}
