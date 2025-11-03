using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodDietTimeGenderSettingDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LiquidFoodDietSetting;

            if (!IsPostBack)
            {
                txtDietID.Text = Request.QueryString["dietId"];
                txtTime.Text = Request.QueryString["time"];

                var d = new Diet();
                d.LoadByPrimaryKey(txtDietID.Text);
                txtDietName.Text = d.DietName;
            }
        }

        private LiquidFoodDietTimeGenderCollection LiquidFoodDietTimeGenders
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLiquidFoodDietTimeGender" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LiquidFoodDietTimeGenderCollection)(obj));
                    }
                }

                var coll = new LiquidFoodDietTimeGenderCollection();
                var query = new LiquidFoodDietTimeGenderQuery("a");
                var gQ = new AppStandardReferenceItemQuery("b");
                var foodQ = new FoodQuery("c");
                var food2Q = new FoodQuery("d");
                query.InnerJoin(gQ).On(query.Gender == gQ.ItemID && gQ.StandardReferenceID == AppEnum.StandardReference.GenderType);
                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(food2Q).On(query.ChildrenFoodID == food2Q.FoodID);
                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        gQ.ItemName.As("refToAppStandardReferenceItem_ItemName"),
                        food2Q.FoodName.As("refToFood_ChildrenFoodName")
                    );
                query.Where(query.DietID == txtDietID.Text, query.Time == txtTime.Text);
                coll.Load(query);

                Session["collLiquidFoodDietTimeGender" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLiquidFoodDietTimeGender" + Request.UserHostName] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = LiquidFoodDietTimeGenders;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String g =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        LiquidFoodDietTimeGenderMetadata.ColumnNames.Gender]);
            LiquidFoodDietTimeGender entity = FindItem(g);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                LiquidFoodDietTimeGenders.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        protected void grdDetail_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            if (item == null) return;

            String g =
                Convert.ToString(
                    item.OwnerTableView.DataKeyValues[item.ItemIndex][
                        LiquidFoodDietTimeGenderMetadata.ColumnNames.Gender]);

            LiquidFoodDietTimeGender entity = FindItem(g);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                LiquidFoodDietTimeGenders.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = LiquidFoodDietTimeGenders.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                LiquidFoodDietTimeGenders.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private LiquidFoodDietTimeGender FindItem(String g)
        {
            LiquidFoodDietTimeGenderCollection coll = LiquidFoodDietTimeGenders;
            LiquidFoodDietTimeGender retEntity = null;
            foreach (LiquidFoodDietTimeGender rec in coll)
            {
                if (rec.Gender.Equals(g))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(LiquidFoodDietTimeGender entity, GridCommandEventArgs e)
        {
            var userControl = (LiquidFoodDietTimeGenderSettingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.DietID = txtDietID.Text;
                entity.Time = txtTime.Text;
                entity.Gender = userControl.Gender;
                entity.GenderName = userControl.GenderName;
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
