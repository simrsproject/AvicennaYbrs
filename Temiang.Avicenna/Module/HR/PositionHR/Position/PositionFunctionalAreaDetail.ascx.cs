using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionFunctionalAreaDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRPositionFunctionalArea, AppEnum.StandardReference.PositionFunctionalArea);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtPositionFunctionalAreaID.Text = "1";
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtPositionFunctionalAreaID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PositionFunctionalAreaMetadata.ColumnNames.PositionFunctionalAreaID));
            cboSRPositionFunctionalArea.SelectedValue = (String)DataBinder.Eval(DataItem, PositionFunctionalAreaMetadata.ColumnNames.SRPositionFunctionalArea);
            txtDescription.Text= (String)DataBinder.Eval(DataItem, PositionFunctionalAreaMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionFunctionalAreaCollection coll =
                    (PositionFunctionalAreaCollection)Session["collPositionFunctionalArea"];

                //TODO: Betulkan cara pengecekannya
                string id = txtPositionFunctionalAreaID.Text;
                bool isExist = false;
                foreach (PositionFunctionalArea item in coll)
                {
                    if (item.PositionID.Equals(id))
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
        public Int32 PositionFunctionalAreaID
        {
            get { return Convert.ToInt32(txtPositionFunctionalAreaID.Text); }
        }
        public String SRPositionFunctionalArea
        {
            get { return cboSRPositionFunctionalArea.SelectedValue; }
        }
        public String PositionFunctionalAreaName
        {
            get { return cboSRPositionFunctionalArea.Text; }
        }

        public String Description
        {
            get { return txtDescription.Text; }
        }

        #endregion
        #region Method & Event TextChanged

        #endregion
    }
}
