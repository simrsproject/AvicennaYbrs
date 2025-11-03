using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI;
using System.Data;

namespace Temiang.Avicenna.Module.Inventory.Procurement
{
    public partial class RequestOrderNotesDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var it = new ItemTransaction();
                it.LoadByPrimaryKey(Request.QueryString["tno"]);

                Title = "Request Order Notes for PO# : " + Request.QueryString["tno"];

                (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                (Helper.FindControlRecursive(this, "btnCancel") as Button).Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new ItemTransactionItemQuery("a");
            var itref = new ItemTransactionQuery("b");
            query.LeftJoin(itref).On(query.ReferenceNo == itref.TransactionNo);
            query.Select(
                query.TransactionNo,
                query.SequenceNo,
                query.ItemID,
                query.Description,
                query.Quantity,
                query.SRItemUnit,
                query.ReferenceNo,
                itref.Notes
                );
            query.Where(query.TransactionNo == Request.QueryString["tno"]);

            grdList.DataSource = query.LoadDataTable();
        }
       
    }
}
