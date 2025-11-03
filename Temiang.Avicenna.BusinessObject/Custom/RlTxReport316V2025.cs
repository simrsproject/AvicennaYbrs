using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport316V2025
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

        public static void Process(DateTime startDate, DateTime endDate, int rlMasterReportItemId, out int PKBPascaPersalinan, out int PKBPascaKeguguran, out int PKBInterval,
            out int PKBTotal, out int KomplikasiKB, out int KegagalanKB, out int EfekSamping, out int DropOut)
        {
            PKBPascaPersalinan = 0;
            PKBPascaKeguguran = 0;
            PKBInterval = 0;
            PKBTotal = 0;
            KomplikasiKB = 0;
            KegagalanKB = 0;
            EfekSamping = 0;
            DropOut = 0;
            

            var phrhd = new PatientHealthRecordQuery("a");
            var phrdt = new PatientHealthRecordLineQuery("b");
            phrhd.Select(phrhd.TransactionNo);
            phrhd.InnerJoin(phrdt).On(phrhd.TransactionNo == phrdt.TransactionNo && phrhd.RegistrationNo == phrdt.RegistrationNo);
            phrhd.Where(phrhd.RecordDate.Date().Between(startDate, endDate),
                        phrhd.QuestionFormID == "RL3.12",
                        phrdt.QuestionID == "RL3.12.01",
                        phrdt.QuestionAnswerSelectionLineID == rlMasterReportItemId);
            DataTable dtb = phrhd.LoadDataTable();

            foreach (DataRow row in dtb.Rows)
            {
                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '01' THEN 1 ELSE 0 END AS PKBPascaPersalinan>",
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '02' THEN 1 ELSE 0 END AS PKBPascaKeguguran>",
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '03' THEN 1 ELSE 0 END AS PKBInterval>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.02");
                DataTable dtb02 = phrdt.LoadDataTable();
                foreach (DataRow row02 in dtb02.Rows)
                {
                    PKBPascaPersalinan += Convert.ToInt32(row02["PKBPascaPersalinan"]);
                    PKBPascaKeguguran += Convert.ToInt32(row02["PKBPascaKeguguran"]);
                    PKBInterval += Convert.ToInt32(row02["PKBInterval"]);
                    PKBTotal += Convert.ToInt32(row02["PKBPascaPersalinan"])
                             + Convert.ToInt32(row02["PKBPascaKeguguran"])
                             + Convert.ToInt32(row02["PKBInterval"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerText = 1 THEN 1 ELSE 0 END AS KomplikasiKB>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.03");
                DataTable dtb03 = phrdt.LoadDataTable();
                foreach (DataRow row03 in dtb03.Rows)
                {
                    KomplikasiKB += Convert.ToInt32(row03["KomplikasiKB"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerText = '1' THEN 1 ELSE 0 END AS KegagalanKB>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.04");
                DataTable dtb04 = phrdt.LoadDataTable();
                foreach (DataRow row04 in dtb04.Rows)
                {
                    KegagalanKB += Convert.ToInt32(row04["KegagalanKB"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerText = '1' THEN 1 ELSE 0 END AS EfekSamping>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.05");
                DataTable dtb05 = phrdt.LoadDataTable();
                foreach (DataRow row05 in dtb05.Rows)
                {
                    EfekSamping += Convert.ToInt32(row05["EfekSamping"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerText = '1' THEN 1 ELSE 0 END AS DropOut>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.06");
                DataTable dtb06 = phrdt.LoadDataTable();
                foreach (DataRow row06 in dtb06.Rows)
                {
                    DropOut += Convert.ToInt32(row06["DropOut"]);
                }
            }
        }
    }
}
