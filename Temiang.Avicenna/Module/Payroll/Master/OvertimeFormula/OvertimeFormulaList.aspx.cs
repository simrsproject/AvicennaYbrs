using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class OvertimeFormulaList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "OvertimeFormulaSearch.aspx";
            UrlPageDetail = "OvertimeFormulaDetail.aspx";

            ProgramID = AppConstant.Program.OvertimeFormula;

            ToolBarMenuQuickSearch.Visible = true;
        }
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }
        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(OvertimeMetadata.ColumnNames.OvertimeID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Overtimes;
        }

        private DataTable Overtimes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                OvertimeQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (OvertimeQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new OvertimeQuery("a");
                    var sc = new SalaryComponentQuery("b");

                    query.Select(
                        query.OvertimeID,
                        query.OvertimeName,
                        query.SalaryComponentID,
                        sc.SalaryComponentName,
                        query.Notes,
                        query.ValidFrom,
                        query.ValidTo,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                            );
                    query.InnerJoin(sc).On
                        (
                            query.SalaryComponentID == sc.SalaryComponentID
                        );
                    query.OrderBy(query.OvertimeID.Ascending);

                    ApplyQuickSearch(query, "OvertimeName", "OvertimeID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}