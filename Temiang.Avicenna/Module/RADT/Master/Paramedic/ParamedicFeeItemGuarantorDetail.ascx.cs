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
    public partial class ParamedicFeeItemGuarantorDetail : BaseUserControl
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

                chkIsParamedicFeeUsingPercentage.Checked = true;
                chkIsDeductionFeeUsePercentage.Checked = true;
                txtParamedicFeeAmount.Value = 0D;
                txtParamedicFeeAmountReferral.Value = 0D;
                txtDeductionFeeAmount.Value = 0D;
                txtDeductionFeeAmountReferral.Value = 0D;

                return;
            }
            ViewState["IsNewRecord"] = false;
            cboGuarantorID.Enabled = false;

            var guarantor = new GuarantorQuery();
            guarantor.Select
                (
                    guarantor.GuarantorID,
                    guarantor.GuarantorName
                );
            guarantor.Where(guarantor.GuarantorID ==
                            (String)
                            DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID));

            cboGuarantorID.DataSource = guarantor.LoadDataTable();
            cboGuarantorID.DataBind();

            cboGuarantorID.SelectedValue = (String)DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.GuarantorID);

            txtParamedicFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmount));
            txtParamedicFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.ParamedicFeeAmountReferral));
            chkIsParamedicFeeUsingPercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.IsParamedicFeeUsePercentage);
            txtDeductionFeeAmount.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmount));
            txtDeductionFeeAmountReferral.Value = Convert.ToDouble(DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.DeductionFeeAmountReferral));
            chkIsDeductionFeeUsePercentage.Checked = (bool)DataBinder.Eval(DataItem, ParamedicFeeItemGuarantorMetadata.ColumnNames.IsDeductionFeeUsePercentage);
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //Check duplicate key
            if (ViewState["IsNewRecord"].Equals(true))
            {
                ParamedicFeeItemGuarantorCollection coll = (ParamedicFeeItemGuarantorCollection)Session["collParamedicFeeItemGuarantor"];

                string guarID = cboGuarantorID.SelectedValue;
                bool isExist = false;
                foreach (ParamedicFeeItemGuarantor item in coll)
                {
                    if (item.GuarantorID.Equals(guarID))
                    {
                        isExist = true;
                        break;
                    }
                }
                if (isExist)
                {
                    args.IsValid = false;
                    ((CustomValidator)source).ErrorMessage = string.Format("Guarantor ID : {0} already exist", guarID);
                }
            }
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var guar = new GuarantorQuery();
            guar.es.Top = 10;
            guar.Select
                (
                    guar.GuarantorID,
                    guar.GuarantorName
                );
            guar.Where
                (
                    guar.Or
                        (
                            guar.GuarantorID.Like(searchTextContain),
                            guar.GuarantorName.Like(searchTextContain)
                        ),
                    guar.IsActive == true
                );
            guar.OrderBy(guar.GuarantorID.Ascending);

            cboGuarantorID.DataSource = guar.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        #region Properties for return entry value

        public String GuarantorID
        {
            get { return cboGuarantorID.SelectedValue; }
        }

        public String GuarantorName
        {
            get { return cboGuarantorID.Text; }
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