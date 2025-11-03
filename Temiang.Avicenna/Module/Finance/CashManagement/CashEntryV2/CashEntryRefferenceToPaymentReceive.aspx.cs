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

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryRefferenceToPaymentReceive : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;

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

                // populate cashier id
                List<string> PayMethod = new List<string>();
                //PayMethod.Add("PaymentMethod-002");
                //PayMethod.Add("PaymentMethod-003");
                //PayMethod.Add("PaymentMethod-004");
                //PayMethod.Add("PaymentMethod-001");

                var pmq = new PaymentMethodQuery("pmq");
                var ptq = new PaymentTypeQuery("ptq");
                pmq.InnerJoin(ptq).On(pmq.SRPaymentTypeID == ptq.SRPaymentTypeID)
                    .Where(ptq.IsCashierFrontOffice == true)
                    .Select(pmq.SRPaymentMethodID, pmq.PaymentMethodName);
                pmq.es.Distinct = true;
                var dtpm = pmq.LoadDataTable();
                PayMethod = dtpm.AsEnumerable().Select(x => x.Field<string>("SRPaymentMethodID")).ToList();
                var DtbCashierID = (new TransPaymentItem())
                        .GetPaymentDetailWithPagingCashierName(PayMethod.ToArray());

                var cboItem = new RadComboBoxItem("","");
                cboCashierName.Items.Add(cboItem);
                foreach (System.Data.DataRow row in DtbCashierID.Rows) {
                    var cboItem1 = new RadComboBoxItem(row["ApproveByUserID"].ToString() + " - " + row["UserName"].ToString(), row["ApproveByUserID"].ToString());
                    cboCashierName.Items.Add(cboItem1);
                }

                lbxPaymentMethod.DataSource = dtpm;
                lbxPaymentMethod.DataBind();
            }
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                List<string> PayMethod = new List<string>();
                //if (chkCard.Checked)
                //{
                //    PayMethod.Add("PaymentMethod-002");
                //    PayMethod.Add("PaymentMethod-003");
                //}
                //if (chkTransfer.Checked) {
                //    PayMethod.Add("PaymentMethod-004");
                //}
                //if (chkCash.Checked) {
                //    PayMethod.Add("PaymentMethod-001");
                //}
                foreach (ListItem listItem in lbxPaymentMethod.Items)
                {
                    if (listItem.Selected)
                    {
                        PayMethod.Add(listItem.Value);
                    }
                }

                if (PayMethod.Count == 0)
                {
                    grdListItem.VirtualItemCount = 0;
                    grdListItem.DataSource = null;
                }
                else
                {
                    var tp = (new TransPaymentItem())
                        .GetPaymentDetailWithPaging(this.grdListItem.CurrentPageIndex, this.grdListItem.PageSize,
                        txtPaymentNo.Text, txtRegistrationNo.Text, txtPatientName.Text,
                        txtPaymentDateTimeFrom.SelectedDate, txtPaymentDateTimeTo.SelectedDate,
                        txtAmount.Value, cboEDCMachineID.Text, PayMethod.ToArray(), 
                        cboRegistrationType.SelectedValue, cboCashierName.SelectedValue);

                    var iCount = (new TransPaymentItem())
                        .GetPaymentDetailWithPagingCount(
                        txtPaymentNo.Text, txtRegistrationNo.Text, txtPatientName.Text,
                        txtPaymentDateTimeFrom.SelectedDate, txtPaymentDateTimeTo.SelectedDate,
                        txtAmount.Value, cboEDCMachineID.Text, PayMethod.ToArray(),
                        cboRegistrationType.SelectedValue, cboCashierName.SelectedValue);

                    grdListItem.VirtualItemCount = iCount;
                    grdListItem.DataSource = tp;
                }
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

        protected void ChkHD_CheckedChanged(object sender, EventArgs e)
        {
            var chkHD = (CheckBox)sender;
            foreach (var gi in grdListItem.Items)
            {
                if (gi is GridDataItem)
                {
                    var item = ((GridDataItem)gi);
                    var chk = item.FindControl("Chk") as CheckBox;
                    if (chk != null)
                    {
                        if (chk.Checked != chkHD.Checked) {
                            chk.Checked = chkHD.Checked;
                            Chk_CheckedChanged(chk, new EventArgs());
                        }
                    }
                }
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
            grdListItem.CurrentPageIndex = 0;
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
            else
                return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string str = "";
            if (!HasSelected())
                str = "'rebind'";
            else
            {
                string selected = string.Empty;
                foreach (var sel in SelectedPay) {
                    selected += (selected.Length == 0 ? "" : "#") + sel.PaymentNo + "^" + sel.SequenceNo;
                }

                //str = "'" + grdListItem.SelectedValue.ToString() + "|PaymentReceive'";
                str = string.Format("'{0}|GakDipake|PaymentReceive'",selected);
            }
            return "oWnd.argument.init = " + str;
        }
    }
}
