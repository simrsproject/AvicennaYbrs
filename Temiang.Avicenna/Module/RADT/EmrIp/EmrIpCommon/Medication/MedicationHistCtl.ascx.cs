using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.EmrIp.EmrIpCommon.Medication
{
    public partial class MedicationHistCtl : System.Web.UI.UserControl
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }
        public string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        public string GridClientID
        {
            get { return grdMedicationHist.ClientID; }
        }
        public bool IsConsumeMethodChangeAble { get; set; }
        public void SetDataSource(DataTable dtb, DateTime? fromDate)
        {
            // Tambah 10 kolom + 10 sub kolom untuk history
            // Hanya ambil sampai konsumsi ke 5 di tgl yg sama (disesuaikan dg cetakan form Daftar Obat) 
            for (int i = 1; i < 11; i++)
            {
                dtb.Columns.Add(string.Format("ScheduleDate_{0:00}", i), typeof(string));

                for (int j = 1; j < 6; j++)
                {
                    dtb.Columns.Add(string.Format("ScheduleHour_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("HoHour_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("RealHour_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("Balance_{0:00}_{1}", i, j), typeof(decimal));
                    dtb.Columns.Add(string.Format("SetupBy_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("HoBy_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("HoTo_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("VerifBy_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("RealBy_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("ServiceUnit_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("Consumed_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("Note_{0:00}_{1}", i, j), typeof(string));
                    dtb.Columns.Add(string.Format("Sign_{0:00}_{1}", i, j), typeof(string));
                }
            }

            // Ambil tanggal2 konsumsi
            var dtbSchedule = ScheduleDateDataTable(fromDate);

            // Add raspro info
            dtb.Columns.Add("RasproSeqNo", typeof(int));
            dtb.Columns.Add("SRRaspro", typeof(string));

            // Update History Realisasi
            foreach (DataRow row in dtb.Rows)
            {
                var tp = new TransPrescription();
                if (tp.LoadByPrimaryKey(row["RefTransactionNo"].ToString()) && tp.RasproSeqNo.ToInt() > 0)
                {
                    row["RasproSeqNo"] = tp.RasproSeqNo;
                    var rr = new RegistrationRaspro();
                    rr.LoadByPrimaryKey(RegistrationNo, tp.RasproSeqNo ?? 0);
                    row["SRRaspro"] = rr.SRRaspro;
                }
                else
                {
                    row["RasproSeqNo"] = 0;
                    row["SRRaspro"] = string.Empty;
                }

                // Ganti break line
                row["ItemDescription"] = row["ItemDescription"].ToString().Replace(Environment.NewLine, "<br>");

                var qr = new MedicationReceiveUsedQuery("a");
                qr.Where(qr.MedicationReceiveNo == row["MedicationReceiveNo"], qr.ScheduleDateTime.IsNotNull(), qr.ScheduleDateTime >= fromDate);
                qr.OrderBy(qr.SequenceNo.Ascending);
                var medUseds = new MedicationReceiveUsedCollection();
                medUseds.Load(qr);

                var balance = row["ReceiveQty"].ToDecimal();
                var scheduleDate = DateTime.Today.AddDays(10);
                var rowNo = 1;
                var colNo = 1;
                foreach (var used in medUseds)
                {
                    if (colNo > 5) // Hanya ambil sampai konsumsi ke 5 di tgl yg sama (disesuaikan dg cetakan form Daftar Obat)
                        continue;

                    if (!scheduleDate.Equals(Convert.ToDateTime(used.ScheduleDateTime).Date))
                    {
                        colNo = 1;
                        scheduleDate = Convert.ToDateTime(used.ScheduleDateTime).Date;
                        var rowFound = dtbSchedule.Rows.Find(scheduleDate);
                        rowNo = rowFound["RowNo"].ToInt();

                        if (rowNo > 10) // Hanya ambil sampai konsumsihari ke 10 (disesuaikan dg cetakan form Daftar Obat)
                            break;

                        row[string.Format("ScheduleDate_{0:00}", rowNo)] = scheduleDate.ToString(AppConstant.DisplayFormat.Date);
                    }

                    var scheduleDateTime = Convert.ToDateTime(used.ScheduleDateTime);
                    row[string.Format("ScheduleHour_{0:00}_{1}", rowNo, colNo)] = string.Format("{0:00}:{1:00}", scheduleDateTime.Hour,
                        scheduleDateTime.Minute);

                    if (used.HandoversDateTime != null)
                    {
                        var handoversDateTime = Convert.ToDateTime(used.HandoversDateTime);
                        row[string.Format("HoHour_{0:00}_{1}", rowNo, colNo)] = string.Format("{0:00}:{1:00}", handoversDateTime.Hour,
                            handoversDateTime.Minute);
                    }

                    if (used.RealizedDateTime != null)
                    {
                        var realizedDateTime = Convert.ToDateTime(used.RealizedDateTime);
                        row[string.Format("RealHour_{0:00}_{1}", rowNo, colNo)] = string.Format("{0:00}:{1:00}", realizedDateTime.Hour,
                            realizedDateTime.Minute);
                    }

                    if (used.RealizedDateTime != null)
                        balance = balance - used.Qty ?? 0;

                    row[string.Format("Balance_{0:00}_{1}", rowNo, colNo)] = balance;
                    row[string.Format("SetupBy_{0:00}_{1}", rowNo, colNo)] = used.SetupByUserID;
                    row[string.Format("HoBy_{0:00}_{1}", rowNo, colNo)] = used.HandoversByUserID;
                    row[string.Format("HoTo_{0:00}_{1}", rowNo, colNo)] = used.HandoversToUserID;
                    row[string.Format("VerifBy_{0:00}_{1}", rowNo, colNo)] = used.VerificationByUserID;
                    row[string.Format("RealBy_{0:00}_{1}", rowNo, colNo)] = used.RealizedByUserID;
                    row[string.Format("Consumed_{0:00}_{1}", rowNo, colNo)] = used.RealizedDateTime == null ? string.Empty : (used.IsNotConsume == null || used.IsNotConsume == false) ? "V" : "X";

                    if (used.PatientSignID > 0)
                    {
                        //row[string.Format("Sign_{0:00}_{1}", rowNo, colNo)] = string.Format(
                        //    "<a style=\"mouse:pointer;\" onclick=\"openMedStatPatSign('{0}')\"><img style=\"max-width: 100%;max-height: 100%;\" src=\"{0}\\Handler\\SignPoolHandler.ashx?signid={1}\" alt=\"\" /></a>",
                        //    Helper.UrlRoot(), used.PatientSignID);

                        row[string.Format("Sign_{0:00}_{1}", rowNo, colNo)] = string.Format(
                            "<a style=\"cursor: pointer\" onclick=\"openMedStatPatSign('{1}')\"><img style=\"max-width: 100%;max-height: 100%;\" src=\"{0}\\Images\\checklist16.png\" alt=\"\" /></a>",
                            Helper.UrlRoot(), used.PatientSignID);
                    }
                    else
                        row[string.Format("Sign_{0:00}_{1}", rowNo, colNo)] = "&nbsp;";

                    // Note MedicationHistCtl
                    var note = used.Note;
                    if (!string.IsNullOrEmpty(used.SRMedicationReason))
                    {
                        var medReason = StandardReference.GetItemName(AppEnum.StandardReference.MedicationReason, used.SRMedicationReason);
                        note = string.Format("{0}. {1}", medReason, used.Note);
                    }
                    row[string.Format("Note_{0:00}_{1}", rowNo, colNo)] = note;

                    // Status ServiceUnit
                    if (used.RealizedDateTime != null)
                    {
                        foreach (PatientTransferHistory hist in TransferHistory)
                        {
                            if (hist.DateOfEntry <= used.RealizedDateTime && String.Compare(hist.TimeOfEntry,
                                    string.Format("{0:00}:{1:00}", used.RealizedDateTime.Value.Hour,
                                        used.RealizedDateTime.Value.Minute)) < 0)
                            {
                                var suName = ServiceUnit.GetServiceUnitName(hist.ServiceUnitID);
                                row[string.Format("ServiceUnit_{0:00}_{1}", rowNo, colNo)] = suName;
                                break;
                            }
                        }


                    }

                    colNo = colNo + 1;
                }

            }

            grdMedicationHist.DataSource = dtb;
            grdMedicationHist.Rebind();
        }

        private PatientTransferHistoryCollection _transferHistory;
        private PatientTransferHistoryCollection TransferHistory
        {
            get
            {
                if (_transferHistory == null)
                {
                    var trfQr = new PatientTransferHistoryQuery();
                    trfQr.Where(trfQr.RegistrationNo == RegistrationNo);
                    trfQr.OrderBy(trfQr.TimeOfEntry.Ascending);
                    _transferHistory = new PatientTransferHistoryCollection();
                    _transferHistory.Load(trfQr);
                }
                return _transferHistory;
            }

        }
        private DataTable ScheduleDateDataTable(DateTime? fromDate)
        {
            var mrQr = new MedicationReceiveQuery("a");
            var usedQr = new MedicationReceiveUsedQuery("mru");
            mrQr.InnerJoin(usedQr).On(mrQr.MedicationReceiveNo == usedQr.MedicationReceiveNo);
            //mrQr.Where(mrQr.IsVoid != true,
            //    mrQr.Or(mrQr.RegistrationNo == RegistrationNo, mrQr.RegistrationNo == FromRegistrationNo), usedQr.ScheduleDateTime.IsNotNull(), usedQr.ScheduleDateTime >= fromDate);

            var mergeRegistrations = AppCache.RelatedRegistrations(IsPostBack, RegistrationNo);
            mrQr.Where(mrQr.Or(mrQr.IsVoid == false, mrQr.IsVoid.IsNull()), mrQr.RegistrationNo.In(mergeRegistrations), usedQr.ScheduleDateTime.IsNotNull(), usedQr.ScheduleDateTime >= fromDate);

            mrQr.Select("<DATEADD(dd, DATEDIFF(dd, 0, mru.ScheduleDateTime), 0) as ScheduleDate>");

            mrQr.OrderBy("<1 DESC>");
            mrQr.es.Distinct = true;

            var dtb = mrQr.LoadDataTable();
            dtb.Columns.Add("RowNo", typeof(Int16));
            int rowNo = 1;
            foreach (DataRow row in dtb.Rows)
            {
                row["RowNo"] = rowNo;
                rowNo++;
            }

            dtb.PrimaryKey = new DataColumn[] { dtb.Columns["ScheduleDate"] };
            return dtb;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected object MedicationRealizationHtml(GridItem container, int dayNo)
        {
            if (DataBinder.Eval(container.DataItem, string.Format("ScheduleDate_{0:00}", dayNo)) == DBNull.Value)
                return @"
<table id='medused'>
    <tr>
        <th colspan='5'>&nbsp;</th>
    </tr>
    <tr>
        <td style='width:20px'>&nbsp;</td>
        <td style = 'width: 20px'></td>
        <td style = 'width: 20px'></td>
        <td style = 'width: 20px'></td>
        <td style = 'width: 20px'></td>
     </tr>
     <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        </tr>
     <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
     <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
        </tr>
     <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table> ";

            var htmlScript = string.Format(@"
<table id='medused'>
    <tr>
        <th colspan='5'>{0}</th>
    </tr>
    <tr>
        <td style='width:20px'>&nbsp;{1}</td>
        <td style = 'width: 20px'>{2}</td>
        <td style = 'width: 20px'>{3}</td>
        <td style = 'width: 20px'>{4}</td>
        <td style = 'width: 20px'>{5}</td>
     </tr>

    <tr>
        <td>&nbsp;{6}</td>
        <td>{7}</td>
        <td>{8}</td>
        <td>{9}</td>
        <td>{10}</td>
    </tr>
    <tr>
        <td>&nbsp;{11}</td>
        <td>{12}</td>
        <td>{13}</td>
        <td>{14}</td>
        <td>{15}</td>
    </tr>
    <tr>
        <td>&nbsp;{16}</td>
        <td>{17}</td>
        <td>{18}</td>
        <td>{19}</td>
        <td>{20}</td>
    </tr>
    <tr>
        <td>&nbsp;{21}</td>
        <td>{22}</td>
        <td>{23}</td>
        <td>{24}</td>
        <td>{25}</td>
    </tr>
     <tr>
        <td>&nbsp;{26}</td>
        <td>{27}</td>
        <td>{28}</td>
        <td>{29}</td>
        <td>{30}</td>
    </tr>
    <tr>
        <td>&nbsp;{31}</td>
        <td>{32}</td>
        <td>{33}</td>
        <td>{34}</td>
        <td>{35}</td>
    </tr>
    <tr>
        <td>&nbsp;{36}</td>
        <td>{37}</td>
        <td>{38}</td>
        <td>{39}</td>
        <td>{40}</td>
    </tr>
    <tr>
        <td>&nbsp;{41}</td>
        <td>{42}</td>
        <td>{43}</td>
        <td>{44}</td>
        <td>{45}</td>
    </tr>
    <tr>
        <td>{46}</td>
        <td>{47}</td>
        <td>{48}</td>
        <td>{49}</td>
        <td>{50}</td>
    </tr>
    <tr>
        <td>{51}</td>
        <td>{52}</td>
        <td>{53}</td>
        <td>{54}</td>
        <td>{55}</td>
    </tr>
    <tr>
        <td>{56}</td>
        <td>{57}</td>
        <td>{58}</td>
        <td>{59}</td>
        <td>{60}</td>
    </tr>
<tr>
        <td>{61}</td>
        <td>{62}</td>
        <td>{63}</td>
        <td>{64}</td>
        <td>{65}</td>
    </tr>
                </table> ", DataBinder.Eval(container.DataItem, string.Format("ScheduleDate_{0:00}", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("ScheduleHour_{0:00}_1", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("ScheduleHour_{0:00}_2", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("ScheduleHour_{0:00}_3", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("ScheduleHour_{0:00}_4", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("ScheduleHour_{0:00}_5", dayNo)),

                DataBinder.Eval(container.DataItem, string.Format("HoHour_{0:00}_1", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("HoHour_{0:00}_2", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("HoHour_{0:00}_3", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("HoHour_{0:00}_4", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("HoHour_{0:00}_5", dayNo)),

                DataBinder.Eval(container.DataItem, string.Format("RealHour_{0:00}_1", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("RealHour_{0:00}_2", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("RealHour_{0:00}_3", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("RealHour_{0:00}_4", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("RealHour_{0:00}_5", dayNo)),

                DataBinder.Eval(container.DataItem, string.Format("Balance_{0:00}_1", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Balance_{0:00}_2", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Balance_{0:00}_3", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Balance_{0:00}_4", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Balance_{0:00}_5", dayNo)),

                TextWithToolTip(container, string.Format("SetupBy_{0:00}_1", dayNo)),
                TextWithToolTip(container, string.Format("SetupBy_{0:00}_2", dayNo)),
                TextWithToolTip(container, string.Format("SetupBy_{0:00}_3", dayNo)),
                TextWithToolTip(container, string.Format("SetupBy_{0:00}_4", dayNo)),
                TextWithToolTip(container, string.Format("SetupBy_{0:00}_5", dayNo)),

                TextWithToolTip(container, string.Format("HoBy_{0:00}_1", dayNo)),
                TextWithToolTip(container, string.Format("HoBy_{0:00}_2", dayNo)),
                TextWithToolTip(container, string.Format("HoBy_{0:00}_3", dayNo)),
                TextWithToolTip(container, string.Format("HoBy_{0:00}_4", dayNo)),
                TextWithToolTip(container, string.Format("HoBy_{0:00}_5", dayNo)),

                TextWithToolTip(container, string.Format("HoTo_{0:00}_1", dayNo)),
                TextWithToolTip(container, string.Format("HoTo_{0:00}_2", dayNo)),
                TextWithToolTip(container, string.Format("HoTo_{0:00}_3", dayNo)),
                TextWithToolTip(container, string.Format("HoTo_{0:00}_4", dayNo)),
                TextWithToolTip(container, string.Format("HoTo_{0:00}_5", dayNo)),

                TextWithToolTip(container, string.Format("VerifBy_{0:00}_1", dayNo)),
                TextWithToolTip(container, string.Format("VerifBy_{0:00}_2", dayNo)),
                TextWithToolTip(container, string.Format("VerifBy_{0:00}_3", dayNo)),
                TextWithToolTip(container, string.Format("VerifBy_{0:00}_4", dayNo)),
                TextWithToolTip(container, string.Format("VerifBy_{0:00}_5", dayNo)),

                TextWithToolTip(container, string.Format("RealBy_{0:00}_1", dayNo)),
                TextWithToolTip(container, string.Format("RealBy_{0:00}_2", dayNo)),
                TextWithToolTip(container, string.Format("RealBy_{0:00}_3", dayNo)),
                TextWithToolTip(container, string.Format("RealBy_{0:00}_4", dayNo)),
                TextWithToolTip(container, string.Format("RealBy_{0:00}_5", dayNo)),

                ConsumedIcon(DataBinder.Eval(container.DataItem, string.Format("Consumed_{0:00}_1", dayNo))),
                ConsumedIcon(DataBinder.Eval(container.DataItem, string.Format("Consumed_{0:00}_2", dayNo))),
                ConsumedIcon(DataBinder.Eval(container.DataItem, string.Format("Consumed_{0:00}_3", dayNo))),
                ConsumedIcon(DataBinder.Eval(container.DataItem, string.Format("Consumed_{0:00}_4", dayNo))),
                ConsumedIcon(DataBinder.Eval(container.DataItem, string.Format("Consumed_{0:00}_5", dayNo))),

                TextWithToolTip(container, string.Format("ServiceUnit_{0:00}_1", dayNo)),
                TextWithToolTip(container, string.Format("ServiceUnit_{0:00}_2", dayNo)),
                TextWithToolTip(container, string.Format("ServiceUnit_{0:00}_3", dayNo)),
                TextWithToolTip(container, string.Format("ServiceUnit_{0:00}_4", dayNo)),
                TextWithToolTip(container, string.Format("ServiceUnit_{0:00}_5", dayNo)),

                TextWithToolTip(container, string.Format("Note_{0:00}_1", dayNo)),
                TextWithToolTip(container, string.Format("Note_{0:00}_2", dayNo)),
                TextWithToolTip(container, string.Format("Note_{0:00}_3", dayNo)),
                TextWithToolTip(container, string.Format("Note_{0:00}_4", dayNo)),
                TextWithToolTip(container, string.Format("Note_{0:00}_5", dayNo)),

                DataBinder.Eval(container.DataItem, string.Format("Sign_{0:00}_1", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Sign_{0:00}_2", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Sign_{0:00}_3", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Sign_{0:00}_4", dayNo)),
                DataBinder.Eval(container.DataItem, string.Format("Sign_{0:00}_5", dayNo))
            );

            return htmlScript;
        }

        private static string ConsumedIcon(object status)
        {
            if ("V".Equals(status))
                return string.Format("<img src=\"{0}/Images/Toolbar/post_green_16.png\" />", Helper.UrlRoot());
            else if ("X".Equals(status))
                return string.Format("<img src=\"{0}/Images/Toolbar/cancel16.png\" />", Helper.UrlRoot());

            return "&nbsp;";
        }
        private static string TextWithToolTip(GridItem container, string fieldName)
        {
            var text = DataBinder.Eval(container.DataItem, fieldName).ToString();
            if (string.IsNullOrEmpty(text))
                return "&nbsp;";

            if (text.Length > 4)
                return string.Format(@"<div class='tooltip'>{0}
                <span class='tooltiptext'>{1}</span>
                </div>", text.Substring(0, 4), text);
            else
                return text;
        }

        protected string MedicationChangeConsumeMethodHtml(object medNo, object conmtd, object patientID, object balanceQty, object isAntibiotic, object isVoid, object isContinue)
        {
            if (!IsConsumeMethodChangeAble) return string.Empty;
            if (AppSession.UserLogin.SRUserType != BusinessObject.Common.UserLogin.UserType.Doctor) return String.Empty;
            if (balanceQty.ToDecimal() == 0) return string.Empty;
            if (true.Equals(isAntibiotic)) return string.Empty;
            if (true.Equals(isVoid)) return string.Empty;
            if (false.Equals(isContinue)) return string.Empty;

            return string.Format(
                "<a  href=\"#\" onclick=\"javascript:entryMedicationChangeConsumeMethod('{0}','{1}','{2}'); return false;\"><img style='vertical-align: text-bottom;' src=\"{3}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",
                medNo, conmtd, patientID, Helper.UrlRoot());
        }

        protected string MedicationStopContinueHtml(object medNo, object isVoid, object isContinue, object patientID)
        {
            //if (AppSession.UserLogin.SRUserType != BusinessObject.Common.UserLogin.UserType.Doctor) return String.Empty;

            if (true.Equals(isVoid)) return String.Empty;

            if (Request.QueryString["mod"] == "view") // tampilan mode view (Fajri - 2023/11/06)
                return string.Format("<img src=\"{0}/Images/Toolbar/unlock16_d.png\"  alt=\"Stop\" Title=\"Click to stop this medication\" /></a>", Helper.UrlRoot());

            if (isContinue.Equals(true)) // show menu stop
                return string.Format("<a href=\"#\" onclick=\"javascript:entryMedicationStatusConfirm('{1}','stop'); return false;\"><img src=\"{0}/Images/Toolbar/unlock16.png\"  alt=\"Stop\" Title=\"Click to stop this medication\" /></a>", Helper.UrlRoot(), medNo);
            else
            {
                // Show info last stopped
                var qr = new MedicationReceiveStatusQuery();
                qr.OrderBy(qr.StatusDateTime.Descending);
                qr.es.Top = 1;
                qr.Where(qr.MedicationReceiveNo == medNo.ToInt(), qr.SRMedicationStatusType == "STOP");
                var medStat = new MedicationReceiveStatus();
                if (medStat.Load(qr) && !string.IsNullOrWhiteSpace(medStat.LastUpdateByUserID))
                {
                    return string.Format("[Stop by: {2} ({3})]<br /><a href=\"#\" onclick=\"javascript:entryMedicationStatusConfirm('{1}','continue'); return false;\"><img src=\"{0}/Images/Toolbar/lock16.png\"  alt=\"Continue\" Title=\"Click to continue this medication\"/></a>", Helper.UrlRoot(), medNo
                        , AppUser.GetUserName(medStat.LastUpdateByUserID), medStat.LastUpdateDateTime.Value.ToString(AppConstant.DisplayFormat.DateHourMinute));
                }
                else
                    return string.Format("<a href=\"#\" onclick=\"javascript:entryMedicationStatusConfirm('{1}','continue'); return false;\"><img src=\"{0}/Images/Toolbar/lock16.png\"  alt=\"Continue\" Title=\"Click to continue this medication\"/></a>", Helper.UrlRoot(), medNo);
            }
        }
        protected string MedicationVoidHtml(object medNo, object isVoid, object patientID)
        {
            if (AppSession.UserLogin.SRUserType != BusinessObject.Common.UserLogin.UserType.Doctor) return String.Empty;

            if (true.Equals(isVoid))
            {
                // Show info last Void
                var qr = new MedicationReceiveStatusQuery();
                qr.OrderBy(qr.StatusDateTime.Descending);
                qr.es.Top = 1;
                qr.Where(qr.MedicationReceiveNo == medNo.ToInt(), qr.SRMedicationStatusType == "VOID");
                var medStat = new MedicationReceiveStatus();
                if (medStat.Load(qr) && !string.IsNullOrWhiteSpace(medStat.LastUpdateByUserID))
                {
                    return string.Format("[Void by: {0} ({1})]", AppUser.GetUserName(medStat.LastUpdateByUserID), medStat.LastUpdateDateTime.Value.ToString(AppConstant.DisplayFormat.DateHourMinute));
                }
                return "[Void]";
            }

            // tampilan mode view (Fajri - 2023/11/06)
            var url = string.Empty;
            if (Request.QueryString["mod"] == "view")
                url = string.Format("<img src=\"{0}/Images/Toolbar/row_delete16_d.png\" alt=\"Void\" />", Helper.UrlRoot());
            else
                url = string.Format("<a href=\"#\" onclick=\"javascript:entryMedicationStatusConfirm('{1}','void'); return false;\"><img src=\"{0}/Images/Toolbar/row_delete16.png\"  alt=\"Void\" Title=\"Click to void this medication\"/></a>", Helper.UrlRoot(), medNo);

            return url;
        }
        protected string MedicationEditHtml(object medNo, object patientID)
        {
            //Proteksi bisa edit ada di entriannya

            // tampilan mode view (Fajri - 2023/11/06)
            var url = string.Empty;
            if (Request.QueryString["mod"] == "view")
                url = string.Format("<img src=\"{0}/Images/Toolbar/edit16_d.png\" alt=\"New\" />", Helper.UrlRoot());
            else 
                url = string.Format(
                "<a  href=\"#\" onclick=\"javascript:entryMedicationReceiveEdit('{0}','{1}'); return false;\"><img style='vertical-align: text-bottom;' src=\"{2}/Images/Toolbar/edit16.png\"  alt=\"New\" /></a>",
                medNo, patientID, Helper.UrlRoot());

            return url;
        }

        #region Schedule Setup
        protected string MedicationScheduleSetupHtml(GridItem container)
        {
            int medrecno = Convert.ToInt32(DataBinder.Eval(container.DataItem, "MedicationReceiveNo"));
            var srConsumeMethod = Convert.ToString(DataBinder.Eval(container.DataItem, "SRConsumeMethod"));
            var patientID = Convert.ToString(DataBinder.Eval(container.DataItem, "PatientID"));
            var srMedicationConsume = Convert.ToString(DataBinder.Eval(container.DataItem, "SRMedicationConsume"));
            var consumeQty = Convert.ToDecimal(DataBinder.Eval(container.DataItem, "ConsumeQty"));
            return MedicationScheduleSetupHtml(medrecno, srConsumeMethod, srMedicationConsume, consumeQty);
        }
        private string MedicationScheduleSetupHtml(int medrecno, string srConsumeMethod, string srMedicationConsume, decimal consumeQty)
        {
            var scriptForScheduleDate = DateTime.Today;
            var strb = new StringBuilder();

            var cm = new ConsumeMethod();
            cm.LoadByPrimaryKey(srConsumeMethod);

            var scheduleNo = 0;
            for (int i = 0; i < cm.IterationQty; i++)
            {
                var timeScheduleDisplay = "00:00";
                var timeScheduleDisplays = timeScheduleDisplay.Split(':');
                scheduleNo = i + 1;

                // Check custom schedule
                var qrSchedule = new MedicationScheduleQuery();
                qrSchedule.Where(qrSchedule.MedicationReceiveNo == medrecno, qrSchedule.ScheduleStartDate <= scriptForScheduleDate, qrSchedule.ScheduleNo == scheduleNo);
                qrSchedule.OrderBy(qrSchedule.ScheduleStartDate.Descending);
                qrSchedule.es.Top = 1;

                var histSch = new MedicationSchedule();
                if (histSch.Load(qrSchedule))
                {
                    // Jika ada history setup maka munculkan jam dari history
                    consumeQty = histSch.Qty ?? 0;
                    var scheduleTime = histSch.ScheduleTime.Value;

                    timeScheduleDisplay =
                        string.Format("{0:00}:{1:00}", scheduleTime.Hour, scheduleTime.Minute);
                    timeScheduleDisplays = timeScheduleDisplay.Split(':');
                }
                else
                {
                    // Ambil dari master ConsumeMethod
                    timeScheduleDisplay = cm.GetColumn(string.Format("Time{0:00}", scheduleNo)).ToString();
                    timeScheduleDisplays = timeScheduleDisplay.Split(':');

                    if (srMedicationConsume.Equals("AC"))
                    {
                        // Sebelum makan maka kurangi 30menit
                        timeScheduleDisplays = timeScheduleDisplay.Split(':');
                        var minute = (timeScheduleDisplays[0].ToInt() * 60) + timeScheduleDisplays[1].ToInt();
                        minute = minute - 30;
                        timeScheduleDisplay = string.Format("{0}:{1}",
                            string.Format("{0:00}", Math.Floor((decimal)(minute / 60))),
                            string.Format("{0:00}", minute % 60));
                        timeScheduleDisplays = timeScheduleDisplay.Split(':');
                    }
                }

                if (timeScheduleDisplays.Length < 2) continue;

                // Tampilkan menu entry schedule
                if (Request.QueryString["mod"] == "view") // tampilan mode view (Fajri - 2023/11/06)
                    strb.AppendFormat("<img src=\"{0}/Images/Toolbar/edit16_d.png\" alt=\"New\" />&nbsp;{1}({2})</a>&nbsp;|", Helper.UrlRoot(), timeScheduleDisplay, consumeQty);
                else
                    strb.AppendFormat( "&nbsp;<a  href=\"#\" onclick=\"javascript:entryMedicationScheduleSetup('{0}', '{1}', '{2}','{3}'); return false;\"><img style='vertical-align: text-bottom;' src=\"{4}/Images/Toolbar/edit16.png\"  alt=\"New\" />&nbsp;{3}({5})</a>&nbsp;|", medrecno, scriptForScheduleDate, scheduleNo, timeScheduleDisplay, Helper.UrlRoot(), consumeQty);

            }

            var retval = strb.ToString();
            return !string.IsNullOrEmpty(retval) ? retval.Substring(0, retval.Length - 2) : string.Empty;
        }

        #endregion

    }
}