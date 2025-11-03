using System;
using System.Drawing;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI;
using System.Linq;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Charges.Billing
{
    public partial class PatientFinancialControlList : BasePage
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

            ProgramID = AppConstant.Program.PatientFinancialControl;

            if (!IsPostBack)
            {
                txtDate.SelectedDate = DateTime.Now;

                var coll = new ServiceUnitCollection();
                var unit = new ServiceUnitQuery("a");
                unit.Where
                    (
                        unit.SRRegistrationType.In(
                            AppConstant.RegistrationType.EmergencyPatient,
                            AppConstant.RegistrationType.InPatient,
                            AppConstant.RegistrationType.OutPatient,
                            AppConstant.RegistrationType.MedicalCheckUp
                        ),
                        unit.IsActive == true
                    );
                unit.OrderBy(unit.DepartmentID.Ascending);
                coll.Load(unit);
                cboServiceUnitID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (ServiceUnit entity in coll)
                {
                    cboServiceUnitID.Items.Add(new RadComboBoxItem(entity.ServiceUnitName, entity.ServiceUnitID));
                }

                var deps = new DepartmentCollection();
                var dep = new DepartmentQuery();
                dep.Where(dep.IsActive == true);
                dep.OrderBy(dep.DepartmentID.Ascending);
                deps.Load(dep);
                cboDepartmentID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
                foreach (Department entity in deps)
                {
                    cboDepartmentID.Items.Add(new RadComboBoxItem(entity.DepartmentName, entity.DepartmentID));
                }
                cboDepartmentID.SelectedIndex = 0;

                cboStatus.Items.Add(new RadComboBoxItem("All", ""));
                cboStatus.Items.Add(new RadComboBoxItem("Balance", "0"));
                cboStatus.Items.Add(new RadComboBoxItem("Not Balance", "1"));
                cboStatus.SelectedIndex = 2;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
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

            var dataSource = Registrations;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }
        }

        private DataTable Registrations
        {
            get
            {
                var isEmptyFilter = txtDate.IsEmpty && string.IsNullOrEmpty(cboDepartmentID.SelectedValue) && string.IsNullOrEmpty(cboServiceUnitID.SelectedValue) && string.IsNullOrEmpty(txtRegistrationNo.Text) &&
                   string.IsNullOrEmpty(txtPatientName.Text);
                if (!ValidateSearch(isEmptyFilter, "Registration")) return null;

                DataTable dtb = RegistrationIps;
                dtb.Merge(RegistrationOps);

                return dtb;
            }
        }

        private DataTable RegistrationIps
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var unit = new ServiceUnitQuery("s");
                var guar = new GuarantorQuery("g");
                var dep = new DepartmentQuery("d");
                var infoSum = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.Select
                    (
                        qr.RegistrationNo,
                        qp.MedicalNo,
                        qp.PatientName,
                        unit.ServiceUnitName,
                        guar.GuarantorName,
                        qr.IsClosed,
                        qr.DepartmentID,
                        dep.DepartmentName,
                        infoSum.NoteCount,
                        @"<DATEDIFF(DAY, r.RegistrationDate, CASE WHEN r.SRDischargeMethod IS NULL THEN GETDATE() ELSE r.DischargeDate END) AS LOS>",
                        @"<0 AS DpAmount>",
                        @"<0 AS TotalTransaction>",
                        @"<0 AS TotalPayment>",
                        sal.ItemName.As("SalutationName"),
                        "<'../../Billing/FinalizeBilling/FinalizeBillingVerification.aspx?regNo=' + r.RegistrationNo + '&regType=' + r.SRRegistrationType + '&md=view&from=2' AS RegUrl>"
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
                qr.InnerJoin(dep).On(qr.DepartmentID == dep.DepartmentID);
                qr.LeftJoin(infoSum).On(qr.RegistrationNo == infoSum.RegistrationNo & infoSum.NoteCount > 0);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (!txtDate.IsEmpty)
                    qr.Where(qr.DischargeDate >= txtDate.SelectedDate, qr.DischargeDate < txtDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboDepartmentID.SelectedValue))
                    qr.Where(qr.DepartmentID == cboDepartmentID.SelectedValue);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where(
                    //    qr.Or(
                    //        qr.RegistrationNo == searchReg,
                    //        qp.MedicalNo == searchReg,
                    //        qp.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(
                    qr.SRRegistrationType == AppConstant.RegistrationType.InPatient,
                    qr.IsVoid == false
                    //qr.IsClosed == false
                    );

                qr.OrderBy(qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    row["DpAmount"] = Convert.ToDecimal(TotalDownPayment(row["RegistrationNo"].ToString()));
                    row["TotalTransaction"] = Convert.ToDecimal(TotalTransaction(row["RegistrationNo"].ToString()));
                    row["TotalPayment"] = Convert.ToDecimal(TotalPayment(row["RegistrationNo"].ToString()));

                    if (cboStatus.SelectedValue == "0")
                    {
                        if (Convert.ToDecimal(row["TotalTransaction"]) != Convert.ToDecimal(row["TotalPayment"]))
                            row.Delete();
                    } else if (cboStatus.SelectedValue == "1")
                    {
                        if (Convert.ToDecimal(row["TotalTransaction"]) == Convert.ToDecimal(row["TotalPayment"]))
                            row.Delete();
                    }
                }
                tbl.AcceptChanges();

                return tbl;
            }
        }

        private DataTable RegistrationOps
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var unit = new ServiceUnitQuery("s");
                var guar = new GuarantorQuery("g");
                var dep = new DepartmentQuery("d");
                var infoSum = new RegistrationInfoSumaryQuery("h");
                var merge = new MergeBillingQuery("m");
                var sal = new AppStandardReferenceItemQuery("sal");

                qr.Select
                    (
                        qr.RegistrationNo,
                        qp.MedicalNo,
                        qp.PatientName,
                        unit.ServiceUnitName,
                        guar.GuarantorName,
                        qr.IsClosed,
                        qr.DepartmentID,
                        dep.DepartmentName,
                        infoSum.NoteCount,
                        @"<0 AS LOS>",
                        @"<0 AS DpAmount>",
                        @"<0 AS TotalTransaction>",
                        @"<0 AS TotalPayment>",
                        sal.ItemName.As("SalutationName"),
                        "<'../../Billing/FinalizeBilling/FinalizeBillingVerification.aspx?regNo=' + r.RegistrationNo + '&regType=' + r.SRRegistrationType + '&md=view&from=2' AS RegUrl>"
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
                qr.InnerJoin(dep).On(qr.DepartmentID == dep.DepartmentID);
                qr.LeftJoin(infoSum).On(qr.RegistrationNo == infoSum.RegistrationNo & infoSum.NoteCount > 0);
                qr.InnerJoin(merge).On(qr.RegistrationNo == merge.RegistrationNo &&
                                       merge.FromRegistrationNo == string.Empty);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);

                if (!txtDate.IsEmpty)
                    qr.Where(qr.RegistrationDate >= txtDate.SelectedDate, qr.RegistrationDate < txtDate.SelectedDate.Value.AddDays(1));
                if (!string.IsNullOrEmpty(cboDepartmentID.SelectedValue))
                    qr.Where(qr.DepartmentID == cboDepartmentID.SelectedValue);
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    //qr.Where(
                    //    qr.Or(
                    //        qr.RegistrationNo == searchReg,
                    //        qp.MedicalNo == searchReg,
                    //        qp.OldMedicalNo == searchReg,
                    //        string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                    //        string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                    //        )
                    //    );
                    Helper.AddFilterMedNoOrRegNoOrPatName(qr, qp, searchReg, "registration");
                }
                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                qr.Where(
                    qr.SRRegistrationType != AppConstant.RegistrationType.InPatient,
                    qr.IsVoid == false
                    //qr.IsClosed == false
                    );

                qr.OrderBy(qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    row["DpAmount"] = Convert.ToDecimal(TotalDownPayment(row["RegistrationNo"].ToString()));
                    row["TotalTransaction"] = Convert.ToDecimal(TotalTransaction(row["RegistrationNo"].ToString()));
                    row["TotalPayment"] = Convert.ToDecimal(TotalPayment(row["RegistrationNo"].ToString()));

                    if (cboStatus.SelectedValue == "0")
                    {
                        if (Convert.ToDecimal(row["TotalTransaction"]) != Convert.ToDecimal(row["TotalPayment"]))
                            row.Delete();
                    }
                    else if (cboStatus.SelectedValue == "1")
                    {
                        if (Convert.ToDecimal(row["TotalTransaction"]) == Convert.ToDecimal(row["TotalPayment"]))
                            row.Delete();
                    }
                }
                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.DataSource = Registrations;
            grdList.DataBind();
        }

        #region Total Transaction & Payment

        protected string TotalPayment(string regNo)
        {
            string[] patientParam = new string[2],
                     mergeRegistrationNos = Helper.MergeBilling.GetMergeRegistration(regNo);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            decimal tpayment = Helper.Payment.GetTotalPayment(mergeRegistrationNos, true, patientParam);
            decimal treturn = Helper.Payment.GetTotalPayment(mergeRegistrationNos, false);
            double pat = (double)tpayment + (double)treturn;
            double guar =
                (double)Helper.Payment.GetTotalPayment(mergeRegistrationNos, true, AppSession.Parameter.PaymentTypeCorporateAR) +
                (double)Helper.Payment.GetTotalPayment(mergeRegistrationNos, true, AppSession.Parameter.PaymentTypeSaldoAR);

            return string.Format("{0:n2}", pat+guar);
        }

        protected string TotalDownPayment(string regNo)
        {
            var mergeRegistrationNos = Helper.MergeBilling.GetMergeRegistration(regNo);
            
            decimal tDownPayment = Helper.Payment.GetTotalDownPayment(mergeRegistrationNos);

            return string.Format("{0:n2}", tDownPayment);
        }

        protected string TotalTransaction(string regNo)
        {
            #region "Old"
            //var reg = Registration(regNo);
            //var additionalPlafond = AdditionalPlafond(regNo);
            //var mergeRegistrationNos = Helper.MergeBilling.GetMergeRegistration(regNo);

            //var total = TotalGuarantorAndRemainingPatientAmount(reg, mergeRegistrationNos, additionalPlafond);

            //return string.Format("{0:n2}", total);
            #endregion

            var reg = Registration(regNo);
            var mergeRegistrationNos = Helper.MergeBilling.GetMergeRegistration(regNo);

            decimal tpatient;
            decimal tguarantor;
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);


            Helper.CostCalculation.GetBillingTotal(mergeRegistrationNos, reg.SRBussinesMethod, 0, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);

            
            return string.Format("{0:n2}", tpatient + tguarantor);
        }

        private Registration Registration(string regNo)
        {
            var reg = new Registration();
            reg.LoadByPrimaryKey(regNo);
            return reg;
        }

        private decimal AdditionalPlafond(string regno)
        {
            decimal cobPlafond = 0;
            var cob = new RegistrationGuarantorCollection();
            cob.Query.Where(cob.Query.RegistrationNo == regno);
            cob.LoadAll();
            cobPlafond = cob.Sum(c => (c.PlafondAmount ?? 0));
            Session["cobPlafond"] = cobPlafond;
            return cobPlafond;
        }
        private decimal TotalGuarantorAndRemainingPatientAmount(Registration reg, string[] mergeRegistrationNos, decimal additionalPlafond)
        {
            decimal tpatient;
            decimal tguarantor;
            var guarantor = new Guarantor();
            guarantor.LoadByPrimaryKey(reg.GuarantorID);

            decimal plafondAmt = reg.PlavonAmount ?? 0;
            if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID) && plafondAmt == 0)
                plafondAmt = reg.ApproximatePlafondAmount ?? 0;

            Helper.CostCalculation.GetBillingTotal(mergeRegistrationNos, reg.SRBussinesMethod, plafondAmt + additionalPlafond, out tpatient, out tguarantor,
                                                   guarantor, reg.IsGlobalPlafond ?? false);

            var discGuarantor = (decimal)Helper.Payment.GetPaymentDiscount(mergeRegistrationNos, true);
            var totalAmountGuarantor = tguarantor - discGuarantor;
            var remainingPatientAmt = RemainingPatientAmount(reg, mergeRegistrationNos, tpatient);
            return totalAmountGuarantor + remainingPatientAmt;
        }

        private decimal RemainingPatientAmount(Registration reg, string[] mergeRegistrationNos, decimal tpatient)
        {
            var discPatient = (decimal)Helper.Payment.GetPaymentDiscount(mergeRegistrationNos, false);

            string[] patientParam = new string[2];
            patientParam.SetValue(AppSession.Parameter.PaymentTypePayment, 0);
            patientParam.SetValue(AppSession.Parameter.PaymentTypePersonalAR, 1);

            decimal tpayment = Helper.Payment.GetTotalPayment(mergeRegistrationNos, true, patientParam);
            decimal treturn = Helper.Payment.GetTotalPayment(mergeRegistrationNos, false);
            var totalPaymentAmountPatient = (decimal)tpayment + (decimal)treturn;

            var remainingAmountPatient = tpatient; //- totalPaymentAmountPatient - discPatient;

            decimal selisih = 0;
            //selisih kelas untuk bpjs
            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
                {
                    if (reg.CoverageClassID != reg.ChargeClassID)
                    {
                        //if ((reg.PlavonAmount2 ?? 0) > 0)
                        //{
                        //    var class1 = new Class();
                        //    class1.LoadByPrimaryKey(reg.CoverageClassID);

                        //    var asri1 = new AppStandardReferenceItem();
                        //    asri1.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class1.SRClassRL);

                        //    var class2 = new Class();
                        //    class2.LoadByPrimaryKey(reg.ChargeClassID);

                        //    var asri2 = new AppStandardReferenceItem();
                        //    asri2.LoadByPrimaryKey(AppEnum.StandardReference.ClassRL.ToString(), class2.SRClassRL);

                        //    if (asri2.Note.ToInt() < asri1.Note.ToInt())
                        //        selisih = (reg.PlavonAmount2 ?? 0) - (reg.PlavonAmount ?? 0);
                        //}
                        var cov = new RegistrationCoverageDetail();
                        cov.Query.Select(cov.Query.CalculatedAmount.Sum());
                        cov.Query.Where(cov.Query.RegistrationNo == reg.RegistrationNo);
                        if (cov.Query.Load()) selisih = cov.CalculatedAmount ?? 0;
                        else
                        {
                            var acov = new RegistrationApproximateCoverageDetail();
                            acov.Query.Select(acov.Query.CalculatedAmount.Sum());
                            acov.Query.Where(acov.Query.RegistrationNo == reg.RegistrationNo);
                            if (acov.Query.Load()) selisih = acov.CalculatedAmount ?? 0;
                        }
                    }
                }
            }

            if (reg.SRRegistrationType == AppConstant.RegistrationType.InPatient)
            {
                if (AppSession.Parameter.GuarantorAskesID.Contains(reg.GuarantorID))
                    if (reg.CoverageClassID != reg.ChargeClassID)
                        //if ((reg.PlavonAmount2 ?? 0) > 0)
                        //    remainingAmountPatient = (((reg.PlavonAmount2 ?? 0) == 0) ? (decimal)tpatient : (decimal)selisih) - totalPaymentAmountPatient - discPatient;
                        remainingAmountPatient = ((decimal)selisih > (decimal)tpatient ? ((decimal)tpatient == 0 ? (decimal)selisih : (decimal)tpatient) : (decimal)selisih) - totalPaymentAmountPatient - discPatient;
            }
            return remainingAmountPatient;
        }



        #endregion
    }
}
