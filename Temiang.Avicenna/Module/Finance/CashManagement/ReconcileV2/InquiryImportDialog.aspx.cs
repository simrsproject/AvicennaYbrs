using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using Temiang.Dal.Interfaces;
using System.Data;
using System.IO;
using System.Configuration;
using Telerik.Web.UI;
using DevExpress.Data.Linq;
using System.Drawing;

namespace Temiang.Avicenna.Module.Finance.CashManagement.ReconcileV2
{
    public partial class InquiryImportDialog : BasePageDialog
    {
        private string _message;

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.RECONCILE;
        }

        private string BankID
        {
            get { return Request.QueryString["bankid"]; }
        }
        public override bool OnButtonOkClicked()
        {
            if (bankInquiry == null || bankInquiryDetail == null) {
                ShowInformationHeader("No data");
                return false;
            }
            if (bankInquiryDetail.Count == 0) {
                ShowInformationHeader("No data detail");
                return false;
            }

            var biColl = new BankInquiryCollection();
            biColl.Query.Where(biColl.Query.FileName == bankInquiry.FileName);
            if (biColl.LoadAll()) {
                ShowInformationHeader("An identical file name has been uploaded");
                return false;
            }

            using (var trans = new esTransactionScope())
            {
                bankInquiry.Save();
                foreach (var bid in bankInquiryDetail) {
                    bid.InquiryID = bankInquiry.InquiryID;
                }
                bankInquiryDetail.Save();
                trans.Complete();
            }

            return true;
        }

        private BankInquiry bankInquiry {
            get {
                return Session["bankInquiry"] as BankInquiry;
            }
            set {
                Session["bankInquiry"] = value;
            }
        }

        private BankInquiryDetailCollection bankInquiryDetail
        {
            get
            {
                return Session["collBankInquiryDetail"] as BankInquiryDetailCollection; 
            }
            set {
                Session["collBankInquiryDetail"] = value;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (string.IsNullOrEmpty(_message))
                return "oWnd.argument.command = 'rebind';";
            return string.Format("alert(\"{0}\");oWnd.argument.command = 'rebind';", _message);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!fileuploadExcel.HasFile) return;
            if (ConfigurationManager.AppSettings["DocumentFolder"] == null) return;
            string targetFolder = ConfigurationManager.AppSettings["DocumentFolder"];
            if (!System.IO.Directory.Exists(targetFolder))
                System.IO.Directory.CreateDirectory(targetFolder);

            string path = string.Format("{0}{1}", ConfigurationManager.AppSettings["DocumentFolder"], fileuploadExcel.PostedFile.FileName);

            fileuploadExcel.SaveAs(path);

            try
            {
                var table = Common.ExcelUtil.LoadFirstSheetToDataTable(path);

                if (table != null && table.Rows.Count > 0)
                {
                    FetchImported(table);
                    grdList.DataSource = null;
                    grdList.DataSource = bankInquiryDetail;
                    grdList.DataBind();

                    bankInquiry = new BankInquiry();
                    bankInquiry.BankID = BankID;
                    bankInquiry.Debit = bankInquiryDetail.Sum(d => d.Debit);
                    bankInquiry.Credit = bankInquiryDetail.Sum(d => d.Credit);
                    bankInquiry.CreatedDateTime = DateTime.Now;
                    bankInquiry.CreatedByUserID = AppSession.UserLogin.UserID;
                    bankInquiry.LastUpdateDateTime = DateTime.Now;
                    bankInquiry.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    bankInquiry.FileName = fileuploadExcel.PostedFile.FileName;

                    bankInquiry.TransactionDate = DateTime.Now;
                    if (bankInquiryDetail.Count > 0) {
                        bankInquiry.TransactionDate = bankInquiryDetail.OrderByDescending(t => t.TransactionDateTime).Select(t => t.TransactionDateTime.Value.Date).First();
                    }
                }
                File.Delete(path);
            }
            catch (Exception ex)
            {
                _message = ex.Message;
                AjaxManager.Alert(_message);
                File.Delete(path);
            }
        }

        private void FetchImported(DataTable table)
        {
            bankInquiryDetail = new BankInquiryDetailCollection();

            string dFormat = "dd/MM/yyyy HH:mm:ss";

            foreach (DataColumn col in table.Columns) {
                col.ColumnName = col.ColumnName.Trim();
            }

            foreach (DataRow row in table.Rows)
            {
                if (row["Tgl#"] is DBNull) continue;

                var bid = bankInquiryDetail.AddNew();

                bid.RelatedTransactionNo = string.Empty;

                DateTime transd = new DateTime();
                var bDate = DateTime.TryParseExact(
                    row["Tgl#"].ToString(), dFormat,
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, 
                    out transd);
                if (!bDate) {
                    throw new Exception(string.Format("Invalid date time format {0}, the format should be {1}", row["Tgl#"].ToString(), dFormat));
                }
                bid.TransactionDateTime = transd;
                bid.Description = row["Rincian Transaksi"].ToString();
                bid.ReferenceNo = row["No# Referensi"].ToString();

                bid.Debit = System.Convert.ToDecimal(row["Debit"].ToString());
                bid.Credit = System.Convert.ToDecimal(row["Kredit"].ToString());
                bid.Balance = System.Convert.ToDecimal(row["Saldo"].ToString());

                bid.SRCashTransactionCode = string.Empty;

                bid.CreatedDateTime = DateTime.Now;
                bid.CreatedByUserID = AppSession.UserLogin.UserID;
                bid.LastUpdateDateTime = DateTime.Now;
                bid.LastUpdateByUserID = AppSession.UserLogin.UserID;
            }
        }
    }
}
