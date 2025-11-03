using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Temiang.Avicenna.BusinessObject;

namespace Temiang.Avicenna.Module.Charges
{
    public partial class PatientDepositDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["type"]))
                ProgramID = AppConstant.Program.DownPayment;
            else
                ProgramID = AppConstant.Program.PatientDepositReturn;
        }

        protected void grdPatientDeposit_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            var query = new TransPaymentPatientQuery("a");
            var patient = new PatientQuery("b");
            var detail = new TransPaymentPatientItemQuery("c");
            var asri1 = new AppStandardReferenceItemQuery("d");
            var asri2 = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                    detail.PaymentNo,
                    detail.SequenceNo,
                    query.PaymentDate,
                    query.Notes,
                    asri1.ItemName.As("PaymentTypeName"),
                    asri2.ItemName.As("PaymentMethodName"),
                    detail.Amount
                );
            query.InnerJoin(patient).On(query.PatientID == patient.PatientID);
            query.InnerJoin(detail).On(query.PaymentNo == detail.PaymentNo);
            query.InnerJoin(asri1).On(detail.SRPaymentType == asri1.ItemID && asri1.StandardReferenceID == AppEnum.StandardReference.PaymentType.ToString());
            query.InnerJoin(asri2).On(detail.SRPaymentMethod == asri2.ItemID && asri2.StandardReferenceID == AppEnum.StandardReference.PaymentMethod.ToString());
            query.Where(
                query.Or(query.PatientID == Request.QueryString["id"], patient.MedicalNo == Request.QueryString["id"]),
                query.IsApproved == true,
                query.Or(query.ReferenceNo.IsNull(), query.ReferenceNo == string.Empty),
                query.TransactionCode == Temiang.Avicenna.BusinessObject.Reference.TransactionCode.DownPayment);
            query.OrderBy(query.PaymentNo.Ascending);

            grdPatientDeposit.DataSource = query.LoadDataTable();
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument = 'deposit'";
        }

        public override bool OnButtonOkClicked()
        {
            foreach (Telerik.Web.UI.GridDataItem dataItem in grdPatientDeposit.MasterTableView.Items)
            {
                if (!((CheckBox)dataItem.FindControl("detailChkbox")).Checked) continue;

                var deposit = new TransPaymentPatientItem();
                deposit.LoadByPrimaryKey(dataItem.GetDataKeyValue("PaymentNo").ToString(), dataItem.GetDataKeyValue("SequenceNo").ToString());

                if (ProgramID == AppConstant.Program.DownPayment)
                {
                    var coll = Session["DownPayment:collTransPaymentItem" + Request.UserHostName] as TransPaymentItemCollection;
                    var entity = coll.Where(c => c.ReferenceNo == deposit.PaymentNo && c.ReferenceSequenceNo == deposit.SequenceNo).SingleOrDefault();
                    if (entity == null)
                    {
                        var sequenceNo = string.Empty;
                        if (!coll.HasData) sequenceNo = "001";
                        else
                        {
                            var i = (coll.OrderByDescending(c => c.SequenceNo).Select(c => c.SequenceNo)).Take(1);
                            int seqNo = int.Parse(i.Single()) + 1;
                            sequenceNo = string.Format("{0:000}", seqNo);
                        }

                        entity = coll.AddNew();
                        entity.PaymentNo = string.Empty;

                        //var sequenceNo = string.Empty;
                        //if (!string.IsNullOrEmpty(coll[coll.Count - 1].SequenceNo))
                        //{
                        //    int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                        //    sequenceNo = string.Format("{0:000}", seqNo);
                        //}
                        //else sequenceNo = "001";

                        entity.SequenceNo = sequenceNo;
                    }
                    else entity = coll.FindByPrimaryKey(entity.PaymentNo, entity.SequenceNo);

                    entity.SRPaymentType = deposit.SRPaymentType;

                    var asri = new AppStandardReferenceItem();
                    asri.LoadByPrimaryKey(AppEnum.StandardReference.PaymentType.ToString(), deposit.SRPaymentType);
                    entity.PaymentTypeName = asri.ItemName;

                    entity.SRPaymentMethod = deposit.SRPaymentMethod;

                    asri = new AppStandardReferenceItem();
                    asri.LoadByPrimaryKey(AppEnum.StandardReference.PaymentMethod.ToString(), deposit.SRPaymentMethod);
                    entity.PaymentMethodName = asri.ItemName;

                    entity.str.SRCardProvider = deposit.SRCardProvider;
                    entity.str.SRCardType = deposit.SRCardType;
                    entity.str.EDCMachineID = deposit.EDCMachineID;
                    entity.CardHolderName = deposit.CardHolderName;
                    entity.CardFeeAmount = deposit.CardFeeAmount;
                    entity.BankID = deposit.BankID;
                    entity.Amount = deposit.Amount;
                    entity.IsFromDownPayment = false;
                    entity.CardNo = deposit.CardNo;
                    entity.ReferenceNo = deposit.PaymentNo;
                    entity.ReferenceSequenceNo = deposit.SequenceNo;
                }
                else
                {
                    var coll = Session["DownPayment:collTransPaymentItem" + Request.UserHostName] as TransPaymentPatientItemCollection;
                    var entity = coll.Where(c => c.ReferenceNo == deposit.PaymentNo && c.ReferenceSequenceNo == deposit.SequenceNo).SingleOrDefault();
                    if (entity == null)
                    {
                        entity = coll.AddNew();
                        entity.PaymentNo = string.Empty;

                        var sequenceNo = string.Empty;
                        if (!string.IsNullOrEmpty(coll[coll.Count - 1].SequenceNo))
                        {
                            int seqNo = int.Parse(coll[coll.Count - 1].SequenceNo) + 1;
                            sequenceNo = string.Format("{0:000}", seqNo);
                        }
                        else sequenceNo = "001";

                        entity.SequenceNo = sequenceNo;
                    }
                    else entity = coll.FindByPrimaryKey(entity.PaymentNo, entity.SequenceNo);

                    entity.SRPaymentType = deposit.SRPaymentType;

                    var asri = new AppStandardReferenceItem();
                    asri.LoadByPrimaryKey(AppEnum.StandardReference.PaymentType.ToString(), deposit.SRPaymentType);
                    entity.PaymentTypeName = asri.ItemName;

                    entity.SRPaymentMethod = deposit.SRPaymentMethod;

                    asri = new AppStandardReferenceItem();
                    asri.LoadByPrimaryKey(AppEnum.StandardReference.PaymentMethod.ToString(), deposit.SRPaymentMethod);
                    entity.PaymentMethodName = asri.ItemName;

                    entity.str.SRCardProvider = deposit.SRCardProvider;
                    entity.str.SRCardType = deposit.SRCardType;
                    entity.str.EDCMachineID = deposit.EDCMachineID;
                    entity.CardHolderName = deposit.CardHolderName;
                    entity.CardFeeAmount = deposit.CardFeeAmount;
                    entity.BankID = deposit.BankID;
                    entity.Amount = deposit.Amount;
                    entity.CardNo = deposit.CardNo;
                    entity.ReferenceNo = deposit.PaymentNo;
                    entity.ReferenceSequenceNo = deposit.SequenceNo;
                }
            }

            return true;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var selected = ((CheckBox)sender).Checked;

            foreach (Telerik.Web.UI.GridDataItem dataItem in grdPatientDeposit.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }
    }
}
