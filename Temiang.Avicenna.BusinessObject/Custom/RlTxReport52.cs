using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport52
    {
        public string RlMasterReportItemCode
        {
            get { return GetColumn("refToRlMasterReportItem_RlMasterReportItemCode").ToString(); }
            set { SetColumn("refToRlMasterReportItem_RlMasterReportItemCode", value); }
        }

        public string RlMasterReportItemName
        {
            get { return GetColumn("refToRlMasterReportItem_RlMasterReportItemName").ToString(); }
            set { SetColumn("refToRlMasterReportItem_RlMasterReportItemName", value); }
        }

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId, string parameterValue, string assasmentObgynPoliKebidanan, string assasmentObgynPenyKandungan,
            out int jml)
        {
            jml = 0;
            string parValue = parameterValue;

            if (!string.IsNullOrEmpty(parValue))
            {
                //var parValueList = new string[1];
                //if (parValue.Contains(","))
                var parValueList = parValue.Split(',');

                switch (rlMasterReportItemId)
                {
                    case 1727: //Penyakit Dalam
                    case 1728: //Bedah -- KLINIK BEDAH, KLINIK BEDAH PLASTIK, KLINIK BEDAH MINOR, KLINIK BEDAH TUMOR, KLINIK BEDAH ANAK, KLINIK DIGESTIVE

                    case 1734: //Bedah Saraf
                    case 1735: //Saraf
                    case 1736: //Jiwa

                    case 1738: //Psikologi
                    case 1739: //THT
                    case 1740: //Mata
                    case 1741: //Kulit dan Kelamin
                    case 1742: //Gigi & Mulut -- Poli Gigi & Bedah Mulut & Ortodenti
                    case 1743: //Geriatri
                    case 1744: //Kardiologi
                    case 1745: //Radiologi
                    case 1746: //Bedah Orthopedi
                    case 1747: //Paru - Paru
                    case 1748: //Kusta
                    case 1749: //Umum -- Poli Umum, dan Klinik 24 Jam

                    case 1751: //Rehabilitasi Medik -- Klinik Rehabilitasi Medik
                    case 1752: //Akupungtur Medik -- 
                    case 1753: //Konsultasi Gizi
                    case 1754: //Day Care -- heamodialisa + kemo, VK kamar bersalin, ODC di kamar bedah
                    case 1755: //Lain - Lain
                        var regq = new RegistrationQuery("a");
                        regq.Where(
                            regq.IsVoid == false,
                            regq.ServiceUnitID.In(parValueList)
                            );
                        regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                        jml = regq.LoadDataTable().Rows.Count;

                        break;

                    case 1729: //Kesehatan Anak (Neonatal) -- klinik anak & imunisasi umur 0hari - 28 hari

                        var regq2 = new RegistrationQuery("a");
                        regq2.Where(
                            regq2.IsVoid == false,
                            regq2.ServiceUnitID.In(parValueList),
                            regq2.AgeInYear == 0,
                            regq2.AgeInMonth == 0,
                            regq2.AgeInDay <= 28
                            );
                        regq2.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                        jml = regq2.LoadDataTable().Rows.Count;

                        break;

                    case 1730: //Kesehatan Anak Lainnya) -- klinik anak & imunisasi umur > 29 hari
                        var regq2b = new RegistrationQuery("a");
                        regq2b.Where(
                            regq2b.IsVoid == false,
                            regq2b.ServiceUnitID.In(parValueList),
                            regq2b.AgeInYear == 0,
                            regq2b.AgeInMonth == 0,
                            regq2b.AgeInDay > 28
                            );
                        regq2b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                        jml = regq2b.LoadDataTable().Rows.Count;

                        regq2b = new RegistrationQuery("a");
                        regq2b.Where(
                            regq2b.IsVoid == false,
                            regq2b.ServiceUnitID.In(parValueList),
                            regq2b.Or(regq2b.AgeInMonth > 0, regq2b.AgeInYear > 0),
                            regq2b.AgeInYear <= 13
                            );
                        regq2b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                        jml += regq2b.LoadDataTable().Rows.Count;

                        break;

                    case 1731: //Obstetri & Ginekologi (Ibu Hamil) -- poli kebidanan dan kia (pemeriksaan kehamilan) ambil dari data SU di Obstetric Type yang Obstetri & Ginekologi (Ibu Hamil)
                        var regO1 = new RegistrationQuery("a");
                        var ass1 = new PatientAssessmentQuery("b");
                        regO1.InnerJoin(ass1).On(ass1.RegistrationNo == regO1.RegistrationNo);
                        regO1.Where(
                            regO1.IsVoid == false,
                            regO1.ServiceUnitID.In(parValueList),
                            ass1.SRAssessmentType == assasmentObgynPoliKebidanan
                            );
                        regO1.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regO1.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                        jml = regO1.LoadDataTable().Rows.Count;

                        break;

                    case 1732: //Obstetri & Ginekologi Lainnya) -- poli kebidanan dan kia (pemeriksaan kehamilan) ambil dari data SU di Obstetric Type yang Obstetri & Ginekologi (Lainnya)
                        var regO2 = new RegistrationQuery("a");
                        var ass2 = new PatientAssessmentQuery("b");
                        regO2.InnerJoin(ass2).On(ass2.RegistrationNo == regO2.RegistrationNo);
                        regO2.Where(
                            regO2.IsVoid == false,
                            regO2.ServiceUnitID.In(parValueList),
                            ass2.SRAssessmentType == assasmentObgynPenyKandungan
                            );
                        regO2.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regO2.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
                        jml = regO2.LoadDataTable().Rows.Count;

                        break;

                    case 1733: //Keluarga Berencana
                        var regq4b = new RegistrationQuery("a");
                        regq4b.Where(
                            regq4b.IsVoid == false,
                            regq4b.ServiceUnitID.In(parValueList),
                            regq4b.SRObstetricType.NotIn("01", "02")
                            );
                        regq4b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq4b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                        jml = regq4b.LoadDataTable().Rows.Count;

                        break;

                    case 1737: //Napza -- dari IGD ( laporan narkotika) dari Visit Reason EMR
                        var regq5 = new RegistrationQuery("a");
                        regq5.Where(
                            regq5.IsVoid == false,
                            regq5.ServiceUnitID.In(parValueList),
                            regq5.SRVisitReason == "009"
                            );
                        regq5.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq5.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                        jml = regq5.LoadDataTable().Rows.Count;

                        break;

                    case 1750: //Rawat Darurat -- IGD
                        var regq6 = new RegistrationQuery("a");
                        regq6.Where(
                            regq6.IsVoid == false,
                            regq6.ServiceUnitID.In(parValueList),
                            regq6.Or(regq6.SRVisitReason != "009", regq6.SRVisitReason.IsNull())
                            );
                        regq6.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq6.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

                        jml = regq6.LoadDataTable().Rows.Count;

                        break;

                    case 1756: //TOTAL
                        jml = 0;//total;
                        break;
                }
            }
        }
    }
}
