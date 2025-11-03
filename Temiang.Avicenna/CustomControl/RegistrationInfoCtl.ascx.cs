using System;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.CustomControl
{
    public partial class RegistrationInfoCtl : System.Web.UI.UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
                PopulateRegistrationInfo();
        }
        public string RegistrationNo => Request.QueryString["regno"];

        public bool? IsShowVitalSign
        {
            set
            {
                if (value == null)
                    hdnIsShowVitalSign.Value = "0";
                else 
                    hdnIsShowVitalSign.Value = (value??false) ? "1" : "0";
            }
            get => hdnIsShowVitalSign.Value == "1";
        }

        public bool? IsShowDianosis
        {
            set
            {
                if (value == null)
                    hdnIsShowDiagnosis.Value = "0";
                else 
                    hdnIsShowDiagnosis.Value = (value??false) ? "1" : "0";
            }
            get => hdnIsShowDiagnosis.Value == "1";
        }

        public bool? IsShowPatientAllergy
        {
            set
            {
                if (value == null)
                    hdnIsShowPatientAllergy.Value = "0";
                else 
                    hdnIsShowPatientAllergy.Value = (value??false) ? "1" : "0";
            }
            get => hdnIsShowPatientAllergy.Value == "1";
        }

        public bool? IsShowPhysicianTeam
        {
            set
            {
                if (value == null)
                    hdnIsShowPhysicianTeam.Value = "0";
                else 
                    hdnIsShowPhysicianTeam.Value = (value??false) ? "1" : "0";
            }
            get => hdnIsShowPhysicianTeam.Value == "1";
        }

        private string ParamedicName(string registrationNo)
        {
            // Paramedic
            var medic = new Paramedic();
            var qrparteam = new ParamedicTeamQuery("pt");
            qrparteam.Where(qrparteam.RegistrationNo == registrationNo);
            qrparteam.es.Top = 1;
            if ((new ParamedicTeam().Load(qrparteam)))
            {
                var parteams = new ParamedicTeamCollection();
                parteams.Query.Where(parteams.Query.RegistrationNo == RegistrationNo);
                parteams.Query.OrderBy(parteams.Query.SRParamedicTeamStatus.Ascending);
                parteams.LoadAll();
                var strBld = new StringBuilder();
                var i = 1;
                foreach (var parteam in parteams)
                {
                    medic = new Paramedic();
                    if (medic.LoadByPrimaryKey(parteam.ParamedicID))
                    {
                        strBld.AppendFormat("{0}. {1}<br />", i, medic.ParamedicName);
                        i++;
                    }
                }
                return strBld.ToString();
            }

            medic.LoadByPrimaryKey(RegistrationCurrent.ParamedicID);
            return medic.ParamedicName;
        }

        private Registration _regCurr;
        private Registration RegistrationCurrent
        {
            get
            {
                if (_regCurr == null)
                {
                    _regCurr = new Registration();
                    _regCurr.LoadByPrimaryKey(RegistrationNo);
                }

                return _regCurr;
            }
        }
        private Registration PopulateRegistrationInfo()
        {
            var reg = RegistrationCurrent;

            var pat = new Patient();
            pat.LoadByPrimaryKey(reg.PatientID);

            lblPatientName.Text = pat.PatientName;

            lblMedicalNo.Text = pat.MedicalNo;
            lblRegistrationDate.Text = Convert.ToDateTime(reg.RegistrationDate).ToString(AppConstant.DisplayFormat.DateShortMonth);
            lblRegistrationNo.Text = reg.RegistrationNo;
            lblGender.Text = pat.Sex == "M" ? "Male" : "Female";
            lblDateOfBirth.Text = string.Format("{0} ({1}y {2}m {3}d)", (pat.DateOfBirth ?? new DateTime()).ToString(AppConstant.DisplayFormat.DateShortMonth),
                reg.AgeInYear, reg.AgeInMonth, reg.AgeInDay);

            lblChronicDisease.Text = Patient.ChronicDisease(reg.PatientID);
            divChronicDisease.Visible = !string.IsNullOrEmpty(lblChronicDisease.Text);

            PopulateParamedicTeam(RegistrationNo);

            var grr = new Guarantor();
            grr.LoadByPrimaryKey(reg.GuarantorID);
            lblGuarantor.Text = grr.GuarantorName;

            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(RegistrationCurrent.ServiceUnitID);
            lblServiceUnit.Text = unit.ServiceUnitName;

            if (!string.IsNullOrWhiteSpace(reg.RoomID))
            {
                var room = new ServiceRoom();
                room.LoadByPrimaryKey(reg.RoomID);
                lblRoom.Text = room.RoomName;
            }

            PopulatePatientAllergy();
            PopulateEpisodeDiagnose();
            PopulatePatientImage(reg.PatientID);
            PopulateLastVitalSign();
            return reg;
        }

        private void PopulateLastVitalSign()
        {
            if (!(IsShowVitalSign??false))
            {
                pnlVitalSign.Visible = false;
                litVitalSign.Text = String.Empty;
                return;
            }
            var reg = RegistrationCurrent;
            litVitalSign.Text = string.Format("<span style=\"font-size:large;\"><strong>W:</strong> {0}&nbsp;&nbsp;&nbsp;<strong>H:</strong> {1}&nbsp;&nbsp;&nbsp;<strong>T:</strong> {2}&nbsp;&nbsp;&nbsp;<strong>BP:</strong> {3}</span>",
            VitalSign.LastVitalSignWithUnit(reg.RegistrationNo, reg.FromRegistrationNo, VitalSign.VitalSignEnum.BodyWeight, DateTime.Now),
            VitalSign.LastVitalSignWithUnit(reg.RegistrationNo, reg.FromRegistrationNo, VitalSign.VitalSignEnum.BodyHeight, DateTime.Now),
            VitalSign.LastVitalSignWithUnit(reg.RegistrationNo, reg.FromRegistrationNo, VitalSign.VitalSignEnum.Temperature, DateTime.Now),
            VitalSign.LastVitalSignWithUnit(reg.RegistrationNo, reg.FromRegistrationNo, VitalSign.VitalSignEnum.BloodPressure, DateTime.Now));
        }

        private void PopulatePatientImage(string patientID)
        {
            // Patient Photo
            imgPatientPhoto.ImageUrl = string.Empty;
            // Load from database
            var patientImg = new PatientImage();
            if (patientImg.LoadByPrimaryKey(patientID))
            {
                // Show Image
                if (patientImg.Photo != null)
                {
                    imgPatientPhoto.ImageUrl = string.Format("data:image/jpg;base64,{0}",
                        Convert.ToBase64String(patientImg.Photo));
                }
                else
                {
                    imgPatientPhoto.ImageUrl = lblGender.Text == "Male" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";
                }
            }
            else
                imgPatientPhoto.ImageUrl = lblGender.Text == "Male" ? "~/Images/Asset/Patient/ManVector.png" : "~/Images/Asset/Patient/WomanVector.png";

        }

        private void  PopulateParamedicTeam(string registrationNo)
        {
            if (!(IsShowPhysicianTeam??false))
            {
                pnlPhysicianTeam.Visible = false;
                litPhysicianTeam.Text = String.Empty;
                return;
            }

            // Paramedic
            var medic = new Paramedic();
            var qrparteam = new ParamedicTeamQuery("pt");
            qrparteam.Where(qrparteam.RegistrationNo == registrationNo);
            qrparteam.es.Top = 1;
            if ((new ParamedicTeam().Load(qrparteam)))
            {
                var pt = new ParamedicTeamQuery();
                pt.Where(pt.RegistrationNo == registrationNo);
                pt.OrderBy(pt.SRParamedicTeamStatus.Ascending);
                pt.es.Distinct = true;
                pt.Select(pt.ParamedicID, pt.SRParamedicTeamStatus);

                var dtb = pt.LoadDataTable();

                var statusDpjpId = AppParameter.GetParameterValue(AppParameter.ParameterItem.ParamedicTeamStatusDpjpID);

                var strBld = new StringBuilder();
                var i = 1;
                strBld.Append("<table>");
                foreach (DataRow row in dtb.Rows)
                {
                    medic = new Paramedic();
                    if (medic.LoadByPrimaryKey(row["ParamedicID"].ToString()))
                    {

                        strBld.AppendFormat("<tr><td style=\"width:5px;vertical-align: top;\">{0}.</td><td>{1}</td></tr>", i, statusDpjpId.Equals(row["SRParamedicTeamStatus"]) ? string.Format("<b>{0}</b>", medic.ParamedicName) : medic.ParamedicName);
                        i++;
                    }
                }
                strBld.Append("</table>");
                litPhysicianTeam.Text = strBld.ToString();
            }

            litPhysicianTeam.Text = string.Empty;
        }

        private void PopulatePatientAllergy()
        {
            if (!(IsShowPatientAllergy??false))
            {
                pnlPatientAllergy.Visible = false;
                litPatientAllergy.Text = String.Empty;
                return;
            }

            var paQ = new PatientAllergyQuery("a");
            paQ.Select(paQ.AllergenName, paQ.DescAndReaction);
            paQ.Where(paQ.PatientID == RegistrationCurrent.PatientID);
            var dtb = paQ.LoadDataTable();
            var sb = new StringBuilder();
            sb.AppendLine("<table style='width:100%'>");
            foreach (DataRow dataRow in dtb.Rows)
            {
                sb.AppendLine("<tr>");
                sb.AppendFormat("<td style='width:100px;font-weight: bold;'>{0}</td>", dataRow["AllergenName"]);
                sb.AppendLine("<td style='width:5px'>:</td>");
                sb.AppendFormat("<td style='color: red;'>{0}</td>", dataRow["DescAndReaction"]);
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</table>");
            litPatientAllergy.Text = sb.ToString();
        }

        private void PopulateEpisodeDiagnose()
        {
            if (!(IsShowDianosis??false))
            {
                pnlDiagnosis.Visible = false;
                litDiagnosis.Text = String.Empty;
                return;
            }

            litDiagnosis.Text = EpisodeDiagnose.DiagnoseSummaryHtml(RegistrationNo);
        }
    }
}