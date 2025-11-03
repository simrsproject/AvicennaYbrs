using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.HtmlChart;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.Module.Reports.OptionControl;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{

    public partial class VitalSignChartCtl : System.Web.UI.UserControl
    {
        protected string ChartTitle
        {
            get
            {
                return Convert.ToString(ViewState["ct"]);
            }
            set
            {
                ViewState["ct"] = value;
            }
        }
        private DateTime _dateOfBirth;
        private bool _isShowValue = false;
        private List<VitalSignItemValue> _vitalSignItemValues = new List<VitalSignItemValue>();
        private bool _isForEws;
        private bool _isExcludeFromScoreEws;
        private bool _isExistEwsLevel = false;
        private const int EwsColumnCount = 26;
        private int _gmt = AppParameter.GetParameterValue(AppParameter.ParameterItem.GMT).ToInt();
        private VitalSignEwsLevelCollection _vitalSignEwsLevels = new VitalSignEwsLevelCollection();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ClearChart()
        {
            chtVitalSign.PlotArea.Series.Clear();
        }
        #region Populate Chart Searchby VitalSIgnID
        /// <summary>
        /// Untuk chart non Ews dan Ews versi lama yg seberannya bisa salah dalam total scorenya
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="vitalSignID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="isForEws"></param>
        /// <param name="isTitleShow"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public List<VitalSignItemValue> PopulateChart(string patientID, DateTime dateOfBirth, string vitalSignID, DateTime fromDate, DateTime toDate, bool isForEws = false,bool isTitleShow=true, int columnCount=0)
        {
            var ewsType="OEWS"; //Other EWS
            _isForEws = isForEws;
            _dateOfBirth = dateOfBirth;

            chtVitalSign.PlotArea.Series.Clear();

            var vitalSign = new VitalSign();
            if (!vitalSign.LoadByPrimaryKey(vitalSignID))
                return new List<VitalSignItemValue>(); ;

            // Untuk EWS bisa masing2 grafiknya spy scorenya dihitung (Handono 230930 -> RS GPI)
            var parentVitalSignID = _isForEws ? String.Empty : vitalSign.ParentVitalSignID;
            _isShowValue = vitalSign.ValueType == "NUM";

            decimal minValue = vitalSign.NumMinValue ?? 0;
            decimal maxValue = vitalSign.NumMaxValue ?? 0;

            //Clear
            chtVitalSign.PlotArea.Series.Clear();

            //chtVitalSign.PlotArea.YAxis.TitleAppearance.Visible = isTitleShow;
            chtVitalSign.PlotArea.YAxis.TitleAppearance.Visible = true;
            chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = ".";

            ChartTitle = string.Empty;

            if (isTitleShow)
            {
                if (!string.IsNullOrEmpty(vitalSign.VitalSignUnit))
                    //chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0} ({1})", vitalSign.VitalSignName, vitalSign.VitalSignUnit);
                    ChartTitle = string.Format("{0} ({1})", vitalSign.VitalSignName, vitalSign.VitalSignUnit);
                else
                    //chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0}", vitalSign.VitalSignName);
                    ChartTitle = string.Format("{0}", vitalSign.VitalSignName);
            }

            if (_isForEws || columnCount > 0)
            {
                chtVitalSign.PlotArea.XAxis.Type = AxisType.Auto;
                chtVitalSign.PlotArea.XAxis.LabelsAppearance.Visible = false;
                chtVitalSign.PlotArea.XAxis.LabelsAppearance.DataFormatString = "{0}";
                chtVitalSign.PlotArea.XAxis.MaxValue = 26;
                chtVitalSign.PlotArea.XAxis.MinValue = 0;
            }
            else
            {
                chtVitalSign.PlotArea.XAxis.Type = AxisType.Date;
                if (fromDate != toDate)
                {
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.DataFormatString = "{0:dd-MMM HH:MM}";
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.RotationAngle = -45;
                }
                else
                {
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.DataFormatString = "{0:HH:MM}";
                    chtVitalSign.PlotArea.XAxis.BaseUnit = DateTimeBaseUnit.Hours;
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.RotationAngle = -45;
                }
            }

            // Min Max YAxis
            int ageInDay = (fromDate - dateOfBirth).Days;
            var qr = new VitalSignEwsQuery();
            qr.Where(qr.SREwsType == ewsType);
            qr.Where(qr.VitalSignID == (!string.IsNullOrEmpty(parentVitalSignID) ? parentVitalSignID : vitalSign.VitalSignID), qr.StartAgeInDay <= ageInDay, qr.EndAgeInDay >= ageInDay);
            qr.es.Top = 1;
            qr.OrderBy(qr.StartAgeInDay.Descending);

            var vsEws = new VitalSignEws();
            if (vsEws.Load(qr))
            {
                minValue = vsEws.ChartMinValue ?? 0;
                maxValue = vsEws.ChartMaxValue ?? 0;

                _isExcludeFromScoreEws = vsEws.IsExcludeFromScoreEws ?? false;
            }
            else
            {
                // Default value
                vsEws = new VitalSignEws();
                vsEws.StartAgeInDay = 0;
                vsEws.ChartMaxValue = 0;
                vsEws.ChartMinValue = 0;
            }

            decimal realMinYValue = 0;
            decimal realMaxYValue = 0;
            var vitalSignIds = new List<string>();
            if (!string.IsNullOrEmpty(parentVitalSignID))
            {
                if (isTitleShow)
                {
                    // Override Title with parent VitalSign
                    var vtp = new VitalSign();
                    vtp.LoadByPrimaryKey(parentVitalSignID);
                    if (!string.IsNullOrEmpty(vtp.VitalSignUnit))
                        chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0} ({1})", vtp.VitalSignName, vtp.VitalSignUnit);
                    else
                        chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0}", vtp.VitalSignName);
                }

                // Add Series
                vitalSign = new VitalSign();
                vitalSign.LoadByPrimaryKey(parentVitalSignID);

                var query = new VitalSignQuery("v");
                query.Where(query.ParentVitalSignID == parentVitalSignID);
                query.Select(query.VitalSignID, query.VitalSignName, query.NumDecimalDigits);
                query.OrderBy(query.RowIndexInGroup.Ascending);
                var dtb = query.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    chtVitalSign.PlotArea.Series.Add(PopulateScatterLineSeries(patientID, "OEWS", row["VitalSignID"].ToString(), fromDate, toDate,
                        row["VitalSignName"].ToString(), row["NumDecimalDigits"].ToString().ToInt(), out realMinYValue,
                        out realMaxYValue, columnCount));

                    if (!_isForEws)
                    {
                        if (minValue > 0 && realMinYValue < 0) // Cegah minus
                            realMinYValue = 0;

                        if (minValue == 0 || minValue > realMinYValue)
                            minValue = realMinYValue;

                        if (maxValue < realMaxYValue)
                            maxValue = realMaxYValue;
                    }

                    vitalSignIds.Add(row["VitalSignID"].ToString());
                }

                chtVitalSign.Legend.Appearance.Visible = !_isForEws; //Biar lebar sama dan sejajar dgn score table
            }
            else
            {
                // Single Chart
                chtVitalSign.PlotArea.Series.Add(PopulateScatterLineSeries(patientID, ewsType, vitalSign.VitalSignID, fromDate, toDate,
                    vitalSign.VitalSignName, vitalSign.NumDecimalDigits.ToInt(), out realMinYValue, out realMaxYValue, columnCount));

                if (!_isForEws)
                {
                    if (minValue > 0 && realMinYValue < 0) // Cegah minus
                        realMinYValue = 0;

                    if (minValue > realMinYValue)
                        minValue = realMinYValue;

                    if (maxValue < realMaxYValue)
                        maxValue = realMaxYValue;
                }

                chtVitalSign.Legend.Appearance.Visible = false;
            }

            //// Markup Min Max jika seting min max tidak diset
            //if (ews.ChartMinValue == 0)
            //{
            //    if (minValue > 0)
            //    {
            //        minValue = minValue - (maxValue / 10);
            //        if (minValue < 0)
            //            minValue = 0;
            //    }
            //    else
            //    {
            //        minValue = minValue - (maxValue / 10);
            //    }
            //}

            //if (ews.ChartMaxValue == 0)
            //{
            //    maxValue = maxValue + (maxValue / 10);
            //}

            chtVitalSign.PlotArea.YAxis.MinValue = minValue;
            chtVitalSign.PlotArea.YAxis.MaxValue = maxValue;

            if (vitalSign.ChartYAxisStep != null && vitalSign.ChartYAxisStep > 0) // Add label step (Handono 2303)
                chtVitalSign.PlotArea.YAxis.Step = vitalSign.ChartYAxisStep;

            if (vsEws.ChartYAxisStep != null && vsEws.ChartYAxisStep > 0) // Override Add label step (Handono 2023 nov 27)
                chtVitalSign.PlotArea.YAxis.Step = vsEws.ChartYAxisStep;

            // Add Plot Band color
            var qrLevel = new VitalSignEwsLevelQuery();
            if (string.IsNullOrEmpty(ewsType))
                qrLevel.Where(qrLevel.SREwsType == "EWS");
            else
                qrLevel.Where(qrLevel.SREwsType == ewsType);

            qrLevel.Where(qrLevel.StartAgeInDay == vsEws.StartAgeInDay);
            if (string.IsNullOrEmpty(parentVitalSignID))
            {
                qrLevel.Where(qrLevel.VitalSignID == vitalSign.VitalSignID);
            }
            else
            {
                qrLevel.Where(qrLevel.VitalSignID.In(vitalSignIds));
            }
            qrLevel.OrderBy(qrLevel.StartValue.Ascending);

            var ewsLevels = new VitalSignEwsLevelCollection();
            _isExistEwsLevel = ewsLevels.Load(qrLevel);
            if (_isExistEwsLevel)
            {
                decimal from = -1000;
                Color color = Color.Empty;
                foreach (var ewsLevel in ewsLevels)
                {
                    if (from == -1000)
                    {
                        from = ewsLevel.StartValue.ToDecimal();
                        color = Color.FromName(VitalSign.EwsLevelColor((ewsLevel.EwsLevel ?? 0)));
                    }
                    else
                    {
                        var to = ewsLevel.StartValue.ToDecimal();
                        chtVitalSign.PlotArea.YAxis.PlotBands.Add(new PlotBand(from, to, color, 100));

                        //Reset
                        from = ewsLevel.StartValue.ToDecimal();
                        color = Color.FromName(VitalSign.EwsLevelColor((ewsLevel.EwsLevel ?? 0)));

                    }
                }

                // Last PlotBand
                if (from > -1000 && vsEws.ChartMaxValue > 0)
                {
                    chtVitalSign.PlotArea.YAxis.PlotBands.Add(new PlotBand(from, vsEws.ChartMaxValue.ToDecimal(), color,
                        100));

                }
            }

            // Set Title
            // chtVitalSign.ChartTitle.Text = vitalSign.VitalSignName;
            if (_isForEws)
                litEwsValueAndScore.Text = EwsScoreHtml();
            return _vitalSignItemValues;
        }
        private ScatterLineSeries PopulateScatterLineSeries(string patientID, string ewsType, string vitalSignID, DateTime fromDate, DateTime toDate, string vitalSignName, int numDecimalDigits, out decimal realMinYValue, out decimal realMaxYValue, int columnCount = 0)
        {
            var isShowLabel = false;
            var dtb = HistoryVitalSign(patientID, vitalSignID, fromDate, toDate, numDecimalDigits, columnCount);

            // Populate EwsLevel
            if (_isForEws && !_isExcludeFromScoreEws)
            {
                foreach (VitalSignItemValue value in _vitalSignItemValues)
                {
                    if (value.Value > 0)
                    {
                        value.Level = EwsLevelValue(ewsType, value.VitalSignID, _dateOfBirth, value.Time, value.Value);
                    }

                }
            }

            var series = new ScatterLineSeries { Name = vitalSignName };

            series.LabelsAppearance.DataFormatString = "{1}";
            series.LabelsAppearance.Visible = _isShowValue;
            series.TooltipsAppearance.Visible = _isShowValue;
            if (_isForEws)
            {
                series.TooltipsAppearance.Visible = false;
            }

            if (_isShowValue && !_isForEws)
            {
                series.TooltipsAppearance.Color = System.Drawing.Color.White;
                series.TooltipsAppearance.ClientTemplate = " #=kendo.format(\\'{0}\\',value.y)# on #= kendo.format(\\'{0:d-MMM-yyyy HH:mm}\\', new Date(value.x)) #";
            }

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

                if (_isForEws || columnCount > 0)
                {
                    // Untuk EWS atau ada tambahan kolom kosong, XAxis berdasarkan No
                    if (!(row["VitalSignValue"] is DBNull))
                    {
                        var seriesItem =
                            new ScatterSeriesItem(row["No"].ToInt(), row["VitalSignValue"].ToDecimal());
                        series.SeriesItems.Add(seriesItem);
                    }
                    else
                    {
                        var seriesItem =
                            new ScatterSeriesItem(row["No"].ToInt(), null);
                        series.SeriesItems.Add(seriesItem);
                    }
                }
                else
                {
                    if (!(row["VitalSignValue"] is DBNull))
                    {
                        // XAxis berdasarkan tanggal
                        var vitalSignDateTime = (DateTime)row["VitalSignDateTime"];
                        var seriesItem =
                            new ScatterSeriesItem(ConvertToJavaScriptDateTime(vitalSignDateTime), row["VitalSignValue"].ToDecimal());
                        series.SeriesItems.Add(seriesItem);
                    }
                }

            }
            realMinYValue = minValue - 5;
            realMaxYValue = maxValue + 5;
            return series;
        }
        private DataTable HistoryVitalSign(string patientID, string vitalSignID, DateTime fromDate, DateTime toDate, int numDecimalDigits, int columnCount = 0)
        {
            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);

            var reg = new RegistrationQuery("r");
            phr.InnerJoin(reg).On(phr.RegistrationNo == reg.RegistrationNo);

            var quest = new QuestionQuery("q");
            phr.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            phr.Where(reg.PatientID == patientID, quest.VitalSignID == vitalSignID);

            if (fromDate > toDate)
            {
                var tmpDate = fromDate;
                fromDate = toDate;
                toDate = tmpDate;
            }

            if (DateTime.Now > toDate.AddSeconds(10))
                phr.Where(phr.RecordDate >= fromDate, phr.RecordDate < toDate.AddDays(1));
            else
                phr.Where(phr.RecordDate >= fromDate);

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

            if (_isForEws || columnCount > 0)
            {
                // Untuk EWS buat kolom sebanyak 26 kolom sesuai dgn form EWS
                var rowCount = dtb.Rows.Count;
                if (columnCount == 0)
                    columnCount = EwsColumnCount;

                if (dtb.Rows.Count < columnCount)
                {
                    for (int i = rowCount; i < columnCount; i++)
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
                    var date = Convert.ToDateTime(row["RecordDate"]);
                    var time = Convert.ToString(row["RecordTime"]);

                    if (string.IsNullOrEmpty(time))
                        time = "00:00";

                    var times = time.Split(':');
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

                // Insert ke _vitalSignItemValues
                var isFound = false;
                foreach (var value in _vitalSignItemValues)
                {
                    if ((_isForEws && value.No.Equals(row["No"].ToInt()) && !string.IsNullOrEmpty(value.VitalSignID))
                        || (!_isForEws && value.TransactionNo == Convert.ToString(row["TransactionNo"]) && !string.IsNullOrEmpty(value.VitalSignID)))
                    {
                        isFound = true;
                        value.Value2 = Convert.ToDecimal(row["VitalSignValue"]);
                        value.VitalSignID2 = vitalSignID;
                        value.ValueInString2 = row["QuestionAnswerText"].ToString();
                        break;
                    }
                }

                if (!isFound && _vitalSignItemValues.Count <= columnCount)
                {
                    if (row["TransactionNo"] != DBNull.Value)
                        _vitalSignItemValues.Add(new VitalSignItemValue()
                        {
                            No = row["No"].ToInt(),
                            TransactionNo = row["TransactionNo"].ToString(),
                            Time = Convert.ToDateTime(row["VitalSignDateTime"]),
                            VitalSignID = vitalSignID,
                            Value = Convert.ToDecimal(row["VitalSignValue"]),
                            ValueInString = row["QuestionAnswerText"].ToString()
                        });
                    else
                        _vitalSignItemValues.Add(new VitalSignItemValue()
                        {
                            No = row["No"].ToInt()
                        });
                }
            }

            return dtb;
        }
        #endregion Populate Chart Searchby VitalSIgnID

        #region Populate Chart Search by QuestionID (from Nursing Template)
        public List<VitalSignItemValue> PopulateChartTableByQuestion(string patientID, DateTime dateOfBirth, string registrationNo, string fromRegistrationNo, string questionAnswerSelectionID, string questionFormID, string questionID, string answerType, string answerSuffix, DateTime fromDate, DateTime toDate, string chartTitle, string ewsType = "", int columnCount = 0)
        {
            ChartTitle = chartTitle;
            chtVitalSign.Visible = false;
            litVitalSign.Visible = true;
            if (string.IsNullOrWhiteSpace(questionAnswerSelectionID))
            {
                litVitalSign.Text = string.Format("Define first for {0} range of age {1} days, please contact IT support", ewsType, (fromDate - dateOfBirth).TotalDays);
                return new List<VitalSignItemValue>();
            }

            _isForEws = !string.IsNullOrWhiteSpace(ewsType);
            _dateOfBirth = dateOfBirth;


            var vitalSignID = questionID; // Monitoring non VitalSign type

            // Populate _vitalSignItemValues
            var dtb = HistoryVitalSignByQuestion(patientID, registrationNo, fromRegistrationNo, vitalSignID, questionFormID, questionID, answerType, answerSuffix, questionAnswerSelectionID, fromDate, toDate, 0, columnCount);

            // Untuk Question dgn isian dari QuestionAnswerSelectionLine maka Level = Score dan Score sudah terset pada Value
            foreach (VitalSignItemValue value in _vitalSignItemValues)
            {
                value.Level = value.Value.ToInt();
            }

            // Chart Table
            var answerSelColl = new QuestionAnswerSelectionLineCollection();
            answerSelColl.Query.Where(answerSelColl.Query.QuestionAnswerSelectionID == questionAnswerSelectionID);
            answerSelColl.Query.OrderBy(answerSelColl.Query.QuestionAnswerSelectionLineID.Ascending);
            answerSelColl.LoadAll();

            foreach (var answerSel in answerSelColl)
            {
                var height = Math.Ceiling(answerSel.QuestionAnswerSelectionLineText.Length.ToDecimal() / 20);
                if (height == 0)
                    height = 22;
                else
                    height = ((height - 1) * 14) + 22; // 22 <- tinggi minimal
                answerSel.LastUpdateByUserID = string.Format("{0}px", height); // Pakai untuk menampung perkiraan tinggi row
            }
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 120px;\">");

            // Y Label
            sb.AppendLine("         <table class='tblgraph' width=\"100%\">");
            sb.AppendLine("             <tr><th> &nbsp; Time&nbsp;</th></tr>");

            foreach (var answerSel in answerSelColl)
            {

                sb.AppendFormat("             <tr style=\"height:{0}\"><td>{1}</td></tr>", answerSel.LastUpdateByUserID, answerSel.QuestionAnswerSelectionLineText);
            }

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Caption
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in _vitalSignItemValues)
            {
                var time = value.Time.ToString("HH:mm");
                sb.AppendLine("<th>");
                sb.AppendLine(string.IsNullOrEmpty(value.TransactionNo) ? "&nbsp;" : time);
                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");


            // Value Row
            var checkImg = string.Format("<img src='{0}/Resources/Checked16.png'/>", Helper.UrlRoot());
            foreach (var answerSel in answerSelColl)
            {
                var colorLevel = VitalSign.EwsLevelColor(answerSel.Score.ToInt());
                sb.AppendFormat("<tr style=\"height:{0}\">", answerSel.LastUpdateByUserID);
                foreach (VitalSignItemValue value in _vitalSignItemValues)
                {
                    sb.AppendFormat("<td style='background-color:{0};'>", colorLevel);
                    if (!string.IsNullOrEmpty(value.TransactionNo) && value.ValueInSelectionLineID == answerSel.QuestionAnswerSelectionLineID)
                        sb.Append(checkImg);
                    else
                        sb.Append("&nbsp;");

                    sb.AppendLine("</td>");

                }
                sb.AppendLine("</tr>");
            }


            sb.AppendLine("</table>");

            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");

            litVitalSign.Text = sb.ToString();

            return _vitalSignItemValues;
        }

        public List<VitalSignItemValue> PopulateChartByQuestion(string patientID, DateTime dateOfBirth, string registrationNo, string fromRegistrationNo, string vitalSignID, string questionFormID, string questionID, string answerType, string answerSuffix, DateTime fromDate, DateTime toDate, string ewsType = "", bool isTitleShow = true, int columnCount = 0)
        {
            chtVitalSign.Visible = true;
            litVitalSign.Visible = false;

            _isForEws = !string.IsNullOrWhiteSpace(ewsType);
            _dateOfBirth = dateOfBirth;


            var vitalSign = new VitalSign();
            if (!vitalSign.LoadByPrimaryKey(vitalSignID))
                return new List<VitalSignItemValue>(); ;

            // Untuk EWS bisa masing2 grafiknya spy scorenya dihitung (Handono 230930 -> RS GPI)
            var parentVitalSignID = _isForEws ? String.Empty : vitalSign.ParentVitalSignID;
            _isShowValue = vitalSign.ValueType == "NUM";

            decimal minValue = vitalSign.NumMinValue ?? 0;
            decimal maxValue = vitalSign.NumMaxValue ?? 0;

            //Clear
            chtVitalSign.PlotArea.Series.Clear();

            //chtVitalSign.PlotArea.YAxis.TitleAppearance.Visible = isTitleShow;
            chtVitalSign.PlotArea.YAxis.TitleAppearance.Visible = true;
            chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = ".";
            ChartTitle = string.Empty;

            if (isTitleShow)
            {
                if (!string.IsNullOrEmpty(vitalSign.VitalSignUnit))
                    //chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0} ({1})", vitalSign.VitalSignName, vitalSign.VitalSignUnit);
                    ChartTitle = string.Format("{0} ({1})", vitalSign.VitalSignName, vitalSign.VitalSignUnit);
                else
                    //chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0}", vitalSign.VitalSignName);
                    ChartTitle = string.Format("{0}", vitalSign.VitalSignName);
            }

            if (_isForEws || columnCount > 0)
            {
                chtVitalSign.PlotArea.XAxis.Type = AxisType.Auto;
                chtVitalSign.PlotArea.XAxis.LabelsAppearance.Visible = false;
                chtVitalSign.PlotArea.XAxis.LabelsAppearance.DataFormatString = "{0}";
                chtVitalSign.PlotArea.XAxis.MaxValue = 26;
                chtVitalSign.PlotArea.XAxis.MinValue = 0;
            }
            else
            {
                chtVitalSign.PlotArea.XAxis.Type = AxisType.Date;
                if (fromDate != toDate)
                {
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.DataFormatString = "{0:dd-MMM HH:MM}";
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.RotationAngle = -45;
                }
                else
                {
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.DataFormatString = "{0:HH:MM}";
                    chtVitalSign.PlotArea.XAxis.BaseUnit = DateTimeBaseUnit.Hours;
                    chtVitalSign.PlotArea.XAxis.LabelsAppearance.RotationAngle = -45;
                }
            }

            // Min Max YAxis
            int ageInDay = (fromDate - dateOfBirth).Days;
            var qr = new VitalSignEwsQuery();
            if (string.IsNullOrEmpty(ewsType))
                qr.Where(qr.SREwsType == "EWS"); // Default value untuk info range normal value & max min
            else
                qr.Where(qr.SREwsType == ewsType);

            qr.Where(qr.VitalSignID == (!string.IsNullOrEmpty(parentVitalSignID) ? parentVitalSignID : vitalSign.VitalSignID), qr.StartAgeInDay <= ageInDay, qr.EndAgeInDay >= ageInDay);
            qr.es.Top = 1;
            qr.OrderBy(qr.StartAgeInDay.Descending);

            var vsEws = new VitalSignEws();
            if (vsEws.Load(qr))
            {
                minValue = vsEws.ChartMinValue ?? 0;
                maxValue = vsEws.ChartMaxValue ?? 0;

                _isExcludeFromScoreEws = vsEws.IsExcludeFromScoreEws ?? false;
            }
            else
            {
                // Default value
                vsEws = new VitalSignEws();
                vsEws.StartAgeInDay = 0;
                vsEws.ChartMaxValue = 0;
                vsEws.ChartMinValue = 0;
            }

            decimal realMinYValue = 0;
            decimal realMaxYValue = 0;
            var vitalSignIds = new List<string>();
            if (!string.IsNullOrEmpty(parentVitalSignID))
            {
                if (isTitleShow)
                {
                    // Override Title with parent VitalSign
                    var vtp = new VitalSign();
                    vtp.LoadByPrimaryKey(parentVitalSignID);
                    if (!string.IsNullOrEmpty(vtp.VitalSignUnit))
                        chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0} ({1})", vtp.VitalSignName, vtp.VitalSignUnit);
                    else
                        chtVitalSign.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0}", vtp.VitalSignName);
                }

                // Add Series
                vitalSign = new VitalSign();
                vitalSign.LoadByPrimaryKey(parentVitalSignID);

                var query = new VitalSignQuery("v");
                query.Where(query.ParentVitalSignID == parentVitalSignID);
                query.Select(query.VitalSignID, query.VitalSignName, query.NumDecimalDigits);
                query.OrderBy(query.RowIndexInGroup.Ascending);
                var dtb = query.LoadDataTable();
                foreach (DataRow row in dtb.Rows)
                {
                    chtVitalSign.PlotArea.Series.Add(PopulateScatterLineSeriesByQuestion(patientID, registrationNo, fromRegistrationNo, ewsType, row["VitalSignID"].ToString(), questionFormID, questionID, answerType, answerSuffix, string.Empty, fromDate, toDate,
                        row["VitalSignName"].ToString(), row["NumDecimalDigits"].ToString().ToInt(), out realMinYValue,
                        out realMaxYValue, columnCount));

                    if (!_isForEws)
                    {
                        if (minValue > 0 && realMinYValue < 0) // Cegah minus
                            realMinYValue = 0;

                        if (minValue == 0 || minValue > realMinYValue)
                            minValue = realMinYValue;

                        if (maxValue < realMaxYValue)
                            maxValue = realMaxYValue;
                    }

                    vitalSignIds.Add(row["VitalSignID"].ToString());
                }

                chtVitalSign.Legend.Appearance.Visible = !_isForEws; //Biar lebar sama dan sejajar dgn score table
            }
            else
            {
                // Single Chart
                chtVitalSign.PlotArea.Series.Add(PopulateScatterLineSeriesByQuestion(patientID, registrationNo, fromRegistrationNo, ewsType, vitalSign.VitalSignID, questionFormID, questionID, answerType, answerSuffix, string.Empty, fromDate, toDate,
                    vitalSign.VitalSignName, vitalSign.NumDecimalDigits.ToInt(), out realMinYValue, out realMaxYValue, columnCount));

                if (!_isForEws)
                {
                    if (minValue > 0 && realMinYValue < 0) // Cegah minus
                        realMinYValue = 0;

                    if (minValue > realMinYValue)
                        minValue = realMinYValue;

                    if (maxValue < realMaxYValue)
                        maxValue = realMaxYValue;
                }

                chtVitalSign.Legend.Appearance.Visible = false;
            }

            chtVitalSign.PlotArea.YAxis.MinValue = minValue;
            chtVitalSign.PlotArea.YAxis.MaxValue = maxValue;

            if (vitalSign.ChartYAxisStep != null && vitalSign.ChartYAxisStep > 0) // Add label step (Handono 2303)
                chtVitalSign.PlotArea.YAxis.Step = vitalSign.ChartYAxisStep;

            if (vsEws.ChartYAxisStep != null && vsEws.ChartYAxisStep > 0) // Override Add label step (Handono 2023 nov 27)
                chtVitalSign.PlotArea.YAxis.Step = vsEws.ChartYAxisStep;

            // Add Plot Band color
            PopulatePlotBandcolor(ewsType, vitalSign.VitalSignID, vitalSignIds, vsEws.StartAgeInDay, vsEws.ChartMaxValue);

            // Set Title
            // chtVitalSign.ChartTitle.Text = vitalSign.VitalSignName;

            litEwsValueAndScore.Text = EwsScoreHtml();
            return _vitalSignItemValues;
        }

        private void PopulatePlotBandcolor(string ewsType, string vitalSignID, List<string> vitalSignIds, int? startAgeInDay, decimal? chartMaxValue)
        {
            var qrLevel = new VitalSignEwsLevelQuery();
            if (string.IsNullOrEmpty(ewsType))
                qrLevel.Where(qrLevel.SREwsType == "EWS");
            else
                qrLevel.Where(qrLevel.SREwsType == ewsType);

            qrLevel.Where(qrLevel.StartAgeInDay == startAgeInDay);
            if (vitalSignIds.Any())
                qrLevel.Where(qrLevel.VitalSignID.In(vitalSignIds));
            else
                qrLevel.Where(qrLevel.VitalSignID == vitalSignID);

            qrLevel.OrderBy(qrLevel.StartValue.Ascending);

            var ewsLevels = new VitalSignEwsLevelCollection();
            _isExistEwsLevel = ewsLevels.Load(qrLevel);
            if (_isExistEwsLevel)
            {
                decimal from = -1000;
                Color color = Color.Empty;
                foreach (var ewsLevel in ewsLevels)
                {
                    if (from == -1000)
                    {
                        from = ewsLevel.StartValue.ToDecimal();
                        color = Color.FromName(VitalSign.EwsLevelColor((ewsLevel.EwsLevel ?? 0)));
                    }
                    else
                    {
                        var to = ewsLevel.StartValue.ToDecimal();
                        chtVitalSign.PlotArea.YAxis.PlotBands.Add(new PlotBand(from, to, color, 100));

                        //Reset
                        from = ewsLevel.StartValue.ToDecimal();
                        color = Color.FromName(VitalSign.EwsLevelColor((ewsLevel.EwsLevel ?? 0)));

                    }
                }

                // Last PlotBand
                if (from > -1000 && chartMaxValue > 0)
                {
                    chtVitalSign.PlotArea.YAxis.PlotBands.Add(new PlotBand(from, chartMaxValue.ToDecimal(), color,
                        100));

                }
            }
        }

        private ScatterLineSeries PopulateScatterLineSeriesByQuestion(string patientID, string registrationNo, string fromRegistrationNo, string ewsType, string vitalSignID, string questionFormID, string questionID, string answerType, string answerSuffix, string questionAnswerSelectionID, DateTime fromDate, DateTime toDate, string vitalSignName, int numDecimalDigits, out decimal realMinYValue, out decimal realMaxYValue, int columnCount = 0)
        {
            var dtb = HistoryVitalSignByQuestion(patientID, registrationNo, fromRegistrationNo, vitalSignID, questionFormID, questionID, answerType, answerSuffix, questionAnswerSelectionID, fromDate, toDate, numDecimalDigits, columnCount);

            // Populate EwsLevel
            if (_isForEws && !_isExcludeFromScoreEws)
            {
                foreach (VitalSignItemValue value in _vitalSignItemValues)
                {
                    if (value.Value > 0)
                    {
                        value.Level = EwsLevelValue(ewsType, value.VitalSignID, _dateOfBirth, value.Time, value.Value);
                    }
                }
            }

            var series = new ScatterLineSeries { Name = vitalSignName };

            series.LabelsAppearance.DataFormatString = "{1}";
            series.LabelsAppearance.Visible = _isShowValue;
            series.TooltipsAppearance.Visible = _isShowValue;
            if (_isForEws)
            {
                series.TooltipsAppearance.Visible = false;
            }

            if (_isShowValue && !_isForEws)
            {
                series.TooltipsAppearance.Color = System.Drawing.Color.White;
                series.TooltipsAppearance.ClientTemplate = " #=kendo.format(\\'{0}\\',value.y)# on #= kendo.format(\\'{0:d-MMM-yyyy HH:mm}\\', new Date(value.x)) #";
            }

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

                if (_isForEws || columnCount > 0)
                {
                    // Untuk EWS atau ada tambahan kolom kosong, XAxis berdasarkan No
                    if (!(row["VitalSignValue"] is DBNull))
                    {
                        var seriesItem =
                            new ScatterSeriesItem(row["No"].ToInt(), row["VitalSignValue"].ToDecimal());
                        series.SeriesItems.Add(seriesItem);
                    }
                    else
                    {
                        var seriesItem =
                            new ScatterSeriesItem(row["No"].ToInt(), null);
                        series.SeriesItems.Add(seriesItem);
                    }
                }
                else
                {
                    if (!(row["VitalSignValue"] is DBNull))
                    {
                        // XAxis berdasarkan tanggal
                        var vitalSignDateTime = (DateTime)row["VitalSignDateTime"];
                        var seriesItem =
                            new ScatterSeriesItem(ConvertToJavaScriptDateTime(vitalSignDateTime), row["VitalSignValue"].ToDecimal());
                        series.SeriesItems.Add(seriesItem);
                    }
                }

            }
            realMinYValue = minValue - 5;
            realMaxYValue = maxValue + 5;
            return series;
        }
        private DataTable HistoryVitalSignByQuestion(string patientID, string registrationNo, string fromRegistrationNo, string vitalSignID, string questionFormID, string questionID, string answerType, string answerSuffix, string questionAnswerSelectionID, DateTime fromDate, DateTime toDate, int numDecimalDigits, int columnCount = 0)
        {
            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            var nth = new NursingTransHDQuery("nth");
            var ndtd = new NursingDiagnosaTransDTQuery("ndtd");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);
            phr.InnerJoin(nth).On(phr.RegistrationNo == nth.RegistrationNo);
            phr.InnerJoin(ndtd).On(nth.TransactionNo == ndtd.TransactionNo && phr.TransactionNo == ndtd.ReferenceToPhrNo);

            phr.Where(phrl.RegistrationNo == registrationNo, phrl.QuestionFormID == questionFormID, phrl.QuestionID == questionID, phrl.Or(ndtd.IsDeleted.IsNull(), ndtd.IsDeleted == false)); // Query cost optimal checked by Handono 231205

            if (fromDate > toDate)
            {
                var tmpDate = fromDate;
                fromDate = toDate;
                toDate = tmpDate;
            }

            if (DateTime.Now > toDate.AddSeconds(10))
                phr.Where(phr.RecordDate >= fromDate, phr.RecordDate < toDate.AddDays(1));
            else
                phr.Where(phr.RecordDate >= fromDate);

            // RecordDate tidak seragam ...ada yg include time ada yg tidak
            phr.Select(phr.TransactionNo,
                phr.RecordDate.Date().As("RecordDate"),
                phr.RecordTime,
                phrl.QuestionAnswerPrefix, phrl.QuestionAnswerSuffix
                , phrl.QuestionAnswerNum.As("VitalSignValue"), phrl.QuestionAnswerText, phrl.QuestionAnswerSelectionLineID);

            phr.OrderBy(phr.RecordDate.Date().Ascending, phr.RecordTime.Ascending);

            var dtb = phr.LoadDataTable();
            dtb.Columns.Add("No", typeof(System.Int16));
            dtb.Columns.Add("Description", typeof(System.String));
            dtb.Columns.Add("VitalSignDateTime", typeof(System.DateTime));

            if (_isForEws || columnCount > 0)
            {
                // Untuk EWS buat kolom sebanyak 26 kolom sesuai dgn form EWS
                var rowCount = dtb.Rows.Count;
                if (columnCount == 0)
                    columnCount = EwsColumnCount;

                if (dtb.Rows.Count < columnCount)
                {
                    for (int i = rowCount; i < columnCount; i++)
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
                    var date = Convert.ToDateTime(row["RecordDate"]);
                    var time = Convert.ToString(row["RecordTime"]);

                    if (string.IsNullOrEmpty(time))
                        time = "00:00";

                    var times = time.Split(':');
                    row["VitalSignDateTime"] = new DateTime(date.Year, date.Month, date.Day, times[0].ToInt(),
                        times[1].ToInt(), 0);

                    // Cek SRAnswerType jika bukan NUM maka cari nilainya di Score
                    if (!"NUM".Equals(answerType))
                    {
                        row["VitalSignValue"] = 0;
                        if (row["QuestionAnswerSelectionLineID"] != DBNull.Value)
                        {
                            var answerLine = new QuestionAnswerSelectionLine();
                            if (answerLine.LoadByPrimaryKey(questionAnswerSelectionID,
                                row["QuestionAnswerSelectionLineID"].ToString()))
                            {
                                row["VitalSignValue"] = answerLine.Score ?? 0;
                            }
                        }

                        row["Description"] = string.Format("{0}", row["QuestionAnswerText"]);
                    }
                    else
                        row["Description"] = string.Format("{0} {1}",
                            Convert.ToDecimal(row["VitalSignValue"]).ToString("F"), answerSuffix);
                }

                // Insert ke _vitalSignItemValues
                var isFound = false;
                foreach (var value in _vitalSignItemValues)
                {
                    if ((_isForEws && value.No.Equals(row["No"].ToInt()) && !string.IsNullOrEmpty(value.VitalSignID))
                        || (!_isForEws && value.TransactionNo == Convert.ToString(row["TransactionNo"]) && !string.IsNullOrEmpty(value.VitalSignID)))
                    {
                        isFound = true;
                        value.Value2 = Convert.ToDecimal(row["VitalSignValue"]);
                        value.VitalSignID2 = vitalSignID;
                        value.ValueInString2 = row["QuestionAnswerText"].ToString();
                        value.ValueInSelectionLineID = row["QuestionAnswerSelectionLineID"].ToString();
                        break;
                    }
                }

                if (!isFound && _vitalSignItemValues.Count <= columnCount)
                {
                    if (row["TransactionNo"] != DBNull.Value)
                        _vitalSignItemValues.Add(new VitalSignItemValue()
                        {
                            No = row["No"].ToInt(),
                            TransactionNo = row["TransactionNo"].ToString(),
                            Time = Convert.ToDateTime(row["VitalSignDateTime"]),
                            VitalSignID = vitalSignID,
                            Value = Convert.ToDecimal(row["VitalSignValue"]),
                            ValueInString = row["QuestionAnswerText"].ToString(),
                            ValueInSelectionLineID = row["QuestionAnswerSelectionLineID"].ToString()
                        });
                    else
                        _vitalSignItemValues.Add(new VitalSignItemValue()
                        {
                            No = row["No"].ToInt()
                        });
                }
            }

            return dtb;
        }
        #endregion Populate Chart Search by QuestionID (from Nursing Template)

        private int EwsLevelValue(string ewsType, string vitalSignID, DateTime birthDate, DateTime vitalSignDate, decimal vitalSignValue)
        {
            if (string.IsNullOrEmpty(ewsType)) ewsType = "EWS";
            int ageInDay = (vitalSignDate - birthDate).Days;
            var ewsLevel = _vitalSignEwsLevels.FirstOrDefault(r => r.SREwsType == ewsType && r.VitalSignID == vitalSignID && r.StartAgeInDay <= ageInDay && r.StartValue <= vitalSignValue);

            if (ewsLevel != null)
                return ewsLevel.EwsLevel ?? 0;

            var qr = new VitalSignEwsLevelQuery();
            qr.Where(qr.SREwsType == ewsType, qr.VitalSignID == vitalSignID, qr.StartAgeInDay <= ageInDay, qr.StartValue <= vitalSignValue);
            qr.es.Top = 1;
            qr.OrderBy(qr.StartAgeInDay.Descending, qr.StartValue.Descending);

            ewsLevel = new VitalSignEwsLevel();
            if (!ewsLevel.Load(qr))
            {
                ewsLevel = new VitalSignEwsLevel();
                ewsLevel.SREwsType = ewsType;
                ewsLevel.VitalSignID = vitalSignID;
                ewsLevel.StartAgeInDay = ageInDay;
                ewsLevel.StartValue = vitalSignValue;
            }
            //_vitalSignEwsLevels.AttachEntity(ewsLevel);

            return ewsLevel.EwsLevel ?? 0;
        }
        private decimal ConvertToJavaScriptDateTime(DateTime fromDate)
        {
            return (decimal)fromDate.Subtract(new DateTime(1970, 1, 1, _gmt, 0, 0)).TotalMilliseconds;
        }

        private string EwsScoreHtml()
        {
            if (!_isForEws) return string.Empty;


            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 120px;\">");
            sb.AppendLine("         <table  width=\"100%\" class='tblgraph'>");
            sb.AppendLine("             <tr><th> &nbsp; Time&nbsp;</th></tr>");
            if (_isShowValue)
            {
                sb.AppendLine("             <tr><td> &nbsp; Value&nbsp;</td></tr>");
            }

            // Score
            if (_isExistEwsLevel && !_isExcludeFromScoreEws)
            {
                sb.AppendLine("             <tr><td> &nbsp; Score&nbsp;</td></tr>");
            }

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Caption
            sb.AppendLine("<tr>");
            foreach (VitalSignItemValue value in _vitalSignItemValues)
            {
                var time = value.Time.ToString("HH:mm");
                sb.AppendLine("<th>");
                //sb.AppendLine(time == "00:00" ? "&nbsp;" : time);
                sb.AppendLine(string.IsNullOrEmpty(value.TransactionNo) ? "&nbsp;" : time);
                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            // Value
            if (_isShowValue)
            {
                sb.AppendLine("<tr>");
                foreach (VitalSignItemValue value in _vitalSignItemValues)
                {
                    var time = value.Time.ToString("HH:mm");
                    sb.AppendLine("<td>");
                    if (string.IsNullOrEmpty(value.TransactionNo))
                        sb.Append("&nbsp;");
                    else
                        sb.AppendFormat("{0}", Helper.RemoveZeroDigits(value.Value));

                    //if (time != "00:00" && value.Value2 > 0)
                    if (!string.IsNullOrEmpty(value.TransactionNo) && value.Value2 > 0)
                    {
                        sb.AppendFormat("/{0}", Helper.RemoveZeroDigits(value.Value2));
                    }

                    sb.AppendLine("</td>");
                }

                sb.AppendLine("</tr>");
            }

            // Score
            if (_isExistEwsLevel && !_isExcludeFromScoreEws)
            {
                // Score level jika ada 2 VitalSign maka hanya yg ke 1 saja yg ditampilkan
                // Score level jika ada 2 VitalSign bisa juga semuanya ikut dihitung jadi baca dari setingan (Handono 230930 RS GPI)
                sb.AppendLine("<tr>");
                //sb.AppendLine("<td>Score</td>");
                foreach (VitalSignItemValue value in _vitalSignItemValues)
                {
                    var time = value.Time.ToString("HH:mm");
                    //if (time != "00:00")
                    if (!string.IsNullOrEmpty(value.TransactionNo))
                    {
                        sb.AppendFormat("<td style='background-color:{0};'>", VitalSign.EwsLevelColor(value.Level));
                        sb.AppendFormat("{0:0}", value.Level);
                    }
                    else
                        sb.Append("<td>&nbsp;");

                    sb.AppendLine("</td>");
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");

            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }
    }
}