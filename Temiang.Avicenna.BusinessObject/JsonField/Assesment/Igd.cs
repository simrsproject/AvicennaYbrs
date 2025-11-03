using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class Igd : BaseJsonField
    {
        #region Survei Primary
        private QuestionGroupAnswerValue _interventionPrehospital;
        [JsonProperty("InterventionPrehospital", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue InterventionPrehospital
        {
            get
            {
                if (_interventionPrehospital == null)
                {
                    _interventionPrehospital = _interventionPrehospital = new QuestionGroupAnswerValue();
                    _interventionPrehospital.QuestionGroupID = "IGD.IPH";
                }
                return _interventionPrehospital;

            }
            set { _interventionPrehospital = value; }
        }

        private QuestionGroupAnswerValue _jalanNapas;
        [JsonProperty("JalanNapas", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue JalanNapas
        {
            get
            {
                if (_jalanNapas == null)
                {
                    _jalanNapas = _jalanNapas = new QuestionGroupAnswerValue();
                    _jalanNapas.QuestionGroupID = "IGD.PS.JNP";
                }
                return _jalanNapas;

            }
            set { _jalanNapas = value; }
        }

        private QuestionGroupAnswerValue _pernapasan;
        [JsonProperty("Pernapasan", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue Pernapasan
        {
            get
            {
                if (_pernapasan == null)
                {
                    _pernapasan = _pernapasan = new QuestionGroupAnswerValue();
                    _pernapasan.QuestionGroupID = "IGD.PS.PNP";
                }
                return _pernapasan;

            }
            set { _pernapasan = value; }
        }

        private QuestionGroupAnswerValue _sirkulasi;
        [JsonProperty("Sirkulasi", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue Sirkulasi
        {
            get
            {
                if (_sirkulasi == null)
                {
                    _sirkulasi = _sirkulasi = new QuestionGroupAnswerValue();
                    _sirkulasi.QuestionGroupID = "IGD.PS.SIR";
                }
                return _sirkulasi;

            }
            set { _sirkulasi = value; }
        }

        private QuestionGroupAnswerValue _penilaianBayi;
        [JsonProperty("PenilaianBayi", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue PenilaianBayi
        {
            get
            {
                if (_penilaianBayi == null)
                {
                    _penilaianBayi = _penilaianBayi = new QuestionGroupAnswerValue();
                    _penilaianBayi.QuestionGroupID = "IGD.PS.PBY";
                }
                return _penilaianBayi;

            }
            set { _penilaianBayi = value; }
        }

        private QuestionGroupAnswerValue _disabilitas;
        [JsonProperty("Disabilitas", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue Disabilitas
        {
            get
            {
                if (_disabilitas == null)
                {
                    _disabilitas = _disabilitas = new QuestionGroupAnswerValue();
                    _disabilitas.QuestionGroupID = "IGD.PS.DSB";
                }
                return _disabilitas;

            }
            set { _disabilitas = value; }
        }

        private QuestionGroupAnswerValue _eksposur;
        [JsonProperty("Eksposur", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue Eksposur
        {
            get
            {
                if (_eksposur == null)
                {
                    _eksposur = _eksposur = new QuestionGroupAnswerValue();
                    _eksposur.QuestionGroupID = "IGD.PS.EXP";
                }
                return _eksposur;

            }
            set { _eksposur = value; }
        }

        private AbNormalAndNotes _Paten;
        public AbNormalAndNotes Paten
        {
            get { return _Paten ?? (_Paten = new AbNormalAndNotes()); }
            set { _Paten = value; }
        }

        private AbNormalAndNotes _ObsPartial;
        public AbNormalAndNotes ObsPartial
        {
            get { return _ObsPartial ?? (_ObsPartial = new AbNormalAndNotes()); }
            set { _ObsPartial = value; }
        }

        private AbNormalAndNotes _ObsTotal;
        public AbNormalAndNotes ObsTotal
        {
            get { return _ObsTotal ?? (_ObsTotal = new AbNormalAndNotes()); }
            set { _ObsTotal = value; }
        }

        private AbNormalAndNotes _Trauma;
        public AbNormalAndNotes Trauma
        {
            get { return _Trauma ?? (_Trauma = new AbNormalAndNotes()); }
            set { _Trauma = value; }
        }

        private AbNormalAndNotes _Resiko;
        public AbNormalAndNotes Resiko
        {
            get { return _Resiko ?? (_Resiko = new AbNormalAndNotes()); }
            set { _Resiko = value; }
        }

        private AbNormalAndNotes _BendaAsing;
        public AbNormalAndNotes BendaAsing
        {
            get { return _BendaAsing ?? (_BendaAsing = new AbNormalAndNotes()); }
            set { _BendaAsing = value; }
        }

        private AbNormalAndNotes _Kesimpulan;
        public AbNormalAndNotes Kesimpulan
        {
            get { return _Kesimpulan ?? (_Kesimpulan = new AbNormalAndNotes()); }
            set { _Kesimpulan = value; }
        }



        #endregion

        #region Survey Sekunder
        [JsonProperty("Condition", NullValueHandling = NullValueHandling.Ignore)]
        public string Condition { get; set; }

        private Gcs _consciousness;

        [JsonProperty("Consciousness", NullValueHandling = NullValueHandling.Ignore)]
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }



        private QuestionGroupAnswerValue _kepalaLeher;
        [JsonProperty("KepalaLeher", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue KepalaLeher
        {
            get
            {
                if (_kepalaLeher == null)
                {
                    _kepalaLeher = _kepalaLeher = new QuestionGroupAnswerValue();
                    _kepalaLeher.QuestionGroupID = "IGD.SS.KLH";
                }
                return _kepalaLeher;

            }
            set { _kepalaLeher = value; }
        }

        private QuestionGroupAnswerValue _thorax;
        [JsonProperty("Thorax", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue Thorax
        {
            get
            {
                if (_thorax == null)
                {
                    _thorax = _thorax = new QuestionGroupAnswerValue();
                    _thorax.QuestionGroupID = "IGD.SS.TRX";
                }
                return _thorax;

            }
            set { _thorax = value; }
        }

        private QuestionGroupAnswerValue _abdomenPelvis;
        [JsonProperty("AbdomenPelvis", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue AbdomenPelvis
        {
            get
            {
                if (_abdomenPelvis == null)
                {
                    _abdomenPelvis = _abdomenPelvis = new QuestionGroupAnswerValue();
                    _abdomenPelvis.QuestionGroupID = "IGD.SS.ABP";
                }
                return _abdomenPelvis;

            }
            set { _abdomenPelvis = value; }
        }

        private QuestionGroupAnswerValue _ancillaryExam;
        [JsonProperty("AncillaryExam", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue AncillaryExam
        {
            get
            {
                if (_ancillaryExam == null)
                {
                    _ancillaryExam = _ancillaryExam = new QuestionGroupAnswerValue();
                    _ancillaryExam.QuestionGroupID = "IGD.SS.AEX";
                }
                return _ancillaryExam;

            }
            set { _ancillaryExam = value; }
        }

        private QuestionGroupAnswerValue _others;
        [JsonProperty("Others", NullValueHandling = NullValueHandling.Ignore)]
        public QuestionGroupAnswerValue Others
        {
            get
            {
                if (_others == null)
                {
                    _others = _others = new QuestionGroupAnswerValue();
                    _others.QuestionGroupID = "IGD.SS.OTH";
                }
                return _others;

            }
            set { _others = value; }
        }

        private AbNormalAndNotes _Kepala;
        public AbNormalAndNotes Kepala
        {
            get { return _Kepala ?? (_Kepala = new AbNormalAndNotes()); }
            set { _Kepala = value; }
        }

        private AbNormalAndNotes _Konjungtiva;
        public AbNormalAndNotes Konjungtiva
        {
            get { return _Konjungtiva ?? (_Konjungtiva = new AbNormalAndNotes()); }
            set { _Konjungtiva = value; }
        }

        private AbNormalAndNotes _Sklera;
        public AbNormalAndNotes Sklera
        {
            get { return _Sklera ?? (_Sklera = new AbNormalAndNotes()); }
            set { _Sklera = value; }
        }

        private AbNormalAndNotes _BibirLidah;
        public AbNormalAndNotes BibirLidah
        {
            get { return _BibirLidah ?? (_BibirLidah = new AbNormalAndNotes()); }
            set { _BibirLidah = value; }
        }

        private AbNormalAndNotes _Mukosa;
        public AbNormalAndNotes Mukosa
        {
            get { return _Mukosa ?? (_Mukosa = new AbNormalAndNotes()); }
            set { _Mukosa = value; }
        }

        private AbNormalAndNotes _Mata;
        public AbNormalAndNotes Mata
        {
            get { return _Mata ?? (_Mata = new AbNormalAndNotes()); }
            set { _Mata = value; }
        }

        private AbNormalAndNotes _KondisiKepala;
        public AbNormalAndNotes KondisiKepala
        {
            get { return _KondisiKepala ?? (_KondisiKepala = new AbNormalAndNotes()); }
            set { _KondisiKepala = value; }
        }

        private AbNormalAndNotes _Leher;
        public AbNormalAndNotes Leher
        {
            get { return _Leher ?? (_Leher = new AbNormalAndNotes()); }
            set { _Leher = value; }
        }

        private AbNormalAndNotes _Trakea;
        public AbNormalAndNotes Trakea
        {
            get { return _Trakea ?? (_Trakea = new AbNormalAndNotes()); }
            set { _Trakea = value; }
        }

        private AbNormalAndNotes _Jvp;
        public AbNormalAndNotes Jvp
        {
            get { return _Jvp ?? (_Jvp = new AbNormalAndNotes()); }
            set { _Jvp = value; }
        }

        private AbNormalAndNotes _LNN;
        public AbNormalAndNotes LNN
        {
            get { return _LNN ?? (_LNN = new AbNormalAndNotes()); }
            set { _LNN = value; }
        }

        private AbNormalAndNotes _Tiroid;
        public AbNormalAndNotes Tiroid
        {
            get { return _Tiroid ?? (_Tiroid = new AbNormalAndNotes()); }
            set { _Tiroid = value; }
        }

        private AbNormalAndNotes _KondisiLeher;
        public AbNormalAndNotes KondisiLeher
        {
            get { return _KondisiLeher ?? (_KondisiLeher = new AbNormalAndNotes()); }
            set { _KondisiLeher = value; }
        }

        private AbNormalAndNotes _Thorax2;
        public AbNormalAndNotes Thorax2
        {
            get { return _Thorax2 ?? (_Thorax2 = new AbNormalAndNotes()); }
            set { _Thorax2 = value; }
        }

        private AbNormalAndNotes _Jantung;
        public AbNormalAndNotes Jantung
        {
            get { return _Jantung ?? (_Jantung = new AbNormalAndNotes()); }
            set { _Jantung = value; }
        }

        private AbNormalAndNotes _Paru;
        public AbNormalAndNotes Paru
        {
            get { return _Paru ?? (_Paru = new AbNormalAndNotes()); }
            set { _Paru = value; }
        }

        private AbNormalAndNotes _Abdomen2;
        public AbNormalAndNotes Abdomen2
        {
            get { return _Abdomen2 ?? (_Abdomen2 = new AbNormalAndNotes()); }
            set { _Abdomen2 = value; }
        }

        private AbNormalAndNotes _Punggung;
        public AbNormalAndNotes Punggung
        {
            get { return _Punggung ?? (_Punggung = new AbNormalAndNotes()); }
            set { _Punggung = value; }
        }

        private AbNormalAndNotes _Ekstremitas;
        public AbNormalAndNotes Ekstremitas
        {
            get { return _Ekstremitas ?? (_Ekstremitas = new AbNormalAndNotes()); }
            set { _Ekstremitas = value; }
        }

        private AbNormalAndNotes _Genitalia;
        public AbNormalAndNotes Genitalia
        {
            get { return _Genitalia ?? (_Genitalia = new AbNormalAndNotes()); }
            set { _Genitalia = value; }
        }

        #endregion

        private Triage5Level _triage;
        [JsonProperty("Triage", NullValueHandling = NullValueHandling.Ignore)]
        public Triage5Level Triage
        {
            get { return _triage ?? (_triage = new Triage5Level()); }
            set { _triage = value; }
        }


        private Flacc _flacc;
        [JsonProperty("Flacc", NullValueHandling = NullValueHandling.Ignore)]
        public Flacc Flacc
        {
            get { return _flacc ?? (_flacc = new Flacc()); }
            set { _flacc = value; }
        }

        private Esi _esi;
        [JsonProperty("Esi", NullValueHandling = NullValueHandling.Ignore)]
        public Esi Esi
        {
            get { return _esi ?? (_esi = new Esi()); }
            set { _esi = value; }
        }

        private AbNormalAndNotes2 _head;
        [JsonProperty("Head", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndNotes2 Head
        {
            get { return _head ?? (_head = new AbNormalAndNotes2()); }
            set { _head = value; }
        }


        private AbNormalAndReason _eye;
        [JsonProperty("Eye", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndReason Eye
        {
            get { return _eye ?? (_eye = new AbNormalAndReason()); }
            set { _eye = value; }
        }

        private AbNormalAndNotes2 _neck;
        [JsonProperty("Neck", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndNotes2 Neck
        {
            get { return _neck ?? (_neck = new AbNormalAndNotes2()); }
            set { _neck = value; }
        }

        private AbNormalAndReason _pulmo;
        [JsonProperty("Pulmo", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndReason Pulmo
        {
            get { return _pulmo ?? (_pulmo = new AbNormalAndReason()); }
            set { _pulmo = value; }
        }


        private AbNormalAndReason _cor;
        [JsonProperty("Cor", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndReason Cor
        {
            get { return _cor ?? (_cor = new AbNormalAndReason()); }
            set { _cor = value; }
        }

        private AbNormalAndReason _abd;
        [JsonProperty("Abd", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndReason Abdomen
        {
            get { return _abd ?? (_abd = new AbNormalAndReason()); }
            set { _abd = value; }
        }

        private AbNormalAndReason _ext;
        [JsonProperty("Ext", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndReason Extremity
        {
            get { return _ext ?? (_ext = new AbNormalAndReason()); }
            set { _ext = value; }
        }

        private AbNormalAndNotes2 _skin;
        [JsonProperty("Skin", NullValueHandling = NullValueHandling.Ignore)]
        public AbNormalAndNotes2 Skin
        {
            get { return _skin ?? (_skin = new AbNormalAndNotes2()); }
            set { _skin = value; }
        }

        public string Notes { get; set; }
        public string NutritionSkrinning { get; set; }
    }
}
