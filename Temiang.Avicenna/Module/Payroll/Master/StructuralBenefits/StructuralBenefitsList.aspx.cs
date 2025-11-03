using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class StructuralBenefitsList : BasePageList
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

            UrlPageSearch = "StructuralBenefitsSearch.aspx";
            UrlPageDetail = "StructuralBenefitsDetail.aspx";

            ProgramID = AppConstant.Program.StructuralBenefits;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(OrganizationUnitMetadata.ColumnNames.OrganizationUnitID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = OrganizationUnits;
        }

        private DataTable OrganizationUnits
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                OrganizationUnitQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (OrganizationUnitQuery)Session[SessionNameForQuery];
                else
                {
                    query = new OrganizationUnitQuery();
                    query.Select
                        (
                            query.OrganizationUnitID,
                            query.OrganizationUnitCode,
                            query.OrganizationUnitName
                        );
                    query.Where(query.IsActive == true, query.SROrganizationLevel == "1");
                    query.OrderBy(query.OrganizationUnitCode.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("OrganizationUnitID").ToString();

            var query = new StructuralBenefitsQuery("a");
            var positionQ = new PositionQuery("b");

            query.Select
                (
                    query,
                    positionQ.PositionName
                );
            query.InnerJoin(positionQ).On(query.PositionID == positionQ.PositionID);
            query.Where(query.OrganizationUnitID == id.ToInt());
            query.OrderBy(query.PositionID.Ascending, query.ValidFrom.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
