using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class OvertimeFormulaItemDetail : BaseUserControl
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

                txtOvertimeDetailID.Text = "1";
                return;
            }

            ViewState["IsNewRecord"] = false;

            txtOvertimeDetailID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, OvertimeDetailMetadata.ColumnNames.OvertimeDetailID));
            txtHourFrom.Value = Convert.ToDouble(DataBinder.Eval(DataItem, OvertimeDetailMetadata.ColumnNames.HourFrom));
            txtHourTo.Value = Convert.ToDouble(DataBinder.Eval(DataItem, OvertimeDetailMetadata.ColumnNames.HourTo));
            txtValue.Value = Convert.ToDouble(DataBinder.Eval(DataItem, OvertimeDetailMetadata.ColumnNames.Value));
            txtFormula.Value = Convert.ToDouble(DataBinder.Eval(DataItem, OvertimeDetailMetadata.ColumnNames.Formula));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (OvertimeDetailCollection)Session["collOvertimeDetail"];

                string id = txtOvertimeDetailID.Text;
                bool isExist = false;
                foreach (OvertimeDetail item in coll)
                {
                    if (item.OvertimeDetailID.Equals(id))
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
        public Int64 OvertimeDetailID
        {
            get { return Convert.ToInt64(txtOvertimeDetailID.Text); }
        }
        public Decimal HourFrom
        {
            get { return Convert.ToDecimal(txtHourFrom.Value); }
        }
        public Decimal HourTo
        {
            get { return Convert.ToDecimal(txtHourTo.Value); }
        }
        public Decimal Value
        {
            get { return Convert.ToDecimal(txtValue.Value); }
        }
        public Decimal Formula
        {
            get { return Convert.ToDecimal(txtFormula.Value); }
        }
        #endregion
    }
}