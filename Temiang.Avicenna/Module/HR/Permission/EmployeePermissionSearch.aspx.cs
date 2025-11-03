using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Permission
{
    public partial class EmployeePermissionSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeePermission;

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved Yet", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Not Verified Yet", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Verified", "3"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeePermissionQuery("a");
            var supervisor = new VwEmployeeTableQuery("b");
            var personal = new VwEmployeeTableQuery("c");
            var type = new AppStandardReferenceItemQuery("d");
            var usr = new AppUserQuery("e");

            query.InnerJoin(supervisor).On(query.SupervisorID == supervisor.PersonID);
            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(type).On(query.SRPermissionType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.PermissionType.ToString());
            query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);

            query.Select(
                query.PermissionID,
                query.PermissionDate,
                query.SupervisorID,
                supervisor.EmployeeName.As("SupervisorName"),
                query.PersonID,
                personal.EmployeeNumber,
                personal.EmployeeName,
                query.SRPermissionType,
                type.ItemName.As("PermissionTypeName"),
                query.PermissionDateFrom,
                query.PermissionDateTo,
                query.Notes,
                query.IsApproved,
                query.IsVoid,
                query.IsVerified,
                query.VerifiedDateTime,
                usr.UserName.As("VerifiedBy"),
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );

            query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);
            query.OrderBy
                (
                    query.PermissionID.Descending
                );

            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
            {
                query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
            }
            if (!txtFromPermissionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.PermissionDate >= txtFromPermissionDate.SelectedDate);
            }
            if (!txtToPermissionDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.PermissionDate <= txtToPermissionDate.SelectedDate);
            }
            if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
            {
                switch (cboStatus.SelectedValue)
                {
                    case "0":
                        query.Where(query.Or(query.IsApproved.IsNull(), query.IsApproved == false));
                        break;
                    case "1":
                        query.Where(query.IsApproved == true);
                        break;
                    case "2":
                        query.Where(query.Or(query.IsVerified.IsNull(), query.IsVerified == false));
                        break;
                    case "3":
                        query.Where(query.IsVerified == true);
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
            var query = new VwEmployeeTableQuery();
            query.es.Top = 20;
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
            e.Item.Text = ((DataRowView)e.Item.DataItem)["EmployeeNumber"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["EmployeeName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PersonID"].ToString();
        }
    }
}