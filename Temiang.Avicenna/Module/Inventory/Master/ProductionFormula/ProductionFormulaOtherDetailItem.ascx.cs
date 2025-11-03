using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.Master
{
    public partial class ProductionFormulaOtherDetailItem : BaseUserControl
    {
        private RadComboBox CboItemID
        {
            get
            {
                return (RadComboBox)Helper.FindControlRecursive(Page, "cboItemID");
            }
        }

        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            //cboSRItemType.Enabled = false;
            txtQuantity.Value = 0;
            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                return;
            }

            ViewState["IsNewRecord"] = false;

            ComboBox.ItemProductItemsRequested(cboItemID, (String)DataBinder.Eval(DataItem, "ItemID"));

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, ProductionFormulaOtherItemMetadata.ColumnNames.ItemID);
            txtQuantity.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ProductionFormulaOtherItemMetadata.ColumnNames.Qty));

            string srItemUnit = string.Empty;
            var item = new Item();
            item.LoadByPrimaryKey(cboItemID.SelectedValue);
            if (item.SRItemType == BusinessObject.Reference.ItemType.Medical)
            {
                var x = new ItemProductMedic();
                x.LoadByPrimaryKey(cboItemID.SelectedValue);
                srItemUnit = x.SRItemUnit;
                ViewState["UnitID"] = x.SRItemUnit;
            }
            else if (item.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
            {
                var y = new ItemProductNonMedic();
                y.LoadByPrimaryKey(cboItemID.SelectedValue);
                srItemUnit = y.SRItemUnit;
                ViewState["UnitID"] = y.SRItemUnit;
            }
            else
            {
                var z = new ItemKitchen();
                z.LoadByPrimaryKey(cboItemID.SelectedValue);
                srItemUnit = z.SRItemUnit;
                ViewState["UnitID"] = z.SRItemUnit;
            }
            var asri = new AppStandardReferenceItem();
            asri.LoadByPrimaryKey("ItemUnit", srItemUnit);
            txtSRItemUnit.Text = asri.ItemName;
           
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtQuantity.Value <= 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Quantity Must Bigger than 0");
                return;
            }

            //Check Qty
            if (txtQuantity.Value == null || txtQuantity.Value < 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Qty must greaher than 0";
            }

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll =
                     (ProductionFormulaOtherItemCollection)Session["collProductionFormulaOtherItem"];

                string itemID = cboItemID.SelectedValue;
                bool isExist = false;
                foreach (ProductionFormulaOtherItem row in coll)
                {
                    if (row.ItemID.Equals(itemID))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Item {0} has exist", cboItemID.Text);
                }
            }
        }

        #region Properties for return entry value

        public String ItemID
        {
            get { return cboItemID.SelectedValue; }
        }

        public Decimal Qty
        {
            get { return Convert.ToDecimal(txtQuantity.Value); }
        }

        public String SRItemUnit
        {
            get { return ViewState["UnitID"].ToString(); }
        }

        #endregion

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            //ItemTransactionItemCollection collitem = (ItemTransactionItemCollection)Session["ProductionFormulaItems"];
            //if (collitem.Count == 0)
            //    cboSRItemType.Enabled = true;
        }

        #region ComboBox ItemID

        protected void cboItemID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");

            query.Where
                (
                    query.ItemID != CboItemID.SelectedValue,
                    query.Or
                        (
                            query.ItemID.Like(searchTextContain),
                            query.ItemName.Like(searchTextContain)
                        ),
                    query.IsActive == true, query.IsItemProduction == true
                );
            query.Select
                (
                    query.ItemID,
                    query.ItemName
                );
            query.Where(query.SRItemType.In(ItemType.Medical, ItemType.NonMedical, ItemType.Kitchen));
            query.es.Top = 20;
            query.OrderBy(query.ItemName.Ascending);

            DataTable dtb = query.LoadDataTable();

            cboItemID.DataSource = dtb;
            cboItemID.DataBind();
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.ItemItemDataBound(e);
        }

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string unitID = string.Empty;
            string unitName;
            using (new esTransactionScope())
            {
                var item = new Item();
                item.LoadByPrimaryKey(cboItemID.SelectedValue);


                if (item.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var medic = new ItemProductMedic();
                    medic.LoadByPrimaryKey(e.Value);
                    unitID = medic.SRItemUnit;
                }
                else if (item.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var nonmedic = new ItemProductNonMedic();
                    nonmedic.LoadByPrimaryKey(e.Value);
                    unitID = nonmedic.SRItemUnit;
                }
                else
                {
                    var kitchen = new ItemKitchen();
                    kitchen.LoadByPrimaryKey(e.Value);
                    unitID = kitchen.SRItemUnit;
                }

                ViewState["UnitID"] = unitID;
                var unit = new AppStandardReferenceItem();
                unit.LoadByPrimaryKey("ItemUnit", unitID);
                unitName = unit.ItemName;
            }
            txtSRItemUnit.Text = unitName;
        }

        #endregion
    }
}