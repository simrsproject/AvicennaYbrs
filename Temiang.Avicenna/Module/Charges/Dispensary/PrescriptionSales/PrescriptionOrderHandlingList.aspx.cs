using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Drawing;
using Telerik.Web.UI;
using System.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Linq;
using Temiang.Avicenna.BusinessObject.Reference;
using Newtonsoft.Json;

namespace Temiang.Avicenna.Module.Charges
{
    /// <summary>
    /// Prescription Order Handling & History
    /// </summary>
    /// Create By: SCI Team
    /// Modified:
    /// -- 25-Nov-2023 Handono --
    /// - UI: Efisiensi autorefresh Order List proses yg bertabrakan dengan proses lainnya untuk misal kasus load page detail yg lama
    ///      -> asp:Timer tidak bisa didisable via javascript sehingga diganti menggunakan control HTML dan javascript agar bisa dipause
    public partial class PrescriptionOrderHandlingList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e); // !!Jika tidak dipanggil, tampilan jadi tidak rapih

            if (Request.QueryString["rt"] == "opr")
                ProgramID = AppConstant.Program.PrescriptionRealizationOpr;
            else
                ProgramID = AppConstant.Program.PrescriptionRealization;

            // Untuk menjalankan Autorefresh
            AjaxManager.ClientEvents.OnResponseEnd = "AjaxManager_OnResponseEnd";

            if (!IsPostBack)
            {
                //service unit
                var unit = new ServiceUnitCollection();
                var query = new ServiceUnitQuery();

                if (AppSession.Parameter.IsPrescOrderHandlingBasedOnDispensary)
                {
                    query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.ClusterPatient,
                                                               AppConstant.RegistrationType.EmergencyPatient,
                                                               AppConstant.RegistrationType.OutPatient,
                                                               AppConstant.RegistrationType.InPatient,
                                                               AppConstant.RegistrationType.MedicalCheckUp));
                }
                else
                {
                    if (Request.QueryString["rt"] == "ipr")
                        query.Where(query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                    else if (Request.QueryString["rt"] == "opr")
                    {
                        query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.ClusterPatient,
                                                                AppConstant.RegistrationType.EmergencyPatient,
                                                                AppConstant.RegistrationType.OutPatient,
                                                                AppConstant.RegistrationType.MedicalCheckUp));
                    }
                    else
                    {
                        query.Where(query.SRRegistrationType.In(AppConstant.RegistrationType.ClusterPatient,
                                                                AppConstant.RegistrationType.EmergencyPatient,
                                                                AppConstant.RegistrationType.OutPatient,
                                                                AppConstant.RegistrationType.InPatient,
                                                                AppConstant.RegistrationType.MedicalCheckUp));
                    }
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

                txtOrderDate.SelectedDate = DateTime.Now;
                txtPrescriptionDate.SelectedDate = DateTime.Now;

                grdOrder.Columns.FindByUniqueName("edit1").Visible = Request.QueryString["rt"] == "ipr" || !AppSession.Parameter.IsPrescSalesOpNeedSoape;
                grdOrder.Columns.FindByUniqueName("edit2").Visible = Request.QueryString["rt"] == "opr" && AppSession.Parameter.IsPrescSalesOpNeedSoape;
                grdOrder.Columns.FindByUniqueName("PrescriptionCategory").Visible = Request.QueryString["rt"] == "ipr";

                //grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = AppSession.Parameter.CasemixValidationRegistrationType.Any(a => !string.IsNullOrEmpty(a));

                if (AppSession.Parameter.IsPrescOrderHandlingBasedOnDispensary)
                {
                    if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.InPatient) ||
                        AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.EmergencyPatient) ||
                        AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.OutPatient) ||
                        AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.MedicalCheckUp))
                    {
                        grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = true;
                    }
                    else
                        grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = false;
                }
                else
                {
                    if (Request.QueryString["rt"] == "opr")
                    {
                        if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.EmergencyPatient) ||
                            AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.OutPatient) ||
                            AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.MedicalCheckUp))
                        {
                            grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = true;
                        }
                        else
                            grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = false;
                    }
                    else
                    {
                        if (AppSession.Parameter.CasemixValidationRegistrationType.Contains(AppConstant.RegistrationType.InPatient))
                            grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = true;
                        else
                            grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible = false;
                    }
                }

                //grdOrder.Columns[0].Visible = Request.QueryString["rt"] == "ipr" || !AppSession.Parameter.IsPrescSalesOpNeedSoape;
                //grdOrder.Columns[1].Visible = Request.QueryString["rt"] == "opr" && AppSession.Parameter.IsPrescSalesOpNeedSoape;
                //grdOrder.Columns[4].Visible = Request.QueryString["rt"] == "ipr"; //prescription category

                grdPrescription.Columns.FindByUniqueName("Delivery").Visible = AppSession.Parameter.IsPrescriptionPendingDelivery;
                grdPrescription.Columns[4].Visible = Request.QueryString["rt"] == "ipr"; //order no
                grdPrescription.Columns[14].Visible = Request.QueryString["rt"] == "opr"; //paid
                grdPrescription.Columns[15].Visible = Request.QueryString["rt"] == "opr"; //proceed by pharmacist
                grdPrescription.Columns[20].Visible = AppSession.Parameter.IsShowPrintLabelOnTransEntry;

                trPrescriptionStatus.Visible = (Request.QueryString["rt"] == "opr");

                trOrderSRFloor.Visible = false; //(Request.QueryString["rt"] == "opr");
                trPrescriptionSRFloor.Visible = false; //(Request.QueryString["rt"] == "opr");
                trPrescriptionCategory.Visible = (Request.QueryString["rt"] == "ipr");
                trPrescriptionCreatedBy.Visible = (Request.QueryString["rt"] == "ipr");
                StandardReference.InitializeIncludeSpace(cboOrderSRFloor, AppEnum.StandardReference.Floor);
                StandardReference.InitializeIncludeSpace(cboPrescriptionSRFloor, AppEnum.StandardReference.Floor);
                StandardReference.InitializeIncludeSpace(cboSRPrescriptionCategory, AppEnum.StandardReference.PrescriptionCategory);

                var utype = new AppStandardReferenceItemCollection();
                utype.Query.Where(utype.Query.StandardReferenceID == "UserType", utype.Query.ItemID.In("DTR", "NRS"));
                utype.LoadAll();
                cboPrescriptionCreatedBy.Items.Add(new RadComboBoxItem("", ""));
                foreach (var t in utype)
                {
                    cboPrescriptionCreatedBy.Items.Add(new RadComboBoxItem(t.ItemName, t.ItemID));
                }

                //Timer1.Interval = AppSession.Parameter.IntervalRefreshPrescriptionOrderList; // Pindah ke javascript (Handono 231125)
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                RestoreValueFromCookie();
                ComboBox.PopulateWithServiceUnitForTransaction(cboDispensaryID, TransactionCode.Prescription, true);
            }
        }

        protected void grdOrder_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdOrder.DataSource = TransPrescriptionOrders;
        }

        private string[] _patientIdSearchs = null;
        private string[] PatientIdSearchs
        {
            get
            {
                // Check record in Registration and Patient
                if (!string.IsNullOrWhiteSpace(txtRegistrationNo.Text))
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    if (txtRegistrationNo.Text.ToLower().Contains("reg"))
                    {
                        var reg = new Registration();
                        if (!reg.LoadByPrimaryKey(txtRegistrationNo.Text))
                        {
                            return null;
                        }
                        else
                            _patientIdSearchs = new[] { reg.PatientID };
                    }
                    else
                    {
                        // Check patient Exist
                        var patQr = new PatientQuery("p");
                        patQr.Select(patQr.PatientID);
                        patQr.es.Top = 50; // Batasi hanya 50 berdasrkan LastVisitDate (Handono 20241121)
                        patQr.OrderBy(patQr.LastVisitDate.Descending);

                        var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());
                        patQr.Where(
                            patQr.Or(
                                patQr.ReverseMedicalNo.Like(reverseMedNoSearch),
                                patQr.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                                )
                            );

                        var dtbPids = patQr.LoadDataTable();
                        if (dtbPids.Rows.Count > 0)
                            // Isi _patientIdSearchs untuk pencarian dan ini efektif jika patientid nya "sedikit"
                            _patientIdSearchs = dtbPids.AsEnumerable().Select(r => r.Field<string>("PatientID")).ToArray();
                        else
                            // Isi kosong spy tidak menggunakan pencarian berdasarkan PatientID
                            _patientIdSearchs = new string[0] { };

                        if (dtbPids.Rows.Count == 0) return null;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(txtPatientName.Text))
                {
                    // Check patient Exist
                    var searchPatient = txtPatientName.Text.Trim() + "%"; //Sudah konfirmasi ke IT RSI dan bu Rimma kalau user biasanya cari dengan nama depan dulu (Handono 202411)
                    var patQr = new PatientQuery("p");
                    patQr.Select(patQr.PatientID);
                    patQr.es.Top = 50; // Batasi hanya 50 berdasrkan LastVisitDate (Handono 20241121)
                    patQr.OrderBy(patQr.LastVisitDate.Descending);
                    patQr.Where(patQr.FullName.Like(searchPatient));
                    var dtbPids = patQr.LoadDataTable();
                    if (dtbPids.Rows.Count > 0)
                        // Isi _patientIdSearchs untuk pencarian dan ini efektif jika patientid nya "sedikit"
                        _patientIdSearchs = dtbPids.AsEnumerable().Select(r => r.Field<string>("PatientID")).ToArray();
                    else
                        // Isi kosong spy tidak menggunakan pencarian berdasarkan PatientID
                        _patientIdSearchs = new string[0] { };

                    if (dtbPids.Rows.Count == 0) return null;

                }

                return _patientIdSearchs;
            }
        }
        private DataTable TransPrescriptionOrders
        {
            get
            {
                // Check pencarian berdasarkan MedicalNo atau Name
                if ((!string.IsNullOrWhiteSpace(txtRegistrationNo.Text) || (!string.IsNullOrWhiteSpace(txtPatientName.Text))) && PatientIdSearchs == null)
                    return null;

                var presc = new TransPrescriptionQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var medic = new ParamedicQuery("e");
                var grr = new GuarantorQuery("f");
                var unitfm = new ServiceUnitQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");
                var ctg = new AppStandardReferenceItemQuery("ctg");
                var casemixguar = new CasemixCoveredGuarantorQuery("casemixg");

                //presc.es.Distinct = true;
                presc.es.Top = AppSession.Parameter.MaxResultRecord;

                presc.Select
                    (
                        presc.PrescriptionDate,
                        presc.PrescriptionNo,
                        presc.RegistrationNo,
                        patient.MedicalNo,
                        patient.PatientName,
                        unit.ServiceUnitName.As("FromServiceUnit"),
                        unitfm.ServiceUnitName,
                        medic.ParamedicName,
                        grr.GuarantorName,
                        presc.IsApproval,
                        @"<'True' AS Soape>",
                        sal.ItemName.As("SalutationName"),
                        @"<CAST(0 AS BIT) AS IsDebtAvailable>",
                        presc.SRPrescriptionCategory,
                        ctg.ItemName.As("PrescriptionCategoryName"),
                        @"<CASE WHEN ISNULL(ctg.ReferenceID, '') = '' THEN '#FFFFFF' ELSE ctg.ReferenceID END AS PrescriptionCategoryBackColor>",
                        presc.CreatedDateTime,
                        presc.IsCito,
                        @"<CASE WHEN casemixg.GuarantorID IS NULL THEN CAST(0 AS BIT) ElSE CAST(1 AS BIT) END AS IsGuarantorBpjsCasemix>"
                    );

                presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);
                presc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                presc.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                presc.LeftJoin(medic).On(presc.ParamedicID == medic.ParamedicID);
                presc.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                presc.InnerJoin(unitfm).On(presc.ServiceUnitID == unitfm.ServiceUnitID);
                presc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);
                presc.LeftJoin(ctg).On(ctg.StandardReferenceID == "PrescriptionCategory" & presc.SRPrescriptionCategory == ctg.ItemID);
                presc.LeftJoin(casemixguar).On(casemixguar.GuarantorID == reg.GuarantorID);

                if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
                {
                    presc.Where(presc.ServiceUnitID == Request.QueryString["unit"]);
                    if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
                    {
                        presc.Where(presc.LocationID == Request.QueryString["loc"]);
                    }
                }
                else
                {
                    if (AppSession.Parameter.IsPrescOrderHandlingBasedOnDispensary)
                    {
                        if (Request.QueryString["rt"] == "ipr")
                            presc.Where(presc.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyID);
                        else
                            presc.Where(presc.ServiceUnitID != AppSession.Parameter.ServiceUnitPharmacyID);
                    }
                    else
                    {
                        if (Request.QueryString["rt"] == "opr")
                            presc.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient);
                        else
                            presc.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                    }
                }

                if (grdOrder.Columns.FindByUniqueName("NeedValidationByCasemix").Visible)
                    presc.Select(@"<ISNULL((SELECT TOP 1 CAST(1 AS BIT) AS x FROM TransPrescriptionItem AS tpi
                            WHERE tpi.PrescriptionNo = a.PrescriptionNo AND ISNULL(tpi.IsCasemixApproved, 0) = 0 AND tpi.CasemixApprovedDateTime IS NULL), CAST(0 AS BIT)) AS 'IsNeedValidationByCasemix'>");
                else
                    presc.Select(@"<CAST(0 AS BIT) AS IsNeedValidationByCasemix>");

                presc.OrderBy(presc.PrescriptionNo.Descending);
                presc.Where(presc.IsFromSOAP == true,
                            presc.IsApproval == false,
                            presc.IsVoid == false,
                            presc.IsPrescriptionReturn == false,
                            presc.Or(presc.IsUnitDosePrescription.IsNull(), presc.IsUnitDosePrescription == false));

                if (Request.QueryString["rt"] == "ipr" &&
                    AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionMustVerifyByDpjp))
                {
                    presc.Where(presc.IsVerified == true);
                }

                if (!txtOrderDate.IsEmpty)
                    presc.Where(presc.PrescriptionDate >= txtOrderDate.SelectedDate, presc.PrescriptionDate < txtOrderDate.SelectedDate.Value.AddDays(1));
                if (txtOrderNo.Text != string.Empty)
                    presc.Where(presc.PrescriptionNo == txtOrderNo.Text);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    presc.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (cboParamedicID.SelectedValue != string.Empty)
                    presc.Where(presc.ParamedicID == cboParamedicID.SelectedValue);
                //if (txtRegistrationNo.Text != string.Empty)
                //{
                //    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                //    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                //    presc.Where(
                //        presc.Or(
                //            presc.RegistrationNo == searchReg,
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );

                //    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                //    //    presc.Where(
                //    //            presc.Or(
                //    //                presc.RegistrationNo == searchReg,
                //    //                patient.MedicalNo == searchReg,
                //    //                patient.OldMedicalNo == searchReg,
                //    //                string.Format("< OR REPLACE({1}.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patient.es.JoinAlias),
                //    //                string.Format("< OR REPLACE({1}.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patient.es.JoinAlias)
                //    //                )
                //    //            );
                //    //else
                //    //    presc.Where(
                //    //            presc.Or(
                //    //                presc.RegistrationNo == searchReg,
                //    //                patient.MedicalNo == searchReg,
                //    //                patient.OldMedicalNo == searchReg,
                //    //                string.Format("< OR {1}.MedicalNo LIKE '%{0}%'>", searchReg, patient.es.JoinAlias),
                //    //                string.Format("< OR {1}.OldMedicalNo LIKE '%{0}%'>", searchReg, patient.es.JoinAlias)
                //    //            )
                //    //        );
                //}

                //if (txtPatientName.Text != string.Empty)
                //{
                //    string searchPatient = Helper.EscapeQuery(txtPatientName.Text) + "%";
                //    presc.Where
                //        (
                //            patient.FullName.Like(searchPatient)
                //            //string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                //        );
                //}

                // Filter PatientID jika didepan ada patient nya berdasarkan pencarin nama atau MedicalNo
                if (PatientIdSearchs != null && PatientIdSearchs.Length > 0)
                    presc.Where(patient.PatientID.In(PatientIdSearchs), reg.PatientID.In(_patientIdSearchs));

                if (!string.IsNullOrEmpty(cboOrderSRFloor.SelectedValue))
                    presc.Where(presc.SRFloor == cboOrderSRFloor.SelectedValue);
                if (!string.IsNullOrEmpty(cboSRPrescriptionCategory.SelectedValue))
                    presc.Where(presc.SRPrescriptionCategory == cboSRPrescriptionCategory.SelectedValue);
                if (!string.IsNullOrEmpty(cboPrescriptionCreatedBy.SelectedValue))
                {
                    var usr = new AppUserQuery("usr");
                    presc.InnerJoin(usr).On(usr.UserID == presc.CreatedByUserID);
                    presc.Where(usr.SRUserType == cboPrescriptionCreatedBy.SelectedValue);
                }

                // Ganti dengan distinct (Fajri)
                //presc.GroupBy(presc.PrescriptionDate, presc.PrescriptionNo, presc.RegistrationNo, patient.MedicalNo, patient.PatientName,
                //        unit.ServiceUnitName, unitfm.ServiceUnitName, medic.ParamedicName, grr.GuarantorName, presc.IsApproval, sal.ItemName,
                //        presc.SRPrescriptionCategory, ctg.ItemName, presc.CreatedDateTime, presc.IsCito, ctg.ReferenceID, casemixguar.GuarantorID);
                presc.es.Distinct = true;

                DataTable dtbl = presc.LoadDataTable();

                if ((Request.QueryString["rt"] == "opr" && AppSession.Parameter.IsPrescSalesOpNeedSoape))
                {
                    foreach (DataRow row in dtbl.Rows)
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
                    dtbl.AcceptChanges();
                }

                return dtbl;
            }
        }

        protected void grdPrescription_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "setStatus")
            {
                var cmd = e.CommandArgument.ToString().Split('|');
                var prescriptionNo = cmd[0];
                var regNo = cmd[2];
                var prescStat = cmd[1];
                //var pres = new TransPrescription();
                //if (pres.LoadByPrimaryKey(cmd[0]) && cmd.Length > 1)
                //{
                switch (prescStat)
                {
                    case "complete":
                        {
                            PrescriptionSalesDetail.UpdatePrescriptionStatus(prescriptionNo, prescStat);
                            //pres.CompleteDateTime = DateTime.Now;
                            //pres.Save();

                            if (Helper.IsBpjsAntrolIntegration)
                            {
                                try
                                {
                                    var reg = new Registration();
                                    reg.LoadByPrimaryKey(regNo);

                                    if (!string.IsNullOrWhiteSpace(reg.AppointmentNo) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
                                    {
                                        var log = new WebServiceAPILog();
                                        log.DateRequest = DateTime.Now;
                                        log.IPAddress = string.Empty;
                                        log.UrlAddress = "PrescriptionOrderHandlingList";
                                        log.Params = JsonConvert.SerializeObject(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                        {
                                            Kodebooking = reg.AppointmentNo,
                                            Taskid = 7,
                                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                        });

                                        var svc = new Common.BPJS.Antrian.Service();
                                        var response = svc.UpdateWaktuAntrian(new Common.BPJS.Antrian.Update.WaktuAntrian.Request.Root()
                                        {
                                            Kodebooking = reg.AppointmentNo,
                                            Taskid = 7,
                                            Waktu = Convert.ToInt64(DateTimeOffset.Now.ToUnixTimeMilliseconds())
                                        });

                                        log.Response = JsonConvert.SerializeObject(response);
                                        log.Save();
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            grdPrescription.Rebind();
                            break;
                        }
                    default:
                        {
                            PrescriptionSalesDetail.UpdatePrescriptionStatus(prescriptionNo, prescStat);
                            grdPrescription.Rebind();
                            break;
                        }
                }
                //}
            }
            else if (e.CommandName == "PrintPatientSticker")
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
                grdPrescription.MasterTableView.GroupsDefaultExpanded = true;
            }
        }

        private DataTable TransPrescriptions
        {
            get
            {
                var presc = new TransPrescriptionQuery("a");
                var reg = new RegistrationQuery("b");
                var patient = new PatientQuery("c");
                var unit = new ServiceUnitQuery("d");
                var medic = new ParamedicQuery("e");
                var grr = new GuarantorQuery("f");
                var unitfm = new ServiceUnitQuery("g");
                var sal = new AppStandardReferenceItemQuery("sal");

                //presc.es.Distinct = true;
                presc.Select
                    (
                        presc.PrescriptionDate,
                        presc.PrescriptionNo,
                        presc.OrderNo,
                        presc.RegistrationNo,
                        reg.PatientID,
                        patient.MedicalNo,
                        patient.PatientName,
                        unit.ServiceUnitName.As("FromServiceUnit"),
                        unitfm.ServiceUnitName,
                        medic.ParamedicName,
                        grr.GuarantorName,
                        presc.IsApproval,
                        presc.IsBillProceed,
                        presc.IsVoid,
                        "<CAST(0 AS BIT) AS IsPaid>",
                        presc.IsProceedByPharmacist,
                        presc.ApprovalDateTime,
                        presc.IsPrinted,
                        presc.CompleteDateTime,
                        presc.DeliverDateTime,
                        "<0 AS Status>",
                        sal.ItemName.As("SalutationName"),
                        presc.KioskQueueNo,
                        string.Format("<CASE WHEN b.GuarantorID IN ({0}) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS IsBpjsPatient>", GuarantorAskesIDList),
                        "<CAST(0 AS BIT) AS IsHasPendingDelivery>"
                    );

                presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);
                presc.InnerJoin(patient).On(reg.PatientID == patient.PatientID);
                presc.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
                presc.LeftJoin(medic).On(presc.ParamedicID == medic.ParamedicID);
                presc.InnerJoin(grr).On(reg.GuarantorID == grr.GuarantorID);
                presc.InnerJoin(unitfm).On(presc.ServiceUnitID == unitfm.ServiceUnitID);
                presc.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & patient.SRSalutation == sal.ItemID);

                if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
                {
                    presc.Where(presc.ServiceUnitID == Request.QueryString["unit"]);
                    if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
                    {
                        presc.Where(presc.LocationID == Request.QueryString["loc"]);
                    }
                }
                else
                {
                    if (AppSession.Parameter.IsPrescOrderHandlingBasedOnDispensary)
                    {
                        if (Request.QueryString["rt"] == "ipr")
                            presc.Where(presc.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyID);
                        else
                        {
                            presc.Where(presc.ServiceUnitID != AppSession.Parameter.ServiceUnitPharmacyID);
                        }
                    }
                    else
                    {
                        if (Request.QueryString["rt"] == "opr")
                            presc.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient);
                        else
                        {
                            presc.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                        }
                    }
                }

                presc.Where(presc.IsFromSOAP == true,
                            presc.IsPrescriptionReturn == false,
                            presc.Or(presc.IsApproval == true, presc.IsVoid == true),
                            presc.Or(presc.IsUnitDosePrescription.IsNull(), presc.IsUnitDosePrescription == false));

                bool IsFilterMaxResultRecord = true;
                if (!txtPrescriptionDate.IsEmpty)
                {
                    presc.Where(presc.PrescriptionDate >= txtPrescriptionDate.SelectedDate, presc.PrescriptionDate < txtPrescriptionDate.SelectedDate.Value.AddDays(1));
                    IsFilterMaxResultRecord = false;
                }
                if (txtPrescriptionNo.Text != string.Empty)
                    presc.Where(presc.PrescriptionNo == txtPrescriptionNo.Text);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                {
                    presc.Where(reg.ServiceUnitID == cboServiceUnitID.SelectedValue);
                    //IsFilterMaxResultRecord = false;
                }
                if (cboParamedicID.SelectedValue != string.Empty)
                    presc.Where(presc.ParamedicID == cboParamedicID.SelectedValue);
                if (!string.IsNullOrEmpty(cboDispensaryID.SelectedValue))
                    presc.Where(presc.ServiceUnitID == cboDispensaryID.SelectedValue);
                //if (txtRegistrationNo.Text != string.Empty)
                //{
                //    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                //    var reverseMedNoSearch = string.Format("{0}%", searchReg.Replace("-", "").Reverse());

                //    presc.Where(
                //        presc.Or(
                //            presc.RegistrationNo == searchReg,
                //            patient.ReverseMedicalNo.Like(reverseMedNoSearch),
                //            patient.ReverseOldMedicalNo.Like(reverseMedNoSearch)
                //            )
                //        );

                //    //if (AppSession.Parameter.IsMedicalNoContainStrip)
                //    //    presc.Where(
                //    //            presc.Or(
                //    //                presc.RegistrationNo == searchReg,
                //    //                patient.MedicalNo == searchReg,
                //    //                patient.OldMedicalNo == searchReg,
                //    //                string.Format("< OR REPLACE({1}.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patient.es.JoinAlias),
                //    //                string.Format("< OR REPLACE({1}.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg, patient.es.JoinAlias)
                //    //                )
                //    //            );
                //    //else
                //    //    presc.Where(
                //    //            presc.Or(
                //    //                presc.RegistrationNo == searchReg,
                //    //                patient.MedicalNo == searchReg,
                //    //                patient.OldMedicalNo == searchReg,
                //    //                string.Format("< OR {1}.MedicalNo LIKE '%{0}%'>", searchReg, patient.es.JoinAlias),
                //    //                string.Format("< OR {1}.OldMedicalNo LIKE '%{0}%'>", searchReg, patient.es.JoinAlias)
                //    //            )
                //    //        );

                //    IsFilterMaxResultRecord = false;
                //}

                //if (txtPatientName.Text != string.Empty)
                //{
                //    string searchPatient = Helper.EscapeQuery(txtPatientName.Text) + "%";
                //    presc.Where
                //        (
                //            patient.FullName.Like(searchPatient)
                //            //string.Format("<LTRIM(RTRIM(LTRIM(c.FirstName + ' ' + c.MiddleName)) + ' ' + c.LastName) LIKE '{0}'>", searchPatient)
                //        );
                //}


                // Filter PatientID jika didepan ada patient nya berdasarkan pencarin nama atau MedicalNo
                if (PatientIdSearchs != null && PatientIdSearchs.Length > 0)
                {
                    presc.Where(patient.PatientID.In(PatientIdSearchs), reg.PatientID.In(_patientIdSearchs));
                    IsFilterMaxResultRecord = false;
                }

                if (!string.IsNullOrEmpty(cboStatus.SelectedValue))
                {
                    if (cboStatus.SelectedValue == "1")
                        presc.Where(presc.IsProceedByPharmacist == true);
                    else
                        presc.Where(presc.Or(presc.IsProceedByPharmacist.IsNull(), presc.IsProceedByPharmacist == false));
                    IsFilterMaxResultRecord = false;
                }
                if (!string.IsNullOrEmpty(cboDeliveryStatus.SelectedValue))
                {
                    switch (cboDeliveryStatus.SelectedValue)
                    {
                        case "1":
                            {
                                presc.Where(presc.CompleteDateTime.IsNull());
                                break;
                            }
                        case "2":
                            {
                                presc.Where(presc.CompleteDateTime.IsNotNull(), presc.DeliverDateTime.IsNull());
                                break;
                            }
                        case "3":
                            {
                                presc.Where(presc.DeliverDateTime.IsNotNull());
                                break;
                            }
                    }
                    IsFilterMaxResultRecord = false;
                }

                if (!string.IsNullOrEmpty(cboPrescriptionSRFloor.SelectedValue))
                    presc.Where(presc.SRFloor == cboPrescriptionSRFloor.SelectedValue);

                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    //if (txtPrescriptionDate.SelectedDate.HasValue)
                    //{
                    //    presc.Where(presc.Or(
                    //    presc.PrescriptionNo == txtBarcode.Text,
                    //    presc.KioskQueueNo == txtBarcode.Text,
                    //    patient.MedicalNo == txtBarcode.Text
                    //    ));

                    presc.Where(presc.IsVoid == false);
                    //}
                    //else
                    //{
                    //    presc.Where(presc.PrescriptionNo == txtBarcode.Text);
                    //}
                    if (txtBarcode.Text.Contains("REG/"))
                    {
                        presc.Where(presc.RegistrationNo == txtBarcode.Text);
                    }
                    else if (txtBarcode.Text.Contains("RS"))
                    {
                        presc.Where(presc.PrescriptionNo == txtBarcode.Text);
                    }
                    if (Helper.IsNumeric(txtBarcode.Text.Replace("-", "").Replace(".", "")))
                    {
                        // norm
                        presc.Where(patient.MedicalNo == txtBarcode.Text);
                    }
                    else
                    {
                        presc.Where(presc.Or(
                            presc.PrescriptionNo == txtBarcode.Text,
                            presc.KioskQueueNo == txtBarcode.Text,
                            patient.MedicalNo == txtBarcode.Text
                        ));
                    }
                }
                if (!string.IsNullOrEmpty(txtKioskQueueNo.Text))
                    presc.Where(presc.KioskQueueNo == txtKioskQueueNo.Text);

                if (IsFilterMaxResultRecord)
                    presc.es.Top = AppSession.Parameter.MaxResultRecord;

                presc.OrderBy(presc.ApprovalDateTime.Descending, presc.PrescriptionNo.Descending);
                //presc.GroupBy(presc.PrescriptionDate, presc.PrescriptionNo, presc.OrderNo, presc.RegistrationNo, reg.PatientID, patient.MedicalNo,
                //        patient.PatientName, unit.ServiceUnitName, unitfm.ServiceUnitName, medic.ParamedicName, grr.GuarantorName, presc.IsApproval,
                //        presc.IsBillProceed, presc.IsVoid, presc.IsProceedByPharmacist, presc.ApprovalDateTime, presc.IsPrinted, presc.CompleteDateTime,
                //        presc.DeliverDateTime, sal.ItemName, presc.KioskQueueNo, reg.GuarantorID);

                DataTable dtbl = presc.LoadDataTable();

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
            SaveValueToCookie();

            grdOrder.Rebind();
            grdPrescription.Rebind();
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            grdOrder.Rebind();
        }

        //protected void txtBarcode_TextChanged(object sender, EventArgs e)
        //{
        //    lblBarcodeMsg.InnerText = "";
        //    switch (rbBarcodeMode.SelectedValue)
        //    {
        //        case "3":
        //        case "4":
        //            {
        //                var tpColl = new TransPrescriptionCollection();
        //                tpColl.Query.Where(tpColl.Query.PrescriptionNo == txtBarcode.Text);
        //                if (!tpColl.LoadAll())
        //                {
        //                    if (txtPrescriptionDate.SelectedDate.HasValue)
        //                    {
        //                        tpColl.QueryReset();
        //                        tpColl.Query.Where(tpColl.Query.KioskQueueNo == txtBarcode.Text,
        //                            tpColl.Query.PrescriptionDate == txtPrescriptionDate.SelectedDate)
        //                            .OrderBy(tpColl.Query.PrescriptionDate.Descending);
        //                        if (!tpColl.LoadAll())
        //                        {
        //                            var presc = new TransPrescriptionQuery("tp");
        //                            var reg = new RegistrationQuery("reg");
        //                            var pat = new PatientQuery("pat");

        //                            presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo)
        //                                .InnerJoin(pat).On(reg.PatientID == pat.PatientID)
        //                                .Where(
        //                                    presc.PrescriptionDate == txtPrescriptionDate.SelectedDate,
        //                                    pat.MedicalNo == txtBarcode.Text
        //                                ).Select(presc);

        //                            if (!string.IsNullOrEmpty(Request.QueryString["unit"]))
        //                            {
        //                                presc.Where(presc.ServiceUnitID == Request.QueryString["unit"]);
        //                                if (!string.IsNullOrEmpty(Request.QueryString["loc"]))
        //                                {
        //                                    presc.Where(presc.LocationID == Request.QueryString["loc"]);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                if (AppSession.Parameter.IsPrescOrderHandlingBasedOnDispensary)
        //                                {
        //                                    if (Request.QueryString["rt"] == "ipr")
        //                                        presc.Where(presc.ServiceUnitID == AppSession.Parameter.ServiceUnitPharmacyID);
        //                                    else
        //                                    {
        //                                        presc.Where(presc.ServiceUnitID != AppSession.Parameter.ServiceUnitPharmacyID);
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (Request.QueryString["rt"] == "opr")
        //                                        presc.Where(reg.SRRegistrationType != AppConstant.RegistrationType.InPatient);
        //                                    else
        //                                    {
        //                                        presc.Where(reg.SRRegistrationType == AppConstant.RegistrationType.InPatient);
        //                                    }
        //                                }
        //                            }
        //                            presc.Where(presc.IsFromSOAP == true,
        //                            presc.IsPrescriptionReturn == false,
        //                            presc.Or(presc.IsApproval == true, presc.IsVoid == true),
        //                            presc.Or(presc.IsUnitDosePrescription.IsNull(), presc.IsUnitDosePrescription == false));

        //                            tpColl.QueryReset();
        //                            if (!tpColl.Load(presc)) { 
        //                                // ???
        //                            }
        //                        }
        //                    }
        //                }
        //                if (tpColl.Any())
        //                {
        //                    if (tpColl.Count > 1)
        //                    {
        //                        // more than one result, do the action manually
        //                        lblBarcodeMsg.InnerText = "Barcode search returns more than one result, please proceed manually";
        //                    }
        //                    else {
        //                        var pres = tpColl.First();
        //                        switch (rbBarcodeMode.SelectedValue)
        //                        {
        //                            case "3":
        //                                {
        //                                    if (!pres.CompleteDateTime.HasValue)
        //                                    {
        //                                        pres.CompleteDateTime = (new DateTime()).NowAtSqlServer();
        //                                        tpColl.Save();
        //                                    }
        //                                    break;
        //                                }
        //                            case "4":
        //                                {
        //                                    if (!pres.DeliverDateTime.HasValue)
        //                                    {
        //                                        pres.DeliverDateTime = (new DateTime()).NowAtSqlServer();
        //                                        tpColl.Save();
        //                                    }
        //                                    break;
        //                                }
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //    }

        //    grdPrescription.Rebind();
        //    txtBarcode.Text = "";
        //    txtBarcode.Focus();
        //}
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

        public System.Drawing.Color GetColorCategory(object backColor)
        {
            System.Drawing.Color color = System.Drawing.Color.White;
            color = ColorTranslator.FromHtml(backColor.ToString());

            return color;
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
                }
            }
        }

        protected void btnRefreshOrder_Click(object sender, EventArgs e)
        {
            grdOrder.Rebind();
        }
    }
}
