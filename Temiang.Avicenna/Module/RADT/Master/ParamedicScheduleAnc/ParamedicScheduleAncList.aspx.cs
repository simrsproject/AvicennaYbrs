using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;
using static Temiang.Avicenna.Common.SirsDinkes.Eis.Json.KetersediaanBed.Get.Response;

namespace Temiang.Avicenna.Module.RADT.Master.ParamedicScheduleAnc
{
    public partial class ParamedicScheduleAncList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.ParamedicScheduleAnc;

            if (!Page.IsPostBack)
            {
                cboPeriodMonth.Items.Add(new RadComboBoxItem("January", "01"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("February", "02"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("March", "03"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("April", "04"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("May", "05"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("June", "06"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("July", "07"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("August", "08"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("September", "09"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("October", "10"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("November", "11"));
                cboPeriodMonth.Items.Add(new RadComboBoxItem("December", "12"));

                for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 30; i--)
                {
                    cboPeriodYear.Items.Add(new RadComboBoxItem(i.ToString(), i.ToString()));
                }

                cboPeriodYear.SelectedValue = DateTime.Now.Year.ToString();
                cboPeriodMonth.SelectedValue = DateTime.Now.ToString("MM");

                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    query.IsActive == true
                    );
                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ParamedicSchedules;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;

        }

        private DataTable ParamedicSchedules
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboPeriodMonth.SelectedValue) && string.IsNullOrEmpty(cboPeriodYear.SelectedValue) && txtScheduleDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboPhysicianID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Physician Schedule (Medical Support Unit)")) return null;

                var query = new ParamedicScheduleDateQuery("a");
                var par = new ParamedicQuery("b");
                var unit = new ServiceUnitQuery("c");
                var monthRef = new AppStandardReferenceItemQuery("d");
                var tcode = new ServiceUnitTransactionCodeQuery("e");

                query.Select
                    (
                        query.ServiceUnitID,
                        query.ParamedicID,
                        query.PeriodYear,
                        query.ScheduleDate,
                        unit.ServiceUnitName,
                        par.ParamedicName,
                        @"<a.PeriodYear + a.PeriodMonth AS 'PeriodYearMonthID'>",
                        @"<a.PeriodYear + ' - ' + d.ItemName AS 'PeriodYearMonthName'>"
                    );
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID && unit.IsUsingJobOrder == true);
                query.InnerJoin(par).On(query.ParamedicID == par.ParamedicID);
                query.InnerJoin(monthRef).On(monthRef.StandardReferenceID == "MonthID" && monthRef.ItemID == query.PeriodMonth);
                query.InnerJoin(tcode).On(tcode.ServiceUnitID == query.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.JobOrder.ToString());

                if (txtScheduleDate.IsEmpty)
                    query.Where(query.PeriodMonth == cboPeriodMonth.SelectedValue, query.PeriodYear == cboPeriodYear.SelectedValue);
                else
                    query.Where(query.ScheduleDate == txtScheduleDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboPhysicianID.SelectedValue))
                    query.Where(query.ParamedicID == cboPhysicianID.SelectedValue);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);

                query.OrderBy(par.ParamedicName.Ascending, unit.ServiceUnitName.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "new")
            {
                string url = string.Format("ParamedicScheduleAncDetail.aspx?md={0}", eventArgument);
                Page.Response.Redirect(url, true);
            }
        }

        protected void cboPhysicianID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            query.Where(
                query.Or(query.ParamedicID == e.Text,
                query.ParamedicName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.ParamedicID, query.ParamedicName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboPhysicianID.DataSource = dtb;
            cboPhysicianID.DataBind();
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ParamedicItemDataBound(e);
        }
    }
}