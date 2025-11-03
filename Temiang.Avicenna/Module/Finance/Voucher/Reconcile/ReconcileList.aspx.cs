using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.Module.Finance.Voucher.Reconcile
{
    public partial class ReconcileList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.Journal_Reconcile;

            if (!IsPostBack)
            {
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

                if (rblRegistrationType.SelectedIndex == 0)
                    lblDate.Text = "Discharge Date";
                else lblDate.Text = "Registration Date";

                txtDate.SelectedDate = DateTime.Now;

                cboFilter.Items.Add(new RadComboBoxItem("All", "0"));
                cboFilter.Items.Add(new RadComboBoxItem("Need Reconciliation", "1"));
                cboFilter.SelectedValue = "1";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack) RestoreValueFromCookie();
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!IsPostBack && !IsListLoadRecordOnInit) return;

            grdList.DataSource = Registrations;
        }

        private DataTable Registrations
        {
            get
            {
                var qr = new RegistrationQuery("r");
                var qp = new PatientQuery("p");
                var qm = new ParamedicQuery("m");
                var unit = new ServiceUnitQuery("s");
                var room = new ServiceRoomQuery("d");
                var mrg = new MergeBillingQuery("b");
                var guar = new GuarantorQuery("g");
                var infoSum = new RegistrationInfoSumaryQuery("h");
                var sal = new AppStandardReferenceItemQuery("sal");
                var gdc = new GuarantorDocumentChecklistQuery("gdc");
                var dc = new AppStandardReferenceItemQuery("dc");

                qr.es.Top = AppSession.Parameter.MaxResultRecord;

                qr.Select
                    (
                    qr.PatientID,
                        qr.RegistrationNo,
                        qr.RegistrationDate,
                        qr.RegistrationTime,
                        qp.MedicalNo,
                        qp.PatientName,
                        qp.Sex,
                        qm.ParamedicName,
                        unit.ServiceUnitName,
                        room.RoomName,
                        qr.BedID,
                        qr.IsTransferedToInpatient,
                        qr.SRRegistrationType,
                        qr.IsHoldTransactionEntry,
                        guar.GuarantorName,
                        qr.DischargePlanDate,
                        qr.IsConsul,
                        qr.SRRegistrationType,
                        qr.DischargeDate,
                        "<CASE WHEN r.DischargeDate IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsDischarge>",
                        qr.ChargeClassID, qr.CoverageClassID, qr.ClassID, qr.PlavonAmount, sal.ItemName.As("SalutationName"),
                        "<CASE WHEN (r.ParamedicID IS NULL OR r.IsConsul = 1) THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsNewVisible>",
                        "<CASE WHEN b.FromRegistrationNo = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS IsMergeBilling>",
                        qr.ExternalQueNo,
                        "<CAST(0 AS NUMERIC(18,2)) AS Debit>", "<CAST(0 AS NUMERIC(18,2)) AS Credit>", "<CAST(0 AS NUMERIC(18,2)) AS Diff>"
                    );

                qr.InnerJoin(qp).On(qr.PatientID == qp.PatientID);
                qr.LeftJoin(qm).On(qr.ParamedicID == qm.ParamedicID);
                qr.LeftJoin(unit).On(qr.ServiceUnitID == unit.ServiceUnitID);
                qr.LeftJoin(room).On(qr.RoomID == room.RoomID);
                qr.InnerJoin(mrg).On(qr.RegistrationNo == mrg.RegistrationNo);
                qr.InnerJoin(guar).On(qr.GuarantorID == guar.GuarantorID);
                qr.LeftJoin(infoSum).On(qr.RegistrationNo == infoSum.RegistrationNo);
                qr.LeftJoin(sal).On(sal.StandardReferenceID == "Salutation" & qp.SRSalutation == sal.ItemID);
                qr.LeftJoin(gdc).On(qr.GuarantorID == gdc.GuarantorID & qr.SRRegistrationType == gdc.SRRegistrationType);
                qr.LeftJoin(dc).On(dc.StandardReferenceID == "DocumentChecklist" & gdc.SRDocumentChecklist == dc.ItemID);

                if (rblRegistrationType.SelectedIndex == 0)
                {
                    qr.Where(qr.SRRegistrationType == AppConstant.RegistrationType.InPatient, qr.DischargeDate.Date() == txtDate.SelectedDate);
                }
                else 
                {
                    qr.Where(qr.SRRegistrationType != AppConstant.RegistrationType.InPatient, qr.RegistrationDate.Date() == txtDate.SelectedDate);
                }
                    
                if (cboServiceUnitID.SelectedValue != string.Empty)
                    qr.Where(qr.ServiceUnitID == cboServiceUnitID.SelectedValue);
                if (txtRegistrationNo.Text != string.Empty)
                {
                    string searchReg = Helper.EscapeQuery(txtRegistrationNo.Text);
                    qr.Where(
                        qr.Or(
                            qr.RegistrationNo == searchReg,
                            qp.MedicalNo == searchReg,
                            qp.OldMedicalNo == searchReg,
                            string.Format("< OR REPLACE(p.MedicalNo, '-', '') LIKE '%{0}%'>", searchReg),
                            string.Format("< OR REPLACE(p.OldMedicalNo, '-', '') LIKE '%{0}%'>", searchReg)
                            )
                        );
                }

                if (txtPatientName.Text != string.Empty)
                {
                    string searchPatient = "%" + Helper.EscapeQuery(txtPatientName.Text) + "%";
                    qr.Where
                        (
                          string.Format("<LTRIM(RTRIM(LTRIM(p.FirstName + ' ' + p.MiddleName)) + ' ' + p.LastName) LIKE '{0}'>", searchPatient)
                        );
                }

                if (!string.IsNullOrEmpty(cboGuarantorID.SelectedValue))
                    qr.Where(qr.GuarantorID == cboGuarantorID.SelectedValue);
                if (chkIsReconcile.Checked)
                    qr.Where(qr.IsReconcile == true);
                else qr.Where(qr.Or(qr.IsReconcile == false, qr.IsReconcile.IsNull()));

                qr.Where(qr.IsVoid == false);

                if (!AppSession.Parameter.IsSeparatePaymentForOpConsul && AppSession.Parameter.HealthcareInitialAppsVersion != "RSMM")
                    qr.Where(qr.Or(qr.IsConsul == false, mrg.FromRegistrationNo == string.Empty));

                qr.OrderBy(qr.RegistrationDate.Descending, qr.RegistrationNo.Descending);

                DataTable tbl = qr.LoadDataTable();

                foreach (DataRow row in tbl.Rows)
                {
                    decimal _debit = 0;
                    decimal _credit = 0;

                    var h = new JournalTransactionsQuery("h");
                    var j = new JournalTransactionDetailsQuery("j");
                    var c = new ChartOfAccountsQuery("c");
                    var s = new SubLedgersQuery("s");
                    var v = new VwTransactionQuery("v");

                    j.Select(j.Debit.Sum().As("Debit"), j.Credit.Sum().As("Credit"));
                    j.InnerJoin(h).On(j.JournalId == h.JournalId);
                    j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
                    j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
                    j.InnerJoin(v).On(h.RefferenceNumber == v.TransactionNo);
                    j.Where(h.IsPosted == true, c.IsReconcile == true, v.RegistrationNo.In(MergeRegistrationList(row["RegistrationNo"].ToString())));

                    DataTable dtb = j.LoadDataTable();
                    if (dtb.Rows.Count > 0)
                    {
                        _debit += dtb.Rows[0]["Debit"].ToDecimal();
                        _credit += dtb.Rows[0]["Credit"].ToDecimal();
                    }

                    h = new JournalTransactionsQuery("h");
                    j = new JournalTransactionDetailsQuery("j");
                    c = new ChartOfAccountsQuery("c");
                    s = new SubLedgersQuery("s");
                    var p = new TransPaymentQuery("p");

                    j.Select(j.Debit.Sum().As("Debit"), j.Credit.Sum().As("Credit"));
                    j.InnerJoin(h).On(j.JournalId == h.JournalId);
                    j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
                    j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
                    j.InnerJoin(p).On(h.RefferenceNumber == p.PaymentNo);
                    j.Where(h.IsPosted == true, c.IsReconcile == true, p.RegistrationNo.In(MergeRegistrationList(row["RegistrationNo"].ToString())));

                    DataTable dtb2 = j.LoadDataTable();
                    if (dtb2.Rows.Count > 0)
                    {
                        _debit += dtb2.Rows[0]["Debit"].ToDecimal();
                        _credit += dtb2.Rows[0]["Credit"].ToDecimal();
                    }

                    row["Debit"] = _debit;
                    row["Credit"] = _credit;
                    row["Diff"] = _debit - _credit;

                    if (_debit == _credit && !chkIsReconcile.Checked && cboFilter.SelectedValue == "1")
                        row.Delete();
                }
                tbl.AcceptChanges();

                return tbl;
            }
        }

        protected void rblRegistrationType_OnTextChanged(object sender, EventArgs e)
        {
            if (rblRegistrationType.SelectedIndex == 0)
                lblDate.Text = "Discharge Date";
            else lblDate.Text = "Registration Date";

            SaveValueToCookie();

            grdList.DataSource = Registrations;
            grdList.DataBind();
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            SaveValueToCookie();

            grdList.DataSource = Registrations;
            grdList.DataBind();
        }

        protected void chkIsReconcile_CheckedChanged(object sender, EventArgs e)
        {
            SaveValueToCookie();

            grdList.DataSource = Registrations;
            grdList.DataBind();
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
            query.es.Top = 30;
            query.Where
                (
                    query.GuarantorName.Like(searchTextContain),
                    query.SRGuarantorType != AppSession.Parameter.GuarantorTypeMemberID,
                    query.IsActive == true
                );
            query.OrderBy(query.GuarantorName.Ascending);

            cboGuarantorID.DataSource = query.LoadDataTable();
            cboGuarantorID.DataBind();
        }

        private string[] MergeRegistrationList(string registrationNo)
        {
            return Helper.MergeBilling.GetMergeRegistration(registrationNo);
        }
    }
}