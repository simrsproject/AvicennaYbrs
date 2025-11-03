using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class NeurologiPeIp : BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        public string Notes { get; set; }
        public string Cardio { get; set; }
        public string Gastro { get; set; }
        public string GastroOther { get; set; }
        public string Urogenital { get; set; }
        public string UrogenitalOther { get; set; }
        public string Extremitas { get; set; }
    }

    public class NeurologiRos : BaseJsonField
    {
        public string Motorik { get; set; }
        public string MotorikPower { get; set; }
        public string MotorikWeakness { get; set; }
        public string Sensorik { get; set; }
        public string SensorikParestesia { get; set; }
        public string SensorikOtherProblem { get; set; }
        public string FungsiLuhur { get; set; }
        public string FungsiLuhurOther { get; set; }
        public string Craniales { get; set; }
        public string CranialesPupil { get; set; }
        public string CranialesReflekCahaya { get; set; }
        public string CranialesVisus { get; set; }
        public string CranialesFundus { get; set; }
        public string CranialesBolaMata { get; set; }
        public string CranialesReflekKornea { get; set; }
        public string CranialesOther { get; set; }
        public string Keseimbangan { get; set; }
        public string KeseimbanganOther { get; set; }
        public string Vegetatif { get; set; }
    }
}
