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

namespace Temiang.Avicenna.Module.Cssd.Master
{
    public partial class MachineItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRCssdProcessType, AppEnum.StandardReference.CssdProcessType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                cboSRCssdProcessType.Enabled = true;
                return;
            }
            cboSRCssdProcessType.Enabled = false;
            ViewState["IsNewRecord"] = false;
            cboSRCssdProcessType.SelectedValue = (String)DataBinder.Eval(DataItem, CssdMachineItemMetadata.ColumnNames.SRCssdProcessType);
            txtDuration.Value = Convert.ToDouble(DataBinder.Eval(DataItem, CssdMachineItemMetadata.ColumnNames.Duration));
        }
        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtDuration.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Duration Must Bigger than 0");
                return;
            }
        }

        #region Properties for return entry value
        public String SRCssdProcessType
        {
            get { return cboSRCssdProcessType.SelectedValue; }
        }
        public String CssdProcessType
        {
            get { return cboSRCssdProcessType.Text; }
        }
        public int Duration
        {
            get { return Convert.ToInt32(txtDuration.Value); }
        }
        #endregion

        #region Method & Event TextChanged
        #endregion
    }
}