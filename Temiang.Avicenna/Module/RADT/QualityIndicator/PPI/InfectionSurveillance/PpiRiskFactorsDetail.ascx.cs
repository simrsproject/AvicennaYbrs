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
namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class PpiRiskFactorsDetail : BaseUserControl
    {
        public object DataItem { get; set; }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRRiskFactorsType, AppEnum.StandardReference.RiskFactorsType);
            StandardReference.InitializeIncludeSpace(cboSRRiskFactorsLocation, AppEnum.StandardReference.RiskFactorsLocation);


            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                var coll = (PpiRiskFactorsCollection)Session["collPpiRiskFactors"];
                if (coll.Count == 0)
                    ViewState["SequenceNo"] = "001";
                else
                {
                    var sequenceNo = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                    int seqNo = int.Parse(sequenceNo.Single()) + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);
                }

                return;
            }

            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.SequenceNo);

            cboSRRiskFactorsType.SelectedValue = (String)DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsType);
            PopulateRiskFactorsId(cboSRRiskFactorsType.SelectedValue);
            cboRiskFactorsID.SelectedValue = (String)DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.RiskFactorsID);
            cboSRRiskFactorsLocation.SelectedValue = (String)DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.SRRiskFactorsLocation);

            object dateOfInitialInstallation = DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.DateOfInitialInstallation);
            if (dateOfInitialInstallation != null)
                txtDateOfInitialInstallation.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.DateOfInitialInstallation);
            else
                txtDateOfInitialInstallation.Clear();

            object dateOfFinalInstallation = DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.DateOfFinalInstallation);
            if (dateOfFinalInstallation != null)
                txtDateOfFinalInstallation.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PpiRiskFactorsMetadata.ColumnNames.DateOfFinalInstallation);
            else
                txtDateOfFinalInstallation.Clear();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime fromdate = txtDateOfInitialInstallation.SelectedDate ?? (new DateTime()).NowAtSqlServer();
            DateTime todate = txtDateOfFinalInstallation.SelectedDate ?? (new DateTime()).NowAtSqlServer();

            if (fromdate > todate)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage =
                    string.Format("Date Started : {0} can not be greater than Date Finished : {1} already exist", fromdate.ToString("dd-MMM-yyyy"), todate.ToString("dd-MMM-yyyy"));
            }
        }

        private void PopulateRiskFactorsId(string srRiskFactorsType)
        {
            cboRiskFactorsID.Items.Clear();

            var query = new RiskFactorsQuery();
            query.Select
                (
                    query.RiskFactorsID,
                    query.RiskFactorsName
                );
            query.Where
                (
                    query.SRRiskFactorsType == srRiskFactorsType
                );
            query.OrderBy(query.RiskFactorsID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboRiskFactorsID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboRiskFactorsID.Items.Add(new RadComboBoxItem(row["RiskFactorsName"].ToString(), row["RiskFactorsID"].ToString()));
            }
        }

        protected void cboSRRiskFactorsType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateRiskFactorsId(e.Value);
            }
            else
            {
                cboRiskFactorsID.Items.Clear();
                cboRiskFactorsID.SelectedValue = string.Empty;
                cboRiskFactorsID.Text = string.Empty;
            }
        }

        #region Properties for return entry value

        public String SequenceNo
        {
            get { return (string)ViewState["SequenceNo"]; }
        }

        public String SRRiskFactorsType
        {
            get { return cboSRRiskFactorsType.SelectedValue; }
        }

        public String RiskFactorsTypeName
        {
            get { return cboSRRiskFactorsType.Text; }
        }

        public String RiskFactorsID
        {
            get { return cboRiskFactorsID.SelectedValue; }
        }

        public String RiskFactorsName
        {
            get { return cboRiskFactorsID.Text; }
        }

        public String SRRiskFactorsLocation
        {
            get { return cboSRRiskFactorsLocation.SelectedValue; }
        }

        public String RiskFactorsLocationName
        {
            get { return cboSRRiskFactorsLocation.Text; }
        }

        public DateTime? DateOfInitialInstallation
        {
            get { return txtDateOfInitialInstallation.SelectedDate; }
        }

        public DateTime? DateOfFinalInstallation
        {
            get { return txtDateOfFinalInstallation.SelectedDate; }
        }

        #endregion
    }
}