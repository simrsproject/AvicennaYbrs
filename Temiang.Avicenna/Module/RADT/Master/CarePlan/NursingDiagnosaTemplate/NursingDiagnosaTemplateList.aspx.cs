using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.NursingCare.Master
{
    public partial class NursingDiagnosaTemplateList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "NursingDiagnosaTemplateSearch.aspx";
            UrlPageDetail = "NursingDiagnosaTemplateDetaill.aspx";

            ProgramID = AppConstant.Program.NursingDiagnosaTemplate;

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
            string id = dataItem.GetDataKeyValue(NursingDiagnosaTemplateMetadata.ColumnNames.TemplateID).ToString();
            Page.Response.Redirect(string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id), true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Diagnosa;
        }

        private DataTable Diagnosa
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                NursingDiagnosaTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (NursingDiagnosaTemplateQuery)Session[SessionNameForQuery];
                else
                {
                    query = new NursingDiagnosaTemplateQuery("a");
                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
