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
    /// Summary description for Neurology_P4.
    /// </summary>
    public partial class Neurology_P4 : Telerik.Reporting.Report
    {
        public Neurology_P4(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            txtLamaRawat.Value = string.Format("{0:n0}", asses.EstimatedDayInPatient);
            txtPrognosis.Value = asses.Prognosis;
            txtParamedicName.Value = ParamedicTeam.DPJP(reg.RegistrationNo).ParamedicName; 
        }   
    }
}