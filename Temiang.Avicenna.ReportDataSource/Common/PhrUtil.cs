using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.ReportDataSource.Common
{
    public static class PhrUtil
    {
        public static class PatientLetterQuestion
        {
            // Sick Letter
            public const string FromDate = "LTR.FROMDT";
            public const string ToDate = "LTR.TODT";
            public const string Note = "LTR.NOTE";

            // Patient ID
            public const string FatherName = "PAT.FATHER";
            public const string MotherName = "PAT.MOTHER";
            public const string CityOfBirth = "PAT.CTOB";
            public const string SpouseName = "PAT.SPOUS";

            // Dead Letter
            public const string DeadDate = "LTR.DEADTM";

            // Corpse Letter
            public const string CorpseToID = "LTR.TOID";
            public const string CorpseToName = "LTR.TONAME";
            public const string CorpseToAddress = "LTR.TOADDR";
            public const string CorpseToRelationship = "LTR.TORLT";

        }

        public static class Pemeriksaan
        {
            public const string Keadaan = "VSIG-UMUM";
            public const string Kesadaran = "VSIG-KSDR";
        }

        [Obsolete("Gunakan VitalSign.VitalSignEnum",true)]
        public static class VitalSign
        {
            public const string BB = "B11.B11.01";
            public const string TB = "B11.B11.02";
            //public const string TDS = "A.KDV.1TDS";
            //public const string TDD = "A.KDV.2TDD";
            public const string TDS = "KDV.TDR.1S";
            public const string TDD = "KDV.TDR.2D";
          
            public const string BPS = "A.KDV.1TDS";
            public const string BPD = "A.KDV.2TDD";

            public const string Nadi = "A.KDV.ND";
            public const string Pernafasan = "B4.15";
            //public const string Suhu = "B4.17";
            public const string Suhu = "A.KDV.TEM";
            public const string Gizi = "GIZISTAT";
        } 

        public static class TumbuhKembang
        {
            public const string LingkarKpl = "BHIST-0023";
            public const string TBLahir = "BHIST-0025";
            public const string BBLahir = "BHIST-0024";
            public const string ASI = "BHIST-0026";
            public const string SusuFormula = "BHIST-0027";
            public const string Makanan = "BHIST-0028";



            public const string AngkatKpla = "BHIST-0023";
            public const string Tengkurap = "BHIST-0031";
            public const string Duduk = "BHIST-0032";
            public const string Merangkak = "BHIST-0033";
            public const string Berdiri = "BHIST-0034";
            public const string Berjalan = "BHIST-0035";
            public const string MeraihBnda = "BHIST-0036";
            public const string Memegang = "BHIST-0037";
            public const string Mengoceh = "BHIST-0038";
            public const string Berbicara = "BHIST-0039";
            public const string Keluhan = "BHIST-0042";
        }        
        public static class Perinatal
        {
            public const string Penolong = "BHIST-0003";
            public const string LamaBulan = "BHIST-0001";
            public const string LamaMinggu = "BHIST-0002";
            public const string Komplikasi = "BHIST-0004";
            public const string CaraLahir = "BHIST-0005";
            public const string Penyulit = "BHIST-0006";

        }
        public static class Psikologis
        {
            public const string Tenang = "PSIK-WTNG";
            public const string Sedih = "PSIK-WSDH";
            public const string Takut = "PSIK-WTKT";
            public const string Menghindar = "PSIK-MKMT";
            public const string Penurut = "PSIK-TPNR";
            public const string Agresif = "PSIK-AGRV";
            public const string Pasif = "PSIK-PSIF";
            public const string Lainnya = "PSIK-OTHR";

        }

        public static PatientHealthRecordLine FindInSingleGroup(this PatientHealthRecordLineCollection lines, string questionID)
        {
            foreach (PatientHealthRecordLine recordLine in lines)
            {
                if (recordLine.QuestionID == questionID)
                    return recordLine;
            }

            var phrlEmpty =  new PatientHealthRecordLine();
            phrlEmpty.QuestionAnswerNum=0;
            phrlEmpty.QuestionAnswerText=string.Empty;
            phrlEmpty.QuestionAnswerSelectionLineID=string.Empty;
            return phrlEmpty;
        }
    }
}