using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class ThrScheduleItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtCounterItemID.Text = "1";

                return;
            }
            ViewState["IsNewRecord"] = false;

            txtCounterItemID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ThrScheduleItemMetadata.ColumnNames.CounterItemID));
            cboSRReligion.SelectedValue = (String)DataBinder.Eval(DataItem, ThrScheduleItemMetadata.ColumnNames.SRReligion);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ThrScheduleItemCollection coll =
                    (ThrScheduleItemCollection)Session["collThrScheduleItem"];

                //TODO: Betulkan cara pengecekannya
                string id = cboSRReligion.SelectedValue;
                bool isExist = false;
                foreach (ThrScheduleItem item in coll)
                {
                    if (item.SRReligion.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Religion: {0} has exist", cboSRReligion.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 CounterItemID
        {
            get { return Convert.ToInt32(txtCounterItemID.Text); }
        }
        public String SRReligion
        {
            get { return cboSRReligion.SelectedValue; }
        }
        public String ReligionName
        {
            get { return cboSRReligion.Text; }
        }

        #endregion
    }
}