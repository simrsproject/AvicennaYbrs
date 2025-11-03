using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.Interfaces;
using static Temiang.Avicenna.Common.SirsDinkes.Eis.Json.KetersediaanBed.Get.Response;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicScheduleAdditionalQuotaList : BasePage
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

            ProgramID = AppConstant.Program.ParamedicScheduleAdditionalQuota;

            if (!IsPostBack)
            {
                txtScheduleDate.SelectedDate = DateTime.Now;
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

            var dataSource = ParamedicScheduleDates;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable ParamedicScheduleDates
        {
            get
            {
                var isEmptyFilter = txtScheduleDate.IsEmpty && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboParamedicID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Physician Schedule (Additional Quota)")) return null;

                var query = new ParamedicScheduleDateQuery("a");
                var ps = new ParamedicScheduleQuery("b");
                var par = new ParamedicQuery("c");
                var unit = new ServiceUnitQuery("d");
                var ot = new OperationalTimeQuery("e");

                query.Select
                    (
                        query.ServiceUnitID,
                        unit.ServiceUnitName,
                        query.ParamedicID,
                        par.ParamedicName,
                        query.ScheduleDate,
                        query.OperationalTimeID,
                        ot.OperationalTimeName,
                        ot.OperationalTimeBackcolor,
                        @"<ISNULL(b.Quota, 0) AS Quota>",
                        @"<ISNULL(b.QuotaOnline, 0) AS QuotaOnline>",
                        @"<ISNULL(b.QuotaBpjs, 0) AS QuotaBpjs>",
                        @"<ISNULL(b.QuotaBpjsOnline, 0) AS QuotaBpjsOnline>",
                        @"<ISNULL(a.AddQuota, 0) AS AddQuota>",
                        @"<ISNULL(a.AddQuotaOnline, 0) AS AddQuotaOnline>",
                        @"<ISNULL(a.AddQuotaBpjs, 0) AS AddQuotaBpjs>",
                        @"<ISNULL(a.AddQuotaBpjsOnline, 0) AS AddQuotaBpjsOnline>"
                    );

                query.InnerJoin(ps).On(ps.ServiceUnitID == query.ServiceUnitID && ps.ParamedicID == query.ParamedicID && ps.PeriodYear == query.PeriodYear);
                query.InnerJoin(par).On(par.ParamedicID == query.ParamedicID);
                query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);
                query.InnerJoin(ot).On(ot.OperationalTimeID == query.OperationalTimeID);

                query.Where(query.ScheduleDate == txtScheduleDate.SelectedDate);
                if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (!(string.IsNullOrEmpty(cboParamedicID.SelectedValue)))
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

                query.OrderBy(query.ParamedicID.Ascending, query.ServiceUnitID.Ascending);
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);

            var query = new ServiceUnitQuery("a");
            var tcode = new ServiceUnitTransactionCodeQuery("c");
            query.LeftJoin(tcode).On(tcode.ServiceUnitID == query.ServiceUnitID && tcode.SRTransactionCode == BusinessObject.Reference.TransactionCode.JobOrder.ToString());

            query.es.Top = 30;
            query.Select(query.ServiceUnitID, query.ServiceUnitName);
            query.Where
                (
                    query.Or
                        (
                             query.ServiceUnitID.Like(searchText),
                             query.ServiceUnitName.Like(searchText)
                        ),
                    query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    query.IsActive == true,
                    tcode.ServiceUnitID.IsNull()
                );
            query.OrderBy(query.ServiceUnitName.Ascending);

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboParamedicID.Items.Clear();
            cboParamedicID.Text = string.Empty;
        }

        protected void ParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchText = string.Format("%{0}%", e.Text);
            var query = new ParamedicQuery("a");
            var serviceUnitParamedic = new ServiceUnitParamedicQuery("b");
            query.Select(query.ParamedicID, query.ParamedicName);
            query.Where
                (
                    query.Or
                        (
                            query.ParamedicID.Like(searchText),
                            query.ParamedicName.Like(searchText)
                        ),
                    query.IsActive == true
                );
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(serviceUnitParamedic.ServiceUnitID == cboServiceUnitID.SelectedValue);
            query.InnerJoin(serviceUnitParamedic).On(query.ParamedicID == serviceUnitParamedic.ParamedicID);
            query.OrderBy(query.ParamedicName.Ascending);

            query.es.Top = 30;
            query.es.Distinct = true;

            DataTable dtb = query.LoadDataTable();

            cboParamedicID.DataSource = dtb;
            cboParamedicID.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();

        }
    }
}