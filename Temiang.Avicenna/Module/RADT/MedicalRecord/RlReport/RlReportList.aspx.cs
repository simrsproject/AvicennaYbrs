using System;
using System.Data;
using System.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.MedicalRecord
{
    public partial class RlReportList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RlReport;
            if (!IsPostBack)
            {
                //txtPeriodYear.Text = DateTime.Now.Year.ToString();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = RlMasterReports;
        }

        private DataTable RlMasterReports
        {
            get
            {
                var query = new RlMasterReportQuery();
                query.Select(query.RlMasterReportID, query.RlMasterReportNo, query.RlMasterReportName);
                query.Where(query.IsActive == true);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new RlTxReportQuery("a");
            var month1 = new AppStandardReferenceItemQuery("b");
            var month2 = new AppStandardReferenceItemQuery("c");
            query.LeftJoin(month1).On(query.PeriodMonthStart == month1.ReferenceID && month1.StandardReferenceID == "MonthID");
            query.LeftJoin(month2).On(query.PeriodMonthEnd == month2.ReferenceID && month2.StandardReferenceID == "MonthID");
            query.Select
                (
                    query.RlMasterReportID,
                    query.RlTxReportNo,
                    month1.ItemName.As("StartMonth"),
                    month2.ItemName.As("EndMonth"),
                    query.PeriodYear,
                    query.IsApproved
                );
            query.Where(query.RlMasterReportID == e.DetailTableView.ParentItem.GetDataKeyValue("RlMasterReportID"));
            if (!string.IsNullOrEmpty(txtPeriodYear.Text))
                query.Where(query.PeriodYear == txtPeriodYear.Text);
            else
                query.es.Top = 15;

            query.OrderBy(query.PeriodYear.Descending, month1.ItemID.Descending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
