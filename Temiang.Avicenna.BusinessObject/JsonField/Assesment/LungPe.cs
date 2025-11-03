using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class LungPe : BaseJsonField
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


        private Paru _paru;
        public Paru Paru
        {
            get { return _paru ?? (_paru = new Paru()); }
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
        public string Notes { get; set; }
        public string NutritionSkrinning { get; set; }
        public string Jantung { get; set; }
        public string AuscultationWheezing { get; set; }

        private PhysicalExamMetod _jantungMethod;
        public PhysicalExamMetod JantungMethod
        {
            get { return _jantungMethod ?? (_jantungMethod = new PhysicalExamMetod()); }
            set { _jantungMethod = value; }
        }

        private PhysicalExamMetod _abdomenMethod;
        public PhysicalExamMetod AbdomenMethod
        {
            get { return _abdomenMethod ?? (_abdomenMethod = new PhysicalExamMetod()); }
            set { _abdomenMethod = value; }
        }

    }

    public class Paru
    {
        private InspeksiParu _inspeksi;
        public InspeksiParu Inspeksi
        {
            get { return _inspeksi ?? (_inspeksi = new InspeksiParu()); }
            set { _inspeksi = value; }
        }

        private PalpasiParu _palpasi;
        public PalpasiParu Palpasi
        {
            get { return _palpasi ?? (_palpasi = new PalpasiParu()); }
            set { _palpasi = value; }
        }

        private ConditionAndLocation _perkusi;
        public ConditionAndLocation Perkusi
        {
            get { return _perkusi ?? (_perkusi = new ConditionAndLocation()); }
            set { _perkusi = value; }
        }

        private Auskultasi _auskultasi;
        public Auskultasi Auskultasi
        {
            get { return _auskultasi ?? (_auskultasi = new Auskultasi()); }
            set { _auskultasi = value; }
        }
    }

    public class Auskultasi
    {
        private ConditionAndLocation _vesikular;
        public ConditionAndLocation Vesikular
        {
            get { return _vesikular ?? (_vesikular = new ConditionAndLocation()); }
            set { _vesikular = value; }
        }

        private ConditionAndLocation _ronchi;
        public ConditionAndLocation Ronchi
        {
            get { return _ronchi ?? (_ronchi = new ConditionAndLocation()); }
            set { _ronchi = value; }
        }


        public string RonchiLevel { get; set; }

        private ExistAndNotes _bising;
        public ExistAndNotes Bising
        {
            get { return _bising ?? (_bising = new ExistAndNotes()); }
            set { _bising = value; }
        }
    }

    public class InspeksiParu
    {
        private AbNormalAndNotes _respiratory;
        public AbNormalAndNotes Respiratory
        {
            get { return _respiratory ?? (_respiratory = new AbNormalAndNotes()); }
            set { _respiratory = value; }
        }

        private AbNormalAndNotes _selaIga;
        public AbNormalAndNotes SelaIga
        {
            get { return _selaIga ?? (_selaIga = new AbNormalAndNotes()); }
            set { _selaIga = value; }
        }
    }

    public class PalpasiParu
    {
        private AbNormalAndNotes _fremitus;
        public AbNormalAndNotes Fremitus
        {
            get { return _fremitus ?? (_fremitus = new AbNormalAndNotes()); }
            set { _fremitus = value; }
        }

        private ExistAndNotes _nyeriTekan;
        public ExistAndNotes NyeriTekan
        {
            get { return _nyeriTekan ?? (_nyeriTekan = new ExistAndNotes()); }
            set { _nyeriTekan = value; }
        }

        private ExistAndNotes _krepitasi;
        public ExistAndNotes Krepitasi
        {
            get { return _krepitasi ?? (_krepitasi = new ExistAndNotes()); }
            set { _krepitasi = value; }
        }
    }

    public class ConditionAndLocation
    {
        public string Condition { get; set; }
        public string Location { get; set; }
    }
}
