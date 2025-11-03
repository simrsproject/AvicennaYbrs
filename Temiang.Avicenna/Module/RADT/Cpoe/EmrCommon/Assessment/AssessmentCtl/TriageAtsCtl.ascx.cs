using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr.AssessmentCtl
{
    public partial class TriageAtsCtl : BaseAssessmentCtl
    {
        #region override method

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                //// Tampilkan data terakhir di episode patient bersangkutan
                //// Jika mode edit akan tertimpa nilainya di event OnPopulateEntryControl
                //var reg = new Registration();
                //reg.Query.Where(reg.Query.RegistrationNo.In(MergeRegistrations), reg.Query.Complaint != string.Empty);
                //reg.Query.OrderBy(reg.Query.RegistrationDateTime.Descending);
                //reg.Query.es.Top = 1;
                //if (reg.Query.Load())
                //{
                //    txtComplaint.Text = reg.Complaint;
                //    txtHpi.Text = reg.Hpi;
                //}

                //// Hist dari asesment
                //var qr = new PatientAssessmentQuery();
                //qr.Where(qr.Or(qr.RegistrationNo.In(MergeRegistrations)));
                //qr.es.Top = 1;
                //qr.OrderBy(qr.LastUpdateDateTime.Descending);

                //var assessment = new PatientAssessment();
                //if (assessment.Load(qr))
                //{
                //    cboAnamnesisType.SelectedIndex = assessment.IsAutoAnamnesis == true ? 0 : 1;
                //    txtAlloanamnesisSource.Text = assessment.AllowAnamnesisSource;
                //    txtMedikamentosa.Text = assessment.Medikamentosa;
                //    txtAnamnesisNotes.Text = assessment.AnamnesisNotes;

                //    // Override Hpi
                //    if (!string.IsNullOrWhiteSpace(assessment.Hpi))
                //        txtHpi.Text = assessment.Hpi;
                //}

                //// Ambil status tgl skrg utk new Asesmen krn OnPopulateEntryControl tidak akan dijalankan
                //grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, MergeRegistrations, true, DateTime.Now);
            }
        }

        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            //txtHpi.Text = assessment.Hpi;
            //cboAnamnesisType.SelectedIndex = assessment.IsAutoAnamnesis == true ? 0 : 1;
            //txtAlloanamnesisSource.Text = assessment.AllowAnamnesisSource;
            //txtMedikamentosa.Text = assessment.Medikamentosa;
            //txtAnamnesisNotes.Text = assessment.AnamnesisNotes;
            //grdVitalSign.DataSource = null;
            //grdVitalSign.DataSource = VitalSign.VitalSignLastValue(assessment.RegistrationNo, MergeRegistrations, true,
            //    assessment.AssessmentDateTime.Value);
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            //var reg = new Registration();
            //if (!reg.LoadByPrimaryKey(RegistrationNo))
            //    return;

            //reg.Complaint = txtComplaint.Text;
            //reg.Hpi = txtHpi.Text;
            //reg.Save();

            //var asses = assessment;
            //asses.Hpi = txtHpi.Text;
            //asses.IsAutoAnamnesis = (cboAnamnesisType.SelectedIndex == 0);
            //asses.AllowAnamnesisSource = txtAlloanamnesisSource.Text;
            //asses.Medikamentosa = txtMedikamentosa.Text;
            //asses.AnamnesisNotes = txtAnamnesisNotes.Text;
        }


        #endregion


    }
}