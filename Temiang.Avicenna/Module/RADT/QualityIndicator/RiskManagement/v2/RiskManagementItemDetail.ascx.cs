using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator.v2
{
    public partial class RiskManagementItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRRiskManagementCategory, AppEnum.StandardReference.RiskManagementCategory);
            StandardReference.InitializeIncludeSpace(cboSRRiskManagementImpact, AppEnum.StandardReference.RiskManagementImpact);
            StandardReference.InitializeIncludeSpace(cboSRRiskManagementProbability, AppEnum.StandardReference.RiskManagementProbability);
            StandardReference.InitializeIncludeSpace(cboSRRiskManagementBands, AppEnum.StandardReference.RiskManagementBands);
            StandardReference.InitializeIncludeSpace(cboSRRiskManagementControlling, AppEnum.StandardReference.RiskManagementControlling);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (RiskManagementItemCollection)Session["collRiskManagementItem" + Request.UserHostName];
                if (coll.Count == 0)
                    hdnSequenceNo.Value = "001";
                else
                {
                    var seqNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int id = Convert.ToInt32(seqNo.Single()) + 1;

                    hdnSequenceNo.Value = string.Format("{0:000}", id);
                }

                txtImpactScore.Value = 0;
                txtProbabilityScore.Value = 0;
                txtRiskScore.Value = 0;
                btnRiskScore.Text = "0";
                txtTotalScore.Value = 0;
                btnTotalScore.Text = "0";

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboSRRiskManagementCategory.SelectedValue = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.SRRiskManagementCategory);
            txtRiskManagementDescription.Text = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.RiskManagementDescription);
            cboSRRiskManagementImpact.SelectedValue = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.SRRiskManagementImpact);
            txtImpactScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.ImpactScore));
            cboSRRiskManagementProbability.SelectedValue = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.SRRiskManagementProbability);
            txtProbabilityScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.ProbabilityScore));
            txtRiskScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.RiskScore));
            btnRiskScore.Text = txtRiskScore.Value.ToString();

            cboSRRiskManagementBands.SelectedValue = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.SRRiskManagementBands);
            BandsColor(cboSRRiskManagementBands.SelectedValue);

            cboSRRiskManagementControlling.SelectedValue = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.SRRiskManagementControlling);
            txtControllingScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.ControllingScore));
            txtTotalScore.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.TotalScore));
            btnTotalScore.Text = txtTotalScore.Value.ToString();

            txtRiskRating.Value = Convert.ToDouble(DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.RiskRating));

            txtReason.Text = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.Reason);
            txtAction.Text = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.Action);
            txtPic.Text = (String)DataBinder.Eval(DataItem, RiskManagementItemMetadata.ColumnNames.Pic);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return hdnSequenceNo.Value; }
        }

        public String SRRiskManagementCategory
        {
            get { return cboSRRiskManagementCategory.SelectedValue; }
        }

        public String RiskManagementCategoryName
        {
            get { return cboSRRiskManagementCategory.Text; }
        }

        public String RiskManagementDescription
        {
            get { return txtRiskManagementDescription.Text; }
        }

        public String SRRiskManagementImpact
        {
            get { return cboSRRiskManagementImpact.SelectedValue; }
        }

        public String RiskManagementImpactName
        {
            get { return cboSRRiskManagementImpact.Text; }
        }

        public Int16 ImpactScore
        {
            get { return Convert.ToInt16(txtImpactScore.Value); }
        }

        public String SRRiskManagementProbability
        {
            get { return cboSRRiskManagementProbability.SelectedValue; }
        }

        public String RiskManagementProbabilityName
        {
            get { return cboSRRiskManagementProbability.Text; }
        }

        public Int16 ProbabilityScore
        {
            get { return Convert.ToInt16(txtProbabilityScore.Value); }
        }

        public Int16 RiskScore
        {
            get { return Convert.ToInt16(txtRiskScore.Value); }
        }

        public String SRRiskManagementBands
        {
            get { return cboSRRiskManagementBands.SelectedValue; }
        }

        public String RiskManagementBandsName
        {
            get { return cboSRRiskManagementBands.Text; }
        }

        public String RiskManagementBandsColor
        {
            get 
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.RiskManagementBands.ToString(), cboSRRiskManagementBands.SelectedValue))
                {
                    return std.ReferenceID;
                }
                
                return "#FFFFFF"; 
            }
        }

        public String SRRiskManagementControlling
        {
            get { return cboSRRiskManagementControlling.SelectedValue; }
        }

        public String RiskManagementControllingName
        {
            get { return cboSRRiskManagementControlling.Text; }
        }

        public Int16 ControllingScore
        {
            get { return Convert.ToInt16(txtControllingScore.Value); }
        }

        public Int16 TotalScore
        {
            get { return Convert.ToInt16(txtTotalScore.Value); }
        }

        public Int16 RiskRating
        {
            get { return Convert.ToInt16(txtRiskRating.Value); }
        }

        public String Reason
        {
            get { return txtReason.Text; }
        }

        public String Action
        {
            get { return txtAction.Text; }
        }

        public String Pic
        {
            get { return txtPic.Text; }
        }
        #endregion

        #region Autopostback
        protected void cboSRRiskManagementImpact_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
                txtImpactScore.Value = GetNumericValue(AppEnum.StandardReference.RiskManagementImpact.ToString(), e.Value);
            else
                txtImpactScore.Value = 0;
            ScoreCalculation();
        }

        protected void cboSRRiskManagementProbability_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
                txtProbabilityScore.Value = GetNumericValue(AppEnum.StandardReference.RiskManagementProbability.ToString(), e.Value);
            else
                txtProbabilityScore.Value = 0;
            ScoreCalculation();
        }

        protected void cboSRRiskManagementControlling_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
                txtControllingScore.Value = GetNumericValue(AppEnum.StandardReference.RiskManagementControlling.ToString(), e.Value);
            else
                txtControllingScore.Value = 0;
            ScoreCalculation();
        }

        protected void cboSRRiskManagementBands_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BandsColor(e.Value);
        }

        private double GetNumericValue(string appStdId, string itemId)
        {
            double retval = 0;
            var std = new AppStandardReferenceItem();
            if (std.LoadByPrimaryKey(appStdId, itemId))
                retval = Convert.ToInt16(std.NumericValue);

            return retval;
        }

        private void ScoreCalculation()
        {
            txtRiskScore.Value = txtImpactScore.Value * txtProbabilityScore.Value;
            txtTotalScore.Value = txtRiskScore.Value * txtControllingScore.Value;

            btnRiskScore.Text = txtRiskScore.Value.ToString();
            btnTotalScore.Text = txtTotalScore.Value.ToString();
        }

        private void BandsColor(string itemId)
        {
            System.Drawing.Color color = System.Drawing.Color.White;

            if (!string.IsNullOrEmpty(itemId))
            {
                var std = new AppStandardReferenceItem();
                if (std.LoadByPrimaryKey(AppEnum.StandardReference.RiskManagementBands.ToString(), itemId))
                    color = ColorTranslator.FromHtml(std.ReferenceID);
            }

            txtBandsColor.BackColor = color;
        }

        #endregion
    }
}