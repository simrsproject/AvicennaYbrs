using System;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport310
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

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId, out int jml)
        {
            jml = 0;

            var tdtq = new TransChargesItemQuery("a");
            var thdq = new TransChargesQuery("b");
            var itemq = new VwItemServicesRlReportQuery("c");

            tdtq.Select(@"<ISNULL(SUM(a.ChargeQuantity), 0) AS 'qty'>");
            tdtq.InnerJoin(thdq).On(
                tdtq.TransactionNo == thdq.TransactionNo &&
                thdq.IsApproved == true &&
                thdq.IsVoid == false);
            tdtq.InnerJoin(itemq).On(tdtq.ItemID == itemq.ItemID &
                                     itemq.RlMasterReportItemID == rlMasterReportItemId);
            tdtq.Where(string.Format("<MONTH(b.TransactionDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            tdtq.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year.ToString()));

            DataTable dtb = tdtq.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                jml = dtb.Rows.Cast<DataRow>().Aggregate(jml, (current, row) => current + Convert.ToInt16(row["qty"]));
            }
        }
    }
}
