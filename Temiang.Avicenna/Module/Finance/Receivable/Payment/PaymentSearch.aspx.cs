using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class PaymentSearch : BasePageDialog
    {
        public class SearchValue
        {
            public string InvoiceNo;
            public string InvoiceReferenceNo;
            public DateTime? InvoiceDate;
            public DateTime? InvoiceDateTo;
            public DateTime? PaymentDate;
            public DateTime? PaymentDateTo;
            public string GuarantorID;
            public string GuarantorName;
            public string RegistrationNo;
            public string MedicalNo;
            public string PatientName;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.AR_PAYMENT;

            if (Session[SessionNameForQuery] != null)
            {
                PaymentSearch.SearchValue sv = (PaymentSearch.SearchValue)Session[SessionNameForQuery];
                txtInvoiceNo.Text = sv.InvoiceNo;
                txtInvoiceReferenceNo.Text = sv.InvoiceReferenceNo;
                txtInvoiceDate.SelectedDate = sv.InvoiceDate;
                txtInvoiceDateTo.SelectedDate = sv.InvoiceDateTo;
                txtPaymentDate.SelectedDate = sv.PaymentDate;
                txtPaymentDateTo.SelectedDate = sv.PaymentDateTo;
                //txtGuarantorName.Text = sv.GuarantorName;
                cboGuarantorName.SelectedValue = sv.GuarantorID;
                cboGuarantorName.Text = sv.GuarantorName;
                txtRegistrationNo.Text = sv.RegistrationNo;
                txtMedicalNo.Text = sv.MedicalNo;
                txtPatientName.Text = sv.PatientName;
            }
        }

        public override bool OnButtonOkClicked()
        {
            SearchValue sv = new SearchValue();

            sv.InvoiceNo = txtInvoiceNo.Text;
            sv.InvoiceReferenceNo = txtInvoiceReferenceNo.Text;
            sv.InvoiceDate = txtInvoiceDate.SelectedDate;
            sv.InvoiceDateTo = txtInvoiceDateTo.SelectedDate;
            sv.PaymentDate = txtPaymentDate.SelectedDate;
            sv.PaymentDateTo = txtPaymentDateTo.SelectedDate;
            //sv.GuarantorName = txtGuarantorName.Text;
            sv.GuarantorID = cboGuarantorName.SelectedValue;
            sv.GuarantorName = cboGuarantorName.Text;
            sv.RegistrationNo = txtRegistrationNo.Text;
            sv.MedicalNo = txtMedicalNo.Text;
            sv.PatientName = txtPatientName.Text;

            Session[SessionNameForQuery] = sv;
            Session.Remove(SessionNameForList); //reset

            return true;

            //var query = new InvoicesQuery("a");
            //var guar = new GuarantorQuery("b");
            //var sr = new AppStandardReferenceItemQuery("c");
            //var itm = new InvoicesItemQuery("d");

            //query.LeftJoin(itm).On(query.InvoiceNo == itm.InvoiceNo);
            //query.LeftJoin(guar).On(query.GuarantorID == guar.GuarantorID);
            //query.LeftJoin(sr).On(
            //    query.SRReceivableStatus == sr.ItemID &&
            //    sr.StandardReferenceID == AppEnum.StandardReference.ReceivableStatus
            //    );

            //query.Select(
            //       query.InvoiceNo,
            //       query.InvoiceReferenceNo,
            //       query.PaymentDate,
            //       guar.GuarantorName,
            //       query.IsApproved,
            //       query.IsVoid,
            //       sr.ItemName.As("ReceivableStatus"),
            //       "<SUM(ISNULL(d.PaymentAmount, 0)) AS ReceivableStatus>",
            //       query.SRPhysicianFeeProportionalStatus,
            //       query.PhysicianFeeProportionalPercentage
            //   );

            //query.Where(query.IsInvoicePayment == true, query.IsWriteOff == false, query.IsAddPayment.IsNull());
            //query.GroupBy(query.InvoiceNo, query.InvoiceReferenceNo, query.PaymentDate, guar.GuarantorName,
            //              query.IsApproved, query.IsVoid, sr.ItemName, query.SRPhysicianFeeProportionalStatus,
            //                      query.PhysicianFeeProportionalPercentage);
            //query.OrderBy(query.InvoiceNo.Descending);

            //if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
            //{
            //    if (cboFilterInvoiceNo.SelectedIndex == 1)
            //        query.Where(query.InvoiceNo == txtInvoiceNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtInvoiceNo.Text);
            //        query.Where(query.InvoiceNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtInvoiceReferenceNo.Text))
            //{
            //    if (cboFilterInvoiceReferenceNo.SelectedIndex == 1)
            //    {
            //        //query.Where(query.InvoiceReferenceNo == txtInvoiceReferenceNo);
            //        query.Where(itm.InvoiceReferenceNo == txtInvoiceReferenceNo);
            //    }
            //    else
            //    {
            //        //query.Where(query.InvoiceReferenceNo.Like(string.Format(".%{0}%", txtInvoiceReferenceNo.Text)));
            //        string searchTextContain = string.Format("%{0}%", txtInvoiceReferenceNo.Text);
            //        query.Where(itm.InvoiceReferenceNo.Like(searchTextContain));
            //    }
            //}
            //if (!txtInvoiceDate.SelectedDate.ToString().Trim().Equals(string.Empty) && !txtInvoiceDateTo.SelectedDate.ToString().Trim().Equals(string.Empty))
            //{
            //    query.Where(query.InvoiceDate >= txtInvoiceDate.SelectedDate, query.InvoiceDate <= txtInvoiceDateTo.SelectedDate);
            //}
            //if (!txtPaymentDate.IsEmpty && !txtPaymentDateTo.IsEmpty)
            //{
            //    query.Where(query.PaymentDate >= txtPaymentDate.SelectedDate, query.PaymentDate <= txtPaymentDateTo.SelectedDate);
            //}
            //if (!string.IsNullOrEmpty(txtGuarantorName.Text))
            //{
            //    if (cboFilterGuarantorName.SelectedIndex == 1)
            //        query.Where(guar.GuarantorName == txtGuarantorName.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtGuarantorName.Text);
            //        query.Where(guar.GuarantorName.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtRegistrationNo.Text))
            //{
            //    if (cboFilterRegistrationNo.SelectedIndex == 1)
            //        query.Where(itm.RegistrationNo == txtRegistrationNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtRegistrationNo.Text);
            //        query.Where(itm.RegistrationNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtMedicalNo.Text))
            //{
            //    var pat = new PatientQuery("pat");
            //    query.InnerJoin(pat).On(itm.PatientID == pat.PatientID);
            //    if (cboFilterMedicalNo.SelectedIndex == 1)
            //        query.Where(pat.MedicalNo == txtMedicalNo.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtMedicalNo.Text);
            //        query.Where(pat.MedicalNo.Like(searchTextContain));
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtPatientName.Text))
            //{
            //    if (cboFilterPatientName.SelectedIndex == 1)
            //        query.Where(itm.PatientName == txtPatientName.Text);
            //    else
            //    {
            //        string searchTextContain = string.Format("%{0}%", txtPatientName.Text);
            //        query.Where(itm.PatientName.Like(searchTextContain));
            //    }
            //}

            //Session[SessionNameForQuery] = query;
            //Session.Remove(SessionNameForList); //reset

            //return true;
        }

        protected void cboGuarantorName_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var gua = new GuarantorQuery("a");
            gua.Where(gua.IsActive.Equal(true));
            if (cboFilterGuarantorName.SelectedIndex == 1)
                gua.Where(gua.Or(gua.GuarantorID.Like(e.Text + "%"), gua.GuarantorName.Like(e.Text + "%")));
            else
                gua.Where(gua.Or(gua.GuarantorID.Like("%" + e.Text + "%"), gua.GuarantorName.Like("%" + e.Text + "%")));
            gua.Select(gua.GuarantorID, gua.GuarantorName);
            gua.es.Top = 20;

            DataTable tbl = gua.LoadDataTable();
            var r = tbl.NewRow();
            r["GuarantorID"] = r["GuarantorName"] = string.Empty;
            tbl.Rows.InsertAt(r, 0);
            cboGuarantorName.DataSource = tbl;
            cboGuarantorName.DataBind();
        }
        protected void cboGuarantorName_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["GuarantorName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["GuarantorID"].ToString();
        }
        protected void cboFilterGuarantorName_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboGuarantorName.Items.Clear();
            cboGuarantorName.SelectedValue = string.Empty;
            cboGuarantorName.Text = string.Empty;
        }
    }
}
