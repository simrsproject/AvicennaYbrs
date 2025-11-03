using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.QualityIndicator
{
    public partial class HandHygieneSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.HandHygiene;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
            }
        }

        public override bool OnButtonOkClicked()
        {
            OnButtonOkClick();

            return true;
        }

        private void OnButtonOkClick()
        {
            var query = new HandHygieneQuery("a");
            var qemp = new VwEmployeeTableQuery("b");
            var qip = new AppStandardReferenceItemQuery("c");
            var qdep = new OrganizationUnitQuery("d");
            var qdiv = new OrganizationUnitQuery("e");
            var qsub = new OrganizationUnitQuery("f");
            var qunit = new OrganizationUnitQuery("g");
            var qobs = new VwEmployeeTableQuery("h");

            query.InnerJoin(qemp).On(qemp.PersonID == query.EmployeeID);
            query.LeftJoin(qip).On(qip.StandardReferenceID == AppEnum.StandardReference.ProfessionType && qip.ItemID == qemp.SRProfessionType);
            query.LeftJoin(qdep).On(qdep.OrganizationUnitID == query.DepartmentID);
            query.LeftJoin(qdiv).On(qdiv.OrganizationUnitID == query.DivisionID);
            query.LeftJoin(qsub).On(qsub.OrganizationUnitID == query.SubDivisionID);
            query.LeftJoin(qunit).On(qunit.OrganizationUnitID.ToString() == query.ServiceUnitID);
            query.InnerJoin(qobs).On(qobs.PersonID == query.ObserverID);

            query.OrderBy
                (
                    query.TransactionDate.Descending, query.TransactionNo.Descending
                );

            query.Select(
                query.TransactionNo,
                query.TransactionDate,
                query.StartTime,
                query.EndTime,
                query.SessionLength,
                query.EmployeeID,
                qemp.EmployeeNumber,
                qemp.EmployeeName,
                qip.ItemName.As("ProfessionType"),
                qdep.OrganizationUnitName.As("DepartmentName"),
                qdiv.OrganizationUnitName.As("DivisionName"),
                qsub.OrganizationUnitName.As("SubDivisionName"),
                qunit.OrganizationUnitName.As("ServiceUnitName"),
                qobs.EmployeeName.As("ObserverName"),
                query.IsApproved,
                query.IsVoid
                );

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtTransactionDate.IsEmpty)
            {
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(txtObserverName.Text))
            {
                if (cboFilterObserverName.SelectedIndex == 1)
                    query.Where(qobs.EmployeeName == txtObserverName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtObserverName.Text);
                    query.Where(qobs.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtEmployeeNumber.Text))
            {
                if (cboFilterEmployeeNumber.SelectedIndex == 1)
                    query.Where(qemp.EmployeeNumber == txtEmployeeNumber.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeNumber.Text);
                    query.Where(qemp.EmployeeNumber.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(qemp.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text); 
                    query.Where(qemp.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRProfessionType.SelectedValue))
            {
                query.Where(qemp.SRProfessionType == cboSRProfessionType.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboOrganizationUnitID.SelectedValue))
            {
                query.Where(query.Or(
                    query.DepartmentID == cboOrganizationUnitID.SelectedValue.ToInt(), 
                    query.DivisionID == cboOrganizationUnitID.SelectedValue.ToInt(), 
                    query.SubDivisionID == cboOrganizationUnitID.SelectedValue.ToInt(), 
                    query.ServiceUnitID == cboOrganizationUnitID.SelectedValue));
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset
        }

        protected void cboOrganizationUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new OrganizationUnitQuery();

            query.Where(
                query.OrganizationUnitName.Like(searchTextContain));

            query.Select(query.OrganizationUnitID, query.OrganizationUnitCode, query.OrganizationUnitName);
            query.OrderBy(query.OrganizationUnitCode.Ascending);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboOrganizationUnitID.DataSource = dtb;
            cboOrganizationUnitID.DataBind();

        }
        protected void cboOrganizationUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["OrganizationUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["OrganizationUnitID"].ToString();
        }
    }
}