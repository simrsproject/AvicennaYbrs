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
    public partial class BloodExterminationDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "BloodExterminationSearch.aspx";
            UrlPageList = "BloodExterminationList.aspx";

            ProgramID = AppConstant.Program.BloodStockExtermination;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
                StandardReference.InitializeIncludeSpace(cboSRReasonsForExtermination, AppEnum.StandardReference.ReasonsForExtermination);

        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new BloodExtermination());

            txtTransactionNo.Text = GetNewTransactionNo();
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
            cboSRReasonsForExtermination.SelectedValue = string.Empty;
            cboOfficerByUserID.Items.Clear();
            cboOfficerByUserID.Text = string.Empty;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new BloodExtermination();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new BloodExtermination();
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
            auditLogFilter.TableName = "BloodExtermination";
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
            var entity = new BloodExtermination();
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
            var trans = (BloodExtermination)entity;

            txtTransactionNo.Text = trans.TransactionNo;
            txtTransactionDate.SelectedDate = trans.TransactionDate;
            cboSRReasonsForExtermination.SelectedValue = trans.SRReasonsForExtermination;
            txtWeight.Value = Convert.ToDouble(trans.Weight);
            if (!string.IsNullOrEmpty(trans.BloodBankOfficerByUserID))
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == trans.BloodBankOfficerByUserID);
                cboOfficerByUserID.DataSource = usr.LoadDataTable();
                cboOfficerByUserID.DataBind();
                cboOfficerByUserID.SelectedValue = trans.BloodBankOfficerByUserID;
            }
            else
            {
                var usr = new AppUserQuery();
                usr.Where(usr.UserID == AppSession.UserLogin.UserID);
                cboOfficerByUserID.DataSource = usr.LoadDataTable();
                cboOfficerByUserID.DataBind();
                cboOfficerByUserID.SelectedValue = AppSession.UserLogin.UserID;
            }
            txtIncineratorOperator.Text = trans.IncineratorOperator;
            txtKnownBy.Text = trans.KnownBy;
            txtNotes.Text = trans.Notes;

            PopulateItemGrid();

            ViewState["IsApproved"] = trans.IsApproved ?? false;
            ViewState["IsVoid"] = trans.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new BloodExtermination();
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

            SetApproval(entity, true, args);
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
        }

        private void SetApproval(BloodExtermination entity, bool isApproval, ValidateArgs args)
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

                var bagnos = (BloodExterminationItems.Select(i => i.BagNo)).Distinct();

                var bloodBagnos = new BloodBagNoCollection();
                bloodBagnos.Query.Where(bloodBagnos.Query.BagNo.In(bagnos));
                bloodBagnos.LoadAll();

                foreach (var bagno in bloodBagnos)
                {
                    bagno.IsExtermination = true;
                    bagno.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    bagno.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                bloodBagnos.Save();

                var bloodBalances = new BloodBalanceCollection();
                bloodBalances.Query.Where(bloodBalances.Query.BagNo.In(bagnos));
                bloodBalances.LoadAll();
                foreach (var balance in bloodBalances)
                {
                    balance.Balance = 0;
                    balance.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    balance.LastUpdateByUserID = AppSession.UserLogin.UserID;
                }

                bloodBalances.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new BloodExtermination();
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

        private void SetVoid(BloodExtermination entity, bool isVoid)
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

        private void SetEntityValue(BloodExtermination entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = GetNewTransactionNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.SRReasonsForExtermination = cboSRReasonsForExtermination.SelectedValue;
            entity.Weight = Convert.ToDecimal(txtWeight.Value);
            entity.BloodBankOfficerByUserID = cboOfficerByUserID.SelectedValue;
            entity.IncineratorOperator = txtIncineratorOperator.Text;
            entity.KnownBy = txtKnownBy.Text;
            entity.Notes = txtNotes.Text;

            entity.IsApproved = false;
            entity.IsVoid = false;

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            foreach (var item in BloodExterminationItems)
            {
                item.TransactionNo = txtTransactionNo.Text;
                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }
        }

        private void SaveEntity(BloodExtermination entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                BloodExterminationItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new BloodExterminationQuery();
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

            var entity = new BloodExtermination();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of Blood Bank Transaction Item

        private BloodExterminationItemCollection BloodExterminationItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collBloodExterminationItem"];
                    if (obj != null)
                    {
                        return ((BloodExterminationItemCollection)(obj));
                    }
                }

                var coll = new BloodExterminationItemCollection();
                var query = new BloodExterminationItemQuery("a");
                var bn = new BloodBagNoQuery("b");
                var bt = new AppStandardReferenceItemQuery("c");
                var bg = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query,
                        bt.ItemName.As("refToAppStandardReferenceItem_BloodType"),
                        bn.BloodRhesus.As("refToBloodBagNo_BloodRhesus"),
                        bg.ItemName.As("refToAppStandardReferenceItem_BloodGroup"),
                        bn.VolumeBag.Coalesce("0").As("refBloodBagNo_VolumeBag"),
                        @"<LEFT(CONVERT(VARCHAR, b.ExpiredDateTime, 113), 17) AS 'refToBloodBagNo_ExpiredDateTime'>"
                    );
                query.InnerJoin(bn).On(bn.BagNo == query.BagNo);
                query.InnerJoin(bt).On(bt.StandardReferenceID == AppEnum.StandardReference.BloodType && bt.ItemID == bn.SRBloodType);
                query.InnerJoin(bg).On(bg.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bg.ItemID == query.SRBloodGroup);
                query.Where(query.TransactionNo == txtTransactionNo.Text);

                coll.Load(query);

                Session["collBloodExterminationItem"] = coll;
                return coll;
            }
            set
            {
                Session["collBloodExterminationItem"] = value;
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
            BloodExterminationItems = null; //Reset Record Detail
            grdItem.DataSource = BloodExterminationItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        private BloodExterminationItem FindItem(String bagNo)
        {
            BloodExterminationItemCollection coll = BloodExterminationItems;
            BloodExterminationItem retEntity = null;
            foreach (BloodExterminationItem rec in coll)
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
            grdItem.DataSource = BloodExterminationItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            var editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String bagNo =
                Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][BloodExterminationItemMetadata.ColumnNames.BagNo]);
            BloodExterminationItem entity = FindItem(bagNo);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            String bagNo =
                Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BloodExterminationItemMetadata.ColumnNames.BagNo]);
            BloodExterminationItem entity = FindItem(bagNo);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            BloodExterminationItem entity = BloodExterminationItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdItem.Rebind();
        }

        private void SetEntityValue(BloodExterminationItem entity, GridCommandEventArgs e)
        {
            var userControl = (BloodExterminationItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.BagNo = userControl.BagNo;
                entity.BloodType = userControl.BloodType;
                entity.BloodRhesus = userControl.BloodRhesus == "0" ? "+" : "-";
                entity.SRBloodGroup = userControl.SrBloodGroup;
                entity.BloodGroup = userControl.BloodGroup;
                entity.ExpiredDateTime = userControl.ExpiredDate;
                entity.VolumeBag = userControl.VolumeBag;
            }
        }

        #endregion

        protected void cboOfficerByUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitBloodBankID, e.Text);
        }

        protected void cboOfficerByUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }

        private string GetNewTransactionNo()
        {
            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.BloodExterminationNo);

            return _autoNumber.LastCompleteNumber;
        }
    }
}
