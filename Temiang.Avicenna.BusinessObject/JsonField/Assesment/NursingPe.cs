using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class NursingPe : BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        private AbNormalAndNotes _kepala;
        public AbNormalAndNotes Kepala
        {
            get { return _kepala ?? (_kepala = new AbNormalAndNotes()); }
            set { _kepala = value; }
        }

        private AbNormalAndNotes _mata;
        public AbNormalAndNotes Mata
        {
            get { return _mata ?? (_mata = new AbNormalAndNotes()); }
            set { _mata = value; }
        }

        private AbNormalAndNotes _tht;
        public AbNormalAndNotes Tht
        {
            get { return _tht ?? (_tht = new AbNormalAndNotes()); }
            set { _tht = value; }
        }

        private AbNormalAndNotes _mulut;
        public AbNormalAndNotes Mulut
        {
            get { return _mulut ?? (_mulut = new AbNormalAndNotes()); }
            set { _mulut = value; }
        }

        private AbNormalAndNotes _leher;
        public AbNormalAndNotes Leher
        {
            get { return _leher ?? (_leher = new AbNormalAndNotes()); }
            set { _leher = value; }
        }

        private AbNormalAndNotes _thorax;
        public AbNormalAndNotes Thorax
        {
            get { return _thorax ?? (_thorax = new AbNormalAndNotes()); }
            set { _thorax = value; }
        }

        private AbNormalAndNotes _jantung;
        public AbNormalAndNotes Jantung
        {
            get { return _jantung ?? (_jantung = new AbNormalAndNotes()); }
            set { _jantung = value; }
        }


        public string JantungInspeksi { get; set; }
        public string JantungPalpasi { get; set; }
        public string JantungPerkusi { get; set; }
        public string JantungAusk { get; set; }

        private AbNormalAndNotes _paru;
        public AbNormalAndNotes Paru
        {
            get { return _paru ?? (_paru = new AbNormalAndNotes()); }
            set { _paru = value; }
        }

        public string ParuInspeksi { get; set; }
        public string ParuPalpasi { get; set; }
        public string ParuPerkusi { get; set; }
        public string ParuAusk { get; set; }

        private AbNormalAndNotes _abdomen;
        public AbNormalAndNotes Abdomen
        {
            get { return _abdomen ?? (_abdomen = new AbNormalAndNotes()); }
            set { _abdomen = value; }
        }

        public string AbdoInspeksi { get; set; }
        public string AbdoPalpasi { get; set; }
        public string AbdoPerkusi { get; set; }

        private AbNormalAndNotes _genitaliaAndAnus;
        public AbNormalAndNotes GenitaliaAndAnus
        {
            get { return _genitaliaAndAnus ?? (_genitaliaAndAnus = new AbNormalAndNotes()); }
            set { _genitaliaAndAnus = value; }
        }
        private AbNormalAndNotes _ekstremitas;
        public AbNormalAndNotes Ekstremitas
        {
            get { return _ekstremitas ?? (_ekstremitas = new AbNormalAndNotes()); }
            set { _ekstremitas = value; }
        }

        private AbNormalAndNotes _kulit;
        public AbNormalAndNotes Kulit
        {
            get { return _kulit ?? (_kulit = new AbNormalAndNotes()); }
            set { _kulit = value; }
        }

        public string Inspekulo { get; set; }
        public string Notes { get; set; }
        public string NutritionSkrinning { get; set; }

        private PhysicalExamMetod _jantungMethod;
        public PhysicalExamMetod JantungMethod
        {
            get { return _jantungMethod ?? (_jantungMethod = new PhysicalExamMetod()); }
            set { _jantungMethod = value; }
        }

        private PhysicalExamMetod _paruMethod;
        public PhysicalExamMetod ParuMethod
        {
            get { return _paruMethod ?? (_paruMethod = new PhysicalExamMetod()); }
            set { _paruMethod = value; }
        }

        private PhysicalExamMetod _abdomenMethod;
        public PhysicalExamMetod AbdomenMethod
        {
            get { return _abdomenMethod ?? (_abdomenMethod = new PhysicalExamMetod()); }
            set { _abdomenMethod = value; }
        }

        [JsonProperty("Fdolm", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Fdolm { get; set; }
    }
}
