using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.Cashier
{
    public partial class ClosingVisiteDownPaymentList : BasePage
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

            ProgramID = AppConstant.Program.ClosingVisiteDownPayment;

            if (!IsPostBack)
            {
                txtClosingDate1.SelectedDate = DateTime.Now;
                txtClosingDate2.SelectedDate = DateTime.Now;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (!IsPostBack) RestoreValueFromCookie();
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

            var dataSource = Patients;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                grd.DataSource = dataSource;
            }
        }
        protected void grdListD_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var grd = (RadGrid)source;

            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grd.DataSource = new String[] { };
                return;
            }

            var dataSource = ClosingPayments;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //SaveValueToCookie();

            grdList.Rebind();
        }

        protected void btnFilterD_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //SaveValueToCookie();

            grdListD.Rebind();
        }

        private DataTable Patients
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(txtMedicalNo.Text) && string.IsNullOrEmpty(txtPatientName.Text) && string.IsNullOrEmpty(txtSsn.Text) && txtVisitDate1.IsEmpty && txtVisitDate2.IsEmpty && 
                    string.IsNullOrEmpty(txtPaymentNo.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new TransPaymentQuery("a");
                var qp = new PatientQuery("p");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                query.Select
                    (
                        query.PatientID,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qp.Ssn,
                        sal.ItemName.As("SalutationName")
                    );

                query.InnerJoin(qp).On(qp.PatientID == query.PatientID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                query.Where(query.TransactionCode == BusinessObject.Reference.TransactionCode.DownPayment,
                             query.RegistrationNo == string.Empty,
                             query.IsVisiteDownPayment == true,
                             query.Or(query.IsClosedVisiteDownPayment.IsNull(), query.IsClosedVisiteDownPayment == false));

                if (txtMedicalNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtMedicalNo.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            query.Or(
                                qp.MedicalNo == searchReg,
                                qp.OldMedicalNo == searchReg,
                                string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        query.Where(
                            query.Or(
                                qp.MedicalNo == searchReg,
                                qp.OldMedicalNo == searchReg,
                                string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                                string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (txtSsn.Text != string.Empty)
                {
                    string searchSsn = Helper.EscapeQuery(txtSsn.Text);
                    query.Where(qp.Ssn == searchSsn);
                }
                if (!txtVisitDate1.IsEmpty && !txtVisitDate2.IsEmpty)
                    query.Where(query.PaymentDate >= txtVisitDate1.SelectedDate, query.PaymentDate <= txtVisitDate2.SelectedDate);
                if (!string.IsNullOrEmpty(txtPaymentNo.Text))
                    query.Where(query.PaymentNo == txtPaymentNo.Text);

                query.OrderBy(qp.MedicalNo.Ascending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        private DataTable ClosingPayments
        {
            get
            {
                var isEmptyFilter =string.IsNullOrEmpty(txtMedicalNoD.Text) && string.IsNullOrEmpty(txtPatientNameD.Text) && string.IsNullOrEmpty(txtSsnD.Text) && txtClosingDate1.IsEmpty &&
                    txtClosingDate2.IsEmpty && string.IsNullOrEmpty(txtClosingNo.Text) && string.IsNullOrEmpty(txtPaymentNoD.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                var query = new ClosingVisiteDownPaymentQuery("a");
                var qp = new PatientQuery("p");
                var sal = new AppStandardReferenceItemQuery("sal");

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.es.Distinct = true;

                query.Select
                    (
                        query.ClosingNo,
                        query.ClosingDate,
                        query.PatientID,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qp.Ssn,
                        sal.ItemName.As("SalutationName"), 
                        query.IsApproved,
                        query.IsVoid,
                        "<'ClosingVisiteDownPaymentDetail.aspx?md=view&id='+a.ClosingNo+'&patid='+a.PatientID+'&ut=' AS ClosingUrl>"
                    );

                query.InnerJoin(qp).On(qp.PatientID == query.PatientID);
                query.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (txtMedicalNoD.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtMedicalNoD.Text);
                    if (AppSession.Parameter.IsMedicalNoContainStrip)
                        query.Where(
                            qp.Or(
                                qp.MedicalNo == searchReg,
                                qp.OldMedicalNo == searchReg,
                                string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                                string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                                )
                            );
                    else
                        query.Where(
                            qp.Or(
                                qp.MedicalNo == searchReg,
                                qp.OldMedicalNo == searchReg,
                                string.Format("< OR p.MedicalNo LIKE '%{0}%'>", searchReg),
                                string.Format("< OR p.OldMedicalNo LIKE '%{0}%'>", searchReg)
                                )
                            );
                }
                if (txtPatientNameD.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientNameD.Text) + "%";
                    query.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }
                if (txtSsnD.Text != string.Empty)
                {
                    string searchSsn = Helper.EscapeQuery(txtSsnD.Text);
                    query.Where(qp.Ssn == searchSsn);
                }
                if (!txtClosingDate1.IsEmpty && !txtClosingDate2.IsEmpty)
                    query.Where(query.ClosingDate >= txtClosingDate1.SelectedDate, query.ClosingDate <= txtClosingDate2.SelectedDate);
                if (!string.IsNullOrEmpty(txtClosingNo.Text))
                    query.Where(query.ClosingNo == txtClosingNo.Text);
                if (!string.IsNullOrEmpty(txtPaymentNoD.Text))
                {
                    var py = new ClosingVisiteDownPaymentItemQuery("py");
                    query.InnerJoin(py).On(query.ClosingNo == py.ClosingNo);
                    query.Where(py.PaymentNo == txtPaymentNo.Text);
                }

                query.OrderBy(query.ClosingNo.Ascending);

                DataTable tbl = query.LoadDataTable();

                return tbl;
            }
        }

        protected void grdListD_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            string closingNo = e.DetailTableView.ParentItem.GetDataKeyValue("ClosingNo").ToString();

            var query = new ClosingVisiteDownPaymentItemQuery("a");
            var payment = new TransPaymentQuery("b");

            query.Select
                (
                    query.PaymentNo,
                    payment.PaymentDate,
                    payment.PaymentTime,
                    query.Amount
                );

            query.InnerJoin(payment).On(payment.PaymentNo == query.PaymentNo);

            query.Where
                (
                    query.ClosingNo == closingNo
                );

            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}