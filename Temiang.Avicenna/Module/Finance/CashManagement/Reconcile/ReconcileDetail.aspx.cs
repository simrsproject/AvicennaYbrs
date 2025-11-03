using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Temiang.Avicenna.Module.Finance.CashManagement.Reconcile
{
    public partial class ReconcileDetail : BasePageDetail
    {
        protected string BankID
        {
            get
            {
                return Request.QueryString["bankid"];
            }
        }

        private double BankBalanceTemp
        {
            set
            {
                Session["BankBalanceTemp"] = value;
            }
            get
            {
                return Session["BankBalanceTemp"] == null ? 0 : (double)Session["BankBalanceTemp"];
            }
        }

        private DateTime BalanceDateTemp
        {
            set
            {
                Session["BalanceDateTemp"] = value;
            }
            get
            {
                return Session["BalanceDateTemp"] == null ? DateTime.Now : (DateTime)Session["BalanceDateTemp"];
            }
        }


        private bool ChkAllTransTmp
        {
            set
            {
                Session["ChkAllTransTmp"] = value;
            }
            get
            {
                return Session["ChkAllTransTmp"] == null ? false : (bool)Session["ChkAllTransTmp"];
            }
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            ToolBarMenuSearch.Enabled = false;
            //UrlPageSearch = "VoucherEntrySearch.aspx";

            ProgramID = AppConstant.Program.RECONCILE;
            UrlPageList = "ReconcileList.aspx";
            UrlPageSearch = "ReconcileSearch.aspx";

            if (!IsPostBack)
            {
                this.ToolBarMenuMoveNext.Enabled = false;
                this.ToolBarMenuMovePrev.Enabled = false;

                txtBankBalance.Value = BankBalanceTemp;
                txtDate.SelectedDate = BalanceDateTemp;
                chkAllTransaction.Checked = ChkAllTransTmp;

            }
        }

        protected void grdTransaction_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            int iRowCount; decimal Balance, ReconciledBalance;
            DateTime date = txtDate.SelectedDate.HasValue ?
                txtDate.SelectedDate.Value : DateTime.Now;
            var cbTrans = (new CashTransactionBalanceCollection()).GetCashTransactionByPage(
                BankID, date, !chkAllTransaction.Checked,
                ((grdTransaction.CurrentPageIndex * grdTransaction.PageSize) + 1),
                ((grdTransaction.CurrentPageIndex + 1) * grdTransaction.PageSize),
                out iRowCount, out Balance, out ReconciledBalance);

            txtBalance.Value = System.Convert.ToDouble(ReconciledBalance);
            txtCurrentBalance.Value = System.Convert.ToDouble(Balance);
            CalculateDifferent();

            grdTransaction.VirtualItemCount = iRowCount;
            grdTransaction.DataSource = cbTrans;
        }

        protected void grdTransaction_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "setStatus")
            {
                var cmd = e.CommandArgument.ToString().Split('|');
                if (cmd[0].Equals("page"))
                {
                    List<int> ids = new List<int>();
                    foreach (GridDataItem x in grdTransaction.MasterTableView.Items)
                    {
                        ids.Add(x.GetDataKeyValue("TransactionId").ToInt());
                    }
                    if (ids.Count > 0)
                    {
                        Reconcile(ids.ToArray(), true);
                    }
                }
                else
                {
                    int idTrans = System.Convert.ToInt32(cmd[0]);
                    switch (cmd[1])
                    {
                        case "Reconcile":
                            {
                                Reconcile(new int[] { idTrans }, true);
                                break;
                            }
                        case "UnReconcile":
                            {
                                Reconcile(new int[] { idTrans }, false);
                                break;
                            }
                    }
                }
                grdTransaction.Rebind();
            }
        }

        protected void txtBankBalance_TextChanged(object o, EventArgs e)
        {
            BankBalanceTemp = txtBankBalance.Value ?? 0;
            CalculateDifferent();
        }

        private void CalculateDifferent()
        {
            txtDifferent.Value = (txtBankBalance.Value ?? 0) - (txtBalance.Value ?? 0);
        }

        private void Reconcile(int[] ids, bool Reconcile)
        {
            if (ids.Count() == 0) return;
            CashTransactionCollection ctColl = new CashTransactionCollection();
            ctColl.Query.Where(ctColl.Query.TransactionId.In(ids));
            if (Reconcile)
            {
                ctColl.Query.Where(ctColl.Query.TransactionId.In(ids),
                ctColl.Query.IsCleared.Equal(false));
                ctColl.LoadAll();
            }
            else
            {
                ctColl.Query.Where(ctColl.Query.TransactionId.In(ids),
                ctColl.Query.IsCleared.Equal(true));
                ctColl.LoadAll();
            }

            ctColl.Reconcile(Reconcile, AppSession.UserLogin.UserID);
            ctColl.Save();
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdTransaction.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            //SaveValueToCookie();
            BalanceDateTemp = txtDate.SelectedDate ?? DateTime.Now;
            ChkAllTransTmp = chkAllTransaction.Checked;
            grdTransaction.Rebind();
        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            decimal Balance, ReconciledBalance;
            DateTime date = txtDate.SelectedDate.HasValue ?
                txtDate.SelectedDate.Value : DateTime.Now;

            var ctbColl = new CashTransactionBalanceCollection();
            var dtb = ctbColl.GetCashTransactionExportToExcel(
                BankID, date, !chkAllTransaction.Checked,
                out Balance, out ReconciledBalance
                ) as DataTable;

            txtBalance.Value = System.Convert.ToDouble(ReconciledBalance);
            txtCurrentBalance.Value = System.Convert.ToDouble(Balance);

            Session["ExportBankReconcile"] = dtb;
            string script = string.Format("function f(){{openDialog('{0}', {1}, {2}, {3});Sys.Application.remove_load(f);}}Sys.Application.add_load(f);",
                             "ExportToExcelDialog.aspx",
                             "true",
                             400,
                             400);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Export", script, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bank bank = new Bank();
                if (bank.LoadByPrimaryKey(BankID))
                {
                    lblBankName.Text = bank.BankName;
                }
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);

            EnableControl();
        }

        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            EnableControl();
        }
        #endregion

        private void EnableControl()
        {
            txtDate.Enabled = true;
            txtBankBalance.ReadOnly = false;
            chkAllTransaction.Enabled = true;
        }
    }
}