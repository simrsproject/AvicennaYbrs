using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Linq;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ItemProductMedicalBalanceDetail : BaseUserControl
    {
        private object _dataItem;

        private string ItemID
        {
            get { 
                return ((RadTextBox)Helper.FindControlRecursive(Page, "txtItemID")).Text;
            }
        }

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            trItemSubBin.Visible = AppSession.Parameter.IsUsingItemSubBin;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboLocationID.Enabled = false;
            PopulateCboLocationID(cboLocationID, (String)DataBinder.Eval(DataItem, "LocationName"));
            PopulateCboSRItemBin(cboSRItemBin, (String)DataBinder.Eval(DataItem, "SRItemBin"));

            ItemProductMedic medic = new ItemProductMedic();
            medic.LoadByPrimaryKey(ItemID);
            txtSRItemUnitName.Text = medic.SRItemUnit;

            txtMinimum.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.Minimum));
            txtMaximum.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.Maximum));
            txtBalance.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.Balance));
            txtItemSubBin.Text = (String)DataBinder.Eval(DataItem, ItemBalanceMetadata.ColumnNames.ItemSubBin);
        }
        #region ComboBox LocationID
        protected void cboLocationID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboLocationID((RadComboBox)sender, e.Text);
        }

        protected void cboLocationID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ItemProductMedic medic = new ItemProductMedic();
            medic.LoadByPrimaryKey(ItemID);
            txtSRItemUnitName.Text = medic.SRItemUnit;
            cboSRItemBin.Items.Clear();
            cboSRItemBin.Text = string.Empty;
        }

        protected void cboSRItemBin_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboSRItemBin((RadComboBox)sender, e.Text);
        }

        private void PopulateCboSRItemBin(RadComboBox comboBox, string textSearch)
        {
            StandardReference
                .PopulateCboSRItemBin(comboBox, textSearch, cboLocationID.SelectedValue);
        }

        private void PopulateCboLocationID(RadComboBox comboBox, string textSearch)
        {
            string searchTextContain = string.Format("%{0}%", textSearch);
            LocationQuery query = new LocationQuery("a");
            ItemBalanceQuery bal = new ItemBalanceQuery("b");
            query.LeftJoin(bal).On(query.LocationID == bal.LocationID & bal.ItemID == ItemID);

            query.Where(
                //query.SRItemType == BusinessObject.Reference.ItemType.Medical,
                query.Or(query.LocationName.Like(searchTextContain), 
                query.LocationID.Like(searchTextContain)));
            ItemProductMedicQuery prod = new ItemProductMedicQuery("c");
            query.LeftJoin(prod).On(bal.ItemID == prod.ItemID);
            AppStandardReferenceItemQuery std = new AppStandardReferenceItemQuery("d");
            query.LeftJoin(std).On(prod.SRItemUnit == std.ItemID & std.StandardReferenceID == "ItemUnit");

            query.Select(query.LocationID, query.LocationName, bal.Balance, std.ItemName.As("Unit"));

            query.es.Top = 30;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
            if (dtb.Rows.Count > 0)
            {
                comboBox.SelectedValue = dtb.Rows[0]["LocationID"].ToString();
            }
        }

        protected void cboLocationID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["LocationName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["LocationID"].ToString();
        }

        protected void cboSRItemBin_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        #endregion

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check Entry LocationID
            LocationQuery qrLoc = new LocationQuery();
            qrLoc.es.Top = 1;
            qrLoc.Where(qrLoc.LocationName == cboLocationID.Text);
            Location loc = new Location();
            if (!loc.Load(qrLoc))
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Selected location not valid, please select exist location";
                return;
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ItemBalanceCollection coll =
                    (ItemBalanceCollection)Session["collItemBalance"];

                if(coll.Where(x => x.LocationID.Equals(cboLocationID.SelectedValue)).Any())
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", cboLocationID.SelectedValue);
                }
            }
        }

        #region Properties for return entry value
        public String LocationID
        {
            get { return cboLocationID.SelectedValue; }
        }
        public String LocationName
        {
            get { return cboLocationID.Text; }
        }
        public String SRItemBin
        {
            get { return cboSRItemBin.SelectedValue; }
        }
        public String SRItemBinName
        {
            get { return cboSRItemBin.Text; }
        }

        public String SRItemUnitName
        {
            get { return txtSRItemUnitName.Text; }
        }
        public Decimal Minimum
        {
            get { return Convert.ToDecimal(txtMinimum.Value); }
        }
        public Decimal Maximum
        {
            get { return Convert.ToDecimal(txtMaximum.Value); }
        }
        public String ItemSubBin
        {
            get { return txtItemSubBin.Text; }
        }
        #endregion
    }
}
