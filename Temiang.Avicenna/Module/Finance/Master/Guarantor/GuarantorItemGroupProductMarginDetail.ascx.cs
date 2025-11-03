using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class GuarantorItemGroupProductMarginDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            ComboBox.PopulateWithItemProductMargin(cboMarginID);
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;
                txtMarginPercentage.Value = 0;
                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemGroupID.Enabled = false;

            PopulateCboItemGroupID(cboItemGroupID, (String)DataBinder.Eval(DataItem, "ItemGroupID"), false);
            cboItemGroupID.SelectedValue = (String)DataBinder.Eval(DataItem, "ItemGroupID");
            cboMarginID.SelectedValue= (String)DataBinder.Eval(DataItem, "MarginID");
            txtMarginPercentage.Value = Convert.ToDouble(DataBinder.Eval(DataItem, "MarginPercentage"));
        }

        #region ComboBox ServiceUnitID
        protected void cboItemGroupID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboItemGroupID((RadComboBox)sender, e.Text, true);
        }

        private void PopulateCboItemGroupID(RadComboBox comboBox, string textSearch, bool isNew)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            var query = new ItemGroupQuery("a");
            var itype = new AppStandardReferenceItemQuery("b");
            query.InnerJoin(itype).On(itype.StandardReferenceID == AppEnum.StandardReference.ItemType.ToString() && itype.ItemID == query.SRItemType);
            query.Where(
                query.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
            if (isNew)
            {
                query.Where(query.IsActive == true, 
                    query.ItemGroupName.Like(searchTextContain));
            }
            else
                query.Where(query.ItemGroupID == textSearch);
            query.Select(query.ItemGroupID, query.ItemGroupName, itype.ItemName.As("ItemTypeName"));

            query.es.Top = 10;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboItemGroupID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemGroupName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemGroupID"].ToString();
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (GuarantorItemGroupProductMarginCollection)Session["collGuarantorItemGroupProductMargin"];

                string id = cboItemGroupID.SelectedValue;
                if (string.IsNullOrEmpty(id))
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item Group required.");
                    return;
                }

                bool isExist = false;
                foreach (GuarantorItemGroupProductMargin row in coll)
                {
                    if (row.ItemGroupID.Equals(id))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value
        public String ItemGroupID
        {
            get { return cboItemGroupID.SelectedValue; }
        }
        public String ItemGroupName
        {
            get { return cboItemGroupID.Text; }
        }
        public String MarginID
        {
            get { return cboMarginID.SelectedValue; }
        }
        public String MarginName
        {
            get { return cboMarginID.Text; }
        }
        public Decimal MarginPercentage
        {
            get { return Convert.ToDecimal(txtMarginPercentage.Value); }
        }
        #endregion
    }
}