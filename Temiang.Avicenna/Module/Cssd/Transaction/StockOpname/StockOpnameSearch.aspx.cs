using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class StockOpnameSearch : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CssdStockOpname;

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
            if (!IsPostBack)
                RestoreValueFromCookie();
        }

        public override bool OnButtonOkClicked()
        {
            var query = new CssdStockOpnameQuery("a");
            var unit = new ServiceUnitQuery("b");
            var user = new AppUserServiceUnitQuery("c");
            query.Select
                (
                    query.TransactionNo,
                    query.TransactionDate,
                    unit.ServiceUnitName,
                    query.Notes,
                    "<CAST((CASE WHEN (SELECT COUNT(TransactionNo) FROM CssdStockOpnameApproval WHERE TransactionNo = a.TransactionNo AND IsApproved = 0) = 0 THEN 1 ELSE 0 END) AS BIT) AS IsApproved>",
                    query.IsVoid
                );
            query.InnerJoin(unit).On(unit.ServiceUnitID == query.ServiceUnitID);
            query.InnerJoin(user).On(user.UserID == AppSession.UserLogin.UserID && user.ServiceUnitID == query.ServiceUnitID);

            if (!string.IsNullOrEmpty(txtTransactionNo.Text))
            {
                if (cboFilterTransactionNo.SelectedIndex == 1)
                    query.Where(query.TransactionNo == txtTransactionNo.Text);
                else
                {
                    string searchTextContain = string.Format("%{0}%", txtTransactionNo.Text);
                    query.Where(query.TransactionNo.Like(searchTextContain));
                }
            }
            if (!txtTransactionDateFrom.IsEmpty && !txtTransactionDateTo.IsEmpty)
                query.Where(query.TransactionDate.Between(txtTransactionDateFrom.SelectedDate.Value.Date, txtTransactionDateTo.SelectedDate.Value.Date));

            query.OrderBy(query.TransactionNo.Descending);

            Session[SessionNameForQuery] = query;
            Session.Remove(SessionNameForList); //reset

            SaveValueToCookie();

            return true;
        }
    }
}