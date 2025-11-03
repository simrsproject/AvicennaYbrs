using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Payroll.Transaction
{
    public partial class EmployeeLoanItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                EmployeeLoanItemCollection coll = (EmployeeLoanItemCollection)Session["collEmployeeLoanItem"];
                if (!coll.Any()) txtInstallmentNumber.Value = 1;
                else
                {
                    var num = (from c in coll
                               orderby c.InstallmentNumber descending
                               select c.InstallmentNumber).Take(1).SingleOrDefault();
                    num = num + 1;
                    txtInstallmentNumber.Value = num++ ?? 1;
                }
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtEmployeeLoanDetailID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.EmployeeLoanDetailID));
            txtInstallmentNumber.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.InstallmentNumber));
            //txtPlanDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.PlanDate);
            txtPlanAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.PlanAmount));
            //txtMainPayment.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.MainPayment));
            //txtInterestPayment.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.InterestPayment));
            //txtActualDate.SelectedDate = (DateTime?)DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.ActualDate);
            //txtActualAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.ActualAmount));
            //chkIsPaid.Checked = (bool)DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.IsPaid);	

            var pp = new PayrollPeriod();
            var payrollPeriodId = Convert.ToInt32(DataBinder.Eval(DataItem, EmployeeLoanItemMetadata.ColumnNames.PayrollPeriodID));
            if (pp.LoadByPrimaryKey(payrollPeriodId))
            {
                ComboBox.PayrollPeriodItemsRequested(cboStartPaymentID, payrollPeriodId.ToString());
                cboStartPaymentID.SelectedValue = payrollPeriodId.ToString();
                var row = (cboStartPaymentID.DataSource as DataTable).Rows[0];
                cboStartPaymentID.Text = row["PayrollPeriodName"].ToString();
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                EmployeeLoanItemCollection coll = (EmployeeLoanItemCollection)Session["collEmployeeLoanItem"];

                //TODO: Betulkan cara pengecekannya
                string id = Convert.ToString(txtEmployeeLoanDetailID.Text);
                bool isExist = false;
                foreach (EmployeeLoanItem item in coll)
                {
                    if (item.EmployeeLoanID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public Int32 EmployeeLoanDetailID
        {
            get { return Convert.ToInt32(txtEmployeeLoanDetailID.Text); }
        }
        public Int32 InstallmentNumber
        {
            get { return Convert.ToInt32(txtInstallmentNumber.Text); }
        }
        public DateTime PlanDate
        {
            get { return Convert.ToDateTime(txtPlanDate.SelectedDate); }
        }
        public Decimal PlanAmount
        {
            get { return Convert.ToDecimal(txtPlanAmount.Value); }
        }
        public Decimal MainPayment
        {
            get { return Convert.ToDecimal(txtMainPayment.Value); }
        }
        public Decimal InterestPayment
        {
            get { return Convert.ToDecimal(txtInterestPayment.Value); }
        }
        public DateTime ActualDate
        {
            get { return Convert.ToDateTime(txtActualDate.SelectedDate); }
        }
        public Decimal ActualAmount
        {
            get { return Convert.ToDecimal(txtActualAmount.Value); }
        }
        public Boolean IsPaid
        {
            get { return chkIsPaid.Checked; }
        }
        public Int32 PayrollPeriodID
        {
            get { return Convert.ToInt32(cboStartPaymentID.SelectedValue); }

        }
        public string PayrollPeriodName
        {
            get { return cboStartPaymentID.Text; }

        }
        #endregion

        protected void cboStartPaymentID_DataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected void cboStartPaymentID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }
    }
}
