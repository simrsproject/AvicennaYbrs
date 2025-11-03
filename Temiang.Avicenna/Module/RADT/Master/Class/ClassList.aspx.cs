using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ClassList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ClassSearch.aspx";
            UrlPageDetail = "ClassDetail.aspx";

            ProgramID = AppConstant.Program.ServiceClass;

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
            string id = dataItem.GetDataKeyValue(ClassMetadata.ColumnNames.ClassID).ToString();
            Page.Response.Redirect("ClassDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Class;
        }

        private DataTable Class
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ClassQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ClassQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ClassQuery("a");
                    AppStandardReferenceItemQuery item = new AppStandardReferenceItemQuery("b");
                    query.LeftJoin(item).On(query.SRClassRL == item.ItemID && item.StandardReferenceID == "ClassRL");
                    query.Select
                        (
                            query.ClassID,
                            query.ClassName,
                            query.ShortName,
                            item.ItemName,
                            query.MarginPercentage,
                            query.Margin2Percentage,
                            query.DepositAmount,
                            query.ClassSeq,
                            query.IsInPatientClass,
                            query.IsTariffClass,
                            query.IsActive
                        );
                    query.OrderBy(query.ClassID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ClassName", "ClassID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
