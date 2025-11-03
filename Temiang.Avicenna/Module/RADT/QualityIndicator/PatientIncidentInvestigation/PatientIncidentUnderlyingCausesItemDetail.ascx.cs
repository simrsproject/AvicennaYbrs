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
    public partial class PatientIncidentUnderlyingCausesItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            PopulateFactor();

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            cboFactorID.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorID);
            PopulateFactorItem(cboFactorID.SelectedValue);
            cboFactorItemID.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.FactorItemID);
            PopulateFactorItemComp(cboFactorID.SelectedValue, cboFactorItemID.SelectedValue);
            cboComponentID.SelectedValue = (String)DataBinder.Eval(DataItem, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentID);
            txtComponentName.Text = (String)DataBinder.Eval(DataItem, PatientIncidentUnderlyingCausesItemComponentMetadata.ColumnNames.ComponentName);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (PatientIncidentUnderlyingCausesItemComponentCollection)Session["collPatientIncidentUnderlyingCausesItemComponent"];

                string factorId = cboFactorID.SelectedValue;
                string factorItemId = cboFactorItemID.SelectedValue;
                string compId = cboComponentID.SelectedValue;
                bool isExist = false;

                foreach (PatientIncidentUnderlyingCausesItemComponent item in coll)
                {
                    if (item.FactorID.Equals(factorId) && item.FactorItemID.Equals(factorItemId) && item.ComponentID.Equals(compId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage =
                        string.Format("Factor : {0}, Factor Item : {1}, Component : {2} already exist", cboFactorID.Text, cboFactorItemID.Text, cboComponentID.Text);
                }
            }
        }

        protected void cboFactorID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateFactorItem(e.Value);
            }
            else
            {
                cboFactorItemID.Items.Clear();
                cboFactorItemID.SelectedValue = string.Empty;
                cboFactorItemID.Text = string.Empty;
            }
            cboComponentID.Items.Clear();
            cboComponentID.SelectedValue = string.Empty;
            cboComponentID.Text = string.Empty;
        }

        protected void cboFactorItem_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                PopulateFactorItemComp(cboFactorID.SelectedValue, e.Value);
            }
            else
            {
                cboComponentID.Items.Clear();
                cboComponentID.SelectedValue = string.Empty;
                cboComponentID.Text = string.Empty;
            }
        }

        protected void cboComponentID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var comp = new ContributoryFactorsClassificationFrameworkItemComponent();
                if (comp.LoadByPrimaryKey(cboFactorID.SelectedValue, cboFactorItemID.SelectedValue, e.Value))
                {
                    txtComponentName.Enabled = comp.IsAllowEdit ?? false;
                    txtComponentName.Text = string.Empty;
                }
            }
            else
            {
                txtComponentName.Text = string.Empty;
                txtComponentName.Enabled = false;
            }
        }

        private void PopulateFactor()
        {
            cboFactorID.Items.Clear();

            var query = new ContributoryFactorsClassificationFrameworkQuery();
            query.Select
                (
                    query.FactorID,
                    query.FactorName
                );
            query.OrderBy(query.FactorID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboFactorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboFactorID.Items.Add(new RadComboBoxItem(row["FactorName"].ToString(), row["FactorID"].ToString()));
            }
        }

        private void PopulateFactorItem(string factorId)
        {
            cboFactorItemID.Items.Clear();

            var query = new ContributoryFactorsClassificationFrameworkItemQuery();
            query.Select
                (
                    query.FactorItemID,
                    query.FactorItemName
                );
            query.Where
                (
                    query.FactorID == factorId
                );
            query.OrderBy(query.FactorItemID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboFactorItemID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboFactorItemID.Items.Add(new RadComboBoxItem(row["FactorItemName"].ToString(), row["FactorItemID"].ToString()));
            }
        }

        private void PopulateFactorItemComp(string factorId, string factorItemId)
        {
            cboComponentID.Items.Clear();

            var query = new ContributoryFactorsClassificationFrameworkItemComponentQuery();
            query.Select
                (
                    query.ComponentID,
                    query.ComponentName
                );
            query.Where
                (
                    query.FactorID == factorId, query.FactorItemID == factorItemId
                );
            query.OrderBy(query.ComponentID.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboComponentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (DataRow row in dtb.Rows)
            {
                cboComponentID.Items.Add(new RadComboBoxItem(row["ComponentName"].ToString(), row["ComponentID"].ToString()));
            }
        }

        #region Properties for return entry value

        public String FactorID
        {
            get { return cboFactorID.SelectedValue; }
        }

        public String FactorName
        {
            get { return cboFactorID.Text; }
        }

        public String FactorItemID
        {
            get { return cboFactorItemID.SelectedValue; }
        }

        public String FactorItemName
        {
            get { return cboFactorItemID.Text; }
        }

        public String ComponentID
        {
            get { return cboComponentID.SelectedValue; }
        }

        public String Component
        {
            get { return cboComponentID.Text; }
        }

        public String ComponentName
        {
            get { return txtComponentName.Text; }
        }

        #endregion
    }
}