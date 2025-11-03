using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeVerificationByDischargeDateDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var fb = new MedicalRecordFileBorrowed();
                //fb.LoadByPrimaryKey(Request.QueryString["trn"]);

                //var pat = new Patient();
                //pat.LoadByPrimaryKey(fb.PatientID);

                //this.Title = pat.MedicalNo + " (" + (pat.FirstName + " " + pat.MiddleName + " " + pat.LastName).Trim() + ")";

                var par = new ParamedicQuery();
                par.Where(par.ParamedicID == Request.QueryString["parid"]);
                cboPhysicianID.DataSource = par.LoadDataTable();
                cboPhysicianID.DataBind();
                cboPhysicianID.SelectedValue = Request.QueryString["parid"];
            }
        }

        protected void cboPhysicianID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboPhysicianID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ParamedicQuery("a");
            query.es.Top = 30;
            query.Where
                (
                    query.ParamedicName.Like(string.Format("%{0}%", e.Text)),
                    query.IsActive == true
                );
            query.OrderBy(query.ParamedicName.Ascending);

            cboPhysicianID.DataSource = query.LoadDataTable();
            cboPhysicianID.DataBind();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            if (string.IsNullOrEmpty(cboPhysicianID.SelectedValue))
            {
                ShowInformationHeader("Physician is required.");
                return false;
            }

            var pParamedicId = cboPhysicianID.SelectedValue;

            #region fee calculation
            decimal pFeeAmount = 0;
            decimal pCalculatedAmount = 0;
            decimal pCalcDeductionAmount = 0;
            decimal pDeductionAmount = 0;
            bool pIsRefferal = false;
            bool pIsCalculatedInPercent = false;
            bool pIsFree = false;
            bool pIsCalcDeductionInPercent = false;

            var pParamedicIdRefferal = string.Empty;
            var pItemId = string.Empty;
            decimal pQty = 0;
            decimal pPrice = 0;
            decimal pDiscount = 0;

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            if (!string.IsNullOrEmpty(reg.ReferralID))
            {
                var refferal = new Referral();
                if (refferal.LoadByPrimaryKey(reg.ReferralID))
                    pParamedicIdRefferal = refferal.ParamedicID;
            }

            var feeQ = new ParamedicFeeTransChargesItemCompByDischargeDateQuery();
            feeQ.Select(feeQ.ItemID, feeQ.Qty, feeQ.Price, feeQ.Discount);
            feeQ.Where(feeQ.TransactionNo == Request.QueryString["trno"],
                       feeQ.SequenceNo == Request.QueryString["seqno"],
                       feeQ.TariffComponentID == Request.QueryString["compid"]);
            feeQ.es.Top = 1;
            DataTable feeDtb = feeQ.LoadDataTable();
            foreach (DataRow row in feeDtb.Rows)
            {
                pQty = Convert.ToDecimal(row["Qty"]);
                pPrice = Convert.ToDecimal(row["Price"]);
                pDiscount = Convert.ToDecimal(row["Discount"]);
                pItemId = row["ItemID"].ToString();
            }

            var freeGuars = new AppStandardReferenceItemCollection();
            freeGuars.Query.Select(freeGuars.Query.ItemID);
            freeGuars.Query.Where(
                freeGuars.Query.StandardReferenceID == AppEnum.StandardReference.GuarantorFreeOfPhysicianFee,
                freeGuars.Query.IsActive == true,
                freeGuars.Query.ItemID == reg.GuarantorID
                );
            freeGuars.LoadAll();

            var itemExcs = new AppStandardReferenceItemCollection();
            itemExcs.Query.Select(itemExcs.Query.ItemID);
            itemExcs.Query.Where(
                itemExcs.Query.StandardReferenceID == AppEnum.StandardReference.FeeItemExcForFreeGuar,
                itemExcs.Query.IsActive == true,
                itemExcs.Query.ItemID == pItemId
                );
            itemExcs.LoadAll();

            var maxdisc = AppSession.Parameter.MaxDiscTxInPercentage;
            if (AppSession.Parameter.IsPhysicianFeeCalcBasedOnGuarantorCategory)
            {
                #region general
                var medic = new Paramedic();
                if (medic.LoadByPrimaryKey(pParamedicId))
                {
                    var initFee = pQty * (pPrice - pDiscount);
                    decimal maxdiscAmt;
                    decimal fee;
                    decimal deduction;

                    bool isRefferal, isCalInPercent, isCalDeducInPercent;
                    decimal calAmt, calDeduc;

                    if (pParamedicId == pParamedicIdRefferal)
                    {
                        //01. Paramedic - Item - Guarantor - Comp
                        var mtxItemGuarComp = new ParamedicFeeItemGuarantorComp();
                        if (mtxItemGuarComp.LoadByPrimaryKey(pParamedicId, pItemId, reg.GuarantorID, Request.QueryString["compid"]))
                        {
                            if (mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false)
                                deduction = initFee * (mtxItemGuarComp.DeductionFeeAmountReferral ?? 0) / 100;
                            else
                                deduction = mtxItemGuarComp.DeductionFeeAmountReferral ?? 0;

                            if (mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false)
                            {
                                maxdiscAmt = pPrice * (mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                fee = (initFee - deduction) * (mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0) / 100;
                            }
                            else
                            {
                                maxdiscAmt = mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0;
                                fee = pQty * (mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0);
                            }
                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                            fee = pDiscount >= maxdiscAmt ? 0 : fee;

                            isCalInPercent = mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false;
                            calAmt = mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0;
                            isCalDeducInPercent = mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false;
                            calDeduc = mtxItemGuarComp.DeductionFeeAmountReferral ?? 0;
                        }
                        else
                        {
                            //02. Paramedic - Item - Comp
                            var mtxItemComp = new ParamedicFeeItemComp();
                            if (mtxItemComp.LoadByPrimaryKey(pParamedicId, pItemId, Request.QueryString["compid"]))
                            {
                                if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                    deduction = initFee * (mtxItemComp.DeductionFeeAmountReferral ?? 0) / 100;
                                else
                                    deduction = mtxItemComp.DeductionFeeAmountReferral ?? 0;

                                if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                                {
                                    maxdiscAmt = pPrice * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                    fee = (initFee - deduction) * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                }
                                else
                                {
                                    maxdiscAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                                    fee = pQty * (mtxItemComp.ParamedicFeeAmountReferral ?? 0);
                                }
                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                                calAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                                isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                                calDeduc = mtxItemComp.DeductionFeeAmountReferral ?? 0;
                            }
                            else
                            {
                                //03. Paramedic - Item - Guarantor
                                var mtxItemGuarr = new ParamedicFeeItemGuarantor();
                                if (mtxItemGuarr.LoadByPrimaryKey(pParamedicId, pItemId, reg.GuarantorID))
                                {
                                    if (mtxItemGuarr.IsDeductionFeeUsePercentage ?? false)
                                        deduction = initFee * (mtxItemGuarr.DeductionFeeAmountReferral ?? 0) / 100;
                                    else
                                        deduction = mtxItemGuarr.DeductionFeeAmountReferral ?? 0;

                                    if (mtxItemGuarr.IsParamedicFeeUsePercentage ?? false)
                                    {
                                        maxdiscAmt = pPrice * (mtxItemGuarr.ParamedicFeeAmountReferral ?? 0) / 100;
                                        fee = (initFee - deduction) * (mtxItemGuarr.ParamedicFeeAmountReferral ?? 0) / 100;
                                    }
                                    else
                                    {
                                        maxdiscAmt = mtxItemGuarr.ParamedicFeeAmountReferral ?? 0;
                                        fee = pQty * (mtxItemGuarr.ParamedicFeeAmountReferral ?? 0);
                                    }
                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                    fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                    isCalInPercent = mtxItemGuarr.IsParamedicFeeUsePercentage ?? false;
                                    calAmt = mtxItemGuarr.ParamedicFeeAmountReferral ?? 0;
                                    isCalDeducInPercent = mtxItemGuarr.IsDeductionFeeUsePercentage ?? false;
                                    calDeduc = mtxItemGuarr.DeductionFeeAmountReferral ?? 0;
                                }
                                else
                                {
                                    //04. Paramedic - Item
                                    var matrix = new ParamedicFeeItem();
                                    if (matrix.LoadByPrimaryKey(pParamedicId, pItemId))
                                    {
                                        if (matrix.IsDeductionFeeUsePercentage ?? false)
                                            deduction = initFee * (matrix.DeductionFeeAmountReferral ?? 0) / 100;
                                        else
                                            deduction = matrix.DeductionFeeAmountReferral ?? 0;

                                        if (matrix.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = pPrice * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                            fee = (initFee - deduction) * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                            fee = pQty * (matrix.ParamedicFeeAmountReferral ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                        fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                        isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = matrix.DeductionFeeAmountReferral ?? 0;
                                    }
                                    else
                                    {
                                        //05. Paramedic
                                        if (medic.IsDeductionFeeUsePercentage ?? false)
                                            deduction = initFee * (medic.DeductionFeeAmountReferral ?? 0) / 100;
                                        else
                                            deduction = medic.DeductionFeeAmountReferral ?? 0;

                                        if (medic.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = pPrice * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                            fee = (initFee - deduction) * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = medic.ParamedicFeeAmountReferral ?? 0;
                                            fee = pQty * (medic.ParamedicFeeAmountReferral ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                        fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = medic.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = medic.ParamedicFeeAmountReferral ?? 0;
                                        isCalDeducInPercent = medic.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = medic.DeductionFeeAmountReferral ?? 0;
                                    }
                                }
                            }
                        }

                        isRefferal = true;
                    }
                    else
                    {
                        //01. Paramedic - Item - Guarantor - Comp
                        var mtxItemGuarComp = new ParamedicFeeItemGuarantorComp();
                        if (mtxItemGuarComp.LoadByPrimaryKey(pParamedicId, pItemId, reg.GuarantorID, Request.QueryString["compid"]))
                        {
                            if (mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false)
                                deduction = initFee * (mtxItemGuarComp.DeductionFeeAmount ?? 0) / 100;
                            else
                                deduction = mtxItemGuarComp.DeductionFeeAmount ?? 0;

                            if (mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false)
                            {
                                maxdiscAmt = pPrice * (mtxItemGuarComp.ParamedicFeeAmount ?? 0) / 100;
                                fee = (initFee - deduction) * (mtxItemGuarComp.ParamedicFeeAmount ?? 0) / 100;
                            }
                            else
                            {
                                maxdiscAmt = mtxItemGuarComp.ParamedicFeeAmount ?? 0;
                                fee = pQty * (mtxItemGuarComp.ParamedicFeeAmount ?? 0);
                            }
                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                            fee = pDiscount >= maxdiscAmt ? 0 : fee;

                            isCalInPercent = mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false;
                            calAmt = mtxItemGuarComp.ParamedicFeeAmount ?? 0;
                            isCalDeducInPercent = mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false;
                            calDeduc = mtxItemGuarComp.DeductionFeeAmount ?? 0;
                        }
                        else
                        {
                            //02. Paramedic - Item - Comp
                            var mtxItemComp = new ParamedicFeeItemComp();
                            if (mtxItemComp.LoadByPrimaryKey(pParamedicId, pItemId, Request.QueryString["compid"]))
                            {
                                if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                    deduction = initFee * (mtxItemComp.DeductionFeeAmount ?? 0) / 100;
                                else
                                    deduction = mtxItemComp.DeductionFeeAmount ?? 0;

                                if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                                {
                                    maxdiscAmt = pPrice * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                                    fee = (initFee - deduction) * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                                }
                                else
                                {
                                    maxdiscAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                                    fee = pQty * (mtxItemComp.ParamedicFeeAmount ?? 0);
                                }
                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                                calAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                                isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                                calDeduc = mtxItemComp.DeductionFeeAmount ?? 0;
                            }
                            else
                            {
                                //03. Paramedic - Item - Guarantor
                                var mtxItemGuarr = new ParamedicFeeItemGuarantor();
                                if (mtxItemGuarr.LoadByPrimaryKey(pParamedicId, pItemId, reg.GuarantorID))
                                {
                                    if (mtxItemGuarr.IsDeductionFeeUsePercentage ?? false)
                                        deduction = initFee * (mtxItemGuarr.DeductionFeeAmount ?? 0) / 100;
                                    else
                                        deduction = mtxItemGuarr.DeductionFeeAmount ?? 0;

                                    if (mtxItemGuarr.IsParamedicFeeUsePercentage ?? false)
                                    {
                                        maxdiscAmt = pPrice * (mtxItemGuarr.ParamedicFeeAmount ?? 0) / 100;
                                        fee = (initFee - deduction) * (mtxItemGuarr.ParamedicFeeAmount ?? 0) / 100;
                                    }
                                    else
                                    {
                                        maxdiscAmt = mtxItemGuarr.ParamedicFeeAmount ?? 0;
                                        fee = pQty * (mtxItemGuarr.ParamedicFeeAmount ?? 0);
                                    }
                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                    fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                    isCalInPercent = mtxItemGuarr.IsParamedicFeeUsePercentage ?? false;
                                    calAmt = mtxItemGuarr.ParamedicFeeAmount ?? 0;
                                    isCalDeducInPercent = mtxItemGuarr.IsDeductionFeeUsePercentage ?? false;
                                    calDeduc = mtxItemGuarr.DeductionFeeAmount ?? 0;
                                }
                                else
                                {
                                    //04. Paramedic - Item
                                    var matrix = new ParamedicFeeItem();
                                    if (matrix.LoadByPrimaryKey(pParamedicId, pItemId))
                                    {
                                        if (matrix.IsDeductionFeeUsePercentage ?? false)
                                            deduction = initFee * (matrix.DeductionFeeAmount ?? 0) / 100;
                                        else
                                            deduction = matrix.DeductionFeeAmount ?? 0;

                                        if (matrix.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = pPrice * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                            fee = (initFee - deduction) * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = matrix.ParamedicFeeAmount ?? 0;
                                            fee = pQty * (matrix.ParamedicFeeAmount ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                        fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = matrix.ParamedicFeeAmount ?? 0;
                                        isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = matrix.DeductionFeeAmount ?? 0;
                                    }
                                    else
                                    {
                                        //05. Paramedic
                                        if (medic.IsDeductionFeeUsePercentage ?? false)
                                            deduction = initFee * (medic.DeductionFeeAmount ?? 0) / 100;
                                        else
                                            deduction = medic.DeductionFeeAmount ?? 0;

                                        if (medic.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = pPrice * (medic.ParamedicFeeAmount ?? 0) / 100;
                                            fee = (initFee - deduction) * (medic.ParamedicFeeAmount ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = medic.ParamedicFeeAmount ?? 0;
                                            fee = pQty * (medic.ParamedicFeeAmount ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                        fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = medic.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = medic.ParamedicFeeAmount ?? 0;
                                        isCalDeducInPercent = medic.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = medic.DeductionFeeAmount ?? 0;
                                    }
                                }
                            }
                        }

                        isRefferal = false;
                    }

                    pFeeAmount = fee;
                    pIsRefferal = isRefferal;
                    pIsCalculatedInPercent = isCalInPercent;
                    pCalculatedAmount = calAmt;
                    pIsFree = freeGuars.Count > 0 && itemExcs.Count == 0;
                    pIsCalcDeductionInPercent = isCalDeducInPercent;
                    pCalcDeductionAmount = calDeduc;
                    pDeductionAmount = deduction;
                }
                #endregion
            }
            else
            {
                #region Per Category
                var guar = new Guarantor();
                guar.LoadByPrimaryKey(reg.GuarantorID);

                var guarCategory = guar.SRPhysicianFeeType;

                var medic = new ParamedicFeeGuarantorCategory();
                if (medic.LoadByPrimaryKey(pParamedicId, guarCategory))
                {
                    var initFee = pQty * (pPrice - pDiscount);
                    decimal maxdiscAmt;
                    decimal fee;
                    decimal deduction;

                    bool isRefferal, isCalInPercent, isCalDeducInPercent;
                    decimal calAmt, calDeduc;

                    if (pParamedicId == pParamedicIdRefferal)
                    {
                        //01. Paramedic - Guarantor Category - Item - Comp
                        var mtxItemComp = new ParamedicFeeGuarantorCategoryItemComp();
                        if (mtxItemComp.LoadByPrimaryKey(pItemId, pParamedicId, guarCategory, Request.QueryString["compid"]))
                        {
                            if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                deduction = initFee * (mtxItemComp.DeductionFeeAmountReferral ?? 0) / 100;
                            else
                                deduction = mtxItemComp.DeductionFeeAmountReferral ?? 0;

                            if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                            {
                                maxdiscAmt = pPrice * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                fee = (initFee - deduction) * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                            }
                            else
                            {
                                maxdiscAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                                fee = pQty * (mtxItemComp.ParamedicFeeAmountReferral ?? 0);
                            }
                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                            fee = pDiscount >= maxdiscAmt ? 0 : fee;

                            isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                            calAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                            isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                            calDeduc = mtxItemComp.DeductionFeeAmountReferral ?? 0;
                        }
                        else
                        {
                            //02. Paramedic - Guarantor Category - Item
                            var matrix = new ParamedicFeeGuarantorCategoryItem();
                            if (matrix.LoadByPrimaryKey(pItemId, pParamedicId, guarCategory))
                            {
                                if (matrix.IsDeductionFeeUsePercentage ?? false)
                                    deduction = initFee * (matrix.DeductionFeeAmountReferral ?? 0) / 100;
                                else
                                    deduction = matrix.DeductionFeeAmountReferral ?? 0;

                                if (matrix.IsParamedicFeeUsePercentage ?? false)
                                {
                                    maxdiscAmt = pPrice * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                    fee = (initFee - deduction) * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                }
                                else
                                {
                                    maxdiscAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                    fee = pQty * (matrix.ParamedicFeeAmountReferral ?? 0);
                                }
                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                calAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                calDeduc = matrix.DeductionFeeAmountReferral ?? 0;
                            }
                            else
                            {
                                //03. Paramedic - Guarantor Category
                                var mtxCat = new ParamedicFeeGuarantorCategory();
                                if (mtxCat.LoadByPrimaryKey(pParamedicId, guarCategory))
                                {
                                    if (mtxCat.IsDeductionFeeUsePercentage ?? false)
                                        deduction = initFee * (mtxCat.DeductionFeeAmountReferral ?? 0) / 100;
                                    else
                                        deduction = mtxCat.DeductionFeeAmountReferral ?? 0;

                                    if (mtxCat.IsParamedicFeeUsePercentage ?? false)
                                    {
                                        maxdiscAmt = pPrice * (mtxCat.ParamedicFeeAmountReferral ?? 0) / 100;
                                        fee = (initFee - deduction) * (mtxCat.ParamedicFeeAmountReferral ?? 0) / 100;
                                    }
                                    else
                                    {
                                        maxdiscAmt = mtxCat.ParamedicFeeAmountReferral ?? 0;
                                        fee = pQty * (mtxCat.ParamedicFeeAmountReferral ?? 0);
                                    }
                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                    fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                    isCalInPercent = mtxCat.IsParamedicFeeUsePercentage ?? false;
                                    calAmt = mtxCat.ParamedicFeeAmountReferral ?? 0;
                                    isCalDeducInPercent = mtxCat.IsDeductionFeeUsePercentage ?? false;
                                    calDeduc = mtxCat.DeductionFeeAmountReferral ?? 0;
                                }
                                else
                                {
                                    //04. Paramedic 
                                    if (medic.IsDeductionFeeUsePercentage ?? false)
                                        deduction = initFee * (medic.DeductionFeeAmountReferral ?? 0) / 100;
                                    else
                                        deduction = medic.DeductionFeeAmountReferral ?? 0;

                                    if (medic.IsParamedicFeeUsePercentage ?? false)
                                    {
                                        maxdiscAmt = pPrice * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                        fee = (initFee - deduction) * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                    }
                                    else
                                    {
                                        maxdiscAmt = medic.ParamedicFeeAmountReferral ?? 0;
                                        fee = pQty * (medic.ParamedicFeeAmountReferral ?? 0);
                                    }
                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                    fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                    isCalInPercent = medic.IsParamedicFeeUsePercentage ?? false;
                                    calAmt = medic.ParamedicFeeAmountReferral ?? 0;
                                    isCalDeducInPercent = medic.IsDeductionFeeUsePercentage ?? false;
                                    calDeduc = medic.DeductionFeeAmountReferral ?? 0;
                                }
                            }
                        }

                        isRefferal = true;
                    }
                    else
                    {
                        //01. Paramedic - Guarantor Category - Item - Comp
                        var mtxItemComp = new ParamedicFeeGuarantorCategoryItemComp();
                        if (mtxItemComp.LoadByPrimaryKey(pItemId, pParamedicId, guarCategory, Request.QueryString["compid"]))
                        {
                            if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                deduction = initFee * (mtxItemComp.DeductionFeeAmount ?? 0) / 100;
                            else
                                deduction = mtxItemComp.DeductionFeeAmount ?? 0;

                            if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                            {
                                maxdiscAmt = pPrice * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                                fee = (initFee - deduction) * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                            }
                            else
                            {
                                maxdiscAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                                fee = pQty * (mtxItemComp.ParamedicFeeAmount ?? 0);
                            }
                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                            fee = pDiscount >= maxdiscAmt ? 0 : fee;

                            isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                            calAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                            isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                            calDeduc = mtxItemComp.DeductionFeeAmount ?? 0;
                        }
                        else
                        {
                            //02. Paramedic - Guarantor Category - Item
                            var matrix = new ParamedicFeeGuarantorCategoryItem();
                            if (matrix.LoadByPrimaryKey(pItemId, pParamedicId, guarCategory))
                            {
                                if (matrix.IsDeductionFeeUsePercentage ?? false)
                                    deduction = initFee * (matrix.DeductionFeeAmount ?? 0) / 100;
                                else
                                    deduction = matrix.DeductionFeeAmount ?? 0;

                                if (matrix.IsParamedicFeeUsePercentage ?? false)
                                {
                                    maxdiscAmt = pPrice * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                    fee = (initFee - deduction) * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                }
                                else
                                {
                                    maxdiscAmt = matrix.ParamedicFeeAmount ?? 0;
                                    fee = pQty * (matrix.ParamedicFeeAmount ?? 0);
                                }
                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                calAmt = matrix.ParamedicFeeAmount ?? 0;
                                isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                calDeduc = matrix.DeductionFeeAmount ?? 0;
                            }
                            else
                            {
                                //03. Paramedic - Guarantor Category
                                var mtxCat = new ParamedicFeeGuarantorCategory();
                                if (mtxCat.LoadByPrimaryKey(pParamedicId, guarCategory))
                                {
                                    if (mtxCat.IsDeductionFeeUsePercentage ?? false)
                                        deduction = initFee * (mtxCat.DeductionFeeAmount ?? 0) / 100;
                                    else
                                        deduction = mtxCat.DeductionFeeAmount ?? 0;

                                    if (mtxCat.IsParamedicFeeUsePercentage ?? false)
                                    {
                                        maxdiscAmt = pPrice * (mtxCat.ParamedicFeeAmount ?? 0) / 100;
                                        fee = (initFee - deduction) * (mtxCat.ParamedicFeeAmount ?? 0) / 100;
                                    }
                                    else
                                    {
                                        maxdiscAmt = mtxCat.ParamedicFeeAmount ?? 0;
                                        fee = pQty * (mtxCat.ParamedicFeeAmount ?? 0);
                                    }
                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                    fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                    isCalInPercent = mtxCat.IsParamedicFeeUsePercentage ?? false;
                                    calAmt = mtxCat.ParamedicFeeAmount ?? 0;
                                    isCalDeducInPercent = mtxCat.IsDeductionFeeUsePercentage ?? false;
                                    calDeduc = mtxCat.DeductionFeeAmount ?? 0;
                                }
                                else
                                {
                                    //04. Paramedic
                                    if (medic.IsDeductionFeeUsePercentage ?? false)
                                        deduction = initFee * (medic.DeductionFeeAmount ?? 0) / 100;
                                    else
                                        deduction = medic.DeductionFeeAmount ?? 0;

                                    if (medic.IsParamedicFeeUsePercentage ?? false)
                                    {
                                        maxdiscAmt = pPrice * (medic.ParamedicFeeAmount ?? 0) / 100;
                                        fee = (initFee - deduction) * (medic.ParamedicFeeAmount ?? 0) / 100;
                                    }
                                    else
                                    {
                                        maxdiscAmt = medic.ParamedicFeeAmount ?? 0;
                                        fee = pQty * (medic.ParamedicFeeAmount ?? 0);
                                    }
                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : (pPrice * maxdisc / 100);
                                    fee = pDiscount >= maxdiscAmt ? 0 : fee;

                                    isCalInPercent = medic.IsParamedicFeeUsePercentage ?? false;
                                    calAmt = medic.ParamedicFeeAmount ?? 0;
                                    isCalDeducInPercent = medic.IsDeductionFeeUsePercentage ?? false;
                                    calDeduc = medic.DeductionFeeAmount ?? 0;
                                }
                            }
                        }

                        isRefferal = false;
                    }

                    pFeeAmount = fee;
                    pIsRefferal = isRefferal;
                    pIsCalculatedInPercent = isCalInPercent;
                    pCalculatedAmount = calAmt;
                    pIsFree = freeGuars.Count > 0 && itemExcs.Count == 0;
                    pIsCalcDeductionInPercent = isCalDeducInPercent;
                    pCalcDeductionAmount = calDeduc;
                    pDeductionAmount = deduction;
                }
                #endregion
            }

            #endregion

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
                    fee.ParamedicID = pParamedicId;
                    fee.FeeAmount = pFeeAmount;
                    fee.IsRefferal = pIsRefferal;
                    fee.IsCalculatedInPercent = pIsCalculatedInPercent;
                    fee.CalculatedAmount = pCalculatedAmount;
                    fee.IsFree = pIsFree;
                    fee.IsCalcDeductionInPercent = pIsCalcDeductionInPercent;
                    fee.CalcDeductionAmount = pCalcDeductionAmount;
                    fee.DeductionAmount = pDeductionAmount;
                    fee.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    fee.LastUpdateDateTime = DateTime.Now;
                }
            }

            //-- update ParamedicID di table transChargesItemComp
            var pParamedicCollectionName = string.Empty;
            var tciComps = new TransChargesItemCompCollection();
            tciComps.Query.Where(tciComps.Query.TransactionNo == Request.QueryString["trno"],
                               tciComps.Query.SequenceNo == Request.QueryString["seqno"]);
            tciComps.LoadAll();
            foreach (var tciComp in tciComps)
            {
                if (tciComp.TariffComponentID == Request.QueryString["compid"])
                {
                    tciComp.ParamedicID = pParamedicId;
                    tciComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    tciComp.LastUpdateDateTime = DateTime.Now;
                }

                if (!string.IsNullOrEmpty(tciComp.ParamedicID))
                {
                    var tComp = new TariffComponent();
                    if (tComp.LoadByPrimaryKey(tciComp.TariffComponentID))
                    {
                        if (tComp.IsPrintParamedicInSlip ?? false)
                        {
                            var par = new Paramedic();
                            par.LoadByPrimaryKey(tciComp.ParamedicID);
                            if (par.IsPrintInSlip ?? true)
                            {
                                if (pParamedicCollectionName.Length == 0)
                                    pParamedicCollectionName = par.ParamedicName;
                                else if (!pParamedicCollectionName.Contains(par.ParamedicName))
                                    pParamedicCollectionName = pParamedicCollectionName + "; " + par.ParamedicName;
                            }
                        }
                    }
                }
            }

            //-- update ParamedicCollectionName di table TransChargesItem
            var tci = new TransChargesItem();
            tci.LoadByPrimaryKey(Request.QueryString["trno"], Request.QueryString["seqno"]);
            tci.ParamedicCollectionName = pParamedicCollectionName;

            using (esTransactionScope trans = new esTransactionScope())
            {
                feeColl.Save();
                tciComps.Save();
                tci.Save();

                trans.Complete();
            }

            (new ParamedicFeeTransChargesItemCompByDischargeDateCollection()).UpdateDataParamedic(
                Request.QueryString["trno"],
                Request.QueryString["seqno"],
                Request.QueryString["compid"]);

            return true;
        }
    }
}
