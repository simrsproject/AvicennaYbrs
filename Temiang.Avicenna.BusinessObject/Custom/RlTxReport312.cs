using System;
using System.Data;

namespace Temiang.Avicenna.BusinessObject
{
    public partial class RlTxReport312
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

        public static void Process(DateTime startDate, DateTime endDate, int rlMasterReportItemId, out int konselingAnc, out int konselingPascaPersalinan, out int kbBaruCmBukanRujukan,
            out int kbBaruCmRujukanRi, out int kbBaruCmRujukanRj, out int kbBaruCmTotal, out int kbBaruDkNifas, out int kbBaruDkAbortus, out int kbBaruDkLain, out int kunjunganUlang,
            out int keluhanEfekSamping, out int keluhanEfekSampingDiRujuk)
        {
            konselingAnc = 0;
            konselingPascaPersalinan = 0;
            kbBaruCmBukanRujukan = 0;
            kbBaruCmRujukanRi = 0;
            kbBaruCmRujukanRj = 0;
            kbBaruCmTotal = 0;
            kbBaruDkNifas = 0;
            kbBaruDkAbortus = 0;
            kbBaruDkLain = 0;
            kunjunganUlang = 0;
            keluhanEfekSamping = 0;
            keluhanEfekSampingDiRujuk = 0;

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
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '01' THEN 1 ELSE 0 END AS konselingAnc>",
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '02' THEN 1 ELSE 0 END AS konselingPascaPersalinan>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.02");
                DataTable dtb02 = phrdt.LoadDataTable();
                foreach (DataRow row02 in dtb02.Rows)
                {
                    konselingAnc += Convert.ToInt32(row02["konselingAnc"]);
                    konselingPascaPersalinan += Convert.ToInt32(row02["konselingPascaPersalinan"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '01' THEN 1 ELSE 0 END AS kbBaruCmBukanRujukan>",
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '02' THEN 1 ELSE 0 END AS kbBaruCmRujukanRi>",
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '03' THEN 1 ELSE 0 END AS kbBaruCmRujukanRj>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.03");
                DataTable dtb03 = phrdt.LoadDataTable();
                foreach (DataRow row03 in dtb03.Rows)
                {
                    kbBaruCmBukanRujukan += Convert.ToInt32(row03["kbBaruCmBukanRujukan"]);
                    kbBaruCmRujukanRi += Convert.ToInt32(row03["kbBaruCmRujukanRi"]);
                    kbBaruCmRujukanRj += Convert.ToInt32(row03["kbBaruCmRujukanRj"]);
                    kbBaruCmTotal += (Convert.ToInt32(row03["kbBaruCmBukanRujukan"]) +
                                      Convert.ToInt32(row03["kbBaruCmRujukanRi"]) +
                                      Convert.ToInt32(row03["kbBaruCmRujukanRj"]));
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '01' THEN 1 ELSE 0 END AS kbBaruDkNifas>",
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '02' THEN 1 ELSE 0 END AS kbBaruDkAbortus>",
                    @"<CASE WHEN b.QuestionAnswerSelectionLineID = '03' THEN 1 ELSE 0 END AS kbBaruDkLain>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.04");
                DataTable dtb04 = phrdt.LoadDataTable();
                foreach (DataRow row04 in dtb04.Rows)
                {
                    kbBaruDkNifas += Convert.ToInt32(row04["kbBaruDkNifas"]);
                    kbBaruDkAbortus += Convert.ToInt32(row04["kbBaruDkAbortus"]);
                    kbBaruDkLain += Convert.ToInt32(row04["kbBaruDkLain"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerText = '1' THEN 1 ELSE 0 END AS kunjunganUlang>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.05");
                DataTable dtb05 = phrdt.LoadDataTable();
                foreach (DataRow row05 in dtb05.Rows)
                {
                    kunjunganUlang += Convert.ToInt32(row05["kunjunganUlang"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerText = '1' THEN 1 ELSE 0 END AS keluhanEfekSamping>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.06");
                DataTable dtb06 = phrdt.LoadDataTable();
                foreach (DataRow row06 in dtb06.Rows)
                {
                    keluhanEfekSamping += Convert.ToInt32(row06["keluhanEfekSamping"]);
                }

                phrdt = new PatientHealthRecordLineQuery("b");
                phrdt.Select(
                    @"<CASE WHEN b.QuestionAnswerText = '1' THEN 1 ELSE 0 END AS keluhanEfekSampingDiRujuk>"
                    );
                phrdt.Where(phrdt.TransactionNo == row["TransactionNo"].ToString(), phrdt.QuestionID == "RL3.12.07");
                DataTable dtb07 = phrdt.LoadDataTable();
                foreach (DataRow row07 in dtb07.Rows)
                {
                    keluhanEfekSampingDiRujuk += Convert.ToInt32(row07["keluhanEfekSampingDiRujuk"]);
                }
            }
        }
    }
}
