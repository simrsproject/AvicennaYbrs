using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionDutyDetail : BaseUserControl
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
                //misal --> chkIsActive.Checked = true;               
                return;
            }
            ViewState["IsNewRecord"] = false;

            txtDutyName.Text = (String)DataBinder.Eval(DataItem, PositionDutyMetadata.ColumnNames.DutyName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, PositionDutyMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionDutyCollection coll = (PositionDutyCollection)Session["collPositionDuty"];

                //TODO: Betulkan cara pengecekannya
                string DutyName = txtDutyName.Text;
                bool isExist = false;
                foreach (PositionDuty item in coll)
                {
                    if (item.DutyName.Equals(DutyName))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("DutyName: {0} has exist", DutyName);
                }
            }
        }

        #region Properties for return entry value
               

        public String DutyName
        {
            get { return txtDutyName.Text; }
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
