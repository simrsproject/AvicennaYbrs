using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class NeurologiPe : BaseJsonField
    {
        /// <summary>
        /// Entry control untuk asesmen Neorolgy
        /// Tanpa GCS tapi dg tambahan Pysical Examination General 
        /// </summary>
        /// Create By: Fajri
        /// Create Date: 2023-March-16
        /// Client Req: RSYS
        /// ----------------------------------------------------
        private AbNormalAndNotes _kepala;
        public AbNormalAndNotes Kepala
        {
            get { return _kepala ?? (_kepala = new AbNormalAndNotes()); }
            set { _kepala = value; }
        }

        //private AbNormalAndNotes _Nervus;
        //public AbNormalAndNotes Nervus
        //{
        //    get { return _Nervus ?? (_Nervus = new AbNormalAndNotes()); }
        //    set { _Nervus = value; }
        //}

        private StatusMotorikTest _right;
        public StatusMotorikTest Right
        {
            get { return _right ?? (_right = new StatusMotorikTest()); }
            set { _right = value; }
        }

        private StatusMotorikTest _left;
        public StatusMotorikTest Left
        {
            get { return _left ?? (_left = new StatusMotorikTest()); }
            set { _left = value; }
        }

        private AbNormalAndNotes2 _NervusOlfaktoris;
        public AbNormalAndNotes2 NervusOlfaktoris
        {
            get { return _NervusOlfaktoris ?? (_NervusOlfaktoris = new AbNormalAndNotes2()); }
            set { _NervusOlfaktoris = value; }
        }

        private AbNormalAndNotes2 _NervusOptikus;
        public AbNormalAndNotes2 NervusOptikus
        {
            get { return _NervusOptikus ?? (_NervusOptikus = new AbNormalAndNotes2()); }
            set { _NervusOptikus = value; }
        }

        private AbNormalAndNotes2 _NervusOkulomotoris;
        public AbNormalAndNotes2 NervusOkulomotoris
        {
            get { return _NervusOkulomotoris ?? (_NervusOkulomotoris = new AbNormalAndNotes2()); }
            set { _NervusOkulomotoris = value; }
        }

        private AbNormalAndNotes2 _NervusTroklear;
        public AbNormalAndNotes2 NervusTroklear
        {
            get { return _NervusTroklear ?? (_NervusTroklear = new AbNormalAndNotes2()); }
            set { _NervusTroklear = value; }
        }

        private AbNormalAndNotes2 _NervusTrigeminus;
        public AbNormalAndNotes2 NervusTrigeminus
        {
            get { return _NervusTrigeminus ?? (_NervusTrigeminus = new AbNormalAndNotes2()); }
            set { _NervusTrigeminus = value; }
        }

        private AbNormalAndNotes2 _NervusAbducens;
        public AbNormalAndNotes2 NervusAbducens
        {
            get { return _NervusAbducens ?? (_NervusAbducens = new AbNormalAndNotes2()); }
            set { _NervusAbducens = value; }
        }

        private AbNormalAndNotes2 _NervusFasialis;
        public AbNormalAndNotes2 NervusFasialis
        {
            get { return _NervusFasialis ?? (_NervusFasialis = new AbNormalAndNotes2()); }
            set { _NervusFasialis = value; }
        }

        private AbNormalAndNotes2 _NervusVestibukokhlearis;
        public AbNormalAndNotes2 NervusVestibukokhlearis
        {
            get { return _NervusVestibukokhlearis ?? (_NervusVestibukokhlearis = new AbNormalAndNotes2()); }
            set { _NervusVestibukokhlearis = value; }
        }


        private AbNormalAndNotes2 _NervusGlossofaringeal;
        public AbNormalAndNotes2 NervusGlossofaringeal
        {
            get { return _NervusGlossofaringeal ?? (_NervusGlossofaringeal = new AbNormalAndNotes2()); }
            set { _NervusGlossofaringeal = value; }
        }


        private AbNormalAndNotes2 _NervusVagus;
        public AbNormalAndNotes2 NervusVagus
        {
            get { return _NervusVagus ?? (_NervusVagus = new AbNormalAndNotes2()); }
            set { _NervusVagus = value; }
        }


        private AbNormalAndNotes2 _NervusAsesoris;
        public AbNormalAndNotes2 NervusAsesoris
        {
            get { return _NervusAsesoris ?? (_NervusAsesoris = new AbNormalAndNotes2()); }
            set { _NervusAsesoris = value; }
        }

        private AbNormalAndNotes2 _NervusHipoglossus;
        public AbNormalAndNotes2 NervusHipoglossus
        {
            get { return _NervusHipoglossus ?? (_NervusHipoglossus = new AbNormalAndNotes2()); }
            set { _NervusHipoglossus = value; }
        }


        private AbNormalAndNotes _mata;
        public AbNormalAndNotes Mata
        {
            get { return _mata ?? (_mata = new AbNormalAndNotes()); }
            set { _mata = value; }
        }

        private ExistAndNotes _colorBlind;
        public ExistAndNotes ColorBlind
        {
            get { return _colorBlind ?? (_colorBlind = new ExistAndNotes()); }
            set { _colorBlind = value; }
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
        private AbNormalAndNotes _auskulatasi;
        public AbNormalAndNotes Auskulatasi
        {
            get { return _auskulatasi ?? (_auskulatasi = new AbNormalAndNotes()); }
            set { _auskulatasi = value; }
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

        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        public string Neurologis { get; set; }

        public string Notes { get; set; }
        public string OtherNotes { get; set; }

        private Meningeal _meningeal;
        [JsonProperty("Meningeal", NullValueHandling = NullValueHandling.Ignore)]
        public Meningeal Meningeal
        {
            get { return _meningeal ?? (_meningeal = new Meningeal()); }
            set { _meningeal = value; }
        }

        private Funduscopy _Funduscopy;
        [JsonProperty("Funduscopy", NullValueHandling = NullValueHandling.Ignore)]
        public Funduscopy Funduscopy
        {
            get { return _Funduscopy ?? (_Funduscopy = new Funduscopy()); }
            set { _Funduscopy = value; }
        }

        [JsonProperty("Cranialis", NullValueHandling = NullValueHandling.Ignore)]
        public string Cranialis { get; set; }


        private Motorik _Motorik;
        [JsonProperty("Motorik", NullValueHandling = NullValueHandling.Ignore)]
        public Motorik Motorik
        {
            get { return _Motorik ?? (_Motorik = new Motorik()); }
            set { _Motorik = value; }
        }

        private Motorik _motorikR;
        [JsonProperty("MotorikR", NullValueHandling = NullValueHandling.Ignore)]
        public Motorik MotorikRight
        {
            get { return _motorikR ?? (_motorikR = new Motorik()); }
            set { _motorikR = value; }
        }

        private Refleks _Refleks;
        [JsonProperty("Refleks", NullValueHandling = NullValueHandling.Ignore)]
        public Refleks Refleks
        {
            get { return _Refleks ?? (_Refleks = new Refleks()); }
            set { _Refleks = value; }
        }

        [JsonProperty("StatOtonom", NullValueHandling = NullValueHandling.Ignore)]
        public string StatOtonom { get; set; }

        private AbNormalAndNotes _hepar;
        public AbNormalAndNotes Hepar
        {
            get { return _hepar ?? (_hepar = new AbNormalAndNotes()); }
            set { _hepar = value; }
        }

        private AbNormalAndNotes _lien;
        public AbNormalAndNotes Lien
        {
            get { return _lien ?? (_lien = new AbNormalAndNotes()); }
            set { _lien = value; }
        }
        private AbNormalAndNotes _reflexFis;
        public AbNormalAndNotes ReflexFis
        {
            get { return _reflexFis ?? (_reflexFis = new AbNormalAndNotes()); }
            set { _reflexFis = value; }
        }

        private AbNormalAndNotes _reflexPat;
        public AbNormalAndNotes ReflexPat
        {
            get { return _reflexPat ?? (_reflexPat = new AbNormalAndNotes()); }
            set { _reflexPat = value; }
        }

        private Pupils _pupils;
        [JsonProperty("Pupils", NullValueHandling = NullValueHandling.Ignore)]
        public Pupils Pupils
        {
            get { return _pupils ?? (_pupils = new Pupils()); }
            set { _pupils = value; }
        }

        private PupilRefleks _pupilRefleks;
        [JsonProperty("PupilRefleks", NullValueHandling = NullValueHandling.Ignore)]
        public PupilRefleks PupilRefleks
        {
            get { return _pupilRefleks ?? (_pupilRefleks = new PupilRefleks()); }
            set { _pupilRefleks = value; }
        }
        public bool IsTumor { get; set; }
        public bool IsHernia { get; set; }
        public bool IsHemorrhoids { get; set; }
        public string Visus { get; set; }
    }


    public class Meningeal
    {
        public bool? KakuKuduk { get; set; }
        public bool? Kernig { get; set; }
        public bool? Lasgque { get; set; }

    }

    public class Funduscopy
    {
        public bool? Papiledema { get; set; }
    }

    public class Motorik
    {
        public int? Superior { get; set; }
        public int? Interior { get; set; }

        public int? RSuperior { get; set; }
        public int? RInterior { get; set; }
        public int? LSuperior { get; set; }
        public int? LInterior { get; set; }
    }

    public class Refleks
    {
        public string Fisiologis { get; set; }
        public string Patologis { get; set; }
    }

    public class Pupils
    {
        public string PupilLeft { get; set; }

        public string PupilRight { get; set; }

    }
    public class PupilRefleks
    {
        public string PupilRefleksLeft { get; set; }

        public string PupilRefleksRight { get; set; }

    }

    public class StatusMotorikTest
    {
        public int? Superior { get; set; }
        public int? Interior { get; set; }

    }

}

