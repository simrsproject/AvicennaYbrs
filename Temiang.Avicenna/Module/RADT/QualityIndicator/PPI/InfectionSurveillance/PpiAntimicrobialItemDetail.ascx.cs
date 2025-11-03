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
    public partial class PpiAntimicrobialItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRTherapyGroup, AppEnum.StandardReference.TherapyGroup);
            StandardReference.InitializeIncludeSpace(cboSRDosageUnit, AppEnum.StandardReference.DosageUnit);
            StandardReference.InitializeIncludeSpace(cboSRAntimicrobialApplicationTiming, AppEnum.StandardReference.AntimicrobialApplicationTiming);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboSRTherapyGroup.SelectedValue = (String)DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.SRTherapyGroup);
            PopulateTherapyId(cboSRTherapyGroup.SelectedValue);
            cboTherapyID.SelectedValue = (String)DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.TherapyID);
            txtDosage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.Dosage));
            cboSRDosageUnit.SelectedValue = (String)DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.SRDosageUnit);
            object startDate = DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.StartDate);
            if (startDate != null)
                txtStartDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.StartDate);
            else
                txtStartDate.Clear();
            object endDate = DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.StartDate);
            if (endDate != null)
                txtEndDate.SelectedDate = (DateTime)DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.EndDate);
            else
                txtEndDate.Clear();
            cboSRAntimicrobialApplicationTiming.SelectedValue = (String)DataBinder.Eval(DataItem, PpiAntimicrobialApplicationsMetadata.ColumnNames.SRAntimicrobialApplicationTiming);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PpiAntimicrobialApplicationsCollection)Session["collPpiAntimicrobialApplications"];

                string tgroup = cboSRTherapyGroup.SelectedValue;
                string tid = cboTherapyID.SelectedValue;
                
                bool isExist = false;

                foreach (PpiAntimicrobialApplications item in coll)
                {
                    if (item.SRTherapyGroup == tgroup && item.TherapyID == tid)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Therapy Group : {0}, Therapy : {1} already exist", cboSRTherapyGroup.Text, cboTherapyID.Text);
                    return;
                }
            }
            DateTime fromdate = txtStartDate.SelectedDate ?? (new DateTime()).NowAtSqlServer();
            DateTime todate = txtEndDate.SelectedDate ?? (new DateTime()).NowAtSqlServer();

            if (fromdate > todate)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage =
                    string.Format("Start Date : {0} can not be greater than End Date : {1} already exist", fromdate.ToString("dd-MMM-yyyy"), todate.ToString("dd-MMM-yyyy"));
            }

        }

        protected void cboSRTherapyGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateTherapyId(e.Value);
            }
            else
            {
                cboTherapyID.Items.Clear();
                cboTherapyID.SelectedValue = string.Empty;
                cboTherapyID.Text = string.Empty;
            }
        }

        private void PopulateTherapyId(string therapyGroup)
        {
            cboTherapyID.Items.Clear();

            var query = new TherapyQuery();
            query.Select
                (
                    query.TherapyID,
                    query.TherapyName
                );
            query.Where
                (
                    query.SRTherapyGroup == therapyGroup
                );
            query.OrderBy(query.TherapyID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboTherapyID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboTherapyID.Items.Add(new RadComboBoxItem(row["TherapyName"].ToString(), row["TherapyID"].ToString()));
            }
        }

        #region Properties for return entry value

        public String SRTherapyGroup
        {
            get { return cboSRTherapyGroup.SelectedValue; }
        }

        public String TherapyGroupName
        {
            get { return cboSRTherapyGroup.Text; }
        }

        public String TherapyID
        {
            get { return cboTherapyID.SelectedValue; }
        }

        public String TherapyName
        {
            get { return cboTherapyID.Text; }
        }

        public Decimal Dosage
        {
            get { return Convert.ToDecimal(txtDosage.Value); }
        }

        public String SRDosageUnit
        {
            get { return cboSRDosageUnit.SelectedValue; }
        }

        public DateTime? StartDate
        {
            get { return txtStartDate.SelectedDate; }
        }

        public DateTime? EndDate
        {
            get { return txtEndDate.SelectedDate; }
        }

        public String SRAntimicrobialApplicationTiming
        {
            get { return cboSRAntimicrobialApplicationTiming.SelectedValue; }
        }

        public String AntimicrobialApplicationTimingName
        {
            get { return cboSRAntimicrobialApplicationTiming.Text; }
        }
        
        #endregion
    }
}