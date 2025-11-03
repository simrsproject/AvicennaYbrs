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

namespace Temiang.Avicenna.Module.RADT.Cpoe.Common
{
    public partial class GrowthChart : BasePageDialog
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

        private DateTime DateOfBirth
        {
            set { ViewState["dob"] = value; }
            get { return Convert.ToDateTime(ViewState["dob"]); }
        }
        private string Gender
        {
            set { ViewState["gdr"] = value; }
            get { return Convert.ToString(ViewState["gdr"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";

            var pat = new Patient();
            if (pat.LoadByPrimaryKey(PatientID))
            {
                this.Title = "Growth Chart : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
            }

            if (!IsPostBack)
            {
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

                DateOfBirth = pat.DateOfBirth ?? DateTime.Today;
                Gender = pat.Sex == "M" ? "1" : "2";
                PopulateVitalSignChartControl();
            }
        }

        private void PopulateVitalSignChartControl()
        {
            var growthChartCtl = "~/Module/RADT/Cpoe/Common/GrowthChartCtl.ascx";
            // Height
            var heightChart = (GrowthChartCtl)LoadControl(growthChartCtl);
            pnlChart.Controls.Add(heightChart);
            heightChart.PopulateChart(PatientID, Gender, DateOfBirth,"L", "HA");

            // Head Circum
            var hcChart = (GrowthChartCtl)LoadControl(growthChartCtl);
            pnlChart.Controls.Add(hcChart);
            hcChart.PopulateChart(PatientID, Gender, DateOfBirth,"L", "CA");

            // Weight
            var weightChart = (GrowthChartCtl)LoadControl(growthChartCtl);
            pnlChart.Controls.Add(weightChart);
            weightChart.PopulateChart(PatientID, Gender, DateOfBirth,"Z", "WA"); //Use Z-Score growth curves

            // BMI
            var bmiChart = (GrowthChartCtl)LoadControl(growthChartCtl);
            pnlChart.Controls.Add(bmiChart);
            bmiChart.PopulateChart(PatientID, Gender, DateOfBirth,"L", "BA");

            // Weight To Length
            var statureChart = (GrowthChartCtl)LoadControl(growthChartCtl);
            pnlChart.Controls.Add(statureChart);
            statureChart.PopulateChart(PatientID, Gender, DateOfBirth,"L", "WL");
        }



        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            PopulateVitalSignChartControl();
        }

    }

}
