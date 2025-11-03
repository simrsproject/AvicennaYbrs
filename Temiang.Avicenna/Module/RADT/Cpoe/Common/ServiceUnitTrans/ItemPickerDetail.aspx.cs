using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class ItemPickerDetail : BasePageDialog
    {
        private TransChargesItemCompCollection TransChargesItemComps
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItemComp" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCompCollection)(obj));
                }

                var coll = new TransChargesItemCompCollection();

                var query = new TransChargesItemCompQuery("a");
                var comp = new TariffComponentQuery("b");
                var tci = new TransChargesItemQuery("d");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);

                query.Select(
                    query,
                    comp.TariffComponentName.As("refToTariffComponent_TariffComponentName"),
                    comp.IsTariffParamedic
                    );
                query.InnerJoin(comp).On(query.TariffComponentID == comp.TariffComponentID);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == (string.IsNullOrEmpty(Request.QueryString["transno"]) ? "" : Request.QueryString["transno"]), query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == (string.IsNullOrEmpty(Request.QueryString["transno"]) ? "" : Request.QueryString["transno"]));

                query.OrderBy(
                        query.SequenceNo.Ascending,
                        query.TariffComponentID.Ascending
                    );

                coll.Load(query);

                Session["collTransChargesItemComp" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemComp" + Request.UserHostName] = value; }
        }

        private TransChargesItemCollection TransChargesItems
        {
            get
            {
                if (IsPostBack)
                {
                    var obj = Session["collTransChargesItem" + Request.UserHostName];
                    if (obj != null)
                        return ((TransChargesItemCollection)(obj));
                }

                var coll = new TransChargesItemCollection();
                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");
                var param = new ParamedicQuery("c");
                var tci = new TransChargesItemQuery("d");
                var tounit = new ServiceUnitQuery("e");

                tci.Select(tci.TransactionNo, tci.SequenceNo);
                tci.Where(tci.TransactionNo == query.TransactionNo, tci.SequenceNo == query.SequenceNo,
                          tci.IsExtraItem == true,
                          tci.IsSelectedExtraItem == false);


                var total = new esQueryItem(query, "Total", esSystemType.Decimal);
                total = ((query.ChargeQuantity * query.Price) - query.DiscountAmount) + query.CitoAmount;

                query.Select
                    (
                        query,
                        total.As("refToTransChargesItem_Total"),
                        @"<CASE WHEN ISNULL(a.FilmNo, '') = '' THEN b.ItemName ELSE b.ItemName + ' [' + a.FilmNo + ']' END AS refToItem_ItemName>",
                    //item.ItemName.As("refToItem_ItemName"),
                        param.ParamedicName.As("refToParamedic_ParamedicName"),
                        tounit.ServiceUnitName.As("refToServiceUnit_ServiceUnitName"),
                        "<CAST((CASE WHEN b.SRItemType IN ('" + ItemType.Medical + "', '" + ItemType.NonMedical + "') THEN 0 ELSE 1 END) AS BIT) AS refTo_IsItemTypeService>"
                    );

                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.LeftJoin(param).On(query.ParamedicID == param.ParamedicID);
                query.LeftJoin(tounit).On(query.ToServiceUnitID == tounit.ServiceUnitID);

                if (Request.QueryString["type"] == "mcu")
                    query.Where(query.TransactionNo == Request.QueryString["transno"]);
                else
                    query.Where(query.TransactionNo == Request.QueryString["transno"], query.NotExists(tci));

                query.OrderBy(query.SequenceNo.Ascending);
                DataTable dtb = query.LoadDataTable();
                coll.Load(query);

                Session["collTransChargesItem" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItem" + Request.UserHostName] = value; }
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
                    query.Where(query.TransactionNo == (string.IsNullOrEmpty(Request.QueryString["transno"]) ? "" : Request.QueryString["transno"]), query.NotExists(tci));
                else
                    query.Where(query.TransactionNo == (string.IsNullOrEmpty(Request.QueryString["transno"]) ? "" : Request.QueryString["transno"]));

                //query.Where(query.TransactionNo == txtTransactionNo.Text);

                coll.Load(query);

                Session["collTransChargesItemConsumption" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["collTransChargesItemConsumption" + Request.UserHostName] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //switch (Request.QueryString["type"])
            //{
            //    case "tr":
            //        ProgramID = AppConstant.Program.ServiceUnitTransaction;
            //        break;
            //    case "jo":
            //        ProgramID = AppConstant.Program.JobOrderTransaction;
            //        break;
            //    case "ds":
            //        ProgramID = AppConstant.Program.DiagnosticSupportTransaction;
            //        break;
            //    default:
            //        ProgramID = AppConstant.Program.EpisodeAndHistory;
            //        break;
            //}

            if (!IsPostBack)
            {
                pnlItemType.Visible = (Request.QueryString["type"] != "jo");
                if (AppSession.Parameter.IsTariffPriceVisibleOnlyForAdm)
                {
                    if (string.IsNullOrEmpty(Request.QueryString["verif"]) || Request.QueryString["verif"] == "0")
                    {
                        grdList.Columns[grdList.Columns.Count - 2].Visible = false;
                    }
                }
            }

        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (e.IsFromDetailTable)
                return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["reg"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var tbl =
                Helper.Tariff.GetItemQuery(Request.QueryString["unit"], Request.QueryString["loc"],
                                           (pnlItemType.Visible ? cboItemType.SelectedValue : string.Empty),
                                           txtFilter.Text, reg.GuarantorID, true).LoadDataTable();

            foreach (DataRow row in tbl.Rows)
            {
                var tariff = (Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, (string)row["ItemID"], reg.GuarantorID, false, reg.SRRegistrationType) ??
                          Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, (string)row["ItemID"], reg.GuarantorID, false, reg.SRRegistrationType)) ??
                         (Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, (string)row["ItemID"], reg.GuarantorID, false, reg.SRRegistrationType) ??
                          Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, (string)row["ItemID"], reg.GuarantorID, false, reg.SRRegistrationType));
                if (tariff == null) continue;

                row["Price"] = tariff.Price ?? 0;
                row["IsAllowCito"] = tariff.IsAllowCito ?? false;
                row["IsAllowVariable"] = tariff.IsAllowVariable ?? false;
                row["IsAdminCalculation"] = tariff.IsAdminCalculation ?? false;
                row["IsCitoFromStandardReference"] = tariff.IsCitoFromStandardReference ?? false;
            }

            tbl.AcceptChanges();

            ViewState["list" + Request.UserHostName] = tbl;
            grdList.DataSource = tbl;
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                var txt = ((RadNumericTextBox)e.Item.FindControl("txtQty"));
                if (txt.Text == string.Empty || txt.Value == 0)
                    return;

                var src = ((DataTable)ViewState["list" + Request.UserHostName]).Rows[e.Item.DataSetIndex];
                var valid = true;

                if (cboItemType.SelectedValue == "00" && (Decimal)src["Balance"] == 0) // 00 : Item
                    return;
                if (cboItemType.SelectedValue == ItemType.Service) // AppSession.Parameter.ItemService : Service
                {
                    var cons = new ItemConsumptionCollection();
                    cons.Query.Where(cons.Query.ItemID == (string)src["ItemID"]);
                    cons.LoadAll();

                    if (cons.Count > 0)
                    {
                        var unit = new ServiceUnit();
                        unit.LoadByPrimaryKey(Request.QueryString["unit"]);

                        foreach (var entity in cons)
                        {
                            var balance = new ItemBalance();
                            if (!balance.LoadByPrimaryKey(Request.QueryString["loc"], entity.DetailItemID))
                            {
                                valid = false;
                                break;
                            }
                            if (balance.Balance < entity.Qty)
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                }

                var chkCito = (CheckBox)e.Item.FindControl("chkCito");
                var cboSRCitoPercentage = (RadComboBox)e.Item.FindControl("cboSRCitoPercentage");

                if ((bool)src["IsCitoFromStandardReference"])
                {
                    if (chkCito.Checked)
                    {
                        if (string.IsNullOrEmpty(cboSRCitoPercentage.SelectedValue))
                        {
                            valid = false;
                        }
                    }
                }

                if (!valid)
                    return;

                var row = ((DataTable)ViewState["selected" + Request.UserHostName]).NewRow();
                row["ItemID"] = src["ItemID"];
                row["ItemName"] = src["ItemName"];
                row["Cito"] = chkCito.Checked;
                row["Variable"] = src["IsAllowVariable"];
                row["Admin"] = src["IsAdminCalculation"];
                row["Qty"] = (int)((RadNumericTextBox)e.Item.FindControl("txtQty")).Value;
                row["Price"] = src["Price"];
                row["SRItemUnit"] = src["SRItemUnit"];
                row["SRCitoPercentage"] = cboSRCitoPercentage.SelectedValue;
                row["SRCitoPercentageName"] = cboSRCitoPercentage.Text;

                var dst = ((DataTable)ViewState["selected" + Request.UserHostName]);

                bool exist = false;
                foreach (DataRow bar in dst.Rows.Cast<DataRow>().Where(bar => bar["ItemID"].ToString() == src["ItemID"].ToString()))
                {
                    exist = true;
                    bar["Cito"] = chkCito.Checked;
                    bar["Qty"] = (int)((RadNumericTextBox)e.Item.FindControl("txtQty")).Value;
                    bar["SRCitoPercentage"] = cboSRCitoPercentage.SelectedValue;
                    bar["SRCitoPercentageName"] = cboSRCitoPercentage.Text;
                    break;
                }

                if (!exist)
                    dst.Rows.Add(row);
                else
                {
                    dst.AcceptChanges();
                    ViewState["selected" + Request.UserHostName] = dst;
                }

                grdSelected.Rebind();
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                var cbo = (RadComboBox)item.FindControl("cboSRCitoPercentage");
                DataRow row = ((DataRowView)item.DataItem).Row;
                if ((bool)row["IsCitoFromStandardReference"])
                {
                    StandardReference.InitializeIncludeSpace(cbo, AppEnum.StandardReference.CitoPercentage);
                }
                else
                {
                    cbo.Visible = false;
                }
            }
        }

        protected void grdSelected_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                var row = ((DataTable)ViewState["selected" + Request.UserHostName]).Rows[e.Item.DataSetIndex];

                ((DataTable)ViewState["selected" + Request.UserHostName]).Rows.Remove(row);

                grdSelected.Rebind();
            }
        }

        protected void grdSelected_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["selected" + Request.UserHostName] == null)
            {
                DataTable tbl;
                SetDataColumnSelected(out tbl);

                ViewState["selected" + Request.UserHostName] = tbl;
                grdSelected.DataSource = tbl;
            }
            else
                grdSelected.DataSource = (DataTable)ViewState["selected" + Request.UserHostName];
        }

        private static void SetDataColumnSelected(out DataTable dataTable)
        {
            var tbl = new DataTable();

            var col = new DataColumn("ItemID", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("ItemName", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("Cito", typeof(bool));
            tbl.Columns.Add(col);

            col = new DataColumn("Variable", typeof(bool));
            tbl.Columns.Add(col);

            col = new DataColumn("Admin", typeof(bool));
            tbl.Columns.Add(col);

            col = new DataColumn("Qty", typeof(int));
            tbl.Columns.Add(col);

            col = new DataColumn("Price", typeof(decimal));
            tbl.Columns.Add(col);

            col = new DataColumn("SRItemUnit", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("SRCitoPercentage", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("SRCitoPercentageName", typeof(string));
            tbl.Columns.Add(col);

            dataTable = tbl;
        }

        public override bool OnButtonOkClicked()
        {
            var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];
            var consumptions = (TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName];

            var tbl = (DataTable)ViewState["selected" + Request.UserHostName];

            var seqNo = charges.Any() ? charges[charges.Count - 1].SequenceNo : "000";

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["reg"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            foreach (DataRow row in tbl.Rows)
            {
                var entity = FindTransChargesItem(row["ItemID"].ToString());

                bool isNewRecord = false;
                if (entity == null)
                {
                    entity = charges.AddNew();

                    entity.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.SequenceNo;

                    isNewRecord = true;
                }

                var item = new Item();
                item.LoadByPrimaryKey(row["ItemID"].ToString());

                //////////////////////////
                ServiceUnitTransEntry.SetEntityDetail(entity, entity.SequenceNo,
                    row["ItemID"].ToString(), row["ItemName"].ToString(), string.Empty,
                    string.Empty, (bool)row["Admin"], (bool)row["Variable"], (bool)row["Cito"],
                    Convert.ToDecimal(row["Qty"]), 0, (string)row["SRItemUnit"], 0, Convert.ToDecimal(row["Price"]),
                    0, string.Empty, false, string.Empty, (item.SRItemType == ItemType.Package),
                    false, string.Empty, string.Empty,
                    (new System.Collections.Generic.List<TransChargesItemComp>()).AsEnumerable(),
                    isNewRecord, false, string.Empty,
                    Request.QueryString["transno"], Request.QueryString["reg"], reg.ChargeClassID,
                    TransChargesItemComps, DateTime.Now.Date,
                    TransChargesItems, TransChargesItemConsumptions, string.Empty, row["SRCitoPercentage"].ToString(), string.Empty, true
                    );
                //////////////////////////
            }
            return true;
        }

        //public override bool OnButtonOkClicked()
        //{
        //    var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];
        //    var consumptions = (TransChargesItemConsumptionCollection)Session["collTransChargesItemConsumption" + Request.UserHostName];

        //    var tbl = (DataTable)ViewState["selected" + Request.UserHostName];

        //    var seqNo = charges.Any() ? charges[charges.Count - 1].SequenceNo : "000";

        //    var reg = new Registration();
        //    reg.LoadByPrimaryKey(Request.QueryString["reg"]);

        //    var grr = new Guarantor();
        //    grr.LoadByPrimaryKey(reg.GuarantorID);

        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        var entity = FindTransChargesItem(row["ItemID"].ToString());

        //        if (entity == null)
        //        {
        //            entity = charges.AddNew();

        //            entity.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
        //            seqNo = entity.SequenceNo;
        //        }

        //        entity.TransactionNo = string.Empty;
        //        entity.ParentNo = string.Empty;
        //        entity.ReferenceNo = string.Empty;
        //        entity.ReferenceSequenceNo = string.Empty;
        //        entity.ItemID = row["ItemID"].ToString();
        //        entity.ItemName = row["ItemName"].ToString();
        //        entity.ChargeClassID = reg.ChargeClassID;
        //        entity.ParamedicID = string.Empty;
        //        entity.ParamedicName = string.Empty;
        //        entity.IsAdminCalculation = (bool)row["Admin"];
        //        entity.IsVariable = (bool)row["Variable"];
        //        entity.IsCito = (bool)row["Cito"];
        //        entity.ChargeQuantity = Convert.ToDecimal(row["Qty"]);
        //        entity.StockQuantity = 0;
        //        entity.SRItemUnit = (string)row["SRItemUnit"];
        //        entity.Price = Convert.ToDecimal(row["Price"]);
        //        entity.DiscountAmount = 0;

        //        if (!(entity.IsCito ?? false))
        //            entity.CitoAmount = 0;
        //        else
        //        {
        //            var tariff = (Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                              Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
        //                             (Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
        //                             Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

        //                entity.CitoAmount = tariff != null ? (!(tariff.IsCitoInPercent ?? false) ? tariff.CitoValue : (tariff.CitoValue / 100) * entity.Price) : 0;
        //                entity.IsCitoInPercent = tariff.IsCitoInPercent ?? false;
        //                entity.BasicCitoAmount = tariff.CitoValue;

        //        }

        //        entity.Total = Helper.Rounding((entity.ChargeQuantity ?? 0) * (((entity.Price ?? 0) - (entity.DiscountAmount ?? 0)) + (entity.CitoAmount ?? 0)), AppEnum.RoundingType.Transaction);
        //        entity.RoundingAmount = Helper.RoundingDiff;
        //        entity.SRDiscountReason = string.Empty;
        //        entity.IsAssetUtilization = false;
        //        entity.AssetID = string.Empty;
        //        entity.IsBillProceed = false;
        //        entity.IsOrderRealization = false;
        //        entity.IsPackage = false;
        //        entity.IsVoid = false;
        //        entity.Notes = string.Empty;
        //        entity.IsItemRoom = false;

        //        var consColl = new ItemConsumptionCollection();
        //        consColl.Query.Where(consColl.Query.ItemID == entity.ItemID);
        //        consColl.LoadAll();

        //        foreach (var cons in consColl)
        //        {
        //            var ent = consumptions.FindByPrimaryKey(entity.TransactionNo, entity.SequenceNo, cons.DetailItemID) ?? consumptions.AddNew();
        //            ent.TransactionNo = entity.TransactionNo;
        //            ent.SequenceNo = entity.SequenceNo;
        //            ent.DetailItemID = cons.DetailItemID;
        //            ent.Qty = entity.ChargeQuantity * cons.Qty;
        //            ent.SRItemUnit = entity.SRItemUnit;
        //        }
        //    }
        //    return true;
        //}

        private TransChargesItem FindTransChargesItem(string itemID)
        {
            var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName];
            return charges.FirstOrDefault(detail => detail.ItemID == itemID);
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.rebind = 'rebind'";
        }

        protected void cboItemType_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(Request.QueryString["unit"]);

            var query = new ItemConsumptionQuery("a");
            var item = new ItemQuery("b");
            var balance = new ItemBalanceQuery("c");

            query.Select
                (
                    query.ItemID,
                    item.ItemName,
                    query.Qty,
                    balance.Balance.Coalesce("0"),
                    @"<CASE WHEN b.SRItemType = '11' THEN ISNULL((SELECT SRItemUnit FROM ItemProductMedic WHERE ItemID = a.ItemID), '') 
                                    WHEN b.SRItemType = '21' THEN ISNULL((SELECT SRItemUnit FROM ItemProductNonMedic WHERE ItemID = a.ItemID), '') 
                                    WHEN b.SRItemType = '81' THEN ISNULL((SELECT SRItemUnit FROM ItemKitchen WHERE ItemID = a.ItemID), '') 
                                    ELSE ISNULL((SELECT SRItemUnit FROM ItemOptic WHERE ItemID = a.ItemID), '') 
                                    END AS 'SRItemUnit'>"
                );
            query.InnerJoin(item).On(query.ItemID == item.ItemID);
            query.LeftJoin(balance).On
                (
                    query.ItemID == balance.ItemID &
                    balance.LocationID == Request.QueryString["loc"]
                );
            query.Where(query.ItemID == e.DetailTableView.ParentItem.GetDataKeyValue("ItemID").ToString());

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
