using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.HR.K3RS
{
    public partial class FormsList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "FormsSearch.aspx";
            UrlPageDetail = "FormsDetail.aspx";

            ProgramID = AppConstant.Program.K3RS_Form;

            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(K3rsFormMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = K3rsForms;
        }

        private DataTable K3rsForms
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                K3rsFormQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (K3rsFormQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new K3rsFormQuery("a");
                    var template = new K3rsFormTemplateQuery("b");
                    query.InnerJoin(template).On(template.TemplateID == query.TemplateID);
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                            query.TransactionNo,
                            query.TransactionDate,
                            query.TemplateID,
                            template.TemplateName,
                            query.Notes,
                            query.Result.Substring(100).As("Result"));
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}