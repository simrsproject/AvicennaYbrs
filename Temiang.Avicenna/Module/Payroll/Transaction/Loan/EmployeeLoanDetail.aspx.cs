using System;
using System.Linq;
using System.Data;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeeLoanDetail : BasePageDetail
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            UrlPageSearch = "EmployeeLoanSearch.aspx";
            UrlPageList = "EmployeeLoanList.aspx";

            ProgramID = AppConstant.Program.EmployeeLoan; //TODO: Isi ProgramID

            //StandardReference Initialize
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRPurposeOfLoan, AppEnum.StandardReference.PurposeOfLoan);
                StandardReference.InitializeIncludeSpace(cboSRHRPaymentMethod, AppEnum.StandardReference.HRPaymentMethod);

                var comps = new CompanyLaborProfileCollection();
                comps.LoadAll();

                cboBusinessPartnerID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in comps)
                {
                    cboBusinessPartnerID.Items.Add(new RadComboBoxItem(entity.CompanyLaborProfileName, entity.CompanyLaborProfileID.ToString()));
                }

                var period = new PayrollPeriodCollection();
                period.LoadAll();

                cboStartPaymentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in period)
                {
                    cboStartPaymentID.Items.Add(new RadComboBoxItem(entity.PayrollPeriodName, entity.PayrollPeriodID.ToString()));
                }
            }
        }

        protected void rbtInstallmentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNumberOfInstallment.ReadOnly = !(rbtInstallmentMethod.SelectedValue == "0");
            txtAmountOfInstallment.ReadOnly = !(rbtInstallmentMethod.SelectedValue == "1");

            txtNumberOfInstallment.Value = 0;
            txtAmountOfInstallment.Value = 0;
        }

        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new EmployeeLoan());

            txtEmployeeLoanID.Value = -1;
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            if (EmployeeLoanItems.Count > 0)
            {
                if (EmployeeLoanItems.Any(item => item.IsPaid == true))
                {
                    args.MessageText = "Detail loan already processed payroll.";
                    args.IsCancel = true;
                    return;
                }

            }

            var entity = new EmployeeLoan();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLoanID.Value)))
            {
                if (!IsApproved(entity, args))
                    return;

                entity.MarkAsDeleted();
                var coll = new EmployeeLoanItemCollection();
                coll.Query.Where(coll.Query.EmployeeLoanID == Convert.ToInt32(txtEmployeeLoanID.Value));
                coll.LoadAll();
                coll.MarkAllAsDeleted();

                using (var trans = new esTransactionScope())
                {
                    coll.Save();
                    entity.Save();

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
            EmployeeLoan entity = new EmployeeLoan();
            entity = new EmployeeLoan();
            entity.AddNew();
            SetEntityValue(entity);
            SaveEntity(entity);
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            EmployeeLoan entity = new EmployeeLoan();
            if (entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLoanID.Value)))
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
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("EmployeeLoanID='{0}'", txtEmployeeLoanID.Value.ToString().Trim());
            auditLogFilter.TableName = "EmployeeLoan";
        }

        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            using (var trans = new esTransactionScope())
            {
                var entity = new EmployeeLoan();
                entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLoanID.Value));

                entity.IsApproved = true;
                entity.ApprovedDateTime = DateTime.Now;
                entity.ApprovedByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            if (EmployeeLoanItems.Count > 0)
            {
                if (EmployeeLoanItems.Any(item => item.IsPaid == true))
                {
                    args.MessageText = "Loan detail already processed to payroll.";
                    args.IsCancel = true;
                    return;
                }
            }

            using (var trans = new esTransactionScope())
            {
                var entity = new EmployeeLoan();
                entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLoanID.Value));

                entity.IsApproved = null;
                entity.ApprovedDateTime = null;
                entity.ApprovedByUserID = null;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                entity.Save();

                trans.Complete();
            }
        }

        protected override void OnMenuEditClick()
        {
            bool isPaid = EmployeeLoanItems.Any(item => item.IsPaid == true);
            if (isPaid)
            {
                cboPersonID.Enabled = false;
                txtLoanDate.Enabled = false;
                txtLoanNo.ReadOnly = true;
                cboSRPurposeOfLoan.Enabled = false;
                cboBusinessPartnerID.Enabled = false;
                txtAmount.ReadOnly = true;
                txtCoverageAmount.ReadOnly = true;
                txtLoanAmountWithInterest.ReadOnly = true;
                txtPercentRateOfInterest.ReadOnly = true;
                rbtInstallmentMethod.Enabled = false;
                txtNumberOfInstallment.ReadOnly = true;
                txtAmountOfInstallment.ReadOnly = true;
                cboSRHRPaymentMethod.Enabled = false;
                cboStartPaymentID.Enabled = false;
                cboSalaryComponetID.Enabled = false;
            }
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            txtEmployeeLoanID.Enabled = (newVal == AppEnum.DataMode.New);

            RefreshCommandItemEmployeeLoanItem(newVal);
        }

        private bool IsApproved(EmployeeLoan entity, ValidateArgs args)
        {
            if (entity.IsApproved ?? false)
            {
                args.MessageText = AppConstant.Message.RecordHasApproved;
                args.IsCancel = true;
                return false;
            }

            return true;
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuDelete.Enabled = !chkIsApproved.Checked;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return !chkIsApproved.Checked;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            EmployeeLoan entity = new EmployeeLoan();
            if (parameters.Length > 0)
            {
                string employeeLoanID = (string)parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(Convert.ToInt32(employeeLoanID));
            }
            else
            {
                entity.LoadByPrimaryKey(Convert.ToInt32(txtEmployeeLoanID.Value));
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            EmployeeLoan employeeLoan = (EmployeeLoan)entity;

            txtEmployeeLoanID.Value = Convert.ToDouble(employeeLoan.EmployeeLoanID ?? -1);

            if (txtEmployeeLoanID.Value != -1)
            {
                {
                    PersonalInfoQuery query = new PersonalInfoQuery();
                    query.Select
                        (
                            query.PersonID,
                            query.EmployeeNumber,
                            query.FirstName,
                            query.MiddleName,
                            query.LastName
                        );
                    query.Where(query.PersonID == Convert.ToInt32(employeeLoan.PersonID));
                    cboPersonID.DataSource = query.LoadDataTable();
                    cboPersonID.DataBind();
                    cboPersonID.SelectedValue = employeeLoan.PersonID.ToString();

                    var row = (cboPersonID.DataSource as DataTable).Rows[0];
                    cboPersonID.Text = row["EmployeeNumber"].ToString() + " - " + 
                        row["FirstName"].ToString() + " " + 
                        row["MiddleName"].ToString() + " " + 
                        row["LastName"].ToString();
                }
                {
                    SalaryComponentQuery query = new SalaryComponentQuery();
                    query.Select
                        (
                            query.SalaryComponentID,
                            query.SalaryComponentCode,
                            query.SalaryComponentName
                        );
                    query.Where(query.SalaryComponentID == Convert.ToInt32(employeeLoan.SalaryCodeInstallment));
                    cboSalaryComponetID.DataSource = query.LoadDataTable();
                    cboSalaryComponetID.DataBind();
                    cboSalaryComponetID.SelectedValue = employeeLoan.SalaryCodeInstallment.ToString();

                    var row = (cboSalaryComponetID.DataSource as DataTable).Rows[0];
                    cboSalaryComponetID.Text = row["SalaryComponentName"].ToString();
                }
                {
                    ComboBox.PayrollPeriodItemsRequested(cboStartPaymentID, employeeLoan.StartPaymentPeriode.ToString());
                    cboStartPaymentID.SelectedValue = employeeLoan.StartPaymentPeriode.ToString();
                    var row = (cboStartPaymentID.DataSource as DataTable).Rows[0];
                    cboStartPaymentID.Text = row["PayrollPeriodName"].ToString();
                }
            }
            else
            {
                cboPersonID.Items.Clear();
                cboPersonID.Text = string.Empty;

                cboSalaryComponetID.Items.Clear();
                cboSalaryComponetID.Text = string.Empty;

                cboStartPaymentID.Items.Clear();
                cboStartPaymentID.Text = string.Empty;
            }

            txtLoanDate.SelectedDate = employeeLoan.LoanDate;
            txtAmount.Value = Convert.ToDouble(employeeLoan.Amount);
            txtLoanAmountWithInterest.Value = Convert.ToDouble(employeeLoan.LoanAmountWithInterest);
            txtPercentRateOfInterest.Value = Convert.ToDouble(employeeLoan.PercentRateOfInterest);
            chkFlatRate.Checked = employeeLoan.FlatRate ?? false;
            cboSRPurposeOfLoan.SelectedValue = employeeLoan.SRPurposeOfLoan;

            cboSRHRPaymentMethod.SelectedValue = employeeLoan.SRHRPaymentMethod;
            txtNumberOfInstallment.Value = Convert.ToDouble(employeeLoan.NumberOfInstallment);
            txtAmountOfInstallment.Value = Convert.ToDouble(employeeLoan.AmountOfInstallment);

            cboBusinessPartnerID.SelectedValue = employeeLoan.CompanyLaborProfileID.ToString();
            txtCoverageAmount.Value = Convert.ToDouble(employeeLoan.CoverageAmount);
            txtLoanNo.Value = Convert.ToDouble(employeeLoan.LoanNo);

            txtNotes.Text = employeeLoan.Notes;
            chkIsApproved.Checked = employeeLoan.IsApproved ?? false;
            rbtInstallmentMethod.SelectedValue = (employeeLoan.InstallmentMethod ?? false) ? "1" : "0";
            txtNumberOfInstallment.ReadOnly = !(rbtInstallmentMethod.SelectedValue == "0");
            txtAmountOfInstallment.ReadOnly = !(rbtInstallmentMethod.SelectedValue == "1");

            PopulateEmployeeLoanItemGrid();
        }

        private void SetEntityValue(EmployeeLoan entity)
        {
            entity.EmployeeLoanID = Convert.ToInt32(txtEmployeeLoanID.Value);
            entity.PersonID = Convert.ToInt32(cboPersonID.SelectedValue);
            entity.LoanDate = txtLoanDate.SelectedDate;
            entity.Amount = Convert.ToDecimal(txtAmount.Value);
            entity.LoanAmountWithInterest = Convert.ToDecimal(txtLoanAmountWithInterest.Value);
            entity.PercentRateOfInterest = Convert.ToDecimal(txtPercentRateOfInterest.Value);
            entity.FlatRate = chkFlatRate.Checked;
            entity.SRPurposeOfLoan = cboSRPurposeOfLoan.SelectedValue;
            entity.NumberOfInstallment = Convert.ToInt32(txtNumberOfInstallment.Value);
            entity.AmountOfInstallment = Convert.ToDecimal(txtAmountOfInstallment.Value);
            entity.SRHRPaymentMethod = cboSRHRPaymentMethod.SelectedValue;
            entity.StartPaymentPeriode = Convert.ToInt32(cboStartPaymentID.SelectedValue);
            entity.SalaryCodeInstallment = Convert.ToInt32(cboSalaryComponetID.SelectedValue);
            entity.CompanyLaborProfileID = string.IsNullOrEmpty(cboBusinessPartnerID.SelectedValue) ? -1 : Convert.ToInt32(cboBusinessPartnerID.SelectedValue);
            entity.CoverageAmount = Convert.ToDecimal(txtCoverageAmount.Value);
            entity.LoanNo = Convert.ToInt32(txtLoanNo.Value);
            entity.Notes = txtNotes.Text;
            entity.IsApproved = chkIsApproved.Checked;
            entity.InstallmentMethod = rbtInstallmentMethod.SelectedValue == "0" ? false : true;

            //Last Update Status
            if (entity.es.IsAdded || entity.es.IsModified)
            {
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }

        private void SaveEntity(EmployeeLoan entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                int period = entity.StartPaymentPeriode ?? -1;

                if (DataModeCurrent != AppEnum.DataMode.New)
                    EmployeeLoanItems.MarkAllAsDeleted();

                var amount = txtAmount.Value - txtCoverageAmount.Value;
                if (rbtInstallmentMethod.SelectedValue == "0")
                {
                    amount = (txtAmount.Value - txtCoverageAmount.Value) / txtNumberOfInstallment.Value;
                    entity.AmountOfInstallment = Convert.ToDecimal(amount);
                    for (int i = 1; i <= entity.NumberOfInstallment; i++)
                    {
                        var detail = EmployeeLoanItems.AddNew();
                        detail.EmployeeLoanID = entity.EmployeeLoanID;
                        detail.InstallmentNumber = i;
                        detail.PlanDate = entity.LoanDate;
                        detail.PlanAmount = Convert.ToDecimal(amount);
                        detail.MainPayment = Convert.ToDecimal(amount);
                        detail.InterestPayment = 0;
                        detail.str.ActualDate = string.Empty;
                        detail.ActualAmount = 0;
                        detail.PayrollPeriodID = period;

                        var pp = new PayrollPeriod();
                        pp.LoadByPrimaryKey(period);
                        detail.PayrollPeriodName = pp.PayrollPeriodName;

                        period++;

                        detail.IsPaid = false;
                        detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        detail.LastUpdateDateTime = DateTime.Now;
                    }
                }
                else
                {
                    amount = (txtAmount.Value - txtCoverageAmount.Value);
                    var installment = txtAmountOfInstallment.Value ?? 0;
                    var sequence = 0D;
                    var index = 0;
                    while (sequence < amount)
                    {
                        index++;
                        if (amount - sequence > installment)
                        {
                            sequence += installment;

                            var detail = EmployeeLoanItems.AddNew();
                            detail.EmployeeLoanID = entity.EmployeeLoanID;
                            detail.InstallmentNumber = index;
                            detail.PlanDate = entity.LoanDate;
                            detail.PlanAmount = Convert.ToDecimal(installment);
                            detail.MainPayment = Convert.ToDecimal(installment);
                            detail.InterestPayment = 0;
                            detail.str.ActualDate = string.Empty;
                            detail.ActualAmount = 0;

                            detail.PayrollPeriodID = period;
                            period++;

                            var pp = new PayrollPeriod();
                            pp.LoadByPrimaryKey(detail.PayrollPeriodID ?? -1);
                            detail.PayrollPeriodName = pp.PayrollPeriodName;

                            detail.IsPaid = false;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = DateTime.Now;
                        }
                        else
                        {
                            var remains = amount - sequence;
                            sequence += (remains ?? 0);

                            var detail = EmployeeLoanItems.AddNew();
                            detail.EmployeeLoanID = entity.EmployeeLoanID;
                            detail.InstallmentNumber = index;
                            detail.PlanDate = entity.LoanDate;
                            detail.PlanAmount = Convert.ToDecimal((remains ?? 0));
                            detail.MainPayment = Convert.ToDecimal((remains ?? 0));
                            detail.InterestPayment = 0;
                            detail.str.ActualDate = string.Empty;
                            detail.ActualAmount = 0;

                            detail.PayrollPeriodID = period;
                            period++;

                            var pp = new PayrollPeriod();
                            pp.LoadByPrimaryKey(detail.PayrollPeriodID ?? -1);
                            detail.PayrollPeriodName = pp.PayrollPeriodName;

                            detail.IsPaid = false;
                            detail.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            detail.LastUpdateDateTime = DateTime.Now;
                        }
                    }
                    entity.NumberOfInstallment = index;
                }

                entity.Save();
                txtEmployeeLoanID.Value = entity.EmployeeLoanID;

                foreach (var item in EmployeeLoanItems)
                {
                    item.EmployeeLoanID = entity.EmployeeLoanID;
                    item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    item.LastUpdateDateTime = DateTime.Now;
                }

                EmployeeLoanItems.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            EmployeeLoanQuery que = new EmployeeLoanQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(que.EmployeeLoanID > txtEmployeeLoanID.Value);
                que.OrderBy(que.EmployeeLoanID.Ascending);
            }
            else
            {
                que.Where(que.EmployeeLoanID < txtEmployeeLoanID.Value);
                que.OrderBy(que.EmployeeLoanID.Descending);
            }
            EmployeeLoan entity = new EmployeeLoan();
            entity.Load(que);
            OnPopulateEntryControl(entity);
        }

        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            PersonalInfoQuery query = new PersonalInfoQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.FirstName,
                    query.MiddleName,
                    query.LastName
                );
            query.Where
                (
                    query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.FirstName.Like(searchTextContain)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["FirstName"].ToString() + " " + ((DataRowView)e.Item.DataItem)["MiddleName"].ToString() + " " + ((DataRowView)e.Item.DataItem)["LastName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboSalaryComponetID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            SalaryComponentQuery query = new SalaryComponentQuery();
            query.es.Top = 10;
            query.Select
                (
                    query.SalaryComponentID,
                    query.SalaryComponentCode,
                    query.SalaryComponentName
                );
            query.Where
                (
                    query.SRSalaryType == AppSession.Parameter.SalaryTypeLoan,
                    query.Or
                        (
                            query.SalaryComponentCode.Like(searchTextContain),
                            query.SalaryComponentName.Like(searchTextContain)
                        )
                );

            cboSalaryComponetID.DataSource = query.LoadDataTable();
            cboSalaryComponetID.DataBind();
        }

        protected void cboSalaryComponetID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }

        private void RefreshCommandItemEmployeeLoanItem(AppEnum.DataMode newVal)
        {
            //Toogle grid command
            bool isVisible = false; //(newVal != AppEnum.DataMode.Read);
            grdEmployeeLoanItem.Columns[0].Visible = isVisible;
            grdEmployeeLoanItem.Columns[grdEmployeeLoanItem.Columns.Count - 1].Visible = isVisible;

            grdEmployeeLoanItem.MasterTableView.CommandItemDisplay = isVisible
                                                                              ? GridCommandItemDisplay.Top
                                                                              : GridCommandItemDisplay.None;
            //Perbaharui tampilan dan data
            grdEmployeeLoanItem.Rebind();
        }

        private EmployeeLoanItemCollection EmployeeLoanItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collEmployeeLoanItem"];
                    if (obj != null)
                    {
                        return ((EmployeeLoanItemCollection)(obj));
                    }
                }

                EmployeeLoanItemCollection coll = new EmployeeLoanItemCollection();

                EmployeeLoanItemQuery query = new EmployeeLoanItemQuery("a");
                var period = new PayrollPeriodQuery("b");

                query.Select
                    (
                        query,
                        period.PayrollPeriodName.As("refToPayrollPeriod_PayrollPeriodName")
                    );

                query.LeftJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
                query.Where(query.EmployeeLoanID == (txtEmployeeLoanID.Value ?? 0));

                query.OrderBy(query.InstallmentNumber.Ascending); //TODO: Betulkan ordernya
                coll.Load(query);

                Session["collEmployeeLoanItem"] = coll;
                return coll;
            }
            set { Session["collEmployeeLoanItem"] = value; }
        }

        private void PopulateEmployeeLoanItemGrid()
        {
            //Display Data Detail
            EmployeeLoanItems = null; //Reset Record Detail
            grdEmployeeLoanItem.DataSource = EmployeeLoanItems; //Requery
            grdEmployeeLoanItem.MasterTableView.IsItemInserted = false;
            grdEmployeeLoanItem.MasterTableView.ClearEditItems();
            grdEmployeeLoanItem.DataBind();
        }

        protected void grdEmployeeLoanItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdEmployeeLoanItem.DataSource = EmployeeLoanItems;
        }

        protected void grdEmployeeLoanItem_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            Int32 employeeLoanItemId = Convert.ToInt32(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][EmployeeLoanItemMetadata.ColumnNames.EmployeeLoanDetailID]);
            EmployeeLoanItem entity = FindEmployeeLoanItem(employeeLoanItemId);
            if (entity != null)
                SetEntityValue(entity, e);
        }

        protected void grdEmployeeLoanItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            Int32 employeeLoanItemId = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex][EmployeeLoanItemMetadata.ColumnNames.EmployeeLoanDetailID]);
            EmployeeLoanItem entity = FindEmployeeLoanItem(employeeLoanItemId);
            if (entity != null)
                entity.MarkAsDeleted();
        }

        protected void grdEmployeeLoanItem_InsertCommand(object source, GridCommandEventArgs e)
        {
            EmployeeLoanItem entity = EmployeeLoanItems.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdEmployeeLoanItem.Rebind();
        }

        private EmployeeLoanItem FindEmployeeLoanItem(Int32 employeeLoanItemId)
        {
            EmployeeLoanItemCollection coll = EmployeeLoanItems;
            EmployeeLoanItem retEntity = null;
            foreach (EmployeeLoanItem rec in coll)
            {
                if (rec.EmployeeLoanDetailID.Equals(employeeLoanItemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(EmployeeLoanItem entity, GridCommandEventArgs e)
        {
            EmployeeLoanItemDetail userControl = (EmployeeLoanItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                //TODO: Lengkapi field yg belum terisi, untuk ref key ke header di set di SetEntity
                entity.InstallmentNumber = userControl.InstallmentNumber;
                entity.PlanDate = txtLoanDate.SelectedDate;
                entity.PlanAmount = userControl.PlanAmount;
                entity.MainPayment = userControl.PlanAmount;
                entity.InterestPayment = 0;
                entity.str.ActualDate = string.Empty;
                entity.ActualAmount = 0;
                entity.PayrollPeriodID = userControl.PayrollPeriodID;
                entity.PayrollPeriodName = userControl.PayrollPeriodName;
                entity.IsPaid = false;
            }
        }

        protected void cboStartPaymentID_DataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected void cboStartPaymentID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void txtNumberOfInstallment_TextChanged(object o, EventArgs e)
        {
            try
            {
                if (rbtInstallmentMethod.SelectedValue == "0")
                    txtAmountOfInstallment.Value = (txtAmount.Value - txtCoverageAmount.Value) / txtNumberOfInstallment.Value;
            }
            catch
            {
            }
        }
    }
}
