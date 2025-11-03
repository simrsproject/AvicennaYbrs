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
using Temiang.Avicenna.BusinessObject.Common;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class MealOrderChangeItemPickListSpecialCond : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MealOrderChangeSc;
            PopulateList();
        }

        private void PopulateList()
        {
            var foodQuery = new FoodQuery("a");
            foodQuery.Where(foodQuery.IsForSpecialCondition == true, foodQuery.IsActive == 1);

            var foods = new FoodCollection();
            foods.Load(foodQuery);

            var groups = new AppStandardReferenceItemCollection();
            groups.Query.Where(groups.Query.StandardReferenceID == AppEnum.StandardReference.FoodGroup1.ToString(),
                groups.Query.ItemID.In(foods.Select(i => i.SRFoodGroup1).Distinct()));
            groups.Query.OrderBy(groups.Query.LineNumber.Ascending);
            groups.LoadAll();

            HtmlTable tab1 = new HtmlTable() { ID = "tab1", Width = "100%" },
                tab2 = new HtmlTable() { ID = "tab2", Width = "100%" },
                tab3 = new HtmlTable() { ID = "tab3", Width = "100%" },
                tab4 = new HtmlTable() { ID = "tab4", Width = "100%" },
                tab5 = new HtmlTable() { ID = "tab5", Width = "100%" };

            int count = (foods.Count + groups.Count) / 5, idx = 0;
            var index = new int[4] { count, count * 2, count * 3, count * 4 };

            var id = 0;
            foreach (var group in groups)
            {
                id++;

                var label = new Label();
                label.ID = string.Format("label_{0}", group.ItemID);
                label.Font.Bold = true;
                label.Text = group.ItemName;

                var cell = new HtmlTableCell();
                //cell.ID = string.Format("{0}cell_{1}", rand, group.ItemGroupID);
                cell.ID = string.Format("cell_{0}", id);
                cell.Style["background-color"] = "ButtonFace";
                cell.Style["color"] = "ButtonText";
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

                foreach (var food in foods.Where(i => i.SRFoodGroup1 == group.ItemID))
                {
                    id++;
                    if (idx < index[0])
                        tab1.Rows.Add(PopulateListFood(food.FoodID, food.FoodName));
                    else if (idx >= index[0] && idx < index[1])
                        tab2.Rows.Add(PopulateListFood(food.FoodID, food.FoodName));
                    else if (idx >= index[1] && idx < index[2])
                        tab3.Rows.Add(PopulateListFood(food.FoodID, food.FoodName));
                    else if (idx >= index[2] && idx < index[3])
                        tab4.Rows.Add(PopulateListFood(food.FoodID, food.FoodName));
                    else if (idx >= index[3])
                        tab5.Rows.Add(PopulateListFood(food.FoodID, food.FoodName));

                    idx++;
                }
            }

            table1.Rows[0].Cells[0].Controls.Add(tab1);
            table1.Rows[0].Cells[1].Controls.Add(tab2);
            table1.Rows[0].Cells[2].Controls.Add(tab3);
            table1.Rows[0].Cells[3].Controls.Add(tab4);
            table1.Rows[0].Cells[4].Controls.Add(tab5);
        }

        private HtmlTableRow PopulateListFood(string foodId, string foodName)
        {
            var check = new CheckBox();
            check.ID = string.Format("i_{0}", foodId);
            check.Text = foodName;

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
            cell.ID = string.Format("cell_{0}", foodId);
            cell.Controls.Add(t);

            var row = new HtmlTableRow();
            row.ID = string.Format("item_{0}", foodId);
            row.Cells.Add(cell);

            return row;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var chkboxes = Helper.AllControls(table1).Where(c => c.GetType() == typeof(CheckBox) &&
                                                          c.ID.Split('_')[0] == "i")
                                                     .Select(t => ((CheckBox)t));

            if (!chkboxes.Any(c => c.Checked)) return false;

            var coll = (MealOrderItemCollection)Session["collMealOrderItem" + Request.UserHostName];

            foreach (var chkbox in chkboxes.Where(c => c.Checked))
            {
                var foodId = chkbox.ID.Split('_')[1];
                var entity = FindMealOrderItem(foodId) ?? coll.AddNew();
                entity.OrderNo = Request.QueryString["ono"];
                entity.SRMealSet = Request.QueryString["set"];
                entity.FoodID = foodId;
                entity.FoodName = chkbox.Text;

                var f = new Food();
                f.LoadByPrimaryKey(entity.FoodID);

                var fg = new AppStandardReferenceItem();
                fg.LoadByPrimaryKey(AppEnum.StandardReference.FoodGroup1.ToString(), f.SRFoodGroup1);

                entity.FoodGroupId = f.SRFoodGroup1;
                entity.FoodGroupName = fg.ItemName;
                entity.IsOptional = true;
                entity.IsCustom = true;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }

            return true;
        }

        private MealOrderItem FindMealOrderItem(string foodId)
        {
            var coll = (MealOrderItemCollection)Session["collMealOrderItem" + Request.UserHostName];
            return coll.FirstOrDefault(detail => detail.FoodID == foodId);
        }
    }
}