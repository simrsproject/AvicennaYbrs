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
    /// Summary description for Internal_P2.
    /// </summary>
    public partial class Internal_P2 : Telerik.Reporting.Report
    {
        public Internal_P2(string programID, PrintJobParameterCollection printJobParameters, BusinessObject.Registration reg, PatientAssessment asses)
        {
            //
            // Required for telerik Reporting designer support
            //
            InitializeComponent();

            PopulateSystemReview(asses);

        }

        private void PopulateSystemReview(PatientAssessment asses)
        {
            var ros = JsonConvert.DeserializeObject<InternalRos>(asses.ReviewOfSystem);

            //Umum
            SetValueCheckBox(ros.Umum.QuestionAnswerValues, "APD.TS0101", chkGigilY, chkGigilN);
            SetValueCheckBox(ros.Umum.QuestionAnswerValues, "APD.TS0102", chkBBY, chkBBN);
            SetValueCheckBox(ros.Umum.QuestionAnswerValues, "APD.TS0103", chkDemamY, chkDemamN);
            SetValueCheckBox(ros.Umum.QuestionAnswerValues, "APD.TS0104", chkKmlmY, chkKmlmN);
            SetValueCheckBox(ros.Umum.QuestionAnswerValues, "APD.TS0105", chkCLY, chkCLN);

            //Mata
            SetValueCheckBox(ros.Mata.QuestionAnswerValues, "APD.TS0201", chkVisY, chkVisN);
            SetValueCheckBox(ros.Mata.QuestionAnswerValues, "APD.TS0202", chkNyeriY, chkNyeriN);
            SetValueCheckBox(ros.Mata.QuestionAnswerValues, "APD.TS0203", chkMerahY, chkMerahN);

            //THT
            SetValueCheckBox(ros.Tht.QuestionAnswerValues, "APD.TS0301", chkTHT1Y, chkTHT1N);
            SetValueCheckBox(ros.Tht.QuestionAnswerValues, "APD.TS0302", chkTHT2Y, chkTHT2N);
            SetValueCheckBox(ros.Tht.QuestionAnswerValues, "APD.TS0303", chkTHT3Y, chkTHT3N);
            SetValueCheckBox(ros.Tht.QuestionAnswerValues, "APD.TS0304", chkTHT4Y, chkTHT4N);
            SetValueCheckBox(ros.Tht.QuestionAnswerValues, "APD.TS0305", chkTHT5Y, chkTHT5N);
            SetValueCheckBox(ros.Tht.QuestionAnswerValues, "APD.TS0306", chkTHT6Y, chkTHT6N);
            SetValueCheckBox(ros.Tht.QuestionAnswerValues, "APD.TS0307", chkTHT7Y, chkTHT7N);

            //Cardiovas
            SetValueCheckBox(ros.Cardiovas.QuestionAnswerValues, "APD.TS0401", chkSDY, chkSDN);
            SetValueCheckBox(ros.Cardiovas.QuestionAnswerValues, "APD.TS0402", chkEdY, chkEdN);
            SetValueCheckBox(ros.Cardiovas.QuestionAnswerValues, "APD.TS0403", chkPNDY, chkPNDN);
            SetValueCheckBox(ros.Cardiovas.QuestionAnswerValues, "APD.TS0404", chkOrY, chkOrN);
            SetValueCheckBox(ros.Cardiovas.QuestionAnswerValues, "APD.TS0405", chkPalY, chkPalN);
            SetValueCheckBox(ros.Cardiovas.QuestionAnswerValues, "APD.TS0406", chkKlaY, chkKlaN);

            //Respirasi
            SetValueCheckBox(ros.Respirasi.QuestionAnswerValues, "APD.TS0501", chkResp01Y, chkResp01N);
            SetValueCheckBox(ros.Respirasi.QuestionAnswerValues, "APD.TS0502", chkResp02Y, chkResp02N);
            SetValueCheckBox(ros.Respirasi.QuestionAnswerValues, "APD.TS0503", chkResp03Y, chkResp03N);

            //Gastrointestinal
            SetValueCheckBox(ros.Gastrointestinal.QuestionAnswerValues, "APD.TS0601", chkGas1Y, chkGas1N);
            SetValueCheckBox(ros.Gastrointestinal.QuestionAnswerValues, "APD.TS0602", chkGas2Y, chkGas2N);
            SetValueCheckBox(ros.Gastrointestinal.QuestionAnswerValues, "APD.TS0603", chkGas3Y, chkGas3N);
            SetValueCheckBox(ros.Gastrointestinal.QuestionAnswerValues, "APD.TS0604", chkGas4Y, chkGas4N);
            SetValueCheckBox(ros.Gastrointestinal.QuestionAnswerValues, "APD.TS0605", chkGas5Y, chkGas5N);
            SetValueCheckBox(ros.Gastrointestinal.QuestionAnswerValues, "APD.TS0606", chkGas6Y, chkGas6N);


            //Saluran Kencing
            SetValueCheckBox(ros.SaluranKencing.QuestionAnswerValues, "APD.TS0701", chkSKncg1Y, chkSKncg1N);
            SetValueCheckBox(ros.SaluranKencing.QuestionAnswerValues, "APD.TS0702", chkSKncg2Y, chkSKncg2N);
            SetValueCheckBox(ros.SaluranKencing.QuestionAnswerValues, "APD.TS0703", chkSKncg3Y, chkSKncg3N);
            SetValueCheckBox(ros.SaluranKencing.QuestionAnswerValues, "APD.TS0704", chkSKncg4Y, chkSKncg4N);
            SetValueCheckBox(ros.SaluranKencing.QuestionAnswerValues, "APD.TS0705", chkSKncg5Y, chkSKncg5N);

            //Musc-skeletal
            SetValueCheckBox(ros.Muscle.QuestionAnswerValues, "APD.TS0801", chkMusc1Y, chkMusc1N);
            SetValueCheckBox(ros.Muscle.QuestionAnswerValues, "APD.TS0802", chkMusc2Y, chkMusc2N);
            SetValueCheckBox(ros.Muscle.QuestionAnswerValues, "APD.TS0803", chkMusc3Y, chkMusc3N);
            SetValueCheckBox(ros.Muscle.QuestionAnswerValues, "APD.TS0804", chkMusc4Y, chkMusc4N);
            SetValueCheckBox(ros.Muscle.QuestionAnswerValues, "APD.TS0805", chkMusc5Y, chkMusc5N);

            //Hematologi
            SetValueCheckBox(ros.Hematologi.QuestionAnswerValues, "APD.TS0901", chkHema1Y, chkHema1N);
            SetValueCheckBox(ros.Hematologi.QuestionAnswerValues, "APD.TS0902", chkHema2Y, chkHema2N);
            SetValueCheckBox(ros.Hematologi.QuestionAnswerValues, "APD.TS0903", chkHema3Y, chkHema3N);

            //Endokrin
            SetValueCheckBox(ros.Endokrin.QuestionAnswerValues, "APD.TS1001", chkEndo1Y, chkEndo1N);
            SetValueCheckBox(ros.Endokrin.QuestionAnswerValues, "APD.TS1002", chkEndo2Y, chkEndo2N);
            SetValueCheckBox(ros.Endokrin.QuestionAnswerValues, "APD.TS1003", chkEndo3Y, chkEndo3N);

            //Dermatologi
            SetValueCheckBox(ros.Dermatologi.QuestionAnswerValues, "APD.TS1101", chkRuamY, chkRuamN);
            SetValueCheckBox(ros.Dermatologi.QuestionAnswerValues, "APD.TS1102", chkPruY, chkPruN);

            //Neurologi
            SetValueCheckBox(ros.Neurologi.QuestionAnswerValues, "APD.TS1201", chkKejY, chkKejN);
            SetValueCheckBox(ros.Neurologi.QuestionAnswerValues, "APD.TS1202", chkLEksY, chkLEksN);
            SetValueCheckBox(ros.Neurologi.QuestionAnswerValues, "APD.TS1203", chkSinY, chkSinN);
            SetValueCheckBox(ros.Neurologi.QuestionAnswerValues, "APD.TS1204", chkPKY, chkPKN);

            //Psikiatri
            SetValueCheckBox(ros.Psikiatri.QuestionAnswerValues, "APD.TS1301", chkCmsY, chkCmsN);
            SetValueCheckBox(ros.Psikiatri.QuestionAnswerValues, "APD.TS1302", chkDepY, chkDepN);

        }

        private void SetValueCheckBox(List<QuestionAnswerValue> listValues, string questionID,
            Telerik.Reporting.CheckBox chkYes, Telerik.Reporting.CheckBox chkNo)
        {
            var answerVal = FindQuestionAnswerValue(listValues, questionID);
            if (answerVal != null)
            {
                chkYes.Value = answerVal.QuestionAnswerSelectionLineID == "Y";
                chkNo.Value = answerVal.QuestionAnswerSelectionLineID == "N";
            }

        }
        private QuestionAnswerValue FindQuestionAnswerValue(List<QuestionAnswerValue> listValues, string questionID)
        {
            foreach (QuestionAnswerValue value in listValues)
            {
                if (value.QuestionID == questionID)
                    return value;
            }

            return null;
        }

    }
}