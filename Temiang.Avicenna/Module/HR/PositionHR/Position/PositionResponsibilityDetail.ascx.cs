using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionResponsibilityDetail : BaseUserControl
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
                ////misal --> chkIsActive.Checked = true;
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtResponsibilityName.Text = (String)DataBinder.Eval(DataItem, PositionResponsibilityMetadata.ColumnNames.ResponsibilityName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, PositionResponsibilityMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionResponsibilityCollection coll =
                    (PositionResponsibilityCollection)Session["collPositionResponsibility"];

                //TODO: Betulkan cara pengecekannya
                string responsibility = txtResponsibilityName.Text;
                bool isExist = false;
                foreach (PositionResponsibility item in coll)
                {
                    if (item.ResponsibilityName.Equals(responsibility))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Responsibility: {0} has exist", responsibility);
                }
            }
        }

        #region Properties for return entry value

        public String ResponsibilityName
        {
            get { return txtResponsibilityName.Text; }
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
