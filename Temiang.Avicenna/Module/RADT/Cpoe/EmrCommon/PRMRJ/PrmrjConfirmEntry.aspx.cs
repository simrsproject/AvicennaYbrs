using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.DynamicQuery;
using Temiang.Dal.Interfaces;


namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PrmrjConfirmEntry : BasePageDialog
    {
        public string RegistrationInfoMedicID
        {
            get
            {
                return Request.QueryString["rim"];
            }
        }        
        public string Registration
        {
            get
            {
                return Request.QueryString["regno"];
            }
        }

        public string PatientID
        {
            get
            {
                return Request.QueryString["patid"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.ElectronicHealthRecord;
            ButtonOk.Text = "Save";
            if (!IsPostBack)
            {
                var pat = new Patient();
                if (pat.LoadByPrimaryKey(PatientID))
                {
                    this.Title = "Save SOAP as PRMRJ of : " + pat.PatientName + " (MRN: " + pat.MedicalNo + ")";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Populate();
            }
        }
        private bool Save()
        {
            using (var tr = new esTransactionScope())
            {
                var ent = new RegistrationInfoMedic();
                if (ent.LoadByPrimaryKey(RegistrationInfoMedicID))
                {
                    ent.IsPRMRJ = true;
                    ent.Save();

                    var followUp = new PrmrjFollowUp();
                    if (!followUp.LoadByPrimaryKey(RegistrationInfoMedicID))
                    {
                        followUp.RegistrationInfoMedicID = RegistrationInfoMedicID;
                    }

                    followUp.ImportantClinicalNotes = txtImportantclinicalNotes.Text;
                    followUp.Planning = txtPlanning.Text;
                    followUp.Remark = txtRemark.Text;
                    followUp.Save();
                }
                tr.Complete();
            }

            return true;
        }

        private void Populate()
        {
            if (string.IsNullOrEmpty(lblChronicDisease.Text))
                lblChronicDisease.Text = Patient.ChronicDisease(PatientID);
            
            PopulateFollowUp();
        }

        private void PopulateFollowUp()
        {
            var followUp = new PrmrjFollowUp();
            if (!followUp.LoadByPrimaryKey(RegistrationInfoMedicID))
            {
                followUp.RegistrationInfoMedicID = RegistrationInfoMedicID;
            }

            txtImportantclinicalNotes.Text = followUp.ImportantClinicalNotes;
            txtPlanning.Text = followUp.Planning;
            txtRemark.Text = followUp.Remark;
        }



        public override bool OnButtonOkClicked()
        {
            return Save();
        }
    }
}
