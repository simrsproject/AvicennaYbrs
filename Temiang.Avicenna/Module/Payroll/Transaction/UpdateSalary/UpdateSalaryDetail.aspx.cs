using System;
using System.Data;
using System.Web.UI;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using DevExpress.XtraRichEdit.Import.Html;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class UpdateSalaryDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "##";
            UrlPageList = "UpdateSalaryList.aspx";

            ProgramID = AppConstant.Program.UpdateEmployeeSalary;

            if (!IsPostBack)
            {
            }
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
            ajax.AddAjaxSetting(grdItem, grdItem);
            ajax.AddAjaxSetting(grdItem, btnRecalculate);
            ajax.AddAjaxSetting(btnRecalculate, lblRecalculateResult);
        }

        protected override void OnMenuNewClick()
        {
        }

        protected override void OnMenuEditClick()
        {

        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            var pp = new ClosingWageTransaction();
            if (pp.LoadByPrimaryKey(cboPayrollPeriodID.SelectedValue.ToInt()) && pp.IsClosed == true)
            {
                args.MessageText = "This payroll period has closed. Data cannot be changed";
                args.IsCancel = true;
            }
            else
            {
                return;
            }
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {

        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            var entity = new WageTransaction();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new WageTransaction();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtWageTransactionID.Value)))
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
            //auditLogFilter.PrimaryKeyData = string.Format("WageTransactionID='{0}'", Convert.ToInt32(txtWageTransactionID.Value));
            //auditLogFilter.TableName = "WageTransaction";
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            RefreshCommandItemWageTransactionItem(newVal);
            var isModified = false;
            foreach (var i in WageTransactionItems)
            {
                if (i.IsModified == true)
                {
                    isModified = true;
                    break;
                }
            }

            btnRecalculate.Visible = (newVal == AppEnum.DataMode.Read) && isModified;
            lblRecalculateResult.Text = string.Empty;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new WageTransaction();
            if (parameters.Length > 0)
            {
                string id = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(id));

                txtWageTransactionID.Value = entity.WageTransactionID;
                txtPersonID.Value = entity.PersonID;
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtWageTransactionID.Value));
            }

            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var wageTransaction = (WageTransaction)entity;

            txtWageTransactionID.Value = Convert.ToInt64(wageTransaction.WageTransactionID);
            txtPersonID.Value= Convert.ToInt32(wageTransaction.PersonID);
            if (!string.IsNullOrEmpty(wageTransaction.PayrollPeriodID.ToString()))
            {
                var period = new PayrollPeriodQuery();
                period.Where(period.PayrollPeriodID == Convert.ToInt32(wageTransaction.PayrollPeriodID));
                var dtb = period.LoadDataTable();
                cboPayrollPeriodID.DataSource = dtb;
                cboPayrollPeriodID.DataBind();
                cboPayrollPeriodID.SelectedValue = wageTransaction.PayrollPeriodID.ToString();
                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    cboPayrollPeriodID.Text = dtb.Rows[0]["PayrollPeriodName"].ToString();
            }
            else
            {
                cboPayrollPeriodID.Items.Clear();
                cboPayrollPeriodID.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(wageTransaction.PersonID.ToString()))
            {
                var personal = new PersonalInfo();
                if (personal.LoadByPrimaryKey(Convert.ToInt32(wageTransaction.PersonID)))
                {
                    txtEmployeeNumber.Text = personal.EmployeeNumber;
                    txtEmployeeName.Text = personal.EmployeeName;
                }
                else
                {
                    txtEmployeeNumber.Text = string.Empty;
                    txtEmployeeName.Text = string.Empty;
                }
            }
            else
            {
                txtEmployeeNumber.Text = string.Empty;
                txtEmployeeName.Text = string.Empty;
            }

            //Display Data Detail
            PopulateWageTransactionItemGrid();
        }

        private void SetEntityValue(WageTransaction entity)
        {
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = DateTime.Now;
        }

        private void SaveEntity(WageTransaction entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                WageTransactionItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new WageTransactionQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.WageTransactionID > Convert.ToInt32(txtWageTransactionID.Value), que.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
                que.OrderBy(que.WageTransactionID.Ascending);
            }
            else
            {
                que.Where(que.WageTransactionID < Convert.ToInt32(txtWageTransactionID.Value), que.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
                que.OrderBy(que.WageTransactionID.Descending);
            }
            var entity = new WageTransaction();
            entity.Load(que);

            OnPopulateEntryControl(entity);
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new PayrollPeriodQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.PayrollPeriodID,
                    query.PayrollPeriodCode,
                    query.PayrollPeriodName
                );
            query.Where
                (
                    query.Or
                        (
                            query.PayrollPeriodCode.Like(searchTextContain),
                            query.PayrollPeriodName.Like(searchTextContain)
                        )
                );
            query.OrderBy(query.PayrollPeriodCode.Descending);

            cboPayrollPeriodID.DataSource = query.LoadDataTable();
            cboPayrollPeriodID.DataBind();
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PayrollPeriodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PayrollPeriodID"].ToString();
        }

        #region Record Detail Method Function EmployeeSalaryMatrix
        private void RefreshCommandItemWageTransactionItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = (newVal != AppEnum.DataMode.Read);
            grdItem.Columns[0].Visible = isVisible;

            //Perbaharui tampilan dan data
            grdItem.Rebind();
        }

        private WageTransactionItemCollection WageTransactionItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collWageTransactionItem"];
                    if (obj != null)
                    {
                        return ((WageTransactionItemCollection)(obj));
                    }
                }

                var coll = new WageTransactionItemCollection();
                var query = new WageTransactionItemQuery("a");
                var salary = new SalaryComponentQuery("b");

                query.Select
                    (query,
                        salary.SalaryComponentCode.As("refToSalaryComponent_SalaryComponentCode"),
                        salary.SalaryComponentName.As("refToSalaryComponent_SalaryComponentName"),
                        salary.SRSalaryType.As("refToSalaryComponent_SRSalaryType")
                    );
                query.InnerJoin(salary).On(query.SalaryComponentID == salary.SalaryComponentID);

                query.Where(query.WageTransactionID == (Convert.ToInt32(txtWageTransactionID.Value ?? 0)));
                query.OrderBy(salary.SalaryComponentCode.Ascending);
                coll.Load(query);

                Session["collWageTransactionItem"] = coll;
                return coll;
            }
            set { Session["collWageTransactionItem"] = value; }
        }

        private void PopulateWageTransactionItemGrid()
        {
            //Display Data Detail
            WageTransactionItems = null; //Reset Record Detail
            grdItem.DataSource = WageTransactionItems; //Requery
            grdItem.MasterTableView.IsItemInserted = false;
            grdItem.MasterTableView.ClearEditItems();
            grdItem.DataBind();
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = WageTransactionItems;
        }

        protected void grdItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int64 id = Convert.ToInt64(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][WageTransactionItemMetadata.ColumnNames.WageTransactionItemID]);
            WageTransactionItem entity = FindWageTransactionItem(id);
            if (entity != null & entity.SRSalaryType == "07") //hanya loan aja yg bisa diupdate angkanya
                SetEntityValue(entity, e);
        }

        private WageTransactionItem FindWageTransactionItem(Int64 id)
        {
            WageTransactionItemCollection coll = WageTransactionItems;
            WageTransactionItem retEntity = null;
            foreach (WageTransactionItem rec in coll)
            {
                if (rec.WageTransactionItemID.Equals(id))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }
        private void SetEntityValue(WageTransactionItem entity, GridCommandEventArgs e)
        {
            var userControl = (UpdateSalaryItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.WageTransactionItemID = userControl.WageTransactionItemID;
                entity.SalaryComponentID = userControl.SalaryComponentID;
                var sc = new SalaryComponent();
                sc.LoadByPrimaryKey(Convert.ToInt32(entity.SalaryComponentID));
                entity.SalaryComponentCode = sc.SalaryComponentCode;
                entity.SalaryComponentName = userControl.SalaryComponentName;
                entity.NominalAmount = userControl.NominalAmount;
                entity.CurrencyAmount = userControl.CurrencyAmount;
                entity.IsModified = true;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        #endregion
        protected void btnRecalculate_Click(object sender, ImageClickEventArgs e)
        {
            var pp = new ClosingWageTransaction();
            if (pp.LoadByPrimaryKey(cboPayrollPeriodID.SelectedValue.ToInt()) && pp.IsClosed == true)
            {
                lblRecalculateResult.Text = "**This payroll period has closed. Data cannot be changed.";
            }
            else
            {
                int payrollPeriodId = cboPayrollPeriodID.SelectedValue.ToInt();
                Int64 wageProcessTypeId = Convert.ToInt64(txtWageTransactionID.Value);
                int personId = Convert.ToInt32(txtPersonID.Value);

                DateTime transactionDate = DateTime.Now;
                string userId = AppSession.UserLogin.UserID;

                int result = ClosingWageTransaction.ProcessWageTransaction(payrollPeriodId, wageProcessTypeId, transactionDate, userId, personId);
                if (result != 0)
                {
                    if (result == -1)
                        lblRecalculateResult.Text = "Closing failed please try again or contact your administrator.";
                    else if (result == -2)
                        lblRecalculateResult.Text = "All transaction must be posted first.";
                    else if (result == -3)
                        lblRecalculateResult.Text = "This periode has been closed.";
                }
                else
                {
                    lblRecalculateResult.Text = "Recalculation is complete.";

                    var wt = new WageTransaction();
                    var wtq = new WageTransactionQuery();
                    wtq.Where(wtq.PayrollPeriodID == cboPayrollPeriodID.SelectedValue.ToInt(), wtq.PersonID == txtPersonID.Value.ToInt());
                    wt.Load(wtq);

                    OnPopulateEntryControl(wt);
                }
            }
        }
    }
}
