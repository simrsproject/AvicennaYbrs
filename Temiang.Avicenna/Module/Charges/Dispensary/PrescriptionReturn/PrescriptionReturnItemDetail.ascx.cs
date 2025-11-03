using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionReturnItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboDiscountReason, AppEnum.StandardReference.DiscountReason);
            TransPrescriptionItemCollection coll = (TransPrescriptionItemCollection)Session["collTransPrescriptionItem" + Request.UserHostName];

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                if (!coll.HasData)
                    ViewState["SequenceNo"] = "001";
                else
                {
                    int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                    ViewState["SequenceNo"] = string.Format("{0:000}", seqNo);
                }

                return;
            }
            ViewState["IsNewRecord"] = false;
            ViewState["SequenceNo"] = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SequenceNo);

            string inter = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemInterventionID);
            txtItemID.Text = string.IsNullOrEmpty(inter) ?
                (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ItemID) : inter;
            var item = new Item();
            item.LoadByPrimaryKey(txtItemID.Text);
            lblItemName.Text = item.ItemName;

            txtReturnQty.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ResultQty));

            var detail = new TransPrescriptionItem();
            detail.LoadByPrimaryKey((String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.PrescriptionNo),
                (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SequenceNo));

            var retHd = new TransPrescriptionQuery("a");
            var retDt = new TransPrescriptionItemQuery("b");

            retDt.InnerJoin(retHd).On(retHd.PrescriptionNo == retDt.PrescriptionNo);
            retDt.Where
                (
                    retHd.ReferenceNo == (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.PrescriptionNo),
                    retHd.IsApproval == true,
                    retDt.ItemID == txtItemID.Text,
                    retDt.IsApprove == true
                );

            var ret = new TransPrescriptionItem();
            if (ret.Load(retDt))
                txtReturnQty.MaxValue = (Double)detail.ResultQty - (Double)ret.ResultQty;
            else
                txtReturnQty.MaxValue = (Double)detail.ResultQty;

            var reg = new Registration();
            reg.LoadByPrimaryKey(((RadTextBox)Helper.FindControlRecursive(Page, "txtRegistrationNo")).Text);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            ItemTariff tariff = (Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType, reg.ChargeClassID, reg.ChargeClassID, txtItemID.Text, reg.GuarantorID, true, reg.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(DateTime.Now.Date, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, txtItemID.Text, reg.GuarantorID, true, reg.SRRegistrationType)) ??
                                (Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, reg.ChargeClassID, txtItemID.Text, reg.GuarantorID, true, reg.SRRegistrationType) ??
                                 Helper.Tariff.GetItemTariff(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, reg.ChargeClassID, txtItemID.Text, reg.GuarantorID, true, reg.SRRegistrationType));

            if (tariff != null)
            {
                txtPrice.Enabled = (tariff.IsAllowVariable ?? false);
                txtDiscount.Enabled = (tariff.IsAllowDiscount ?? false);
                cboDiscountReason.Enabled = (tariff.IsAllowDiscount ?? false);
            }

            txtPrice.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.Price));
            txtDiscount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.DiscountAmount));

            chkIsCompound.Checked = (Boolean)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.IsCompound);

            string parentNo = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.ParentNo);
            TransPrescriptionItem presc = coll.FindByPrimaryKey(((RadTextBox)Helper.FindControlRecursive(Page, "txtReferenceNo")).Text,
                (string.IsNullOrEmpty(parentNo) ? string.Empty : parentNo));

            if (presc != null)
                txtItemID.Text = presc.SequenceNo;
            else
            {
                presc = new TransPrescriptionItem();
                presc.LoadByPrimaryKey(((RadTextBox)Helper.FindControlRecursive(Page, "txtReferenceNo")).Text,
                    (string.IsNullOrEmpty(parentNo) ? string.Empty : parentNo));
                txtCompoundHeaderID.Text = presc.SequenceNo;
            }

            item = new Item();
            item.LoadByPrimaryKey(txtCompoundHeaderID.Text);
            lblCompoundHeaderName.Text = item.ItemName;
            cboDiscountReason.SelectedValue = (String)DataBinder.Eval(DataItem, TransPrescriptionItemMetadata.ColumnNames.SRDiscountReason);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {

        }

        public String SequenceNo
        {
            get
            { return (String)ViewState["SequenceNo"]; }
        }

        public Decimal ReturnQty
        {
            get
            { return Convert.ToDecimal("-" + Math.Abs((Decimal)txtReturnQty.Value).ToString()); }
        }

        public Decimal Price
        {
            get
            { return (Decimal)txtPrice.Value; }
        }

        public Decimal Discount
        {
            get
            { return (Decimal)txtDiscount.Value; }
        }

        public String DiscountReason
        {
            get
            { return (String)cboDiscountReason.SelectedValue; }
        }
    }
}