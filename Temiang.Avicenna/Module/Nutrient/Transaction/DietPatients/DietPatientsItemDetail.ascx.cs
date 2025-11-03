using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Transaction
{
    public partial class DietPatientsItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        private RadComboBox CboFormOfFoodHeader
        {
            get
            { return (RadComboBox)Helper.FindControlRecursive(Page, "cboFormOfFood"); }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            chkIs09.Checked = false;
            chkIs15.Checked = false;
            chkIs21.Checked = false;

            AutoCompleteBox.Initialized(acbLiquidTime, AppEnum.StandardReference.LiquidTime, false, false, ";");

            CboFormOfFoodHeader.Enabled = false;

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtExtraQty.Value = 0;
                return;
            }

            ViewState["IsNewRecord"] = false;
            ComboBox.DietsRequested(cboDietID, (String)DataBinder.Eval(DataItem, "DietID"), string.Empty, false);
            cboDietID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.DietID));
            GetDietInformation(cboDietID.SelectedValue, false);
            txtExtraQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.ExtraQty));
            txtCalorie.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.Calorie));
            txtProtein.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.Protein));
            txtFat.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.Fat));
            txtCarbohydrate.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.Carbohydrate));
            txtSalt.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.Salt));
            txtFiber.Value = Convert.ToDouble(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.Fiber));

            var liquidTime = Convert.ToString(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.LiquidTime));
            AutoCompleteBox.SetValue(acbLiquidTime, liquidTime, acbLiquidTime.Delimiter.ToCharArray()[0]);

            //int ttl = string.IsNullOrEmpty(liquidTime) ? 0 : liquidTime.Length;
            //int idx = 0;
            //while (idx < ttl)
            //{
            //    string parseChar = liquidTime.Substring(idx, 6);
            //    switch (parseChar.Substring(0, 5))
            //    {
            //        case "09:00":
            //            chkIs09.Checked = true;
            //            break;
            //        case "15:00":
            //            chkIs15.Checked = true;
            //            break;
            //        case "21:00":
            //            chkIs21.Checked = true;
            //            break;
            //    }
            //    idx += 6;
            //}

            txtNotes.Text = Convert.ToString(DataBinder.Eval(DataItem, DietPatientItemMetadata.ColumnNames.Notes));
        }

        protected void cboDietID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.DietsRequested((RadComboBox)sender, e.Text, CboFormOfFoodHeader.SelectedValue, true);
        }

        protected void cboDietID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["DietName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["DietID"].ToString();
        }

        protected void cboDietID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            GetDietInformation(e.Value, true);
        }

        protected void txtExtraQty_TextChanged(object sender, EventArgs e)
        {
            if (txtExtraQty.Value.ToInt() == 3)
            {
                chkIs09.Checked = true;
                chkIs15.Checked = true;
                chkIs21.Checked = true;
            }
            else
            {
                chkIs09.Checked = false;
                chkIs15.Checked = false;
                chkIs21.Checked = false;
            }
        }

        protected void chkIs09_CheckedChanged(object sender, EventArgs e)
        {
            var qty = 0;
            if (chkIs09.Checked) qty += 1;
            if (chkIs15.Checked) qty += 1;
            if (chkIs21.Checked) qty += 1;
            txtExtraQty.Value = Convert.ToDouble(qty);
        }

        private void GetDietInformation(string dietId, bool isNew)
        {
            var diet = new Diet();
            if (diet.LoadByPrimaryKey(dietId))
            {
                var dietmenu = new DietMenu();
                if (dietmenu.LoadByPrimaryKey(dietId, CboFormOfFoodHeader.SelectedValue))
                {
                    var menu = new BusinessObject.Menu();
                    menu.LoadByPrimaryKey(dietmenu.MenuID);
                    txtMenu.Text = menu.MenuName;
                }
                else 
                    txtMenu.Text = string.Empty;

                txtCalorieInterval.Value = Convert.ToDouble(diet.CalorieInterval);
                txtCalorieMin.Value = Convert.ToDouble(diet.CalorieMin);
                txtCalorieMax.Value = Convert.ToDouble(diet.CalorieMax);
                txtProteinInterval.Value = Convert.ToDouble(diet.ProteinInterval);
                txtProteinMin.Value = Convert.ToDouble(diet.ProteinMin);
                txtProteinMax.Value = Convert.ToDouble(diet.ProteinMax);
                txtFatInterval.Value = Convert.ToDouble(diet.FatInterval);
                txtFatMin.Value = Convert.ToDouble(diet.FatMin);
                txtFatMax.Value = Convert.ToDouble(diet.FatMax);
                txtCarbohydrateInterval.Value = Convert.ToDouble(diet.CarbohydrateInterval);
                txtCarbohydrateMin.Value = Convert.ToDouble(diet.CarbohydrateMin);
                txtCarbohydrateMax.Value = Convert.ToDouble(diet.CarbohydrateMax);
                txtSaltInterval.Value = Convert.ToDouble(diet.SaltInterval);
                txtSaltMin.Value = Convert.ToDouble(diet.SaltMin);
                txtSaltMax.Value = Convert.ToDouble(diet.SaltMax);
                txtFiberInterval.Value = Convert.ToDouble(diet.FiberInterval);
                txtFiberMin.Value = Convert.ToDouble(diet.FiberMin);
                txtFiberMax.Value = Convert.ToDouble(diet.FiberMax);
                if (isNew)
                {
                    txtCalorie.Value = Convert.ToDouble(diet.Calorie);
                    txtProtein.Value = Convert.ToDouble(diet.Protein);
                    txtFat.Value = Convert.ToDouble(diet.Fat);
                    txtCarbohydrate.Value = Convert.ToDouble(diet.Carbohydrate);
                    txtSalt.Value = Convert.ToDouble(diet.Salt);
                    txtFiber.Value = Convert.ToDouble(diet.Fiber);
                }
            }
            else
            {
                txtMenu.Text = string.Empty;
                txtCalorieInterval.Value = 0;
                txtCalorieMin.Value = 0;
                txtCalorieMax.Value = 0;
                txtProteinInterval.Value = 0;
                txtProteinMin.Value = 0;
                txtProteinMax.Value = 0;
                txtFatInterval.Value = 0;
                txtFatMin.Value = 0;
                txtFatMax.Value = 0;
                txtCarbohydrateInterval.Value = 0;
                txtCarbohydrateMin.Value = 0;
                txtCarbohydrateMax.Value = 0;
                txtSaltInterval.Value = 0;
                txtSaltMin.Value = 0;
                txtSaltMax.Value = 0;
                txtFiberInterval.Value = 0;
                txtFiberMin.Value = 0;
                txtFiberMax.Value = 0;
                txtCalorie.Value = 0;
                txtProtein.Value = 0;
                txtFat.Value = 0;
                txtCarbohydrate.Value = 0;
                txtSalt.Value = 0;
                txtFiber.Value = 0;
            }
        }

        protected void imgCaloriePlus_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCalorie.Value + txtCalorieInterval.Value <= txtCalorieMax.Value)
                txtCalorie.Value += txtCalorieInterval.Value;
        }

        protected void imgCalorieMin_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCalorie.Value - txtCalorieInterval.Value >= txtCalorieMin.Value)
                txtCalorie.Value -= txtCalorieInterval.Value;
        }

        protected void imgProteinPlus_Click(object sender, ImageClickEventArgs e)
        {
            if (txtProtein.Value + txtProteinInterval.Value <= txtProteinMax.Value)
                txtProtein.Value += txtProteinInterval.Value;
        }

        protected void imgProteinMin_Click(object sender, ImageClickEventArgs e)
        {
            if (txtProtein.Value - txtProteinInterval.Value >= txtProteinMin.Value)
                txtProtein.Value -= txtProteinInterval.Value;
        }

        protected void imgFatPlus_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFat.Value + txtFatInterval.Value <= txtFatMax.Value)
                txtFat.Value += txtFatInterval.Value;
        }

        protected void imgFatMin_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFat.Value - txtFatInterval.Value >= txtFatMin.Value)
                txtFat.Value -= txtFatInterval.Value;
        }

        protected void imgCarbohydratePlus_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCarbohydrate.Value + txtCarbohydrateInterval.Value <= txtCarbohydrateMax.Value)
                txtCarbohydrate.Value += txtCarbohydrateInterval.Value;
        }

        protected void imgCarbohydrateMin_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCarbohydrate.Value - txtCarbohydrateInterval.Value >= txtCarbohydrateMin.Value)
                txtCarbohydrate.Value -= txtCarbohydrateInterval.Value;
        }

        protected void imgSaltPlus_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSalt.Value + txtSaltInterval.Value <= txtSaltMax.Value)
                txtSalt.Value += txtSaltInterval.Value;
        }

        protected void imgSaltMin_Click(object sender, ImageClickEventArgs e)
        {
            if (txtSalt.Value - txtSaltInterval.Value >= txtSaltMin.Value)
                txtSalt.Value -= txtSaltInterval.Value;
        }

        protected void imgFiberPlus_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFiber.Value + txtFiberInterval.Value <= txtFiberMax.Value)
                txtFiber.Value += txtFiberInterval.Value;
        }

        protected void imgFiberMin_Click(object sender, ImageClickEventArgs e)
        {
            if (txtFiber.Value - txtFiberInterval.Value >= txtFiberMin.Value)
                txtFiber.Value -= txtFiberInterval.Value;
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (DietPatientItemCollection)Session["collDietPatientItem" + Request.UserHostName];

                bool isExist = coll.Any(dietp => (dietp.DietID.Equals(cboDietID.SelectedValue)));

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Diet ID: {0} has exist", cboDietID.SelectedValue);
                    return;
                }
            }

            if (txtExtraQty.Value > 0)
            {
                var i = 0;
                //if (chkIs09.Checked) i += 1;
                //if (chkIs15.Checked) i += 1;
                //if (chkIs21.Checked) i += 1;


                char delimiter = acbLiquidTime.Delimiter.ToCharArray()[0];
                var value = acbLiquidTime.Text;
                if (!value.Contains(delimiter.ToString()))
                    value = value + delimiter;

                var stdiColl = new AppStandardReferenceItemCollection();
                stdiColl.Query.Where(stdiColl.Query.StandardReferenceID == AppEnum.StandardReference.LiquidTime.ToString());
                stdiColl.LoadAll();

                var vals = value.Split(delimiter);
                foreach (var val in vals)
                {
                    if (string.IsNullOrWhiteSpace(val)) continue;
                    i += 1;
                }

                if (txtExtraQty.Value.ToInt() != i)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = "Extra Liquid Food Qty must be equal to the number of selected Extra Liquid Food Schedule.";
                    return;
                }
            }
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            var coll = (DietPatientItemCollection)Session["collDietPatientItem" + Request.UserHostName];
            if (coll.Count == 0)
            {
                CboFormOfFoodHeader.Enabled = true;
            }
        }

        public String DietID
        {
            get { return cboDietID.SelectedValue; }
        }

        public String DietName
        {
            get { return cboDietID.Text; }
        }

        public Decimal Calorie
        {
            get { return Convert.ToDecimal(txtCalorie.Value); } 
        }

        public Decimal Protein
        {
            get { return Convert.ToDecimal(txtProtein.Value); }
        }

        public Decimal Fat
        {
            get { return Convert.ToDecimal(txtFat.Value); }
        }

        public Decimal Carbohydrate
        {
            get { return Convert.ToDecimal(txtCarbohydrate.Value); }
        }

        public Decimal Salt
        {
            get { return Convert.ToDecimal(txtSalt.Value); }
        }

        public Decimal Fiber
        {
            get { return Convert.ToDecimal(txtFiber.Value); }
        }

        public short ExtraQty
        {
            get { return Convert.ToInt16(txtExtraQty.Value); }
        }

        public String LiquidTime
        {
            get
            {
                //string charToParse = string.Empty;
                //if (chkIs09.Checked)
                //    charToParse += "09:00;";
                //if (chkIs15.Checked)
                //    charToParse += "15:00;";
                //if (chkIs21.Checked)
                //    charToParse += "21:00;";

                //return charToParse;

                return acbLiquidTime.Text;
            }
        }

        public String Notes
        {
            get { return txtNotes.Text; }
        }
    }
}