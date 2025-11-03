using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.ParamedicFee
{
    public partial class ParamedicFeeCalculationByDischargeDateList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ParamedicFeeCalculation;
            txtDatePeriode1.SelectedDate = DateTime.Now;
            txtDatePeriode2.SelectedDate = DateTime.Now;
        }

        protected void cboParamedicID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ParamedicName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ParamedicID"].ToString();
        }

        protected void cboParamedicID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ParamedicQuery("a");

            query.es.Top = 15;
            query.Select
                (
                    query.ParamedicID,
                    (query.ParamedicName + " [" + query.ParamedicID + "]").As("ParamedicName")
                );
            query.Where
                (
                    query.Or
                    (
                       query.ParamedicID.Like(string.Format("%{0}%", e.Text)),
                       query.ParamedicName.Like(string.Format("%{0}%", e.Text))
                    ),
                    query.IsActive == true

                );
            query.OrderBy(query.ParamedicID.Ascending);

            cboParamedicID.DataSource = query.LoadDataTable();
            cboParamedicID.DataBind();
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                CheckBox chk = item["IsModified"].Controls[0] as CheckBox;
                if (chk.Checked)
                {
                    item.BackColor = Color.Red;
                    //celltoVerify1.Font.Bold = true;
                    //celltoVerify1.BackColor = Color.Yellow;
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ParamedicFee();
        }

        private DataTable ParamedicFee()
        {
            //ExtractByDateRangeAndParamedicWithNoMergeBillingWithCorrection();
            ExtractByDateRangeAndParamedicWithMergeBilling();

            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var transH = new TransChargesQuery("b");
            var reg = new RegistrationQuery("c");
            var item = new ItemQuery("d");
            var medic = new ParamedicQuery("e");
            var patient = new PatientQuery("f");
            var unit = new ServiceUnitQuery("g");
            var toUnit = new ServiceUnitQuery("h");

            query.Select(
                transH.RegistrationNo,
                patient.MedicalNo,
                patient.PatientName,
                query.DischargeDate,
                transH.ToServiceUnitID,
                unit.ServiceUnitName,
                transH.TransactionNo,
                transH.TransactionDate,
                transH.ToServiceUnitID,
                "<h.ServiceUnitName AS ToServiceUnitName>",
                query.SequenceNo,
                query.ItemID,
                item.ItemName,
                query.Price,
                query.Discount,
                query.Qty,
                "<(a.Price - a.Discount) * a.Qty AS ParamedicFee>",
                query.ParamedicID,
                medic.ParamedicName,
                query.FeeAmount,
                query.DeductionAmount,
                query.LastCalculatedDateTime,
                query.IsModified,
                query.PaymentMethodName
                );
            query.InnerJoin(transH).On(transH.TransactionNo == query.TransactionNo);
            query.InnerJoin(reg).On(reg.RegistrationNo == transH.RegistrationNo);
            query.InnerJoin(item).On(item.ItemID == query.ItemID);
            query.InnerJoin(medic).On(medic.ParamedicID == query.ParamedicID);
            query.InnerJoin(patient).On(patient.PatientID == reg.PatientID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
            query.InnerJoin(toUnit).On(toUnit.ServiceUnitID == transH.ToServiceUnitID);
            query.Where(query.DischargeDateMergeTo.Between(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate));

            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            query.Where(query.VerificationNo.IsNull());
            // exclude
            var aCollGuar = new AppStandardReferenceItemCollection();
            aCollGuar.Query.Where(aCollGuar.Query.StandardReferenceID == "PhysFeeCalcGuarExcl");
            aCollGuar.LoadAll();
            if (aCollGuar.Count > 0)
            {
                query.Where(reg.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }

            // filter hanya yang bisa proses ke jasmed saja
            query.Where(medic.ParamedicFee == true);

            query.OrderBy(query.IsModified.Descending, transH.RegistrationNo.Ascending);

            var res = query.LoadDataTable();

            return res;
        }

        private void ExtractByDateRangeAndParamedicWithNoMergeBillingWithCorrection()
        {
            ParamedicFeeTransChargesItemCompByDischargeDate.ExtractByDateRangeAndParamedicWithNoMergeBillingWithCorrection(
                txtDatePeriode1.SelectedDate.Value.Date, txtDatePeriode2.SelectedDate.Value.Date, 
                cboParamedicID.SelectedValue, AppSession.UserLogin.UserID);
        }

        private void ExtractByDateRangeAndParamedicWithMergeBilling()
        {
            ParamedicFeeTransChargesItemCompByDischargeDate.ExtractByDateRangeAndParamedicWithMergeBilling(string.Empty,
                txtDatePeriode1.SelectedDate.Value.Date, txtDatePeriode2.SelectedDate.Value.Date,
                cboParamedicID.SelectedValue, AppSession.UserLogin.UserID);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            switch (eventArgument)
            {
                case "rebind":
                    //ProcessWithNoMergeBillingWithCorrection();
                    ProcessWithMergeBilling();
                    grdList.Rebind();
                    break;
                case "refresh":
                    grdList.Rebind();
                    break;
                case "print":
                    Print();
                    break;
            }
        }

        private int UpdateData(DateTime? d1, DateTime? d2, string ParamedicID, bool UseMergeBilling) {
            var o = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            return o.UpdateDataDiscount() + o.UpdateDataParamedic(d1, d2, ParamedicID, UseMergeBilling);
        }

        private void ProcessWithNoMergeBillingWithCorrection()
        {
            UpdateData(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate, cboParamedicID.SelectedValue, false);

            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var transH = new TransChargesQuery("b");
            var regis = new RegistrationQuery("c");
            var item = new ItemQuery("d");
            var paramedic = new ParamedicQuery("e");
            var patient = new PatientQuery("f");
            var unit = new ServiceUnitQuery("g");
            var toUnit = new ServiceUnitQuery("h");
            var refferal = new ReferralQuery("i");

            query.Select(
                query,
                transH.RegistrationNo,
                refferal.ParamedicID.As("ParamedicIDReferral")
                );
            query.InnerJoin(transH).On(transH.TransactionNo == query.TransactionNo);
            query.InnerJoin(regis).On(regis.RegistrationNo == transH.RegistrationNo);
            query.InnerJoin(item).On(item.ItemID == query.ItemID);
            query.InnerJoin(paramedic).On(paramedic.ParamedicID == query.ParamedicID);
            query.InnerJoin(patient).On(patient.PatientID == regis.PatientID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == regis.ServiceUnitID);
            query.InnerJoin(toUnit).On(toUnit.ServiceUnitID == transH.ToServiceUnitID);
            query.LeftJoin(refferal).On(refferal.ReferralID == regis.ReferralID);

            query.Where(query.DischargeDate.Between(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate),
                        query.VerificationNo.IsNull());

            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            // exclude
            var aCollGuar = new AppStandardReferenceItemCollection();
            aCollGuar.Query.Where(aCollGuar.Query.StandardReferenceID == "PhysFeeCalcGuarExcl");
            aCollGuar.LoadAll();
            if (aCollGuar.Count > 0)
            {
                query.Where(regis.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }

            // filter hanya yang bisa diproses ke jasmed saja
            query.Where(paramedic.ParamedicFee == true);

            var app = new AppParameter();
            if (app.LoadByPrimaryKey("acc_IsAutoJournalPhysicianFeeBeforeVerification"))
            {
                if (app.ParameterValue == "Yes")
                {
                    query.Where(query.JournalId.IsNull());
                }
            }

            var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            coll.Load(query);

            DoCalculateFee(coll);
        }

        private void ProcessWithMergeBilling()
        {
            UpdateData(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate, cboParamedicID.SelectedValue, true);

            var query = new ParamedicFeeTransChargesItemCompByDischargeDateQuery("a");
            var transH = new TransChargesQuery("b");
            var regis = new RegistrationQuery("c");
            var item = new ItemQuery("d");
            var paramedic = new ParamedicQuery("e");
            var patient = new PatientQuery("f");
            var unit = new ServiceUnitQuery("g");
            var toUnit = new ServiceUnitQuery("h");
            var refferal = new ReferralQuery("i");

            query.Select(
                query,
                transH.RegistrationNo,
                refferal.ParamedicID.As("ParamedicIDReferral")
                );
            query.InnerJoin(transH).On(transH.TransactionNo == query.TransactionNo);
            query.InnerJoin(regis).On(regis.RegistrationNo == transH.RegistrationNo);
            query.InnerJoin(item).On(item.ItemID == query.ItemID);
            query.InnerJoin(paramedic).On(paramedic.ParamedicID == query.ParamedicID);
            query.InnerJoin(patient).On(patient.PatientID == regis.PatientID);
            query.InnerJoin(unit).On(unit.ServiceUnitID == regis.ServiceUnitID);
            query.InnerJoin(toUnit).On(toUnit.ServiceUnitID == transH.ToServiceUnitID);
            query.LeftJoin(refferal).On(refferal.ReferralID == regis.ReferralID);

            query.Where(query.DischargeDateMergeTo.Between(txtDatePeriode1.SelectedDate, txtDatePeriode2.SelectedDate),
                        query.VerificationNo.IsNull());

            if (!string.IsNullOrEmpty(cboParamedicID.SelectedValue))
                query.Where(query.ParamedicID == cboParamedicID.SelectedValue);

            // exclude
            var aCollGuar = new AppStandardReferenceItemCollection();
            aCollGuar.Query.Where(aCollGuar.Query.StandardReferenceID == "PhysFeeCalcGuarExcl");
            aCollGuar.LoadAll();
            if (aCollGuar.Count > 0)
            {
                query.Where(regis.GuarantorID.NotIn(from a in aCollGuar select a.ItemID));
            }

            // proses hanya yang bisa diproses ke jasmed saja
            query.Where(paramedic.ParamedicFee == true);

            var app = new AppParameter();
            if (app.LoadByPrimaryKey("acc_IsAutoJournalPhysicianFeeBeforeVerification"))
            {
                if (app.ParameterValue == "Yes")
                {
                    query.Where(query.JournalId.IsNull());
                }
            }

            var coll = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
            coll.Load(query);

            DoCalculateFee(coll);
        }

        private void DoCalculateFee(ParamedicFeeTransChargesItemCompByDischargeDateCollection coll) {
            if (coll.Count > 0)
            {
                var freeGuars = new AppStandardReferenceItemCollection();
                freeGuars.Query.Select(freeGuars.Query.ItemID);
                freeGuars.Query.Where(
                    freeGuars.Query.StandardReferenceID == AppEnum.StandardReference.GuarantorFreeOfPhysicianFee,
                    freeGuars.Query.IsActive == true
                    );
                freeGuars.LoadAll();

                var itemExcs = new AppStandardReferenceItemCollection();
                itemExcs.Query.Select(itemExcs.Query.ItemID);
                itemExcs.Query.Where(
                    itemExcs.Query.StandardReferenceID == AppEnum.StandardReference.FeeItemExcForFreeGuar,
                    itemExcs.Query.IsActive == true
                    );
                itemExcs.LoadAll();

                var maxdisc = AppSession.Parameter.MaxDiscTxInPercentage;
                using (var trans = new esTransactionScope())
                {
                    if (AppSession.Parameter.IsPhysicianFeeCalcBasedOnGuarantorCategory)
                    {
                        foreach (var entity in coll)
                        {
                            var reg = new Registration();
                            reg.LoadByPrimaryKey(entity.GetColumn("RegistrationNo").ToString());

                            var freeGuar = (freeGuars.Where(i => i.ItemID == reg.GuarantorID)
                                                     .Select(i => i.ItemID)).Distinct().SingleOrDefault();

                            var itemExc = (itemExcs.Where(i => i.ItemID == entity.ItemID)
                                                     .Select(i => i.ItemID)).Distinct().SingleOrDefault();

                            var qty = Convert.ToDecimal(entity.GetColumn("Qty"));

                            var paramedicIdReferral = entity.GetColumn("ParamedicIDReferral").ToString();
                            var medic = new Paramedic();
                            if (medic.LoadByPrimaryKey(entity.ParamedicID ?? string.Empty))
                            {
                                var initFee = qty * (entity.Price - entity.Discount);
                                decimal maxdiscAmt;
                                decimal fee;
                                decimal deduction;

                                bool isRefferal, isCalInPercent, isCalDeducInPercent;
                                decimal calAmt, calDeduc;

                                if (entity.ParamedicID == paramedicIdReferral)
                                {
                                    //01. Paramedic - Item - Guarantor - Comp
                                    var mtxItemGuarComp = new ParamedicFeeItemGuarantorComp();
                                    if (mtxItemGuarComp.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString(), reg.GuarantorID, entity.GetColumn("TariffComponentID").ToString()))
                                    {
                                        if (mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false)
                                            deduction = (initFee ?? 0) * (mtxItemGuarComp.DeductionFeeAmountReferral ?? 0) / 100;
                                        else
                                            deduction = mtxItemGuarComp.DeductionFeeAmountReferral ?? 0;

                                        if (mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = (entity.Price ?? 0) * (mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                            fee = ((initFee ?? 0) - deduction) * (mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0;
                                            fee = qty * (mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                        fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = mtxItemGuarComp.ParamedicFeeAmountReferral ?? 0;
                                        isCalDeducInPercent = mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = mtxItemGuarComp.DeductionFeeAmountReferral ?? 0;
                                    }
                                    else
                                    {
                                        //02. Paramedic - Item - Comp
                                        var mtxItemComp = new ParamedicFeeItemComp();
                                        if (mtxItemComp.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString(), entity.GetColumn("TariffComponentID").ToString()))
                                        {
                                            if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                                deduction = (initFee ?? 0) * (mtxItemComp.DeductionFeeAmountReferral ?? 0) / 100;
                                            else
                                                deduction = mtxItemComp.DeductionFeeAmountReferral ?? 0;

                                            if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                                            {
                                                maxdiscAmt = (entity.Price ?? 0) * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                                fee = ((initFee ?? 0) - deduction) * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                            }
                                            else
                                            {
                                                maxdiscAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                                                fee = qty * (mtxItemComp.ParamedicFeeAmountReferral ?? 0);
                                            }
                                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                            fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                            isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                                            calAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                                            isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                                            calDeduc = mtxItemComp.DeductionFeeAmountReferral ?? 0;
                                        }
                                        else
                                        {
                                            //03. Paramedic - Item - Guarantor
                                            var mtxItemGuarr = new ParamedicFeeItemGuarantor();
                                            if (mtxItemGuarr.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString(), reg.GuarantorID))
                                            {
                                                if (mtxItemGuarr.IsDeductionFeeUsePercentage ?? false)
                                                    deduction = (initFee ?? 0) * (mtxItemGuarr.DeductionFeeAmountReferral ?? 0) / 100;
                                                else
                                                    deduction = mtxItemGuarr.DeductionFeeAmountReferral ?? 0;

                                                if (mtxItemGuarr.IsParamedicFeeUsePercentage ?? false)
                                                {
                                                    maxdiscAmt = (entity.Price ?? 0) * (mtxItemGuarr.ParamedicFeeAmountReferral ?? 0) / 100;
                                                    fee = ((initFee ?? 0) - deduction) * (mtxItemGuarr.ParamedicFeeAmountReferral ?? 0) / 100;
                                                }
                                                else
                                                {
                                                    maxdiscAmt = mtxItemGuarr.ParamedicFeeAmountReferral ?? 0;
                                                    fee = qty * (mtxItemGuarr.ParamedicFeeAmountReferral ?? 0);
                                                }
                                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                                isCalInPercent = mtxItemGuarr.IsParamedicFeeUsePercentage ?? false;
                                                calAmt = mtxItemGuarr.ParamedicFeeAmountReferral ?? 0;
                                                isCalDeducInPercent = mtxItemGuarr.IsDeductionFeeUsePercentage ?? false;
                                                calDeduc = mtxItemGuarr.DeductionFeeAmountReferral ?? 0;
                                            }
                                            else
                                            {
                                                //04. Paramedic - Item
                                                var matrix = new ParamedicFeeItem();
                                                if (matrix.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString()))
                                                {
                                                    if (matrix.IsDeductionFeeUsePercentage ?? false)
                                                        deduction = (initFee ?? 0) * (matrix.DeductionFeeAmountReferral ?? 0) / 100;
                                                    else
                                                        deduction = matrix.DeductionFeeAmountReferral ?? 0;

                                                    if (matrix.IsParamedicFeeUsePercentage ?? false)
                                                    {
                                                        maxdiscAmt = (entity.Price ?? 0) * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                                        fee = ((initFee ?? 0) - deduction) * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                                    }
                                                    else
                                                    {
                                                        maxdiscAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                                        fee = qty * (matrix.ParamedicFeeAmountReferral ?? 0);
                                                    }
                                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                    fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                                    isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                                    calAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                                    isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                                    calDeduc = matrix.DeductionFeeAmountReferral ?? 0;
                                                }
                                                else
                                                {
                                                    //05. Paramedic
                                                    if (medic.IsDeductionFeeUsePercentage ?? false)
                                                        deduction = (initFee ?? 0) * (medic.DeductionFeeAmountReferral ?? 0) / 100;
                                                    else
                                                        deduction = medic.DeductionFeeAmountReferral ?? 0;

                                                    if (medic.IsParamedicFeeUsePercentage ?? false)
                                                    {
                                                        maxdiscAmt = (entity.Price ?? 0) * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                                        fee = ((initFee ?? 0) - deduction) * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                                    }
                                                    else
                                                    {
                                                        maxdiscAmt = medic.ParamedicFeeAmountReferral ?? 0;
                                                        fee = qty * (medic.ParamedicFeeAmountReferral ?? 0);
                                                    }
                                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                    fee = entity.Discount >= maxdiscAmt ? 0 : fee;

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
                                    if (mtxItemGuarComp.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString(), reg.GuarantorID, entity.GetColumn("TariffComponentID").ToString()))
                                    {
                                        if (mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false)
                                            deduction = (initFee ?? 0) * (mtxItemGuarComp.DeductionFeeAmount ?? 0) / 100;
                                        else
                                            deduction = mtxItemGuarComp.DeductionFeeAmount ?? 0;

                                        if (mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = (entity.Price ?? 0) * (mtxItemGuarComp.ParamedicFeeAmount ?? 0) / 100;
                                            fee = ((initFee ?? 0) - deduction) * (mtxItemGuarComp.ParamedicFeeAmount ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = mtxItemGuarComp.ParamedicFeeAmount ?? 0;
                                            fee = qty * (mtxItemGuarComp.ParamedicFeeAmount ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                        fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = mtxItemGuarComp.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = mtxItemGuarComp.ParamedicFeeAmount ?? 0;
                                        isCalDeducInPercent = mtxItemGuarComp.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = mtxItemGuarComp.DeductionFeeAmount ?? 0;
                                    }
                                    else
                                    {
                                        //02. Paramedic - Item - Comp
                                        var mtxItemComp = new ParamedicFeeItemComp();
                                        if (mtxItemComp.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString(), entity.GetColumn("TariffComponentID").ToString()))
                                        {
                                            if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                                deduction = (initFee ?? 0) * (mtxItemComp.DeductionFeeAmount ?? 0) / 100;
                                            else
                                                deduction = mtxItemComp.DeductionFeeAmount ?? 0;

                                            if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                                            {
                                                maxdiscAmt = (entity.Price ?? 0) * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                                                fee = ((initFee ?? 0) - deduction) * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                                            }
                                            else
                                            {
                                                maxdiscAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                                                fee = qty * (mtxItemComp.ParamedicFeeAmount ?? 0);
                                            }
                                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                            fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                            isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                                            calAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                                            isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                                            calDeduc = mtxItemComp.DeductionFeeAmount ?? 0;
                                        }
                                        else
                                        {
                                            //03. Paramedic - Item - Guarantor
                                            var mtxItemGuarr = new ParamedicFeeItemGuarantor();
                                            if (mtxItemGuarr.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString(), reg.GuarantorID))
                                            {
                                                if (mtxItemGuarr.IsDeductionFeeUsePercentage ?? false)
                                                    deduction = (initFee ?? 0) * (mtxItemGuarr.DeductionFeeAmount ?? 0) / 100;
                                                else
                                                    deduction = mtxItemGuarr.DeductionFeeAmount ?? 0;

                                                if (mtxItemGuarr.IsParamedicFeeUsePercentage ?? false)
                                                {
                                                    maxdiscAmt = (entity.Price ?? 0) * (mtxItemGuarr.ParamedicFeeAmount ?? 0) / 100;
                                                    fee = ((initFee ?? 0) - deduction) * (mtxItemGuarr.ParamedicFeeAmount ?? 0) / 100;
                                                }
                                                else
                                                {
                                                    maxdiscAmt = mtxItemGuarr.ParamedicFeeAmount ?? 0;
                                                    fee = qty * (mtxItemGuarr.ParamedicFeeAmount ?? 0);
                                                }
                                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                                isCalInPercent = mtxItemGuarr.IsParamedicFeeUsePercentage ?? false;
                                                calAmt = mtxItemGuarr.ParamedicFeeAmount ?? 0;
                                                isCalDeducInPercent = mtxItemGuarr.IsDeductionFeeUsePercentage ?? false;
                                                calDeduc = mtxItemGuarr.DeductionFeeAmount ?? 0;
                                            }
                                            else
                                            {
                                                //04. Paramedic - Item
                                                var matrix = new ParamedicFeeItem();
                                                if (matrix.LoadByPrimaryKey(entity.ParamedicID, entity.GetColumn("ItemID").ToString()))
                                                {
                                                    if (matrix.IsDeductionFeeUsePercentage ?? false)
                                                        deduction = (initFee ?? 0) * (matrix.DeductionFeeAmount ?? 0) / 100;
                                                    else
                                                        deduction = matrix.DeductionFeeAmount ?? 0;

                                                    if (matrix.IsParamedicFeeUsePercentage ?? false)
                                                    {
                                                        maxdiscAmt = (entity.Price ?? 0) * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                                        fee = ((initFee ?? 0) - deduction) * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                                    }
                                                    else
                                                    {
                                                        maxdiscAmt = matrix.ParamedicFeeAmount ?? 0;
                                                        fee = qty * (matrix.ParamedicFeeAmount ?? 0);
                                                    }
                                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                    fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                                    isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                                    calAmt = matrix.ParamedicFeeAmount ?? 0;
                                                    isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                                    calDeduc = matrix.DeductionFeeAmount ?? 0;
                                                }
                                                else
                                                {
                                                    //05. Paramedic
                                                    if (medic.IsDeductionFeeUsePercentage ?? false)
                                                        deduction = (initFee ?? 0) * (medic.DeductionFeeAmount ?? 0) / 100;
                                                    else
                                                        deduction = medic.DeductionFeeAmount ?? 0;

                                                    if (medic.IsParamedicFeeUsePercentage ?? false)
                                                    {
                                                        maxdiscAmt = (entity.Price ?? 0) * (medic.ParamedicFeeAmount ?? 0) / 100;
                                                        fee = ((initFee ?? 0) - deduction) * (medic.ParamedicFeeAmount ?? 0) / 100;
                                                    }
                                                    else
                                                    {
                                                        maxdiscAmt = medic.ParamedicFeeAmount ?? 0;
                                                        fee = qty * (medic.ParamedicFeeAmount ?? 0);
                                                    }
                                                    maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                    fee = entity.Discount >= maxdiscAmt ? 0 : fee;

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
                                entity.FeeAmount = fee;
                                entity.IsRefferal = isRefferal;
                                entity.IsCalculatedInPercent = isCalInPercent;
                                entity.CalculatedAmount = calAmt;
                                entity.IsFree = freeGuar != null && itemExc == null;
                                entity.LastCalculatedByUserID = AppSession.UserLogin.UserID;
                                entity.LastCalculatedDateTime = DateTime.Now;
                                entity.IsCalcDeductionInPercent = isCalDeducInPercent;
                                entity.CalcDeductionAmount = calDeduc;
                                entity.DeductionAmount = deduction;
                                entity.Notes = (entity.Notes ?? string.Empty).Replace("/ FOC", "") + (entity.FeeAmount == 0 ? "/ FOC" : "");
                            }
                        }
                    }
                    else
                    {
                        // perhitungan berdasarkan guarantor category
                        foreach (var entity in coll)
                        {
                            var reg = new Registration();
                            reg.LoadByPrimaryKey(entity.GetColumn("RegistrationNo").ToString());

                            var guar = new Guarantor();
                            guar.LoadByPrimaryKey(reg.GuarantorID);

                            var guarCategory = guar.SRPhysicianFeeType ?? string.Empty;

                            var freeGuar = (freeGuars.Where(i => i.ItemID == reg.GuarantorID)
                                                     .Select(i => i.ItemID)).Distinct().SingleOrDefault();

                            var itemExc = (itemExcs.Where(i => i.ItemID == entity.ItemID)
                                                     .Select(i => i.ItemID)).Distinct().SingleOrDefault();

                            var qty = Convert.ToDecimal(entity.GetColumn("Qty"));

                            var paramedicIdReferral = entity.GetColumn("ParamedicIDReferral").ToString();

                            var medic = new ParamedicFeeGuarantorCategory();
                            if (medic.LoadByPrimaryKey(entity.ParamedicID, guarCategory))
                            {
                                var initFee = qty * (entity.Price - entity.Discount);
                                decimal maxdiscAmt;
                                decimal fee;
                                decimal deduction;

                                bool isRefferal, isCalInPercent, isCalDeducInPercent;
                                decimal calAmt, calDeduc;

                                if (entity.ParamedicID == paramedicIdReferral)
                                {
                                    //01. Paramedic - Guarantor Category - Item - Comp
                                    var mtxItemComp = new ParamedicFeeGuarantorCategoryItemComp();
                                    mtxItemComp.Query.Where(mtxItemComp.Query.ParamedicID == entity.ParamedicID,
                                        mtxItemComp.Query.SRPhysicianFeeType == guarCategory,
                                        mtxItemComp.Query.ItemID == entity.GetColumn("ItemID").ToString(),
                                        mtxItemComp.Query.TariffComponentID == entity.GetColumn("TariffComponentID").ToString());
                                    if(mtxItemComp.Load(mtxItemComp.Query)){
                                    //if (mtxItemComp.LoadByPrimaryKey(entity.ParamedicID, guarCategory, entity.GetColumn("ItemID").ToString(), entity.GetColumn("TariffComponentID").ToString()))
                                    //{
                                        if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                            deduction = (initFee ?? 0) * (mtxItemComp.DeductionFeeAmountReferral ?? 0) / 100;
                                        else
                                            deduction = mtxItemComp.DeductionFeeAmountReferral ?? 0;

                                        if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = (entity.Price ?? 0) * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                            fee = ((initFee ?? 0) - deduction) * (mtxItemComp.ParamedicFeeAmountReferral ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                                            fee = qty * (mtxItemComp.ParamedicFeeAmountReferral ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                        fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = mtxItemComp.ParamedicFeeAmountReferral ?? 0;
                                        isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = mtxItemComp.DeductionFeeAmountReferral ?? 0;
                                    }
                                    else
                                    {
                                        //02. Paramedic - Guarantor Category - Item
                                        var matrix = new ParamedicFeeGuarantorCategoryItem();
                                        matrix.Query.Where(matrix.Query.ParamedicID == entity.ParamedicID,
                                            matrix.Query.SRPhysicianFeeType == guarCategory,
                                            matrix.Query.ItemID == entity.GetColumn("ItemID").ToString());
                                        if(matrix.Load(matrix.Query)){
                                        //if (matrix.LoadByPrimaryKey(entity.ParamedicID, guarCategory, entity.GetColumn("ItemID").ToString()))
                                            if (matrix.IsDeductionFeeUsePercentage ?? false)
                                                deduction = (initFee ?? 0) * (matrix.DeductionFeeAmountReferral ?? 0) / 100;
                                            else
                                                deduction = matrix.DeductionFeeAmountReferral ?? 0;

                                            if (matrix.IsParamedicFeeUsePercentage ?? false)
                                            {
                                                maxdiscAmt = (entity.Price ?? 0) * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                                fee = ((initFee ?? 0) - deduction) * (matrix.ParamedicFeeAmountReferral ?? 0) / 100;
                                            }
                                            else
                                            {
                                                maxdiscAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                                fee = qty * (matrix.ParamedicFeeAmountReferral ?? 0);
                                            }
                                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                            fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                            isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                            calAmt = matrix.ParamedicFeeAmountReferral ?? 0;
                                            isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                            calDeduc = matrix.DeductionFeeAmountReferral ?? 0;
                                        }
                                        else
                                        {
                                            //03. Paramedic - Guarantor Category
                                            var mtxCat = new ParamedicFeeGuarantorCategory();
                                            if (mtxCat.LoadByPrimaryKey(entity.ParamedicID, guarCategory))
                                            {
                                                if (mtxCat.IsDeductionFeeUsePercentage ?? false)
                                                    deduction = (initFee ?? 0) * (mtxCat.DeductionFeeAmountReferral ?? 0) / 100;
                                                else
                                                    deduction = mtxCat.DeductionFeeAmountReferral ?? 0;

                                                if (mtxCat.IsParamedicFeeUsePercentage ?? false)
                                                {
                                                    maxdiscAmt = (entity.Price ?? 0) * (mtxCat.ParamedicFeeAmountReferral ?? 0) / 100;
                                                    fee = ((initFee ?? 0) - deduction) * (mtxCat.ParamedicFeeAmountReferral ?? 0) / 100;
                                                }
                                                else
                                                {
                                                    maxdiscAmt = mtxCat.ParamedicFeeAmountReferral ?? 0;
                                                    fee = qty * (mtxCat.ParamedicFeeAmountReferral ?? 0);
                                                }
                                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                                isCalInPercent = mtxCat.IsParamedicFeeUsePercentage ?? false;
                                                calAmt = mtxCat.ParamedicFeeAmountReferral ?? 0;
                                                isCalDeducInPercent = mtxCat.IsDeductionFeeUsePercentage ?? false;
                                                calDeduc = mtxCat.DeductionFeeAmountReferral ?? 0;
                                            }
                                            else
                                            {
                                                //04. Paramedic 
                                                if (medic.IsDeductionFeeUsePercentage ?? false)
                                                    deduction = (initFee ?? 0) * (medic.DeductionFeeAmountReferral ?? 0) / 100;
                                                else
                                                    deduction = medic.DeductionFeeAmountReferral ?? 0;

                                                if (medic.IsParamedicFeeUsePercentage ?? false)
                                                {
                                                    maxdiscAmt = (entity.Price ?? 0) * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                                    fee = ((initFee ?? 0) - deduction) * (medic.ParamedicFeeAmountReferral ?? 0) / 100;
                                                }
                                                else
                                                {
                                                    maxdiscAmt = medic.ParamedicFeeAmountReferral ?? 0;
                                                    fee = qty * (medic.ParamedicFeeAmountReferral ?? 0);
                                                }
                                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                fee = entity.Discount >= maxdiscAmt ? 0 : fee;

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
                                    mtxItemComp.Query.Where(mtxItemComp.Query.ParamedicID == entity.ParamedicID,
                                        mtxItemComp.Query.SRPhysicianFeeType == guarCategory,
                                        mtxItemComp.Query.ItemID == entity.GetColumn("ItemID").ToString(),
                                        mtxItemComp.Query.TariffComponentID == entity.GetColumn("TariffComponentID").ToString());
                                    if(mtxItemComp.Load(mtxItemComp.Query)){
                                    //if (mtxItemComp.LoadByPrimaryKey(entity.GetColumn("ItemID").ToString(), entity.ParamedicID, guarCategory, entity.GetColumn("TariffComponentID").ToString()))
                                    //{
                                        if (mtxItemComp.IsDeductionFeeUsePercentage ?? false)
                                            deduction = (initFee ?? 0) * (mtxItemComp.DeductionFeeAmount ?? 0) / 100;
                                        else
                                            deduction = mtxItemComp.DeductionFeeAmount ?? 0;

                                        if (mtxItemComp.IsParamedicFeeUsePercentage ?? false)
                                        {
                                            maxdiscAmt = (entity.Price ?? 0) * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                                            fee = ((initFee ?? 0) - deduction) * (mtxItemComp.ParamedicFeeAmount ?? 0) / 100;
                                        }
                                        else
                                        {
                                            maxdiscAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                                            fee = qty * (mtxItemComp.ParamedicFeeAmount ?? 0);
                                        }
                                        maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                        fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                        isCalInPercent = mtxItemComp.IsParamedicFeeUsePercentage ?? false;
                                        calAmt = mtxItemComp.ParamedicFeeAmount ?? 0;
                                        isCalDeducInPercent = mtxItemComp.IsDeductionFeeUsePercentage ?? false;
                                        calDeduc = mtxItemComp.DeductionFeeAmount ?? 0;
                                    }
                                    else
                                    {
                                        //02. Paramedic - Guarantor Category - Item
                                        var matrix = new ParamedicFeeGuarantorCategoryItem();
                                        matrix.Query.Where(matrix.Query.ParamedicID == entity.ParamedicID,
                                            matrix.Query.SRPhysicianFeeType == guarCategory,
                                            matrix.Query.ItemID == entity.GetColumn("ItemID").ToString());
                                        if(matrix.Load(matrix.Query)){
                                        //if (matrix.LoadByPrimaryKey(entity.GetColumn("ItemID").ToString(), entity.ParamedicID, guarCategory))
                                        //{
                                            if (matrix.IsDeductionFeeUsePercentage ?? false)
                                                deduction = (initFee ?? 0) * (matrix.DeductionFeeAmount ?? 0) / 100;
                                            else
                                                deduction = matrix.DeductionFeeAmount ?? 0;

                                            if (matrix.IsParamedicFeeUsePercentage ?? false)
                                            {
                                                maxdiscAmt = (entity.Price ?? 0) * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                                fee = ((initFee ?? 0) - deduction) * (matrix.ParamedicFeeAmount ?? 0) / 100;
                                            }
                                            else
                                            {
                                                maxdiscAmt = matrix.ParamedicFeeAmount ?? 0;
                                                fee = qty * (matrix.ParamedicFeeAmount ?? 0);
                                            }
                                            maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                            fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                            isCalInPercent = matrix.IsParamedicFeeUsePercentage ?? false;
                                            calAmt = matrix.ParamedicFeeAmount ?? 0;
                                            isCalDeducInPercent = matrix.IsDeductionFeeUsePercentage ?? false;
                                            calDeduc = matrix.DeductionFeeAmount ?? 0;
                                        }
                                        else
                                        {
                                            //03. Paramedic - Guarantor Category
                                            var mtxCat = new ParamedicFeeGuarantorCategory();
                                            if (mtxCat.LoadByPrimaryKey(entity.ParamedicID, guarCategory))
                                            {
                                                if (mtxCat.IsDeductionFeeUsePercentage ?? false)
                                                    deduction = (initFee ?? 0) * (mtxCat.DeductionFeeAmount ?? 0) / 100;
                                                else
                                                    deduction = mtxCat.DeductionFeeAmount ?? 0;

                                                if (mtxCat.IsParamedicFeeUsePercentage ?? false)
                                                {
                                                    maxdiscAmt = (entity.Price ?? 0) * (mtxCat.ParamedicFeeAmount ?? 0) / 100;
                                                    fee = ((initFee ?? 0) - deduction) * (mtxCat.ParamedicFeeAmount ?? 0) / 100;
                                                }
                                                else
                                                {
                                                    maxdiscAmt = mtxCat.ParamedicFeeAmount ?? 0;
                                                    fee = qty * (mtxCat.ParamedicFeeAmount ?? 0);
                                                }
                                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                                isCalInPercent = mtxCat.IsParamedicFeeUsePercentage ?? false;
                                                calAmt = mtxCat.ParamedicFeeAmount ?? 0;
                                                isCalDeducInPercent = mtxCat.IsDeductionFeeUsePercentage ?? false;
                                                calDeduc = mtxCat.DeductionFeeAmount ?? 0;
                                            }
                                            else
                                            {
                                                //04. Paramedic
                                                if (medic.IsDeductionFeeUsePercentage ?? false)
                                                    deduction = (initFee ?? 0) * (medic.DeductionFeeAmount ?? 0) / 100;
                                                else
                                                    deduction = medic.DeductionFeeAmount ?? 0;

                                                if (medic.IsParamedicFeeUsePercentage ?? false)
                                                {
                                                    maxdiscAmt = (entity.Price ?? 0) * (medic.ParamedicFeeAmount ?? 0) / 100;
                                                    fee = ((initFee ?? 0) - deduction) * (medic.ParamedicFeeAmount ?? 0) / 100;
                                                }
                                                else
                                                {
                                                    maxdiscAmt = medic.ParamedicFeeAmount ?? 0;
                                                    fee = qty * (medic.ParamedicFeeAmount ?? 0);
                                                }
                                                maxdiscAmt = maxdisc == 0 ? maxdiscAmt : ((entity.Price ?? 0) * maxdisc / 100);
                                                fee = entity.Discount >= maxdiscAmt ? 0 : fee;

                                                isCalInPercent = medic.IsParamedicFeeUsePercentage ?? false;
                                                calAmt = medic.ParamedicFeeAmount ?? 0;
                                                isCalDeducInPercent = medic.IsDeductionFeeUsePercentage ?? false;
                                                calDeduc = medic.DeductionFeeAmount ?? 0;
                                            }
                                        }
                                    }

                                    isRefferal = false;
                                }
                                entity.FeeAmount = fee;
                                entity.IsRefferal = isRefferal;
                                entity.IsCalculatedInPercent = isCalInPercent;
                                entity.CalculatedAmount = calAmt;
                                entity.IsFree = freeGuar != null && itemExc == null;
                                entity.LastCalculatedByUserID = AppSession.UserLogin.UserID;
                                entity.LastCalculatedDateTime = DateTime.Now;
                                entity.IsCalcDeductionInPercent = isCalDeducInPercent;
                                entity.CalcDeductionAmount = calDeduc;
                                entity.DeductionAmount = deduction;
                                entity.Notes = (entity.Notes ?? string.Empty).Replace("/ FOC", "") + (entity.FeeAmount == 0 ? "/ FOC" : "");
                            }
                        }
                    }

                    coll.Save();

                    trans.Complete();
                }
            }
        }

        private void Print()
        {
            var jobParameters = new PrintJobParameterCollection();

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "StartDate";
            parDate1.ValueDateTime = txtDatePeriode1.SelectedDate ?? Convert.ToDateTime("1900-01-01 00:00:00");

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "EndDate";
            parDate2.ValueDateTime = txtDatePeriode2.SelectedDate ?? DateTime.Now.AddDays(10);

            var parPhysician = jobParameters.AddNew();
            parPhysician.Name = "ParamedicID";
            parPhysician.ValueString = cboParamedicID.SelectedValue;

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.PhysicianFeeCalculationDraftSlip;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }
    }
}
