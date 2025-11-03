using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Temiang.Avicenna.BusinessObject.JsonField.Assesment
{
    public class ThtPe : BaseJsonField
    {
        public string Condition { get; set; }

        private Gcs _consciousness;
        public Gcs Consciousness
        {
            get { return _consciousness ?? (_consciousness = new Gcs()); }
            set { _consciousness = value; }
        }

        private ThtTelinga _telinga;
        public ThtTelinga Telinga
        {
            get { return _telinga ?? (_telinga = new ThtTelinga()); }
            set { _telinga = value; }
        }
   

        private ThtHidung _hidung;
        public ThtHidung Hidung
        {
            get { return _hidung ?? (_hidung = new ThtHidung()); }
            set { _hidung = value; }
        }

        private ThtLarinxItem _larinx;
        public ThtLarinxItem Larinx
        {
            get { return _larinx ?? (_larinx = new ThtLarinxItem()); }
            set { _larinx = value; }
        }

        private ThtLeherItem _leher;
        public ThtLeherItem Leher
        {
            get { return _leher ?? (_leher = new ThtLeherItem()); }
            set { _leher = value; }
        }

        private ThtTenggorok _tenggorok;
        public ThtTenggorok Tenggorok
        {
            get { return _tenggorok ?? (_tenggorok = new ThtTenggorok()); }
            set { _tenggorok = value; }
        }

        private AbNormalAndNotes _KepalaLeher2;
        public AbNormalAndNotes KepalaLeher2
        {
            get { return _KepalaLeher2 ?? (_KepalaLeher2 = new AbNormalAndNotes()); }
            set { _KepalaLeher2 = value; }
        }

        private AbNormalAndNotes _Trakea2;
        public AbNormalAndNotes Trakea2
        {
            get { return _Trakea2 ?? (_Trakea2 = new AbNormalAndNotes()); }
            set { _Trakea2 = value; }
        }

        private AbNormalAndNotes _Nlglands;
        public AbNormalAndNotes Nlglands
        {
            get { return _Nlglands ?? (_Nlglands = new AbNormalAndNotes()); }
            set { _Nlglands = value; }
        }

        private AbNormalAndNotes _Esofagus2;
        public AbNormalAndNotes Esofagus2
        {
            get { return _Esofagus2 ?? (_Esofagus2 = new AbNormalAndNotes()); }
            set { _Esofagus2 = value; }
        }

        private AbNormalAndNotes _Bronkus2;
        public AbNormalAndNotes Bronkus2
        {
            get { return _Bronkus2 ?? (_Bronkus2 = new AbNormalAndNotes()); }
            set { _Bronkus2 = value; }
        }

        public string KepalaLeher { get; set; }
        public string Trakea { get; set; }
        public string Esofagus { get; set; }
        public string Bronkus { get; set; }
        public string NeckLymph { get; set; }
        public string Notes { get; set; }
        public string Koana { get; set; }
        public string Adenoid { get; set; }
        public string Mukosa { get; set; }
        public string TubaEustachius { get; set; }
    }

    #region Telinga
    public class ThtTelinga
    {
        private ThtTelingaItem _right;
        public ThtTelingaItem Right
        {
            get { return _right ?? (_right = new ThtTelingaItem()); }
            set { _right = value; }
        }

        private ThtTelingaItem _left;
        public ThtTelingaItem Left
        {
            get { return _left ?? (_left = new ThtTelingaItem()); }
            set { _left = value; }
        }

        private AbNormalAndNotes _Daun;
        public AbNormalAndNotes Daun
        {
            get { return _Daun ?? (_Daun = new AbNormalAndNotes()); }
            set { _Daun = value; }
        }

        private AbNormalAndNotes _Liang;
        public AbNormalAndNotes Liang
        {
            get { return _Liang ?? (_Liang = new AbNormalAndNotes()); }
            set { _Liang = value; }
        }

        private AbNormalAndNotes _Discharge;
        public AbNormalAndNotes Discharge
        {
            get { return _Discharge ?? (_Discharge = new AbNormalAndNotes()); }
            set { _Discharge = value; }
        }

        private AbNormalAndNotes _Serumen;
        public AbNormalAndNotes Serumen
        {
            get { return _Serumen ?? (_Serumen = new AbNormalAndNotes()); }
            set { _Serumen = value; }
        }

        private AbNormalAndNotes _Tympani;
        public AbNormalAndNotes Tympani
        {
            get { return _Tympani ?? (_Tympani = new AbNormalAndNotes()); }
            set { _Tympani = value; }
        }

        private AbNormalAndNotes _MiddleEar;
        public AbNormalAndNotes MiddleEar
        {
            get { return _MiddleEar ?? (_MiddleEar = new AbNormalAndNotes()); }
            set { _MiddleEar = value; }
        }

        private AbNormalAndNotes _Tumor;
        public AbNormalAndNotes Tumor
        {
            get { return _Tumor ?? (_Tumor = new AbNormalAndNotes()); }
            set { _Tumor = value; }
        }

        private AbNormalAndNotes _Mastoid;
        public AbNormalAndNotes Mastoid
        {
            get { return _Mastoid ?? (_Mastoid = new AbNormalAndNotes()); }
            set { _Mastoid = value; }
        }

        private AbNormalAndNotes _PreAurikula;
        public AbNormalAndNotes PreAurikula
        {
            get { return _PreAurikula ?? (_PreAurikula = new AbNormalAndNotes()); }
            set { _PreAurikula = value; }
        }

        private AbNormalAndNotes _PostAurikula;
        public AbNormalAndNotes PostAurikula
        {
            get { return _PostAurikula ?? (_PostAurikula = new AbNormalAndNotes()); }
            set { _PostAurikula = value; }
        }

        private AbNormalAndNotes _Hearing;
        public AbNormalAndNotes Hearing
        {
            get { return _Hearing ?? (_Hearing = new AbNormalAndNotes()); }
            set { _Hearing = value; }
        }

        private AbNormalAndNotes _Audiometri;
        public AbNormalAndNotes Audiometri
        {
            get { return _Audiometri ?? (_Audiometri = new AbNormalAndNotes()); }
            set { _Audiometri = value; }
        }

        private AbNormalAndNotes _Oae;
        public AbNormalAndNotes Oae
        {
            get { return _Oae ?? (_Oae = new AbNormalAndNotes()); }
            set { _Oae = value; }
        }

        private AbNormalAndNotes _Keseimbangan;
        public AbNormalAndNotes Keseimbangan
        {
            get { return _Keseimbangan ?? (_Keseimbangan = new AbNormalAndNotes()); }
            set { _Keseimbangan = value; }
        }
        public string Notes { get; set; }

    }
    public class ThtTelingaItem
    {
        public string Daun { get; set; }
        public string Liang { get; set; }
        public string Tympani { get; set; }
        public string MiddleEar { get; set; }
        public string PreAurikula { get; set; }
        public string PostAurikula { get; set; }
        public string Pendengaran { get; set; }
        public string Keseimbangan { get; set; }
        public string Hearing { get; set; }
        public string Discharge { get; set; }
        public string Tumor { get; set; }
        public string Mastoid { get; set; }
        public string Audiometri { get; set; }
        public string Oae { get; set; }
        public string Sekret { get; set; }
        public string Serumen { get; set; }
    }
#endregion

    #region Hidung
    public class ThtHidung
    {
        private ThtHidungItem _right;
        public ThtHidungItem Right
        {
            get { return _right ?? (_right = new ThtHidungItem()); }
            set { _right = value; }
        }

        private ThtHidungItem _left;
        public ThtHidungItem Left
        {
            get { return _left ?? (_left = new ThtHidungItem()); }
            set { _left = value; }
        }


        private AbNormalAndNotes _Luar;
        public AbNormalAndNotes Luar
        {
            get { return _Luar ?? (_Luar = new AbNormalAndNotes()); }
            set { _Luar = value; }
        }

        private AbNormalAndNotes _Kavum;
        public AbNormalAndNotes Kavum
        {
            get { return _Kavum ?? (_Kavum = new AbNormalAndNotes()); }
            set { _Kavum = value; }
        }

        private AbNormalAndNotes _Septum;
        public AbNormalAndNotes Septum
        {
            get { return _Septum ?? (_Septum = new AbNormalAndNotes()); }
            set { _Septum = value; }
        }

        private AbNormalAndNotes _DischargeNOSE;
        public AbNormalAndNotes DischargeNOSE
        {
            get { return _DischargeNOSE ?? (_DischargeNOSE = new AbNormalAndNotes()); }
            set { _DischargeNOSE = value; }
        }

        private AbNormalAndNotes _Mukosa;
        public AbNormalAndNotes Mukosa
        {
            get { return _Mukosa ?? (_Mukosa = new AbNormalAndNotes()); }
            set { _Mukosa = value; }
        }

        private AbNormalAndNotes _TumorNOSE;
        public AbNormalAndNotes TumorNOSE
        {
            get { return _TumorNOSE ?? (_TumorNOSE = new AbNormalAndNotes()); }
            set { _TumorNOSE = value; }
        }

        private AbNormalAndNotes _Konka;
        public AbNormalAndNotes Konka
        {
            get { return _Konka ?? (_Konka = new AbNormalAndNotes()); }
            set { _Konka = value; }
        }

        private AbNormalAndNotes _Sinus;
        public AbNormalAndNotes Sinus
        {
            get { return _Sinus ?? (_Sinus = new AbNormalAndNotes()); }
            set { _Sinus = value; }
        }

        private AbNormalAndNotes _Koana;
        public AbNormalAndNotes Koana
        {
            get { return _Koana ?? (_Koana = new AbNormalAndNotes()); }
            set { _Koana = value; }
        }

        private AbNormalAndNotes _NEndoskopi;
        public AbNormalAndNotes NEndoskopi
        {
            get { return _NEndoskopi ?? (_NEndoskopi = new AbNormalAndNotes()); }
            set { _NEndoskopi = value; }
        }

        public string Notes { get; set; }
    }
    public class ThtHidungItem
    {
        public string Test { get; set; }
        public string Luar { get; set; }
        public string Anterior { get; set; }
        public string Posterior { get; set; }
        public string Sinus { get; set; }
        public string Kavum { get; set; }
        public string Septum { get; set; }
        public string Discharge { get; set; }
        public string Mukosa { get; set; }
        public string Tumor { get; set; }
        public string Konka { get; set; }
        public string Koana { get; set; }
        public string Naso { get; set; }
        public string NEndoskopi { get; set; }
    }
#endregion

    #region Larinx

public class ThtLarinxItem
{
        private AbNormalAndNotes _Epiglotis2;
        public AbNormalAndNotes Epiglotis2
        {
            get { return _Epiglotis2 ?? (_Epiglotis2 = new AbNormalAndNotes()); }
            set { _Epiglotis2 = value; }
        }

        private AbNormalAndNotes _PVokal2;
        public AbNormalAndNotes PVokal2
        {
            get { return _PVokal2 ?? (_PVokal2 = new AbNormalAndNotes()); }
            set { _PVokal2 = value; }
        }

        private AbNormalAndNotes _PVentri2;
        public AbNormalAndNotes PVentri2
        {
            get { return _PVentri2 ?? (_PVentri2 = new AbNormalAndNotes()); }
            set { _PVentri2 = value; }
        }

        private AbNormalAndNotes _Aritenoid2;
        public AbNormalAndNotes Aritenoid2
        {
            get { return _Aritenoid2 ?? (_Aritenoid2 = new AbNormalAndNotes()); }
            set { _Aritenoid2 = value; }
        }

        private AbNormalAndNotes _PAriepiglotis2;
        public AbNormalAndNotes PAriepiglotis2
        {
            get { return _PAriepiglotis2 ?? (_PAriepiglotis2 = new AbNormalAndNotes()); }
            set { _PAriepiglotis2 = value; }
        }

        private AbNormalAndNotes _Rimaglotis2;
        public AbNormalAndNotes Rimaglotis2
        {
            get { return _Rimaglotis2 ?? (_Rimaglotis2 = new AbNormalAndNotes()); }
            set { _Rimaglotis2 = value; }
        }

        private AbNormalAndNotes _Endoskopi2;
        public AbNormalAndNotes Endoskopi2
        {
            get { return _Endoskopi2 ?? (_Endoskopi2 = new AbNormalAndNotes()); }
            set { _Endoskopi2 = value; }
        }

        public string Notes { get; set; }
        public string Epiglotis { get; set; }
        public string PVokal { get; set; }
        public string PVentri { get; set; }
        public string Aritenoid { get; set; }
        public string PAriepiglotis { get; set; }
        public string Rimaglotis { get; set; }
        public string Endoskopi { get; set; }
}
    #endregion


    #region Leher

    public class ThtLeherItem
    {
        public string Notes { get; set; }
    }
    #endregion


    #region Tenggorok
    public class ThtTenggorok
    {
        // Kadung dibuat kri kanan jadi lanjutkan saja
        private ThtTenggorokItem _right;
        public ThtTenggorokItem Right
        {
            get { return _right ?? (_right = new ThtTenggorokItem()); }
            set { _right = value; }
        }

        private ThtTenggorokItem _left;
        public ThtTenggorokItem Left
        {
            get { return _left ?? (_left = new ThtTenggorokItem()); }
            set { _left = value; }
        }

        private AbNormalAndNotes _Rongga;
        public AbNormalAndNotes Rongga
        {
            get { return _Rongga ?? (_Rongga = new AbNormalAndNotes()); }
            set { _Rongga = value; }
        }

        private AbNormalAndNotes _Tonsil;
        public AbNormalAndNotes Tonsil
        {
            get { return _Tonsil ?? (_Tonsil = new AbNormalAndNotes()); }
            set { _Tonsil = value; }
        }

        private AbNormalAndNotes _Faring;
        public AbNormalAndNotes Faring
        {
            get { return _Faring ?? (_Faring = new AbNormalAndNotes()); }
            set { _Faring = value; }
        }

        private AbNormalAndNotes _Orofaring;
        public AbNormalAndNotes Orofaring
        {
            get { return _Orofaring ?? (_Orofaring = new AbNormalAndNotes()); }
            set { _Orofaring = value; }
        }

        private AbNormalAndNotes _Mucosa;
        public AbNormalAndNotes Mucosa
        {
            get { return _Mucosa ?? (_Mucosa = new AbNormalAndNotes()); }
            set { _Mucosa = value; }
        }

        private AbNormalAndNotes _Nasofaring;
        public AbNormalAndNotes Nasofaring
        {
            get { return _Nasofaring ?? (_Nasofaring = new AbNormalAndNotes()); }
            set { _Nasofaring = value; }
        }

        private AbNormalAndNotes _Sound;
        public AbNormalAndNotes Sound
        {
            get { return _Sound ?? (_Sound = new AbNormalAndNotes()); }
            set { _Sound = value; }
        }

        private AbNormalAndNotes _Hipofaring;
        public AbNormalAndNotes Hipofaring
        {
            get { return _Hipofaring ?? (_Hipofaring = new AbNormalAndNotes()); }
            set { _Hipofaring = value; }
        }

        public string Notes { get; set; }
    }
    public class ThtTenggorokItem
    {
        public string Rongga { get; set; }
        public string Gigi { get; set; }
        public string Tonsil { get; set; }
        public string Faring { get; set; }
        public string Orofaring { get; set; }
        public string Laring { get; set; }
        public string Dypneu { get; set; }
        public string Sianosis { get; set; }
        public string Mucosa { get; set; }
        public string Nasofaring { get; set; }
        public string Stridor { get; set; }
        public string Sound { get; set; }
        public string Hipofaring { get; set; }
        public string Tongue { get; set; }
        public string Lips { get; set; }
        public string Palatum { get; set; }
    }
#endregion
}
