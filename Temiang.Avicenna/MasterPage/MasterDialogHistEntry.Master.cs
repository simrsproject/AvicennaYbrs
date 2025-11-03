using System;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.MasterPage
{
    public partial class MasterDialogHistEntry : System.Web.UI.MasterPage
    {
        ///<summary>
        ///
        ///</summary>
        protected BasePageDialogHistEntry BasePageDialogHistEntryCurrent
        {
            get { return (BasePageDialogHistEntry)this.cphEntry.Page; }
        }

        protected void fw_RadScriptManager_AsyncPostBackError(object sender, System.Web.UI.AsyncPostBackErrorEventArgs e)
        {
            fw_RadScriptManager.AsyncPostBackErrorMessage = e.Exception.Message;
        }


        protected void fw_customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var custArgs = new ValidateArgs();
            BasePageDialogHistEntryCurrent.OnServerValidate(custArgs);
            args.IsValid = custArgs.IsCancel == false;
            ((CustomValidator)source).ErrorMessage = custArgs.MessageText;
        }
    }
}
