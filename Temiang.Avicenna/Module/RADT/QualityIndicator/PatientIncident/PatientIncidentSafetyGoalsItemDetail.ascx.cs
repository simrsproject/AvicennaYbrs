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
    public partial class PatientIncidentSafetyGoalsItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRSafetyGoals, AppEnum.StandardReference.SafetyGoals);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboSRSafetyGoals.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentSafetyGoalsMetadata.ColumnNames.SRSafetyGoals);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientIncidentSafetyGoalsCollection)Session["collPatientIncidentSafetyGoals"];

                string id = cboSRSafetyGoals.SelectedValue;
                bool isExist = false;

                foreach (PatientIncidentSafetyGoals item in coll)
                {
                    if (item.SRSafetyGoals.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Safety Goal : {0} already exist", cboSRSafetyGoals.Text);
                }
            }
        }

        #region Properties for return entry value

        public String SRSafetyGoals
        {
            get { return cboSRSafetyGoals.SelectedValue; }
        }

        public String SafetyGoals
        {
            get { return cboSRSafetyGoals.Text; }
        }

        public String Recommendation
        {
            get { return txtRecommendation.Text; }
        }

        #endregion
    }
}