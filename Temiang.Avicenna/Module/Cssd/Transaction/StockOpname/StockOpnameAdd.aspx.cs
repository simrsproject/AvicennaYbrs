using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class StockOpnameAdd : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //StandardReference Initialize
            if (!IsPostBack)
            {
                var query = new ServiceUnitQuery("a");
                var qusr = new AppUserServiceUnitQuery("u");
                query.InnerJoin(qusr).On(query.ServiceUnitID == qusr.ServiceUnitID);
                query.Where(qusr.UserID == AppSession.UserLogin.UserID, query.IsActive == true);
                query.OrderBy(query.ServiceUnitName.Ascending);

                DataTable dtb = query.LoadDataTable();

                cboServiceUnitID.Items.Clear();
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (DataRow item in dtb.Rows)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(item["ServiceUnitName"].ToString(), item["ServiceUnitID"].ToString()));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtTransactionDate.SelectedDate = (new DateTime()).NowAtSqlServer();
        }

        public override bool OnButtonOkClicked()
        {
            rfvServiceUnitID.Validate();

            if (!rfvServiceUnitID.IsValid)
                return false;

            using (esTransactionScope trans = new esTransactionScope())
            {
                string userId = AppSession.UserLogin.UserID;

                var transactionNo = PopulateHeader(userId);
                Session["CssdNewSO" + Request.UserHostName] = transactionNo;

                if (!PopulateDetail(transactionNo, userId)) return false;

                trans.Complete();
            }

            return true;
        }

        private bool PopulateDetail(string transactionNo, string userId)
        {
            //Detil Item
            var query = new ItemQuery("a");
            var balq = new CssdItemBalanceQuery("b");
            query.Select
                (
                    query.ItemID,
                    balq.Balance.Coalesce("0"),
                    balq.BalanceReceived.Coalesce("0"),
                    balq.BalanceDeconImmersion.Coalesce("0"),
                    balq.BalanceDeconAbstersion.Coalesce("0"),
                    balq.BalanceDeconDrying.Coalesce("0"),
                    balq.BalanceFeasibilityTest.Coalesce("0"),
                    balq.BalancePackaging.Coalesce("0"),
                    balq.BalanceUltrasound.Coalesce("0"),
                    balq.BalanceSterilization.Coalesce("0"),
                    balq.BalanceDistribution.Coalesce("0"),
                    balq.BalanceReturned.Coalesce("0")
                );
            query.InnerJoin(balq).On(balq.ServiceUnitID == cboServiceUnitID.SelectedValue && balq.ItemID == query.ItemID);

            query.Where(
                query.IsActive == true,
                query.IsNeedToBeSterilized == true
                );

            query.OrderBy(query.ItemName.Ascending);

            DataTable tbl = query.LoadDataTable();

            if (tbl.Rows.Count == 0)
            {
                pnlInfo.Visible = true;
                lblInfo.Text = "No records to process.";
                return false;
            }

            int index = 1;
            int counter = 0;
            int maxPageSize = 30;
            try
            {
                maxPageSize = System.Convert.ToInt32(AppParameter.GetParameterValue(AppParameter.ParameterItem.CssdStockOpnameRowPerPage));
            }
            catch
            {

            }

            int pageNo = 1;

            var transactionItems = new CssdStockOpnameItemCollection();
            var serverDateNow = (new DateTime()).NowAtSqlServer();

            foreach (DataRow row in tbl.Rows)
            {
                CssdStockOpnameItem detail = transactionItems.AddNew();
                detail.TransactionNo = transactionNo;

                detail.SequenceNo = string.Format("{0:00000}", index++);

                counter++;
                if (counter > maxPageSize)
                {
                    pageNo++;
                    counter = 0;
                    counter++;
                }

                detail.PageNo = pageNo;

                detail.ItemID = (string)row["ItemID"];
                detail.Balance = 0;
	            detail.BalanceReceived = 0;
                detail.BalanceDeconImmersion = 0;
                detail.BalanceDeconAbstersion = 0;
                detail.BalanceDeconDrying = 0;
                detail.BalanceFeasibilityTest = 0;
                detail.BalancePackaging = 0;
                detail.BalanceUltrasound = 0;
                detail.BalanceSterilization = 0;
                detail.BalanceDistribution = 0;
                detail.BalanceReturned = 0;

                detail.PrevBalance = (decimal)row["Balance"];
                detail.PrevBalanceReceived = (decimal)row["BalanceReceived"];
                detail.PrevBalanceDeconImmersion = (decimal)row["BalanceDeconImmersion"];
                detail.PrevBalanceDeconAbstersion = (decimal)row["BalanceDeconAbstersion"];
                detail.PrevBalanceDeconDrying = (decimal)row["BalanceDeconDrying"];
                detail.PrevBalanceFeasibilityTest = (decimal)row["BalanceFeasibilityTest"];
                detail.PrevBalancePackaging = (decimal)row["BalancePackaging"];
                detail.PrevBalanceUltrasound = (decimal)row["BalanceUltrasound"];
                detail.PrevBalanceSterilization = (decimal)row["BalanceSterilization"];
                detail.PrevBalanceDistribution = (decimal)row["BalanceDistribution"];
                detail.PrevBalanceReturned = (decimal)row["BalanceReturned"];

                detail.LastUpdateByUserID = userId;
                detail.LastUpdateDateTime = serverDateNow;
            }

            //Approval table
            var approvals = new CssdStockOpnameApprovalCollection();
            for (int i = 0; i < pageNo; i++)
            {
                CssdStockOpnameApproval approval = approvals.AddNew();
                approval.TransactionNo = transactionNo;
                approval.PageNo = i + 1;
                approval.IsApproved = false;
            }
            approvals.Save();
            
            transactionItems.Save();

            return true;
        }

        private string PopulateHeader(string userID)
        {
            //Item Transaction
            AppAutoNumberLast autoNumberLast = Helper.GetNewAutoNumber(txtTransactionDate.SelectedDate.Value.Date,
                AppEnum.AutoNumber.CssdStockOpnameNo);

            autoNumberLast.Save();

            var so = new CssdStockOpname();
            so.TransactionNo = autoNumberLast.LastCompleteNumber;
            so.TransactionDate = txtTransactionDate.SelectedDate;
            so.ServiceUnitID = cboServiceUnitID.SelectedValue;
            so.Notes = txtNotes.Text;

            so.LastUpdateByUserID = userID;
            so.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();

            so.Save();
            return so.TransactionNo;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'ok';oWnd.argument.trno = '" + Session["CssdNewSO" + Request.UserHostName] + "';oWnd.argument.initial = 'no'";
        }
    }
}