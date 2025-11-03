using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Reports.OptionControl
{
    public partial class LabOrderNoCtl : BaseOptionCtl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //StandardReference Initialize
            if (!IsPostBack)
            {
            }
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).PatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString() + " [" + ((DataRowView)e.Item.DataItem)["MedicalNo"].ToString() +"]";
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }

        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboRegistrationNo.Items.Clear();
            cboRegistrationNo.Text = string.Empty;
        }

        protected void cboRegistrationNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var reg = new RegistrationQuery("a");
            var pat = new PatientQuery("b");
            var unit = new ServiceUnitQuery("c");

            reg.es.Top = 10;
            reg.Select(
                reg.RegistrationNo,
                reg.RegistrationDate,
                unit.ServiceUnitName
                );
            reg.InnerJoin(pat).On(reg.PatientID == pat.PatientID);
            reg.InnerJoin(unit).On(reg.ServiceUnitID == unit.ServiceUnitID);
            reg.Where(
                reg.PatientID == cboPatientID.SelectedValue,
                reg.IsVoid == false,
                reg.IsConsul == false,
                reg.IsFromDispensary == false
                );

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    reg.Where(
                        reg.Or(
                            reg.RegistrationNo.Like(searchLike),
                            pat.FirstName.Like(searchLike),
                            pat.LastName.Like(searchLike),
                            pat.MiddleName.Like(searchLike),
                            pat.MedicalNo.Like(searchLike)
                            )
                        );
                }
            }
            else
            {
                reg.Where(
                    reg.Or(
                        reg.RegistrationNo.Like(string.Format("%{0}%", e.Text)),
                        pat.MedicalNo.Like(string.Format("%{0}%", e.Text)),
                        pat.FirstName.Like(string.Format("%{0}%", e.Text)),
                        pat.MiddleName.Like(string.Format("%{0}%", e.Text)),
                        pat.LastName.Like(string.Format("%{0}%", e.Text))
                        )
                );
            }
            reg.OrderBy(reg.RegistrationDate.Descending, reg.RegistrationTime.Descending);

            cboRegistrationNo.DataSource = reg.LoadDataTable();
            cboRegistrationNo.DataBind();
        }

        protected void cboRegistrationNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["RegistrationNo"].ToString();
        }

        protected void cboRegistrationNo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboTransactionNo.Items.Clear();
            cboTransactionNo.Text = string.Empty;
        }

        protected void cboTransactionNo_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var tc = new TransChargesQuery("a");
            var reg = new RegistrationQuery("b");

            tc.es.Top = 10;
            tc.Select(
                tc.TransactionNo,
                tc.TransactionDate
                );
            tc.InnerJoin(reg).On(reg.RegistrationNo == tc.RegistrationNo);
            tc.Where(
                tc.RegistrationNo == cboRegistrationNo.SelectedValue,
                tc.ToServiceUnitID == AppSession.Parameter.ServiceUnitLaboratoryID,
                tc.IsApproved == true,
                tc.IsBillProceed == true,
                tc.IsVoid == false
                );

            if (e.Text.Trim().Contains(" "))
            {
                var searchs = e.Text.Trim().Split(' ');
                foreach (var search in searchs)
                {
                    var searchLike = "%" + search + "%";
                    tc.Where(tc.TransactionNo.Like(searchLike));
                }
            }
            else
            {
                tc.Where(tc.TransactionNo.Like(string.Format("%{0}%", e.Text)));
            }
            tc.OrderBy(tc.TransactionDate.Descending);

            cboTransactionNo.DataSource = tc.LoadDataTable();
            cboTransactionNo.DataBind();
        }

        protected void cboTransactionNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["TransactionNo"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["TransactionNo"].ToString();
        }

        public override PrintJobParameterCollection PrintJobParameters()
        {
            var parameters = new PrintJobParameterCollection();
            parameters.AddNew("p_TransactionNo", cboTransactionNo.SelectedValue);

            //Retun List
            return parameters;
        }
    }
}