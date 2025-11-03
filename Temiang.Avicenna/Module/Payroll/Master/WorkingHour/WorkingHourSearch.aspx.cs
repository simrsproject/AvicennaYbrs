using System;
using System.Collections.Generic;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WorkingHourSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.WorkingHour;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRShift, AppEnum.StandardReference.Shift);
                cboSRShift.Items.AddRange(new List<Telerik.Web.UI.RadComboBoxItem>
                {
                    //new Telerik.Web.UI.RadComboBoxItem("Morning & Afternoon", "ShiftID-012"),
                    new Telerik.Web.UI.RadComboBoxItem("Morning & Night", "ShiftID-013"),
                    //new Telerik.Web.UI.RadComboBoxItem("Afternoon & Night", "ShiftID-023")
                });
            }
        }

        public override bool OnButtonOkClicked()
        {
            var query = new WorkingHourQuery("a");
            var shift = new AppStandardReferenceItemQuery("b");
            var wd = new AppStandardReferenceItemQuery("c");
            query.LeftJoin(shift).On(query.SRShift == shift.ItemID && shift.StandardReferenceID == AppEnum.StandardReference.Shift);
            query.LeftJoin(wd).On(query.SRWorkingDay == wd.ItemID && wd.StandardReferenceID == AppEnum.StandardReference.WorkingDay);
            query.Select(query, @"<CASE WHEN a.SRShift = 'ShiftID-013' THEN 'Morning & Night' ELSE b.ItemName END AS ItemName>", wd.ItemName.As("WorkingDayName"));

            if (!string.IsNullOrEmpty(txtWorkingHourName.Text))
            {
                if (cboFilterWorkingHourName.SelectedIndex == 1)
                    query.Where(query.WorkingHourName == txtWorkingHourName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtWorkingHourName.Text);
                    query.Where(query.WorkingHourName.Like(searchTextContain));
                }
            }

            if (!string.IsNullOrEmpty(cboSRShift.SelectedValue))
            {
               query.Where(query.SRShift == cboSRShift.SelectedValue);
            }
            query.OrderBy(query.WorkingHourID.Ascending);

            query.es.Top = AppSession.Parameter.MaxResultRecord;

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            return true;
        }
    }
}