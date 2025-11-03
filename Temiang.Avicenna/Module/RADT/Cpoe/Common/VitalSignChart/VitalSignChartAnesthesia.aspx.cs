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
    public partial class VitalSignChartAnesthesia : BasePageDialog
    {
        private List<VitalSignItemValue> _vitalSignItemValues = new List<VitalSignItemValue>();

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
        private string BookingNo
        {
            get { return Request.QueryString["bookingno"]; }
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

        private DateTime DateOfBirth
        {
            set { ViewState["dob"] = value; }
            get { return Convert.ToDateTime(ViewState["dob"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";

            var pat = new Patient();
            if (pat.LoadByPrimaryKey(PatientID))
            {
                this.Title = "Monitoring Intra-Operative Anesthesia of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
            }

            if (!IsPostBack)
            {
                var suBook = new ServiceUnitBooking();
                suBook.LoadByPrimaryKey(BookingNo);
                txtFromDate.SelectedDate = suBook.RealizationDateTimeFrom ?? DateTime.Today;

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

                txtFromDate.SelectedDate = LastVitalSignDate;
                DateOfBirth = pat.DateOfBirth ?? DateTime.Today;
                PopulateVitalSignChartControl();
            }
        }

        private void PopulateVitalSignChartControl()
        {

            int ageInDay = (txtFromDate.SelectedDate.Value - DateOfBirth).Days;

            var vsColl = new AppStandardReferenceItemCollection();
            vsColl.Query.Where(vsColl.Query.StandardReferenceID == "VitalSignAnesthesia");
            vsColl.Query.OrderBy(vsColl.Query.ItemName.Ascending);
            vsColl.LoadAll();


            foreach (var item in vsColl)
            {
                var vitalSignID = string.Empty;
                var parentVitalSignID = string.Empty;

                // Cek apakah merupakan grup VitalSign
                var vsQr = new VitalSignQuery("v");
                vsQr.Where(vsQr.ParentVitalSignID == item.ItemID);
                vsQr.es.Top = 1;

                var vs = new VitalSign();
                vitalSignID = vs.Load(vsQr) ? vs.VitalSignID : item.ItemID;

                var ctl = (VitalSignChartCtl)LoadControl("~/Module/RADT/Cpoe/Common/VitalSignChart/VitalSignChartCtl.ascx");
                pnlChart.Controls.Add(ctl);
                var results = ctl.PopulateChart(PatientID, DateOfBirth, vitalSignID, txtFromDate.SelectedDate.Value, txtFromDate.SelectedDate.Value,false, true);

                foreach (var result in results)
                {
                    // Insert ke _vitalSignItemValues
                    var isFound = false;
                    foreach (var value in _vitalSignItemValues)
                    {
                        if (value.No.Equals(result.No))
                        {
                            isFound = true;
                            value.TotalScore = value.TotalScore + result.Level;  // Total Level
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
                            _vitalSignItemValues.Add(new VitalSignItemValue()
                            {
                                No = result.No,
                                TransactionNo = result.TransactionNo,
                                Time = result.Time,
                                VitalSignID = result.VitalSignID,
                                TotalScore = result.Level,
                                IsExistLevel3 = isExistLevel3
                            });
                        }
                        else
                            _vitalSignItemValues.Add(new VitalSignItemValue()
                            {
                                No = result.No
                            });
                    }

                }

                // Hitung Nilai EWS
                foreach (var value in _vitalSignItemValues)
                {
                    if (value.TotalScore < 5)
                        value.Level = value.IsExistLevel3 ? 2 : 1;
                    else if (value.TotalScore < 7)
                        value.Level = 2; // Yellow
                    else
                        value.Level = 3; // Red

                }
            }
        }



        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            PopulateVitalSignChartControl();
        }

        protected void btnStartFromRegistration_Click(object sender, EventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(!string.IsNullOrEmpty(FromRegistrationNo) ? FromRegistrationNo : RegistrationNo);
            txtFromDate.SelectedDate = reg.RegistrationDate;
            PopulateVitalSignChartControl();
        }

        protected void btnLastVitalSign_Click(object sender, EventArgs e)
        {
            txtFromDate.SelectedDate = LastVitalSignDate;
            PopulateVitalSignChartControl();
        }


        protected string EwsTotalScoreLevelHtml()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<table id='ewsscore' width='100%'>");

            // Caption
            sb.AppendLine("<tr>");
            sb.AppendLine("<th style='width:50px;'></th>");
            foreach (VitalSignItemValue value in _vitalSignItemValues)
            {
                var time = value.Time.ToString("HH:mm");
                sb.AppendLine("<th>");
                sb.AppendLine(time == "00:00" ? "&nbsp;" : time);
                sb.AppendLine("</th>");
            }
            sb.AppendLine("</tr>");

            // Score
            sb.AppendLine("<tr>");
            sb.AppendLine("<td>Score</td>");
            foreach (VitalSignItemValue value in _vitalSignItemValues)
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

            return sb.ToString();
        }

    }

}
