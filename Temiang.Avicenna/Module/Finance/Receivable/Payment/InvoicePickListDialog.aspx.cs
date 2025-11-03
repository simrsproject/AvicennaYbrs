using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Globalization;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicePickListDialog : BasePageDialog
    {
        private double _sum = 0;

        private void copy(InvoicesItemCollection source, InvoicesItemCollection dest)
        {

            foreach (var d in dest)
            {
                var s = source.Where(x => x.InvoiceReferenceNo == d.InvoiceReferenceNo && x.PaymentNo == d.PaymentNo);
                if (!s.Any())
                {
                    d.MarkAsDeleted();
                }
            }

            foreach (var s in source)
            {
                var entity = (dest.Where(i => i.InvoiceReferenceNo == s.InvoiceReferenceNo && i.PaymentNo == s.PaymentNo)).SingleOrDefault();

                if (entity == null)
                {
                    entity = dest.AddNew();
                    entity.InvoiceNo = string.Empty;
                    //dest.AttachEntity(entity);
                }

                if (!string.IsNullOrEmpty(Request.QueryString["blud"]))
                {
                    entity.InvoiceNo = Request.QueryString["id"];
                }

                entity.PaymentNo = s.PaymentNo;
                entity.InvoiceReferenceNo = s.InvoiceReferenceNo;
                entity.PaymentDate = s.PaymentDate;
                entity.RegistrationNo = s.RegistrationNo;
                entity.PatientID = s.PatientID;
                entity.MedicalNo = s.MedicalNo;
                entity.PatientName = s.PatientName;

                entity.Amount = s.Amount;
                entity.VerifyAmount = s.VerifyAmount;
                entity.IsDiscountInPercent = s.IsDiscountInPercent;
                entity.DiscountPercentage = s.DiscountPercentage;
                entity.OtherAmount = s.OtherAmount;
                entity.SRDiscountReason = s.SRDiscountReason;
                entity.DiscountReason = s.DiscountReason;
                entity.BankCost = s.BankCost;
                entity.PaymentAmount = s.PaymentAmount;
                entity.BalanceAmount = s.BalanceAmount;
                entity.ClaimDifferenceAmount = s.ClaimDifferenceAmount;
            }
        }

        private InvoicesItemCollection InvoicesItems
        {
            get
            {
                object obj = Session["InvoicesItemsPayment"];
                if (obj == null)
                {
                    Session["InvoicesItemsPayment"] = new InvoicesItemCollection();
                }
                return (InvoicesItemCollection)obj;
            }
            set { Session["InvoicesItemsPayment"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_PAYMENT;

            if (!IsPostBack)
            {
                var grr = new Guarantor();
                grr.LoadByPrimaryKey(Request.QueryString["grr"]);
                Title = "Invoice List : " + grr.GuarantorName;

                var coll = (InvoicesItemCollection)Session["InvoicesItemsPayment" + Request.UserHostName];
                InvoicesItems = new InvoicesItemCollection();
                var iq = new InvoicesItemQuery("a");
                iq.Where(iq.InvoiceReferenceNo == string.Empty && iq.PaymentNo == string.Empty)
                    .Select(iq,
                        "< cast(0 as decimal(18,2)) refToInvoicesItem_BalanceAmount>",
                        "< '' refPatientID_MedicalNo>",
                        "< '' refToAppStandardReferenceItem_DiscountReason>");
                InvoicesItems.Load(iq);

                copy(coll, InvoicesItems);

                //foreach (var c in coll)
                //{
                //    var newEnt = (InvoicesItem)Helper.CloneObject(c);
                //    InvoicesItems.AttachEntity(newEnt);
                //}

                grdInvoicesDetail.Columns.FindByUniqueNameSafe("remaining").Visible = AppSession.Parameter.IsARPaymentShowRemaining;
                grdInvoicesDetail.Columns.FindByUniqueNameSafe("ClaimDifferenceAmount").Visible = false;
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new InvoicesQuery("a");
            var detail = new InvoicesItemQuery("b");
            var guar = new GuarantorQuery("c");

            detail.es.Distinct = true;
            detail.Select(detail.InvoiceNo);
            detail.InnerJoin(query).On(detail.InvoiceNo == query.InvoiceNo);
            detail.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            detail.Where(
                    guar.GuarantorHeaderID == Request.QueryString["grr"],
                    query.SRReceivableStatus == AppSession.Parameter.ReceivableStatusVerify,
                    query.IsPaymentApproved.IsNull(),
                    query.IsInvoicePayment == false
                );
            //detail.Where("< b.VerifyAmount  <> (ISNULL(b.PaymentAmount, 0) + ISNULL(b.OtherAmount,0) + ISNULL(b.BankCost,0) )>");
            detail.Where("<((b.VerifyAmount > 0 and b.VerifyAmount > (ISNULL(b.PaymentAmount, 0) + ISNULL(b.OtherAmount,0) + ISNULL(b.BankCost,0))) " +
                "or (b.VerifyAmount < 0 and b.VerifyAmount < (ISNULL(b.PaymentAmount, 0) + ISNULL(b.OtherAmount,0) + ISNULL(b.BankCost,0))))>");
            if ((string.IsNullOrEmpty(Request.QueryString["blud"])))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["invno"]))
                    detail.Where(detail.InvoiceNo == Request.QueryString["invno"]);
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    var iic = new InvoicesItemCollection();
                    iic.Query.Where(iic.Query.InvoiceNo == Request.QueryString["id"]);
                    if (iic.Query.Load()) detail.Where(detail.InvoiceNo.In(iic.Select(i => i.InvoiceReferenceNo)));
                }
            }

            //DataTable dtb = detail.LoadDataTable();

            var coll = new InvoicesItemCollection();
            coll.Load(detail);

            grdList.DataSource = coll;
        }

        protected void grdList_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                var coll = (InvoicesItemCollection)Session["InvoicesItemsPayment" + Request.UserHostName];

                var item = ((GridDataItem)e.Item);

                var ii = (InvoicesItem)item.DataItem;

                CheckBox chk = item.FindControl("itemChkbox") as CheckBox;

                if (coll.Where(x => x.InvoiceReferenceNo == ii.InvoiceNo).Any())
                {
                    chk.Checked = true;
                }
            }
        }

        protected void grdList_DataBound(object sender, EventArgs e)
        {
            PopulateInvoiceDetailGrid();
        }

        protected void grdInvoicesDetail_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
                e.Item.PreRender += grdInvoicesDetail_ItemPreRender;
        }

        private void grdInvoicesDetail_ItemPreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null)
                return;

            var drId = dataItem["SRDiscountReason"].Text;
            if (!string.IsNullOrEmpty(drId) && drId != "&nbsp;")
            {
                var dr = (dataItem["PaymentNo"].FindControl("cboSRDiscountReason") as RadComboBox);


                DataView dv = PopulateDiscountReason().DefaultView;
                dv.RowFilter = "ItemID = '" + drId + "'";

                dr.DataSource = dv.ToTable();
                dr.DataValueField = "ItemID";
                dr.DataTextField = "ItemName";
                dr.DataBind();
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (!(source is RadGrid) || string.IsNullOrEmpty(eventArgument))
                return;

            switch (((RadGrid)source).ID)
            {
                case "grdList": {
                        var ea = eventArgument.Split('|');
                        if (ea.Length == 2)
                        {
                            if (ea[0] == "loadInquiry")
                            {
                                if (ea[1] != "undefined") {
                                    var bid = new BankInquiryDetail();
                                    if (bid.LoadByPrimaryKey(System.Convert.ToInt32(ea[1])))
                                    {
                                        hfRelatedBankInquiryID.Value = bid.TransactionID.ToString();
                                        lblRelatedBankInquiryAmount.Text = ((bid.Credit ?? 0) - (bid.Debit ?? 0)).ToString("N2");
                                        lblRelatedBankInquiryDesc.Text = bid.Description;

                                        btnCancelRelatedBankInquiry.Visible = true;

                                        CalculateInquiryDiff();
                                    }
                                }
                            }
                        }
                        break;
                    }
                case "grdInvoicesDetail":
                    PopulateInvoiceDetailGrid();
                    break;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            if (string.IsNullOrEmpty(Request.QueryString["blud"]))
            {
                var selected = false;

                foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
                {
                    selected = ((CheckBox)dataItem.FindControl("detailChkbox")).Checked;
                    if (selected)
                        break;
                }

                if (selected)
                {
                    //return "oWnd.argument.id = '" + grdList.SelectedValue + "'";
                    foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                    {
                        selected = ((CheckBox)dataItem.FindControl("itemChkbox")).Checked;
                        if (selected)
                            return string.Format("oWnd.argument.id = '{0}'", dataItem["InvoiceNo"].Text);
                    }
                }
                return string.Empty;
            }
            else
            {
                return "oWnd.argument.rebind = '1|" + Request.QueryString["id"] + "'";
            }
        }

        public override bool OnButtonOkClicked()
        {
            // validasi
            if (!string.IsNullOrEmpty(lblRelatedBankInquiryDiff.Text)) {
                if (System.Convert.ToDecimal(lblRelatedBankInquiryDiff.Text) != 0) {
                    ShowInformationHeader(string.Format("Transaction does not equal between inquiry ({0}) and payment amount ({1})",
                        lblRelatedBankInquiryAmount.Text,
                        lblSumPaymentAmount.Text));
                    return false;
                }
            }
            decimal pay = 0, inquiry = 0; ;
            if (!string.IsNullOrEmpty(lblSumPaymentAmount.Text))
            {
                pay = System.Convert.ToDecimal(lblSumPaymentAmount.Text);
            }
            if (!string.IsNullOrEmpty(lblRelatedBankInquiryAmount.Text))
            {
                inquiry = System.Convert.ToDecimal(lblRelatedBankInquiryAmount.Text);
                lblRelatedBankInquiryDiff.Text = (inquiry - pay).ToString("N2");
            }
            else
            {
                lblRelatedBankInquiryDiff.Text = string.Empty;
            }

            var coll = (InvoicesItemCollection)Session["InvoicesItemsPayment" + Request.UserHostName];
            //coll.MarkAllAsDeleted();

            copy(InvoicesItems, coll);

            Session["BankInquiryTransactionID"] = hfRelatedBankInquiryID.Value;

            //foreach (var c in InvoicesItems)
            //{
            //    var newEnt = (InvoicesItem)Helper.CloneObject(c);
            //    coll.AttachEntity(newEnt);
            //}

            //foreach (GridDataItem dataItem in grdInvoicesDetail.MasterTableView.Items)
            //{
            //    if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
            //    {
            //        //var entity = (coll.Where(i => i.InvoiceNo == dataItem["InvoiceNo"].Text &&
            //        //                              i.PaymentNo == dataItem["PaymentNo"].Text &&
            //        //                              i.InvoiceNo == (dataItem["InvoiceReferenceNo"].Text == "&nbsp;" ? string.Empty : dataItem["InvoiceReferenceNo"].Text))).SingleOrDefault();

            //        var entity = (coll.Where(i => i.InvoiceReferenceNo == dataItem["InvoiceNo"].Text &&
            //                                      i.PaymentNo == dataItem["PaymentNo"].Text)).SingleOrDefault();

            //        var balanceAmt = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
            //        bool isDiscInPercent = ((CheckBox)dataItem.FindControl("chkIsDiscInPercent")).Checked;
            //        string srDiscReason = ((RadComboBox)dataItem.FindControl("cboSRDiscountReason")).SelectedValue;
            //        string srDiscReasonName = ((RadComboBox)dataItem.FindControl("cboSRDiscountReason")).Text;

            //        if (entity == null)
            //        {
            //            entity = coll.AddNew();
            //            entity.InvoiceNo = string.Empty;
            //            entity.PaymentNo = dataItem["PaymentNo"].Text;
            //            entity.InvoiceReferenceNo = dataItem["InvoiceNo"].Text;
            //            if (dataItem["PaymentDate"].Text == "&nbsp;")
            //                entity.str.PaymentDate = string.Empty;
            //            else
            //                entity.PaymentDate = DateTime.Parse(dataItem["PaymentDate"].Text);
            //            entity.RegistrationNo = dataItem["RegistrationNo"].Text;
            //            entity.PatientID = dataItem["PatientID"].Text;
            //            entity.MedicalNo = dataItem["MedicalNo"].Text;
            //            entity.PatientName = dataItem["PatientName"].Text;

            //            entity.Amount = balanceAmt;
            //            entity.VerifyAmount = balanceAmt;
            //            entity.IsDiscountInPercent = isDiscInPercent;
            //            if (isDiscInPercent)
            //            {
            //                entity.DiscountPercentage = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //                entity.OtherAmount = entity.VerifyAmount*entity.DiscountPercentage/100;
            //            }
            //            else
            //            {
            //                entity.DiscountPercentage = 0;
            //                entity.OtherAmount = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //            }
            //            entity.SRDiscountReason = srDiscReason;
            //            entity.DiscountReason = srDiscReasonName;
            //            entity.BankCost = Convert.ToDecimal((dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value);

            //            entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
            //            //if (entity.OtherAmount > 0)
            //            //{
            //            //    if (entity.PaymentAmount + entity.OtherAmount + entity.BankCost > balanceAmt)
            //            //        entity.PaymentAmount = balanceAmt - entity.OtherAmount - entity.BankCost;
            //            //}
            //            entity.BalanceAmount = entity.PaymentAmount;
            //        }
            //        else
            //        {
            //            entity.IsDiscountInPercent = isDiscInPercent;
            //            if (isDiscInPercent)
            //            {
            //                entity.DiscountPercentage = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //                entity.OtherAmount = entity.VerifyAmount * entity.DiscountPercentage / 100;
            //            }
            //            else
            //            {
            //                entity.DiscountPercentage = 0;
            //                entity.OtherAmount = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //            }
            //            entity.SRDiscountReason = srDiscReason;
            //            entity.DiscountReason = srDiscReasonName;
            //            entity.BankCost = Convert.ToDecimal((dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value);
            //            entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
            //            //if (entity.OtherAmount > 0)
            //            //{
            //            //    if (entity.PaymentAmount + entity.OtherAmount + entity.BankCost > balanceAmt)
            //            //        entity.PaymentAmount = balanceAmt - entity.OtherAmount - entity.BankCost;
            //            //}
            //            entity.BalanceAmount = entity.PaymentAmount;
            //        }
            //    }
            //}

            return true;
        }

        protected void cboSRDiscountReason_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {

            var cbo = (RadComboBox)o;
            var gdi = (GridDataItem)cbo.NamingContainer;
            var chk = gdi.FindControl("detailChkbox") as CheckBox;
            if (chk != null)
            {
                ToggleSelectedState(chk, e);
            }
        }

        protected void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            var txt = (RadNumericTextBox)sender;
            var gdi = (GridDataItem)txt.NamingContainer;
            var chk = gdi.FindControl("detailChkbox") as CheckBox;

            if (AppSession.Parameter.IsArPaymentExcessToDiscount)
            {
                if (!(gdi.FindControl("chkIsDiscInPercent") as CheckBox).Checked)
                {
                    var amount = System.Convert.ToDouble(gdi.Cells[11].Text);
                    var txtPaymentAmount = gdi.FindControl("txtPaymentAmount") as RadNumericTextBox;
                    var txtOtherAmount = gdi.FindControl("txtOtherAmount") as RadNumericTextBox;

                    txtOtherAmount.Value = amount - txtPaymentAmount.Value;
                }
            }

            if (chk != null)
            {
                ToggleSelectedState(chk, e);
            }
        }

        protected void txtOtherAmount_TextChanged(object sender, EventArgs e)
        {
            var txt = (RadNumericTextBox)sender;
            var gdi = (GridDataItem)txt.NamingContainer;
            var chk = gdi.FindControl("detailChkbox") as CheckBox;

            if (chk != null)
            {
                ToggleSelectedState(chk, e);
            }
        }

        protected void ToggleDiscSelectedState(object sender, EventArgs e)
        {
            var txt = (CheckBox)sender;
            var gdi = (GridDataItem)txt.NamingContainer;
            var chk = gdi.FindControl("detailChkbox") as CheckBox;
            if (chk != null)
            {
                ToggleSelectedState(chk, e);
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            var chk = ((CheckBox)sender);

            if (chk.ID == "detailChkbox")
            {
                //var x = chk.Parent;

                Add(chk, (GridDataItem)chk.NamingContainer);
            }
            else if (chk.ID == "headerChkbox")
            {
                foreach (var dataItem in grdInvoicesDetail.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Enabled))
                {
                    var chkDetail = (CheckBox)dataItem.FindControl("detailChkbox");
                    chkDetail.Checked = chk.Checked;
                    ToggleSelectedState(chkDetail, new EventArgs());
                }
            }

            CalculateSum();
        }

        private void Add(CheckBox chk, GridDataItem dataItem)
        {
            Add(chk.Checked, dataItem["InvoiceNo"].Text, dataItem["PaymentNo"].Text,
                Convert.ToDecimal(dataItem["BalanceAmount"].Text),
                Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value),
                Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value),
                ((CheckBox)dataItem.FindControl("chkIsDiscInPercent")).Checked,
                Convert.ToDecimal((dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value),
                ((RadComboBox)dataItem.FindControl("cboSRDiscountReason")).SelectedValue,
                ((RadComboBox)dataItem.FindControl("cboSRDiscountReason")).Text,
                dataItem["PaymentDate"].Text,
                dataItem["RegistrationNo"].Text.Replace("&nbsp;", ""),
                dataItem["PatientID"].Text,
                 dataItem["PatientName"].Text.Replace("&nbsp;",""),
                dataItem["MedicalNo"].Text,
                (dataItem.FindControl("txtRemaining") as RadNumericTextBox),
                Convert.ToDecimal(dataItem["ClaimDifferenceAmount"].Text)
            );

            //var entity = (InvoicesItems.Where(i => i.InvoiceReferenceNo == dataItem["InvoiceNo"].Text &&
            //                                      i.PaymentNo == dataItem["PaymentNo"].Text)).SingleOrDefault();

            //if (!chk.Checked)
            //{
            //    // remove
            //    if (entity != null) InvoicesItems.DetachEntity(entity);
            //    return;
            //}
            //else
            //{

            //    var balanceAmt = Convert.ToDecimal(dataItem["BalanceAmount"].Text);
            //    bool isDiscInPercent = ((CheckBox)dataItem.FindControl("chkIsDiscInPercent")).Checked;
            //    string srDiscReason = ((RadComboBox)dataItem.FindControl("cboSRDiscountReason")).SelectedValue;
            //    string srDiscReasonName = ((RadComboBox)dataItem.FindControl("cboSRDiscountReason")).Text;

            //    if (entity == null)
            //    {
            //        entity = InvoicesItems.AddNew();
            //        entity.InvoiceNo = string.Empty;
            //        entity.PaymentNo = dataItem["PaymentNo"].Text;
            //        entity.InvoiceReferenceNo = dataItem["InvoiceNo"].Text;
            //        if (dataItem["PaymentDate"].Text == "&nbsp;")
            //            entity.str.PaymentDate = string.Empty;
            //        else
            //            entity.PaymentDate = DateTime.Parse(dataItem["PaymentDate"].Text);
            //        entity.RegistrationNo = dataItem["RegistrationNo"].Text;
            //        entity.PatientID = dataItem["PatientID"].Text;
            //        entity.MedicalNo = dataItem["MedicalNo"].Text;
            //        entity.PatientName = dataItem["PatientName"].Text;

            //        entity.Amount = balanceAmt;
            //        entity.VerifyAmount = balanceAmt;
            //        entity.IsDiscountInPercent = isDiscInPercent;
            //        if (isDiscInPercent)
            //        {
            //            entity.DiscountPercentage = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //            entity.OtherAmount = entity.VerifyAmount * entity.DiscountPercentage / 100;
            //        }
            //        else
            //        {
            //            entity.DiscountPercentage = 0;
            //            entity.OtherAmount = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //        }
            //        entity.SRDiscountReason = srDiscReason;
            //        entity.DiscountReason = srDiscReasonName;
            //        entity.BankCost = Convert.ToDecimal((dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value);

            //        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
            //        entity.BalanceAmount = entity.PaymentAmount;
            //    }
            //    else
            //    {
            //        entity.IsDiscountInPercent = isDiscInPercent;
            //        if (isDiscInPercent)
            //        {
            //            entity.DiscountPercentage = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //            entity.OtherAmount = entity.VerifyAmount * entity.DiscountPercentage / 100;
            //        }
            //        else
            //        {
            //            entity.DiscountPercentage = 0;
            //            entity.OtherAmount = Convert.ToDecimal((dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value);
            //        }
            //        entity.SRDiscountReason = srDiscReason;
            //        entity.DiscountReason = srDiscReasonName;
            //        entity.BankCost = Convert.ToDecimal((dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value);
            //        entity.PaymentAmount = Convert.ToDecimal((dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value);
            //        entity.BalanceAmount = entity.PaymentAmount;
            //    }
            //}
        }

        private void Add(bool Checked, string InvoiceNo, string PaymentNo,
            decimal BalanceAmount, decimal PaymentAmount, decimal OtherAmount, bool isDiscInPercent, decimal BankCost,
            string srDiscReason, string srDiscReasonName, string PaymentDate,
            string RegistrationNo, string PatientID, string PatientName, string MedicalNo, RadNumericTextBox txtRemaining, decimal claimDifferenceAmount)
        {
            var entity = (InvoicesItems.Where(i => i.InvoiceReferenceNo == InvoiceNo &&
                                                  i.PaymentNo == PaymentNo)).SingleOrDefault();
            if (!Checked)
            {
                // remove
                if (entity != null) entity.MarkAsDeleted(); //InvoicesItems.DetachEntity(entity);
                return;
            }
            else
            {
                if (entity == null)
                {
                    entity = InvoicesItems.AddNew();
                    entity.InvoiceNo = string.Empty;
                    entity.PaymentNo = PaymentNo;
                    entity.InvoiceReferenceNo = InvoiceNo;
                    if (PaymentDate == "&nbsp;")
                        entity.str.PaymentDate = string.Empty;
                    else
                        entity.PaymentDate = DateTime.ParseExact(PaymentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture); //.Parse(PaymentDate, AppConstant.DisplayFormat.DateCultureInfo);
                    entity.RegistrationNo = RegistrationNo;
                    entity.PatientID = PatientID;
                    entity.MedicalNo = MedicalNo;
                    entity.PatientName = PatientName;

                    entity.Amount = BalanceAmount;
                    entity.VerifyAmount = BalanceAmount;
                    entity.IsDiscountInPercent = isDiscInPercent;
                    if (isDiscInPercent)
                    {
                        entity.DiscountPercentage = OtherAmount;
                        entity.OtherAmount = entity.VerifyAmount * entity.DiscountPercentage / 100;
                    }
                    else
                    {
                        entity.DiscountPercentage = 0;
                        entity.OtherAmount = OtherAmount;
                    }
                    entity.SRDiscountReason = srDiscReason;
                    entity.DiscountReason = srDiscReasonName;
                    entity.BankCost = BankCost;

                    entity.PaymentAmount = PaymentAmount;
                    entity.BalanceAmount = entity.PaymentAmount;
                    entity.ClaimDifferenceAmount = claimDifferenceAmount;
                }
                else
                {
                    entity.IsDiscountInPercent = isDiscInPercent;
                    if (isDiscInPercent)
                    {
                        entity.DiscountPercentage = OtherAmount;
                        entity.OtherAmount = entity.VerifyAmount * entity.DiscountPercentage / 100;
                    }
                    else
                    {
                        entity.DiscountPercentage = 0;
                        entity.OtherAmount = OtherAmount;
                    }
                    entity.SRDiscountReason = srDiscReason;
                    entity.DiscountReason = srDiscReasonName;
                    entity.BankCost = BankCost;
                    entity.PaymentAmount = PaymentAmount;
                    entity.BalanceAmount = entity.PaymentAmount;
                }

                txtRemaining.Value = (entity.VerifyAmount ?? 0).ToDouble() - (entity.PaymentAmount ?? 0).ToDouble() -
                        (entity.OtherAmount ?? 0).ToDouble() - (entity.BankCost ?? 0).ToDouble();
            }
        }

        private void CalculateSum()
        {
            lblSumPaymentAmount.Text = InvoicesItems.Sum(x => x.PaymentAmount).ToDecimal().ToString("n2");
            lblSumDiscount.Text = InvoicesItems.Sum(x => x.OtherAmount).ToDecimal().ToString("n2");
            lblSumBankCost.Text = InvoicesItems.Sum(x => x.BankCost).ToDecimal().ToString("n2");

            lblSelectedCount.Text = InvoicesItems.Count.ToString();

            CalculateInquiryDiff();
        }

        private void CalculateInquiryDiff() {
            decimal pay = 0, inquiry = 0; ;
            if (!string.IsNullOrEmpty(lblSumPaymentAmount.Text)) {
                pay = System.Convert.ToDecimal(lblSumPaymentAmount.Text);
            }
            if (!string.IsNullOrEmpty(lblRelatedBankInquiryAmount.Text))
            {
                inquiry = System.Convert.ToDecimal(lblRelatedBankInquiryAmount.Text);
                lblRelatedBankInquiryDiff.Text = (inquiry - pay).ToString("N2");
            }
            else {
                lblRelatedBankInquiryDiff.Text = string.Empty;
            }
        }

        protected void btnProcessGlobalDiscount_Click(object sender, ImageClickEventArgs e)
        {
            //var dtb = query.LoadDataTable();
            if (cboDiscountTypeID.SelectedValue == "0")
            {
                foreach (var item in InvoicesItems)
                {
                    item.IsDiscountInPercent = cboDiscountTypeID.SelectedValue == "0";
                    item.DiscountPercentage = System.Convert.ToDecimal(cboDiscountTypeID.SelectedValue == "0" ? txtDiscountValue.Value : 0);
                    item.OtherAmount = item.Amount * item.DiscountPercentage / 100;
                    item.BankCost = System.Convert.ToDecimal(txtBankCost.Value);
                    item.SRDiscountReason = cboDiscountReason.SelectedValue;
                    item.DiscountReason = cboDiscountReason.Text;
                    item.PaymentAmount = item.Amount - (item.OtherAmount ?? 0) - (item.BankCost ?? 0);
                }
            }
            else
            {
                if (!chkIsProRata.Checked)
                {
                    foreach (var item in InvoicesItems)
                    {
                        item.IsDiscountInPercent = cboDiscountTypeID.SelectedValue == "0";
                        item.DiscountPercentage = System.Convert.ToDecimal(cboDiscountTypeID.SelectedValue == "0" ? txtDiscountValue.Value : 0);
                        item.OtherAmount = System.Convert.ToDecimal(txtDiscountValue.Value);
                        item.BankCost = System.Convert.ToDecimal(txtBankCost.Value);
                        item.SRDiscountReason = cboDiscountReason.SelectedValue;
                        item.DiscountReason = cboDiscountReason.Text;
                        item.PaymentAmount = item.Amount - (item.OtherAmount ?? 0) - (item.BankCost ?? 0);
                    }
                }
                else
                {
                    decimal total = InvoicesItems.Sum(x => x.Amount ?? 0);
                    if (total > 0)
                    {
                        decimal totalDisc = System.Convert.ToDecimal(txtDiscountValue.Value ?? 0);
                        decimal totalCurrDisc = 0;
                        InvoicesItem lastItem = null;

                        decimal discp = Convert.ToDecimal(string.Format("{0:n2}", (Convert.ToDecimal(txtDiscountValue.Value) / total) * 100));
                        foreach (var item in InvoicesItems)
                        {
                            item.IsDiscountInPercent = cboDiscountTypeID.SelectedValue == "0";
                            item.DiscountPercentage = System.Convert.ToDecimal(cboDiscountTypeID.SelectedValue == "0" ? txtDiscountValue.Value : 0);

                            item.OtherAmount = Math.Round((item.Amount ?? 0) * discp / 100, 0);
                            item.SRDiscountReason = cboDiscountReason.SelectedValue;
                            item.DiscountReason = cboDiscountReason.Text;
                            item.BankCost = System.Convert.ToDecimal(txtBankCost.Value);
                            item.PaymentAmount = item.Amount - (item.OtherAmount ?? 0) - (item.BankCost ?? 0);

                            totalCurrDisc += (item.OtherAmount ?? 0);
                            lastItem = item;
                        }

                        if (lastItem != null && totalDisc != totalCurrDisc)
                        {
                            lastItem.OtherAmount = Convert.ToDecimal(lastItem.OtherAmount) + totalDisc - totalCurrDisc;
                            lastItem.PaymentAmount = lastItem.Amount - lastItem.OtherAmount;
                        }
                    }
                }
            }

            PopulateInvoiceDetailGrid();
            CalculateSum();
        }

        protected void grdInvoicesDetail_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                _sum += double.Parse((dataItem["PaymentAmount"].FindControl("txtPaymentAmount") as RadNumericTextBox).Value.ToString());

                var ivi = InvoicesItems.Where(x => x.PaymentNo == dataItem.GetDataKeyValue("PaymentNo").ToString()).FirstOrDefault();
                if (ivi != null)
                {
                    (dataItem.FindControl("detailChkbox") as CheckBox).Checked = true;
                    (dataItem.FindControl("txtPaymentAmount") as RadNumericTextBox).Value = (ivi.PaymentAmount ?? 0).ToDouble();

                    //(dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value = (ivi.OtherAmount ?? 0).ToDouble();
                    (dataItem.FindControl("txtOtherAmount") as RadNumericTextBox).Value = (ivi.IsDiscountInPercent ?? false) ? (ivi.DiscountPercentage ?? 0).ToDouble() : (ivi.OtherAmount ?? 0).ToDouble();

                    (dataItem.FindControl("chkIsDiscInPercent") as CheckBox).Checked = ivi.IsDiscountInPercent ?? false;

                    (dataItem.FindControl("txtBankCost") as RadNumericTextBox).Value = (ivi.BankCost ?? 0).ToDouble();

                    (dataItem.FindControl("txtRemaining") as RadNumericTextBox).Value = 
                        (ivi.BalanceAmount ?? 0).ToDouble() - (ivi.PaymentAmount ?? 0).ToDouble() - 
                        (ivi.OtherAmount ?? 0).ToDouble() - (ivi.BankCost ?? 0).ToDouble();
                }
            }
            else if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = (GridFooterItem)e.Item;
                //footer["ShipCity"].Controls.Add(new LiteralControl("<span style='color: Black; font-weight: bold;'>Total freight on this page is:</span> "));
                (footer["PaymentAmount"].FindControl("txtSumPaymentAmount") as RadNumericTextBox).Value = Double.Parse(_sum.ToString());
                _sum = 0;
            }
        }

        protected void grdInvoicesDetail_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            PopulateInvoiceDetailGrid();
        }

        protected void grdInvoicesDetail_PageSizeChanged(object source, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            PopulateInvoiceDetailGrid();
        }

        protected void grdInvoicesDetail_SortCommand(object source, GridSortCommandEventArgs e)
        {
            PopulateInvoiceDetailGrid();
        }

        protected void cboSRDiscountReason_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboSRDiscountReason_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString(),
                        query.IsActive == true, query.ItemName.Like("%" + e.Text + "%"));
            query.Select(query.ItemID, query.ItemName);
            var dts = query.LoadDataTable();

            var combo = o as RadComboBox;
            combo.DataSource = dts;
            combo.DataBind();
        }

        private DataTable PopulateDiscountReason()
        {
            if (ViewState["DiscountReason"] != null)
                return ViewState["DiscountReason"] as DataTable;

            var query = new AppStandardReferenceItemQuery("b");

            query.Select(query.ItemID, query.ItemName);
            query.Where(query.StandardReferenceID == AppEnum.StandardReference.DiscountReason.ToString(),
                        query.IsActive == true);

            ViewState["DiscountReason"] = query.LoadDataTable();
            return ViewState["DiscountReason"] as DataTable;
        }

        protected void grdList_ItemChkBoxCheckedChanged(object sender, EventArgs e)
        {
            PopulateInvoiceDetailGrid();
        }

        protected void grdList_HeaderChkBoxCheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            foreach (var dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>())
            {
                ((CheckBox)dataItem.FindControl("itemChkbox")).Checked = chk.Checked;
            }
            PopulateInvoiceDetailGrid();
        }

        private void PopulateInvoiceDetailGrid()
        {
            string isChecked = AppSession.Parameter.DefaultChecklistArPayment;

            var invoices = new List<string>();
            foreach (var dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("itemChkbox")).Enabled))
            {
                if (((CheckBox)dataItem.FindControl("itemChkbox")).Checked == true)
                {
                    invoices.Add(dataItem["InvoiceNo"].Text);
                }
            }

            if (invoices.Count > 0)
            {
                var query = new InvoicesItemQuery("a");
                var patientQ = new PatientQuery("b");
                query.LeftJoin(patientQ).On(query.PatientID == patientQ.PatientID);

                query.Select(
                    query.InvoiceNo,
                    query.PaymentNo,
                    query.InvoiceReferenceNo,
                    query.PaymentDate,
                    query.RegistrationNo,
                    query.PatientID,
                    patientQ.MedicalNo,
                    query.PatientName,
                    query.VerifyAmount,
                    (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("BalanceAmount"),
                    (query.VerifyAmount - (query.PaymentAmount.Coalesce("'0'") + query.OtherAmount.Coalesce("'0'") + query.BankCost.Coalesce("'0'"))).As("PaymentAmount"),
                    //query.OtherAmount.Coalesce("'0'"),
                    //query.BankCost.Coalesce("'0'"),
                    @"<0 AS 'OtherAmount'>",
                    @"<0 AS 'BankCost'>",
                    @"<CAST('" + isChecked + "' AS BIT) AS 'IsChecked'>",
                    @"<CAST(0 AS BIT) AS 'IsDiscountInPercent'>",
                    @"<0 AS 'DiscountPercentage'>",
                    @"<'' AS 'SRDiscountReason'>",
                    query.ClaimDifferenceAmount.Coalesce("'0'")
                    );
                query.Where(query.InvoiceNo.In(invoices));
                //query.Where("<(a.VerifyAmount - (ISNULL(a.PaymentAmount, 0) + ISNULL(a.OtherAmount, 0) + ISNULL(a.BankCost,0))) <> 0>");
                query.Where("<((a.VerifyAmount > 0 and a.VerifyAmount > (ISNULL(a.PaymentAmount, 0) + ISNULL(a.OtherAmount,0) + ISNULL(a.BankCost,0))) " +
                    "or (a.VerifyAmount < 0 and a.VerifyAmount < (ISNULL(a.PaymentAmount, 0) + ISNULL(a.OtherAmount,0) + ISNULL(a.BankCost,0))))>");
                query.OrderBy(query.PaymentNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                if (isChecked == "0")
                {
                    foreach (DataRow row in dtb.Rows)
                    {
                        var entity =
                            (InvoicesItems.Where(
                                i =>
                                i.InvoiceReferenceNo == row["InvoiceNo"].ToString() &&
                                i.PaymentNo == row["PaymentNo"].ToString())).SingleOrDefault();
                        if (entity == null)
                            row["IsChecked"] = false;
                        else row["IsChecked"] = true;
                    }
                }

                foreach (DataRow row in dtb.Rows)
                {
                    row["PaymentAmount"] = row["BalanceAmount"];

                    foreach (var item in InvoicesItems)
                    {
                        var invoiceNo = item.InvoiceReferenceNo;
                        var paymentNo = item.PaymentNo;

                        if (row["InvoiceNo"].ToString() == invoiceNo && row["PaymentNo"].ToString() == paymentNo)
                        {
                            //row["PaymentAmount"] = System.Convert.ToDecimal(row["BalanceAmount"]) -
                            //    System.Convert.ToDecimal(row["OtherAmount"]) -
                            //    System.Convert.ToDecimal(row["BankCost"]);

                            row["IsDiscountInPercent"] = item.IsDiscountInPercent;
                            row["DiscountPercentage"] = item.DiscountPercentage;
                            row["OtherAmount"] = (item.IsDiscountInPercent ?? false) ? item.DiscountPercentage : item.OtherAmount;
                            row["SRDiscountReason"] = item.SRDiscountReason;
                            row["BankCost"] = item.BankCost;
                            row["PaymentAmount"] = item.PaymentAmount;
                            row["BalanceAmount"] = item.Amount;
                            break;
                        }
                    }
                }

                string[] NoInPay = dtb.AsEnumerable().Select(x => x.Field<string>("InvoiceNo") + x.Field<string>("PaymentNo")).ToArray();

                if (NoInPay.Length > 0)
                {
                    var xx = InvoicesItems.Where(x => !NoInPay.Contains(x.InvoiceReferenceNo + x.PaymentNo));
                    foreach (var x in xx)
                    {
                        x.MarkAsDeleted();
                    }
                }
                CalculateSum();

                dtb.AcceptChanges();
                grdInvoicesDetail.DataSource = null;
                grdInvoicesDetail.DataSource = dtb;
                grdInvoicesDetail.DataBind();
            }
            else
            {
                // return empty if non of diagnosa row selected
                grdInvoicesDetail.DataSource = null;
                grdInvoicesDetail.DataBind();
            }
        }

        protected void btnCancelRelatedBankInquiry_Click(object sender, ImageClickEventArgs e)
        {
            hfRelatedBankInquiryID.Value = string.Empty;
            lblRelatedBankInquiryAmount.Text = string.Empty;
            lblRelatedBankInquiryDesc.Text = string.Empty;

            btnCancelRelatedBankInquiry.Visible = false;

            CalculateInquiryDiff();
        }
    }
}
