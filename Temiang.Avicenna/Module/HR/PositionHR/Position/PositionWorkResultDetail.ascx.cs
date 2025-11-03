using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;


namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionWorkResultDetail : BaseUserControl
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

            txtWorkResultName.Text = (String)DataBinder.Eval(DataItem, PositionWorkResultMetadata.ColumnNames.WorkResultName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, PositionWorkResultMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionWorkResultCollection coll =
                    (PositionWorkResultCollection)Session["collPositionWorkResult"];

                //TODO: Betulkan cara pengecekannya
                string workResult = txtWorkResultName.Text;
                bool isExist = false;
                foreach (PositionWorkResult item in coll)
                {
                    if (item.WorkResultName.Equals(workResult))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Work Result: {0} has exist", workResult);
                }
            }
        }

        #region Properties for return entry value

        public String WorkResultName
        {
            get { return txtWorkResultName.Text; }
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