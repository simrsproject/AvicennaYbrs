using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Transaction.Attendance
{
    public partial class AttendanceOutstandingListRstj : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AttendanceOutstandingList;

            if (!IsPostBack)
            {
                var emps = new VwEmployeeTableCollection();
                emps.Query.Where(emps.Query.SREmployeeStatus == "1", emps.Query.AbsenceCardNo.IsNotNull());
                emps.Query.Load();

                cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem(string.Empty, string.Empty));
                foreach (var emp in emps)
                {
                    cboEmployeeName.Items.Add(new Telerik.Web.UI.RadComboBoxItem($"{emp.EmployeeNumber} - {emp.EmployeeName}", emp.AbsenceCardNo.ToString()));
                }

                for (int i = 1; i <= 12; i++)
                {
                    cboMonth.Items.Add(new Telerik.Web.UI.RadComboBoxItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i), i.ToString()));
                }

                cboMonth.SelectedValue = DateTime.Now.Month.ToString();
                txtYear.Value = DateTime.Now.Year;
            }
        }

        protected void grdList_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack) return;

            var svc = new Common.Worklist.RSTJ.Service();

            grdList.DataSource = svc.GetDataAbsensi(cboEmployeeName.SelectedValue, cboMonth.SelectedValue.ToInt(), Convert.ToInt32(txtYear.Value));
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}