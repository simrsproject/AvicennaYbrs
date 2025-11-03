using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.BudgetingByItem
{
    public partial class BudgetingDetailItem : BaseUserControl
    {
        private object _dataItem;

        private string BudgetingNo
        {
            get
            {
                //return Request.QueryString["bno"];
                return (Helper.FindControlRecursive(Page, "txtBudgetingNo") as RadTextBox).Text;
            }
        }
        private int Revision
        {
            get
            {
                //return System.Convert.ToInt32(Request.QueryString["rev"]);
                return System.Convert.ToInt32( (Helper.FindControlRecursive(Page, "txtRev") as RadTextBox).Text);
            }
        }
        private int ChartOfAccountID
        {
            get
            {
                //return System.Convert.ToInt32(Request.QueryString["coaid"]);
                return 0;
            }
        }
        private BudgetingDetailItemCollection BudgetingDetailItems
        {
            get
            {
                return Session["BudgetingDetailItems"] as BudgetingDetailItemCollection;
            }
            set
            {
                Session["BudgetingDetailItems"] = value;
            }
        }

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboItemID.Enabled = false;

            ItemQuery item = new ItemQuery();
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where(item.ItemID == (String)DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.ItemID));

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.ItemID);

            txtQty01.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth01));
            txtQty02.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth02));
            txtQty03.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth03));
            txtQty04.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth04));
            txtQty05.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth05));
            txtQty06.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth06));
            txtQty07.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth07));
            txtQty08.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth08));
            txtQty09.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth09));
            txtQty10.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth10));
            txtQty11.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth11));
            txtQty12.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.QtyMonth12));

            cboItemID_SelectedIndexChanged(cboItemID,
                new RadComboBoxSelectedIndexChangedEventArgs(cboItemID.SelectedItem.Text, cboItemID.SelectedItem.Text, cboItemID.SelectedValue, cboItemID.SelectedValue));
            var srItemUnit = (String)DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.SRItemUnit);
            
            if (cboSRItemUnit.FindItemByValue(srItemUnit) != null) {
                cboSRItemUnit.SelectedValue = srItemUnit;
            }

            txtConversion.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.ConversionFactor));

            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.Price));

            chkIsAsset.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, BudgetingDetailItemMetadata.ColumnNames.IsAsset));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                if (BudgetingDetailItems.Where(x => ((x.BudgetingNo == BudgetingNo && x.Revision == Revision) || x.es.IsAdded) &&
                 x.ChartOfAccountID == ChartOfAccountID).Where(y => y.ItemID == cboItemID.SelectedValue).Any()) {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item ID : {0} already exist", cboItemID.SelectedValue);
                }
            }
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var item = new ItemQuery("a");
            item.Where(item.SRItemType.In("11", "21", "81"), item.IsActive == 1);
            item.es.Top = 10;
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where
                (
                    item.Or
                        (
                            item.ItemID.Like(searchTextContain),
                            item.ItemName.Like(searchTextContain)
                        ),
                    item.IsActive == true
                );
            item.OrderBy(item.ItemID.Ascending);

            cboItemID.DataSource = item.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }


        protected void cboItemID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSRItemUnit.Items.Clear();
            if (cboItemID.SelectedValue != string.Empty)
            {
                var i = new Item();
                if (i.LoadByPrimaryKey(cboItemID.SelectedValue))
                {
                    if ((new string[] { "11", "21", "81" }).Contains(i.SRItemType))
                    {
                        switch (i.SRItemType)
                        {
                            case "11":
                                {
                                    var im = new ItemProductMedic();
                                    if (im.LoadByPrimaryKey(i.ItemID))
                                    {
                                        cboSRItemUnit.Items.Add(new RadComboBoxItem(im.SRItemUnit, im.SRItemUnit));
                                        cboSRItemUnit.Items.Add(new RadComboBoxItem(im.SRPurchaseUnit, im.SRPurchaseUnit));
                                    }
                                    break;
                                }
                            case "21":
                                {
                                    var inm = new ItemProductNonMedic();
                                    if (inm.LoadByPrimaryKey(i.ItemID))
                                    {
                                        cboSRItemUnit.Items.Add(new RadComboBoxItem(inm.SRItemUnit, inm.SRItemUnit));
                                        cboSRItemUnit.Items.Add(new RadComboBoxItem(inm.SRPurchaseUnit, inm.SRPurchaseUnit));
                                    }
                                    break;
                                }
                            case "81":
                                {
                                    var ik = new ItemKitchen();
                                    if (ik.LoadByPrimaryKey(i.ItemID))
                                    {
                                        cboSRItemUnit.Items.Add(new RadComboBoxItem(ik.SRItemUnit, ik.SRItemUnit));
                                        cboSRItemUnit.Items.Add(new RadComboBoxItem(ik.SRPurchaseUnit, ik.SRPurchaseUnit));
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        protected void cboSRItemUnit_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtConversion.Value = 1;
            if (!string.IsNullOrEmpty(cboItemID.SelectedValue)) {
                if (!string.IsNullOrEmpty(cboSRItemUnit.SelectedValue))
                {
                    var item = new Item();
                    if (item.LoadByPrimaryKey(cboItemID.SelectedValue))
                    {
                        switch (item.SRItemType) {
                            case "11": {
                                    var im = new ItemProductMedic();
                                    if (im.LoadByPrimaryKey(item.ItemID)) {
                                        if (im.SRPurchaseUnit == cboSRItemUnit.SelectedValue)
                                        {
                                            txtConversion.Value = System.Convert.ToDouble(im.ConversionFactor ?? 1);
                                        }
                                    }
                                    break;
                                }
                            case "21": {
                                    var inm = new ItemProductNonMedic();
                                    if (inm.LoadByPrimaryKey(item.ItemID))
                                    {
                                        if (inm.SRPurchaseUnit == cboSRItemUnit.SelectedValue)
                                        {
                                            txtConversion.Value = System.Convert.ToDouble(inm.ConversionFactor ?? 1);
                                        }
                                    }
                                    break;
                                }
                            case "81": {
                                    var ik = new ItemKitchen();
                                    if (ik.LoadByPrimaryKey(item.ItemID))
                                    {
                                        if (ik.SRPurchaseUnit == cboSRItemUnit.SelectedValue)
                                        {
                                            txtConversion.Value = System.Convert.ToDouble(ik.ConversionFactor ?? 1);
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public String ItemName
        {
            get { return cboItemID.Text; }
        }

        public Decimal QtyMonth01
        {
            get { return Convert.ToDecimal(txtQty01.Value); }
        }
        public Decimal QtyMonth02
        {
            get { return Convert.ToDecimal(txtQty02.Value); }
        }
        public Decimal QtyMonth03
        {
            get { return Convert.ToDecimal(txtQty03.Value); }
        }
        public Decimal QtyMonth04
        {
            get { return Convert.ToDecimal(txtQty04.Value); }
        }
        public Decimal QtyMonth05
        {
            get { return Convert.ToDecimal(txtQty05.Value); }
        }
        public Decimal QtyMonth06
        {
            get { return Convert.ToDecimal(txtQty06.Value); }
        }
        public Decimal QtyMonth07
        {
            get { return Convert.ToDecimal(txtQty07.Value); }
        }
        public Decimal QtyMonth08
        {
            get { return Convert.ToDecimal(txtQty08.Value); }
        }
        public Decimal QtyMonth09
        {
            get { return Convert.ToDecimal(txtQty09.Value); }
        }
        public Decimal QtyMonth10
        {
            get { return Convert.ToDecimal(txtQty10.Value); }
        }
        public Decimal QtyMonth11
        {
            get { return Convert.ToDecimal(txtQty11.Value); }
        }
        public Decimal QtyMonth12
        {
            get { return Convert.ToDecimal(txtQty12.Value); }
        }

        public bool IsAsset {
            get { return chkIsAsset.Checked; }
        }

        public Decimal Qty
        {
            //get { return Convert.ToDecimal(txtQty.Value); }
            get {
                return QtyMonth01 + QtyMonth02 + QtyMonth03 +
                    QtyMonth04 + QtyMonth05 + QtyMonth06 +
                    QtyMonth07 + QtyMonth08 + QtyMonth09 +
                    QtyMonth10 + QtyMonth11 + QtyMonth12;
            }
        }

        public Decimal Price {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }

        public String SRItemUnit
        {
            get { return cboSRItemUnit.SelectedValue; }
        }

        public decimal ConversionFactor
        {
            get { return System.Convert.ToDecimal(txtConversion.Value ?? 1); }
        }
        #endregion

     
    }
}