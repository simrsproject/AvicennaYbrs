using System;
using System.Linq;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Charges.FinalizeBilling
{
    public partial class TariffComponent : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            if (!IsPostBack)
            {
                if (Request.QueryString["type"] == "1")
                {
                    PopulateParamedicByServiceUnit();

                    cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    var tbl = (DataTable)ViewState["paramedic" + Request.UserHostName];
                    foreach (DataRow row in tbl.Rows)
                    {
                        cboParamedicID.Items.Add(new RadComboBoxItem((string)row[ParamedicMetadata.ColumnNames.ParamedicName], (string)row[ParamedicMetadata.ColumnNames.ParamedicID]));
                    }
                }

                StandardReference.InitializeIncludeSpace(cboSRDiscountReason, AppEnum.StandardReference.DiscountReason);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TYPE"] == "1")
                {
                    var hd = new TransCharges();
                    hd.LoadByPrimaryKey(Request.QueryString["transNo"]);

                    var dt = new TransChargesItem();
                    dt.LoadByPrimaryKey(Request.QueryString["transNo"], Request.QueryString["seqNo"]);

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(Request.QueryString["grrID"]);

                    var item = new Item();
                    item.LoadByPrimaryKey(dt.ItemID);

                    var coll = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, grr.SRTariffType, hd.ClassID, dt.ItemID);
                    if (!coll.Any())
                        coll = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, grr.SRTariffType,
                            AppSession.Parameter.DefaultTariffClass, dt.ItemID);
                    if (!coll.Any())
                        coll = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType,
                            hd.ClassID, dt.ItemID);
                    if (!coll.Any())
                        coll = Helper.Tariff.GetItemTariffComponentCollection(DateTime.Now.Date, AppSession.Parameter.DefaultTariffType,
                            AppSession.Parameter.DefaultTariffClass, dt.ItemID);
                    if (!coll.Any() || item.SRItemType == BusinessObject.Reference.ItemType.Medical || item.SRItemType == BusinessObject.Reference.ItemType.NonMedical || item.SRItemType == BusinessObject.Reference.ItemType.Kitchen)
                        pnlVariableAndDiscount.Visible = true;
                    else
                    {
                        grdTariff.MasterTableView.Columns[2].Visible = AppSession.Parameter.IsTariffComponentPriceVisibleForBilling;
                        grdTariff.MasterTableView.Columns[3].Visible = AppSession.Parameter.IsTariffComponentPriceVisibleForBilling;
                        grdTariff.MasterTableView.Columns[4].Visible = AppSession.Parameter.IsTariffComponentPriceVisibleForBilling;

                        pnlComponentTariff.Visible = true;
                    }
                    pnlParamedicID.Visible = true;
                }
                else
                    pnlVariableAndDiscount.Visible = true;

                LoadTransaction();
            }
        }

        private void LoadTransaction()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(Request.QueryString["grrID"]);

            double disc = 0, price = 0, recipeAmt = 0, qty = 1;
            var reason = string.Empty;
            var notes = string.Empty;

            switch (Request.QueryString["type"])
            {
                case "1":
                    var dt1 = new TransChargesItem();
                    dt1.LoadByPrimaryKey(Request.QueryString["transNo"], Request.QueryString["seqNo"]);
                    cboParamedicID.SelectedValue = dt1.ParamedicID;
                    price = (double)dt1.Price;
                    //disc = (double)dt1.DiscountAmount;
                    reason = ""; //dt1.SRDiscountReason;
                    notes = dt1.Notes;
                    break;
                case "2":
                    var dt2 = new TransPrescriptionItem();
                    dt2.LoadByPrimaryKey(Request.QueryString["transNo"], Request.QueryString["seqNo"]);
                    price = (double)dt2.Price;
                    //disc = (double)dt2.DiscountAmount;
                    recipeAmt = (double)dt2.RecipeAmount;
                    qty = Math.Abs((double)dt2.ResultQty);
                    reason = ""; //dt2.SRDiscountReason;
                    notes = dt2.Notes;
                    break;
            }

            if (pnlVariableAndDiscount.Visible)
            {
                txtPrice.Value = price;
                txtDiscountAmount1.Value = disc;

                var IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);
                txtDiscountAmount1.MaxValue = IsIncR ? price + (recipeAmt / (qty == 0 ? 1 : qty)) : price;
            }

            cboSRDiscountReason.SelectedValue = reason;
            txtNotes.Text = notes;
        }

        protected void grdTariff_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdTariff_ItemPreRender;
        }

        private void grdTariff_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var price = (dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox);
            price.ReadOnly = !(dataItem["IsAllowVariable"].Text == "True");

            var discountpercent = (dataItem["TariffComponentName"].FindControl("txtDiscountPercent") as RadNumericTextBox);
            discountpercent.Visible = (dataItem["IsAllowDiscount"].Text == "True");
            var discount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox);
            discount.Visible = (dataItem["IsAllowDiscount"].Text == "True") && (!AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare);
            if (discount.Visible)
            {
                discount.Value = double.Parse(dataItem["DiscountAmount"].Text);
                /*DiscountAmount itu sudah mix sama discount FeeDiscount, jadi harus dipecah dulu*/
                discount.Value -= double.Parse(dataItem["FeeDiscount"].Text);
            }

            var validDiscount = (dataItem["TariffComponentName"].FindControl("rfvDiscount") as RequiredFieldValidator);
            validDiscount.Visible = discount.Visible;

            var paramedic = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox);
            paramedic.Visible = (dataItem["IsTariffParamedic"].Text == "True");

            var validParamedic = (dataItem["TariffComponentName"].FindControl("rfvPhysicianID") as RequiredFieldValidator);
            validParamedic.Visible = paramedic.Visible;

            if (paramedic.Visible)
            {
                paramedic.Items.Clear();
                paramedic.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));

                if (ViewState["paramedic" + Request.UserHostName] == null)
                    PopulateParamedicByServiceUnit();

                var table = ((DataTable)ViewState["paramedic" + Request.UserHostName]);
                foreach (DataRow row in table.Rows)
                {
                    paramedic.Items.Add(new RadComboBoxItem((string)row["ParamedicName"], (string)row["ParamedicID"]));
                }

                paramedic.SelectedValue = dataItem["ParamedicID"].Text;
            }

            var percentFeeDiscount = (dataItem["TariffComponentName"].FindControl("txtPercentFeeDiscount") as RadNumericTextBox);
            percentFeeDiscount.Visible = (dataItem["IsAllowDiscount"].Text == "True")
                && (AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare);
            if (percentFeeDiscount.Visible)
                percentFeeDiscount.Value = double.Parse(dataItem["FeeDiscountPercentage"].Text);
        }

        private DataTable TariffComponents
        {
            get
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(Request.QueryString["grrID"]);

                var query = new TransChargesItemCompQuery("a");
                var detail = new TransChargesItemQuery("b");

                query.Select(
                    query.TariffComponentID,
                    query.DiscountAmount,
                    query.ParamedicID,
                    query.Price,
                    "<ISNULL(a.CitoAmount, 0) CitoAmount>",
                    "<ISNULL(a.FeeDiscountPercentage, 0) FeeDiscountPercentage>",
                    "<ISNULL(a.FeeDiscount, 0) FeeDiscount>",
                    detail.ItemID,
                    detail.IsCito,
                    "<ISNULL(b.BasicCitoAmount, 0) BasicCitoAmount>"
                    );
                query.InnerJoin(detail).On(query.TransactionNo == detail.TransactionNo && query.SequenceNo == detail.SequenceNo);
                query.Where(query.TransactionNo == Request.QueryString["transNo"] && query.SequenceNo == Request.QueryString["seqNo"]);
                query.OrderBy(query.TariffComponentID.Ascending);

                var compDtColl = query.LoadDataTable();
                //compDtColl.Query.Where(
                //    compDtColl.Query.TransactionNo == Request.QueryString["transNo"],
                //    compDtColl.Query.SequenceNo == Request.QueryString["seqNo"]
                //    );
                //compDtColl.Query.OrderBy(compDtColl.Query.TariffComponentID.Ascending);
                //compDtColl.Load(query);

                var tc = new TransCharges();
                tc.LoadByPrimaryKey(Request.QueryString["transNo"]);

                var dtb = new DataTable();
                InitTariffComponentTable(out dtb);

                bool isAllowDiscount = false;
                foreach (DataRow entity in compDtColl.Rows)
                {
                    var newRow = dtb.NewRow();
                    newRow["TariffComponentID"] = entity["TariffComponentID"];

                    var comp = new BusinessObject.TariffComponent();
                    comp.LoadByPrimaryKey(entity["TariffComponentID"].ToString());
                    newRow["TariffComponentName"] = comp.TariffComponentName;

                    //newRow["IsAllowDiscount"] = true;
                    newRow["IsTariffParamedic"] = comp.IsTariffParamedic;
                    newRow["DiscountAmount"] = entity["DiscountAmount"];
                    newRow["ParamedicID"] = entity["ParamedicID"];
                    newRow["Price"] = entity["Price"];
                    newRow["CitoAmount"] = entity["CitoAmount"];
                    newRow["FeeDiscountPercentage"] = entity["FeeDiscountPercentage"];
                    newRow["FeeDiscount"] = entity["FeeDiscount"];

                    var tariff = ((Helper.Tariff.GetItemTariff(tc.TransactionDate.Value, grr.SRTariffType, tc.ClassID, tc.ClassID, entity["ItemID"].ToString(), grr.GuarantorID, false, reg.SRRegistrationType) ??
                                   Helper.Tariff.GetItemTariff(tc.TransactionDate.Value, grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, tc.ClassID, entity["ItemID"].ToString(), grr.GuarantorID, false, reg.SRRegistrationType)) ??
                                  Helper.Tariff.GetItemTariff(tc.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, tc.ClassID, tc.ClassID, entity["ItemID"].ToString(), grr.GuarantorID, false, reg.SRRegistrationType)) ??
                                 Helper.Tariff.GetItemTariff(tc.TransactionDate.Value, AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, tc.ClassID, entity["ItemID"].ToString(), grr.GuarantorID, false, reg.SRRegistrationType);
                    if (tariff != null)
                    {
                        newRow["IsAllowDiscount"] = tariff.IsAllowDiscount ?? false;
                        newRow["IsAllowVariable"] = tariff.IsAllowVariable ?? false;
                    }
                    else {
                        newRow["IsAllowDiscount"] = false;
                        newRow["IsAllowVariable"] = false;
                    }

                    newRow["IsCito"] = entity["IsCito"];
                    newRow["BasicCitoAmount"] = entity["BasicCitoAmount"];

                    if (Convert.ToBoolean(newRow["IsAllowDiscount"]))
                        isAllowDiscount = true;

                    dtb.Rows.Add(newRow);
                }
                trSRDiscountReason.Visible = isAllowDiscount;
                trNotes.Visible = isAllowDiscount;
                return dtb;
            }
        }

        private void InitTariffComponentTable(out DataTable dataTable)
        {
            var tempTable = new DataTable();

            var column = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "TariffComponentID" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "TariffComponentName" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsAllowDiscount" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsTariffParamedic" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "DiscountAmount" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.String"), ColumnName = "ParamedicID" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "Price" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "CitoAmount" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsAllowVariable" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "FeeDiscountPercentage" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "FeeDiscount" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Boolean"), ColumnName = "IsCito" };
            tempTable.Columns.Add(column);

            column = new DataColumn { DataType = Type.GetType("System.Decimal"), ColumnName = "BasicCitoAmount" };
            tempTable.Columns.Add(column);

            dataTable = tempTable;
        }

        protected void grdTariff_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            PopulateParamedicByServiceUnit();

            grdTariff.DataSource = TariffComponents;
        }

        private void PopulateParamedicByServiceUnit()
        {
            if (ViewState["paramedic" + Request.UserHostName] != null)
                return;

            var reg = new Registration();
            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                var medic = new ParamedicQuery("b");

                medic.Select(
                        medic.ParamedicID,
                        medic.ParamedicName
                    );
                medic.Where(medic.IsActive == true);

                ViewState["paramedic" + Request.UserHostName] = medic.LoadDataTable();
            }
            else
            {
                var hd = new TransCharges();
                hd.LoadByPrimaryKey(Request.QueryString["transNo"]);

                var query = new ServiceUnitParamedicQuery("a");
                var medic = new ParamedicQuery("b");

                query.Select(
                        query.ParamedicID,
                        medic.ParamedicName
                    );
                query.InnerJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.Where(
                        query.ServiceUnitID == hd.ToServiceUnitID,
                        medic.IsActive == true
                    );

                ViewState["paramedic" + Request.UserHostName] = query.LoadDataTable();
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            decimal maxDiscTariffDr = AppSession.Parameter.MaxDiscTxInPercentage;
            decimal maxDiscTariffRs = AppSession.Parameter.MaxDiscTxTariffRsInPercentage;
            decimal maxDiscTariffDrAmt = 0;
            string msg = string.Empty;
            bool isNotValid = false;

            var transNo = Request.QueryString["transNo"];
            var sequenceNo = Request.QueryString["seqNo"];

            var transHD = new TransCharges();
            transHD.LoadByPrimaryKey(transNo);

            var costIb = new CostCalculationCollection();
            costIb.Query.Where(costIb.Query.TransactionNo == transNo, costIb.Query.SequenceNo == sequenceNo,
                               costIb.Query.IntermBillNo.IsNotNull());
            costIb.LoadAll();
            if (costIb.Count > 0)
            {
                isNotValid = true;
                msg = "There is already Intermbill process for selected transaction.";
            }
            else if (pnlComponentTariff.Visible)
            {
                foreach (GridDataItem dataItem in grdTariff.MasterTableView.Items)
                {
                    //decimal maxDisc = (dataItem["TariffComponentName"].FindControl("chkIsTariffParamedic") as CheckBox).Checked
                    //              ? maxDiscTariffDr
                    //              : maxDiscTariffRs;
                    decimal maxDisc;
                    decimal price = Convert.ToDecimal((dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value);

                    //------start------------
                    if ((dataItem["TariffComponentName"].FindControl("chkIsTariffParamedic") as CheckBox).Checked)
                    {
                        if (maxDiscTariffDr == 0)
                        {
                            var parId =
                                (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox).SelectedValue;
                            var reg = new Registration();
                            reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                            var parRefId = string.Empty;
                            if (reg.ReferralID != null)
                            {
                                var referral = new Referral();
                                if (referral.LoadByPrimaryKey(reg.ReferralID))
                                    parRefId = referral.ParamedicID;
                            }

                            //var transNo = Request.QueryString["transNo"];
                            //var sequenceNo = Request.QueryString["seqNo"];
                            var itemId = string.Empty;
                            var tx = new TransChargesItem();
                            if (tx.LoadByPrimaryKey(transNo, sequenceNo))
                                itemId = tx.ItemID;

                            var medic = new Paramedic();
                            if (medic.LoadByPrimaryKey(parId))
                            {
                                var mtxItemGuarr = new ParamedicFeeItemGuarantor();
                                if (mtxItemGuarr.LoadByPrimaryKey(parId, itemId, reg.GuarantorID))
                                {
                                    if (parId == parRefId)
                                        maxDisc = mtxItemGuarr.ParamedicFeeAmountReferral ?? 0;
                                    else
                                        maxDisc = mtxItemGuarr.ParamedicFeeAmount ?? 0;

                                    maxDiscTariffDrAmt = mtxItemGuarr.IsParamedicFeeUsePercentage ?? false
                                                             ? maxDiscTariffDrAmt
                                                             : maxDisc;
                                }
                                else
                                {
                                    var matrix = new ParamedicFeeItem();
                                    if (matrix.LoadByPrimaryKey(parId, itemId))
                                    {
                                        if (parId == parRefId)
                                            maxDisc = matrix.ParamedicFeeAmountReferral ?? 0;
                                        else
                                            maxDisc = matrix.ParamedicFeeAmount ?? 0;

                                        maxDiscTariffDrAmt = matrix.IsParamedicFeeUsePercentage ?? false
                                                                 ? maxDiscTariffDrAmt
                                                                 : maxDisc;
                                    }
                                    else
                                    {
                                        if (parId == parRefId)
                                            maxDisc = medic.ParamedicFeeAmountReferral ?? 0;
                                        else
                                            maxDisc = medic.ParamedicFeeAmount ?? 0;

                                        maxDiscTariffDrAmt = medic.IsParamedicFeeUsePercentage ?? false
                                                                 ? maxDiscTariffDrAmt
                                                                 : maxDisc;
                                    }
                                }
                            }
                            else
                                maxDisc = 0;
                        }
                        else
                            maxDisc = maxDiscTariffDr;
                    }
                    else
                        maxDisc = maxDiscTariffRs;
                    //------end------------

                    string tariffCompName = dataItem.GetDataKeyValue("TariffComponentName").ToString();

                    if (isNotValid == false)
                    {
                        decimal basicCitoAmt = (decimal)(dataItem["TariffComponentName"].FindControl("txtBasicCitoAmount") as RadNumericTextBox).Value;
                        decimal citoAmt = 0;
                        if (basicCitoAmt > 0)
                            citoAmt = price * basicCitoAmt / 100;

                        if ((dataItem["TariffComponentName"].FindControl("txtDiscountPercent") as RadNumericTextBox).Text != string.Empty)
                        {
                            decimal diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscountPercent") as RadNumericTextBox).Value;

                            if (maxDiscTariffDrAmt == 0)
                            {
                                isNotValid = (diskonpercent > maxDisc);
                                if (isNotValid)
                                    msg = "Discount for Tariff Component: " + tariffCompName + " can't more than " + maxDisc.ToString() + "%.";
                            }
                            else
                            {
                                //decimal price = Convert.ToDecimal(dataItem["Price"].Text);
                                decimal discAmt = (price + citoAmt) * diskonpercent / 100;
                                isNotValid = (discAmt > maxDiscTariffDrAmt);
                                if (isNotValid)
                                    msg = "Discount for Tariff Component: " + tariffCompName + " can't more than " + string.Format("{0:n0}", ((discAmt / (price + citoAmt)) * 100)) + "%.";
                            }
                        }
                        else
                        {
                            if ((dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text != string.Empty)
                            {
                                if (maxDiscTariffDrAmt == 0)
                                {
                                    //decimal price = Convert.ToDecimal(dataItem["Price"].Text);
                                    decimal discAmt = (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;
                                    if (price > 0)
                                    {
                                        isNotValid = (((discAmt / (price + citoAmt)) * 100) > maxDisc);
                                        if (isNotValid)
                                            msg = "Discount for Tariff Component: " + tariffCompName + " can't more than " + string.Format("{0:n0}", ((price + citoAmt) * maxDisc / 100)) + ".";
                                    }
                                }
                                else
                                {
                                    decimal discAmt = (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;
                                    isNotValid = discAmt > maxDiscTariffDrAmt;
                                    if (isNotValid)
                                        msg = "Discount for Tariff Component: " + tariffCompName + " can't more than " + string.Format("{0:n0}", maxDiscTariffDrAmt) + ".";
                                }
                            }
                        }
                    }
                }
            }
            if (!isNotValid)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(Request.QueryString["regNo"]);

                using (var trans = new esTransactionScope())
                {
                    //cost calculation
                    var ccQuery = new CostCalculationQuery();
                    ccQuery.Where(
                        ccQuery.TransactionNo == Request.QueryString["transNo"],
                        ccQuery.SequenceNo == Request.QueryString["seqNo"]
                        );

                    var cc = new CostCalculation();
                    cc.Load(ccQuery);

                    if (AppParameter.GetParameterValue(AppParameter.ParameterItem.acc_IsAutoJournalFinalizeBilling) == "Yes")
                    {
                        //save history
                        #region history
                        //save as history
                        var autoNumber = Helper.GetNewAutoNumber(DateTime.Now.Date, AppEnum.AutoNumber.RecalculationProcess);

                        //process header
                        var recalHistory = new RecalculationProcessHistory
                        {
                            RecalculationProcessNo = autoNumber.LastCompleteNumber,
                            RecalculationProcessDate = DateTime.Now,
                            RegistrationNo = reg.RegistrationNo,
                            FromGuarantorID = reg.GuarantorID,
                            ToGuarantorID = reg.GuarantorID,
                            LastUpdateDateTime = DateTime.Now,
                            LastUpdateByUserID = AppSession.UserLogin.UserID
                        };

                        autoNumber.Save();
                        recalHistory.Save();

                        var costHistory = new CostCalculationHistory
                        {
                            RecalculationProcessNo = recalHistory.RecalculationProcessNo,
                            RegistrationNo = cc.RegistrationNo,
                            TransactionNo = cc.TransactionNo,
                            SequenceNo = cc.SequenceNo,
                            ItemID = cc.ItemID,
                            PatientAmount = cc.PatientAmount,
                            GuarantorAmount = cc.GuarantorAmount,
                            DiscountAmount = cc.DiscountAmount,
                            ParamedicAmount = cc.ParamedicAmount,
                            LastUpdateDateTime = cc.LastUpdateDateTime,
                            LastUpdateByUserID = cc.LastUpdateByUserID,
                            ParamedicFeeAmount = cc.ParamedicFeeAmount,
                            ParamedicFeePaymentNo = cc.ParamedicFeePaymentNo,
                            IsPackage = cc.IsPackage,
                            ParentNo = cc.ParentNo
                        };
                        costHistory.Save();

                        //transcharges
                        var ch = new TransCharges();
                        if (ch.LoadByPrimaryKey(costHistory.TransactionNo))
                        {
                            var tch = new TransChargesHistory
                            {
                                RecalculationProcessNo = recalHistory.RecalculationProcessNo,
                                TransactionNo = ch.TransactionNo,
                                RegistrationNo = ch.RegistrationNo,
                                TransactionDate = ch.TransactionDate,
                                ExecutionDate = ch.ExecutionDate,
                                ReferenceNo = ch.ReferenceNo,
                                FromServiceUnitID = ch.FromServiceUnitID,
                                ToServiceUnitID = ch.ToServiceUnitID,
                                ClassID = ch.ClassID,
                                RoomID = ch.RoomID,
                                BedID = ch.BedID,
                                DueDate = ch.DueDate,
                                SRShift = ch.SRShift,
                                SRItemType = ch.SRItemType,
                                IsProceed = ch.IsProceed,
                                IsApproved = ch.IsApproved,
                                IsVoid = ch.IsVoid,
                                IsOrder = ch.IsOrder,
                                IsCorrection = ch.IsCorrection,
                                IsClusterAssign = ch.IsClusterAssign,
                                IsAutoBillTransaction = ch.IsAutoBillTransaction,
                                IsBillProceed = ch.IsBillProceed,
                                Notes = ch.Notes,
                                LastUpdateDateTime = ch.LastUpdateDateTime,
                                LastUpdateByUserID = ch.LastUpdateByUserID,
                                SRTypeResult = ch.SRTypeResult,
                                ResponUnitID = ch.ResponUnitID
                            };
                            tch.Save();
                        }

                        //transchargesitem
                        var ci = new TransChargesItem();
                        if (ci.LoadByPrimaryKey(costHistory.TransactionNo, costHistory.SequenceNo))
                        {
                            var tci = new TransChargesItemHistory
                            {
                                RecalculationProcessNo = recalHistory.RecalculationProcessNo,
                                TransactionNo = ci.TransactionNo,
                                SequenceNo = ci.SequenceNo,
                                ReferenceNo = ci.ReferenceNo,
                                ReferenceSequenceNo = ci.ReferenceSequenceNo,
                                ItemID = ci.ItemID,
                                ChargeClassID = ci.ChargeClassID,
                                ParamedicID = ci.ParamedicID,
                                SecondParamedicID = ci.SecondParamedicID,
                                IsAdminCalculation = ci.IsAdminCalculation,
                                IsVariable = ci.IsVariable,
                                IsCito = ci.IsCito,
                                ChargeQuantity = ci.ChargeQuantity,
                                StockQuantity = ci.StockQuantity,
                                SRItemUnit = ci.SRItemUnit,
                                CostPrice = ci.CostPrice,
                                Price = ci.Price,
                                DiscountAmount = ci.DiscountAmount,
                                CitoAmount = ci.CitoAmount,
                                RoundingAmount = ci.RoundingAmount,
                                SRDiscountReason = ci.SRDiscountReason,
                                IsAssetUtilization = ci.IsAssetUtilization,
                                AssetID = ci.AssetID,
                                IsBillProceed = ci.IsBillProceed,
                                IsOrderRealization = ci.IsOrderRealization,
                                IsPackage = ci.IsPackage,
                                IsApprove = ci.IsApprove,
                                IsVoid = ci.IsVoid,
                                Notes = ci.Notes,
                                FilmNo = ci.FilmNo,
                                LastUpdateDateTime = ci.LastUpdateDateTime,
                                LastUpdateByUserID = ci.LastUpdateByUserID,
                                ParentNo = ci.ParentNo,
                                SRCenterID = ci.SRCenterID,
                                AutoProcessCalculation = ci.AutoProcessCalculation
                            };
                            tci.Save();
                        }

                        //transchargesitemcomp
                        var tcics = new TransChargesItemCompHistoryCollection();

                        var chargesItemComps = new TransChargesItemCompCollection();
                        chargesItemComps.Query.Where(string.Format("<TransactionNo + SequenceNo IN ('{0}')>", costHistory.TransactionNo + costHistory.SequenceNo));
                        chargesItemComps.LoadAll();

                        foreach (var cic in chargesItemComps)
                        {
                            var tcic = tcics.AddNew();
                            tcic.RecalculationProcessNo = recalHistory.RecalculationProcessNo;
                            tcic.TransactionNo = cic.TransactionNo;
                            tcic.SequenceNo = cic.SequenceNo;
                            tcic.TariffComponentID = cic.TariffComponentID;
                            tcic.Price = cic.Price;
                            tcic.DiscountAmount = cic.DiscountAmount;
                            tcic.ParamedicID = cic.ParamedicID;
                            tcic.LastUpdateDateTime = cic.LastUpdateDateTime;
                            tcic.LastUpdateByUserID = cic.LastUpdateByUserID;
                            tcic.IsPackage = cic.IsPackage;
                            tcic.AutoProcessCalculation = cic.AutoProcessCalculation;
                        }
                        tcics.Save();

                        //transprescription
                        var p = new TransPrescription();
                        if (p.LoadByPrimaryKey(costHistory.TransactionNo))
                        {
                            var tp = new TransPrescriptionHistory
                            {
                                RecalculationProcessNo = recalHistory.RecalculationProcessNo,
                                PrescriptionNo = p.PrescriptionNo,
                                PrescriptionDate = p.PrescriptionDate,
                                RegistrationNo = p.RegistrationNo,
                                ServiceUnitID = p.ServiceUnitID,
                                ClassID = p.ClassID,
                                ParamedicID = p.ParamedicID,
                                IsApproval = p.IsApproval,
                                IsVoid = p.IsVoid,
                                Note = p.Note,
                                LastUpdateDateTime = p.LastUpdateDateTime,
                                LastUpdateByUserID = p.LastUpdateByUserID,
                                IsPrescriptionReturn = p.IsPrescriptionReturn,
                                ReferenceNo = p.ReferenceNo,
                                IsFromSOAP = p.IsFromSOAP,
                                IsBillProceed = p.IsBillProceed,
                                IsUnitDosePrescription = p.IsUnitDosePrescription,
                                IsCito = p.IsCito,
                                IsClosed = p.IsClosed,
                                ApprovalDateTime = p.ApprovalDateTime,
                                DeliverDateTime = p.DeliverDateTime
                            };
                            tp.Save();
                        }

                        //transprescriptionitem
                        var pi = new TransPrescriptionItem();
                        if (pi.LoadByPrimaryKey(costHistory.TransactionNo, costHistory.SequenceNo))
                        {
                            var tpi = new TransPrescriptionItemHistory
                            {
                                RecalculationProcessNo = recalHistory.RecalculationProcessNo,
                                PrescriptionNo = pi.PrescriptionNo,
                                SequenceNo = pi.SequenceNo,
                                ParentNo = pi.ParentNo,
                                IsRFlag = pi.IsRFlag,
                                IsCompound = pi.IsCompound,
                                ItemID = pi.ItemID,
                                ItemInterventionID = pi.ItemInterventionID,
                                SRItemUnit = pi.SRItemUnit,
                                ItemQtyInString = pi.ItemQtyInString,
                                IsUsingDosageUnit = pi.IsUsingDosageUnit,
                                SRDosageUnit = pi.SRDosageUnit,
                                FrequencyOfDosing = pi.FrequencyOfDosing,
                                DosingPeriod = pi.DosingPeriod,
                                NumberOfDosage = pi.NumberOfDosage,
                                DurationOfDosing = pi.DurationOfDosing,
                                Acpcdc = pi.Acpcdc,
                                SRMedicationRoute = pi.SRMedicationRoute,
                                ConsumeMethod = string.IsNullOrEmpty(pi.ConsumeMethod) ? string.Empty : pi.ConsumeMethod,
                                PrescriptionQty = pi.PrescriptionQty,
                                TakenQty = pi.TakenQty,
                                ResultQty = pi.ResultQty,
                                CostPrice = pi.CostPrice,
                                InitialPrice = pi.InitialPrice,
                                Price = pi.Price,
                                DiscountAmount = pi.DiscountAmount,
                                EmbalaceID = pi.EmbalaceID,
                                EmbalaceAmount = pi.EmbalaceAmount,
                                IsUseSweetener = pi.IsUseSweetener,
                                SweetenerAmount = pi.SweetenerAmount,
                                LineAmount = pi.LineAmount,
                                Notes = pi.Notes,
                                LastUpdateDateTime = pi.LastUpdateDateTime,
                                LastUpdateByUserID = pi.LastUpdateByUserID,
                                SRDiscountReason = pi.SRDiscountReason,
                                IsApprove = pi.IsApprove,
                                IsVoid = pi.IsVoid,
                                IsBillProceed = pi.IsBillProceed,
                                DurationRelease = pi.DurationRelease,
                                AutoProcessCalculation = pi.AutoProcessCalculation,
                                //  ConsumeMethodText = pi.ConsumeMethodText
                            };
                            tpi.Save();
                        }

                        #endregion
                    }

                    var cost = new CostCalculation();
                    var compColl = new TransChargesItemCompCollection();
                    if (Request.QueryString["TYPE"] == "1")
                    {
                        decimal total = 0, param = 0;

                        var item = new TransChargesItem();

                        if (pnlParamedicID.Visible)
                        {
                            item.LoadByPrimaryKey(Request.QueryString["transNo"], Request.QueryString["seqNo"]);

                            item.ParamedicID = cboParamedicID.SelectedValue;
                            //item.LastUpdateDateTime = DateTime.Now.Date;
                            //item.LastUpdateByUserID = AppSession.UserLogin.UserID;

                            item.Save();
                        }

                        decimal price = 0, citoAmt = 0;
                        var paramedicNameColl = string.Empty;

                        if (pnlComponentTariff.Visible)
                        {
                            compColl.Query.Where(
                                    compColl.Query.TransactionNo == Request.QueryString["transNo"],
                                    compColl.Query.SequenceNo == Request.QueryString["seqNo"]
                                );
                            compColl.LoadAll();

                            foreach (var compEntity in compColl)
                            {
                                foreach (var dataItem in grdTariff.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => compEntity.TariffComponentID == dataItem["TariffComponentID"].Text))
                                {
                                    compEntity.Price = (decimal)(dataItem["TariffComponentName"].FindControl("txtPrice") as RadNumericTextBox).Value;
                                    var basicCitoAmt = (decimal)(dataItem["TariffComponentName"].FindControl("txtBasicCitoAmount") as RadNumericTextBox).Value;
                                    compEntity.CitoAmount = compEntity.Price * basicCitoAmt / 100;

                                    if ((dataItem["TariffComponentName"].FindControl("txtDiscountPercent") as RadNumericTextBox).Text != string.Empty)
                                    {
                                        var diskonpercent = (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscountPercent") as RadNumericTextBox).Value;
                                        compEntity.DiscountAmount = (compEntity.Price + compEntity.CitoAmount) * (diskonpercent / 100);
                                    }
                                    else
                                    {
                                        compEntity.DiscountAmount = (dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Text.Equals(string.Empty) ?
                                            (decimal)0D : (decimal)(dataItem["TariffComponentName"].FindControl("txtDiscount") as RadNumericTextBox).Value;
                                        if (compEntity.DiscountAmount > (compEntity.Price + compEntity.CitoAmount))
                                            compEntity.DiscountAmount = (compEntity.Price + compEntity.CitoAmount);
                                    }

                                    price += compEntity.Price ?? 0;
                                    citoAmt += compEntity.CitoAmount ?? 0;

                                    compEntity.FeeDiscountPercentage = (decimal?)(dataItem["TariffComponentName"].FindControl("txtPercentFeeDiscount") as RadNumericTextBox).Value;
                                    var fee = compEntity.CalculateParamedicPercentDiscount(
                                        AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                        cc.RegistrationNo, cc.ItemID, (compEntity.DiscountAmount ?? 0),
                                        AppSession.UserLogin.UserID, transHD.ClassID, transHD.ToServiceUnitID);

                                    total += compEntity.DiscountAmount ?? 0;

                                    compEntity.ParamedicID = (dataItem["TariffComponentName"].FindControl("cboPhysicianID") as RadComboBox).SelectedValue;

                                    if (!string.IsNullOrEmpty(compEntity.ParamedicID))
                                    {
                                        param += ((item.ChargeQuantity ?? 0) * (compEntity.DiscountAmount ?? 0));

                                        var tComp = new BusinessObject.TariffComponent();
                                        if (tComp.LoadByPrimaryKey(compEntity.TariffComponentID))
                                        {
                                            if (tComp.IsPrintParamedicInSlip ?? false)
                                            {
                                                var par = new Paramedic();
                                                par.LoadByPrimaryKey(compEntity.ParamedicID);
                                                if (par.IsPrintInSlip ?? true)
                                                {
                                                    if (paramedicNameColl.Length == 0)
                                                        paramedicNameColl = par.ParamedicName;
                                                    else if (!paramedicNameColl.Contains(par.ParamedicName))
                                                        paramedicNameColl = paramedicNameColl + "; " + par.ParamedicName;
                                                }
                                            }
                                        }
                                    }

                                    break;
                                }
                            }

                            compColl.Save();
                        }

                        item.LoadByPrimaryKey(Request.QueryString["transNo"], Request.QueryString["seqNo"]);

                        if (price != 0)
                            item.Price = price;
                        if (citoAmt != 0)
                            item.CitoAmount = Math.Abs(item.ChargeQuantity ?? 0) * citoAmt;

                        item.DiscountAmount = pnlComponentTariff.Visible
                                                  ? (total * Math.Abs(item.ChargeQuantity ?? 0))
                                                  : Math.Abs(item.ChargeQuantity ?? 0) *
                                                    (decimal)txtDiscountAmount1.Value;
                        item.SRDiscountReason = cboSRDiscountReason.SelectedValue;
                        item.ParamedicCollectionName = paramedicNameColl;
                        if (trNotes.Visible)
                            item.Notes = txtNotes.Text;

                        item.Save();

                        // jasmed
                        //foreach (var compEntity in compColl)
                        //{
                        if (AppSession.Parameter.IsFeeCalculatedOnTransaction)
                        {
                            // extract fee
                            var feeColl = new ParamedicFeeTransChargesItemCompByDischargeDateCollection();
                            feeColl.SetFeeByTCIC(compColl, AppSession.UserLogin.UserID);
                            feeColl.Save();
                            //feeColl.SetPaymentAndInvoicePaymentAfterSave(AppSession.UserLogin.UserID);
                            //feeColl.Save();
                        }
                        //}
                        //calculation
                        var charges = new TransCharges();
                        charges.LoadByPrimaryKey(Request.QueryString["transNo"]);

                        var grrID = reg.GuarantorID;
                        if (grrID == AppSession.Parameter.SelfGuarantor)
                        {
                            var pat = new Patient();
                            pat.LoadByPrimaryKey(reg.PatientID);
                            if (!string.IsNullOrEmpty(pat.MemberID))
                                grrID = pat.MemberID;
                        }

                        if (cost.Load(ccQuery))
                        {
                            //--start hitung ulang
                            //db:20250318 - hanya u/ item dengan tariff variable
                            if (item.IsVariable == true)
                            {
                                var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, reg.CoverageClassID,
                                                                        item.ItemID, DateTime.Now.Date, false, item.Price);
                                var totalitem = (item.ChargeQuantity * item.Price) + item.CitoAmount;
                                var calc = new Helper.CostCalculation(grrID, item.ItemID, totalitem ?? 0, tblCovered, item.ChargeQuantity ?? 0,
                                                                              item.IsCito ?? false,
                                                                              item.IsCitoInPercent ?? false,
                                                                              item.BasicCitoAmount ?? 0, item.Price ?? 0,
                                                                              charges.IsRoomIn ?? false, item.IsItemRoom ?? false,
                                                                              charges.TariffDiscountForRoomIn ?? 0, item.DiscountAmount ?? 0, false,
                                                                              item.ItemConditionRuleID, charges.TransactionDate.Value, false);
                                cost.GuarantorAmount = calc.GuarantorAmount;
                                cost.PatientAmount = calc.PatientAmount;
                            }
                            //--end hitung ulang

                            //post
                            decimal? totaltrans = Math.Abs(cost.GuarantorAmount ?? 0) +
                                                  Math.Abs(cost.PatientAmount ?? 0) + Math.Abs(cost.DiscountAmount ?? 0);
                            decimal? totaldisc = Math.Abs(item.DiscountAmount ?? 0);


                            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                            {
                                if (totaldisc >= totaltrans)
                                {
                                    cost.GuarantorAmount = 0;
                                    cost.PatientAmount = 0;
                                }
                                else
                                {
                                    cost.GuarantorAmount = totaltrans - totaldisc;
                                    cost.PatientAmount = 0;
                                }
                                cost.DiscountAmount = totaldisc;
                            }
                            else
                            {
                                if (Math.Abs(cost.GuarantorAmount ?? 0) > 0)
                                {
                                    decimal totGuarantorAmt = Math.Abs(cost.GuarantorAmount ?? 0) + Math.Abs(cost.DiscountAmount ?? 0);
                                    cost.DiscountAmount = totaldisc > totGuarantorAmt
                                        ? totGuarantorAmt
                                        : totaldisc;

                                    cost.GuarantorAmount = totaldisc > totGuarantorAmt
                                        ? 0
                                        : totGuarantorAmt - totaldisc;

                                }
                                else
                                {
                                    decimal totPatientAmt = Math.Abs(cost.PatientAmount ?? 0) + Math.Abs(cost.DiscountAmount ?? 0);
                                    cost.DiscountAmount = totaldisc > totPatientAmt
                                        ? totPatientAmt
                                        : totaldisc;

                                    cost.PatientAmount = totaldisc > totPatientAmt
                                        ? 0
                                        : totPatientAmt - totaldisc;
                                }

                                if (totaldisc > cost.DiscountAmount)
                                {
                                    //hitung ulang diskon di TransChargesItem & TransChargesItemComp
                                    if (pnlComponentTariff.Visible)
                                    {
                                        var i = compColl.Count;
                                        foreach (var compEntity in compColl)
                                        {
                                            compEntity.DiscountAmount = i == 1
                                                ? (cost.DiscountAmount / Math.Abs(item.ChargeQuantity ?? 0))
                                                : (compEntity.Price + compEntity.CitoAmount) * (cost.DiscountAmount / item.DiscountAmount);

                                            var fee = compEntity.CalculateParamedicPercentDiscount(
                                                AppSession.Parameter.IsTarifCompPhysicianDiscountMaxByShare,
                                                cc.RegistrationNo, cc.ItemID, (compEntity.DiscountAmount ?? 0),
                                                AppSession.UserLogin.UserID, transHD.ClassID, transHD.ToServiceUnitID);

                                        }

                                        compColl.Save();
                                    }

                                    item.DiscountAmount = cost.DiscountAmount;
                                    item.Save();
                                }
                            }

                            cost.PatientAmount = (item.ChargeQuantity < 0) ? 0 - cost.PatientAmount : cost.PatientAmount;
                            cost.GuarantorAmount = (item.ChargeQuantity < 0) ? 0 - cost.GuarantorAmount : cost.GuarantorAmount;
                            cost.DiscountAmount = (item.ChargeQuantity < 0) ? 0 - cost.DiscountAmount : cost.DiscountAmount;
                            cost.ParamedicAmount = param;
                            cost.LastUpdateDateTime = DateTime.Now;
                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cost.Save();

                        }
                    }
                    else if (Request.QueryString["TYPE"] == "2")
                    {
                        var presc = new TransPrescription();
                        presc.LoadByPrimaryKey(Request.QueryString["transNo"]);

                        var grrID = reg.GuarantorID;
                        if (grrID == AppSession.Parameter.SelfGuarantor)
                        {
                            var pat = new Patient();
                            pat.LoadByPrimaryKey(reg.PatientID);
                            if (!string.IsNullOrEmpty(pat.MemberID))
                                grrID = pat.MemberID;
                        }

                        var item = new TransPrescriptionItem();
                        item.LoadByPrimaryKey(Request.QueryString["transNo"], Request.QueryString["seqNo"]);

                        var tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, string.IsNullOrEmpty(item.ItemInterventionID) ? item.ItemID : item.ItemInterventionID, presc.PrescriptionDate.Value, true);

                        //-----------------
                        decimal resultQty = item.ResultQty ?? 0;
                        decimal rPrice = (item.RecipeAmount ?? 0) + (item.EmbalaceAmount ?? 0) + (item.SweetenerAmount ?? 0);
                        decimal? lineAmt = ((Math.Abs(resultQty) * item.Price) + rPrice);
                        bool IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);
                        if (txtDiscountPercent.Value > 0)
                        {
                            decimal _lineAmt = Convert.ToDecimal(Math.Abs(resultQty) * item.Price) + (IsIncR ? rPrice : 0);
                            if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                            {
                                _lineAmt = Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription);
                                item.DiscountAmount = ((decimal)txtDiscountPercent.Value / 100 * (_lineAmt));
                            }
                            else
                                item.DiscountAmount = ((decimal)txtDiscountPercent.Value / 100 * (_lineAmt));
                        }
                        else
                            item.DiscountAmount = (Math.Abs(item.ResultQty ?? 0) * (decimal)txtDiscountAmount1.Value);

                        item.SRDiscountReason = cboSRDiscountReason.SelectedValue;

                        if (AppSession.Parameter.IsPrescriptionDiscountAfterRounding)
                            lineAmt = Helper.Rounding(Convert.ToDecimal(lineAmt), AppEnum.RoundingType.Prescription) - item.DiscountAmount;
                        else
                            lineAmt = Helper.Rounding(Convert.ToDecimal(lineAmt - item.DiscountAmount), AppEnum.RoundingType.Prescription);
                        
                        item.LineAmount = item.ResultQty < 0 ? 0 - lineAmt : lineAmt;
                        if (trNotes.Visible)
                            item.Notes = txtNotes.Text;
                        //-----------------

                        item.Save();

                        if (cost.Load(ccQuery))
                        {
                            decimal recipeAmount = 0;
                            //if (!IsIncR)
                            //{
                            recipeAmount = (item.EmbalaceAmount ?? 0) + (item.SweetenerAmount ?? 0) + (item.RecipeAmount ?? 0);
                            //}
                            var calc = new Helper.CostCalculation(grrID, reg.IsGlobalPlafond ?? false,
                                       string.IsNullOrEmpty(item.ItemInterventionID) ? item.ItemID : item.ItemInterventionID, Math.Abs(item.LineAmount ?? 0),
                                       tblCovered, Math.Abs(resultQty), item.Price ?? 0, (item.RecipeAmount ?? 0), item.DiscountAmount ?? 0);

                            cost.PatientAmount = calc.PatientAmount;
                            cost.GuarantorAmount = calc.GuarantorAmount;
                            if (reg.SRBussinesMethod == AppSession.Parameter.BusinessMethodFlavon)
                            {
                                cost.GuarantorAmount = calc.GuarantorAmount + calc.PatientAmount;
                                cost.PatientAmount = 0;
                            }
                            cost.DiscountAmount = item.DiscountAmount;

                            //decimal? totaldiscpresc = Math.Abs(item.DiscountAmount ?? 0);

                            //if (Math.Abs(cost.GuarantorAmount ?? 0) > 0)
                            //{
                            //    if (totaldiscpresc >= Math.Abs(cost.GuarantorAmount ?? 0) + Math.Abs(cost.PatientAmount ?? 0))
                            //    {
                            //        cost.GuarantorAmount = 0;
                            //        cost.PatientAmount = 0;
                            //    }
                            //    else
                            //    {
                            //        decimal? tdiscpresc = totaldiscpresc > Math.Abs(cost.GuarantorAmount ?? 0)
                            //                            ? totaldiscpresc - Math.Abs(cost.GuarantorAmount ?? 0)
                            //                            : 0;
                            //        cost.GuarantorAmount = totaldiscpresc > Math.Abs(cost.GuarantorAmount ?? 0)
                            //                                   ? 0
                            //                                   : Math.Abs(cost.GuarantorAmount ?? 0) - totaldiscpresc;
                            //        cost.PatientAmount = Math.Abs(cost.PatientAmount ?? 0) - tdiscpresc;
                            //    }
                            //}
                            //else
                            //{
                            //    cost.PatientAmount = totaldiscpresc > Math.Abs(cost.PatientAmount ?? 0)
                            //                             ? 0
                            //                             : Math.Abs(cost.PatientAmount ?? 0) - totaldiscpresc;
                            //    cost.DiscountAmount = totaldiscpresc > Math.Abs(cost.PatientAmount ?? 0)
                            //                              ? Math.Abs(cost.PatientAmount ?? 0)
                            //                              : totaldiscpresc;
                            //}

                            cost.PatientAmount = (resultQty < 0) ? 0 - cost.PatientAmount : cost.PatientAmount;
                            cost.GuarantorAmount = (resultQty < 0) ? 0 - cost.GuarantorAmount : cost.GuarantorAmount;
                            cost.DiscountAmount = (resultQty < 0) ? 0 - item.DiscountAmount : item.DiscountAmount;
                            cost.LastUpdateDateTime = DateTime.Now;
                            cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            cost.Save();
                        }
                    }

                    trans.Complete();
                }
            }
            else
                ShowInformationHeader(msg);

            return !isNotValid;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        protected void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            var dt = new TransPrescriptionItem();
            if (dt.LoadByPrimaryKey(Request.QueryString["transNo"], Request.QueryString["seqNo"]))
            {
                double price = (double)dt.Price;
                double recipeAmt = (double)dt.RecipeAmount + (double)dt.EmbalaceAmount + (double)dt.SweetenerAmount;
                double qty = Math.Abs((double)dt.ResultQty);
                if (qty == 0) qty = 1;

                var IsIncR = (AppSession.Parameter.IsPrescriptionDiscountIncludeR);
                txtDiscountAmount1.Value = IsIncR ? (txtDiscountPercent.Value / 100) * (price + (recipeAmt/qty)) : (txtDiscountPercent.Value / 100) * price;
                txtDiscountAmount1.MaxValue = IsIncR ? price + (recipeAmt / qty) : price;
            }
            else
            {
                txtDiscountAmount1.Value = (txtDiscountPercent.Value / 100) * txtPrice.Value;
                txtDiscountAmount1.MaxValue = txtPrice.Value ?? 0;
            }
        }
    }
}