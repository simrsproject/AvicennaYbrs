using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class OpeningBalanceCashier : BaseUserControl
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

                return;
            }
            ViewState["IsNewRecord"] = false;

            var usr = new AppUserQuery("a");
            usr.Select(usr.UserID,usr.UserName);
            usr.Where(usr.UserID == (String)DataBinder.Eval(DataItem, CashManagementCashierMetadata.ColumnNames.CashierUserID));
            DataTable tbl = usr.LoadDataTable();
            cboCashierUserID.DataSource = tbl;
            cboCashierUserID.DataBind();
            
            ComboBox.SelectedValue(cboCashierUserID, (String)DataBinder.Eval(DataItem, CashManagementCashierMetadata.ColumnNames.CashierUserID));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CashManagementCashierCollection)Session["collCashManagementCashier"];

                string cashierId = cboCashierUserID.SelectedValue;
                bool isExist = false;
                bool isCheckin = false;
                foreach (CashManagementCashier item in coll)
                {
                    if (item.CashierUserID.Equals(cashierId))
                    {
                        isExist = true;
                        break;
                    }

                    var usr = new AppUser();
                    if (usr.LoadByPrimaryKey(cashierId) && !string.IsNullOrEmpty(usr.CashManagementNo))
                    {
                        isCheckin = true;
                        break;
                    }

                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Cashier User ID: {0} has exist", cashierId);
                }

                if (isCheckin)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Cashier User ID: {0} has checkin", cashierId);
                }
            }
        }

        #region Properties for return entry value

        public String CashierUserID
        {
            get { return cboCashierUserID.SelectedValue; }
        }

        public String CashierUserName
        {
            get { return cboCashierUserID.Text; }
        }
        #endregion

        #region Combobox
        protected void cboCashierUserID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.UserByUnitItemRequested((RadComboBox)sender, AppSession.Parameter.ServiceUnitCashierID, e.Text);
        }

        protected void cboCashierUserID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.UserItemDataBound(e);
        }
        #endregion
    }
}