using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using DevExpress.Data.Mask;
using System.Linq;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class VerificationList2 : BasePage
    {
        private string FormType
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        private bool _isUsingRounding 
        { 
            get
            {
                return AppSession.Parameter.IsUsingRoundingPaymentAP;
            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = FormType == "odr" ? AppConstant.Program.AP_PAYMENTORDERS : AppConstant.Program.AP_VERIFICATION;

            if (!IsPostBack)
            {
                if (FormType != "ver")
                {
                    txtFromDate.SelectedDate = DateTime.Now;
                    txtToDate.SelectedDate = DateTime.Now;
                }

                if (FormType == "ver")
                {
                    txtPlanningPaymentDateFrom.SelectedDate = DateTime.Now;
                    txtPlanningPaymentDateTo.SelectedDate = DateTime.Now;
                }

                if (FormType == "odr")
                {
                    txtPaymentOrdersDateFrom.SelectedDate = DateTime.Now;
                    txtPaymentOrdersDateTo.SelectedDate = DateTime.Now;
                }

                var suppliers = new SupplierCollection();
                suppliers.Query.Where(suppliers.Query.IsActive == true);
                if (suppliers.Query.Load())
                {
                    cboSupplierID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                    foreach (var supplier in suppliers)
                    {
                        cboSupplierID.Items.Add(new RadComboBoxItem(supplier.SupplierName, supplier.SupplierID));
                    }
                }

                StandardReference.InitializeIncludeSpace(cboSRPurchaseCategorization, AppEnum.StandardReference.PurchaseCategorization);

                RadToolBar2.Items[0].Visible = FormType != "odr";
                RadToolBar2.Items[1].Visible = FormType == "";
                RadToolBar2.Items[2].Visible = FormType == "odr";

                RadTabStrip1.Tabs[1].Visible = FormType == "odr";

                pnlVerif.Visible = FormType == "ver";
                pnlVerif2.Visible = FormType != "odr";
                pnlPaymentOrders.Visible = FormType == "odr";

                grdList.Columns[6].Visible = FormType == string.Empty; //txt Planning payment date
                grdList.Columns[7].Visible = FormType == string.Empty; //cbo Invoice Payment
                grdList.Columns[8].Visible = FormType == string.Empty; //cbo Bank

                grdList.Columns[9].Visible = FormType == "ver"; //lbl payment order no
                grdList.Columns[10].Visible = FormType == "ver"; //lbl planning payment date
                grdList.Columns[11].Visible = FormType == "ver"; //lbl Invoice Payment
                grdList.Columns[12].Visible = FormType == "ver"; //lbl Bank

                if (_isUsingRounding)
                {
                    grdList.MasterTableView.DetailTables[0].Columns.FindByUniqueName("RoundingAmount").Visible = FormType == "odr";
                    grdList.MasterTableView.DetailTables[0].Columns.FindByUniqueName("SubTotal").Visible = FormType == "odr";
                    grdList2.Columns.FindByDataField("RoundingAmount").Visible = true;
                }
            }
        }

        protected void grdList_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "cancel")
            {
                GridItem item = e.Item as GridItem;
                if (item == null)
                    return;

                var invNo =
                    Convert.ToString(
                        item.OwnerTableView.DataKeyValues[item.ItemIndex][
                            InvoiceSupplierMetadata.ColumnNames.InvoiceNo]);

                var inv = new InvoiceSupplier();
                inv.LoadByPrimaryKey(invNo);

                if (inv.SRPayableStatus != "1") return;

                inv.SRPayableStatus = AppSession.Parameter.PayableStatusProcess; 
                if (FormType == string.Empty)
                {
                    inv.InvoicePaymentPlanDate = null;
                    inv.SRInvoicePayment = null;
                    inv.BankID = null;
                    inv.BankAccountNo = null;
                }
                inv.VerifyByUserID = null;
                inv.VerifyDate = null;

                inv.Save();

                //rebind data
                grdList.Rebind();
            }
        }

        protected void grdList2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "print")
            {
                var jobParameters = new PrintJobParameterCollection();

                var pPaymentOrderNo = jobParameters.AddNew();
                pPaymentOrderNo.Name = "p_PaymentOrderNo";
                pPaymentOrderNo.ValueString = e.CommandArgument.ToString();

                AppSession.PrintJobParameters = jobParameters;
                AppSession.PrintJobReportID = AppConstant.Report.AP_PaymentOrder;

                string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                                "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                                "oWnd.Show();" +
                                "oWnd.Maximize();";
                RadAjaxPanel1.ResponseScripts.Add(script);
            }
            else if (e.CommandName == "cancel")
            {
                GridItem item = e.Item as GridItem;
                if (item == null)
                    return;

                var invNo =
                    Convert.ToString(
                        item.OwnerTableView.DataKeyValues[item.ItemIndex][
                            InvoiceSupplierMetadata.ColumnNames.InvoiceNo]);

                var inv = new InvoiceSupplier();
                inv.LoadByPrimaryKey(invNo);

                if (inv.SRPayableStatus != "0") return;

                inv.InvoicePaymentPlanDate = null;
                inv.SRInvoicePayment = null;
                inv.BankID = null;
                inv.PaymentOrderNo = null;

                inv.Save();

                if (_isUsingRounding)
                {
                    var coll = new InvoiceSupplierItemCollection();
                    coll.Query.Where(coll.Query.InvoiceNo == invNo);
                    coll.LoadAll();
                    foreach (var detil in coll)
                    {
                        detil.RoundingAmount = 0;
                    }
                    coll.Save();
                }

                //rebind data
                grdList2.Rebind();
                grdList.Rebind();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = InvoiceSuppliers;
        }

        protected void grdList2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList2.DataSource = InvoiceSuppliers2;
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string invoiceNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
            //Load record
            var query = new InvoiceSupplierItemQuery("a");
            var que = new ItemTransactionQuery("b");

            query.Select(
                        query.InvoiceNo,
                        query.TransactionNo,
                        que.InvoiceNo.As("InvoiceSuppNo"),
                        query.PPnAmount.As("Ppn"),
                        query.PPh22Amount.As("Pph22"),
                        query.PPh23Amount.As("Pph23"),
                        query.PphAmount.As("Pph"),
                        query.StampAmount.As("Stamp"),
                        query.OtherDeduction,
                        query.DownPaymentAmount,
                        query.RoundingAmount,
                        @"<SUM(a.Amount + (CASE ISNULL(a.IsPpnExcluded, 0) WHEN 0 THEN a.PPnAmount ELSE 0 END) - ISNULL(a.PphAmount, 0) - a.PPh22Amount - a.PPh23Amount + a.StampAmount - a.OtherDeduction - ISNULL(a.DownPaymentAmount, 0)) AS SubTotal>",
                        @"<a.Amount AS 'VerifyAmount'>",
                        @"<'' AS 'SRInvoicePayment'>",
                        @"<'' AS 'BankID'>",
                        @"<'' AS 'BankAccountNo'>"
                   );
            query.LeftJoin(que).On(query.TransactionNo == que.TransactionNo);

            query.Where(query.InvoiceNo == invoiceNo, query.VerifyDate.IsNull());

            query.GroupBy(query.Amount, query.InvoiceNo, query.TransactionNo, que.InvoiceNo, query.PPnAmount, query.PPh22Amount, query.PPh23Amount, query.PphAmount, query.StampAmount, query.OtherDeduction, query.DownPaymentAmount, query.RoundingAmount);

            DataTable dtb = query.LoadDataTable();
            //Apply
            e.DetailTableView.DataSource = dtb;
        }

        private DataTable InvoiceSuppliers
        {
            get
            {
                var query = new InvoiceSupplierQuery("a");
                var supp = new SupplierQuery("b");
                var pm = new PaymentMethodQuery("d");
                var bank = new BankQuery("e");

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                
                query.Select(
                            query.InvoiceNo,
                            query.InvoiceDate,
                            query.InvoiceDueDate,
                            query.SupplierID,
                            supp.SupplierName,
                            query.InvoicePaymentPlanDate,
                            query.SRInvoicePayment,
                            query.BankID,
                            query.BankAccountNo,
                            query.VerifyDate,
                            query.SRPayableStatus,
                            @"<GETDATE() AS 'AgingDate'>",
                            @"<(SELECT SUM(isi.Amount + (CASE ISNULL(isi.IsPpnExcluded, 0) WHEN 0 THEN isi.PPnAmount ELSE 0 END) - ISNULL(isi.PphAmount, 0) - isi.PPh22Amount - isi.PPh23Amount + isi.StampAmount - isi.OtherDeduction - ISNULL(isi.DownPaymentAmount, 0) + ISNULL(isi.RoundingAmount, 0)) 
                                FROM InvoiceSupplierItem isi 
                                WHERE isi.InvoiceNo = a.InvoiceNo) AS Total>",
                            @"<(SELECT TOP 1 ISNULL(isi.RoundingAmount, 0)
                                FROM InvoiceSupplierItem isi 
                                WHERE isi.InvoiceNo = a.InvoiceNo) AS RoundingAmount>",
                            @"<ISNULL(a.PaymentOrderNo, '') AS 'PaymentOrderNo'>",
                            pm.PaymentMethodName.As("InvoicePaymentName"),
                            bank.BankName
                       );
                query.LeftJoin(supp).On(query.SupplierID == supp.SupplierID);
                query.LeftJoin(pm).On(query.SRInvoicePayment == pm.SRPaymentMethodID & pm.SRPaymentTypeID == AppSession.Parameter.PaymentTypeBackOfficePayment);
                query.LeftJoin(bank).On(query.BankID == bank.BankID);

                if (!txtFromDate.IsEmpty & !txtToDate.IsEmpty)
                    query.Where(query.InvoiceDueDate >= txtFromDate.SelectedDate, query.InvoiceDueDate < txtToDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                    query.Where(query.SupplierID == cboSupplierID.SelectedValue);
                if (!chkShowAll.Checked)
                    query.Where(query.SRPayableStatus == AppSession.Parameter.PayableStatusProcess);
                else
                    query.Where(query.SRPayableStatus.In(AppSession.Parameter.PayableStatusProcess, AppSession.Parameter.PayableStatusVerify));

                if (FormType == "odr")
                {
                    query.Where(query.Or(query.PaymentOrderNo.IsNull(), query.PaymentOrderNo == string.Empty, query.IsVoid == false));
                    query.OrderBy(query.InvoiceNo.Ascending);
                }
                else if (FormType == "ver")
                {
                    query.Where(query.PaymentOrderNo.IsNotNull(), query.PaymentOrderNo != string.Empty);
                    if (!txtPlanningPaymentDateFrom.IsEmpty & !txtPlanningPaymentDateTo.IsEmpty)
                        query.Where(query.InvoicePaymentPlanDate >= txtPlanningPaymentDateFrom.SelectedDate, query.InvoicePaymentPlanDate < txtPlanningPaymentDateTo.SelectedDate.Value.AddDays(1));
                    if (!string.IsNullOrEmpty(txtPaymentOrdersNo.Text))
                        query.Where(query.PaymentOrderNo == txtPaymentOrdersNo.Text);
                    query.OrderBy(query.PaymentOrderNo.Ascending, query.InvoiceNo.Ascending);
                }
                else
                    query.OrderBy(query.InvoiceNo.Ascending);

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var detail = new InvoiceSupplierItemQuery("dt");
                    if (!chkShowAll.Checked) 
                        detail.Where(detail.VerifyDate.IsNull());
                    detail.Where(detail.InvoiceNo == row["InvoiceNo"].ToString());
                    if (!string.IsNullOrEmpty(cboSRPurchaseCategorization.SelectedValue))
                    {
                        var por = new ItemTransactionQuery("por");
                        query.InnerJoin(detail).On(detail.InvoiceNo == query.InvoiceNo);
                        query.InnerJoin(por).On(por.TransactionNo == detail.TransactionNo);
                        query.Where(por.SRPurchaseCategorization == cboSRPurchaseCategorization.SelectedValue);
                    }
                    if (detail.LoadDataTable().Rows.Count == 0) 
                        row.Delete();
                }
                dtb.AcceptChanges();

                return query.LoadDataTable();
            }
        }

        private DataTable InvoiceSuppliers2
        {
            get
            {
                var query = new InvoiceSupplierQuery("a");
                var supp = new SupplierQuery("b");
                var pm = new PaymentMethodQuery("d");
                var bank = new BankQuery("e");

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                query.Select(
                            query.InvoiceNo,
                            supp.SupplierName,
                            query.InvoicePaymentPlanDate,
                            query.PaymentOrderNo,
                            @"<(SELECT SUM(isi.Amount + (CASE ISNULL(isi.IsPpnExcluded, 0) WHEN 0 THEN isi.PPnAmount ELSE 0 END) - ISNULL(isi.PphAmount, 0) - isi.PPh22Amount - isi.PPh23Amount + isi.StampAmount - isi.OtherDeduction - ISNULL(isi.DownPaymentAmount, 0) + ISNULL(isi.RoundingAmount, 0)) 
                                FROM InvoiceSupplierItem isi 
                                WHERE isi.InvoiceNo = a.InvoiceNo) AS Total>",
                            @"<(SELECT SUM(a.RoundingAmount) FROM (
                                SELECT ISNULL(isi.RoundingAmount, 0) AS RoundingAmount
                                FROM InvoiceSupplierItem isi
                                WHERE isi.InvoiceNo = a.InvoiceNo) AS a) AS RoundingAmount>",
                            query.SRPayableStatus,
                            pm.PaymentMethodName.As("InvoicePaymentName"),
                            bank.BankName
                       );
                query.LeftJoin(supp).On(query.SupplierID == supp.SupplierID);
                query.LeftJoin(pm).On(query.SRInvoicePayment == pm.SRPaymentMethodID & pm.SRPaymentTypeID == AppSession.Parameter.PaymentTypeBackOfficePayment);
                query.LeftJoin(bank).On(query.BankID == bank.BankID);

                query.Where(query.PaymentOrderNo.IsNotNull(), query.PaymentOrderNo != string.Empty, query.IsVoid == false);

                if (!string.IsNullOrEmpty(cboSupplierID.SelectedValue))
                    query.Where(query.SupplierID == cboSupplierID.SelectedValue);
                if (!txtPaymentOrdersDateFrom.IsEmpty & !txtPaymentOrdersDateTo.IsEmpty)
                    query.Where(query.InvoicePaymentPlanDate >= txtPaymentOrdersDateFrom.SelectedDate.Value.Date, query.InvoicePaymentPlanDate < txtPaymentOrdersDateTo.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtInvoiceNo.Text);
                    query.Where(query.Or(query.InvoiceNo == txtInvoiceNo.Text, query.InvoiceNo.Like(searchTextContain)));
                }

                query.OrderBy(query.PaymentOrderNo.Ascending, query.InvoiceNo.Ascending);

                return query.LoadDataTable();
            }
        }

        protected void btnSearchInvoice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.DataSource = InvoiceSuppliers;
            grdList.DataBind();
        }

        protected void btnFilterPaymentOrders_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList2.DataSource = InvoiceSuppliers2;
            grdList2.DataBind();
        }

        protected void btnPrintPaymentOrders_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var jobParameters = new PrintJobParameterCollection();

            var parDate1 = jobParameters.AddNew();
            parDate1.Name = "p_StartDate";
            parDate1.ValueDateTime = txtPaymentOrdersDateFrom.SelectedDate ?? (new DateTime()).NowAtSqlServer();

            var parDate2 = jobParameters.AddNew();
            parDate2.Name = "p_EndDate";
            parDate2.ValueDateTime = txtPaymentOrdersDateTo.SelectedDate ?? (new DateTime()).NowAtSqlServer();

            AppSession.PrintJobParameters = jobParameters;
            AppSession.PrintJobReportID = AppConstant.Report.AP_PaymentOrder2;

            string script = @"var oWnd = $find('" + winPrint.ClientID + "');" +
                            "oWnd.SetUrl('" + Page.ResolveUrl("~/Module/Reports/ReportViewer.aspx") + "');" +
                            "oWnd.Show();" +
                            "oWnd.Maximize();";
            RadAjaxPanel1.ResponseScripts.Add(script);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(source is RadGrid))
                return;

            if (eventArgument == "process")
            {
                string msg = ErrMsg(true);

                if (msg.Length == 0)
                {
                    Process();
                    grdList.Rebind();
                    pnlInfo.Visible = false;
                }
                else
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }
            }
            else if (eventArgument == "payment")
            {
                string msg = ErrMsg(false);
                if (msg.Length == 0)
                {
                    var index = 0;
                    var paymentOrderNo = string.Empty;
                    var isUpdated = false;
                    foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                    {
                        if ((dataItem.FindControl("processChkBox") as CheckBox).Checked)
                        {
                            if (index == 0)
                            {
                                var autoNumber = Helper.GetNewAutoNumber((new DateTime()).NowAtSqlServer().Date, AppEnum.AutoNumber.PaymentOrderNo);
                                paymentOrderNo = autoNumber.LastCompleteNumber;
                                autoNumber.Save();
                                index++;
                            }
                            string invNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
                            //DateTime? planDate = ((RadDatePicker)dataItem.FindControl("txtPlanningDate")).SelectedDate;
                            DateTime? planDate = txtPlanningPaymentDate.SelectedDate;

                            var inv = new InvoiceSupplier();
                            inv.LoadByPrimaryKey(invNo);
                            if (!string.IsNullOrEmpty(inv.PaymentOrderNo)) continue;
                            inv.InvoicePaymentPlanDate = planDate;
                            inv.PaymentOrderNo = paymentOrderNo;
                            inv.SRInvoicePayment = cboSRInvoicePayment.SelectedValue;
                            inv.BankID = cboBankID.SelectedValue;
                            inv.LastUpdateDateTime = (new DateTime()).NowAtSqlServer();
                            inv.LastUpdateByUserID = AppSession.UserLogin.UserID;
                            inv.Save();

                            //Request RSKS: Rounding AP (Fajri 2023/08/07)
                            if (_isUsingRounding) { 
                                var coll = new InvoiceSupplierItemCollection();
                                coll.Query.Where(coll.Query.InvoiceNo == invNo);
                                coll.LoadAll();

                                GridTableView tableView = (GridTableView)dataItem.ChildItem.NestedTableViews[0];
                                int rowIndex = 0;

                                foreach (var detil in coll)
                                {
                                    if (rowIndex < tableView.Items.Count)
                                    {
                                        GridDataItem childItem = tableView.Items[rowIndex];
                                        detil.RoundingAmount = Convert.ToDecimal((childItem.FindControl("txtRoundingAmount") as RadNumericTextBox).Value);
                                        coll.Save();
                                        rowIndex++; 
                                    }
                                }
                            }

                            isUpdated = true;
                        }
                    }

                    if (isUpdated)
                    {
                        grdList.Rebind();
                        grdList2.Rebind();
                    }
                    pnlInfo.Visible = false;
                }
                else
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }
            }
        }

        private string ErrMsg(bool isVerif)
        {
            string msg = string.Empty;

            if (FormType == "" & isVerif & AppSession.Parameter.IsAPVerifNeedValidate)
            {
                foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("processChkBox") as CheckBox).Checked)
                    {
                        string invoiceNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
                        string srInvoicePayment = ((RadComboBox)dataItem.FindControl("cboSRInvoicePayment")).SelectedValue;
                        string bankId = ((RadComboBox)dataItem.FindControl("cboBankID")).SelectedValue;

                        if (srInvoicePayment.Trim().Length == 0)
                        {
                            msg = "Invoice Payment is required on Invoice No: " + invoiceNo.Trim();
                        }
                        else
                        {
                            if (srInvoicePayment != AppSession.Parameter.InvoicePaymentCash && bankId.Trim().Length == 0)
                            {
                                msg = "Bank is required on Invoice No: " + invoiceNo.Trim();
                            }
                        }
                    }
                }
            }
            else if (!isVerif) //order payments
            {
                if (txtPlanningPaymentDate.IsEmpty)
                    msg = "Planning Payment Date is required.";
                else if (string.IsNullOrEmpty(cboSRInvoicePayment.SelectedValue))
                {
                    msg = "Invoice Payment is required.";
                }
                else if (cboSRInvoicePayment.SelectedValue != AppSession.Parameter.InvoicePaymentCash & string.IsNullOrEmpty(cboBankID.SelectedValue))
                {
                    msg = "Bank is required.";
                }
            }
            return msg;
        }

        private void Process()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if ((dataItem.FindControl("processChkBox") as CheckBox).Checked)
                {
                    string invNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
                    string invPayment = ((RadComboBox)dataItem.FindControl("cboSRInvoicePayment")).SelectedValue;
                    if (invPayment == "&nbsp;") invPayment = string.Empty;
                    string bankID = ((RadComboBox)dataItem.FindControl("cboBankID")).SelectedValue;
                    if (bankID == "&nbsp;") bankID = string.Empty;
                    string bankAccNo = ((RadComboBox)dataItem.FindControl("cboBankAccountNo")).SelectedValue;
                    if (bankAccNo == "&nbsp;") bankAccNo = string.Empty;
                    DateTime? planDate = ((RadDatePicker)dataItem.FindControl("txtPlanningDate")).SelectedDate;

                    Save(invNo, invPayment, bankID, bankAccNo, planDate);
                }
            }
        }

        private void Save(string invoiceNo, string srInvoicePayment, string bankID, string bankAccNo, DateTime? planDate)
        {
            var invHeader = new InvoiceSupplier();
            invHeader.LoadByPrimaryKey(invoiceNo);
            invHeader.SRPayableStatus = AppSession.Parameter.ReceivableStatusVerify;
            invHeader.SRInvoicePayment = srInvoicePayment;
            invHeader.BankID = bankID;
            invHeader.BankAccountNo = bankAccNo;
            if (invHeader.VerifyDate == null) invHeader.VerifyDate = DateTime.Now;
            invHeader.VerifyByUserID = AppSession.UserLogin.UserID;
            invHeader.LastUpdateByUserID = AppSession.UserLogin.UserID;
            invHeader.LastUpdateDateTime = DateTime.Now;
            invHeader.InvoicePaymentPlanDate = planDate;

            var coll = new InvoiceSupplierItemCollection();
            coll.Query.Where(coll.Query.InvoiceNo == invoiceNo);
            coll.LoadAll();
            foreach (var detil in coll)
            {
                detil.VerifyAmount = detil.Amount + ((detil.IsPpnExcluded ?? false) ? 0: detil.PPnAmount) - detil.PPh22Amount - detil.PPh23Amount +
                                     detil.StampAmount - detil.OtherDeduction - (detil.DownPaymentAmount ?? 0) - (detil.PphAmount ?? 0) + (detil.RoundingAmount ?? 0);
                detil.VerifyDate = invHeader.VerifyDate;
                detil.VerifyByUserID = AppSession.UserLogin.UserID;
            }

            using (esTransactionScope trans = new esTransactionScope())
            {
                //if (IsVerify(invoiceNo))
                {
                    invHeader.Save();
                    coll.Save();
                }

                trans.Complete();
            }
        }

        private static bool IsVerify(string invNo)
        {
            var coll = new InvoiceSupplierCollection();
            coll.Query.Where
                (
                    coll.Query.InvoiceNo == invNo,
                    coll.Query.VerifyDate.IsNotNull()
                );
            coll.LoadAll();

            return (coll.Count == 0);
        }

        protected void cboSRInvoicePayment_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PaymentMethodName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SRPaymentMethodID"].ToString();
        }

        protected void cboSRInvoicePayment_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new PaymentMethodQuery("a");
            query.Where(
                query.SRPaymentTypeID == AppSession.Parameter.PaymentTypeBackOfficePayment,
                query.Or(
                        query.PaymentMethodName.Like("%" + e.Text + "%"),
                        query.SRPaymentMethodID.Like("%" + e.Text + "%")
                        ));
            query.Select(query.SRPaymentMethodID, query.PaymentMethodName);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }

        protected void cboBankID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BankName"].ToString() + " " + ((DataRowView)e.Item.DataItem)["Norek"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BankID"].ToString();
        }

        protected void cboBankID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new BankQuery();
            query.Where(query.Or(query.BankID.Like("%" + e.Text + "%"), query.BankName.Like("%" + e.Text + "%")),
                        query.IsActive == true);
            query.Select(query.BankID, query.BankName, query.NoRek);
            query.OrderBy(query.BankName.Ascending);

            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }

        protected void cboBankAccountNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["BankAccountNo"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["BankName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["BankAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["BankAccountNo"].ToString();
        }

        protected void cboBankAccountNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new SupplierBankQuery();
            query.Where(query.Or(query.BankAccountNo.Like("%" + e.Text + "%"), query.BankName.Like("%" + e.Text + "%"), query.BankAccountName.Like("%" + e.Text + "%")),
                        query.IsActive == true,
                        query.SupplierID == ((GridDataItem)((RadComboBox)o).Parent.Parent)["SupplierID"].Text);
            query.Select(query.BankAccountNo, query.BankName, query.BankAccountName);
            query.OrderBy(query.BankAccountNo.Ascending);

            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();

            grdList.ItemCreated += new GridItemEventHandler(grdList_ItemCreated);
        }

        protected void grdList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) e.Item.PreRender += grdList_PreRender;
        }

        private void grdList_PreRender(object sender, EventArgs e)
        {
            var dataItem = sender as GridDataItem;
            if (dataItem == null) return;

            var payment = dataItem["SRInvoicePayment"].Text;
            var bank = dataItem["BankID"].Text;
            var account = dataItem["BankAccountNo"].Text;

            if (!string.IsNullOrEmpty(payment) && payment != "&nbsp;")
            {
                var cboSRInvoicePayment = ((RadComboBox)dataItem.FindControl("cboSRInvoicePayment"));

                var query = new PaymentMethodQuery("a");
                var PaymentType = new PaymentTypeQuery("b");

                query.InnerJoin(PaymentType).On(query.SRPaymentTypeID == PaymentType.SRPaymentTypeID);
                query.Where(
                    query.SRPaymentMethodID == payment,
                    PaymentType.IsApPayment == true
                    );
                query.Select(query.SRPaymentMethodID, query.PaymentMethodName);

                cboSRInvoicePayment.DataSource = query.LoadDataTable();
                cboSRInvoicePayment.DataBind();
                cboSRInvoicePayment.SelectedValue = payment;
            }

            if (!string.IsNullOrEmpty(bank) && bank != "&nbsp;")
            {
                var cboBankID = ((RadComboBox)dataItem.FindControl("cboBankID"));

                var query = new BankQuery();
                query.Where(query.BankID == bank);
                query.Select(query.BankID, query.BankName, query.NoRek);
                query.OrderBy(query.BankName.Ascending);

                cboBankID.DataSource = query.LoadDataTable();
                cboBankID.DataBind();
                cboBankID.SelectedValue = bank;
            }

            if (!string.IsNullOrEmpty(account) && account != "&nbsp;")
            {
                var cboBankAccountNo = ((RadComboBox)dataItem.FindControl("cboBankAccountNo"));
                var supplier = dataItem["SupplierID"].Text;

                var query = new SupplierBankQuery();
                query.Where(query.BankAccountNo == account,
                            query.IsActive == true,
                            query.SupplierID == supplier);
                query.Select(query.BankAccountNo, query.BankName, query.BankAccountName);
                query.OrderBy(query.BankAccountNo.Ascending);

                cboBankAccountNo.DataSource = query.LoadDataTable();
                cboBankAccountNo.DataBind();
                cboBankAccountNo.SelectedValue = account;
            }
        }
    }
}
