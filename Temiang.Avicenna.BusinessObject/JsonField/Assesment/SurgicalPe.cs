using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class SurgicalPe: BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        private AbNormalAndNotes2 _kepala;
        public AbNormalAndNotes2 Kepala
        {
            get { return _kepala ?? (_kepala = new AbNormalAndNotes2()); }
            set { _kepala = value; }
        }

        private AbNormalAndNotes2 _mata;
        public AbNormalAndNotes2 Mata
        {
            get { return _mata ?? (_mata = new AbNormalAndNotes2()); }
            set { _mata = value; }
        }

        private AbNormalAndNotes2 _tht;
        public AbNormalAndNotes2 Tht
        {
            get { return _tht ?? (_tht = new AbNormalAndNotes2()); }
            set { _tht = value; }
        }

        private AbNormalAndNotes2 _mulut;
        public AbNormalAndNotes2 Mulut
        {
            get { return _mulut ?? (_mulut = new AbNormalAndNotes2()); }
            set { _mulut = value; }
        }

        private AbNormalAndNotes2 _leher;
        public AbNormalAndNotes2 Leher
        {
            get { return _leher ?? (_leher = new AbNormalAndNotes2()); }
            set { _leher = value; }
        }

        private AbNormalAndNotes2 _thorax;
        public AbNormalAndNotes2 Thorax
        {
            get { return _thorax ?? (_thorax = new AbNormalAndNotes2()); }
            set { _thorax = value; }
        }
        private AbNormalAndNotes2 _jantung;
        public AbNormalAndNotes2 Jantung
        {
            get { return _jantung ?? (_jantung = new AbNormalAndNotes2()); }
            set { _jantung = value; }
        }
        private AbNormalAndNotes2 _paru;
        public AbNormalAndNotes2 Paru
        {
            get { return _paru ?? (_paru = new AbNormalAndNotes2()); }
            set { _paru = value; }
        }
        private AbNormalAndNotes2 _abdomen;
        public AbNormalAndNotes2 Abdomen
        {
            get { return _abdomen ?? (_abdomen = new AbNormalAndNotes2()); }
            set { _abdomen = value; }
        }
        private AbNormalAndNotes2 _genitaliaAndAnus;
        public AbNormalAndNotes2 GenitaliaAndAnus
        {
            get { return _genitaliaAndAnus ?? (_genitaliaAndAnus = new AbNormalAndNotes2()); }
            set { _genitaliaAndAnus = value; }
        }
        private AbNormalAndNotes2 _anus;
        public AbNormalAndNotes2 Anus
        {
            get { return _anus ?? (_anus = new AbNormalAndNotes2()); }
            set { _anus = value; }
        }
        private AbNormalAndNotes2 _ekstremitas;
        public AbNormalAndNotes2 Ekstremitas
        {
            get { return _ekstremitas ?? (_ekstremitas = new AbNormalAndNotes2()); }
            set { _ekstremitas = value; }
        }
        private AbNormalAndNotes2 _ekstremitasLower;
        public AbNormalAndNotes2 EkstremitasLower
        {
            get { return _ekstremitasLower ?? (_ekstremitasLower = new AbNormalAndNotes2()); }
            set { _ekstremitasLower = value; }
        }
        private AbNormalAndNotes2 _kulit;
        public AbNormalAndNotes2 Kulit
        {
            get { return _kulit ?? (_kulit = new AbNormalAndNotes2()); }
            set { _kulit = value; }
        }
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
    }
}
