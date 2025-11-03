using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClassAliasDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboBridgingType, AppEnum.StandardReference.BridgingType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            cboBridgingType.SelectedValue = (String)DataBinder.Eval(DataItem, ClassBridgingMetadata.ColumnNames.SRBridgingType);
            cboBridgingType_SelectedIndexChanged(null, new RadComboBoxSelectedIndexChangedEventArgs(string.Empty, string.Empty, cboBridgingType.SelectedValue, string.Empty));
            cboServiceUnitAliasID.SelectedValue = (String)DataBinder.Eval(DataItem, ClassBridgingMetadata.ColumnNames.BridgingID);
            txtServiceUnitAliasName.Text = (String)DataBinder.Eval(DataItem, ClassBridgingMetadata.ColumnNames.BridgingName);
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ClassBridgingMetadata.ColumnNames.IsActive));
        }

        protected void cboBridgingType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitAliasID.Items.Clear();
            cboServiceUnitAliasID.SelectedValue = string.Empty;
            cboServiceUnitAliasID.Text = string.Empty;

            if (e.Value == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
            }
            else if (e.Value == AppEnum.BridgingType.Inhealth.ToString() && Common.Helper.IsInhealthIntegration)
            {
                var collTitle = new AppStandardReferenceItemCollection();
                collTitle.Query.Where(
                    collTitle.Query.StandardReferenceID == AppEnum.StandardReference.InhealthClassType,
                    collTitle.Query.IsActive == true
                    );
                collTitle.Query.OrderBy(collTitle.Query.ItemID.Ascending);
                collTitle.LoadAll();
                cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var item in collTitle)
                {
                    cboServiceUnitAliasID.Items.Add(new RadComboBoxItem(item.ItemID + " - " + item.ItemName, item.ItemID));
                }
            }
        }

        protected void cboServiceUnitAliasID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboBridgingType.SelectedValue == AppEnum.BridgingType.BPJS.ToString() && Common.Helper.IsBpjsIntegration)
            {
            }
            else if (cboBridgingType.SelectedValue == AppEnum.BridgingType.Inhealth.ToString() && Common.Helper.IsInhealthIntegration)
            {
                var collTitle = new AppStandardReferenceItem();
                collTitle.Query.es.Top = 1;
                collTitle.Query.Where(
                    collTitle.Query.StandardReferenceID == AppEnum.StandardReference.InhealthClassType,
                    collTitle.Query.ItemID == cboServiceUnitAliasID.SelectedValue
                    );
                if (collTitle.Query.Load()) txtServiceUnitAliasName.Text = collTitle.ItemName;
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ClassBridgingCollection)Session["collClassBridging"];

                string itemID = cboServiceUnitAliasID.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.BridgingID.Equals(itemID) && item.SRBridgingType.Equals(cboBridgingType.SelectedValue))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Bridging ID : {0} already exist", itemID);
                }
            }
        }

        public String BridgingType
        {
            get { return cboBridgingType.SelectedValue; }
        }

        public String BridgingTypeName
        {
            get { return cboBridgingType.Text; }
        }

        public String BridgingID
        {
            get { return string.IsNullOrEmpty(cboServiceUnitAliasID.SelectedValue) ? cboServiceUnitAliasID.Text : cboServiceUnitAliasID.SelectedValue; }
        }

        public String BridgingName
        {
            get { return txtServiceUnitAliasName.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
    }
}