using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReceiveSummaryList : BasePageDialog
    {
        private AppAutoNumberLast _autoNumberTrans;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PaymentReceive;
            Title = "Billing Summary";

            if (!IsPostBack)
            {
                var r = new Registration();
                r.LoadByPrimaryKey(Request.QueryString["regNo"]);
                RadToolBar2.Items[1].Text = r.IsHoldTransactionEntry ?? false ? "Unlock" : "Lock";
            }
        }

        private DataTable CostCalculations
        {
            get
            {
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"]);

                DataTable table = new DataTable();

                //trans charges
                CostCalculationQuery query = new CostCalculationQuery("a");
                TransChargesQuery trans = new TransChargesQuery("b");
                ServiceUnitQuery unit = new ServiceUnitQuery("c");
                ItemQuery item = new ItemQuery("d");
                var regis = new RegistrationQuery("f");
                var ti = new TransChargesItemQuery("g");

                query.Select
                    (
                        query.TransactionNo,
                        query.SequenceNo,
                        ti.ReferenceNo,
                        ti.ReferenceSequenceNo,
                        ti.ChargeQuantity,
                        unit.ServiceUnitName,
                        item.ItemName,
                        trans.TransactionDate,
                        query.PatientAmount,
                        query.GuarantorAmount,
                        query.DiscountAmount,
                        @"<CASE WHEN (
                                     SELECT TOP 1 p.ParamedicName
                                     FROM   TransChargesItemComp tcic
                                            INNER JOIN TariffComponent tc
                                                 ON  tc.TariffComponentID = tcic.TariffComponentID
                                                 AND tc.IsTariffParamedic = 1
                                            LEFT JOIN Paramedic p
                                                 ON  p.ParamedicID = tcic.ParamedicID
                                     WHERE  tcic.TransactionNo = a.TransactionNo
                                            AND tcic.SequenceNo = a.SequenceNo
                                     ORDER BY
                                            tc.TariffComponentID DESC
                                 ) IS NULL THEN ''
                            ELSE (
                                     SELECT TOP 1 p.ParamedicName
                                     FROM   TransChargesItemComp tcic
                                            INNER JOIN TariffComponent tc
                                                 ON  tc.TariffComponentID = tcic.TariffComponentID
                                                 AND tc.IsTariffParamedic = 1
                                            LEFT JOIN Paramedic p
                                                 ON  p.ParamedicID = tcic.ParamedicID
                                     WHERE  tcic.TransactionNo = a.TransactionNo
                                            AND tcic.SequenceNo = a.SequenceNo
                                     ORDER BY
                                            tc.TariffComponentID DESC
                            )
                        END AS ParamedicName>"
                    );

                query.InnerJoin(trans).On(query.TransactionNo == trans.TransactionNo);
                query.InnerJoin(unit).On(trans.ToServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(regis).On(trans.RegistrationNo == regis.RegistrationNo);
                query.InnerJoin(ti).On(
                    ti.TransactionNo == query.TransactionNo &&
                    ti.SequenceNo == query.SequenceNo
                    );
                query.Where(query.RegistrationNo.In(reg),trans.PackageReferenceNo.IsNull());

                query.Where(
                    query.Or(
                        query.ParentNo == string.Empty,
                        query.ParentNo.IsNull()
                        )
                    );

                query.OrderBy
                    (
                        query.TransactionNo.Ascending,
                        query.SequenceNo.Ascending
                    );

                table = query.LoadDataTable();

                //trans prescription
                query = new CostCalculationQuery("a");
                TransPrescriptionQuery trans2 = new TransPrescriptionQuery("b");
                unit = new ServiceUnitQuery("c");
                item = new ItemQuery("d");
                regis = new RegistrationQuery("f");
                var medic = new ParamedicQuery("e");
                var ti2 = new TransPrescriptionItemQuery("g");
                var ti3 = new TransPrescriptionQuery("h");

                query.Select
                    (
                        query.TransactionNo,
                        query.SequenceNo,
                        ti3.ReferenceNo,
                        ti2.SequenceNo.As("ReferenceSequenceNo"),
                        ti2.ResultQty.As("ChargeQuantity"),
                        unit.ServiceUnitName,
                        item.ItemName,
                        trans2.PrescriptionDate.As("TransactionDate"),
                        query.PatientAmount,
                        query.GuarantorAmount,
                        query.DiscountAmount,
                        medic.ParamedicName
                    );

                query.InnerJoin(trans2).On(query.TransactionNo == trans2.PrescriptionNo);
                query.InnerJoin(unit).On(trans2.ServiceUnitID == unit.ServiceUnitID);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(regis).On(trans2.RegistrationNo == regis.RegistrationNo);
                query.InnerJoin(medic).On(regis.ParamedicID == medic.ParamedicID);

                query.InnerJoin(ti2).On(
                    ti2.PrescriptionNo == query.TransactionNo &&
                    ti2.SequenceNo == query.SequenceNo
                    );
                query.InnerJoin(ti3).On(ti2.PrescriptionNo == ti3.PrescriptionNo);
                query.Where(query.RegistrationNo.In(reg));

                query.OrderBy(query.TransactionNo.Ascending, query.SequenceNo.Ascending);

                table.Merge(query.LoadDataTable());

                //return data
                return table;
            }
        }

        protected void grdBillingSummary_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable dtb = CostCalculations.Copy();

            var temp = CostCalculations.AsEnumerable().Where(c => !string.IsNullOrEmpty(c.Field<string>("ReferenceNo")) &&
                                                                  !string.IsNullOrEmpty(c.Field<string>("ReferenceSequenceNo")))
                                                      .GroupBy(c => new
                                                      {
                                                          ReferenceNo = c.Field<string>("ReferenceNo"),
                                                          ReferenceSequenceNo = c.Field<string>("ReferenceSequenceNo")
                                                      })
                                                      .Select(g => new
                                                      {
                                                          g.Key.ReferenceNo,
                                                          g.Key.ReferenceSequenceNo,
                                                          ChargeQuantity = g.Sum(c => c.Field<decimal>("ChargeQuantity")),
                                                          PatientAmount = g.Sum(c => c.Field<decimal>("PatientAmount")),
                                                          GuarantorAmount = g.Sum(c => c.Field<decimal>("GuarantorAmount")),
                                                          DiscountAmount = g.Sum(c => c.Field<decimal>("DiscountAmount"))
                                                      });

            foreach (DataRow row in dtb.Rows)
            {
                if (row["ReferenceNo"].ToString() != string.Empty && row["ReferenceSequenceNo"].ToString() != string.Empty)
                    continue;

                foreach (var tmp in temp.Where(tmp => row["TransactionNo"].ToString() == tmp.ReferenceNo &&
                                                      row["SequenceNo"].ToString() == tmp.ReferenceSequenceNo))
                {
                    row["PatientAmount"] = (decimal)row["PatientAmount"] + tmp.PatientAmount;
                    row["GuarantorAmount"] = (decimal)row["GuarantorAmount"] + tmp.GuarantorAmount;
                    row["DiscountAmount"] = (decimal)row["DiscountAmount"] + tmp.DiscountAmount;
                    row["ChargeQuantity"] = (decimal)row["ChargeQuantity"] + tmp.ChargeQuantity;
                }
            }

            dtb.AcceptChanges();

            foreach (DataRow row in dtb.Rows.Cast<DataRow>().Where(row => ((decimal)row["ChargeQuantity"] <= 0)))
            {
                row.Delete();
            }

            dtb.AcceptChanges();

            grdBillingSummary.DataSource = dtb;
        }

        private DataTable PendingCalculations
        {
            get
            {
                string[] reg = Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"]);

                var table = new DataTable();

                //transcharges
                var trans = new TransChargesQuery("a");
                var transItem = new TransChargesItemQuery("b");
                var unit = new ServiceUnitQuery("c");
                var item = new ItemQuery("d");

                trans.Select(
                    trans.TransactionNo,
                    transItem.SequenceNo,
                    unit.ServiceUnitName,
                    item.ItemName,
                    trans.TransactionDate,
                    ((transItem.ChargeQuantity * (transItem.Price - transItem.DiscountAmount)) + transItem.CitoAmount).As("Total")
                    );
                trans.InnerJoin(transItem).On(trans.TransactionNo == transItem.TransactionNo);
                trans.InnerJoin(unit).On(trans.ToServiceUnitID == unit.ServiceUnitID);
                trans.InnerJoin(item).On(transItem.ItemID == item.ItemID);
                trans.Where(
                    trans.RegistrationNo.In(reg),
                    //trans.IsApproved == false,
                    trans.Or(
                        trans.PackageReferenceNo == string.Empty,
                        trans.PackageReferenceNo.IsNull()
                        ),
                    trans.IsVoid == false,
                    //transItem.IsApprove == false,
                    transItem.IsVoid == false,
                    transItem.IsBillProceed == false,
                    transItem.Or(
                        transItem.ParentNo == string.Empty,
                        transItem.ParentNo.IsNull()
                        )
                    );
                trans.OrderBy(
                    trans.TransactionNo.Ascending,
                    transItem.SequenceNo.Ascending
                    );

                table = trans.LoadDataTable();

                //transprescription1
                var trans2 = new TransPrescriptionQuery("a");
                var transItem2 = new TransPrescriptionItemQuery("b");
                unit = new ServiceUnitQuery("c");
                item = new ItemQuery("d");

                trans2.Select(
                        trans2.PrescriptionNo.As("TransactionNo"),
                        transItem2.SequenceNo,
                        unit.ServiceUnitName,
                        item.ItemName,
                        trans2.PrescriptionDate.As("TransactionDate"),
                        (transItem2.ResultQty * (transItem2.Price - transItem2.DiscountAmount)).As("Total")
                    );
                trans2.InnerJoin(transItem2).On(trans2.PrescriptionNo == transItem2.PrescriptionNo);
                trans2.InnerJoin(unit).On(trans2.ServiceUnitID == unit.ServiceUnitID);
                trans2.InnerJoin(item).On(transItem2.ItemID == item.ItemID);
                trans2.Where(
                    trans2.RegistrationNo.In(reg),
                    trans2.IsApproval == false,
                    trans2.IsVoid == false,
                    transItem2.IsApprove == false,
                    transItem2.IsVoid == false
                    );
                trans2.OrderBy(
                    trans2.PrescriptionNo.Ascending,
                    transItem2.SequenceNo.Ascending
                    );

                table.Merge(trans2.LoadDataTable());

                //return data
                return table;
            }
        }

        protected void grdPendingSummary_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPendingSummary.DataSource = PendingCalculations;
        }

        protected void cboItemMateraiID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboItemMateraiID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var item = new ItemQuery("a");
            item.es.Top = 10;
            item.Select
                (
                    item.ItemID,
                    item.ItemName
                );
            item.Where
                (
                    item.Or
                        (
                            item.ItemID.Like(searchTextContain),
                            item.ItemName.Like(searchTextContain)
                        ),
                    item.IsActive == true,
                    item.ItemGroupID == AppSession.Parameter.ItemGroupMaterai
                );
            item.OrderBy(item.ItemID.Ascending);

            (o as RadComboBox).DataSource = item.LoadDataTable();
            (o as RadComboBox).DataBind();
        }

        private string GetNewTransactionNo()
        {
            _autoNumberTrans = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.TransactionNo);
            return _autoNumberTrans.LastCompleteNumber;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'refresh'";
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            switch (eventArgument)
            {
                case "process":
                    Process();

                    grdBillingSummary.Rebind();
                    grdPendingSummary.Rebind();
                    break;
                case "refresh":
                    grdBillingSummary.Rebind();
                    grdPendingSummary.Rebind();
                    break;
                case "lock":
                    grdBillingSummary.Rebind();
                    grdPendingSummary.Rebind();

                    var regs = new RegistrationCollection();
                    regs.Query.Where(regs.Query.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regNo"])));
                    regs.LoadAll();
                    using (var trans = new esTransactionScope())
                    {
                        foreach (var reg in regs)
                        {
                            reg.IsHoldTransactionEntry = !(reg.IsHoldTransactionEntry ?? false);
                        }

                        regs.Save();
                        trans.Complete();
                    }

                    var r = new Registration();
                    r.LoadByPrimaryKey(Request.QueryString["regNo"]);
                    RadToolBar2.Items[1].Text = r.IsHoldTransactionEntry ?? false ? "Unlock" : "Lock";
                    break;
            }
        }

        private void Process()
        {
            var cboItemMateraiID = Helper.FindControlRecursive(Page, "cboItemMateraiID") as RadComboBox;

            if (string.IsNullOrEmpty(cboItemMateraiID.SelectedValue))
                return;

            //registration
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            //guarantor
            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);

            #region Materai
            var transCharges = new TransCharges();
            var transChargesItem = new TransChargesItem();
            var transChargesItemCompColl = new TransChargesItemCompCollection();
            var costCalculations = new CostCalculationCollection();

            if (!string.IsNullOrEmpty(cboItemMateraiID.SelectedValue))
            {
                transCharges.TransactionNo = GetNewTransactionNo();
                _autoNumberTrans.LastCompleteNumber = transCharges.TransactionNo;
                _autoNumberTrans.Save();

                transCharges.RegistrationNo = reg.RegistrationNo;
                transCharges.TransactionDate = DateTime.Now;
                transCharges.ReferenceNo = string.Empty;
                transCharges.FromServiceUnitID = reg.ServiceUnitID;
                transCharges.ToServiceUnitID = reg.ServiceUnitID;
                transCharges.ClassID = reg.ChargeClassID;
                transCharges.RoomID = reg.RoomID;
                transCharges.BedID = reg.BedID;
                transCharges.DueDate = DateTime.Now.Date;
                transCharges.SRShift = Registration.GetShiftID();
                transCharges.SRItemType = string.Empty;
                transCharges.IsProceed = false;
                transCharges.IsBillProceed = true;
                transCharges.IsApproved = true;
                transCharges.IsVoid = false;
                transCharges.IsOrder = false;
                transCharges.IsCorrection = false;
                transCharges.IsClusterAssign = false;
                transCharges.IsAutoBillTransaction = true;
                transCharges.Notes = string.Empty;
                transCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                transCharges.LastUpdateDateTime = DateTime.Now;

                transChargesItem.TransactionNo = transCharges.TransactionNo;
                transChargesItem.SequenceNo = "001";
                transChargesItem.ReferenceNo = string.Empty;
                transChargesItem.ReferenceSequenceNo = string.Empty;
                transChargesItem.ItemID = cboItemMateraiID.SelectedValue;
                transChargesItem.ChargeClassID = reg.ChargeClassID;
                transChargesItem.ParamedicID = string.Empty;

                ItemTariff tariff = (Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, grr.SRTariffType,
                                                                 transCharges.ClassID, transCharges.ClassID,
                                                                 cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                     Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, grr.SRTariffType,
                                                                 AppSession.Parameter.DefaultTariffClass, transCharges.ClassID,
                                                                 cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType)) ??
                                    (Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                                              transCharges.ClassID, transCharges.ClassID,
                                                              cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType) ??
                                  Helper.Tariff.GetItemTariff(transCharges.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                                                              AppSession.Parameter.DefaultTariffClass, transCharges.ClassID,
                                                              cboItemMateraiID.SelectedValue, reg.GuarantorID, false, reg.SRRegistrationType));

                transChargesItem.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                var itemService = new ItemService();
                itemService.LoadByPrimaryKey(cboItemMateraiID.SelectedValue);
                transChargesItem.SRItemUnit = itemService.SRItemUnit;

                transChargesItem.CostPrice = tariff.Price ?? 0;
                transChargesItem.IsVariable = false;
                transChargesItem.IsCito = false;
                transChargesItem.ChargeQuantity = (decimal)1D;
                transChargesItem.StockQuantity = (decimal)0D;
                transChargesItem.Price = tariff.Price ?? 0;
                transChargesItem.DiscountAmount = (decimal)0D;
                transChargesItem.CitoAmount = (decimal)0D;
                transChargesItem.RoundingAmount = Helper.RoundingDiff;
                transChargesItem.SRDiscountReason = string.Empty;
                transChargesItem.IsAssetUtilization = false;
                transChargesItem.AssetID = string.Empty;
                transChargesItem.IsBillProceed = true;
                transChargesItem.IsOrderRealization = false;
                transChargesItem.IsPackage = false;
                transChargesItem.IsApprove = true;
                transChargesItem.IsVoid = false;
                transChargesItem.LastUpdateByUserID = AppSession.UserLogin.UserID;
                transChargesItem.LastUpdateDateTime = DateTime.Now;
                transChargesItem.ParentNo = string.Empty;
                transChargesItem.SRCenterID = string.Empty;

                #region item component

                var compQuery = new ItemTariffComponentQuery();
                compQuery.es.Top = 1;
                compQuery.Where
                    (
                        compQuery.SRTariffType == grr.SRTariffType,
                        compQuery.ItemID == cboItemMateraiID.SelectedValue,
                        compQuery.ClassID == reg.ChargeClassID,
                        compQuery.StartingDate <= DateTime.Now.Date
                    );

                var compColl = Helper.Tariff.GetItemTariffComponentCollection(
                    transCharges.TransactionDate.Value, grr.SRTariffType, transCharges.ClassID, cboItemMateraiID.SelectedValue);
                if (!compColl.Any())
                    compColl = Helper.Tariff.GetItemTariffComponentCollection(transCharges.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                        AppSession.Parameter.DefaultTariffClass, cboItemMateraiID.SelectedValue);

                foreach (ItemTariffComponent comp in compColl)
                {
                    var transChargesItemComp = transChargesItemCompColl.AddNew();
                    transChargesItemComp.TransactionNo = transCharges.TransactionNo;
                    transChargesItemComp.SequenceNo = "001";
                    transChargesItemComp.TariffComponentID = comp.TariffComponentID;
                    transChargesItemComp.Price = comp.Price;
                    transChargesItemComp.DiscountAmount = (decimal)0D;
                    transChargesItemComp.ParamedicID = string.Empty;
                    transChargesItemComp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    transChargesItemComp.LastUpdateDateTime = DateTime.Now;
                }
                #endregion

                #region Cost Calculation
                var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grr.GuarantorID, cboItemMateraiID.SelectedValue, DateTime.Now, false);

                var grrID = reg.GuarantorID;

                var pat = new Patient();
                pat.LoadByPrimaryKey(reg.PatientID);

                if (grrID == AppSession.Parameter.SelfGuarantor)
                {
                    if (!string.IsNullOrEmpty(pat.MemberID))
                        grrID = pat.MemberID;
                }

                var rowCovered = tblCovered.AsEnumerable().Where(t => t.Field<string>("ItemID") == transChargesItem.ItemID &&
                                                                      t.Field<bool>("IsInclude")).SingleOrDefault();

                //TransChargesItemComps
                if (rowCovered != null)
                {
                    decimal? discount = 0;
                    bool isDiscount = false, isMargin = false;
                    foreach (var comp in transChargesItemCompColl.Where(t => t.TransactionNo == transChargesItem.TransactionNo &&
                                                                             t.SequenceNo == transChargesItem.SequenceNo)
                                                                 .OrderBy(t => t.TariffComponentID))
                    {
                        decimal? amountValue = (decimal?)rowCovered["AmountValue"];
                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                        {
                            if ((comp.Price - comp.DiscountAmount) <= 0)
                                continue;

                            if ((bool)rowCovered["IsValueInPercent"])
                            {
                                comp.DiscountAmount += (amountValue / 100) * comp.Price;
                                comp.AutoProcessCalculation = 0 - (amountValue / 100) * comp.Price;
                            }
                            else
                            {
                                if (!isDiscount)
                                {
                                    if (discount == 0)
                                    {
                                        if (comp.Price >= amountValue)
                                        {
                                            comp.DiscountAmount += amountValue;
                                            comp.AutoProcessCalculation = 0 - amountValue;
                                            isDiscount = true;
                                        }
                                        else
                                        {
                                            comp.DiscountAmount += comp.Price;
                                            comp.AutoProcessCalculation = 0 - comp.Price;
                                            discount = amountValue - comp.Price;
                                        }
                                    }
                                    else
                                    {
                                        if (comp.Price >= discount)
                                        {
                                            comp.DiscountAmount += discount;
                                            comp.AutoProcessCalculation = 0 - discount;
                                            isDiscount = true;
                                        }
                                        else
                                        {
                                            comp.DiscountAmount += comp.Price;
                                            comp.AutoProcessCalculation = 0 - comp.Price;
                                            discount -= comp.Price;
                                        }
                                    }
                                }
                            }
                        }
                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                        {
                            if ((bool)rowCovered["IsValueInPercent"])
                            {
                                comp.Price += (amountValue / 100) * comp.Price;
                                comp.AutoProcessCalculation = (amountValue / 100) * comp.Price;
                            }
                            else
                            {
                                if (!isMargin)
                                {
                                    comp.Price += amountValue;
                                    comp.AutoProcessCalculation = amountValue;
                                    isMargin = true;
                                }
                            }
                        }
                    }
                }

                //TransChargesItems
                if (transChargesItemCompColl.Count > 0)
                {
                    transChargesItem.AutoProcessCalculation = transChargesItemCompColl.Where(t => t.TransactionNo == transChargesItem.TransactionNo &&
                                                                                                  t.SequenceNo == transChargesItem.SequenceNo)
                                                                                      .Sum(t => t.AutoProcessCalculation);
                    if (transChargesItem.AutoProcessCalculation < 0)
                    {
                        transChargesItem.DiscountAmount += transChargesItem.ChargeQuantity * Math.Abs(transChargesItem.AutoProcessCalculation ?? 0);

                        if (transChargesItem.DiscountAmount > transChargesItem.Price)
                        {
                            transChargesItem.DiscountAmount = transChargesItem.Price;
                            transChargesItem.AutoProcessCalculation = 0 - transChargesItem.Price;
                        }
                    }
                    else if (transChargesItem.AutoProcessCalculation > 0)
                        transChargesItem.Price += transChargesItem.AutoProcessCalculation;
                }
                else
                {
                    if (rowCovered != null)
                    {
                        if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeDiscount))
                        {
                            if ((bool)rowCovered["IsValueInPercent"])
                                transChargesItem.DiscountAmount += (transChargesItem.ChargeQuantity ?? 0) * (((decimal)rowCovered["AmountValue"] / 100) * transChargesItem.Price);
                            else
                                transChargesItem.DiscountAmount += (transChargesItem.ChargeQuantity ?? 0) * (decimal)rowCovered["AmountValue"];

                            if (transChargesItem.DiscountAmount > transChargesItem.Price)
                                transChargesItem.DiscountAmount = transChargesItem.Price;

                            transChargesItem.AutoProcessCalculation = 0 - transChargesItem.DiscountAmount;
                        }
                        else if (rowCovered["SRGuarantorRuleType"].ToString().Equals(AppSession.Parameter.GuarantorRuleTypeMargin))
                        {
                            if ((bool)rowCovered["IsValueInPercent"])
                                transChargesItem.Price += ((decimal)rowCovered["AmountValue"] / 100) * transChargesItem.Price;
                            else
                                transChargesItem.Price += (decimal)rowCovered["AmountValue"];

                            transChargesItem.AutoProcessCalculation = transChargesItem.Price;
                        }
                    }
                }

                //post
                decimal? total = ((transChargesItem.ChargeQuantity * transChargesItem.Price) -
                                  transChargesItem.DiscountAmount) + transChargesItem.CitoAmount;
                var calc = new Helper.CostCalculation(grrID, transChargesItem.ItemID, total ?? 0, tblCovered,
                                                      transChargesItem.ChargeQuantity ?? 0, transChargesItem.DiscountAmount ?? 0);

                //CostCalculation
                CostCalculation cost = costCalculations.AddNew();
                cost.RegistrationNo = reg.RegistrationNo;
                cost.TransactionNo = transChargesItem.TransactionNo;
                cost.SequenceNo = transChargesItem.SequenceNo;
                cost.ItemID = transChargesItem.ItemID;
                cost.PatientAmount = calc.PatientAmount;
                cost.GuarantorAmount = calc.GuarantorAmount;
                cost.DiscountAmount = transChargesItem.DiscountAmount;
                cost.IsPackage = transChargesItem.IsPackage;
                cost.ParentNo = transChargesItem.ParentNo;
                cost.ParamedicAmount = transChargesItem.ChargeQuantity * transChargesItemCompColl.Where(comp => comp.TransactionNo == transChargesItem.TransactionNo &&
                                                                                                                comp.SequenceNo == transChargesItem.SequenceNo &&
                                                                                                                !string.IsNullOrEmpty(comp.ParamedicID))
                                                                                                 .Sum(comp => comp.Price - comp.DiscountAmount);
                cost.LastUpdateDateTime = DateTime.Now;
                cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                #endregion
            }
            #endregion

            using (esTransactionScope trans = new esTransactionScope())
            {

                if (!string.IsNullOrEmpty(cboItemMateraiID.SelectedValue))
                {
                    transCharges.Save();
                    transChargesItem.Save();
                    transChargesItemCompColl.Save();
                    costCalculations.Save();

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                    {
                        if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                        {
                            JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, transCharges.TransactionNo, AppSession.UserLogin.UserID, 0);
                        }
                        else {
                            int? journalId = JournalTransactions.AddNewIncomeJournal(transCharges, transChargesItemCompColl, reg,
                                                                                 unit, costCalculations, "SU",
                                                                                 AppSession.UserLogin.UserID, 0);
                        }
                    }
                }
                trans.Complete();
            }
        }
    }
}
