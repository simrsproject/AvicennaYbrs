using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class HeartPe : BaseJsonField
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
        public string Jantung { get; set; }
        public string Inspeksi { get; set; }


        public string Palpasi { get; set; }
        public string Lifting { get; set; }
        public string Thrill { get; set; }
        public string PerkusiLeft { get; set; }
        public string PerkusiRight { get; set; }
        public string AuscultationS1S2 { get; set; }
        public string AuscultationGallop { get; set; }
        public string Murmur { get; set; }

        public string Paru { get; set; }
        public string ParuInspeksi { get; set; }
        public string ParuPalpasi { get; set; }
        public string ParuPerkusi { get; set; }
        public string ParuAusk { get; set; }

        public string Abdomen { get; set; }
        public string AbdoInspeksi { get; set; }
        public string AbdoPalpasi { get; set; }
        public string AbdoPerkusi { get; set; }

        public string TataLaksana { get; set; }
        public string LamaRawat { get; set; }
        public string Prognosis { get; set; }


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
        public string Notes { get; set; }

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
        public string NutritionSkrinning { get; set; }

    }
}
