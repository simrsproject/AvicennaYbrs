using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using System.Data;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemPickerListItemProduct : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void PopulateList()
        {
            string guarantorId;
            if (string.IsNullOrEmpty(Request.QueryString["guar"]))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["reg"]);
                guarantorId = reg.GuarantorID;
            }
            else 
                guarantorId = Request.QueryString["guar"];

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(guarantorId);

            bool isFornas = (grr.IsItemRestrictionsFornas ?? false);
            bool isFormularium = (grr.IsItemRestrictionsFormularium ?? false);
            bool isGeneric = (grr.IsItemRestrictionsGeneric ?? false);
            bool isNonGeneric = (grr.IsItemRestrictionsNonGeneric ?? false);
            bool isNonGenericLimited = (grr.IsItemRestrictionsNonGenericLimited ?? false);

            var itemQuery = new ItemQuery("a");
            var itemBalanceQuery = new ItemBalanceQuery("b");
            var itemVwQuery = new VwItemProductMedicNonMedicQuery("c");
            itemQuery.InnerJoin(itemBalanceQuery).On(itemQuery.ItemID == itemBalanceQuery.ItemID &&
                                                     itemBalanceQuery.LocationID == Request.QueryString["loc"]);
            itemQuery.InnerJoin(itemVwQuery).On(itemQuery.ItemID == itemVwQuery.ItemID);
            itemQuery.Where(itemQuery.IsActive == true, itemBalanceQuery.Balance >= 1, itemVwQuery.IsSalesAvailable == true);

            var xx = new List<Temiang.Dal.DynamicQuery.esComparison>();

            if (isFornas)
                xx.Add(itemVwQuery.IsFornas == true);

            if (isFormularium)
                xx.Add(itemVwQuery.IsFormularium == true);

            if (isGeneric)
                xx.Add(itemVwQuery.IsGeneric == true);

            if (isNonGeneric)
                xx.Add(itemVwQuery.IsNonGeneric == true);

            if (isNonGenericLimited)
                xx.Add(itemVwQuery.IsNonGenericLimited == true);

            if (xx.Count > 0)
                itemQuery.Where(itemQuery.Or(xx.ToArray()));
            else
            {
                var restrictions = new GuarantorItemRestrictionsQuery("rest");
                var itmrest = new ItemQuery("itmrest");
                restrictions.Select(restrictions.ItemID);
                restrictions.InnerJoin(itmrest).On(restrictions.ItemID == itmrest.ItemID);
                restrictions.Where(restrictions.GuarantorID == grr.GuarantorID, itmrest.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
                DataTable dtRest = restrictions.LoadDataTable();
                if (dtRest.Rows.Count > 0)
                {
                    //itemQuery.InnerJoin(restrictions).On(itemQuery.ItemID == restrictions.ItemID && restrictions.GuarantorID == grr.GuarantorID);
                    if (grr.IsItemProductRestrictionStatusAllowed ?? true)
                        itemQuery.Where(itemQuery.ItemID.In(restrictions));
                    else
                        itemQuery.Where(itemQuery.ItemID.NotIn(restrictions));
                }
            }

            itemQuery.OrderBy(itemQuery.ItemName.Ascending);

            var items = new ItemCollection();
            items.Load(itemQuery);

            if (items.Count > 0)
            {
                var groups = new ItemGroupCollection();
                groups.Query.Where(groups.Query.ItemGroupID.In(items.Select(i => i.ItemGroupID).Distinct()));
                groups.Query.OrderBy(groups.Query.SRItemType.Ascending, groups.Query.ItemGroupID.Ascending);
                groups.LoadAll();

                HtmlTable tab1 = new HtmlTable() { ID = "tab1", Width = "100%" },
                    tab2 = new HtmlTable() { ID = "tab2", Width = "100%" },
                    tab3 = new HtmlTable() { ID = "tab3", Width = "100%" },
                    tab4 = new HtmlTable() { ID = "tab4", Width = "100%" },
                    tab5 = new HtmlTable() { ID = "tab5", Width = "100%" };

                int count = (items.Count + groups.Count) / 5, idx = 0;
                var index = new int[4] { count, count * 2, count * 3, count * 4 };

                foreach (var group in groups)
                {
                    var label = new Label();
                    label.ID = "labelgroup_" + group.ItemGroupID;
                    label.Font.Bold = true;
                    label.Text = group.ItemGroupName;

                    var cell = new HtmlTableCell();
                    cell.ID = "cellgroup_" + group.ItemGroupID;
                    cell.Style["background-color"] = "ButtonFace";
                    cell.Style["color"] = "ButtonText";
                    cell.Controls.Add(label);

                    var row = new HtmlTableRow();
                    row.ID = "rowgroup_" + group.ItemGroupID;
                    row.Cells.Add(cell);

                    if (idx < index[0])
                    {
                        if ((idx + 1) != index[0])
                            tab1.Rows.Add(row);
                        else
                            tab2.Rows.Add(row);
                    }
                    else if (idx >= index[0] && idx < index[1])
                    {
                        if ((idx + 1) != index[1])
                            tab2.Rows.Add(row);
                        else
                            tab3.Rows.Add(row);
                    }
                    else if (idx >= index[1] && idx < index[2])
                    {
                        if ((idx + 1) != index[2])
                            tab3.Rows.Add(row);
                        else
                            tab4.Rows.Add(row);
                    }
                    else if (idx >= index[2] && idx < index[3])
                    {
                        if ((idx + 1) != index[3])
                            tab4.Rows.Add(row);
                        else
                            tab5.Rows.Add(row);
                    }
                    else if (idx >= index[3])
                        tab5.Rows.Add(row);

                    idx++;

                    foreach (var item in items.Where(i => i.ItemGroupID == group.ItemGroupID))
                    {
                        if (idx < index[0])
                            tab1.Rows.Add(PopulateListItem(item.ItemID, item.ItemName));
                        else if (idx >= index[0] && idx < index[1])
                            tab2.Rows.Add(PopulateListItem(item.ItemID, item.ItemName));
                        else if (idx >= index[1] && idx < index[2])
                            tab3.Rows.Add(PopulateListItem(item.ItemID, item.ItemName));
                        else if (idx >= index[2] && idx < index[3])
                            tab4.Rows.Add(PopulateListItem(item.ItemID, item.ItemName));
                        else if (idx >= index[3])
                            tab5.Rows.Add(PopulateListItem(item.ItemID, item.ItemName));

                        idx++;
                    }
                }

                table1.Rows[0].Cells[0].Controls.Add(tab1);
                table1.Rows[0].Cells[1].Controls.Add(tab2);
                table1.Rows[0].Cells[2].Controls.Add(tab3);
                table1.Rows[0].Cells[3].Controls.Add(tab4);
                table1.Rows[0].Cells[4].Controls.Add(tab5);
            }
        }

        private HtmlTableRow PopulateListItem(string itemID, string itemName)
        {
            //var qty = new RadNumericTextBox();
            //qty.ID = "qty_" + itemID;
            //qty.Value = 1;
            //qty.Width = 30;

            //var check = new CheckBox();
            //check.ID = "i_" + itemID;
            //check.Text = itemName;

            var check = new CheckBox();
            check.ID = "i_" + itemID;
            check.Text = string.Empty;

            var qty = new RadNumericTextBox();
            qty.ID = "q_" + itemID;
            qty.Value = 1;
            qty.Width = 40;
            qty.MinValue = 0.01;

            var label = new Label();
            label.ID = "l_" + itemID;
            label.Text = itemName;

            var t = new HtmlTable();
            t.CellPadding = 0;
            t.CellSpacing = 0;
            {
                var r = new HtmlTableRow();
                r.Cells.Add(new HtmlTableCell());
                r.Cells.Add(new HtmlTableCell());
                r.Cells.Add(new HtmlTableCell());
                r.Cells.Add(new HtmlTableCell());

                r.Cells[0].VAlign = "Top";
                r.Cells[0].Controls.Add(check);
                r.Cells[1].VAlign = "Top";
                r.Cells[1].Controls.Add(qty);
                r.Cells[2].Width = "5";
                r.Cells[3].Controls.Add(label);

                t.Rows.Add(r);
            }

            var cell = new HtmlTableCell();
            cell.ID = "cellitem_" + itemID;
            cell.Controls.Add(t);

            var row = new HtmlTableRow();
            row.ID = "rowitem_" + itemID;
            row.Cells.Add(cell);

            return row;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.rebind = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var chkboxes = Helper.AllControls(table1).Where(c => c.GetType() == typeof(CheckBox) &&
                                                          c.ID.Split('_')[0] == "i")
                                                     .Select(t => ((CheckBox)t));

            if (!chkboxes.Any(c => c.Checked)) return false;

            var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]];
            
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["reg"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var tariffDate = (new DateTime()).NowAtSqlServer().Date;

            var qtytextbox = Helper.AllControls(table1).Where(c => c.GetType() == typeof(RadNumericTextBox) &&
                                                           c.ID.Split('_')[0] == "q" &&
                                                           (chkboxes.Where(b => b.Checked)
                                                                    .Select(b => b.ID.Split('_')[1])).Contains(c.ID.Split('_')[1]))
                                                      .Select(t => ((RadNumericTextBox)t));
            var label = Helper.AllControls(table1).Where(c => c.GetType() == typeof(Label) &&
                                                           c.ID.Split('_')[0] == "l" &&
                                                           (chkboxes.Where(b => b.Checked)
                                                                    .Select(b => b.ID.Split('_')[1])).Contains(c.ID.Split('_')[1]))
                                                      .Select(t => ((Label)t));

            var sMsg = string.Empty;
            
            var tcompColl = new TariffComponentCollection();
            tcompColl.LoadAll();

            foreach (var chkbox in chkboxes.Where(c => c.Checked))
            {
                var parentCharges = charges.Where(x => x.SequenceNo.Length.Equals(3));
                var seqNo = parentCharges.Any() ? parentCharges.ElementAt(parentCharges.Count() - 1).SequenceNo : "000";

                var entity = FindTransChargesItem(chkbox.ID.Split('_')[1]);

                if (entity == null)
                {
                    entity = charges.AddNew();

                    entity.SequenceNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.SequenceNo;
                }

                entity.TransactionNo = string.Empty;
                entity.ParentNo = string.Empty;
                entity.ReferenceNo = string.Empty;
                entity.ReferenceSequenceNo = string.Empty;
                entity.ItemID = chkbox.ID.Split('_')[1];
                //entity.ItemName = chkbox.Text;
                var lbl = label.SingleOrDefault(c => c.ID.Split('_')[1] == entity.ItemID);
                entity.ItemName = lbl.Text.Trim();
                
                entity.ChargeClassID = reg.ChargeClassID;
                entity.ParamedicID = string.Empty;
                entity.ParamedicName = string.Empty;
                entity.TariffDate = tariffDate;

                var tariff = (Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(tariffDate, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                             (Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType) ??
                              Helper.Tariff.GetItemTariff(tariffDate, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, entity.ItemID, reg.GuarantorID, false, reg.SRRegistrationType));

                if (tariff == null)
                {
                    var cl = new BusinessObject.Class();
                    cl.LoadByPrimaryKey(entity.ChargeClassID);
                    sMsg += (!string.IsNullOrEmpty(sMsg) ? ", " : "") + string.Format("{0} ({1})", entity.ItemName, cl.ClassName);

                    continue;
                }

                entity.IsAdminCalculation = tariff.IsAdminCalculation;
                entity.IsVariable = false;

                entity.IsCito = false;
                entity.CitoAmount = 0;

                var qty = qtytextbox.SingleOrDefault(c => c.ID.Split('_')[1] == entity.ItemID);
                entity.ChargeQuantity = Convert.ToDecimal(qty.Value); //1;
                entity.StockQuantity = entity.ChargeQuantity; //1;
                var i = new Item();
                i.LoadByPrimaryKey(entity.ItemID);
                switch (i.SRItemType)
                {
                    case BusinessObject.Reference.ItemType.Medical:
                        var im = new ItemProductMedic();
                        im.LoadByPrimaryKey(i.ItemID);
                        entity.SRItemUnit = im.SRItemUnit;
                        break;

                    case BusinessObject.Reference.ItemType.NonMedical:
                        var inm = new ItemProductNonMedic();
                        inm.LoadByPrimaryKey(i.ItemID);
                        entity.SRItemUnit = inm.SRItemUnit;
                        break;

                    case BusinessObject.Reference.ItemType.Kitchen:
                        var ik = new ItemKitchen();
                        ik.LoadByPrimaryKey(i.ItemID);
                        entity.SRItemUnit = ik.SRItemUnit;
                        break;
                }
                
                entity.Price = tariff.Price;
                entity.DiscountAmount = 0;
                entity.Total = Helper.Rounding((entity.ChargeQuantity ?? 0) * (((entity.Price ?? 0) - (entity.DiscountAmount ?? 0)) + (entity.CitoAmount ?? 0)), AppEnum.RoundingType.Transaction);
                entity.RoundingAmount = Helper.RoundingDiff;
                entity.SRDiscountReason = string.Empty;
                entity.IsAssetUtilization = false;
                entity.AssetID = string.Empty;
                entity.IsBillProceed = false;
                entity.IsOrderRealization = false;
                entity.IsPackage = false;
                entity.IsApprove = false;
                entity.IsVoid = false;
                entity.Notes = string.Empty;
                entity.LastUpdateDateTime = DateTime.Now;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.IsItemRoom = false;
                entity.SRCitoPercentage = string.Empty;
                entity.IsItemTypeService = false;
                entity.ItemConditionRuleID = string.Empty;

                if (Helper.GuarantorBpjsCasemix.Contains(reg.GuarantorID) && AppSession.Parameter.CasemixValidationRegistrationType.Contains(reg.SRRegistrationType))
                    entity.IsCasemixApproved = Helper.IsCasemixApproved(entity.ItemID, entity.ChargeQuantity ?? 0, reg.RegistrationNo, entity.TransactionNo, reg.GuarantorID, false);
                else
                    entity.IsCasemixApproved = true;

                var group = new ItemGroup();
                group.LoadByPrimaryKey(i.ItemGroupID);
                entity.ItemGroupName = group.ItemGroupName;
            }

            if (!string.IsNullOrEmpty(sMsg))
            {
                ShowInformationHeader(
                    string.Format("Item product of {0} {1} no tariff, please contact administrator!",
                    sMsg, (sMsg.IndexOf(',') > 0) ? "have" : "has"));

                charges.MarkAllAsDeleted();

                return false;
            }

            return true;
        }

        private TransChargesItem FindTransChargesItem(string itemID)
        {
            var charges = (TransChargesItemCollection)Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]];
            return charges.FirstOrDefault(detail => detail.ItemID == itemID);
        }
    }
}
