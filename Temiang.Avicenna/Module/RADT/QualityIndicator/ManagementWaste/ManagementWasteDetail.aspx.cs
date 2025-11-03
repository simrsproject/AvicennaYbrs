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

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class ManagementWasteDetail : BasePageDetail
    {
        private AppAutoNumberLast _autoNumber;

        #region Page Event & Initialize

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "ManagementWasteSearch.aspx";
            UrlPageList = "ManagementWasteList.aspx";

            ProgramID = AppConstant.Program.ManagementWaste;

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
            //ajax.AddAjaxSetting(cboEmployeeID, txtProfessionType);
            //ajax.AddAjaxSetting(cboEmployeeID, cboDepartmentID);
            //ajax.AddAjaxSetting(cboEmployeeID, cboDivisionID);
            //ajax.AddAjaxSetting(cboEmployeeID, cboSubDivisionID);
            //ajax.AddAjaxSetting(cboEmployeeID, cboUnit);

            //ajax.AddAjaxSetting(cboDepartmentID, cboDivisionID);
            //ajax.AddAjaxSetting(cboDepartmentID, cboSubDivisionID);
            //ajax.AddAjaxSetting(cboDepartmentID, cboUnit);

            //ajax.AddAjaxSetting(cboDivisionID, cboSubDivisionID);
            //ajax.AddAjaxSetting(cboDivisionID, cboUnit);

            //ajax.AddAjaxSetting(cboSubDivisionID, cboUnit);

            //ajax.AddAjaxSetting(txtTransactionDate, txtTransactionDate);
            //ajax.AddAjaxSetting(txtTransactionDate, txtTransactionNo);
            //ajax.AddAjaxSetting(txtTransactionDate, txtSessionLength);

            //ajax.AddAjaxSetting(txtStartTime, txtSessionLength);
            //ajax.AddAjaxSetting(txtEndTime, txtSessionLength);
        }

        #endregion

        #region Toolbar Menu Event

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new ManagementWaste());

            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

            txtTransactionNo.Text = PopulateNewNo();
            txtUserName.Text = AppSession.UserLogin.UserName;

            ViewState["IsApproved"] = false;
            ViewState["IsVoid"] = false;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new ManagementWaste();
            entity.AddNew();

            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {

            var entity = new ManagementWaste();
            if (entity.LoadByPrimaryKey(txtTransactionNo.Text))
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
            auditLogFilter.PrimaryKeyData = string.Format("TransactionNo='{0}'", txtTransactionNo.Text.Trim());
            auditLogFilter.TableName = "ManagementWaste";
        }

        #endregion

        #region ToolBar Menu Support
        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var entity = new ManagementWaste();
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

        private bool IsApprovedOrVoid(ManagementWaste entity, ValidateArgs args)
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
            RefreshCommandManagementWaste(newVal);
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new ManagementWaste();
            if (parameters.Length > 0)
            {
                var tno = (String)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(tno);
            }
            else
                entity.LoadByPrimaryKey(txtTransactionNo.Text);

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var hh = (ManagementWaste)entity;

            txtTransactionNo.Text = hh.TransactionNo;
            txtTransactionDate.SelectedDate = hh.TransactionDate;
            txtUserName.Text = hh.UserName;
            cboServiceUnitID.SelectedValue = hh.ServiceUnitID;
            cboServiceUnitID_ItemsRequested(cboServiceUnitID, new RadComboBoxItemsRequestedEventArgs() { Text = hh.ServiceUnitID });
            cboServiceUnitID.SelectedValue = hh.ServiceUnitID;

            //PopulateItemGrid();
            PopulateManagementWasteItemGrid();

            ViewState["IsApproved"] = hh.IsApproved ?? false;
            ViewState["IsVoid"] = hh.IsVoid ?? false;
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            var entity = new ManagementWaste();
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
            var entity = new ManagementWaste();
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

        private void SetApproval(ManagementWaste entity, bool isApproval, ValidateArgs args)
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
            var entity = new ManagementWaste();
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
            var entity = new ManagementWaste();
            if (!entity.LoadByPrimaryKey(txtTransactionNo.Text))
            {
                args.MessageText = AppConstant.Message.RecordNotExist;
                args.IsCancel = true;
                return;
            }

            SetVoid(entity, false);
        }

        private void SetVoid(ManagementWaste entity, bool isVoid)
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

        private void SetEntityValue(ManagementWaste entity)
        {
            if (DataModeCurrent == AppEnum.DataMode.New)
            {
                txtTransactionNo.Text = PopulateNewNo();
                // save autonumber immediately to decrease time gap between create and save
                _autoNumber.Save();
            }
            entity.TransactionNo = txtTransactionNo.Text;
            entity.TransactionDate = txtTransactionDate.SelectedDate;
            entity.UserName = txtUserName.Text;
            entity.ServiceUnitID = cboServiceUnitID.SelectedValue;
            entity.IsApproved = false;
            entity.IsVoid = false;

            if (entity.es.IsAdded)
            {
                entity.CreatedByUserID = AppSession.UserLogin.UserID;
                entity.CreatedDateTime = (new DateTime()).NowAtSqlServer();
            }

            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(ManagementWaste entity)
        {
            var managementWasteItem = new ManagementWasteItemCollection();
            managementWasteItem.Query.Where(managementWasteItem.Query.TransactionNo == entity.TransactionNo);
            managementWasteItem.LoadAll();

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                string Stdr = dataItem.GetDataKeyValue("SRManagementWaste").ToString();
                bool isYes = ((CheckBox)dataItem.FindControl("chkIsYes")).Checked;
                bool isNotApplicable = ((CheckBox)dataItem.FindControl("chkIsNotApplicable")).Checked;


                foreach (ManagementWasteItem row in managementWasteItem)
                {
                    if (row.SRManagementWaste.Equals(Stdr))
                    {
                        if (!isYes)
                            row.MarkAsDeleted();
                        if (!isNotApplicable)
                            row.MarkAsDeleted();
                        break;

                    }
                }

                ManagementWasteItem item = managementWasteItem.AddNew();

                item.TransactionNo = entity.TransactionNo;
                item.SRManagementWaste = Stdr.ToString();

                item.IsYes = isYes;
                item.IsNotApplicable = isNotApplicable;
                item.Notes = txtNotes.Text;
                item.Recommendation = txtRecommendation.Text;

                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();


            }
            using (var trans = new esTransactionScope())
            {
                entity.Save();
                managementWasteItem.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new ManagementWasteQuery("a");

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

            var entity = new ManagementWaste();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        #region Record Detail Method Function of ManagementWasteItem       
        private void PopulateManagementWasteItemGrid()
        {
            grdItem.DataSource = GetManagementWasteItem();
            grdItem.DataBind();
        }
        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = GetManagementWasteItem();
        }

        private DataTable GetManagementWasteItem()
        {
            var query = new ManagementWasteItemQuery("a");
            var qrS = new AppStandardReferenceItemQuery("b");
            if (this.DataModeCurrent == AppEnum.DataMode.Read)
            {
                query.InnerJoin(qrS).On(query.SRManagementWaste == qrS.ItemID);
                query.Where(query.TransactionNo == txtTransactionNo.Text);
            }
            else
            {
                query.RightJoin(qrS).On(query.SRManagementWaste == qrS.ItemID && query.TransactionNo == txtTransactionNo.Text);
            }
            query.Where(qrS.StandardReferenceID == "ManagementWaste");
            query.OrderBy(qrS.ItemID.Ascending);
            query.Select
                (
                qrS.ItemID.As("SRManagementWaste"),
                qrS.ItemName.As("ManagementWasteName"),
                "<CONVERT(Bit,CASE WHEN COALESCE(a.IsYes,'')='' THEN 0 ELSE 1 END) as IsYes>",
                "<CONVERT(Bit,CASE WHEN COALESCE(a.IsNotApplicable,'')='' THEN 0 ELSE 1 END) as IsNotApplicable>"
                );
            DataTable dtb = query.LoadDataTable();
            return dtb;
        }

        private void RefreshCommandManagementWaste(AppEnum.DataMode newVal)
        {
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;

            grdItem.Rebind();
        }
        #endregion

        #region Combobox        

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var suColl = new ServiceUnitCollection();
            suColl.Query.Where(
                suColl.Query.Or(
                    suColl.Query.ServiceUnitID.Like(searchTextContain),
                    suColl.Query.ServiceUnitName.Like(searchTextContain)
                )
            );
            suColl.LoadAll();
            var cbo = (RadComboBox)o;
            cbo.DataSource = suColl;
            cbo.DataBind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((ServiceUnit)e.Item.DataItem).ServiceUnitName;
            e.Item.Value = ((ServiceUnit)e.Item.DataItem).ServiceUnitID;
        }

        #endregion

        protected void txtTransactionDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtTransactionNo.Text = PopulateNewNo();
        }

        private string PopulateNewNo()
        {
            _autoNumber = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value, AppEnum.AutoNumber.ManagementWasteNo);

            return _autoNumber.LastCompleteNumber;
        }

    }
}