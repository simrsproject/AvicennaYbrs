using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class GeneralCtl2 : BaseAssessmentCtl
    {

        public override EntryGroupEnum EntryGroup
        {
            get { return EntryGroupEnum.Anamnesis; }
        }

        #region override method

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                // Tampilkan data terakhir di episode patient bersangkutan
                // Jika mode edit akan tertimpa nilainya di event OnPopulateEntryControl
                var reg = new Registration();
                reg.Query.Where(reg.Query.RegistrationNo.In(MergeRegistrations), reg.Query.Complaint != string.Empty);
                reg.Query.OrderBy(reg.Query.RegistrationDateTime.Descending);
                reg.Query.es.Top = 1;
                if (reg.Query.Load())
                {
                    txtAnamnesisNotes.Text = reg.Complaint;
                    //txtHpi.Text = reg.Hpi;
                }

                // Hist dari asesment
                var qr = new PatientAssessmentQuery();
                qr.Where(qr.Or(qr.RegistrationNo.In(MergeRegistrations)));
                qr.es.Top = 1;
                qr.OrderBy(qr.LastUpdateDateTime.Descending);

                var assessment = new PatientAssessment();
                if (assessment.Load(qr))
                {                    
                    txtAnamnesisNotes.Text = assessment.AnamnesisNotes;

                    // Override Hpi
                    //if (!string.IsNullOrWhiteSpace(assessment.Hpi))
                    //    txtHpi.Text = assessment.Hpi;
                }
            }
        }

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {           
            txtAnamnesisNotes.Text = assessment.AnamnesisNotes;            
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(RegistrationNo))
                return;

            reg.Complaint = txtAnamnesisNotes.Text;
            reg.Save();

            var asses = assessment;            
            asses.AnamnesisNotes = txtAnamnesisNotes.Text;
        }


        #endregion

        protected string FromRegistrationNo
        {
            get
            {
                return Request.QueryString["fregno"];
            }
        }
        protected void lbtnPrevComplaint_OnClick(object sender, EventArgs e)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(FromRegistrationNo);            
        }
        protected void lbtnPrevHpi_OnClick(object sender, EventArgs e)
        {           
        }

        protected void lbtnPrevAnamnesisNotes_OnClick(object sender, EventArgs e)
        {
            txtAnamnesisNotes.Text = PrevPatientAssessment.AnamnesisNotes;
        }

        private PatientAssessment PrevPatientAssessment
        {
            get
            {
                var qra = new PatientAssessmentQuery("a");
                qra.Where(qra.RegistrationNo == FromRegistrationNo);
                qra.OrderBy(qra.RegistrationInfoMedicID.Ascending);
                qra.es.Top = 1;
                var assesment = new PatientAssessment();
                assesment.Load(qra);

                return assesment;
            }

        }
    }
}