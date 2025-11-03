using System;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.MasterPage
{
    public partial class MasterDetail : System.Web.UI.MasterPage
    {
        protected BasePageDetail BasePageDetailCurrent
        {
            get { return (BasePageDetail)this.ContentPlaceHolder1.Page; }
        }

        protected void fw_RadScriptManager_AsyncPostBackError(object sender, System.Web.UI.AsyncPostBackErrorEventArgs e)
        {
            //var ex = Server.GetLastError();
            //Logger.LogException(ex);
            fw_RadScriptManager.AsyncPostBackErrorMessage = e.Exception.Message;
        }
        protected void fw_btnAutoSave_Click(object sender, EventArgs e)
        {
            var args = new ValidateArgs();
            try
            {
                BasePageDetailCurrent.OnMenuSaveAndEditClick(args);
            }
            catch (Exception ex)
            {
                args.IsCancel = true;
                args.MessageText = ex.Message;
            }

            if (!args.IsCancel)
            {
                fw_hdnRecordHasChanged.Value = "1";
                fw_hdnDataMode.Value = "1"; // AppEnum.DataMode.Edit
            }
            if (!string.IsNullOrEmpty(args.MessageText))
            {
                fw_radNotif.Text = args.MessageText;
                fw_radNotif.Show();
            }
        }
    }
}
