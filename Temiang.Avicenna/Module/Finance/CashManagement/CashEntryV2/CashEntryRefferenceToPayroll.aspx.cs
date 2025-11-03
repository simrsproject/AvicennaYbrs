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
    public partial class CashEntryRefferenceToPayroll : BasePageDialog
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.CASH_ENTRY;
        }

        protected void grdListItem_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            var query = new ClosingWageTransactionQuery("a");
            var period = new PayrollPeriodQuery("b");
            var sequent = new AppStandardReferenceItemQuery("c");

            query.InnerJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
            query.InnerJoin(sequent).On
                  (
                      period.SRPaySequent == sequent.ItemID &
                      sequent.StandardReferenceID == "PaySequent"
                  );

            query.es.Top = 5;
            query.Select(
                            query.PayrollPeriodID,
                            period.PayrollPeriodCode,
                            period.PayrollPeriodName,
                            sequent.ItemName.As("PaySequentName"),
                            period.StartDate,
                            period.EndDate,
                            period.PayDate,
                            query.IsClosed,
                            query.LastUpdateDateTime,
                            query.LastUpdateByUserID
                        );

            if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                query.Where(query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));
            else
            {
                string searchTextContain = string.Format("%{0}%", DateTime.Now.Year.ToString());
                query.Where(period.PayrollPeriodName.Like(searchTextContain), period.SPTMonth <= DateTime.Now.Month);
                query.OrderBy(period.PayrollPeriodCode.Descending);
            }
            query.Where(query.IsClosed == true);
            query.OrderBy(period.PayDate.Descending);

            grdListItem.DataSource = query.LoadDataTable(); 
        }

        protected void grdDetail_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["CashEntryPayroll:Detail" + Request.UserHostName] != null)
                grdDetail.DataSource = ViewState["CashEntryPayroll:Detail" + Request.UserHostName];
        }

        protected void grdDetail_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdListItem.Rebind();
            grdDetail.DataSource = null;
            grdDetail.DataBind();
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            base.RaisePostBackEvent(source, eventArgument);
            if (!(source is RadGrid)) return;
            RadGrid grd = (RadGrid)source;
            switch (grd.ID.ToLower())
            {
                case "grddetail": // Populate Detail
                    string[] pars = eventArgument.Split('|');
                    string payrollPeriodCode = pars[0].Split(':')[1];
                    InitializeDataDetail(payrollPeriodCode);
                    break;
            }
        }

        private void InitializeDataDetail(string payrollPeriodCode)
        {
            DataTable dtb;

            var jtq = new JournalTransactionsQuery();
            jtq.Where(jtq.JournalType == JournalType.Payroll.ToString(), jtq.RefferenceNumber == "PYR" + payrollPeriodCode, jtq.IsPosted == true);
            jtq.OrderBy(jtq.JournalId.Descending);
            jtq.es.Top = 1;
            jtq.es.WithNoLock = true;
            var jtdtb = jtq.LoadDataTable();

            if (jtdtb.Rows.Count > 0)
            {
                var jt = new JournalTransactions();
                jt.Load(jtq);

                var jtdq = new JournalTransactionDetailsQuery("a");
                var coaq = new ChartOfAccountsQuery("b");
                jtdq.InnerJoin(coaq).On(coaq.ChartOfAccountId == jtdq.ChartOfAccountId);
                jtdq.Where(jtdq.JournalId == jt.JournalId, jtdq.Credit > 0);
                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    string searchTextContain = string.Format("%{0}%", txtDescription.Text);
                    jtdq.Where(jtdq.Description.Like(searchTextContain));
                }
                jtdq.Select(jtdq.ChartOfAccountId, coaq.ChartOfAccountCode, coaq.ChartOfAccountName, @"<SUM(a.Debit + a.Credit) AS 'Amount'>", jtdq.Description, @"<CAST(0 AS BIT) AS IsSelect>");
                jtdq.GroupBy(jtdq.ChartOfAccountId, coaq.ChartOfAccountCode, coaq.ChartOfAccountName, jtdq.Description);
                jtdq.OrderBy(coaq.ChartOfAccountCode.Ascending, jtdq.Description.Ascending);
                dtb = jtdq.LoadDataTable();
            }
            else
                dtb = null;

            ViewState["CashEntryPayroll:Detail" + Request.UserHostName] = dtb;

            grdDetail.DataSource = dtb;
            grdDetail.DataBind();
        }

        public override bool OnButtonOkClicked()
        {
            if (grdListItem.SelectedValue == null)
                return false;
            else
            {
                var dtb = (DataTable)ViewState["CashEntryPayroll:Detail" + Request.UserHostName];
                foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
                {
                    bool isSelect = ((CheckBox)dataItem.FindControl("chkIsSelect")).Checked;

                    int coaId = dataItem.GetDataKeyValue("ChartOfAccountId").ToInt();
                    string desciption = dataItem.GetDataKeyValue("Description").ToString();

                    foreach (DataRow row in dtb.Rows)
                    {
                        if (row["ChartOfAccountId"].ToInt().Equals(coaId) && row["Description"].ToString().Equals(desciption))
                        {
                            
                            row["IsSelect"] = isSelect;

                            break;
                        }
                    }

                    ViewState["CashEntryPayroll:Detail" + Request.UserHostName] = dtb;
                }

                Session["CashEntryPayroll:ItemSelected" + Request.UserHostName] = ViewState["CashEntryPayroll:Detail" + Request.UserHostName];

                return true;
            }
                
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            string str = "";
            if (grdListItem.SelectedValue == null)
                str = "'rebind'";
            else
                str = "'" + grdListItem.SelectedValue.ToString() + "||Payroll'";
                
            return "oWnd.argument.init = " + str;
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdDetail.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("chkIsSelect")).Checked = selected;
            }
        }
    }
}