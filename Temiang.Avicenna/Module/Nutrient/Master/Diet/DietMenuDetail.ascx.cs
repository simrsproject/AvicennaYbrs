using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Nutrient.Master
{
    public partial class DietMenuDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboFormOfFood, AppEnum.StandardReference.FormOfFood);

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                chkIsActive.Checked = true;
                return;
            }

            ViewState["IsNewRecord"] = false;
            cboFormOfFood.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, DietMenuMetadata.ColumnNames.FormOfFood));
            
            var mq = new MenuQuery();
            mq.Where(mq.MenuID == Convert.ToString(DataBinder.Eval(DataItem, DietMenuMetadata.ColumnNames.MenuID)));
            cboMenuID.DataSource = mq.LoadDataTable();
            cboMenuID.DataBind();
            cboMenuID.SelectedValue = Convert.ToString(DataBinder.Eval(DataItem, DietMenuMetadata.ColumnNames.MenuID));
            
            chkIsActive.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, DietMenuMetadata.ColumnNames.IsActive));
        }

        protected void cboMenuID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new MenuQuery("a");
            query.Where(query.IsActive == true, query.Or(query.MenuID == e.Text, 
                query.MenuName.Like(searchTextContain)));
            query.Select(query.MenuID, query.MenuName);

            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboMenuID.DataSource = dtb;
            cboMenuID.DataBind();
        }

        protected void cboMenuID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["MenuName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["MenuID"].ToString();
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check entry MenuID
            var mq = new MenuQuery();
            mq.es.Top = 1;
            mq.Where(mq.MenuName == cboMenuID.Text);
            var m = new BusinessObject.Menu();
            if (!m.Load(mq))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected menu not valid, please select exist menu";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (DietMenuCollection)Session["collDietMenu"];

                string id = cboFormOfFood.SelectedValue;
                bool isExist = false;
                foreach (DietMenu row in coll)
                {
                    if (row.FormOfFood.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Form Of Food: {0} has exist", cboFormOfFood.Text);
                }
            }
        }

        public String FormOfFood
        {
            get { return cboFormOfFood.SelectedValue; }
        }

        public String FormOfFoodName
        {
            get { return cboFormOfFood.Text; }
        }

        public String MenuID
        {
            get { return cboMenuID.SelectedValue; }
        }

        public String MenuName
        {
            get { return cboMenuID.Text; }
        }

        public Boolean IsActive
        {
            get { return chkIsActive.Checked; }
        }
    }
}