using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class SupplierList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "SupplierSearch.aspx";
            UrlPageDetail = "SupplierDetail.aspx";

            WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.SUPPLIER;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(SupplierMetadata.ColumnNames.SupplierID).ToString();
            Page.Response.Redirect("SupplierDetail.aspx?md=" + mode + "&id=" + id, true);
        }	

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Suppliers;
        }

        private DataTable Suppliers
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                SupplierQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (SupplierQuery)Session[SessionNameForQuery];
                else
                {
                    query = new SupplierQuery("a");

                    var asriq = new AppStandardReferenceItemQuery("b");
                    query.LeftJoin(asriq).On(query.SRSupplierType == asriq.ItemID &&
                                              asriq.StandardReferenceID ==
                                              AppEnum.StandardReference.SupplierType.ToString());

                    query.Select
                        (
                            query.SupplierID,
                            query.SupplierName,
                            asriq.ItemName.As("SRSupplierType"),
                            query.ContactPerson,
                            query.StreetName,
                            query.City,
                            query.PhoneNo,
                            query.Email,
                            query.IsActive
                        );
                    query.OrderBy(query.SupplierID.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "SupplierName", "SupplierID");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}