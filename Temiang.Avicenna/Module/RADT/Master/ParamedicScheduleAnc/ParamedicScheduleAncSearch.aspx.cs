using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleAnc
{
    public partial class ParamedicScheduleAncSearch : BasePageDialog
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.ParamedicScheduleAnc;
            if (!IsPostBack)
                PopulateMonthComboBox(cboPeriodMonth);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        private void PopulateMonthComboBox(RadComboBox cbo)
        {
            cbo.Items.Add(new RadComboBoxItem("", ""));
            cbo.Items.Add(new RadComboBoxItem("January", "01"));
            cbo.Items.Add(new RadComboBoxItem("February", "02"));
            cbo.Items.Add(new RadComboBoxItem("March", "03"));
            cbo.Items.Add(new RadComboBoxItem("April", "04"));
            cbo.Items.Add(new RadComboBoxItem("May", "05"));
            cbo.Items.Add(new RadComboBoxItem("June", "06"));
            cbo.Items.Add(new RadComboBoxItem("July", "07"));
            cbo.Items.Add(new RadComboBoxItem("August", "08"));
            cbo.Items.Add(new RadComboBoxItem("September", "09"));
            cbo.Items.Add(new RadComboBoxItem("October", "10"));
            cbo.Items.Add(new RadComboBoxItem("November", "11"));
            cbo.Items.Add(new RadComboBoxItem("December", "12"));
        }
        public override bool OnButtonOkClicked()
        {
            var query = new ParamedicScheduleDateQuery("a");
            var par = new ParamedicQuery("p");
            var unit = new ServiceUnitQuery("c");
            var monthRef = new AppStandardReferenceItemQuery("d");
            var tcode = new ServiceUnitTransactionCodeQuery("e");

            query.es.Distinct = true;
            query.Select
                (
                    query.ServiceUnitID,
                    query.ParamedicID,
                    query.PeriodYear,
                    query.PeriodMonth,
                    query.ScheduleDate,
                    unit.ServiceUnitName,
                    par.ParamedicName,
                    @"<a.PeriodYear+a.PeriodMonth AS 'PeriodYearMonthID'>",
                    @"<a.PeriodYear + ' - ' + d.ItemName AS 'PeriodYearMonthName'>"
                );
            query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID && unit.IsUsingJobOrder == true);
            query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
            query.InnerJoin(monthRef).On(monthRef.StandardReferenceID == "MonthID" && monthRef.ItemID == query.PeriodMonth);
            query.InnerJoin(tcode).On(tcode.ServiceUnitID == query.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.JobOrder.ToString());

            query.OrderBy(unit.ServiceUnitName.Ascending, par.ParamedicName.Ascending);

            if (!string.IsNullOrEmpty(txtServiceUnitName.Text))
            {
                if (cboFilterServiceUnitName.SelectedIndex == 1)
                    query.Where(unit.ServiceUnitName == txtServiceUnitName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtServiceUnitName.Text);
                    query.Where(unit.ServiceUnitName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtParamedicName.Text))
            {
                if (cboFilterParamedicName.SelectedIndex == 1)
                    query.Where(par.ParamedicName == txtParamedicName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtParamedicName.Text);
                    query.Where(par.ParamedicName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
                query.Where(query.PeriodYear == txtPeriodYear.Text);
            if (!string.IsNullOrEmpty(cboPeriodMonth.SelectedValue))
                query.Where(query.PeriodMonth == cboPeriodMonth.SelectedValue);
            if (!txtScheduleDate.IsEmpty)
                query.Where(query.ScheduleDate == txtScheduleDate.SelectedDate);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}