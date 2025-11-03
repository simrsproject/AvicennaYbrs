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

namespace Temiang.Avicenna.Module.RADT.Cpoe.Common
{

    public partial class GrowthChartCtl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ClearChart()
        {
            chtGrowthChart.PlotArea.Series.Clear();
        }

        public void PopulateChart(string patientID, string gender, DateTime dateOfBirth, string chartGroup, string chartType)
        {
            string ageGroup = "5Y"; 
            if (((DateTime.Today - dateOfBirth).TotalDays / 30) > 60) // diatas 5 tahun
                ageGroup = "20Y";

            string vitalSignID = "WEIGHT";
            string xVitalSignID = string.Empty;
            switch (chartType)
            {
                case "HA":
                    vitalSignID = "HEIGHT";
                    lblGrowthChartName.Text = string.Format("Length/height-for-age {0}", gender == "1" ? "BOYS" : "GIRLS");
                    break;
                case "WA":
                    vitalSignID = "WEIGHT";
                    lblGrowthChartName.Text = string.Format("Weight-for-age {0}", gender == "1" ? "BOYS" : "GIRLS");
                    break;
                case "CA":
                    vitalSignID = "HCCM"; //Head Circumference (CM)
                    lblGrowthChartName.Text = string.Format("Head circumference-for-age {0}", gender == "1" ? "BOYS" : "GIRLS");
                    break;
                case "BA":
                    vitalSignID = "BMI";
                    lblGrowthChartName.Text = string.Format("Body mass index-for-age percentiles {0}", gender == "1" ? "BOYS" : "GIRLS");
                    break;
                //case "SA":
                //    vitalSignID = "STTR";
                //    lblGrowthChartName.Text = string.Format("Stature-for-age {0}", gender == "1" ? "BOYS" : "GIRLS");
                //    break;
                case "WL":
                    vitalSignID = "WEIGHT";
                    xVitalSignID = "HEIGHT";
                    lblGrowthChartName.Text = string.Format("Weight-for-length/height {0}", gender == "1" ? "BOYS" : "GIRLS");
                    break;
            }

            chtGrowthChart.PlotArea.Series.Clear();
            chtGrowthChart.PlotArea.XAxis.Type = AxisType.Auto;

            //Clear
            chtGrowthChart.PlotArea.Series.Clear();

            var vitalSign = new VitalSign();
            if (vitalSign.LoadByPrimaryKey(vitalSignID))
            {
                if (!string.IsNullOrEmpty(vitalSign.VitalSignUnit))
                    chtGrowthChart.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0} ({1})", vitalSign.VitalSignName, vitalSign.VitalSignUnit);
                else
                    chtGrowthChart.PlotArea.YAxis.TitleAppearance.Text = string.Format("{0}", vitalSign.VitalSignName);

                if (!string.IsNullOrWhiteSpace(xVitalSignID))
                {
                    var xVitalSign = new VitalSign();
                    if (xVitalSign.LoadByPrimaryKey(xVitalSignID))
                    {
                        if (!string.IsNullOrEmpty(xVitalSign.VitalSignUnit))
                            chtGrowthChart.PlotArea.XAxis.TitleAppearance.Text = string.Format("{0} ({1})", xVitalSign.VitalSignName, vitalSign.VitalSignUnit);
                        else
                            chtGrowthChart.PlotArea.XAxis.TitleAppearance.Text = string.Format("{0}", xVitalSign.VitalSignName);
                    }
                }
                else
                {
                    chtGrowthChart.PlotArea.XAxis.TitleAppearance.Text = "Age";
                }

                chtGrowthChart.PlotArea.Series.Add(PopulateScatterLineSeries(patientID, vitalSign.VitalSignID, xVitalSignID, dateOfBirth, vitalSign.VitalSignName));

            }

            chtGrowthChart.Legend.Appearance.Visible = false;

            // Growth curves Line
            // x y point data source -> https://www.cdc.gov/growthcharts/percentile_data_files.htm
            PopulateGrowthSeries(chartGroup, chartType,gender, ageGroup);

        }
        private void PopulateGrowthSeries(string chartGroup, string chartType, string gender, string ageGroup )
        {
            var gcSetQr = new AppGrowthChartPointSetQuery();
            gcSetQr.Where(gcSetQr.ChartGroup==chartGroup, gcSetQr.ChartType == chartType, gcSetQr.Gender == gender, gcSetQr.AgeGroup == ageGroup, gcSetQr.IsVisible == true);
            gcSetQr.OrderBy(gcSetQr.SeriesName.Ascending);

            var gcSetSeries = new AppGrowthChartPointSetCollection();
            gcSetSeries.Load(gcSetQr);

            int yMinValue = 1000;
            int yMaxValue = 0;
            int xMinValue = 1000;
            int xMaxValue = 0;
            foreach (var item in gcSetSeries)
            {
                chtGrowthChart.PlotArea.Series.Add(PopulateGrowthSeries(chartGroup,chartType,gender, ageGroup,  item.SeriesName, item.SeriesWidth.ToInt(), ref yMinValue, ref yMaxValue, ref xMinValue, ref xMaxValue));
            }

            chtGrowthChart.PlotArea.YAxis.MinValue = yMinValue - 2;
            chtGrowthChart.PlotArea.YAxis.MaxValue = yMaxValue + 2;

            chtGrowthChart.PlotArea.XAxis.MinValue = xMinValue;
            chtGrowthChart.PlotArea.XAxis.MaxValue = xMaxValue + 6;
        }
        private ScatterLineSeries PopulateGrowthSeries(string chartGroup,string chartType,string gender, string ageGroup, 
            string seriesName, int lineWidth, ref int yMinValue, ref int yMaxValue, ref int xMinValue, ref int xMaxValue)
        {
            var qr = new AppGrowthChartPointQuery();
            qr.Where(qr.ChartGroup == chartGroup,qr.ChartType == chartType,qr.Gender == gender, qr.AgeGroup == ageGroup,  qr.SeriesName == seriesName);
            qr.OrderBy(qr.XValue.Ascending);

            var chtPoints = new AppGrowthChartPointCollection();
            chtPoints.Load(qr);

            var series = new ScatterLineSeries { Name = seriesName };
            series.LineAppearance.Width = lineWidth;

            series.LabelsAppearance.Visible = false;
            series.TooltipsAppearance.Visible = false;
            series.MarkersAppearance.Visible = false;

            foreach (AppGrowthChartPoint point in chtPoints)
            {
                // XAxis berdasarkan tanggal
                var seriesItem =
                    new ScatterSeriesItem(point.XValue.ToDecimal(), point.YValue.ToDecimal());
                series.SeriesItems.Add(seriesItem);

                if (yMinValue > point.YValue.ToInt())
                    yMinValue = point.YValue.ToInt();

                if (yMaxValue < point.YValue.ToInt())
                    yMaxValue = point.YValue.ToInt();

                if (xMinValue > point.XValue.ToInt())
                    xMinValue = point.XValue.ToInt();

                if (xMaxValue < point.XValue.ToInt())
                    xMaxValue = point.XValue.ToInt();
            }

            return series;
        }


        private ScatterLineSeries PopulateScatterLineSeries(string patientID, string vitalSignID, string xVitalSignID, DateTime dateOfBirth, string vitalSignName)
        {
            var dtb = GetData(patientID, vitalSignID, xVitalSignID, dateOfBirth);

            var series = new ScatterLineSeries { Name = vitalSignName };

            series.LabelsAppearance.DataFormatString = "{1}";
            series.LabelsAppearance.Visible = true;
            series.TooltipsAppearance.Visible = true;
            series.TooltipsAppearance.Color = System.Drawing.Color.White;


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
                    var xValue = (Decimal)row["XValue"];
                    var seriesItem =
                        new ScatterSeriesItem(xValue, row["VitalSignValue"].ToDecimal());
                    series.SeriesItems.Add(seriesItem);
                }

            }
            return series;
        }
        public DataTable GetData(string patientID, string vitalSignID, string xVitalSignID, DateTime dateOfBirth)
        {
            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);

            var reg = new RegistrationQuery("r");
            phr.InnerJoin(reg).On(phr.RegistrationNo == reg.RegistrationNo);

            var quest = new QuestionQuery("q");
            phr.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            phr.Where(reg.PatientID == patientID);

            if (string.IsNullOrWhiteSpace(xVitalSignID))
            {
                phr.Where(quest.VitalSignID == vitalSignID);
            }
            else
                phr.Where(phr.Or(quest.VitalSignID == vitalSignID, quest.VitalSignID == xVitalSignID));

            phr.Select(phr.TransactionNo, phr.RecordDate, phr.RecordTime, quest.VitalSignID, phrl.QuestionAnswerPrefix, phrl.QuestionAnswerSuffix
                , phrl.QuestionAnswerNum.As("VitalSignValue"), phrl.QuestionAnswerText.As("VitalSignValueInText"), quest.SRAnswerType
                , quest.QuestionAnswerSelectionID, phrl.QuestionAnswerText, phrl.QuestionAnswerSelectionLineID);

            phr.OrderBy(phr.RecordDate.Ascending, phr.RecordTime.Ascending);

            var dtb = phr.LoadDataTable();

            dtb.Columns.Add("No", typeof(System.Int16));
            dtb.Columns.Add("Description", typeof(System.String));
            dtb.Columns.Add("XValue", typeof(System.Decimal));

            dtb.PrimaryKey = new DataColumn[] { dtb.Columns["TransactionNo"], dtb.Columns["VitalSignID"] };

            var rowNo = 0;
            foreach (System.Data.DataRow row in dtb.Rows)
            {
                rowNo++;
                row["No"] = rowNo;

                if (row["TransactionNo"] != DBNull.Value)
                {
                    if (string.IsNullOrWhiteSpace(xVitalSignID))
                    {
                        var recordDate = Convert.ToDateTime(row["RecordDate"]);
                        row["XValue"] = ((recordDate - dateOfBirth).TotalDays / 30);
                    }
                    else
                    {
                        // Cari di xVitalSignID
                        var xRow = dtb.Rows.Find(new object[] { row["TransactionNo"], xVitalSignID });
                        if (xRow != null)
                        {
                            row["XValue"] = row["VitalSignValue"];
                        }
                    }

                    row["Description"] = string.Format("{0} {1}",
                        Convert.ToDecimal(row["VitalSignValue"]).ToString("F"), row["QuestionAnswerSuffix"]);
                }
            }

            return dtb;
        }

        private string RemoveZeroDigits(decimal value)
        {
            return value == -1 ? "-" : Convert.ToString(value / 1.000000000000000000000000000000M);
        }
    }
}