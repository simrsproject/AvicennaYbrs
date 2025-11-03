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

namespace Temiang.Avicenna.Module.RADT.EmrIp.MainContent
{
    [Obsolete("Hapus saja nanti",true)]
    public partial class MedicalHistCtl : BaseMainContentCtl
    {
        private const string PastMedHist = "PastMedHist";
        private const string FamilyMedHist = "FamilyMedHist";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GridDiagAndPrescriptionClientID
        {
            get { return grdDiagAndPrescription.ClientID; }
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
            var qr = new RegistrationQuery("r");
            var qrPar = new ParamedicQuery("pr");
            qr.InnerJoin(qrPar).On(qr.ParamedicID == qrPar.ParamedicID);

            qr.Select(qr.RegistrationNo, qr.RegistrationDate, qrPar.ParamedicName, qr.IsConsul, qr.IsNewPatient);
            qr.Where(qr.PatientID == PatientID, qr.IsVoid == false);
            qr.OrderBy(qr.RegistrationNo.Descending);

            var dtbEpisodeDiagnose = qr.LoadDataTable();
            dtbEpisodeDiagnose.Columns.Add(new DataColumn("Diagnosis", typeof(string)));
            dtbEpisodeDiagnose.Columns.Add(new DataColumn("ICD10", typeof(string)));
            dtbEpisodeDiagnose.Columns.Add(new DataColumn("Prescription", typeof(string)));

            // Get detail diagnose
            foreach (DataRow rowHd in dtbEpisodeDiagnose.Rows)
            {
                var regNo = rowHd["RegistrationNo"].ToString();

                // Diagnose entri oleh dokter
                // Seharusnya disave ditempat yg sama utk 1 registrasi
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

                rowHd["ICD10"] = EpisodeDiagnose.DiagnoseSummaryHtml(regNo);
                rowHd["Prescription"] = PrescriptionDetailInHTML(regNo);
            }

            return dtbEpisodeDiagnose;
        }

        private string PrescriptionDetailInHTML(string registrationNo)
        {
            var prescItemQr = new TransPrescriptionItemQuery("a");
            var prescQr = new TransPrescriptionQuery("b");
            var medic = new ParamedicQuery("d");
            var item = new ItemQuery("c");
            var consume = new ConsumeMethodQuery("e");
            var emb = new EmbalaceQuery("g");
            var oriconsume = new ConsumeMethodQuery("h");

            prescQr.Select(
                prescQr.PrescriptionNo,
                prescQr.IsApproval,
                prescQr.IsVoid,
                prescQr.CreatedByUserID,
                prescQr.IsUnitDosePrescription,
                prescItemQr.SequenceNo,
                prescQr.PrescriptionDate,
                medic.ParamedicName,
                prescQr.ParamedicID,
                item.ItemName,
                @"<ISNULL(a.OriResultQty, a.ResultQty) AS ResultQty>",
                @"<ISNULL(a.OriSRItemUnit, a.SRItemUnit) AS SRItemUnit>",
                @"<ISNULL(h.SRConsumeMethodName, e.SRConsumeMethodName) AS SRConsumeMethodName>",
                prescItemQr.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
                prescItemQr.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
                emb.EmbalaceLabel,
                @"<ISNULL(a.OriDosageQty, a.DosageQty) AS DosageQty>",
                @"<ISNULL(a.OriSRDosageUnit, a.SRDosageUnit) AS SRDosageUnit>",
                prescItemQr.EmbalaceQty,
                prescQr.Note,
                @"<ISNULL(a.OriConsumeQty, a.ConsumeQty) AS ConsumeQty>",
                @"<ISNULL(a.OriSRConsumeUnit, a.SRConsumeUnit) AS SRConsumeUnit>",
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                prescItemQr.LineAmount,
                prescItemQr.Notes
                );

            prescQr.LeftJoin(prescItemQr).On(prescItemQr.PrescriptionNo == prescQr.PrescriptionNo);
            prescQr.InnerJoin(medic).On(prescQr.ParamedicID == medic.ParamedicID);
            prescQr.LeftJoin(item).On(prescItemQr.ItemID == item.ItemID);
            prescQr.LeftJoin(consume).On(prescItemQr.SRConsumeMethod == consume.SRConsumeMethod);
            prescQr.LeftJoin(oriconsume).On(prescItemQr.OriSRConsumeMethod == oriconsume.SRConsumeMethod);
            prescQr.LeftJoin(emb).On(prescItemQr.EmbalaceID == emb.EmbalaceID);

            prescQr.Where(prescQr.RegistrationNo == registrationNo, prescQr.IsVoid == false);


            prescQr.OrderBy(prescQr.PrescriptionDate.Descending, prescQr.PrescriptionNo.Descending);
            prescQr.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var table = prescQr.LoadDataTable();


            // Ambil list PrescriptionNo
            var prescs = from t in table.AsEnumerable()
                         group t by new
                         {
                             PrescriptionNo = t.Field<string>("PrescriptionNo")
                         }
                             into g
                             select new
                             {
                                 g.Key.PrescriptionNo
                             };


            var displayTotal = AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor;

            var prescriptionHeader = "";
            var sbPresciption = new StringBuilder();
            var urlRoot = Helper.UrlRoot();
            foreach (var presc in prescs)
            {
                int i = 0;
                double total = 0;
                var sbItem = new StringBuilder();
                foreach (DataRow row in table.AsEnumerable().Where(t => t.Field<string>("PrescriptionNo") == presc.PrescriptionNo))
                {
                    if (i == 0)
                    {
                        prescriptionHeader = string.Format("{0} - {1} {2}", row["PrescriptionNo"], Convert.ToDateTime(row["PrescriptionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth), Convert.ToDateTime(row["PrescriptionDate"]).ToShortTimeString());
                        string editMenu;
                        string deleteMenu;
                        var printMenu = string.Format(
                                    "<a href=\"#\" onclick=\"javascript:printPrescription('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/print16.png\"  /></a>",
                                    row["PrescriptionNo"],urlRoot);

                        if (this.IsUserEditAble.Equals(false)
                            || !row["CreatedByUserID"].Equals(AppSession.UserLogin.UserID)
                            || string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID)
                            || true.Equals(row["IsApproval"]) || true.Equals(row["IsVoid"]))
                        {
                            deleteMenu = string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" />",urlRoot);
                            editMenu = string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" />",urlRoot);

                        }
                        else
                        {
                            deleteMenu =
                                string.Format(
                                    "<a href=\"#\" onclick=\"javascript:deletePrescription('{0}'); return false;\"><img src=\"{1}/Images/Toolbar/row_delete16.png\"  /></a>",
                                    row["PrescriptionNo"],urlRoot);

                            editMenu = string.Format(
                                "<a href=\"#\" onclick=\"javascript:entryPrescription('edit', '{0}', '{1}'); return false;\"><img src=\"{2}/Images/Toolbar/edit16.png\"  /></a>",
                                row["PrescriptionNo"], row["ParamedicID"],urlRoot);

                        }

                        var isApproved = Convert.ToBoolean(row["IsApproval"]);
                        //var isUnitDosePrescription = Convert.ToBoolean(row["IsUnitDosePrescription"]);

                        // End detail Item
                        sbItem.Append("</tr></table>");

                        // Header 
                        sbItem.Append("<table style=\"width: 100%;background-color: ButtonFace;\"><tr>");
                        sbItem.AppendFormat("<td style='font-weight: bold;'><div style=\"float:left;color: {0}; \">[APPR]&nbsp;</div></td>", isApproved ? "red" : "gray");
                        sbItem.AppendFormat("<td align=\"right\" style=\"width: 80px;\">{0}&nbsp;&nbsp;{1}&nbsp;&nbsp;{2}</td>", printMenu, editMenu, deleteMenu);
                        sbItem.Append("</tr></table>");

                        // Start detail Item
                        sbItem.Append("<table style=\"width: 100%;\"><tr>");
                    }
                    i++;

                    if (row["SequenceNo"] == DBNull.Value) continue;

                    if (!Convert.ToBoolean(row["IsCompound"]))
                    {
                        sbItem.Append("<td><b>R/</b>&nbsp;</td>");
                        sbItem.AppendFormat("<td>{0} {1} {2} ({3} @{4} {5} {6}</td>", row["ItemName"], row["ResultQty"], row["SRItemUnit"], row["SRConsumeMethodName"], row["ConsumeQty"], row["SRConsumeUnit"], row["Notes"]);
                    }
                    else
                    {
                        //sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} ({6} @ {7} {8} {9})<br />",
                        //    Convert.ToBoolean(row["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                        //    row["ItemName"], row["EmbalaceQty"], row["EmbalaceLabel"], row["DosageQty"], row["SRDosageUnit"], row["SRConsumeMethodName"],
                        //    row["ConsumeQty"], row["SRConsumeUnit"], row["Notes"]);

                        if (Convert.ToBoolean(row["IsRFlag"]))
                        {
                            sbItem.Append("<td><b>R/</b>&nbsp;</td>");
                            sbItem.AppendFormat("<td>{0} {1} {2} @ {3} {4} ({5} @ {6} {7} {8})</td><td></td>", row["ItemName"], row["EmbalaceQty"], row["EmbalaceLabel"], row["DosageQty"], row["SRDosageUnit"], row["SRConsumeMethodName"],
                                row["ConsumeQty"], row["SRConsumeUnit"], row["Notes"]);
                        }
                        else
                        {
                            sbItem.Append("<td>&nbsp;</td>");
                            sbItem.AppendFormat("<td>{0} {1} {2} @ {3} {4} {5}</td><td></td>", row["ItemName"], row["EmbalaceQty"], row["EmbalaceLabel"], row["DosageQty"], row["SRDosageUnit"], row["Notes"]);
                        }
                    }

                    total += Convert.ToDouble(row["LineAmount"]);
                }

                if (displayTotal == "Yes")
                    sbItem.AppendFormat("<b>{0}</b>", " (Rp. " + string.Format("{0:n2}", (total)) + ")");

                sbItem.Append("</div>");

                sbPresciption.AppendFormat("<fieldset><legend>{0}</legend>{1}</fieldset>", prescriptionHeader, sbItem);

            }
            return sbPresciption.ToString();
        }


        protected void grdDiagAndPrescription_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var prescriptionNo = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "DeletePrescription":
                    DeletePrescription(prescriptionNo);
                    grdDiagAndPrescription.DataSource = null;
                    grdDiagAndPrescription.Rebind();
                    break;
                case "Print":
                    PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();

                    var jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "HealthCareID";
                    jobParameter.ValueString = AppSession.Parameter.HealthcareID;

                    jobParameter = jobParameters.AddNew();
                    jobParameter.Name = "TransactionNo";
                    jobParameter.ValueString = prescriptionNo;

                    AppSession.PrintJobParameters = jobParameters;
                    AppSession.PrintJobReportID = AppConstant.Report.PrescriptionOrderSlip;

                    ShowPrintPreview();
                    break;

            }
        }

        private void DeletePrescription(string prescriptionNo)
        {
            //if (!UserParamedicAuthorized(item["ParamedicID"].Text)) return;

            //if (Convert.ToDateTime(item["PrescriptionDate"].Text).Date < DateTime.Now.Date)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid void to system');", true);
            //    return;
            //}

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

                trans.Complete();
            }
        }
        #endregion

        private void ShowPrintPreview()
        {
            var winPrintPreview = (RadWindow)Helper.FindControlRecursive(this.Page, "winPrintPreview");
            var url = Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx");
            Helper.ShowRadWindowAfterPostback(winPrintPreview, url, "preview", true);
        }

        protected void grdPastMedicalHist_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (string.IsNullOrEmpty(PatientID)) return;

            if (Session["dtbPmh"] == null || !Equals(PatientID, Session["dtbPmh_id"]))
            {
                Session["dtbPmh"] = MedicalHistDataTable();
                Session["dtbPmh_id"] = PatientID;
            }

            grdPastMedicalHist.DataSource = Session["dtbPmh"];

        }
        protected void grdPastMedicalHist_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                Session["dtbPmh"] = null;
                grdPastMedicalHist.DataSource = null;
                grdPastMedicalHist.Rebind();
            }
        }

        private DataTable FamilyMedicalHistDataTable()
        {
            var que = new FamilyMedicalHistoryQuery("a");
            var qrSri = new AppStandardReferenceItemQuery("sri");
            que.LeftJoin(qrSri)
                .On(que.SRMedicalDisease == qrSri.ItemID && qrSri.StandardReferenceID == FamilyMedHist);
            que.Where(que.PatientID == PatientID);
            que.Select(qrSri.ItemName, que.Notes);
            return que.LoadDataTable();
        }


        private DataTable MedicalHistDataTable()
        {
            var dtb = new DataTable();
            dtb.Columns.Add("GroupName", typeof(System.String));
            dtb.Columns.Add("PastMedical", typeof(System.String));

            var pmh = PastMedicalHistDataTable();
            foreach (DataRow row in pmh.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Past Medical History";
                newRow["PastMedical"] = string.Format("{0}: {1}", row["ItemName"], row["Notes"]);
                dtb.Rows.Add(newRow);
            }

            var fmh = FamilyMedicalHistDataTable();
            foreach (DataRow row in fmh.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Family Medical History";
                newRow["PastMedical"] = string.Format("{0}: {1}", row["ItemName"], row["Notes"]);
                dtb.Rows.Add(newRow);
            }

            //Past Surgery
            var surgicalHist = new PastSurgicalHistory();
            if (surgicalHist.LoadByPrimaryKey(PatientID))
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Past Surgical History";
                newRow["PastMedical"] = surgicalHist.SurgicalHistory;
                dtb.Rows.Add(newRow);
            }


            //Surgery Transaction
            var dtbSurgery = EpisodeProcedures();
            foreach (DataRow surgery in dtbSurgery.Rows)
            {
                var newRow = dtb.NewRow();
                newRow["GroupName"] = "Surgical History";
                newRow["PastMedical"] = string.Format("{0} {1} [{2}]", Convert.ToDateTime(surgery["ProcedureDate"]).ToString(AppConstant.DisplayFormat.Date), surgery["ProcedureName"], surgery["ParamedicName"]);
                dtb.Rows.Add(newRow);
            }
            return dtb;
        }

        private DataTable PastMedicalHistDataTable()
        {
            var que = new PastMedicalHistoryQuery("a");
            var qrSri = new AppStandardReferenceItemQuery("sri");
            que.LeftJoin(qrSri)
                .On(que.SRMedicalDisease == qrSri.ItemID && qrSri.StandardReferenceID == PastMedHist);
            que.Where(que.PatientID == PatientID);
            que.Select(que.SRMedicalDisease, qrSri.ItemName, que.Notes);

            var dtbPast = que.LoadDataTable();
            return dtbPast;
        }

        private DataTable EpisodeProcedures()
        {
            var query = new EpisodeProcedureQuery("a");
            var reg = new RegistrationQuery("r");
            var param = new ParamedicQuery("b");
            var proc = new ProcedureQuery("c");

            query.InnerJoin(reg).On(query.RegistrationNo == reg.RegistrationNo);
            query.InnerJoin(param).On(query.ParamedicID == param.ParamedicID);
            query.InnerJoin(proc).On(query.ProcedureID == proc.ProcedureID);

            query.Select
                 (
                     query.ProcedureDate,
                     query.ProcedureID,
                     param.ParamedicName,
                     proc.ProcedureName
                 );

            query.Where(reg.PatientID == PatientID, reg.IsVoid == false, query.IsFromOperatingRoom == true, query.IsVoid == false);
            query.OrderBy(query.SequenceNo.Ascending);

            var dtb = query.LoadDataTable();

            return dtb;
        }


        protected void grdPastMedicalHist_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridGroupHeaderItem)
            {
                // Menghilangkan control group expand collapse
                (e.Item as GridGroupHeaderItem).Cells[0].Controls.Clear();
            }
        }
    }
}