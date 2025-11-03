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
using Telerik.Web.UI.HtmlChart;

namespace Temiang.Avicenna.Module.RADT.CardexMonitoring
{
    public partial class CardexMonitoringDashboard : Page
    {
        private int _gmt = AppParameter.GetParameterValue(AppParameter.ParameterItem.GMT).ToInt();
        private readonly int _columnCount = 24;
        private static readonly Color[] _colors =
    {
        Color.DarkRed,
        Color.DarkBlue,
        Color.DarkGreen,
        Color.DarkGray,
        Color.Purple,
        Color.Orange,
        Color.Violet,
        Color.NavajoWhite,
        Color.MediumSeaGreen,
        Color.IndianRed
    };

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected string PatientID
        {
            get { return Request.QueryString["patid"]; }
        }
        private string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        private string FromRegistrationNo
        {
            get { return Request.QueryString["fregno"]; }
        }
        private DateTime LastVitalSignDate
        {
            get
            {
                if (ViewState["lvsd"] == null)
                    ViewState["lvsd"] = VitalSign.LastVitalSignDate(RegistrationNo, FromRegistrationNo);

                return Convert.ToDateTime(ViewState["lvsd"]);
            }
        }
        private string QuestionFormID
        {
            get
            {
                return (string)ViewState["qfid"];
            }
            set
            {
                ViewState["qfid"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // Reset LastAccessSecureProgramID supaya entrian PHR selalu masuk ke isian passcode dulu
            AppSession.UserLogin.LastAccessSecureProgramID = String.Empty;


            var pat = new Patient();
            if (pat.LoadByPrimaryKey(PatientID))
            {
                switch (Request.QueryString["mc"])
                {
                    case "mc3":
                        this.Title = "Cardex Monitoring C3 : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                        QuestionFormID = "CM.C3";
                        break;
                    case "mc3n":
                        this.Title = "Cardex Monitoring C3 Neonatus : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                        QuestionFormID = "CM.C3N";
                        break;
                    case "mcctcu":
                        this.Title = "Cardex Monitoring CTCU : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                        QuestionFormID = "CM.CTCU";
                        break;
                    default:
                        break;
                }

            }

            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = DateTime.Today;

                lblPatientName.Text = pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                lblSex.Text = pat.Sex;
                if (pat.DateOfBirth != null)
                {
                    var birthDate = pat.DateOfBirth.Value;
                    lblBirthDate.Text = birthDate.ToString(AppConstant.DisplayFormat.Date);

                    lblAge.Text = string.Format("{0}Y, {1}M, {2}D",
                        Helper.GetAgeInYear(birthDate, DateTime.Today), Helper.GetAgeInMonth(birthDate, DateTime.Today),
                        Helper.GetAgeInDay(birthDate, DateTime.Today));
                }

            }

            PopulateCardexMonitoring();
        }

        protected void txtFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            PopulateCardexMonitoring();
        }
        protected void btnPrevDate_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = txtFromDate.SelectedDate.Value.AddDays(-1);
            PopulateCardexMonitoring();
        }

        protected void btnNextDate_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = txtFromDate.SelectedDate.Value.AddDays(1);
            PopulateCardexMonitoring();
        }
        private void PopulateCardexMonitoring()
        {
            PopulateChart();
            litardexMonitoring.Text = CardexMonitoringHtml();
        }

        #region VitalSIgn Chart

        private void PopulateChart()
        {
            DateTime date = txtFromDate.SelectedDate.Value.Date;

            //Clear
            chtVitalSign.PlotArea.Series.Clear();
            chtVitalSign.PlotArea.XAxis.AxisCrossingPoints.Clear();
            chtVitalSign.PlotArea.AdditionalYAxes.Clear();

            // Load Template
            var qr = new QuestionInGroupQuery("a");
            var quest = new QuestionQuery("q");
            qr.InnerJoin(quest).On(qr.QuestionID == quest.QuestionID);

            var vs = new VitalSignQuery("v");
            qr.InnerJoin(vs).On(quest.VitalSignID == vs.VitalSignID);

            var qgif = new QuestionGroupInFormQuery("qgif");
            qr.InnerJoin(qgif).On(qr.QuestionGroupID == qgif.QuestionGroupID);

            qr.Where(qgif.QuestionFormID == QuestionFormID, vs.ValueType == "NUM");
            qr.Select(quest.QuestionText, quest.VitalSignID, vs.NumDecimalDigits);
            var dtbTemplate = qr.LoadDataTable();

            chtVitalSign.PlotArea.XAxis.MaxValue = _columnCount;

            var i = 0;
            foreach (DataRow rowTemplate in dtbTemplate.Rows)
            {
                decimal realMinYValue = 0;
                decimal realMaxYValue = 0;
                var scatterLineSeries = PopulateScatterLineSeries(RegistrationNo, QuestionFormID, rowTemplate["VitalSignID"].ToString(), date,
                rowTemplate["QuestionText"].ToString(), rowTemplate["NumDecimalDigits"].ToInt(), out realMinYValue, out realMaxYValue);

                scatterLineSeries.Appearance.FillStyle.BackgroundColor = _colors[i];

                chtVitalSign.PlotArea.Series.Add(scatterLineSeries);

                chtVitalSign.PlotArea.XAxis.AxisCrossingPoints.Add(new AxisCrossingPoint() { Value = 0 });

                if (i == 0)
                {
                    chtVitalSign.PlotArea.YAxis.Name = rowTemplate["VitalSignID"].ToString();
                    chtVitalSign.PlotArea.YAxis.Color = _colors[i];
                }
                else
                {
                    var axisY = new AxisY();
                    axisY.Name = rowTemplate["VitalSignID"].ToString();
                    axisY.Color = _colors[i];
                    chtVitalSign.PlotArea.AdditionalYAxes.Add(axisY);
                }
                //chtVitalSign.PlotArea.YAxis.Color = chtVitalSign.PlotArea.Series[i].Appearance.FillStyle.BackgroundColor;
                i++;
            }

        }

        private ScatterLineSeries PopulateScatterLineSeries(string registrationNo, string questionFormID, string vitalSignID, DateTime date, string vitalSignName, int numDecimalDigits, out decimal realMinYValue, out decimal realMaxYValue)
        {
            var dtb = VitalSignData(registrationNo, questionFormID, vitalSignID, date, numDecimalDigits);

            var series = new ScatterLineSeries { Name = vitalSignName, AxisName = vitalSignID };

            series.LabelsAppearance.DataFormatString = "{1}";
            series.LabelsAppearance.Visible = true;
            series.TooltipsAppearance.Visible = false;

            series.TooltipsAppearance.Color = System.Drawing.Color.White;
            series.TooltipsAppearance.ClientTemplate = " #=kendo.format(\\'{0}\\',value.y)# on #= kendo.format(\\'{0:d-MMM-yyyy HH:mm}\\', new Date(value.x)) #";

            decimal minValue = 0;
            decimal maxValue = 0;
            foreach (DataRow row in dtb.Rows)
            {
                if (!(row["VitalSignValue"] is DBNull))
                {
                    var vitalSignValue = (decimal)row["VitalSignValue"];
                    if (minValue == 0 || minValue > vitalSignValue)
                        minValue = vitalSignValue;

                    if (maxValue < vitalSignValue)
                        maxValue = vitalSignValue;
                }

                if (!(row["VitalSignValue"] is DBNull))
                {
                    var seriesItem =
                        new ScatterSeriesItem(row["No"].ToInt(), row["VitalSignValue"].ToDecimal());
                    series.SeriesItems.Add(seriesItem);


                    //// XAxis berdasarkan tanggal
                    //var vitalSignDateTime = (DateTime)row["VitalSignDateTime"];
                    //var seriesItem =
                    //    new ScatterSeriesItem(ConvertToJavaScriptDateTime(vitalSignDateTime), row["VitalSignValue"].ToDecimal());
                    //series.SeriesItems.Add(seriesItem);

                }
                else
                {
                    var seriesItem =
                        new ScatterSeriesItem(row["No"].ToInt(), null);
                    series.SeriesItems.Add(seriesItem);
                }


            }
            realMinYValue = minValue - 5;
            realMaxYValue = maxValue + 5;
            return series;
        }
        private decimal ConvertToJavaScriptDateTime(DateTime fromDate)
        {
            return (decimal)fromDate.Subtract(new DateTime(1970, 1, 1, _gmt, 0, 0)).TotalMilliseconds;
        }

        private DataTable VitalSignData(string registrationNo, string questionFormID, string vitalSignID, DateTime date, int numDecimalDigits)
        {
            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);

            var quest = new QuestionQuery("q");
            phr.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            phr.Where(phr.RegistrationNo == registrationNo, phr.QuestionFormID == questionFormID, quest.VitalSignID == vitalSignID);

            phr.Where(phr.RecordDate >= date, phr.RecordDate < date.AddDays(1));

            // RecordDate tidak seragam ...ada yg include time ada yg tidak
            phr.Select(phr.TransactionNo,
                phr.RecordDate.Date().As("RecordDate"),
                phr.RecordTime,
                phrl.QuestionAnswerPrefix, phrl.QuestionAnswerSuffix
                , phrl.QuestionAnswerNum.As("VitalSignValue"), quest.SRAnswerType
                , quest.QuestionAnswerSelectionID, phrl.QuestionAnswerText, phrl.QuestionAnswerSelectionLineID);

            phr.OrderBy(phr.RecordDate.Date().Ascending, phr.RecordTime.Ascending);

            var dtb = phr.LoadDataTable();
            dtb.Columns.Add("No", typeof(System.Int16));
            dtb.Columns.Add("Description", typeof(System.String));
            dtb.Columns.Add("VitalSignDateTime", typeof(System.DateTime));

            if (_columnCount > 0)
            {
                // Untuk EWS buat kolom sebanyak 26 kolom sesuai dgn form EWS
                var rowCount = dtb.Rows.Count;
                if (dtb.Rows.Count < _columnCount)
                {
                    for (int i = rowCount; i < _columnCount; i++)
                    {
                        var newRow = dtb.NewRow();
                        dtb.Rows.Add(newRow);
                    }
                }
            }

            var rowNo = 0;
            foreach (System.Data.DataRow row in dtb.Rows)
            {
                rowNo++;
                row["No"] = rowNo;

                if (row["TransactionNo"] != DBNull.Value)
                {
                    var recDate = Convert.ToDateTime(row["RecordDate"]);
                    var recTime = Convert.ToString(row["RecordTime"]);

                    if (string.IsNullOrEmpty(recTime))
                        recTime = "00:00";

                    var times = recTime.Split(':');
                    row["VitalSignDateTime"] = new DateTime(date.Year, date.Month, date.Day, times[0].ToInt(),
                        times[1].ToInt(), 0);

                    // Cek SRAnswerType jika bukan NUM maka cari nilainya di Score
                    if (!"NUM".Equals(row["SRAnswerType"]))
                    {
                        row["VitalSignValue"] = 0;
                        if (row["QuestionAnswerSelectionLineID"] != DBNull.Value)
                        {
                            var answerLine = new QuestionAnswerSelectionLine();
                            if (answerLine.LoadByPrimaryKey(row["QuestionAnswerSelectionID"].ToString(),
                                row["QuestionAnswerSelectionLineID"].ToString()))
                                row["VitalSignValue"] = answerLine.Score ?? 0;
                        }

                        row["Description"] = string.Format("{0}", row["QuestionAnswerText"]);
                    }
                    else
                        row["Description"] = string.Format("{0} {1}",
                            Convert.ToDecimal(row["VitalSignValue"]).ToString("F"), row["QuestionAnswerSuffix"]);
                }

            }

            return dtb;
        }
        #endregion

        private DataTable CardexMonitoringData(string registrationNo, string questionFormID, string questionID, DateTime date)
        {
            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);

            var quest = new QuestionQuery("q");
            phr.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            phr.Where(phr.RegistrationNo == registrationNo, phr.QuestionFormID == questionFormID, phrl.QuestionID == questionID);

            phr.Where(phr.RecordDate >= date, phr.RecordDate < date.AddDays(1));

            // RecordDate tidak seragam ...ada yg include time ada yg tidak
            phr.Select(phr.TransactionNo,
                phr.RecordDate.Date().As("RecordDate"),
                phr.RecordTime,
                phrl.QuestionAnswerPrefix, phrl.QuestionAnswerSuffix
                , phrl.QuestionAnswerNum.As("Value"), quest.SRAnswerType
                , quest.QuestionAnswerSelectionID, phrl.QuestionAnswerText, phrl.QuestionAnswerSelectionLineID);

            phr.OrderBy(phr.RecordDate.Date().Ascending, phr.RecordTime.Ascending);

            var dtb = phr.LoadDataTable();
            dtb.Columns.Add("No", typeof(System.Int16));
            dtb.Columns.Add("Description", typeof(System.String));

            if (_columnCount > 0)
            {
                // Untuk EWS buat kolom sebanyak 26 kolom sesuai dgn form EWS
                var rowCount = dtb.Rows.Count;
                if (dtb.Rows.Count < _columnCount)
                {
                    for (int i = rowCount; i < _columnCount; i++)
                    {
                        var newRow = dtb.NewRow();
                        dtb.Rows.Add(newRow);
                    }
                }
            }

            var rowNo = 0;
            foreach (System.Data.DataRow row in dtb.Rows)
            {
                rowNo++;
                row["No"] = rowNo;

                if (row["TransactionNo"] != DBNull.Value)
                {

                    // Cek SRAnswerType jika bukan NUM maka cari nilainya di Score
                    if (!"NUM".Equals(row["SRAnswerType"]))
                    {
                        row["Value"] = 0;
                        if (row["QuestionAnswerSelectionLineID"] != DBNull.Value)
                        {
                            var answerLine = new QuestionAnswerSelectionLine();
                            if (answerLine.LoadByPrimaryKey(row["QuestionAnswerSelectionID"].ToString(),
                                row["QuestionAnswerSelectionLineID"].ToString()))
                            {
                                row["Value"] = answerLine.Score ?? 0;
                            }
                        }

                        row["Description"] = string.Format("{0}", row["QuestionAnswerText"]);
                    }
                    else
                        row["Description"] = string.Format("{0} {1}",
                            Convert.ToDecimal(row["Value"]).ToString("F"), row["QuestionAnswerSuffix"]);
                }

            }

            return dtb;
        }

        private string CardexMonitoringHtml()
        {
            // Load Template
            var qr = new QuestionInGroupQuery("a");
            var quest = new QuestionQuery("q");
            qr.InnerJoin(quest).On(qr.QuestionID == quest.QuestionID);

            //var vs = new VitalSignQuery("v");
            //qr.LeftJoin(vs).On(quest.VitalSignID == vs.VitalSignID);

            var qgif = new QuestionGroupInFormQuery("qgif");
            qr.InnerJoin(qgif).On(qr.QuestionGroupID == qgif.QuestionGroupID);

            //qr.Where(qgif.QuestionFormID == QuestionFormID, qr.Or(vs.ValueType != "NUM", quest.VitalSignID == string.Empty, quest.VitalSignID.IsNull()));

            qr.Where(qgif.QuestionFormID == QuestionFormID);

            qr.Select(quest.QuestionText, quest.QuestionID);
            qr.OrderBy(qr.RowIndex.Ascending);

            var dtbTemplate = qr.LoadDataTable();

            var sb = new StringBuilder();
            //sb.AppendLine("<table width=\"100%\" class='tblgraph'>");
            //sb.AppendLine("             <tr><th style = \"width: 150px\"> &nbsp;Time&nbsp;</th>");

            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            //sb.AppendLine("     <td style = \"width: 225px;\">");
            sb.AppendLine("     <td style = \"width: 260px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><th style = \"width: 80px\"> &nbsp; Time&nbsp;</th></tr>");
            // Label Caption
            foreach (DataRow rowTemplate in dtbTemplate.Rows)
            {
                // Label
                sb.AppendFormat("<tr><td>{0}</td></tr>", rowTemplate["QuestionText"]);
            }
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            var i = 0;
            var date = txtFromDate.SelectedDate.Value.Date;

            // Data history
            foreach (DataRow rowTemplate in dtbTemplate.Rows)
            {
                var dtb = CardexMonitoringData(RegistrationNo, QuestionFormID, rowTemplate["QuestionID"].ToString(), date);
                // Time & Menu
                if (i == 0)
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        var time = row["RecordTime"].ToString();
                        var txNo = row["TransactionNo"].ToString();
                        //entryPhr(md, id, regno, fid, unit)
                        sb.AppendFormat("<th><a style=\"cursor:pointer;\" onclick=\"entryPhr('{0}', '{1}', '{2}', '{3}', '{4}')\"><img src=\"../../../../../Images/Toolbar/edit16.png\"/>&nbsp;{5}</a></th>", string.IsNullOrWhiteSpace(time) ? "new" : "edit", txNo, RegistrationNo, QuestionFormID, "", string.IsNullOrWhiteSpace(time) || time == "00:00" ? "&nbsp;" : time);
                    }
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("<tr>");

                // Value
                foreach (DataRow row in dtb.Rows)
                {
                    decimal decVal = 0;
                    if (decimal.TryParse(row["Value"].ToString(), out decVal))
                        sb.AppendFormat("<td>{0}</td>", Helper.RemoveZeroDigits(decVal));
                    else
                        sb.AppendFormat("<td>{0}</td>", row["Value"]);
                }
                sb.AppendLine("</tr>");

                i++;
            }

            sb.AppendLine("</table>");

            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }
    }

}
