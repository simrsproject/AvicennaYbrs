using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class InternalRos : BaseJsonField
    {
        public class RosCode
        {
            public class Umum
            {
                public const string Mengigil = "APD.TS0101";
                public const string PenurunanBB = "APD.TS0102";
                public const string Demam = "APD.TS0103";
                public const string KeringatMalam = "APD.TS0104";
                public const string CepatLelah = "APD.TS0105";
            }
            public class Mata
            {
                public const string PerubahanVisual = "APD.TS0201";
                public const string Nyeri = "APD.TS0202";
                public const string Merah = "APD.TS0203";
            }
            public class Tht
            {
                public const string NyeriKepala = "APD.TS0301";
                public const string SuaraSerak = "APD.TS0302";
                public const string NyeriTelan = "APD.TS0303";
                public const string Epistaksis = "APD.TS0304";
                public const string SulitMenelan = "APD.TS0305";
                public const string PendengaranTurun = "APD.TS0306";
                public const string Tinnitus = "APD.TS0307";
            }
            public class Cardiovas
            {
                public const string SakitDada = "APD.TS0401";
                public const string Edema = "APD.TS0402";
                public const string PND = "APD.TS0403";
                public const string Orthopnea = "APD.TS0404";
                public const string Palpitasi = "APD.TS0405";
                public const string Klaudikasi = "APD.TS0406";
            }
            public class Respirasi
            {
                public const string Batuk = "APD.TS0501";
                public const string SOB = "APD.TS0502";
                public const string Mengi = "APD.TS0503";
            }
            public class Gastrointestinal
            {
                public const string NyeriPerut = "APD.TS0601";
                public const string PerubahanPolaBAB = "APD.TS0602";
                public const string MualMuntah = "APD.TS0603";
                public const string Diare = "APD.TS0604";
                public const string Heartburn = "APD.TS0605";
                public const string HemetamesisMelena = "APD.TS0606";
            }
        }


        private QuestionGroupAnswerValue _umum;
        public QuestionGroupAnswerValue Umum
        {
            get
            {
                if (_umum == null)
                {
                    _umum = _umum = new QuestionGroupAnswerValue();
                    _umum.QuestionGroupID = "APD.TS01";
                }
                return _umum;

            }
            set { _umum = value; }
        }

        private QuestionGroupAnswerValue _mata;
        public QuestionGroupAnswerValue Mata
        {
            get
            {
                if (_mata == null)
                {
                    _mata = _mata = new QuestionGroupAnswerValue();
                    _mata.QuestionGroupID = "APD.TS02";
                }
                return _mata;

            }
            set { _mata = value; }
        }

        private QuestionGroupAnswerValue _tht;
        public QuestionGroupAnswerValue Tht
        {
            get
            {
                if (_tht == null)
                {
                    _tht = _tht = new QuestionGroupAnswerValue();
                    _tht.QuestionGroupID = "APD.TS03";
                }
                return _tht;

            }
            set { _tht = value; }
        }

        private QuestionGroupAnswerValue _cardiovas;
        public QuestionGroupAnswerValue Cardiovas
        {
            get
            {
                if (_cardiovas == null)
                {
                    _cardiovas = _cardiovas = new QuestionGroupAnswerValue();
                    _cardiovas.QuestionGroupID = "APD.TS04";
                }
                return _cardiovas;

            }
            set { _cardiovas = value; }
        }

        private QuestionGroupAnswerValue _respirasi;
        public QuestionGroupAnswerValue Respirasi
        {
            get
            {
                if (_respirasi == null)
                {
                    _respirasi = _respirasi = new QuestionGroupAnswerValue();
                    _respirasi.QuestionGroupID = "APD.TS05";
                }
                return _respirasi;

            }
            set { _respirasi = value; }
        }

        private QuestionGroupAnswerValue _gastrointestinal;
        public QuestionGroupAnswerValue Gastrointestinal
        {
            get
            {
                if (_gastrointestinal == null)
                {
                    _gastrointestinal = _gastrointestinal = new QuestionGroupAnswerValue();
                    _gastrointestinal.QuestionGroupID = "APD.TS06";
                }
                return _gastrointestinal;

            }
            set { _gastrointestinal = value; }
        }

        private QuestionGroupAnswerValue _saluranKencing;
        public QuestionGroupAnswerValue SaluranKencing
        {
            get
            {
                if (_saluranKencing == null)
                {
                    _saluranKencing = _saluranKencing = new QuestionGroupAnswerValue();
                    _saluranKencing.QuestionGroupID = "APD.TS07";
                }
                return _saluranKencing;

            }
            set { _saluranKencing = value; }
        }

        private QuestionGroupAnswerValue _muscle;
        public QuestionGroupAnswerValue Muscle
        {
            get
            {
                if (_muscle == null)
                {
                    _muscle = _muscle = new QuestionGroupAnswerValue();
                    _muscle.QuestionGroupID = "APD.TS08";
                }
                return _muscle;

            }
            set { _muscle = value; }
        }

        private QuestionGroupAnswerValue _hematologi;
        public QuestionGroupAnswerValue Hematologi
        {
            get
            {
                if (_hematologi == null)
                {
                    _hematologi = _hematologi = new QuestionGroupAnswerValue();
                    _hematologi.QuestionGroupID = "APD.TS09";
                }
                return _hematologi;

            }
            set { _hematologi = value; }
        }

        private QuestionGroupAnswerValue _endokrin;
        public QuestionGroupAnswerValue Endokrin
        {
            get
            {
                if (_endokrin == null)
                {
                    _endokrin = _endokrin = new QuestionGroupAnswerValue();
                    _endokrin.QuestionGroupID = "APD.TS10";
                }
                return _endokrin;

            }
            set { _endokrin = value; }
        }

        private QuestionGroupAnswerValue _dermatologi;
        public QuestionGroupAnswerValue Dermatologi
        {
            get
            {
                if (_dermatologi == null)
                {
                    _dermatologi = _dermatologi = new QuestionGroupAnswerValue();
                    _dermatologi.QuestionGroupID = "APD.TS11";
                }
                return _dermatologi;

            }
            set { _dermatologi = value; }
        }

        private QuestionGroupAnswerValue _neurologi;
        public QuestionGroupAnswerValue Neurologi
        {
            get
            {
                if (_neurologi == null)
                {
                    _neurologi = _neurologi = new QuestionGroupAnswerValue();
                    _neurologi.QuestionGroupID = "APD.TS12";
                }
                return _neurologi;

            }
            set { _neurologi = value; }
        }

        private QuestionGroupAnswerValue _psikiatri;
        public QuestionGroupAnswerValue Psikiatri
        {
            get
            {
                if (_psikiatri == null)
                {
                    _psikiatri = _psikiatri = new QuestionGroupAnswerValue();
                    _psikiatri.QuestionGroupID = "APD.TS13";
                }
                return _psikiatri;

            }
            set { _psikiatri = value; }
        }

        private QuestionGroupAnswerValue _sistemPernafasan;
        public QuestionGroupAnswerValue SistemPernafasan
        {
            get
            {
                if (_sistemPernafasan == null)
                {
                    _sistemPernafasan = _sistemPernafasan = new QuestionGroupAnswerValue();
                    _sistemPernafasan.QuestionGroupID = "APD.TS14";
                }
                return _sistemPernafasan;

            }
            set { _sistemPernafasan = value; }
        }

        private QuestionGroupAnswerValue _sistemKardiovaskular;
        public QuestionGroupAnswerValue SistemKardiovaskular
        {
            get
            {
                if (_sistemKardiovaskular == null)
                {
                    _sistemKardiovaskular = _sistemKardiovaskular = new QuestionGroupAnswerValue();
                    _sistemKardiovaskular.QuestionGroupID = "APD.TS15";
                }
                return _sistemKardiovaskular;

            }
            set { _sistemKardiovaskular = value; }
        }

        private QuestionGroupAnswerValue _sistemPersyarafan;
        public QuestionGroupAnswerValue SistemPersyarafan
        {
            get
            {
                if (_sistemPersyarafan == null)
                {
                    _sistemPersyarafan = _sistemPersyarafan = new QuestionGroupAnswerValue();
                    _sistemPersyarafan.QuestionGroupID = "APD.TS16";
                }
                return _sistemPersyarafan;

            }
            set { _sistemPersyarafan = value; }
        }

        private QuestionGroupAnswerValue _sistemEkskresi;
        public QuestionGroupAnswerValue SistemEkskresi
        {
            get
            {
                if (_sistemEkskresi == null)
                {
                    _sistemEkskresi = _sistemEkskresi = new QuestionGroupAnswerValue();
                    _sistemEkskresi.QuestionGroupID = "APD.TS17";
                }
                return _sistemEkskresi;

            }
            set { _sistemEkskresi = value; }
        }

        private QuestionGroupAnswerValue _sistemPencernaan;
        public QuestionGroupAnswerValue SistemPencernaan
        {
            get
            {
                if (_sistemPencernaan == null)
                {
                    _sistemPencernaan = _sistemPencernaan = new QuestionGroupAnswerValue();
                    _sistemPencernaan.QuestionGroupID = "APD.TS18";
                }
                return _sistemPencernaan;

            }
            set { _sistemPencernaan = value; }
        }

        private QuestionGroupAnswerValue _sistemMuskuloskeletal;
        public QuestionGroupAnswerValue SistemMuskuloskeletal
        {
            get
            {
                if (_sistemMuskuloskeletal == null)
                {
                    _sistemMuskuloskeletal = _sistemMuskuloskeletal = new QuestionGroupAnswerValue();
                    _sistemMuskuloskeletal.QuestionGroupID = "APD.TS19";
                }
                return _sistemMuskuloskeletal;

            }
            set { _sistemMuskuloskeletal = value; }
        }

        private QuestionGroupAnswerValue _sistemReproduksi;
        public QuestionGroupAnswerValue SistemReproduksi
        {
            get
            {
                if (_sistemReproduksi == null)
                {
                    _sistemReproduksi = _sistemReproduksi = new QuestionGroupAnswerValue();
                    _sistemReproduksi.QuestionGroupID = "APD.TS20";
                }
                return _sistemReproduksi;

            }
            set { _sistemReproduksi = value; }
        }

        private QuestionGroupAnswerValue _dataPsikoSosiSpi;
        public QuestionGroupAnswerValue DataPsikoSosiSpi
        {
            get
            {
                if (_dataPsikoSosiSpi == null)
                {
                    _dataPsikoSosiSpi = _dataPsikoSosiSpi = new QuestionGroupAnswerValue();
                    _dataPsikoSosiSpi.QuestionGroupID = "APD.TS21";
                }
                return _dataPsikoSosiSpi;

            }
            set { _dataPsikoSosiSpi = value; }
        }

        private QuestionGroupAnswerValue _hambatanDiri;
        public QuestionGroupAnswerValue HambatanDiri
        {
            get
            {
                if (_hambatanDiri == null)
                {
                    _hambatanDiri = _hambatanDiri = new QuestionGroupAnswerValue();
                    _hambatanDiri.QuestionGroupID = "APD.TS21";
                }
                return _hambatanDiri;

            }
            set { _hambatanDiri = value; }
        }
    }

}
