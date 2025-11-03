using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class LaundryItemsDetail : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "Laundry Items";

                var item = new Item();
                if (item.LoadByPrimaryKey(Request.QueryString["id"]))
                {
                    txtItemID.Text = item.ItemID;
                    txtItemName.Text = item.ItemName;
                    txtNotes.Text = item.Notes;
                    chkIsActive.Checked = item.IsActive ?? false;
                }
                else
                {
                    txtItemID.Text = string.Empty;
                    txtItemName.Text = string.Empty;
                    txtNotes.Text = string.Empty;
                    chkIsActive.Checked = false;
                }

                var ipnm = new ItemProductNonMedic();
                if (ipnm.LoadByPrimaryKey(Request.QueryString["id"]))
                {
                    txtSRItemUnit.Text = ipnm.SRItemUnit;
                    txtWeight.Value = Convert.ToDouble(ipnm.Weight ?? 0);
                }
                else
                {
                    txtSRItemUnit.Text = string.Empty;
                    txtWeight.Value = 0;
                }
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            if (!Page.IsValid)
                return false;

            using (var trans = new esTransactionScope())
            {
                var ipnm = new ItemProductNonMedic();
                if (ipnm.LoadByPrimaryKey(Request.QueryString["id"]))
                {
                    ipnm.Weight = Convert.ToDecimal(txtWeight.Value);
                    ipnm.Save();
                }
                
                trans.Complete();
            }

            return true;
        }
    }
}