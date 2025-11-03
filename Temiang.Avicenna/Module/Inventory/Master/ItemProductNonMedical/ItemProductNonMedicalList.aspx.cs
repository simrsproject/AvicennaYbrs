using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductNonMedicalList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "ItemProductNonMedicalSearch.aspx";
            UrlPageDetail = "ItemProductNonMedicalDetail.aspx";

            ProgramID = AppConstant.Program.ItemProductNonMedical;

            WindowSearch.Height = 400;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            UrlPageDetailImport = "openWinImport('" + ProgramID + "');";
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
            string id = dataItem.GetDataKeyValue(ItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("ItemProductNonMedicalDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemProducts;
        }

        private DataTable ItemProducts
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ItemQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ItemQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ItemQuery("a");
                    var qs = new ItemProductNonMedicQuery("b");
                    var group = new ItemGroupQuery("c");
                    var pa = new ProductAccountQuery("d");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                    query.Where(query.SRItemType == BusinessObject.Reference.ItemType.NonMedical);
                    query.LeftJoin(qs).On(query.ItemID == qs.ItemID);
                    query.LeftJoin(group).On(query.ItemGroupID == group.ItemGroupID);
                    query.LeftJoin(pa).On(pa.ProductAccountID == query.ProductAccountID);
                    query.Select
                        (
                            query.ItemID,
                            group.ItemGroupName,
                            query.ItemName,
                            query.IsActive,
                            query.Notes,
                            qs.SRItemUnit,
                            qs.SRPurchaseUnit,
                            qs.ConversionFactor,
                            pa.ProductAccountName,
                            qs.IsInventoryItem,
                            qs.IsControlExpired
                        );
                    query.OrderBy(query.ItemID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "ItemName", "ItemID");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        public override void OnMenuExportToExcelClick(ValidateArgs args)
        {
            try
            {
                var table = GetDataGridDataTable();
                if (table.Rows.Count > 0)
                {
                    var fileName = "NONMEDICAL_" + DateTime.Now.Date.ToString("yyyyMMdd");

                    Common.CreateExcelFile.CreateExcelDocument(table, fileName.Replace('/', '_').Replace(".", "_").Replace(" ", "_") + AppSession.Parameter.ExcelFileExtension, this.Response);
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw new Exception(error);
            }

            args.IsCancel = true;
        }

        private DataTable GetDataGridDataTable()
        {
            var query = new ItemQuery("a");
            query.es.Top = 1;
            query.Select
                (
                @"<'' AS ItemID>",
                @"<'' AS ItemName>",
                @"<'IG' AS ItemGroupID>",
                @"<'A' AS ABC_Class>",
                @"<'' AS ProductAccountID>",

                @"<'' AS ItemUnitID>",
                @"<'' AS PurchaseUnitID>",

                @"<1 AS ConversionFactor>",
                
                @"<0 AS PurchasePriceInBaseUnit>",
                @"<0 AS PurchaseDiscountInPercentage>",

                @"<1 AS IsInventoryItem>",
                @"<0 AS IsControlExpired>",
                @"<0 AS IsProductionItem>",
                @"<0 AS IsSalesAvailable>",
                @"<0 AS IsConsignment>",

                @"<0 AS IsActive>"
                );

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
    }
}