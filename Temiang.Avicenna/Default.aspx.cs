using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.IO;

namespace Temiang.Avicenna
{
    public partial class Default : BasePage
    {
        public void Page_Load()
        {
            //imgCompany.Visible = !HttpContext.Current.IsDebuggingEnabled;

            // Check Approval queuing & redirect if exist
            if ((AppSession.Parameter.IsUseApprovalLevel || AppSession.Parameter.IsDistributionUseApprovalLevel) && IsApprovalQueuing())
            {
                Helper.RegisterStartupScript(this.Page, "assessEntry",
    "setTimeout(function(){alert('Some transaction need to approv, this page will redirect to approval page'); window.location.replace('" + Page.ResolveUrl("~/MyHome/MyHome.aspx") + "?tb=appr');;}, 1000);");
            }
            else
            {
                Helper.RegisterStartupScript(this.Page, "loadHomepage", "LoadHomePage();");
            }
        }

        public string BaseURL()
        {
            return Helper.UrlRoot2();
        }

        private static bool IsApprovalQueuing()
        {
            var atq = new ApprovalTransactionQuery("at");
            var itq = new ItemTransactionQuery("it");
            atq.InnerJoin(itq).On(atq.TransactionNo == itq.TransactionNo);
            atq.Select(atq.TransactionNo, atq.ApprovalLevel);
            atq.Where(atq.UserID == AppSession.UserLogin.UserID && atq.IsApproved == 0 && itq.IsApproved == 0 && itq.IsVoid == 0);

            var dtbAppTrans = atq.LoadDataTable();

            var isApprovalQueuing = false;
            foreach (DataRow row in dtbAppTrans.Rows)
            {
                var at = new ApprovalTransaction();
                atq = new ApprovalTransactionQuery("at");
                atq.Where(atq.TransactionNo == row["TransactionNo"].ToString() &&
                          atq.ApprovalLevel < row["ApprovalLevel"].ToInt());
                atq.es.Top = 1;
                atq.OrderBy(atq.ApprovalLevel.Descending);
                if (at.Load(atq))
                {
                    if (at.IsApproved == true)
                    {
                        // Jika user level dibawahnya sudah approve 
                        isApprovalQueuing = true;
                    }
                }
                else
                {
                    // Jika tidak ada user level dibawahnya
                    isApprovalQueuing = true;
                }

                if (isApprovalQueuing)
                {
                    break;
                }
            }
            return isApprovalQueuing;
        }

    }
}