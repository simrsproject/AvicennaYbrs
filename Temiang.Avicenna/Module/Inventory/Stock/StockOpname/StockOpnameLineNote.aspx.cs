using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Inventory.StockOpname.RSCH
{
    public partial class StockOpnameLineNote : BasePageDialog
    {
        private string TransactionNo
        {
            get { return Request.QueryString["trno"];}
        }
        private string SeqNo
        {
            get { return Request.QueryString["seqno"];}
        }
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var line  = new ItemTransactionItem();
                line.LoadByPrimaryKey(TransactionNo, SeqNo);

                var item = new Item();
                item.LoadByPrimaryKey(line.ItemID);

                txtItemID.Text = item.ItemID;
                txtItemName.Text = item.ItemName;

                txtNote.Text = line.Note;
                txtNote.Focus();
            }
        }

        public override bool OnButtonOkClicked()
        {
            var line  = new ItemTransactionItem();
            line.LoadByPrimaryKey(TransactionNo, SeqNo);

            line.Note = txtNote.Text;
            line.Save();

            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'ok'";
        }
    }
}