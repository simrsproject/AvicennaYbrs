using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Cpoe.EmrCommon.OperatingNotes
{
    public partial class OperatingNotesTemplateNew : BasePageDialog
    {
        private string ParamedicID
        {
            get { return Request.QueryString["parid"]; }
        }

        private string FormType
        {
            get { return this.Request.QueryString["tp"]; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Save As New Template";
                txtOperatingNotes.Text = HttpContext.Current.Server.UrlDecode(Request.QueryString["notes"]);
            }
        }

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            if (string.IsNullOrEmpty(txtTemplateName.Text))
            {
                args.IsCancel = true;
                args.MessageText = "Template name can't empty.";
                args.IsCancel = true;
                return;
            }
            if (string.IsNullOrEmpty(txtOperatingNotes.Text))
            {
                args.IsCancel = true;
                args.MessageText = FormType == "opr" ? "Operating notes can't empty." : (FormType == "ans" ? "Anesthetist notes can't empty." : "Post surgery instructions can't empty.");
                args.IsCancel = true;
                return;
            }
            SaveAsNewTemplate(txtTemplateName.Text, txtOperatingNotes.Text);

            // Reset session untuk data grid template pada parent page
            var msg = FormType == "opr" ? "Operating Notes Template" : (FormType == "ans" ? "Anesthetist Notes Template" : "Post Surgery Instructions Template");
            Helper.ShowMessageAfterPostback(this, msg + " has save with name " + txtTemplateName.Text);
        }

        private void SaveAsNewTemplate(string templateName, string operatingNotes)
        {
            var newID = 1;
            var qr = new OperationNotesTemplateQuery();
            qr.es.Top = 1;
            qr.OrderBy(qr.TemplateID.Descending);
            qr.Select(qr.TemplateID);
            var dtb = qr.LoadDataTable();
            if (dtb.Rows != null && dtb.Rows.Count > 0)
            {
                newID = (dtb.Rows[0][0]).ToInt() + 1;
            }

            var ent = new OperationNotesTemplate();
            ent.TemplateID = newID;
            ent.TemplateName = templateName;
            ent.TemplateText = operatingNotes;
            ent.ParamedicID = ParamedicID;
            ent.IsPostOp = (FormType == "psi" || FormType == "apsi");
            ent.Save();
        }
    }
}
