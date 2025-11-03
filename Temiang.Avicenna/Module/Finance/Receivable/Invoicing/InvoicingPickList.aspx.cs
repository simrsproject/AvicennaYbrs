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

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicingPickList : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_INVOICING;

            if (!IsPostBack)
            {
                ViewState["InvoiceNo"] = string.Empty;
                if (Request.QueryString["all"] == "false")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["sd"]))
                        txtPaymentFromDate.SelectedDate = Convert.ToDateTime(Request.QueryString["sd"]);
                    if (!string.IsNullOrEmpty(Request.QueryString["ed"]))
                        txtPaymentToDate.SelectedDate = Convert.ToDateTime(Request.QueryString["ed"]);
                    if (!string.IsNullOrEmpty(Request.QueryString["sd2"]))
                        txtDischargeDateFrom.SelectedDate = Convert.ToDateTime(Request.QueryString["sd2"]);
                    if (!string.IsNullOrEmpty(Request.QueryString["ed2"]))
                        txtDischargeDateTo.SelectedDate = Convert.ToDateTime(Request.QueryString["ed2"]);
                }

                var suColl = new ServiceUnitCollection();
                suColl.Query.Where(suColl.Query.IsActive == true, suColl.Query.SRRegistrationType != string.Empty);
                suColl.Query.OrderBy(suColl.Query.ServiceUnitName.Ascending);
                suColl.LoadAll();

                cboServiceUnit.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (var su in suColl) {
                    cboServiceUnit.Items.Add(new RadComboBoxItem(su.ServiceUnitName, su.ServiceUnitID));
                }
            }
        }

        private DataTable TransPayments
        {
            get
            {
                string paymentType = Request.QueryString["rt"] == AppSession.Parameter.ReceivableTypeCorporate
                                     ? AppSession.Parameter.PaymentTypeCorporateAR
                                     : AppSession.Parameter.PaymentTypePersonalAR;

                DataTable dtb =
                    (new InvoicesCollection()).TransPaymentOutstandingWithParameter(
                    Request.QueryString["gid"], paymentType, txtPaymentFromDate.SelectedDate,
                    txtPaymentToDate.SelectedDate, txtRegistrationNo.Text, txtPatientName.Text,
                    cboGuarantorID.SelectedValue, AppSession.Parameter.HealthcareInitialAppsVersion,
                    txtDischargeDateFrom.SelectedDate, txtDischargeDateTo.SelectedDate, 
                    Request.QueryString["regtype"], AppConstant.RegistrationType.InPatient, 
                    cboServiceUnit.SelectedValue);

                return dtb;
            }
        }

        protected void grdItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdItem.DataSource = TransPayments;
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
            var invoice = (InvoicesItemCollection)Session["collInvoicesItem" + Request.UserHostName];

            //if (AppSession.Parameter.HealthcareInitialAppsVersion != "RSUI") invoice.MarkAllAsDeleted();

            foreach (GridDataItem dataItem in grdItem.MasterTableView.Items)
            {
                if (((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                {
                    //invoice
                    {
                        var source = new TransPayment();
                        source.LoadByPrimaryKey(dataItem["PaymentNo"].Text);

                        //if (AppSession.Parameter.HealthcareInitialAppsVersion == "RSUI")
                        //{
                        if (invoice.Any(i => i.PaymentNo == source.PaymentNo)) continue;
                        //}

                        var entity = invoice.AddNew();

                        entity.InvoiceNo = Request.QueryString["inv"].ToString();
                        entity.PaymentNo = source.PaymentNo;
                        entity.PaymentDate = source.PaymentDate;
                        entity.RegistrationNo = source.RegistrationNo;

                        var reg = new Registration();
                        reg.LoadByPrimaryKey(entity.RegistrationNo);

                        entity.PatientID = reg.PatientID;

                        var pat = new Patient();
                        pat.LoadByPrimaryKey(entity.PatientID);

                        entity.PatientName = pat.PatientName;
                        entity.Amount = Convert.ToDecimal(dataItem["Amount"].Text);
                        entity.VerifyAmount = entity.Amount;
                        entity.PpnAmount = 0;
                        entity.PphAmount = 0;
                        entity.IsPpn = false;
                        entity.IsPph = false;
                        entity.PpnPercentage = 0;
                        entity.PphPercentage = 0;
                        entity.SRPph = string.Empty;
                        entity.ClaimDifferenceAmount = 0;
                        entity.LastUpdateDateTime = DateTime.Now;
                        entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

                        var guarId = reg.GuarantorID;
                        var py = new TransPayment();
                        if (py.LoadByPrimaryKey(entity.PaymentNo))
                        {
                            guarId = py.GuarantorID;
                        }

                        var guar = new Guarantor();
                        guar.LoadByPrimaryKey(guarId);
                        entity.GuarantorName = guar.GuarantorName;
                    }
                }
            }

            return true;
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            grdItem.Rebind();
        }

        protected void cboGuarantorID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }

        protected void cboGuarantorID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new GuarantorQuery();
            query.es.Top = 20;
            query.Select(
                query.GuarantorID,
                query.GuarantorName
                );
            query.Where(
                query.GuarantorName.Like(searchTextContain),
                query.GuarantorHeaderID == Request.QueryString["gid"]
                );
            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }
    }
}
