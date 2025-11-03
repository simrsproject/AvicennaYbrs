using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using Temiang.Avicenna.BusinessObject.Common;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.RADT.Emr
{
    public partial class PrescriptionTemplateNew : BasePageDialog
    {
        private string PrescriptionNo
        {
            get { return Request.QueryString["prescno"]; }
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
        private TransPrescriptionItemTemplateCollection TransPrescriptionItemTemplate(string templateNo)
        {
            var tpit = new TransPrescriptionItemTemplateCollection();
            tpit.Query.Where(tpit.Query.TemplateNo == templateNo);
            tpit.LoadAll();
            return tpit;
        }

        private string SaveAsNewTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName)) return "Template name is empty";

            var isFromPharmacy = false;
            using (var trans = new esTransactionScope())
            {
                TransPrescriptionItemCollection prescriptionSource;

                if (!string.IsNullOrEmpty(PrescriptionNo))
                {
                    // Save template from prescription history
                    prescriptionSource = new TransPrescriptionItemCollection();
                    var qr = new TransPrescriptionItemQuery("a");
                    qr.Where(qr.PrescriptionNo == PrescriptionNo);
                    prescriptionSource.Load(qr);
                }
                else
                {
                    // Save template from prescription entry
                    var obj = Session["collTransPrescriptionItem" + Request.UserHostName]; // Dipanggil dari PrescriptionEntry.aspx
                    if (obj == null)
                    {
                        isFromPharmacy=true;
                        obj = Session[Request.QueryString["sn"]];  // Dipanggil dari PrescriptionSalesDetail.aspx
                    }

                    if (obj != null)
                        prescriptionSource = ((TransPrescriptionItemCollection)(obj));
                    else
                        prescriptionSource = new TransPrescriptionItemCollection();
                }

                if (prescriptionSource != null)
                {
                    var entity = new TransPrescriptionTemplate();
                    entity.AddNew();
                    // generate new template no
                    AppAutoNumberLast _autoNumber;
                    _autoNumber = Helper.GetNewAutoNumber(DateTime.Now, AppEnum.AutoNumber.PrescTemplateNo);
                    entity.TemplateNo = _autoNumber.LastCompleteNumber;
                    _autoNumber.Save();

                    var dateserver = (new DateTime()).NowAtSqlServer();
                    entity.LastCreateUserID = AppSession.UserLogin.UserID;
                    entity.LastCreateDateTime = dateserver;

                    entity.TemplateName = templateName;
                    entity.ParamedicID = isFromPharmacy? "PHARMACY": AppSession.UserLogin.ParamedicID;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.LastUpdateDateTime = dateserver;
                    entity.Save();

                    // save detail
                    var tpit = TransPrescriptionItemTemplate(entity.TemplateNo);
                    tpit.MarkAllAsDeleted();

                    foreach (var t in prescriptionSource)
                    {
                        var n = tpit.AddNew();
                        n.TemplateNo = entity.TemplateNo;
                        n.SequenceNo = t.SequenceNo;
                        n.ParentNo = t.ParentNo;
                        n.IsRFlag = t.IsRFlag;
                        n.IsCompound = t.IsCompound;
                        n.ItemID = t.ItemID;
                        n.SRItemUnit = t.SRItemUnit;
                        n.ItemQtyInString = t.ItemQtyInString;
                        n.SRDosageUnit = t.SRDosageUnit;
                        n.PrescriptionQty = t.PrescriptionQty;
                        n.TakenQty = t.TakenQty;
                        n.ResultQty = t.ResultQty;
                        n.EmbalaceID = t.EmbalaceID;
                        n.EmbalaceAmount = t.EmbalaceAmount;
                        n.Notes = t.Notes;
                        n.SRConsumeMethod = t.SRConsumeMethod;
                        n.DosageQty = t.DosageQty;
                        n.EmbalaceQty = t.EmbalaceQty;
                        n.ConsumeQty = t.ConsumeQty;
                        n.SRConsumeUnit = t.SRConsumeUnit;
                        n.LastCreateUserID = AppSession.UserLogin.UserID;
                        n.LastCreateDateTime = dateserver;
                        n.LastUpdateByUserID = AppSession.UserLogin.UserID;
                        n.LastUpdateDateTime = dateserver;
                        n.SRMedicationConsume = t.Acpcdc;
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
                Session["prescTemplate"] = null;
                Helper.ShowMessageAfterPostback(this, "Prescription Template has save with name " + txtTemplateName.Text);
            }
        }

    }
}
