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
using System.Drawing.Imaging;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    /// <summary>
    /// Form display Partograf untuk memantau kemajuan kala satu persalinan dan informasi 
    /// untuk membuat keputusan klinik dalam bentuk grafik dan table
    /// </summary>
    /// 
    /// Create By: Handono
    /// Create Date: Feb 2023
    /// Request: RS Yos Sudarso
    /// Ref: https://www.alomedika.com/tindakan-medis/obstetrik-dan-ginekologi/partograph/teknik
    /// 
    /// Modif History:
    /// -- 2023 March 21 Handono --
    /// Posisi point pada grafik cervix dan contraction dibuat sejajar
    /// 
    /// -- 2023 Des 03 Handono --
    /// - Jumlah kolom dibuat sama untuk semua grafik
    /// - Tambah fitur edit dan add data menggunakan PhrCtl yg dimodif bisa untuk NursingDiagnosaTemplate
    /// - Tombol navigasi tambah prev, next date
    /// - Rubah tombol Last VS - > Last Monitoring
    /// 
    /// -- 2023 Des 08 Handono --
    /// - Jumlah kolom bisa diseting di parameter PartographColCount
    /// - Perbaikan Garis alert pada Cervix Graph
    /// 
    /// -- 2024 Jan 11 Handono --
    /// - Posisi start tergantung angka Pembukaan Cervix yg jika di >4 maka posisi start menyesuaikan dgn garis kuning
    public partial class Partograph : BasePageDialog
    {
        private int ColumnCount
        {
            get
            {
                if (ViewState["cc"] == null)
                {
                    var colCount = AppParameter.GetParameterValue(AppParameter.ParameterItem.PartographColCount).ToInt();
                    if (colCount > 46) colCount = 46; // Batasi
                    ViewState["cc"] = colCount;
                }
                return (int)ViewState["cc"];
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {

        }
        private string FromRegistrationNo
        {
            get { return Request.QueryString["fregno"]; }
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["patid"]))
                    return Request.QueryString["patid"];

                if (!string.IsNullOrEmpty(RegistrationNo))
                    return CurrentRegistration.PatientID;
                else
                    return string.Empty;
            }
        }

        private string QuestionFormID
        {
            get
            {
                if (ViewState["qif"] == null)
                {
                    // Check Template
                    var nd = new NursingDiagnosaTemplate();
                    nd.Query.Where(nd.Query.TemplateName == "Partograph", nd.Query.IsActive == true);
                    nd.Query.es.Top = 1;
                    if (nd.Query.Load())
                        ViewState["qif"] = nd.TemplateID.ToString();
                    else
                        ViewState["qif"] = string.Empty;
                }
                return Convert.ToString(ViewState["qif"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //ButtonOk.Visible = false;
            //ButtonCancel.Text = "Close";
            FooterVisible = false;
            this.Title = "Partograph";


            if (!IsPostBack)
            {
                var pat = new Patient();
                pat.LoadByPrimaryKey(PatientID);


                txtFromDate.SelectedDate = CurrentRegistration.RegistrationDate;
                lblPatientName.Text = pat.PatientName;
                lblMedicalNo.Text = pat.MedicalNo;
                lblRegistrationNo.Text = RegistrationNo;
                lblSex.Text = pat.Sex;
                var toDay = DateTime.Today;
                if (pat.DateOfBirth != null)
                {
                    var birthDate = pat.DateOfBirth.Value;
                    lblBirthDate.Text = birthDate.ToString(AppConstant.DisplayFormat.Date);

                    lblAge.Text = string.Format("{0}Y, {1}M, {2}D",
                        Helper.GetAgeInYear(birthDate, toDay), Helper.GetAgeInMonth(birthDate, toDay),
                        Helper.GetAgeInDay(birthDate, toDay));
                }

                txtFromDate.SelectedDate = toDay;
                PopulatePartograph(QuestionFormID, RegistrationNo, FromRegistrationNo);
            }
        }
        #region Navigation
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulatePartograph(QuestionFormID, RegistrationNo, FromRegistrationNo);
        }

        protected void txtFromDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            PopulatePartograph(QuestionFormID, RegistrationNo, FromRegistrationNo);
        }

        protected void btnPrevDate_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = txtFromDate.SelectedDate.Value.AddDays(-1);
            PopulatePartograph(QuestionFormID, RegistrationNo, FromRegistrationNo);
        }

        protected void btnNextDate_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = txtFromDate.SelectedDate.Value.AddDays(1);
            PopulatePartograph(QuestionFormID, RegistrationNo, FromRegistrationNo);
        }
        protected void btnStartFromRegistration_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = CurrentRegistration.RegistrationDate;
            PopulatePartograph(QuestionFormID, RegistrationNo, FromRegistrationNo);
        }

        protected void btnLastMonitoring_Click(object sender, EventArgs e)
        {
            var lastMon = LastMonitoringDate(RegistrationNo, FromRegistrationNo, QuestionFormID);
            if (lastMon != null && lastMon.Year == 1900) { lastMon = DateTime.Today; }
            txtFromDate.SelectedDate = lastMon;
            PopulatePartograph(QuestionFormID, RegistrationNo, FromRegistrationNo);
        }
        private DateTime LastMonitoringDate(string registrationNo, string referFromRegistrationNo, string questionFormID)
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


            phrl.Where(phrl.QuestionFormID == questionFormID);

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


        private void PopulatePartograph(string questionFormID, string registrationNo, string fromRegistrationNo)
        {
            var startCol = 0;
            var regs = AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
            var dtbPhrLine = PhrLineHist(questionFormID, regs, txtFromDate.SelectedDate.Value);

            // Cek pembukaan servik untuk start kolom
            var exp = string.Format("VitalSignID='{0}'", "PTG.KP.SVK");
            var sortBy = "RecordDate ASC, RecordTime ASC, VitalSignID ASC";
            var rowsServik = dtbPhrLine.Select(exp, sortBy);
            if (rowsServik.Length > 0)
            {
                startCol = Convert.ToInt32(rowsServik[0]["QuestionAnswerNum"]) - 4;
                if (startCol < 0) startCol = 0;
            }

            // Denyut Jantung janin
            PopulateChart(chtFetalHearthRate, ColumnCount, "PTG.KJ.DJJ", string.Empty, dtbPhrLine, false, startCol);

            litLiquorMoulding.Text = LiquorMouldingGraph(dtbPhrLine, startCol);

            //Servic
            PopulateChart(chtCervix, ColumnCount, "PTG.KP.PKP", "PTG.KP.SVK", dtbPhrLine, true, startCol);

            //Blood Preasure
            PopulatePulseBpChart(dtbPhrLine, ColumnCount, startCol);

            litContraction.Text = ContractionGraph(dtbPhrLine, startCol);
            litDrugAndFluid.Text = DrugAndFluidGraph(dtbPhrLine, startCol);
            litOxytocin.Text = OxytocinGraph(dtbPhrLine, startCol);
            litSuhu.Text = TemperatureGraph(dtbPhrLine, startCol);
            litUrine.Text = UrineGraph(dtbPhrLine, startCol);

            litMenu.Text = MenuAddEditHtml(dtbPhrLine, RegistrationNo, QuestionFormID, startCol);
        }

        #region Populate Chart
        private DataTable PhrLineHist(string questionFormID, List<string> regs, DateTime date)
        {
            //Populate Datasource
            var phr = new PatientHealthRecordQuery("phr");
            var phrl = new PatientHealthRecordLineQuery("phrl");
            phr.InnerJoin(phrl).On(phr.TransactionNo == phrl.TransactionNo);

            var quest = new QuestionQuery("q");
            phr.InnerJoin(quest).On(phrl.QuestionID == quest.QuestionID);

            phr.Where(phr.RegistrationNo.In(regs), phr.QuestionFormID == questionFormID);

            phr.Where(phr.RecordDate >= date, phr.RecordDate < date.AddDays(1));

            // RecordDate tidak seragam ...ada yg include time ada yg tidak
            phr.Select(phr.TransactionNo,
                phr.RecordDate.Date().As("RecordDate"),
                phr.RecordTime,
                phrl.QuestionID,
                quest.VitalSignID,
                phrl.QuestionAnswerPrefix, phrl.QuestionAnswerSuffix
                , phrl.QuestionAnswerNum
                , quest.QuestionAnswerSelectionID, phrl.QuestionAnswerText, phrl.QuestionAnswerSelectionLineID);

            return phr.LoadDataTable();
        }

        private void PopulateChart(RadHtmlChart cht, int colCount, string vitalSignId1, string vitalSignId2, DataTable dtbPhrLine, bool isShowAllValue = false, int startCol = 0)
        {
            // Datasource
            var dtb = new DataTable();
            for (int j = 0; j < colCount; j++)
            {
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));
            }

            for (int i = 0; i < 3; i++)
            {
                var row = dtb.NewRow();
                dtb.Rows.Add(row);
            }

            //var dtbVs = VitalSignHist(questionFormID, vitalSignId1, vitalSignId2);
            var exp = string.Format("VitalSignID='{0}' OR VitalSignID='{1}'", vitalSignId1, vitalSignId2);
            var sortBy = "RecordDate ASC, RecordTime ASC, VitalSignID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                if (vitalSignId1.Equals(rowVs["VitalSignID"]))
                {
                    // Info Time
                    dtb.Rows[0][colNo] = rowVs["RecordTime"];
                    dtb.Rows[1][colNo] = rowVs["QuestionAnswerNum"].ToInt();

                    if (!string.IsNullOrWhiteSpace(vitalSignId2))
                    {
                        var rowVs2 = rowsSelecteds[rowNo + 1]; // Next row seharusnya berisi vitalSignId2
                        if (rowVs["RecordDate"].Equals(rowVs2["RecordDate"]) && rowVs["RecordTime"].Equals(rowVs2["RecordTime"]))
                            dtb.Rows[2][colNo] = rowVs2["QuestionAnswerNum"].ToInt();
                    }

                    colNo++;
                    if (colNo >= colCount)
                        break;
                }

                rowNo++;
            }

            var xAxis = cht.PlotArea.XAxis;
            xAxis.Items.Clear();

            var ser1 = cht.PlotArea.Series[0];


            ser1.Items.Clear();

            for (int j = 0; j < colCount; j++)
            {
                // xAxis
                //xAxis.Items.Add(new AxisItem(string.Format("{0}\\n{1}", j, dtb.Rows[0][j]))); // Start dari 0
                xAxis.Items.Add(new AxisItem(string.Format("{0}", j))); // Start dari 0, info jam tidak ditampilkan ..bisa lihat header

                var val1 = dtb.Rows[1][j];
                if (val1 != DBNull.Value && (isShowAllValue || val1.ToDecimal() > 0))
                    ser1.Items.Add(val1.ToDecimal());
                else
                    ser1.Items.Add(new SeriesItem());

            }

            if (!string.IsNullOrWhiteSpace(vitalSignId2))
            {
                var ser2 = cht.PlotArea.Series[1];
                ser2.Items.Clear();

                for (int j = 0; j < colCount; j++)
                {
                    var val2 = dtb.Rows[2][j];
                    if (val2 != DBNull.Value && (isShowAllValue || val2.ToDecimal() > 0))
                        ser2.Items.Add(val2.ToDecimal());
                    else
                        ser2.Items.Add(new SeriesItem());
                }
            }
        }


        private void PopulatePulseBpChart(DataTable dtbPhrLine, int columCount, int startCol)
        {
            // Datasource
            var dtb = new DataTable();
            for (int j = 0; j < columCount; j++)
            {
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));
            }

            for (int i = 0; i < 4; i++)
            {
                var row = dtb.NewRow();
                dtb.Rows.Add(row);
            }

            //var dtbVs = VitalSignHist(questionFormID, "BP1", "BP2", "HR");
            var exp = string.Format("VitalSignID='{0}' OR VitalSignID='{1}' OR VitalSignID='{2}'", "BP1", "BP2", "HR");
            var sortBy = "RecordDate ASC, RecordTime ASC, VitalSignID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                if ("BP1".Equals(rowVs["VitalSignID"]))
                {
                    // Info Time
                    dtb.Rows[0][colNo] = rowVs["RecordTime"];
                    dtb.Rows[1][colNo] = rowVs["QuestionAnswerNum"].ToInt();

                    var rowVs2 = rowsSelecteds[rowNo + 1]; // Next row seharusnya berisi vitalSignId2
                    if (rowVs["RecordDate"].Equals(rowVs2["RecordDate"]) && rowVs["RecordTime"].Equals(rowVs2["RecordTime"]))
                        dtb.Rows[2][colNo] = rowVs2["QuestionAnswerNum"].ToInt();

                    var rowVs3 = rowsSelecteds[rowNo + 2]; // Next row seharusnya berisi vitalSignId3
                    if (rowVs["RecordDate"].Equals(rowVs3["RecordDate"]) && rowVs3["RecordTime"].Equals(rowVs3["RecordTime"]))
                        dtb.Rows[3][colNo] = rowVs3["QuestionAnswerNum"].ToInt();

                    colNo++;
                    if (colNo >= columCount)
                        break;
                }

                rowNo++;
            }

            var xAxis = chtPulseBp.PlotArea.XAxis;
            xAxis.Items.Clear();

            var serPulse = chtPulseBp.PlotArea.Series[0];
            //var serBP = (RangeColumnSeries)chtPulseBp.PlotArea.Series[1]; // RangeColumnSeries for BP
            var serBPS = chtPulseBp.PlotArea.Series[1];
            var serBPD = chtPulseBp.PlotArea.Series[2];

            serPulse.Items.Clear();
            //serBP.SeriesItems.Clear();
            serBPS.Items.Clear();
            serBPD.Items.Clear();
            for (int j = 0; j < columCount; j++)
            {
                // xAxis
                //xAxis.Items.Add(new AxisItem(string.Format("{0}\\n{1}", j + 1, dtb.Rows[0][j])));
                xAxis.Items.Add(new AxisItem(string.Format("{0}", j))); // Start dari 0, info jam tidak ditampilkan ..bisa lihat header

                // BP
                //var val2 = dtb.Rows[1][j]; // BP1 Sistolic
                //var val1 = dtb.Rows[2][j]; // BP2 Diastolic
                //if (val1 != DBNull.Value)
                //    serBP.SeriesItems.Add(new RangeSeriesItem(val1.ToDecimal(), val2.ToDecimal()));
                //else
                //    serBP.SeriesItems.Add(new RangeSeriesItem(0, 0));

                var val1 = dtb.Rows[1][j];
                if (val1 != DBNull.Value)
                    serBPS.Items.Add(val1.ToInt());
                else
                    serBPS.Items.Add(new SeriesItem());

                var val2 = dtb.Rows[2][j];
                if (val2 != DBNull.Value)
                    serBPD.Items.Add(val2.ToInt());
                else
                    serBPD.Items.Add(new SeriesItem());

                var val3 = dtb.Rows[3][j];
                if (val3 != DBNull.Value)
                    serPulse.Items.Add(val3.ToInt());
                else
                    serPulse.Items.Add(new SeriesItem());
            }
        }

        protected string LiquorMouldingGraph(DataTable dtbPhrLine, int startCol)
        {
            var colCount = ColumnCount;

            // Datasource
            var dtb = new DataTable();
            for (int j = 0; j < colCount; j++)
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));

            for (int i = 0; i < 2; i++)
            {
                var row = dtb.NewRow();
                dtb.Rows.Add(row);
            }


            //var dtbVs = VitalSignHist(questionFormID, "PTG.KJ.KTB", "PTG.KJ.PNY");
            var exp = string.Format("QuestionID='{0}' OR QuestionID='{1}'", "PTG.KJ.KTB", "PTG.KJ.PNY");
            var sortBy = "RecordDate ASC, RecordTime ASC, QuestionID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                if ("PTG.KJ.KTB".Equals(rowVs["QuestionID"])) //Liquor
                {
                    var rowVs2 = rowsSelecteds[rowNo + 1]; // Next row seharusnya berisi Question PTG.KJ.PNY Moulding

                    if (rowVs["RecordDate"].Equals(rowVs2["RecordDate"]) && rowVs["RecordTime"].Equals(rowVs2["RecordTime"]))
                    {
                        dtb.Rows[0][colNo] = string.Format("{0}|{1}", rowVs["QuestionAnswerSelectionLineID"], rowVs["QuestionAnswerText"]);
                        dtb.Rows[1][colNo] = string.Format("{0}|{1}", rowVs2["QuestionAnswerSelectionLineID"], rowVs2["QuestionAnswerText"]);
                        colNo++;
                        if (colNo >= colCount)
                            break;
                    }
                }
                rowNo++;
            }

            // Create graph
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp; Liquor&nbsp;</td></tr>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp; Moulding&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            for (int i = 0; i < 2; i++)
            {
                sb.AppendLine("<tr>");
                for (int j = 0; j < colCount; j++)
                {
                    var val = dtb.Rows[i][j].ToString();
                    if (val.Contains("|"))
                    {
                        var vals = val.Split('|');
                        sb.AppendFormat("<td><div class=\"tooltip\">{0}<span class=\"tooltiptext\">{1}</span></div></td>", vals[0], vals[1]);
                    }
                    else
                        sb.AppendLine("<td></td>");

                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        protected string ContractionGraph(DataTable dtbPhrLine, int startCol)
        {
            //Kontraksi Uterus
            //Kolom kontraksi uterus berada tepat di bawah kolom untuk pencatatan penurunan bagian terbawah janin.
            //Pencatatan kolom kontraksi uterus dilakukan setiap 30 menit sekali selama 10 menit.
            //Selama 10 menit petugas medis akan mencatat berapa kali kontraksi yang terjadi selama 10 menit
            //serta berapa lama kontraksi dalam hitungan detik.[1] Pencatatan menggunakan simbol sebagai berikut;

            //Tandai kotak dengan titik-titik untuk hasil kontraksi yang berlangsung selama<20 detik.
            //Tandai kotak dengan garis-garis untuk hasil kontraksi yang berlangsung selama 20 - 40 detik
            //Arsir penuh kotak untuk hasil kontraksi yang berlangsung selama > 40 detik[1]
            //Src: https://www.alomedika.com/tindakan-medis/obstetrik-dan-ginekologi/partograph/teknik

            // 01: <20 -> image garis2 horizontal
            // 02: 20 - 40 -> image garis2 diagonal
            // 03: >40 -> gray background


            var colCount = ColumnCount;

            // Datasource
            var dtb = new DataTable();
            // Add Column
            for (int j = 0; j < colCount; j++)
            {
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));
            }

            // Add Column untuk text pada tooltip
            for (int j = 0; j < colCount; j++)
            {
                dtb.Columns.Add(new DataColumn("T_" + j.ToString(), typeof(string)));
            }

            // Add Row
            for (int i = 0; i < 5; i++)
            {
                var row = dtb.NewRow();
                dtb.Rows.Add(row);
            }

            var exp = string.Format("QuestionID='{0}' OR QuestionID='{1}'", "PTG.KP.KUT", "PTG.KP.JMK");
            var sortBy = "RecordDate ASC, RecordTime ASC, QuestionID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                if ("PTG.KP.JMK".Equals(rowVs["QuestionID"]))
                {
                    var jmk = rowVs["QuestionAnswerNum"].ToInt();
                    var rowKut = rowsSelecteds[rowNo + 1]; // Next row seharusnya berisi Question PTG.KP.KUT

                    if (rowVs["RecordDate"].Equals(rowKut["RecordDate"]) && rowVs["RecordTime"].Equals(rowKut["RecordTime"]))
                    {
                        if (jmk == 0) jmk = 1;
                        if (jmk > 5) jmk = 5; // Max 5
                        for (int i = 0; i < jmk; i++)
                        {
                            dtb.Rows[4 - i][colNo] = rowKut["QuestionAnswerSelectionLineID"].ToString();
                            dtb.Rows[4 - i][colNo + colCount] = rowKut["QuestionAnswerText"].ToString();
                        }
                        colNo++;

                        if (colNo >= colCount)
                            break;
                    }
                }
                rowNo++;
            }

            // Create graph
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            for (int i = 0; i < 5; i++)
            {
                sb.AppendFormat("             <tr><td style=\"width:80px\">&nbsp;{0}&nbsp;</td></tr>", 5 - i);
            }
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            for (int i = 0; i < 5; i++)
            {
                sb.AppendLine("<tr>");
                for (int j = 0; j < colCount; j++)
                {
                    //sb.AppendFormat("<td><div class=\"tooltip\">{0}<span class=\"tooltiptext\">{1}</span></div></td>", vals[0], vals[1]);

                    var val = (dtb.Rows[i][j]).ToString();
                    var text = (dtb.Rows[i][colCount + j]).ToString();
                    if (!string.IsNullOrWhiteSpace(val))
                        sb.AppendFormat("<td class=\"ctgBg{0}\"><div class=\"tooltip\"><span class=\"tooltiptext\">{1}</span></div></td>", val, text);
                    else
                        sb.AppendLine("<td></td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }

        protected string OxytocinGraph(DataTable dtbPhrLine, int startCol)
        {
            var colCount = ColumnCount;

            // Datasource
            var dtb = new DataTable();
            for (int j = 0; j < colCount; j++)
            {
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));
            }

            for (int i = 0; i < 2; i++)
            {
                var row = dtb.NewRow();
                dtb.Rows.Add(row);
            }

            // PTG.OK.OKS - Pemberian Oksitoksin UI/500 ml: - Jumlah (ml)
            // PTG.OK.TTS - Pemberian Oksitoksin UI/500 ml: - Tetesan per menit

            var exp = string.Format("QuestionID='{0}' OR QuestionID='{1}'", "PTG.OK.OKS", "PTG.OK.TTS");
            var sortBy = "RecordDate ASC, RecordTime ASC, QuestionID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                if ("PTG.OK.OKS".Equals(rowVs["QuestionID"])) // Jumlah ml
                {
                    var rowVs2 = rowsSelecteds[rowNo + 1]; // Next row seharusnya berisi Question PTG.OK.TTS

                    dtb.Rows[0][colNo] = rowVs["QuestionAnswerNum"].ToInt().ToString();
                    if (rowVs["RecordDate"].Equals(rowVs2["RecordDate"]) && rowVs["RecordTime"].Equals(rowVs2["RecordTime"]))
                        dtb.Rows[1][colNo] = rowVs2["QuestionAnswerNum"].ToInt().ToString();

                    colNo++;
                    if (colNo >= colCount)
                        break;
                }
                rowNo++;
            }

            // Create graph
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp;Oxytocin U/L&nbsp;</td></tr>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp;drops/min&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");


            for (int i = 0; i < 2; i++)
            {
                sb.AppendLine("<tr>");
                for (int j = 0; j < colCount; j++)
                {
                    var val = dtb.Rows[i][j];
                    if (val.Equals("0"))
                        val = string.Empty;

                    sb.AppendFormat("<td>{0}</td>", val);

                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        protected string DrugAndFluidGraph(DataTable dtbPhrLine, int startCol)
        {
            var colCount = ColumnCount;

            // Datasource
            var dtb = new DataTable();
            for (int j = 0; j < colCount; j++)
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));

            var row = dtb.NewRow();
            dtb.Rows.Add(row);

            //var dtbVs = VitalSignHist(questionFormID, "PTG.OB.OBC");
            var exp = string.Format("QuestionID='{0}'", "PTG.OB.OBC");
            var sortBy = "RecordDate ASC, RecordTime ASC, QuestionID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                dtb.Rows[0][colNo] = rowVs["QuestionAnswerText"];

                colNo++;
                if (colNo >= colCount)
                    break;

                rowNo++;
            }

            // Create graph
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp;&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            sb.AppendLine("<tr>");
            for (int j = 0; j < colCount; j++)
            {
                var val = dtb.Rows[0][j];
                if (val.Equals("0"))
                    val = string.Empty;

                sb.AppendFormat("<td>{0}</td>", val);
            }
            sb.AppendLine("</tr>");

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        protected string TemperatureGraph(DataTable dtbPhrLine, int startCol)
        {
            var colCount = ColumnCount;
            // Datasource
            var dtb = new DataTable();
            for (int j = 0; j < colCount; j++)
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));

            var row = dtb.NewRow();
            dtb.Rows.Add(row); // Time
            row = dtb.NewRow();
            dtb.Rows.Add(row); // Value

            var exp = string.Format("VitalSignID='{0}'", "TEMP");
            var sortBy = "RecordDate ASC, RecordTime ASC, VitalSignID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                dtb.Rows[0][colNo] = rowVs["RecordTime"];
                dtb.Rows[1][colNo] = rowVs["QuestionAnswerNum"].ToDecimal().ToString("N1");

                colNo++;
                if (colNo >= colCount)
                    break;

                rowNo++;
            }

            // Create graph
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp;Temp&nbsp;</td></tr>");
            //sb.AppendLine("             <tr><td> &nbsp;Time&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Value
            sb.AppendLine("<tr>");
            for (int j = 0; j < colCount; j++)
            {
                var val = dtb.Rows[1][j];
                if (val.ToDecimal() == 0)
                    val = string.Empty;

                sb.AppendFormat("<td>{0}</td>", val);
            }
            sb.AppendLine("</tr>");

            //// Time
            //sb.AppendLine("<tr>");
            //for (int j = 0; j < colCount; j++)
            //{
            //    sb.AppendFormat("<td>{0}</td>", dtb.Rows[0][j]);
            //}
            //sb.AppendLine("</tr>");

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        protected string UrineGraph(DataTable dtbPhrLine, int startCol)
        {
            var colCount = ColumnCount;

            // Datasource
            var dtb = new DataTable();
            for (int j = 0; j < colCount; j++)
                dtb.Columns.Add(new DataColumn("C_" + j.ToString(), typeof(string)));

            for (int i = 0; i < 4; i++)
            {
                var row = dtb.NewRow();
                dtb.Rows.Add(row);
            }

            // PTG.U.AST Urine - Aseton
            // PTG.U.PTN Urine - Protein
            // PTG.U.VOL Urine - Volume

            //var dtbVs = VitalSignHist(questionFormID, "PTG.U.AST", "PTG.U.PTN", "PTG.U.VOL");
            var exp = string.Format("QuestionID='{0}' OR QuestionID='{1}' OR QuestionID='{2}'", "PTG.U.AST", "PTG.U.PTN", "PTG.U.VOL");
            var sortBy = "RecordDate ASC, RecordTime ASC, QuestionID ASC";
            var rowsSelecteds = dtbPhrLine.Select(exp, sortBy);

            var rowNo = 0;
            var colNo = startCol;
            foreach (DataRow rowVs in rowsSelecteds)
            {
                if ("PTG.U.AST".Equals(rowVs["QuestionID"])) // Aseton
                {
                    var rowVsProt = rowsSelecteds[rowNo + 1]; // Next row seharusnya berisi Protein
                    var rowVsVol = rowsSelecteds[rowNo + 2]; // Next row seharusnya berisi Volume

                    dtb.Rows[0][colNo] = rowVs["RecordTime"];

                    // Urutan display Protein, Aseton, Volume
                    dtb.Rows[2][colNo] = rowVs["QuestionAnswerNum"].ToInt().ToString(); // Aseton
                    if (rowVs["RecordDate"].Equals(rowVsProt["RecordDate"]) && rowVs["RecordTime"].Equals(rowVsProt["RecordTime"]))
                        dtb.Rows[1][colNo] = rowVsProt["QuestionAnswerNum"].ToInt().ToString(); // Protein

                    if (rowVs["RecordDate"].Equals(rowVsVol["RecordDate"]) && rowVs["RecordTime"].Equals(rowVsVol["RecordTime"]))
                        dtb.Rows[3][colNo] = rowVsVol["QuestionAnswerNum"].ToInt().ToString(); // Volume

                    colNo++;
                    if (colNo >= colCount)
                        break;
                }
                rowNo++;
            }

            // Create graph
            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><td style = \"width: 80px\"> &nbsp;Protein&nbsp;</td></tr>");
            sb.AppendLine("             <tr><td> &nbsp;Acetone&nbsp;</td></tr>");
            sb.AppendLine("             <tr><td> &nbsp;Volume&nbsp;</td></tr>");
            sb.AppendLine("             <tr><td> &nbsp;Time&nbsp;</td></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Value
            for (int i = 1; i < 4; i++)
            {
                sb.AppendLine("<tr>");
                for (int j = 0; j < colCount; j++)
                {

                    var val = dtb.Rows[i][j];
                    if (val.Equals("0"))
                        val = string.Empty;

                    sb.AppendFormat("<td>{0}</td>", val);

                }
                sb.AppendLine("</tr>");
            }

            // Time
            sb.AppendLine("<tr>");
            for (int j = 0; j < colCount; j++)
            {
                sb.AppendFormat("<td>{0}</td>", dtb.Rows[0][j]);
            }
            sb.AppendLine("</tr>");

            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }
        #endregion Populate Chart

        #region Populate Menu Edit/Add
        private string MenuAddEditHtml(DataTable dtbPhrLine, string registrationNo, string questionFormID, int startCol)
        {
            var colCount = ColumnCount;
            var sortBy = "RecordDate ASC, RecordTime ASC";

            var rowsSelecteds = dtbPhrLine.DefaultView.ToTable(true, "TransactionNo", "RecordDate", "RecordTime").Select(string.Empty, sortBy);

            var sb = new StringBuilder();
            sb.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            sb.AppendLine(" <tr>");
            sb.AppendLine("     <td style = \"width: 80px;\">");
            sb.AppendLine("         <table class='tblgraph'>");
            sb.AppendLine("             <tr><th style = \"width: 80px\"> &nbsp;Time&nbsp;</th></tr>");
            sb.AppendLine("         </table>");
            sb.AppendLine("     </td>");
            sb.AppendLine("     <td>");
            sb.AppendLine("         <table class='tblgraph'>");

            // Caption
            if (colCount > 30)
                sb.AppendLine("<tr style=\"font-size:90%\">");
            else
                sb.AppendLine("<tr>");

            var isNewMenuCreated = false;
            var rowFoundCount = rowsSelecteds.Length;
            for (int j = 0; j < colCount; j++)
            {
                if (j < startCol)
                {
                    sb.AppendLine("<th></th>");
                    continue;
                }

                var time = string.Empty;
                var txNo = string.Empty;
                if (j < rowFoundCount + startCol)
                {
                    time = rowsSelecteds[j - startCol]["RecordTime"].ToString();
                    txNo = rowsSelecteds[j - startCol]["TransactionNo"].ToString();
                }

                sb.AppendLine("<th>");
                if (string.IsNullOrWhiteSpace(txNo))
                {
                    if (!isNewMenuCreated)
                    {
                        var newImg = string.Format("<img src='{0}/Images/Toolbar/new16.png'/>", Helper.UrlRoot());
                        sb.AppendFormat("<a style=\"cursor:pointer;\" onclick=\"entryQuestionRespond('{0}', '{1}', '{2}', '{3}', '{4}')\">{5}</a>", "new", txNo, registrationNo, questionFormID, "", newImg);
                        isNewMenuCreated = true;
                    }
                    else
                        sb.AppendLine("&nbsp;");
                }
                else
                    sb.AppendFormat("<a style=\"cursor:pointer;\" onclick=\"entryQuestionRespond('{0}', '{1}', '{2}', '{3}', '{4}')\">{5}</a>", "view", txNo, registrationNo, questionFormID, "", time);

                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            sb.AppendLine("</tr>");
            sb.AppendLine("</table>");

            sb.AppendLine("     </td>");
            sb.AppendLine(" </tr>");
            sb.AppendLine("</table>");
            return sb.ToString();
        }

        #endregion
    }

}
