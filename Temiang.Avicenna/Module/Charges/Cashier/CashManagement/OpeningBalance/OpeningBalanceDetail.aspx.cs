using System;
using System.Data;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class OpeningBalanceDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        private void PopulateNewTransactionNo()
        {
            if (DataModeCurrent != AppEnum.DataMode.New) return;

            _autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.CashManagementNo);
            txtTransactionNo.Text = _autoNumber.LastCompleteNumber;
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "OpeningBalanceSearch.aspx";
            UrlPageList = "OpeningBalanceList.aspx";

            ProgramID = AppConstant.Program.CashierOpeningBalance;

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRShift, AppEnum.StandardReference.Shift);
                StandardReference.InitializeIncludeSpace(cboSRCashierCounter, AppEnum.StandardReference.CashierCounter);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_TransactionNo", txtTransactionNo.Text);
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new CashManagement();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new CashManagement();
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
            var entity = new CashManagement();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            var cashier = new CashManagementCashierCollection();
            cashier.Query.Where(cashier.Query.TransactionNo == entity.TransactionNo,
                                cashier.Query.IsCashierCheckin.IsNotNull(), cashier.Query.IsCashierCheckin == true);
            cashier.LoadAll();

            if (cashier.Count > 0)
            {
                args.MessageText = "Cashier already checkin.";
                args.IsCancel = true;
                return;
            }

            SetApproval(entity, false, args);
        }

        private void SetApproval(CashManagement entity, bool isApproval, ValidateArgs args)
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
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
            var entity = new CashManagement();
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

        private void SetVoid(CashManagement entity, bool isVoid)
        {
            entity.IsVoid = isVoid;
            entity.VoidByUserID = AppSession.UserLogin.UserID;
            entity.VoidDateTime = (new DateTime()).NowAtSqlServer();
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            entity.Save();
        }

        private bool IsApprovedOrVoid(CashManagement entity, ValidateArgs args)
        {
            if (entity.IsApproved != null && entity.IsApproved.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            if (entity.IsVoid != null && entity.IsVoid.Value)
            {
                args.MessageText = AppConstant.Message.RecordHasVoided;
                args.IsCancel = true;
                return false;
            }

            var cashier = new CashManagementCashierCollection();
            cashier.Query.Where(cashier.Query.TransactionNo == entity.TransactionNo,
                                cashier.Query.IsCashierCheckin.IsNotNull(), cashier.Query.IsCashierCheckin == true);
            cashier.LoadAll();

            if (cashier.Count > 0)
            {
                args.MessageText = "Cashier already checkin.";
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new CashManagement());
            PopulateNewTransactionNo();
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            var entity = new CashManagement();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                entity.MarkAsDeleted();

                var cashier = new CashManagementCashierCollection();
                cashier.Query.Where(cashier.Query.TransactionNo == txtTransactionNo.Text);
                cashier.MarkAllAsDeleted();

                using (esTransactionScope trans = new esTransactionScope())
                {
                    cashier.Save();
                    entity.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
            }
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            string date = string.Format("{0:dd-MMM-yyyy}", txtOpeningDate.SelectedDate);
            string shift = cboSRShift.Text;
            string counter = cboSRCashierCounter.Text;

            var coll = new CashManagementCollection();
            coll.Query.Where(coll.Query.OpeningBalance.Date() == txtOpeningDate.SelectedDate,
                             coll.Query.SRShift == cboSRShift.SelectedValue,
                             coll.Query.SRCashierCounter == cboSRCashierCounter.SelectedValue);
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "Opening Balance for date: " + date + ", shift: " + shift + ", counter: " + counter + " already exist.";
                args.IsCancel = true;
                return;
            }

            coll = new CashManagementCollection();
            coll.Query.Where(coll.Query.OpeningBalance.Date() == txtOpeningDate.SelectedDate,
                             coll.Query.SRCashierCounter == cboSRCashierCounter.SelectedValue, coll.Query.ClosingDate.IsNull());
            coll.LoadAll();
            if (coll.Count > 0)
            {
                args.MessageText = "There is outstanding Opening Balance for date: " + date + ", counter: " + counter + ".";
                args.IsCancel = true;
                return;
            }

            PopulateNewTransactionNo();
            // save autonumber immediately to decrease time gap between create and save
            _autoNumber.Save();

            var entity = new CashManagement();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.DuplicateKey;
                args.IsCancel = true;
                return;
            }
            entity = new CashManagement();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new CashManagement();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                SetEntityValue(entity);
                SaveEntity(entity);
            }
            else
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
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
            auditLogFilter.TableName = "CashManagement";
        }

        #endregion

        #region ToolBar Menu Support
        public override bool OnGetStatusMenuEdit()
        {
            return txtTransactionNo.Text != string.Empty;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return !chkIsVoid.Checked;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemGrid(oldVal, newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new CashManagement();
            if (parameters.Length > 0)
            {
                String transNo = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(transNo);
            }
            else
            {
                entity.LoadByPrimaryKey(txtTransactionNo.Text);
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var cm = (CashManagement)entity;
            txtTransactionNo.Text = cm.TransactionNo;
            if (!string.IsNullOrEmpty(cm.CreatedByUserID))
            {
                txtCreatedByUserID.Text = cm.CreatedByUserID;
                var usr = new AppUser();
                usr.LoadByPrimaryKey(cm.CreatedByUserID);
                txtCreatedByUserName.Text = usr.UserName;
            }
            else
            {
                txtCreatedByUserID.Text = AppSession.UserLogin.UserID;
                txtCreatedByUserName.Text = AppSession.UserLogin.UserName;
            }
            
            if (cm.OpeningDate != null)
            {
                txtOpeningDate.SelectedDate = cm.OpeningDate.Value.Date;
                txtOpeningTime.Text = cm.OpeningDate.Value.ToString("HH:mm");
            }
            else
            {
                txtOpeningDate.SelectedDate = (new DateTime()).NowAtSqlServer().Date;
                txtOpeningTime.Text = (new DateTime()).NowAtSqlServer().ToString("HH:mm");
            }

            cboSRShift.SelectedValue = cm.SRShift;
            cboSRCashierCounter.SelectedValue = cm.SRCashierCounter;
            txtOpeningBalance.Value = Convert.ToDouble(cm.OpeningBalance);

            if (cm.ClosingDate != null)
            {
                txtClosingDate.SelectedDate = cm.ClosingDate.Value.Date;
                txtClosingTime.Text = cm.ClosingDate.Value.ToString("HH:mm");
                txtClosingBalance.Value = Convert.ToDouble(cm.ClosingBalance);
            }
            else
            {
                txtClosingDate.SelectedDate = null;
                txtClosingTime.Text = "00:00";
                txtClosingBalance.Value = 0;
            }

            chkIsApproved.Checked = cm.IsApproved ?? false;
            chkIsVoid.Checked = cm.IsVoid ?? false;

            //Display Data Detail
            PopulateGridDetail();
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(CashManagement entity)
        {
            entity.TransactionNo = txtTransactionNo.Text;
            entity.OpeningDate = DateTime.Parse(txtOpeningDate.SelectedDate.Value.ToShortDateString() + " " + txtOpeningTime.TextWithLiterals);
            entity.SRShift = cboSRShift.SelectedValue;
            entity.SRCashierCounter = cboSRCashierCounter.SelectedValue;
            entity.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Value); 
            entity.IsApproved = chkIsApproved.Checked;
            entity.IsVoid = chkIsVoid.Checked;
            
            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = txtCreatedByUserID.Text;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
            }

            foreach (var item in CashManagementCashiers)
            {
                item.TransactionNo = txtTransactionNo.Text;

                //Last Update Status
                if (item.es.IsAdded || item.es.IsModified)
                {
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }
            }
        }

        private void SaveEntity(CashManagement entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                CashManagementCashiers.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new CashManagementQuery();
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
            var entity = new CashManagement();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }
        #endregion

        #region Method & Event TextChanged
        #endregion

        #region Record Detail Method Function

        private CashManagementCashierCollection CashManagementCashiers
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collCashManagementCashier"];
                    if (obj != null)
                        return ((CashManagementCashierCollection)(obj));
                }

                var coll = new CashManagementCashierCollection();
                var query = new CashManagementCashierQuery("a");
                var usr = new AppUserQuery("b");
                query.InnerJoin(usr).On(query.CashierUserID == usr.UserID);
                query.Select(query, usr.UserName.As("refToAppUser_UserName"));
                query.Where(query.TransactionNo == txtTransactionNo.Text);
                query.OrderBy(query.CashierUserID.Ascending);
                coll.Load(query);

                Session["collCashManagementCashier"] = coll;
                return coll;
            }
            set { Session["collCashManagementCashier"] = value; }
        }

        private void RefreshCommandItemGrid(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdCashier.Columns[0].Visible = isVisible;
            grdCashier.Columns[grdCashier.Columns.Count - 1].Visible = isVisible;

            grdCashier.MasterTableView.CommandItemDisplay = isVisible ? GridCommandItemDisplay.Top : GridCommandItemDisplay.None;

            //Reset Detail
            if (oldVal != AppEnum.DataMode.Read)
                CashManagementCashiers = null;

            //Perbaharui tampilan dan data
            grdCashier.Rebind();
        }

        private void PopulateGridDetail()
        {
            //Display Data Detail
            CashManagementCashiers = null; //Reset Record Detail
            grdCashier.DataSource = CashManagementCashiers;
            grdCashier.DataBind();
        }

        protected void grdCashier_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdCashier.DataSource = CashManagementCashiers;
        }

        protected void grdCashier_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            String cashierId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][CashManagementCashierMetadata.ColumnNames.CashierUserID]);
            CashManagementCashier entity = FindItemGrid(cashierId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdCashiere_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = CashManagementCashiers.AddNew();
            SetEntityValue(entity, e);
        }

        private void SetEntityValue(CashManagementCashier entity, GridCommandEventArgs e)
        {
            var userControl = (OpeningBalanceCashier)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.CashierUserID = userControl.CashierUserID;
                entity.CashierUserName = userControl.CashierUserName;
                entity.IsCashierCheckin = false;
            }
        }

        private CashManagementCashier FindItemGrid(string cashierId)
        {
            CashManagementCashierCollection coll = CashManagementCashiers;
            CashManagementCashier retval = null;
            foreach (CashManagementCashier rec in coll)
            {
                if (rec.CashierUserID.Equals(cashierId))
                {
                    retval = rec;
                    break;
                }
            }
            return retval;
        }

        #endregion
    }
}
