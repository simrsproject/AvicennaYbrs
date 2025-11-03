using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Inventory.Warehouse
{
    public partial class UpdateItemBarcode : BasePageDialog
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtBarcode.Text = Request.QueryString["bc"];
            txtBarcodeEntry.Focus();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            // Untuk data refresh item id bersangkutan
            var script = @" //create the argument that will be returned to the parent page
                    var oArg = new Object();
                    oArg.callbackMethod = 'submit';
                    oArg.eventArgument = 'updbarcode|" + Request.QueryString["id"]+"|"+txtBarcodeEntry.Text + @"';
                    oArg.eventTarget = '" + Request.QueryString["cet"] + @"';

                    //Close the RadWindow            
                    oWnd.close(oArg);";

            return script;
        }
        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (txtBarcodeEntry.Text == string.Empty)
            {
                args.IsCancel = true;
                args.MessageText = "Barcode can't empty";
                return;
            }

            var item = new Item();
            if (item.LoadByPrimaryKey(Request.QueryString["id"]))
            {
                item.Barcode = txtBarcodeEntry.Text;
                item.Save();
            }
        } 
    }
}
