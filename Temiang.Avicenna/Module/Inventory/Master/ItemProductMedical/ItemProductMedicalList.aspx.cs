using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductMedicalList : BasePageList
    {
        private string FormType
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["type"]))
                    return string.Empty;
                return Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = FormType == "direct"
                            ? AppConstant.Program.ItemProductMedicalDirectPurchase
                            : AppConstant.Program.ItemProductMedical;

            UrlPageSearch = "ItemProductMedicalSearch.aspx?type=" + FormType;
            UrlPageDetail = "ItemProductMedicalDetail.aspx?type=" + FormType;

            WindowSearch.Height = 400;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;

            UrlPageDetailImport = "openWinImport('" + ProgramID + "');";
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
            string id = dataItem.GetDataKeyValue(ItemMetadata.ColumnNames.ItemID).ToString();
            Page.Response.Redirect("ItemProductMedicalDetail.aspx?md=" + mode + "&id=" + id + "&type=" + FormType, true);
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
                    var qs = new ItemProductMedicQuery("b");
                    var group = new ItemGroupQuery("c");
                    var pa = new ProductAccountQuery("d");

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    
                    query.Where(query.SRItemType == BusinessObject.Reference.ItemType.Medical);
                    if (FormType == "direct")
                        query.Where(qs.IsDirectPurchase == true);
                    else
                        query.Where(qs.IsDirectPurchase == false);

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
                            qs.IsAntibiotic,
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
                    var fileName = "MEDICAL_" + DateTime.Now.Date.ToString("yyyyMMdd");

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
                @"<'' AS ProductTypeID>",
                @"<'' AS DrugLabelID>",
                @"<'A' AS ABC_Class>",
                @"<'' AS GroupTherapyID>",
                @"<'' AS TherapyID>",
                @"<'' AS ProductAccountID>",

                @"<'' AS ItemUnitID>",
                @"<'' AS PurchaseUnitID>",

                @"<1 AS ConversionFactor>",
                @"<'' AS DosageUnitID>",
                @"<0 AS Dosage>",

                @"<0 AS PurchasePriceInBaseUnit>",
                @"<0 AS PurchaseDiscountInPercentage>",

                @"<1 AS IsInventoryItem>",
                @"<1 AS IsControlExpired>",
                @"<0 AS IsProductionItem>",
                @"<1 AS IsSalesAvailable>",
                @"<0 AS IsConsignment>",
                @"<0 AS IsActualDeduct>",

                @"<0 AS IsFormulary>",
                @"<0 AS IsNationalFormulary>",
                @"<0 AS IsGeneric>",
                @"<0 AS IsNonGeneric>",
                @"<0 AS IsNonGenericLimited>",
                @"<0 AS IsPrecursor>",

                @"<0 AS IsOTC>",
                @"<0 AS IsHardDrug>",
                @"<0 AS IsHAM>",
                @"<0 AS IsOOT>",
                @"<0 AS IsNarcotic>",
                @"<0 AS IsPsychotropic>",
                @"<0 AS IsMorphine>",
                @"<0 AS IsPethidine>",
                @"<0 AS IsLASA>",
                @"<0 AS IsTraditionalMedicine>",
                @"<0 AS IsSupplement>",
                @"<0 AS IsAntibiotic>",
                @"<0 AS IsMedication>",

                @"<0 AS IsActive>"
                );

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
    }
}