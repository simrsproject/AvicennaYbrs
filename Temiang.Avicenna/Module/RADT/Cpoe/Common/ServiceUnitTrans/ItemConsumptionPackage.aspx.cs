using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ItemConsumptionPackage : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //ProgramID = AppConstant.Program.ServiceUnitTransaction;

            if (!IsPostBack)
            {
                var item = new Item();
                item.LoadByPrimaryKey(Request.QueryString["item"]);

                Title = "Item Consumption : " + item.ItemName + " [" + Request.QueryString["item"] + "]";
            }
        }

        public override bool OnButtonOkClicked()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                var entity = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName]).FindByPrimaryKey(
                    dataItem["TransactionNo"].Text, dataItem["SequenceNo"].Text, dataItem["DetailItemID"].Text);
                if (entity != null)
                    entity.QtyRealization = Convert.ToDecimal((dataItem.FindControl("txtQtyRealization") as RadNumericTextBox).Value ?? 0);
            }

            return true;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //var list = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName]).Where(i => i.TransactionNo == Request.QueryString["trans"] &&
            //                                                                                                     i.SequenceNo.Substring(0, 3) == Request.QueryString["seq"] &&
            //                                                                                                     i.IsPackage == true);
            var list = ((TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName]).Where(i => i.TransactionNo == Request.QueryString["trans"] &&
                                                                                                                 i.SequenceNo.Substring(0, 3) == Request.QueryString["seq"]);
            grdList.DataSource = list;
        }

        private TransChargesItemConsumptionCollection TransChargesItemConsumptions
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemConsumption" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemConsumptionCollection)(obj));
                }

                var coll = new TransChargesItemConsumptionCollection();

                var query = new TransChargesItemConsumptionQuery("a");
                var item = new ItemQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName")
                    );
                query.InnerJoin(item).On(query.DetailItemID == item.ItemID);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == Request.QueryString["trans"], query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == Request.QueryString["trans"]);

                coll.Load(query);

                Session["collTransChargesItemConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemConsumption" + Request.UserHostName] = value; }
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
