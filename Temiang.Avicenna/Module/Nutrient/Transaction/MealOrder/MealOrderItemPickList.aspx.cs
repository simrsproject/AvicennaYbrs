using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class MealOrderItemPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MealOrder;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var srMealSet = Request.QueryString["set"];
            var menuItemId = Request.QueryString["menu"];

            var query = new MenuItemFoodQuery("a");
            var foodQ = new FoodQuery("b");
            var stdQ = new AppStandardReferenceItemQuery("c");
            var std2Q = new AppStandardReferenceItemQuery("d");
            query.Select(query.FoodID, foodQ.FoodName, foodQ.SRFoodGroup1, stdQ.ItemName.As("FoodGroupName"));
            query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
            query.InnerJoin(stdQ).On(foodQ.SRFoodGroup1 == stdQ.ItemID &
                                     stdQ.StandardReferenceID == AppEnum.StandardReference.FoodGroup1);
            if (AppSession.Parameter.IsFoodSelectedByMenuItemFoodGroup)
            {
                query.Select(query.SRMenuItemFoodGroup.As("SRFoodGroup2"), "<ISNULL(d.ItemName, 'Standard') AS FoodGroup2Name>");
                query.LeftJoin(std2Q).On(query.SRMenuItemFoodGroup == std2Q.ItemID &
                                     std2Q.StandardReferenceID == AppEnum.StandardReference.MenuItemFoodGroup);
                query.OrderBy(query.SRMenuItemFoodGroup.Ascending, foodQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
            }
            else if (AppSession.Parameter.IsFoodSelectedByType)
            {
                query.Select(foodQ.SRFoodGroup2, "<ISNULL(d.ItemName, 'General') AS FoodGroup2Name>");
                query.LeftJoin(std2Q).On(foodQ.SRFoodGroup2 == std2Q.ItemID &
                                     std2Q.StandardReferenceID == AppEnum.StandardReference.FoodGroup2);
                query.OrderBy(foodQ.SRFoodGroup2.Ascending, foodQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
            }
            else
            {
                query.Select(foodQ.SRFoodGroup1.As("SRFoodGroup2"), stdQ.ItemName.As("FoodGroup2Name"));
                query.OrderBy(foodQ.SRFoodGroup1.Ascending, query.FoodID.Ascending);
            }

            query.Where(query.MenuItemID == menuItemId, query.SRMealSet == srMealSet, query.IsOptional == true);
            if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSSA")
                query.Where(query.FoodID.NotIn(AppSession.Parameter.LiquidFoodId, AppSession.Parameter.BlenderizedFoodId));

            if (!string.IsNullOrEmpty(txtFilter.Text))
            {
                string searchTextContain = string.Format("%{0}%", txtFilter.Text);
                query.Where(foodQ.FoodName.Like(searchTextContain));
            }
                
            DataTable tbl = query.LoadDataTable();

            ViewState["list"] = tbl;
            grdList.DataSource = tbl;
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                var src = ((DataTable)ViewState["list"]).Rows[e.Item.DataSetIndex];

                bool isSelectedByGroup = false;
                if ((AppSession.Parameter.IsFoodSelectedByType || AppSession.Parameter.IsFoodSelectedByMenuItemFoodGroup) &&
                    !string.IsNullOrEmpty(src["SRFoodGroup2"].ToString()) && !src["SRFoodGroup2"].ToString().Equals(AppSession.Parameter.MenuItemFoodGroupStandard) && 
                    !AppSession.Parameter.FoodGroupOneDishMeal.Contains(src["SRFoodGroup1"].ToString()))
                {
                    isSelectedByGroup = true;
                }

                if (!isSelectedByGroup)
                {
                    var row = ((DataTable)ViewState["selected"]).NewRow();
                    row["FoodID"] = src["FoodID"];
                    row["FoodName"] = src["FoodName"];
                    row["SRFoodGroup1"] = src["SRFoodGroup1"];
                    row["FoodGroupName"] = src["FoodGroupName"];
                    row["SRFoodGroup2"] = src["SRFoodGroup2"];
                    row["FoodGroup2Name"] = src["FoodGroup2Name"];

                    var dst = ((DataTable)ViewState["selected"]);

                    bool exist = dst.Rows.Cast<DataRow>().Where(bar => bar["FoodID"].ToString() == src["FoodID"].ToString()).Any();

                    if (!exist)
                        dst.Rows.Add(row);
                    else
                    {
                        dst.AcceptChanges();
                        ViewState["selected"] = dst;
                    }
                }
                else
                {
                    var ds = ((DataTable)ViewState["list"]);
                    foreach (DataRow xRow in ds.Rows)
                    {
                        if (xRow["SRFoodGroup2"].ToString() == src["SRFoodGroup2"].ToString())
                        {
                            var row = ((DataTable)ViewState["selected"]).NewRow();
                            row["FoodID"] = xRow["FoodID"];
                            row["FoodName"] = xRow["FoodName"];
                            row["SRFoodGroup1"] = src["SRFoodGroup1"];
                            row["FoodGroupName"] = xRow["FoodGroupName"];
                            row["SRFoodGroup2"] = xRow["SRFoodGroup2"];
                            row["FoodGroup2Name"] = xRow["FoodGroup2Name"];

                            var dst = ((DataTable)ViewState["selected"]);

                            bool exist = dst.Rows.Cast<DataRow>().Where(bar => bar["FoodID"].ToString() == xRow["FoodID"].ToString()).Any();

                            if (!exist)
                                dst.Rows.Add(row);
                            else
                            {
                                dst.AcceptChanges();
                                ViewState["selected"] = dst;
                            }
                        }
                    }
                }

                grdSelected.Rebind();
            }
        }

        protected void grdSelected_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                var src = ((DataTable)ViewState["selected"]).Rows[e.Item.DataSetIndex];

                bool isSelectedByGroup = false;
                if ((AppSession.Parameter.IsFoodSelectedByType || AppSession.Parameter.IsFoodSelectedByMenuItemFoodGroup) &&
                    !string.IsNullOrEmpty(src["SRFoodGroup2"].ToString()) && !src["SRFoodGroup2"].ToString().Equals(AppSession.Parameter.MenuItemFoodGroupStandard) && 
                    !AppSession.Parameter.FoodGroupOneDishMeal.Contains(src["SRFoodGroup1"].ToString()))
                {
                    isSelectedByGroup = true;
                }

                if (!isSelectedByGroup)
                {
                    var row = ((DataTable)ViewState["selected"]).Rows[e.Item.DataSetIndex];
                    var dst = ((DataTable)ViewState["selected"]);

                    bool exist = dst.Rows.Cast<DataRow>().Where(bar => bar["FoodID"].ToString() == row["FoodID"].ToString()).Any();

                    if (exist)
                    {
                        dst.Rows.Remove(row);
                        ViewState["selected"] = dst;
                    }
                }
                else //delete semua 
                {
                    var dst = ((DataTable)ViewState["selected"]);
                    dst = null;
                    ViewState["selected"] = dst;
                }

                grdSelected.Rebind();
            }
        }

        protected void grdSelected_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["selected"] == null)
            {
                DataTable tbl;
                SetDataColumnSelected(out tbl);

                ViewState["selected"] = tbl;
                grdSelected.DataSource = tbl;
            }
            else
                grdSelected.DataSource = (DataTable)ViewState["selected"];
        }

        private static void SetDataColumnSelected(out DataTable dataTable)
        {
            var tbl = new DataTable();

            var col = new DataColumn("FoodID", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("FoodName", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("SRFoodGroup1", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("FoodGroupName", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("SRFoodGroup2", typeof(string));
            tbl.Columns.Add(col);

            col = new DataColumn("FoodGroup2Name", typeof(string));
            tbl.Columns.Add(col);

            dataTable = tbl;
        }

        public override bool OnButtonOkClicked()
        {
            ViewState["MealSet" + Request.UserHostName] = Request.QueryString["set"];

            var coll = (MealOrderItemCollection)Session["collMealOrderItem" + Request.UserHostName];

            var tbl = (DataTable)ViewState["selected"];

            if (AppSession.Parameter.IsUseStandardMealMenuForAllClass)
            {
                var list = coll.Where(c => c.FoodGroupId == AppSession.Parameter.FoodGroupOneCarbohydrate && c.IsOptional == true);
                if (list.Count() > 0)
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Optional food already selected. Check back your food list.";

                    return false;
                }

                if (tbl.Rows.Count > 1)
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Selected optional food should not be more than 1.";

                    return false;
                }
            }
            if (AppSession.Parameter.FoodGroupOneDishMeal.Length > 0)
            {
                try
                {
                    DataTable tblFiltered = tbl.AsEnumerable()
                        .Where(row => AppSession.Parameter.FoodGroupOneDishMeal.Contains(row.Field<String>("SRFoodGroup1")))
                        .CopyToDataTable();
                    if (tblFiltered.Rows.Count > 1)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Selected optional food should not be more than 1.";

                        return false;
                    }
                }
                catch { }
            }
            if (AppSession.Parameter.IsFoodSelectedByType || AppSession.Parameter.IsFoodSelectedByMenuItemFoodGroup)
            {
                try
                {
                    DataTable tblGroup = tbl.AsEnumerable()
                        .Where(row => row.Field<String>("SRFoodGroup2") != string.Empty)
                        .GroupBy(g => new { Col1 = g["SRFoodGroup2"] })
                        .Select(g => g.OrderBy(r => r["SRFoodGroup2"]).First())
                        .CopyToDataTable();
                    if (tblGroup.Rows.Count > 1)
                    {
                        pnlInfo.Visible = true;
                        lblInfo.Text = "Selected optional menu item group should not be more than 1.";

                        return false;
                    }
                }
                catch { }
            }

            foreach (DataRow row in tbl.Rows)
            {
                var entity = FindMealOrderItem(row["FoodID"].ToString()) ?? coll.AddNew();

                entity.OrderNo = Request.QueryString["no"];
                entity.SRMealSet = Request.QueryString["set"];
                entity.FoodID = row["FoodID"].ToString();
                entity.FoodName = row["FoodName"].ToString();
                entity.FoodGroupId = row["SRFoodGroup1"].ToString();
                entity.FoodGroupName = row["FoodGroupName"].ToString();
                entity.IsOptional = true;
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

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind|" + (string)ViewState["MealSet" + Request.UserHostName] + "'";
        }
    }
}
