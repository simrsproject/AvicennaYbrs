using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport34V2025
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

        public static void Process(int fromMonth, int toMonth, int year, string rlMasterReportItemCode, out int jml)
        {
            var regq = new RegistrationQuery("a");

            regq.Where(regq.IsVoid == false, regq.IsConsul == false, regq.IsFromDispensary == false, regq.IsNonPatient == false);
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            if (rlMasterReportItemCode == "1")
                regq.Where(regq.IsNewVisit == true);
            else
                regq.Where(regq.IsNewVisit == false);

            var regs = new RegistrationCollection();
            regs.Load(regq);

            jml = regs.Count;
        }
    }
}
