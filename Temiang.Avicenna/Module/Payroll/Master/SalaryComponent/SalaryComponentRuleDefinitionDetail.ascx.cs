using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class SalaryComponentRuleDefinitionDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //Init Visible Control
            trOrganizationUnit.Visible = (Helper.FindControlRecursive(this.Page, "chkIsOrganizationUnit") as CheckBox).Checked;
            trEmployeeStatus.Visible = (Helper.FindControlRecursive(this.Page, "chkIsEmployeeStatus") as CheckBox).Checked;
            trPosition.Visible = (Helper.FindControlRecursive(this.Page, "chkIsPosition") as CheckBox).Checked;
            trReligion.Visible = (Helper.FindControlRecursive(this.Page, "chkIsReligion") as CheckBox).Checked;
            trEmployee.Visible = (Helper.FindControlRecursive(this.Page, "chkIsEmployee") as CheckBox).Checked;

            trEmploymentType.Visible = (Helper.FindControlRecursive(this.Page, "chkIsEmploymentType") as CheckBox).Checked;
            trPositionGrade.Visible = (Helper.FindControlRecursive(this.Page, "chkIsPositionGrade") as CheckBox).Checked;
            trMaritalStatus.Visible = (Helper.FindControlRecursive(this.Page, "chkIsMaritalStatus") as CheckBox).Checked;
            trServiceYear.Visible = (Helper.FindControlRecursive(this.Page, "chkIsServiceYear") as CheckBox).Checked;
            trPercentageComponent.Visible = (Helper.FindControlRecursive(this.Page, "chkIsServiceYear") as CheckBox).Checked;

            trSalaryTableNumber.Visible = (Helper.FindControlRecursive(this.Page, "chkIsSalaryTableNumber") as CheckBox).Checked;

            trEmployeeGrade.Visible = (Helper.FindControlRecursive(this.Page, "chkIsEmployeeGrade") as CheckBox).Checked;
            trNoOfDependent.Visible = (Helper.FindControlRecursive(this.Page, "chkIsNoOfDependent") as CheckBox).Checked;
            trAttendanceMatrix.Visible = (Helper.FindControlRecursive(this.Page, "chkIsAttedanceMatrixID") as CheckBox).Checked;
            trEducationLevel.Visible = (Helper.FindControlRecursive(this.Page, "chkEducationLevel") as CheckBox).Checked;
            trEmployeeType.Visible = (Helper.FindControlRecursive(this.Page, "chkIsEmployeeType") as CheckBox).Checked;
            trServiceUnitID.Visible = (Helper.FindControlRecursive(this.Page, "chkIsServiceUnitID") as CheckBox).Checked;

            StandardReference.InitializeIncludeSpace(cboSREmployeeStatus, AppEnum.StandardReference.EmployeeStatus);
            StandardReference.InitializeIncludeSpace(cboSRReligion, AppEnum.StandardReference.Religion);
            StandardReference.InitializeIncludeSpace(cboSREmploymentType, AppEnum.StandardReference.EmploymentType);
            StandardReference.InitializeIncludeSpace(cboSRMaritalStatus, AppEnum.StandardReference.TaxStatus, "MARITAL");
            StandardReference.InitializeIncludeSpace(cboSREmployeeType, AppEnum.StandardReference.EmployeeType);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                try
                {
                    txtValidFrom.SelectedDate = (Helper.FindControlRecursive(this.Page, "txtValidFrom") as RadDatePicker).SelectedDate;
                    txtValidTo.SelectedDate = (Helper.FindControlRecursive(this.Page, "txtValidTo") as RadDatePicker).SelectedDate;
                }
                catch
                {

                }

                cboOrganizationUnitID.Enabled = true;
                cboSREmployeeStatus.Enabled = true;
                cboPositionID.Enabled = true;
                cboSRReligion.Enabled = true;
                cboPersonalID.Enabled = true;
                cboSREmploymentType.Enabled = true;
                cboPositionGradeID.Enabled = true;
                cboSRMaritalStatus.Enabled = true;
                txtServiceYear.Enabled = true;
                txtSalaryTableNumber.Enabled = true;
                cboEmployeeGradeMasterID.Enabled = true;
                txtNoOfDependent.Enabled = true;
                cboAttedanceMatrixID.Enabled = true;
                cboEducationLevel.Enabled = true;
                cboSREmployeeType.Enabled = true;
                cboServiceUnitID.Enabled = true;

                //TODO: Inisialisasi control untuk new row
                //misal --> chkIsActive.Checked = true;
                txtSalaryComponentRuleDefinitionID.Text = "1";
                return;
            }

            cboOrganizationUnitID.Enabled = false;
            cboSREmployeeStatus.Enabled = false;
            cboPositionID.Enabled = false;
            cboSRReligion.Enabled = false;
            cboPersonalID.Enabled = false;
            cboSREmploymentType.Enabled = false;
            cboPositionGradeID.Enabled = false;
            cboSRMaritalStatus.Enabled = false;
            txtServiceYear.Enabled = false;
            txtSalaryTableNumber.Enabled = false;
            cboEmployeeGradeMasterID.Enabled = false;
            txtNoOfDependent.Enabled = false;
            cboAttedanceMatrixID.Enabled = false;
            cboEducationLevel.Enabled = false;
            cboSREmployeeType.Enabled = false;
            cboServiceUnitID.Enabled = false;

            ViewState["IsNewRecord"] = false;

            txtSalaryComponentRuleDefinitionID.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryComponentRuleDefinitionID));
            txtValidFrom.SelectedDate = (DateTime)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidFrom);
            txtValidTo.SelectedDate = (DateTime)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.ValidTo);
            PopulatecboOrganizationUnitID(cboOrganizationUnitID, (String)DataBinder.Eval(DataItem, "OrganizationUnitName"), "3");
            cboSREmployeeStatus.SelectedValue = (String)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeStatus);
            PopulatecboPositionID(cboPositionID, (String)DataBinder.Eval(DataItem, "PositionName"));
            cboSRReligion.SelectedValue = (String)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SRReligion);
            PopulatecboPersonalID(cboPersonalID, (String)DataBinder.Eval(DataItem, "EmployeeName"));
            cboSREmploymentType.SelectedValue = (String)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmploymentType);
            PopulatecboPositionGradeID(cboPositionGradeID, (String)DataBinder.Eval(DataItem, "PositionGradeName"));
            cboSRMaritalStatus.SelectedValue = (String)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SRMaritalStatus);
            txtServiceYear.Text = (string)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.ServiceYear);
            txtSalaryTableNumber.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SalaryTableNumber));
            PopulatecboEmployeeGradeMasterID(cboEmployeeGradeMasterID, (String)DataBinder.Eval(DataItem, "EmployeeGradeName"));
            txtNoOfDependent.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.NoOfDependent));
            PopulatecboAttedanceMatrixID(cboAttedanceMatrixID, (String)DataBinder.Eval(DataItem, "AttedanceMatrixName"));
            txtNominalAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.NominalAmount));
            txtPercentageAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageAmount));
            PopulatecboSalaryComponentID(cboPercentageComponent, Convert.ToInt32(DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.PercentageComponentID)).ToString());
            PopulatecboEducationLevel(cboEducationLevel, (string)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREducationLevelID));
            cboSREmployeeType.SelectedValue = (String)DataBinder.Eval(DataItem, SalaryComponentRuleDefinitionMetadata.ColumnNames.SREmployeeType);
            PopulatecboOrganizationUnitID(cboServiceUnitID, (String)DataBinder.Eval(DataItem, "OrganizationUnitName"), "1");
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                SalaryComponentRuleDefinitionCollection coll =
                    (SalaryComponentRuleDefinitionCollection)Session["collSalaryComponentRuleDefinition"];

                //TODO: Betulkan cara pengecekannya
                string id = txtSalaryComponentRuleDefinitionID.Text;
                bool isExist = false;
                foreach (SalaryComponentRuleDefinition item in coll)
                {
                    if (item.SalaryComponentID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }

            }

        }

        #region Properties for return entry value
        public Int64 SalaryComponentRuleDefinitionID
        {
            get { return Convert.ToInt64(txtSalaryComponentRuleDefinitionID.Text); }
        }
        public DateTime ValidFrom
        {
            get { return Convert.ToDateTime(txtValidFrom.SelectedDate); }
        }
        public DateTime ValidTo
        {
            get { return Convert.ToDateTime(txtValidTo.SelectedDate); }
        }
        public Int32 OrganizationUnitID
        {
            get { return Convert.ToInt32(cboOrganizationUnitID.SelectedValue); }
        }
        public String OrganizationUnitName
        {
            get { return cboOrganizationUnitID.Text; }
        }
        public String SREmployeeStatus
        {
            get { return cboSREmployeeStatus.SelectedValue; }
        }
        public String EmployeeStatusName
        {
            get { return cboSREmployeeStatus.Text; }
        }
        public Int32 PositionID
        {
            get { return Convert.ToInt32(cboPositionID.SelectedValue); }
        }
        public string PositionName
        {
            get { return cboPositionID.Text; }
        }
        public String SRReligion
        {
            get { return cboSRReligion.SelectedValue; }
        }
        public String ReligionName
        {
            get { return cboSRReligion.Text; }
        }
        public Int32 PersonID
        {
            get { return Convert.ToInt32(cboPersonalID.SelectedValue); }
        }
        public string EmployeeName
        {
            get { return cboPersonalID.Text; }
        }
        public String SREmploymentType
        {
            get { return cboSREmploymentType.SelectedValue; }
        }
        public String EmploymentTypeName
        {
            get { return cboSREmploymentType.Text; }
        }
        public Int32 PositionGradeID
        {
            get { return Convert.ToInt32(cboPositionGradeID.SelectedValue); }
        }
        public string PositionGradeName
        {
            get { return cboPositionGradeID.Text; }
        }
        public String SRMaritalStatus
        {
            get { return cboSRMaritalStatus.SelectedValue; }
        }
        public String MaritalStatusName
        {
            get { return cboSRMaritalStatus.Text; }
        }
        public string ServiceYear
        {
            get { return txtServiceYear.Text; }
        }
        public Int32 SalaryTableNumber
        {
            get { return Convert.ToInt32(txtSalaryTableNumber.Text); }
        }
        public Int32 EmployeeGradeID
        {
            get { return Convert.ToInt32(cboEmployeeGradeMasterID.SelectedValue); }
        }
        public string EmployeeGradeName
        {
            get { return cboEmployeeGradeMasterID.Text; }
        }
        public Int32 NoOfDependent
        {
            get { return Convert.ToInt32(txtNoOfDependent.Text); }
        }
        public Int32 AttedanceMatrixID
        {
            get { return Convert.ToInt32(cboAttedanceMatrixID.SelectedValue); }
        }
        public string AttedanceMatrixName
        {
            get { return cboAttedanceMatrixID.Text; }
        }
        public Decimal NominalAmount
        {
            get { return Convert.ToDecimal(txtNominalAmount.Value); }
        }
        public Decimal PercentageAmount
        {
            get { return Convert.ToDecimal(txtPercentageAmount.Value); }
        }
        public int? PercentageComponentID
        {
            get
            {
                try
                {
                    return int.Parse(cboPercentageComponent.SelectedValue);
                }
                catch
                {
                    return null;
                }
            }
        }
        public String EducationLevelID
        {
            get { return cboEducationLevel.SelectedValue; }
        }
        public String EducationLevelName
        {
            get { return cboEducationLevel.Text; }
        }
        public String SREmployeeType
        {
            get { return cboSREmployeeType.SelectedValue; }
        }
        public String EmployeeTypeName
        {
            get { return cboSREmployeeType.Text; }
        }
        public String ServiceUnitID
        {
            get { return cboServiceUnitID.SelectedValue; }
        }
        public String ServiceUnitName
        {
            get { return cboServiceUnitID.Text; }
        }
        #endregion

        #region Method & Event TextChanged
        #endregion

        #region ComboBox
        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboOrganizationUnitID((RadComboBox)sender, e.Text, "3");
        }
        private void PopulatecboOrganizationUnitID(RadComboBox comboBox, string textSearch, string srOrganizationLevel)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            OrganizationUnitQuery query = new OrganizationUnitQuery("a");
            EmployeeOrganizationQuery employee = new EmployeeOrganizationQuery("b");
            query.LeftJoin(employee).On(query.OrganizationUnitID == employee.OrganizationID);

            query.Where(
                query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == srOrganizationLevel);

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.Items.Add(new RadComboBoxItem("0", ""));
                comboBox.SelectedValue = dtb.Rows[0]["OrganizationUnitID"].ToString();
            }
        }
        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboOrganizationUnitID((RadComboBox)sender, e.Text, "1");
        }
        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }

        protected void cboPositionID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboPositionID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            PositionQuery query = new PositionQuery("a");
            EmployeePositionQuery employee = new EmployeePositionQuery("b");
            query.LeftJoin(employee).On(query.PositionID == employee.PositionID);

            query.Where(
                query.PositionName.Like(searchTextContain));

            query.Select(query.PositionID, query.PositionCode, query.PositionName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionID"].ToString();
            }
        }
        protected void cboPositionID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionCode"].ToString() + " " + ((DataRowView)e.Item.DataItem)["PositionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionID"].ToString();
        }

        protected void cboPersonalID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPersonalID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboPersonalID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            VwEmployeeTableQuery query = new VwEmployeeTableQuery("a");
            SalaryComponentRuleDefinitionQuery rule = new SalaryComponentRuleDefinitionQuery("b");
            query.LeftJoin(rule).On(query.PersonID == rule.PersonID);

            query.Where
                 (
                     query.Or
                         (
                             query.EmployeeNumber.Like(searchTextContain),
                             query.EmployeeName.Like(searchTextContain)
                         )
                 );

            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PersonID"].ToString();
                //comboBox.SelectedValue = dtb.Rows[0]["EmployeeName"].ToString();
            }
        }
        protected void cboPersonalID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboPositionGradeID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboPositionGradeID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboPositionGradeID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            PositionGradeQuery query = new PositionGradeQuery();

            query.Where
                 (
                     query.Or
                         (
                             query.PositionGradeCode.Like(searchTextContain),
                             query.PositionGradeName.Like(searchTextContain)
                         )
                 );

            query.Select
                (
                    query.PositionGradeID,
                    query.PositionGradeCode,
                    query.PositionGradeName
                );

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["PositionGradeID"].ToString();
            }
        }
        protected void cboPositionGradeID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PositionGradeCode"].ToString() + " " + ((DataRowView)e.Item.DataItem)["PositionGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PositionGradeID"].ToString();
        }

        protected void cboEmployeeGradeMasterID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboEmployeeGradeMasterID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboEmployeeGradeMasterID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            EmployeeGradeMasterQuery query = new EmployeeGradeMasterQuery("a");
            SalaryComponentRuleDefinitionQuery rule = new SalaryComponentRuleDefinitionQuery("b");
            query.LeftJoin(rule).On(query.EmployeeGradeMasterID == rule.EmployeeGradeID);

            query.Where(
                query.EmployeeGradeName.Like(searchTextContain));

            query.Select(query.EmployeeGradeMasterID, query.EmployeeGradeCode, query.EmployeeGradeName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["EmployeeGradeMasterID"].ToString();
            }
        }
        protected void cboEmployeeGradeMasterID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeGradeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["EmployeeGradeMasterID"].ToString();
        }

        protected void cboAttedanceMatrixID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboAttedanceMatrixID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboAttedanceMatrixID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            AttedanceMatrixQuery query = new AttedanceMatrixQuery("a");
            SalaryComponentRuleDefinitionQuery rule = new SalaryComponentRuleDefinitionQuery("b");
            query.LeftJoin(rule).On(query.AttedanceMatrixID == rule.EmployeeGradeID);

            query.Where(
                query.AttedanceMatrixName.Like(searchTextContain));

            query.Select(query.AttedanceMatrixID, query.AttedanceMatrixName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["AttedanceMatrixID"].ToString();
            }
        }
        protected void cboAttedanceMatrixID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["AttedanceMatrixName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["AttedanceMatrixID"].ToString();
        }

        protected void cboPercentageComponent_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboSalaryComponentID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboSalaryComponentID(RadComboBox comboBox, string textSearch)
        {
            if (string.IsNullOrEmpty(textSearch))
                textSearch = "::";

            var query = new SalaryComponentQuery("a");

            if (Helper.IsNumeric(textSearch))
                query.Where(query.SalaryComponentName == textSearch);
            else
            {
                string searchTextContain = string.Format("%{0}%", textSearch);
                query.Where(query.SalaryComponentName.Like(searchTextContain));
            }

            query.es.Top = 20;

            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["SalaryComponentID"].ToString();
            }
        }
        protected void cboPercentageComponent_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SalaryComponentName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SalaryComponentID"].ToString();
        }

        protected void cboEducationLevel_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboEducationLevel((RadComboBox)sender, e.Text);
        }
        private void PopulatecboEducationLevel(RadComboBox comboBox, string textSearch)
        {
            if (string.IsNullOrEmpty(textSearch))
                textSearch = "::";

            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new AppStandardReferenceItemQuery();

            query.Where(
                query.StandardReferenceID == AppEnum.StandardReference.EducationLevel,
                query.Or(
                    query.ItemID.Like(searchTextContain),
                    query.ItemName.Like(searchTextContain)
                    )
                );

            query.Select(query.ItemID, query.ItemName);

            query.es.Top = 20;
            query.es.Distinct = true;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["ItemID"].ToString();
            }
        }
        protected void cboEducationLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }
        #endregion ComboBox
    }
}
