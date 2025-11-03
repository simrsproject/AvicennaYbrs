using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeVerificationByDischargeDateChangeShareDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var feeC = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                var feeQ = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
                feeQ.Where(feeQ.TransactionNo == Request.QueryString["trno"],
                           feeQ.SequenceNo == Request.QueryString["seqno"],
                           feeQ.TariffComponentID == Request.QueryString["compid"]);
                if (feeC.Load(feeQ)) {
                    chkUsingPrecentage.Checked = feeC[0].IsCalculatedInPercent ?? true;
                    txtPercentage.Value = (feeC[0].IsCalculatedInPercent ?? true) ? 
                        System.Convert.ToDouble(feeC[0].CalculatedAmount) : 0;
                    txtNominal.Value = System.Convert.ToDouble(feeC[0].FeeAmount ?? 0);
                }

                ChkIsPercentTicked();
            }
        }

        private void ChkIsPercentTicked() {
            txtPercentage.Enabled = chkUsingPrecentage.Checked;
            txtNominal.Enabled = !chkUsingPrecentage.Checked;
        }

        protected void chkUsingPrecentage_OnCheckedChanged(Object sender, EventArgs e) {
            ChkIsPercentTicked();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (false)
            {
                ShowInformationHeader("Raise Error here.");
                return false;
            }
            //-- update ParamedicID & perhitungan jasmed table ParamedicFeeTransChargesItemCompSettled
            //-- hanya u/ jasmed yg belum di-verifikasi
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            feeColl.Query.Where(feeColl.Query.TransactionNo == Request.QueryString["trno"],
                            feeColl.Query.SequenceNo == Request.QueryString["seqno"],
                            feeColl.Query.TariffComponentID == Request.QueryString["compid"]);
            feeColl.LoadAll();
            foreach (var fee in feeColl)
            {
                if (string.IsNullOrEmpty(fee.VerificationNo))
                {
                    //fee.ParamedicID = pParamedicId;
                    fee.IsCalculatedInPercent = chkUsingPrecentage.Checked;
                    if (fee.IsCalculatedInPercent.Value)
                    {
                        fee.CalculatedAmount = System.Convert.ToDecimal(txtPercentage.Value ?? 0);

                        var initFee = (fee.Qty * (fee.Price - fee.Discount));
                        fee.FeeAmount = (initFee - fee.DeductionAmount) * 
                            fee.CalculatedAmount / 100;
                    }
                    else
                    {
                        fee.CalculatedAmount = System.Convert.ToDecimal(txtNominal.Value);
                        fee.FeeAmount = fee.Qty * (System.Convert.ToDecimal(txtNominal.Value));
                    }

                    //fee.FeeAmount = pFeeAmount;
                    //fee.IsRefferal = pIsRefferal;
                    
                    
                    //fee.IsFree = pIsFree;
                    //fee.IsCalcDeductionInPercent = pIsCalcDeductionInPercent;
                    //fee.CalcDeductionAmount = pCalcDeductionAmount;
                    //fee.DeductionAmount = pDeductionAmount;
                    fee.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    fee.LastUpdateDateTime = DateTime.Now;
                }
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                feeColl.Save();

                trans.Complete();
            }

            return true;
        }
    }
}
