using System;
using System.Linq;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI.WebControls;
using DevExpress.XtraRichEdit.Mouse;

namespace Temiang.Avicenna.Module.Charges.FinalizeBilling
{
    public partial class ChargeClass : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.VerificationFinalizeBilling;

            if (!IsPostBack)
            {
                var cls = new ClassCollection();
                cls.Query.Where(cls.Query.IsTariffClass == true, cls.Query.IsActive == true);
                cls.Query.OrderBy(cls.Query.ClassSeq.Ascending);
                cls.LoadAll();

                cboChargeClass.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
               
                foreach (var c in cls)
                {
                    cboChargeClass.Items.Add(new RadComboBoxItem(c.ClassName, c.ClassID));
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public override bool OnButtonOkClicked()
        {
            Validate();

            if (!IsValid)
                return false;

            string msg = string.Empty;
            bool isNotValid = false;

            if (string.IsNullOrEmpty(cboChargeClass.SelectedValue))
            {
                isNotValid = true;
                msg = "Charge Class is required.";
            }
            else
            {
                var c = new Class();
                if (!c.LoadByPrimaryKey(cboChargeClass.SelectedValue))
                {
                    isNotValid = true;
                    msg = "Invalid Charge Class.";
                }
            }

            //if (!isNotValid)
            //{
            //    var refNo = Request.QueryString["transNo"];
            //    var refTransHds = new TransChargesCollection();
            //    refTransHds.Query.Where(refTransHds.Query.ReferenceNo == refNo, refTransHds.Query.IsVoid == false);
            //    refTransHds.LoadAll();
            //    if (refTransHds.Count > 0)
            //    {
            //        isNotValid = true;

            //        var transNos = string.Empty;
            //        foreach (var i in refTransHds)
            //        {
            //            if (transNos == string.Empty)
            //                transNos = "This transaction number already has correction transaction with no: " + i.TransactionNo + "(" + (i.IsApproved ?? false ? "Approved" : "Saved") + ")";
            //            else
            //                transNos = transNos + ", " + i.TransactionNo + "(" + (i.IsApproved ?? false ? "Approved" : "Saved") + ")";
            //        }
            //    }
            //    else
            //    {
            //        var refPrescHds = new TransPrescriptionCollection();
            //        refPrescHds.Query.Where(refPrescHds.Query.ReferenceNo == refNo, refPrescHds.Query.IsVoid == false);
            //        refPrescHds.LoadAll();
            //        if (refPrescHds.Count > 0)
            //        {
            //            isNotValid = true;

            //            var prescNos = string.Empty;
            //            foreach (var i in refPrescHds)
            //            {
            //                if (prescNos == string.Empty)
            //                    prescNos = "This prescription number already has returned transaction with no: " + i.PrescriptionNo + "(" + (i.IsApproval ?? false ? "Approved" : "Saved") + ")";
            //                else
            //                    prescNos = prescNos + ", " + i.PrescriptionNo + "(" + (i.IsApproval ?? false ? "Approved" : "Saved") + ")";
            //            }
            //        }
            //    }
            //}

            if (!isNotValid)
            {
                using (var trans = new esTransactionScope())
                {
                    var transNo = Request.QueryString["transNo"];

                    var transHD = new TransCharges();
                    if (transHD.LoadByPrimaryKey(transNo))
                    {
                        transHD.ClassID = cboChargeClass.SelectedValue;

                        var transDT = new TransChargesItemCollection();
                        transDT.Query.Where(transDT.Query.TransactionNo == transNo);
                        transDT.LoadAll();

                        foreach (var i in transDT)
                        {
                            i.ChargeClassID = cboChargeClass.SelectedValue;
                        }

                        transHD.Save();
                        transDT.Save();

                        var correctionHds = new TransChargesCollection();
                        correctionHds.Query.Where(correctionHds.Query.ReferenceNo == transNo, correctionHds.Query.IsApproved == true, correctionHds.Query.IsVoid == false);
                        correctionHds.LoadAll();

                        foreach (var hd in correctionHds)
                        {
                            hd.ClassID = cboChargeClass.SelectedValue;

                            var correctionDts = new TransChargesItemCollection();
                            correctionDts.Query.Where(correctionDts.Query.ReferenceNo == transNo);
                            correctionDts.LoadAll();

                            foreach (var dt in correctionDts)
                            {
                                dt.ChargeClassID = cboChargeClass.SelectedValue;
                            }

                            if (correctionDts != null)
                                correctionDts.Save();
                        }

                        if (correctionHds != null)
                            correctionHds.Save();
                    }
                    else
                    {
                        var prescHD = new TransPrescription();
                        if (prescHD.LoadByPrimaryKey(transNo))
                        {
                            prescHD.ClassID = cboChargeClass.SelectedValue;
                            prescHD.Save();

                            var returnHds = new TransPrescriptionCollection();
                            returnHds.Query.Where(returnHds.Query.ReferenceNo == transNo, returnHds.Query.IsApproval == true, returnHds.Query.IsVoid == false);
                            returnHds.LoadAll();

                            foreach (var hd in returnHds)
                            {
                                hd.ClassID = cboChargeClass.SelectedValue;
                            }

                            if (returnHds != null)
                                returnHds.Save();
                        }
                    }

                    trans.Complete();
                }
            }
            else
                ShowInformationHeader(msg);

            return !isNotValid;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'rebind'";
        }

    }
}