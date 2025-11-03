using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class EmployeeLeaveRequestSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "request" ? AppConstant.Program.EmployeeLeaveRequest : AppConstant.Program.EmployeeLeaveRequest2;

            if (!IsPostBack)
            {
                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Not Approved", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Approved", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Not Validated", "2"));
                cboStatus.Items.Add(new RadComboBoxItem("Validated", "3"));
                if (AppSession.Parameter.EmployeeLeaveApprovalLevel != "1")
                {
                    if (AppSession.Parameter.EmployeeLeaveApprovalLevel == "3")
                    {
                        cboStatus.Items.Add(new RadComboBoxItem("Not Validated #1", "4"));
                        cboStatus.Items.Add(new RadComboBoxItem("Validated #1", "5"));
                    }
                    cboStatus.Items.Add(new RadComboBoxItem("Not Validated #2", "6"));
                    cboStatus.Items.Add(new RadComboBoxItem("Validated #2", "7"));
                }
                cboStatus.Items.Add(new RadComboBoxItem("Not Verified", "8"));
                cboStatus.Items.Add(new RadComboBoxItem("Verified", "9"));
                cboStatus.Items.Add(new RadComboBoxItem("Rejected", "X"));
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeLeaveRequestQuery("a");
            //var personal = new VwEmployeeTableQuery("b");
            var personal = new PersonalInfoQuery("b");
            var leave = new EmployeeLeaveQuery("c");
            var type = new AppStandardReferenceItemQuery("d");
            var usr = new AppUserQuery("e");
            var usrval = new AppUserQuery("f");
            var usrval1 = new AppUserQuery("f1");
            var usrval2 = new AppUserQuery("f2");

            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(leave).On(query.EmployeeLeaveID == leave.EmployeeLeaveID);
            query.InnerJoin(type).On(leave.SREmployeeLeaveType == type.ItemID & type.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType.ToString());
            query.LeftJoin(usr).On(query.VerifiedByUserID == usr.UserID);
            query.LeftJoin(usrval).On(query.ValidatedByUserID == usrval.UserID);
            query.LeftJoin(usrval1).On(query.Validated1ByUserID == usrval1.UserID);
            query.LeftJoin(usrval2).On(query.Validated2ByUserID == usrval2.UserID);

            if (FormType == "request") query.Where(query.CreatedByUserID == AppSession.UserLogin.UserID);
            
            query.OrderBy
                (
                    query.LeaveRequestID.Descending
                );

            query.Select(
                query.LeaveRequestID,
                query.RequestDate,
                query.PersonID,
                personal.EmployeeNumber,
                personal.EmployeeName,
                query.EmployeeLeaveID,
                @"<d.ItemName + ' [' + CASE WHEN ISNULL(c.Notes, '') = '' THEN 'Period: ' + CONVERT(VARCHAR, c.StartDate, 106) + ' - ' + CONVERT(VARCHAR, c.EndDate, 106) ELSE c.Notes END + ']' AS EmployeeLeaveTypeName>",
                query.RequestLeaveDateFrom,
                query.RequestLeaveDateTo,
                query.RequestDays,
                query.RequestWorkingDate,
                query.Notes,
                query.IsApproved,
                query.IsVoid,
                query.IsRequestApproved,
                query.ApprovedLeaveDateFrom,
                query.ApprovedLeaveDateTo,
                query.ApprovedDays,
                query.IsVerified,
                query.VerifiedDateTime,
                usr.UserName.As("VerifiedBy"),
                query.IsValidated,
                query.ValidatedDateTime,
                usrval.UserName.As("ValidatedBy"),
                query.IsValidated1,
                query.Validated1DateTime,
                usrval1.UserName.As("Validated1By"),
                query.IsValidated2,
                query.Validated2DateTime,
                usrval2.UserName.As("Validated2By"),
                query.LastUpdateDateTime,
                query.LastUpdateByUserID
                );

            if (!string.IsNullOrEmpty(cboPersonID.SelectedValue))
            {
                query.Where(query.PersonID == cboPersonID.SelectedValue.ToInt());
            }
            if (!txtFromRequestDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtToRequestDate.SelectedDate.ToString().Trim().Equals(string.Empty))
            {
                query.Where(query.RequestDate.Between(txtFromRequestDate.SelectedDate, txtToRequestDate.SelectedDate));
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
                        query.Where(query.Or(query.IsValidated.IsNull(), query.IsValidated == false));
                        break;
                    case "3":
                        query.Where(query.IsValidated == true);
                        break;
                    case "4":
                        query.Where(query.Or(query.IsValidated1.IsNull(), query.IsValidated1 == false));
                        break;
                    case "5":
                        query.Where(query.IsValidated1 == true);
                        break;
                    case "6":
                        query.Where(query.Or(query.IsValidated2.IsNull(), query.IsValidated2 == false));
                        break;
                    case "7":
                        query.Where(query.IsValidated2 == true);
                        break;
                    case "8":
                        query.Where(query.Or(query.IsVerified.IsNull(), query.IsVerified == false));
                        break;
                    case "9":
                        query.Where(query.IsVerified == true);
                        break;
                    case "X":
                        query.Where(query.IsRequestApproved == false);
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
