using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.MainContent
{
    public partial class MedicalHistCtl : BaseMainContentCtl
    {
        const int MaxPrescriptionCount = 20;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GridDiagAndPrescriptionClientID
        {
            get { return grdDiagAndPrescription.ClientID; }
        }


        public String ReferFromRegistrationNo
        {
            set { ViewState["freg"] = value; }
            get { return Convert.ToString(ViewState["freg"]); }
        }
        public RadGrid GridDiagAndPrescription
        {
            get { return grdDiagAndPrescription; }
        }

        protected string RegistrationType
        {
            get
            {
                // AMbil dari main page
                return Request.QueryString["rt"];
            }
        }

        public bool IsPrescriptionAddAble(string registrationNo, bool isPostBack)
        {

            var reg = new Registration();
            reg.LoadByPrimaryKey(registrationNo);

            if (reg.IsClosed ?? false)
                return false;

            if (reg.IsLockVerifiedBilling ?? false)
                return false;

            if (AppParameter.IsYes(AppParameter.ParameterItem.IsBillingEmrAddButtonEnabled) && reg.IsHoldTransactionEntry == true)
                return false;

            // Jika belum ada diagnosa maka tidak boleh entry resep utk non rawat inap
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionNonIPMustDiagnoseMainFirst) && reg.SRRegistrationType != AppConstant.RegistrationType.InPatient)
            {
                var epd = new EpisodeDiagnose();
                epd.Query.Where(epd.Query.RegistrationNo == registrationNo,
                    epd.Query.SRDiagnoseType == AppSession.Parameter.DiagnoseTypeMain);
                epd.Query.es.Top = 1;
                if (!epd.Query.Load())
                    return false;
            }

            // Check Asesmen / SOAP
            if ((AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionIprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
                || (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionOprMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.OutPatient)
                || (AppParameter.IsYes(AppParameter.ParameterItem.IsPrescriptionEmrMustAssessmentFirst) && reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient)
                )
            {
                var soap = new RegistrationInfoMedic();
                soap.Query.Where(soap.Query.RegistrationNo == registrationNo);
                soap.Query.es.Top = 1;
                if (!soap.Query.Load())
                    return false;
            }

            return (AppSession.UserLogin.SRUserType == AppUser.UserType.Nurse ||
                     IsUserInParamedicTeam(registrationNo, isPostBack, reg.ServiceUnitID));

        }

        public void GridDiagnoseHistDatabind()
        {
            grdDiagAndPrescription.DataBind();
        }
        #region Patient Diagnose History

        protected void grdDiagAndPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!string.IsNullOrEmpty(PatientID))
            {
                grdDiagAndPrescription.DataSource = DiagnoseHistDataTable();
            }
        }

        private DataTable DiagnoseHistDataTable()
        {
            using (var tr = new esTransactionScope()) // Supaya dalam 1 connection proses
            {
                var qr = new RegistrationQuery("r");
                var qrPar = new ParamedicQuery("pr");
                qr.InnerJoin(qrPar).On(qr.ParamedicID == qrPar.ParamedicID);

                qr.Select(qr.RegistrationNo, qr.RegistrationDate, qr.RegistrationDateTime, qrPar.ParamedicName,
                    qr.IsConsul, qr.IsNewPatient, qr.SRRegistrationType);
                if (RegistrationType == AppConstant.RegistrationType.InPatient)
                {
                    qr.Where(qr.RegistrationNo.In(AppCache.RelatedRegistrations(IsPostBack, RegistrationNo)));
                }
                else
                {
                    //if (PatientRelateds.Count == 1)
                    //    qr.Where(qr.PatientID == PatientID);
                    //else
                    //    qr.Where(qr.PatientID.In(PatientRelateds));

                    // Non Inpatient ambil 5 registrasi terakhir (Handono 2022-09)
                    var regCount = AppSession.Parameter.GetParameterValue(AppParameter.ParameterItem.EmrHistoryRegistrationCount).ToInt();
                    var lastRegNos = Patient.Last.RegistrationNos(PatientID, regCount, RegistrationNo);
                    if (lastRegNos.Count == 1)
                        qr.Where(qr.RegistrationNo == lastRegNos[0]);
                    else
                        qr.Where(qr.RegistrationNo.In(lastRegNos));
                }

                qr.Where(qr.IsVoid == false);

                qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationTime.Descending);

                var dtbEpisodeDiagnose = qr.LoadDataTable();

                dtbEpisodeDiagnose.Columns.Add(new DataColumn("Diagnosis", typeof(string)));
                dtbEpisodeDiagnose.Columns.Add(new DataColumn("ICD10", typeof(string)));
                dtbEpisodeDiagnose.Columns.Add(new DataColumn("Prescription", typeof(string)));

                int prescriptionCount = 0;
                // Get detail Prescription & diagnose
                foreach (DataRow rowHd in dtbEpisodeDiagnose.Rows)
                {

                    if (prescriptionCount >= MaxPrescriptionCount) // Batasi supaya tidak lola
                    {
                        rowHd.Delete();
                        continue;
                    }

                    var regNo = rowHd["RegistrationNo"].ToString();
                    rowHd["Prescription"] = PrescriptionDetailInHTML(regNo, this.IsUserEditAble, false,
                        RegistrationType, ref prescriptionCount);

                    if (string.IsNullOrWhiteSpace(rowHd["Prescription"].ToString()))
                    {
                        rowHd.Delete();
                        continue;
                    }

                    // Untuk Reg bersangkutan tidak perlu dimunculkan krn sudah ada dibagian lain
                    if (regNo != RegistrationNo)
                    {
                        // Diagnose entri oleh dokter
                        var assesQr = new PatientAssessmentQuery();
                        assesQr.Where(assesQr.RegistrationNo == regNo);
                        assesQr.es.Top = 1;
                        assesQr.Select(assesQr.Diagnose);
                        var asses = new PatientAssessment();
                        if (asses.Load(assesQr)) // Cari di PatientAssessment
                        {
                            rowHd["Diagnosis"] = asses.Diagnose;
                        }
                        else
                        {
                            // Jika tidak ada berarti bisa jadi tidak menggunakan Assessment
                            var rimQr = new RegistrationInfoMedicQuery();
                            rimQr.Where(rimQr.RegistrationNo == regNo);
                            rimQr.es.Top = 1;
                            rimQr.Select(rimQr.Info3);
                            var rim = new RegistrationInfoMedic();
                            if (rim.Load(rimQr))
                            {
                                rowHd["Diagnosis"] = rim.Info3;
                            }
                        }

                        var icd10 = string.Empty;
                        var finalDiag = EpisodeDiagnose.DiagnoseSummaryHtml(regNo);
                        if (rowHd["SRRegistrationType"].ToString() == AppConstant.RegistrationType.InPatient)
                        {
                            // Munculkan info Work Diagnose & Final Diagnose
                            var workDiag = RegistrationInfoMedicDiagnose.DiagnoseSummaryHtml(regNo);
                            icd10 = string.Concat("<fieldset><legend>Work Diagnose</legend>", rowHd["Diagnosis"],
                                "<br/>", workDiag, "</fieldset>", "<fieldset><legend>Final Diagnose</legend>",
                                finalDiag, "</fieldset>");

                        }
                        else
                        {
                            // Munculkan hanya Final Diagnose
                            icd10 = string.Concat("<fieldset><legend>Final Diagnose</legend>", rowHd["Diagnosis"],
                                "<br/>", finalDiag, "</fieldset>");
                        }

                        rowHd["ICD10"] = icd10;
                    }

                }

                dtbEpisodeDiagnose.AcceptChanges();
                return dtbEpisodeDiagnose;
            }
        }

        public static string PrescriptionDetailInHTML(string registrationNo, bool isUserEditAble, bool isForProgressHist, string registrationType, ref int prescriptionCount)
        {
            if (prescriptionCount >= MaxPrescriptionCount) // Batasi supaya tidak lola
                return string.Empty;

            // Migration to SP for optimizing query (Handono 2024010-17)

            //var prescItemQr = new TransPrescriptionItemQuery("a");
            //var prescQr = new TransPrescriptionQuery("b");
            //var item = new ItemQuery("c");
            //var itemIntervention = new ItemQuery("ii");
            //var consume = new ConsumeMethodQuery("e");
            //var emb = new EmbalaceQuery("g");
            ////var oriconsume = new ConsumeMethodQuery("h");
            //var acdcpc = new AppStandardReferenceItemQuery("acdcpc");
            //var usr = new AppUserQuery("u");

            //prescQr.Select(
            //    prescQr.PrescriptionNo,
            //    prescQr.IsApproval,
            //    prescQr.IsVoid,
            //    prescQr.CreatedByUserID,
            //    usr.UserName.As("CreatedByUserName"),
            //    prescQr.CreatedDateTime,
            //    prescQr.VerifiedByUserID,
            //    prescQr.VerifiedDateTime,
            //    prescQr.CompleteDateTime,
            //    prescQr.DeliverDateTime,
            //    prescQr.IsUnitDosePrescription,
            //    prescItemQr.SequenceNo,
            //    prescQr.PrescriptionDate,
            //    prescQr.ParamedicID,
            //    item.ItemName,
            //    itemIntervention.ItemName.As("ItemNameIntervention"),
            //    //@"<ISNULL(a.OriResultQty, a.ResultQty) AS ResultQty>",
            //    //@"<ISNULL(a.OriSRItemUnit, a.SRItemUnit) AS SRItemUnit>",
            //    prescItemQr.ResultQty,
            //    prescItemQr.SRItemUnit,
            //    //@"<ISNULL(h.SRConsumeMethodName, e.SRConsumeMethodName)+ ' ' + COALESCE(acdcpc.ItemName,'') AS SRConsumeMethodName>",
            //    @"<e.SRConsumeMethodName+ ' ' + COALESCE(acdcpc.ItemName,'') AS SRConsumeMethodName>",
            //    prescItemQr.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
            //    prescItemQr.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
            //    emb.EmbalaceLabel,
            //    //@"<ISNULL(a.OriDosageQty, a.DosageQty) AS DosageQty>",
            //    //@"<ISNULL(a.OriSRDosageUnit, a.SRDosageUnit) AS SRDosageUnit>",
            //    prescItemQr.DosageQty,
            //    prescItemQr.SRDosageUnit,
            //    prescItemQr.EmbalaceQty,
            //    prescQr.Note,
            //    //@"<ISNULL(a.OriConsumeQty, a.ConsumeQty) AS ConsumeQty>",
            //    //@"<ISNULL(a.OriSRConsumeUnit, a.SRConsumeUnit) AS SRConsumeUnit>",
            //    prescItemQr.ConsumeQty,
            //    prescItemQr.SRConsumeUnit,
            //    "<(a.ParentNo + a.SequenceNo) as OrderKey>",
            //    prescItemQr.LineAmount,
            //    prescItemQr.Notes
            //    );

            //prescQr.InnerJoin(prescItemQr).On(prescItemQr.PrescriptionNo == prescQr.PrescriptionNo);
            //prescQr.LeftJoin(usr).On(prescQr.CreatedByUserID == usr.UserID);
            //prescQr.LeftJoin(item).On(prescItemQr.ItemID == item.ItemID);
            //prescQr.LeftJoin(itemIntervention).On(prescItemQr.ItemInterventionID == itemIntervention.ItemID);
            //prescQr.LeftJoin(consume).On(prescItemQr.SRConsumeMethod == consume.SRConsumeMethod);
            ////prescQr.LeftJoin(oriconsume).On(prescItemQr.OriSRConsumeMethod == oriconsume.SRConsumeMethod);
            //prescQr.LeftJoin(emb).On(prescItemQr.EmbalaceID == emb.EmbalaceID);
            //prescQr.LeftJoin(acdcpc).On(prescItemQr.Acpcdc == acdcpc.ItemID &&
            //                          acdcpc.StandardReferenceID == AppEnum.StandardReference.MedicationConsume);

            //prescQr.Where(prescQr.RegistrationNo == registrationNo, prescQr.IsVoid == false);
            //prescQr.OrderBy(prescQr.PrescriptionDate.Descending, prescQr.PrescriptionNo.Descending);
            //prescQr.OrderBy("OrderKey", esOrderByDirection.Ascending);

            //var table = prescQr.LoadDataTable();


            var pars = new esParameters();
            pars.Add("p_RegistrationNo", registrationNo);
            var table = BusinessObject.Common.Utils.LoadDataTableFromStoreProcedure("sphis_Emr_MedicationHist_PrescriptionDetail", pars, 0);

            if (table.Rows.Count == 0) return string.Empty;

            // Ambil list PrescriptionNo
            var prescs = new List<string>();
            DataTable dtbPrescriptionNo = null;
            if (isForProgressHist)
            {
                dtbPrescriptionNo = table.DefaultView.ToTable(true, "PrescriptionNo", "CompleteDateTime").Select(null, "CompleteDateTime DESC, PrescriptionNo DESC").CopyToDataTable();
            }
            else
            {
                dtbPrescriptionNo = table.DefaultView.ToTable(true, "PrescriptionNo", "CompleteDateTime", "CreatedDateTime").Select(null, "CreatedDateTime DESC, PrescriptionNo DESC").CopyToDataTable();
            }

            foreach (DataRow row in dtbPrescriptionNo.Rows)
            {
                prescs.Add(row["PrescriptionNo"].ToString());
            }

            var displayTotal = AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor;

            var prescriptionHeader = "";
            var sbPresciption = new StringBuilder();
            var urlRoot = Helper.UrlRoot();

            foreach (var prescriptionNo in prescs)
            {
                int i = 0;
                double total = 0;
                var sbItem = new StringBuilder();
                var dtbPresc = table.DefaultView.ToTable().Select(string.Format("PrescriptionNo = '{0}'", prescriptionNo), "OrderKey ASC").CopyToDataTable();

                //foreach (DataRow row in table.AsEnumerable().Where(t => t.Field<string>("PrescriptionNo") == prescriptionNo))
                foreach (DataRow row in dtbPresc.Rows)
                {
                    if (i == 0)
                    {
                        prescriptionCount++;
                        prescriptionHeader = CreateHeader(registrationNo, isUserEditAble, isForProgressHist, registrationType, urlRoot, sbItem, row);

                        // Start detail Item
                        sbItem.Append("<table style=\"width: 100%;\">");
                    }
                    i++;

                    if (row["SequenceNo"] == DBNull.Value) continue;

                    var itemName = string.Empty;
                    if (row["ItemNameIntervention"] != null && !string.IsNullOrEmpty(row["ItemNameIntervention"].ToString()))
                        itemName = string.Format("<del style='color: gray;'>{0}</del> {1}", row["ItemName"], row["ItemNameIntervention"]);
                    else
                        itemName = row["ItemName"].ToString();

                    if (!Convert.ToBoolean(row["IsCompound"]))
                    {
                        sbItem.Append("<tr><td style=\"vertical-align: top;\"><b>R/</b>&nbsp;</td>");
                        sbItem.AppendFormat("<td>{0} {1} {2} ({3} @{4} {5} {6})</td></tr>",
                            itemName,
                            row["ResultQty"],
                            //StandardReference.GetItemName(AppEnum.StandardReference.ItemUnit, row["SRItemUnit"].ToString()),
                            row["SRItemUnitName"],
                            row["SRConsumeMethodName"],
                            row["ConsumeQty"],
                            //StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRConsumeUnit"].ToString()),
                            row["SRConsumeUnitName"],
                            row["Notes"]);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row["IsRFlag"]))
                        {
                            sbItem.Append("<tr><td style=\"vertical-align: top;\"><b>R/</b>&nbsp;</td>");
                            sbItem.AppendFormat("<td>{0} {1} {2} @ {3} {4} ({5} @ {6} {7} {8})</td><td></td></tr>",
                                itemName,
                                row["EmbalaceQty"],
                                row["EmbalaceLabel"],
                                row["DosageQty"],
                                //StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRDosageUnit"].ToString()),
                                row["SRDosageUnitName"],
                                row["SRConsumeMethodName"],
                                row["ConsumeQty"],
                                //StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRConsumeUnit"].ToString()),
                                row["SRConsumeUnitName"],
                                row["Notes"]);
                        }
                        else
                        {
                            sbItem.Append("<tr><td>&nbsp;</td>");
                            sbItem.AppendFormat("<td>{0} {1} {2} @ {3} {4} {5}</td><td></td></tr>",
                                itemName,
                                row["EmbalaceQty"],
                                row["EmbalaceLabel"],
                                row["DosageQty"],
                                //StandardReference.GetItemName(AppEnum.StandardReference.DosageUnit, row["SRDosageUnit"].ToString()),
                                row["SRDosageUnitName"],
                                row["Notes"]);
                        }
                    }
                    total += Convert.ToDouble(row["LineAmount"]);
                }

                if (displayTotal)
                    sbItem.AppendFormat("<tr><td></td><td><b>{0}</b></td><td></td></tr>", " (Rp. " + string.Format("{0:n2}", (total)) + ")");

                // End detail Item
                sbItem.Append("</table>");

                sbPresciption.AppendFormat("<fieldset><legend>{0}</legend>{1}</fieldset>", prescriptionHeader, sbItem);

                if (prescriptionCount >= MaxPrescriptionCount) // Batasi supaya tidak lola
                {

                    var mnuShowMore = string.Format(
                                        "<a href=\"#\" onclick=\"javascript:openPrescriptionHist('{0}', '{1}'); return false;\"><img src=\"{2}/Images/Toolbar/views16.png\"  />&nbsp;Show More</a>",
                                        registrationNo, registrationType, Helper.UrlRoot());

                    sbPresciption.AppendFormat("<br/>&nbsp;{0} ...<br/><br/>", mnuShowMore);
                    break;
                }
            }
            return sbPresciption.ToString();
        }

        private static string CreateHeader(string registrationNo, bool isUserEditAble, bool isForProgressHist, string registrationType, string urlRoot, StringBuilder sbItem, DataRow row)
        {
            string prescriptionHeader;
            if (row["CreatedDateTime"] == DBNull.Value)
                prescriptionHeader = string.Format("{0}", row["PrescriptionNo"]);
            else
                prescriptionHeader = string.Format("[{0}] @ {1} by {2}", row["PrescriptionNo"], Convert.ToDateTime(row["CreatedDateTime"]).ToString(AppConstant.DisplayFormat.DateShortMonthHourMinute), row["CreatedByUserName"]);

            var editMenu = string.Empty;
            var deleteMenu = string.Empty;
            var printMenu = string.Format(
                        "<a href=\"#\" onclick=\"javascript:printPrescription('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/print16.png\"  /></a>",
                        row["PrescriptionNo"], Helper.UrlRoot());

            if (isUserEditAble.Equals(false)
                || !row["CreatedByUserID"].Equals(AppSession.UserLogin.UserID)
                || true.Equals(row["IsApproval"]) || true.Equals(row["IsVoid"])
                || (row["VerifiedByUserID"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["VerifiedByUserID"].ToString()))
                )
            {
                deleteMenu = string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" />", urlRoot);
                editMenu = string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" />", urlRoot);
            }
            else
            {
                deleteMenu =
                    string.Format(
                        "<a href=\"#\" onclick=\"javascript:deletePrescription('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/row_delete16.png\"  /></a>",
                        row["PrescriptionNo"], urlRoot);

                editMenu = string.Format(
                    "<a href=\"#\" onclick=\"javascript:entryPrescription('edit', '{0}', '{1}'); return false;\"><img src=\"{2}/Images/Toolbar/edit16.png\"  /></a>",
                    row["PrescriptionNo"], row["ParamedicID"], urlRoot);

            }

            // Verify Menu
            var verifyMenu = string.Empty;

            if (registrationType == AppConstant.RegistrationType.InPatient &&
                AppSession.Parameter.IsYes(AppParameter.ParameterItem.IsPrescriptionMustVerifyByDpjp))
            {
                if (ParamedicTeam.IsParamedicTeamStatusDpjp(registrationNo,
                        AppSession.UserLogin.ParamedicID) &&
                    false.Equals(row["IsApproval"]) && false.Equals(row["IsVoid"]) &&
                    (row["VerifiedByUserID"] == DBNull.Value ||
                     string.IsNullOrWhiteSpace(row["VerifiedByUserID"].ToString()))
                )
                {
                    verifyMenu = string.Format(
                        "<a href=\"#\" onclick=\"javascript:verifyPrescription('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/post16.png\"  /></a>",
                        row["PrescriptionNo"], urlRoot);
                }
                else
                    verifyMenu = string.Format("<img src=\"{0}/Images/Toolbar/post16_d.png\" />", urlRoot);
            }

            var isApproved = Convert.ToBoolean(row["IsApproval"]);
            var isComplete = row["CompleteDateTime"] != DBNull.Value;
            var isDelivered = row["DeliverDateTime"] != DBNull.Value;
            var verifiedDate = string.Empty;
            // Verified
            if (row["VerifiedDateTime"] != DBNull.Value)
            {
                verifiedDate = string.Format("<div style=\"float:left; \">[<img src='{1}/Images/Verified16.png'/>&nbsp;{0}]&nbsp;&nbsp;</div>",
                    Convert.ToDateTime(row["VerifiedDateTime"])
                        .ToString(AppConstant.DisplayFormat.LongDatePattern), Helper.UrlRoot());
            }
            // Header 
            sbItem.Append("<table style=\"width: 100%;background-color: ButtonFace;\"><tr>");
            sbItem.AppendFormat("<td >{1}<div style=\"font-size:x-small;font-weight:bold;float:left; color: {0}; \">[APPR]&nbsp;</div><div style=\"font-size:x-small;font-weight:bold;float:left; color: {2}; \">[COMP]&nbsp;</div><div style=\"font-size:x-small;font-weight:bold;float:left; color: {3}; \">[DELV]&nbsp;</div></td>", isApproved ? "red" : "gray", verifiedDate, isComplete ? "orange" : "gray", isDelivered ? "green" : "gray");


            if (!isForProgressHist)
                sbItem.AppendFormat(
                    "<td align=\"right\" style=\"width: 100px;\">{0}&nbsp;&nbsp;{1}&nbsp;&nbsp;{2}&nbsp;&nbsp;{3}</td>",
                    printMenu, editMenu, deleteMenu, verifyMenu);
            else
                sbItem.AppendFormat("<td></td> ");

            sbItem.Append("</tr></table>");

            // Add Note if exist
            var presc = new TransPrescription();
            if (presc.LoadByPrimaryKey(row["PrescriptionNo"].ToString()) && !string.IsNullOrWhiteSpace(presc.Note))
            {
                sbItem.AppendFormat("{0}<br/>", presc.Note);
            }

            return prescriptionHeader;
        }

        protected void grdDiagAndPrescription_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "refresh":
                    {
                        grdDiagAndPrescription.DataSource = null;
                        grdDiagAndPrescription.Rebind();
                        break;
                    }
                case "VoidPrescription":
                    {
                        var prescriptionNo = e.CommandArgument.ToString();
                        VoidPrescription(prescriptionNo);
                        grdDiagAndPrescription.DataSource = null;
                        grdDiagAndPrescription.Rebind();
                        break;
                    }
                case "Verify":
                    {
                        var prescriptionNo = e.CommandArgument.ToString();
                        VerifyPrescription(prescriptionNo);
                        grdDiagAndPrescription.DataSource = null;
                        grdDiagAndPrescription.Rebind();
                        break;
                    }
                case "Print":
                    {
                        var prescriptionNo = e.CommandArgument.ToString();
                        var jobParameters = new PrintJobParameterCollection();

                        // Check report type
                        string rptType;
                        var ph = new AppProgramHealthcare();
                        if (ph.LoadByPrimaryKey(AppConstant.Report.PrescriptionOrderSlip, AppSession.Parameter.HealthcareInitial))
                        {
                            rptType = ph.ProgramType;
                        }
                        else
                        {
                            var prg = new AppProgram();
                            prg.LoadByPrimaryKey(AppConstant.Report.PrescriptionOrderSlip);
                            rptType = prg.ProgramType;
                        }

                        if (rptType.ToLower() == "rslip")
                        {
                            // Memerlukan parameter HealthcareID di  printJobParameters[0]
                            var jobParameter0 = jobParameters.AddNew();
                            jobParameter0.Name = "p_HealthCareID";
                            jobParameter0.ValueString = AppSession.Parameter.HealthcareID;
                        }

                        var jobParameter = jobParameters.AddNew();
                        jobParameter.Name = "p_PrescriptionNo";
                        jobParameter.ValueString = prescriptionNo;

                        AppSession.PrintJobParameters = jobParameters;
                        AppSession.PrintJobReportID = AppConstant.Report.PrescriptionOrderSlip;

                        ShowPrintPreview();
                        break;
                    }
            }
        }

        private void VoidPrescription(string prescriptionNo)
        {
            var entity = new TransPrescription();
            entity.Query.Where(entity.Query.PrescriptionNo == prescriptionNo);
            if (!entity.Query.Load()) return;
            if (entity.IsApproval ?? false) return;
            if (entity.IsVoid ?? false) return;
            entity.IsVoid = true;

            var coll = new TransPrescriptionItemCollection();
            coll.Query.Where(coll.Query.PrescriptionNo == entity.PrescriptionNo);
            coll.LoadAll();

            foreach (var c in coll)
            {
                c.IsVoid = true;
            }

            using (var trans = new esTransactionScope())
            {
                entity.Save();
                coll.Save();

                // Update soap planning prescription
                TransPrescription.SoapeUpdatePrescriptionHist(entity.ParamedicID, RegistrationNo, entity.PrescriptionDate.Value);

                trans.Complete();
            }
        }
        private void VerifyPrescription(string prescriptionNo)
        {
            var entity = new TransPrescription();
            if (!entity.LoadByPrimaryKey(prescriptionNo)) return;
            if (entity.IsApproval ?? false) return;
            if (entity.IsVoid ?? false) return;

            entity.IsVerified = true;
            entity.VerifiedByUserID = AppSession.UserLogin.UserID;
            entity.VerifiedDateTime = DateTime.Now;
            entity.Save();
        }
        #endregion





    }
}