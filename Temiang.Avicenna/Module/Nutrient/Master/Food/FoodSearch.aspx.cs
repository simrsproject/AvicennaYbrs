using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class FoodSearch : BasePageDialog
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (FormType == "sales")
                ProgramID = AppConstant.Program.FoodCafetaria;
            else
                ProgramID = AppConstant.Program.Food;
            
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRFoodGroup2, AppEnum.StandardReference.FoodGroup2, FormType == "sales" ? "U" : "P");
                StandardReference.InitializeIncludeSpace(cboSRFoodGroup1, AppEnum.StandardReference.FoodGroup1, true);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                RestoreValueFromCookie();
        }
        public override bool OnButtonOkClicked()
        {
            var query = new FoodQuery("a");
            var std = new AppStandardReferenceItemQuery("b");
            var std2 = new AppStandardReferenceItemQuery("c");
            var std3 = new AppStandardReferenceItemQuery("d");
            query.InnerJoin(std).On(query.SRFoodGroup1 == std.ItemID && std.StandardReferenceID == "FoodGroup1");
            query.InnerJoin(std2).On(query.SRItemUnit == std2.ItemID && std2.StandardReferenceID == "ItemUnit");
            query.LeftJoin(std3).On(query.SRFoodGroup2 == std3.ItemID && std3.StandardReferenceID == "FoodGroup2");
            query.Select
                (
                    query.FoodID,
                    query.FoodName,
                    query.Weight,
                    query.SRFoodGroup2,
                     @"<ISNULL(d.ItemName, 'General') AS FoodType>",
                    std.ItemName.As("FoodGroup1"),
                    std2.ItemName.As("ItemUnit"),
                    query.IsPackage,
                    query.IsActive
                );
            if (FormType == "sales")
                query.Where(query.IsSalesAvailable == true);
            else
                query.Where(query.IsSalesAvailable == false);

            if (!string.IsNullOrEmpty(txtFoodID.Text))
            {
                if (cboFilterFoodID.SelectedIndex == 1)
                    query.Where(query.FoodID == txtFoodID.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFoodID.Text);
                    query.Where(query.FoodID.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(txtFoodName.Text))
            {
                if (cboFilterFoodName.SelectedIndex == 1)
                    query.Where(query.FoodName == txtFoodName.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtFoodName.Text);
                    query.Where(query.FoodName.Like(searchTextContain));
                }
            }
            if (!string.IsNullOrEmpty(cboSRFoodGroup1.SelectedValue))
            {
                query.Where(query.SRFoodGroup1 == cboSRFoodGroup1.SelectedValue);
            }
            if (!string.IsNullOrEmpty(cboSRFoodGroup2.SelectedValue))
            {
                query.Where(query.SRFoodGroup2 == cboSRFoodGroup2.SelectedValue);
            }

            query.OrderBy(query.SRFoodGroup1.Ascending, query.FoodID.Ascending);
            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}
