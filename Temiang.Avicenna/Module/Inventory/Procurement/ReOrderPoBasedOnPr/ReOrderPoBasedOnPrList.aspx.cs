using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using Telerik.Web.UI.PivotGrid.Queryable.Groups;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class ReOrderPoBasedOnPrList : BasePage
    {
        private AppAutoNumberLast _autoNumber;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ReOrderPoBasedOnPr;
            if (!IsPostBack)
            {
                ComboBox.PopulateWithServiceUnitForTransaction(cboFromServiceUnitID, TransactionCode.PurchaseRequest, false);
                ComboBox.PopulateWithServiceUnitForTransaction(cboToServiceUnitID, TransactionCode.PurchaseOrder, true);

                if (!string.IsNullOrEmpty(Request.QueryString["su"]))
                    cboToServiceUnitID.SelectedValue = Request.QueryString["su"];
                else
                    cboToServiceUnitID.Text = string.Empty;

                ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);

                if (!string.IsNullOrEmpty(Request.QueryString["it"]))
                    cboSRItemType.SelectedValue = Request.QueryString["it"];
                else
                    cboSRItemType.Text = string.Empty;

                txtDate.SelectedDate = DateTime.Now;
                txtDate2.SelectedDate = DateTime.Now;
                cboFromServiceUnitID.Text = string.Empty;
                btnFilterToServiceUnit.Visible = AppSession.Parameter.IsReOrderPoBasedOnPrWithSeparatePurchasingUnit;

                trSRItemCategory.Visible = AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory;
                StandardReference.InitializeIncludeSpace(cboSRItemCategory, AppEnum.StandardReference.ItemCategory);

                grdList.Columns.FindByUniqueName("cboFabricID").Visible = AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
            grdListPo.Rebind();
            grdListPqr.Rebind();
        }

        protected void cboToServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRItemType.Items.Clear();
            cboSRItemType.Text = string.Empty;
            ComboBox.PopulateWithServiceUnitForTransactionItemType(cboSRItemType, cboToServiceUnitID.SelectedValue, TransactionCode.PurchaseOrder);

            cboItemGroupID.Items.Clear();
            cboItemGroupID.Text = string.Empty;
        }

        protected void cboSRItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemGroupID.Items.Clear();
            cboItemGroupID.Text = string.Empty;
        }

        protected void cboItemGroupID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ItemGroupItemsRequested((RadComboBox)sender, e.Text, cboSRItemType.SelectedValue);
        }
        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemGroupItemDataBound(e);
        }

        private DataTable PopulateSupplier()
        {
            bool isFilterBySupplier = AppSession.Parameter.PurcOrderItemTypeRestrictionForItemSupplier.Contains(cboSRItemType.SelectedValue);

            if (ViewState["supplier"] != null)
                return ViewState["supplier"] as DataTable;

            var supp = new SupplierQuery("b");
            if (isFilterBySupplier)
            {
                var sui = new SupplierItemQuery("sui");
                supp.InnerJoin(sui).On(supp.SupplierID == sui.SupplierID);
                supp.Select(sui.ItemID);
            }
            else {
                supp.Select("<'' as ItemID>");
            }
            //supp.es.Top = 10;
            supp.Select(
                "<b.SupplierID + ';' + CASE WHEN b.IsPKP = 1 THEN 'true' ELSE 'false' END + ';' + CAST(b.TaxPercentage AS VARCHAR(MAX)) AS SupplierID>",
                supp.SupplierID.As("SuppliedIDFilter"),
                supp.SupplierName//,
                //supp.IsPKP,
                //supp.TaxPercentage
                );
            supp.Where(supp.IsActive == true);

            ViewState["supplier"] = supp.LoadDataTable();
            return ViewState["supplier"] as DataTable;
        }

        private DataTable ItemBalances()
        {
            var su = new ServiceUnit();
            var floc = su.GetMainLocationId(cboFromServiceUnitID.SelectedValue);
            var tloc = su.GetMainLocationId(cboToServiceUnitID.SelectedValue);

            if (string.IsNullOrEmpty(floc))
                floc = "xxx";
            if (string.IsNullOrEmpty(tloc))
                tloc = "xxx";

            var query = new ItemTransactionItemQuery("a");
            var qhd = new ItemTransactionQuery("b");
            var qfunit = new ServiceUnitQuery("c");
            var qtunit = new ServiceUnitQuery("d");
            var qfib = new ItemBalanceQuery("e");
            var qtib = new ItemBalanceQuery("f");
            var qi = new ItemQuery("g");

            if (AppSession.Parameter.IsPoBasedOnPr)
            {
                query.Select(
                qhd.IsInventoryItem,
                qhd.IsNonMasterOrder,
                qhd.IsAssets,
                qhd.TransactionNo,
                qhd.SRPurchaseCategorization,
                @"<ISNULL(b.SRItemCategory, '') AS SRItemCategory>",
                qfunit.ServiceUnitName.As("FromUnit"),
                qhd.Notes,
                query.SequenceNo,
                query.ItemID,
                    //qi.ItemName,
                @"<ISNULL(g.ItemName, a.Description) AS ItemName>",
                query.Quantity,
                query.QuantityFinishInBaseUnit,
                query.Price,
                query.IsDiscountInPercent.Coalesce("0").Cast(Dal.DynamicQuery.esCastType.Boolean),
                query.Discount1Percentage,
                query.Discount2Percentage,
                query.Discount,//mk
                    //@"<((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit) / a.ConversionFactor AS QtyOrder>",
                @"<ISNULL(e.Balance, 0) AS FromBalance>",
                @"<ISNULL(f.Balance, 0) AS ToBalance>",
                @"<a.SRItemUnit + '/' + CAST(a.ConversionFactor AS VARCHAR(MAX)) AS Unit>",
                @"<a.ConversionFactor AS ConversionFactor>",
                @"<a.SRItemUnit AS SRItemUnit>",
                @"<ISNULL((SELECT TOP 1 si.SupplierID FROM SupplierItem si WHERE si.ItemID = a.ItemID ORDER BY si.LastUpdateDateTime DESC), '') AS SupplierID>",
                @"<ISNULL(a.Specification, '') AS Specification>"
                );
                //query.InnerJoin(qhd).On(query.TransactionNo == qhd.TransactionNo &&
                //                        qhd.TransactionCode == TransactionCode.PurchaseRequest && qhd.IsApproved == true &&
                //                        qhd.SRItemType == cboSRItemType.SelectedValue && qhd.IsClosed == false && query.IsClosed == false);
                query.InnerJoin(qhd).On(query.TransactionNo == qhd.TransactionNo &&
                                        qhd.TransactionCode == TransactionCode.PurchaseRequest && qhd.IsApproved == true &&
                                        qhd.SRItemType == cboSRItemType.SelectedValue && query.IsClosed == false);
                query.InnerJoin(qfunit).On(qhd.FromServiceUnitID == qfunit.ServiceUnitID);
                query.InnerJoin(qtunit).On(qhd.ToServiceUnitID == qtunit.ServiceUnitID);
                query.LeftJoin(qfib).On(query.ItemID == qfib.ItemID && qfib.LocationID == floc);
                query.LeftJoin(qtib).On(query.ItemID == qtib.ItemID && qtib.LocationID == tloc);
                query.LeftJoin(qi).On(query.ItemID == qi.ItemID);

                if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
                {
                    query.Select(
                        @"<((a.Quantity * a.ConversionFactor) - (ISNULL((SELECT SUM((iti.Quantity * iti.ConversionFactor)) AS QtyFinished 
FROM ItemTransactionItem AS iti
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.IsVoid = 0 AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo), 0))) / a.ConversionFactor AS QtyOrder>");
                    query.Where(query.RequestQty.IsNotNull(), query.Quantity > 0);
                    query.Where(@"<((a.Quantity * a.ConversionFactor) - (ISNULL((SELECT SUM((iti.Quantity * iti.ConversionFactor)) AS QtyFinished 
FROM ItemTransactionItem AS iti
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.IsVoid = 0 AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo), 0))) > 0>");

                }
                else
                {
                    query.Select(
                        @"<((a.Quantity * a.ConversionFactor) - (ISNULL((SELECT SUM((iti.Quantity * iti.ConversionFactor)) AS QtyFinished 
FROM ItemTransactionItem AS iti
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.IsVoid = 0 AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo), 0)))/a.ConversionFactor AS QtyOrder>");
                    query.Where(@"<((a.Quantity * a.ConversionFactor) - (ISNULL((SELECT SUM((iti.Quantity * iti.ConversionFactor)) AS QtyFinished 
FROM ItemTransactionItem AS iti
INNER JOIN ItemTransaction AS it ON it.TransactionNo = iti.TransactionNo
WHERE it.IsVoid = 0 AND iti.ReferenceNo = a.TransactionNo AND iti.ReferenceSequenceNo = a.SequenceNo), 0))) > 0>");

                }
            }
            else
            {
                query.Select(
                qhd.IsInventoryItem,
                qhd.IsNonMasterOrder,
                qhd.IsAssets,
                qhd.TransactionNo,
                qhd.SRPurchaseCategorization,
                @"<ISNULL(b.SRItemCategory, '') AS SRItemCategory>",
                qfunit.ServiceUnitName.As("FromUnit"),
                qhd.Notes,
                query.SequenceNo,
                query.ItemID,
                    //qi.ItemName,
                @"<ISNULL(g.ItemName, a.Description) AS ItemName>",
                query.Quantity,
                query.QuantityFinishInBaseUnit,
                query.Price,
                query.IsDiscountInPercent.Coalesce("0").Cast(Dal.DynamicQuery.esCastType.Boolean),
                query.Discount1Percentage,
                query.Discount2Percentage,
                query.Discount,
                    //@"<((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit)/x.ConversionFactor AS QtyOrder>",
                @"<ISNULL(e.Balance, 0) AS FromBalance>",
                @"<ISNULL(f.Balance, 0) AS ToBalance>",
                @"<ISNULL(x.SRPurchaseUnit, a.SRItemUnit) + '/' + CAST(ISNULL(x.ConversionFactor, a.ConversionFactor) AS VARCHAR(MAX)) AS Unit>",
                @"<ISNULL(x.ConversionFactor, a.ConversionFactor) AS ConversionFactor>",
                @"<ISNULL(x.SRPurchaseUnit, a.SRItemUnit) AS SRItemUnit>",
                @"<ISNULL((SELECT TOP 1 si.SupplierID FROM SupplierItem si WHERE si.ItemID = a.ItemID ORDER BY si.LastUpdateDateTime DESC), '') AS SupplierID>",
                @"<ISNULL(a.Specification, '') AS Specification>"
                );
                //query.InnerJoin(qhd).On(query.TransactionNo == qhd.TransactionNo &&
                //                        qhd.TransactionCode == TransactionCode.PurchaseRequest && qhd.IsApproved == true &&
                //                        qhd.SRItemType == cboSRItemType.SelectedValue && qhd.IsClosed == false && query.IsClosed == false);
                query.InnerJoin(qhd).On(query.TransactionNo == qhd.TransactionNo &&
                                        qhd.TransactionCode == TransactionCode.PurchaseRequest && qhd.IsApproved == true &&
                                        qhd.SRItemType == cboSRItemType.SelectedValue && query.IsClosed == false);
                query.InnerJoin(qfunit).On(qhd.FromServiceUnitID == qfunit.ServiceUnitID);
                query.InnerJoin(qtunit).On(qhd.ToServiceUnitID == qtunit.ServiceUnitID);
                query.LeftJoin(qfib).On(query.ItemID == qfib.ItemID && qfib.LocationID == floc);
                query.LeftJoin(qtib).On(query.ItemID == qtib.ItemID && qtib.LocationID == tloc);
                query.LeftJoin(qi).On(query.ItemID == qi.ItemID);

                if (AppSession.Parameter.IsUsingApprovalPurchaseRequest)
                {
                    query.Select(
                        @"<((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit)/ISNULL(x.ConversionFactor, a.ConversionFactor) AS QtyOrder>");
                    query.Where(query.Or(query.RequestQty.IsNotNull(), query.RequestQty != 0));
                    query.Where("<((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit) > 0>");

                }
                else
                {
                    query.Select(
                        @"<((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit)/ISNULL(x.ConversionFactor, a.ConversionFactor) AS QtyOrder>");
                    query.Where("<((a.Quantity * a.ConversionFactor) - a.QuantityFinishInBaseUnit) > 0>");
                }
            }

            if (AppSession.Parameter.IsReOrderPoBasedOnPrWithSeparatePurchasingUnit)
                query.Where(qhd.ToServiceUnitID == cboToServiceUnitID.SelectedValue);

            query.Where(qhd.SRItemType == cboSRItemType.SelectedValue, qhd.Or(qhd.IsConsignment.IsNull(), qhd.IsConsignment == false));

            if (cboSRItemType.SelectedValue == ItemType.Medical)
            {
                var qipm = new ItemProductMedicQuery("x");
                query.LeftJoin(qipm).On(query.ItemID == qipm.ItemID);

                if (AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess)
                    query.Select(@"<ISNULL(x.FabricID, '') AS 'FabricID'>");
                else query.Select(@"<'' AS 'FabricID'>");
            }
            else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
            {
                var qipm = new ItemProductNonMedicQuery("x");
                query.LeftJoin(qipm).On(query.ItemID == qipm.ItemID);
                if (AppSession.Parameter.IsUsingFactoryInTheItemProcurementProcess)
                    query.Select(@"<ISNULL(x.FabricID, '') AS 'FabricID'>");
                else query.Select(@"<'' AS 'FabricID'>");
            }
            else
            {
                var qipm = new ItemKitchenQuery("x");
                query.LeftJoin(qipm).On(query.ItemID == qipm.ItemID);
                query.Select(@"<'' AS 'FabricID'>");
            }
            
            if (!txtDate.IsEmpty && !txtDate2.IsEmpty)
            {
                query.Where(qhd.TransactionDate >= txtDate.SelectedDate.Value, qhd.TransactionDate < txtDate2.SelectedDate.Value.AddDays(1));
            }
            if (!txtPlanningDate.IsEmpty && !txtPlanningDate2.IsEmpty)
            {
                query.Where(qhd.PlanningDate >= txtPlanningDate.SelectedDate.Value, qhd.PlanningDate < txtPlanningDate2.SelectedDate.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(cboFromServiceUnitID.SelectedValue))
            {
                query.Where(qhd.FromServiceUnitID == cboFromServiceUnitID.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
            {
                query.Where(qhd.ToServiceUnitID == cboToServiceUnitID.SelectedValue);
            }
            if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(cboSRItemCategory.SelectedValue))
            {
                query.Where(qhd.SRItemCategory == cboSRItemCategory.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboItemGroupID.SelectedValue))
            {
                query.Where(qi.ItemGroupID == cboItemGroupID.SelectedValue);
            }

            query.OrderBy(qi.ItemName.Ascending);

            var tbl = query.LoadDataTable();

            if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue) && tbl.Rows.Count > 0)
            {
                // filter pembelian terakhir ke supplier tsb
                var tb = (new SupplierItemCollection()).GetLastData(cboSupplierID.SelectedValue);
                var newtb = tbl.AsEnumerable().Where(x => (tb.AsEnumerable().Select(y => y["ItemID"].ToString())).Contains(x["ItemID"].ToString()));
                if (newtb.Count() > 0)
                {
                    tbl = newtb.CopyToDataTable();
                }
                else {
                    tbl.Clear();
                }
            }

            return tbl;
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdList_ItemPreRender;
        }

        private void grdList_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var suppId = dataItem["SupplierID"].Text;
            if (!string.IsNullOrEmpty(suppId) && suppId != "&nbsp;")
            {
                var supplier = (dataItem["ItemID"].FindControl("cboSupplierID") as RadComboBox);

                var itemID = dataItem.Cells[3].Text;

                DataView dv = PopulateSupplier().DefaultView;
                dv.RowFilter = "SuppliedIDFilter = '" + suppId + "'";
                bool isFilterBySupplier = AppSession.Parameter.PurcOrderItemTypeRestrictionForItemSupplier.Contains(cboSRItemType.SelectedValue);
                if (isFilterBySupplier) {
                    dv.RowFilter += " and ItemID = '" + itemID + "'";
                }

                supplier.DataSource = dv.ToTable();
                supplier.DataValueField = "SupplierID";
                supplier.DataTextField = "SupplierName";
                supplier.DataBind();
                ComboBox.SelectedValue(supplier, suppId);
            }

            var fabricId = dataItem["FabricID"].Text;
            if (!string.IsNullOrEmpty(fabricId) && fabricId != "&nbsp;")
            {
                var fabric = (dataItem["ItemID"].FindControl("cboFabricID") as RadComboBox);

                var itemID = dataItem.Cells[3].Text;

                DataView dv = PopulateFabric(itemID).DefaultView;
                dv.RowFilter = "FabricID = '" + fabricId + "'";
                dv.RowFilter += " and ItemID = '" + itemID + "'";

                fabric.DataSource = dv.ToTable();
                fabric.DataValueField = "FabricID";
                fabric.DataTextField = "FabricName";
                fabric.DataBind();
                ComboBox.SelectedValue(fabric, fabricId);
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ItemBalances();
        }

        protected void grdListPo_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdListPo.DataSource = ItemTransactions;
            }
        }

        protected void grdListPo_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            var itemreff = new ItemTransactionItemQuery("c");

            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.LeftJoin(itemreff).On(query.ReferenceNo == itemreff.TransactionNo &
                                        query.ReferenceSequenceNo == itemreff.SequenceNo);
            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    iq.ItemName,
                    query.Description,
                    query.SRItemUnit,
                    query.Quantity,
                    query.ConversionFactor,
                    @"<a.SRItemUnit + '/' + CAST(a.ConversionFactor AS VARCHAR(MAX)) AS SRItemUnitPo>",
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount,
                    itemreff.Quantity.As("QuantityReq"),
                    @"<c.SRItemUnit + '/' + CAST(c.ConversionFactor AS VARCHAR(MAX)) AS SRItemUnitReq>"
                );
            var dtb = query.LoadDataTable();

            e.DetailTableView.DataSource = dtb;
        }

        private DataTable ItemTransactions
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var sup = new SupplierQuery("b");
                var qryserviceunit = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");
                var poType = new AppStandardReferenceItemQuery("s");
                var user = new AppUserServiceUnitQuery("u");

                query.InnerJoin(poType).On(
                    query.SRPurchaseOrderType == poType.ItemID &&
                    poType.StandardReferenceID == AppEnum.StandardReference.PurchaseOrderType
                    );
                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.InnerJoin(qryserviceunit).On(qryserviceunit.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(itemtype).On(
                    itemtype.ItemID == query.SRItemType &&
                    itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                    );
                query.InnerJoin(user).On(query.FromServiceUnitID == user.ServiceUnitID &&
                                         user.UserID == AppSession.UserLogin.UserID);

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       sup.SupplierName,
                       query.ReferenceNo,
                       qryserviceunit.ServiceUnitName.As("FServiceUnitID"),
                       itemtype.ItemName,
                       query.IsApproved,
                       query.IsClosed,
                       query.Notes,
                       query.IsVoid,
                       poType.ItemName.As("PurchaseOrderType"),
                       query.FromServiceUnitID,
                       query.SRItemType
                       );

                query.Select(
                    "<'../PurchaseOrder/PurchaseOrderDetail.aspx?md=view&id=' + a.TransactionNo + '&pr=&rop=2&su=' + a.FromServiceUnitID +'&it=' + a.SRItemType + '&cons=0' as PoUrl>");

                query.Where(query.TransactionCode == TransactionCode.PurchaseOrder, query.IsBySystem == true);
                query.Where(query.FromServiceUnitID == cboToServiceUnitID.SelectedValue,
                            query.SRItemType == cboSRItemType.SelectedValue);

                if (!txtPurcOrderDate.IsEmpty)
                {
                    query.Where(query.TransactionDate == txtPurcOrderDate.SelectedDate.Value);
                }

                if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(cboSRItemCategory.SelectedValue))
                    query.Where(query.SRItemCategory == cboSRItemCategory.SelectedValue);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void grdListPqr_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdListPqr.DataSource = PriceQuoteRequests;
            }
        }

        protected void grdListPqr_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            var query = new ItemTransactionItemQuery("a");
            var iq = new ItemQuery("b");
            query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
            query.Where(query.TransactionNo == dataItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.ItemID.Ascending);

            query.Select
                (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    iq.ItemName,
                    query.Description,
                    query.SRItemUnit,
                    query.Quantity,
                    query.ConversionFactor,
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount
                );
            var dtb = query.LoadDataTable();

            e.DetailTableView.DataSource = dtb;
        }

        protected void grdListPqr_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "print")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.PriceQuoteRequest;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        private DataTable PriceQuoteRequests
        {
            get
            {
                var query = new ItemTransactionQuery("a");
                var sup = new SupplierQuery("b");
                var su = new ServiceUnitQuery("c");
                var itemtype = new AppStandardReferenceItemQuery("d");

                query.LeftJoin(sup).On(query.BusinessPartnerID == sup.SupplierID);
                query.InnerJoin(su).On(su.ServiceUnitID == query.FromServiceUnitID);
                query.LeftJoin(itemtype).On(
                    itemtype.ItemID == query.SRItemType &&
                    itemtype.StandardReferenceID == AppEnum.StandardReference.ItemType
                    );

                query.Select(
                       query.TransactionNo,
                       query.TransactionDate,
                       sup.SupplierName,
                       su.ServiceUnitName.As("FServiceUnitID"),
                       itemtype.ItemName,
                       query.IsApproved,
                       query.IsClosed,
                       query.Notes,
                       query.IsVoid,
                       query.FromServiceUnitID,
                       query.SRItemType
                       );
                query.Where(query.TransactionCode == TransactionCode.PriceQuoteRequest, query.IsBySystem == true);
                query.Where(query.FromServiceUnitID == cboToServiceUnitID.SelectedValue,
                            query.SRItemType == cboSRItemType.SelectedValue);

                if (!txtPqrDate.IsEmpty)
                {
                    query.Where(query.TransactionDate == txtPqrDate.SelectedDate.Value);
                }

                if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory && !string.IsNullOrEmpty(cboSRItemCategory.SelectedValue))
                    query.Where(query.SRItemCategory == cboSRItemCategory.SelectedValue);

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.OrderBy(query.TransactionNo.Descending);

                DataTable dtb = query.LoadDataTable();

                return dtb;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid)) return;

            if (eventArgument == "calc")
            {
                pnlInfo.Visible = false;
                lblInfo.Text = string.Empty;

                grdList.Rebind();
                grdListPo.Rebind();
            }
            else if (eventArgument == "generate")
            {
                var dateTransaction = DateTime.Now.Date;

                pnlInfo.Visible = false;
                bool isValid = true;
                if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Purchasing Unit required.";
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Item Type required.";
                    isValid = false;
                }

                if (!isValid) return;

                var isSeparationOfItemPurchaseCategorization = AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsSeparationOfItemPurchaseCategorization);
                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
                {
                    var transNos = string.Empty;

                    using (var trans = new esTransactionScope())
                    {
                        var coll = new ItemTransactionItemCollection();

                        var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked &&
                                                                                                         !string.IsNullOrEmpty(((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue))
                                                                                      .Select(dataItem => new
                                                                                      {
                                                                                          SupplierID = ((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue.Split(';')[0],
                                                                                          IsPkp = Convert.ToBoolean(((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue.Split(';')[1]),
                                                                                          TaxPercentage = Convert.ToDecimal(((RadComboBox)dataItem.FindControl("cboSupplierID")).SelectedValue.Split(';')[2]),
                                                                                          TransactionNo = dataItem["TransactionNo"].Text,
                                                                                          SequenceNo = dataItem["SequenceNo"].Text,
                                                                                          ItemID = dataItem["ItemID"].Text,
                                                                                          ItemName = dataItem["ItemName"].Text,
                                                                                          Quantity = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0),
                                                                                          SRItemUnit = dataItem["SRItemUnit"].Text,
                                                                                          ConversionFactor = Convert.ToDecimal(dataItem["ConversionFactor"].Text),
                                                                                          SRPurchaseCategorization = dataItem["SRPurchaseCategorization"].Text,
                                                                                          SRItemCategory = dataItem["SRItemCategory"].Text,
                                                                                          IsInventoryItem = ((CheckBox)dataItem.FindControl("chkIsInventoryItem")).Checked,
                                                                                          IsNonMasterOrder = ((CheckBox)dataItem.FindControl("chkIsNonMasterOrder")).Checked,
                                                                                          IsAssets = ((CheckBox)dataItem.FindControl("chkIsAssets")).Checked,
                                                                                          Price = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtPrice")).Value ?? 0),
                                                                                          IsDiscountInPercent = ((CheckBox)dataItem.FindControl("chkIsDiscountInPercent")).Checked,
                                                                                          Discount1Percentage = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtDiscount1Percentage")).Value ?? 0),
                                                                                          Discount2Percentage = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtDiscount2Percentage")).Value ?? 0),
                                                                                          Discount = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtDiscount")).Value ?? 0),
                                                                                          Specification = dataItem["Specification"].Text,
                                                                                          FabricID = ((RadComboBox)dataItem.FindControl("cboFabricID")).SelectedValue
                                                                                      });

                        if (items.Count() > 0)
                        {
                            if (AppSession.Parameter.IsPoBasedOnPr)
                            {
                                #region qty tidak di-sum
                                if (!isSeparationOfItemPurchaseCategorization || cboSRItemType.SelectedValue != ItemType.NonMedical)
                                {
                                    #region !isSeparationOfItemPurchaseCategorization || cboSRItemType.SelectedValue != ItemType.NonMedical
                                    if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                                    {
                                        foreach (var group in (from g in items
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.SRItemCategory,
                                                                   g.IsAssets
                                                               }
                                                               into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   SRItemCategory = grp.Key.SRItemCategory,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in items.Where(i => i.SupplierID == group.SupplierID))
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.ItemID = i.ItemID;
                                                c.ReferenceNo = i.TransactionNo;
                                                c.ReferenceSequenceNo = i.SequenceNo;
                                                c.Quantity = i.Quantity;
                                                c.SRItemUnit = i.SRItemUnit;
                                                c.ConversionFactor = i.ConversionFactor;
                                                c.Description = i.ItemName;
                                                c.Specification = i.Specification == "&nbsp;" ? "" : i.Specification;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (!group.IsNonMasterOrder)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }

                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = i.Price;
                                                    c.IsDiscountInPercent = i.IsDiscountInPercent;
                                                    c.Discount1Percentage = i.Discount1Percentage;
                                                    c.Discount2Percentage = i.Discount2Percentage;
                                                    c.Discount = i.Discount;
                                                }

                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;

                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = group.SRItemCategory;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }
                                    else
                                    {
                                        //qt coba test dl datanyanya dapat atau gak

                                        //var indexX = 0;
                                        //string SupplierID;
                                        //bool IsPkp;
                                        //decimal TaxPercentage;
                                        //bool IsInventoryItem;
                                        //bool IsNonMasterOrder;
                                        //bool IsAssets;
                                        //foreach (var xx in items)
                                        //{
                                        //    indexX++;
                                        //    SupplierID = xx.SupplierID;
                                        //    IsPkp = xx.IsPkp;
                                        //    TaxPercentage = xx.TaxPercentage;
                                        //    IsInventoryItem = xx.IsInventoryItem;
                                        //    IsNonMasterOrder = xx.IsNonMasterOrder;
                                        //    IsAssets = xx.IsAssets;
                                        //}




                                        foreach (var group in (from g in items
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.IsAssets
                                                               }
                                                                   into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in items.Where(i => i.SupplierID == group.SupplierID)) // apa harus ada pembelian dulu kak? ke supplier inseval itu? gak sih. ini seolah2 groupnya gak ada datanya
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.ItemID = i.ItemID;
                                                c.ReferenceNo = i.TransactionNo;
                                                c.ReferenceSequenceNo = i.SequenceNo;
                                                c.Quantity = i.Quantity;
                                                c.SRItemUnit = i.SRItemUnit;
                                                c.ConversionFactor = i.ConversionFactor;
                                                c.Description = i.ItemName;
                                                c.Specification = i.Specification == "&nbsp;" ? "" : i.Specification;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (!group.IsNonMasterOrder)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }

                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = i.Price;
                                                    c.IsDiscountInPercent = i.IsDiscountInPercent;
                                                    c.Discount1Percentage = i.Discount1Percentage;
                                                    c.Discount2Percentage = i.Discount2Percentage;
                                                    c.Discount = i.Discount;
                                                }

                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;

                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = string.Empty;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region isSeparationOfItemPurchaseCategorization && cboSRItemType.SelectedValue == ItemType.NonMedical

                                    if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                                    {
                                        foreach (var group in (from g in items
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.SRPurchaseCategorization,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.SRItemCategory,
                                                                   g.IsAssets
                                                               }
                                                               into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   SRPurchaseCategorization = grp.Key.SRPurchaseCategorization,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   SRItemCondition = grp.Key.SRItemCategory,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in items.Where(i => i.SupplierID == group.SupplierID && i.SRPurchaseCategorization == group.SRPurchaseCategorization))
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.ItemID = i.ItemID;
                                                c.ReferenceNo = i.TransactionNo;
                                                c.ReferenceSequenceNo = i.SequenceNo;
                                                c.Quantity = i.Quantity;
                                                c.SRItemUnit = i.SRItemUnit;
                                                c.ConversionFactor = i.ConversionFactor;
                                                c.Description = i.ItemName;
                                                c.Specification = i.Specification == "&nbsp;" ? "" : i.Specification;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (!group.IsNonMasterOrder)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }

                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = i.Price;
                                                    c.IsDiscountInPercent = i.IsDiscountInPercent;
                                                    c.Discount1Percentage = i.Discount1Percentage;
                                                    c.Discount2Percentage = i.Discount2Percentage;
                                                    c.Discount = i.Discount;
                                                }

                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;

                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;
                                            entity.SRPurchaseCategorization = group.SRPurchaseCategorization;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = group.SRItemCondition;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }
                                    else
                                    {
                                        foreach (var group in (from g in items
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.SRPurchaseCategorization,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.IsAssets
                                                               }
                                                               into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   SRPurchaseCategorization = grp.Key.SRPurchaseCategorization,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in items.Where(i => i.SupplierID == group.SupplierID && i.SRPurchaseCategorization == group.SRPurchaseCategorization))
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.ItemID = i.ItemID;
                                                c.ReferenceNo = i.TransactionNo;
                                                c.ReferenceSequenceNo = i.SequenceNo;
                                                c.Quantity = i.Quantity;
                                                c.SRItemUnit = i.SRItemUnit;
                                                c.ConversionFactor = i.ConversionFactor;
                                                c.Description = i.ItemName;
                                                c.Specification = i.Specification == "&nbsp;" ? "" : i.Specification;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (!group.IsNonMasterOrder)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.Price = (ipm.PriceInBaseUnit ?? 0) * (c.ConversionFactor ?? 0);
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }

                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = i.Price;
                                                    c.IsDiscountInPercent = i.IsDiscountInPercent;
                                                    c.Discount1Percentage = i.Discount1Percentage;
                                                    c.Discount2Percentage = i.Discount2Percentage;
                                                    c.Discount = i.Discount;
                                                }

                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;

                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;
                                            entity.SRPurchaseCategorization = group.SRPurchaseCategorization;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = string.Empty;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }


                                    #endregion
                                }

                                #endregion
                            }
                            else
                            {
                                #region qty di-sum

                                foreach (var item in items)
                                {
                                    var x = new ItemTransactionItem();
                                    if (x.LoadByPrimaryKey(item.TransactionNo, item.SequenceNo))
                                    {
                                        x.QuantityFinishInBaseUnit += (item.Quantity * x.ConversionFactor);
                                        if (x.QuantityFinishInBaseUnit >= x.Quantity * x.ConversionFactor)
                                            x.IsClosed = true;
                                        x.Save();
                                    }
                                }

                                if (!isSeparationOfItemPurchaseCategorization || cboSRItemType.SelectedValue != ItemType.NonMedical)
                                {
                                    #region !isSeparationOfItemPurchaseCategorization || cboSRItemType.SelectedValue != ItemType.NonMedical

                                    if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                                    {
                                        var itemGroups = items.GroupBy(c => new
                                        {
                                            c.SupplierID,
                                            c.IsPkp,
                                            c.TaxPercentage,
                                            c.IsInventoryItem,
                                            c.IsNonMasterOrder,
                                            c.ItemID,
                                            c.ItemName,
                                            c.SRItemCategory,
                                            c.IsAssets,
                                            c.FabricID
                                        }).Select(q => new
                                        {
                                            q.Key.SupplierID,
                                            q.Key.IsPkp,
                                            q.Key.TaxPercentage,
                                            q.Key.IsInventoryItem,
                                            q.Key.IsNonMasterOrder,
                                            q.Key.ItemID,
                                            q.Key.ItemName,
                                            q.Key.SRItemCategory,
                                            q.Key.IsAssets,
                                            q.Key.FabricID,
                                            Quantity = q.Sum(p => (p.Quantity))
                                        });

                                        foreach (var group in (from g in itemGroups
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.SRItemCategory,
                                                                   g.IsAssets
                                                               }
                                                                   into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   SRItemCategory = grp.Key.SRItemCategory,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in itemGroups.Where(i => i.SupplierID == group.SupplierID))
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.ItemID = i.ItemID;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.Quantity = i.Quantity;
                                                c.Description = i.ItemName;
                                                c.Specification = string.Empty;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (group.IsNonMasterOrder == false)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }
                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = 0;
                                                    c.IsDiscountInPercent = true;
                                                    c.Discount1Percentage = 0;
                                                    c.Discount2Percentage = 0;
                                                    c.Discount = 0;
                                                }
                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;
                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = group.SRItemCategory;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }
                                    else
                                    {
                                        var itemGroups = items.GroupBy(c => new
                                        {
                                            c.SupplierID,
                                            c.IsPkp,
                                            c.TaxPercentage,
                                            c.IsInventoryItem,
                                            c.IsNonMasterOrder,
                                            c.IsAssets,
                                            c.ItemID,
                                            c.ItemName,
                                            c.FabricID
                                        }).Select(q => new
                                        {
                                            q.Key.SupplierID,
                                            q.Key.IsPkp,
                                            q.Key.TaxPercentage,
                                            q.Key.IsInventoryItem,
                                            q.Key.IsNonMasterOrder,
                                            q.Key.IsAssets,
                                            q.Key.ItemID,
                                            q.Key.ItemName,
                                            q.Key.FabricID,
                                            Quantity = q.Sum(p => (p.Quantity))
                                        });

                                        foreach (var group in (from g in itemGroups
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.IsAssets
                                                               }
                                                                   into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in itemGroups.Where(i => i.SupplierID == group.SupplierID))
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.ItemID = i.ItemID;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.Quantity = i.Quantity;
                                                c.Description = i.ItemName;
                                                c.Specification = string.Empty;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (group.IsNonMasterOrder == false)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }
                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = 0;
                                                    c.IsDiscountInPercent = true;
                                                    c.Discount1Percentage = 0;
                                                    c.Discount2Percentage = 0;
                                                    c.Discount = 0;
                                                }
                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;
                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = string.Empty;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region isSeparationOfItemPurchaseCategorization && cboSRItemType.SelectedValue == ItemType.NonMedical
                                    if (AppSession.Parameter.IsPurchaseRequestBasedOnItemCategory)
                                    {
                                        var itemGroups = items.GroupBy(c => new
                                        {
                                            c.SupplierID,
                                            c.IsPkp,
                                            c.TaxPercentage,
                                            c.ItemID,
                                            c.ItemName,
                                            c.SRPurchaseCategorization,
                                            c.IsInventoryItem,
                                            c.IsNonMasterOrder,
                                            c.SRItemCategory,
                                            c.IsAssets,
                                            c.FabricID
                                        }).Select(q => new
                                        {
                                            q.Key.SupplierID,
                                            q.Key.IsPkp,
                                            q.Key.TaxPercentage,
                                            q.Key.ItemID,
                                            q.Key.ItemName,
                                            q.Key.SRPurchaseCategorization,
                                            q.Key.IsInventoryItem,
                                            q.Key.IsNonMasterOrder,
                                            q.Key.SRItemCategory,
                                            q.Key.IsAssets,
                                            q.Key.FabricID,
                                            Quantity = q.Sum(p => (p.Quantity))
                                        });

                                        foreach (var group in (from g in itemGroups
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.SRPurchaseCategorization,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.SRItemCategory,
                                                                   g.IsAssets
                                                               }
                                                                   into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   SRPurchaseCategorization = grp.Key.SRPurchaseCategorization,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   SRItemCategory = grp.Key.SRItemCategory,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in itemGroups.Where(i => i.SupplierID == group.SupplierID && i.SRPurchaseCategorization == group.SRPurchaseCategorization))
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.ItemID = i.ItemID;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.Quantity = i.Quantity;
                                                c.Description = i.ItemName;
                                                c.Specification = string.Empty;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (!group.IsNonMasterOrder)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }

                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = 0;
                                                    c.IsDiscountInPercent = true;
                                                    c.Discount1Percentage = 0;
                                                    c.Discount2Percentage = 0;
                                                    c.Discount = 0;
                                                }

                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;
                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;
                                            entity.SRPurchaseCategorization = group.SRPurchaseCategorization;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = group.SRItemCategory;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }
                                    else
                                    {
                                        var itemGroups = items.GroupBy(c => new
                                        {
                                            c.SupplierID,
                                            c.IsPkp,
                                            c.TaxPercentage,
                                            c.ItemID,
                                            c.ItemName,
                                            c.SRPurchaseCategorization,
                                            c.IsInventoryItem,
                                            c.IsNonMasterOrder,
                                            c.IsAssets,
                                            c.FabricID
                                        }).Select(q => new
                                        {
                                            q.Key.SupplierID,
                                            q.Key.IsPkp,
                                            q.Key.TaxPercentage,
                                            q.Key.ItemID,
                                            q.Key.ItemName,
                                            q.Key.SRPurchaseCategorization,
                                            q.Key.IsInventoryItem,
                                            q.Key.IsNonMasterOrder,
                                            q.Key.IsAssets,
                                            q.Key.FabricID,
                                            Quantity = q.Sum(p => (p.Quantity))
                                        });

                                        foreach (var group in (from g in itemGroups
                                                               group g by new
                                                               {
                                                                   g.SupplierID,
                                                                   g.IsPkp,
                                                                   g.TaxPercentage,
                                                                   g.SRPurchaseCategorization,
                                                                   g.IsInventoryItem,
                                                                   g.IsNonMasterOrder,
                                                                   g.IsAssets
                                                               }
                                                                   into grp
                                                               orderby grp.Key.SupplierID
                                                               select new
                                                               {
                                                                   SupplierID = grp.Key.SupplierID,
                                                                   IsPkp = grp.Key.IsPkp,
                                                                   TaxPercentage = grp.Key.TaxPercentage,
                                                                   SRPurchaseCategorization = grp.Key.SRPurchaseCategorization,
                                                                   IsInventoryItem = grp.Key.IsInventoryItem,
                                                                   IsNonMasterOrder = grp.Key.IsNonMasterOrder,
                                                                   IsAssets = grp.Key.IsAssets
                                                               }))
                                        {
                                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PurchaseOrder, su.DepartmentID);
                                            decimal chargeAmt = 0;

                                            foreach (var i in itemGroups.Where(i => i.SupplierID == group.SupplierID && i.SRPurchaseCategorization == group.SRPurchaseCategorization))
                                            {
                                                var c = coll.AddNew();

                                                #region detail

                                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                                c.ItemID = i.ItemID;
                                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                                c.Quantity = i.Quantity;
                                                c.Description = i.ItemName;
                                                c.Specification = string.Empty;
                                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                                if (!group.IsNonMasterOrder)
                                                {
                                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                                    {
                                                        var ipm = new ItemProductMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                                    {
                                                        var ipm = new ItemProductNonMedic();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }
                                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                                    {
                                                        var ipm = new ItemKitchen();
                                                        ipm.LoadByPrimaryKey(c.ItemID);
                                                        c.SRItemUnit = ipm.SRPurchaseUnit;
                                                        c.ConversionFactor = ipm.ConversionFactor;
                                                        c.Price = ipm.PriceInPurchaseUnit ?? 0;
                                                        c.IsDiscountInPercent = true;
                                                        c.Discount1Percentage = ipm.PurchaseDiscount1 ?? 0;
                                                        c.Discount2Percentage = ipm.PurchaseDiscount2 ?? 0;
                                                    }

                                                    var suppItem = new SupplierItem();
                                                    if (suppItem.LoadByPrimaryKey(i.SupplierID, c.ItemID))
                                                    {
                                                        if (c.Price != 0)
                                                        {
                                                            c.Price = ((suppItem.PriceInPurchaseUnit ?? 0) /
                                                                       (suppItem.ConversionFactor ?? 0)) * c.ConversionFactor;
                                                            c.IsDiscountInPercent = true;
                                                            c.Discount1Percentage = suppItem.PurchaseDiscount1 ?? 0;
                                                            c.Discount2Percentage = suppItem.PurchaseDiscount2 ?? 0;
                                                        }
                                                    }

                                                    decimal disc1 = Convert.ToDecimal(c.Price * c.Discount1Percentage / 100);
                                                    decimal disc2 = Convert.ToDecimal((c.Price - disc1) * c.Discount2Percentage / 100);
                                                    c.Discount = disc1 + disc2;
                                                }
                                                else
                                                {
                                                    c.Price = 0;
                                                    c.IsDiscountInPercent = true;
                                                    c.Discount1Percentage = 0;
                                                    c.Discount2Percentage = 0;
                                                    c.Discount = 0;
                                                }

                                                c.PriceInCurrency = c.Price;
                                                c.DiscountInCurrency = c.Discount;
                                                c.IsBonusItem = false;
                                                c.IsTaxable = true;
                                                c.IsClosed = false;
                                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                                c.LastUpdateDateTime = DateTime.Now;

                                                #endregion

                                                chargeAmt += (c.Quantity ?? 0) * ((c.Price ?? 0) - (c.Discount ?? 0));
                                            }

                                            var entity = new ItemTransaction();

                                            #region header

                                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                                            entity.TransactionCode = TransactionCode.PurchaseOrder;
                                            entity.TransactionDate = dateTransaction;
                                            entity.BusinessPartnerID = group.SupplierID;
                                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                                            entity.CurrencyRate = 1;
                                            entity.ReferenceNo = string.Empty;
                                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                                            entity.ToServiceUnitID = cboFromServiceUnitID.SelectedValue;
                                            entity.ServiceUnitCostID = cboFromServiceUnitID.SelectedValue;
                                            entity.TermID = AppSession.Parameter.DefaultTerm;
                                            entity.SRPurchaseOrderType = AppSession.Parameter.DefaultPurchaseOrderType;
                                            entity.SRItemType = cboSRItemType.SelectedValue;
                                            entity.DiscountAmount = 0;
                                            entity.ChargesAmount = chargeAmt;
                                            entity.DownPaymentAmount = 0;
                                            entity.SRDownPaymentType = AppSession.Parameter.DefaultDownPaymentType;
                                            entity.SRPurchaseCategorization = group.SRPurchaseCategorization;

                                            entity.IsTaxable = Convert.ToInt16(group.IsPkp ? 1 : AppSession.Parameter.SupplierNonPkpTaxStatusDefault.ToInt());
                                            entity.TaxPercentage = group.IsPkp ? group.TaxPercentage : 0;
                                            entity.AmountTaxed = chargeAmt;
                                            entity.TaxAmount = chargeAmt * entity.TaxPercentage / 100;
                                            if (AppSession.Parameter.IsPORoundingDownZeroDigit)
                                            {
                                                entity.ChargesAmount = System.Convert.ToInt64(entity.ChargesAmount);
                                                entity.AmountTaxed = System.Convert.ToInt64(entity.AmountTaxed);
                                                entity.TaxAmount = System.Convert.ToInt64(entity.TaxAmount);
                                            }

                                            entity.Notes = string.Empty;
                                            entity.IsNonMasterOrder = group.IsNonMasterOrder;
                                            entity.LeadTime = string.Empty;
                                            entity.ContractNo = string.Empty;
                                            entity.IsBySystem = true;
                                            entity.IsInventoryItem = group.IsInventoryItem;
                                            entity.IsAssets = group.IsAssets;
                                            entity.SRItemCategory = string.Empty;

                                            var supp = new Supplier();
                                            supp.LoadByPrimaryKey(group.SupplierID);

                                            entity.TermOfPayment = supp.TermOfPayment;
                                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                            entity.LastUpdateDateTime = DateTime.Now;

                                            #endregion

                                            _autoNumber.Save();
                                            entity.Save();
                                            coll.Save();

                                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }

                            trans.Complete();
                        }
                        else
                        {
                            pnlInfo.Visible = true;
                            lblInfo.Text = "No record to be process.";
                            return;
                        }
                        
                    }

                    pnlInfo.Visible = true;
                    if (transNos == string.Empty)
                        lblInfo.Text = "Generate Purchase Order failed.";
                    else
                        lblInfo.Text = "Generate Purchase Order Succeed with No. : " + transNos;

                    grdList.Rebind();
                    grdListPo.Rebind();
                }
            }
            else if (eventArgument == "request")
            {
                var dateTransaction = DateTime.Now.Date;

                pnlInfo.Visible = false;
                bool isValid = true;
                if (string.IsNullOrEmpty(cboToServiceUnitID.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Purchasing Unit required.";
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(cboSRItemType.SelectedValue))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Item Type required.";
                    isValid = false;
                }

                if (!isValid) return;

                var su = new ServiceUnit();
                if (su.LoadByPrimaryKey(cboToServiceUnitID.SelectedValue))
                {
                    var transNos = string.Empty;

                    using (var trans = new esTransactionScope())
                    {
                        var coll = new ItemTransactionItemCollection();

                        var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                                                                                      .Select(dataItem => new
                                                                                      {
                                                                                          SupplierID = cboSupplierID.SelectedValue,
                                                                                          ItemID = dataItem["ItemID"].Text,
                                                                                          ItemName = dataItem["ItemName"].Text,
                                                                                          SRItemUnit = dataItem["SRItemUnit"].Text,
                                                                                          Quantity = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQty")).Value ?? 0),
                                                                                          ConversionFactor = Convert.ToDecimal(dataItem["ConversionFactor"].Text),
                                                                                          FabricID = ((RadComboBox)dataItem.FindControl("cboFabricID")).SelectedValue
                                                                                      });

                        #region qty di-sum
                        var itemGroups = items.GroupBy(c => new
                        {
                            c.SupplierID,
                            c.ItemID,
                            c.ItemName,
                            c.SRItemUnit,
                            c.FabricID
                        }).Select(q => new
                        {
                            q.Key.SupplierID,
                            q.Key.ItemID,
                            q.Key.ItemName,
                            q.Key.SRItemUnit,
                            q.Key.FabricID,
                            Quantity = q.Sum(p => (p.Quantity * p.ConversionFactor))
                        });

                        foreach (var group in (from g in itemGroups
                                               group g by new
                                               {
                                                   g.SupplierID
                                               }
                                                   into grp
                                                   orderby grp.Key.SupplierID
                                                   select new
                                                   {
                                                       SupplierID = grp.Key.SupplierID
                                                   }))
                        {
                            _autoNumber = Helper.GetNewAutoNumber(dateTransaction, TransactionCode.PriceQuoteRequest, su.DepartmentID);

                            foreach (var i in itemGroups.Where(i => i.SupplierID == group.SupplierID))
                            {
                                var c = coll.AddNew();

                                #region detail

                                c.TransactionNo = _autoNumber.LastCompleteNumber;
                                c.ItemID = i.ItemID;
                                c.SequenceNo = string.Format("{0:000}", coll.Count + 1);
                                c.Quantity = i.Quantity;
                                c.ConversionFactor = 1;

                                //var item = new Item();
                                //item.LoadByPrimaryKey(c.ItemID);
                                //c.Description = item.ItemName;
                                c.Description = i.ItemName;
                                c.Specification = string.Empty;
                                c.FabricID = i.FabricID == "&nbsp;" ? "" : i.FabricID;

                                if (!string.IsNullOrEmpty(i.ItemID))
                                {
                                    if (cboSRItemType.SelectedValue == ItemType.Medical)
                                    {
                                        var ipm = new ItemProductMedic();
                                        ipm.LoadByPrimaryKey(c.ItemID);
                                        c.SRItemUnit = ipm.SRItemUnit;
                                    }
                                    else if (cboSRItemType.SelectedValue == ItemType.NonMedical)
                                    {
                                        var ipm = new ItemProductNonMedic();
                                        ipm.LoadByPrimaryKey(c.ItemID);
                                        c.SRItemUnit = ipm.SRItemUnit;
                                    }
                                    else if (cboSRItemType.SelectedValue == ItemType.Kitchen)
                                    {
                                        var ipm = new ItemKitchen();
                                        ipm.LoadByPrimaryKey(c.ItemID);
                                        c.SRItemUnit = ipm.SRItemUnit;
                                    }
                                }
                                else
                                {
                                    c.SRItemUnit = i.SRItemUnit;
                                }
                                c.Price = 0;
                                c.IsDiscountInPercent = true;
                                c.Discount1Percentage = 0;
                                c.Discount2Percentage = 0;
                                c.PriceInCurrency = c.Price;
                                c.Discount = 0;
                                c.DiscountInCurrency = c.Discount;
                                c.IsBonusItem = false;
                                c.IsClosed = false;
                                c.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                c.LastUpdateDateTime = DateTime.Now;

                                #endregion

                            }

                            var entity = new ItemTransaction();

                            #region header

                            entity.TransactionNo = _autoNumber.LastCompleteNumber;
                            entity.TransactionCode = TransactionCode.PriceQuoteRequest;
                            entity.TransactionDate = dateTransaction;
                            entity.BusinessPartnerID = group.SupplierID;
                            entity.CurrencyID = AppSession.Parameter.CurrencyRupiahID;
                            entity.CurrencyRate = 1;
                            entity.ReferenceNo = string.Empty;
                            entity.FromServiceUnitID = cboToServiceUnitID.SelectedValue;
                            entity.ToServiceUnitID = string.Empty;
                            entity.ServiceUnitCostID = string.Empty;
                            entity.TermID = AppSession.Parameter.DefaultTerm;
                            entity.SRPurchaseOrderType = string.Empty;
                            entity.SRItemType = cboSRItemType.SelectedValue;
                            entity.DiscountAmount = 0;
                            entity.ChargesAmount = 0;
                            entity.DownPaymentAmount = 0;
                            entity.SRDownPaymentType = string.Empty;

                            entity.IsTaxable = 1;
                            entity.TaxPercentage = 0;
                            entity.TaxAmount = 0;

                            entity.Notes = string.Empty;
                            entity.IsNonMasterOrder = false;
                            entity.LeadTime = string.Empty;
                            entity.ContractNo = string.Empty;
                            entity.IsBySystem = true;
                            entity.IsInventoryItem = true;

                            var supp = new Supplier();
                            supp.LoadByPrimaryKey(group.SupplierID);

                            entity.TermOfPayment = supp.TermOfPayment;
                            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            entity.LastUpdateDateTime = DateTime.Now;

                            #endregion

                            _autoNumber.Save();
                            entity.Save();
                            coll.Save();

                            transNos = string.IsNullOrEmpty(transNos) ? entity.TransactionNo : transNos + ", " + entity.TransactionNo;
                        }
                        #endregion

                        trans.Complete();

                        pnlInfo.Visible = true;
                        if (transNos == string.Empty) 
                            lblInfo.Text = "Generate Price Quote Request failed.";
                        else
                            lblInfo.Text = "Generate Price Quote Request Succeed with No. : " + transNos;

                        grdList.Rebind();
                        grdListPqr.Rebind();
                    }
                }
            }
        }

        protected void cboSupplierID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SupplierName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SupplierID"].ToString();
        }

        protected void cboSupplierID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //DataTable tbl = PopulateItem(e.Text);
            var itemID = ((o as RadComboBox).NamingContainer as GridDataItem).Cells[3].Text;
            (o as RadComboBox).DataSource = PopulateSupplier(e.Text, itemID);
            (o as RadComboBox).DataBind();
        }

        private DataTable PopulateSupplier(string supplierName, string itemID)
        {
            if (!string.IsNullOrEmpty(supplierName))
            {
                DataView dv = PopulateSupplier().DefaultView;
                dv.RowFilter = "SupplierName LIKE '%" + supplierName + "%'";

                bool isFilterBySupplier = AppSession.Parameter.PurcOrderItemTypeRestrictionForItemSupplier.Contains(cboSRItemType.SelectedValue);
                if (isFilterBySupplier)
                {
                    dv.RowFilter += " and ItemID = '" + itemID + "'";
                }

                return dv.ToTable();
            }
            else
            {
                var dtb = new DataTable();

                dtb.Columns.Add(new DataColumn("SupplierID"));
                dtb.Columns.Add(new DataColumn("SupplierName"));

                return dtb;
            }
        }

        protected void cboSupplier_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SupplierQuery("a");
            query.Where(
                query.Or(query.SupplierID == e.Text,
                query.SupplierName.Like(searchTextContain)),
                query.IsActive == true
                );
            query.Select(query.SupplierID, query.SupplierName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSupplierID.DataSource = dtb;
            cboSupplierID.DataBind();
        }

        protected void cboFabricID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["FabricName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["FabricID"].ToString();
        }

        protected void cboFabricID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            //DataTable tbl = PopulateItem(e.Text);
            var itemID = ((o as RadComboBox).NamingContainer as GridDataItem).Cells[3].Text;
            (o as RadComboBox).DataSource = PopulateFabric(e.Text, itemID);
            (o as RadComboBox).DataBind();
        }

        private DataTable PopulateFabric(string fabricName, string itemID)
        {
            if (!string.IsNullOrEmpty(fabricName))
            {
                DataView dv = PopulateFabric(itemID).DefaultView;
                dv.RowFilter = "FabricName LIKE '%" + fabricName + "%'";
                dv.RowFilter += " and ItemID = '" + itemID + "'";

                return dv.ToTable();
            }
            else
            {
                var dtb = new DataTable();

                dtb.Columns.Add(new DataColumn("FabricID"));
                dtb.Columns.Add(new DataColumn("FabricName"));

                return dtb;
            }
        }

        private DataTable PopulateFabric(string itemId)
        {
            if (ViewState["fabric"] != null)
                return ViewState["fabric"] as DataTable;

            var fq = new FabricQuery("f");
            fq.Select(@"<'' AS ItemID>", fq.FabricID, fq.FabricName);

            DataTable dtb = fq.LoadDataTable();

            var imq = new VwItemProductFabricQuery("a");
            fq = new FabricQuery("f");
            imq.InnerJoin(fq).On(fq.FabricID == imq.FabricID);
            imq.Select(imq.ItemID, fq.FabricID, fq.FabricName);
            imq.Where(imq.ItemID == itemId);

            dtb.Merge(imq.LoadDataTable());

            ViewState["fabric"] = dtb;
            return ViewState["fabric"] as DataTable;
        }
    }
}
