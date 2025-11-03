using System;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class LiquidFoodTimeGenderSettingDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.LiquidFoodSetting;

            if (!IsPostBack)
            {
                txtStandardReferenceID.Text = Request.QueryString["stdId"];
                lblItemID.Text = txtStandardReferenceID.Text == "LQ-Unit" ? "Unit" : "Class";
                txtItemID.Text = Request.QueryString["itemId"];
                txtTime.Text = Request.QueryString["time"];

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

        private LiquidFoodTimeGenderCollection LiquidFoodTimeGenders
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["collLiquidFoodTimeGender" + Request.UserHostName];
                    if (obj != null)
                    {
                        return ((LiquidFoodTimeGenderCollection)(obj));
                    }
                }

                var coll = new LiquidFoodTimeGenderCollection();
                var query = new LiquidFoodTimeGenderQuery("a");
                var foodQ = new FoodQuery("b");
                var gQ = new AppStandardReferenceItemQuery("c");
                var food2Q = new FoodQuery("d");
                query.InnerJoin(foodQ).On(query.FoodID == foodQ.FoodID);
                query.InnerJoin(gQ).On(query.Gender == gQ.ItemID && gQ.StandardReferenceID == AppEnum.StandardReference.GenderType);
                query.InnerJoin(food2Q).On(query.ChildrenFoodID == food2Q.FoodID);
                query.Select
                    (
                        query,
                        foodQ.FoodName.As("refToFood_FoodName"),
                        gQ.ItemName.As("refToAppStandardReferenceItem_ItemName"),
                        food2Q.FoodName.As("refToFood_ChildrenFoodName")
                    );
                query.Where(query.StandardReferenceID == txtStandardReferenceID.Text, query.ItemID == txtItemID.Text, query.Time == txtTime.Text);
                coll.Load(query);

                Session["collLiquidFoodTimeGender" + Request.UserHostName] = coll;
                return coll;
            }
            set
            {
                Session["collLiquidFoodTimeGender" + Request.UserHostName] = value;
            }
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdDetail.DataSource = LiquidFoodTimeGenders;
        }

        protected void grdDetail_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (editedItem == null) return;

            String g =
                Convert.ToString(
                    editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex][
                        LiquidFoodTimeGenderMetadata.ColumnNames.Gender]);
            LiquidFoodTimeGender entity = FindItem(g);
            if (entity != null)
                SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                LiquidFoodTimeGenders.Save();

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
                        LiquidFoodTimeGenderMetadata.ColumnNames.Gender]);

            LiquidFoodTimeGender entity = FindItem(g);
            if (entity != null)
            {
                entity.MarkAsDeleted();
                LiquidFoodTimeGenders.Save();
            }
        }

        protected void grdDetail_InsertCommand(object source, GridCommandEventArgs e)
        {
            var entity = LiquidFoodTimeGenders.AddNew();
            SetEntityValue(entity, e);

            using (var trans = new esTransactionScope())
            {
                LiquidFoodTimeGenders.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

            //Stay in insert mode
            e.Canceled = true;
            grdDetail.Rebind();
        }

        private LiquidFoodTimeGender FindItem(String g)
        {
            LiquidFoodTimeGenderCollection coll = LiquidFoodTimeGenders;
            LiquidFoodTimeGender retEntity = null;
            foreach (LiquidFoodTimeGender rec in coll)
            {
                if (rec.Gender.Equals(g))
                {
                    retEntity = rec;
                    break;
                }
            }
            return retEntity;
        }

        private void SetEntityValue(LiquidFoodTimeGender entity, GridCommandEventArgs e)
        {
            var userControl = (LiquidFoodTimeGenderSettingItemDetail)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            if (userControl != null)
            {
                entity.StandardReferenceID = txtStandardReferenceID.Text;
                entity.ItemID = txtItemID.Text;
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
