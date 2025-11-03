using System;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.Payable
{
    public partial class InvoicingPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = Request.QueryString["type"] == "1"
                            ? AppConstant.Program.AP_INVOICING
                            : AppConstant.Program.AP_INVOICING2;

            if (!IsPostBack)
            {
                ViewState["InvoiceNo"] = string.Empty;

                if (AppSession.Parameter.IsPphUsesAfixedValue)
                {
                    grdItem.Columns[10].Visible = true;
                }
                else
                    grdItem.Columns[11].Visible = true;

                if (AppSession.Parameter.IsTxUsingEdDetail)
                {
                    grdDetail.Columns[grdDetail.Columns.Count - 2].Visible = false;//BatchNumber
                    grdDetail.Columns[grdDetail.Columns.Count - 1].Visible = false;//ED
                }

                grdItem.Columns[grdItem.Columns.Count - 1].Visible = Request.QueryString["type"] == "1";

                StandardReference.InitializeIncludeSpace(cboSRPurchaseCategorization, AppEnum.StandardReference.PurchaseCategorization);
            }
        }

        private DataTable ItemTransactions
        {
            get
            {
                DataTable dtb; 

                if (Request.QueryString["trn"] == string.Empty)
                {
                    dtb =
                    (new InvoiceSupplierCollection()).ItemTransactionOutstandingWithParameter(
                        Request.QueryString["sid"], BusinessObject.Reference.TransactionCode.PurchaseOrderReceive,
                        BusinessObject.Reference.TransactionCode.PurchaseOrderReturn,
                        AppSession.Parameter.AllowPOCashInPOR.Equals("Yes"),
                        AppSession.Parameter.HealthcareInitialAppsVersion, cboSRPurchaseCategorization.SelectedValue);

                    // set 0 dp yang penerimaan lebih dari 1, sisain 1 POR yang pertama
                    var PORefs = dtb.AsEnumerable().Select(f => f["ReferenceNo"].ToString()).Distinct().ToArray();
                    if (PORefs.Any()) {
                        var porColl = new ItemTransactionCollection();
                        porColl.Query.Where(porColl.Query.ReferenceNo.In(PORefs), porColl.Query.IsVoid == false, porColl.Query.IsApproved == true)
                            .OrderBy(porColl.Query.ReferenceNo.Ascending, porColl.Query.TransactionNo.Ascending);
                        if (porColl.LoadAll()) {
                            foreach (var PORef in PORefs) {
                                var porCollByRef = porColl.Where(p => p.ReferenceNo == PORef).OrderBy(p => p.TransactionNo);
                                if (porCollByRef.Any()) {
                                    var i = 0;
                                    foreach (var porbr in porCollByRef) {
                                        if (i > 0) {
                                            var dtRow = dtb.AsEnumerable().Where(f => f["TransactionNo"].ToString() == porbr.TransactionNo).FirstOrDefault();
                                            if (dtRow != null) {
                                                dtRow["DownPaymentAmount"] = 0;
                                                // ribet amat ya
                                            }
                                        }
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    dtb.AcceptChanges();
                }
                else
                {
                    dtb =
                        (new InvoiceSupplierCollection()).ItemTransactionOutstandingForEditWithParameter(
                            Request.QueryString["trn"], Request.QueryString["inv"], cboSRPurchaseCategorization.SelectedValue);
                }

                return dtb;
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = ItemTransactions;
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["Detail"] != null)
                grdDetail.DataSource = ViewState["Detail"];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSourceDetail();
        }

        private void UpdateDataSourceDetail()
        {
            DataTable dtb = (DataTable)ViewState["Detail"];
            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                string seqNo = dataItem.GetDataKeyValue("SequenceNo").ToString();
                string batchNumber = ((RadTextBox)dataItem.FindControl("txtBatchNumber")).Text ?? string.Empty;
                DateTime? expiredDate = ((RadDatePicker)dataItem.FindControl("txtExpiredDate")).SelectedDate;

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["SequenceNo"].Equals(seqNo))
                    {
                        row["BatchNumber"] = batchNumber;
                        if (expiredDate != null)
                            row["ExpiredDate"] = expiredDate;
                        break;
                    }
                }

                ViewState["Detail"] = dtb;
            }
        }

        private void InitializeDataDetail(string transactionNo)
        {
            DataTable dtb;

            using (new esTransactionScope())
            {
                var itemTransaction = new ItemTransaction();
                itemTransaction.LoadByPrimaryKey(transactionNo);

                var query = new ItemTransactionItemQuery("a");
                var iq = new ItemQuery("b");
                query.LeftJoin(iq).On(query.ItemID == iq.ItemID);
                if (itemTransaction.SRItemType == BusinessObject.Reference.ItemType.Medical)
                {
                    var itemDetil = new ItemProductMedicQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }
                else if (itemTransaction.SRItemType == BusinessObject.Reference.ItemType.NonMedical)
                {
                    var itemDetil = new ItemProductNonMedicQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }
                else
                {
                    var itemDetil = new ItemKitchenQuery("c");
                    query.LeftJoin(itemDetil).On(query.ItemID == itemDetil.ItemID);
                }

                query.Where(query.TransactionNo == transactionNo);
                query.OrderBy(query.ItemID.Ascending);

                query.Select
                    (
                    query.TransactionNo,
                    query.SequenceNo,
                    query.ItemID,
                    query.SRItemUnit,
                    query.Quantity,
                    query.BatchNumber,
                    query.ExpiredDate,
                    query.Price,
                    query.Discount1Percentage,
                    query.Discount2Percentage,
                    query.Discount,
                    query.ConversionFactor,
                    "<ISNULL(b.ItemName, a.Description) ItemName>",
                    "<ISNULL(c.SRItemUnit, a.SRItemUnit) AS BaseUnitID>"
                    );
                dtb = query.LoadDataTable();
            }

            ViewState["Detail"] = dtb;
            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    //string transNo = pars[0].Split(':')[1];
                    string transNo = pars[0].ToString();
                    InitializeDataDetail(transNo);
                    break;
                case "grditem": // 
                    var command = eventArgument.Split(':')[0];
                    switch (command)
                    {
                        case "rebind":
                            grdItem.Rebind();

                            ViewState["Detail"] = null;
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            break;
                    }
                    break;
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        public override bool OnButtonOkClicked()
        {
            var invoice = (InvoiceSupplierItemCollection)Session["collInvoiceSupplierItem" + Request.UserHostName];

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    //invoice
                    string serialNo = ((RadTextBox)dataItem.FindControl("txtInvoiceSN")).Text ?? string.Empty;
                    DateTime? taxDate = ((RadDatePicker)dataItem.FindControl("txtTaxInvoiceDate")).SelectedDate;

                    var source = new ItemTransaction();
                    source.LoadByPrimaryKey(dataItem["TransactionNo"].Text);

                    var entity = (invoice.Where(i => i.TransactionNo == source.TransactionNo)).SingleOrDefault() ??
                                 invoice.AddNew();
                    entity.InvoiceNo = Request.QueryString["inv"];
                    entity.TransactionNo = source.TransactionNo;
                    entity.TransactionDate = source.TransactionDate;
                    entity.Amount = Convert.ToDecimal(dataItem["ChargesAmount"].Text);
                    entity.PPnAmount = source.TaxAmount;
                    entity.IsPpnExcluded = !(AppSession.Parameter.IsApInvoiceIncPPN);
                    entity.PPh22Amount = Convert.ToDecimal(dataItem["PPH22Amount"].Text);
                    entity.PPh23Amount = Convert.ToDecimal(dataItem["PPH23Amount"].Text);
                    if (AppSession.Parameter.IsPphUsesAfixedValue)
                    {
                        entity.PphAmount = Convert.ToDecimal(dataItem["Pph"].Text) - entity.PPh22Amount - entity.PPh23Amount;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(entity.SRPph))
                        {
                            decimal basic = Convert.ToDecimal(dataItem["PphAmount"].Text);
                            var pph = new AppStandardReferenceItem();
                            if (pph.LoadByPrimaryKey("Pph", entity.SRPph))
                            {
                                if (pph.ReferenceID == "Progresif")
                                    entity.PphAmount = InvoiceSupplier.PphProgresif(basic);
                                else
                                    entity.PphAmount = basic * (entity.PphPercentage / 100);
                            }
                            else
                                entity.PphAmount = 0;
                        }
                        else
                            entity.PphAmount = 0;// Convert.ToDecimal(dataItem["PphAmount"].Text);
                    }
                    entity.StampAmount = Convert.ToDecimal(dataItem["StampAmount"].Text);
                    entity.OtherDeduction = Convert.ToDecimal(dataItem["OtherDeduction"].Text);
                    entity.DownPaymentAmount = Convert.ToDecimal(dataItem["DownPaymentAmount"].Text);
                    entity.CurrencyID = source.CurrencyID;
                    entity.CurrencyRate = source.CurrencyRate;
                    entity.InvoiceSN = serialNo;
                    if (taxDate != null)
                        entity.TaxInvoiceDate = taxDate;
                    entity.LastUpdateDateTime = DateTime.Now;
                    entity.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    entity.InvoiceSupplierNo = source.InvoiceNo;
                    entity.InvoiceSupplierDate = source.InvoiceSupplierDate ?? source.TransactionDate;
                    entity.IsAllowEdit = true;
                    entity.SRItemType = source.SRItemType;
                }
            }

            return true;
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdItem.Rebind();
        }
    }
}
