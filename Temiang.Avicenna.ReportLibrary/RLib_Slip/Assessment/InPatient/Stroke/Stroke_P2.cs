using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.JsonField.Assesment;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ReportLibrary.RLib_Slip.Assessment.InPatient
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for Stroke_P2.
    /// </summary>
    public partial class Stroke_P2 : Telerik.Reporting.Report
    {
        public Stroke_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);

            var patientID = printJobParameters.FindByParameterName("PatientID").ValueString;
            var rimid = printJobParameters.FindByParameterName("RegistrationInfoMedicID").ValueString;


            PopulatePhysicalExam(asses);

      
            if (asses.AssessmentDateTime != null)
            {
                var asesDateTime = asses.AssessmentDateTime.Value;
                txtTekananDarah.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.BloodPressure, asesDateTime);

                txtNadi.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.HeartRate, asses.AssessmentDateTime.Value);

                txtPernafasan.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.RespiratoryRate, asesDateTime);

                txtSuhu.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.Temperature, asesDateTime);

                txtSkorNyeri.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.PainScale, asesDateTime);

                txtSpO2.Value = VitalSign.LastVitalSignWithUnit(asses.RegistrationNo, reg.FromRegistrationNo,
                    VitalSign.VitalSignEnum.SpO2, asesDateTime);

           }
        }

        
        private void PopulatePhysicalExam(PatientAssessment asses)
        {
            if (string.IsNullOrEmpty(asses.PhysicalExam)) return;
            try
            {
                // Convert to class w json
                var pexam = JsonConvert.DeserializeObject<NeurologiPe>(asses.PhysicalExam);

                //<asp:ListItem Text="Mild" Value="Mild" Selected="True"></asp:ListItem>
                //   <asp:ListItem Text="Moderate" Value="Moderate"></asp:ListItem>
                //   <asp:ListItem Text="Severe" Value="Severe"></asp:ListItem>

                //TODO

            }
            catch (Exception)
            {
                //Nothing
            }
        }

       
        
        private List<MeasuredGoal> MeasuredGoalItems(string measuredGoals)
        {
            var goals = new MeasuredGoals();
            if (!string.IsNullOrEmpty(measuredGoals))
            {
                // Convert to class w json
                try
                {
                    goals = JsonConvert.DeserializeObject<MeasuredGoals>(measuredGoals);
                }
                catch (Exception)
                {
                }
            }

            var list = new List<MeasuredGoal>();
            if (goals.Items != null)
            {
                list = goals.Items;
            }

            foreach (var measuredGoal in list)
            {
                if (!string.IsNullOrEmpty(measuredGoal.IterationTimeType))
                {
                    if (measuredGoal.IterationTimeType == "HH")
                        measuredGoal.IterationTimeType = "Jam";
                    else
                        measuredGoal.IterationTimeType = "Menit";
                }
            }
            return list;
        }
    }
}