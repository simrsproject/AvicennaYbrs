using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry
{
    public partial class VoucherImport : BasePageDialog
    {
        private string _message;
        private string _coaNotRegistered;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;
        }

        public override bool OnButtonOkClicked()
        {
            if (!fileuploadExcel.HasFile) return true;

            //if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return true;
            //if (!Directory.Exists(ConfigurationManager.AppSettings["DocumentFolder"]))
            //    Directory.CreateDirectory(ConfigurationManager.AppSettings["DocumentFolder"]);
            //string path = ConfigurationManager.AppSettings["DocumentFolder"] + fileuploadExcel.PostedFile.FileName;

            string tmp_doc = AppParameter.GetParameterValue(AppParameter.ParameterItem.TmpDocumentFolder);
            if (string.IsNullOrEmpty(tmp_doc))
                tmp_doc = ConfigurationManager.AppSettings["DocumentFolder"];
            
            if (string.IsNullOrEmpty(tmp_doc)) return true;
            if (!Directory.Exists(tmp_doc))
                Directory.CreateDirectory(tmp_doc);
            string path = tmp_doc + fileuploadExcel.PostedFile.FileName;

            fileuploadExcel.SaveAs(path);

            try
            {
                var table = Common.ExcelUtil.LoadFirstSheetToDataTable(path);

                if (table != null && table.Rows.Count > 0)
                {
                    CreateJournal(table);
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                File.Delete(path);
            }

            return true;
        }

        private JournalTransactions CreateJournalHeader(string journalType, string journalPrefix, DateTime date, string firstRowDescription)
        {
            //Journal Header
            var journal = new JournalTransactions
            {
                JournalType = journalType,
                JournalCode = JournalCodes.GetOrCreateAutoJournalCode(journalPrefix, date)
            };
            journal.TransactionNumber = JournalCodes.GenerateAndIncrementAutoNumber(journal.JournalCode);
            journal.TransactionDate = date;

            if (chkIsFirstRowAsHeader.Checked == true)
                journal.Description = firstRowDescription;
            else
                journal.Description = string.Format("Journal {0}", date.ToString(AppConstant.DisplayFormat.DateLong));

            journal.IsPosted = false;
            journal.DateCreated = journal.LastUpdateDateTime = DateTime.Now;
            journal.CreatedBy = journal.LastUpdateByUserID = AppSession.UserLogin.UserID;
            journal.str.RefferenceNumber = string.Empty;
            journal.Save();
            return journal;
        }

        private void CreateJournalDetail(Int32 journalId, string chartOfAccountCode, DataRow row, string debitCredit, decimal amount)
        {
            var coa = new ChartOfAccounts();
            var coaQ = new ChartOfAccountsQuery();
            coaQ.Where(coaQ.ChartOfAccountCode == chartOfAccountCode);
            if (!coa.Load(coaQ))
            {
                _coaNotRegistered += string.Concat(chartOfAccountCode, ", ");
                return;
            }

            var debitAmt = (debitCredit == "D" ? amount : 0);
            var creditAmt = (debitCredit == "C" ? amount : 0);

            var journalDt = new JournalTransactionDetails
            {
                JournalId = journalId,
                ChartOfAccountId = coa.ChartOfAccountId,
                Debit = debitAmt,
                Credit = creditAmt,
                Description = Convert.ToString(row["Description"]),
                DocumentNumber = string.Empty,
                SubLedgerId = 0
            };

            // SubLedger
            if (row.Table.Columns.Contains("SubledgerName"))
            {
                var sl = new SubLedgers();
                sl.Query.Where(sl.Query.SubLedgerName == row["SubledgerName"].ToString());
                sl.Query.es.Top = 1;
                if (sl.Load(sl.Query)) {
                    journalDt.SubLedgerId = sl.SubLedgerId;
                }
            }else if (row.Table.Columns.Contains("DebitSubLedgerID"))
            {
                if (debitCredit == "D" && row["DebitSubLedgerID"] != null && row["DebitSubLedgerID"].ToInt() > 0)
                    journalDt.SubLedgerId = row["DebitSubLedgerID"].ToInt();
                else if (debitCredit == "C" && row["CreditSubLedgerID"] != null && row["CreditSubLedgerID"].ToInt() > 0)
                    journalDt.SubLedgerId = row["CreditSubLedgerID"].ToInt();
            }

            journalDt.Save();
        }

        private bool IsValidSource(DataTable dtbSource)
        {
            var isExistDebitCreditAmountColumn = dtbSource.Columns.Contains("DebitAmount");
            var isExistSubLedger = dtbSource.Columns.Contains("DebitSubLedgerID");
            foreach (DataRow row in dtbSource.Rows)
            {
            }
            return true;
        }

        private void CreateJournal(DataTable dtbSource)
        {
            decimal? totalDebit = 0;
            decimal? totalCredit = 0;
            decimal amount = 0;
            int journalCreated = 0;
            JournalTransactions journal = null;

            var isExistDebitCreditAmountColumn = dtbSource.Columns.Contains("DebitAmount");

            using (esTransactionScope trans = new esTransactionScope())
            {
                var isCreateNewJournal = true;
                foreach (DataRow row in dtbSource.Rows)
                {
                    if (row["Date"] == DBNull.Value || string.IsNullOrEmpty(row["Date"].ToString()))
                    {
                        // Split journal if find new row
                        isCreateNewJournal = true;
                        continue;
                    }

                    if (isCreateNewJournal)
                    {
                        isCreateNewJournal = false;
                        // Approve jika debit & credit sama 
                        if ((totalCredit > 0 && totalDebit > 0) && (totalDebit == totalCredit))
                        {
                            journal.BeginEdit();
                            journal.IsPosted = true;
                            journal.EndEdit();
                            journal.Save();
                        }

                        var date = Convert.ToDateTime(row["Date"]);

                        var isClosingPeriod = PostingStatus.IsPeriodeClosed(date);
                        if (isClosingPeriod)
                        {
                            throw new Exception("Financial statements for period: " +
                                               string.Format("{0:MMMM-yyyy}", date) +
                                               " have been closed. Please contact the authorities.");
                        }

                        totalDebit = 0;
                        totalCredit = 0;
                        journal = CreateJournalHeader(JournalType.General, "MM", date, Convert.ToString(row["Description"])); // General Memorial
                        journalCreated++;
                    }

                    var debitCoa = Convert.ToString(row["DebitChartOfAccountCode"]);
                    if (!string.IsNullOrEmpty(debitCoa))
                    {
                        amount = Math.Round(isExistDebitCreditAmountColumn ? Convert.ToDecimal(row["DebitAmount"]) : Convert.ToDecimal(row["Amount"]), 2);
                        totalDebit += amount;
                        CreateJournalDetail(journal.JournalId ?? 0, debitCoa, row, "D", amount);
                    }

                    var creditCoa = Convert.ToString(row["CreditChartOfAccountCode"]);
                    if (!string.IsNullOrEmpty(creditCoa))
                    {
                        amount = Math.Round(isExistDebitCreditAmountColumn ? Convert.ToDecimal(row["CreditAmount"]) : Convert.ToDecimal(row["Amount"]), 2);
                        totalCredit += amount;
                        CreateJournalDetail(journal.JournalId ?? 0, creditCoa, row, "C", amount);
                    }
                }

                // Utk journal terakhir Approve jika debit & credit sama 
                if ((totalCredit > 0 && totalDebit > 0) && (totalDebit == totalCredit))
                {
                    journal.BeginEdit();
                    journal.IsPosted = true;
                    journal.EndEdit();
                    journal.Save();
                }

                if (string.IsNullOrEmpty(_coaNotRegistered))
                {
                    trans.Complete();
                    _message = string.Format("Journal import success, {0} journal created", journalCreated);
                }
                else
                {
                    _message = string.Format("Journal import canceled, please register first this Chart of Account : {0}", _coaNotRegistered);
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (string.IsNullOrEmpty(_message))
                return string.Empty;
            return string.Format("alert(\"{0}\");", _message);
        }
    }
}
