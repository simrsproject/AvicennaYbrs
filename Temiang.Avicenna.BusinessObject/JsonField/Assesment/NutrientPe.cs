using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class NutrientPe : BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        public string WeightUsually { get; set; }
        public string WeightCurrent { get; set; }
        public string BodyLegth { get; set; }
        public string BMI { get; set; }
        public string Time { get; set; }
        public string TimeType { get; set; }
        public string VisitType { get; set; }
        public string WeightChangeInSixMonth { get; set; }
        public string PercentChangeInSixMonth { get; set; }
        public string ChangeInSixMonth { get; set; }
        public string FoodIntake { get; set; }
        public string ChangeFoodIntake { get; set; }
        public string Gastrointestinal { get; set; }
        public string FreqNausea { get; set; }
        public string DurationNausea { get; set; }
        public string FreqGag { get; set; }
        public string DurationGag { get; set; }
        public string FreqDiarrhea { get; set; }
        public string DurationDiarrhea { get; set; }
        public string FreqAnorexia { get; set; }
        public string DurationAnorexia { get; set; }
        public string GastrointestinalChange { get; set; }
        public string Diagnose { get; set; }
        public string Metabolic { get; set; }
        public string LostFat { get; set; }
        public string LostMuscle { get; set; }
        public string Ankle { get; set; }
        public string Anasarca { get; set; }
        public string Ascites { get; set; }
        public string AncillaryExam { get; set; }
        public string Sga { get; set; }
        public string NutritionSkrinning { get; set; }
    }
}
