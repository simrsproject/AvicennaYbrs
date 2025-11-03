using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ImageTemplateList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "ImageTemplateSearch.aspx";
            UrlPageDetail = "ImageTemplateDetail.aspx";

            ProgramID = AppConstant.Program.ImageTemplate;

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
            string id = dataItem.GetDataKeyValue(ImageTemplateMetadata.ColumnNames.ImageTemplateID).ToString();
            Page.Response.Redirect("ImageTemplateDetail.aspx?md=" + mode + "&id=" + id, true);
        }
        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ImageTemplates;
        }

        private DataTable ImageTemplates
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                ImageTemplateQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ImageTemplateQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ImageTemplateQuery();
                    query.OrderBy(query.ImageTemplateID.Ascending);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

