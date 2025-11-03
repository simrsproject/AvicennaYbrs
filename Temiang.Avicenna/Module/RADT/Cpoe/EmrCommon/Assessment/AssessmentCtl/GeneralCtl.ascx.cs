using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class GeneralCtl : BaseAssessmentCtl
    {

        public override EntryGroupEnum EntryGroup
        {
            get { return EntryGroupEnum.Anamnesis; }
        }

        #region override method

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    if (!IsPostBack)
        //    {
                //var reg = new Registration();
                //if (reg.LoadByPrimaryKey(RegistrationNo))
                //{
                //    txtComplaint.Text = reg.Complaint;
                //    txtHpi.Text = reg.Hpi;
                //}

                //if (string.IsNullOrWhiteSpace(txtComplaint.Text) && string.IsNullOrWhiteSpace(FromRegistrationNo)) // Ambil history terakhir jika belum terisi
                //{
                //    // Tampilkan data terakhir di episode patient bersangkutan
                //    // HPI akan tertimpa nilainya di event OnPopulateEntryControl jika mode edit 
                //    reg = new Registration();
                //    if (reg.LoadByPrimaryKey(FromRegistrationNo))
                //    {
                //        txtComplaint.Text = reg.Complaint;
                //        txtHpi.Text = reg.Hpi;
                //    }
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
        //    }
        //}

        private bool? _isShowPnlFdolm = null;
        private bool IsShowPnlFdolm {
            get
            {
                if (_isShowPnlFdolm == null)
                {
                    _isShowPnlFdolm = false;

                    // Display First Day Of Last Menstruation hanya untuk pasien wanita
                    var pat = new Patient();
                    if (pat.LoadByPrimaryKey(PatientID))
                    {
                        if (pat.Sex == "F")
                        {
                            //RSSTJ : panel akan tampil bersarakan SRAssesmentType (Fajri - 2023/10/25)
                            var ShowPanelFdolm = AppSession.Parameter.AssessmentTypeIDsForShowPanelFdolm;
                            if (ShowPanelFdolm != null)
                            {
                                if (ShowPanelFdolm.Contains(AssessmentType))
                                {
                                    _isShowPnlFdolm = true;
                                }
                            }
                            else
                            {
                                // Anggap diatas 9 tahun baru haid
                                if (pat.DateOfBirth != null && ((DateTime.Now-pat.DateOfBirth).Value.TotalDays/355)>9)
                                {
                                    _isShowPnlFdolm = true;
                                }
                            }
                        }
                    }
                }

                return _isShowPnlFdolm??false;
            }
    } 

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ajxPnlFdolm.Visible = IsShowPnlFdolm;
            rowSCTChiefComplaint.Visible = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SatuSehatOrganizationID"]);

            rfvComplaint.Visible = false;
            if (AppParameter.IsYes(AppParameter.ParameterItem.IsEmrChiefComplaintTextRequired))
                rfvComplaint.Visible = true;
            curbPanel.Visible = AppParameter.GetParameterValue(AppParameter.ParameterItem.IsShowCurb65ScoreInAssesmentAndMDS).Equals("Yes", StringComparison.OrdinalIgnoreCase);
        }

        public override void OnMenuNewClick()
        {
            // Load default value
            if (IsShowPnlFdolm)
            {
                var fdlom = PatientField.GetValueDateTime(PatientID, AppField.FieldNameEnum.Fdolm);
                if (fdlom != null)
                {
                    txtFdolm.SelectedDate = fdlom.Value;
                    txtEstBirthDate.SelectedDate = EstBirthDate(fdlom.Value);
                    PopulatePregnantAgeInfo(fdlom);
                }
            }

            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                txtComplaint.Text = reg.Complaint;
                txtHpi.Text = reg.Hpi;
            }

            if (string.IsNullOrWhiteSpace(txtComplaint.Text) && !string.IsNullOrWhiteSpace(FromRegistrationNo)) // Ambil history terakhir jika belum terisi
            {
                // Tampilkan data terakhir di episode patient bersangkutan
                reg = new Registration();
                if (reg.LoadByPrimaryKey(FromRegistrationNo))
                {
                    txtComplaint.Text = reg.Complaint;
                    txtHpi.Text = reg.Hpi;
                }
            }

            // Hist dari asesment
            var qr = new PatientAssessmentQuery();
            qr.Where(qr.Or(qr.RegistrationNo.In(MergeRegistrations)));
            qr.es.Top = 1;
            qr.OrderBy(qr.LastUpdateDateTime.Descending);

            var assessment = new PatientAssessment();
            if (assessment.Load(qr))
            {
                cboAnamnesisType.SelectedIndex = assessment.IsAutoAnamnesis == true ? 0 : 1;
                txtAlloanamnesisSource.Text = assessment.AllowAnamnesisSource;
                txtMedikamentosa.Text = assessment.Medikamentosa;
                txtAnamnesisNotes.Text = assessment.AnamnesisNotes;

                // Override Hpi
                if (!string.IsNullOrWhiteSpace(assessment.Hpi))
                    txtHpi.Text = assessment.Hpi;
            }

            // Ambil status tgl skrg utk new Asesmen krn OnPopulateEntryControl tidak akan dijalankan
            grdVitalSign.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, MergeRegistrations, true, DateTime.Now);
            grdCurb.DataSource = VitalSign.VitalSignLastValue(RegistrationNo, MergeRegistrations, true, DateTime.Now,true);
        }
        protected override void OnPopulateEntryControl(PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var reg = new Registration();
            if (reg.LoadByPrimaryKey(RegistrationNo))
            {
                txtComplaint.Text = reg.Complaint;
            }

            if (string.IsNullOrWhiteSpace(txtComplaint.Text) && string.IsNullOrWhiteSpace(FromRegistrationNo)) // Ambil history terakhir jika belum terisi
            {
                // Tampilkan data terakhir di episode patient bersangkutan
                reg = new Registration();
                if (reg.LoadByPrimaryKey(FromRegistrationNo))
                {
                    txtComplaint.Text = reg.Complaint;
                }
            }

            if (rowSCTChiefComplaint.Visible)
                ComboBox.PopulateWithOneRow(cboSCTChiefComplaint, assessment.SCTChiefComplaint, Temiang.Avicenna.BusinessObject.Common.Enums.EntityClassName.Snomedct, "Code", "Display");


            txtHpi.Text = assessment.Hpi;
            cboAnamnesisType.SelectedIndex = assessment.IsAutoAnamnesis == true ? 0 : 1;
            txtAlloanamnesisSource.Text = assessment.AllowAnamnesisSource;
            txtMedikamentosa.Text = assessment.Medikamentosa;
            txtAnamnesisNotes.Text = assessment.AnamnesisNotes;
            grdVitalSign.DataSource = null;
            grdVitalSign.DataSource = VitalSign.VitalSignLastValue(assessment.RegistrationNo, MergeRegistrations, true,
                assessment.AssessmentDateTime.Value);
            grdCurb.DataSource = null;
            grdCurb.DataSource = VitalSign.VitalSignLastValue(assessment.RegistrationNo, MergeRegistrations, true,
                assessment.AssessmentDateTime.Value, true);

            //if (IsShowPnlFdolm)
            //{

            //    var fdlom = PatientField.GetValueDateTime(PatientID, AppField.FieldNameEnum.Fdolm) ?? assessment.Fdolm.Value;
            //    if (fdlom == null)
            //    {
            //        txtFdolm.Clear();
            //        txtEstBirthDate.Clear();
            //    }
            //    else
            //    {
            //        //var fdlom = assessment.Fdolm.Value;
            //        txtFdolm.SelectedDate = fdlom;

            //        txtEstBirthDate.SelectedDate = EstBirthDate(fdlom);
            //        PopulatePregnantAgeInfo(fdlom);
            //    }
            //}

            if (IsShowPnlFdolm)
            {
                var fdlom = PatientField.GetValueDateTime(PatientID, AppField.FieldNameEnum.Fdolm) ?? assessment.Fdolm;

                if (fdlom == null)
                {
                    txtFdolm.Clear();
                    txtEstBirthDate.Clear();
                }
                else
                {
                    txtFdolm.SelectedDate = fdlom.Value;

                    if (fdlom.Value != null)
                    {
                        txtEstBirthDate.SelectedDate = EstBirthDate(fdlom.Value);
                        PopulatePregnantAgeInfo(fdlom.Value);
                    }
                    else
                    {
                        txtFdolm.Clear();
                        txtEstBirthDate.Clear();
                    }
                }
            }
        }

        protected override void OnDataModeChanged(bool isEdited)
        {
        }

        protected override void OnSetEntityValue(ValidateArgs args, PatientAssessment assessment, RegistrationInfoMedic rim)
        {
            var reg = new Registration();
            if (!reg.LoadByPrimaryKey(RegistrationNo))
                return;

            reg.Complaint = txtComplaint.Text;
            reg.Hpi = txtHpi.Text;
            reg.Save();

            var asses = assessment;
            asses.ChiefComplaint = txtComplaint.Text;
            if (rowSCTChiefComplaint.Visible)
                asses.SCTChiefComplaint = cboSCTChiefComplaint.SelectedValue;
            else
                asses.str.SCTChiefComplaint = string.Empty;

            asses.Hpi = txtHpi.Text;
            asses.IsAutoAnamnesis = (cboAnamnesisType.SelectedIndex == 0);
            asses.AllowAnamnesisSource = txtAlloanamnesisSource.Text;
            asses.Medikamentosa = txtMedikamentosa.Text;
            asses.AnamnesisNotes = txtAnamnesisNotes.Text;

            if (IsShowPnlFdolm)
            {
                if (!txtFdolm.IsEmpty)
                    asses.Fdolm = txtFdolm.SelectedDate;

                // Update Additional PatientField
                PatientField.Update(PatientID, AppField.FieldNameEnum.Fdolm, DateTime.Now,
                    txtFdolm.IsEmpty ? null : txtFdolm.SelectedDate);
            }
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
            txtComplaint.Text = reg.Complaint;
        }
        protected void lbtnPrevHpi_OnClick(object sender, EventArgs e)
        {
            txtHpi.Text = PrevPatientAssessment.Hpi;
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

        protected void txtFdolm_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (txtFdolm.IsEmpty)
                txtEstBirthDate.Clear();
            else
            {
                txtEstBirthDate.SelectedDate = EstBirthDate(txtFdolm.SelectedDate.Value);
                PopulatePregnantAgeInfo(txtFdolm.SelectedDate.Value);
            }
        }

        private void PopulatePregnantAgeInfo(DateTime? fdlom)
        {
            lblPregnantAge.Text = "Pregnant Age: ";
            if (fdlom != null)
            {
                var ageInDays = (DateTime.Today - fdlom.Value).TotalDays;
                var week = Math.Floor((ageInDays / 7)).ToInt();
                var day = (ageInDays % 7);
                
                if (week > 0)
                    lblPregnantAge.Text = string.Concat(lblPregnantAge.Text, week, " weeks ");

                if (day > 0)
                    lblPregnantAge.Text = string.Concat(lblPregnantAge.Text, day, " days ");
            }
        }

        private DateTime EstBirthDate(DateTime fdlom)
        {
            if (fdlom.Month <= 3) // Jan s/d Maret
                return (new DateTime(fdlom.Year, fdlom.Month + 9, fdlom.Day)).AddDays(7);
            else
                return (new DateTime(fdlom.Year + 1, fdlom.Month - 3, fdlom.Day)).AddDays(7);
        }
    }
}