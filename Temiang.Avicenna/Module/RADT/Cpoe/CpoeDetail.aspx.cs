using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class CpoeDetail : BasePage
    {
        private CpoeTypeEnum CpoeType
        {
            get
            {
                return this.Request.QueryString["rt"] == "EMR" ? CpoeTypeEnum.Emergency : CpoeTypeEnum.InPatient;
            }
        }

        public string GetPatientDocumentFolder(object dataItem) {
            var entity = dataItem as PatientDocument;
            if (entity == null) return "";

            var targetFolderOld = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocument", entity.PatientID.Trim());
            var targetFolderYearly = "";
            if (!string.IsNullOrEmpty(entity.DocumentFolderYearly))
                targetFolderYearly = Path.Combine(AppSession.Parameter.ApplicationDocumentFolder, "PatientDocumentYearly", entity.DocumentFolderYearly, entity.PatientID.Trim());

            var targetFolder = targetFolderOld;
            if (!System.IO.Directory.Exists(targetFolder))
            {
                // jika old blm ada brarti pakai yearly
                targetFolder = string.IsNullOrEmpty(targetFolderYearly) ? targetFolderOld : targetFolderYearly;
            }
            return targetFolder;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = CpoeType == CpoeTypeEnum.InPatient ? AppConstant.Program.CpoeInPatient : AppConstant.Program.CpoeEmergency;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            PopulateRegistrationInfo();

            //jo
            JobOrders = null;

            ViewState["TransactionNo" + Request.UserHostName] = string.Empty;
            ComboBox.PopulateWithServiceUnitForTransactionJO(cboServiceUnitIDJO, TransactionCode.JobOrder, false);
            txtTransactionDateJO.SelectedDate = DateTime.Now.Date;
        }

        private string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        protected string ParamedicID
        {
            get
            {
                return Request.QueryString["parid"];
            }
        }
        private void PopulateRegistrationInfo()
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            cpnRegInfo.Title = string.Format("<font color='white'>{0}{1}{2}</font>", pat.FirstName,
                string.IsNullOrEmpty(pat.MiddleName) ? " " : string.Format(" {0} ", pat.MiddleName), pat.LastName);

            lblMedicalNo.Text = pat.MedicalNo;
            lblPatientID.Text = pat.PatientID;
            lblRegistrationDate.Text = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.DateShortMonth);
            lblRegistrationNo.Text = reg.RegistrationNo;
            lblGender.Text = pat.Sex == "M" ? "Male" : "Female";
            lblDateOfBirth.Text = string.Format("{0} ({1}y {2}m {3}d)", (pat.DateOfBirth ?? new DateTime()).ToString(AppConstant.DisplayFormat.DateShortMonth),
                reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay);

            var medic = new Paramedic();
            medic.LoadByPrimaryKey(ParamedicID);
            lblPhysician.Text = medic.ParamedicName;

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            lblGuarantor.Text = grr.GuarantorName;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(Request.QueryString["unit"]);
            lblServiceUnit.Text = unit.ServiceUnitName;

            lblLos.Text = string.Format("{0}y {1}m {2}d", Helper.GetAgeInYear(reg.RegistrationDate.Value, DateTime.Now).ToString(),
                Helper.GetAgeInMonth(reg.RegistrationDate.Value, DateTime.Now).ToString(),
                Helper.GetAgeInDay(reg.RegistrationDate.Value, DateTime.Now).ToString());

            PopulatePatientAllergy();
            PopulateEpisodeDiagnose();

            // Last Registration No
            var regq = new RegistrationQuery();
            regq.Select(regq.RegistrationNo);
            regq.Where(regq.PatientID == lblPatientID.Text, regq.IsVoid == false, regq.IsNewPatient == true);
            regq.OrderBy(regq.RegistrationDate.Ascending);
            regq.es.Top = 1;
            var regdtb = regq.LoadDataTable();
            if (regdtb.Rows.Count > 0)
            {
                lblLastRegistrationNo.Text = regdtb.Rows[0]["RegistrationNo"].ToString();
            }


            //  if (reg.IsNewPatient == true)
            // {
            //  lblNewPatient.Visible = true;
            // lbEditPhysicalExam.Visible = true;
            // }
        }

        private string PatientID
        {
            get
            {
                return lblPatientID.Text;
            }
        }
        private string ServiceUnitID
        {
            get
            {
                return Request.QueryString["unit"];
            }
        }
        private void PopulatePatientAllergy()
        {
            var paQ = new PatientAllergyQuery("a");
            paQ.Select(paQ.AllergenName, paQ.DescAndReaction);
            paQ.Where(paQ.PatientID == PatientID);
            var dtb = paQ.LoadDataTable();
            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");
            foreach (DataRow dataRow in dtb.Rows)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td style='width:100px;font-weight: bold;'>{0}</td>", dataRow["AllergenName"]);
                sb.AppendLine("<td style='width:5px'>:</td>");
                sb.AppendFormat("<td style='color: red;'>{0}</td>", dataRow["DescAndReaction"]);
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            litPatientAllergy.Text = sb.ToString();
        }

        private void PopulateEpisodeDiagnose()
        {
            cpnDiagnosis.Title =
                string.Format(
                    "Diagnosis<div style='float: right;padding:4px'><a  href='#' onclick=\"" + "entryEpisodeDiagnose('new','{0}'); event.cancelBubble = true;if(event.stopPropagation) event.stopPropagation();" + "\"> <img src='../../../Images/Toolbar/new16.png'  alt='edit' /></a></div>",
                    RegistrationNo);
            var que = new EpisodeDiagnoseQuery("a");
            var queRef = new AppStandardReferenceItemQuery("b");
            que.InnerJoin(queRef).On(que.SRDiagnoseType == queRef.ItemID).Where(queRef.StandardReferenceID == "DiagnoseType");
            que.Select(que.DiagnoseID, que.DiagnosisText, queRef.ItemName, que.SequenceNo, que.LastUpdateDateTime, que.LastUpdateByUserID, que.IsVoid);
            que.Where(que.RegistrationNo == RegistrationNo);


            var dtb = que.LoadDataTable();
            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");
            foreach (DataRow dataRow in dtb.Rows)
            {
                if (true.Equals(dataRow["IsVoid"]))
                {
                    sb.AppendFormat("<tr><td style='font-weight: bold;text-decoration: line-through;'>{0}</td></tr>", dataRow["ItemName"]);
                }
                else
                {
                    sb.AppendFormat("<tr><td style='font-weight: bold;'>{0}", dataRow["ItemName"]);
                    sb.AppendFormat("&nbsp;<a  href='#' onclick=\"" + "entryEpisodeDiagnose('edit','{0}','{1}');" + "\"> <img src='../../../Images/Toolbar/edit16.png'  alt='edit' /></a>", RegistrationNo, dataRow["SequenceNo"]);
                    sb.AppendFormat("<div style='float: right;><a  href='#' onclick=\"" + "voidEpisodeDiagnose('{0}','{1}');" + "\"> <img src='../../../Images/Toolbar/delete16.png'  alt='void'/></a></div>", RegistrationNo, dataRow["SequenceNo"]);
                    sb.Append("</td></tr>");
                }
                sb.AppendFormat("<tr><td style='padding-left:6px;{0}'>{1} {2}</td></tr>", true.Equals(dataRow["IsVoid"]) ? "text-decoration: line-through;" : string.Empty, dataRow["DiagnoseID"], dataRow["DiagnosisText"]);
                sb.AppendLine("<tr><td></td></tr>");
            }
            sb.AppendLine("</table>");
            litDiagnosis.Text = sb.ToString();
        }
        private bool UserAuthorized
        {
            get
            {
                if (!IsUserEditAble)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "unauthorized", "alert('Unauthorized access to system');", true);
                    return false;
                }
                return true;
            }
        }

        #region Status Show All History
        protected void chkShowAll_OnLoad(object sender, EventArgs e)
        {
            // Valuenya harus diset anual krn berada di Command template 
            var chk = ((CheckBox)sender);
            chk.Checked = Convert.ToBoolean(ViewState[chk.ID]);
        }

        protected void chkShowAll_OnCheckedChanged(object sender, EventArgs e)
        {
            var chk = ((CheckBox)sender);
            ViewState[chk.ID] = chk.Checked;
            switch (chk.ID)
            {
                case "chkShowAllRegistrationInfoMedic":
                    grdRegistrationInfoMedic.DataBind();
                    break;
                case "chkShowAllBodyDiagram":
                    grdBodyDiagram.DataBind();
                    break;
            }

        }
        private bool IsShowAllRegistrationInfoMedic
        {
            get { return Convert.ToBoolean(ViewState["chkShowAllRegistrationInfoMedic"]); }
        }

        private bool IsShowAllEpisodeDiagnose
        {
            get { return Convert.ToBoolean(ViewState["chkShowAllEpisodeDiagnose"]); }
        }
        private bool IsShowAllSoap
        {
            get { return Convert.ToBoolean(ViewState["chkShowAllSoap"]); }
        }

        #endregion

        #region Notes History

        protected void grdRegistrationInfoMedic_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var entity = new RegistrationInfoMedic();
            entity.Query.Where(
                entity.Query.RegistrationInfoMedicID == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["RegistrationInfoMedicID"])
                );
            if (entity.Query.Load())
            {
                if (!(entity.IsDeleted ?? false))
                {
                    entity.IsDeleted = true;
                    entity.Save();

                    // Refresh
                    grdRegistrationInfoMedic.DataSource = RegistrationInfoMedicDataTable();
                }
            }
        }
        private DataTable RegistrationInfoMedicDataTable()
        {
            //var mrgs = new MergeBillingCollection();
            //mrgs.Query.Where(mrgs.Query.FromRegistrationNo == RegistrationNo);

            // 01. Notes
            var que = new RegistrationInfoMedicQuery("a");
            var reg = new RegistrationQuery("x");

            que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo);

            //if (true.Equals(IsShowAllRegistrationInfoMedic))
            //{
            //    var reg = new RegistrationQuery("f");

            //    que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo);
            //    que.Where(
            //        reg.PatientID == PatientID
            //        );
            //}
            //else
            //{

            //if (mrgs.Query.Load())
            que.Where(reg.PatientID == PatientID);
            //else
            //{
            //    if (Request.QueryString["rt"] == AppConstant.RegistrationType.InPatient)
            //        que.Where(que.RegistrationNo == RegistrationNo);
            //    else
            //    {
            //        var reg = new RegistrationQuery("x");
            //        que.InnerJoin(reg).On(que.RegistrationNo == reg.RegistrationNo && reg.SRRegistrationType == AppConstant.RegistrationType.EmergencyPatient);
            //        que.Where(reg.PatientID == PatientID);
            //    }
            //}
            ////}
            que.OrderBy(que.RegistrationInfoMedicID.Descending);

            var coll = new RegistrationInfoMedicCollection();
            coll.Load(que);

            //transfer from ot/emr to ip
            //mrgs = new MergeBillingCollection();
            //mrgs.Query.Where(mrgs.Query.FromRegistrationNo == RegistrationNo);
            //if (mrgs.Count > 0)
            //{
            var epq = new EpisodeSOAPEQuery("a");
            reg = new RegistrationQuery("x");

            epq.InnerJoin(reg).On(epq.RegistrationNo == reg.RegistrationNo);
            epq.Where(reg.PatientID == PatientID);

            var eps = new EpisodeSOAPECollection();
            if (eps.Load(epq))
            {
                foreach (var ep in eps)
                {
                    var c = coll.AddNew();
                    c.RegistrationInfoMedicID = ep.RegistrationNo;
                    c.SRMedicalNotesInputType = "SOAP";
                    c.DateTimeInfo = ep.LastUpdateDateTime;
                    c.CreatedByUserID = ep.LastUpdateByUserID;
                    c.Info1 = ep.Subjective;
                    c.Info2 = ep.Objective;
                    c.Info3 = ep.Assesment;
                    c.Info4 = ep.Planning;
                    c.IsDeleted = ep.IsVoid;
                    c.ServiceUnitID = ep.ServiceUnitID;
                }
            }
            //}

            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("RegistrationInfoMedicID", typeof(string)));
            dtb.Columns.Add(new DataColumn("SRMedicalNotesInputType", typeof(string)));
            dtb.Columns.Add(new DataColumn("Notes", typeof(string)));
            dtb.Columns.Add(new DataColumn("VitalSigns", typeof(string)));
            dtb.Columns.Add(new DataColumn("DateTimeInfo", typeof(DateTime)));
            dtb.Columns.Add(new DataColumn("LastUpdateByUserID", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsDeleted", typeof(bool)));

            litLastVitalSign.Text = string.Empty;
            foreach (RegistrationInfoMedic rim in coll.OrderByDescending(c => c.DateTimeInfo))
            {
                var newRow = dtb.NewRow();
                newRow["RegistrationInfoMedicID"] = rim.RegistrationInfoMedicID;
                newRow["SRMedicalNotesInputType"] = rim.SRMedicalNotesInputType;
                newRow["DateTimeInfo"] = rim.DateTimeInfo;
                newRow["LastUpdateByUserID"] = rim.LastUpdateByUserID;
                newRow["IsDeleted"] = rim.IsDeleted;

                var paramedicName = string.Empty;
                if (!string.IsNullOrEmpty(rim.CreatedByUserID))
                {
                    var user = new AppUser();
                    if (user.LoadByPrimaryKey(rim.CreatedByUserID))
                    {
                        paramedicName = user.UserName;
                        if (!string.IsNullOrEmpty(user.ParamedicID))
                        {
                            var paramedic = new Paramedic();
                            paramedicName = paramedic.LoadByPrimaryKey(user.ParamedicID)
                                ? paramedic.ParamedicName
                                : user.UserName;
                        }
                    }
                }

                var sbNote = new StringBuilder();
                if (rim.IsDeleted == true) sbNote.AppendLine("<div style='text-decoration:line-through;'>");

                var unit = string.Empty;
                if (!string.IsNullOrEmpty(rim.ServiceUnitID))
                {
                    var su = new ServiceUnit();
                    su.LoadByPrimaryKey(rim.ServiceUnitID);
                    unit = su.ServiceUnitName;
                }

                sbNote.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
                sbNote.AppendFormat("<tr><td colspan='2' style='font-weight: bold'>{0} - {1} - {2} - {3}", Convert.ToDateTime(rim.DateTimeInfo).ToString(AppConstant.DisplayFormat.DateShortMonth), Convert.ToDateTime(rim.DateTimeInfo).ToShortTimeString(), unit, paramedicName);
                sbNote.AppendLine("</td></tr>");
                if (rim.SRMedicalNotesInputType == "SBAR" || rim.SRMedicalNotesInputType == "SOAP")
                {
                    sbNote.AppendFormat("<tr><td style='font-weight: bold; width:15px;padding-left:10px'>{0}:</td><td>{1}</td></tr>",
                        rim.SRMedicalNotesInputType.Substring(0, 1), rim.Info1);
                    sbNote.AppendLine();
                    sbNote.AppendFormat("<tr><td style='font-weight: bold; width:15px;padding-left:10px'>{0}:</td><td>{1}</td></tr>",
                        rim.SRMedicalNotesInputType.Substring(1, 1), rim.Info2);
                    sbNote.AppendLine();
                    sbNote.AppendFormat("<tr><td style='font-weight: bold; width:15px;padding-left:10px'>{0}:</td><td>{1}</td></tr>",
                        rim.SRMedicalNotesInputType.Substring(2, 1), rim.Info3);
                    sbNote.AppendLine();
                    sbNote.AppendFormat("<tr><td style='font-weight: bold; width:15px;padding-left:10px'>{0}:</td><td>{1}</td></tr>",
                        rim.SRMedicalNotesInputType.Substring(3, 1), rim.Info4);
                }
                else
                {
                    sbNote.AppendFormat("<tr><td colspan='2' style='padding-left:10px;'>{0}</td></tr>", rim.Info1);
                }

                sbNote.AppendLine("</table>");

                if (rim.IsDeleted == true) sbNote.AppendLine("</div>");

                newRow["Notes"] = sbNote.ToString();

                // 02. VitalSign
                var queRimv = new RegistrationInfoMedicVitalSignQuery("a");
                var queVitalSign = new VitalSignQuery("b");
                queRimv.InnerJoin(queVitalSign).On(queRimv.VitalSignID == queVitalSign.VitalSignID);
                queRimv.Select(queRimv.VitalSignID, queRimv.VitalSignUnit, queRimv.VitalSignValueText,
                    queVitalSign.VitalSignName);
                queRimv.Where(queRimv.RegistrationInfoMedicID == rim.RegistrationInfoMedicID);
                var dtbVs = queRimv.LoadDataTable();

                var sbVs = new StringBuilder();
                if (dtbVs.Rows.Count > 0) sbVs.AppendLine("<table width='100%'>");
                foreach (DataRow row in dtbVs.Rows)
                {
                    sbVs.AppendLine("<tr>");
                    sbVs.AppendFormat("<td style='width:100px;font-weight: bold;'>{0}</td>", row["VitalSignName"]);
                    sbVs.AppendLine("<td style='width:5px'>:</td>");
                    sbVs.AppendFormat("<td >{0}&nbsp;{1}</td>", row["VitalSignValueText"], row["VitalSignUnit"]);
                    sbVs.AppendLine("</tr>");
                }
                if (dtbVs.Rows.Count > 0) sbVs.AppendLine("</table>");
                var vitalSign = sbVs.ToString();
                newRow["VitalSigns"] = vitalSign;

                if (string.IsNullOrEmpty(litLastVitalSign.Text))
                    litLastVitalSign.Text = vitalSign;

                // Add Row
                dtb.Rows.Add(newRow);
            }

            var x = (from d in dtb.AsEnumerable()
                     where !string.IsNullOrEmpty(d.Field<string>("VitalSigns"))
                     orderby d.Field<DateTime>("DateTimeInfo") descending
                     select d.Field<string>("VitalSigns")).Take(1).SingleOrDefault();
            if (x != null) litLastVitalSign.Text = x;

            return dtb;
        }

        protected void grdRegistrationInfoMedic_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdRegistrationInfoMedic.DataSource = RegistrationInfoMedicDataTable();
        }


        #endregion

        #region Prescription History
        protected void chkShowAllPrescription_OnCheckedChanged(object sender, EventArgs e)
        {
            grdPrescription.DataBind();

        }
        private DataTable TransPrescriptionsDataTable()
        {

            var query = new TransPrescriptionItemQuery("a");
            var presc = new TransPrescriptionQuery("b");
            var medic = new ParamedicQuery("d");
            var item = new ItemQuery("c");
            var consume = new ConsumeMethodQuery("e");
            var emb = new EmbalaceQuery("g");
            var oriconsume = new ConsumeMethodQuery("h");

            presc.Select(
                presc.PrescriptionNo,
                query.SequenceNo,
                presc.PrescriptionDate,
                medic.ParamedicName,
                presc.ParamedicID,
                presc.CreatedByUserID,
                item.ItemName,
                @"<ISNULL(a.OriResultQty, a.ResultQty) AS ResultQty>",
                @"<ISNULL(a.OriSRItemUnit, a.SRItemUnit) AS SRItemUnit>",
                @"<ISNULL(h.SRConsumeMethodName, e.SRConsumeMethodName) AS SRConsumeMethodName>",
                presc.IsUnitDosePrescription,
                presc.IsApproval.Coalesce("CAST(0 AS BIT)").As("IsApproval"),
                presc.IsVoid.Coalesce("CAST(0 AS BIT)").As("IsVoid"),
                query.IsRFlag.Coalesce("CAST(0 AS BIT)").As("IsRFlag"),
                query.IsCompound.Coalesce("CAST(0 AS BIT)").As("IsCompound"),
                emb.EmbalaceLabel,
                @"<ISNULL(a.OriDosageQty, a.DosageQty) AS DosageQty>",
                @"<ISNULL(a.OriSRDosageUnit, a.SRDosageUnit) AS SRDosageUnit>",
                query.EmbalaceQty,
                presc.Note,
                @"<ISNULL(a.OriConsumeQty, a.ConsumeQty) AS ConsumeQty>",
                @"<ISNULL(a.OriSRConsumeUnit, a.SRConsumeUnit) AS SRConsumeUnit>",
                "<(a.ParentNo + a.SequenceNo) as ORDERKEY>",
                query.LineAmount,
                query.Notes
                );

            presc.LeftJoin(query).On(query.PrescriptionNo == presc.PrescriptionNo);
            presc.InnerJoin(medic).On(presc.ParamedicID == medic.ParamedicID);
            presc.LeftJoin(item).On(query.ItemID == item.ItemID);
            presc.LeftJoin(consume).On(query.SRConsumeMethod == consume.SRConsumeMethod);
            presc.LeftJoin(oriconsume).On(query.OriSRConsumeMethod == oriconsume.SRConsumeMethod);
            presc.LeftJoin(emb).On(query.EmbalaceID == emb.EmbalaceID);

            presc.Where(
                presc.IsVoid == false
                );

            //if (chkShowAllPrescription.Checked.Equals(true))
            //{
            //    var reg = new RegistrationQuery("f");
            //    presc.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);
            //    presc.Where(
            //        reg.PatientID == PatientID
            //        );
            //}
            //else
            //{
            var mrgs = new MergeBillingCollection();
            mrgs.Query.Where(mrgs.Query.FromRegistrationNo == RegistrationNo);
            if (mrgs.Query.Load())
                presc.Where(
                    presc.RegistrationNo.In(mrgs.Select(m => m.RegistrationNo), RegistrationNo)
                    );
            else
                presc.Where(
                    presc.RegistrationNo == RegistrationNo
                    );
            //}
            presc.OrderBy(presc.PrescriptionDate.Descending, presc.PrescriptionNo.Descending);
            presc.OrderBy("ORDERKEY", esOrderByDirection.Ascending);

            var table = presc.LoadDataTable();


            // Ambil list PrescriptionNo
            var prescs = from t in table.AsEnumerable()
                         group t by new
                         {
                             PrescriptionNo = t.Field<string>("PrescriptionNo")
                         }
                             into g
                         select new
                         {
                             PrescriptionNo = g.Key.PrescriptionNo
                         };

            var dtb = new DataTable();
            dtb.Columns.Add(new DataColumn("PrescriptionNo", typeof(string)));
            dtb.Columns.Add(new DataColumn("PrescriptionSummary", typeof(string)));
            dtb.Columns.Add(new DataColumn("PrescriptionItem", typeof(string)));
            dtb.Columns.Add(new DataColumn("IsVoid", typeof(bool)));
            dtb.Columns.Add(new DataColumn("IsApproval", typeof(bool)));
            dtb.Columns.Add(new DataColumn("PrescriptionDate", typeof(DateTime)));
            dtb.Columns.Add(new DataColumn("CreatedByUserID", typeof(string)));
            dtb.Columns.Add(new DataColumn("ParamedicID", typeof(string)));

            var displayTotal = AppSession.Parameter.IsShowPrescPriceOnDisplayDoctor;

            foreach (var p in prescs)
            {
                var row = dtb.NewRow();

                int i = 0;
                double total = 0;
                var sb = new StringBuilder();
                var sbItem = new StringBuilder();
                sb.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
                sbItem.AppendLine("<table width='100%' cellpadding='0' cellspacing='0'>");
                foreach (DataRow r in table.AsEnumerable().Where(t => t.Field<string>("PrescriptionNo") == p.PrescriptionNo))
                {

                    if (i == 0)
                    {
                        var isApproved = Convert.ToBoolean(r["IsApproval"]);
                        var isUnitDosePrescription = Convert.ToBoolean(r["IsUnitDosePrescription"]);
                        sb.AppendLine("<tr><td style='font-weight: bold'>");
                        sb.AppendFormat("{0} - {1}", r["PrescriptionNo"], Convert.ToDateTime(r["PrescriptionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth));

                        sb.AppendFormat("&nbsp;&nbsp;{0}",
                            isApproved
                                ? "<img src='../../../Images/Toolbar/post16.png' />"
                                : "<img src='../../../Images/Toolbar/post16_d.png' />");
                        sb.AppendFormat("&nbsp;{0}", isUnitDosePrescription ? "<img src='../../../Images/Toolbar/refresh16.png' />" : "<img src='../../../Images/Toolbar/refresh16_d.png' />");

                        //if (!isApproved && AppSession.UserLogin.UserID.Equals(r["CreatedByUserID"]))
                        //{
                        //    sb.AppendFormat(@"&nbsp;&nbsp;<a href='#' onclick=""javascript:entryPrescription('edit', '{0}', '{1}');""><img style='border: 0px; vertical-align: middle;' src='../../../../Images/Toolbar/edit16.png'/></a>", r["PrescriptionNo"], r["ParamedicID"]);
                        //}
                        sb.AppendFormat("</br>{0}</td></tr>", r["ParamedicName"]);

                        if (!string.IsNullOrEmpty(r["Note"].ToString())) sb.AppendFormat("<tr><td align='left' style='font-style: italic'>{0}</td></tr>", r["Note"]);

                        sb.AppendLine("</table>");


                        if (r["SequenceNo"] != DBNull.Value) sbItem.AppendLine("<tr><td align='left' style='font-style: italic'>");


                        row["IsVoid"] = r["IsVoid"];
                        row["PrescriptionDate"] = r["PrescriptionDate"];
                        row["CreatedByUserID"] = r["CreatedByUserID"];
                        row["IsApproval"] = r["IsApproval"];
                    }
                    i++;

                    if (r["SequenceNo"] == DBNull.Value) continue;

                    if (!Convert.ToBoolean(r["IsCompound"]))
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} ({4} @ {5} {6} {7})<br />",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                            r["ItemName"], r["ResultQty"], r["SRItemUnit"], r["SRConsumeMethodName"], r["ConsumeQty"], r["SRConsumeUnit"], r["Notes"]);
                    }
                    else
                    {
                        sbItem.AppendFormat("{0} {1} {2} {3} @ {4} {5} ({6} @ {7} {8} {9})<br />",
                            Convert.ToBoolean(r["IsRFlag"]) ? string.Format("<b>{0}</b>", @"R/") : "&nbsp;&nbsp;&nbsp;&nbsp;",
                            r["ItemName"], r["EmbalaceQty"], r["EmbalaceLabel"], r["DosageQty"], r["SRDosageUnit"], r["SRConsumeMethodName"],
                            r["ConsumeQty"], r["SRConsumeUnit"], r["Notes"]);
                    }

                    total += Convert.ToDouble(r["LineAmount"]);
                }

                if (displayTotal)
                    sbItem.AppendFormat("<b>{0}</b>", " (Rp. " + string.Format("{0:n2}", (total)) + ")");

                sbItem.AppendLine("</td></tr></table>");

                row["PrescriptionNo"] = p.PrescriptionNo;
                row["PrescriptionSummary"] = sb.ToString();
                row["PrescriptionItem"] = sbItem.ToString();


                dtb.Rows.Add(row);
            }
            return dtb;
        }



        protected void grdPrescription_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdPrescription.DataSource = TransPrescriptionsDataTable();
        }

        protected void grdPrescription_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (!UserAuthorized) return;

            var item = e.Item as GridDataItem;
            if (item == null) return;

            if (Convert.ToDateTime(item["PrescriptionDate"].Text).Date < DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid void to system');", true);
                return;
            }

            var entity = new TransPrescription();
            entity.Query.Where(entity.Query.PrescriptionNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["PrescriptionNo"]));
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

            grdPrescription.DataSource = TransPrescriptionsDataTable();
        }

        protected void grdPrescription_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (Convert.ToBoolean(((System.Data.DataRowView)(item.DataItem)).Row.ItemArray[3])) item["TemplateItemName1"].CssClass = "text";
            }
        }

        #endregion

        //#region Soape
        //private DataTable EpisodeSoapeDatasource
        //{
        //    get
        //    {
        //        var soape = new EpisodeSOAPEQuery("a");
        //        var medic = new ParamedicQuery("d");

        //        soape.Select(
        //            soape,
        //            medic.ParamedicName
        //            );
        //        soape.InnerJoin(medic).On(soape.ParamedicID == medic.ParamedicID);
        //        soape.Where(
        //            soape.IsVoid == false,
        //            soape.RegistrationNo == RegistrationNo
        //            );
        //        soape.OrderBy(
        //            soape.RegistrationNo.Descending,
        //            soape.SequenceNo.Descending,
        //            soape.ServiceUnitID.Ascending
        //            );

        //        return soape.LoadDataTable();
        //    }
        //}
        //protected void grdBodyDiagram_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    grdBodyDiagram.DataSource = EpisodeSoapeDatasource;
        //}

        //protected void grdBodyDiagram_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    var item = e.Item as GridDataItem;
        //    if (item == null) return;

        //    if (Convert.ToDateTime(item["SOAPEDate"].Text).Date < DateTime.Now.Date)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid void to system');", true);
        //        return;
        //    }

        //    var entity = new EpisodeSOAPE();
        //    entity.Query.Where(
        //        entity.Query.RegistrationNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["RegistrationNo"]) &&
        //        entity.Query.SequenceNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"])
        //        );
        //    if (entity.Query.Load())
        //    {
        //        if (!(entity.IsVoid ?? false))
        //        {
        //            entity.IsVoid = true;
        //            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer(); 
        //            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
        //            entity.Save();

        //            grdBodyDiagram.Rebind();
        //        }
        //    }
        //}

        //protected void grdBodyDiagram_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    //if (e.CommandName == "View")
        //    //{
        //    //    var item = e.Item as GridDataItem;
        //    //    if (item == null) return;

        //    //    var gridDataKeys = item.OwnerTableView.DataKeyValues[item.ItemIndex];
        //    //    var regNo = Convert.ToString(gridDataKeys["RegistrationNo"]);
        //    //    var seqNo = Convert.ToString(gridDataKeys["SequenceNo"]);

        //    //    var script = string.Format("openWinEpisodeSoapDialog('edit', '{0}', '{1}')", regNo, seqNo);
        //    //    Helper.RegisterStartupScript(this, "ViewSoap", script);
        //    //}
        //    //else 
        //    if (e.CommandName == "Print")
        //    {
        //        var item = e.Item as GridDataItem;
        //        if (item == null) return;
        //        var gridDataKeys = item.OwnerTableView.DataKeyValues[item.ItemIndex];
        //        var jobParameters = new PrintJobParameterCollection();

        //        var jobParameter = jobParameters.AddNew();
        //        jobParameter.Name = "RegistrationNo";
        //        jobParameter.ValueString = Convert.ToString(gridDataKeys["RegistrationNo"]);

        //        var jobParameter2 = jobParameters.AddNew();
        //        jobParameter2.Name = "SequenceNo";
        //        jobParameter2.ValueString = Convert.ToString(gridDataKeys["SequenceNo"]);

        //        AppSession.PrintJobParameters = jobParameters;
        //        AppSession.PrintJobReportID = AppConstant.Report.SOAP;

        //        winPrintPreview.ShowAfterPostback(Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx"), "printPreview", true);

        //    }
        //}

        //protected void grdBodyDiagram_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (GridDataItem)e.Item;
        //        if (Convert.ToBoolean(((System.Data.DataRowView)(item.DataItem)).Row.ItemArray[12]))
        //        {
        //            item["TemplateItemName1"].CssClass = "text";
        //            item["TemplateItemName2"].CssClass = "text";
        //        }
        //    }
        //}
        //#endregion
        #region Body Diagram
        private DataTable EpisodeBodyDiagramDatasource
        {
            get
            {
                var episode = new EpisodeBodyDiagramQuery("a");
                var medic = new ParamedicQuery("d");

                episode.Select(
                    episode,
                    medic.ParamedicName
                    );
                episode.InnerJoin(medic).On(episode.ParamedicID == medic.ParamedicID);
                episode.Where(
                    episode.IsDeleted == false,
                    episode.RegistrationNo == RegistrationNo
                    );
                episode.OrderBy(
                    episode.RegistrationNo.Descending,
                    episode.SequenceNo.Descending,
                    episode.ServiceUnitID.Ascending
                    );

                return episode.LoadDataTable();
            }
        }
        protected void grdBodyDiagram_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdBodyDiagram.DataSource = EpisodeBodyDiagramDatasource;
        }

        protected void grdBodyDiagram_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var entity = new EpisodeBodyDiagram();
            entity.Query.Where(
                entity.Query.RegistrationNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["RegistrationNo"]) &&
                entity.Query.SequenceNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"])
                );
            if (entity.Query.Load())
            {
                if (!(entity.IsDeleted ?? false))
                {
                    entity.IsDeleted = true;
                    entity.Save();

                    grdBodyDiagram.Rebind();
                }
            }
        }


        #endregion

        #region Laboratory
        protected void btnFilterOrder_Click(object sender, ImageClickEventArgs e)
        {
            var dtb = GetLabHistOrder;

            grdLabHist.DataSource = dtb;
            grdLabHist.DataBind();

            cboExamOrderName.Items.Clear();
            cboExamOrderName.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (var row in dtb.AsEnumerable().Select(d => new
            {
                FractionName = d.Field<string>("LabOrderSummary"),
                FractionCode = d.Field<string>("LabOrderCode")
            }).Distinct())
            {
                cboExamOrderName.Items.Add(new RadComboBoxItem(row.FractionName.ToString(), row.FractionCode.ToString()));
            }
        }
        private DataTable GetLabHistOrder
        {
            get
            {
                var dtb = new DataTable();
                dtb.Columns.Add(new DataColumn("OrderLabNo", typeof(string)));
                dtb.Columns.Add(new DataColumn("LabOrderCode", typeof(string)));
                dtb.Columns.Add(new DataColumn("LabOrderSummary", typeof(string)));
                dtb.Columns.Add(new DataColumn("Result", typeof(string)));
                dtb.Columns.Add(new DataColumn("StandarValue", typeof(string)));
                dtb.Columns.Add(new DataColumn("OrderLabTglOrder", typeof(DateTime)));

                if (dtb.Rows.Count > 0) dtb.Rows.Clear();

                if (AppSession.Parameter.IsUsingHisInterop) return dtb;

                #region if (AppSession.Parameter.HealthcareInitial == "RSCH")
                switch (AppSession.Parameter.HealthcareInitial)
                {
                    case "RSCH":
                        var hasil = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("hp");
                        var hd = new TransChargesQuery("a");
                        var reg = new RegistrationQuery("b");

                        hasil.InnerJoin(hd).On(hasil.OrderLabNo == hd.TransactionNo);
                        hasil.InnerJoin(reg).On(hd.RegistrationNo == reg.RegistrationNo && reg.PatientID == lblPatientID.Text);

                        if (!txtExamDate1.IsEmpty && !txtExamDate2.IsEmpty)
                            hasil.Where(hasil.OrderLabTglOrder.Between(txtExamDate1.SelectedDate.Value.Date, txtExamDate2.SelectedDate.Value.Date));
                        if (!string.IsNullOrEmpty(cboExamOrderName.SelectedValue))
                            hasil.Where(hasil.CheckupResultFractionCode == cboExamOrderName.SelectedValue);

                        //--start--
                        hasil.Select(
                            hasil.OrderLabNo,
                            hasil.CheckupResultFractionCode.As("LabOrderCode"),
                            hasil.CheckupResultFractionName.As("LabOrderSummary"),
                            "<CASE WHEN hp.WithinRange = '' THEN '<font color=''red''>' + hp.OutRange + '</font>' ELSE WithinRange END AS Result>",
                            (hasil.StandarValue + " " + hasil.Satuan).As("StandarValue"),
                            hasil.OrderLabTglOrder
                            );
                        hasil.OrderBy(hasil.OrderLabNo.Ascending, hasil.Seq.Ascending);
                        //--end--
                        return hasil.LoadDataTable();
                    case "RSUTAMA":
                    case "RSUI":
                        var hasil2 = new BusinessObject.Interop.RSUI.VwHasilPasienQuery("hp");

                        if (!txtExamDate1.IsEmpty && !txtExamDate2.IsEmpty)
                            hasil2.Where(hasil2.OrderLabTglOrder.Between(txtExamDate1.SelectedDate.Value.Date, txtExamDate2.SelectedDate.Value.Date));
                        if (!string.IsNullOrEmpty(cboExamOrderName.SelectedValue))
                            hasil2.Where(hasil2.LabOrderCode == cboExamOrderName.SelectedValue);

                        hasil2.Where(hasil2.PatientID == lblPatientID.Text);

                        //--start--
                        hasil2.OrderBy(hasil2.OrderLabTglOrder.Descending, hasil2.OrderLabNo.Ascending, hasil2.DispSeq.Ascending);
                        //--end--
                        return hasil2.LoadDataTable();

                }

                #endregion

                return dtb;
            }
        }
        #endregion

        #region Patient Document
        //private DataTable PatientDocumentDataTable()
        //{
        //    var query = new PatientDocumentQuery("a");
        //    query.Where(query.PatientID == PatientID);
        //    query.OrderBy(query.RegistrationNo.Descending);
        //    return query.LoadDataTable();
        //}
        private PatientDocumentCollection PatientDocumentDataTable()
        {
            var coll = new PatientDocumentCollection();
            var query = coll.Query;
            query.Where(query.PatientID == PatientID);
            query.OrderBy(query.RegistrationNo.Descending);
            coll.LoadAll();
            return coll;
        }
        protected void grdDocument_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocument.DataSource = PatientDocumentDataTable();
        }

        #endregion

        #region PHR
        protected void grdQuestionForm_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdQuestionForm.DataSource = QuestionForm();
        }

        private DataTable QuestionForm()
        {
            string serviceUnitID = Request.QueryString["unit"];
            string registrationNo = lblRegistrationNo.Text;
            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");

            query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == serviceUnitID);

            query.Where(
                query.IsActive == true
                );

            query.Select(string.Format("<'{0}' as RegistrationNo>", registrationNo),
                query.QuestionFormID,
                @"<CASE WHEN a.QuestionFormID = 'MEDHIS' then 1
                        WHEN a.QuestionFormID = 'PHYEXAM' THEN 2
                        WHEN a.QuestionFormID = 'SUMMARY' THEN 3
                        ELSE 4 END AS formID>",
                query.QuestionFormName,
                query.IsMCUForm
                );

            return query.LoadDataTable();
        }
        private DataTable PatientHeathRecordDataTable()
        {
            var query = new QuestionFormQuery("a");
            var suQr = new QuestionFormInServiceUnitQuery("s");
            var phrQr = new PatientHealthRecordQuery("b");
            var empQr = new EmployeeQuery("e");
            var reg = new RegistrationQuery("x");
            var par = new ParamedicQuery("y");
            var unit = new ServiceUnitQuery("z");

            query.InnerJoin(suQr).On(query.QuestionFormID == suQr.QuestionFormID && suQr.ServiceUnitID == ServiceUnitID);
            query.InnerJoin(phrQr).On(phrQr.QuestionFormID == query.QuestionFormID);
            query.InnerJoin(reg).On(phrQr.RegistrationNo == reg.RegistrationNo && reg.PatientID == PatientID);
            query.LeftJoin(par).On(reg.ParamedicID == par.ParamedicID);
            query.LeftJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            query.LeftJoin(empQr).On(phrQr.EmployeeID == empQr.EmployeeID);

            query.Where(query.IsActive == true);

            query.Select(
                phrQr.TransactionNo,
                reg.RegistrationNo,
                par.ParamedicName,
                unit.ServiceUnitName,
                query.QuestionFormID,
                @"<CASE WHEN a.QuestionFormID = 'MEDHIS' THEN 1
                        WHEN a.QuestionFormID = 'PHYEXAM' THEN 2
                        WHEN a.QuestionFormID = 'SUMMARY' THEN 3
                        ELSE 4 END AS formID>",
                query.QuestionFormName,
                query.IsMCUForm,
                @"<CAST(CONVERT(VARCHAR(10), b.RecordDate, 112) + ' ' + b.RecordTime AS DATETIME) AS RecordDate>",
                //phrQr.RecordDate,
                phrQr.EmployeeID,
                empQr.EmployeeName,
                phrQr.IsComplete,
                phrQr.ReferenceNo
                );

            return query.LoadDataTable();
        }

        protected void grdPHR_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (!e.CommandArgument.ToString().Contains("_")) return;

            var arg = e.CommandArgument.ToString().Split('|');
            var pars = arg[1].Split('_');

            if (arg[0] == "print")
            {
                var jobParameters = new PrintJobParameterCollection();
                jobParameters.AddNew("p_RegistrationNo", pars[0]);
                jobParameters.AddNew("p_QuestionFormID", pars[1]);
                jobParameters.AddNew("p_TransactionNo", pars[2]);

                AppSession.PrintJobParameters = jobParameters;

                var form = new QuestionForm();
                form.LoadByPrimaryKey(pars[1]);
                AppSession.PrintJobReportID = form.ReportProgramID;

                var url = Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx");
                Helper.ShowRadWindowAfterPostback(winPrintPreview, url, "preview", true);
            }
            else if (arg[0] == "delete")
            {
                using (var trans = new esTransactionScope())
                {
                    var dt = new PatientHealthRecordLineCollection();
                    dt.Query.Where(
                        dt.Query.TransactionNo == pars[2],
                        dt.Query.RegistrationNo == pars[0],
                        dt.Query.QuestionFormID == pars[1]
                        );
                    dt.Query.Load();
                    dt.MarkAllAsDeleted();
                    dt.Save();

                    var hd = new PatientHealthRecord();
                    hd.LoadByPrimaryKey(pars[2], pars[0], pars[1]);
                    hd.MarkAsDeleted();
                    hd.Save();

                    trans.Complete();
                }

            }
        }

        protected void grdPhr_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdPhr.DataSource = PatientHeathRecordDataTable();
        }
        #endregion

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            switch (eventArgument)
            {
                case "allergy":
                    PopulatePatientAllergy();
                    break;
                case "diagnose":
                    PopulateEpisodeDiagnose();
                    break;
                    //default:
                    //    if (eventArgument.Contains("void:diagnose"))
                    //    {

                    //    }
                    //    break;
            }

            //job order
            if (sourceControl is RadGrid && eventArgument == "rebind")
            {

                if ((sourceControl as RadGrid).UniqueID == grdTransChargesItem.UniqueID)
                {
                    cboServiceUnitIDJO.Enabled = !JobOrders.Any();
                    grdTransChargesItem.Rebind();
                }
            }


        }

        #region Charting
        private DataTable PatientDocumentImageChartingDatasource
        {
            get
            {
                var query = new PatientDocumentImageQuery("a");
                query.Where(query.PatientID == lblPatientID.Text); // TODO: Tambah filter image type Charting
                return query.LoadDataTable();
            }
        }

        protected void grdCharting_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCharting.DataSource = PatientDocumentImageChartingDatasource;
        }

        protected void grdCharting_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var keys = e.CommandArgument.ToString().Split('|');
            var ent = new PatientDocumentImage();
            if (ent.LoadByPrimaryKey(keys[0], Convert.ToInt32(keys[1])))
            {
                ent.MarkAsDeleted();
                ent.Save();
            }
        }
        #endregion


        protected bool IsUserCanNotAdd()
        {
            if (this.IsUserAddAble.Equals(false)) return true;

            if (string.IsNullOrEmpty(AppSession.UserLogin.ParamedicID))
            {
                // User selain dokter bisa tambah record dan hak aksesnya diset di entriannya page yg dipanggil
                return false;
            }
            else
            {
                if (AppSession.UserLogin.ParamedicID.Equals(ParamedicID))
                    return false;
            }


            return !IsUserInParamedicTeam();
        }

        private bool IsUserInParamedicTeam()
        {
            // Jika user paramedic cek Paramedic Team nya
            if (IsPostBack)
                return (bool)Session["IsUserInParamedicTeam"];

            var qrPt = new ParamedicTeamQuery("pt");
            qrPt.Where(qrPt.RegistrationNo == RegistrationNo && qrPt.ParamedicID == AppSession.UserLogin.ParamedicID &&
                       (qrPt.EndDate.IsNull() || qrPt.EndDate >= DateTime.Today));
            var dtbPt = qrPt.LoadDataTable();
            bool retval = dtbPt != null && dtbPt.Rows.Count > 0;

            Session["IsUserInParamedicTeam"] = retval;
            return retval;
        }

        protected void grdImagingResult_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                var param = e.CommandArgument.ToString().Split(';');

                var test = new TestResult();
                test.LoadByPrimaryKey(param[0], param[1]);

                txtImagingResult.Content = test.TestResult;
            }
        }

        protected void grdImagingResult_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (Convert.ToBoolean(((System.Data.DataRowView)(item.DataItem)).Row.ItemArray[2])) item["TemplateItemName1"].CssClass = "text";
            }
        }

        protected void grdImagingResult_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdImagingResult.DataSource = ImagingResultCollections;
        }

        private DataTable ImagingResultCollections
        {
            get
            {
                var mrgs = new MergeBillingCollection();
                mrgs.Query.Where(mrgs.Query.FromRegistrationNo == RegistrationNo);
                mrgs.Query.Load();

                var mrg = mrgs.AddNew();
                mrg.RegistrationNo = RegistrationNo;
                mrg.FromRegistrationNo = string.Empty;

                var test = new TestResultQuery("a");
                var tci = new TransChargesItemQuery("b");
                var med = new ParamedicQuery("c");
                var item = new ItemQuery("d");
                var tc = new TransChargesQuery("e");
                var reg = new RegistrationQuery("f");

                test.Select
                    (
                        test.TransactionNo,
                        test.TestResultDateTime,
                        med.ParamedicName,
                        test.ItemID,
                        item.ItemName
                    );
                test.InnerJoin(tci).On(test.TransactionNo == tci.TransactionNo && test.ItemID == tci.ItemID && tci.IsVoid == false);
                test.InnerJoin(tc).On(test.TransactionNo == tc.TransactionNo && tc.IsVoid == false);
                test.InnerJoin(reg).On(tc.RegistrationNo == reg.RegistrationNo && reg.RegistrationNo.In(mrgs.Select(m => m.RegistrationNo)));
                test.InnerJoin(med).On(test.ParamedicID == med.ParamedicID);
                test.InnerJoin(item).On(test.ItemID == item.ItemID);

                test.OrderBy(test.TestResultDateTime.Descending);

                return test.LoadDataTable();
            }
        }

        protected void cboItemIDJO_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            DataTable tbl = PopulateServiceItem(e.Text);
            (o as RadComboBox).DataSource = tbl.Rows.Count == 0 ? PopulateServiceItem(e.Text) : tbl;
            (o as RadComboBox).DataBind();
        }

        protected void cboItemIDJO_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        private DataTable PopulateServiceItem(string searchText)
        {
            DataTable tbl = null;
            string searchTextContain = string.Format("%{0}%", searchText);
            try
            {
                var query = new ItemQuery("a");
                var itemUnit = new ServiceUnitItemServiceQuery("c");

                query.es.Top = 15;
                query.Select
                    (
                        query.ItemID,
                        (query.ItemName + " [" + query.ItemID + "]").As("ItemName")
                    );
                query.InnerJoin(itemUnit).On
                    (
                        query.ItemID == itemUnit.ItemID &&
                        itemUnit.ServiceUnitID == cboServiceUnitIDJO.SelectedValue
                    );
                query.Where
                    (
                        query.Or
                            (
                                query.ItemName.Like(searchTextContain),
                                query.ItemID.Like(searchTextContain)
                            ),
                        query.IsActive == true
                    );
                query.OrderBy(query.ItemName.Ascending);

                tbl = query.LoadDataTable();

                String item2 = string.Empty;

                foreach (DataRow row in tbl.Rows)
                {
                    var item1 = (string)row["ItemID"];
                    if (item1 != item2)
                        item2 = (string)row["ItemID"];
                    else
                        row.Delete();
                }

                tbl.AcceptChanges();
            }
            catch
            {
            }

            return tbl;
        }

        private ItemTariffQuery GetItemTariffQuery(string tariffType, string classID, string itemID)
        {
            var query = new ItemTariffQuery();
            query.es.Top = 1;
            query.Where
                (
                    query.SRTariffType == tariffType,
                    query.ClassID == classID,
                    query.ItemID == itemID,
                    query.StartingDate <= DateTime.Now.Date
                );
            query.OrderBy(query.StartingDate.Descending);

            return query;
        }

        private TransChargesItemCollection JobOrders
        {
            get
            {
                if (Session["collTransChargesItem" + Request.UserHostName] == null)
                {
                    var query = new TransChargesItemQuery("a");
                    query.Select(
                        query,
                        "<'' AS refToItem_ItemName>"
                        );
                    query.Where(query.TransactionNo == string.Empty);

                    var coll = new TransChargesItemCollection();
                    coll.Load(query);

                    Session["collTransChargesItem" + Request.UserHostName] = coll;
                }
                return Session["collTransChargesItem" + Request.UserHostName] as TransChargesItemCollection;
            }
            set { Session["collTransChargesItem" + Request.UserHostName] = value; }
        }

        protected void grdTransChargesItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdTransChargesItem.DataSource = JobOrders;
        }

        protected void grdTransChargesItem_DeleteCommand(object source, GridCommandEventArgs e)
        {
            var item = e.Item as GridDataItem;
            if (item == null) return;

            var entity = JobOrders.SingleOrDefault(j => j.SequenceNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SequenceNo"]));
            if (entity == null) return;
            entity.MarkAsDeleted();

            cboServiceUnitIDJO.Enabled = !JobOrders.Any();
            grdTransChargesItem.Rebind();
        }

        protected void grdTransChargesItem_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                var cmdItem = grdTransChargesItem.MasterTableView.GetItems(GridItemType.CommandItem)[0];

                if (string.IsNullOrEmpty((cmdItem.FindControl("cboItemIDJO") as RadComboBox).SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "order", "alert('Item order required');", true);
                    return;
                }

                var detail = JobOrders.SingleOrDefault(j => j.ItemID == (cmdItem.FindControl("cboItemIDJO") as RadComboBox).SelectedValue);
                if (detail != null) return;

                var reg = new Registration();
                reg.LoadByPrimaryKey(lblRegistrationNo.Text);

                var grr = new Guarantor();
                grr.LoadByPrimaryKey(reg.GuarantorID);

                detail = JobOrders.AddNew();

                detail.TransactionNo = string.Empty;

                var seqNo = (JobOrders.OrderByDescending(j => j.SequenceNo)).Take(1).SingleOrDefault();
                detail.SequenceNo = (seqNo == null || string.IsNullOrEmpty(seqNo.SequenceNo)) ? "001" : string.Format("{0:000}", int.Parse(seqNo.SequenceNo) + 1);

                detail.ParentNo = string.Empty;
                detail.ReferenceNo = string.Empty;
                detail.ReferenceSequenceNo = string.Empty;
                detail.ItemID = (cmdItem.FindControl("cboItemIDJO") as RadComboBox).SelectedValue;
                detail.ItemName = (cmdItem.FindControl("cboItemIDJO") as RadComboBox).Text;
                detail.ChargeClassID = reg.ChargeClassID;
                detail.ParamedicID = ParamedicID;

                var tariff = new ItemTariff();
                if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, reg.ChargeClassID, detail.ItemID)))
                    if (!tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, reg.ChargeClassID, detail.ItemID)))
                        if (!tariff.Load(GetItemTariffQuery(grr.SRTariffType, AppSession.Parameter.DefaultTariffClass, detail.ItemID)))
                            tariff.Load(GetItemTariffQuery(AppSession.Parameter.DefaultTariffType, AppSession.Parameter.DefaultTariffClass, detail.ItemID));

                detail.IsAdminCalculation = tariff.IsAdminCalculation;
                detail.IsVariable = false;
                detail.IsCito = false;
                detail.ChargeQuantity = 1;
                detail.StockQuantity = 0;
                detail.SRItemUnit = "X";
                detail.CostPrice = 0;
                detail.Price = tariff.Price;
                detail.CitoAmount = !(detail.IsCito ?? false) ? 0 : ((!tariff.IsCitoInPercent ?? false) ? tariff.CitoValue : (tariff.CitoValue / 100) * detail.Price);
                detail.RoundingAmount = Helper.RoundingDiff;
                detail.SRDiscountReason = string.Empty;
                detail.IsAssetUtilization = false;
                detail.AssetID = string.Empty;
                detail.IsBillProceed = false;
                detail.IsOrderRealization = false;
                detail.IsPackage = false;
                detail.IsVoid = false;
                detail.Notes = string.Empty;
                detail.IsItemTypeService = true;
                detail.SRCenterID = string.Empty;
                detail.IsApprove = false;

                cboServiceUnitIDJO.Enabled = !JobOrders.Any();
                grdTransChargesItem.Rebind();
            }
        }

        protected void tbJobOrder_Click(object sender, RadToolBarEventArgs e)
        {
            if (!Validate) return;

            switch (e.Item.Value)
            {
                case "new":
                    ViewState["TransactionNo" + Request.UserHostName] = string.Empty;

                    JobOrders = null;
                    grdTransChargesItem.Rebind();

                    cboServiceUnitIDJO.SelectedValue = string.Empty;
                    cboServiceUnitIDJO.Enabled = !JobOrders.Any();
                    txtTransactionDateJO.SelectedDate = DateTime.Now.Date;
                    txtNotesJO.Text = string.Empty;
                    break;
                case "save":
                    if (!JobOrders.Any()) return;

                    var reg = new Registration();
                    reg.LoadByPrimaryKey(lblRegistrationNo.Text);

                    var grr = new Guarantor();
                    grr.LoadByPrimaryKey(reg.GuarantorID);

                    TransCharges header;
                    AppAutoNumberLast autoNumber = null;

                    if (string.IsNullOrEmpty(ViewState["TransactionNo" + Request.UserHostName].ToString()))
                    {
                        autoNumber = Helper.GetNewAutoNumber(txtTransactionDateJO.SelectedDate.Value.Date, AppEnum.AutoNumber.JobOrderNo);
                        header = new TransCharges
                        {
                            TransactionNo = autoNumber.LastCompleteNumber,
                            RegistrationNo = lblRegistrationNo.Text,
                            TransactionDate = txtTransactionDateJO.SelectedDate,
                            ExecutionDate = txtTransactionDateJO.SelectedDate,
                            ReferenceNo = string.Empty,
                            ResponUnitID = string.Empty,
                            FromServiceUnitID = reg.ServiceUnitID,
                            ToServiceUnitID = cboServiceUnitIDJO.SelectedValue,
                            SRTypeResult = string.Empty,
                            ClassID = reg.ChargeClassID,
                            RoomID = reg.RoomID,
                            BedID = reg.BedID,
                            DueDate = txtTransactionDateJO.SelectedDate,
                            SRShift = Registration.GetShiftID(),
                            SRItemType = string.Empty,
                            IsProceed = true,
                            IsApproved = true,
                            IsVoid = false,
                            IsAutoBillTransaction = false,
                            IsBillProceed = false,
                            IsOrder = true,
                            IsCorrection = false,
                            Notes = txtNotesJO.Text,
                            LastUpdateByUserID = AppSession.UserLogin.UserID,
                            LastUpdateDateTime = DateTime.Now,
                            CreatedByUserID = AppSession.UserLogin.UserID,
                            CreatedDateTime = DateTime.Now
                        };
                    }
                    else
                    {
                        header = new TransCharges();
                        header.LoadByPrimaryKey(ViewState["TransactionNo" + Request.UserHostName].ToString());
                        header.TransactionDate = txtTransactionDateJO.SelectedDate;
                        header.ToServiceUnitID = cboServiceUnitIDJO.SelectedValue;
                        header.Notes = txtNotesJO.Text;
                    }

                    foreach (var detail in JobOrders)
                    {
                        detail.TransactionNo = string.IsNullOrEmpty(ViewState["TransactionNo" + Request.UserHostName].ToString()) ? header.TransactionNo : ViewState["TransactionNo" + Request.UserHostName].ToString();
                    }

                    using (var trans = new esTransactionScope())
                    {
                        if (string.IsNullOrEmpty(ViewState["TransactionNo" + Request.UserHostName].ToString())) autoNumber.Save();
                        header.Save();
                        JobOrders.Save();

                        trans.Complete();
                    }

                    tbJobOrder_Click(null, new RadToolBarEventArgs(tbJobOrder.Items[0]));

                    grdJobOrder.Rebind();
                    break;
            }
        }

        protected void grdJobOrder_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                PrintJobParameterCollection jobParameters = new PrintJobParameterCollection();
                PrintJobParameter jobParameter;

                jobParameter = jobParameters.AddNew();
                jobParameter.Name = "TransactionNo";
                jobParameter.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.JobOrderNotesDiagnostic;

                //string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                //"oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                //"oWnd.Show();" +
                //"oWnd.Maximize();";
                //RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "Update")
            {
                ViewState["TransactionNo" + Request.UserHostName] = e.CommandArgument;

                var header = new TransCharges();
                header.LoadByPrimaryKey(e.CommandArgument.ToString());

                if (header.TransactionDate.Value.Date < DateTime.Now.Date)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid void to system');", true);
                    return;
                }

                if (header.IsBillProceed ?? false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "validated", "alert('Transaction already validated by unit');", true);
                    return;
                }

                cboServiceUnitIDJO.SelectedValue = header.ToServiceUnitID;
                cboServiceUnitIDJO.Enabled = false;

                txtTransactionDateJO.SelectedDate = header.TransactionDate.Value.Date;
                txtNotesJO.Text = header.Notes;

                var query = new TransChargesItemQuery("a");
                var item = new ItemQuery("b");

                query.Select(
                    query,
                    item.ItemName.As("refToItem_ItemName")
                    );
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.Where(query.TransactionNo == e.CommandArgument);

                var coll = new TransChargesItemCollection();
                coll.Load(query);

                JobOrders = coll;

                grdTransChargesItem.Rebind();
            }
            //if (e.CommandName == RadGrid.ExpandCollapseCommandName && e.Item is GridDataItem)
            //{
            //    ((GridDataItem)e.Item).ChildItem.FindControl("InnerContainer").Visible = !e.Item.Expanded;
            //    //((GridDataItem)e.Item).ChildItem.FindControl("Panel1").Visible = !e.Item.Expanded;
            //}
        }

        protected void grdJobOrder_DeleteCommand(object source, GridCommandEventArgs e)
        {
            if (!Validate) return;

            var item = e.Item as GridDataItem;
            if (item == null) return;

            if (Convert.ToDateTime(item["TransactionDate"].Text).Date < DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid", "alert('Invalid void to system');", true);
                return;
            }

            using (var trans = new esTransactionScope())
            {
                var coll = new TransChargesItemCollection();
                coll.Query.Where(coll.Query.TransactionNo == Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["TransactionNo"]));
                coll.LoadAll();

                foreach (var c in coll)
                {
                    if (c.IsOrderRealization ?? false) continue;

                    c.IsApprove = false;
                    c.IsBillProceed = false;
                    c.IsVoid = true;
                }

                coll.Save();

                if (coll.Count() == coll.Count(c => c.IsVoid ?? false))
                {
                    var entity = new TransCharges();
                    entity.LoadByPrimaryKey(Convert.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["TransactionNo"]));
                    if (!(entity.IsVoid ?? false))
                    {
                        entity.IsApproved = false;
                        entity.IsBillProceed = false;
                        entity.IsVoid = true;

                        entity.Save();
                    }
                }

                trans.Complete();
            }


            grdJobOrder.Rebind();
        }

        protected void grdJobOrder_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (Convert.ToBoolean(((System.Data.DataRowView)(item.DataItem)).Row.ItemArray[2])) item["TemplateItemName1"].CssClass = "text";
            }
        }

        protected void grdJobOrder_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdJobOrder.DataSource = TransChargesCollections;
        }

        private DataTable TransChargesCollections
        {
            get
            {
                var query = new TransChargesItemQuery("a");
                var presc = new TransChargesQuery("b");
                var item = new ItemQuery("c");
                var reg = new RegistrationQuery("f");
                var unit = new ServiceUnitQuery("x");

                query.Select(
                    query.TransactionNo,
                    query.SequenceNo,
                    presc.TransactionDate,
                    item.ItemName,
                    query.ChargeQuantity,
                    query.SRItemUnit,
                    query.IsApprove,
                    query.IsOrderRealization,
                    query.IsVoid,
                    unit.ServiceUnitName
                    );

                query.InnerJoin(presc).On(query.TransactionNo == presc.TransactionNo);
                query.InnerJoin(item).On(query.ItemID == item.ItemID);
                query.InnerJoin(reg).On(presc.RegistrationNo == reg.RegistrationNo);
                query.InnerJoin(unit).On(presc.ToServiceUnitID == unit.ServiceUnitID);

                var mrgs = new MergeBillingCollection();
                mrgs.Query.Where(mrgs.Query.FromRegistrationNo == RegistrationNo);
                if (mrgs.Query.Load())
                    query.Where(
                        reg.RegistrationNo.In(mrgs.Select(m => m.RegistrationNo), RegistrationNo),
                        presc.IsOrder == true,
                        presc.IsVoid == false
                        );
                else
                    query.Where(
                        reg.RegistrationNo.In(RegistrationNo),
                        presc.IsOrder == true,
                        presc.IsVoid == false
                        );
                query.Where(
                    presc.IsOrder == true,
                    presc.IsVoid == false
                    );

                query.OrderBy(
                    //presc.TransactionDate.Descending,
                    presc.TransactionNo.Descending
                    //query.SequenceNo.Ascending
                    );
                var table = query.LoadDataTable();

                //var prescs = table.AsEnumerable().GroupBy(t => t.Field<string>("TransactionNo")).Select(g => g);

                var prescs = from t in table.AsEnumerable()
                             group t by new
                             {
                                 TransactionNo = t.Field<string>("TransactionNo"),
                                 TransactionDate = t.Field<DateTime>("TransactionDate")
                             } into g
                             select g;


                var dtb = JobOrderTable.Clone();

                string summary = string.Empty;

                foreach (var p in prescs)
                {
                    var row = dtb.NewRow();

                    summary = "<table width='100%' cellpadding='0' cellspacing='0'>";
                    int i = 0;
                    foreach (DataRow r in table.AsEnumerable().Where(t => t.Field<string>("TransactionNo") == p.Key.TransactionNo))
                    {
                        if (i == 0)
                        {
                            summary += "<tr><td style='font-weight: bold'>";
                            summary += string.Format("{0} - {1}<br />{2}<br />", r["TransactionNo"],
                                Convert.ToDateTime(r["TransactionDate"]).ToString(AppConstant.DisplayFormat.DateShortMonth), r["ServiceUnitName"]);
                            summary += "</td></tr><tr><td align='left' style='font-style: italic'>";

                            row[2] = r["IsVoid"];
                        }
                        i++;

                        summary += string.Format("{0} {1} {2} {3}<br />", r["ItemName"], r["ChargeQuantity"], r["SRItemUnit"],
                            (Convert.ToBoolean(r["IsOrderRealization"]) ? "<img src='../../../Images/Toolbar/post16.png' />" : "<img src='../../../Images/Toolbar/post16_d.png' />"));
                    }
                    summary += "</td></tr></table>";

                    row[0] = p.Key.TransactionNo;
                    row[1] = summary;
                    row[3] = p.Key.TransactionDate;

                    dtb.Rows.Add(row);
                }
                return dtb;
            }
        }

        private DataTable JobOrderTable
        {
            get
            {
                if (ViewState["JobOrderTable" + Request.UserHostName] != null) return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;

                var table = new DataTable();
                table.Columns.Add(new DataColumn("TransactionNo", typeof(string)));
                table.Columns.Add(new DataColumn("JobOrderSummary", typeof(string)));
                table.Columns.Add(new DataColumn("IsVoid", typeof(bool)));
                table.Columns.Add(new DataColumn("TransactionDate", typeof(DateTime)));

                ViewState["JobOrderTable" + Request.UserHostName] = table;
                return ViewState["JobOrderTable" + Request.UserHostName] as DataTable;
            }
        }

        private bool Validate
        {
            get
            {
                if (!IsUserEditAble)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "unauthorized", "alert('Unauthorized access to system');", true);
                    return false;
                }
                return true;
            }
        }
    }
}
