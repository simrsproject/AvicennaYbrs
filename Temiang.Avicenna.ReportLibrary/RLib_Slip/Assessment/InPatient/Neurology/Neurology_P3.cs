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
    /// Summary description for Neurology_P3.
    /// </summary>
    public partial class Neurology_P3 : Telerik.Reporting.Report
    {
        public Neurology_P3(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            Helper.InitializeLogoAndTextBottom(this.pageHeader);



            PopulateTherapy(asses.RegistrationInfoMedicID);

            txtAncillaryExam.Value = asses.OtherExam;
                        

        }


        private void PopulateTherapy(string registrationInfoMedicID)
        {
            var rim = new RegistrationInfoMedic();
            rim.LoadByPrimaryKey(registrationInfoMedicID);
            txtTherapy.Value = rim.Info4;
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