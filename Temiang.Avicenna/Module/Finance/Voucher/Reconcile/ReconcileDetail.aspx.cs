using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using Temiang.Dal.DynamicQuery;
using Temiang.Avicenna.BusinessObject.Reference;
using System.Configuration;
using Temiang.Avicenna.Module.RADT;

namespace Temiang.Avicenna.Module.Finance.Voucher.Reconcile
{
    public partial class ReconcileDetail : BasePage
    {
        private decimal _debit;
        private decimal _credit;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            ProgramID = AppConstant.Program.Journal_Reconcile;

            if (!IsPostBack)
            {
                _debit = 0;
                _credit = 0;

                PopulateEntryControl();
                CollapsePanel1.IsCollapsed = "true";

                cboSortBy.Items.Add(new RadComboBoxItem("Journal ID", "0"));
                cboSortBy.Items.Add(new RadComboBoxItem("Account Code", "1"));
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }

        private void PopulateEntryControl()
        {
            var regNo = Page.Request.QueryString["regNo"];
            if (string.IsNullOrEmpty(regNo))
                return;

            var registration = new Registration();
            registration.LoadByPrimaryKey(regNo);

            txtRegistrationNo.Text = registration.RegistrationNo;

            var patient = new Patient();
            patient.LoadByPrimaryKey(registration.PatientID);

            txtMedicalNo.Text = patient.MedicalNo;
            var std = new AppStandardReferenceItem();
            txtSalutation.Text = std.LoadByPrimaryKey("Salutation", patient.SRSalutation) ? std.ItemName : string.Empty;
            txtPatientName.Text = patient.PatientName;
            txtPlaceDOB.Text = string.Format("{0}, {1}", patient.CityOfBirth, Convert.ToDateTime(patient.DateOfBirth).ToString("dd-MMM-yyyy"));

            var par = new Paramedic();
            if (par.LoadByPrimaryKey(registration.ParamedicID))
                txtParamedicName.Text = par.ParamedicName;
            else txtParamedicName.Text = string.Empty;

            var unit = new ServiceUnit();
            if (unit.LoadByPrimaryKey(registration.ServiceUnitID))
                txtServiceUnitName.Text = unit.ServiceUnitName;
            else txtServiceUnitName.Text = string.Empty;

            var guar = new Guarantor();
            if (guar.LoadByPrimaryKey(registration.GuarantorID))
                txtGuarantorName.Text = guar.GuarantorName;
            else txtGuarantorName.Text = string.Empty;

            optSexMale.Checked = (patient.Sex == "M");
            optSexMale.Enabled = (patient.Sex == "M");
            optSexFemale.Checked = (patient.Sex == "F");
            optSexFemale.Enabled = (patient.Sex == "F");

            txtAgeYear.Value = (double)registration.AgeInYear;
            txtAgeMonth.Value = (double)registration.AgeInMonth;
            txtAgeDay.Value = (double)registration.AgeInDay;

            var cls = new Class();
            if (cls.LoadByPrimaryKey(registration.ChargeClassID))
                txtChargeClassName.Text = cls.ClassName;
            else txtChargeClassName.Text = string.Empty;

            RefreshButtonReconsile(registration.IsReconcile ?? false);

            txtRegistrationDate.SelectedDate = registration.RegistrationDate;
            txtRegistrationTime.Text = registration.RegistrationTime;
            pnlForInpatient.Visible = (registration.SRRegistrationType == AppConstant.RegistrationType.InPatient);
            if (pnlForInpatient.Visible)
            {
                var x = registration.DischargeDate != null ? registration.DischargeDate.Value.Date : (new DateTime()).NowAtSqlServer().Date;
                var y = registration.RegistrationDate.Value.Date;
                txtLengthOfStay.Value = (x - y).TotalDays == 0 ? 1 : (x - y).TotalDays + 1;

                txtDischargeDate.SelectedDate = registration.DischargeDate;
                txtDischargeTime.Text = registration.DischargeTime;

            }

            CollapsePanel1.Title = txtPatientName.Text +
                                        " [" + txtMedicalNo.Text + "] [" + txtRegistrationNo.Text + "] " +
                                        (optSexMale.Checked ? "M [" : "F [") +
                                        txtParamedicName.Text + "] " +
                                        txtServiceUnitName.Text + ", " +
                                        txtGuarantorName.Text;

            lblRegistrationInfo2.Text = RegistrationInfoSumary.GetDocumentCheckListCountRemains(txtRegistrationNo.Text);
        }

        private void Refresh()
        {
            grdVoucherEntryItem.Rebind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(source is RadGrid))
                return;

            switch (eventArgument)
            {
                case "rebind":
                    Refresh();
                    break;

                case "reconsile":
                    Validate();
                    if (!IsValid)
                        return;
                    Reconsiled();
                    break;
            }
        }

        private string[] MergeRegistrationList()
        {
            if (ViewState["JournalReconsile:MergeRegistration" + Request.UserHostName] == null)
                ViewState["JournalReconsile:MergeRegistration" + Request.UserHostName] = Helper.MergeBilling.GetMergeRegistration(txtRegistrationNo.Text);

            return (string[])ViewState["JournalReconsile:MergeRegistration" + Request.UserHostName];
        }

        #region RadToolBar
        private void Reconsiled()
        {
            var r = new Registration();
            r.LoadByPrimaryKey(txtRegistrationNo.Text);
            bool statusNow = (r.IsReconcile ?? false);

            using (var trans = new esTransactionScope())
            {
                var regColl = new RegistrationCollection();
                regColl.Query.Where(regColl.Query.RegistrationNo.In(MergeRegistrationList()));
                regColl.LoadAll();

                foreach (var reg in regColl)
                {
                    reg.IsReconcile = !statusNow;
                }

                regColl.Save();
                trans.Complete();
            }

            RefreshButtonReconsile(!statusNow);
        }

        private void RefreshButtonReconsile(bool? isReconsile)
        {
            var btn = RadToolBar2.Items[2];
            btn.Text = isReconsile ?? false ? "Canceled Reconciliation" : "Reconciled";
            btn.ImageUrl = isReconsile ?? false ? "~/Images/Toolbar/post_green_16.png" : "~/Images/Toolbar/post16_d.png";
            btn.HoveredImageUrl = isReconsile ?? false ? "~/Images/Toolbar/post_green_16.png" : "~/Images/Toolbar/post16_d.png";
            btn.DisabledImageUrl = isReconsile ?? false ? "~/Images/Toolbar/post_green_16.png" : "~/Images/Toolbar/post16_d.png";
        }
        #endregion

        #region Tab: List
        protected void grdVoucherEntryItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdVoucherEntryItem.DataSource = VoucherEntryItems;
        }

        private DataTable VoucherEntryItems
        {
            get
            {
                DataTable dtb;
                dtb = Transactions;
                dtb.Merge(Payments);

                txtDebit.Value = Convert.ToDouble(_debit);
                txtCredit.Value= Convert.ToDouble(_credit);
                txtDifferent.Value = Convert.ToDouble(_debit - _credit);

                if (cboSortBy.SelectedValue == "0") //id
                    dtb.DefaultView.Sort = "DetailId ASC"; 
                else //code
                    dtb.DefaultView.Sort = "ChartOfAccountCode ASC, DetailId ASC";

                dtb.DefaultView.ToTable();

                return dtb;
            }
            
        }

        private DataTable Transactions
        {
            get
            {
                var h = new JournalTransactionsQuery("h");
                var j = new JournalTransactionDetailsQuery("j");
                var c = new ChartOfAccountsQuery("c");
                var s = new SubLedgersQuery("s");
                var v = new VwTransactionQuery("v");

                j.Select(j, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
                j.InnerJoin(h).On(j.JournalId == h.JournalId);
                j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
                j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
                j.InnerJoin(v).On(h.RefferenceNumber == v.TransactionNo);
                j.Where(h.IsPosted == true, v.RegistrationNo.In(MergeRegistrationList()));
                if (rblJournal.SelectedIndex == 0)
                    j.Where(c.IsReconcile == true);
                if (!string.IsNullOrEmpty(cboChartOfAccountId.SelectedValue))
                    j.Where(j.ChartOfAccountId == cboChartOfAccountId.SelectedValue.ToInt());

                j.OrderBy(j.DetailId.Ascending);

                DataTable dtb = j.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    _debit += row["Debit"].ToDecimal();
                    _credit += row["Credit"].ToDecimal();
                }

                return dtb;
            }
        }

        private DataTable Payments
        {
            get
            {
                var h = new JournalTransactionsQuery("h");
                var j = new JournalTransactionDetailsQuery("j");
                var c = new ChartOfAccountsQuery("c");
                var s = new SubLedgersQuery("s");
                var p = new TransPaymentQuery("p");

                j.Select(j, c.ChartOfAccountCode, c.ChartOfAccountName, s.SubLedgerName, s.Description.As("SubLedger_Description"));
                j.InnerJoin(h).On(j.JournalId == h.JournalId);
                j.InnerJoin(c).On(j.ChartOfAccountId == c.ChartOfAccountId);
                j.LeftJoin(s).On(j.SubLedgerId == s.SubLedgerId);
                j.InnerJoin(p).On(h.RefferenceNumber == p.PaymentNo);
                j.Where(h.IsPosted == true, p.RegistrationNo.In(MergeRegistrationList()));
                if (rblJournal.SelectedIndex == 0)
                    j.Where(c.IsReconcile == true);
                if (!string.IsNullOrEmpty(cboChartOfAccountId.SelectedValue))
                    j.Where(j.ChartOfAccountId == cboChartOfAccountId.SelectedValue.ToInt());

                j.OrderBy(j.DetailId.Ascending);

                DataTable dtb = j.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    _debit += row["Debit"].ToDecimal();
                    _credit += row["Credit"].ToDecimal();
                }

                return dtb;
            }
        }

        protected void rblJournal_OnTextChanged(object sender, EventArgs e)
        {
            cboChartOfAccountId.Items.Clear();
            cboChartOfAccountId.SelectedValue = string.Empty;
            cboChartOfAccountId.Text = string.Empty;

            grdVoucherEntryItem.Rebind();
        }

        protected void cboSortBy_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdVoucherEntryItem.Rebind();
        }

        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grdVoucherEntryItem.Rebind();
        }

        protected void cboChartOfAccountId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new ChartOfAccountsQuery();
            query.Select(query.ChartOfAccountId, query.ChartOfAccountCode, query.ChartOfAccountName);
            query.Where
                        (
                            query.Or
                            (
                                query.ChartOfAccountCode.Like(searchTextContain),
                                query.ChartOfAccountName.Like(searchTextContain)
                            ), 
                            query.IsDetail == true
                        );
            if (rblJournal.SelectedIndex == 0)
                query.Where(query.IsReconcile == true);

            query.es.Top = 20;
            var tbl = query.LoadDataTable();
            cboChartOfAccountId.DataSource = tbl;
            cboChartOfAccountId.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + @" - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        #endregion
    }
}