using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Bpjs
{
    public partial class CasemixExceptionItemDetail : BaseUserControl
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

                PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ItemServiceExcludeProduct, txtItemID);

                txtQty.Value = 0;
                txtQtyIpr.Value = 0;
                txtQtyOpr.Value = 0;
                txtQtyEmr.Value = 0;

                chkIsUsingGlobalSetting.Checked = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.ReadOnly = true;
            txtItemID.Text = (String)DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.ItemID);
            txtItemID_TextChanged(null, null);

            chkIsUsingGlobalSetting.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsUsingGlobalSetting));

            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.Qty));
            txtQtyIpr.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.QtyIpr));
            txtQtyOpr.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.QtyOpr));
            txtQtyEmr.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.QtyEmr));

            chkIsNeedCasemixValidate.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidate));
            chkIsNeedCasemixValidateIpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateIpr));
            chkIsNeedCasemixValidateOpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateOpr));
            chkIsNeedCasemixValidateEmr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateEmr));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CasemixCoveredDetailCollection)Session["collCasemixCoveredDetail"];

                string itemID = txtItemID.Text;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID : {0} {1} already exist", itemID, lblItemName.Text);
                }
            }
        }

        public string ItemID
        {
            get { return txtItemID.Text; }
        }

        public string ItemName
        {
            get { return lblItemName.Text; }
        }

        public double Qty
        {
            get { return txtQty.Value ?? 0; }
        }

        public double QtyIpr
        {
            get { return txtQtyIpr.Value ?? 0; }
        }

        public double QtyOpr
        {
            get { return txtQtyOpr.Value ?? 0; }
        }

        public double QtyEmr
        {
            get { return txtQtyEmr.Value ?? 0; }
        }

        public bool IsUsingGlobalSetting
        {
            get { return chkIsUsingGlobalSetting.Checked; }
        }

        public bool IsNeedCasemixValidate
        {
            get { return chkIsNeedCasemixValidate.Checked; }
        }

        public bool IsNeedCasemixValidateIpr
        {
            get { return chkIsNeedCasemixValidateIpr.Checked; }
        }

        public bool IsNeedCasemixValidateOpr
        {
            get { return chkIsNeedCasemixValidateOpr.Checked; }
        }

        public bool IsNeedCasemixValidateEmr
        {
            get { return chkIsNeedCasemixValidateEmr.Checked; }
        }

        protected void txtItemID_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemID.Text)) lblItemName.Text = string.Empty;
            else
            {
                var item = new Item();
                if (item.LoadByPrimaryKey(txtItemID.Text))
                    lblItemName.Text = item.ItemName;
                else
                    lblItemName.Text = string.Empty;
            }
        }
    }
}