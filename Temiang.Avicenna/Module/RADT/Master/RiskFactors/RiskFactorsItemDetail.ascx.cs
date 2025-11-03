using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class RiskFactorsItemDetail : BaseUserControl
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
                return;
            }
            ViewState["IsNewRecord"] = false;
            txtRiskFactorsID.ReadOnly = true;

            txtRiskFactorsID.Text = (String)DataBinder.Eval(DataItem, RiskFactorsMetadata.ColumnNames.RiskFactorsID);
            txtRiskFactorsName.Text = (String)DataBinder.Eval(DataItem, RiskFactorsMetadata.ColumnNames.RiskFactorsName);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (RiskFactorsCollection)Session["collRiskFactors"];

                string id = txtRiskFactorsID.Text;
                bool isExist = false;
                foreach (RiskFactors item in coll)
                {
                    if (item.RiskFactorsID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Risk Factors ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value

        public String RiskFactorsID
        {
            get { return txtRiskFactorsID.Text; }
        }

        public String RiskFactorsName
        {
            get { return txtRiskFactorsName.Text; }
        }

        #endregion
    }
}