using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class VerificationDetail : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_VERIFICATION;

            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSRReceivableStatus, AppEnum.StandardReference.ReceivableStatus);
                StandardReference.InitializeIncludeSpace(cboSRReceivableType, AppEnum.StandardReference.ReceivableType);

                PopulateEntryControl();
            }

            txtInvoiceDate.Enabled = false;
            cboSRReceivableType.Enabled = false;
            cboGuarantorID.Enabled = false;
            pnlInformation.Visible = false;
        }

        private void PopulateEntryControl()
        {
            string invNo = Page.Request.QueryString["invNo"];
            if (string.IsNullOrEmpty(invNo))
                return;

            Invoices invoices = new Invoices();
            invoices.LoadByPrimaryKey(invNo);

            txtInvoiceNo.Text = invoices.InvoiceNo;
            cboSRReceivableType.SelectedValue = invoices.SRReceivableType;

            GuarantorQuery query = new GuarantorQuery();
            query.Where(query.GuarantorID == invoices.GuarantorID);
            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
            cboGuarantorID.SelectedValue = invoices.GuarantorID;

            txtInvoiceDate.SelectedDate = invoices.InvoiceDate;
            txtInvoiceDueDate.SelectedDate = invoices.InvoiceDueDate;
            txtTermOfPayment.Value = Convert.ToDouble(invoices.InvoiceTOP);
            txtNotes.Text = invoices.InvoiceNotes;
            cboSRReceivableStatus.SelectedValue = invoices.SRReceivableStatus;
            txtVerifyDate.SelectedDate = invoices.VerifyDate;
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = InvoicesItems;
        }

        private InvoicesItemCollection InvoicesItems
        {
            get
            {
                if (IsPostBack)
                {
                    object obj = Session["InvoicesItems" + Request.UserHostName];
                    if (obj != null)
                        return ((InvoicesItemCollection)(obj));
                }



                InvoicesItemCollection coll = new InvoicesItemCollection();
                InvoicesItemQuery query = new InvoicesItemQuery("a");
                InvoicesQuery hQuery = new InvoicesQuery("b");

                query.InnerJoin(hQuery).On(query.InvoiceNo == hQuery.InvoiceNo);
                query.Where(query.InvoiceNo == txtInvoiceNo.Text);
                query.OrderBy
                    (
                    query.PaymentNo.Ascending
                    );

                esQueryItem amount = new esQueryItem(query, "VerifyAmountProcess", esSystemType.Decimal);

                if (cboSRReceivableStatus.SelectedValue == AppSession.Parameter.ReceivableStatusProcess)
                    amount = query.Amount;
                else
                    amount = query.VerifyAmount;

                query.Select
                    (
                    query.InvoiceNo,
                        query.PaymentNo,
                        query.PaymentDate,
                        query.RegistrationNo,
                        query.PatientID,
                        query.PatientName,
                        query.Amount,
                        query.VerifyAmount,
                        amount.As("refToInvoicesItem_VerifyAmountProcess")
                    );

                coll.Load(query);
                Session["InvoicesItems" + Request.UserHostName] = coll;
                return coll;
            }
            set { Session["InvoicesItems" + Request.UserHostName] = value; }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                InvoicesItems = null;

                grdItem.Rebind();
            }
            else if (eventArgument == "process")
            {
                Validate();
                if (!IsValid)
                    return;

                Process();
                Save();
                pnlInformation.Visible = true;
            }
        }

        private void Process()
        {
            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                string paymentNo = dataItem.GetDataKeyValue("PaymentNo").ToString();
                double amount = ((RadNumericTextBox)dataItem.FindControl("txtVerifyAmount")).Value ?? 0;

                foreach (InvoicesItem item in InvoicesItems)
                {
                    if (item.PaymentNo.Equals(paymentNo))
                    {
                        item.VerifyAmount = (decimal)amount;
                        break;
                    }
                }
            }
        }

        private void Save()
        {
            Invoices inv = new Invoices();
            inv.LoadByPrimaryKey(txtInvoiceNo.Text);

            inv.InvoiceNotes = txtNotes.Text;
            inv.SRReceivableStatus = AppSession.Parameter.ReceivableStatusVerify;
            inv.VerifyDate = DateTime.Now;
            inv.VerifyByUserID = AppSession.UserLogin.UserID;
            inv.LastUpdateByUserID = AppSession.UserLogin.UserID;
            inv.LastUpdateDateTime = DateTime.Now;

            using (esTransactionScope trans = new esTransactionScope())
            {

                inv.Save();

                InvoicesItems.Save();

                trans.Complete();
            }
        }

        private void Lock()
        {

        }

        public string GetStatus(object isOrder, object IsOrderRealization, object IsApprove)
        {
            //if (IsApprove.Equals(false))
            //    return "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
            //else
            //{
            //    if (isOrder.Equals(false))
            //        return "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" />";
            //    else
            //    {
            //        if (IsOrderRealization.Equals(false))
            //            return "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
            //        else
            //            return "<img src=\"../../../../Images/Toolbar/post16.png\" border=\"0\" />";
            //    }
            //}
            return "<img src=\"../../../../Images/Toolbar/post16_d.png\" border=\"0\" />";
        }

        public bool GetStatusCheck(object isOrder, object IsOrderRealization, object IsApprove)
        {

            return false;
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            //foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            //{
            //    bool status = (sender as CheckBox).Checked;
            //    CheckBox chk = (dataItem.FindControl("detailChkbox") as CheckBox);
            //    if (chk.Enabled)
            //    {
            //        if (chk.Checked != status)
            //            chk.Checked = status;
            //    }
            //}
        }

        #region ComboBox GuarantorID

        protected void cboGuarantorID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            PopulateCboGuarantorID((RadComboBox)sender, e.Text);
        }

        private void PopulateCboGuarantorID(RadComboBox comboBox, string textSearch)
        {
            var query = new GuarantorQuery();

            query.Select(query.GuarantorID, query.GuarantorName);


            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            comboBox.DataSource = dtb;
            comboBox.DataBind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        #endregion
    }
}
