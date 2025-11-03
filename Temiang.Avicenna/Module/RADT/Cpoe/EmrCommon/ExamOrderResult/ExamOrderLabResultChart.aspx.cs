using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using System.Data;
using System.Data.Linq;
using Telerik.Charting;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using DataRow = System.Data.DataRow;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ExamOrderLabResultChart : BasePageDialog
    {

        protected void Page_Init(object sender, EventArgs e)
        {
        }
        private string _patientID;
        private string PatientID
        {
            get
            {
                // Jangan ambil dari QueryString krn bisa jadi utk PatientID yg berbeda tetapi masih pasien yg sama (PatientRelated)
                //return Request.QueryString["patid"];
                if (!string.IsNullOrEmpty(RegistrationNo) && string.IsNullOrEmpty(_patientID))
                {
                    var reg = new Registration();
                    reg.LoadByPrimaryKey(RegistrationNo);
                    _patientID = reg.PatientID;
                }
                else
                    _patientID = Request.QueryString["patid"];

                return _patientID;
            }
        }
        private string RegistrationNo
        {
            get { return Request.QueryString["regno"]; }
        }
        private string FractionID
        {
            get { return Request.QueryString["frid"]; }
        }
        private string FractionName
        {
            get { return Request.QueryString["frnm"]; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";

            var pat = new Patient();
            if (pat.LoadByPrimaryKey(PatientID))
            {
                this.Title = "Lab Result Chart : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
            }

            if (!IsPostBack)
            {
                var reg = new Registration();
                reg.LoadByPrimaryKey(RegistrationNo);
                txtFromDate.SelectedDate = reg.RegistrationDate;

                PopulateChart();
            }

        }

        private void PopulateChart()
        {
            var dtbResult = new DataTable();
            if (AppSession.Parameter.IsUsingHisInterop)
            {
                switch (AppSession.Parameter.LisInterop)
                {
                    case "SYSMEX":
                        {
                            var qrInt = new BusinessObject.Interop.SYSMEX.VwHasilPasienQuery("q");
                            qrInt.Where(qrInt.PatientID == PatientID, qrInt.LabOrderCode == FractionID);

                            if (txtFromDate.SelectedDate != null)
                                qrInt.Where(qrInt.OrderLabTglOrder >= txtFromDate.SelectedDate);

                            qrInt.OrderBy(qrInt.OrderLabTglOrder.Ascending, qrInt.OrderLabNo.Ascending);
                            dtbResult = qrInt.LoadDataTable();
                            dtbResult.Columns.Add("Color", typeof(string));
                            break;
                        }
                    case "RSCH":
                        {
                            var patient = new Patient();
                            patient.LoadByPrimaryKey(PatientID);
                            var qrRsch = new BusinessObject.Interop.RSCH.VwHasilPasienQuery("q");
                            qrRsch.Where(qrRsch.OrderLabNoMR == patient.MedicalNo,
                                qrRsch.CheckupResultFractionCode == FractionID);

                            if (txtFromDate.SelectedDate != null)
                                qrRsch.Where(qrRsch.OrderLabTglOrder >= txtFromDate.SelectedDate);

                            qrRsch.OrderBy(qrRsch.OrderLabTglOrder.Ascending, qrRsch.OrderLabNo.Ascending);
                            dtbResult = qrRsch.LoadDataTable();
                            dtbResult.Columns.Add("Color", typeof(string));
                            break;
                        }
                    case "VANSLAB":
                        {
                            var patient = new Patient();
                            patient.LoadByPrimaryKey(PatientID);

                            var qr = new BusinessObject.Interop.VANSLAB.LabHasilQuery("a");
                            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.VANSLAB_LIS_INTEROP_CONNECTION_NAME;

                            qr.Where(qr.NoRM == patient.MedicalNo, qr.KodePemeriksaan == FractionID);

                            if (txtFromDate.SelectedDate != null)
                                qr.Where(qr.TglHasil >= txtFromDate.SelectedDate);

                            qr.Select(qr.NoRegistrasi.As("OrderLabNo"), qr.TglHasil.As("OrderLabTglOrder"), qr.Hasil.As("Result"), qr.Unit.As("UNIT"),
                                "<CASE WHEN a.flag='L' THEN 'blue' WHEN a.flag='H' THEN 'red' ELSE '' END as Color>");

                            qr.OrderBy(qr.TglHasil.Ascending, qr.NoRegistrasi.Ascending);
                            dtbResult = qr.LoadDataTable();
                            break;
                        }
                    case "WYNAKOM":
                        {
                            var reg = new RegistrationQuery();
                            reg.Where(reg.PatientID == PatientID, reg.RegistrationDate >= txtFromDate.SelectedDate);
                            reg.Select(reg.RegistrationNo);
                            var dtbRegs = reg.LoadDataTable();

                            var qr = new BusinessObject.Interop.Wynakom.OrderedResultsQuery("a");
                            qr.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;

                            var qrWReg = new BusinessObject.Interop.Wynakom.RegistrationQuery("r");
                            qrWReg.es2.Connection.Name = AppConstant.HIS_INTEROP.WYNAKOM_LIS_INTEROP_CONNECTION_NAME;
                            qr.InnerJoin(qrWReg).On(qr.LisRegNo == qrWReg.LisRegNo);
                            qr.Where(qrWReg.VisitNumber.In(dtbRegs.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray()), qr.LisTestId == FractionID);

                            if (txtFromDate.SelectedDate != null)
                                qr.Where(qrWReg.OrderDateTime >= txtFromDate.SelectedDate);

                            qr.Select(qr.HisRegNo.As("OrderLabNo"), qrWReg.OrderDateTime.As("OrderLabTglOrder"), qr.Result.As("Result"), qr.TestUnitsName.As("UNIT"),
                                "<CASE WHEN a.test_flag_sign='L' OR a.test_flag_sign='H' THEN 'orange' WHEN a.test_flag_sign='HH' OR a.test_flag_sign='LL' THEN 'red' ELSE '' END as Color>");
                            qr.OrderBy(qrWReg.OrderDateTime.Ascending, qr.HisRegNo.Ascending);
                            dtbResult = qr.LoadDataTable();

                            break;
                        }
                    default:
                        return;
                }
            }

            if (dtbResult.Rows.Count < 1)
                dtbResult = LabResultFromManualEntry();
            else
            {
                // Tambahkan dari inout manual ItemLaboratoryDetail
                dtbResult.Merge(LabResultFromManualEntry());

                // Sort
                dtbResult = dtbResult.Select(string.Empty, "OrderLabTglOrder ASC, OrderLabNo ASC").CopyToDataTable();
            }

            dtbResult.Columns.Add("ResultNum", typeof(System.Decimal));
            dtbResult.Columns.Add("IsNumericResult", typeof(System.Boolean));

            // Populate ResultNum
            decimal minValue = 99999999;
            decimal maxValue = 0;
            var unit = string.Empty;
            foreach (DataRow row in dtbResult.Rows)
            {
                unit = row["UNIT"].ToString();

                try
                {
                    var resultNum = Convert.ToDecimal(row["Result"]);
                    row["ResultNum"] = resultNum;
                    row["IsNumericResult"] = true;

                    if (minValue > resultNum)
                        minValue = resultNum;

                    if (maxValue < resultNum)
                        maxValue = resultNum;

                }
                catch
                {
                    row["IsNumericResult"] = false;
                }
            }

            //Clear
            chartControl.PlotArea.Series.Clear();
            chartControl.PlotArea.Series.Add(PopulateScatterLineSeries(dtbResult, FractionName, 2));
            chartControl.Legend.Appearance.Visible = false;

            // Set Min Max
            chartControl.PlotArea.YAxis.MinValue = minValue - (minValue * (decimal)0.2);
            chartControl.PlotArea.YAxis.MaxValue = maxValue + (maxValue * (decimal)0.2);

            chartControl.PlotArea.YAxis.TitleAppearance.Text = unit;

            // Set Title
            chartControl.ChartTitle.Text = FractionName;
        }

        private DataTable LabResultFromManualEntry()
        {
            var qr = new TransChargesItemQuery("dt");
            var order = new TransChargesQuery("hd");
            qr.InnerJoin(order).On(qr.TransactionNo == order.TransactionNo);

            var ilab = new ItemLaboratoryQuery("il");
            qr.InnerJoin(ilab).On(qr.ItemID == ilab.ItemID);

            var reg = new RegistrationQuery("r");
            qr.InnerJoin(reg).On(order.RegistrationNo == reg.RegistrationNo);

            qr.Where(reg.PatientID == PatientID, qr.ItemID == FractionID);

            if (txtFromDate.SelectedDate != null)
                qr.Where(order.TransactionDate >= txtFromDate.SelectedDate);


            qr.Select(order.TransactionNo.As("OrderLabNo"), order.TransactionDate.As("OrderLabTglOrder"), qr.ResultValue.As("Result"),
                ilab.SRLaboratoryUnit.As("UNIT"));

            qr.OrderBy(order.TransactionDate.Ascending, qr.TransactionNo.Ascending);

            var dtbResult = qr.LoadDataTable();
            dtbResult.Columns.Add("Color", typeof(string));
            return dtbResult;
        }

        private ScatterLineSeries PopulateScatterLineSeries(DataTable dtbResult, string vitalSignName, int numDecimalDigits)
        {
            ScatterLineSeries series = new ScatterLineSeries();
            series.Name = vitalSignName;
            series.LabelsAppearance.Visible = true;
            series.LabelsAppearance.DataFormatString = "{1}";

            series.TooltipsAppearance.Color = System.Drawing.Color.White;
            //series.TooltipsAppearance.ClientTemplate = "#= kendo.format(\\'{0:d-MMM-yyyy HH:mm}\\', new Date(value.x)) # <br /> #=kendo.format(\\'{0}\\',value.y)#";
            series.TooltipsAppearance.ClientTemplate = "#= kendo.format(\\'{0:d-MMM-yyyy HH:mm}\\', new Date(value.x)) #";

            foreach (DataRow row in dtbResult.Rows)
            {
                if (true.Equals(row["IsNumericResult"]))
                {
                    var value = (decimal)row["ResultNum"];
                    var date = (DateTime)row["OrderLabTglOrder"];
                    ScatterSeriesItem seriesItem;
                    switch (row["Color"].ToString())
                    {
                        case "red":
                            seriesItem = new ScatterSeriesItem(ConvertToJavaScriptDateTime(date), value, System.Drawing.Color.Red);
                            break;
                        case "orange":
                            seriesItem = new ScatterSeriesItem(ConvertToJavaScriptDateTime(date), value, System.Drawing.Color.Orange);
                            break;
                        default:
                            seriesItem = new ScatterSeriesItem(ConvertToJavaScriptDateTime(date), value, System.Drawing.Color.Black);
                            break;
                    }
                    series.SeriesItems.Add(seriesItem);
                }
            }
            return series;
        }
        private decimal ConvertToJavaScriptDateTime(DateTime fromDate)
        {
            return (decimal)fromDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            PopulateChart();
        }
    }

}
