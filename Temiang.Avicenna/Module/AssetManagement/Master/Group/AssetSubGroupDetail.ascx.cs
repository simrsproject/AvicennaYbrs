using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Master
{
    public partial class AssetSubGroupDetail : BaseUserControl
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
                chkIsActive.Checked = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtAssetSubGroupId.Text = (String)DataBinder.Eval(DataItem, AssetSubGroupMetadata.ColumnNames.AssetSubGroupId);
            txtAssetSubGroupName.Text = (String)DataBinder.Eval(DataItem, AssetSubGroupMetadata.ColumnNames.AssetSubGroupName);
            txtInitial.Text = (String)DataBinder.Eval(DataItem, AssetSubGroupMetadata.ColumnNames.Initial);
            chkIsActive.Checked = (bool)DataBinder.Eval(DataItem, AssetSubGroupMetadata.ColumnNames.IsActive);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (AssetSubGroupCollection)Session["collAssetSubGroup"];

                string id = txtAssetSubGroupId.Text;
                bool isExist = false;

                foreach (AssetSubGroup item in coll)
                {
                    if (item.AssetSubGroupId.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("ID : {0} already exist", id);
                }
            }
        }

        #region Properties for return entry value

        public String AssetSubGroupId
        {
            get { return txtAssetSubGroupId.Text; }
        }
        public string AssetSubGroupName
        {
            get { return txtAssetSubGroupName.Text; }
        }
        public string Initial
        {
            get { return txtInitial.Text; }
        }
        public bool IsActive
        {
            get { return chkIsActive.Checked; }
        }

        #endregion
    }
}