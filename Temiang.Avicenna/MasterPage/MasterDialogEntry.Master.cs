using System;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;

namespace Temiang.Avicenna.MasterPage
{
    public partial class MasterDialogEntry : System.Web.UI.MasterPage
    {
        protected int? _autoSaveInterval = null;
        protected BasePageDialogEntry BasePageDialogEntryCurrent
        {
            get { return (BasePageDialogEntry)this.ContentPlaceHolder1.Page; }
        }

        protected void fw_RadScriptManager_AsyncPostBackError(object sender, System.Web.UI.AsyncPostBackErrorEventArgs e)
        {
            //var ex = Server.GetLastError();
            //Logger.LogException(ex);
            fw_RadScriptManager.AsyncPostBackErrorMessage = e.Exception.Message;
        }

        protected void fw_customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var custArgs = new ValidateArgs();
            BasePageDialogEntryCurrent.OnServerValidate(custArgs);
            args.IsValid = custArgs.IsCancel == false;
            ((CustomValidator)source).ErrorMessage = custArgs.MessageText;
        }
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //    if (!IsPostBack)
        //    {
        //        // Add Save And Edit
        //        var tbi = new RadToolBarButton();
        //        tbi.Text = "Save and Edit";
        //        tbi.ImageUrl = "~/Images/Toolbar/edit16.png";
        //        tbi.Value = "saveandedit";
        //        tbi.OuterCssClass = "rightButton";
        //        fw_tbarData.Items.Add(tbi);


        //        // Add Save And Edit
        //        var tbc = new RadToolBarButton();
        //        tbc.Text = "Autosave";
        //        tbc.ImageUrl = "~/Images/Toolbar/Scheduler16.png";
        //        tbc.Value = "autosave";
        //        tbc.OuterCssClass = "rightButton";
        //        tbc.CheckOnClick = true;
        //        tbc.AllowSelfUnCheck = true;
        //        tbc.CheckOnClick = true;
        //        tbc.PostBack = false;
        //        fw_tbarData.Items.Add(tbc);
        //    }
        //}

        protected void fw_btnAutoSave_Click(object sender, EventArgs e)
        {
            var args = new ValidateArgs();
            try
            {
                BasePageDialogEntryCurrent.OnMenuSaveAndEditClick(args);
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
