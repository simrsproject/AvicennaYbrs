using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.KEHRS
{
    public partial class SafetyCultureIncidentReportsWitnessItem : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboVictimID
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboPersonID"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRProfessionType, AppEnum.StandardReference.ProfessionType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;

            if ((int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID) > 0)
            {
                PopulateCboPersonID(cboPersonID, (int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID));
                cboPersonID.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessPersonID).ToString();
            }
            cboSRProfessionType.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSRProfessionType).ToString();
            if ((int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessOrganizationID) > 0)
            {
                PopulateCboOrganizationUnitID(cboOrganizationUnitID, (int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessOrganizationID));
                cboOrganizationUnitID.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessOrganizationID).ToString();
            }
            if ((int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubOrganizationID) > 0)
            {
                PopulateCboOrganizationUnitID(cboSubOrganizationUnitID, (int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubOrganizationID));
                cboSubOrganizationUnitID.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubOrganizationID).ToString();
            }
            if ((int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubDivisonID) > 0)
            {
                PopulateCboOrganizationUnitID(cboSubDivisonID, (int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubDivisonID));
                cboSubDivisonID.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessSubDivisonID).ToString();
            }
            var unitId = (string)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessServiceUnitID);
            if (!string.IsNullOrEmpty(unitId))
            {
                PopulateCboOrganizationUnitID(cboServiceUnit, (int)DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessServiceUnitID));
                cboServiceUnit.SelectedValue = DataBinder.Eval(DataItem, EmployeeSafetyCultureIncidentReportsWitnessMetadata.ColumnNames.WitnessServiceUnitID).ToString();
            }
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(cboPersonID.SelectedValue))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Witness Name required.");
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                    (EmployeeSafetyCultureIncidentReportsWitnessCollection)Session["collEmployeeSafetyCultureIncidentReportsWitness" + Request.UserHostName];

                //bool isExist = false;
                var witnessId = cboPersonID.SelectedValue.ToInt();

                //string id = cboLocationID.SelectedValue;
                bool isExist = coll.Any(item => item.WitnessPersonID.Equals(witnessId));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Witness: {0} has exist", cboPersonID.Text);
                }
            }
        }

        #region Properties for return entry value
        public Int32 WitnessPersonID
        {
            get { return Convert.ToInt32(cboPersonID.SelectedValue); }
        }

        public String WitnessName
        {
            get { return cboPersonID.Text; }
        }

        public String WitnessSRProfessionType
        {
            get { return cboSRProfessionType.SelectedValue; }
        }

        public String WitnessProfessionTypeName
        {
            get { return cboSRProfessionType.Text; }
        }

        public Int32 WitnessOrganizationID
        {
            get { return Convert.ToInt32(cboOrganizationUnitID.SelectedValue); }
        }

        public string WitnessOrganizationName
        {
            get { return cboOrganizationUnitID.Text; }
        }

        public Int32 WitnessSubOrganizationID
        {
            get { return string.IsNullOrEmpty(cboSubOrganizationUnitID.SelectedValue) ? 0 : Convert.ToInt32(cboSubOrganizationUnitID.SelectedValue); }
        }

        public string WitnessSubOrganizationName
        {
            get { return cboSubOrganizationUnitID.Text; }
        }

        public Int32 WitnessSubDivisonID
        {
            get { return string.IsNullOrEmpty(cboSubDivisonID.SelectedValue) ? 0 : Convert.ToInt32(cboSubDivisonID.SelectedValue); }
        }

        public string WitnessSubDivisonName
        {
            get { return cboSubDivisonID.Text; }
        }

        public string WitnessServiceUnitID
        {
            get { return cboServiceUnit.SelectedValue; }
        }

        public string WitnessServiceUnitName
        {
            get { return cboServiceUnit.Text; }
        }

        #endregion

        #region Method & Event TextChanged

        #endregion

        #region ComboBox
        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where(
                query.PersonID != CboVictimID.SelectedValue.ToInt(),
                query.SREmployeeStatus == AppSession.Parameter.EmployeeStatusActive,
                query.Or
                        (
                            query.EmployeeNumber.Like(searchTextContain),
                            query.EmployeeName.Like(searchTextContain)
                        )
                );

            cboPersonID.DataSource = query.LoadDataTable();
            cboPersonID.DataBind();
        }

        protected void cboPersonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPersonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboOrganizationUnitID.Items.Clear();
            cboOrganizationUnitID.SelectedValue = string.Empty;
            cboOrganizationUnitID.Text = string.Empty;

            cboSubOrganizationUnitID.Items.Clear();
            cboSubOrganizationUnitID.SelectedValue = string.Empty;
            cboSubOrganizationUnitID.Text = string.Empty;

            cboSubDivisonID.Items.Clear();
            cboSubDivisonID.SelectedValue = string.Empty;
            cboSubDivisonID.Text = string.Empty;

            cboServiceUnit.Items.Clear();
            cboServiceUnit.SelectedValue = string.Empty;
            cboServiceUnit.Text = string.Empty;

            var emp = new VwEmployeeTable();
            var empq = new VwEmployeeTableQuery();
            empq.Where(empq.PersonID == e.Value.ToInt());
            emp.Load(empq);
            if (emp != null)
            {
                cboSRProfessionType.SelectedValue = emp.SRProfessionType;

                if (emp.OrganizationUnitID > 0)
                {
                    PopulateCboOrganizationUnitID(cboOrganizationUnitID, emp.OrganizationUnitID.ToInt());
                    cboOrganizationUnitID.SelectedValue = emp.OrganizationUnitID.ToString();
                }
                if (emp.SubOrganizationUnitID > 0)
                {
                    PopulateCboOrganizationUnitID(cboSubOrganizationUnitID, emp.SubOrganizationUnitID.ToInt());
                    cboSubOrganizationUnitID.SelectedValue = emp.SubOrganizationUnitID.ToString();
                }
                if (emp.SubDivisonID > 0)
                {
                    PopulateCboOrganizationUnitID(cboSubDivisonID, emp.SubDivisonID.ToInt());
                    cboSubDivisonID.SelectedValue = emp.SubDivisonID.ToString();
                }
                var unitId = emp.ServiceUnitID;
                if (!string.IsNullOrEmpty(unitId))
                {
                    PopulateCboOrganizationUnitID(cboServiceUnit, unitId.ToInt());
                    cboServiceUnit.SelectedValue = unitId;
                }
            }
        }

        private void PopulateCboPersonID(RadComboBox comboBox, int personId)
        {
            var query = new VwEmployeeTableQuery();
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );
            query.Where(query.PersonID == personId);
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

        }

        private void PopulateCboOrganizationUnitID(RadComboBox comboBox, int textSearch)
        {
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitID == textSearch);

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();

        }
        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
        #endregion
    }
}