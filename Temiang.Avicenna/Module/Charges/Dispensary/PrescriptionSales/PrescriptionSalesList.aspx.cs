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
using System.Configuration;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PrescriptionSalesList : BasePage
    {
        /*
         * QueryString :
         *      rt      : opr = RJ, [empty] = RI
         *      type    : sales = Sales handling, [empty] = Order handling
         *      unit    : [serviceunitid], [empty] = All
         *      loc     : [locationid], [empty] = All
         */
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            if (Request.QueryString["rt"] == "opr")
                ProgramID = Request.QueryString["type"] == "sales" ? AppConstant.Program.PrescriptionSalesOpr : AppConstant.Program.PrescriptionRealizationOpr;
            else
                ProgramID = Request.QueryString["type"] == "sales" ? AppConstant.Program.PrescriptionSales : AppConstant.Program.PrescriptionRealization;

            if (!IsPostBack)
            {
                //toolbar
                if (AppSession.Parameter.IsAllowDirectPrescOnInpatientSalesHandling)
                {
                    RadToolBar2.Visible = (Request.QueryString["type"] == "sales");
                    RadToolBar2.Items[1].Visible = (Request.QueryString["rt"] == "opr") && AppSession.Parameter.IsVisibleOtc;
                }
                else
                {
                    RadToolBar2.Visible = (Request.QueryString["type"] == "sales") && (Request.QueryString["rt"] == "opr");
                    RadToolBar2.Items[1].Visible = AppSession.Parameter.IsVisibleOtc;
                }

                if (Helper.IsApotekOnlineIntegration)
                {
                    txtPPK.Text = ConfigurationManager.AppSettings["ApotekHospitalID"];
                    txtTglAwal.SelectedDate = (new DateTime()).NowAtSqlServer();
                    txtTglAkhir.SelectedDate = (new DateTime()).NowAtSqlServer();
                    trBpjsApol.Visible = true;
                    TabPrescApol.Visible = true;
                }

                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery();

                if (Request.QueryString["rt"] == "ipr")
                    query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                else if (Request.QueryString["rt"] == "opr")
                {
                    query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient,
                                                            AppConstant.RegistrationType.OutPatient,
                                                            AppConstant.RegistrationType.MedicalCheckUp));
                }
                else
                {
                    query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.EmergencyPatient,
                                                            AppConstant.RegistrationType.OutPatient,
                                                            AppConstant.RegistrationType.InPatient,
                                                            AppConstant.RegistrationType.MedicalCheckUp));
                }
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

                cboStatus.Items.Add(new RadComboBoxItem("", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Proceed - Yes", "1"));
                cboStatus.Items.Add(new RadComboBoxItem("Proceed - No", "2"));

                pnlRegDate.Visible = (Request.QueryString["type"] == "sales");

                if (Request.QueryString["rt"] == "opr")
                    txtRegistrationDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                pnlOrderDate.Visible = (Request.QueryString["type"] != "sales");
                txtOrderDate.SelectedDate = (new DateTime()).NowAtSqlServer();
                txtPrescriptionDate.SelectedDate = (new DateTime()).NowAtSqlServer();

                grdRegistration.MasterTableView.Columns[0].Visible = (Request.QueryString["type"] == "sales");
                grdRegistration.MasterTableView.Columns[16].Visible = AppSession.Parameter.IsShowPrintLabelOnTransEntry;
                grdRegistration.MasterTableView.Columns[grdRegistration.MasterTableView.Columns.Count - 1].Visible = (Request.QueryString["type"] == "sales");

                grdRegistration.Columns[0].Visible = Request.QueryString["rt"] == "ipr" || !AppSession.Parameter.IsPrescSalesOpNeedSoape;
                grdRegistration.Columns[1].Visible = Request.QueryString["rt"] == "opr" && AppSession.Parameter.IsPrescSalesOpNeedSoape;
                grdRegistration.Columns.FindByUniqueName("Delivery").Visible = AppSession.Parameter.IsPrescriptionPendingDelivery;

                grdPrescription.Columns.FindByUniqueName("OrderNo").Visible = false;//(Request.QueryString["rt"] == "ipr") && AppSession.Parameter.IsUsingPrescriptionOrder; //order no
                grdPrescription.Columns.FindByUniqueName("Note").Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSBHP"; //notes

                grdPrescription.Columns.FindByUniqueName("IsPaid").Visible = (Request.QueryString["rt"] == "opr"); //paid
                grdPrescription.Columns.FindByUniqueName("IsProceedByPharmacist").Visible = (Request.QueryString["rt"] == "opr"); //proceed
                grdPrescription.Columns.FindByUniqueName("Profile").Visible = (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && AppSession.Parameter.HealthcareInitialAppsVersion != "RSMM"); //profile

                grdPrescription.Columns.FindByUniqueName("IsDirect").Visible = (Request.QueryString["rt"] == "opr" || AppSession.Parameter.IsAllowDirectPrescOnInpatientSalesHandling);

                //grdPrescription.Columns[4].Visible = false; //(Request.QueryString["rt"] == "ipr") && AppSession.Parameter.IsUsingPrescriptionOrder; //order no
                //grdPrescription.Columns[11].Visible = AppSession.Parameter.HealthcareInitialAppsVersion == "RSBHP"; //notes
                //grdPrescription.Columns[16].Visible = (Request.QueryString["rt"] == "opr"); //paid
                //grdPrescription.Columns[17].Visible = (Request.QueryString["rt"] == "opr"); //proceed
                //grdPrescription.Columns[grdPrescription.Columns.Count - 1].Visible = (AppSession.Parameter.HealthcareInitialAppsVersion != "RSCH" && AppSession.Parameter.HealthcareInitialAppsVersion != "RSMM"); //profile

                trPrescriptionStatus.Visible = (Request.QueryString["rt"] == "opr");
                trPrescriptionSRFloor.Visible = false; //(Request.QueryString["rt"] == "opr");
                StandardReference.InitializeIncludeSpace(cboPrescriptionSRFloor, AppEnum.StandardReference.Floor);

                PopulateRegistrationType();
                if (Request.QueryString["rt"] == "opr")
                    cboRegistrationType.SelectedValue = string.Empty;
                else
                {
                    cboRegistrationType.SelectedValue = AppConstant.RegistrationType.InPatient;
                    trRegistrationType.Visible = false;
                }

                PopulateNumberPatient();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack) return;

            RestoreValueFromCookie();

            ComboBox.PopulateWithServiceUnitForTransaction(cboDispensaryID, TransactionCode.Prescription, true);
        }

        protected void grdRegistration_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PrintPatientSticker")
            {
                var jobParameters = new PrintJobParameterCollection();

                var jobParameter = jobParameters.AddNew();
                jobParameter.Name = "p_RegistrationNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.RegistrationLabel;
                var SuPrintLabelPatientID = AppSession.Parameter.AppProgramServiceUnitPatientLabel;
                if (!string.IsNullOrEmpty(SuPrintLabelPatientID)) AppSession.PrintJobReportID = SuPrintLabelPatientID;

                string script = @"openRpt();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
        }

        protected void grdRegistration_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                var grd = (RadGrid)source;
                grd.DataSource = new String[] { };
                return;
            }

            if (!e.IsFromDetailTable)
            {
                grdRegistration.DataSource = TransCharges;

                if ((Request.QueryString["type"] != "sales"))
                    grdRegistration.MasterTableView.GroupsDefaultExpanded = true;
            }
        }

        protected void grdRegistration_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var regno = e.DetailTableView.ParentItem.GetDataKeyValue("RegistrationNo").ToString();
            var isCheckinConfirmed = Convert.ToBoolean(e.DetailTableView.ParentItem.GetDataKeyValue("IsCheckinConfirmed"));

            var query = new TransPrescriptionQuery("a");

            var qServ = new ServiceUnitQuery("s");
            query.InnerJoin(qServ).On(query.ServiceUnitID == qServ.ServiceUnitID);

            var qPar = new ParamedicQuery("m");
            query.LeftJoin(qPar).On(query.ParamedicID == qPar.ParamedicID);

            query.Select
                (
                    query.PrescriptionNo,
                    query.PrescriptionDate,
                    qServ.ServiceUnitName,
                    qPar.ParamedicName,
                    query.IsApproval,
                    query.IsVoid,
                    query.IsBillProceed,
                    query.RegistrationNo,
                    @"<CAST(1 AS BIT) AS IsCheckinConfirmed>",
                    query.IsDirect,
                    @"<CASE WHEN a.IsDirect = 1 AND a.ParamedicID = '' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsOtc'>"
                );

            query.Where
                (
                    query.RegistrationNo == regno,
                    query.IsPrescriptionReturn == false,
                    query.IsFromSOAP == !(Request.QueryString["type"] == "sales"),
                    query.IsUnitDosePrescription == false
                );

            query.OrderBy(query.PrescriptionNo.Descending);

            DataTable dtb = query.LoadDataTable();

            if (!isCheckinConfirmed)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    row["IsCheckinConfirmed"] = false;
                }
                dtb.AcceptChanges();
            }

            e.DetailTableView.DataSource = dtb;
        }

        private DataTable TransCharges
        {
            get
            {
                var dtb = new DataTable();
                if (Request.QueryString["rt"] == "ipr")
                {
                    dtb = TransChargesInPatient;
                }
                else if (Request.QueryString["rt"] == "opr")
                {
                    dtb = TransChargesOutPatient;
                }
                else
                {
                    dtb = TransChargesInPatient;
                    dtb.Merge(TransChargesOutPatient);
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
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        query.RegistrationTime,
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
                        "<'' AS BedID>",
                        string.Format("<CASE WHEN g.GuarantorID IN ({0}) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBpjsPatient>", GuarantorAskesIDList),
                        @"<'True' AS Soape>",
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(0 AS BIT) AS IsDebtAvailable>",
                        query.DischargeDate,
                        @"<CAST(1 AS BIT) AS IsCheckinConfirmed>",
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        query.SRRegistrationType,
                        "<CAST(0 AS BIT) AS IsHasPendingDelivery>"
                    );

                if (Request.QueryString["type"] != "sales")
                {
                    var soap = new VwTransPrescriptionFromSOAPQuery("s");
                    query.InnerJoin(soap).On(query.RegistrationNo == soap.RegistrationNo);
                    if (!txtOrderDate.IsEmpty)
                        query.Where(soap.PrescriptionDate >= txtOrderDate.SelectedDate, soap.PrescriptionDate < txtOrderDate.SelectedDate.Value.AddDays(1));
                }
                if (Request.QueryString["type"] == "sales")
                {
                    if (!txtRegistrationDate.IsEmpty)
                        query.Where(query.RegistrationDate >= txtRegistrationDate.SelectedDate, query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                }

                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(gdc).On(query.GuarantorID == gdc.GuarantorID & query.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (AppSession.Parameter.IsPatientOprOnPrescSalesForPolyclinicOnly)
                {
                    var tcode = new ServiceUnitTransactionCodeQuery("tcode");
                    query.LeftJoin(tcode).On(tcode.ServiceUnitID == query.ServiceUnitID && tcode.SRTransactionCode == TransactionCode.JobOrder);
                    query.Where(tcode.SRTransactionCode.IsNull());
                }

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);


                //if (txtRegistrationNo.Text != string.Empty)
                //{
                //    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                //    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                //    query.Where(
                //        query.Or(
                //            query.RegistrationNo == searchReg,
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );

                //    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                //}

                //if (txtPatientName.Text != string.Empty)
                //{
                //    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                //    query.Where
                //        (
                //            patient.FullName.Like(searchPatient)
                //        //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                //        );
                //}

                if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text) || !string.IsNullOrWhiteSpace(txtPatientName.Text))
                {
                    Helper.AddFilterPatientId(query, patient, txtRegistrationNo.Text, txtPatientName.Text);
                }

                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                    query.Where(query.SRRegistrationType == cboRegistrationType.SelectedValue);

                query.Where
                    (
                    query.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                    query.IsClosed == false,
                    query.IsHoldTransactionEntry == false,
                    query.IsNonPatient == false,
                    query.IsFromDispensary == false
                    );

                query.OrderBy
                    (
                        query.RegistrationDate.Descending, query.RegistrationTime.Descending,
                        query.ServiceUnitID.Ascending,
                        query.ParamedicID.Ascending
                    );

                DataTable dtbl = query.LoadDataTable();

                if (AppSession.Parameter.IsPrescSalesOpNeedSoape || AppSession.Parameter.IsPrescriptionPendingDelivery)
                {
                    foreach (DataRow row in dtbl.Rows)
                    {
                        if (row["SRRegistrationType"].ToString() == AppConstant.RegistrationType.OutPatient)
                        {
                            // From table EpisodeSOAPE
                            var soapColl = new EpisodeSOAPECollection();
                            soapColl.Query.Where(
                                soapColl.Query.RegistrationNo == row["RegistrationNo"].ToString() &&
                                soapColl.Query.IsVoid == false,
                                soapColl.Query.Or(soapColl.Query.Imported.IsNull(), soapColl.Query.Imported == false)
                                );
                            soapColl.LoadAll();

                            //From Table RegistrationInfoMedic
                            var rimColl = new RegistrationInfoMedicCollection();
                            rimColl.Query.Where(
                                rimColl.Query.RegistrationNo == row["RegistrationNo"].ToString(),
                                rimColl.Query.Or(rimColl.Query.IsDeleted.IsNull(), rimColl.Query.IsDeleted == false)
                                );
                            rimColl.LoadAll();

                            if (soapColl.Count == 0 && rimColl.Count == 0)
                                row["Soape"] = "False";
                        }

                        if (AppSession.Parameter.IsPrescriptionPendingDelivery && row["IsBpjsPatient"].ToBoolean() == true)
                        {
                            var tpi = new TransPrescriptionItemQuery("a");
                            var tp = new TransPrescriptionQuery("b");
                            tpi.InnerJoin(tp).On(tpi.PrescriptionNo == tp.PrescriptionNo);
                            tpi.Where(tp.RegistrationNo == row["RegistrationNo"].ToString(), tp.IsApproval == true, tpi.IsApprove == true, tpi.IsPendingDelivery == true);
                            tpi.Where("<a.[ResultQty] > ISNULL(a.[DeliveryQty], 0)>");
                            DataTable tpiDtb = tpi.LoadDataTable();
                            if (tpiDtb.Rows.Count > 0)
                                row["IsHasPendingDelivery"] = true;
                        }
                    }
                    dtbl.AcceptChanges();
                }

                return dtbl;

            }
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
                var sumInfo = new RegistrationInfoSumaryQuery("h");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                query.es.Distinct = true;
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select
                    (
                        room.RoomName,
                        query.RegistrationDate,
                        query.RegistrationTime,
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
                        string.Format("<CASE WHEN g.GuarantorID IN ({0}) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBpjsPatient>", GuarantorAskesIDList),
                        @"<'True' AS Soape>",
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(0 AS BIT) AS IsDebtAvailable>",
                        query.DischargeDate,
                        @"<CAST(1 AS BIT) AS IsCheckinConfirmed>",
                        @"<CASE WHEN e.DischargeDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarged>",
                        @"<CASE WHEN h.NoteCount <= 0 THEN NULL ELSE h.NoteCount END AS NoteCount>",
                        @"<CASE WHEN dc.LineNumber IS NULL OR (dc.LineNumber - h.DocumentCheckListCount) <= 0 THEN NULL ELSE (dc.LineNumber - h.DocumentCheckListCount) END AS DocumentCheckListCountRemains>",
                        query.SRRegistrationType,
                        "<CAST(0 AS BIT) AS IsHasPendingDelivery>"
                    );

                if (Request.QueryString["type"] != "sales")
                {
                    var soap = new VwTransPrescriptionFromSOAPQuery("s");
                    query.InnerJoin(soap).On(query.RegistrationNo == soap.RegistrationNo);
                    if (!txtOrderDate.IsEmpty)
                        query.Where(soap.PrescriptionDate >= txtOrderDate.SelectedDate, soap.PrescriptionDate < txtOrderDate.SelectedDate.Value.AddDays(1));
                }

                if (Request.QueryString["type"] == "sales")
                {
                    if (!txtRegistrationDate.IsEmpty)
                        query.Where(query.RegistrationDate >= txtRegistrationDate.SelectedDate, query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                }

                query.InnerJoin(unit).On(query.ServiceUnitID == unit.ServiceUnitID);
                query.LeftJoin(room).On(query.RoomID == room.RoomID);
                query.LeftJoin(medic).On(query.ParamedicID == medic.ParamedicID);
                query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
                query.InnerJoin(grr).On(query.GuarantorID == grr.GuarantorID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                query.LeftJoin(sumInfo).On(query.RegistrationNo == sumInfo.RegistrationNo);
                query.LeftJoin(gdc).On(query.GuarantorID == gdc.GuarantorID & query.SRRegistrationType == gdc.SRRegistrationType);
                query.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (cboServiceUnitID.SelectedValue != string.Empty)
                    query.Where(query.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    query.Where(query.ParamedicID == cboParamedicID.SelectedValue);
                //if (txtRegistrationNo.Text != string.Empty)
                //{
                //    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                //    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                //    query.Where(
                //        query.Or(
                //            query.RegistrationNo == searchReg,
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );

                //    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                //}
                //if (txtPatientName.Text != string.Empty)
                //{
                //    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                //    query.Where
                //        (
                //            patient.FullName.Like(searchPatient)
                //        //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                //        );
                //}

                if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text) || !string.IsNullOrWhiteSpace(txtPatientName.Text))
                {
                    Helper.AddFilterPatientId(query, patient, txtRegistrationNo.Text, txtPatientName.Text);
                }

                query.Where
                    (
                        query.IsClosed == false,
                        query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                        query.IsHoldTransactionEntry == false
                    );

                query.OrderBy
                    (
                        query.RegistrationDate.Descending, query.RegistrationTime.Descending,
                        query.ServiceUnitID.Ascending,
                        query.ParamedicID.Ascending
                    );

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    //if (Convert.ToBoolean(row["IsDischarged"]) && AppSession.Parameter.IsDisplayExecutionDateOnPrescriptionSales)
                    //    row["IsCheckinConfirmed"] = true;
                    //else
                    //{
                    if (AppSession.Parameter.IsPatientIprOnPrescSalesForCheckinConfirmedOnly)
                    {
                        var bed = new Bed();
                        if (bed.LoadByPrimaryKey(row["BedID"].ToString()))
                        {
                            if (bed.IsNeedConfirmation == true & bed.RegistrationNo == row["RegistrationNo"].ToString() & bed.SRBedStatus == AppSession.Parameter.BedStatusPending)
                            {
                                row["IsCheckinConfirmed"] = false;
                            }
                        }
                    }
                    //}
                    if (AppSession.Parameter.IsPrescriptionPendingDelivery && row["IsBpjsPatient"].ToBoolean() == true)
                    {
                        var tpi = new TransPrescriptionItemQuery("a");
                        var tp = new TransPrescriptionQuery("b");
                        tpi.InnerJoin(tp).On(tpi.PrescriptionNo == tp.PrescriptionNo);
                        tpi.Where(tp.RegistrationNo == row["RegistrationNo"].ToString(), tp.IsApproval == true, tpi.IsApprove == true, tpi.IsPendingDelivery == true);
                        tpi.Where("<a.[ResultQty] > ISNULL(a.[DeliveryQty], 0)>");
                        DataTable tpiDtb = tpi.LoadDataTable();
                        if (tpiDtb.Rows.Count > 0)
                            row["IsHasPendingDelivery"] = true;
                    }
                }
                dtbl.AcceptChanges();

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
                        string.Format("<CASE WHEN e.GuarantorID IN ({0}) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBpjsPatient>", GuarantorAskesIDList),
                        "<CAST(0 AS BIT) AS IsHasPendingDelivery>",
                        presc.IsDirect,
                        @"<CASE WHEN a.IsDirect = 1 AND a.ParamedicID = '' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'IsOtc'>"
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

                if (Request.QueryString["type"] != "sales")
                {
                    var soap = new VwTransPrescriptionFromSOAPQuery("s");
                    query.InnerJoin(soap).On(query.RegistrationNo == soap.RegistrationNo);
                    if (!txtOrderDate.IsEmpty)
                        query.Where(soap.PrescriptionDate >= txtOrderDate.SelectedDate, soap.PrescriptionDate < txtOrderDate.SelectedDate.Value.AddDays(1));
                    query.Where(presc.IsFromSOAP == true);
                }
                else
                    query.Where(presc.IsFromSOAP == false);
                query.Where(presc.IsUnitDosePrescription == false, presc.Or(presc.IsPos.IsNull(), presc.IsPos == false));

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
                //if (txtRegistrationNo.Text != string.Empty)
                //{
                //    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                //    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                //    query.Where(
                //        query.Or(
                //            query.RegistrationNo == searchReg,
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );

                //    //Helper.AddFilterMedNoOrRegNoOrPatName(query, patient, searchReg, "registration");
                //}

                //if (txtPatientName.Text != string.Empty)
                //{
                //    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                //    query.Where
                //        (
                //            patient.FullName.Like(searchPatient)
                //        //string.Format("<LTRIM(RTRIM(LTRIM(f.FirstName + ' ' + f.MiddleName)) + ' ' + f.LastName) LIKE '{0}'>", searchPatient)
                //        );
                //}

                if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text) || !string.IsNullOrWhiteSpace(txtPatientName.Text))
                {
                    Helper.AddFilterPatientId(query, patient, txtRegistrationNo.Text, txtPatientName.Text);
                }

                if (!txtPrescriptionDate.IsEmpty)
                {
                    query.Where(presc.PrescriptionDate >= txtPrescriptionDate.SelectedDate, presc.PrescriptionDate < txtPrescriptionDate.SelectedDate.Value.AddDays(1));
                    IsFilterMaxResultRecord = false;
                }
                if (txtPrescriptionNo.Text != string.Empty)
                    query.Where(presc.PrescriptionNo == Helper.EscapeQuery(txtPrescriptionNo.Text));
                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "1")
                        query.Where(presc.IsProceedByPharmacist == true);
                    else
                        query.Where(query.Or(presc.IsProceedByPharmacist.IsNull(), presc.IsProceedByPharmacist == false));
                    IsFilterMaxResultRecord = false;
                }

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
                    if (Request.QueryString["rt"] == "opr")
                        query.Where(query.SRRegistrationType != AppConstant.RegistrationType.InPatient);
                    else
                    {
                        query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                    }
                }

                if (!string.IsNullOrEmpty(cboPrescriptionSRFloor.SelectedValue))
                    query.Where(presc.SRFloor == cboPrescriptionSRFloor.SelectedValue);

                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    if (txtPrescriptionDate.SelectedDate.HasValue)
                    {
                        query.Where(query.Or(
                        presc.PrescriptionNo == txtBarcode.Text,
                        presc.KioskQueueNo == txtBarcode.Text,
                        patient.MedicalNo == txtBarcode.Text
                        ));

                        query.Where(presc.IsVoid == false);
                    }
                    else
                    {
                        presc.Where(presc.PrescriptionNo == txtBarcode.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtKioskQueueNo.Text))
                    query.Where(presc.KioskQueueNo == txtKioskQueueNo.Text);

                if (IsFilterMaxResultRecord)
                    query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.OrderBy(presc.PrescriptionNo.Descending);

                DataTable dtbl = query.LoadDataTable();

                foreach (DataRow row in dtbl.Rows)
                {
                    row["Status"] = !(row["DeliverDateTime"] is DBNull) ? 3 :
                        (!(row["CompleteDateTime"] is DBNull) ? 2 : (!(row["IsProceedByPharmacist"] is DBNull) ? 1 : 0));

                    if (Request.QueryString["rt"] == "opr")
                    {
                        var tpio = new TransPaymentItemOrderCollection();
                        tpio.Query.Where(tpio.Query.TransactionNo == row["PrescriptionNo"],
                                         tpio.Query.IsPaymentProceed == true, tpio.Query.IsPaymentReturned == false);
                        tpio.LoadAll();
                        if (tpio.Count > 0)
                            row["IsPaid"] = true;
                    }

                    if (AppSession.Parameter.IsPrescriptionPendingDelivery && row["IsBpjsPatient"].ToBoolean() == true)
                    {
                        var tpi = new TransPrescriptionItemQuery("a");
                        tpi.Where(tpi.PrescriptionNo == row["PrescriptionNo"].ToString(), tpi.IsApprove == true, tpi.IsPendingDelivery == true);
                        tpi.Where("<a.[ResultQty] > ISNULL(a.[DeliveryQty], 0)>");
                        DataTable tpiDtb = tpi.LoadDataTable();
                        if (tpiDtb.Rows.Count > 0)
                            row["IsHasPendingDelivery"] = true;
                    }
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
            PopulateNumberPatient();
            //SaveValueToCookie();

            grdRegistration.Rebind();
            grdPrescription.Rebind();
        }

        //private void PopulateNumberPatient()
        //{
        //    var coll = new RegistrationCollection();
        //    if (Request.QueryString["rt"] == "opr")
        //    {
        //        if (txtRegistrationDate.IsEmpty)
        //        {
        //            lblRegistrationCount.Text = string.Format("{0}", 0);
        //            return;
        //        }

        //        coll.Query.Where(coll.Query.RegistrationDate >= txtRegistrationDate.SelectedDate, coll.Query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
        //        if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
        //            coll.Query.Where(coll.Query.SRRegistrationType == cboRegistrationType.SelectedValue);
        //        else
        //            coll.Query.Where(coll.Query.SRRegistrationType != AppConstant.RegistrationType.InPatient);
        //    }
        //    else
        //    {
        //        if (!txtRegistrationDate.IsEmpty)
        //        {
        //            coll.Query.Where(coll.Query.RegistrationDate >= txtRegistrationDate.SelectedDate, coll.Query.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
        //        }
        //        coll.Query.Where(coll.Query.SRRegistrationType == AppConstant.RegistrationType.InPatient,
        //            coll.Query.Or(coll.Query.DischargeDate.IsNull(), coll.Query.SRDischargeMethod == string.Empty));
        //    }
        //    coll.Query.Where(coll.Query.IsVoid == false,
        //            coll.Query.IsNonPatient == false,
        //            coll.Query.IsFromDispensary == false,
        //            coll.Query.IsDirectPrescriptionReturn == false);

        //    coll.LoadAll();

        //    lblRegistrationCount.Text = string.Format("{0}", coll.Count);
        //}

        private void PopulateNumberPatient()
        {
            // Optimize query (Handono 2025-03-10)
            var qr = new RegistrationQuery("r");
            if (Request.QueryString["rt"] == "opr")
            {
                if (txtRegistrationDate.IsEmpty)
                {
                    lblRegistrationCount.Text = string.Format("{0}", 0);
                    return;
                }

                qr.Where(qr.RegistrationDate >= txtRegistrationDate.SelectedDate, qr.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboRegistrationType.SelectedValue))
                    qr.Where(qr.SRRegistrationType == cboRegistrationType.SelectedValue);
                else
                    qr.Where(qr.SRRegistrationType != AppConstant.RegistrationType.InPatient);
            }
            else
            {
                if (!txtRegistrationDate.IsEmpty)
                {
                    qr.Where(qr.RegistrationDate >= txtRegistrationDate.SelectedDate, qr.RegistrationDate < txtRegistrationDate.SelectedDate.Value.AddDays(1));
                }
                qr.Where(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    qr.Or(qr.DischargeDate.IsNull(), qr.SRDischargeMethod == string.Empty));
            }
            qr.Where(qr.IsVoid == false,
                    qr.IsNonPatient == false,
                    qr.IsFromDispensary == false,
                    qr.IsDirectPrescriptionReturn == false);

            qr.Select(qr.RegistrationNo.Count().As("RegCount"));
            var dtb = qr.LoadDataTable();
            if (dtb.Rows.Count > 0)
                lblRegistrationCount.Text = string.Format("{0}", dtb.Rows[0][0]);
            else
                lblRegistrationCount.Text = "0";
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

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
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

        private string GuarantorAskesIDList
        {
            get
            {
                if (ViewState["GuarantorAskesIDList"] != null) return ViewState["GuarantorAskesIDList"].ToString();
                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSMP")
                {
                    var grr = new GuarantorCollection();
                    grr.Query.Where(grr.Query.GuarantorID.NotIn(AppSession.Parameter.SelfGuarantor));
                    grr.Query.Load();

                    var str = string.Empty;
                    foreach (var list in grr)
                    {
                        str += "'" + list.GuarantorID + "',";
                    }
                    str = str.Remove(str.Length - 1);
                    ViewState["GuarantorAskesIDList"] = str;
                    return str;
                }
                else
                {
                    var grr = new GuarantorCollection();
                    grr.Query.Where(grr.Query.GuarantorID.In(AppSession.Parameter.GuarantorAskesID));
                    grr.Query.Load();

                    var str = string.Empty;
                    foreach (var list in grr)
                    {
                        str += "'" + list.GuarantorID + "',";
                    }
                    str = str.Remove(str.Length - 1);
                    ViewState["GuarantorAskesIDList"] = str;
                    return str;

                    //ViewState["GuarantorAskesIDList"] = "''";
                    //return "''";
                }
            }
        }

        protected void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            lblBarcodeMsg.InnerText = "";
            switch (rbBarcodeMode.SelectedValue)
            {
                case "3":
                case "4":
                    {
                        var dtb = TransPrescriptions;

                        if (dtb.Rows.Count == 1)
                        {
                            var pres = new TransPrescription();
                            pres.LoadByPrimaryKey(dtb.Rows[0]["PrescriptionNo"].ToString());
                            switch (rbBarcodeMode.SelectedValue)
                            {
                                case "3":
                                    {
                                        if (!pres.CompleteDateTime.HasValue)
                                        {
                                            pres.CompleteDateTime = (new DateTime()).NowAtSqlServer();
                                            pres.Save();
                                        }
                                        break;
                                    }
                                case "4":
                                    {
                                        if (!pres.CompleteDateTime.HasValue)
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                                                string.Format("alert('set complete must be done!!');"), true);
                                        }
                                        else if (!pres.DeliverDateTime.HasValue)
                                        {
                                            pres.DeliverDateTime = (new DateTime()).NowAtSqlServer();
                                            pres.Save();
                                        }
                                        break;
                                    }
                            }
                        }
                        else if (dtb.Rows.Count > 1)
                        {
                            // more than one result, do the action manually
                            lblBarcodeMsg.InnerText = "Barcode search returns more than one result, please proceed manually";
                        }
                        break;
                    }
                default:
                    //txtRegistrationNo.Text = txtBarcode.Text;
                    break;
            }

            grdPrescription.Rebind();
            txtBarcode.Text = "";
            txtBarcode.Focus();
        }

        protected void cboRegistrationType_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateRegistrationType();
        }

        private void PopulateRegistrationType()
        {
            var query = new AppStandardReferenceItemQuery();
            query.Select(query.ItemID, query.ItemName);
            query.Where
            (
                query.StandardReferenceID == "RegistrationType",
                query.IsActive == true
            );
            if (Request.QueryString["rt"] == "opr")
                query.Where(query.ItemID != AppConstant.RegistrationType.InPatient);
            else
                query.Where(query.ItemID == AppConstant.RegistrationType.InPatient);

            query.OrderBy(query.ItemName.Descending);
            DataTable dtb = query.LoadDataTable();

            if (Request.QueryString["rt"] == "opr")
            {
                var r = dtb.NewRow();
                r["ItemName"] = string.Empty; r["ItemID"] = string.Empty;
                dtb.Rows.InsertAt(r, 0);
                dtb.AcceptChanges();
            }

            cboRegistrationType.DataSource = dtb;
            cboRegistrationType.DataBind();
        }

        protected void cboRegistrationType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.StandardReferenceItemDataBound(e);
        }

        #region apol
        private DataTable BpjsApol
        {
            get
            {
                var cached = this.Session[SessionNameForList] as DataTable;
                if (cached != null) return cached;

                BpjsApolQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (BpjsApolQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new BpjsApolQuery("a");
                    var sep = new BpjsSEPQuery("b");

                    query.es.Distinct = true;
                    query.LeftJoin(sep).On(query.REFASALSJP == sep.NoSEP);

                    query.Select(
                        query.NORESEP.As("NORESEP"),
                        query.REFASALSJP.As("NOSEP_KUNJUNGAN"),
                        sep.NomorKartu.As("NOKARTU"),
                        sep.NamaPasien.As("PesertaNama"),
                        query.TGLRSP.As("TGLRESEP"),
                        query.TGLPELRSP.As("TGLPELRSP"),
                        query.KDJNSOBAT.As("KDJNSOBAT")
                    );

                    query.OrderBy(query.NORESEP.Descending);
                }

                var dtb = query.LoadDataTable();

                string[] needed = { "NOAPOTIK", "TGLENTRY", "BYTAGRSP", "BYVERRSP", "FASKESASAL" };
                foreach (var col in needed)
                {
                    if (!dtb.Columns.Contains(col)) dtb.Columns.Add(col, typeof(string));
                }

                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }


        protected void grdListApol_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Helper.IsApotekOnlineIntegration)
            {
                grdListApol.DataSource = BpjsApol;
            }
        }


        protected void grdListApol_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;
            var noResep = Convert.ToString(item.GetDataKeyValue("NORESEP"))?.Trim();
            var noSEP = Convert.ToString(item.GetDataKeyValue("NOSEP_KUNJUNGAN"))?.Trim();

            if (string.IsNullOrWhiteSpace(noResep))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('NoResep is not found.');", true);
                return;
            }

            try
            {
                var svc = new Common.BPJS.Apotek.Service();
                var response = svc.HapusResep(new Common.BPJS.Apotek.Resep.HapusResep.Request.Root
                {
                    Request = new Common.BPJS.Apotek.Resep.HapusResep.Request
                    {
                        NOSJP = noSEP,
                        REFASALSJP = noSEP,
                        NORESEP = noResep
                    }
                });

                if (response?.Metadata?.IsValid == true)
                {
                    var dt = Session[SessionNameForList] as DataTable;
                    if (dt != null)
                    {
                        var rows = dt.Select($"NORESEP = '{noResep.Replace("'", "''")}'");
                        foreach (var r in rows) dt.Rows.Remove(r);
                        dt.AcceptChanges();
                        Session[SessionNameForList] = dt;
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "success", "alert('Resep deleted successfully.');", true);
                    grdListApol.Rebind();
                }
                else
                {
                    var msg = response?.Metadata?.Message ?? "Unknown error.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "error", $"alert('Error: {msg}');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "exception", $"alert('An error occurred: {ex.Message}');", true);
            }
        }

        private DataTable BuildApolDataTableForGrid()
        {
            var dt = new DataTable();
            dt.Columns.Add("NORESEP", typeof(string));
            dt.Columns.Add("NOAPOTIK", typeof(string));
            dt.Columns.Add("NOSEP_KUNJUNGAN", typeof(string));
            dt.Columns.Add("NOKARTU", typeof(string));
            dt.Columns.Add("PesertaNama", typeof(string));
            dt.Columns.Add("TGLENTRY", typeof(DateTime));
            dt.Columns.Add("TGLRESEP", typeof(DateTime));
            dt.Columns.Add("TGLPELRSP", typeof(DateTime));
            dt.Columns.Add("BYTAGRSP", typeof(decimal));
            dt.Columns.Add("BYVERRSP", typeof(decimal));
            dt.Columns.Add("KDJNSOBAT", typeof(string));
            dt.Columns.Add("FASKESASAL", typeof(string));
            return dt;
        }

        private static DateTime? ParseDate(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            string[] fmts = { "yyyy-MM-dd", "yyyy-MM-ddTHH:mm:ss", "dd-MM-yyyy", "dd/MM/yyyy", "yyyy/MM/dd" };
            if (DateTime.TryParseExact(s, fmts, System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AssumeLocal, out var d))
                return d;
            if (DateTime.TryParse(s, out d)) return d;
            return null;
        }

        protected void btnDaftarResepApol_Click(object sender, EventArgs e)
        {
            var svc = new Common.BPJS.Apotek.Service();

            try
            {
                var kdPpk = string.IsNullOrWhiteSpace(txtPPK.Text)
                                ? (ConfigurationManager.AppSettings["ApotekHospitalID"] ?? string.Empty)
                                : txtPPK.Text.Trim();

                var jnsObat = cboJnsObt.SelectedValue;  // "0/1/2/3"
                var jnsTgl = cboTgl.SelectedValue;     // "TGLPELSJP" / "TGLRSP"

                var tglMulai = txtTglAwal.SelectedDate?.ToString("yyyy-MM-dd");
                var tglAkhir = txtTglAkhir.SelectedDate?.ToString("yyyy-MM-dd");

                var request = new Common.BPJS.Apotek.Resep.DaftarResep.Request.Root()
                {
                    KdPPK = kdPpk,
                    KdJnsObat = jnsObat,
                    JnsTgl = jnsTgl,
                    TglMulai = tglMulai,
                    TglAkhir = tglAkhir
                };

                var response = svc.GetDaftarResep(request);

                var dt = BuildApolDataTableForGrid();

                if (response?.Metadata?.IsValid == true && response.Response != null)
                {
                    foreach (var res in response.Response)
                    {
                        var row = dt.NewRow();
                        row["NORESEP"] = res.NoResep ?? "";
                        row["NOAPOTIK"] = res.NoApotik ?? "";
                        row["NOSEP_KUNJUNGAN"] = res.NoSep_Kunjungan ?? "";
                        row["NOKARTU"] = res.NoKartu ?? "";
                        row["PesertaNama"] = res.Nama ?? "";
                        row["TGLENTRY"] = ParseDate(res.TglEntry) ?? (object)DBNull.Value;
                        row["TGLRESEP"] = ParseDate(res.TglResep) ?? (object)DBNull.Value;
                        row["TGLPELRSP"] = ParseDate(res.TglPelResep) ?? (object)DBNull.Value;

                        if (decimal.TryParse(Convert.ToString(res.ByTagRsp), out var byTag)) row["BYTAGRSP"] = byTag; else row["BYTAGRSP"] = 0m;
                        if (decimal.TryParse(Convert.ToString(res.ByVerRsp), out var byVer)) row["BYVERRSP"] = byVer; else row["BYVERRSP"] = 0m;

                        row["KDJNSOBAT"] = res.KdJnsObat ?? "";
                        row["FASKESASAL"] = res.FaskesAsal ?? "";
                        dt.Rows.Add(row);
                    }
                }

                Session[SessionNameForList] = dt;
                grdListApol.Rebind();
            }
            catch (Exception)
            {
                var dt = BuildApolDataTableForGrid();
                Session[SessionNameForList] = dt;
                grdListApol.Rebind();
            }
        }
        #endregion
    }
}
