using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemExtraPackage : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ServiceUnitTransaction;

            if (!IsPostBack)
            {
                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["item"]);

                Title = "Item Extra : " + item.ItemName + " [" + Request.QueryString["item"] + "]";
            }
        }

        protected void grdItemPackage_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
                var query = new ItemPackageQuery("a");
                var item = new ItemQuery("b");

                query.Select(
                    "<CAST(0 as BIT) as IsSelected>",
                    query.DetailItemID,
                    item.ItemName.As("refToItem_ItemName")
                    );
                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);
                query.Where(
                    query.ItemID == Request.QueryString["item"],
                    query.IsExtraItem == true
                    );

                var dtb = query.LoadDataTable();

                if ((Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]] as TransChargesItemCollection).Count != 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        row["IsSelected"] = (Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]] as TransChargesItemCollection).Any(s => s.ItemID == row["DetailItemID"].ToString() &&
                                                                                                                     (s.IsSelectedExtraItem ?? false));
                    }
                }
                grdItemPackage.DataSource = dtb;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdItemPackage.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override bool OnButtonOkClicked()
        {
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(Request.QueryString["grr"]);

            var transChargesItem = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]];
            var transChargesItemComps = (TransChargesItemCompCollection)Session["collTransChargesItemComp" + Request.UserHostName + Request.QueryString["pageId"]];
            var transChargesItemConsumptions = (TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]];
            var parent = transChargesItem.Single(d => d.SequenceNo == Request.QueryString["seq"]);
            var seqNo = (transChargesItem.Where(t => t.ParentNo == Request.QueryString["seq"])
                                         .OrderByDescending(t => t.SequenceNo)
                                         .Select(t => t.SequenceNo.Substring(3, 3))).Take(1).SingleOrDefault();

            foreach (GridDataItem dataItem in grdItemPackage.MasterTableView.Items)
            {
                var pac = new ItemPackage();
                pac.Query.Where(
                    pac.Query.ItemID == Request.QueryString["item"] &&
                    pac.Query.DetailItemID == dataItem.GetDataKeyValue("DetailItemID").ToString() &&
                    pac.Query.IsExtraItem == true
                    );
                pac.Query.Load();

                var ent = transChargesItem.Where(s => s.ItemID == dataItem.GetDataKeyValue("DetailItemID").ToString()).SingleOrDefault();
                if (ent == null)
                {
                    ent = transChargesItem.AddNew();

                    var sequenceNo = (transChargesItem.Where(c => c.ParentNo == Request.QueryString["seq"]).
                        OrderByDescending(c => c.SequenceNo)
                        .Select(c => c.SequenceNo)).Take(1);
                    ent.SequenceNo = string.Format("{0:000000}", int.Parse(sequenceNo.Single()) + 1);

                    ent.ParentNo = Request.QueryString["seq"];
                    ent.ReferenceNo = string.Empty;
                    ent.ReferenceSequenceNo = string.Empty;
                    ent.ItemID = dataItem.GetDataKeyValue("DetailItemID").ToString();

                    var i = new Item();
                    i.LoadByPrimaryKey(ent.ItemID);
                    ent.ItemName = i.ItemName;
                    ent.ChargeClassID = parent.ChargeClassID;
                    ent.ParamedicID = string.Empty;
                    ent.IsAdminCalculation = false;
                    ent.IsVariable = false;
                    ent.IsCito = false;
                    ent.ChargeQuantity = parent.ChargeQuantity*pac.Quantity;

                    switch (i.SRItemType)
                    {
                        case ItemType.Medical:
                            ent.StockQuantity = ent.ChargeQuantity;

                            var ipm = new ItemProductMedic();
                            ipm.LoadByPrimaryKey(ent.ItemID);

                            ent.CostPrice = ipm.CostPrice ?? 0;
                            break;
                        case ItemType.NonMedical:
                            ent.StockQuantity = ent.ChargeQuantity;

                            var ipn = new ItemProductNonMedic();
                            ipn.LoadByPrimaryKey(ent.ItemID);

                            ent.CostPrice = ipn.CostPrice ?? 0;
                            break;
                        default:
                            ent.StockQuantity = 0;
                            ent.CostPrice = 0;
                            break;
                    }

                    ent.SRItemUnit = pac.SRItemUnit;
                    ent.DiscountAmount = 0;
                    ent.CitoAmount = 0;
                    ent.RoundingAmount = 0;
                    ent.SRDiscountReason = string.Empty;
                    ent.IsAssetUtilization = false;
                    ent.AssetID = string.Empty;
                    ent.IsBillProceed = false;
                    ent.IsOrderRealization = false;
                    ent.IsPackage = false;
                    ent.IsVoid = false;
                    ent.Notes = string.Empty;
                    ent.IsItemTypeService = i.SRItemType != ItemType.Medical && i.SRItemType != ItemType.NonMedical;
                    ent.ToServiceUnitID = pac.ServiceUnitID;

                    decimal pricePackage = 0;

                    switch (i.SRItemType)
                    {
                        case ItemType.Medical:
                        case ItemType.NonMedical:
                        case ItemType.Kitchen:
                            var entity = new TransChargesItem();
                            
                                var tariffCompPack = new ItemPackageTariffComponentCollection();
                                tariffCompPack.Query.Where(tariffCompPack.Query.ItemID == pac.ItemID, tariffCompPack.Query.DetailItemID == pac.DetailItemID);
                                tariffCompPack.LoadAll();
                                if (tariffCompPack.Count > 0)
                                {
                                    var comp = tariffCompPack.First();                                   
                                    pricePackage = comp.Price ?? 0;                                    
                                }                            
                            break;
                        case ItemType.Diagnostic:
                        case ItemType.Laboratory:
                        case ItemType.Package:
                        case ItemType.Radiology:
                        case ItemType.Service:
                            // component
                            var comps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date,
                                                                                       grr.SRTariffType,
                                                                                       ent.ChargeClassID, ent.ItemID);
                            if (!comps.Any())
                                comps = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date,
                                                                                       AppSession.Parameter.
                                                                                           DefaultTariffType,
                                                                                       AppSession.Parameter.
                                                                                           DefaultTariffClass,
                                                                                       ent.ItemID);

                            foreach (var comp in comps)
                            {
                                var tcomp = transChargesItemComps.AddNew();
                                tcomp.SequenceNo = ent.SequenceNo;
                                tcomp.TariffComponentID = comp.TariffComponentID;

                                var tc = new TariffComponent();
                                tc.LoadByPrimaryKey(tcomp.TariffComponentID);
                                tcomp.TariffComponentName = tc.TariffComponentName;

                                var tcp = new ItemPackageTariffComponent();
                                if (tcp.LoadByPrimaryKey(Request.QueryString["item"], ent.ItemID,
                                                         tcomp.TariffComponentID))
                                    tcomp.Price = tcp.Price ?? 0;
                                else
                                    tcomp.Price = comp.Price ?? 0;

                                tcomp.DiscountAmount = 0;
                                tcomp.ParamedicID = string.Empty;
                                tcomp.IsPackage = true;

                                pricePackage += tcomp.Price ?? 0;
                            }

                            // consumption
                            var cons = new ItemConsumptionCollection();
                            cons.Query.Where(cons.Query.ItemID == pac.DetailItemID);
                            cons.LoadAll();

                            foreach (var consEntity in cons)
                            {
                                var consItem = transChargesItemConsumptions.AddNew();
                                consItem.SequenceNo = ent.SequenceNo;
                                consItem.DetailItemID = consEntity.DetailItemID;

                                i = new Item();
                                i.LoadByPrimaryKey(consItem.DetailItemID);
                                consItem.ItemName = i.ItemName;

                                consItem.Qty = ent.ChargeQuantity*consEntity.Qty;
                                consItem.QtyRealization = consItem.Qty;
                                consItem.SRItemUnit = consEntity.SRItemUnit;

                                var tariff = new ItemTariff();
                                if (
                                    !tariff.Load(GetItemTariffQuery(grr.SRTariffType, ent.ChargeClassID,
                                                                    consItem.DetailItemID)))
                                    tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType,
                                                                   AppSession.Parameter.DefaultTariffClass,
                                                                   consItem.DetailItemID));
                                consItem.Price = tariff.Price ?? 0;

                                consItem.IsPackage = true;
                            }
                            break;
                        default:
                            if (pac.IsStockControl ?? false)
                            {
                                var consItem = transChargesItemConsumptions.AddNew();
                                consItem.SequenceNo = ent.SequenceNo;
                                consItem.DetailItemID = pac.DetailItemID;

                                i = new Item();
                                i.LoadByPrimaryKey(consItem.DetailItemID);
                                consItem.ItemName = i.ItemName;

                                consItem.Qty = ent.ChargeQuantity*pac.Quantity;
                                consItem.QtyRealization = consItem.Qty;
                                consItem.SRItemUnit = pac.SRItemUnit;

                                var tariff = new ItemTariff();
                                if (
                                    !tariff.Load(GetItemTariffQuery(grr.SRTariffType, ent.ChargeClassID,
                                                                    consItem.DetailItemID)))
                                    tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType,
                                                                   AppSession.Parameter.DefaultTariffClass,
                                                                   consItem.DetailItemID));
                                consItem.Price = tariff.Price ?? 0;
                                pricePackage = tariff.Price ?? 0;
                                consItem.IsPackage = true;
                            }
                            break;
                    }

                    ent.Price = pricePackage;
                    ent.IsExtraItem = true;
                    ent.IsSelectedExtraItem = ((CheckBox) dataItem.FindControl("detailChkbox")).Checked;
                }
                else
                    ent.IsSelectedExtraItem = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
            }

            return true;
        }

        private static ItemTariffQuery GetItemTariffQuery(string tariffType, string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.SRTariffType == tariffType,
                    query.ClassID == classID,
                    query.ItemID == itemID,
                    query.StartingDate <= DateTime.Now
                );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }
    }
}
