using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Avicenna.WebService;

namespace Temiang.Avicenna.Module.RADT.InPatient
{
    public partial class CheckInConfirmationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.CheckInConfirmation;

            if (!IsPostBack)
            {
                var units = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);
                query.Where(
                    query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    query.IsActive == true
                    );
                query.OrderBy(query.ServiceUnitName.Ascending);
                units.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in units)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                grdList.Columns.FindByUniqueName("cboSMF").Visible = !AppSession.Parameter.IsCheckinConfirmationUsingDetails; // cboSmf
                grdList.Columns.FindByUniqueName("SmfName").Visible = AppSession.Parameter.IsCheckinConfirmationUsingDetails; // lblSmf
                grdList.Columns.FindByUniqueName("IsRoomingIn").Visible = AppSession.Parameter.IsUsingRoomingIn; // IsRoomingIn
                grdList.Columns.FindByUniqueName("chkIsFileReceived").Visible = !AppSession.Parameter.IsCheckinConfirmationUsingDetails; // chkFileReceived
                grdList.Columns.FindByUniqueName("DirectConfirmed").Visible = !AppSession.Parameter.IsCheckinConfirmationUsingDetails; // Direct Confirmed
                grdList.Columns.FindByUniqueName("UsingDetail").Visible = AppSession.Parameter.IsCheckinConfirmationUsingDetails; // Using Detail
            }
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Beds;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
        }

        private DataTable Beds
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(cboRoomID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) &&
                    string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Check In Confirmation")) return null;

                var qr = new BedQuery("a");
                var srq = new ServiceRoomQuery("b");
                var suq = new ServiceUnitQuery("c");
                var cq = new ClassQuery("e");
                var rq = new RegistrationQuery("f");
                var pq = new PatientQuery("g");
                var md = new ParamedicQuery("h");
                var guarq = new GuarantorQuery("i");
                var qusr = new AppUserServiceUnitQuery("u");
                var riq = new BedRoomInQuery("ri");
                var smf = new SmfQuery("smf");
                var ccq = new ClassQuery("cc");
                var sal = new AppStandardReferenceItemQuery("sal");
                var cl = new ClassQuery("cl");
                var cl2 = new ClassQuery("cl2");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                        rq.RegistrationDate,
                        qr.RegistrationNo,
                        pq.MedicalNo,
                        pq.PatientName,
                        pq.Sex,
                        srq.ServiceUnitID,
                        suq.ServiceUnitName,
                        qr.RoomID,
                        srq.RoomName,
                        qr.ClassID,
                        cq.ClassName,
                        qr.BedID,
                        md.ParamedicName,
                        guarq.GuarantorName,
                        @"<CAST(0 AS BIT) AS 'IsFromTransfer'>",
                        @"<CAST(0 AS BIT) AS 'IsRoomingIn'>",
                        rq.SmfID,
                        @"<CAST(0 AS BIT) AS 'IsFileReceived'>",
                        rq.ChargeClassID,
                        rq.CoverageClassID,
                        @"<'' AS TransferNo>",
                        smf.SmfName,
                        ccq.ClassName.As("ChargeClassName"),
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(cl.ClassSeq AS VARCHAR) AS ClassSeq1>",
                        @"<CAST(cl2.ClassSeq AS VARCHAR) AS ClassSeq2>"
                    );

                qr.InnerJoin(srq).On(qr.RoomID == srq.RoomID);
                qr.InnerJoin(suq).On(srq.ServiceUnitID == suq.ServiceUnitID);
                qr.InnerJoin(cq).On(qr.ClassID == cq.ClassID);
                qr.LeftJoin(rq).On(qr.RegistrationNo == rq.RegistrationNo);
                qr.LeftJoin(pq).On(rq.PatientID == pq.PatientID);
                qr.InnerJoin(qusr).On(rq.ServiceUnitID == qusr.ServiceUnitID);
                qr.LeftJoin(md).On(rq.ParamedicID == md.ParamedicID);
                qr.InnerJoin(guarq).On(rq.GuarantorID == guarq.GuarantorID);
                qr.LeftJoin(smf).On(rq.SmfID == smf.SmfID);
                qr.InnerJoin(ccq).On(ccq.ClassID == rq.ChargeClassID);
                //qr.LeftJoin(riq).On(qr.BedID == riq.BedID);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == pq.SRSalutation);
                qr.LeftJoin(cl).On(cl.ClassID == rq.ChargeClassID);
                qr.LeftJoin(cl2).On(cl2.ClassID == rq.CoverageClassID);
                qr.Where
                    (
                        qr.IsActive == true,
                        qr.SRBedStatus == AppSession.Parameter.BedStatusPending,
                        qusr.UserID == AppSession.UserLogin.UserID,
                        rq.IsClosed == false,
                        suq.SRRegistrationType == AppConstant.RegistrationType.InPatient//,
                        //riq.BedID.IsNull()
                    );

                if (cboRoomID.SelectedValue != string.Empty)
                    qr.Where(rq.RoomID == cboRoomID.SelectedValue);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(rq.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        qr.Where(
                            qr.Or(
                                    qr.RegistrationNo == searchReg,
                                    pq.MedicalNo == searchReg,
                                    string.Format("< OR REPLACE(g.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        qr.Where(
                            qr.Or(
                                    qr.RegistrationNo == searchReg,
                                    pq.MedicalNo == searchReg,
                                    string.Format("< OR g.MedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }
                
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = Helper.EscapeQuery(txtPatientName.Text);
                    string searchText = string.Format("%{0}%", searchPatient);
                    qr.Where
                        (
                            qr.Or
                                (
                                    pq.FirstName.Like(searchText),
                                    pq.MiddleName.Like(searchText),
                                    pq.LastName.Like(searchText)
                                )
                        );
                }

                qr.OrderBy
                    (
                        qr.RoomID.Ascending,
                        qr.ClassID.Ascending,
                        qr.BedID.Ascending
                    );

                DataTable table = qr.LoadDataTable();

                if (AppSession.Parameter.IsUsingRoomingIn)
                {
                    qr = new BedQuery("a");
                    srq = new ServiceRoomQuery("b");
                    suq = new ServiceUnitQuery("c");
                    cq = new ClassQuery("e");
                    rq = new RegistrationQuery("f");
                    pq = new PatientQuery("g");
                    md = new ParamedicQuery("h");
                    guarq = new GuarantorQuery("i");
                    qusr = new AppUserServiceUnitQuery("u");
                    riq = new BedRoomInQuery("ri");
                    smf = new SmfQuery("smf");
                    ccq = new ClassQuery("cc");
                    sal = new AppStandardReferenceItemQuery("sal");
                    cl = new ClassQuery("cl");
                    cl2 = new ClassQuery("cl2");

                    qr.es.Top = AppSession.Parameter.MaxResultRecord;

                    qr.Select
                        (
                            rq.RegistrationDate,
                            riq.RegistrationNo,
                            pq.MedicalNo,
                            pq.PatientName,
                            pq.Sex,
                            srq.ServiceUnitID,
                            suq.ServiceUnitName,
                            qr.RoomID,
                            srq.RoomName,
                            qr.ClassID,
                            cq.ClassName,
                            qr.BedID,
                            md.ParamedicName,
                            guarq.GuarantorName,
                            @"<CAST(0 AS BIT) AS 'IsFromTransfer'>",
                            @"<CAST(1 AS BIT) AS 'IsRoomingIn'>",
                            rq.SmfID,
                            @"<CAST(0 AS BIT) AS 'IsFileReceived'>",
                            @"<'' AS TransferNo>",
                            smf.SmfName,
                            ccq.ClassName.As("ChargeClassName"),
                            sal.ItemName.As("SalutationName"),
                            @"<CAST(cl.ClassSeq AS VARCHAR) AS ClassSeq1>",
                            @"<CAST(cl2.ClassSeq AS VARCHAR) AS ClassSeq2>"
                        );

                    qr.InnerJoin(riq).On(qr.BedID == riq.BedID);
                    qr.InnerJoin(srq).On(qr.RoomID == srq.RoomID);
                    qr.InnerJoin(suq).On(srq.ServiceUnitID == suq.ServiceUnitID);
                    qr.InnerJoin(cq).On(qr.ClassID == cq.ClassID);
                    qr.LeftJoin(rq).On(riq.RegistrationNo == rq.RegistrationNo);
                    qr.LeftJoin(pq).On(rq.PatientID == pq.PatientID);
                    qr.InnerJoin(qusr).On(rq.ServiceUnitID == qusr.ServiceUnitID);
                    qr.InnerJoin(md).On(rq.ParamedicID == md.ParamedicID);
                    qr.InnerJoin(guarq).On(rq.GuarantorID == guarq.GuarantorID);
                    qr.LeftJoin(smf).On(rq.SmfID == smf.SmfID);
                    qr.InnerJoin(ccq).On(ccq.ClassID == rq.ChargeClassID);
                    qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == pq.SRSalutation);
                    qr.LeftJoin(cl).On(cl.ClassID == rq.ChargeClassID);
                    qr.LeftJoin(cl2).On(cl2.ClassID == rq.CoverageClassID);

                    qr.Where
                        (
                            qr.IsActive == true,
                            riq.SRBedStatus == AppSession.Parameter.BedStatusPending,
                            qusr.UserID == AppSession.UserLogin.UserID,
                            rq.IsClosed == false,
                            suq.SRRegistrationType == AppConstant.RegistrationType.InPatient
                        );

                    if (cboRoomID.SelectedValue != string.Empty)
                        qr.Where(rq.RoomID == cboRoomID.SelectedValue);
                    if (cboServiceUnitID.SelectedValue != string.Empty)
                        qr.Where(rq.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    if (txtRegistrationNo.Text != string.Empty)
                    {
                        string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                        qr.Where(
                            qr.Or(
                                    riq.RegistrationNo == searchReg,
                                    pq.MedicalNo == searchReg,
                                    string.Format("< OR REPLACE(g.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    }
                    if (txtPatientName.Text != string.Empty)
                    {
                        string searchPatient = Helper.EscapeQuery(txtPatientName.Text);
                        string searchText = string.Format("%{0}%", searchPatient);
                        qr.Where
                            (
                                qr.Or
                                    (
                                        pq.FirstName.Like(searchText),
                                        pq.MiddleName.Like(searchText),
                                        pq.LastName.Like(searchText)
                                    )
                            );
                    }

                    qr.OrderBy
                        (
                            qr.RoomID.Ascending,
                            qr.ClassID.Ascending,
                            qr.BedID.Ascending
                        );
                    DataTable table2 = qr.LoadDataTable();
                    table.Merge(table2);
                }

                foreach (DataRow row in table.Rows)
                {
                    var q = new PatientTransferQuery();
                    q.Where(q.RegistrationNo == row["RegistrationNo"].ToString(),
                            q.ToServiceUnitID == row["ServiceUnitID"].ToString(), q.IsApprove == true);
                    q.Select(q.TransferNo);
                    q.es.Top = 1;
                    q.OrderBy(q.TransferDate.Descending);
                    DataTable dtb = q.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        row["IsFromTransfer"] = true;
                        row["TransferNo"] = dtb.Rows[0]["TransferNo"].ToString();
                    }
                }

                table.AcceptChanges();

                return table;
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "validate")
            {
                GridItem item = e.Item as GridItem;
                if (item == null)
                    return;

                if (string.IsNullOrEmpty((item.FindControl("cboSMF") as RadComboBox).SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validate", "alert('SMF is required');", true);
                    return;
                }

                bool isFileReceived = ((CheckBox)item.FindControl("chkIsFileReceived")).Checked;

                //auto bill
                #region auto bill

                //var autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.TransactionNo);
                //string transNo = string.Empty;

                //var reg = new Registration();
                //reg.LoadByPrimaryKey((string)item.OwnerTableView.DataKeyValues[item.ItemIndex][BedMetadata.ColumnNames.RegistrationNo]);

                //var grrID = reg.GuarantorID;
                //if (grrID == AppSession.Parameter.SelfGuarantor)
                //{
                //    var pat = new Patient();
                //    pat.LoadByPrimaryKey(reg.PatientID);
                //    if (!string.IsNullOrEmpty(pat.MemberID))
                //        grrID = pat.MemberID;
                //}

                //var grr = new Guarantor();
                //grr.LoadByPrimaryKey(grrID);

                //var billColl = new ServiceUnitAutoBillItemCollection();
                //billColl.Query.Where
                //    (
                //        billColl.Query.ServiceUnitID == reg.ServiceUnitID,
                //        billColl.Query.IsGenerateOnRegistration == true,
                //        billColl.Query.IsActive == true
                //    );
                //billColl.LoadAll();

                //var chargesHD = new TransCharges();

                //if (billColl.Count > 0)
                //{
                //    chargesHD.TransactionNo = autoNumber.LastCompleteNumber;
                //    autoNumber.LastCompleteNumber = chargesHD.TransactionNo;
                //    autoNumber.Save();

                //    transNo = chargesHD.TransactionNo;

                //    chargesHD.RegistrationNo = reg.RegistrationNo;
                //    chargesHD.TransactionDate = (new DateTime()).NowAtSqlServer().Date;
                //    chargesHD.ReferenceNo = string.Empty;
                //    chargesHD.FromServiceUnitID = reg.ServiceUnitID;
                //    chargesHD.ToServiceUnitID = reg.ServiceUnitID;
                //    chargesHD.ClassID = reg.ChargeClassID;
                //    chargesHD.RoomID = reg.RoomID;
                //    chargesHD.BedID = reg.BedID;
                //    chargesHD.DueDate = (new DateTime()).NowAtSqlServer().Date;
                //    chargesHD.SRShift = Registration.GetShiftID();
                //    chargesHD.SRItemType = string.Empty;
                //    chargesHD.IsProceed = false;
                //    chargesHD.IsBillProceed = true;
                //    chargesHD.IsApproved = true;
                //    chargesHD.IsVoid = false;
                //    chargesHD.IsOrder = false;
                //    chargesHD.IsCorrection = false;
                //    chargesHD.IsClusterAssign = false;
                //    chargesHD.IsAutoBillTransaction = true;
                //    chargesHD.Notes = string.Empty;
                //    chargesHD.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    chargesHD.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                //}

                //var TransChargesItemsDT = new TransChargesItemCollection();
                //var TransChargesItemsDTComp = new TransChargesItemCompCollection();
                //var TransChargesItemsDTConsumption = new TransChargesItemConsumptionCollection();
                //var CostCalculations = new CostCalculationCollection();

                //string itemTypeHD, seqNo = string.Empty;

                //foreach (ServiceUnitAutoBillItem billItem in billColl)
                //{
                //    var it = new Item();
                //    it.LoadByPrimaryKey(billItem.ItemID);
                //    itemTypeHD = it.SRItemType;

                //    seqNo = TransChargesItemsDT.Count == 0 ? "001" :
                //        string.Format("{0:000}", int.Parse(TransChargesItemsDT[TransChargesItemsDT.Count - 1].SequenceNo) + 1);

                //    var chargesDT = TransChargesItemsDT.AddNew();
                //    chargesDT.TransactionNo = transNo;
                //    chargesDT.SequenceNo = seqNo;
                //    chargesDT.ReferenceNo = string.Empty;
                //    chargesDT.ReferenceSequenceNo = string.Empty;
                //    chargesDT.ItemID = billItem.ItemID;
                //    chargesDT.ChargeClassID = reg.ChargeClassID;
                //    chargesDT.ParamedicID = reg.ParamedicID;

                //    ItemTariff tariff = Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, grr.SRTariffType,
                //                                                    chargesHD.ClassID, billItem.ItemID, reg.GuarantorID);
                //    if (tariff == null)
                //        tariff = Helper.Tariff.GetItemTariff(chargesHD.TransactionDate.Value, AppSession.Parameter.DefaultTariffType,
                //                                             AppSession.Parameter.DefaultTariffClass,
                //                                             billItem.ItemID, reg.GuarantorID);

                //    chargesDT.IsAdminCalculation = tariff.IsAdminCalculation ?? false;

                //    if (itemTypeHD == BusinessObject.Reference.ItemType.Medical)
                //    {
                //        var ipm = new ItemProductMedic();
                //        ipm.LoadByPrimaryKey(billItem.ItemID);
                //        chargesDT.SRItemUnit = ipm.SRItemUnit;

                //        chargesDT.CostPrice = ipm.CostPrice ?? 0;
                //    }
                //    else if (itemTypeHD == BusinessObject.Reference.ItemType.NonMedical)
                //    {
                //        var ipn = new ItemProductNonMedic();
                //        ipn.LoadByPrimaryKey(billItem.ItemID);
                //        chargesDT.SRItemUnit = ipn.SRItemUnit;

                //        chargesDT.CostPrice = ipn.CostPrice ?? 0;
                //    }
                //    else
                //    {
                //        var service = new ItemService();
                //        service.LoadByPrimaryKey(billItem.ItemID);
                //        chargesDT.SRItemUnit = service.SRItemUnit;

                //        chargesDT.CostPrice = tariff.Price ?? 0;
                //    }

                //    chargesDT.IsVariable = false;
                //    chargesDT.IsCito = false;
                //    chargesDT.ChargeQuantity = billItem.Quantity;

                //    if (itemTypeHD == BusinessObject.Reference.ItemType.Medical || itemTypeHD == BusinessObject.Reference.ItemType.NonMedical)
                //        chargesDT.StockQuantity = billItem.Quantity;
                //    else
                //        chargesDT.StockQuantity = (decimal)0D;

                //    chargesDT.Price = tariff.Price ?? 0;
                //    chargesDT.DiscountAmount = (decimal)0D;
                //    chargesDT.CitoAmount = (decimal)0D;
                //    chargesDT.RoundingAmount = Helper.RoundingDiff;
                //    chargesDT.SRDiscountReason = string.Empty;
                //    chargesDT.IsAssetUtilization = false;
                //    chargesDT.AssetID = string.Empty;
                //    chargesDT.IsBillProceed = true;
                //    chargesDT.IsOrderRealization = false;
                //    chargesDT.IsPackage = false;
                //    chargesDT.IsApprove = true;
                //    chargesDT.IsVoid = false;
                //    chargesDT.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    chargesDT.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //    #region item component
                //    var compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                //        grr.SRTariffType, chargesHD.ClassID, billItem.ItemID);
                //    if (!compColl.Any())
                //        compColl = Helper.Tariff.GetItemTariffComponentCollection(chargesHD.TransactionDate.Value,
                //            AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, billItem.ItemID);

                //    foreach (ItemTariffComponent comp in compColl)
                //    {
                //        TransChargesItemComp compCharges = TransChargesItemsDTComp.AddNew();
                //        compCharges.TransactionNo = transNo;
                //        compCharges.SequenceNo = seqNo;
                //        compCharges.TariffComponentID = comp.TariffComponentID;
                //        compCharges.Price = comp.Price;
                //        compCharges.DiscountAmount = (decimal)0D;

                //        TariffComponent tcomp = new TariffComponent();
                //        tcomp.LoadByPrimaryKey(comp.TariffComponentID);
                //        if (tcomp.IsTariffParamedic ?? false)
                //            compCharges.ParamedicID = reg.ParamedicID;
                //        else
                //            compCharges.ParamedicID = string.Empty;

                //        compCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //        compCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                //    }
                //    #endregion

                //    #region Item Consumption
                //    var consColl = new ItemConsumptionCollection();
                //    consColl.Query.Where(consColl.Query.ItemID == billItem.ItemID);
                //    consColl.LoadAll();

                //    foreach (ItemConsumption cons in consColl)
                //    {
                //        TransChargesItemConsumption consCharges = TransChargesItemsDTConsumption.AddNew();
                //        consCharges.TransactionNo = transNo;
                //        consCharges.SequenceNo = seqNo;
                //        consCharges.DetailItemID = cons.ItemID;
                //        consCharges.Qty = cons.Qty;
                //        consCharges.SRItemUnit = cons.SRItemUnit;
                //        consCharges.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //        consCharges.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                //    }
                //    #endregion

                //    #region auto calculation
                //    string[] itemID = new string[TransChargesItemsDT.Count];
                //    int index = 0;
                //    foreach (TransChargesItem detail in TransChargesItemsDT)
                //    {
                //        itemID.SetValue(detail.ItemID, index);

                //        index++;
                //    }

                //    if (itemID.Length == 0)
                //    {
                //        itemID = new string[1];
                //        itemID.SetValue(string.Empty, 0);
                //    }

                //    DataTable tblCovered = Helper.GetCoveredItems(reg.RegistrationNo, grrID, itemID, (new DateTime()).NowAtSqlServer().Date, false);

                //    foreach (TransChargesItem detail in TransChargesItemsDT)
                //    {
                //        CostCalculation cost = CostCalculations.AddNew();
                //        cost.RegistrationNo = reg.RegistrationNo;
                //        cost.TransactionNo = detail.TransactionNo;
                //        cost.SequenceNo = detail.SequenceNo;
                //        cost.ItemID = detail.ItemID;

                //        decimal? total = detail.ChargeQuantity * (detail.Price - detail.DiscountAmount) + detail.CitoAmount;
                //        decimal? qty = detail.ChargeQuantity;
                //        Helper.CostCalculation calc = new Helper.CostCalculation(grrID, detail.ItemID, total ?? 0, tblCovered, qty ?? 0, 
                //                                                                 detail.DiscountAmount ?? 0);

                //        cost.PatientAmount = calc.PatientAmount;
                //        cost.GuarantorAmount = calc.GuarantorAmount;
                //        cost.DiscountAmount = calc.DiscountAmount;

                //        decimal param = 0;
                //        foreach (TransChargesItemComp comp in TransChargesItemsDTComp)
                //        {
                //            if (comp.TransactionNo == detail.TransactionNo && comp.SequenceNo == detail.SequenceNo &&
                //                !string.IsNullOrEmpty(comp.ParamedicID))
                //                param += (comp.Price ?? 0);
                //        }

                //        cost.ParamedicAmount = param;

                //        cost.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                //        cost.LastUpdateByUserID = AppSession.UserLogin.UserID;
                //    }
                //    #endregion
                //}
                #endregion

                var registrationNo =
                    Convert.ToString(
                        item.OwnerTableView.DataKeyValues[item.ItemIndex][BedMetadata.ColumnNames.RegistrationNo]);

                //---- cek dulu apakah checkin confirmation dari patient transfer atau registration
                var pth = new PatientTransferHistoryQuery();
                pth.Select(pth.TransferNo);
                pth.Where(pth.RegistrationNo == registrationNo, pth.TransferNo != string.Empty);
                pth.OrderBy(pth.TransferNo.Descending);
                pth.es.Top = 1;
                DataTable pthDt = pth.LoadDataTable();
                var transferNo = pthDt.Rows.Count > 0 ? pthDt.Rows[0]["TransferNo"].ToString() : string.Empty;

                //registration
                var reg = new Registration();
                reg.LoadByPrimaryKey(registrationNo);
                reg.SmfID = (item.FindControl("cboSMF") as RadComboBox).SelectedValue;
                reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                //history
                var hist = new PatientTransferHistory();
                bool isHist = false;

                if (hist.LoadByPrimaryKey(reg.RegistrationNo, transferNo))
                {
                    hist.SmfIDBefore = hist.SmfID;
                    hist.SmfID = reg.SmfID;
                    hist.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    hist.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                    isHist = true;
                }
                
                //var unit = new ServiceUnit();
                //unit.LoadByPrimaryKey(chargesHD.str.ToServiceUnitID);

                var cch = new CheckinConfirmHistory();
                cch.AddNew();
                cch.RegistrationNo = reg.RegistrationNo;
                cch.TransferNo = transferNo;
                cch.BedID = reg.BedID;
                cch.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                cch.LastUpdateByUserID = AppSession.UserLogin.UserID;

                using (esTransactionScope trans = new esTransactionScope())
                {
                    #region charges
                    //if (TransChargesItemsDT.Count > 0)
                    //{
                    //    chargesHD.Save();

                    //    // stock calculation
                    //    // charges
                    //    var chargesBalances = new ItemBalanceCollection();
                    //    var chargesDetailBalances = new ItemBalanceDetailCollection();
                    //    var chargesMovements = new ItemMovementCollection();

                    //    string itemNoStock;

                    //    ItemBalance.PrepareItemBalances(TransChargesItemsDT, unit.ServiceUnitID, unit.LocationID, AppSession.UserLogin.UserID,
                    //        true, ref chargesBalances, ref chargesDetailBalances, ref chargesMovements, out itemNoStock);

                    //    if (!string.IsNullOrEmpty(itemNoStock))
                    //        return;

                    //    TransChargesItemsDT.Save();
                    //    TransChargesItemsDTComp.Save();
                    //    CostCalculations.Save();

                    //    if (chargesBalances != null)
                    //        chargesBalances.Save();
                    //    if (chargesDetailBalances != null)
                    //        chargesDetailBalances.Save();
                    //    if (chargesMovements != null)
                    //        chargesMovements.Save();

                    //    // consumption
                    //    var consumptionBalances = new ItemBalanceCollection();
                    //    var consumptionDetailBalances = new ItemBalanceDetailCollection();
                    //    var consumptionMovements = new ItemMovementCollection();

                    //    ItemBalance.PrepareItemBalances(TransChargesItemsDTConsumption, unit.ServiceUnitID, unit.LocationID,
                    //        AppSession.UserLogin.UserID, ref consumptionBalances, ref consumptionDetailBalances, ref consumptionMovements,
                    //        out itemNoStock);

                    //    if (!string.IsNullOrEmpty(itemNoStock))
                    //        return;

                    //    TransChargesItemsDTConsumption.Save();

                    //    if (consumptionBalances != null)
                    //        consumptionBalances.Save();
                    //    if (consumptionDetailBalances != null)
                    //        consumptionDetailBalances.Save();
                    //    if (consumptionMovements != null)
                    //        consumptionMovements.Save();
                    //}
                    #endregion

                    reg.Save();
                    //if (transferNo == string.Empty)
                    //{
                    //    var h = new PatientTransferHistory();
                    //    if (h.LoadByPrimaryKey(reg.RegistrationNo, transferNo))
                    //    {
                    //        h.DateOfEntry = reg.RegistrationDate;
                    //        h.TimeOfEntry = reg.RegistrationTime;
                    //        h.Save();
                    //    }
                    //}
                    if (!string.IsNullOrEmpty(transferNo))
                    {
                        var pt = new PatientTransfer();
                        if (pt.LoadByPrimaryKey(transferNo))
                        {
                            pt.IsValidated = true;
                            pt.ValidatedByUserID = AppSession.UserLogin.UserID;
                            pt.ValidatedDateTime = (new DateTime()).NowAtSqlServer();
                            pt.Save();
                        }
                    }
                    else
                    {
                        if (AppSession.Parameter.IsAutoClosedRegFromOnCheckinConfirmed)
                        {
                            var regColl = new RegistrationCollection();
                            regColl.Query.Where(regColl.Query.RegistrationNo.In(MergeRegistrationList(reg.RegistrationNo)));
                            regColl.LoadAll();
                            if (regColl.Count > 0)
                            {
                                foreach (var r in regColl)
                                {
                                    r.IsClosed = true;
                                }
                                regColl.Save();
                            }
                        }
                    }

                    if (isHist)
                        hist.Save();

                    //update bed status
                    if (reg.IsRoomIn ?? false)
                    {
                        var bedRoomingIn = new BedRoomInCollection();
                        bedRoomingIn.Query.Where(
                            bedRoomingIn.Query.BedID ==
                            Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BedMetadata.ColumnNames.BedID]),
                            bedRoomingIn.Query.RegistrationNo ==
                            Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BedMetadata.ColumnNames.RegistrationNo]));
                        bedRoomingIn.LoadAll();
                        foreach (var x in bedRoomingIn)
                        {
                            x.SRBedStatus = AppSession.Parameter.BedStatusOccupied;
                            x.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            x.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }
                        bedRoomingIn.Save();
                    }
                    else
                    {
                        var bed = new Bed();
                        if (bed.LoadByPrimaryKey(Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][BedMetadata.ColumnNames.BedID])))
                        {
                            bed.SRBedStatus = AppSession.Parameter.BedStatusOccupied;
                            bed.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            bed.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            bed.Save();

                            var bsh = new BedStatusHistory();
                            bsh.AddNew();
                            bsh.BedID = bed.BedID;
                            bsh.SRBedStatusFrom = AppSession.Parameter.BedStatusPending;
                            bsh.SRBedStatusTo = AppSession.Parameter.BedStatusOccupied;
                            bsh.RegistrationNo = bed.RegistrationNo;
                            bsh.TransferNo = transferNo;
                            bsh.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            bsh.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            bsh.Save();
                        }
                    }
                    cch.Save();

                    if (isFileReceived)
                    {
                        var file = new MedicalRecordFileStatusMovement();
                        if (!file.LoadByPrimaryKey(reg.ServiceUnitID, reg.RegistrationNo))
                            file.AddNew();
                        
                        file.RegistrationNo = reg.RegistrationNo;
                        file.LastPositionServiceUnitID = reg.ServiceUnitID;
                        file.LastPositionDateTime = DateTime.Now;
                        file.LastPositionUserID = AppSession.UserLogin.UserID;

                        file.Save();
                    }

                    var bedmanagColl = new BedManagementCollection();
                    bedmanagColl.Query.Where(bedmanagColl.Query.BedID == reg.BedID, 
                        bedmanagColl.Query.RegistrationNo == reg.RegistrationNo,
                        bedmanagColl.Query.IsReleased == false);
                    bedmanagColl.LoadAll();
                    foreach (var bm in bedmanagColl)
                    {
                        bm.IsReleased = true;
                        bm.ReleasedByUserID = AppSession.UserLogin.UserID;
                        bm.ReleasedDateTime = (new DateTime()).NowAtSqlServer();
                    }
                    if (bedmanagColl != null)
                        bedmanagColl.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }

                // AUTOBILL
                if (AppSession.Parameter.IsAutobillIprActivated)
                {
                    // if checkin confirm pertama
                    if (string.IsNullOrEmpty(transferNo)) {
                        // hit autobill
                        var cb = new ChargeBed();
                        cb.ExecuteByRegistrationNo(reg.RegistrationNo);
                        cb.Dispose();
                        var ca = new AutoBillOnSchedule();
                        ca.ExecuteByRegistrationNo(reg.RegistrationNo);
                        ca.Dispose();
                    }
                }

                //rebind data
                grdList.Rebind();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        private DataTable SMF
        {
            get
            {
                if (ViewState["smf"] != null) return ViewState["smf"] as DataTable;

                var smf = new SmfQuery();
                ViewState["smf"] = smf.LoadDataTable();

                return ViewState["smf"] as DataTable;
            }
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdList_ItemPreRender;
        }

        protected void grdList_ItemPreRender(object sender, EventArgs e)
        {
            if (sender is GridDataItem)
            {
                var cbo = ((sender as GridDataItem)["RegistrationNo"].FindControl("cboSMF") as RadComboBox);
                cbo.DataValueField = "SmfID";
                cbo.DataTextField = "SmfName";
                cbo.DataSource = SMF;
                cbo.DataBind();

                cbo.SelectedValue = (sender as GridDataItem)["SmfID"].Text;
            }
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var tooltip = string.Empty;
                var dataItem = e.Item as GridDataItem;
                if (dataItem.OwnerTableView.Name == "master")
                {
                    if (dataItem["ChargeClassID"].Text != dataItem["CoverageClassID"].Text)
                    {
                        // Beri warna merah jika CoverageClassID berbeda dg ChargeClassID Up, 
                        // Beri warna biru jika CoverageClassID berbeda dg ChargeClassID Down, 
                        var classSeq1 = dataItem["ClassSeq1"].Text.ToInt();
                        var classSeq2 = dataItem["ClassSeq2"].Text.ToInt();

                        dataItem.ForeColor = classSeq1 < classSeq2 ? Color.Red : Color.Blue;
                        dataItem.Font.Bold = true;
                        tooltip = "Charge class is different from coverage class.";
                    }
                    if (dataItem["ChargeClassID"].Text != dataItem["ClassID"].Text)
                    {
                        var c = new Class();
                        c.LoadByPrimaryKey(dataItem["ClassID"].Text);
                        if (c.IsTariffClass ?? false)
                        {
                            dataItem.Font.Bold = true;
                            dataItem.Font.Italic = true;
                            tooltip = tooltip == string.Empty ? "Charge class is different from bed class." : "Charge class is different from coverage and bed class.";
                        }
                    }
                    dataItem.ToolTip = tooltip;
                }
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
                grdList.Rebind();

            else if (eventArgument.Contains("|"))
            {
                string msg = string.Empty;
                var param = eventArgument.Split('|');
                var parameter = param[1];

                var transfer = new PatientTransfer();
                transfer.LoadByPrimaryKey(parameter);

                //update bed status
                Bed bedTo = new Bed(), bedFrom = new Bed();
                bedTo.LoadByPrimaryKey(transfer.ToBedID);
                bedFrom.LoadByPrimaryKey(transfer.FromBedID);

                var bed = new Bed();
                bed.LoadByPrimaryKey(transfer.ToBedID);
                if (bed.IsNeedConfirmation == true & transfer.IsValidated == true)
                {
                    if (transfer.IsValidated == true)
                    {
                        msg = "Patient transfer is already validated (check in confirmed). Un-Approval is not allowed.";
                    }
                }
                else
                {
                    //cek apakah ada transfer yg lain setelah itu
                    var pt = new PatientTransferQuery();
                    pt.Select(pt.TransferNo);
                    pt.Where(pt.RegistrationNo == transfer.RegistrationNo, pt.IsVoid == false, pt.TransferNo > transfer.TransferNo);
                    pt.OrderBy(pt.TransferNo.Descending);
                    pt.es.Top = 1;
                    DataTable ptDt = pt.LoadDataTable();
                    var transferNo = ptDt.Rows.Count > 0 ? ptDt.Rows[0]["TransferNo"].ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(transferNo))
                    {
                        msg = "Patient is already transfered with transfer no." + transferNo + ". Void is not allowed.";
                    }
                }
                if (string.IsNullOrEmpty(msg) & !string.IsNullOrEmpty(bedFrom.RegistrationNo))
                {
                    msg = "Bed origin is already registered to another patient. Void is not allowed.";
                }
                if (string.IsNullOrEmpty(msg) & bedFrom.SRBedStatus != AppSession.Parameter.BedStatusUnoccupied)
                {
                    msg = "Bed origin status is not empty. Void is not allowed.";
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }
                else
                {
                    pnlInfo.Visible = false;
                    lblInfo.Text = string.Empty;

                    using (esTransactionScope trans = new esTransactionScope())
                    {
                        //update patient transfer
                        transfer.IsApprove = false;
                        transfer.IsVoid = true;
                        transfer.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        transfer.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        bedTo = new Bed();
                        bedTo.LoadByPrimaryKey(transfer.ToBedID);
                        bedFrom = new Bed();
                        bedFrom.LoadByPrimaryKey(transfer.FromBedID);

                        //update bed status
                        if (!(transfer.IsRoomInTo ?? false))
                        {
                            bedTo.RegistrationNo = string.Empty;
                            bedTo.SRBedStatus = AppSession.Parameter.BedStatusUnoccupied;
                        }
                        bedTo.IsRoomIn = transfer.IsRoomInTo;
                        bedTo.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        bedTo.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        if (!(transfer.IsRoomInFrom ?? false))
                        {
                            bedFrom.RegistrationNo = transfer.RegistrationNo;
                            bedFrom.SRBedStatus = AppSession.Parameter.BedStatusOccupied;

                            bedFrom.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            bedFrom.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                        }

                        //update bed room in
                        var briFColl = new BedRoomInCollection();
                        var briT = new BedRoomIn();

                        if (transfer.IsRoomInFrom ?? false)
                        {
                            var bri = new BedRoomInCollection();
                            bri.Query.Where(bri.Query.BedID == transfer.FromBedID, bri.Query.DateOfExit.IsNull(),
                                            bri.Query.IsVoid == false, bri.Query.RegistrationNo != transfer.RegistrationNo);
                            bri.LoadAll();

                            bedFrom.IsRoomIn = bri.Count > 0;
                        }

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(transfer.RegistrationNo);

                        //update registration
                        reg.ServiceUnitID = transfer.FromServiceUnitID;
                        reg.RoomID = transfer.FromRoomID;
                        reg.BedID = transfer.FromBedID;
                        reg.ClassID = transfer.FromClassID;
                        reg.ChargeClassID = transfer.FromChargeClassID;
                        if (reg.GuarantorID == AppSession.Parameter.SelfGuarantor)
                            reg.CoverageClassID = reg.ChargeClassID;
                        reg.SmfID = transfer.FromSpecialtyID;
                        reg.IsRoomIn = transfer.IsRoomInTo;

                        reg.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        reg.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

                        var thuColl = new PatientTransferHistoryCollection();
                        var thi = new PatientTransferHistory();

                        //update PatientTransferHistory before
                        var thuQuery = new PatientTransferHistoryQuery();
                        thuQuery.Where(thuQuery.RegistrationNo == transfer.RegistrationNo, thuQuery.TransferNo != transfer.TransferNo);
                        thuQuery.es.Top = 1;
                        thuQuery.OrderBy(thuQuery.TransferNo.Descending);
                        thuColl.Load(thuQuery);

                        foreach (var item in thuColl)
                        {
                            item.DateOfExit = null;
                            item.TimeOfExit = null;
                            item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }

                        if (thi.LoadByPrimaryKey(transfer.RegistrationNo, transfer.TransferNo))
                            thi.MarkAsDeleted();

                        //update BedRoomIn before
                        if (transfer.IsRoomInFrom ?? false)
                        {
                            briFColl.Query.Where(briFColl.Query.BedID == transfer.FromBedID, //briFColl.Query.DateOfExit.IsNull(),
                                            briFColl.Query.IsVoid == false, briFColl.Query.RegistrationNo == transfer.RegistrationNo);
                            briFColl.LoadAll();

                            foreach (var item in briFColl)
                            {
                                item.DateOfExit = null;
                                item.TimeOfExit = null;
                                item.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                item.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            }
                        }
                        else
                            briFColl = null;

                        //insert BedRoomIn now
                        if (transfer.IsRoomInTo ?? false)
                        {
                            if (briT.LoadByPrimaryKey(transfer.ToBedID, transfer.RegistrationNo,
                                                      transfer.TransferDate ?? DateTime.Now.Date, transfer.TransferTime))
                                briT.MarkAsDeleted();
                        }
                        else
                            briT = null;

                        //save
                        transfer.Save();
                        if (transfer.ToBedID != transfer.FromBedID)
                            bedFrom.Save();
                        bedTo.Save();
                        reg.Save();
                        thuColl.Save();
                        thi.Save();
                        if (briFColl != null)
                            briFColl.Save();
                        if (briT != null)
                            briT.Save();

                        var bedmanag = new BedManagementCollection();
                        bedmanag.Query.Where(bedmanag.Query.BedID == transfer.ToBedID,
                                             bedmanag.Query.RegistrationNo == transfer.RegistrationNo,
                                             bedmanag.Query.IsReleased == true,
                                             bedmanag.Query.IsVoid == false);
                        bedmanag.LoadAll();
                        foreach (var b in bedmanag)
                        {
                            b.IsReleased = false;
                            b.ReleasedDateTime = null;
                            b.ReleasedByUserID = null;
                            b.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            b.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        }
                        bedmanag.Save();

                        //Commit if success, Rollback if failed
                        trans.Complete();
                    }
                }
                
                grdList.Rebind();
            }
        }

        protected void cboServiceUnitID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboRoomID.Items.Clear();
            cboRoomID.SelectedValue = string.Empty;
            cboRoomID.Text = string.Empty;
        }

        protected void cboRoomID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RoomName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RoomID"].ToString();
        }

        protected void cboRoomID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new ServiceRoomQuery("a");
            var suq = new ServiceUnitQuery("b");
            query.InnerJoin(suq).On(suq.ServiceUnitID == query.ServiceUnitID &&
                                    suq.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            query.Where(query.IsActive == true);
            if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
                query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
            else
            {
                var usr = new AppUserServiceUnitQuery("c");
                query.InnerJoin(usr).On(usr.ServiceUnitID == query.ServiceUnitID && usr.UserID == AppSession.UserLogin.UserID);
            }
            query.Select(query.RoomID, query.RoomName);
            query.OrderBy(query.RoomID.Ascending);
            query.es.Top = 20;
            cboRoomID.DataSource = query.LoadDataTable();
            cboRoomID.DataBind();
        }

        private string[] MergeRegistrationList(string regno)
        {
            var mrgs = new MergeBillingCollection();
            mrgs.Query.Where(mrgs.Query.FromRegistrationNo == regno);
            mrgs.LoadAll();
            if (mrgs.Count == 0)
            {
                var arr = new string[1];
                arr.SetValue(string.Empty, 0);
                return arr;
            }

            return mrgs.Select(m => m.RegistrationNo).ToArray();
        }
    }
}
