using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlTypes;
using System.Text;
using System.Web.Services;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ResumeMedisInPatientEntry : BasePageDialogEntry
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;
            ProgramReferenceID = "MDS";

            // Program Fiture
            IsSingleRecordMode = false; //Save then close
            ToolBar.NavigationVisible = false;
            ToolBar.ApprovalUnApprovalVisible = false;
            ToolBar.VoidUnVoidVisible = false;
            ToolBar.DeleteVisible = false;

            ToolBar.PrintVisible = true;
            ToolBar.EditVisible = true;
            ToolBar.AddVisible = false;

            // View resume medis dari PRMRJ
            if (Request.QueryString["editable"] == "false")
                ToolBar.EditVisible = false;

            // -------------------

            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Medical Discharge Summary of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ", Reg No: " + RegistrationNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRUnitIntended, AppEnum.StandardReference.UnitIntended);
                ComboBox.PopulateWithParamedic(cboTreatingPhysician);
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            if (ToolBar.EditVisible && DataModeCurrent == AppEnum.DataMode.Read)
            {
                // Otomatis switch ke mode new jika record belum ada
                var medRes = new MedicalDischargeSummary();
                if (!medRes.LoadByPrimaryKey(RegistrationNo))
                {
                    var args = new ValidateArgs();
                    ForceToNewMode(args);

                    if (args.IsCancel == false)
                    {
                        // Reset datasource obat dibawa pulang
                        Session["DrugBroughtHome"] = null;
                        grdPrescription.DataSource = null;
                        grdPrescription.Rebind();
                    }
                }
            }

            base.OnLoadComplete(e);
        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {

            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(RegistrationNo))
                return;

            txtRegistrationDate.Text = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);

            var medRes = new MedicalDischargeSummary();
            if (medRes.LoadByPrimaryKey(RegistrationNo))
            {
                if (medRes.DischargeDate != null)
                {
                    txtDischargeDate.SelectedDate = medRes.DischargeDate;
                    txtDischargeTime.SelectedDate = Convert.ToDateTime(medRes.DischargeTime);
                }
                else
                {
                    txtDischargeDate.Clear();
                    txtDischargeTime.Clear();
                }
                StandardReference.InitializeWithOneRow(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, medRes.SRDischargeMethod);
                StandardReference.InitializeWithOneRow(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, medRes.SRDischargeCondition);

                cboTreatingPhysician.SelectedValue = medRes.ParamedicID;
                txtTreatingPhysicianName.Text = medRes.ParamedicName;
                cboSRUnitIntended.SelectedValue = medRes.SRUnitIntended;


                txtPhysicalExamination.Text = medRes.PhysicalExam;
                txtPrescription.Text = medRes.Medications;
                txtAncillaryExamination.Text = medRes.AncillaryExam;

                txtTreatmentIndications.Text = medRes.TreatmentIndications;
                txChiefComplaint.Text = medRes.ChiefComplaint;
                txtHistoryOfPresentIllness.Text = medRes.HistOfPresentIllness;
                txtMedicalProcedures.Text = medRes.MedicalProcedures;
                //ComboBox.PopulateWithOneProcedure(cboProcedureID, medRes.ProcedureID);
                txtProcedureName.Text = medRes.ProcedureName;
                txtPastMedicalHistory.Text = medRes.PastMedicalHistory;
                txtSuggestionFollowUp.Text = medRes.SuggestionFollowUp;
                txtPrognosis.Text = medRes.Prognosis;

                // External Referral
                // DischargeMethod -> I02	DIRUJUK
                if (cboSRDischargeMethod.SelectedValue == "I02")
                {
                    var refExt = new ReferExternal();
                    if (!refExt.LoadByPrimaryKey(RegistrationNo)) return;
                    ComboBox.PopulateWithOneRow(cboReferralID, refExt.ReferralID, Enums.EntityClassName.Referral);
                    StandardReference.InitializeWithOneRow(cboSRReferReason, AppEnum.StandardReference.ReferReason, refExt.SRReferReason);
                    txtOtherInformation.Text = refExt.OtherInformation;
                    txtReferralAgreedBy.Text = refExt.ReferralAgreedBy;
                    txtReferralAgreedTime.SelectedDate = refExt.ReferralAgreedTime;
                }

            }

            //// Final Diagnose
            //epDiagCtl.Populate(RegistrationNo, true);

            // Control Plan
            var plan = new MedicalDischargeSummaryByNurse();
            if (plan.LoadByPrimaryKey(RegistrationNo))
            {
                controlPlanCtl.Populate(plan.ControlPlan);
            }


            // Override list print
            var tbarPrint = ToolBarMenuPrint;
            tbarPrint.Buttons.Clear();


            var btn = new RadToolBarButton("Medical Discharge Summary")
            {
                Value = string.Format("rpt_{0}", "SLP.01.0089"),
                Enabled = !string.IsNullOrWhiteSpace(medRes.RegistrationNo)
            };
            tbarPrint.Buttons.Add(btn);

            var btn2 = new RadToolBarButton("External Patient Referral Form")
            {
                Value = string.Format("rpt_{0}", "SLP.01.RM14B"),
                Enabled = !string.IsNullOrWhiteSpace(medRes.SRDischargeMethod) && medRes.SRDischargeMethod == "I02"
            };
            tbarPrint.Buttons.Add(btn2);

            var btn3 = new RadToolBarButton("Referral Reply Letter")
            {
                Value = string.Format("rpt_{0}", "SLP.01.095"),
                Enabled = reg.SRReferralGroup != AppSession.Parameter.ReferralGroupDatangSendiri
            };
            tbarPrint.Buttons.Add(btn3);
        }


        private static string PrescriptionHist(List<string> mergeRegistrations)
        {
            // Obat patent
            var query = PrescriptionItemNameList(mergeRegistrations, false);
            var dtbPresc = query.LoadDataTable();
            var strb = new StringBuilder();
            foreach (DataRow row in dtbPresc.Rows)
            {
                strb.AppendFormat(" \u2022 {0} ({1} {2} {3})",
                    row["ItemName"], row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"]);
                strb.AppendLine(string.Empty);
            }

            //•
            // Obat Racikan
            query = PrescriptionItemNameList(mergeRegistrations, true);
            dtbPresc = query.LoadDataTable();
            foreach (DataRow row in dtbPresc.Rows)
            {
                var consumeMethod = string.Format("{0} {1} {2}", row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"]);
                var itemDescription = PrescriptionItemCompound(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), consumeMethod);
                strb.AppendLine(string.Empty);
                strb.AppendFormat(" \u2022 {0}", itemDescription);
                strb.AppendLine(string.Empty);
            }
            var prescriptionHist = strb.ToString();
            return prescriptionHist;
        }

        private static TransPrescriptionItemQuery PrescriptionItemNameList(List<string> mergeRegistrations, bool isCompound)
        {
            //Prescription History, yg diambil hanya daftar obat dan consume methodnya
            var query = new TransPrescriptionItemQuery("a");
            var qrPresc = new TransPrescriptionQuery("b");
            query.InnerJoin(qrPresc).On(query.PrescriptionNo == qrPresc.PrescriptionNo);

            var qrItem = new ItemQuery("i");
            query.InnerJoin(qrItem).On(query.ItemID == qrItem.ItemID);

            var itemIntervention = new ItemQuery("int");
            query.LeftJoin(itemIntervention).On(query.ItemInterventionID == itemIntervention.ItemID);

            var consume = new ConsumeMethodQuery("e");
            query.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);


            query.Select(
                "<COALESCE(int.ItemName, i.ItemName) as ItemName>",
                consume.SRConsumeMethodName,
                query.ConsumeQty,
                query.SRConsumeUnit,
                query.IsCompound

            );

            if (isCompound)
            {
                query.Select(query.ParentNo,
                    query.SequenceNo, query.PrescriptionNo);
                query.Where(query.Or(query.ParentNo.IsNull(), query.ParentNo == string.Empty));
            }
            else
            {
                query.Select("<'' as ParentNo>",
                    "<'' as SequenceNo>",
                    "<'' as PrescriptionNo>");
            }
            query.OrderBy("ItemName", esOrderByDirection.Ascending);
            query.es.Distinct = true;
            //if (!string.IsNullOrEmpty(fromRegistrationNo))
            //    query.Where(query.Or(qrPresc.RegistrationNo == registrationNo,
            //        qrPresc.RegistrationNo == fromRegistrationNo));
            //else
            //    query.Where(qrPresc.RegistrationNo == registrationNo);

            query.Where(qrPresc.RegistrationNo.In(mergeRegistrations));

            query.Where(query.IsCompound == isCompound);
            return query;
        }

        private static string PrescriptionItemCompound(string prescriptionNo, string sequenceNo, string consumeMethod)
        {
            // Racikan
            var query = new TransPrescriptionItemQuery("a");
            var qItem = new ItemQuery("b");
            var qItemMedic = new ItemProductMedicQuery("im");
            var qItemIntervention = new ItemQuery("c");

            query.InnerJoin(qItem).On(query.ItemID == qItem.ItemID);
            query.InnerJoin(qItemMedic).On(query.ItemID == qItemMedic.ItemID);
            query.LeftJoin(qItemIntervention).On(query.ItemInterventionID == qItemIntervention.ItemID);

            query.Select
            (
                query.ItemInterventionID, query.ParentNo, query.IsRFlag,
                qItem.ItemName, query.SRDosageUnit, query.DosageQty,
                qItemIntervention.ItemName.Coalesce("''").As("ItemNameIntervention")
            );

            query.Where(query.PrescriptionNo == prescriptionNo, query.Or(query.SequenceNo == sequenceNo, query.ParentNo == sequenceNo));
            query.OrderBy(query.SequenceNo.Ascending);

            var dtb = query.LoadDataTable();
            var sbItem = new StringBuilder();
            foreach (DataRow row in dtb.Rows)
            {
                var itemName = row["ItemName"].ToString();


                if (row["ItemInterventionID"] != DBNull.Value &&
                    !string.IsNullOrEmpty(row["ItemInterventionID"].ToString()))
                {
                    itemName = row["ItemNameIntervention"].ToString();
                }

                if (row["ParentNo"] != DBNull.Value && string.IsNullOrEmpty(row["ParentNo"].ToString()))
                {
                    //Header
                    sbItem = new StringBuilder();
                    sbItem.AppendFormat("{0} @{1} {2} ({3}){4}", itemName, row["DosageQty"], row["SRDosageUnit"], consumeMethod, Environment.NewLine);

                }
                else
                {
                    sbItem.AppendFormat("     - {0} @{1} {2}{3}", itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                }
            }
            return sbItem.ToString();
        }

        #region LaboratoryResult
        public static string LaboratoryHist(List<string> mergeRegs)
        {
            var strb = new StringBuilder();
            try
            {
                var dtbLab = LaboratoryResult(mergeRegs);
                var orderNo = string.Empty;

                var isFirstRow = true;
                foreach (DataRow row in dtbLab.Rows)
                {
                    if (orderNo != row["OrderLabNo"].ToString())
                    {
                        if (!isFirstRow)
                            strb.AppendLine(string.Empty);

                        orderNo = row["OrderLabNo"].ToString();
                        strb.AppendFormat("Lab No: {0} ({1})", row["OrderLabNo"], Convert.ToDateTime(row["OrderLabTglOrder"]).ToString(AppConstant.DisplayFormat.Date));
                        strb.AppendLine(string.Empty);
                    }
                    if (row["Result"] != null && !string.IsNullOrWhiteSpace(row["Result"].ToString()))
                        strb.AppendFormat(" \u2022 {0}: {1} {2}", row["LabOrderSummary"], row["Result"], row["Satuan"]);
                    else
                        strb.AppendFormat("{0}", row["LabOrderSummary"]);
                    strb.AppendLine(string.Empty);

                    isFirstRow = false;
                }
            }
            catch (Exception e)
            {
                //if (HttpContext.Current.IsDebuggingEnabled)
                //throw;
            }
            return strb.ToString();
        }

        private static DataTable LaboratoryResult(List<string> mergeRegs)
        {
            DataTable dtbResult = null;

            if (AppSession.Parameter.IsUsingHisInterop)
            {
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                        dtbResult = LabHistOrderResultFromSysmex(mergeRegs);
                        break;
                    case "RSCH":
                        dtbResult = LabHistOrderResultFromRSCH(mergeRegs);
                        break;
                    case "VANSLAB":
                        dtbResult = LabHistOrderResultFromVanslab(mergeRegs);
                        break;
                    default:
                        dtbResult = LabHistOrderResultFromManualEntry(mergeRegs);
                        break;
                }
            }
            else
                dtbResult = LabHistOrderResultFromManualEntry(mergeRegs);

            if (dtbResult == null)
            {
                // Return blank DataTable
                dtbResult = new DataTable();
                dtbResult.Columns.Add(new DataColumn("OrderLabNo", typeof(string)));
                dtbResult.Columns.Add(new DataColumn("LabOrderCode", typeof(string)));
                dtbResult.Columns.Add(new DataColumn("LabOrderSummary", typeof(string)));
                dtbResult.Columns.Add(new DataColumn("Result", typeof(string)));
                dtbResult.Columns.Add(new DataColumn("StandarValue", typeof(string)));
                dtbResult.Columns.Add(new DataColumn("OrderLabTglOrder", typeof(DateTime)));
            }

            return dtbResult;
        }

        private static DataTable LabHistOrderResultFromSysmex(List<string> mergeRegs)
        {
            // Hasil Lab terakhir dimunculkan semua tetapi Hasil Lab sebelumnya hanya yg tidak normal
            var hasil = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("hp");

            var lastLab = new TransChargesQuery("a");
            lastLab.Select(lastLab.TransactionNo);
            lastLab.Where(lastLab.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID,
                lastLab.RegistrationNo.In(mergeRegs));
            lastLab.es.Top = 1;
            lastLab.OrderBy(lastLab.TransactionDate.Descending, lastLab.TransactionNo.Descending);
            var tc = new TransCharges();
            if (tc.Load(lastLab))
            {
                // Lab terakhir diambil semua
                hasil = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("hp");
                hasil.Where(hasil.OrderLabNo == tc.TransactionNo);

                hasil.Select(
                    hasil.OrderLabNo,
                    hasil.OrderLabTglOrder,
                    hasil.LabOrderCode,
                    hasil.LabOrderSummary,
                    hasil.Result.As("Result"),
                    hasil.Unit.As("Satuan"),
                    hasil.StandarValue,
                    hasil.OrderLabTglOrder
                );
                hasil.OrderBy(hasil.DispSeq.Ascending);
                var dtb = hasil.LoadDataTable();

                // Lab berikutnya hanya yg tidak normal
                var prevLab = new TransChargesQuery("a");
                prevLab.Select(prevLab.TransactionNo);
                prevLab.Where(prevLab.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID, prevLab.TransactionNo != tc.TransactionNo, prevLab.RegistrationNo.In(mergeRegs));
                prevLab.OrderBy(lastLab.TransactionNo.Descending);
                var dtbPrevLab = prevLab.LoadDataTable();
                foreach (DataRow row in dtbPrevLab.Rows)
                {
                    hasil = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("hp");
                    hasil.Where(hasil.OrderLabNo == row["TransactionNo"].ToString(), hasil.Flag > string.Empty);

                    hasil.Select(
                        hasil.OrderLabNo,
                        hasil.OrderLabTglOrder,
                        hasil.LabOrderCode,
                        hasil.LabOrderSummary,
                        hasil.Result.As("Result"),
                        hasil.Unit.As("Satuan"),
                        hasil.StandarValue,
                        hasil.OrderLabTglOrder
                    );
                    hasil.OrderBy(hasil.OrderLabTglOrder.Descending, hasil.OrderLabNo.Descending, hasil.DispSeq.Ascending);
                    var dtbPrevHasil = hasil.LoadDataTable();

                    // Merge
                    dtb.Merge(dtbPrevHasil);
                }

                return dtb;
            }

            return null;
        }

        private static DataTable LabHistOrderResultFromRSCH(List<string> mergeRegs)
        {
            // Hasil Lab pertama dimunculkan semua tetapi Hasil Lab berikutnya hanya yg tidak normal
            var hasil = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("hp");
            var hd = new TransChargesQuery("a");

            var query = new TransChargesQuery("a");
            query.Select(query.TransactionNo);
            query.Where(query.RegistrationNo.In(mergeRegs));
            query.es.Top = 1;
            query.OrderBy(query.TransactionNo.Ascending);
            var tc = new TransCharges();
            if (tc.Load(query))
            {
                // Lab pertama diambil semua
                hasil.InnerJoin(hd).On(hasil.OrderLabNo == hd.TransactionNo);
                hasil.Where(hd.TransactionNo == tc.TransactionNo);

                hasil.Select(
                    hasil.OrderLabNo,
                    hasil.OrderLabTglOrder,
                    hasil.CheckupResultFractionCode.As("LabOrderCode"),
                    hasil.CheckupResultFractionName.As("LabOrderSummary"),
                    "<CASE WHEN COALESCE(hp.WithinRange,'') = '' THEN hp.OutRange ELSE WithinRange END AS Result>",
                    hasil.Satuan,
                    hasil.StandarValue
                );
                hasil.OrderBy(hasil.OrderLabNo.Ascending, hasil.Seq.Ascending);
                var firstLab = hasil.LoadDataTable();

                // Lab berikutnya hanya yg tidak normal
                hasil = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("hp");
                hd = new TransChargesQuery("a");

                hasil.InnerJoin(hd).On(hasil.OrderLabNo == hd.TransactionNo);
                hasil.Where(hd.TransactionNo > tc.TransactionNo, hd.RegistrationNo.In(mergeRegs),
                    hasil.OutRange > string.Empty);

                hasil.Select(
                    hasil.OrderLabNo,
                    hasil.OrderLabTglOrder,
                    hasil.CheckupResultFractionCode.As("LabOrderCode"),
                    hasil.CheckupResultFractionName.As("LabOrderSummary"),
                    hasil.Satuan,
                    hasil.StandarValue
                );
                hasil.OrderBy(hasil.OrderLabNo.Ascending, hasil.Seq.Ascending);
                var nextLab = hasil.LoadDataTable();

                // Merge
                firstLab.Merge(nextLab);
                return firstLab;
            }

            return null;
        }

        private static DataTable LabHistOrderResultFromVanslab(List<string> mergeRegs)
        {
            // RSMM
            // Hasil Lab terakhir dimunculkan semua tetapi Hasil Lab sebelumnya hanya yg tidak normal
            var hasil = new Temiang.Avicenna.BusinessObject.Interop.VANSLAB.LabHasilQuery("hp");
            hasil.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;

            var lastLab = new TransChargesQuery("a");
            lastLab.Select(lastLab.TransactionNo);
            lastLab.Where(lastLab.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID,
                lastLab.RegistrationNo.In(mergeRegs));
            lastLab.es.Top = 1;
            lastLab.OrderBy(lastLab.TransactionDate.Descending, lastLab.TransactionNo.Descending);
            var tc = new TransCharges();
            if (tc.Load(lastLab))
            {
                // Lab terakhir diambil semua
                hasil = new Temiang.Avicenna.BusinessObject.Interop.VANSLAB.LabHasilQuery("hp");
                hasil.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
                hasil.Where(hasil.NoRegistrasi == tc.TransactionNo);

                hasil.Select(
                    hasil.NoRegistrasi.As("OrderLabNo"),
                    hasil.TglDaftar.As("OrderLabTglOrder"),
                    hasil.KodePemeriksaan.As("LabOrderCode"),
                    hasil.NamaPemeriksaan.As("LabOrderSummary"),
                    hasil.Hasil.As("Result"),
                    hasil.Unit.As("Satuan"),
                    hasil.Normal.As("StandarValue"),
                    hasil.KodeSir
                );
                hasil.OrderBy(hasil.NoUrut.Ascending);
                var dtb = hasil.LoadDataTable();

                // Lab berikutnya hanya yg tidak normal
                var prevLab = new TransChargesQuery("a");
                prevLab.Select(prevLab.TransactionNo);
                prevLab.Where(prevLab.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID, prevLab.TransactionNo != tc.TransactionNo, prevLab.RegistrationNo.In(mergeRegs));
                prevLab.OrderBy(lastLab.TransactionNo.Descending);
                var dtbPrevLab = prevLab.LoadDataTable();
                foreach (DataRow row in dtbPrevLab.Rows)
                {
                    hasil = new Temiang.Avicenna.BusinessObject.Interop.VANSLAB.LabHasilQuery("hp");
                    hasil.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
                    hasil.Where(hasil.NoRegistrasi == row["TransactionNo"].ToString(), hasil.Flag > string.Empty);

                    hasil.Select(
                        hasil.NoRegistrasi.As("OrderLabNo"),
                        hasil.TglDaftar.As("OrderLabTglOrder"),
                        hasil.KodePemeriksaan.As("LabOrderCode"),
                        hasil.NamaPemeriksaan.Trim().As("LabOrderSummary"),
                        hasil.Hasil.As("Result"),
                        hasil.Unit.As("Satuan"),
                        hasil.Normal.As("StandarValue"),
                        hasil.KodeSir
                    );
                    hasil.OrderBy(hasil.TglDaftar.Descending, hasil.NoRegistrasi.Descending, hasil.NoUrut.Ascending);
                    var dtbPrevHasil = hasil.LoadDataTable();

                    // Merge
                    dtb.Merge(dtbPrevHasil);
                }

                // Hapus tipe lab yg Confidential
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["kode_sir"] == null || string.IsNullOrWhiteSpace(row["kode_sir"].ToString())) continue;
                    var ilab = new ItemLaboratory();
                    if (ilab.LoadByPrimaryKey(row["kode_sir"].ToString()))
                    {
                        if (ilab.IsConfidential ?? false)
                            row.Delete();
                    }
                }

                return dtb;
            }

            return null;
        }
        private static DataTable LabHistOrderResultFromManualEntry(List<string> mergeRegs)
        {
            //// Ambil data dari ItemLaboratoryDetail
            //var qr = new TransChargesItemQuery("dt");
            //var order = new TransChargesQuery("hd");
            //qr.InnerJoin(order).On(qr.TransactionNo == order.TransactionNo);

            //var item = new ItemQuery("i");
            //qr.InnerJoin(item).On(qr.ItemID == item.ItemID);

            //var itemGroup = new ItemGroupQuery("g");
            //qr.InnerJoin(itemGroup).On(item.ItemGroupID == itemGroup.ItemGroupID);

            //qr.Select(qr.TransactionNo.As("OrderLabNo"), qr.ItemID.As("LabOrderCode"), item.ItemName.As("LabOrderSummary"),
            //    qr.ResultValue.As("Result"),
            //    order.TransactionDate.As("OrderLabTglOrder"), itemGroup.ItemGroupName.As("TEST_GROUP"));

            //qr.Where(qr.TransactionNo == transactionNo);

            //var dtbTransChargesItem = qr.LoadDataTable();
            //dtbTransChargesItem.Columns.Add(new DataColumn("StandarValue", typeof(string)));

            //// Isi StandarValue
            //var reg = new Registration();
            //reg.LoadByPrimaryKey(RegistrationNo);

            //var patient = new Patient();
            //patient.LoadByPrimaryKey(reg.PatientID);

            //var ageInDays = (reg.RegistrationDate - patient.DateOfBirth).Value.TotalDays;

            //foreach (DataRow row in dtbTransChargesItem.Rows)
            //{
            //    var stdval = new ItemLaboratoryDetailQuery();
            //    stdval.Where(stdval.ItemID == row["LabOrderCode"].ToString());
            //    stdval.Where(stdval.Sex == patient.Sex);
            //    stdval.Where(stdval.TotalAgeMin <= ageInDays && stdval.TotalAgeMax >= ageInDays);
            //    var dtbStdVal = stdval.LoadDataTable();
            //    if (dtbStdVal.Rows.Count > 0)
            //    {
            //        try
            //        {
            //            // Test is numeric value
            //            var normalValueMin = Convert.ToDecimal(dtbStdVal.Rows[0]["NormalValueMin"]);
            //            var normalValueMax = Convert.ToDecimal(dtbStdVal.Rows[0]["NormalValueMax"]);

            //            // if no error
            //            row["StandarValue"] = string.Format("{0} - {1}", dtbStdVal.Rows[0]["NormalValueMin"],
            //                dtbStdVal.Rows[0]["NormalValueMax"]);
            //        }
            //        catch
            //        {
            //            row["StandarValue"] = dtbStdVal.Rows[0]["NormalValueMin"];
            //        }
            //    }
            //}

            //return dtbTransChargesItem;
            return null;
        }
        #endregion

        private string MedicalProcedures()
        {
            //Planing Procedure
            // Ambil di Asessmen Therapy

            //Real Procedure
            var query = new EpisodeProcedureQuery("a");
            var proc = new ProcedureQuery("p");
            query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);
            var para = new ParamedicQuery("par");
            query.LeftJoin(para).On(query.ParamedicID == para.ParamedicID);
            query.Where(query.RegistrationNo.In(MergeRegistrations));

            query.Select(query.ProcedureDate, proc.ProcedureName, para.ParamedicName);
            var dtbSurgery = query.LoadDataTable();
            var strb = new StringBuilder();
            foreach (DataRow row in dtbSurgery.Rows)
            {
                strb.AppendFormat("{0} {1} by {2}",
                    Convert.ToDateTime(row["ProcedureDate"]).ToString(AppConstant.DisplayFormat.Date), row["ProcedureName"],
                    row["ParamedicName"]);
                strb.AppendLine("");
            }

            var surgery = strb.ToString();
            return surgery;
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //if (!IsPostBack || oldVal != newVal)
            //    epDiagCtl.Populate(RegistrationNo, true, newVal == AppEnum.DataMode.Read);

            if (oldVal != newVal)
            {
                Session["DrugBroughtHome"] = null;
                grdPrescription.Rebind();
            }

            finalDiagCtl.Rebind(newVal != AppEnum.DataMode.Read);
            resumeMedisProcedureCtl.Rebind(newVal != AppEnum.DataMode.Read);

            var isVisible = newVal != AppEnum.DataMode.Read;
            lbtnResetHistoryOfPresentIllness.Visible = isVisible;
            lbtnResetPastMedicalHistory.Visible = isVisible;
            lbtnResetPhysicalExamination.Visible = isVisible;
            lbtnResetAncillaryExamination.Visible = isVisible;
            lbtnResetFinalDiag.Visible = isVisible;
            lbtnResetMedicalProcedures.Visible = isVisible;
            lbtnResetPrescription.Visible = isVisible;
            lbtnResetResumeMedisProcedure.Visible = isVisible;
        }
        protected override void OnMenuNewClick()
        {
            var val = cboSRDischargeMethod.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, AppConstant.RegistrationType.InPatient);
            ComboBox.SelectedValue(cboSRDischargeMethod, val);

            val = cboSRDischargeCondition.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, AppConstant.RegistrationType.InPatient);
            ComboBox.SelectedValue(cboSRDischargeCondition, val);

            val = cboSRReferReason.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRReferReason, AppEnum.StandardReference.ReferReason);
            ComboBox.SelectedValue(cboSRReferReason, val);

            if (txtDischargeDate.SelectedDate == null)
            {
                var timeNow = (new DateTime()).NowAtSqlServer();
                txtDischargeDate.SelectedDate = timeNow.Date;
                txtDischargeTime.SelectedDate = timeNow;

                if (string.IsNullOrEmpty(cboTreatingPhysician.Text))
                {
                    ComboBox.SelectedValue(cboTreatingPhysician, ParamedicID);
                }
            }

            var medRes = new MedicalDischargeSummary();
            if (!medRes.LoadByPrimaryKey(RegistrationNo))
            {
                txtPhysicalExamination.Text = Patient.Last.PhysicalExamination(RegistrationNo, FromRegistrationNo, true);
                txtPrescription.Text = PrescriptionHist(MergeRegistrations);
                txtAncillaryExamination.Text = LaboratoryHist(MergeRegistrations);

                finalDiagCtl.ImportWorkDiagnose(RegistrationNo, false);
                resumeMedisProcedureCtl.ImportEpisodeProcedure(RegistrationNo, false);

                txtPastMedicalHistory.Text = Patient.PastMedicalHistory(PatientID);
                txtHistoryOfPresentIllness.Text = Patient.Last.PatientAssessment(RegistrationNo, FromRegistrationNo).Hpi;
                txtMedicalProcedures.Text = MedicalProcedures();

                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                txChiefComplaint.Text = reg.Complaint;
            }


        }
        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            SaveMedicalResume(args);
        }


        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            SaveMedicalResume(args);
        }

        protected override void OnMenuPrintClick(ValidateArgs args, string programID, PrintJobParameterCollection printJobParameters)
        {
            printJobParameters.AddNew("p_RegistrationNo", RegistrationNo);
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
        }

        protected override void OnMenuEditClick()
        {
            var val = cboSRDischargeMethod.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRDischargeMethod, AppEnum.StandardReference.DischargeMethod, AppConstant.RegistrationType.InPatient);
            ComboBox.SelectedValue(cboSRDischargeMethod, val);

            val = cboSRDischargeCondition.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRDischargeCondition, AppEnum.StandardReference.DischargeCondition, AppConstant.RegistrationType.InPatient);
            ComboBox.SelectedValue(cboSRDischargeCondition, val);

            val = cboSRReferReason.SelectedValue;
            StandardReference.InitializeIncludeSpace(cboSRReferReason, AppEnum.StandardReference.ReferReason);
            ComboBox.SelectedValue(cboSRReferReason, val);

            if (txtDischargeDate.SelectedDate == null)
            {
                var timeNow = (new DateTime()).NowAtSqlServer();
                txtDischargeDate.SelectedDate = timeNow.Date;
                txtDischargeTime.SelectedDate = timeNow;

                if (string.IsNullOrEmpty(cboTreatingPhysician.Text))
                {
                    ComboBox.SelectedValue(cboTreatingPhysician, ParamedicID);
                }
            }
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnBeforeMenuNewClick(ValidateArgs args)
        {

        }
        protected override void OnMenuApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuUnApprovalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void OnMenuVoidClick(ValidateArgs args)
        {
        }

        protected override void OnMenuUnVoidClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        protected override void OnMenuRejournalClick(ValidateArgs args)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            return string.Empty;
        }
        public override string OnGetScriptToolBarSaveClicking()
        {
            return string.Empty;
        }
        public override bool OnGetStatusMenuEdit()
        {
            // Dalam mode bisa save dari record baru atau yg sudah ada
            // TODO: Hasil obrolan team internal sci di Timika -> Medical Resume tidak perlu dilock jika reg sudah close, bagaimana dg yg lain
            //if (ParamedicTeam.IsParamedicTeamStatusDpjp(RegistrationNo, AppSession.UserLogin.ParamedicID,
            //    DateTime.Today))
            //{
            //    return true;
            //}            

            if (ParamedicTeam.IsParamedicTeamStatusDpjpOrSharing(RegistrationNo, AppSession.UserLogin.ParamedicID))
            {
                return true;
            }
            return false;
        }

        public override bool OnGetStatusMenuDelete()
        {
            return true;
        }

        public override bool? OnGetStatusMenuApproval()
        {
            return true;
        }

        public override bool OnGetStatusMenuVoid()
        {
            return true;
        }

        protected override void OnInitializeAjaxManager(RadAjaxManager ajaxManager)
        {
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        private void SaveMedicalResume(ValidateArgs args)
        {
            // Save for registration selected in main EMR
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);


            using (var trans = new esTransactionScope())
            {
                finalDiagCtl.Save(args);
                if (args.IsCancel)
                    return;

                // Update registration, jangan update DischargeDate di registration krn akan dianggap pasien sudah pulang padahal pada tahap ini 
                // pasien belum benar2 pulang krn masih harus menyelesaikan transaksi pembayaran

                SaveMedicalDischargeSummary();
                SaveReferExternal();
                SaveRegistrationInfoMedic(reg.RegistrationNo, reg.ServiceUnitID);
                SavePrescriptionHome();
                SavePlanControl();
                //epDiagCtl.Save(RegistrationNo);

                resumeMedisProcedureCtl.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }

        }

        private void SavePlanControl()
        {
            var oplan = controlPlanCtl.GetControlPlan();
            var ent = new MedicalDischargeSummaryByNurse();
            if (!ent.LoadByPrimaryKey(RegistrationNo))
            {
                if (oplan.Items.Count > 0)
                {
                    ent.RegistrationNo = RegistrationNo;
                    ent.ControlPlan = JsonConvert.SerializeObject(oplan);
                    ent.Save();
                }
            }
            else
            {
                ent.ControlPlan = JsonConvert.SerializeObject(oplan);
                ent.Save();
            }
        }

        private void SavePrescriptionHome()
        {
            var dtbBroughtHome = (DataTable)Session["DrugBroughtHome"];
            if (dtbBroughtHome != null)
                foreach (DataRow row in dtbBroughtHome.Rows)
                {
                    if (row["IsBroughtHome"] != row["IsBroughtHomeOri"])
                    {
                        var nmd = new MedicationReceive();
                        if (nmd.LoadByPrimaryKey(row["MedicationReceiveNo"].ToInt()))
                        {
                            nmd.IsBroughtHome = Convert.ToBoolean(row["IsBroughtHome"]);
                            nmd.Save();
                        }
                    }
                }
        }

        private void SaveRegistrationInfoMedic(string refNo, string serviceUnitID)
        {
            var ent = new RegistrationInfoMedic();
            var qr = new RegistrationInfoMedicQuery();
            qr.Where(qr.RegistrationNo == RegistrationNo, qr.SRMedicalNotesInputType == "MDS");
            qr.es.Top = 1;

            ent.Load(qr);

            if (string.IsNullOrEmpty(ent.RegistrationInfoMedicID))
            {
                var autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.RegInfoMedicNo);
                ent.RegistrationInfoMedicID = autoNumber.LastCompleteNumber;
                autoNumber.Save();

                ent.RegistrationNo = RegistrationNo;

                ent.SRMedicalNotesInputType = "MDS";
                ent.ServiceUnitID = serviceUnitID;
                ent.ParamedicID = ParamedicID;
            }

            ent.Info1 = string.Format("Medical Discharge Summary");
            ent.Info2 = string.Empty;
            ent.Info3 = string.Empty;
            ent.Info4 = string.Empty;
            ent.IsPRMRJ = true;
            var date = DateTime.Now;
            ent.DateTimeInfo = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
            ent.ReferenceNo = refNo;
            ent.ReferenceType = "MDS";
            ent.Save();
        }

        private void SaveMedicalDischargeSummary()
        {
            var medsum = new MedicalDischargeSummary();
            if (!medsum.LoadByPrimaryKey(RegistrationNo))
            {
                medsum.AddNew();
                medsum.RegistrationNo = RegistrationNo;
            }

            medsum.SRDischargeCondition = cboSRDischargeCondition.SelectedValue;
            medsum.SRDischargeMethod = cboSRDischargeMethod.SelectedValue;
            medsum.ParamedicID = cboTreatingPhysician.SelectedValue;
            medsum.ParamedicName = txtTreatingPhysicianName.Text;
            medsum.SRUnitIntended = cboSRUnitIntended.SelectedValue;

            medsum.TreatmentIndications = txtTreatmentIndications.Text;
            medsum.AncillaryExam = txtAncillaryExamination.Text;
            medsum.MedicalProcedures = txtMedicalProcedures.Text;
            //medsum.ProcedureID = cboProcedureID.SelectedValue;
            medsum.ProcedureName = txtProcedureName.Text;
            medsum.Medications = txtPrescription.Text;
            medsum.AncillaryExam = txtAncillaryExamination.Text;
            medsum.PhysicalExam = txtPhysicalExamination.Text;
            medsum.ChiefComplaint = txChiefComplaint.Text;
            medsum.HistOfPresentIllness = txtHistoryOfPresentIllness.Text;
            medsum.MedicalProcedures = txtMedicalProcedures.Text;
            medsum.PastMedicalHistory = txtPastMedicalHistory.Text;
            medsum.DischargeDate = txtDischargeDate.SelectedDate;
            medsum.DischargeTime = txtDischargeTime.SelectedDate.Value.ToString("HH:mm");
            medsum.SuggestionFollowUp = txtSuggestionFollowUp.Text;
            medsum.Prognosis = txtPrognosis.Text;

            medsum.Save();
        }

        private void SaveReferExternal()
        {
            // External Referral
            // DischargeMethod -> I02	DIRUJUK
            var isExist = false;
            var refExt = new ReferExternal();
            isExist = refExt.LoadByPrimaryKey(RegistrationNo);

            if (!isExist && cboSRDischargeMethod.SelectedValue == "I02")
            {
                isExist = true;
                refExt.AddNew();
                refExt.RegistrationNo = RegistrationNo;
            }

            if (isExist)
            {
                // Simpan saja kalau sudah terlanjur ada
                refExt.ReferralID = cboReferralID.SelectedValue;
                refExt.SRReferReason = cboSRReferReason.SelectedValue;
                refExt.OtherInformation = txtOtherInformation.Text;
                refExt.ReferralAgreedBy = txtReferralAgreedBy.Text;
                refExt.ReferralAgreedTime = txtReferralAgreedTime.SelectedDate;
                refExt.Save();
            }

        }

        #region Medication Reconciliaion
        protected void grdPrescription_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "BroughtHomeAll" || e.CommandName == "NotBroughtHomeAll")
            {
                var dtbBroughtHome = (DataTable)Session["DrugBroughtHome"];
                foreach (DataRow row in dtbBroughtHome.Rows)
                {
                    row["IsBroughtHome"] = e.CommandName == "BroughtHomeAll";
                }

                var grd = (RadGrid)source;
                grd.Rebind();
            }
            else if (e.CommandName == "BroughtHome" || e.CommandName == "NotBroughtHome")
            {
                var medicationReceiveNo = Convert.ToDecimal(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MedicationReceiveNo"]).ToInt();
                var dtbBroughtHome = (DataTable)Session["DrugBroughtHome"];
                foreach (DataRow row in dtbBroughtHome.Rows)
                {
                    if (row["MedicationReceiveNo"].ToInt() == medicationReceiveNo)
                    {
                        row["IsBroughtHome"] = e.CommandName == "BroughtHome";
                        grdPrescription.Rebind();
                        break;
                    }
                }
            }
        }
        protected void grdPrescription_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (DataModeCurrent == AppEnum.DataMode.Read) return;

            if (e.Item is GridDataItem)
            {
                var item = (GridDataItem)e.Item;
                var isSelected = true.Equals(item.GetDataKeyValue("IsBroughtHome"));
                if (isSelected)
                {
                    item.Style.Add(HtmlTextWriterStyle.Color, "blue");
                }
            }


        }
        protected void grdPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || Session["DrugBroughtHome"] == null)
            {
                Session["DrugBroughtHome"] = MedicationReceiveDataTable(RegistrationNo, FromRegistrationNo);
            }

            grdPrescription.DataSource = Session["DrugBroughtHome"];
            grdPrescription.Columns[0].Visible = (DataModeCurrent != AppEnum.DataMode.Read);
        }

        private DataTable MedicationReceiveDataTable(string registrationNo, string fromRegistrationNo)
        {
            var query = new MedicationReceiveQuery("a");
            var cm = new ConsumeMethodQuery("cm");
            query.LeftJoin(cm).On(query.SRConsumeMethod == cm.SRConsumeMethod);

            var patrec = new MedicationReceiveFromPatientQuery("b");
            query.LeftJoin(patrec).On(query.MedicationReceiveNo == patrec.MedicationReceiveNo);

            query.Select(query, patrec.Condition, patrec.ExpireDate, patrec.ApprovedByParamedicID, patrec.LastConsumeDateTime, cm.SRConsumeMethodName,
                query.IsBroughtHome.As("IsBroughtHomeOri"));

            query.Where(query.IsVoid != true,
                query.BalanceQty > 0,
                query.IsContinue == true,
                query.Or(query.RegistrationNo == fromRegistrationNo, query.RegistrationNo == registrationNo));

            if (DataModeCurrent == AppEnum.DataMode.Read)
                query.Where(query.IsBroughtHome == true);

            query.OrderBy(query.MedicationReceiveNo.Descending);
            var dtb = query.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                row["ItemDescription"] = row["ItemDescription"].ToString().Replace(Environment.NewLine, "<br>");
            }

            return dtb;
        }


        #endregion

        //protected void cboProcedureID_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    var entity = new Procedure();
        //    if (entity.LoadByPrimaryKey(cboProcedureID.SelectedValue)) txtProcedureName.Text = entity.ProcedureName;

        //}

        #region Reset to History Value
        protected void lbtnResetPhysicalExamination_OnClick(object sender, EventArgs e)
        {
            txtPhysicalExamination.Text = Patient.Last.PhysicalExamination(RegistrationNo, FromRegistrationNo, true);
        }

        protected void lbtnResetPrescription_OnClick(object sender, EventArgs e)
        {
            txtPrescription.Text = PrescriptionHist(MergeRegistrations);
        }

        protected void lbtnResetAncillaryExamination_OnClick(object sender, EventArgs e)
        {
            txtAncillaryExamination.Text = LaboratoryHist(MergeRegistrations);
        }

        protected void lbtnResetPastMedicalHistory_OnClick(object sender, EventArgs e)
        {
            txtPastMedicalHistory.Text = Patient.PastMedicalHistory(PatientID);
        }

        protected void lbtnResetHistoryOfPresentIllness_OnClick(object sender, EventArgs e)
        {
            txtHistoryOfPresentIllness.Text = Patient.Last.PatientAssessment(RegistrationNo, FromRegistrationNo).Hpi;
        }

        protected void lbtnResetMedicalProcedures_OnClick(object sender, EventArgs e)
        {
            txtMedicalProcedures.Text = MedicalProcedures();
        }

        protected void lbtnResetFinalDiag_OnClick(object sender, EventArgs e)
        {
            finalDiagCtl.ImportWorkDiagnose(RegistrationNo, true);
        }

        protected void lbtnResetResumeMedisProcedure_OnClick(object sender, EventArgs e)
        {
            resumeMedisProcedureCtl.ImportEpisodeProcedure(RegistrationNo, true);
        }

        #endregion



    }
}
