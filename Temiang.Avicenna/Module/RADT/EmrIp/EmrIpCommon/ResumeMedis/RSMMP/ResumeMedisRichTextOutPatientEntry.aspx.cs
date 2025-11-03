using System;
using System.Collections.Generic;
using System.Web.UI;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using System.Linq;

namespace Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.ResumeMedis.RSMMP
{
    /// <summary>
    /// Discharge Summary New Version 
    /// </summary>
    /// <remarks>
    /// New Fiture :
    /// - Use RadEdit (RichText)
    /// - Lookup select Prescription History
    /// - Lookup select Lab History
    /// - Localist
    /// </remarks>
    /// Created By: Diky
    /// Create Start: 
    /// ======================
    public partial class ResumeMedisRichTextOutPatientEntry : BasePageDialogEntry
    {
        private bool IsCallFromCaseMix => Request.QueryString["csmix"] == "1";
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicMedicalRecord;

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
                //StandardReference.InitializeIncludeSpace(cboSRUnitIntended, AppEnum.StandardReference.UnitIntended);
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
                    if (!IsCallFromCaseMix)
                    {
                        var args = new ValidateArgs();
                        ForceToNewMode(args);
                    }
                    else
                        ToolBarMenuEdit.Enabled = false;
                }
            }

            base.OnLoadComplete(e);
        }

        #region override method
        protected override void OnPopulateEntryControl(ValidateArgs args)
        {
            var reg = RegistrationCurrent;
            if (reg == null)
                return;

            //txtRegistrationDate.Text = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.Date);
            txtRegistrationDate.SelectedDate = reg.RegistrationDate;

            var medRes = new MedicalDischargeSummary();
            if (medRes.LoadByPrimaryKey(RegistrationNo))
            {
                if (medRes.DocumentDate != null)
                    txtRegistrationDate.SelectedDate = medRes.DocumentDate;



                cboTreatingPhysician.SelectedValue = medRes.ParamedicID;
                txtTreatingPhysicianName.Text = medRes.ParamedicName;



                edtPhysicalExamination.Content = medRes.PhysicalExam;
                edtPrescription.Content = medRes.Medications;
                edtLab.Content = medRes.AncillaryExam;
                edtAncillaryExamOther.Content = medRes.AncillaryExamOther;
                edtChiefComplaint.Content = medRes.ChiefComplaint;
                edtHistoryOfPresentIllness.Content = medRes.HistOfPresentIllness;
                //ComboBox.PopulateWithOneProcedure(cboProcedureID, medRes.ProcedureID);
                txtProcedureName.Text = medRes.ProcedureName;
                edtSuggestionFollowUp.Content = medRes.SuggestionFollowUp;


                //// External Referral
                //// DischargeMethod -> I02	DIRUJUK
                //if (cboSRDischargeMethod.SelectedValue == "I02")
                //{
                //    var refExt = new ReferExternal();
                //    if (!refExt.LoadByPrimaryKey(RegistrationNo)) return;
                //    ComboBox.PopulateWithOneRow(cboReferralID, refExt.ReferralID, Enums.EntityClassName.Referral);
                //    StandardReference.InitializeWithOneRow(cboSRReferReason, AppEnum.StandardReference.ReferReason, refExt.SRReferReason);
                //    edtOtherInformation.Content = refExt.OtherInformation;
                //    txtReferralAgreedBy.Text = refExt.ReferralAgreedBy;
                //    txtReferralAgreedTime.SelectedDate = refExt.ReferralAgreedTime;
                //}

                var dcDate = reg.RegistrationDate.Value;
                var dcTime = reg.RegistrationTime.Split(':');
                grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, String.Empty, false, new DateTime(dcDate.Year, dcDate.Month, dcDate.Day, dcTime[0].ToInt(), dcTime[1].ToInt(), 0));
            }
            else
                grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, String.Empty, false, DateTime.Now);


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


            var btn = new RadToolBarButton("Medical Discharge Summary Outpatient")
            {
                Value = string.Format("rpt_{0}", "SLP.01.0089b"),
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
            strb.AppendLine("<ul>");
            foreach (DataRow row in dtbPresc.Rows)
            {
                strb.AppendFormat("<li> {0} ({1} {2} {3})</li>",
                    row["ItemName"], row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"]);
            }

            // Obat Racikan
            query = PrescriptionItemNameList(mergeRegistrations, true);
            dtbPresc = query.LoadDataTable();
            foreach (DataRow row in dtbPresc.Rows)
            {
                var consumeMethod = string.Format("{0} {1} {2}", row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"]);
                var itemDescription = PrescriptionItemCompound(row["PrescriptionNo"].ToString(), row["SequenceNo"].ToString(), consumeMethod);
                strb.AppendFormat("{0}", itemDescription);
            }
            strb.AppendLine("</ul>");
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

            var itemProduct = new ItemProductMedicQuery("ip");
            query.InnerJoin(itemProduct).On(query.ItemID == itemProduct.ItemID);

            var itemIntervention = new ItemQuery("int");
            query.LeftJoin(itemIntervention).On(query.ItemInterventionID == itemIntervention.ItemID);

            var itemProductInt = new ItemProductMedicQuery("ipi");
            query.LeftJoin(itemProductInt).On(query.ItemInterventionID == itemProductInt.ItemID);


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
            // Hanya tipe medication
            query.Where(query.Or(itemProductInt.IsMedication == true, query.And(itemProductInt.IsMedication.IsNull(), itemProduct.IsMedication == true)));
            query.Where(qrItem.ItemGroupID.In("FM.01", "FM.02", "FM.03", "FM.07", "FM.09"));
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
                    sbItem.AppendFormat("<li>{0} @{1} {2} ({3}){4}</li>", itemName, row["DosageQty"], row["SRDosageUnit"], consumeMethod, Environment.NewLine);
                    sbItem.AppendLine("<ul>");
                }
                else
                {
                    sbItem.AppendFormat("<li> {0} @{1} {2}{3}</li>", itemName, row["DosageQty"], row["SRDosageUnit"], Environment.NewLine);

                }
            }
            sbItem.AppendLine("</ul>");
            return sbItem.ToString();
        }

        #region LaboratoryResult
        public static string LabHist(List<string> mergeRegs)
        {
            var isFirstRow = true;
            var strb = new StringBuilder();
            try
            {
                // Lab
                var dtbLab = LaboratoryResult(mergeRegs);
                var orderNo = string.Empty;

                foreach (DataRow row in dtbLab.Rows)
                {
                    if (orderNo != row["OrderLabNo"].ToString())
                    {
                        if (!isFirstRow)
                            strb.AppendLine("</ul>");

                        orderNo = row["OrderLabNo"].ToString();
                        strb.AppendFormat("<strong>Lab No: {0} ({1})</strong>", row["OrderLabNo"], Convert.ToDateTime(row["OrderLabTglOrder"]).ToString(AppConstant.DisplayFormat.Date));
                        strb.AppendLine("<ul>");
                    }
                    if (row["Result"] != null && !string.IsNullOrWhiteSpace(row["Result"].ToString()))
                        strb.AppendFormat("<li> {0}: {1} {2}</li>", row["LabOrderSummary"], row["Result"], row["Satuan"]);
                    else
                        strb.AppendFormat("<li> {0}</li>", row["LabOrderSummary"]);

                    isFirstRow = false;
                }
            }
            catch (Exception e)
            {
            }

            if (!isFirstRow)
                strb.AppendLine("</ul>");
            return strb.ToString();
        }

        public static DataTable LaboratoryResult(List<string> mergeRegs)
        {
            DataTable dtbResult = null;
            if (AppSession.Parameter.IsUsingHisInterop)
                dtbResult = LabHistOrderResultFromInterop(mergeRegs);
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
        #region Interop
        #region VANSLAB
        private static DataTable LastLabOrderVansLab(string transactionNo)
        {
            var hasil = new Temiang.Avicenna.BusinessObject.Interop.VANSLAB.LabHasilQuery("hp");
            hasil.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
            hasil.Where(hasil.NoRegistrasi == transactionNo);

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
            return hasil.LoadDataTable();
        }

        private static DataTable PrevLabOrderVansLab(string transactionNo)
        {
            var hasil = new Temiang.Avicenna.BusinessObject.Interop.VANSLAB.LabHasilQuery("hp");
            hasil.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;
            hasil.Where(hasil.NoRegistrasi == transactionNo, hasil.Flag > string.Empty);

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
            return hasil.LoadDataTable();
        }
        #endregion

        #region SYSMEX
        private static DataTable LastLabOrderSysmex(string transactionNo)
        {
            // Lab terakhir diambil semua
            var hasil = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("hp");
            hasil.Where(hasil.OrderLabNo == transactionNo);

            hasil.Select(
                hasil.OrderLabNo,
                hasil.OrderLabTglOrder,
                hasil.LabOrderCode,
                hasil.LabOrderSummary,
                hasil.Result.As("Result"),
                hasil.Unit.As("Satuan"),
                hasil.StandarValue
            );
            hasil.OrderBy(hasil.DispSeq.Ascending);
            return hasil.LoadDataTable();
        }

        private static DataTable PrevLabOrderSysmex(string transactionNo)
        {
            var hasil = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("hp");
            hasil.Where(hasil.OrderLabNo == transactionNo, hasil.Flag > string.Empty);

            hasil.Select(
                hasil.OrderLabNo,
                hasil.OrderLabTglOrder,
                hasil.LabOrderCode,
                hasil.LabOrderSummary,
                hasil.Result.As("Result"),
                hasil.Unit.As("Satuan"),
                hasil.StandarValue
            );
            hasil.OrderBy(hasil.OrderLabTglOrder.Descending, hasil.OrderLabNo.Descending, hasil.DispSeq.Ascending);
            return hasil.LoadDataTable();
        }
        #endregion

        #region WYNACOM
        private static DataTable LastLabOrderWynacom(string transactionNo)
        {
            // All Result
            string toTransactionNo = string.Format("{0}^ZZZ", transactionNo);
            var qr = new BusinessObject.Interop.Wynakom.OrderedResultsQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
            qr.es.WithNoLock = true;
            //qr.Where(qr.HisRegNo == transactionNo);
            qr.Where(qr.HisRegNo >= transactionNo, qr.HisRegNo < toTransactionNo); // Resultnya bisa bertahap contoh JO220825-00181^005

            qr.Select(
                qr.HisRegNo.As("OrderLabNo"),
                qr.AuthorizationDate.As("OrderLabTglOrder"),
                qr.LisTestId.As("LabOrderCode"),
                qr.TestName.As("LabOrderSummary"),
                qr.Result.As("Result"),
                qr.AuthorizationDate.As("ResultDatetime"),
                qr.TestUnitsName.As("Satuan"),
                qr.ReferenceValue.As("StandarValue")
            );
            qr.OrderBy(qr.Sequence.Ascending);
            return qr.LoadDataTable();
        }

        private static DataTable PrevLabOrderWynacom(string transactionNo)
        {
            // Just not normal
            string toTransactionNo = string.Format("{0}^ZZZ", transactionNo);
            var qr = new BusinessObject.Interop.Wynakom.OrderedResultsQuery("a");
            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
            qr.es.WithNoLock = true;
            //qr.Where(qr.HisRegNo == transactionNo, qr.TestFlagSign > string.Empty);
            qr.Where(qr.HisRegNo >= transactionNo, qr.HisRegNo < toTransactionNo, qr.TestFlagSign > string.Empty); // Resultnya bisa bertahap contoh JO220825-00181^005

            qr.Select(
                qr.HisRegNo.As("OrderLabNo"),
                qr.AuthorizationDate.As("OrderLabTglOrder"),
                qr.LisTestId.As("LabOrderCode"),
                qr.TestName.As("LabOrderSummary"),
                qr.Result.As("Result"),
                qr.AuthorizationDate.As("ResultDatetime"),
                qr.TestUnitsName.As("Satuan"),
                qr.ReferenceValue.As("StandarValue")
            );
            qr.OrderBy(qr.AuthorizationDate.Descending, qr.HisRegNo.Descending, qr.Sequence.Ascending);
            return qr.LoadDataTable();

        }
        #endregion

        private static DataTable LabHistOrderResultFromInterop(List<string> mergeRegs)
        {
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
                DataTable dtb = null;
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                        dtb = LastLabOrderSysmex(tc.TransactionNo);
                        break;
                    case "WYNAKOM":
                        dtb = LastLabOrderWynacom(tc.TransactionNo);
                        break;
                    case "VANSLAB":
                        dtb = LastLabOrderVansLab(tc.TransactionNo);
                        break;
                    default:
                        return null;
                }

                // Lab berikutnya hanya yg tidak normal
                var prevLab = new TransChargesQuery("a");
                prevLab.Select(prevLab.TransactionNo);
                prevLab.Where(prevLab.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID, prevLab.TransactionNo != tc.TransactionNo, prevLab.RegistrationNo.In(mergeRegs));
                prevLab.OrderBy(lastLab.TransactionNo.Descending);
                var dtbPrevLab = prevLab.LoadDataTable();
                DataTable dtbPrevHasil = null;
                foreach (DataRow row in dtbPrevLab.Rows)
                {
                    var transactionNo = row["TransactionNo"].ToString();
                    switch (AppSession.Parameter.LisInterop)
                    {
                        case "SYSMEX":
                            dtbPrevHasil = PrevLabOrderSysmex(transactionNo);
                            break;
                        case "WYNAKOM":
                            dtbPrevHasil = PrevLabOrderWynacom(transactionNo);
                            break;
                        case "VANSLAB":
                            dtbPrevHasil = PrevLabOrderVansLab(transactionNo);
                            break;
                    }

                    // Merge
                    dtb.Merge(dtbPrevHasil);
                }

                // Hapus tipe lab yg Confidential
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                        break;
                    case "WYNAKOM":
                        break;
                    case "VANSLAB":
                        {
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
                            break;
                        }
                }

                return dtb;
            }

            return null;
        }
        #endregion

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

        private string AncillaryExamOtherHist(List<string> mergeRegs)
        {
            // Radiologi
            var qr = new TestResultQuery("tr");
            var order = new TransChargesQuery("hd");
            qr.InnerJoin(order).On(qr.TransactionNo == order.TransactionNo);

            var item = new ItemQuery("i");
            qr.InnerJoin(item).On(qr.ItemID == item.ItemID);

            //var radUnits = new List<string>();
            //radUnits.Add(AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitRadiologyID));
            //radUnits.Add(AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitRadiologyID2));
            //var radOther = AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitRadiologyIDs);
            //if (!string.IsNullOrWhiteSpace(radOther))
            //{
            //    if (radOther.Contains(";"))
            //    {
            //        var radOthers = radOther.Split(';');
            //        foreach (string line in radOthers)
            //        {
            //            radUnits.Add(line);
            //        }
            //    }
            //    else
            //        radUnits.Add(radOther);

            //}

            qr.Where(order.IsOrder == true, order.IsApproved == true, order.RegistrationNo.In(mergeRegs));
            qr.Where(order.ToServiceUnitID != AppParameter.GetParameterValue(AppParameter.ParameterItem.ServiceUnitLaboratoryID)); // Selain Lab

            qr.Select(item.ItemName, qr.TestResult);
            qr.OrderBy(item.ItemName.Ascending);

            var dtbSurgery = qr.LoadDataTable();
            var strb = new StringBuilder();
            foreach (DataRow row in dtbSurgery.Rows)
            {
                strb.AppendFormat("<p><strong>{0}</strong><br />", row["ItemName"]);
                strb.AppendFormat("{0}</p>", row["TestResult"]);
            }

            var surgery = strb.ToString();
            return surgery;
        }

        private string DietHist()
        {
            var query = new DietPatientQuery("a");
            var dit = new DietPatientItemQuery("dit");
            query.InnerJoin(dit).On(query.TransactionNo == dit.TransactionNo);
            var diet = new DietQuery("p");
            query.InnerJoin(diet).On(dit.DietID == diet.DietID);
            query.Where(query.RegistrationNo.In(MergeRegistrations));
            query.Select(diet.DietName, dit.Calorie, dit.Protein, dit.Fat, dit.Carbohydrate, dit.Salt, dit.Notes);
            query.OrderBy(dit.TransactionNo.Ascending, diet.DietName.Ascending);

            var dtbSurgery = query.LoadDataTable();
            var strb = new StringBuilder();
            strb.AppendLine("<ul>");
            foreach (DataRow row in dtbSurgery.Rows)
            {
                strb.AppendFormat("<li>{0} ({5}) Cal:{1:N2}, Pro:{2:N2}, Car:{3:N2}, Salt:{4:N2}</li>",
                    row["DietName"], row["Calorie"], row["Protein"], row["Carbohydrate"],
                    row["Salt"], row["Notes"]);
            }
            strb.AppendLine("</ul>");


            var surgery = strb.ToString();
            return surgery;
        }
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



            finalDiagCtl.Rebind(newVal != AppEnum.DataMode.Read);
            resumeMedisProcedureCtl.Rebind(newVal != AppEnum.DataMode.Read);

            var isVisible = newVal != AppEnum.DataMode.Read;
            lbtnResetHistoryOfPresentIllness.Visible = isVisible;
            lbtnResetPhysicalExamination.Visible = isVisible;
            lbtnResetAncillaryExamOther.Visible = isVisible;
            lbtnResetLab.Visible = isVisible;
            lbtnResetFinalDiag.Visible = isVisible;
            lbtnResetPrescription.Visible = isVisible;
            lbtnResetResumeMedisProcedure.Visible = isVisible;

        }
        protected override void OnMenuNewClick()
        {
            var isMedicalDischargeSummaryDefaultValue = AppParameter.IsYes(AppParameter.ParameterItem.IsMedicalDischargeSummaryDefaultValue);




            //val = cboSRReferReason.SelectedValue;
            //StandardReference.InitializeIncludeSpace(cboSRReferReason, AppEnum.StandardReference.ReferReason);
            //ComboBox.SelectedValue(cboSRReferReason, val);

            if (string.IsNullOrEmpty(cboTreatingPhysician.Text))
            {
                ComboBox.SelectedValue(cboTreatingPhysician, ParamedicID);
            }



            var medRes = new MedicalDischargeSummary();
            if (!medRes.LoadByPrimaryKey(RegistrationNo))
            {
                edtPhysicalExamination.Content = Patient.Last.PhysicalExamination(RegistrationNo, FromRegistrationNo);
                edtPrescription.Content = PrescriptionHist(MergeRegistrations);

                var labHist = LabHist(MergeRegistrations);
                var ancHist = AncillaryExamOtherHist(MergeRegistrations);

                if (isMedicalDischargeSummaryDefaultValue)
                {
                    edtLab.Content = labHist;
                    edtAncillaryExamOther.Content = ancHist;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(labHist))
                        edtLab.Content = "Hasil Terlampir";

                    if (!string.IsNullOrWhiteSpace(ancHist))
                        edtAncillaryExamOther.Content = "Hasil Terlampir";
                }

                finalDiagCtl.ImportWorkDiagnose(RegistrationNo, false);
                resumeMedisProcedureCtl.ImportEpisodeProcedure(RegistrationNo, false);

                edtHistoryOfPresentIllness.Content = Patient.Last.PatientAssessment(RegistrationNo, FromRegistrationNo).Hpi;

                edtChiefComplaint.Content = RegistrationCurrent.Complaint;
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

            //val = cboSRReferReason.SelectedValue;
            //StandardReference.InitializeIncludeSpace(cboSRReferReason, AppEnum.StandardReference.ReferReason);
            //ComboBox.SelectedValue(cboSRReferReason, val);

            if (string.IsNullOrEmpty(cboTreatingPhysician.Text))
            {
                ComboBox.SelectedValue(cboTreatingPhysician, ParamedicID);
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

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsAllPhysicianAllowEditMedicalDischarge))
            {
                var reg = RegistrationCurrent;
                var mergeRegistration = AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
                return ParamedicTeam.IsParamedicInTeam(AppSession.UserLogin.ParamedicID, RegistrationNo, mergeRegistration, reg.ServiceUnitID, reg.SRRegistrationType);
            }
            return ParamedicTeam.IsParamedicTeamStatusDpjpOrSharing(RegistrationNo, AppSession.UserLogin.ParamedicID);
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
            var reg = RegistrationCurrent;

            using (var trans = new esTransactionScope())
            {
                // Update registration, jangan update DischargeDate di registration krn akan dianggap pasien sudah pulang padahal pada tahap ini 
                // pasien belum benar2 pulang krn masih harus menyelesaikan transaksi pembayaran

                SaveMedicalDischargeSummary();
                SaveReferExternal();
                SaveRegistrationInfoMedic(reg.RegistrationNo, reg.ServiceUnitID);
                SavePrescriptionHome();
                SavePlanControl(args);
                if (!args.IsCancel)
                {
                    finalDiagCtl.Save(args);
                    if (args.IsCancel)
                        return;

                    resumeMedisProcedureCtl.Save();

                    //Commit if success, Rollback if failed
                    trans.Complete();
                }
            }

        }

        private void SavePlanControl(ValidateArgs args)
        {
            var oplan = controlPlanCtl.GetControlPlan();

            // Save in appointment
            var pat = new Patient();
            pat.LoadByPrimaryKey(PatientID);
            foreach (Temiang.Avicenna.BusinessObject.JsonField.ControlPlanItem planItem in oplan.Items)
            {
                if (planItem.ControlPlanDateTime > DateTime.Today
                    && !string.IsNullOrEmpty(planItem.ServiceUnitID)
                    && !string.IsNullOrEmpty(planItem.ParamedicID)
                    && string.IsNullOrEmpty(planItem.AppointmentNo))
                {
                    var qSchedule = new ParamedicScheduleDate();
                    if (qSchedule.LoadByPrimaryKey(planItem.ServiceUnitID, planItem.ParamedicID, planItem.ControlPlanDateTime.Year.ToString(), planItem.ControlPlanDateTime.Date))
                    {
                        try
                        {
                            var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
                                planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
                                PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, RegistrationNo);

                            planItem.AppointmentTime = slot["AppointmentTime"].ToString();
                            planItem.AppointmentQue = slot["AppointmentQue"].ToInt();
                            planItem.AppointmentNo = slot["AppointmentNo"].ToString();
                        }
                        catch (Exception ex)
                        {
                            args.MessageText = ex.Message;
                            args.IsCancel = true;
                        }
                    }
                    else
                    {
                        var qSlot = new ServiceUnitParamedic();
                        if (qSlot.LoadByPrimaryKey(planItem.ServiceUnitID, planItem.ParamedicID) && qSlot.IsUsingQue == true)
                        {
                            try
                            {
                                var slot = Temiang.Avicenna.WebService.V1_1.AppointmentWS.AppointmentSetEntityValue(string.Empty, planItem.ServiceUnitID, planItem.ParamedicID,
                                    planItem.ControlPlanDateTime.Date.ToShortDateString(), "AUTO", string.Empty,
                                    PatientID, pat.FirstName, pat.MiddleName, pat.LastName, pat.DateOfBirth.Value.Date.ToShortDateString(), pat.CityOfBirth, pat.Sex,
                                    pat.StreetName, pat.District, pat.City, pat.County, pat.State, pat.ZipCode,
                                    pat.PhoneNo, pat.Email, pat.Ssn, pat.GuarantorID, pat.Notes, AppSession.Parameter.AppointmentStatusOpen,
                                    pat.MobilePhoneNo, "", "", 0, AppSession.UserLogin.UserID, AppSession.Parameter.AppointmentTypeControlPlan, RegistrationNo);

                                planItem.AppointmentTime = slot["AppointmentTime"].ToString();
                                planItem.AppointmentQue = slot["AppointmentQue"].ToInt();
                                planItem.AppointmentNo = slot["AppointmentNo"].ToString();
                            }
                            catch (Exception ex)
                            {
                                args.MessageText = ex.Message;
                                args.IsCancel = true;
                            }
                        }
                    }
                }
            }


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
                medsum = new MedicalDischargeSummary();
                medsum.RegistrationNo = RegistrationNo;
            }


            medsum.ParamedicID = cboTreatingPhysician.SelectedValue;
            medsum.ParamedicName = txtTreatingPhysicianName.Text;



            //medsum.ProcedureID = cboProcedureID.SelectedValue;
            medsum.ProcedureName = txtProcedureName.Text;
            medsum.Medications = edtPrescription.Content;
            medsum.AncillaryExam = edtLab.Content;
            medsum.AncillaryExamOther = edtAncillaryExamOther.Content;
            medsum.PhysicalExam = edtPhysicalExamination.Content;
            medsum.ChiefComplaint = edtChiefComplaint.Content;
            medsum.HistOfPresentIllness = edtHistoryOfPresentIllness.Content;
            medsum.SuggestionFollowUp = edtSuggestionFollowUp.Text;
            medsum.IsRichTextMode = true; // Utk membedakan layar entry dan cetakan krn data yg lama bisa masalah dalam konversinya jika dibuat mode richtext
            medsum.DocumentDate = txtRegistrationDate.SelectedDate;
            medsum.Save();

            // Save Localist / Body Image 
            if (Session["rimBodyImage"] != null)
            {
                var dtbSession = (DataTable)Session["rimBodyImage"];
                foreach (DataRow row in dtbSession.Rows)
                {
                    if (true.Equals(row["IsModified"]))
                    {
                        SaveLocalistStatus(RegistrationNo, row["BodyID"].ToString(),
                            (byte[])row["BodyImage"]);
                    }
                }
            }

            // Menyebabkan error jika user klik gambarnya setelah save
            // TODO: Cari cara release sesion nya spy tidak jadi sampah
            //Session.Remove("rimBodyImage");
            //Session.Remove("rimBodyImage_id");
        }

        private void SaveLocalistStatus(string regNo, string bodyId, byte[] bodyImage)
        {
            var es = new MedicalDischargeSummaryBodyDiagram();
            if (!es.LoadByPrimaryKey(regNo, bodyId))
            {
                es = new MedicalDischargeSummaryBodyDiagram()
                {
                    RegistrationNo = regNo,
                    IsDeleted = false,
                    BodyID = bodyId,
                    CreatedDateTime = DateTime.Now,
                    CreatedByUserID = AppSession.UserLogin.UserID
                };
            }

            es.BodyImage = bodyImage;
            es.Save();
        }

        protected void lvLocalistStatus_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsPostBack || !RegistrationNo.Equals(Session["rimBodyImage_id"]))
            {
                PopulateBodyImageSession();
            }

            var dtbSession = (DataTable)Session["rimBodyImage"];
            lvLocalistStatus.DataSource = dtbSession;
        }

        private void PopulateBodyImageSession()
        {
            var reg = RegistrationCurrent;
            var qrBody = new BodyDiagramQuery("bd");

            var qr = new MedicalDischargeSummaryBodyDiagramQuery("rim");
            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                var qrSubd = new AssessmentTypeBodyDiagramQuery("bsu");

                if (string.IsNullOrEmpty(RegistrationNo))
                    qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationNo == "_");
                else
                    qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationNo == RegistrationNo);

                qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

                var smf = new Smf();
                smf.LoadByPrimaryKey(reg.SmfID ?? string.Empty);

                qr.Where(qrSubd.SRAssessmentType == (smf.SRAssessmentType ?? string.Empty));
            }
            else
            {
                var qrSubd = new BodyDiagramServiceUnitQuery("bsu");

                if (string.IsNullOrEmpty(RegistrationNo))
                    qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationNo == "_");
                else
                    qr.RightJoin(qrSubd).On(qr.BodyID == qrSubd.BodyID && qr.RegistrationNo == RegistrationNo);

                qr.InnerJoin(qrBody).On(qrSubd.BodyID == qrBody.BodyID);

                qr.Where(qrSubd.ServiceUnitID == reg.ServiceUnitID);

            }

            qr.Select(qr.RegistrationNo,
                "<CASE WHEN rim.RegistrationNo IS NULL THEN bd.BodyImage ELSE rim.BodyImage END as BodyImage>",
                qr.LastUpdateByUserID, qr.CreatedDateTime, qr.LastUpdateDateTime,
                "<CASE WHEN rim.RegistrationNo IS NULL THEN 'new' ELSE 'edit' END as EntryMode>",
                qrBody.BodyID, qrBody.BodyName, "<CONVERT(BIT,0) IsModified>");

            var dtb = qr.LoadDataTable();

            // Jangan rubah session kecuali dirubah juga pada page /Module/RADT/Cpoe/Common/LocalistStatus/LocalistStatusEntry.aspx
            Session["rimBodyImage_id"] = RegistrationNo;
            Session["rimBodyImage"] = dtb;
        }

        private void SaveReferExternal()
        {
            // External Referral
            // DischargeMethod -> I02	DIRUJUK
            var isExist = false;
            var refExt = new ReferExternal();
            isExist = refExt.LoadByPrimaryKey(RegistrationNo);


            //if (isExist)
            //{
            //    // Simpan saja kalau sudah terlanjur ada
            //    refExt.ReferralID = cboReferralID.SelectedValue;
            //    refExt.SRReferReason = cboSRReferReason.SelectedValue;
            //    refExt.OtherInformation = edtOtherInformation.Content;
            //    refExt.ReferralAgreedBy = txtReferralAgreedBy.Text;
            //    refExt.ReferralAgreedTime = txtReferralAgreedTime.SelectedDate;
            //    refExt.Save();
            //}

        }

        #region Medication Take Home
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
                query.IsContinue == true,
                query.Or(query.RegistrationNo == fromRegistrationNo, query.RegistrationNo == registrationNo));

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsMedicalDischargeSummaryHomPrescAll))
                query.Where(query.BalanceQty > 0);
            else
            {
                // Hanya ambil yg tipe HOme Prescription
                var tp = new TransPrescriptionQuery("tp");
                query.InnerJoin(tp).On(query.RefTransactionNo == tp.PrescriptionNo);
                query.Where(tp.IsForTakeItHome == true);
            }

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
            edtPhysicalExamination.Content = Patient.Last.PhysicalExamination(RegistrationNo, FromRegistrationNo);
        }

        protected void lbtnResetPrescription_OnClick(object sender, EventArgs e)
        {
            edtPrescription.Content = PrescriptionHist(MergeRegistrations);
        }

        protected void lbtnResetLab_OnClick(object sender, EventArgs e)
        {
            edtLab.Content = LabHist(MergeRegistrations);
        }
        protected void lbtnResetAncillaryExamOther_OnClick(object sender, EventArgs e)
        {
            edtLab.Content = AncillaryExamOtherHist(MergeRegistrations);
        }


        protected void lbtnResetHistoryOfPresentIllness_OnClick(object sender, EventArgs e)
        {
            edtHistoryOfPresentIllness.Content = Patient.Last.PatientAssessment(RegistrationNo, FromRegistrationNo).Hpi;
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
