using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;

namespace Temiang.Avicenna.Module.Charges.Dispensary
{
    public partial class PrescriptionTransactionList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.PrescriptionReturn;

            if (IsPostBack) return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(reg.ServiceUnitID);

            txtRegistrationNo.Text = reg.RegistrationNo;
            txtMedicalNo.Text = pat.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", pat.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = pat.PatientName;
            txtServiceUnitID.Text = reg.BedID == string.Empty ? unit.ServiceUnitName : unit.ServiceUnitName + " [Bed No: " + reg.BedID + "]";
            txtGender.Text = pat.Sex;
            txtPlaceDOB.Text = string.Format("{0}, {1}", pat.CityOfBirth, Convert.ToDateTime(pat.DateOfBirth).ToString("dd-MMM-yyyy"));
            txtAgeDay.Value = Helper.GetAgeInDay(pat.DateOfBirth.Value);
            txtAgeMonth.Value = Helper.GetAgeInMonth(pat.DateOfBirth.Value);
            txtAgeYear.Value = Helper.GetAgeInYear(pat.DateOfBirth.Value);

            ComboBox.PopulateWithServiceUnitForTransaction(cboDispensaryID, BusinessObject.Reference.TransactionCode.Prescription, true);

            grdTransPrescriptionItem.Columns[grdTransPrescriptionItem.Columns.Count - 2].Visible = (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) > 0;

            pnlDispensary.Visible = AppSession.Parameter.IsPrescriptionReturnToOneLocation;
            if (pnlDispensary.Visible && AppSession.Parameter.IsPrescriptionReturnToOneLocationWithUserDefUnit)
            {
                var usr = new AppUser();
                if (usr.LoadByPrimaryKey(AppSession.UserLogin.UserID) && !string.IsNullOrEmpty(usr.ServiceUnitID))
                {
                    cboDispensaryID.SelectedValue = usr.ServiceUnitID;
                    ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, cboDispensaryID.SelectedValue);
                    cboLocationID.SelectedIndex = 1;
                }
            }
            
            if (!string.IsNullOrEmpty(OrderNo))
            {
                var order = new TransPrescriptionOrder();
                if (order.LoadByPrimaryKey(OrderNo))
                    txtNotes.Text = order.Notes;
            }

            (Helper.FindControlRecursive(Page, "btnOk") as Button).Attributes["onClick"] = "if (!confirm('Would you approval this transaction?')) return false;";
            (Helper.FindControlRecursive(Page, "btnOk") as Button).Text = "Approve";
        }

        private DataTable TransPrescriptionItems
        {
            get
            {
                var query = new TransPrescriptionItemQuery("a");
                var item1 = new ItemQuery("b");
                var item2 = new ItemQuery("c");
                var hd = new TransPrescriptionQuery("d");
                var et = new TransPrescriptionItemEtiquetteQuery("et");

                query.Select
                    (
                        hd.ApprovalDateTime,
                        hd.RegistrationNo,
                        query,
                        query.LineAmount.As("Total"),
                        "<CASE WHEN ISNULL(c.ItemName, '') = '' THEN b.ItemName ELSE c.ItemName END AS ItemName>",
                        "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                        "<ISNULL(et.BatchNumber, '') + CASE WHEN et.ExpiredDate IS NULL THEN '' ELSE  ' [ED: ' + CONVERT(VARCHAR, et.ExpiredDate, 103) + ']' END AS BatchNo>",
                        "<CAST(0 AS DECIMAL) AS ReturnedQty>"
                    );
                query.InnerJoin(hd).On(query.PrescriptionNo == hd.PrescriptionNo);
                query.InnerJoin(item1).On(query.ItemID == item1.ItemID);
                query.LeftJoin(item2).On(query.ItemInterventionID == item2.ItemID);
                query.LeftJoin(et).On(et.PrescriptionNo == query.PrescriptionNo && et.SequenceNo == query.SequenceNo);
                query.Where
                    (
                        hd.IsPrescriptionReturn == false,
                        query.IsCompound == false,
                        query.IsApprove == true,
                        query.IsVoid == false
                    );
                if (AppSession.Parameter.IsAllowPrescriptionReturnForMultipleRegistration)
                    query.Where(hd.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"])));
                else
                    query.Where(hd.RegistrationNo == Request.QueryString["regno"]);

                query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

                DataTable tbl1 = query.LoadDataTable();

                if (tbl1.Rows.Count == 0) return tbl1;

                // update pending delivery
                //foreach(DataRow row in tbl1.Rows){
                //    if ((row["IsPendingDelivery"]) is DBNull ? false : (bool)(row["IsPendingDelivery"]))
                //    {
                //        row["ResultQty"] = (row["DeliveryQty"] is DBNull) ? 0 : ((decimal)row["DeliveryQty"]);
                //    }
                //}

                var header = new TransPrescriptionQuery("c");
                query = new TransPrescriptionItemQuery("a");

                query.Select(query, header.ReferenceNo.As("ReferenceNoHeader"));
                query.InnerJoin(header).On(query.PrescriptionNo == header.PrescriptionNo);
                query.Where
                    (
                        header.IsPrescriptionReturn == true,
                        header.IsApproval == true,
                        header.ReferenceNo.In(tbl1.AsEnumerable().Select(t => t.Field<string>("PrescriptionNo")).Distinct().ToList()),
                        query.IsApprove == true,
                        query.IsVoid == false
                    );

                DataTable tbl2 = query.LoadDataTable();

                foreach (DataRow row2 in tbl2.Rows)
                {
                    foreach (DataRow row1 in tbl1.Rows.Cast<DataRow>().Where(row1 => row1["PrescriptionNo"].Equals(row2["ReferenceNoHeader"]) && row1["SequenceNo"].Equals(row2["SequenceNo"]) && row1["ItemID"].Equals(row2["ItemID"])))
                    {
                        row1["PrescriptionQty"] = (decimal)row1["PrescriptionQty"] + (decimal)row2["PrescriptionQty"];
                        row1["TakenQty"] = (decimal)row1["TakenQty"] + (decimal)row2["TakenQty"];
                        row1["ResultQty"] = (decimal)row1["ResultQty"] + (decimal)row2["ResultQty"];
                        row1["ReturnedQty"] = (decimal)row1["ReturnedQty"] - (decimal)row2["ResultQty"];
                    }
                }

                tbl1.AcceptChanges();

                DataView view = tbl1.DefaultView;
                view.RowFilter = "ResultQty > 0";

                return view.ToTable();
            }
        }

        private string OrderNo {
            get {
                return Request.QueryString["ono"];
            }
        }

        private TransPrescriptionOrderDetailCollection OrderDetails {
            get {
                return (TransPrescriptionOrderDetailCollection)Session["tpod"];
            }
            set {
                Session["tpod"] = value;
            }
        }
        private void LoadOrder() {
            if (!string.IsNullOrEmpty(OrderNo))
            {
                var odColl = new TransPrescriptionOrderDetailCollection();
                var od = new TransPrescriptionOrderDetailQuery("od");
                var oh = new TransPrescriptionOrderQuery("oh");
                od.InnerJoin(oh).On(od.OrderNo == oh.OrderNo)
                    .Where(oh.OrderNo == OrderNo, oh.IsClosed == false);
                odColl.Load(od);
                Session["tpod"] = odColl;
            }
            else
            {
                Session["tpod"] = null;
            }
        }

        protected void grdTransPrescriptionItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            LoadOrder();

            grdTransPrescriptionItem.DataSource = TransPrescriptionItems;
        }

        protected void grdTransPrescriptionItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var dataItem = e.Item as GridDataItem;
                var chk = (CheckBox)dataItem.FindControl("chkIsUsingAdmin");
                if (chk != null)
                {
                    chk.Checked = (AppSession.Parameter.IsPrescriptionReturnAdminChecked);
                }
                if (!string.IsNullOrEmpty(OrderNo))
                {
                    var dr = ((DataRowView)e.Item.DataItem).Row;
                    var xx = OrderDetails.Where(d => d.PrescriptionNo == dr.Field<string>("PrescriptionNo") &&
                    d.SequenceNo == dr.Field<string>("SequenceNo")).FirstOrDefault();
                    if (xx != null) {
                        var chkSel = (CheckBox)dataItem.FindControl("detailChkbox");
                        if (chkSel != null) chkSel.Checked = true;
                        var txtQty = (RadNumericTextBox)dataItem.FindControl("txtQty");
                        if (txtQty != null) txtQty.Value = Convert.ToDouble(xx.Qty);
                    }
                }
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdTransPrescriptionItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var reg = new Registration();
            var mb = new MergeBilling();
            if (mb.LoadByPrimaryKey(Request.QueryString["regno"]) && !string.IsNullOrEmpty(mb.FromRegistrationNo))
            {
                reg.LoadByPrimaryKey(mb.FromRegistrationNo);
            }
            else
                reg.LoadByPrimaryKey(Request.QueryString["regno"]);

            if (reg.IsHoldTransactionEntry ?? false)
            {
                this.ShowInformationHeader("Registration locked.");
                return false;
            }
            if (reg.IsClosed ?? false)
            {
                this.ShowInformationHeader("Registration closed.");
                return false;
            }

            var dtAva = TransPrescriptionItems;

            // validate
            var iCount = 0;
            foreach (GridDataItem item in grdTransPrescriptionItem.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
            {
                var qty = Convert.ToDecimal((item.FindControl("txtQty") as RadNumericTextBox).Value);
                if (qty < 0)
                {
                    //s show error, entry minus is not allowed
                    this.ShowInformationHeader("Error: entry minus is not allowed!!");
                    return false;
                }
                var qtyAva = dtAva.AsEnumerable().Where(x => x["PrescriptionNo"].ToString() == item["PrescriptionNo"].Text && x["SequenceNo"].ToString() == item["SequenceNo"].Text)
                    .Sum(x => (decimal)x["ResultQty"]);
                if (qtyAva < qty) {
                    this.ShowInformationHeader("Error: available qty is less than return qty for item " + ((Label)item.FindControl("lblItemName")).Text + " (Remaining available qty : " + String.Format("{0:n2}", Math.Abs(qtyAva)) + ")");
                    return false;
                }

                // validasi sisa berapa yang boleh diretur
                iCount++;
            }
            if (iCount == 0)
            {
                this.ShowInformationHeader("Error: Nothing to be processed, please click cancel instead!");
                return false;
            }

            if (pnlDispensary.Visible)
            {
                if (string.IsNullOrEmpty(cboDispensaryID.SelectedValue))
                {
                    this.ShowInformationHeader("Dispensary required.");
                    return false;
                }

                if (string.IsNullOrEmpty(cboLocationID.SelectedValue))
                {
                    this.ShowInformationHeader("Location required.");
                    return false;
                }
            }

            using (var trans = new esTransactionScope())
            {
                var details = new TransPrescriptionItemCollection();
                var costs = new CostCalculationCollection();
                var headers = new TransPrescriptionCollection();

                var order = new TransPrescriptionOrder();
                if (!string.IsNullOrEmpty(OrderNo)) {
                    order.LoadByPrimaryKey(OrderNo);
                    order.IsClosed = true;
                    order.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                }

                foreach (GridDataItem item in grdTransPrescriptionItem.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked))
                {
                    var qty = Convert.ToDecimal(0 - (item.FindControl("txtQty") as RadNumericTextBox).Value);
                    if (qty == 0) continue;

                    //detail
                    var detail = new TransPrescriptionItem();
                    detail.LoadByPrimaryKey(item["PrescriptionNo"].Text, item["SequenceNo"].Text);

                    detail.IsReturned = true;
                    detail.Save();

                    detail.MarkAllColumnsAsDirty(DataRowState.Added);

                    var isUsingAdmin = ((CheckBox)item.FindControl("chkIsUsingAdmin")).Checked;
                    var oriResultQty = detail.ResultQty;

                    //detail
                    detail.PrescriptionQty = qty;
                    detail.TakenQty = qty;

                    if (AppSession.Parameter.PrescriptionReturnRecipeAmountReturned == "No" && !AppSession.Parameter.IsPrescriptionDiscountIncludeR)
                        detail.RecipeAmount = 0;
                    else
                    {
                        if (AppSession.Parameter.IsPrescriptionDiscountIncludeR)
                        {
                            detail.RecipeAmount = (Math.Abs(qty) / oriResultQty) * detail.RecipeAmount;
                        }
                        else
                        {
                            if (Math.Abs(qty) < detail.ResultQty)
                            {
                                var returnedQty = Convert.ToDecimal((item.FindControl("txtReturnedQty") as RadNumericTextBox).Value);
                                if (Math.Abs(qty) + returnedQty < detail.ResultQty)
                                    detail.RecipeAmount = 0;
                            }
                        }
                    }

                    detail.DiscountAmount = (Math.Abs(qty) / oriResultQty) * detail.DiscountAmount;

                    if (isUsingAdmin)
                    {
                        detail.DiscountAmount = detail.DiscountAmount - (detail.DiscountAmount * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);
                        detail.Price = detail.Price - (((AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100) * detail.Price);
                        detail.IsUsingAdminReturn = (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) > 0;
                    }

                    detail.ResultQty = qty;
                    
                    //var lineAmt = (((Math.Abs(detail.ResultQty ?? 0) * detail.Price) - detail.DiscountAmount) + detail.RecipeAmount);
                    //detail.LineAmount = 0 - (Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription));

                    var lineAmt = ((Math.Abs(detail.ResultQty ?? 0) * detail.Price) + detail.RecipeAmount);
                    if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                    {
                        if (AppSession.Parameter.IsPrescriptionDiscountIncludeR)
                        {
                            lineAmt = (Math.Abs(qty) / oriResultQty) * detail.LineAmount;
                            if (isUsingAdmin)
                                lineAmt = lineAmt - (lineAmt * (AppSession.Parameter.PrescriptionReturnAdminValue ?? 0) / 100);
                        }
                        else
                            lineAmt = Helper.Rounding((lineAmt ?? 0), AppEnum.RoundingType.Prescription) - detail.DiscountAmount;
                    }
                    else
                        lineAmt = Helper.Rounding((lineAmt ?? 0) - (detail.DiscountAmount ?? 0), AppEnum.RoundingType.Prescription);

                    detail.LineAmount = 0 - lineAmt;

                    detail.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    detail.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    detail.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                    detail.CreatedByUserID = AppSession.UserLogin.UserID;

                    details.AttachEntity(detail);

                    //cost
                    var cost = new CostCalculation();
                    cost.Query.Where(
                        cost.Query.RegistrationNo == item["RegistrationNo"].Text,
                        cost.Query.TransactionNo == item["PrescriptionNo"].Text,
                        cost.Query.SequenceNo == item["SequenceNo"].Text
                        );
                    cost.Query.Load();

                    decimal patAmtCost = cost.PatientAmount ?? 0;
                    decimal guarAmtCost = cost.GuarantorAmount ?? 0;

                    cost.MarkAllColumnsAsDirty(DataRowState.Added);

                    var presc = new TransPrescription();
                    presc.LoadByPrimaryKey(item["PrescriptionNo"].Text);

                    var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, reg.GuarantorID, string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID, presc.PrescriptionDate.Value.Date, true);
                    var calc = new Helper.CostCalculation(reg.GuarantorID, reg.IsGlobalPlafond ?? false,
                                   string.IsNullOrEmpty(detail.ItemInterventionID) ? detail.ItemID : detail.ItemInterventionID, Math.Abs(detail.LineAmount ?? 0),
                                   tblCovered, Math.Abs(detail.ResultQty ?? 0), detail.Price ?? 0, detail.RecipeAmount ?? 0, detail.DiscountAmount ?? 0);


                    if (patAmtCost != 0 && guarAmtCost != 0)
                    {
                        cost.PatientAmount = 0 - calc.PatientAmount;
                        cost.GuarantorAmount = 0 - calc.GuarantorAmount;
                    }
                    else
                    {
                        if (patAmtCost != 0 && guarAmtCost == 0)
                        {
                            cost.PatientAmount = 0 - (calc.PatientAmount + calc.GuarantorAmount);
                            cost.GuarantorAmount = 0;
                        }
                        else if (patAmtCost == 0 && guarAmtCost != 0)
                        {
                            cost.PatientAmount = 0;
                            cost.GuarantorAmount = 0 - (calc.PatientAmount + calc.GuarantorAmount);
                        }
                        else
                        {
                            cost.PatientAmount = 0 - calc.PatientAmount;
                            cost.GuarantorAmount = 0 - calc.GuarantorAmount;
                        }
                    }
                    cost.DiscountAmount = 0 - detail.DiscountAmount;// calc.DiscountAmount;
                    cost.IntermBillNo = null;
                    cost.IsChecked = null;
                    cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    cost.LastUpdateByUserID = AppSession.UserLogin.UserID;

                    costs.AttachEntity(cost);

                    //return false;
                }

                /* cek kalo ada detil baru create header */
                if (details.Count > 0)
                {
                    //header
                    var groups = from d in details
                                 group d by d.PrescriptionNo into g
                                 select g.Key;
                    foreach (var group in groups)
                    {
                        var hd = new TransPrescription();
                        hd.LoadByPrimaryKey(group);

                        AppAutoNumberLast autoNumber;
                        if (AppSession.Parameter.IsPrescriptionReturnNoFormatBasedOnRegType)
                            autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date,
                                                                 reg.SRRegistrationType == AppConstant.RegistrationType.InPatient
                                                                     ? AppEnum.AutoNumber.PrescRetIpNo
                                                                     : AppEnum.AutoNumber.PrescRetOpNo);
                        else
                            autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PrescriptionNo);

                        var header = headers.AddNew();
                        header.PrescriptionNo = autoNumber.LastCompleteNumber;
                        header.PrescriptionDate = (new DateTime()).NowAtSqlServer().Date;
                        header.RegistrationNo = hd.RegistrationNo;
                        if (pnlDispensary.Visible)
                        {
                            header.ServiceUnitID = cboDispensaryID.SelectedValue;
                            header.LocationID = cboLocationID.SelectedValue;
                        }
                        else
                        {
                            header.ServiceUnitID = hd.ServiceUnitID;
                            header.LocationID = hd.LocationID;
                        }

                        var loc = new Location();
                        if (loc.LoadByPrimaryKey(header.LocationID) && loc.IsHoldForTransaction == true)
                        {
                            this.ShowInformationHeader("Location: " + loc.LocationName + " in Hold For Transaction status. Transaction is not allowed.");
                            return false;
                        }

                        //header.ClassID = hd.ClassID;
                        header.ClassID = reg.ChargeClassID;
                        header.ParamedicID = hd.ParamedicID;
                        header.IsPrescriptionReturn = true;
                        header.ReferenceNo = hd.PrescriptionNo;
                        header.IsFromSOAP = hd.IsFromSOAP;
                        header.IsApproval = true;
                        header.IsBillProceed = true;
                        header.ApprovalDateTime = (new DateTime()).NowAtSqlServer();
                        header.ApprovedByUserID = AppSession.UserLogin.UserID;
                        header.IsVoid = false;
                        header.OrderNo = string.Empty;
                        header.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        header.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        header.CreatedDateTime = (new DateTime()).NowAtSqlServer();
                        header.CreatedByUserID = AppSession.UserLogin.UserID;

                        header.FromServiceUnitID = reg.ServiceUnitID;
                        header.FromRoomID = reg.RoomID;
                        header.FromBedID = reg.BedID;

                        //auto number
                        autoNumber.Save();

                        //detail
                        foreach (var detail in details.Where(d => d.PrescriptionNo == hd.PrescriptionNo))
                        {
                            detail.PrescriptionNo = header.PrescriptionNo;
                            detail.ReferenceNo = order.OrderNo;
                        }

                        //cost
                        foreach (var cost in costs.Where(d => d.TransactionNo == hd.PrescriptionNo))
                        {
                            cost.TransactionNo = header.PrescriptionNo;
                        }
                    }

                    //save
                    if (!string.IsNullOrEmpty(OrderNo)) order.Save();
                    headers.Save();
                    details.Save();
                    costs.Save();

                    //movement
                    var units = new ServiceUnitCollection();
                    units.Query.Where(units.Query.ServiceUnitID.In((headers.Select(h => h.ServiceUnitID)).Distinct()));
                    units.Query.Load();

                    foreach (var header in headers)
                    {
                        var chargesBalances = new ItemBalanceCollection();
                        var chargesDetailBalances = new ItemBalanceDetailCollection();
                        var chargesDetailBalanceEds = new ItemBalanceDetailEdCollection();
                        var chargesMovements = new ItemMovementCollection();

                        ItemBalance.PrepareItemBalancesForReturn(header, details.Where(d => d.PrescriptionNo == header.PrescriptionNo),
                            BusinessObject.Reference.TransactionCode.PrescriptionReturn, header.ServiceUnitID, header.LocationID,
                            AppSession.UserLogin.UserID, true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, ref chargesDetailBalanceEds, 
                            AppSession.Parameter.IsEnabledStockWithEdControl);

                        if (chargesBalances != null) chargesBalances.Save();
                        if (chargesDetailBalances != null) chargesDetailBalances.Save();
                        if (chargesDetailBalanceEds != null) chargesDetailBalanceEds.Save();
                        if (chargesMovements != null) chargesMovements.Save();

                        //journal
                        /* Automatic Journal Testing Start */
                        if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsJournalCashBased) == "No")
                        {
                            if (AppParameter.IsYes(AppParameter.ParameterItem.acc_IsJournalAccualNoTemporary))
                            {
                                JournalTransactions.AddNewPatientIncomeAccrual(BusinessObject.JournalType.Income, header.PrescriptionNo, AppSession.UserLogin.UserID, 0);
                            }
                            else {
                                var type = AppParameter.GetParameterValue(AppParameter.ParameterItem.RegistrationTypeForAccrualJournal).Split(',');
                                if (type.Contains(reg.SRRegistrationType))
                                {
                                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(header.PrescriptionDate.Value.Date);
                                    if (isClosingPeriod)
                                    {
                                        this.ShowInformationHeader("Financial statements for period: " +
                                                           string.Format("{0:MMMM-yyyy}", header.PrescriptionDate.Value.Date) +
                                                           " have been closed. Please contact the authorities.");
                                        return false;
                                    }

                                    int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalTemporaryNetto(header, reg, units.SingleOrDefault(u => u.ServiceUnitID == header.ServiceUnitID),
                                        costs.Where(c => c.TransactionNo == header.PrescriptionNo), "RS", AppSession.UserLogin.UserID, 0);
                                }
                            }
                        }
                        //else if (AppSession.Parameter.IsUsingIntermBill != "Yes")
                        //{
                        //    var isClosingPeriod = PostingStatus.IsPeriodeClosed(header.PrescriptionDate.Value.Date);
                        //    if (isClosingPeriod)
                        //    {
                        //        this.ShowInformationHeader("Financial statements for period: " +
                        //                           string.Format("{0:MMMM-yyyy}", header.PrescriptionDate.Value.Date) +
                        //                           " have been closed. Please contact the authorities.");
                        //        return false;
                        //    }

                        //    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsPemisahanCOAUangRacikan) == "1")
                        //    {
                        //        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournalWithSeparationPersonalizedRecipeMoney(header, reg,
                        //            units.SingleOrDefault(u => u.ServiceUnitID == header.ServiceUnitID),
                        //            costs.Where(c => c.TransactionNo == header.PrescriptionNo), "RS", AppSession.UserLogin.UserID, 0);
                        //    }
                        //    else
                        //    {
                        //        int? journalId = JournalTransactions.AddNewPrescriptionReturnJournal(header, reg,
                        //            units.SingleOrDefault(u => u.ServiceUnitID == header.ServiceUnitID),
                        //            costs.Where(c => c.TransactionNo == header.PrescriptionNo), "RS", AppSession.UserLogin.UserID, 0);
                        //    }
                        //}

                        /* Automatic Journal Testing End */
                    }
                }

                trans.Complete();
            }

            return true;
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        //private DataTable PopulateItem(string parameter)
        //{
        //    var query = new ItemQuery("a");
        //    var tpiq = new TransPrescriptionItemQuery("b");
        //    var tpq = new TransPrescriptionQuery("c");
        //    var py = new TransPaymentItemOrderQuery("d");

        //    query.InnerJoin(tpiq).On(query.ItemID == tpiq.ItemID);
        //    query.InnerJoin(tpq).On(tpiq.PrescriptionNo == tpq.PrescriptionNo);
        //    query.LeftJoin(py).On(
        //        tpiq.PrescriptionNo == py.TransactionNo &&
        //        tpiq.SequenceNo == py.SequenceNo &&
        //        py.IsPaymentProceed == true &&
        //        py.IsPaymentReturned == false
        //        );

        //    query.es.Top = 30;
        //    query.Select
        //        (
        //            query.ItemID,
        //            query.ItemName
        //        );

        //    query.Where
        //        (
        //            query.Or
        //                (
        //                    query.ItemName.Like(string.Format("{0}%", parameter)),
        //                    query.ItemID.Like(string.Format("{0}%", parameter))
        //                ),
        //            tpq.RegistrationNo.In(Helper.MergeBilling.GetMergeRegistration(Request.QueryString["regno"])),
        //            tpq.IsApproval == true,
        //            tpq.IsPrescriptionReturn == false,
        //            py.PaymentNo.IsNull()
        //        );
        //    query.OrderBy(query.ItemName.Ascending);

        //    var tbl = query.LoadDataTable();
        //    var item2 = string.Empty;

        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        var item1 = (string)row["ItemID"];
        //        if (item1 != item2)
        //            item2 = (string)row["ItemID"];
        //        else
        //            row.Delete();
        //    }

        //    tbl.AcceptChanges();

        //    return tbl;
        //}

        protected void cboDispensaryID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ComboBox.PopulateWithServiceUnitForLocation(cboLocationID, e.Value);
            cboLocationID.SelectedIndex = 1;
        }
    }
}
