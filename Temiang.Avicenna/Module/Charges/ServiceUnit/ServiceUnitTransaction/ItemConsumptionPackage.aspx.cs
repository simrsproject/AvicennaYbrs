using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;
using DevExpress.DataProcessing;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemConsumptionPackage : BasePageDialog
    {
        private LocationCollection locs {
            get {
                return (LocationCollection)Session["icp_LocColl"];
            }
            set {
                Session["icp_LocColl"] = value;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.ServiceUnitTransaction;

            if (!IsPostBack)
            {
                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["item"]);

                Title = "Item Consumption : " + item.ItemName + " [" + Request.QueryString["item"] + "]";

                var locColl = new LocationCollection();
                locColl.LoadByServiceUnitID(Request.QueryString["unit"]);
                locs = locColl;

                if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingItemConsAndExpFactorOnJORealizationList) == "Yes" && Request.QueryString["md"] == "view")
                {
                    TransChargesItemConsumptions = null;
                    var cons = TransChargesItemConsumptions;

                    (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                    (Helper.FindControlRecursive(this, "btnCancel") as Button).Text = "Close";

                    grdList.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                    grdList.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                var entity = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]]).FindByPrimaryKey(
                    dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text, dataItem["DetailItemID"].Text);
                if (entity != null)
                {
                    entity.QtyRealization = Convert.ToDecimal((dataItem.FindControl("txtQtyRealization") as RadNumericTextBox).Value ?? 0);
                    entity.LocationID = (dataItem.FindControl("cboLocationID") as RadComboBox).SelectedValue;
                }
            }

            return true;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //var list = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]]).Where(i => i.TransactionNo == Request.QueryString["trans"] &&
            //                                                                                                     i.SequenceNo.Substring(0, 3) == Request.QueryString["seq"] &&
            //                                                                                                     i.IsPackage == true);
            //var list = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]]).Where(i => i.TransactionNo == Request.QueryString["trans"] &&
            //                                                                                                     i.SequenceNo.Substring(0, 3) == Request.QueryString["seq"]);
            var list = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]]).Where(i => i.TransactionNo == Request.QueryString["trans"] &&
                                                                                                                 i.SequenceNo == Request.QueryString["seq"]);
            grdList.DataSource = list;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                var tcc = ((TransChargesItemConsumption)dataItem.DataItem);
                var cbo = dataItem.FindControl("cboLocationID") as RadComboBox;
                if (cbo != null) {
                    cbo.Items.Add(string.Empty);
                    foreach (var loc in locs) {
                        cbo.Items.Add(new RadComboBoxItem(loc.LocationName, loc.LocationID));
                    }
                    if (!string.IsNullOrEmpty(tcc.LocationID)) {
                        var ci = cbo.Items.FindItemByValue(tcc.LocationID);
                        if (ci != null) {
                            cbo.SelectedValue = tcc.LocationID;
                        }
                    }
                    if (string.IsNullOrEmpty(cbo.SelectedValue)) {
                        if (cbo.Items.Count == 2) cbo.Items[1].Selected = true;
                    }
                }
            }
        }

        private TransChargesItemConsumptionCollection TransChargesItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]];
                    if (obj != null)
                        return ((TransChargesItemConsumptionCollection)(obj));
                }

                var coll = new TransChargesItemConsumptionCollection();

                var query = new TransChargesItemConsumptionQuery("a");
                var item = new ItemQuery("b");
                var tci = new TransChargesItemQuery("d");
                var loc = new LocationQuery("loc");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName")
                    );

                if (AppSession.Parameter.IsValidateMaxQtyItemConsumptions)
                    query.Select(@"<CASE WHEN a.Qty = 0 THEN 10000 ELSE a.Qty END AS 'refTo_MaxQty'>");
                else
                    query.Select(@"<CAST(10000 AS NUMERIC(10,2)) AS 'refTo_MaxQty'>");

                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);
                query.InnerJoin(loc).On(query.LocationID == loc.LocationID).Select(
                    loc.LocationName.As("refToLocation_LocationName"));

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == Request.QueryString["trans"], query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == Request.QueryString["trans"]);

                coll.Load(query);

                Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]] = coll;
                return coll;
            }
            set { Session["collTransChargesItemConsumption" + Request.UserHostName + Request.QueryString["pageId"]] = value; }
        }

        protected void grdList_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = TransChargesItemConsumptions.AddNew();
            SetEntityValue(entity, e);

            //Stay in insert mode
            e.Canceled = true;
            grdList.Rebind();
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String itemId = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemConsumptionMetadata.ColumnNames.DetailItemID]);
            var entity = FindItemConsumption(itemId);
            if (entity != null && entity.Qty == 0)
                entity.MarkAsDeleted();
        }

        private TransChargesItemConsumption FindItemConsumption(String itemId)
        {
            TransChargesItemConsumptionCollection coll = TransChargesItemConsumptions;
            TransChargesItemConsumption retEntity = null;
            foreach (TransChargesItemConsumption rec in coll)
            {
                if (rec.DetailItemID.Equals(itemId))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(TransChargesItemConsumption entity, GridCommandEventArgs e)
        {
            var userControl = (ItemConsumptionPackageEntry)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.TransactionNo = Request.QueryString["trans"];
                entity.SequenceNo = Request.QueryString["seq"];
                entity.DetailItemID = userControl.DetailItemID;
                entity.ItemName = userControl.DetailItemName;
                entity.Qty = 0;
                entity.QtyRealization = userControl.Qty;
                entity.MaxValue = 10000;
                entity.SRItemUnit = userControl.SRItemUnit;

                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["reg"]);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                var tariff = new ItemTariff();
                if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, reg.ChargeClassID, userControl.DetailItemID)))
                {
                    if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, userControl.DetailItemID)))
                    {
                        if (!tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, userControl.DetailItemID)))
                            tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, userControl.DetailItemID));
                    }
                }
                entity.Price = tariff.Price ?? 0;

                var i = new Item();
                i.LoadByPrimaryKey(entity.DetailItemID);
                switch (i.SRItemType)
                {
                    case ItemType.Medical:
                        var im = new ItemProductMedic();
                        im.LoadByPrimaryKey(i.ItemID);
                        entity.AveragePrice = im.CostPrice;
                        entity.FifoPrice = im.PriceInBaseUnit;
                        break;
                    case ItemType.NonMedical:
                        var inm = new ItemProductNonMedic();
                        inm.LoadByPrimaryKey(i.ItemID);
                        entity.AveragePrice = inm.CostPrice;
                        entity.FifoPrice = inm.PriceInBaseUnit;
                        break;
                    case ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(i.ItemID);
                        entity.AveragePrice = ik.CostPrice;
                        entity.FifoPrice = ik.PriceInBaseUnit;
                        break;
                    default:
                        entity.AveragePrice = entity.Price;
                        entity.FifoPrice = entity.Price;
                        break;
                }
                entity.IsPackage = false;
            }
        }

        public static ItemTariffQuery GetItemTariffQuery(string tariffType, string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.SRTariffType == tariffType,
                    query.ClassID == classID,
                    query.ItemID == itemID,
                    query.StartingDate <= (new DateTime()).NowAtSqlServer()
                );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }

        protected override void RaisePostBackEvent(System.Web.UI.IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (!(sourceControl is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            if (eventArgument.Equals("rebind"))
                grdList.Rebind();
            
        }
    }
}
