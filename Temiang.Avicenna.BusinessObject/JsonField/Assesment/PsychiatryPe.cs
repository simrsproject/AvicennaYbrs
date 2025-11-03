using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class PsychiatryPe : BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        public string PemeriksaanFisik { get; set; }
        public string Sensorik { get; set; }
        public string Motorik { get; set; }
        public string Otonom { get; set; }
        public string Neurologis { get; set; }
        public string JnsUsia { get; set; }
        public string Penampilan { get; set; }
        public string Sikap { get; set; }
        public string KondisiUmum { get; set; }
        public string Kesadaran { get; set; }
        public string Concentration { get; set; }
        public string MaintainCon { get; set; }
        public string DistractCon { get; set; }
        public string Memory { get; set; }
        public string Judgement { get; set; }
        public string Insight { get; set; }
        public string Mood { get; set; }
        public string Afek { get; set; }
        public string Perception { get; set; }
        public string ProsesPikir { get; set; }
        public string ArusPikir { get; set; }
        public string IsiPikir { get; set; }
        public string Psikodinamik { get; set; }
        public string OtherThing { get; set; }

        public string Aksis1 { get; set; }
        public string Aksis2 { get; set; }
        public string Aksis3 { get; set; }
        public string Aksis4 { get; set; }
        public string Aksis5 { get; set; }

        public string Farmakoterapi { get; set; }
        public string Psikofarmaka { get; set; }
        public string Psikoterapi { get; set; }
        public string Psikoedukasi { get; set; }
        public string Psikososial { get; set; }

        public string Vitam { get; set; }
        public string Functionam { get; set; }
        public string Sanation { get; set; }
        public string Notes { get; set; }
        public bool IsInsomnia { get; set; }
        public string Insomnia { get; set; }
        public string NutritionSkrinning { get; set; }
    }
}
