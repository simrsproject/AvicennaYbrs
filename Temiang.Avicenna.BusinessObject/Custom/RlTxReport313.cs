using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport313
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

        public static void Process(int fromMonth, int toMonth, int year, string rlMasterReportItemCode, out int rj, out int rd, out int ri)
        {
            rj = 0;
            rd = 0;
            ri = 0;

            var itq = new ItemQuery("a");
            var ipmq = new ItemProductMedicQuery("b");
            var prescdt = new TransPrescriptionItemQuery("c");
            var preschd = new TransPrescriptionQuery("d");
            var regq = new RegistrationQuery("e");

            itq.es.Distinct = true;
            itq.Select(regq.SRRegistrationType, itq.ItemID);
            itq.InnerJoin(ipmq).On(itq.ItemID == ipmq.ItemID);
            itq.InnerJoin(prescdt).On(itq.ItemID == prescdt.ItemID);
            itq.InnerJoin(preschd).On(
                prescdt.PrescriptionNo == preschd.PrescriptionNo &&
                preschd.IsApproval == true
                );
            itq.InnerJoin(regq).On(preschd.RegistrationNo == regq.RegistrationNo);
            regq.Where(string.Format("<MONTH(d.PrescriptionDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            regq.Where(string.Format("<YEAR(d.PrescriptionDate) = {0}>", year.ToString()));

            switch (rlMasterReportItemCode)
            {
                case "1": //obat generik
                    itq.Where(ipmq.IsGeneric == true);
                    break;
                case "2": //obat non generik formularium
                    itq.Where(
                        ipmq.IsNonGeneric == true,
                        ipmq.IsFormularium == true
                        );
                    break;
                case "3": //obat non generik non formulariom
                    itq.Where(
                        ipmq.IsNonGeneric == true,
                        ipmq.IsFormularium == false
                        );
                    break;
            }

            DataTable dtb = itq.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    switch (row["SRRegistrationType"].ToString())
                    {
                        case "OPR":
                            rj += 1;
                            break;

                        case "EMR":
                            rd += 1;
                            break;

                        case "IPR":
                            ri += 1;
                            break;
                    }
                }
            }
        }
    }
}
