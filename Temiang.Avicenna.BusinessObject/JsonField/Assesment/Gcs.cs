using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    /// <summary>
    /// Glasgow Coma Scale
    /// </summary>
    public class Gcs
    {
        public string GetSoapObjective(string condition)
        {
            var strBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(condition))
            {
                strBuilder.AppendFormat("Keadaan Umum: Sakit {0}", condition == "Mild" ? "Ringan" : condition == "Moderate" ? "Sedang" : "Berat");
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(this.ConsciousnessDescription))
            {
                strBuilder.AppendFormat("Kesadaran: {0} GCS: E: {1} M: {2} V: {3}",
                    this.ConsciousnessDescription, this.Eye.Score, this.Motor.Score,
                    this.Verbal.Score);
                strBuilder.AppendLine(string.Empty);
            }

            if (!string.IsNullOrEmpty(this.PainScale))
            {
                strBuilder.AppendFormat("Skala Nyeri: ({0}) {1}", this.PainScale, PainScaleDesc(this.PainScale.ToInt()));
            }

            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.IsUsingSplitPainScaleAndFlaccBasedOnAge) == "Yes")
            {
                var strbFlacc = new StringBuilder();
                var flaccScore = 0;
                var flacc = LoadStandardReferenceItem("Flacc", this.Flacc.Face ?? string.Empty);
                if (flacc.ItemID != null)
                {
                    flaccScore = flaccScore + flacc.NumericValue.ToInt();
                    strbFlacc.AppendFormat(" - Face: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                    strbFlacc.AppendLine(string.Empty);
                }
                flacc = LoadStandardReferenceItem("Flacc", this.Flacc.Legs ?? string.Empty);
                if (flacc.ItemID != null)
                {
                    flaccScore = flaccScore + flacc.NumericValue.ToInt();
                    strbFlacc.AppendFormat(" - Legs: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                    strbFlacc.AppendLine(string.Empty);
                }
                flacc = LoadStandardReferenceItem("Flacc", this.Flacc.Activity ?? string.Empty);
                if (flacc.ItemID != null)
                {
                    flaccScore = flaccScore + flacc.NumericValue.ToInt();
                    strbFlacc.AppendFormat(" - Activity: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                    strbFlacc.AppendLine(string.Empty);
                }
                flacc = LoadStandardReferenceItem("Flacc", this.Flacc.Cry ?? string.Empty);
                if (flacc.ItemID != null)
                {
                    flaccScore = flaccScore + flacc.NumericValue.ToInt();
                    strbFlacc.AppendFormat(" - Cry: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                    strbFlacc.AppendLine(string.Empty);
                }
                flacc = LoadStandardReferenceItem("Flacc", this.Flacc.Consolability ?? string.Empty);
                if (flacc.ItemID != null)
                {
                    flaccScore = flaccScore + flacc.NumericValue.ToInt();
                    strbFlacc.AppendFormat(" - Consolability: {0}", string.Format("{0} [{1}]", flacc.ItemName, flacc.NumericValue.ToInt()));
                    strbFlacc.AppendLine(string.Empty);
                }
                var flaccStr = strbFlacc.ToString();
                if (!string.IsNullOrWhiteSpace(flaccStr))
                {
                    strBuilder.AppendFormat("FLACC ({0}):", flaccScore);
                    strbFlacc.AppendLine(string.Empty);
                    strBuilder.AppendLine(flaccStr);
                }
            }

            return strBuilder.ToString();
        }
        public string PainScaleDesc(int painScale)
        {
            if (painScale == 0)
                return "Tidak sakit";
            else if (painScale < 3)
                return "Sedikit sakit";
            else if (painScale < 5)
                return "Agak mengganggu";
            else if (painScale < 7)
                return "Mengganggu aktivitas";
            else if (painScale < 9)
                return "Sangat mengganggu";
            else
                return "Tak tertahankan";
        }
        public int ConsciousnessValue
        {
            get
            {
                // Maslah dgn data yg lama
                if (Eye.Score == 99)
                    Eye.Score = 0;
                if (Motor.Score == 99)
                    Motor.Score = 0;
                if (Verbal.Score == 99)
                    Verbal.Score = 0;

                var total = Eye.Score + Motor.Score + Verbal.Score;

                return total;
            }
        }

        public string ConsciousnessDescription
        {
            get
            {
                //1. Composmentis : 15-14 
                //2. Apatis : 13-12 
                //3. Delirium : 11-10 
                //4. Somnolen : 9-7 
                //5. Stupor : 6-4 
                //6. Coma : 3 
                var score = ConsciousnessValue;
                var retVal = ConsciousnessCategory(score);
                if (!string.IsNullOrEmpty(retVal) && !string.IsNullOrEmpty(ConsciousnessNote))
                    retVal = string.Format("{0} ({1})", retVal, ConsciousnessNote);
                else if (string.IsNullOrEmpty(retVal) && !string.IsNullOrEmpty(ConsciousnessNote))
                    retVal = ConsciousnessNote;

                return retVal;
            }
        }

        private string ConsciousnessCategory(int score)
        {
            if (score < 4 && score > 0)
                return "Coma";
            if (score >= 4 && score < 7)
                return "Stupor";
            if (score >= 7 && score < 10)
                return "Somnolen";
            if (score >= 10 && score < 12)
                return "Delirium";
            if (score >= 12 && score < 14)
                return "Apatis";
            if (score >= 14 && score < 16)
                return "Composmentis";

            return string.Empty;
        }

        private static AppStandardReferenceItem LoadStandardReferenceItem(string standardReference, string itemID)
        {
            var stdi = new AppStandardReferenceItem();
            stdi.LoadByPrimaryKey(standardReference, itemID);
            return stdi;
        }
        public string ConsciousnessNote { get; set; }
        public string PainScale { get; set; }
        private GcsItem _eye;
        public GcsItem Eye
        {
            get { return _eye ?? (_eye = new GcsItem()); }
            set { _eye = value; }
        }
        private GcsItem _motor;
        public GcsItem Motor
        {
            get { return _motor ?? (_motor = new GcsItem()); }
            set { _motor = value; }
        }
        private GcsItem _verbal;
        public GcsItem Verbal
        {
            get { return _verbal ?? (_verbal = new GcsItem()); }
            set { _verbal = value; }
        }
        private Flacc _flacc;
        public Flacc Flacc
        {
            get { return _flacc ?? (_flacc = new Flacc()); }
            set { _flacc = value; }
        }
    }

    /// <summary>
    /// Code diambil dari StandardReferenceItem
    /// </summary>
    public class GcsItem
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
