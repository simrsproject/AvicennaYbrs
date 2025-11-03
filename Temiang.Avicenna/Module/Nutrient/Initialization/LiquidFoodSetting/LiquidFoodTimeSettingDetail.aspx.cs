using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodTimeSettingDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LiquidFoodSetting;

            if (!IsPostBack)
            {
                txtStandardReferenceID.Text = Request.QueryString["stdId"];
                lblItemID.Text = txtStandardReferenceID.Text == "LQ-Unit" ? "Unit" : "Class";
                txtItemID.Text = Request.QueryString["itemId"];
                
                var std = new AppStandardReference();
                if (std.LoadByPrimaryKey(txtStandardReferenceID.Text))
                {
                    txtStandardReferenceName.Text = std.StandardReferenceName;
                }

                var stdi = new AppStandardReferenceItem();
                if (stdi.LoadByPrimaryKey(txtStandardReferenceID.Text, txtItemID.Text))
                {
                    txtItemName.Text = stdi.ItemName;
                }
            }
        }

        private LiquidFoodTimeCollection LiquidFoodTimes
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLiquidFoodTime" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LiquidFoodTimeCollection)(obj));
                    }
                }

                var coll = new LiquidFoodTimeCollection();
                var query = new LiquidFoodTimeQuery("a");
                var foodQ = new FoodQuery("b");
                var food2Q = new FoodQuery("c");
                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(food2Q).On(query.ChildrenFoodID == food2Q.FoodID);
                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        food2Q.FoodName.As("refToFood_ChildrenFoodName")
                    );
                query.Where(query.StandardReferenceID == txtStandardReferenceID.Text, query.ItemID == txtItemID.Text);
                coll.Load(query);

                Session["collLiquidFoodTime" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLiquidFoodTime" + Request.UserHostName] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = LiquidFoodTimes;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String time =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        LiquidFoodTimeMetadata.ColumnNames.Time]);
            LiquidFoodTime entity = FindItem(time);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                LiquidFoodTimes.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String time =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        LiquidFoodTimeMetadata.ColumnNames.Time]);

            LiquidFoodTime entity = FindItem(time);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                LiquidFoodTimes.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = LiquidFoodTimes.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                LiquidFoodTimes.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private LiquidFoodTime FindItem(String time)
        {
            LiquidFoodTimeCollection coll = LiquidFoodTimes;
            LiquidFoodTime retEntity = null;
            foreach (LiquidFoodTime rec in coll)
            {
                if (rec.Time.Equals(time))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(LiquidFoodTime entity, GridCommandEventArgs e)
        {
            var userControl = (LiquidFoodTimeSettingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = txtStandardReferenceID.Text;
                entity.ItemID = txtItemID.Text;
                entity.Time = userControl.Time;
                entity.FoodID = userControl.FoodID;
                entity.FoodName = userControl.FoodName;
                entity.ChildrenFoodID = userControl.ChildrenFoodID;
                entity.ChildrenFoodName = userControl.ChildrenFoodName;
                entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                entity.LastUpdateDateTime = DateTime.Now;
            }
        }     
    }
}
