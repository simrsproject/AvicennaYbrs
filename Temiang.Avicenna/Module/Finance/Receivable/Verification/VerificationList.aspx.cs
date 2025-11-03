using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class VerificationList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;

        protected void Page_Init(object sender, EventArgs e)
        {
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }

            ProgramID = AppConstant.Program.AR_VERIFICATION;

            if (!IsPostBack)
            {
                txtFromDate.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                txtToDate.SelectedDate = DateTime.Now;
            }
            ComboBox.PopulateWithGuarantor(cboGuarantorID);
        }

        private bool ValidateSearch(bool isEmptyFilter, string searchingLabel)
        {
            if (!IsListLoadRecordIfFiltered) return true;
            if (!IsPostBack) return false;
            if (!isEmptyFilter) return true;
            if (!_isHideEmptySearchMessage)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "invalid",
                    string.Format("alert('Please entry {0} searching criteria');", searchingLabel), true);
            }
            return false;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = Invoicess;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
                if (!e.IsFromDetailTable)
                grd.DataSource = dataSource;
        }

        private DataTable Invoicess
        {
            get
            {
                var isEmptyFilter = txtFromDate.IsEmpty && txtToDate.IsEmpty && string.IsNullOrEmpty(txtInvoiceNo.Text) && string.IsNullOrEmpty(cboGuarantorID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Invoice Verification")) return null;

                var query = new InvoicesQuery("a");
                var guar = new GuarantorQuery("b");
                var sr = new AppStandardReferenceItemQuery("c");
                var itm = new InvoicesItemQuery("d");

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(
                           query.InvoiceNo,
                           query.InvoiceDate,
                           query.InvoiceDueDate,
                           @"<CASE WHEN b.GuarantorID = '" + AppSession.Parameter.SelfGuarantor + "' THEN " +
                                "(SELECT TOP 1 PatientName FROM InvoicesItem WHERE InvoiceNo = a.InvoiceNo) " +
                                "ELSE b.GuarantorName END AS GuarantorName>",                           
                           query.IsApproved,
                           query.IsVoid,
                           query.InvoiceNotes,
                           sr.ItemName.As("refToAppStandardReference_ReceivableStatusName"),
                           //@"<GETDATE() AS 'AgingDate'>",
                           "<SUM(d.Amount + ISNULL(d.PpnAmount, 0) + ISNULL(d.PphAmount, 0)) - ISNULL(a.DiscountAmount, 0)  AS TotalAmount>"
                       );

                if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI" || AppSession.Parameter.HealthcareInitialAppsVersion == "RSPM")
                    query.Select(query.InvoiceDate.As("AgingDate"));
                else
                    query.Select(@"<GETDATE() AS 'AgingDate'>");

                query.InnerJoin(guar).On(query.GuarantorID == guar.GuarantorID);
                query.InnerJoin(sr).On
                    (
                        query.SRReceivableStatus == sr.ItemID && 
                        sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
                    );
                query.InnerJoin(itm).On(itm.InvoiceNo == query.InvoiceNo);

                if (!txtFromDate.IsEmpty && !txtFromDate.IsEmpty)
                    query.Where(query.InvoiceDate >= txtFromDate.SelectedDate, query.InvoiceDate <= txtToDate.SelectedDate);

                if (!string.IsNullOrEmpty(txtInvoiceNo.Text)) {
                    query.Where(query.InvoiceNo.Like(txtInvoiceNo.Text));
                }
                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    query.Where(query.GuarantorID == cboGuarantorID.SelectedValue);

                query.Where(query.SRReceivableStatus == AppSession.Parameter.ReceivableStatusProcess);
                query.GroupBy(query.InvoiceNo, query.InvoiceDate, query.InvoiceDueDate, guar.GuarantorID, guar.GuarantorName,
                                  query.IsApproved, query.IsVoid, query.InvoiceNotes, sr.ItemName, query.DiscountAmount);

                query.OrderBy(query.InvoiceNo.Descending);

                return query.LoadDataTable();
            }
        }

        protected void btnSearchInvoice_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.DataSource = Invoicess;
            grdList.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            if (eventArgument == "process")
            {
                Validate();
                if (!IsValid)
                    return;

                Process();
                
                grdList.Rebind();
            }
        }

        private void Process()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if ((dataItem.FindControl("processChkBox") as CheckBox).Checked)
                {
                    string invoiceNo = dataItem.GetDataKeyValue("InvoiceNo").ToString();
                    DateTime agingDate = ((RadDatePicker)dataItem.FindControl("txtAgingDate")).SelectedDate ?? DateTime.Now;

                    Save(invoiceNo, agingDate);
                }
            }
        }

        private void Save(string invoiceNo, DateTime agingDate)
        {
            Invoices inv = new Invoices();
            inv.LoadByPrimaryKey(invoiceNo);
            inv.SRReceivableStatus = AppSession.Parameter.ReceivableStatusVerify;
            inv.VerifyDate = DateTime.Now;
            inv.VerifyByUserID = AppSession.UserLogin.UserID;
            inv.LastUpdateByUserID = AppSession.UserLogin.UserID;
            inv.LastUpdateDateTime = DateTime.Now;
            inv.AgingDate = agingDate;
            inv.InvoiceDueDate = agingDate.AddDays((int)inv.InvoiceTOP);

            using (esTransactionScope trans = new esTransactionScope())
            {
                inv.Save();
                trans.Complete();
            }
        }
    }
}
