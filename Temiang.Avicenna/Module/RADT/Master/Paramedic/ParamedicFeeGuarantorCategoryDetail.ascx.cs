using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class ParamedicFeeGuarantorCategoryDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRPhysicianFeeType, AppEnum.StandardReference.PhysicianFeeType);

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
            cboSRPhysicianFeeType.Enabled = false;

            cboSRPhysicianFeeType.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.SRPhysicianFeeType);
            txtParamedicFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmount));
            txtParamedicFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.ParamedicFeeAmountReferral));
            chkIsParamedicFeeUsingPercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsParamedicFeeUsePercentage);
            txtDeductionFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmount));
            txtDeductionFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.DeductionFeeAmountReferral));
            chkIsDeductionFeeUsePercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeGuarantorCategoryMetadata.ColumnNames.IsDeductionFeeUsePercentage);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                var coll = (ParamedicFeeGuarantorCategoryCollection)Session["collParamedicFeeGuarantorCategory"];

                string type = cboSRPhysicianFeeType.SelectedValue;
                bool isExist = false;
                foreach (ParamedicFeeGuarantorCategory item in coll)
                {
                    if (item.SRPhysicianFeeType.Equals(type))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Guarantor Category : {0} already exist", cboSRPhysicianFeeType.Text);
                }
            }
        }

        #region Properties for return entry value

        public String SRPhysicianFeeType
        {
            get { return cboSRPhysicianFeeType.SelectedValue; }
        }

        public String PhysicianFeeType
        {
            get { return cboSRPhysicianFeeType.Text; }
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