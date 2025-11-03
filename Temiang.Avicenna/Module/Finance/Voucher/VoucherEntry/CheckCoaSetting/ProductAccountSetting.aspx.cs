using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Voucher.VoucherEntry.CheckCoaSetting
{
    public partial class ProductAccountSetting : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VOUCHER_MEMORIAL;

            if (IsPostBack) return;

            var item = new Item();
            item.LoadByPrimaryKey(Request.QueryString["itemID"]);

            Title = item.ItemName;

            var query = new ProductAccountQuery("a");
            query.es.Top = 10;
            query.Select
                (
                    query.ProductAccountID,
                    query.ProductAccountName
                );
            query.Where
                (
                    query.ProductAccountID == item.ProductAccountID,
                    query.IsActive == true
                );

            cboProductAccount.DataSource = query.LoadDataTable();
            cboProductAccount.DataBind();

        }

        protected void cboProductAccount_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ProductAccountQuery("a");
            query.es.Top = 10;
            query.Select
                (
                    query.ProductAccountID,
                    query.ProductAccountName
                );
            query.Where
                (
                    query.Or
                        (
                            query.ProductAccountID.Like(searchTextContain),
                            query.ProductAccountName.Like(searchTextContain)
                        ),
                        query.IsActive == true
                );

            cboProductAccount.DataSource = query.LoadDataTable();
            cboProductAccount.DataBind();
        }

        protected void cboProductAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ProductAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ProductAccountID"].ToString();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.mode = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var item = new Item();
            item.LoadByPrimaryKey(Request.QueryString["itemID"]);
            item.ProductAccountID = cboProductAccount.SelectedValue;
            item.Save();

            return true;
        }
    }
}
