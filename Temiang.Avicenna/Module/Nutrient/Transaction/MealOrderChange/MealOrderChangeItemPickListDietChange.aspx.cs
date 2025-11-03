using System;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class MealOrderChangeItemPickListDietChange : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.MealOrderChangeDc;

            if (!IsPostBack)
            {
                StandardReference.Initialize(cboFormOfFood, AppEnum.StandardReference.FormOfFood);

                string regNo = Request.QueryString["regno"];
                string orderNo = Request.QueryString["ono"];
                var mo = new MealOrder();
                mo.LoadByPrimaryKey(orderNo);

                var dp = new DietPatient();
                var dpQ = new DietPatientQuery();
                dpQ.Where(dpQ.RegistrationNo == regNo,
                    dpQ.EffectiveStartDate.Date() <= mo.EffectiveDate, dpQ.IsVoid == false);
                dpQ.OrderBy(dpQ.EffectiveStartDate.Descending, dpQ.EffectiveStartTime.Descending, dpQ.TransactionNo.Descending);
                dpQ.es.Top = 1;
                dp.Load(dpQ);
                txtDietPatientNo.Text = dp.TransactionNo;

                var dpiQ = new DietPatientItemQuery("a");
                var dQ = new DietQuery("b");
                dpiQ.InnerJoin(dQ).On(dQ.DietID == dpiQ.DietID);
                dpiQ.Where(dpiQ.TransactionNo == dp.TransactionNo);
                dpiQ.Select(dpiQ.DietID, dQ.DietName);
                DataTable dpidt = dpiQ.LoadDataTable();
                cboDietID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                var dietId = string.Empty;
                foreach (DataRow row in dpidt.Rows)
                {
                    cboDietID.Items.Add(new RadComboBoxItem(row["DietName"].ToString(), row["DietID"].ToString()));
                    if (string.IsNullOrEmpty(dietId))
                        dietId = row["DietID"].ToString();
                }
                cboDietID.SelectedValue = dietId;
                cboDietID.Enabled = dpidt.Rows.Count > 1;

                StandardReference.Initialize(cboFormOfFood, AppEnum.StandardReference.FormOfFood);
                cboFormOfFood.SelectedValue = dp.FormOfFood;

                GetMenuItemId(regNo, dietId, dp.FormOfFood, mo.VersionID, mo.SeqNo);
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var srMealSet = Request.QueryString["set"];
            var menuItemId = txtMenuItemID.Text;
            var regno = Request.QueryString["regno"];

            var reg = new Registration();
            reg.LoadByPrimaryKey(regno);

            var m = new MenuItem();
            m.LoadByPrimaryKey(menuItemId);

            var mq = new MenuItemQuery();
            mq.Where(mq.MenuID == m.MenuID, mq.VersionID == m.VersionID, mq.SeqNo == m.SeqNo, mq.ClassID == reg.ChargeClassID);

            m = new MenuItem();
            m.Load(mq);

            var query = new MenuItemFoodQuery("a");
            var foodQ = new FoodQuery("b");
            var stdQ = new AppStandardReferenceItemQuery("c");
            var std2Q = new AppStandardReferenceItemQuery("d");
            query.Select(query.IsOptional, query.FoodID, foodQ.FoodName, foodQ.SRFoodGroup1, stdQ.ItemName.As("FoodGroupName"), foodQ.SRFoodGroup2, "<ISNULL(d.ItemName, 'General') AS FoodGroup2Name>");
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

            query.Where(query.MenuItemID == m.MenuItemID, query.SRMealSet == srMealSet);
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

                if (Convert.ToBoolean(src["IsOptional"]) == false)
                {
                    var ds = ((DataTable)ViewState["list"]);
                    foreach (DataRow xRow in ds.Rows)
                    {
                        if (Convert.ToBoolean(xRow["IsOptional"]) == Convert.ToBoolean(src["IsOptional"]))
                        {
                            var row = ((DataTable)ViewState["selected"]).NewRow();
                            row["FoodID"] = xRow["FoodID"];
                            row["FoodName"] = xRow["FoodName"];
                            row["SRFoodGroup1"] = src["SRFoodGroup1"];
                            row["FoodGroupName"] = xRow["FoodGroupName"];
                            row["SRFoodGroup2"] = xRow["SRFoodGroup2"];
                            row["FoodGroup2Name"] = xRow["FoodGroup2Name"];
                            row["IsOptional"] = xRow["IsOptional"];

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
                else
                {
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
                        row["IsOptional"] = src["IsOptional"];

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
                                row["IsOptional"] = xRow["IsOptional"];

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
                }

                grdSelected.Rebind();
            }
        }

        protected void grdSelected_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                var src = ((DataTable)ViewState["selected"]).Rows[e.Item.DataSetIndex];

                if (Convert.ToBoolean(src["IsOptional"]) == false)
                {
                    var dst = ((DataTable)ViewState["selected"]);
                    dst = null;
                    ViewState["selected"] = dst;
                }
                else
                {
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

            col = new DataColumn("IsOptional", typeof(bool));
            tbl.Columns.Add(col);

            dataTable = tbl;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (MealOrderItemCollection)Session["collMealOrderItem" + Request.UserHostName];

            var tbl = (DataTable)ViewState["selected"];

            foreach (DataRow row in tbl.Rows)
            {
                var entity = FindMealOrderItem(row["FoodID"].ToString()) ?? coll.AddNew();

                entity.OrderNo = Request.QueryString["no"];
                entity.SRMealSet = Request.QueryString["set"];
                entity.FoodID = row["FoodID"].ToString();
                entity.FoodName = row["FoodName"].ToString();
                entity.FoodGroupId = row["SRFoodGroup1"].ToString();
                entity.FoodGroupName = row["FoodGroupName"].ToString();
                entity.IsOptional = Convert.ToBoolean(row["IsOptional"]);
                entity.IsCustom = false;
                entity.DietPatientNo = txtDietPatientNo.Text;
                entity.DietID = cboDietID.SelectedValue;
                entity.MenuID = txtMenuID.Text;
                entity.MenuItemID = txtMenuItemID.Text;
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
            return "oWnd.argument.command = 'rebind'";
        }

        protected void cboDietID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string regNo = Request.QueryString["regno"];
            string orderNo = Request.QueryString["ono"];
            var mo = new MealOrder();
            mo.LoadByPrimaryKey(orderNo);

            var dp = new DietPatient();
            dp.LoadByPrimaryKey(txtDietPatientNo.Text);

            GetMenuItemId(regNo, cboDietID.SelectedValue, dp.FormOfFood, mo.VersionID, mo.SeqNo);

            grdList.Rebind();
        }

        private void GetMenuItemId(string regNo, string dietId, string formOfFood, string versionId, string seqNo)
        {
            if (!string.IsNullOrEmpty(dietId))
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(regNo);

                var diet = new DietMenu();
                diet.LoadByPrimaryKey(dietId, formOfFood);
                txtMenuID.Text = diet.MenuID;

                var menu = new Menu();
                txtMenu.Text = menu.LoadByPrimaryKey(diet.MenuID) ? menu.MenuName : string.Empty;

                var menuItemQ = new MenuItemQuery();
                menuItemQ.es.Top = 1;
                menuItemQ.Where(menuItemQ.MenuID == txtMenuID.Text,
                    menuItemQ.VersionID == versionId, menuItemQ.SeqNo == seqNo,
                    menuItemQ.ClassID == reg.ChargeClassID, menuItemQ.IsActive == true);
                menuItemQ.OrderBy(menuItemQ.LastUpdateDateTime.Descending);

                var menuItem = new MenuItem();
                txtMenuItemID.Text = menuItem.Load(menuItemQ) ? menuItem.MenuItemID : string.Empty;
            }
            else
            {
                txtMenuID.Text = string.Empty;
                txtMenu.Text = string.Empty;
                txtMenuItemID.Text = string.Empty;
            }
        }
    }
}