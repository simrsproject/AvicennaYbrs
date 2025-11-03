using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class FoodList : BasePageList
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (FormType == "sales")
            {
                UrlPageSearch = "FoodSearch.aspx?type=sales";
                UrlPageDetail = "FoodDetail.aspx?type=sales";
                ProgramID = AppConstant.Program.FoodCafetaria;
            }
            else
            {
                UrlPageSearch = "FoodSearch.aspx?";
                UrlPageDetail = "FoodDetail.aspx?";
                ProgramID = AppConstant.Program.Food;
            }

            this.WindowSearch.Height = 400;
            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            if (!IsPostBack)
            {
                grdList.Columns[5].Visible = !AppSession.Parameter.IsFoodSelectedByType && string.IsNullOrEmpty(FormType);
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", FormType);
            return script;
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
            string id = dataItem.GetDataKeyValue(FoodMetadata.ColumnNames.FoodID).ToString();
            string url = string.Format("FoodDetail.aspx?md={0}&id={1}&type={2}", mode, id, FormType);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = Foods;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //Load record
            var query = new FoodPackageQuery("a");
            var fQ = new FoodQuery("b");

            string foodId = e.DetailTableView.ParentItem.GetDataKeyValue("FoodID").ToString();
            query.Select
                (
                    query,
                    fQ.FoodName.As("FoodDetailName")
                );
            query.InnerJoin(fQ).On(query.FoodDetailID == fQ.FoodID);
            query.Where(query.FoodID == foodId);
            query.OrderBy(query.FoodDetailID.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable Foods
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                FoodQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (FoodQuery)Session[SessionNameForQuery];
                else
                {
                    query = new FoodQuery("a");
                    var std = new AppStandardReferenceItemQuery("b");
                    var std2 = new AppStandardReferenceItemQuery("c");
                    var std3 = new AppStandardReferenceItemQuery("d");
                    query.InnerJoin(std).On(query.SRFoodGroup1 == std.ItemID && std.StandardReferenceID == "FoodGroup1");
                    query.InnerJoin(std2).On(query.SRItemUnit == std2.ItemID && std2.StandardReferenceID == "ItemUnit");
                    query.LeftJoin(std3).On(query.SRFoodGroup2 == std3.ItemID && std3.StandardReferenceID == "FoodGroup2");
                    query.Select
                        (
                            query.FoodID,
                            query.FoodName,
                            query.Weight,
                            query.SRFoodGroup2,
                            @"<ISNULL(d.ItemName, 'General') AS FoodType>",
                            std.ItemName.As("FoodGroup1"),
                            std2.ItemName.As("ItemUnit"),
                            query.QtyPortion,
                            query.IsPackage,
                            query.IsActive
                        );
                    if (FormType == "sales")
                        query.Where(query.IsSalesAvailable == true);
                    else query.Where(query.IsSalesAvailable == false);

                    query.OrderBy(query.SRFoodGroup1.Ascending, query.FoodID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "FoodName", "FoodID");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}
