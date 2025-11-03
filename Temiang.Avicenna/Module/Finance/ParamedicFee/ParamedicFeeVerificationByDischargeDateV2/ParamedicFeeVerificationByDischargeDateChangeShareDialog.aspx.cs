using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee.V2
{
    public partial class ParamedicFeeVerificationByDischargeDateChangeShareDialog : BasePageDialog
    {
        private string TransactionNo
        {
            get { return Request.QueryString["trno"]; }
        }
        private string SequenceNo
        {
            get { return Request.QueryString["seqno"]; }
        }
        private string TariffComponentID
        {
            get { return Request.QueryString["compid"]; }
        }
        private string ParamedicID
        {
            get { return Request.QueryString["parid"]; }
        }
        private bool IsPhysicianMember
        {
            get { return System.Convert.ToBoolean(Request.QueryString["IsPhysicianMember"]); }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsPhysicianMember)
                {
                    var feeBT = new ParamedicFeeTransChargesItemCompByTeam();
                    if (feeBT.LoadByPrimaryKey(TransactionNo, SequenceNo, TariffComponentID, ParamedicID))
                    {
                        txtNominal.Value = System.Convert.ToDouble(feeBT.FeeAmount ?? 0);
                        txtDiscountExtra.Value = System.Convert.ToDouble(0);
                    }
                }
                else {
                    var feeC = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                    var feeQ = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
                    feeQ.Where(feeQ.TransactionNo == TransactionNo,
                               feeQ.SequenceNo == SequenceNo,
                               feeQ.TariffComponentID == TariffComponentID);
                    if (feeC.Load(feeQ))
                    {
                        chkUsingPrecentage.Checked = feeC[0].IsCalculatedInPercent ?? true;
                        txtPercentage.Value = (feeC[0].IsCalculatedInPercent ?? true) ?
                            System.Convert.ToDouble(feeC[0].CalculatedAmount) : 0;

                        txtNominal.Value = System.Convert.ToDouble(feeC[0].FeeAmount ?? 0) + System.Convert.ToDouble(feeC[0].DiscountExtra ?? 0);
                        txtDiscountExtra.Value = System.Convert.ToDouble(feeC[0].DiscountExtra ?? 0);
                    }
                }

                ChkIsPercentTicked();
            }
        }

        private void ChkIsPercentTicked() {
            txtPercentage.Enabled = chkUsingPrecentage.Checked;
            txtNominal.Enabled = !chkUsingPrecentage.Checked;

            if (IsPhysicianMember) {
                chkUsingPrecentage.Enabled = false;
                txtPercentage.Enabled = false;
            }
        }

        protected void chkUsingPrecentage_OnCheckedChanged(Object sender, EventArgs e) {
            ChkIsPercentTicked();
        }

        protected void txtPercentage_TextChanged(object o, EventArgs e) {
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            var ptpColl = new ParamedicFeeTransPaymentCollection();
            var decColl = new ParamedicFeeDeductionsCollection();
            var feeBtColl = new ParamedicFeeTransChargesItemCompByTeamCollection();
            Recalculate(feeColl, ptpColl, decColl, feeBtColl);
            feeColl.CalculateGrossFee(AppSession.UserLogin.UserID);

            if (feeColl.Count > 0) { 
                var fee = feeColl.First();
                txtNominal.Value = System.Convert.ToDouble(fee.FeeAmount ?? 0) + 
                    System.Convert.ToDouble(fee.DiscountExtra ?? 0);
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        private void Recalculate(ParamedicFeeTransChargesItemCompByDischargeDateCollection feeColl, ParamedicFeeTransPaymentCollection ptpColl,
            ParamedicFeeDeductionsCollection decColl, ParamedicFeeTransChargesItemCompByTeamCollection feeByTeamColl) {

            if (IsPhysicianMember)
            {
                var feeQ = feeByTeamColl.Query;
                feeQ.Where(feeQ.TransactionNo == TransactionNo,
                                feeQ.SequenceNo == SequenceNo,
                                feeQ.TariffComponentID == TariffComponentID,
                                feeQ.ParamedicID == ParamedicID);
                feeByTeamColl.LoadAll();
                
                //ptpColl.Query.Where(
                //    ptpColl.Query.TransactionNo == TransactionNo,
                //    ptpColl.Query.SequenceNo == SequenceNo,
                //    ptpColl.Query.TariffComponentID == TariffComponentID,
                //    ptpColl.Query.ParamedicID == ParamedicID);
                //ptpColl.LoadAll();

                foreach (var fee in feeByTeamColl)
                {
                    if (string.IsNullOrEmpty(fee.VerificationNo))
                    {
                        fee.CalculatedAmount = System.Convert.ToDecimal(txtNominal.Value) / fee.Qty;
                        fee.FeeAmount = System.Convert.ToDecimal(txtNominal.Value);

                        fee.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        fee.LastUpdateDateTime = DateTime.Now;

                        foreach (var ptp in ptpColl)
                        {
                            ptp.Amount = ptp.AmountPercentage / 100 * fee.FeeAmount;
                        }
                    }
                    fee.ChangeNote = txtChangeNote.Text;
                }
            }
            else {
                var feeQ = feeColl.MainQuery();
                feeQ.Where(feeQ.TransactionNo == Request.QueryString["trno"],
                                feeQ.SequenceNo == Request.QueryString["seqno"],
                                feeQ.TariffComponentID == Request.QueryString["compid"]);
                feeColl.Load(feeQ);

                ptpColl.Query.Where(
                    ptpColl.Query.TransactionNo == Request.QueryString["trno"],
                    ptpColl.Query.SequenceNo == Request.QueryString["seqno"],
                    ptpColl.Query.TariffComponentID == Request.QueryString["compid"]);
                ptpColl.LoadAll();

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
                            fee.CalculatedAmount = System.Convert.ToDecimal(txtNominal.Value) / fee.Qty;
                            fee.FeeAmount = System.Convert.ToDecimal(txtNominal.Value);
                        }
                        fee.DiscountExtra = System.Convert.ToDecimal(txtDiscountExtra.Value);
                        fee.FeeAmount -= fee.DiscountExtra;

                        //fee.FeeAmount = pFeeAmount;
                        //fee.IsRefferal = pIsRefferal;


                        //fee.IsFree = pIsFree;
                        //fee.IsCalcDeductionInPercent = pIsCalcDeductionInPercent;
                        //fee.CalcDeductionAmount = pCalcDeductionAmount;
                        //fee.DeductionAmount = pDeductionAmount;
                        fee.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        fee.LastUpdateDateTime = DateTime.Now;

                        foreach (var ptp in ptpColl)
                        {
                            ptp.Amount = ptp.AmountPercentage / 100 * fee.FeeAmount;
                        }
                    }
                    fee.ChangeNote = txtChangeNote.Text;
                }
            }

            #region Deduction
            var decQuery = new ParamedicFeeDeductionsQuery("a");
            var feeQuery = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("b");
            decQuery.InnerJoin(feeQuery).On(
                decQuery.TransactionNo.Equal(feeQuery.TransactionNo) &&
                decQuery.SequenceNo.Equal(feeQuery.SequenceNo) &&
                decQuery.TariffComponentID.Equal(feeQuery.TariffComponentID))
                .Where(decQuery.TransactionNo == Request.QueryString["trno"],
                   decQuery.SequenceNo == Request.QueryString["seqno"],
                   decQuery.TariffComponentID == Request.QueryString["compid"]
                )
                .Select(
                    decQuery
                );
            decColl.Load(decQuery);
            feeColl.CalculateDeductionBeforeTax(decColl, AppSession.UserLogin.UserID);
            #endregion
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (txtChangeNote.Text.Trim().Equals(string.Empty))
            {
                ShowInformationHeader("Change note required.");
                return false;
            }

            //-- update ParamedicID & perhitungan jasmed table ParamedicFeeTransChargesItemCompSettled
            //-- hanya u/ jasmed yg belum di-verifikasi
            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            var ptpColl = new ParamedicFeeTransPaymentCollection();
            var decColl = new ParamedicFeeDeductionsCollection();
            var feeBtColl = new ParamedicFeeTransChargesItemCompByTeamCollection();
            Recalculate(feeColl, ptpColl, decColl, feeBtColl);

            
            using (esTransactionScope trans = new esTransactionScope())
            {
                feeColl.Save();
                ptpColl.Save();
                decColl.Save();
                feeBtColl.Save();
                //feeColl.CalculateGrossFee(AppSession.UserLogin.UserID);
                //feeColl.Save();
                // nolin presentase payment untuk kalkulasi ulang

                // lupa ini kenapa diset lagi ya??

                //feeColl.ResetPaymentPercentage();
                //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                //feeColl.Save();

                trans.Complete();
            }

            return true;
        }
    }
}
