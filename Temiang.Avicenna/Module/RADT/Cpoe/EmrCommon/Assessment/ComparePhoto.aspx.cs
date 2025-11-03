using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class ComparePhoto : BasePageDialog
    {
        public string RegistrationNo
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public string RegistrationInfoMedicID
        {
            get
            {
                return Request.QueryString["rimid"];
            }
        }

        private string AssessmentType
        {
            get
            {
                return Request.QueryString["astp"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonOk.Visible = false;
            ButtonCancel.Text = "Close";

            // Current
            var assessmentDateTime = DateTime.Now;
            var asses = new PatientAssessment();
            if (asses.LoadByPrimaryKey(RegistrationInfoMedicID))
            {
                imgPhoto.DataValue = asses.Photo;
                assessmentDateTime = asses.AssessmentDateTime?? DateTime.Now;
            }

            

            // Prev
            asses = new PatientAssessment();

            var reg = new Registration();
            reg.LoadByPrimaryKey(RegistrationNo);
            var patientID = reg.PatientID;
           

            var qr = new PatientAssessmentQuery();
            qr.es.Top = 1;
            qr.Where(qr.PatientID == reg.PatientID, qr.SRAssessmentType == AssessmentType, qr.AssessmentDateTime < assessmentDateTime);
            qr.OrderBy(qr.AssessmentDateTime.Descending);
            if (asses.Load(qr))
                imgPhotoPrev.DataValue = asses.Photo;

        }
    }
}