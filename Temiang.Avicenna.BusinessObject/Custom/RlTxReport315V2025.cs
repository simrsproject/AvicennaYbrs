using System;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport315V2025
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

        public static void Process(int fromMonth, int toMonth, int year, int rlMasterReportItemId, out int jml, out int jmllk, out int jmlpr)
        {
            jml = 0;
            jmllk = 0;
            jmlpr = 0;

            var tdtq = new TransChargesItemQuery("a");
            var thdq = new TransChargesQuery("b");
            var itemq = new VwItemServicesRlReportQuery("c");
            var regq = new RegistrationQuery("d");
            var patq = new PatientQuery("e");

            tdtq.Select(@"<ISNULL(SUM(a.ChargeQuantity), 0) AS 'qty', 
                  SUM(CASE WHEN e.Sex = 'M' THEN a.ChargeQuantity ELSE 0 END) AS 'jmllk', 
                  SUM(CASE WHEN e.Sex = 'F' THEN a.ChargeQuantity ELSE 0 END) AS 'jmlpr'>");
            tdtq.InnerJoin(thdq).On(
                tdtq.TransactionNo == thdq.TransactionNo &&
                thdq.IsApproved == true &&
                thdq.IsVoid == false);
            tdtq.InnerJoin(itemq).On(tdtq.ItemID == itemq.ItemID &
                                     itemq.RlMasterReportItemID == rlMasterReportItemId);
            tdtq.InnerJoin(regq).On(thdq.RegistrationNo == regq.RegistrationNo &
                                    regq.IsVoid == false);
            tdtq.InnerJoin(patq).On(regq.PatientID == patq.PatientID);
            tdtq.Where(string.Format("<MONTH(b.TransactionDate) BETWEEN {0} AND {1}>", fromMonth.ToString(), toMonth.ToString()));
            tdtq.Where(string.Format("<YEAR(b.TransactionDate) = {0}>", year.ToString()));

            DataTable dtb = tdtq.LoadDataTable();

            if (dtb.Rows.Count > 0)
            {
                jml = dtb.Rows.Cast<DataRow>().Aggregate(jml, (current, row) => current + Convert.ToInt16(row["qty"]));
                jmllk = dtb.Rows.Cast<DataRow>().Aggregate(0, (current, row) => current + (row["jmllk"] != DBNull.Value ? Convert.ToInt16(row["jmllk"]) : 0));
                jmlpr = dtb.Rows.Cast<DataRow>().Aggregate(0, (current, row) => current + (row["jmlpr"] != DBNull.Value ? Convert.ToInt16(row["jmlpr"]) : 0));
            }
        }
    }
}
