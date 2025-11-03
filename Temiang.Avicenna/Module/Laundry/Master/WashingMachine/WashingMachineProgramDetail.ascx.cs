using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class WashingMachineProgramDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRLaundryProgram, AppEnum.StandardReference.LaundryProgram);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                cboSRLaundryProgram.Enabled = true;
                return;
            }
            cboSRLaundryProgram.Enabled = false;
            ViewState["IsNewRecord"] = false;
            cboSRLaundryProgram.SelectedValue = (String)DataBinder.Eval(DataItem, LaundryWashingMachineProgramMetadata.ColumnNames.SRLaundryProgram);
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
        }

        #region Properties for return entry value
        public String SRLaundryProgram
        {
            get { return cboSRLaundryProgram.SelectedValue; }
        }
        public String LaundryProgramName
        {
            get { return cboSRLaundryProgram.Text; }
        }
        #endregion
    }
}