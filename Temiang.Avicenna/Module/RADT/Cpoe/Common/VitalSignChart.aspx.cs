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
using Telerik.Charting;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe
{
    public partial class VitalSignChart : BasePageDialog
    {

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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["patid"]))
                    return Request.QueryString["patid"];

                if (!string.IsNullOrEmpty(RegistrationNo) )
                    return CurrentRegistration.PatientID;
                else
                    return string.Empty;
                    
            }
        }
        private string FromRegistrationNo
        {
            get { return Request.QueryString["fregno"]; }
        }
        private string QuestionID
        {
            get { return Request.QueryString["qid"]; }
        }
        private string VitalSignID
        {
            get { return Request.QueryString["vid"]; }
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
                this.Title = "Vital Signs Chart : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
            }

            if (!IsPostBack)
            {
                //var reg = new Registration();
                //reg.LoadByPrimaryKey(!string.IsNullOrEmpty(FromRegistrationNo) ? FromRegistrationNo : RegistrationNo);
                txtFromDate.SelectedDate = CurrentRegistration.RegistrationDate;
                txtToDate.SelectedDate = DateTime.Today;
                DateOfBirth = pat.DateOfBirth ?? DateTime.Today;
                PopulateChart();
            }
        }

        private void PopulateChart()
        {
            vitalSignChartCtl.ClearChart();

            // Get VitalSignID
            string vitalSignID;
            if (string.IsNullOrEmpty(VitalSignID))
            {
                var quest = new Question();
                quest.LoadByPrimaryKey(QuestionID);
                vitalSignID = quest.VitalSignID;
            }
            else
            {
                vitalSignID = VitalSignID;
            }

            var vitalSign = new VitalSign();
            if (vitalSign.LoadByPrimaryKey(vitalSignID))
                vitalSignChartCtl.PopulateChart(PatientID, DateOfBirth, vitalSignID, txtFromDate.SelectedDate.Value, txtToDate.SelectedDate.Value);
        }


        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            PopulateChart();
        }
    }

}
