using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class StrokeAnamnesisIp : BaseJsonField
    {

    }

    public class StrokeRos : BaseJsonField
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
    }
}
