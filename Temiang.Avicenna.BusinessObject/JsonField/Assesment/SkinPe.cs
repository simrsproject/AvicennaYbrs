using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class SkinPe : BaseJsonField
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

        private AbNormalAndNotes _paru;
        public AbNormalAndNotes Paru
        {
            get { return _paru ?? (_paru = new AbNormalAndNotes()); }
            set { _paru = value; }
        }

        private AbNormalAndNotes _abdomen;
        public AbNormalAndNotes Abdomen
        {
            get { return _abdomen ?? (_abdomen = new AbNormalAndNotes()); }
            set { _abdomen = value; }
        }

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

        public string Dermatologikus { get; set; }
        public string Notes { get; set; }
        public string NutritionSkrinning { get; set; }
    }
}
