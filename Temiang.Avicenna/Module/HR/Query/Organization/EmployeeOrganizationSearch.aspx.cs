using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Query
{
    public partial class EmployeeOrganizationSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.QueryEmployeeOrganization;//TODO: Isi ProgramID

            if (!IsPostBack)
                trSubDivision.Visible = AppSession.Parameter.IsUsingFourLevelOrganizationUnit;
        }

        public override bool OnButtonOkClicked()
        {
            OrganizationUnitQuery organization = new OrganizationUnitQuery("c");
            PersonalInfoQuery personal = new PersonalInfoQuery("b");
            var query = new EmployeeOrganizationQuery("a");
            var division = new OrganizationUnitQuery("d");
            var section = new OrganizationUnitQuery("e");
            var subdivision = new OrganizationUnitQuery("f");

            query.Select
                (
                   query.EmployeeOrganizationID,
                   query.PersonID,
                   personal.EmployeeNumber,
                   personal.EmployeeName,
                   query.OrganizationID,
                   organization.OrganizationUnitCode,
                   organization.OrganizationUnitName.As("Department"),
                   query.ValidFrom,
                   query.ValidTo,
                   query.IsActive,
                   division.OrganizationUnitName.As("Division"),
                   subdivision.OrganizationUnitName.As("SubDivision"),
                   section.OrganizationUnitName.As("Section")
                );

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.LeftJoin(organization).On(query.OrganizationID == organization.OrganizationUnitID);
            query.LeftJoin(division).On(query.SubOrganizationID == division.OrganizationUnitID);
            query.LeftJoin(section).On(query.ServiceUnitID == section.OrganizationUnitID);
            query.LeftJoin(subdivision).On(query.SubDivisonID == subdivision.OrganizationUnitID);

            query.OrderBy(query.PersonID.Ascending); //TODO: Betulkan ordernya

            if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(personal.EmployeeNumber == txtEmployeeNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNo.Text);
                    query.Where(personal.EmployeeNumber.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFirstName.Text))
            {
                if (cboFirstName.SelectedIndex == 1)
                    query.Where(personal.FirstName == txtFirstName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFirstName.Text);
                    query.Where(personal.FirstName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
            {
                query.Where(query.OrganizationID == cboOrganizationUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSubOrganizationUnitID.SelectedValue))
            {
                query.Where(query.SubOrganizationID == cboSubOrganizationUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSubDivisonID.SelectedValue))
            {
                query.Where(query.SubDivisonID == cboSubDivisonID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            {
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }

        #region ComboBox
        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboOrganizationUnitID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboOrganizationUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();
            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.Where(query.SROrganizationLevel == "3");
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
        protected void cboOrganizationUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubOrganizationUnitID.Items.Clear();
            cboSubOrganizationUnitID.Text = string.Empty;
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
        }
        protected void cboSubOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboSubOrganizationUnitID((RadComboBox)sender, e.Text);
        }
        private void PopulatecboSubOrganizationUnitID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();
            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID.As("SubOrganizationID"), query.OrganizationUnitName.As("SubOrganizationName"));
            query.Where(query.SROrganizationLevel == "2");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSubOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubOrganizationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubOrganizationID"].ToString();
        }
        protected void cboSubOrganizationUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
            cboSubDivisonID.Items.Clear();
            cboSubDivisonID.Text = string.Empty;
        }
        protected void cboSubDivisonID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSubDivisonID((RadComboBox)sender, e.Text);
        }
        private void PopulateCboSubDivisonID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();

            query.Where(query.OrganizationUnitName.Like(searchTextContain));
            query.Select(query.OrganizationUnitID.As("SubDivisionID"), query.OrganizationUnitName.As("SubDivisionName"));
            query.Where(query.SROrganizationLevel == "1",
                query.Or(query.ParentOrganizationUnitID == cboSubOrganizationUnitID.SelectedValue.ToInt(), query.ParentOrganizationUnitID == cboOrganizationUnitID.SelectedValue.ToInt())
                );
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboSubDivisonID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubDivisionName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubDivisionID"].ToString();
        }
        protected void cboSubDivisonID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboServiceUnitID.Items.Clear();
            cboServiceUnitID.Text = string.Empty;
        }
        protected void cboServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulatecboUnit((RadComboBox)sender, e.Text);
        }
        private void PopulatecboUnit(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new OrganizationUnitQuery();
            query.Select(query.OrganizationUnitID.As("ServiceUnitID"), query.OrganizationUnitName.As("ServiceUnitName"));
            query.Where(query.OrganizationUnitName.Like(searchTextContain), query.SROrganizationLevel == "0");
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();

            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }
        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
        #endregion
    }
}
