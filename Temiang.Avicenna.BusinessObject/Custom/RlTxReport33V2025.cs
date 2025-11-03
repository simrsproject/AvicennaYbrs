using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport33V2025
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

        public static void Process(int fromMonth, int toMonth, int year, string parValue, string parValue2, string parValueCode, string dischargeConditionDieLessThen48, string dischargeConditionDieMoreThen48, string dischargeConditionDie,
            out int pRujukan, out int pNonRujukan, out int pDiRawat, out int pDiRujuk, out int pPulang, out int pMatiDiUgdLaki, out int pMatiDiUgdPerempuan, out int pDoaLaki, out int pDoaPerempuan, out int pLukaLaki, out int pLukaPerempuan, out int pFalseEmergency)
        {
            pRujukan = 0;
            pNonRujukan = 0;
            pDiRawat = 0;
            pDiRujuk = 0;
            pPulang = 0;
            pMatiDiUgdLaki = 0;
            pDoaLaki = 0;
            pMatiDiUgdPerempuan = 0;
            pDoaPerempuan = 0;
            pLukaLaki = 0;
            pLukaPerempuan = 0;
            pFalseEmergency = 0;

            var regs = new RegistrationCollection();

            var regq = new RegistrationQuery("a");
            var stdq = new AppStandardReferenceItemQuery("c");
            var patq = new PatientQuery("p");

            regq.InnerJoin(stdq).On(
                regq.SRReferralGroup == stdq.ItemID &&
                stdq.StandardReferenceID == "ReferralGroup"
                );
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue,
                stdq.ReferenceID.In("RS", "PUSKESMAS", "FASKES", "RSLAIN", "FASKESLAIN")
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            regs.Load(regq);
            pRujukan = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");

            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue);
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            regs.Load(regq);
            pNonRujukan = regs.Count - pRujukan;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRDischargeMethod.In("DischrgMeth-005", "E02", "E10"));

            regs.Load(regq);
            pDiRawat = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRDischargeMethod.In("DischrgMeth-002", "E09", "E11", "E12"));

            regs.Load(regq);
            pDiRujuk = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRDischargeMethod.In("DischrgMeth-001", "DischrgMeth-003", "DischrgMeth-004", "E04", "E03", "E05"));

            regs.Load(regq);
            pPulang = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("p");
            regq.InnerJoin(patq).On(
                regq.PatientID == patq.PatientID
                );
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(regq.SRDischargeCondition.In(dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48, dischargeConditionDie));
            regq.Where(regq.SRTriage != "Triage-005");
            regq.Where(patq.Sex == "M");

            regs.Load(regq);
            pMatiDiUgdLaki = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("p");
            regq.InnerJoin(patq).On(
                regq.PatientID == patq.PatientID
                );
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            regq.Where(regq.SRDischargeCondition.In(dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48, dischargeConditionDie));
            regq.Where(regq.SRTriage != "Triage-005");
            regq.Where(patq.Sex == "F");

            regs.Load(regq);
            pMatiDiUgdPerempuan = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("p");
            regq.InnerJoin(patq).On(
                regq.PatientID == patq.PatientID
                );
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRDischargeCondition.In(dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48, dischargeConditionDie));
            regq.Where(regq.SRTriage == "Triage-005");
            regq.Where(patq.Sex == "M");
            regs.Load(regq);
            pDoaLaki = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("p");
            regq.InnerJoin(patq).On(
                regq.PatientID == patq.PatientID
                );
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRDischargeCondition.In(dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48, dischargeConditionDie));
            regq.Where(regq.SRTriage == "Triage-005");
            regq.Where(patq.Sex == "F");
            regs.Load(regq);
            pDoaPerempuan = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("p");
            stdq = new AppStandardReferenceItemQuery("c");

            regq.InnerJoin(patq).On(
                regq.PatientID == patq.PatientID
                );
            regq.InnerJoin(stdq).On(
                regq.SRPatientInCondition == stdq.ItemID &&
                stdq.StandardReferenceID == "PatientInCondition"
                );
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue,
                stdq.CustomField == "LK",
                stdq.ReferenceID == "EMR"
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRTriage != "Triage-005");
            regq.Where(patq.Sex == "M");
            regs.Load(regq);
            pLukaLaki = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            patq = new PatientQuery("p");
            stdq = new AppStandardReferenceItemQuery("c");

            regq.InnerJoin(patq).On(
                regq.PatientID == patq.PatientID
                );
            regq.InnerJoin(stdq).On(
                regq.SRPatientInCondition == stdq.ItemID &&
                stdq.StandardReferenceID == "PatientInCondition"
                );
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue,
                stdq.CustomField == "LK",
                stdq.ReferenceID == "EMR"
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRTriage != "Triage-005");
            regq.Where(patq.Sex == "F");
            regs.Load(regq);
            pLukaPerempuan = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            if (!string.IsNullOrEmpty(parValue2))
            {
                regq.Where(regq.SRVisitReason == parValue2);
            }
            if (parValueCode == "2.1")
            {
                regq.Where(regq.AgeInYear >= 18);
            }
            if (parValueCode == "2.2")
            {
                regq.Where(regq.AgeInYear <= 17);
            }
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRTriage != "Triage-005");
            regq.Where(regq.SRPatientInType == "02");
            regq.Where(regq.SRERCaseType != "01");
            regs.Load(regq);
            pFalseEmergency = regs.Count;
        }
    }
}
