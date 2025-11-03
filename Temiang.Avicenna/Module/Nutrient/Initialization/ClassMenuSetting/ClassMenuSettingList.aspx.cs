using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class ClassMenuSettingList : BasePageList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ToolBarMenuQuickSearch.Enabled = true;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 140;

            UrlPageSearch = "ClassMenuSettingSearch.aspx";
            UrlPageDetail = "ClassMenuSettingDetail.aspx";

            ProgramID = AppConstant.Program.ClassMenuSetting;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(ClassMetadata.ColumnNames.ClassID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Classes;
        }

        private DataTable Classes
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ClassQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ClassQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ClassQuery("a");
                    var detail = new ClassMenuSettingQuery("b");
                    query.LeftJoin(detail).On(query.ClassID == detail.ClassID);
                    query.Select
                        (
                            query.ClassID,
                            query.ClassName, 
                            detail.IsOptional
                        );
                    query.Where(query.IsActive == true, query.IsInPatientClass == true, query.IsTariffClass == true);
                    query.OrderBy(query.ClassID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
