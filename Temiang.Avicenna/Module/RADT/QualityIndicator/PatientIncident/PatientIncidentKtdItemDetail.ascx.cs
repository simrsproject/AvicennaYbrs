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
    public partial class PatientIncidentKtdItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRIncidentKTD, AppEnum.StandardReference.IncidentKtd);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboSRIncidentKTD.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentKTDMetadata.ColumnNames.SRIncidentKTD);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientIncidentKTDCollection)Session["collPatientIncidentKTD"];

                string ktdId = cboSRIncidentKTD.SelectedValue;
                bool isExist = false;

                foreach (PatientIncidentKTD item in coll)
                {
                    if (item.SRIncidentKTD.Equals(ktdId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("KTD : {0} already exist", cboSRIncidentKTD.Text);
                }
            }
        }

        #region Properties for return entry value

        public String SRIncidentKTD
        {
            get { return cboSRIncidentKTD.SelectedValue; }
        }

        public String IncidentKTD
        {
            get { return cboSRIncidentKTD.Text; }
        }

        #endregion
    }
}