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
    public partial class ItemDownPaymentVisite : System.Web.UI.UserControl
    {
        private object _dataItem;

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

                txtQty.Value = 0D;
                txtPrice.Value = 0D;
                txtDiscount.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;

            var unitq = new ServiceUnitQuery();
            unitq.Where(unitq.ServiceUnitID == (String)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ServiceUnitID));
            cboServiceUnitID.DataSource = unitq.LoadDataTable();
            cboServiceUnitID.DataBind();

            cboServiceUnitID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ServiceUnitID);

            var query = new ItemQuery();
            query.Where(query.ItemID == (String)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ItemID));
            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();

            cboItemID.SelectedValue = (String)DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.ItemID);
            txtQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.VisiteQty));
            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.Price));
            txtDiscount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPaymentItemVisiteMetadata.ColumnNames.Discount));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var coll = (TransPaymentItemVisiteCollection)Session["DownPayment:TransPaymentItemVisite" + Request.UserHostName];

            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                string id = cboItemID.SelectedValue;
                bool isExist = coll.Any(item => item.ItemID.Equals(id));
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("ID: {0} has exist", id);
                }
            }
        }

        #region Properties for return entry value

        public String ServiceUnitID
        {
            get { return cboServiceUnitID.SelectedValue; }
        }

        public String ServiceUnitName
        {
            get { return cboServiceUnitID.Text; }
        }

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

        #endregion

        protected void cboItemID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
            {
                txtPrice.Value = 0D;
                return;
            }

            //db:20230723 --> guarantor & class sesuai default 
            //var guarantorID = Helper.FindControlRecursive(this.Page, "txtGuarantorID").ToString();
            //var regNo = Helper.FindControlRecursive(this.Page, "txtRegistrationNo").ToString();

            //var reg = new Registration();
            //reg.LoadByPrimaryKey(regNo);

            //var date = Helper.FindControlRecursive(this.Page, "txtPaymentDate") as RadDatePicker;
            //ItemTariff tariff = Helper.Tariff.GetItemTariff(date.SelectedDate.Value,
            //                                                AppSession.Parameter.DefaultTariffType,
            //                                                AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, e.Value,
            //                                                guarantorID, false, reg.SRRegistrationType);

            var guarantorID = AppSession.Parameter.SelfGuarantor;
            
            var date = Helper.FindControlRecursive(this.Page, "txtPaymentDate") as RadDatePicker;
            ItemTariff tariff = (Helper.Tariff.GetItemTariff(date.SelectedDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.OutPatientClassID, AppSession.Parameter.OutPatientClassID, e.Value, guarantorID, false, AppConstant.RegistrationType.OutPatient) ??
                Helper.Tariff.GetItemTariff(date.SelectedDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, AppSession.Parameter.OutPatientClassID, e.Value, guarantorID, false, AppConstant.RegistrationType.OutPatient));

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
            var unitQ = new ServiceUnitItemServiceQuery("b");
            query.es.Top = 20;
            query.InnerJoin(unitQ).On(unitQ.ServiceUnitID == cboServiceUnitID.SelectedValue && unitQ.ItemID == query.ItemID);
            query.Where(
                    query.ItemName.Like(searchTextContain),
                    query.IsActive == true
                );
            query.OrderBy(query.ItemName.Ascending);

            cboItemID.DataSource = query.LoadDataTable();
            cboItemID.DataBind();
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboItemID.Items.Clear();
            cboItemID.SelectedValue = string.Empty;
            cboItemID.Text = string.Empty;
        }

        protected void cboServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void cboServiceUnitID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ServiceUnitQuery();
            query.es.Top = 20;
            query.Where(
                    query.ServiceUnitName.Like(searchTextContain),
                    query.SRRegistrationType == AppConstant.RegistrationType.OutPatient,
                    query.IsActive == true
                );
            query.OrderBy(query.ServiceUnitName.Ascending);

            cboServiceUnitID.DataSource = query.LoadDataTable();
            cboServiceUnitID.DataBind();
        }
    }
}