using System;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.BusinessObject.Reference;
using Temiang.Avicenna.Common;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;
using System.Text;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class PaymentReceiveRenameDetail : BasePageDetail
    {

        #region Page Event & Initialize

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.PaymentReceiveRename;
            UrlPageList = "PaymentReceiveRenameRegistrationList.aspx?pc=no";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuPrintClick(ValidateArgs args, ref string programID,
                                                 PrintJobParameterCollection printJobParameters)
        {
            var hd = new TransPayment();
            if (hd.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                hd.PrintNumber++;
                if (!hd.IsPrinted ?? false)
                    hd.IsPrinted = true;
                hd.LastPrintedDateTime = (new DateTime()).NowAtSqlServer();
                hd.LastPrintedByUserID = AppSession.UserLogin.UserID;
                hd.Save();
            }

            if (AppSession.Parameter.IsUsedPrintSlipLogForPaymentReceipt)
                PrintSlipLog.InsertUpdate(programID, "PaymentNo", txtPaymentNo.Text, AppSession.UserLogin.UserID);

            switch (programID)
            {
                case AppConstant.Report.PaymentReceiptSlip:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("PaymentTypeDiscount", AppSession.Parameter.PaymentTypeDiscount);

                    break;
                case AppConstant.Report.PaymentReturnReceipt:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_PaymentRRtInPatientP:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("RegistrationNo", txtRegistrationNo.Text);
                    printJobParameters.AddNew("GuarantorAskesID", string.Empty);

                    break;
                case AppConstant.Report.RSSA_PaymentRRtInPatientG:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("RegistrationNo", txtRegistrationNo.Text);
                    printJobParameters.AddNew("GuarantorAskesID", string.Empty);//AppSession.Parameter.GuarantorAskesID);

                    break;
                case AppConstant.Report.PaymentReceiptDetailOutPatient:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiptDetailOutPatient2:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("PaymentTypeDiscount", AppSession.Parameter.PaymentTypeDiscount);
                    
                    break;
                case AppConstant.Report.PaymentReceiptPrescDetail:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiveReceiptNoDP:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiptDetail:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_Slip_Mandiri:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_Slip_Kalbar:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.RSSA_PaymentReceiveReceipt:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;

                case AppConstant.Report.PaymentReceiptGlobal:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    printJobParameters.AddNew("DownPayment", null,
                                              Helper.Payment.GetTotalDownPaymentOnly(
                                                  Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text)),
                                              null);
                    printJobParameters.AddNew("ParamedicTariffComponentID",
                                              AppSession.Parameter.ParamedicTariffComponentID);

                    var jobParameter = printJobParameters.AddNew();
                    jobParameter.Name = "RegistrationNo";
                    jobParameter.ValueString = string.Empty;

                    var merge = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);
                    foreach (var str in merge)
                    {
                        jobParameter.ValueString += str + ",";
                    }
                    jobParameter.ValueString = jobParameter.ValueString.Substring(0, jobParameter.ValueString.Length - 1);

                    break;
                case AppConstant.Report.SpectaclePrescriptionSlip:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);

                    break;
                case AppConstant.Report.StatementOfDebt:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;
                case AppConstant.Report.PaymentReceiptSlip2:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);

                    break;

                default:
                    printJobParameters.AddNew("PaymentNo", txtPaymentNo.Text);
                    printJobParameters.AddNew("UserName", AppSession.UserLogin.UserName);
                    break;
            }
        }

        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            var entity = new TransPayment();
            if (entity.LoadByPrimaryKey(txtPaymentNo.Text))
            {
                if (entity.IsVoid == true)
                    args.MessageText = AppConstant.Message.RecordHasVoided;
                else
                {
                    SetEntityValue(entity);
                    SaveEntity(entity);
                }
            }
            else
                args.MessageText = AppConstant.Message.RecordNotExist;
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            auditLogFilter.PrimaryKeyData = string.Format("PaymentNo='{0}'", txtPaymentNo.Text.Trim());
            auditLogFilter.TableName = "TransPayment";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            var entity = new TransPayment();
            if (parameters.Length > 0)
            {
                var paymentNo = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity.LoadByPrimaryKey(paymentNo);
            }
            else
                entity.LoadByPrimaryKey(txtPaymentNo.Text);
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity entity)
        {
            var transPayment = (TransPayment)entity;
            txtPaymentNo.Text = transPayment.PaymentNo;
            txtRegistrationNo.Text = transPayment.RegistrationNo;
            var registration = new Registration();
            registration.LoadByPrimaryKey(txtRegistrationNo.Text);
            txtServiceUnitID.Text = registration.ServiceUnitID;
            var patient = new Patient();
            if (patient.LoadByPrimaryKey(registration.PatientID))
            {
                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;
                var std = new AppStandardReferenceItem();
                txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            }
            else
            {
                txtMedicalNo.Text = string.Empty;
                txtPatientName.Text = string.Empty;
                txtSalutation.Text = string.Empty;
            }
            var unit = new ServiceUnit();
            unit.LoadByPrimaryKey(txtServiceUnitID.Text);
            lblServiceUnitName.Text = unit.ServiceUnitName;

            txtPrintReceiptAsName.Text = transPayment.PrintReceiptAsName;
            txtGuarantorID.Text = registration.GuarantorID;
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(txtGuarantorID.Text);
            lblGuarantorName.Text = guarantor.GuarantorName;

            txtPaymentDate.SelectedDate = transPayment.PaymentDate;
            txtPaymentTime.Text = transPayment.PaymentTime;

            txtNotes.Text = transPayment.Notes;
        }

        #endregion

        #region Private Method Standard

        private void SetEntityValue(esTransPayment entity)
        {
            entity.PrintReceiptAsName = txtPrintReceiptAsName.Text;
            entity.Notes = txtNotes.Text;
            
            entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
            entity.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
        }

        private void SaveEntity(TransPayment entity)
        {
            using (var trans = new esTransactionScope())
            {
                entity.Save();

                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            var que = new TransPaymentQuery();
            que.es.Top = 1; // SELECT TOP 1 ..
            if (isNextRecord)
            {
                que.Where(
                    que.PaymentNo > txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.Payment
                    //que.Or(que.TransactionCode == TransactionCode.Payment,
                    //       que.TransactionCode == TransactionCode.PaymentReturn)
                    );
                que.OrderBy(que.PaymentNo.Ascending);
            }
            else
            {
                que.Where(
                    que.PaymentNo < txtPaymentNo.Text,
                    que.TransactionCode == TransactionCode.Payment
                    );
                que.OrderBy(que.PaymentNo.Descending);
            }

            var entity = new TransPayment();
            if (entity.Load(que))
                OnPopulateEntryControl(entity);
        }

        #endregion

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            ToolBarMenuSearch.Enabled = false;
            ToolBarMenuAdd.Enabled = false;
        }
    }
}