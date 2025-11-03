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

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicFeeItemGuarantorCompDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            var colltc = new TariffComponentCollection();
            colltc.Query.Where(colltc.Query.IsTariffParamedic == true);
            colltc.Query.OrderBy(colltc.Query.TariffComponentID.Ascending);
            colltc.LoadAll();
            cboTariffComponentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var item in colltc)
            {
                cboTariffComponentID.Items.Add(new RadComboBoxItem(item.TariffComponentName, item.TariffComponentID));
            }

            if (DataItem is GridInsertionObject)
            {
                ViewState["IsNewRecord"] = true;

                chkIsParamedicFeeUsingPercentage.Checked = true;
                chkIsDeductionFeeUsePercentage.Checked = true;
                txtParamedicFeeAmount.Value = 0D;
                txtParamedicFeeAmountReferral.Value = 0D;
                txtDeductionFeeAmount.Value = 0D;
                txtDeductionFeeAmountReferral.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboTariffComponentID.Enabled = false;

            cboTariffComponentID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.TariffComponentID);

            txtParamedicFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmount));
            txtParamedicFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.ParamedicFeeAmountReferral));
            chkIsParamedicFeeUsingPercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsParamedicFeeUsePercentage);
            txtDeductionFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmount));
            txtDeductionFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.DeductionFeeAmountReferral));
            chkIsDeductionFeeUsePercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorCompMetadata.ColumnNames.IsDeductionFeeUsePercentage);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicFeeItemGuarantorCompCollection)Session["collParamedicFeeItemGuarantorComp"];

                string tcId = cboTariffComponentID.SelectedValue;
                bool isExist = false;
                foreach (ParamedicFeeItemGuarantorComp item in coll)
                {
                    if (item.TariffComponentID.Equals(tcId))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Tariff Component : {0} already exist", cboTariffComponentID.Text);
                }
            }
        }

        #region Properties for return entry value

        public String TariffComponentID
        {
            get { return cboTariffComponentID.SelectedValue; }
        }

        public String TariffComponentName
        {
            get { return cboTariffComponentID.Text; }
        }

        public Boolean IsFeeUsingPercentage
        {
            get { return chkIsParamedicFeeUsingPercentage.Checked; }
        }

        public Decimal FeeAmount
        {
            get { return Convert.ToDecimal(txtParamedicFeeAmount.Value); }
        }

        public decimal FeeAmountReferral
        {
            get { return Convert.ToDecimal(txtParamedicFeeAmountReferral.Value); }
        }

        public Boolean IsDeductionFeeUsePercentage
        {
            get { return chkIsDeductionFeeUsePercentage.Checked; }
        }

        public Decimal DeductionFeeAmount
        {
            get { return Convert.ToDecimal(txtDeductionFeeAmount.Value); }
        }

        public decimal DeductionFeeAmountReferral
        {
            get { return Convert.ToDecimal(txtDeductionFeeAmountReferral.Value); }
        }

        #endregion
    }
}