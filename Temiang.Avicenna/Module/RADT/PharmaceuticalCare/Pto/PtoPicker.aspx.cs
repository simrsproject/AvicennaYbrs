using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Temiang.Avicenna.Module.RADT.PharmaceuticalCare
{
    public partial class PtoPicker : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["refid"] == "PTOA")
                Title = "Assessment Picker";
            else
                Title = "Planning Picker";

            PopulateList();
        }

        private void PopulateList()
        {
            var stdRefId = Request.QueryString["refid"];

            var itemQuery = new AppStandardReferenceItemQuery("a");
            itemQuery.Where(itemQuery.IsActive == true, itemQuery.StandardReferenceID == stdRefId);
            itemQuery.OrderBy(itemQuery.ItemID.Ascending);

            var items = new AppStandardReferenceItemCollection();
            items.Load(itemQuery);

            var groups = new AppStandardReferenceItemCollection();
            groups.Query.Where(groups.Query.StandardReferenceID == stdRefId, groups.Query.Or(groups.Query.ReferenceID.IsNull(), groups.Query.ReferenceID == string.Empty));
            groups.Query.OrderBy(groups.Query.ItemID.Ascending);
            groups.LoadAll();

            HtmlTable tab1 = new HtmlTable() { ID = "tab1", Width = "100%" },
                tab2 = new HtmlTable() { ID = "tab2", Width = "100%" },
                tab3 = new HtmlTable() { ID = "tab3", Width = "100%" },
                tab4 = new HtmlTable() { ID = "tab4", Width = "100%" },
                tab5 = new HtmlTable() { ID = "tab5", Width = "100%" };

            int count = (items.Count + groups.Count) / 5, idx = 0;
            var index = new int[4] { count, count * 2, count * 3, count * 4 };

            var id = 0;
            foreach (var group in groups)
            {
                id++;

                var label = new Label();
                label.ID = string.Format("label_{0}", group.ItemID);
                label.Font.Bold = true;
                if (id==1)
                    label.Text = group.ItemName;
                else 
                    label.Text = "<br/>"+ group.ItemName;

                var cell = new HtmlTableCell();
                cell.ID = string.Format("cell_{0}", id);
                cell.Style["background-color"] = "ButtonFace";
                cell.Style["color"] = "ButtonText";
                cell.Style["padding"] = "6px 0px 4px 4px";
                cell.Controls.Add(label);

                var row = new HtmlTableRow();
                row.ID = string.Format("group_{0}", group.ItemID);
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

                foreach (var item in items.Where(i => i.ReferenceID == group.ItemID))
                {
                    id++;
                    if (idx < index[0])
                        tab1.Rows.Add(PopulateListItem(item.ItemID, item.ItemName, id));
                    else if (idx >= index[0] && idx < index[1])
                        tab2.Rows.Add(PopulateListItem(item.ItemID, item.ItemName, id));
                    else if (idx >= index[1] && idx < index[2])
                        tab3.Rows.Add(PopulateListItem(item.ItemID, item.ItemName, id));
                    else if (idx >= index[2] && idx < index[3])
                        tab4.Rows.Add(PopulateListItem(item.ItemID, item.ItemName, id));
                    else if (idx >= index[3])
                        tab5.Rows.Add(PopulateListItem(item.ItemID, item.ItemName, id));

                    idx++;
                }
            }

            table1.Rows[0].Cells[0].Controls.Add(tab1);
            table1.Rows[0].Cells[1].Controls.Add(tab2);
            table1.Rows[0].Cells[2].Controls.Add(tab3);
            table1.Rows[0].Cells[3].Controls.Add(tab4);
            table1.Rows[0].Cells[4].Controls.Add(tab5);
        }

        private HtmlTableRow PopulateListItem(string itemID, string itemName, int id)
        {
            var check = new CheckBox();
            check.ID = string.Format("i_{0}", id);
            check.Attributes.Add("id", itemID);

            check.Text = itemName;

            var t = new HtmlTable();
            t.CellPadding = 0;
            t.CellSpacing = 0;
            {
                var r = new HtmlTableRow();
                r.Cells.Add(new HtmlTableCell());

                r.Cells[0].VAlign = "Top";
                r.Cells[0].Controls.Add(check);

                t.Rows.Add(r);
            }

            var cell = new HtmlTableCell();
            cell.ID = string.Format("cell_{0}", id);
            cell.Controls.Add(t);

            var row = new HtmlTableRow();
            row.ID = string.Format("item_{0}", itemID);
            row.Cells.Add(cell);

            return row;
        }

        private string _retVal;
        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oArg.retval= '" + Uri.EscapeUriString(_retVal??string.Empty) + "'";
        }

        public override bool OnButtonOkClicked()
        {
            var chkboxes = Helper.AllControls(table1).Where(c => c.GetType() == typeof(CheckBox) &&
                                                          c.ID.Split('_')[0] == "i")
                                                     .Select(t => ((CheckBox)t));

            if (!chkboxes.Any(c => c.Checked)) return false;


            var strb = new StringBuilder();
            foreach (var chkbox in chkboxes.Where(c => c.Checked))
            {
                strb.AppendLine(chkbox.Text);
            }

            _retVal = strb.ToString();
            return true;
        }


    }
}
