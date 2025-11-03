using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Collections.Generic;
using System.Web;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionUddList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            //ProgramID = Request.QueryString["rt"] == "opr"
            //    ? AppConstant.Program.PrescriptionUddOpr
            //    : AppConstant.Program.PrescriptionUddIpr;

            ProgramID = AppConstant.Program.PrescriptionUddIpr;

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery();

                //if (Request.QueryString["rt"] == "ipr")
                //    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                //else if (Request.QueryString["rt"] == "opr")
                //{
                //    query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient,
                //                                            AppConstant.RegistrationType.OutPatient,
                //                                            AppConstant.RegistrationType.MedicalCheckUp));
                //}
                //else
                //{
                //    query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient,
                //                                            AppConstant.RegistrationType.OutPatient,
                //                                            AppConstant.RegistrationType.InPatient,
                //                                            AppConstant.RegistrationType.MedicalCheckUp));
                //}
                query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                query.Where(query.IsActive == true);

                query.OrderBy(unit.Query.ServiceUnitName.Ascending);
                unit.Load(query);

                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in unit)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

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

                //cboStatus.Items.Add(new RadComboBoxItem("", ""));
                //cboStatus.Items.Add(new RadComboBoxItem("Proceed - Yes", "1"));
                //cboStatus.Items.Add(new RadComboBoxItem("Proceed - No", "2"));

                //pnlRegDate.Visible = (Request.QueryString["type"] == "sales");

                //if (Request.QueryString["rt"] == "opr")
                //    txtRegistrationDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                txtPrescriptionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                //trPrescriptionStatus.Visible = (Request.QueryString["rt"] == "opr");
                //trPrescriptionSRFloor.Visible = false; //(Request.QueryString["rt"] == "opr");
                //StandardReference.InitializeIncludeSpace(cboPrescriptionSRFloor, AppEnum.StandardReference.Floor);

                grdPrescription.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.InPatient);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            // Dispensary
            ComboBox.PopulateWithServiceUnitForTransaction(cboDispensaryID, Temiang.Avicenna.BusinessObject.Reference.TransactionCode.Prescription, true);
            // Remove for not in parameter
            var serviceUnitIdListForUdd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitIdListForUdd);
            if (string.IsNullOrEmpty(serviceUnitIdListForUdd))
                serviceUnitIdListForUdd = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitPharmacyID);

            if (!string.IsNullOrEmpty(serviceUnitIdListForUdd))
            {
                serviceUnitIdListForUdd = String.Concat(serviceUnitIdListForUdd, ";");
                var itemSelecteds = new List<RadComboBoxItem>();
                foreach (RadComboBoxItem item in cboDispensaryID.Items)
                {
                    if (!string.IsNullOrEmpty(item.Value) && serviceUnitIdListForUdd.Contains(String.Concat(item.Value, ";")))
                        itemSelecteds.Add(item);
                }

                cboDispensaryID.Items.Clear();
                cboDispensaryID.Items.AddRange(itemSelecteds);

                ComboBox.SelectedValue(cboDispensaryID, AppSession.Parameter.ServiceUnitPharmacyID);
            }

            RestoreValueFromCookie();
        }

        protected void grdUddItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            //if (e.CommandName == "PrintPatientSticker")
            //{
            //    var jobParameters = new PrintJobParameterCollection();

            //    var jobParameter = jobParameters.AddNew();
            //    jobParameter.Name = "p_RegistrationNo";
            //    jobParameter.ValueString = e.CommandArgument.ToString();

            //    AppSession.PrintJobParameters = jobParameters;
            //    AppSession.PrintJobReportID = AppConstant.Report.RegistrationLabel;
            //    var SuPrintLabelPatientID = AppSession.Parameter.AppProgramServiceUnitPatientLabel;
            //    if (!string.IsNullOrEmpty(SuPrintLabelPatientID)) AppSession.PrintJobReportID = SuPrintLabelPatientID;

            //    string script = @"openRpt();";
            //    RadAjaxPanel1.ResponseScripts.Add(script);
            //}
        }

        protected void grdUddItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                grdUddItem.DataSource = PatientUddPending();
            }
        }

        private DataTable PatientUddPending()
        {
            //var dtb = new DataTable();
            //if (Request.QueryString["rt"] == "ipr")
            //{
            //    dtb = PatientUddPendingInPatient;
            //}
            //else if (Request.QueryString["rt"] == "opr")
            //{
            //    dtb = PatientUddPendingNonInPatient;
            //}
            //else
            //{
            //    dtb = PatientUddPendingInPatient;
            //    dtb.Merge(PatientUddPendingNonInPatient);
            //}

            //return dtb;

            return PatientUddPendingInPatient; // Hanya pasien rawat inap
        }

        //private DataTable PatientUddPendingNonInPatient
        //{
        //    get
        //    {
        //        var query = new RegistrationQuery("a");
        //        var pat = new PatientQuery("b");
        //        var unit = new ServiceUnitQuery("c");
        //        var room = new ServiceRoomQuery("d");
        //        var par = new ParamedicQuery("e");
        //        var grr = new GuarantorQuery("f");
        //        var sal = new AppStandardReferenceItemQuery("g");
        //        var sumInfo = new RegistrationInfoSumaryQuery("h");

        //        query.InnerJoin(pat).On(pat.PatientID == query.PatientID);
        //        query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);
        //        query.LeftJoin(room).On(room.RoomID == query.RoomID);
        //        query.LeftJoin(par).On(par.ParamedicID == query.ParamedicID);
        //        query.InnerJoin(grr).On(grr.GuarantorID == query.GuarantorID);
        //        query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == pat.SRSalutation);
        //        query.LeftJoin(sumInfo).On(sumInfo.RegistrationNo == query.RegistrationNo);

        //        query.es.Distinct = true;
        //        query.es.Top = 100;

        //        query.Select(

        //            query.RegistrationNo,
        //            query.RegistrationDate,
        //            query.RegistrationTime,
        //            query.PatientID,
        //            pat.MedicalNo,
        //            pat.PatientName,
        //            pat.Sex,
        //            sal.ItemName.As("SalutationName"),

        //            query.ServiceUnitID,
        //            unit.ServiceUnitName,
        //            room.RoomName,
        //            query.BedID,

        //            query.ParamedicID,
        //            par.ParamedicName,

        //            grr.GuarantorName,
        //            @"<CAST(1 AS BIT) AS IsCheckinConfirmed>",
        //            @"<CASE WHEN a.SRDischargeMethod = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarged>",
        //            @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
        //            @"<CAST(0 AS BIT) AS IsDebtAvailable>"
        //            );

        //        query.Where
        //            (
        //                query.SRRegistrationType != AppConstant.RegistrationType.InPatient,
        //                query.IsClosed == false,
        //                query.IsHoldTransactionEntry == false
        //            );

        //        if (!txtRegistrationDate.IsEmpty)
        //            query.Where(query.RegistrationDate >= txtRegistrationDate.SelectedDate, query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
        //        if (cboServiceUnitID.SelectedValue != string.Empty)
        //            query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
        //        if (cboParamedicID.SelectedValue != string.Empty)
        //            query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
        //        if (txtRegistrationNo.Text != string.Empty)
        //        {
        //            string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
        //            query.Where(
        //                query.Or(
        //                    string.Format("<a.RegistrationNo = '{0}' OR >", searchReg),
        //                    string.Format("<b.MedicalNo = '{0}' OR >", searchReg),
        //                    string.Format("<b.OldMedicalNo = '{0}'>", searchReg),
        //                    string.Format("< OR REPLACE(b.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
        //                    string.Format("< OR REPLACE(b.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
        //                    )
        //                );
        //        }
        //        if (txtPatientName.Text != string.Empty)
        //        {
        //            string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
        //            query.Where
        //                (
        //                  string.Format("<LTRIM(RTRIM(LTRIM(b.FirstName + ' ' + b.MiddleName)) + ' ' + b.LastName) LIKE '{0}'>", searchPatient)
        //                );
        //        }

        //        //Filter Patient yg memiliki UDD

        //        var mainLocId = "xx"; //Harus dipilih dispensari nya
        //        if (!string.IsNullOrEmpty(cboDispensaryID.SelectedValue))
        //        {
        //            mainLocId = (new ServiceUnit()).GetMainLocationId(cboDispensaryID.SelectedValue);
        //        }

        //        var uddItem = new UddItemQuery("udd");
        //        uddItem.Select(uddItem.RegistrationNo);
        //        uddItem.Where(uddItem.RegistrationNo == query.RegistrationNo, uddItem.IsStop == false, uddItem.LocationID == mainLocId);
        //        uddItem.es.Top = 1;
        //        query.Where(query.RegistrationNo.In(uddItem));

        //        //filter pasien yg udd-nya sudah diproses per hari ini
        //        var uddTx = new TransPrescriptionQuery("tx");
        //        uddTx.Select(uddTx.RegistrationNo);
        //        uddTx.Where(uddTx.PrescriptionDate.Date() == DateTime.Now.Date,
        //            uddTx.RegistrationNo == query.RegistrationNo,
        //            uddTx.IsUnitDosePrescription == true, uddTx.LocationID == mainLocId);
        //        if (AppSession.Parameter.IsFilterPrescUddListOnlyWithValidTx)
        //            uddTx.Where(uddTx.IsVoid == false);



        //        uddTx.es.Top = 1;
        //        query.Where(query.RegistrationNo.NotIn(uddTx));

        //        query.OrderBy
        //            (
        //                query.RegistrationDate.Descending, query.RegistrationTime.Descending,
        //                query.ServiceUnitID.Ascending,
        //                query.ParamedicID.Ascending
        //            );

        //        DataTable dtbl = query.LoadDataTable();
        //        dtbl.Columns.Add("DispSuId", typeof(string));
        //        dtbl.Columns.Add("DispLocId", typeof(string));

        //        var dispSuId = cboDispensaryID.SelectedValue;
        //        foreach (DataRow row in dtbl.Rows)
        //        {
        //            row["DispSuId"] = dispSuId;
        //            row["DispLocId"] = mainLocId;
        //        }

        //        foreach (DataRow row in dtbl.Rows)
        //        {
        //            if (Convert.ToBoolean(row["IsDischarged"]) && AppSession.Parameter.IsDisplayExecutionDateOnPrescriptionSales)
        //                row["IsCheckinConfirmed"] = true;
        //            else
        //            {
        //                if (AppSession.Parameter.IsPatientIprOnPrescSalesForCheckinConfirmedOnly)
        //                {
        //                    var bed = new Bed();
        //                    if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
        //                    {
        //                        if (bed.IsNeedConfirmation == true & bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
        //                        {
        //                            row["IsCheckinConfirmed"] = false;
        //                        }
        //                    }
        //                }
        //            }

        //        }
        //        dtbl.AcceptChanges();

        //        return dtbl;
        //    }
        //}

        private DataTable PatientUddPendingInPatient
        {
            get
            {
                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("b");
                var unit = new ServiceUnitQuery("c");
                var room = new ServiceRoomQuery("d");
                var grr = new GuarantorQuery("f");
                var sal = new AppStandardReferenceItemQuery("g");
                var sumInfo = new RegistrationInfoSumaryQuery("h");

                reg.InnerJoin(pat).On(pat.PatientID == reg.PatientID);
                reg.InnerJoin(unit).On(unit.ServiceUnitID == reg.ServiceUnitID);
                reg.LeftJoin(room).On(room.RoomID == reg.RoomID);
                reg.InnerJoin(grr).On(grr.GuarantorID == reg.GuarantorID);
                reg.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & sal.ItemID == pat.SRSalutation);
                reg.LeftJoin(sumInfo).On(sumInfo.RegistrationNo == reg.RegistrationNo);

                var udi = new UddItemQuery("udi");
                reg.InnerJoin(udi).On(reg.RegistrationNo == udi.RegistrationNo);

                var par = new ParamedicQuery("e");
                reg.InnerJoin(par).On(par.ParamedicID == udi.ParamedicID);

                var bed = new BedQuery("bd");
                reg.InnerJoin(bed).On(bed.RegistrationNo == reg.RegistrationNo); // Tambah join untuk filter Patient yg masih exist di Bed krn masih banyak patient yg belum di Discharge (Handono 2025-Mar-10)

                reg.es.Distinct = true;
                reg.es.Top = 300;

                reg.Select(
                    reg.RegistrationNo,
                    reg.RegistrationDate,
                    reg.RegistrationTime,
                    reg.PatientID,
                    pat.MedicalNo,
                    pat.PatientName,
                    pat.Sex,
                    sal.ItemName.As("SalutationName"),
                    udi.ParamedicID,
                    par.ParamedicName,
                    reg.ServiceUnitID,
                    unit.ServiceUnitName,
                    room.RoomName,
                    reg.BedID,
                    grr.GuarantorName,
                    udi.StartDateTime,
                    "<COALESCE(udi.StartDateTime,udi.LastUpdateDateTime) AS UddStartDateTime>",
                    "<CAST(1 AS BIT) AS IsCheckinConfirmed>",
                    "<CASE WHEN reg.SRDischargeMethod = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarged>",
                    "<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                    "<CAST(0 AS BIT) AS IsDebtAvailable>",
                    "<CAST(CASE WHEN COALESCE(udi.StartDateTime,udi.LastUpdateDateTime)>'" + DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd") + @"' THEN (SELECT COUNT(1) FROM TransPrescription AS tp
INNER JOIN TransPrescriptionItem AS tpi ON tpi.PrescriptionNo = tp.PrescriptionNo
WHERE tp.RegistrationNo = reg.RegistrationNo
                    AND tp.IsUnitDosePrescription = 1
                    AND tpi.ItemID = udi.ItemID
                    AND tpi.SRConsumeMethod = udi.SRConsumeMethod
                    AND tpi.ConsumeQty = udi.ConsumeQty
                    AND tpi.SRConsumeUnit = udi.SRConsumeUnit" + (AppSession.Parameter.IsFilterPrescUddListOnlyWithValidTx ? " AND tp.IsVoid=0" : String.Empty) + ") ELSE 1 END AS BIT) as IsNotNew>"
                    );

                reg.Where
                    (
                        reg.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        reg.Or(reg.DischargeDate.IsNull(), reg.SRDischargeMethod == string.Empty),
                        reg.IsClosed == false,
                        reg.IsHoldTransactionEntry == false
                    );

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    reg.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    reg.Where(reg.ParamedicID == cboParamedicID.SelectedValue);

                if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text) || !string.IsNullOrWhiteSpace(txtPatientName.Text))
                {
                    Helper.AddFilterPatientId(reg, pat, txtRegistrationNo.Text, txtPatientName.Text);
                }

                //Filter lokasi
                var mainLocId = "xx"; //Harus dipilih dispensari nya
                if (!string.IsNullOrEmpty(cboDispensaryID.SelectedValue))
                {
                    mainLocId = (new ServiceUnit()).GetMainLocationId(cboDispensaryID.SelectedValue);
                }

                reg.Where(udi.LocationID == mainLocId, udi.IsStop != true);


                // Outstanding UddItem Item current date
                var tmpDate = DateTime.Now.Date.AddDays(-1);
                var afterDate = new DateTime(tmpDate.Year, tmpDate.Month, tmpDate.Day, 23, 59, 59);
                var beforeDate = DateTime.Now.Date.AddDays(1);
                var tp = new TransPrescriptionQuery("tp");
                var tpi = new TransPrescriptionItemQuery("tpi");
                tp.Select(tpi.ItemID);
                tp.es.Top = 1;
                tp.InnerJoin(tpi).On(tp.PrescriptionNo == tpi.PrescriptionNo);
                tp.Where(tp.RegistrationNo == reg.RegistrationNo,
                    tp.PrescriptionDate > afterDate, tp.PrescriptionDate < beforeDate,
                    tp.IsUnitDosePrescription == true,
                    tpi.ItemID == udi.ItemID,
                    tpi.SRConsumeMethod == udi.SRConsumeMethod,
                    tpi.ConsumeQty == udi.ConsumeQty,
                    tpi.SRConsumeUnit == udi.SRConsumeUnit
                    );
                if (AppSession.Parameter.IsFilterPrescUddListOnlyWithValidTx)
                    tp.Where(tp.IsVoid != true);

                reg.Where(udi.ItemID.NotIn(tp));

                // Sort untuk keperluan penghapusan row duplikat
                reg.OrderBy(reg.RegistrationNo.Ascending, udi.ParamedicID.Ascending,udi.StartDateTime.Descending);

                var dtbl = reg.LoadDataTable();

                // Hapus record double karena ada penambahan field "<COALESCE(udi.StartDateTime,udi.LastUpdateDateTime) AS UddStartDateTime>"
                var regNo = string.Empty;
                var parId = string.Empty;
                foreach (DataRow row in dtbl.Rows)
                {
                    if (regNo.Equals(row["RegistrationNo"]) && parId.Equals(row["ParamedicID"]))
                    {
                        row.Delete();
                    }
                    else
                    {
                        regNo = row["RegistrationNo"].ToString();
                        parId = row["ParamedicID"].ToString();
                    }
                }
                dtbl.AcceptChanges();


                // Sort by UddStartDateTime Desc
                if (dtbl.Rows.Count > 0)
                {
                    dtbl = dtbl.AsEnumerable()
                       .OrderByDescending(r => r.Field<DateTime>("UddStartDateTime"))
                       .CopyToDataTable();

                    // Add Columns
                    dtbl.Columns.Add("DispSuId", typeof(string));
                    dtbl.Columns.Add("DispLocId", typeof(string));

                    var dispSuId = cboDispensaryID.SelectedValue;
                    foreach (DataRow row in dtbl.Rows)
                    {
                        row["DispSuId"] = dispSuId;
                        row["DispLocId"] = mainLocId;
                    }
                }
                return dtbl;
            }
        }

        protected void grdPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                var grd = (RadGrid)source;
                grd.DataSource = new String[] { };
                return;
            }

            if (!e.IsFromDetailTable)
            {
                grdPrescription.DataSource = TransPrescriptions;

                if ((Request.QueryString["type"] != "sales"))
                    grdPrescription.MasterTableView.GroupsDefaultExpanded = true;
            }
        }

        private DataTable TransPrescriptions
        {
            get
            {
                var presc = new TransPrescriptionQuery("a");
                var unit = new ServiceUnitQuery("b");
                var room = new ServiceRoomQuery("c");
                var medic = new ParamedicQuery("d");
                var query = new RegistrationQuery("e");
                var patient = new PatientQuery("f");
                var grr = new GuarantorQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");
                var disp = new ServiceUnitQuery("disp");
                var casemixguar = new CasemixCoveredGuarantorQuery("casemixg");

                query.es.Distinct = true;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        "<0 AS QueNo>",
                        medic.ParamedicName,
                        query.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        patient.Sex,
                        query.BedID,
                        unit.ServiceUnitName,
                        query.ServiceUnitID,
                        query.ParamedicID,
                        query.PatientID,
                        grr.GuarantorName,
                        query.BedID,

                        presc.PrescriptionNo,
                        presc.PrescriptionDate,
                        presc.OrderNo,
                        presc.IsBillProceed,
                        presc.IsApproval,
                        presc.IsVoid,
                        "<CAST(0 AS BIT) AS IsPaid>",
                        presc.IsProceedByPharmacist,
                        presc.IsPrinted,
                        @"<'True' AS Soape>",
                        presc.Note,
                        presc.CompleteDateTime,
                        presc.DeliverDateTime,
                        "<0 AS Status>",
                        presc.ApprovalDateTime,
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(1 AS BIT) AS IsCheckinConfirmed>",
                        presc.KioskQueueNo,
                        disp.ServiceUnitName.As("DispensaryName"),
                        presc.ServiceUnitID.As("DispSuId"),
                        presc.LocationID.As("DispLocId"),
                        @"<CASE WHEN casemixg.GuarantorID IS NULL THEN CAST(0 AS BIT) ElSE CAST(1 AS BIT) END AS IsGuarantorBpjsCasemix>"
                    );

                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.InnerJoin(presc).On(query.RegistrationNo == presc.RegistrationNo &
                                          presc.IsPrescriptionReturn == false);
                query.LeftJoin(medic).On(presc.ParamedicID == medic.ParamedicID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.InnerJoin(disp).On(disp.ServiceUnitID == presc.ServiceUnitID);
                query.LeftJoin(casemixguar).On(casemixguar.GuarantorID == query.GuarantorID);

                if (grdPrescription.Columns.FindByUniqueName("NeedValidationByCasemix").Visible)
                    query.Select(@"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) AS x FROM TransPrescriptionItem AS tpi
                            WHERE tpi.PrescriptionNo = a.PrescriptionNo AND ISNULL(tpi.IsCasemixApproved, 0) = 0 AND tpi.CasemixApprovedDateTime IS NULL), CAST(0 AS BIT)) AS 'IsNeedValidationByCasemix'>");
                else
                    query.Select(@"<CAST(0 AS BIT) AS IsNeedValidationByCasemix>");

                query.Where(presc.IsUnitDosePrescription == true, presc.IsFromSOAP == false, presc.Or(presc.IsPos.IsNull(), presc.IsPos == false));

                bool IsFilterMaxResultRecord = true;
                if (!string.IsNullOrEmpty(cboDispensaryID.SelectedValue))
                {
                    query.Where(presc.ServiceUnitID == cboDispensaryID.SelectedValue);
                    IsFilterMaxResultRecord = false;
                }
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                    query.Where(
                        query.Or(
                            query.RegistrationNo == searchReg,
                            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                            )
                        );

                    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                            patient.FullName.Like(searchPatient)
                            //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (!txtPrescriptionDate.IsEmpty)
                {
                    query.Where(presc.PrescriptionDate >= txtPrescriptionDate.SelectedDate, presc.PrescriptionDate < txtPrescriptionDate.SelectedDate.Value.AddDays(1));
                    IsFilterMaxResultRecord = false;
                }
                if (txtPrescriptionNo.Text != string.Empty)
                    query.Where(presc.PrescriptionNo == Helper.EscapeQuery(txtPrescriptionNo.Text));

                //if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                //{
                //    if (cboStatus.SelectedValue == "1")
                //        query.Where(presc.IsProceedByPharmacist == true);
                //    else
                //        query.Where(query.Or(presc.IsProceedByPharmacist.IsNull(), presc.IsProceedByPharmacist == false));
                //    IsFilterMaxResultRecord = false;
                //}

                if (!string.IsNullOrEmpty(cboDeliveryStatus.SelectedValue))
                {
                    switch (cboDeliveryStatus.SelectedValue)
                    {
                        case "1":
                            {
                                query.Where(presc.CompleteDateTime.IsNull());
                                break;
                            }
                        case "2":
                            {
                                query.Where(presc.CompleteDateTime.IsNotNull(), presc.DeliverDateTime.IsNull());
                                break;
                            }
                        case "3":
                            {
                                query.Where(presc.DeliverDateTime.IsNotNull());
                                break;
                            }
                    }
                    IsFilterMaxResultRecord = false;
                }

                if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
                {
                    query.Where(presc.ServiceUnitID == Request.QueryString["unit"]);
                    if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
                    {
                        query.Where(presc.LocationID == Request.QueryString["loc"]);
                    }
                }
                else
                {
                    //if (Request.QueryString["rt"] == "opr")
                    //    query.Where(query.SRRegistrationType != AppConstant.RegistrationType.InPatient);
                    //else
                    //{
                    //    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                    //}

                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                }

                //if (!string.IsNullOrEmpty(cboPrescriptionSRFloor.SelectedValue))
                //    query.Where(presc.SRFloor == cboPrescriptionSRFloor.SelectedValue);

                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    if (txtPrescriptionDate.SelectedDate.HasValue)
                    {
                        query.Where(query.Or(
                        presc.PrescriptionNo == txtBarcode.Text,
                        query.And(
                            presc.KioskQueueNo == txtBarcode.Text,
                            presc.PrescriptionDate == txtPrescriptionDate.SelectedDate
                            )
                        ));
                    }
                    else
                    {
                        query.Where(presc.PrescriptionNo == txtBarcode.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtKioskQueueNo.Text))
                    query.Where(presc.KioskQueueNo == txtKioskQueueNo.Text);

                if (rbPrescriptionStatus.SelectedValue == "1")
                    query.Where(presc.IsApproval == false, presc.IsVoid == false);
                else if (rbPrescriptionStatus.SelectedValue == "2")
                    query.Where(presc.IsApproval == true, presc.IsVoid == false);
                else if (rbPrescriptionStatus.SelectedValue == "3")
                    query.Where(presc.IsVoid == true);

                if (IsFilterMaxResultRecord)
                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.OrderBy(presc.PrescriptionNo.Descending);

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    row["Status"] = !(row["DeliverDateTime"] is DBNull) ? 3 :
                        (!(row["CompleteDateTime"] is DBNull) ? 2 : (!(row["IsProceedByPharmacist"] is DBNull) ? 1 : 0));

                    //if (Request.QueryString["rt"] == "opr")
                    //{
                    //    var tpio = new TransPaymentItemOrderCollection();
                    //    tpio.Query.Where(tpio.Query.TransactionNo == row["PrescriptionNo"],
                    //                     tpio.Query.IsPaymentProceed == true, tpio.Query.IsPaymentReturned == false);
                    //    tpio.LoadAll();
                    //    if (tpio.Count > 0)
                    //        row["IsPaid"] = true;
                    //}
                }
                dtbl.AcceptChanges();

                return dtbl;
            }
        }

        protected void grdPrescription_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemI = new ItemQuery("c");

            var emb = new EmbalaceQuery("x");
            var cons = new ConsumeMethodQuery("y");

            query.Select
                (
                    query,
                    qItem.ItemName.As("ItemName"),
                    qItemI.ItemName.As("ItemInterventionName"),
                    "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                    emb.EmbalaceLabel.As("EmbalaceLabel"),
                    cons.SRConsumeMethodName.As("SRConsumeMethodName")
                );
            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.LeftJoin(qItemI).On(query.ItemInterventionID == qItemI.ItemID);
            query.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);
            query.LeftJoin(cons).On(query.SRConsumeMethod == cons.SRConsumeMethod);
            query.Where(query.PrescriptionNo == e.DetailTableView.ParentItem.GetDataKeyValue("PrescriptionNo").ToString());
            query.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        protected string GetItemName(object isRFlag, object itemName)
        {
            if (isRFlag.Equals(true))
                return itemName.ToString();
            else
                return "&nbsp;&nbsp;&nbsp;" + itemName.ToString();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdUddItem.Rebind();
            grdPrescription.Rebind();
        }

        protected void grdPrescription_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Profile")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.RSCH_REKAP_RESEP_PASIEN;

                string script = @"var oWnd = $find('" + winHistory.ClientID + "');" +
                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                "oWnd.Show();" +
                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "setStatus")
            {
                var cmd = e.CommandArgument.ToString().Split('|');
                var pres = new TransPrescription();
                if (pres.LoadByPrimaryKey(cmd[0]) && cmd.Length > 1)
                {
                    if (pres.IsApproval ?? false)
                    {
                        switch (cmd[1])
                        {
                            case "complete":
                                {
                                    pres.CompleteDateTime = (new DateTime()).NowAtSqlServer();
                                    pres.Save();
                                    grdPrescription.Rebind();
                                    break;
                                }
                            case "deliver":
                                {
                                    pres.DeliverDateTime = (new DateTime()).NowAtSqlServer();
                                    pres.Save();
                                    grdPrescription.Rebind();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        // show error not yet approved
                    }
                }
            }
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            switch (rbBarcodeMode.SelectedValue)
            {
                case "3":
                case "4":
                    {
                        var tpColl = new TransPrescriptionCollection();
                        tpColl.Query.Where(tpColl.Query.PrescriptionNo == txtBarcode.Text);
                        if (!tpColl.LoadAll())
                        {
                            if (txtPrescriptionDate.SelectedDate.HasValue)
                            {
                                tpColl.QueryReset();
                                tpColl.Query.Where(tpColl.Query.KioskQueueNo == txtBarcode.Text,
                                    tpColl.Query.PrescriptionDate == txtPrescriptionDate.SelectedDate)
                                    .OrderBy(tpColl.Query.PrescriptionDate.Descending);
                                if (!tpColl.LoadAll())
                                {

                                }
                            }
                        }
                        if (tpColl.Any())
                        {
                            var pres = tpColl.First();
                            switch (rbBarcodeMode.SelectedValue)
                            {
                                case "3":
                                    {
                                        if (!pres.CompleteDateTime.HasValue)
                                        {
                                            pres.CompleteDateTime = (new DateTime()).NowAtSqlServer();
                                            tpColl.Save();
                                        }
                                        break;
                                    }
                                case "4":
                                    {
                                        if (!pres.DeliverDateTime.HasValue)
                                        {
                                            pres.DeliverDateTime = (new DateTime()).NowAtSqlServer();
                                            tpColl.Save();
                                        }
                                        break;
                                    }
                            }
                        }
                        break;
                    }
            }

            grdPrescription.Rebind();
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }
    }
}
