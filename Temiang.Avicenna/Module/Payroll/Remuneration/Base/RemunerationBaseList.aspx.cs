using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Remuneration.Base
{
    public partial class RemunerationBaseList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "RemunerationBaseSearch.aspx";
            UrlPageDetail = "RemunerationBaseDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.EmployeeRemunerationBase;
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
            string id = dataItem.GetDataKeyValue(WageBaseMetadata.ColumnNames.WageBaseID).ToString();
            string url = string.Format("RemunerationBaseDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = WageBases;
        }

        private DataTable WageBases
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                WageBaseQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (WageBaseQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new WageBaseQuery("a");
                    query.Select(query);
                    query.OrderBy(query.ValidFrom.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}