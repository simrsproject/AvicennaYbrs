using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.CashManagement
{
    public partial class PaymentReceivePickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASHIER_CORRECTION;

            if (!Page.IsPostBack) {
                SelectedPay = null;

                cboEDCMachineID.Items.Clear();
                var query = new EDCMachineQuery();
                query.Where(
                        query.IsActive == true
                    ).Select(query.EDCMachineName);
                query.es.Distinct = true;

                var edc = query.LoadDataTable();
                cboEDCMachineID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                for (var i = 0; i < edc.Rows.Count; i++)
                {
                    cboEDCMachineID.Items.Add(new RadComboBoxItem((string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineName],
                        (string)edc.Rows[i][EDCMachineMetadata.ColumnNames.EDCMachineName]));
                }
            }
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                var tp = (new TransPaymentItem())
                    .GetPaymentDetailForCorrectionWithPaging(this.grdListItem.CurrentPageIndex, this.grdListItem.PageSize,
                    txtPaymentNo.Text, txtRegistrationNo.Text, txtPatientName.Text,
                    txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate, 
                    txtAmount.Value, cboEDCMachineID.Text);

                var iCount = (new TransPaymentItem())
                    .GetPaymentDetailForCorrectionWithPagingCount(
                    txtPaymentNo.Text, txtRegistrationNo.Text, txtPatientName.Text,
                    txtPaymentDateFrom.SelectedDate, txtPaymentDateTo.SelectedDate,
                    txtAmount.Value, cboEDCMachineID.Text);

                grdListItem.VirtualItemCount = iCount;
                grdListItem.DataSource = tp;
            }
        }

        protected void grdListItem_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var item = ((GridDataItem)e.Item);
                var chk = item.FindControl("Chk") as CheckBox;
                if (chk != null)
                {
                    DataRowView dr = (DataRowView)item.DataItem;
                    var iCount = SelectedPay.Where(x => x.PaymentNo == dr["PaymentNo"].ToString() &&
                        x.SequenceNo == dr["SequenceNo"].ToString()).Count();

                    chk.Checked = iCount > 0;
                }
            }
        }

        private class SelectedPayment {
            public string PaymentNo;
            public string SequenceNo;
            public decimal Value;
        }

        private List<SelectedPayment> SelectedPay {
            get
            {
                if (Session["SelectedPay"] == null)
                {
                    Session["SelectedPay"] = new List<SelectedPayment>();
                }
                return (List<SelectedPayment>)Session["SelectedPay"];
            }
            set {
                Session["SelectedPay"] = value;
            }
        }

        protected void Chk_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;

            GridDataItem gdi = ((GridDataItem)chk.Parent.Parent);

            string paymentNo = gdi.GetDataKeyValue("dataKey").ToString().Split('|')[0];
            string seqNo = gdi.GetDataKeyValue("dataKey").ToString().Split('|')[1];
            decimal val = System.Convert.ToDecimal(gdi["Amount"].Text.Replace("&nbps;", "").Trim());

            if (chk.Checked)
            {
                SelectedPay.Add(new SelectedPayment()
                {
                    PaymentNo = paymentNo,
                    SequenceNo = seqNo,
                    Value = val
                });
            }
            else {
                var s = SelectedPay.Where(x => x.PaymentNo == paymentNo && x.SequenceNo == seqNo).FirstOrDefault();
                if (s != null) {
                    SelectedPay.Remove(s);
                }
            }

            UpdateInfoHeader();
        }

        private void UpdateInfoHeader() {
            lblSelectedCount.Text = SelectedPay.Count.ToString();
            lblSelectedAmount.Text = SelectedPay.Sum(x => x.Value).ToString("N2");
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdListItem.Rebind();
        }

        private bool HasSelected() {
            //foreach (var gi in grdListItem.Items)
            //{
            //    if (gi is GridDataItem)
            //    {
            //        var item = ((GridDataItem)gi);
            //        var chk = item.FindControl("Chk") as CheckBox;
            //        if (chk != null)
            //        {
            //            if (chk.Checked) return true;
            //        }
            //    }
            //}
            //return false;
            return SelectedPay.Count > 0;
        }

        public override bool OnButtonOkClicked()
        {
            if (!HasSelected())
                return false;

            var tpicColl = (TransPaymentItemCorrectionCollection)Session["colTransPaymentCorrectionItems" + Request.UserHostName];

            foreach (var pay in SelectedPay) {

                var coll = new TransPaymentItemCorrectionCollection();
                var tpic = new TransPaymentItemCorrectionQuery("tpic");
                var tpi = new TransPaymentItemQuery("tpi");
                var tp = new TransPaymentQuery("tp");
                var reg = new RegistrationQuery("reg");
                var pat = new PatientQuery("pat");

                var pm = new PaymentMethodQuery("pm");

                var cpo = new AppStandardReferenceItemQuery("cpo");
                var cto = new AppStandardReferenceItemQuery("cto");
                var edco = new EDCMachineQuery("edco");

                var cpc = new AppStandardReferenceItemQuery("cpc");
                var ctc = new AppStandardReferenceItemQuery("ctc");
                var edcc = new EDCMachineQuery("edcc");


                tpic.RightJoin(tpi).On(tpic.PaymentNo == tpi.PaymentNo && tpic.SequenceNo == tpi.SequenceNo)
                    .InnerJoin(tp).On(tpi.PaymentNo == tp.PaymentNo)
                    .InnerJoin(reg).On(tp.RegistrationNo == reg.RegistrationNo)
                    .InnerJoin(pat).On(reg.PatientID == pat.PatientID)

                    .InnerJoin(pm).On(tpi.SRPaymentType == pm.SRPaymentTypeID && tpi.SRPaymentMethod == pm.SRPaymentMethodID)

                    .LeftJoin(cpo).On(cpo.StandardReferenceID == "CardProvider" && tpi.SRCardProvider == cpo.ItemID)
                    .LeftJoin(cto).On(cto.StandardReferenceID == "CardType" && tpi.SRCardType == cto.ItemID)
                    .LeftJoin(edco).On(tpi.EDCMachineID == edco.EDCMachineID)

                    .LeftJoin(cpc).On(cpc.StandardReferenceID == "CardProvider" && tpic.SRCardProvider == cpc.ItemID)
                    .LeftJoin(ctc).On(ctc.StandardReferenceID == "CardType" && tpic.SRCardType == ctc.ItemID)
                    .LeftJoin(edcc).On(tpic.EDCMachineID == edcc.EDCMachineID)

                    .Select(
                        //tpic,
                        tpic.PaymentCorrectionNo,
                        tpi.PaymentNo,
                        tpi.SequenceNo,
                        tpi.SRPaymentType,
                        tpi.SRPaymentMethod,
                        reg.RegistrationNo.As("refToRegistration_RegistrationNo"),
                        pat.PatientName.As("refToPatient_PatientName"),
                        pm.PaymentMethodName.As("refToPM_PaymentMethodOName"),
                        cpo.ItemName.As("refToCP_CardProviderOName"),
                        cto.ItemName.As("refToCT_CardTypeOName"),
                        edco.EDCMachineName.As("refToEDC_EDCMachineOName"),
                        cpc.ItemName.As("refToCP_CardProviderCName"),
                        ctc.ItemName.As("refToCT_CardTypeCName"),
                        edcc.EDCMachineName.As("refToEDC_EDCMachineCName"),
                        tpi.Amount.As("refToPayment_Amount")
                    )

                    .Where(tpi.PaymentNo == pay.PaymentNo, tpi.SequenceNo == pay.SequenceNo);

                if (coll.Load(tpic)) {
                    foreach (var c in coll) {
                        c.PaymentNo = pay.PaymentNo;
                        c.SequenceNo = pay.SequenceNo;
                        c.PaymentCorrectionNo = "";

                        if (!tpicColl.Where(x => x.PaymentNo == c.PaymentNo &&
                            x.SequenceNo == c.SequenceNo).Any()) {
                                var cNew = tpicColl.AddNew();
                                cNew.PaymentCorrectionNo = string.Empty;
                                cNew.PaymentNo = c.PaymentNo;
                                cNew.SequenceNo = c.SequenceNo;
                                cNew.SRPaymentType = c.SRPaymentType;
                                cNew.SRPaymentMethod = c.SRPaymentMethod;
                                //cNew.SRCardProvider = c.SRCardProvider;
                                //cNew.SRCardType = c.SRCardType;
                                //cNew.EDCMachineID = c.EDCMachineID;
                                //cNew.CardFeeAmount = c.CardFeeAmount;
                                cNew.CardProviderOName = c.CardProviderOName;
                                cNew.CardTypeOName = c.CardTypeOName;
                                cNew.EDCMachineOName = c.EDCMachineOName;

                                cNew.RegistrationNo = c.RegistrationNo;
                                cNew.PatientName = c.PatientName;
                                cNew.PaymentMethodOName = c.PaymentMethodOName;
                                cNew.Amount = c.Amount;
                        }
                    }
                }
            }
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }
    }
}
