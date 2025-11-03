using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Stock
{
    public partial class BalanceDetailExpiredDateItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadTextBox TxtLocationId
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtLocationID");
            }
        }
        private RadTextBox TxtItemId
        {
            get
            {
                return (RadTextBox)Helper.FindControlRecursive(Page, "txtItemID");
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            txtBalance.Enabled = false;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtBalance.Value = 0;
                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtBatchNumber.ReadOnly = true;
            txtExpiredDate.Enabled = false;

            txtBatchNumber.Text = (string)DataBinder.Eval(DataItem, ItemBalanceDetailEdMetadata.ColumnNames.BatchNumber);

            object expiredDate = DataBinder.Eval(DataItem, ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate);
            if (expiredDate != null)
                txtExpiredDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, ItemBalanceDetailEdMetadata.ColumnNames.ExpiredDate);
            else
                txtExpiredDate.Clear();

            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ItemBalanceDetailEdMetadata.ColumnNames.IsActive));
            txtBalance.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemBalanceDetailEdMetadata.ColumnNames.Balance));
            //if (txtBalance.Value > 0)
            //    chkIsActive.Enabled = false;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtExpiredDate.SelectedDate != null)
            {
                if (txtExpiredDate.SelectedDate < (new DateTime()).NowAtSqlServer()) //DateTime.Now
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Expired date must greather than now.");
                    return;
                }
            }
            else
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Expired Date is required.");
                return;
            }

            if (txtBalance.Value > 0 && !chkIsActive.Checked)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Status active is required.");
                return;
            }

            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ItemBalanceDetailEdCollection)Session["collItemBalanceDetailEd" + TxtLocationId.Text + TxtItemId.Text + Request.UserHostName];

                DateTime ed = txtExpiredDate.SelectedDate.Value.Date;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.BatchNumber.Trim() == txtBatchNumber.Text.Trim() && item.ExpiredDate.Value.Date.Equals(ed))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Batch Number {1} With Expired Date : {0} already exist", ed.ToString("dd-MMM-yyyy"), txtBatchNumber.Text);
                }
            }
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
        }

        #region Properties for return entry value
        public String BatchNumber
        {
            get { return txtBatchNumber.Text.Trim(); }
        }
        public DateTime? ExpiredDate
        {
            get { return txtExpiredDate.SelectedDate; }
        }
        public Decimal Balance
        {
            get { return Convert.ToDecimal(txtBalance.Value); }
        }
        public bool IsActive
        {
            get { return chkIsActive.Checked; }
        }

        public bool IsNewRecord
        {
            get { return Convert.ToBoolean(ViewState["IsNewRecord"]); }
        }

        #endregion
    }
}