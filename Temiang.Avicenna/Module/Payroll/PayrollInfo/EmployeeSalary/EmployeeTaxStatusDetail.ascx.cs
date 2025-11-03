using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.Payroll.PayrollInfo
{
    public partial class EmployeeTaxStatusDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private string PageId
        {
            get { return ((HiddenField)Helper.FindControlRecursive(Page, "hdnPageId")).Value; }
        }

        private RadComboBox CboSRTaxStatus
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboSRTaxStatus"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRTaxStatus, AppEnum.StandardReference.TaxStatus, "TAX");

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtSPTYear.Value = DateTime.Now.Year;
                cboSRTaxStatus.SelectedValue = CboSRTaxStatus.SelectedValue;
                return;
            }
            ViewState["IsNewRecord"] = false;
            txtSPTYear.ReadOnly = true;

            txtSPTYear.Value = Convert.ToDouble(DataBinder.Eval(DataItem, EmployeeTaxStatusMetadata.ColumnNames.SPTYear));
            cboSRTaxStatus.SelectedValue = (String)DataBinder.Eval(DataItem, EmployeeTaxStatusMetadata.ColumnNames.SRTaxStatus);

            var pp = new PayrollPeriodQuery("pp");
            var cwt = new ClosingWageTransactionQuery("cwt");
            pp.InnerJoin(cwt).On(cwt.PayrollPeriodID == pp.PayrollPeriodID && pp.SPTYear == txtSPTYear.Value.ToInt() && cwt.IsClosed == true);
            DataTable dtb = pp.LoadDataTable();
            cboSRTaxStatus.Enabled = dtb.Rows.Count == 0;
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (EmployeeTaxStatusCollection)Session["collEmployeeTaxStatus" + Request.UserHostName + PageId];

                //TODO: Betulkan cara pengecekannya
                Int32 id = Convert.ToInt32(txtSPTYear.Value);
                bool isExist = false;
                foreach (EmployeeTaxStatus item in coll)
                {
                    if (item.SPTYear.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("SPT Year : {0} has exist", id.ToString());
                }
            }
        }

        #region Properties for return entry value
        public Int32 SPTYear
        {
            get { return Convert.ToInt32(txtSPTYear.Value); }
        }
        public String SRTaxStatus
        {
            get { return cboSRTaxStatus.SelectedValue; }
        }
        public String TaxStatusName
        {
            get { return cboSRTaxStatus.Text; }
        }
        #endregion
    }
}