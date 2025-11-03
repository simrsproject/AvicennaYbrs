using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport32
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

        public static void Process(int fromMonth, int toMonth, int year, string parValue, string dischargeConditionDieLessThen48, string dischargeConditionDieMoreThen48, string dischargeConditionDie,
            out int pRujukan, out int pNonRujukan, out int pDiRawat, out int pDiRujuk, out int pPulang, out int pMatiDiUgd, out int pDoa)
        {
            pRujukan = 0;
            pNonRujukan = 0;
            pDiRawat = 0;
            pDiRujuk = 0;
            pPulang = 0;
            pMatiDiUgd = 0;
            pDoa = 0;

            var regs = new RegistrationCollection();

            var regq = new RegistrationQuery("a");
            var stdq = new AppStandardReferenceItemQuery("c");

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
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRDischargeMethod.In("DischrgMeth-001", "DischrgMeth-003", "DischrgMeth-004", "E04", "E03", "E05"));

            regs.Load(regq);
            pPulang = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            regq.Where(regq.SRDischargeCondition.In(dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48, dischargeConditionDie));
            regq.Where(regq.SRTriage != "Triage-005");

            regs.Load(regq);
            pMatiDiUgd = regs.Count;

            //----------------------------
            regs = new RegistrationCollection();
            regq = new RegistrationQuery("a");
            regq.Where(
                regq.IsVoid == false,
                regq.SRRegistrationType == "EMR",
                regq.SRERCaseType == parValue
                );
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));
            regq.Where(regq.SRDischargeCondition.In(dischargeConditionDieLessThen48, dischargeConditionDieMoreThen48, dischargeConditionDie));
            regq.Where(regq.SRTriage == "Triage-005");
            regs.Load(regq);
            pDoa = regs.Count;
        }
    }
}
