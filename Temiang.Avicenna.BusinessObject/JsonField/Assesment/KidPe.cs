using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class KidPe : BaseJsonField
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

        private PhysicalExamMetod _jantungPem;
        public PhysicalExamMetod JantungPem
        {
            get { return _jantungPem ?? (_jantungPem = new PhysicalExamMetod()); }
            set { _jantungPem = value; }
        }

        private PhysicalExamMetod _paruPem;
        public PhysicalExamMetod ParuPem
        {
            get { return _paruPem ?? (_paruPem = new PhysicalExamMetod()); }
            set { _paruPem = value; }
        }

        private PhysicalExamMetod _abdomenPem;
        public PhysicalExamMetod AbdomenPem
        {
            get { return _abdomenPem ?? (_abdomenPem = new PhysicalExamMetod()); }
            set { _abdomenPem = value; }
        }

        private AbNormalAndNotes _jantung;
        public AbNormalAndNotes Jantung
        {
            get { return _jantung ?? (_jantung = new AbNormalAndNotes()); }
            set { _jantung = value; }
        }

        private AbNormalAndNotes _jantungIrama;
        public AbNormalAndNotes JantungIrama
        {
            get { return _jantungIrama ?? (_jantungIrama = new AbNormalAndNotes()); }
            set { _jantungIrama = value; }
        }

        private AbNormalAndNotes _jantungBunyi;
        public AbNormalAndNotes JantungBunyi
        {
            get { return _jantungBunyi ?? (_jantungBunyi = new AbNormalAndNotes()); }
            set { _jantungBunyi = value; }
        }

        private AbNormalAndNotes _paru;
        public AbNormalAndNotes Paru
        {
            get { return _paru ?? (_paru = new AbNormalAndNotes()); }
            set { _paru = value; }
        }

        private AbNormalAndNotes _paruPergerakan;
        public AbNormalAndNotes ParuPergerakan
        {
            get { return _paruPergerakan ?? (_paruPergerakan = new AbNormalAndNotes()); }
            set { _paruPergerakan = value; }
        }

        private AbNormalAndNotes _paruPerkusi;
        public AbNormalAndNotes ParuPerkusi
        {
            get { return _paruPerkusi ?? (_paruPerkusi = new AbNormalAndNotes()); }
            set { _paruPerkusi = value; }
        }

        private AbNormalAndNotes _paruPernapasan;
        public AbNormalAndNotes ParuPernapasan
        {
            get { return _paruPernapasan ?? (_paruPernapasan = new AbNormalAndNotes()); }
            set { _paruPernapasan = value; }
        }

        private AbNormalAndNotes _paruRonchi;
        public AbNormalAndNotes ParuRonchi
        {
            get { return _paruRonchi ?? (_paruRonchi = new AbNormalAndNotes()); }
            set { _paruRonchi = value; }
        }

        private AbNormalAndNotes _paruWheezing;
        public AbNormalAndNotes ParuWheezing
        {
            get { return _paruWheezing ?? (_paruWheezing = new AbNormalAndNotes()); }
            set { _paruWheezing = value; }
        }

        private AbNormalAndNotes _abdomen;
        public AbNormalAndNotes Abdomen
        {
            get { return _abdomen ?? (_abdomen = new AbNormalAndNotes()); }
            set { _abdomen = value; }
        }

        private AbNormalAndNotes _abdomenKelainan;
        public AbNormalAndNotes AbdomenKelainan
        {
            get { return _abdomenKelainan ?? (_abdomenKelainan = new AbNormalAndNotes()); }
            set { _abdomenKelainan = value; }
        }

        private AbNormalAndNotes _abdomenBenjolan;
        public AbNormalAndNotes AbdomenBenjolan
        {
            get { return _abdomenBenjolan ?? (_abdomenBenjolan = new AbNormalAndNotes()); }
            set { _abdomenBenjolan = value; }
        }

        private AbNormalAndNotes _abdomenNyeriTekan;
        public AbNormalAndNotes AbdomenNyeriTekan
        {
            get { return _abdomenNyeriTekan ?? (_abdomenNyeriTekan = new AbNormalAndNotes()); }
            set { _abdomenNyeriTekan = value; }
        }

        private AbNormalAndNotes _abdomenHernia;
        public AbNormalAndNotes AbdomenHernia
        {
            get { return _abdomenHernia ?? (_abdomenHernia = new AbNormalAndNotes()); }
            set { _abdomenHernia = value; }
        }

        private AbNormalAndNotes _abdomenBisingUsus;
        public AbNormalAndNotes AbdomenBisingUsus
        {
            get { return _abdomenBisingUsus ?? (_abdomenBisingUsus = new AbNormalAndNotes()); }
            set { _abdomenBisingUsus = value; }
        }

        private AbNormalAndNotes _abdomenDistensi;
        public AbNormalAndNotes AbdomenDistensi
        {
            get { return _abdomenDistensi ?? (_abdomenDistensi = new AbNormalAndNotes()); }
            set { _abdomenDistensi = value; }
        }

        private AbNormalAndNotes _spineLimb;
        public AbNormalAndNotes SpineLimb
        {
            get { return _spineLimb ?? (_spineLimb = new AbNormalAndNotes()); }
            set { _spineLimb = value; }
        }

        private AbNormalAndNotes _genitaliaAndAnus;
        public AbNormalAndNotes GenitaliaAndAnus
        {
            get { return _genitaliaAndAnus ?? (_genitaliaAndAnus = new AbNormalAndNotes()); }
            set { _genitaliaAndAnus = value; }
        }

        private AbNormalAndNotes _genitaliaPenis;
        public AbNormalAndNotes GenitaliaPenis
        {
            get { return _genitaliaPenis ?? (_genitaliaPenis = new AbNormalAndNotes()); }
            set { _genitaliaPenis = value; }
        }

        private AbNormalAndNotes _genitaliaTestis;
        public AbNormalAndNotes GenitaliaTestis
        {
            get { return _genitaliaTestis ?? (_genitaliaTestis = new AbNormalAndNotes()); }
            set { _genitaliaTestis = value; }
        }

        private AbNormalAndNotes _genitaliaLabiaMinor;
        public AbNormalAndNotes GenitaliaLabiaMinor
        {
            get { return _genitaliaLabiaMinor ?? (_genitaliaLabiaMinor = new AbNormalAndNotes()); }
            set { _genitaliaLabiaMinor = value; }
        }

        private AbNormalAndNotes _genitaliaAnus;
        public AbNormalAndNotes GenitaliaAnus
        {
            get { return _genitaliaAnus ?? (_genitaliaAnus = new AbNormalAndNotes()); }
            set { _genitaliaAnus = value; }
        }

        private AbNormalAndNotes _ekstremitas;
        public AbNormalAndNotes Ekstremitas
        {
            get { return _ekstremitas ?? (_ekstremitas = new AbNormalAndNotes()); }
            set { _ekstremitas = value; }
        }

        private AbNormalAndNotes _ekstremitasEdema;
        public AbNormalAndNotes EkstremitasEdema
        {
            get { return _ekstremitasEdema ?? (_ekstremitasEdema = new AbNormalAndNotes()); }
            set { _ekstremitasEdema = value; }
        }

        private AbNormalAndNotes _ekstremitasCrt;
        public AbNormalAndNotes EkstremitasCrt
        {
            get { return _ekstremitasCrt ?? (_ekstremitasCrt = new AbNormalAndNotes()); }
            set { _ekstremitasCrt = value; }
        }

        private AbNormalAndNotes _kulit;
        public AbNormalAndNotes Kulit
        {
            get { return _kulit ?? (_kulit = new AbNormalAndNotes()); }
            set { _kulit = value; }
        }

        private StatusNeurogis _statneuro;
        public StatusNeurogis StatusNeurogis
        {
            get { return _statneuro ?? (_statneuro = new StatusNeurogis()); }
            set { _statneuro = value; }
        }

        public string Other { get; set; }

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


    public class StatusNeurogis
    {
        public bool Grm { get; set; }

        private AbNormalAndNotes _fisiologis;
        public AbNormalAndNotes Fisiologis
        {
            get { return _fisiologis ?? (_fisiologis = new AbNormalAndNotes()); }
            set { _fisiologis = value; }
        }

        public bool Patologis { get; set; }

        private AbNormalAndNotes _motorik;
        public AbNormalAndNotes Motorik
        {
            get { return _motorik ?? (_motorik = new AbNormalAndNotes()); }
            set { _motorik = value; }
        }

       
    }
}
