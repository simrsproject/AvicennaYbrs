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

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskGradingMtxItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRIncidentProbabilityFrequency, AppEnum.StandardReference.IncidentProbabilityFrequency);
            StandardReference.InitializeIncludeSpace(cboSRIncidentFollowUp, AppEnum.StandardReference.IncidentFollowUp);

            var riskGradings = new RiskGradingCollection();
            riskGradings.Query.OrderBy(riskGradings.Query.RiskGradingID.Ascending);
            riskGradings.LoadAll();
            cboRiskGradingID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var item in riskGradings)
            {
                cboRiskGradingID.Items.Add(new RadComboBoxItem(item.RiskGradingName, item.RiskGradingID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboSRIncidentProbabilityFrequency.Enabled = false;
            cboSRIncidentFollowUp.Enabled = false;

            cboSRIncidentProbabilityFrequency.SelectedValue = (String)DataBinder.Eval(DataItem, RiskGradingMtxMetadata.ColumnNames.SRIncidentProbabilityFrequency);
            cboSRIncidentFollowUp.SelectedValue = (String)DataBinder.Eval(DataItem, RiskGradingMtxMetadata.ColumnNames.SRIncidentFollowUp);
            cboRiskGradingID.SelectedValue = (String)DataBinder.Eval(DataItem, RiskGradingMtxMetadata.ColumnNames.RiskGradingID);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (RiskGradingMtxCollection)Session["collRiskGradingMtx"];

                string probId = cboSRIncidentProbabilityFrequency.SelectedValue;
                bool isExist = false;
                foreach (RiskGradingMtx item in coll)
                {
                    if (item.SRIncidentProbabilityFrequency.Equals(probId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator) source).ErrorMessage =
                        string.Format("Incident Probability Frequency : {0} already exist",
                                      cboSRIncidentProbabilityFrequency.Text);
                }
            }
        }

        #region Properties for return entry value

        public String SRIncidentProbabilityFrequency
        {
            get { return cboSRIncidentProbabilityFrequency.SelectedValue; }
        }

        public String IncidentProbabilityFrequency
        {
            get { return cboSRIncidentProbabilityFrequency.Text; }
        }

        public String SRIncidentFollowUp
        {
            get { return cboSRIncidentFollowUp.SelectedValue; }
        }

        public String IncidentFollowUp
        {
            get { return cboSRIncidentFollowUp.Text; }
        }

        public String RiskGradingID
        {
            get { return cboRiskGradingID.SelectedValue; }
        }

        public String RiskGradingName
        {
            get { return cboRiskGradingID.Text; }
        }

        #endregion
    }
}