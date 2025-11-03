using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport13
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

        public static void Process(DateTime pdate, string smfId, out int vvip, out int vip, out int i, out int ii, out int iii, out int khusus)
        {
            vvip = 0;
            vip = 0;
            i = 0;
            ii = 0;
            iii = 0;
            khusus = 0;

            var query = new NumberOfBedSmfQuery("a");
            var cq = new ClassQuery("c");
            var stdq = new AppStandardReferenceItemQuery("d");
            query.InnerJoin(cq).On(query.ClassID == cq.ClassID);
            query.InnerJoin(stdq).On(
                cq.SRClassRL == stdq.ItemID &&
                stdq.StandardReferenceID == "ClassRL");
            query.Where(query.StartingDate == pdate, query.SmfID == smfId);
            query.Select(stdq.ReferenceID.As("ClassID"), query.NumberOfBed.Sum());
            query.GroupBy(stdq.ReferenceID);
            DataTable dtb = query.LoadDataTable();
            if (dtb.Rows.Count > 0)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ClassID"].ToString() == "VVIP")
                        vvip += Convert.ToInt32(row["NumberOfBed"]);
                    if (row["ClassID"].ToString() == "VIP")
                        vip += Convert.ToInt32(row["NumberOfBed"]);
                    if (row["ClassID"].ToString() == "I")
                        i += Convert.ToInt32(row["NumberOfBed"]);
                    if (row["ClassID"].ToString() == "II")
                        ii += Convert.ToInt32(row["NumberOfBed"]);
                    if (row["ClassID"].ToString() == "III")
                        iii += Convert.ToInt32(row["NumberOfBed"]);
                    if (row["ClassID"].ToString() == "X")
                        khusus += Convert.ToInt32(row["NumberOfBed"]);
                }
            }
        }
    }
}
