using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PatientIncidentRelatedUnitItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            ComboBox.PopulateWithServiceUnit(cboServiceUnitID, false);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboServiceUnitID.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentRelatedUnitMetadata.ColumnNames.ServiceUnitID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientIncidentRelatedUnitCollection)Session["collPatientIncidentRelatedUnit"];

                string unitId = cboServiceUnitID.SelectedValue;
                bool isExist = false;

                foreach (PatientIncidentRelatedUnit item in coll)
                {
                    if (item.ServiceUnitID.Equals(unitId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Service Unit : {0} already exist", cboServiceUnitID.Text);
                }
            }
        }

        #region Properties for return entry value

        public String ServiceUnitID
        {
            get { return cboServiceUnitID.SelectedValue; }
        }

        public String ServiceUnitName
        {
            get { return cboServiceUnitID.Text; }
        }

        #endregion
    }
}