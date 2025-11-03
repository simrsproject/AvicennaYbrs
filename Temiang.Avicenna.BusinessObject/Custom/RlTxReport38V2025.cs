using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport38V2025
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

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId, out int jmlPemeriksaanL, out int jmlPemeriksaanP)
        {
            jmlPemeriksaanL = 0;
            jmlPemeriksaanP = 0;

            var tdtq = new TransChargesItemQuery("a");
            var thdq = new TransChargesQuery("b");
            var itemq = new VwItemServicesRlReportQuery("c");
            var rq = new RegistrationQuery("d");
            var pq = new PatientQuery("e");

            tdtq.Select(@"<ISNULL(SUM(a.ChargeQuantity), 0) AS 'qty',
                            ISNULL(SUM(CASE WHEN e.Sex = 'M' THEN a.ChargeQuantity ELSE 0 END), 0) 'jmlPemeriksaanL',
                            ISNULL(SUM(CASE WHEN e.Sex = 'F' THEN a.ChargeQuantity ELSE 0 END), 0) 'jmlPemeriksaanP'
                        >");
            tdtq.InnerJoin(thdq).On(
                tdtq.TransactionNo == thdq.TransactionNo &&
                thdq.IsApproved == true &&
                thdq.IsVoid == false);
            tdtq.InnerJoin(itemq).On(tdtq.ItemID == itemq.ItemID &
                                     itemq.RlMasterReportItemID == rlMasterReportItemId);
            tdtq.InnerJoin(rq).On(thdq.RegistrationNo == rq.RegistrationNo);
            tdtq.InnerJoin(pq).On(rq.PatientID == pq.PatientID);
            tdtq.Where(string.Format("<MONTH(b.TransactionDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            tdtq.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year.ToString()));

            DataTable dtb = tdtq.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                jmlPemeriksaanL = dtb.Rows.Cast<DataRow>().Aggregate(jmlPemeriksaanL, (current, row) => current + Convert.ToInt32(row["jmlPemeriksaanL"]));
                jmlPemeriksaanP = dtb.Rows.Cast<DataRow>().Aggregate(jmlPemeriksaanP, (current, row) => current + Convert.ToInt32(row["jmlPemeriksaanP"]));
            }
        }
    }
}
