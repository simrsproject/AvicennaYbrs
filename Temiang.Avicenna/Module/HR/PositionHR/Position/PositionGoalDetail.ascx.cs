using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.PositionInformation
{
    public partial class PositionGoalDetail : BaseUserControl
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

            txtGoalName.Text = (String)DataBinder.Eval(DataItem, PositionGoalMetadata.ColumnNames.GoalName);
            txtDescription.Text = (String)DataBinder.Eval(DataItem, PositionGoalMetadata.ColumnNames.Description);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                PositionGoalCollection coll = (PositionGoalCollection)Session["collPositionGoal"];

                //TODO: Betulkan cara pengecekannya
                string goalName = txtGoalName.Text;
                bool isExist = false;
                foreach (PositionGoal item in coll)
                {
                    if (item.GoalName.Equals(GoalName))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Goal Name: {0} has exist", goalName);
                }
            }
        }

        #region Properties for return entry value
        public String GoalName
        {
            get { return txtGoalName.Text; }
        }

        public String Description
        {
            get { return txtDescription.Text; }
        }
        #endregion
    }
}