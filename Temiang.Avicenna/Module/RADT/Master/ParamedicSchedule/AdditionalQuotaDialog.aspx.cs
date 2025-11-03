using System;
using System.Drawing;
using System.Linq;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicScheduleAdditionalQuotaDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicScheduleAdditionalQuota;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var par = new Paramedic();
                par.LoadByPrimaryKey(Request.QueryString["pid"]);
                txtParamedicName.Text = par.ParamedicName;

                var unit = new ServiceUnit();
                unit.LoadByPrimaryKey(Request.QueryString["uid"]);
                txtServiceUnitName.Text = unit.ServiceUnitName;

                txtScheduleDate.SelectedDate = Convert.ToDateTime(Request.QueryString["sdate"]);

                var psd = new ParamedicScheduleDate();
                if (psd.LoadByPrimaryKey(Request.QueryString["uid"].ToString(), Request.QueryString["pid"].ToString(), txtScheduleDate.SelectedDate.Value.Year.ToString(), txtScheduleDate.SelectedDate.Value.Date))
                {
                    var ot = new OperationalTime();
                    if (ot.LoadByPrimaryKey(psd.OperationalTimeID))
                    {
                        txtOperationalTimeID.BackColor = ColorTranslator.FromHtml(ot.OperationalTimeBackcolor); 
                        txtOperationalTimeName.Text = ot.OperationalTimeName;
                    }
                    else
                        txtOperationalTimeName.Text = string.Empty;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                double addQuota = ((RadNumericTextBox)dataItem.FindControl("txtAddQuota")).Value ?? 0;
                double addQuotaOnline = ((RadNumericTextBox)dataItem.FindControl("txtAddQuotaOnline")).Value ?? 0;
                double addQuotaBpjs = ((RadNumericTextBox)dataItem.FindControl("txtAddQuotaBpjs")).Value ?? 0;
                double addQuotaBpjsOnline = ((RadNumericTextBox)dataItem.FindControl("txtAddQuotaBpjsOnline")).Value ?? 0;

                var entity = new ParamedicScheduleDate();
                if (entity.LoadByPrimaryKey(Request.QueryString["uid"].ToString(), Request.QueryString["pid"].ToString(), txtScheduleDate.SelectedDate.Value.Year.ToString(), txtScheduleDate.SelectedDate.Value.Date))
                {
                    entity.AddQuota = Convert.ToInt16(addQuota);
                    entity.AddQuotaOnline = Convert.ToInt16(addQuotaOnline);
                    entity.AddQuotaBpjs = Convert.ToInt16(addQuotaBpjs);
                    entity.AddQuotaBpjsOnline = Convert.ToInt16(addQuotaBpjsOnline);

                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = DateTime.Now;

                    entity.Save();
                }
            }

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ParamedicScheduleDates;
        }

        private DataTable ParamedicScheduleDates
        {
            get
            {
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

                query.Where(query.ServiceUnitID == Request.QueryString["uid"].ToString(), query.ParamedicID == Request.QueryString["pid"].ToString(), 
                    query.PeriodYear == txtScheduleDate.SelectedDate.Value.Year.ToString(), query.ScheduleDate == txtScheduleDate.SelectedDate);
                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }
    }
}