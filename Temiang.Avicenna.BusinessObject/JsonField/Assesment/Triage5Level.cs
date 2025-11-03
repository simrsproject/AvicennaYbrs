using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class Triage5Level
    {
        public int TriageValue
        {
            get
            {
                return TriageId.Score;
            }
        }

        public string AirwayDescription
        {
            get
            {
                //1. Obstruksi/Obstruksi Partial : 1 
                //2. Bebas : 2-5 

                var score = TriageValue;
                if (score == 1)
                    return "Obstruksi/Obstruksi Partial";
                if (score > 1 && score < 6)
                    return "Bebas";

                return string.Empty;
            }
        }

        public string BreathingDescription
        {
            get
            {
                //1. Respiratory Distress berat/tidak ada respirasi/hipoventilasi : 1 
                //2. Respiratory Distress sedang : 2
                //3. Respiratory Distress ringan : 3
                //4. Tidak terjadi respiratory distress : 4-5

                var score = TriageValue;
                if (score == 1)
                    return "Respiratory Distress berat/tidak ada respirasi/hipoventilasi";
                if (score == 2)
                    return "Respiratory Distress sedang";
                if (score == 3)
                    return "Respiratory Distress ringan";
                if (score > 3 && score < 6)
                    return "Tidak terjadi respiratory distress";

                return string.Empty;
            }
        }

        public string CirculationDescription
        {
            get
            {
                //1. Gangguan hemodinamik berat/tidak ada sirkulasi : 1 
                //2. Gangguan hemodinamik sedang : 2-3
                //3. Tidak terjadi gangguan hemodinamik : 4-5

                var score = TriageValue;
                if (score == 1)
                    return "Gangguan hemodinamik berat/tidak ada sirkulasi";
                if (score == 2 || score == 3)
                    return "Gangguan hemodinamik sedang";
                if (score > 3 && score < 6)
                    return "Tidak terjadi gangguan hemodinamik";

                return string.Empty;

            }
        }

        public string ConsciousDescription
        {
            get
            {
                //1. GCS < 9 : 1 
                //2. GCS 9 - 12 : 2
                //3. GCS > 12 : 3
                //4. GCS Normal : 4-5

                var score = TriageValue;
                if (score == 1)
                    return "GCS < 9";
                if (score == 2)
                    return "GCS 9 - 12";
                if (score == 3)
                    return "GCS > 12";
                if (score > 3 && score < 6)
                    return "GCS Normal";

                return string.Empty;
            }
        }

        private TriageItem _triageId;
        public TriageItem TriageId
        {
            get { return _triageId ?? (_triageId = new TriageItem()); }
            set { _triageId = value; }
        }
    }

    public class TriageItem
    {
        public string Code { get; set; }
        public int Score { get; set; }

        public void SetValue(string value)
        {
            Score = 0;

            if (string.IsNullOrEmpty(value))
                return;
            if (!value.Contains("_"))
                return;

            // Data dari StandardReferenceItem ItemID & Note yg digabung dg sep _
            var values = value.Split('_');
            Code = values[0];
            if (!string.IsNullOrEmpty(values[1]))
                Score = Convert.ToInt32(values[1]);
        }
    }
}
