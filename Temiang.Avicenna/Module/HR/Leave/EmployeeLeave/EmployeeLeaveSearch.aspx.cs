using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.Leave
{
    public partial class EmployeeLeaveSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.EmployeeLeave;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSREmployeeLeaveType, AppEnum.StandardReference.EmployeeLeaveType);
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new EmployeeLeaveQuery("a");
            var personal = new VwEmployeeTableQuery("b");
            var type = new AppStandardReferenceItemQuery("c");
            var request = new EmployeeLeaveRequestQuery("d");

            query.es.Top = AppSession.Parameter.MaxResultRecord;
            query.Select(
                query.EmployeeLeaveID,
                query.PersonID,
                personal.EmployeeNumber,
                personal.EmployeeName,
                query.SREmployeeLeaveType,
                type.ItemName.As("EmployeeLeaveTypeName"),
                query.StartDate,
                query.EndDate,
                query.LeaveEntitlementsQty,
                query.Notes,
                query.LastUpdateDateTime,
                query.LastUpdateByUserID,
                @"<a.LeaveEntitlementsQty - ISNULL(SUM(d.ApprovedDays), 0) AS Balance>"
                );
            query.InnerJoin(personal).On(query.PersonID == personal.PersonID);
            query.InnerJoin(type).On(query.SREmployeeLeaveType == type.ItemID &&
                               type.StandardReferenceID == AppEnum.StandardReference.EmployeeLeaveType);
            query.LeftJoin(request).On(query.EmployeeLeaveID == request.EmployeeLeaveID &&
                                               request.IsVerified == true && request.IsRequestApproved == true);
            query.GroupBy(query.EmployeeLeaveID,
                                  query.PersonID,
                                  personal.EmployeeNumber,
                                  personal.EmployeeName,
                                  query.SREmployeeLeaveType,
                                  type.ItemName,
                                  query.StartDate,
                                  query.EndDate,
                                  query.LeaveEntitlementsQty,
                                  query.Notes,
                                  query.LastUpdateDateTime,
                                  query.LastUpdateByUserID);

            query.OrderBy(query.EmployeeLeaveID.Descending);

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
            if (!string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                if (cboFilterEmployeeName.SelectedIndex == 1)
                    query.Where(personal.EmployeeName == txtEmployeeName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtEmployeeName.Text);
                    query.Where(personal.EmployeeName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSREmployeeLeaveType.SelectedValue))
            {
                query.Where(query.SREmployeeLeaveType == cboSREmployeeLeaveType.SelectedValue);
            }

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}
