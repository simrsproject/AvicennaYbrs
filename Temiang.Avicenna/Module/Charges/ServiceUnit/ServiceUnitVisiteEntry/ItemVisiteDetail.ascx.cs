using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class ItemVisiteDetail : System.Web.UI.UserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var units = new ServiceUnitCollection();
            units.Query.Where(units.Query.SRRegistrationType != string.Empty, units.Query.IsActive == true);
            units.Query.Load();

            cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var unit in units)
            {
                cboServiceUnitID.Items.Add(new RadComboBoxItem(unit.ServiceUnitName, unit.ServiceUnitID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                txtQty.Value = 1;
                txtPrice.Value = 0;

                txtDiscount.ReadOnly = rblPaymentType.SelectedValue == "1";
                txtDiscount.Value = 0;

                cboServiceUnitID.SelectedValue = txtServiceUnitID.Text;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var query = new ItemQuery();
            query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ItemID));
            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.VisiteQty));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.Price));
            txtDiscount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.Discount));
            cboServiceUnitID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ServiceUnitID);
            txtExpiredDate.SelectedDate = (DateTime?)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ExpiredDate);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (TransPaymentItemVisiteCollection)Session["collTransPaymentItemVisite" + Request.UserHostName];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string id = cboItemID.SelectedValue;
                bool isExist = coll.Any(item => item.ItemID.Equals(id));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                    return;
                }
            }

            if (txtQty.Value == 0)
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = "Visite Qty must greater than zero";
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

        public int Qty
        {
            get { return Convert.ToInt32(txtQty.Value); }
        }

        public Decimal Price
        {
            get { return Convert.ToDecimal(txtPrice.Value); }
        }

        public Decimal Discount
        {
            get { return Convert.ToDecimal(txtDiscount.Value); }
        }

        public String ServiceUnitID
        {
            get { return cboServiceUnitID.SelectedValue; }
        }

        public String ServiceUnitName
        {
            get { return cboServiceUnitID.Text; }
        }

        public DateTime? ExpiredDate
        {
            get { return txtExpiredDate.SelectedDate; }
        }

        #endregion

        private RadioButtonList rblPaymentType
        {
            get { return Helper.FindControlRecursive(this.Page, "rblPaymentType") as RadioButtonList; }
        }

        private RadTextBox txtServiceUnitID
        {
            get { return Helper.FindControlRecursive(this.Page, "txtServiceUnitID") as RadTextBox; }
        }


        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                txtPrice.Value = 0D;
                return;
            }

            if (rblPaymentType.SelectedValue == "1")
            {
                txtPrice.Value = 0D;
                return;
            }

            var guarantorID = Helper.FindControlRecursive(this.Page, "txtGuarantorID").ToString();
            var regNo = Helper.FindControlRecursive(this.Page, "txtRegistrationNo").ToString();
            var date = Helper.FindControlRecursive(this.Page, "txtPaymentDate") as RadDatePicker;

            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);

            ItemTariff tariff = Helper.Tariff.GetItemTariff(date.SelectedDate.Value,
                                                            AppSession.Parameter.DefaultTariffType,
                                                            AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.OutPatientClassID, e.Value,
                                                            guarantorID, false, reg.SRRegistrationType);
            if (tariff != null)
                txtPrice.Value = Convert.ToDouble(tariff.Price);
        }

        protected void cboItemID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ItemQuery("a");
            var suis = new ServiceUnitItemServiceQuery("b");

            query.es.Top = 30;
            query.Select(query);
            query.InnerJoin(suis).On(query.ItemID == suis.ItemID && suis.ServiceUnitID == cboServiceUnitID.SelectedValue);
            query.Where(
                query.ItemName.Like(searchTextContain),
                query.SRItemType.NotIn(
                    BusinessObject.Reference.ItemType.Medical,
                    BusinessObject.Reference.ItemType.NonMedical
                    ),
                query.IsActive == true
                );
            query.OrderBy(query.ItemName.Ascending);

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }
    }
}