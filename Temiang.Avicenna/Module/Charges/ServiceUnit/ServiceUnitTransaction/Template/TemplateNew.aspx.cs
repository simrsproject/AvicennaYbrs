using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.ServiceUnitTransaction
{
    public partial class TemplateNew : BasePageDialog
    {
        private string TransactionNo
        {
            get { return Request.QueryString["transNo"]; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "Save As New Template";
            }
        }
        #region Save as Template
        private TransChargesItemTemplateCollection TransChargesItemTemplate(string templateNo)
        {
            var tpit = new TransChargesItemTemplateCollection();
            tpit.Query.Where(tpit.Query.TemplateNo == templateNo);
            tpit.LoadAll();
            return tpit;
        }

        private string SaveAsNewTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName)) return "Template name is empty";
            using (var trans = new esTransactionScope())
            {
                TransChargesItemCollection transChargesSource;

                // Save template from job order entry
                var obj = Session["collTransChargesItem" + Request.UserHostName + Request.QueryString["pageId"]];
                if (obj != null)
                    transChargesSource = ((TransChargesItemCollection)(obj));
                else
                    transChargesSource = new TransChargesItemCollection();

                if (transChargesSource != null)
                {
                    var entity = new TransChargesTemplate();
                    entity.AddNew();
                    // generate new template no
                    AppAutoNumberLast _autoNumber;
                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.JobOrderTemplateNo);
                    entity.TemplateNo = _autoNumber.LastCompleteNumber;
                    _autoNumber.Save();

                    var dateserver = (new DateTime()).NowAtSqlServer();
                    entity.LastCreateUserID = AppSession.UserLogin.UserID;
                    entity.LastCreateDateTime = dateserver;

                    entity.TemplateName = templateName;
                    entity.ParamedicID = AppSession.UserLogin.ParamedicID;
                    entity.ToServiceUnitID = Request.QueryString["toUnit"];
                    entity.IsDeleted = false;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = dateserver;
                    entity.Save();

                    // save detail
                    var tpit = TransChargesItemTemplate(entity.TemplateNo);
                    tpit.MarkAllAsDeleted();

                    foreach (var t in transChargesSource)
                    {
                        var n = tpit.AddNew();
                        n.TemplateNo = entity.TemplateNo;
                        n.SequenceNo = t.SequenceNo;
                        n.ItemID = t.ItemID;
                        n.ChargeQuantity = t.ChargeQuantity;
                        n.LastCreateUserID = AppSession.UserLogin.UserID;
                        n.LastCreateDateTime = dateserver;
                        n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        n.LastUpdateDateTime = dateserver;
                    }
                    tpit.Save();
                }
                trans.Complete();
            }
            return string.Empty;
        }
        #endregion

        protected override void OnButtonOkClicked(ValidateArgs args)
        {
            var msg = SaveAsNewTemplate(txtTemplateName.Text);
            if (!string.IsNullOrEmpty(msg))
            {
                args.MessageText = msg;
                args.IsCancel = true;
            }
            else
            {
                // Reset session untuk data grid template pada parent page
                Helper.ShowMessageAfterPostback(this, "Exam Order Template has save with name " + txtTemplateName.Text);
            }
        }
    }
}