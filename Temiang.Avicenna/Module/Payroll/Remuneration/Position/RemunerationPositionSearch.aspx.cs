using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.RemunerationPosition
{
    public partial class RemunerationPositionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeRemunerationPosition;

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Void", "2"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeWageStructureAndScalePositionQuery("a");
            var personalq = new VwEmployeeTableQuery("b");
            var wtq = new EmployeeWageStructureAndScalePositionQuery("c");
            var wgroupq = new AppStandardReferenceItemQuery("d");
            var wsubgroupq = new AppStandardReferenceItemQuery("e");
            var jobposq = new AppStandardReferenceItemQuery("f");

            query.Select
                (query,
                    personalq.EmployeeNumber,
                    personalq.EmployeeName,
                    wgroupq.ItemName.As("EmployeeWorkGroupName"),
                    wsubgroupq.ItemName.As("EmployeeWorkSubGroupName"),
                    jobposq.ItemName.As("EmployeeJobPositionName")
                );
            query.InnerJoin(personalq).On(personalq.PersonID == query.PersonID);
            query.InnerJoin(wtq).On(wtq.WageStructureAndScalePositionID == query.WageStructureAndScalePositionID);
            query.InnerJoin(wgroupq).On(wgroupq.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkGroup.ToString() && wgroupq.ItemID == wtq.SREmployeeWorkGroup);
            query.InnerJoin(wsubgroupq).On(wsubgroupq.StandardReferenceID == AppEnum.StandardReference.EmployeeWorkSubGroup.ToString() && wsubgroupq.ItemID == wtq.SREmployeeWorkSubGroup);
            query.InnerJoin(jobposq).On(jobposq.StandardReferenceID == AppEnum.StandardReference.EmployeeJobPosition.ToString() && jobposq.ItemID == wtq.SREmployeeJobPosition);
            query.OrderBy(query.WageStructureAndScalePositionID.Descending);

           
            if (!txtFromDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToDate.SelectedDate.ToString().Trim().Equals(string.Empty))
                query.Where(query.ValidFrom.Between(txtFromDate.SelectedDate, txtToDate.SelectedDate));
            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
                query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(cboServiceUnitName.SelectedValue))
                query.Where(personalq.ServiceUnitID == cboServiceUnitName.SelectedValue.ToInt());
            if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
            {
                switch (cboStatus.SelectedValue)
                {
                    case "0":
                        query.Where(query.IsVoid == false, query.Or(query.IsApproved.IsNull(), query.IsApproved == false));
                        break;
                    case "1":
                        query.Where(query.IsApproved == true);
                        break;
                    case "2":
                        query.Where(query.IsVoid == true);
                        break;
                }
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }


        protected void cboPersonID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new VwEmployeeTableQuery("a");
            query.es.Top = 20;
            query.es.Distinct = true;
            query.Select
                (
                    query.PersonID,
                    query.EmployeeNumber,
                    query.EmployeeName
                );

            query.Where
                (
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
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"] + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"];
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }

        protected void cboServiceUnitName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();
            query.es.Top = 20;
            query.Select(query.OrganizationUnitID.As("ServiceUnitID"), query.OrganizationUnitName.As("ServiceUnitName"));
            query.Where(query.OrganizationUnitName.Like(searchTextContain));

            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.Where(query.SROrganizationLevel == "0");

            cboServiceUnitName.DataSource = query.LoadDataTable();
            cboServiceUnitName.DataBind();
        }

        protected void cboServiceUnitName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }
    }
}