using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Text;
using Telerik.Charting;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class VitalSignChartEws : BasePageDialog
    {
        //private string EwsType => string.IsNullOrWhiteSpace(Request.QueryString["ewstype"]) ? "OEWS" : Request.QueryString["ewstype"];
        private string EwsType => Request.QueryString["ewstype"];
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        private Registration _curentReg = null;
        private Registration CurrentRegistration
        {
            get
            {
                if (!string.IsNullOrEmpty(RegistrationNo) && _curentReg == null)
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);

                    _curentReg = reg;
                }
                return _curentReg;
            }
        }
        public override string PatientID
        {
            get
            {
                if (!string.IsNullOrEmpty(RegistrationNo) && _curentReg == null)
                    return CurrentRegistration.PatientID;
                else
                    return Request.QueryString["patid"];
            }
        }
        private string FromRegistrationNo
        {
            get { return Request.QueryString["fregno"]; }
        }

        private DateTime DateOfBirth
        {
            set { ViewState["dob"] = value; }
            get { return Convert.ToDateTime(ViewState["dob"]); }
        }

        private int TemplateID
        {
            get
            {
                if (ViewState["qif"] == null)
                {
                    ViewState["qif"] = "0";
                    if (!string.IsNullOrEmpty(EwsType))
                    {
                        // Check Template
                        var nd = new NursingDiagnosaTemplate();
                        nd.Query.Where(nd.Query.TemplateName == EwsType, nd.Query.IsActive == true);
                        nd.Query.es.Top = 1;
                        if (nd.Query.Load())
                            ViewState["qif"] = nd.TemplateID.ToString();
                    }
                }
                return Convert.ToInt32(ViewState["qif"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ButtonOk.Visible = false;
            //ButtonCancel.Text = "Close";

            FooterVisible = false;


            if (!IsPostBack)
            {
                var toDay = DateTime.Today;
                txtFromDate.SelectedDate = toDay;
                switch (EwsType)
                {
                    case "OEWS":
                        Title = "Early Warning Score w/ Data From All TTV";
                        txtFromDate.SelectedDate = VitalSign.LastVitalSignDate(RegistrationNo, string.Empty);
                        break;
                    case "EWS":
                        Title = "Early Warning Score (EWS)";
                        break;
                    case "MEOWS":
                        Title = "Maternal Early Obstetric Warning Score (MEOWS)";
                        break;
                    case "PEWS":
                        Title = "Pediatric Early Warning Score (PEWS)";
                        break;
                    default:
                        Title = "Vital Sign History";
                        break;
                }

                var pat = new Patient();
                pat.LoadByPrimaryKey(PatientID);

                lblPatientName.Text = pat.PatientName;
                lblMedicalNo.Text = pat.MedicalNo;
                lblRegistrationNo.Text = RegistrationNo;
                lblSex.Text = pat.Sex;

                if (pat.DateOfBirth != null)
                {
                    var birthDate = pat.DateOfBirth.Value;
                    lblBirthDate.Text = birthDate.ToString(AppConstant.DisplayFormat.Date);

                    lblAge.Text = string.Format("{0}Y, {1}M, {2}D",
                        Helper.GetAgeInYear(birthDate, toDay), Helper.GetAgeInMonth(birthDate, toDay),
                        Helper.GetAgeInDay(birthDate, toDay));
                }


                DateOfBirth = pat.DateOfBirth ?? toDay;
                PopulateVitalSignChartControlByQuestion(TemplateID, RegistrationNo, FromRegistrationNo);
            }
        }

        #region EWS old version
        [Obsolete("EWS model lama yg bisa salah jika entrinya tidak dalam 1 template")]
        private void PopulateVitalSignChartControl()
        {
            var colCount = 0;
            var vitalSignItemValues = new List<VitalSignItemValue>();
            int ageInDay = (txtFromDate.SelectedDate.Value - DateOfBirth).Days;
            var ewsColl = new VitalSignEwsCollection();

            // SubQuery
            var subQr = new VitalSignEwsQuery("a");
            subQr.Select(subQr.StartAgeInDay);
            subQr.Where(subQr.SREwsType == "OEWS", subQr.StartAgeInDay <= ageInDay, subQr.EndAgeInDay >= ageInDay, subQr.VitalSignID == ewsColl.Query.VitalSignID);
            subQr.es.Top = 1;
            subQr.OrderBy(subQr.StartAgeInDay.Descending);

            // Query
            ewsColl.Query.Where(ewsColl.Query.StartAgeInDay == (subQr));
            ewsColl.Query.OrderBy(ewsColl.Query.IndexNo.Ascending);
            ewsColl.LoadAll();


            foreach (var ews in ewsColl)
            {
                var vitalSignID = string.Empty;

                // VitalSign tipe grup pada EWS grafiknya bisa terpisah supaya score nya ikut dihitung (Handono 230930 -> RS GPI)
                ////// Cek apakah merupakan grup VitalSign
                ////var vsQr = new VitalSignQuery("v");
                ////vsQr.Where(vsQr.ParentVitalSignID == ews.VitalSignID);
                ////vsQr.es.Top = 1;

                ////var vs = new VitalSign();
                ////vitalSignID = vs.Load(vsQr) ? vs.VitalSignID : ews.VitalSignID;

                vitalSignID = ews.VitalSignID;
                var ctl = (VitalSignChartCtl)LoadControl("~/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartCtl.ascx");
                pnlChart.Controls.Add(ctl);
                var results = ctl.PopulateChart(PatientID, DateOfBirth, vitalSignID, txtFromDate.SelectedDate.Value, txtFromDate.SelectedDate.Value, true);

                if (!string.IsNullOrEmpty(EwsType))
                {
                    colCount = results.Count;
                    foreach (var result in results)
                    {
                        // Insert ke _vitalSignItemValues
                        var isFound = false;
                        foreach (var summary in vitalSignItemValues)
                        {
                            //if (value.No.Equals(result.No))
                            if (result.TransactionNo != null && result.TransactionNo.Equals(summary.TransactionNo) && result.Time.Equals(summary.Time))
                            {
                                isFound = true;
                                summary.TotalScore = summary.TotalScore + result.Level;  // Total Level
                                if (summary.Time.ToString("HH:mm") == "00:00")
                                    summary.Time = result.Time;

                                if (!summary.IsExistLevel3)
                                    summary.IsExistLevel3 = result.Level == 3;

                                break;
                            }
                        }

                        if (!isFound)
                        {
                            if (!string.IsNullOrEmpty(result.TransactionNo))
                            {
                                var isExistLevel3 = result.Level == 3;
                                vitalSignItemValues.Add(new VitalSignItemValue()
                                {
                                    No = result.No,
                                    TransactionNo = result.TransactionNo,
                                    Time = result.Time,
                                    VitalSignID = result.VitalSignID,
                                    TotalScore = result.Level,
                                    IsExistLevel3 = isExistLevel3
                                });
                            }
                        }
                    }

                    // Hitung Nilai EWS
                    foreach (var summary in vitalSignItemValues)
                    {
                        if (!string.IsNullOrEmpty(summary.TransactionNo))
                        {
                            if (summary.TotalScore < 5)
                                summary.Level = summary.IsExistLevel3 ? 2 : 1;
                            else if (summary.TotalScore < 7)
                                summary.Level = 2; // Yellow
                            else
                                summary.Level = 3; // Red
                        }
                    }

                }
            }

            if (!string.IsNullOrEmpty(EwsType))
            {
                // Tambah kolom
                vitalSignItemValues = vitalSignItemValues.OrderBy(o => o.Time).ToList();
                if (vitalSignItemValues.Count < colCount)
                {
                    var length = colCount - vitalSignItemValues.Count;
                    for (int i = 0; i < length; i++)
                    {
                        vitalSignItemValues.Add(new VitalSignItemValue());
                    }
                }
                litEwsTotal.Text = EwsTotalScoreLevelHtml_Old(vitalSignItemValues);
            }

        }

        private string EwsTotalScoreLevelHtml_Old(List<VitalSignItemValue> vitalSignItemValues)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><th style = \"width: 80px\"> &nbsp; Time&nbsp;</th></tr>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp; Score&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Caption
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in vitalSignItemValues)
            {
                var time = value.Time.ToString("HH:mm");
                sb.AppendLine("<th>");
                sb.AppendLine(time == "00:00" ? "&nbsp;" : time);
                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            // Score
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in vitalSignItemValues)
            {
                if (value.Time.ToString("HH:mm") != "00:00")
                {
                    sb.AppendFormat("<td style='background-color:{0};'>", VitalSign.EwsLevelColor(value.Level));
                    sb.AppendFormat("{0:0}", value.TotalScore);
                }
                else
                {
                    sb.Append("<td>&nbsp;");
                }

                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        #endregion EWS old version

        private void PopulateVitalSignChartControlByQuestion(int templateID, string registrationNo, string fromRegistrationNo)
        {
            if (templateID == 0)
            {
                PopulateVitalSignChartControl(); // EWS w/ no template (old version)
                return;
            }

            var totalVsItemValues = new List<VitalSignItemValue>();
            var vsItemValues = new List<VitalSignItemValue>();
            double ageInDay = (txtFromDate.SelectedDate.Value - DateOfBirth).TotalDays;

            var ndtdColl = new NursingDiagnosaTemplateDetailCollection();
            ndtdColl.Query.Where(ndtdColl.Query.TemplateID == templateID);
            ndtdColl.Query.OrderBy(ndtdColl.Query.RowIndex.Ascending);
            ndtdColl.LoadAll();

            foreach (var ndtd in ndtdColl)
            {
                var isVitalSignType = false;
                var quest = new Question();
                if (!quest.LoadByPrimaryKey(ndtd.QuestionID)) continue;

                if (!string.IsNullOrEmpty(quest.VitalSignID))
                {
                    var ews = new VitalSignEws();
                    ews.Query.Where(ews.Query.SREwsType == EwsType, ews.Query.VitalSignID == quest.VitalSignID, ews.Query.StartAgeInDay <= ageInDay, ews.Query.EndAgeInDay >= ageInDay);
                    ews.Query.es.Top = 1;
                    isVitalSignType = ews.Query.Load();
                }

                var ctl = (VitalSignChartCtl)LoadControl("~/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartCtl.ascx");
                pnlChart.Controls.Add(ctl);
                if (isVitalSignType)
                    vsItemValues = ctl.PopulateChartByQuestion(PatientID, DateOfBirth, registrationNo, fromRegistrationNo, quest.VitalSignID, templateID.ToString(), quest.QuestionID, quest.SRAnswerType, quest.AnswerSuffix, txtFromDate.SelectedDate.Value, txtFromDate.SelectedDate.Value, EwsType, true);
                else
                    vsItemValues = ctl.PopulateChartTableByQuestion(PatientID, DateOfBirth, registrationNo, fromRegistrationNo, quest.QuestionAnswerSelectionID, templateID.ToString(), quest.QuestionID, quest.SRAnswerType, quest.AnswerSuffix, txtFromDate.SelectedDate.Value, txtFromDate.SelectedDate.Value, quest.QuestionText, EwsType);

                // Copy to Summary total
                foreach (var result in vsItemValues)
                {
                    var isFound = false;
                    foreach (var value in totalVsItemValues)
                    {
                        if (value.No.Equals(result.No))
                        {
                            isFound = true;
                            value.TotalScore = value.TotalScore + result.Level;  // Total Level
                            value.Level2Count = value.Level2Count + (result.Level == 2 ? 1 : 0);  // Yellow Score
                            value.Level3Count = value.Level3Count + (result.Level == 3 ? 1 : 0);  // Red Score

                            if (value.Time.ToString("HH:mm") == "00:00")
                                value.Time = result.Time;

                            if (!value.IsExistLevel3)
                                value.IsExistLevel3 = result.Level == 3;

                            break;
                        }
                    }

                    if (!isFound)
                    {
                        if (!string.IsNullOrEmpty(result.TransactionNo))
                        {
                            var isExistLevel3 = result.Level == 3;
                            totalVsItemValues.Add(new VitalSignItemValue()
                            {
                                No = result.No,
                                TransactionNo = result.TransactionNo,
                                Time = result.Time,
                                VitalSignID = result.VitalSignID,
                                TotalScore = result.Level,
                                Level2Count = (result.Level == 2 ? 1 : 0),
                                Level3Count = (result.Level == 3 ? 1 : 0),
                                IsExistLevel3 = isExistLevel3
                            });
                        }
                        else
                            totalVsItemValues.Add(new VitalSignItemValue()
                            {
                                No = result.No
                            });
                    }

                }

                // Hitung Nilai EWS
                foreach (var value in totalVsItemValues)
                {
                    if (value.TotalScore < 5)
                        value.Level = value.IsExistLevel3 ? 2 : 1;
                    else if (value.TotalScore < 7)
                        value.Level = 2; // Yellow
                    else
                        value.Level = 3; // Red

                }
            }

            // Tambah kolom kosong
            if (totalVsItemValues.Count == 0)
            {
                // 26 col
                for (int i = 0; i < 26; i++)
                {
                    totalVsItemValues.Add(new VitalSignItemValue()
                    {
                        No = i + 1
                    });

                }
            }

            switch (EwsType)
            {
                case "EWS":
                case "PEWS":
                    litEwsTotal.Text = EwsTotalScoreLevelHtml(totalVsItemValues);
                    break;
                case "MEOWS":
                    litEwsTotal.Text = MeowsTotalScoreLevelHtml(totalVsItemValues);
                    break;
            }
        }
        #region Navigation
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateVitalSignChartControlByQuestion(TemplateID, RegistrationNo, FromRegistrationNo);
        }
        protected void txtFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            PopulateVitalSignChartControlByQuestion(TemplateID, RegistrationNo, FromRegistrationNo);
        }
        protected void btnPrevDate_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = txtFromDate.SelectedDate.Value.AddDays(-1);
            PopulateVitalSignChartControlByQuestion(TemplateID, RegistrationNo, FromRegistrationNo);
        }

        protected void btnNextDate_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = txtFromDate.SelectedDate.Value.AddDays(1);
            PopulateVitalSignChartControlByQuestion(TemplateID, RegistrationNo, FromRegistrationNo);
        }
        protected void btnStartFromRegistration_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = CurrentRegistration.RegistrationDate;
            PopulateVitalSignChartControlByQuestion(TemplateID, RegistrationNo, FromRegistrationNo);
        }

        protected void btnLastMonitoring_Click(object sender, EventArgs e)
        {
            if (EwsType == "OEWS")
                txtFromDate.SelectedDate = VitalSign.LastVitalSignDate(RegistrationNo, string.Empty);
            else
            {
                var lastMon = LastMonitoringDate(RegistrationNo, FromRegistrationNo, TemplateID);
                if (lastMon != null && lastMon.Year == 1900) { lastMon = DateTime.Today; }
                txtFromDate.SelectedDate = lastMon;
            }
            PopulateVitalSignChartControlByQuestion(TemplateID, RegistrationNo, FromRegistrationNo);
        }
        private DateTime LastMonitoringDate(string registrationNo, string referFromRegistrationNo, int templateID)
        {
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var quest = new QuestionQuery("q");
            phrl.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            var phr = new PatientHealthRecordQuery("phr");
            phrl.InnerJoin(phr).On(phrl.TransactionNo == phr.TransactionNo & phrl.RegistrationNo == phr.RegistrationNo);

            if (!string.IsNullOrEmpty(referFromRegistrationNo))
                phrl.Where(phrl.Or(phrl.RegistrationNo == registrationNo, phrl.RegistrationNo == referFromRegistrationNo));
            else
                phrl.Where(phrl.RegistrationNo == registrationNo);


            phrl.Where(phrl.QuestionFormID == templateID.ToString());

            phrl.Select(phr.RecordDate);
            phrl.OrderBy(phr.RecordDate.Descending);
            phrl.es.Top = 1;
            var dtb = phrl.LoadDataTable();

            var lastMonDate = new DateTime(1900, 1, 1);

            // Ambil yg terakhir 
            foreach (DataRow row in dtb.Rows)
            {
                lastMonDate = Convert.ToDateTime(row["RecordDate"]);
            }

            return lastMonDate;
        }
        #endregion Navigation

        private string EwsTotalScoreLevelHtml(List<VitalSignItemValue> vsItemValues)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 120px;\">");
            sb.AppendLine("         <table width=\"100%\" class='tblgraph'>");
            sb.AppendLine("             <tr><th> &nbsp; Time&nbsp;</th></tr>");
            sb.AppendLine("             <tr><td> &nbsp; Score&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Caption
            sb.AppendLine("<tr>");
            var isNewMenuCreated = false;
            foreach (VitalSignItemValue value in vsItemValues)
            {
                var time = value.Time.ToString("HH:mm");
                sb.AppendLine("<th>");
                if (string.IsNullOrWhiteSpace(value.TransactionNo))
                {
                    if (!isNewMenuCreated)
                    {
                        var newImg = string.Format("<img src='{0}/Images/Toolbar/new16.png'/>", Helper.UrlRoot());
                        sb.AppendFormat("<a style=\"cursor:pointer;\" onclick=\"entryQuestionRespond('{0}', '{1}', '{2}', '{3}', '{4}')\">{5}</a>", "new", value.TransactionNo, RegistrationNo, TemplateID, "", newImg);
                        isNewMenuCreated = true;
                    }
                    else
                        sb.AppendLine("&nbsp;");
                }
                else
                    sb.AppendFormat("<a style=\"cursor:pointer;\" onclick=\"entryQuestionRespond('{0}', '{1}', '{2}', '{3}', '{4}')\">{5}</a>", "view", value.TransactionNo, RegistrationNo, TemplateID, "", time);

                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            // Score
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in vsItemValues)
            {
                if (!string.IsNullOrWhiteSpace(value.TransactionNo))
                {
                    sb.AppendFormat("<td style='background-color:{0};'>", VitalSign.EwsLevelColor(value.Level));
                    sb.AppendFormat("{0:0}", value.TotalScore);
                }
                else
                {
                    sb.Append("<td>&nbsp;");
                }

                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        private string MeowsTotalScoreLevelHtml(List<VitalSignItemValue> vsItemValues)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 120px;\">");
            sb.AppendLine("         <table width=\"100%\" class='tblgraph'>");
            sb.AppendLine("             <tr><th> &nbsp; Time&nbsp;</th></tr>");
            sb.AppendLine("             <tr><td> &nbsp; Score&nbsp;</td></tr>");
            sb.AppendLine("             <tr><td> &nbsp; Yellow&nbsp;</td></tr>");
            sb.AppendLine("             <tr><td> &nbsp; Red&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Caption
            sb.AppendLine("<tr>");
            var isNewMenuCreated = false;
            foreach (VitalSignItemValue value in vsItemValues)
            {
                var time = value.Time.ToString("HH:mm");
                sb.AppendLine("<th>");

                if (string.IsNullOrWhiteSpace(value.TransactionNo))
                    if (!isNewMenuCreated)
                    {
                        var newImg = string.Format("<img src='{0}/Images/Toolbar/new16.png'/>", Helper.UrlRoot());
                        sb.AppendFormat("<a style=\"cursor:pointer;\" onclick=\"entryQuestionRespond('{0}', '{1}', '{2}', '{3}', '{4}')\">{5}</a>", "new", value.TransactionNo, RegistrationNo, TemplateID, "", newImg);
                        isNewMenuCreated = true;
                    }
                    else
                        sb.AppendLine("&nbsp;");
                else
                    sb.AppendFormat("<a style=\"cursor:pointer;\" onclick=\"entryQuestionRespond('{0}', '{1}', '{2}', '{3}', '{4}')\">{5}</a>", "view", value.TransactionNo, RegistrationNo, TemplateID, "", time);

                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            // Score
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in vsItemValues)
            {
                if (!string.IsNullOrWhiteSpace(value.TransactionNo))
                    sb.AppendFormat("<td style='background-color:{0};'>{1:0}", VitalSign.EwsLevelColor(value.Level), value.TotalScore);
                else
                    sb.Append("<td>&nbsp;");

                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");

            // Yellow Score
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in vsItemValues)
            {
                if (!string.IsNullOrWhiteSpace(value.TransactionNo))
                    sb.AppendFormat("<td>{0}", value.Level2Count == 0 ? "-" : string.Format("{0:0}", value.Level2Count));
                else
                    sb.Append("<td>&nbsp;");

                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");

            // Red Score
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in vsItemValues)
            {
                if (!string.IsNullOrWhiteSpace(value.TransactionNo))
                    sb.AppendFormat("<td>{0}", value.Level3Count == 0 ? "-" : string.Format("{0:0}", value.Level3Count));
                else
                    sb.Append("<td>&nbsp;");

                sb.AppendLine("</td>");
            }
            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }


    }

}
