using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Common
{
    public class BasePageDialog : BasePage
    {
        override protected string SignatureUrlPage
        {
            get
            {
                return "~/Login/PassCodeWithoutMenu.aspx";
            }
        }
        protected void ShowMessage(string msg)
        {
            var hdf = (HiddenField)Helper.FindControlRecursive(Master, "hdfMessage");
            hdf.Value = msg;
        }

        public virtual string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return string.Empty;
        }

        public virtual bool OnButtonOkClicked()
        {
            ValidateArgs args = new ValidateArgs();
            OnButtonOkClicked(args);
            if (args.IsCancel || args.MessageText != string.Empty)
            {
                ShowInformationHeader(args.MessageText);
                return false;
            }
            return true;
        }

        protected virtual void OnButtonOkClicked(ValidateArgs args)
        {
        }

        protected Panel Footer
        {
            get
            {
                var pnl = (Panel)Helper.FindControlRecursive(Master, "panFooter");
                return pnl;
            }
        }

        protected bool FooterVisible
        {
            get
            {
                var pnl = (Panel)Helper.FindControlRecursive(Master, "panFooter");
                return pnl.Visible;
            }
            set
            {
                var pnlSep = (Panel)Helper.FindControlRecursive(Master, "pnlBottomSep");
                pnlSep.Visible = value;

                var pnl = (Panel)Helper.FindControlRecursive(Master, "panFooter");
                pnl.Visible = value;

            }
        }

        protected Button ButtonOk
        {
            get
            {
                var btn = (Button)Helper.FindControlRecursive(Master, "btnOk");
                return btn;
            }
        }
        protected Button ButtonCancel
        {
            get
            {
                var btn = (Button)Helper.FindControlRecursive(Master, "btnCancel");
                return btn;
            }
        }
        private RadAjaxManager _ajaxManager;
        protected RadAjaxManager AjaxManager
        {
            get
            {
                return _ajaxManager ?? (_ajaxManager = (RadAjaxManager)Helper.FindControlRecursive(this, "fw_RadAjaxManager"));
            }
        }
        protected void ShowInformationHeader(string messageText)
        {
            var pnlInfo = (Panel)Helper.FindControlRecursive(Master, "fw_PanelInfo");
            var lblInfo = (Label)Helper.FindControlRecursive(Master, "fw_LabelInfo");
            lblInfo.Text = messageText;
            pnlInfo.Visible = true;
        }

        protected void HideInformationHeader()
        {
            var pnlInfo = (Panel)Helper.FindControlRecursive(Master, "fw_PanelInfo");
            var lblInfo = (Label)Helper.FindControlRecursive(Master, "fw_LabelInfo");
            lblInfo.Text = string.Empty;
            pnlInfo.Visible = false;
        }
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            AjaxManager.AjaxSettings.Clear();
            OnInitializeAjaxManagerSettingsCollection(AjaxManager.AjaxSettings);
        }
        protected virtual void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        protected AppEnum.DataMode DataModeCurrent
        {
            get
            {
                var dataMode = Request.QueryString["mod"];
                switch (dataMode)
                {
                    case "new":
                        return AppEnum.DataMode.New;
                    case "edit":
                        return AppEnum.DataMode.New;
                }
                return AppEnum.DataMode.Read;
            }
        }
    }
}
