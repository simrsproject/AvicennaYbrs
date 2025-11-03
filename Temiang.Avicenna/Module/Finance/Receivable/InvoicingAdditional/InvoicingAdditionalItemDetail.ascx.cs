using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.Receivable
{
    public partial class InvoicingAdditionalItemDetail : BaseUserControl
    {
        private object _dataItem;

        public object DataItem
        {
            get { return _dataItem; }
            set { _dataItem = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            StandardReference.InitializeIncludeSpace(cboSRPph, AppEnum.StandardReference.Pph);

            var patt = new Patient();
            var coa = new ChartOfAccounts();
            if (DataItem is GridInsertionObject)
            {
                txtPaymentNo.Text = "-";
                txtAmount.Value = 0;
                chkIsPph.Checked = false;
                chkIsPph.Checked = false;
                txtPpnPercentage.Value = 0;
                txtPphPercentage.Value = 0;
                txtPpnAmount.Value = 0;
                txtPphAmount.Value = 0;

                var guar = new Guarantor();
                var guarID = ((RadComboBox)Helper.FindControlRecursive(this.Page, "cboGuarantorID")).SelectedValue;
                if (guar.LoadByPrimaryKey(guarID))
                {
                    if (coa.LoadByPrimaryKey(guar.ChartOfAccountIdTemporary ?? 0))
                    {
                        //ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                        var args = new RadComboBoxItemsRequestedEventArgs();
                        args.Text = coa.ChartOfAccountCode;
                        cboChartOfAccountId_ItemsRequested(cboChartOfAccountId, args);
                        cboChartOfAccountId_SelectedIndexChanged(cboChartOfAccountId, new RadComboBoxSelectedIndexChangedEventArgs(coa.ChartOfAccountCode, string.Empty, coa.ChartOfAccountId.ToString(), string.Empty));
                    }
                    SubLedgerId = guar.SubledgerIdTemporary ?? 0;
                }
                return;
            }
            txtPaymentNo.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PaymentNo);
            txtAmount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.Amount));
            chkIsPpn.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.IsPpn));
            chkIsPph.Checked = Convert.ToBoolean(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.IsPph));
            txtPpnPercentage.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PpnPercentage));
            txtPphPercentage.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PphPercentage));
            txtPpnAmount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PpnAmount));
            txtPphAmount.Value =
                Convert.ToDouble(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PphAmount));
            rbPph.SelectedValue = (txtPphAmount.Value ?? 0) >= 0 ? "+" : "-";
            cboSRPph.SelectedValue = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.SRPph);
            txtNotes.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.Notes);

            var patid = Convert.ToString(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PatientID));
            if (patt.LoadByPrimaryKey(patid))
            {

                var args = new RadComboBoxItemsRequestedEventArgs();
                args.Text = patt.PatientID;
                cboPatientID_ItemsRequested(cboPatientID, args);
                cboPatientID_SelectedIndexChanged(cboPatientID, new RadComboBoxSelectedIndexChangedEventArgs(patt.PatientName, string.Empty, patt.PatientID.ToString(), string.Empty));
            }

            txtPatientName.Text = (String)DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.PatientName);
            //ChartOfAccountCodeText = string.Format("{0} - {1}", DataBinder.Eval(DataItem, "ChartOfAccountCode"), DataBinder.Eval(DataItem, "ChartOfAccountName"));

            var coaid = Convert.ToInt32(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.ChartOfAccountId));
            if (coa.LoadByPrimaryKey(coaid))
            {
                //ChartOfAccountCodeText = string.Format("{0} - {1}", coa.ChartOfAccountCode, coa.ChartOfAccountName);
                var args = new RadComboBoxItemsRequestedEventArgs();
                args.Text = coa.ChartOfAccountCode;
                cboChartOfAccountId_ItemsRequested(cboChartOfAccountId, args);
                cboChartOfAccountId_SelectedIndexChanged(cboChartOfAccountId, new RadComboBoxSelectedIndexChangedEventArgs(coa.ChartOfAccountCode, string.Empty, coa.ChartOfAccountId.ToString(), string.Empty));
            }

            SubLedgerId = Convert.ToInt16(DataBinder.Eval(DataItem, InvoicesItemMetadata.ColumnNames.SubLedgerId));
        }

        protected void customValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if (txtAmount.Value <= 0)
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).ErrorMessage = string.Format("Amount Must Bigger than 0");
            //    return;
            //}

            var coa = new ChartOfAccounts();
            if (coa.LoadByPrimaryKey(ChartOfAccountId))
            {
                if (coa.SubLedgerId > 0)
                {
                    if (!string.IsNullOrEmpty(cboSubledgerId.SelectedValue))
                    {
                        var sub = new SubLedgers();
                        if (!sub.LoadByPrimaryKey(cboSubledgerId.SelectedValue.ToInt()))
                        {
                            args.IsValid = false;
                            ((CustomValidator)source).ErrorMessage = string.Format("Invalid Subledger.");
                            return;
                        }
                    }
                    else
                    {
                        args.IsValid = false;
                        ((CustomValidator)source).ErrorMessage = string.Format("Subledger is required.");
                        return;
                    }
                }
            }
            else
            {
                args.IsValid = false;
                ((CustomValidator)source).ErrorMessage = string.Format("Invalid Chart Of Account.");
                return;
            }
        }


        #region Properties for return entry value
        public String PaymentNo
        {
            get { return txtPaymentNo.Text; }
        }
        public Decimal Amount
        {
            get { return Convert.ToDecimal(txtAmount.Value); }
        }
        public Boolean IsPpn
        {
            get { return chkIsPpn.Checked; }
        }
        public Boolean IsPph
        {
            get { return chkIsPph.Checked; }
        }
        public Decimal PpnPercentage
        {
            get { return Convert.ToDecimal(txtPpnPercentage.Value); }
        }
        public Decimal PphPercentage
        {
            get { return Convert.ToDecimal(txtPphPercentage.Value); }
        }
        public Decimal PpnAmount
        {
            get { return Convert.ToDecimal(txtPpnAmount.Value); }
        }
        public Decimal PphAmount
        {
            get { return Convert.ToDecimal(txtPphAmount.Value); }
        }
        public String SRPph
        {
            get { return cboSRPph.SelectedValue; }
        }
        public String PphName
        {
            get { return cboSRPph.Text; }
        }
        public String Notes
        {
            get { return txtNotes.Text; }
        }
        public String PatientID
        {
            get { return cboPatientID.SelectedValue; }
        }

        public String MedicalNo
        {
            get { return txtMedicalNo.Text; }
        }

        public String PatientName
        {
            get { return txtPatientName.Text; }
        }
        public int ChartOfAccountId
        {
            get
            {
                //try
                //{
                //    int retVal = 0;
                //    var coa = new ChartOfAccountsQuery();
                //    coa.Where(coa.ChartOfAccountCode == cboChartOfAccountCode.Text.Split('-')[0]);
                //    coa.Select(coa.ChartOfAccountId);
                //    var dtb = coa.LoadDataTable();
                //    if (dtb != null && dtb.Rows.Count > 0)
                //        int.TryParse(dtb.Rows[0][0].ToString(), out retVal);
                //    return retVal;
                //}
                //catch (Exception)
                //{
                //    //nothing;
                //}
                //return 0;
                return System.Convert.ToInt32((cboChartOfAccountId.SelectedValue == "") ? "0": cboChartOfAccountId.SelectedValue);
            }
        }

        //public string ChartOfAccountCodeText
        //{
        //    get { return this.cboChartOfAccountId.Text; }
        //    //set { this.cboChartOfAccountCode.Text = value; }
        //}

        public int SubLedgerId
        {
            get
            {
                try
                {
                    int retVal = 0;
                    int groupID;
                    int coaID = ChartOfAccountId;
                    if (coaID == 0)
                    {
                        groupID = 0;
                    }
                    else
                    {
                        ChartOfAccounts coa = new ChartOfAccounts();
                        coa.LoadByPrimaryKey(coaID);
                        groupID = coa.SubLedgerId ?? 0;
                    }

                    var sub = new SubLedgersQuery();
                    sub.Where(sub.SubLedgerName == cboSubledgerId.Text.Split('-')[0], sub.GroupId == groupID);
                    sub.Select(sub.SubLedgerId);
                    var dtb = sub.LoadDataTable();
                    if (dtb != null && dtb.Rows.Count > 0)
                        int.TryParse(dtb.Rows[0][0].ToString(), out retVal);
                    return retVal;
                }
                catch (Exception)
                {
                    //nothing;
                }
                return 0;
            }
            set
            {
                var query = new SubLedgersQuery();
                query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
                query.Where(query.SubLedgerId == value);
                var dtb = query.LoadDataTable();
                cboSubledgerId.DataSource = dtb;
                cboSubledgerId.DataBind();
                ComboBox.SelectedValue(cboSubledgerId, value.ToString());
            }
        }
        #endregion


        #region ComboBox ChartOfAccountId
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
                            query.IsDetail == true,
                            query.IsActive == true
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboChartOfAccountId.DataSource = dtb;
            cboChartOfAccountId.DataBind();
        }

        protected void cboChartOfAccountId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ChartOfAccountCode"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["ChartOfAccountName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ChartOfAccountId"].ToString();
        }

        protected void cboChartOfAccountId_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboSubledgerId.Items.Clear();
            cboSubledgerId.Text = string.Empty;

            if (e.Value.ToString() != string.Empty)
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                if (!coa.LoadByPrimaryKey(Convert.ToInt32(e.Value)))
                {
                    cboChartOfAccountId.Text = string.Empty;
                    return;
                }
            }
            else
            {
                cboChartOfAccountId.Items.Clear();
                cboChartOfAccountId.Text = string.Empty;
                return;
            }
        }
        #endregion

        #region ComboBox SubledgerId
        protected void cboSubledgerId_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            int groupID;
            int coaID = ChartOfAccountId;
            if (coaID == 0)
            {
                groupID = 0;
            }
            else
            {
                ChartOfAccounts coa = new ChartOfAccounts();
                coa.LoadByPrimaryKey(coaID);
                groupID = coa.SubLedgerId ?? 0;
            }
            string searchTextContain = string.Format("%{0}%", e.Text);
            var query = new SubLedgersQuery();
            query.Select(query.SubLedgerId, query.SubLedgerName, query.Description);
            query.Where(query.GroupId == groupID);
            query.Where
                        (
                            query.Or
                            (
                                query.SubLedgerName.Like(searchTextContain),
                                query.Description.Like(searchTextContain)
                            )
                        );
            query.es.Top = 20;
            DataTable dtb = query.LoadDataTable();
            cboSubledgerId.DataSource = dtb;
            cboSubledgerId.DataBind();
        }

        protected void cboSubledgerId_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["SubLedgerName"].ToString() + " - " + ((DataRowView)e.Item.DataItem)["Description"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["SubLedgerId"].ToString();
        }
        #endregion

        private void PopulatePatientInformation(string patientID)
        {
            if (string.IsNullOrEmpty(patientID))
                return;

            var patient = new Patient();
            if (patient.LoadByPrimaryKey(patientID))
            {
                cboPatientID.SelectedValue = patient.PatientID;
                cboPatientID.Text = patient.PatientName;
                txtMedicalNo.Text = patient.MedicalNo;
                txtPatientName.Text = patient.PatientName;
            }
        }

        protected void btnCancel_ButtonClick(object sender, EventArgs e)
        {
            InvoicesItemCollection collitem = (InvoicesItemCollection)Session["PaymentItems" + Request.UserHostName];
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            cboSRPph_OnSelectedIndexChanged(null, null);
            chkIsPpn_CheckedChanged(null, null);
        }

        protected void rbPph_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSRPph_OnSelectedIndexChanged(sender, e);
        }

        protected void cboSRPph_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var isAdd = rbPph.SelectedValue == "+";

            var pph = new AppStandardReferenceItem();
            if (pph.LoadByPrimaryKey("Pph", cboSRPph.SelectedValue))
            {
                if (pph.ReferenceID == "Progresif")
                {
                    txtPphPercentage.Value = 0;

                    decimal pphAmt = InvoiceSupplier.PphProgresif(Convert.ToDecimal(txtAmount.Value));
                    txtPphAmount.Value = Convert.ToDouble(pphAmt);
                }
                else
                {
                    chkIsPph.Checked = true;
                    txtPphPercentage.Value = Convert.ToDouble(pph.ReferenceID);
                    txtPphAmount.Value = txtAmount.Value * (txtPphPercentage.Value / 100);
                }
                if (!isAdd) txtPphAmount.Value = txtPphAmount.Value * -1;
            }
            else
            {
                chkIsPph.Checked = false;
                txtPphPercentage.Value = 0;
                txtPphAmount.Value = 0;
            }
        }

        protected void chkIsPpn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsPpn.Checked)
            {
                txtPpnPercentage.Value = AppSession.Parameter.TaxPercentage;
                txtPpnAmount.Value = txtAmount.Value * (txtPpnPercentage.Value / 100);
            }
            else
            {
                txtPpnPercentage.Value = 0;
                txtPpnAmount.Value = 0;
            }
        }


        protected void cboPatientID_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PopulatePatientInformation(e.Value);
        }

        protected void cboPatientID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var dtbPatient = (new PatientCollection()).NonPatientRegisterAble(e.Text, string.Empty, string.Empty, string.Empty, 10);
            cboPatientID.DataSource = dtbPatient;
            cboPatientID.DataBind();
        }

        protected void cboPatientID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["PatientName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["PatientID"].ToString();
        }
    }
}