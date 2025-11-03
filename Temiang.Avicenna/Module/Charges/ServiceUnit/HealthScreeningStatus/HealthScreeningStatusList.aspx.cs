using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class HealthScreeningStatusList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.HealthScreeningMonitoring;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery("a");

                AppUserServiceUnitQuery qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);

                query.Where(
                    query.SRRegistrationType.In(AppConstant.RegistrationType.MedicalCheckUp),
                    query.IsActive == true
                    );

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                    cboServiceUnitID.SelectedValue = Request.QueryString["cid"];

                //paramedic
                var param = new ParamedicCollection();
                param.Query.Where
                    (
                        param.Query.IsActive == true,
                        param.Query.IsAvailable == true
                    );
                param.Query.OrderBy(param.Query.ParamedicName.Ascending);
                param.LoadAll();

                cboParamedicID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Paramedic entity in param)
                {
                    cboParamedicID.Items.Add(new RadComboBoxItem(entity.ParamedicName, entity.ParamedicID));
                }

                var grr = new GuarantorCollection();
                grr.Query.Where(grr.Query.IsActive == true);
                grr.Query.Load();
                cboGuarantorID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var entity in grr)
                {
                    cboGuarantorID.Items.Add(new RadComboBoxItem(entity.GuarantorName, entity.GuarantorID));
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                var grd = (RadGrid)source;
                grd.DataSource = new String[] { };
                return;
            }

            var dtb = TransChargesOutPatient;
            if (!string.IsNullOrEmpty(Request.QueryString["rt"]) && Request.QueryString["rt"] == AppConstant.RegistrationType.InPatient)
                dtb.Merge(TransChargesInPatient);
            grdList.DataSource = dtb;
        }

        private DataTable TransChargesInPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        sal.ItemName.As("SalutationName")
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == txtRegistrationNo.Text,
                    //        patient.MedicalNo == txtRegistrationNo.Text,
                    //        patient.OldMedicalNo == txtRegistrationNo.Text,
                    //        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(
                          string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsHoldTransactionEntry == false,
                        query.IsVoid == false,
                        query.IsFromDispensary == false
                    );

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );


                DataTable dtb = query.LoadDataTable();

                if (chkIsHasPendingProcess.Checked)
                {
                    foreach (DataRow row in dtb.Rows.Cast<DataRow>().Where(row => ListDetailTable(row["RegistrationNo"].ToString()).Rows.Count == 0))
                    {
                        row.Delete();
                    }
                }

                return dtb;
            }
        }

        private DataTable TransChargesOutPatient
        {
            get
            {
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<1 AS QueNo>",
                        unit.ServiceUnitID,
                        query.ParamedicID,
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        grr.GuarantorName,
                        query.PatientID,
                        query.IsConsul,
                        sal.ItemName.As("SalutationName")
                    );

                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //query.Where(
                    //    query.Or(
                    //        query.RegistrationNo == txtRegistrationNo.Text,
                    //        patient.MedicalNo == txtRegistrationNo.Text,
                    //        patient.OldMedicalNo == txtRegistrationNo.Text,
                    //        string.Format("< OR REPLACE(f.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(f.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where(
                          string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtRegistrationDate.IsEmpty)
                    query.Where(query.RegistrationDate == txtRegistrationDate.SelectedDate.Value.Date);
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                var group = new esQueryItem(query, "Group", esSystemType.String);
                group = unit.ServiceUnitName;

                query.Select(group.As("Group"));
                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.MedicalCheckUp,
                        query.IsHoldTransactionEntry == false,
                        query.IsVoid == false,
                        query.IsFromDispensary == false
                    );

                query.OrderBy
                    (
                        query.ParamedicID.Ascending,
                        query.RegistrationNo.Ascending
                    );

                DataTable dtb = query.LoadDataTable();

                if (chkIsHasPendingProcess.Checked)
                {
                    foreach (DataRow row in dtb.Rows.Cast<DataRow>().Where(row => ListDetailTable(row["RegistrationNo"].ToString()).Rows.Count == 0))
                    {
                        row.Delete();
                    }
                }

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //var query = new TransChargesQuery("a");
            //var reg = new RegistrationQuery("b");
            //var patient = new PatientQuery("c");
            //var unit = new ServiceUnitQuery("d");
            //var tcItem = new TransChargesItemQuery("e");
            //var item = new ItemQuery("f");
            //var tci = new TransChargesItemQuery("g");

            //var cc = new CostCalculationQuery("cc");
            //var tpio = new TransPaymentItemOrderQuery("tpio");


            //tci.Select(tci.TransactionNo, tci.SequenceNo);
            //tci.Where(tci.TransactionNo == tcItem.TransactionNo, tci.SequenceNo == tcItem.SequenceNo,
            //          tci.IsExtraItem == true,
            //          tci.IsSelectedExtraItem == false,
            //          tci.IsVoid == false);

            //query.Select
            //    (
            //        query.TransactionNo,
            //        tcItem.SequenceNo,
            //        query.ReferenceNo,
            //        query.TransactionDate,
            //        unit.ServiceUnitName,
            //        query.RegistrationNo,
            //        patient.MedicalNo,
            //        patient.PatientName,
            //        query.IsAutoBillTransaction,
            //        //query.IsApproved,
            //        "<CAST(CASE e.IsVoid WHEN 1 THEN 0 ELSE a.IsApproved END as BIT) IsApproved>",
            //        tcItem.IsVoid,
            //        query.IsCorrection,
            //        query.IsBillProceed,
            //        item.ItemName,
            //        query.LastUpdateByUserID,
            //        //tcItem.ChargeQuantity,
            //        tcItem.Price,
            //        tcItem.ParamedicCollectionName,
            //        tcItem.CitoAmount,
            //        tcItem.DiscountAmount,
            //        query.FromServiceUnitID,
            //        reg.ParamedicID,
            //        "<CASE WHEN a.IsOrder = 0 THEN a.IsApproved ELSE e.IsOrderRealization END AS IsOrderRealization>",
            //        tcItem.IsExtraItem,
            //        tcItem.IsSelectedExtraItem,
            //        @"<e.ChargeQuantity + ISNULL((SELECT SUM(ChargeQuantity)
            //                                    FROM TransChargesItem 
            //                                    WHERE ReferenceNo = e.TransactionNo
            //                                        AND ReferenceSequenceNo = e.SequenceNo
            //                                        AND IsVoid = 0), 0) AS 'ChargeQuantity'>",
            //        @"<CASE WHEN cc.IntermbillNo IS NULL AND tpio.PaymentNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsPaid>"
            //    );

            //query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            //query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            //query.InnerJoin(tcItem).On(query.TransactionNo == tcItem.TransactionNo);
            //query.InnerJoin(item).On(tcItem.ItemID == item.ItemID);
            //query.InnerJoin(unit).On(query.ToServiceUnitID == unit.ServiceUnitID);

            //query.LeftJoin(cc).On(query.PackageReferenceNo == cc.TransactionNo &&
            //                      tcItem.SequenceNo.Substring(1, 3) == cc.SequenceNo && cc.IntermBillNo.IsNotNull());
            //query.LeftJoin(tpio).On(query.PackageReferenceNo == tpio.TransactionNo &&
            //                        tcItem.SequenceNo.Substring(1, 3) == tpio.SequenceNo &&
            //                        tpio.IsPaymentProceed == true && tpio.IsPaymentReturned == false);

            ////if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            ////    query.Where(unit.ServiceUnitID == cboServiceUnitID.SelectedValue);

            //query.Where
            //    (
            //        query.RegistrationNo == e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString(),
            //        query.IsCorrection == false,
            //        reg.IsClosed == false,
            //        query.Or(
            //            tcItem.ParentNo.IsNull(),
            //            tcItem.ParentNo == string.Empty
            //        ),
            //        query.IsVoid == false,
            //        //tcItem.IsVoid == false,
            //        query.IsAutoBillTransaction == false,
            //        query.IsPackage == false,
            //        //query.PackageReferenceNo.IsNotNull(),
            //        query.NotExists(tci)
            //    );
            //query.OrderBy(query.TransactionNo.Ascending, tcItem.SequenceNo.Ascending);

            //var table = query.LoadDataTable();
            //DataView dv = new DataView(table);
            //dv.RowFilter = "ChargeQuantity > 0";
            //table = dv.ToTable();

            //e.DetailTableView.DataSource = table;
            e.DetailTableView.DataSource = ListDetailTable(e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString());
        }

        private DataTable ListDetailTable(string registrationNo)
        {
            var query = new TransChargesQuery("a");
            var reg = new RegistrationQuery("b");
            var patient = new PatientQuery("c");
            var unit = new ServiceUnitQuery("d");
            var tcItem = new TransChargesItemQuery("e");
            var item = new ItemQuery("f");
            var tci = new TransChargesItemQuery("g");

            var cc = new CostCalculationQuery("cc");
            var tpio = new TransPaymentItemOrderQuery("tpio");


            tci.Select(tci.TransactionNo, tci.SequenceNo);
            tci.Where(tci.TransactionNo == tcItem.TransactionNo, tci.SequenceNo == tcItem.SequenceNo,
                      tci.IsExtraItem == true,
                      tci.IsSelectedExtraItem == false,
                      tci.IsVoid == false);

            query.Select
                (
                    query.TransactionNo,
                    tcItem.SequenceNo,
                    query.ReferenceNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    query.RegistrationNo,
                    patient.MedicalNo,
                    patient.PatientName,
                    query.IsAutoBillTransaction,
                    //query.IsApproved,
                    "<CAST(CASE e.IsVoid WHEN 1 THEN 0 ELSE a.IsApproved END as BIT) IsApproved>",
                    tcItem.IsVoid,
                    query.IsCorrection,
                    query.IsBillProceed,
                    item.ItemName,
                    query.LastUpdateByUserID,
                    //tcItem.ChargeQuantity,
                    tcItem.Price,
                    tcItem.ParamedicCollectionName,
                    tcItem.CitoAmount,
                    tcItem.DiscountAmount,
                    query.FromServiceUnitID,
                    reg.ParamedicID,
                    "<CASE WHEN a.IsOrder = 0 THEN a.IsApproved ELSE e.IsOrderRealization END AS IsOrderRealization>",
                    tcItem.IsExtraItem,
                    tcItem.IsSelectedExtraItem,
                    @"<e.ChargeQuantity + ISNULL((SELECT SUM(ChargeQuantity)
                                                FROM TransChargesItem 
                                                WHERE ReferenceNo = e.TransactionNo
                                                    AND ReferenceSequenceNo = e.SequenceNo
                                                    AND IsVoid = 0), 0) AS 'ChargeQuantity'>",
                    @"<CASE WHEN cc.IntermbillNo IS NULL AND tpio.PaymentNo IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsPaid>"
                );

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
            query.InnerJoin(tcItem).On(query.TransactionNo == tcItem.TransactionNo);
            query.InnerJoin(item).On(tcItem.ItemID == item.ItemID);
            query.InnerJoin(unit).On(query.ToServiceUnitID == unit.ServiceUnitID);

            query.LeftJoin(cc).On(query.PackageReferenceNo == cc.TransactionNo &&
                                  tcItem.SequenceNo.Substring(1, 3) == cc.SequenceNo && cc.IntermBillNo.IsNotNull());
            query.LeftJoin(tpio).On(query.PackageReferenceNo == tpio.TransactionNo &&
                                    tcItem.SequenceNo.Substring(1, 3) == tpio.SequenceNo &&
                                    tpio.IsPaymentProceed == true && tpio.IsPaymentReturned == false);

            //if (!string.IsNullOrEmpty(cboServiceUnitID.SelectedValue))
            //    query.Where(unit.ServiceUnitID == cboServiceUnitID.SelectedValue);

            query.Where
                (
                    query.RegistrationNo == registrationNo,
                    query.IsCorrection == false,
                    reg.IsClosed == false,
                    query.Or(
                        tcItem.ParentNo.IsNull(),
                        tcItem.ParentNo == string.Empty
                    ),
                    query.IsVoid == false,
                    //tcItem.IsVoid == false,
                    query.IsAutoBillTransaction == false,
                    query.IsPackage == false,
                    tcItem.ParentNo == string.Empty,
                    //query.PackageReferenceNo.IsNotNull(),
                    query.NotExists(tci)
                );
            if (chkIsHasPendingProcess.Checked)
            {
                query.Where(query.IsApproved == false, query.IsVoid == false);
            }

            query.OrderBy(query.TransactionNo.Ascending, tcItem.SequenceNo.Ascending);

            var table = query.LoadDataTable();
            DataView dv = new DataView(table);
            dv.RowFilter = "ChargeQuantity > 0";
            table = dv.ToTable();

            return table;
        }

        protected void grdList_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            using (var trans = new esTransactionScope())
            {
                var transactionNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.TransactionNo]);
                var sequenceNo = Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex][TransChargesItemMetadata.ColumnNames.SequenceNo]);
                var isPaid = Convert.ToBoolean(item.OwnerTableView.DataKeyValues[item.ItemIndex]["IsPaid"]);
                
                if (!string.IsNullOrEmpty(transactionNo) && !string.IsNullOrEmpty(sequenceNo))
                {
                    var tc = new TransCharges();
                    tc.LoadByPrimaryKey(transactionNo);

                    var tci = new TransChargesItem();
                    tci.LoadByPrimaryKey(transactionNo, sequenceNo);
                    if (tci.IsVoid == false && isPaid == false)
                    {
                        if (AppSession.Parameter.IsReducePriceWhenDeletingMcuPackageDetails)
                        {
                            decimal? discount = 0;

                            var tcip = new TransChargesItem();
                            if (tcip.LoadByPrimaryKey(tc.PackageReferenceNo, sequenceNo.Substring(0, 3)))
                            {
                                var package = new ItemPackage();
                                package.Query.Where(package.Query.ItemID == tcip.ItemID && package.Query.DetailItemID == tci.ItemID);
                                if (package.Query.Load())
                                {
                                    var comp = new ItemPackageTariffComponent();
                                    comp.Query.Select(comp.Query.Price.Sum(), comp.Query.Discount.Sum());
                                    comp.Query.Where(comp.Query.ItemID == tcip.ItemID && comp.Query.DetailItemID == tci.ItemID);
                                    if (comp.Query.Load())
                                    {
                                        //discount = (comp.Price - ((package.IsDiscountInPercent ?? false) ? (((package.DiscountValue ?? 0) / 100) * comp.Price) : (package.DiscountValue ?? 0)));
                                        //discount = comp.Discount; remark by deby
                                        discount = comp.Price - comp.Discount;
                                        tcip.DiscountAmount += discount;
                                        tcip.LastUpdateDateTime = DateTime.Now;
                                        tcip.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                        tcip.Save();
                                    }
                                }

                                var tcicp = new TransChargesItemComp();
                                if (tcicp.LoadByPrimaryKey(tc.PackageReferenceNo, sequenceNo.Substring(0, 3), AppSession.Parameter.TariffComponentJasaSaranaID))
                                {
                                    tcicp.DiscountAmount += discount;
                                    tcicp.LastUpdateDateTime = DateTime.Now;
                                    tcicp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    tcicp.Save();
                                }

                                var ccp = new CostCalculation();
                                ccp.Query.Where(ccp.Query.TransactionNo == tc.PackageReferenceNo, ccp.Query.SequenceNo == sequenceNo.Substring(0, 3));
                                if (ccp.Query.Load())
                                {
                                    //ccp.PatientAmount = ccp.PatientAmount == 0 ? 0 : (tcip.ChargeQuantity * tcip.Price) - discount; remark by deby
                                    //ccp.GuarantorAmount = ccp.GuarantorAmount == 0 ? 0 : (tcip.ChargeQuantity * tcip.Price) - discount; remark by deby
                                    ccp.PatientAmount = ccp.PatientAmount == 0 ? 0 : ccp.PatientAmount - discount;
                                    ccp.GuarantorAmount = ccp.GuarantorAmount == 0 ? 0 : ccp.GuarantorAmount - discount;
                                    ccp.DiscountAmount += discount;
                                    ccp.LastUpdateDateTime = DateTime.Now;
                                    ccp.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    ccp.Save();
                                }
                            }
                        }

                        tci = new TransChargesItem();
                        if (tci.LoadByPrimaryKey(transactionNo, sequenceNo))
                        {
                            tci.IsApprove = false;
                            tci.IsBillProceed = false;
                            tci.IsVoid = true;
                            tci.VoidByUserID = AppSession.UserLogin.UserID;
                            tci.VoidDateTime = (new DateTime()).NowAtSqlServer();
                            tci.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            tci.LastUpdateDateTime = DateTime.Now;
                            //tci.MarkAsDeleted();
                            tci.Save();
                        }

                        var cc = new CostCalculation();
                        cc.Query.Where(cc.Query.TransactionNo == transactionNo, cc.Query.SequenceNo == sequenceNo);
                        if (cc.Query.Load())
                        {
                            cc.MarkAsDeleted();
                            cc.Save();
                        }

                        var tcis = new TransChargesItemCollection();
                        tcis.Query.Where(tcis.Query.TransactionNo == transactionNo, tcis.Query.IsVoid == false);
                        if (tcis.Query.Load())
                        {
                            if (tcis.Count == 0)
                            {
                                tc = new TransCharges();
                                if (tc.LoadByPrimaryKey(transactionNo))
                                {
                                    tc.IsApproved = false;
                                    tc.IsBillProceed = false;
                                    tc.IsVoid = true;
                                    tc.VoidByUserID = AppSession.UserLogin.UserID;
                                    tc.VoidDateTime = (new DateTime()).NowAtSqlServer();
                                    tc.LastUpdateByUserID = AppSession.UserLogin.UserID;
                                    tc.LastUpdateDateTime = DateTime.Now;
                                    //tc.MarkAsDeleted();
                                    tc.Save();
                                }
                            }
                        }

                        trans.Complete();
                    }
                }
            }
        }

        protected void tmrLab_Tick(object sender, EventArgs e)
        {
            grdList.Rebind();
        }
    }
}
