using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StandardSalaryFaktorDetail : BaseUserControl
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

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtStandardSalaryFaktorID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtGradeServiceYear.Enabled = false;
            txtStandardSalaryFaktorID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, StandardSalaryFaktorMetadata.ColumnNames.StandardSalaryFaktorID));
            txtGradeServiceYear.Value = Convert.ToDouble(DataBinder.Eval(DataItem, StandardSalaryFaktorMetadata.ColumnNames.GradeServiceYear));
            txtAmountSalary.Value = Convert.ToDouble(DataBinder.Eval(DataItem, StandardSalaryFaktorMetadata.ColumnNames.AmountSalary));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                StandardSalaryFaktorCollection coll =
                    (StandardSalaryFaktorCollection)Session["collStandardSalaryFaktor"];

                //TODO: Betulkan cara pengecekannya
                string id = txtGradeServiceYear.Text;
                bool isExist = false;
                foreach (StandardSalaryFaktor item in coll)
                {
                    if (item.StandardSalaryID.Equals(id))
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
        public Int32 StandardSalaryFaktorID
        {
            get { return Convert.ToInt32(txtStandardSalaryFaktorID.Text); }
        }
        public Int32 GradeServiceYear
        {
            get { return Convert.ToInt32(txtGradeServiceYear.Text); }
        }
        public Decimal AmountSalary
        {
            get { return Convert.ToDecimal(txtAmountSalary.Value); }
        }
        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
