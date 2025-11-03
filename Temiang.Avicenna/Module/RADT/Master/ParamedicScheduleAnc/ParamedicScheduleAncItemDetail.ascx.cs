using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Globalization;

namespace Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleAnc
{
    public partial class ParamedicScheduleAncItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var otcoll = new OperationalTimeCollection();
            otcoll.Query.Where(
                otcoll.Query.StartTime2 == string.Empty, 
                otcoll.Query.StartTime3 == string.Empty, 
                otcoll.Query.StartTime4 == string.Empty, 
                otcoll.Query.StartTime5 == string.Empty);
            otcoll.LoadAll();
            cboOperationalTimeID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var ot in otcoll)
            {
                cboOperationalTimeID.Items.Add(new RadComboBoxItem(ot.OperationalTimeName, ot.OperationalTimeID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                ViewState["ParamedicID"] = string.Empty;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboOperationalTimeID.Enabled = false;

            cboOperationalTimeID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicScheduleDateItemMetadata.ColumnNames.OperationalTimeID);
            chkIsIpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ParamedicScheduleDateItemMetadata.ColumnNames.IsIpr));
            chkIsOpr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ParamedicScheduleDateItemMetadata.ColumnNames.IsOpr));
            chkIsEmr.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, ParamedicScheduleDateItemMetadata.ColumnNames.IsEmr));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicScheduleDateItemCollection)Session["collParamedicScheduleDateItem"];

                string id = cboOperationalTimeID.SelectedValue;
                bool isExist = false;
                foreach (var item in coll)
                {
                    if (item.OperationalTimeID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Operational Time : {0} already exist", cboOperationalTimeID.Text);
                }
            }
        }

        public String OperationalTimeID
        {
            get { return cboOperationalTimeID.SelectedValue; }
        }

        public String OperationalTimeName
        {
            get { return cboOperationalTimeID.Text; }
        }

        public bool IsIpr
        {
            get { return chkIsIpr.Checked; }
        }

        public bool IsOpr
        {
            get { return chkIsOpr.Checked; }
        }

        public bool IsEmr
        {
            get { return chkIsEmr.Checked; }
        }
    }
}