using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport35V2025
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
            out int jml, out int jmllk, out int jmlpr, out int jmllk2, out int jmlpr2)
        {
            jml = 0;
            jmllk = 0;
            jmlpr = 0;

            jmllk2 = 0;
            jmlpr2 = 0;
            string parValue = parameterValue;

            if (!string.IsNullOrEmpty(parValue))
            {
                //var parValueList = new string[1];
                //if (parValue.Contains(","))
                var parValueList = parValue.Split(',');
 
                switch (rlMasterReportItemId)
                {
                    case 172: //Penyakit Dalam
                    case 173: //Bedah -- KLINIK BEDAH, KLINIK BEDAH PLASTIK, KLINIK BEDAH MINOR, KLINIK BEDAH TUMOR, KLINIK BEDAH ANAK, KLINIK DIGESTIVE



                    case 179: //Jiwa

                    case 181: //Psikologi
                    case 182: //THT
                    case 183: //Mata
                    case 184: //Kulit dan Kelamin
                    case 185: //Gigi & Mulut -- Poli Gigi & Bedah Mulut & Ortodenti
                    case 186: //Geriatri
                    case 187: //Kardiologi
                    case 188: //Radiologi
                    case 189: //Bedah Orthopedi
                    case 190: //Paru - Paru

                    case 191: //Kanker

                    case 192: //Uronefrologi



                    case 193: //Kusta
                    case 194: //Umum -- Poli Umum, dan Klinik 24 Jam

                    case 196: //Rehabilitasi Medik -- Klinik Rehabilitasi Medik
                    case 197: //Akupungtur Medik -- 
                    case 198: //Konsultasi Gizi
                    case 199: //Day Care -- heamodialisa + kemo, VK kamar bersalin, ODC di kamar bedah

                    case 200: //MCU 
                    case 201: //Bedah Saraf (Stroke)
                    case 202: //Bedah Saraf (Lainnya)

                    case 203: //Saraf (Stroke)
                    case 204: //Saraf (Lainnya)





                    case 205: //Lain - Lain
                        var regq = new RegistrationQuery("a");
                        var patq = new PatientQuery("b");
                        var h = new HealthcareQuery("c");
                        var city = h.Select(h.City);

                        regq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
                        regq.Where(
                            regq.IsVoid == false,
                            regq.ServiceUnitID.In(parValueList)

                            );

                        regq.Select(regq.PatientID, regq.ServiceUnitID, regq.RegistrationDate, patq.City, patq.Sex);
                        regq.es.Distinct = true;
                        regq.Where(patq.City == city);
                        regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));


                        var regq205 = new RegistrationQuery("a");
                        var patq205 = new PatientQuery("b");
                        h = new HealthcareQuery("c");
                        city = h.Select(h.City);

                        regq205.InnerJoin(patq205).On(regq205.PatientID == patq205.PatientID);
                        regq205.Where(
                            regq205.IsVoid == false,
                            regq205.ServiceUnitID.In(parValueList)

                            );

                        regq205.Select(regq205.PatientID, regq205.ServiceUnitID, regq205.RegistrationDate, patq205.City, patq205.Sex);
                        regq205.es.Distinct = true;
                        regq205.Where(patq205.City != city);
                        regq205.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq205.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));








                        jml = regq.LoadDataTable().Rows.Count + regq205.LoadDataTable().Rows.Count;
                        jmllk = regq.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regq.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");




                        jmllk2 = regq205.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regq205.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");



                        break;

                    case 174: //Kesehatan Anak (Neonatal) -- klinik anak & imunisasi umur 0hari - 28 hari

                        var regq2 = new RegistrationQuery("a");
                        var patq2 = new PatientQuery("b");
                        var h2 = new HealthcareQuery("c");
                        var city2 = h2.Select(h2.City);

                        regq2.InnerJoin(patq2).On(regq2.PatientID == patq2.PatientID);
                        regq2.Where(
                            regq2.IsVoid == false,
                            regq2.ServiceUnitID.In(parValueList),
                            regq2.AgeInYear == 0,
                            regq2.AgeInMonth == 0,
                            regq2.AgeInDay <= 28
                            );

                        regq2.Select(regq2.PatientID, regq2.ServiceUnitID, regq2.RegistrationDate, patq2.City, patq2.Sex);
                        regq2.es.Distinct = true;
                        regq2.Where(patq2.City == city2);
                        regq2.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));



                        var regq2174 = new RegistrationQuery("a");
                        var patq2174 = new PatientQuery("b");
                        h2 = new HealthcareQuery("c");
                        city2 = h2.Select(h2.City);

                        regq2174.InnerJoin(patq2174).On(regq2174.PatientID == patq2174.PatientID);
                        regq2174.Where(
                            regq2174.IsVoid == false,
                            regq2174.ServiceUnitID.In(parValueList),
                            regq2174.AgeInYear == 0,
                            regq2174.AgeInMonth == 0,
                            regq2174.AgeInDay <= 28
                            );

                        regq2174.Select(regq2174.PatientID, regq2174.ServiceUnitID, regq2174.RegistrationDate, patq2174.City, patq2174.Sex);
                        regq2174.es.Distinct = true;
                        regq2174.Where(patq2174.City != city2);
                        regq2174.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2174.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));




                        jml = regq2.LoadDataTable().Rows.Count + regq2174.LoadDataTable().Rows.Count;
                        jmllk = regq2.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regq2.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");


                        jmllk2 = regq2174.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regq2174.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");
                        break;

                    case 175: //Kesehatan Anak Lainnya) -- klinik anak & imunisasi umur > 29 hari
                        var regq2b = new RegistrationQuery("a");
                        var patq2b = new PatientQuery("b");
                        var h2b = new HealthcareQuery("c");
                        var city2b = h2b.Select(h2b.City);

                        regq2b.InnerJoin(patq2b).On(regq2b.PatientID == patq2b.PatientID);
                        regq2b.Where(
                            regq2b.IsVoid == false,
                            regq2b.ServiceUnitID.In(parValueList),
                            regq2b.AgeInYear == 0,
                            regq2b.AgeInMonth == 0,
                            regq2b.AgeInDay > 28
                            );

                        regq2b.Select(regq2b.PatientID, regq2b.ServiceUnitID, regq2b.RegistrationDate, patq2b.City, patq2b.Sex);
                        regq2b.es.Distinct = true;
                        regq2b.Where(patq2b.City == city2b);
                        regq2b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));


                        var regq2b175 = new RegistrationQuery("a");
                        var patq2b175 = new PatientQuery("b");
                        h2b = new HealthcareQuery("c");
                        city2b = h2b.Select(h2b.City);

                        regq2b175.InnerJoin(patq2b175).On(regq2b175.PatientID == patq2b175.PatientID);
                        regq2b175.Where(
                            regq2b175.IsVoid == false,
                            regq2b175.ServiceUnitID.In(parValueList),
                            regq2b175.AgeInYear == 0,
                            regq2b175.AgeInMonth == 0,
                            regq2b175.AgeInDay > 28
                            );

                        regq2b175.Select(regq2b175.PatientID, regq2b175.ServiceUnitID, regq2b175.RegistrationDate, patq2b175.City, patq2b175.Sex);
                        regq2b175.es.Distinct = true;
                        regq2b175.Where(patq2b175.City != city2b);
                        regq2b175.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2b175.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));





                        jml = regq2b.LoadDataTable().Rows.Count + regq2b175.LoadDataTable().Rows.Count;
                        jmllk = regq2b.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regq2b.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");


                        jmllk2 = regq2b175.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regq2b175.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");



                        regq2b = new RegistrationQuery("a");
                        patq2b = new PatientQuery("b");
                        regq2b.InnerJoin(patq2b).On(regq2b.PatientID == patq2b.PatientID);
                        regq2b.Where(
                            regq2b.IsVoid == false,
                            regq2b.ServiceUnitID.In(parValueList),
                            regq2b.Or(regq2b.AgeInMonth > 0, regq2b.AgeInYear > 0),
                            regq2b.AgeInYear <= 13
                            );
                        regq2b.Select(regq2b.PatientID, regq2b.ServiceUnitID, regq2b.RegistrationDate, patq2b.City, patq2b.Sex);
                        regq2b.es.Distinct = true;
                        regq2b.Where(patq2b.City == city2b);
                        regq2b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));


                        regq2b175 = new RegistrationQuery("a");
                        patq2b175 = new PatientQuery("b");
                        regq2b175.InnerJoin(patq2b175).On(regq2b175.PatientID == patq2b175.PatientID);
                        regq2b175.Where(
                            regq2b175.IsVoid == false,
                            regq2b175.ServiceUnitID.In(parValueList),
                            regq2b175.Or(regq2b.AgeInMonth > 0, regq2b.AgeInYear > 0),
                            regq2b175.AgeInYear <= 13
                            );
                        regq2b175.Select(regq2b.PatientID, regq2b175.ServiceUnitID, regq2b175.RegistrationDate, patq2b175.City, patq2b175.Sex);
                        regq2b175.es.Distinct = true;
                        regq2b175.Where(patq2b175.City != city2b);
                        regq2b175.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq2b175.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));








                        jml += regq2b.LoadDataTable().Rows.Count + regq2b175.LoadDataTable().Rows.Count;
                        jmllk += regq2b.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr += regq2b.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");

                        jmllk2 += regq2b175.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 += regq2b175.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");



                        break;

                    case 176: //Obstetri & Ginekologi (Ibu Hamil) -- poli kebidanan dan kia (pemeriksaan kehamilan) ambil dari data SU di Obstetric Type yang Obstetri & Ginekologi (Ibu Hamil)
                        var regO1 = new RegistrationQuery("a");
                        var ass1 = new PatientAssessmentQuery("b");
                        var pat1 = new PatientQuery("c");
                        var hO1 = new HealthcareQuery("d");
                        var cityO1 = hO1.Select(hO1.City);


                        regO1.InnerJoin(ass1).On(ass1.RegistrationNo == regO1.RegistrationNo);
                        regO1.InnerJoin(pat1).On(pat1.PatientID == regO1.PatientID);
                        regO1.Where(
                            regO1.IsVoid == false,
                            regO1.ServiceUnitID.In(parValueList),
                            ass1.SRAssessmentType == assasmentObgynPoliKebidanan
                            );

                        regO1.Select(regO1.PatientID, regO1.ServiceUnitID, regO1.RegistrationDate, pat1.City, pat1.Sex, ass1.SRAssessmentType);
                        regO1.es.Distinct = true;
                        regO1.Where(pat1.City == cityO1);
                        regO1.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regO1.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));



                        var regO1176 = new RegistrationQuery("a");
                        var ass1176 = new PatientAssessmentQuery("b");
                        var pat1176 = new PatientQuery("c");
                        hO1 = new HealthcareQuery("d");
                        cityO1 = hO1.Select(hO1.City);


                        regO1176.InnerJoin(ass1176).On(ass1176.RegistrationNo == regO1176.RegistrationNo);
                        regO1176.InnerJoin(pat1176).On(pat1176.PatientID == regO1176.PatientID);
                        regO1176.Where(
                            regO1176.IsVoid == false,
                            regO1176.ServiceUnitID.In(parValueList),
                            ass1176.SRAssessmentType == assasmentObgynPoliKebidanan
                            );

                        regO1176.Select(regO1176.PatientID, regO1176.ServiceUnitID, regO1176.RegistrationDate, pat1176.City, pat1176.Sex, ass1176.SRAssessmentType);
                        regO1176.es.Distinct = true;
                        regO1176.Where(pat1.City != cityO1);
                        regO1176.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regO1176.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));





                        jml = regO1.LoadDataTable().Rows.Count + regO1176.LoadDataTable().Rows.Count;
                        jmllk = regO1.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regO1.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");


                        jmllk2 = regO1176.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regO1176.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");

                        break;

                    case 177: //Obstetri & Ginekologi Lainnya) -- poli kebidanan dan kia (pemeriksaan kehamilan) ambil dari data SU di Obstetric Type yang Obstetri & Ginekologi (Lainnya)
                        var regO2 = new RegistrationQuery("a");
                        var ass2 = new PatientAssessmentQuery("b");
                        var pat2 = new PatientQuery("c");
                        var hO2 = new HealthcareQuery("d");
                        var cityO2 = hO2.Select(hO2.City);



                        regO2.InnerJoin(ass2).On(ass2.RegistrationNo == regO2.RegistrationNo);
                        regO2.InnerJoin(pat2).On(pat2.PatientID == regO2.PatientID);
                        regO2.Where(
                            regO2.IsVoid == false,
                            regO2.ServiceUnitID.In(parValueList),
                            ass2.SRAssessmentType == assasmentObgynPenyKandungan
                            );

                        regO2.Select(regO2.PatientID, regO2.ServiceUnitID, regO2.RegistrationDate, pat2.City, pat2.Sex, ass2.SRAssessmentType);
                        regO2.es.Distinct = true;
                        regO2.Where(pat2.City == cityO2);
                        regO2.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regO2.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));



                        var regO2177 = new RegistrationQuery("a");
                        var ass2177 = new PatientAssessmentQuery("b");
                        var pat2177 = new PatientQuery("c");
                        hO2 = new HealthcareQuery("d");
                        cityO2 = hO2.Select(hO2.City);



                        regO2177.InnerJoin(ass2177).On(ass2177.RegistrationNo == regO2177.RegistrationNo);
                        regO2177.InnerJoin(pat2177).On(pat2177.PatientID == regO2177.PatientID);
                        regO2177.Where(
                            regO2177.IsVoid == false,
                            regO2177.ServiceUnitID.In(parValueList),
                            ass2177.SRAssessmentType == assasmentObgynPenyKandungan
                            );

                        regO2177.Select(regO2177.PatientID, regO2177.ServiceUnitID, regO2177.RegistrationDate, pat2177.City, pat2177.Sex, ass2177.SRAssessmentType);
                        regO2177.es.Distinct = true;
                        regO2177.Where(pat2177.City != cityO2);
                        regO2177.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regO2177.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));


                        jml = regO2.LoadDataTable().Rows.Count + regO2177.LoadDataTable().Rows.Count;



                        jmllk = regO2.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regO2.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");


                        jmllk2 = regO2177.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regO2177.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");


                        break;

                    case 178: //Keluarga Berencana
                        var regq4b = new RegistrationQuery("a");
                        var patq4b = new PatientQuery("b");
                        var h4b = new HealthcareQuery("c");
                        var city4b = h4b.Select(h4b.City);

                        regq4b.InnerJoin(patq4b).On(regq4b.PatientID == patq4b.PatientID);

                        regq4b.Where(
                            regq4b.IsVoid == false,
                            regq4b.ServiceUnitID.In(parValueList),
                            regq4b.SRObstetricType.NotIn("01", "02")
                            );

                        regq4b.Select(regq4b.PatientID, regq4b.ServiceUnitID, regq4b.RegistrationDate, patq4b.City, patq4b.Sex);
                        regq4b.es.Distinct = true;
                        regq4b.Where(patq4b.City == city4b);
                        regq4b.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq4b.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));



                        var regq4b178 = new RegistrationQuery("a");
                        var patq4b178 = new PatientQuery("b");
                        h4b = new HealthcareQuery("c");
                        city4b = h4b.Select(h4b.City);

                        regq4b178.InnerJoin(patq4b178).On(regq4b178.PatientID == patq4b178.PatientID);

                        regq4b178.Where(
                            regq4b178.IsVoid == false,
                            regq4b178.ServiceUnitID.In(parValueList),
                            regq4b178.SRObstetricType.NotIn("01", "02")
                            );

                        regq4b178.Select(regq4b178.PatientID, regq4b178.ServiceUnitID, regq4b178.RegistrationDate, patq4b178.City, patq4b178.Sex);
                        regq4b178.es.Distinct = true;
                        regq4b178.Where(patq4b178.City != city4b);
                        regq4b178.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq4b178.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));





                        jml = regq4b.LoadDataTable().Rows.Count + regq4b178.LoadDataTable().Rows.Count;
                        jmllk = regq4b.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regq4b.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");

                        jmllk2 = regq4b178.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regq4b178.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");

                        break;

                    case 180: //Napza -- dari IGD ( laporan narkotika) dari Visit Reason EMR
                        var regq5 = new RegistrationQuery("a");
                        var patq5 = new PatientQuery("b");
                        var h5 = new HealthcareQuery("c");
                        var city5 = h5.Select(h5.City);

                        regq5.InnerJoin(patq5).On(regq5.PatientID == patq5.PatientID);
                        regq5.Where(
                            regq5.IsVoid == false,
                            regq5.ServiceUnitID.In(parValueList),
                            regq5.SRVisitReason == "009"
                            );



                        regq5.Select(regq5.PatientID, regq5.ServiceUnitID, regq5.RegistrationDate, patq5.City, patq5.Sex);
                        regq5.es.Distinct = true;

                        regq5.Where(patq5.City == city5);

                        regq5.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq5.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));


                        var regq5180 = new RegistrationQuery("a");
                        var patq5180 = new PatientQuery("b");
                        h5 = new HealthcareQuery("c");
                        city5 = h5.Select(h5.City);

                        regq5180.InnerJoin(patq5180).On(regq5180.PatientID == patq5180.PatientID);
                        regq5180.Where(
                            regq5180.IsVoid == false,
                            regq5180.ServiceUnitID.In(parValueList),
                            regq5180.SRVisitReason == "009"
                            );



                        regq5180.Select(regq5.PatientID, regq5180.ServiceUnitID, regq5180.RegistrationDate, patq5180.City, patq5180.Sex);
                        regq5180.es.Distinct = true;

                        regq5180.Where(patq5180.City != city5);

                        regq5180.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq5180.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));





                        jml = regq5.LoadDataTable().Rows.Count + regq5180.LoadDataTable().Rows.Count;
                        jmllk = regq5.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regq5.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");


                        jmllk2 = regq5180.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regq5180.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");

                        break;

                    case 195: //Rawat Darurat -- IGD
                        var regq6 = new RegistrationQuery("a");
                        var patq6 = new PatientQuery("b");
                        var h6 = new HealthcareQuery("c");
                        var city6 = h6.Select(h6.City);


                        regq6.InnerJoin(patq6).On(regq6.PatientID == patq6.PatientID);
                        regq6.Where(
                                  regq6.IsVoid == false,
                                  regq6.ServiceUnitID.In(parValueList),
                                  regq6.Or(regq6.SRVisitReason != "009", regq6.SRVisitReason.IsNull())
                                  );

                        regq6.Select(regq6.PatientID, regq6.ServiceUnitID, regq6.RegistrationDate, patq6.City, patq6.Sex);
                        regq6.es.Distinct = true;
                        regq6.Where(patq6.City == city6);
                        regq6.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq6.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));



                        var regq6195 = new RegistrationQuery("a");
                        var patq6195 = new PatientQuery("b");
                        h6 = new HealthcareQuery("c");
                        city6 = h6.Select(h6.City);


                        regq6195.InnerJoin(patq6195).On(regq6195.PatientID == patq6195.PatientID);
                        regq6195.Where(
                                  regq6195.IsVoid == false,
                                  regq6195.ServiceUnitID.In(parValueList),
                                  regq6195.Or(regq6195.SRVisitReason != "009", regq6195.SRVisitReason.IsNull())
                                  );

                        regq6195.Select(regq6195.PatientID, regq6195.ServiceUnitID, regq6195.RegistrationDate, patq6195.City, patq6195.Sex);
                        regq6195.es.Distinct = true;
                        regq6195.Where(patq6195.City != city6);
                        regq6195.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
                        regq6195.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));





                        jml = regq6.LoadDataTable().Rows.Count + regq6195.LoadDataTable().Rows.Count;
                        jmllk = regq6.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr = regq6.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");

                        jmllk2 = regq6195.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "M");
                        jmlpr2 = regq6195.LoadDataTable().AsEnumerable().Count(row => row.Field<string>("sex") == "F");

                        break;

                    case 206: //TOTAL
                        jml = 0;//total;
                        jmllk = 0;
                        jmlpr = 0;
                        jmllk2 = 0;
                        jmlpr2 = 0;
                        break;

                }
            }
        }
    }
}
