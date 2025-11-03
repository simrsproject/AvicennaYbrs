using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.HR.Credential.Process.Medic
{
    public partial class CredentialingSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        private string Role
        {
            get
            {
                return Request.QueryString["role"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Role == "usr" ? AppConstant.Program.MedicCredentialSelfAssessment : AppConstant.Program.MedicCredentialSelfAssessmentAdmin;

            if (!IsPostBack)
            {
            }
        }

        protected void cboSRClinicalWorkArea_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalWorkArea_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea,
                    query.ItemName.Like(searchText),
                    query.ReferenceID == "01",
                    query.IsActive == true
                );
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalWorkArea.DataSource = query.LoadDataTable();
            cboSRClinicalWorkArea.DataBind();
        }

        protected void cboSRClinicalWorkArea_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRClinicalAuthorityLevel.Items.Clear();
            cboSRClinicalAuthorityLevel.SelectedValue = string.Empty;
            cboSRClinicalAuthorityLevel.Text = string.Empty;
        }

        protected void cboSRClinicalAuthorityLevel_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRClinicalAuthorityLevel_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new AppStandardReferenceItemQuery("a");
            query.Where
                (
                    query.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel,
                    query.ItemName.Like(searchText),
                    query.IsActive == true
                );
            query.Where(query.ReferenceID == cboSRClinicalWorkArea.SelectedValue);
            query.OrderBy(query.ItemID.Ascending);

            cboSRClinicalAuthorityLevel.DataSource = query.LoadDataTable();
            cboSRClinicalAuthorityLevel.DataBind();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CredentialProcessQuery("a");
            var personal = new PersonalInfoQuery("b");
            var profession = new AppStandardReferenceItemQuery("c");
            var area = new AppStandardReferenceItemQuery("d");
            var level = new AppStandardReferenceItemQuery("e");

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(profession).On(profession.StandardReferenceID == AppEnum.StandardReference.ProfessionGroup.ToString() & profession.ItemID == query.SRProfessionGroup);
            query.InnerJoin(area).On(area.StandardReferenceID == AppEnum.StandardReference.ClinicalWorkArea.ToString() & area.ItemID == query.SRClinicalWorkArea);
            query.LeftJoin(level).On(level.StandardReferenceID == AppEnum.StandardReference.ClinicalAuthorityLevel.ToString() & level.ItemID == query.SRClinicalAuthorityLevel);
            query.OrderBy
                (
                    query.TransactionNo.Descending
                );

            query.Select(
                query.TransactionNo,
                query.TransactionDate,
                query.PersonID,
                personal.EmployeeNumber,
                personal.EmployeeName,
                profession.ItemName.As("ProfessionGroupName"),
                area.ItemName.As("ClinicalWorkAreaName"),
                level.ItemName.As("ClinicalAuthorityLevelName"),
                query.IsApproved,
                query.IsVoid
                );
            
            query.Where(query.SRProfessionGroup == "01");
            
            if (Role == "usr")
                query.Where(query.PersonID == AppSession.UserLogin.PersonID.ToInt());

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                string searchText = string.Format("%{0}%", txtTransactionNo.Text);
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                    query.Where(query.TransactionNo.Like(searchText));
            }
            if (!txtTransactionDate.IsEmpty)
                query.Where(query.TransactionDate == txtTransactionDate.SelectedDate);
            if (!string.IsNullOrEmpty(txtEmployeeNo.Text))
            {
                string searchText = string.Format("%{0}%", txtEmployeeNo.Text);
                if (cboFilterEmployeeNo.SelectedIndex == 1)
                    query.Where(personal.EmployeeNumber == txtEmployeeNo.Text);
                else
                    query.Where(personal.EmployeeNumber.Like(searchText));
            }
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                string searchText = string.Format("%{0}%", txtEmployeeName.Text);
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(personal.EmployeeName == txtEmployeeName.Text);
                else
                    query.Where(personal.EmployeeName.Like(searchText));
            }
            if (!string.IsNullOrEmpty(cboSRClinicalWorkArea.SelectedValue))
                query.Where(query.SRClinicalWorkArea == cboSRClinicalWorkArea.SelectedValue);
            if (!string.IsNullOrEmpty(cboSRClinicalAuthorityLevel.SelectedValue))
                query.Where(query.SRClinicalAuthorityLevel == cboSRClinicalAuthorityLevel.SelectedValue);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}