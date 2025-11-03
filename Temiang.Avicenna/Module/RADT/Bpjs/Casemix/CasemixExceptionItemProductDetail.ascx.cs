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
    public partial class CasemixExceptionItemProductDetail : BaseUserControl
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

                PopUpSearch.InitializeOnButtonClick(AppEnum.PopUpSearch.ItemProductMedical, txtItemID);

                chkIsUsingGlobalSetting.Checked = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtItemID.ReadOnly = true;
            txtItemID.Text = (String)DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.ItemID);
            txtItemID_TextChanged(null, null);

            chkIsUsingGlobalSetting.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsUsingGlobalSetting));

            chkIsNeedCasemixValidate.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidate));
            chkIsAllowedToOrder.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrder));

            chkIsNeedCasemixValidateIpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateIpr));
            chkIsAllowedToOrderIpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderIpr));

            chkIsNeedCasemixValidateOpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateOpr));
            chkIsAllowedToOrderOpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderOpr));

            chkIsNeedCasemixValidateEmr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsNeedCasemixValidateEmr));
            chkIsAllowedToOrderEmr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, CasemixCoveredDetailMetadata.ColumnNames.IsAllowedToOrderEmr));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (CasemixCoveredDetailCollection)Session["collCasemixCoveredDetailItemProduct"];

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

        public bool IsUsingGlobalSetting
        {
            get { return chkIsUsingGlobalSetting.Checked; }
        }

        public bool IsNeedCasemixValidate
        {
            get { return chkIsNeedCasemixValidate.Checked; }
        }

        public bool IsAllowedToOrder
        {
            get { return chkIsAllowedToOrder.Checked; }
        }

        public bool IsNeedCasemixValidateIpr
        {
            get { return chkIsNeedCasemixValidateIpr.Checked; }
        }

        public bool IsAllowedToOrderIpr
        {
            get { return chkIsAllowedToOrderIpr.Checked; }
        }

        public bool IsNeedCasemixValidateOpr
        {
            get { return chkIsNeedCasemixValidateOpr.Checked; }
        }

        public bool IsAllowedToOrderOpr
        {
            get { return chkIsAllowedToOrderOpr.Checked; }
        }

        public bool IsNeedCasemixValidateEmr
        {
            get { return chkIsNeedCasemixValidateEmr.Checked; }
        }

        public bool IsAllowedToOrderEmr
        {
            get { return chkIsAllowedToOrderEmr.Checked; }
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