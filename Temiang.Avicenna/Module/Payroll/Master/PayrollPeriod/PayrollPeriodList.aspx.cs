using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class PayrollPeriodList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PayrollPeriodSearch.aspx";
            UrlPageDetail = "PayrollPeriodDetail.aspx";

            ProgramID = AppConstant.Program.PayrollPeriod; //TODO: Isi ProgramID
            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
               
            }

            // Quick Search
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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(PayrollPeriodMetadata.ColumnNames.PayrollPeriodID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PayrollPeriods;
        }

        private DataTable PayrollPeriods
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PayrollPeriodQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PayrollPeriodQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery sequent = new AppStandardReferenceItemQuery("b"); 
                    query = new PayrollPeriodQuery("a");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    sequent.ItemName.As("PaySequentName"),
                                    query
                                );
                    query.InnerJoin(sequent).On
                           (
                               query.SRPaySequent == sequent.ItemID &
                               sequent.StandardReferenceID == "PaySequent"
                           );
                    query.OrderBy(query.PayrollPeriodCode.Descending, query.SRPaySequent.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query);
                }
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

