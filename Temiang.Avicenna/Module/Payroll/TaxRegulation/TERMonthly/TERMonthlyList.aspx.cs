using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class TERMonthlyList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "TERMonthlySearch.aspx";
            UrlPageDetail = "TERMonthlyDetail.aspx";

            ProgramID = AppConstant.Program.TERMonthly; //TODO: Isi ProgramID
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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(TERMonthlyMetadata.ColumnNames.TERMonthlyID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = TERMonthlys;
        }

        private DataTable TERMonthlys
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                TERMonthlyQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (TERMonthlyQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new TERMonthlyQuery("a");
                    var ts = new AppStandardReferenceItemQuery("b");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select
                        (
                            query.TERMonthlyID,
                            query.ValidFrom,
                            query.SRTaxStatus,
                            ts.ItemName.As("TaxStatusName"),
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                        );
                    query.InnerJoin(ts).On(ts.StandardReferenceID == AppEnum.StandardReference.TaxStatus.ToString() && ts.ItemID == query.SRTaxStatus);
                    query.OrderBy(query.ValidFrom.Descending, query.SRTaxStatus.Ascending);

                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}