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
using Temiang.Avicenna.BusinessObject.Reference;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorItemPickerList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            PopulateList();
        }

        private void PopulateList()
        {
            var itemQuery = new ItemQuery("a");
            if (Request.QueryString["itemtype"] == ItemType.Service)
            {
                var itemDetQuery = new ItemServiceQuery("b");

                itemQuery.InnerJoin(itemDetQuery).On(itemQuery.ItemID == itemDetQuery.ItemID);
                itemQuery.Where(itemQuery.IsActive == true);
                itemQuery.OrderBy(itemQuery.ItemID.Ascending);
            }
            else if (Request.QueryString["itemtype"] == ItemType.Laboratory)
            {
                var itemDetQuery = new ItemLaboratoryQuery("b");

                itemQuery.InnerJoin(itemDetQuery).On(itemQuery.ItemID == itemDetQuery.ItemID);
                itemQuery.Where(itemQuery.IsActive == true);
                itemQuery.OrderBy(itemQuery.ItemID.Ascending);
            }
            else if (Request.QueryString["itemtype"] == ItemType.Radiology)
            {
                var itemDetQuery = new ItemRadiologyQuery("b");

                itemQuery.InnerJoin(itemDetQuery).On(itemQuery.ItemID == itemDetQuery.ItemID);
                itemQuery.Where(itemQuery.IsActive == true);
                itemQuery.OrderBy(itemQuery.ItemID.Ascending);
            }
            else
            {
                var itemDetQuery = new VwItemProductMedicNonMedicQuery("b");

                itemQuery.InnerJoin(itemDetQuery).On(itemQuery.ItemID == itemDetQuery.ItemID);
                itemQuery.Where(itemQuery.IsActive == true, itemDetQuery.IsSalesAvailable == true);
                itemQuery.OrderBy(itemQuery.ItemID.Ascending);
            }

            var items = new ItemCollection();
            items.Load(itemQuery);

            if (items.Count > 0)
            {
                var groups = new ItemGroupCollection();
                groups.Query.Where(groups.Query.ItemGroupID.In(items.Select(i => i.ItemGroupID).Distinct()));
                groups.Query.OrderBy(groups.Query.ItemGroupID.Ascending);
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
            var check = new CheckBox();
            check.ID = "i_" + itemID;
            check.Text = itemName;

            var t = new HtmlTable();
            t.CellPadding = 0;
            t.CellSpacing = 0;
            {
                var r = new HtmlTableRow();
                r.Cells.Add(new HtmlTableCell());
                r.Cells.Add(new HtmlTableCell());

                r.Cells[0].VAlign = "Top";
                r.Cells[0].Controls.Add(check);

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

            var guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemRestrictions"];

            if (Request.QueryString["itemtype"] == ItemType.Service)
            {
                guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemServiceRestrictions"];
            }
            else if (Request.QueryString["itemtype"] == ItemType.Laboratory)
            {
                guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemLaboratoryRestrictions"];
            }
            else if (Request.QueryString["itemtype"] == ItemType.Radiology)
            {
                guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemRadiologyRestrictions"];
            }

            foreach (var chkbox in chkboxes.Where(c => c.Checked))
            {
                var entity = FindItem(chkbox.ID.Split('_')[1]);

                if (entity == null)
                {
                    entity = guarantorItemRestrictions.AddNew();
                }

                entity.ItemID = chkbox.ID.Split('_')[1];
                entity.ItemName = chkbox.Text;
            }

            return true;
        }

        private GuarantorItemRestrictions FindItem(string itemID)
        {
            var guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemRestrictions"];
            if (Request.QueryString["itemtype"] == ItemType.Service)
            {
                guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemServiceRestrictions"];
            }
            else if (Request.QueryString["itemtype"] == ItemType.Laboratory)
            {
                guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemLaboratoryRestrictions"];
            }
            else if (Request.QueryString["itemtype"] == ItemType.Radiology)
            {
                guarantorItemRestrictions = (GuarantorItemRestrictionsCollection)Session["collGuarantorItemRadiologyRestrictions"];
            }

            return guarantorItemRestrictions.FirstOrDefault(detail => detail.ItemID == itemID);
        }
    }
}
