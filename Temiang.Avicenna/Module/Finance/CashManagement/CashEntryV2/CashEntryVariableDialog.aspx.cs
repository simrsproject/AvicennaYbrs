using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Temiang.Avicenna.Common;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using System.Data;

namespace Temiang.Avicenna.Module.Finance.CashManagement.CashEntryV2
{
    public partial class CashEntryVariableDialog : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;

            //Helper.SetupComboBox(txtCashList);
            //cboTemplate.ItemsRequested += cboTemplate_ItemsRequested;
            cboTemplate.ItemDataBound += cboTemplate_ItemDataBound;
            cboTemplate.TextChanged += cboTemplate_TextChanged;

            txtAmount.Value = 0;

            if (!Page.IsPostBack) {
                var tColl = new CashTransactionTemplateCollection();
                tColl.Query.Where(tColl.Query.IsActive == true);
                tColl.LoadAll();
                cboTemplate.DataSource = tColl;
                cboTemplate.DataBind();
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            var bankNb = Request.QueryString["nb"];
            double tAmt = txtAmount.Value ?? 0;
            double tFixed = 0;
            double tPctg = 0;
            foreach (GridDataItem gdi in grdListItem.Items)
            {
                var txtAmountFixed = gdi["AmountFixed"].FindControl("txtAmountFixed") as RadNumericTextBox;
                tFixed += txtAmountFixed.Value ?? 0;
            }
            double tVarAloc = tAmt - tFixed;
            double tVar = 0;
            foreach (GridDataItem gdi in grdListItem.Items)
            {
                var pctg = CashTransactionTemplateDetails.AsEnumerable()
                    .Where(r => r["TemplateDetailId"].ToString() == gdi.GetDataKeyValue("TemplateDetailId").ToString())
                    .First()["AmountVariablePercentage"].ToDecimal().ToDouble();
                //pctg.valu
                var txtAmountVariableCalculated = gdi["AmountVariableCalculated"].FindControl("txtAmountVariableCalculated") as RadNumericTextBox;
                var tv = pctg / 100 * tVarAloc;
                txtAmountVariableCalculated.Value = tv;
                tVar += tv;

                tPctg += pctg;
            }

            lblPctg.Text = tPctg.ToString("N2");
            lblVariable.Text = tVar.ToString("N2");
            lblFixed.Text = tFixed.ToString("N2");
            lblTotal.Text = (tVar + tFixed).ToString("N2");
        }

        //void cboTemplate_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    RadComboBox box = o as RadComboBox;
        //    string val = e.Text;
        //    if (val.Length != 0)
        //    {
        //        box.Items.Clear();
        //        CashTransactionListCollection coll = CashTransactionList.GetLike(val, false);

        //        box.DataSource = coll;
        //        box.DataBind();
        //    }
        //}

        void cboTemplate_TextChanged(object sender, EventArgs e)
        {
            CashTransactionTemplateDetails = null;
            grdListItem.Rebind();
        }

        void cboTemplate_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var entity = ((CashTransactionTemplate)e.Item.DataItem);
            e.Item.Attributes["TemplateName"] = entity.TemplateName;
        }

        private DataTable CashTransactionTemplateDetails
        {
            get
            {
                object obj = ViewState["vsCashTransactionTemplateDetail"];
                if (obj != null && Page.IsPostBack) return ((DataTable)(obj));

                int templId = string.IsNullOrEmpty(cboTemplate.SelectedValue) ? -1 : System.Convert.ToInt32(cboTemplate.SelectedValue);

                CashTransactionTemplateDetailQuery query = new CashTransactionTemplateDetailQuery("a");
                ChartOfAccountsQuery coa = new ChartOfAccountsQuery("b");
                SubLedgersQuery sub = new SubLedgersQuery("c");

                query.Select(query,
                    coa.ChartOfAccountCode,
                    coa.ChartOfAccountName,
                    coa.NormalBalance,
                    sub.SubLedgerName, sub.Description.As("SubLedgerDesc"),
                    "<0 AS AmountVariableCalculated>"
                    );
                query.InnerJoin(coa).On(query.ChartOfAccountId == coa.ChartOfAccountId);
                query.LeftJoin(sub).On(query.SubLedgerId == sub.SubLedgerId);
                query.Where(query.TemplateId == templId);
                query.OrderBy(coa.ChartOfAccountCode.Ascending, sub.Description.Ascending);

                var dtb = query.LoadDataTable();

                ViewState["vsCashTransactionTemplateDetail"] = dtb;

                return dtb;
            }
            set { ViewState["vsCashTransactionTemplateDetail"] = value; }
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdListItem.DataSource = CashTransactionTemplateDetails;
        }

        public override bool OnButtonOkClicked()
        {
            if (!IsAmountValid()) {
                return false;
            }

            var coll = (CashTransactionDetailCollection)Session["collCashTransactionDetail"];

            foreach (GridDataItem dataItem in grdListItem.MasterTableView.Items)
            {
                decimal debitAmount = 0;
                decimal creditAmount = 0;
                decimal amountFixed = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmountFixed")).Value);
                decimal amountVar = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmountVariableCalculated")).Value);

                decimal amount = amountFixed + amountVar;

                string coaNb = dataItem["NormalBalance"].Text.ToUpper();

                if (Request.QueryString["nb"] == "K")
                {
                    if (coaNb == "D")
                    {
                        debitAmount = amount > 0 ? amount : 0;
                        creditAmount = amount > 0 ? 0 : (amount * -1);
                        amount = amount;
                    }
                    else {
                        creditAmount = amount > 0 ? amount : 0;
                        debitAmount = amount > 0 ? 0 : (amount * -1);
                        amount = -amount;
                    }
                }
                else
                {
                    if (coaNb == "K")
                    {
                        debitAmount = amount > 0 ? amount : 0;
                        creditAmount = amount > 0 ? 0 : (amount * -1);
                        amount = amount;
                    }
                    else
                    {
                        creditAmount = amount > 0 ? amount : 0;
                        debitAmount = amount > 0 ? 0 : (amount * -1);
                        amount = -amount;
                    }
                }

                var detail = coll.AddNew();
                detail.TransactionId = Request.QueryString["id"].ToInt();
                detail.ChartOfAccountId = dataItem["ChartOfAccountId"].Text.ToInt();
                detail.Debit = debitAmount;
                detail.Credit = creditAmount;
                detail.Amount = amount;
                detail.Description = cboTemplate.Text;
                detail.SubLedgerId = dataItem["SubLedgerId"].Text.ToInt();
                detail.CostCenterId = 0;
                detail.ListID = dataItem.GetDataKeyValue("TemplateDetailId").ToString();
            }

            return true;
        }

        private bool IsAmountValid() {
            decimal tAmount = 0;
            foreach (GridDataItem dataItem in grdListItem.MasterTableView.Items)
            {
                decimal amountFixed = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmountFixed")).Value);
                decimal amountVar = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtAmountVariableCalculated")).Value);

                tAmount += amountFixed + amountVar;
            }

            if (tAmount != System.Convert.ToDecimal(txtAmount.Value ?? 0)) {
                ShowInformationHeader("Invalid Amount");
                return false;
            }
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.init = 'rebind'";
        }
    }
}
