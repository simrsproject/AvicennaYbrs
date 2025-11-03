using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport51kemenkes
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

            regq.Where(regq.IsVoid == false, regq.IsConsul == false);
            regq.Where(string.Format("<MONTH(a.RegistrationDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(a.RegistrationDate) = {0}>", year.ToString()));

            if (rlMasterReportItemCode == "1")
                regq.Where(regq.IsNewPatient == true);
            else
                regq.Where(regq.IsNewPatient == false);

            var regs = new RegistrationCollection();
            regs.Load(regq);

            jml = regs.Select(r => r.PatientID).Distinct().Count();
        }
    }
}
