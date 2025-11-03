using System;
using System.Data;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using Temiang.Dal.Interfaces;

namespace Temiang.Avicenna.Module.Payroll.Process
{
    public partial class WageTransactionProcessList : BasePage
    {
        private bool _isHideEmptySearchMessage = false;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _isHideEmptySearchMessage = false;
            if (Page.IsPostBack)
            {
                if (Request["__EVENTTARGET"].Contains("grd") &&
                    Request["__EVENTARGUMENT"].Contains("rebind"))
                {
                    _isHideEmptySearchMessage = true;
                }
            }
            ProgramID = AppConstant.Program.ProcessClosingWageTransaction;

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
            if (!IsPostBack && !IsListLoadRecordOnInit)
            {
                grdList.DataSource = new String[] { };
                return;
            }

            var grd = (RadGrid)source;
            var dataSource = ClosingWageTransactions;
            if (dataSource == null)
                grd.DataSource = new String[] { }; // Clear rows
            else
            {
                if (!e.IsFromDetailTable)
                    grd.DataSource = dataSource;
            }          
        }

        private DataTable ClosingWageTransactions
        {
            get
            {
                var isEmptyFilter = string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue);
                if (!ValidateSearch(isEmptyFilter, "Process & Closing Wage")) return null;

                var query = new ClosingWageTransactionQuery("a");
                var period = new PayrollPeriodQuery("b");
                var sequent = new AppStandardReferenceItemQuery("c");

                query.InnerJoin(period).On(query.PayrollPeriodID == period.PayrollPeriodID);
                query.InnerJoin(sequent).On
                      (
                          period.SRPaySequent == sequent.ItemID &
                          sequent.StandardReferenceID == "PaySequent"
                      );

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(
                                query.PayrollPeriodID,
                                period.PayrollPeriodName,
                                sequent.ItemName.As("PaySequentName"),
                                period.StartDate,
                                period.EndDate,
                                period.PayDate,
                                query.IsClosed,
                                query.LastUpdateDateTime,
                                query.LastUpdateByUserID,
                                "<CAST(0 AS BIT) AS 'IsProcessed'>"
                            );

                if (!string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    query.Where(query.PayrollPeriodID == Convert.ToInt32(cboPayrollPeriodID.SelectedValue));

                if (AppSession.Parameter.HealthcareInitial == "RSCH")
                {
                    if (string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    {
                        query.Where(period.SPTYear == DateTime.Now.Year);
                        query.OrderBy(period.SPTMonth.Ascending);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(cboPayrollPeriodID.SelectedValue))
                    {
                        string searchTextContain = string.Format("%{0}%", DateTime.Now.Year.ToString());
                        query.Where(period.PayrollPeriodName.Like(searchTextContain), period.SPTMonth <= DateTime.Now.Month);
                        query.OrderBy(period.PayrollPeriodCode.Descending);
                    }
                }

                DataTable dtb = query.LoadDataTable();

                foreach (DataRow row in dtb.Rows)
                {
                    var c = new WageTransactionCollection();
                    c.Query.Where(c.Query.PayrollPeriodID == Convert.ToInt32(row["PayrollPeriodID"]), c.Query.WageProcessTypeID == 1);
                    c.LoadAll();

                    row["IsProcessed"] = c.Count > 0;
                }
                dtb.AcceptChanges();

                return dtb;
            }
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void cboPayrollPeriodID_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.PayrollPeriodItemsRequested((RadComboBox)o, e.Text);
        }

        protected void cboPayrollPeriodID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            ComboBox.PayrollPeriodItemDataBound(e);
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "rebind")
            {
                pnlInfo.Visible = false;
                grdList.Rebind();
            }
            else if (eventArgument.Contains("reprocessing"))
            {
                var msg = string.Empty;

                int payrollPeriodId = Convert.ToInt32(eventArgument.Split('|')[1]);
                Int64 wageProcessTypeId = 1;

                DateTime transactionDate = DateTime.Now;
                string userId = AppSession.UserLogin.UserID;

                int result = ClosingWageTransaction.ProcessWageTransaction(payrollPeriodId, wageProcessTypeId, transactionDate, userId, -1);
                if (result != 0)
                {
                    if (result == -1)
                        msg = "Closing failed please try again or contact your administrator.";
                    else if (result == -2)
                        msg = "All transaction must be posted first";
                    else if (result == -3)
                        msg = "This periode has been closed";
                }
                if (string.IsNullOrEmpty(msg))
                {
                    var x = new ClosingWageTransaction();
                    x.LoadByPrimaryKey(Convert.ToInt32(eventArgument.Split('|')[1]));
                    x.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    x.LastUpdateDateTime = DateTime.Now;
                    x.Save();

                    var p = new PayrollPeriod();
                    p.LoadByPrimaryKey(Convert.ToInt32(eventArgument.Split('|')[1]));

                    msg = eventArgument.Split('|')[2] + " wage transaction for payroll period : " + p.PayrollPeriodName + " succeed.";
                }

                pnlInfo.Visible = true;
                lblInfo.Text = msg;

                grdList.Rebind();
            }
            else if (eventArgument.Contains("closing"))
            {
                var msg = string.Empty;

                var x = new ClosingWageTransaction();
                x.LoadByPrimaryKey(Convert.ToInt32(eventArgument.Split('|')[1]));

                var isCreateJurnal = false;
                if (AppSession.Parameter.acc_IsAutoJournalPayroll)
                {
                    isCreateJurnal = true;

                    var pp = new PayrollPeriod();
                    pp.LoadByPrimaryKey(x.PayrollPeriodID.ToInt());

                    var isClosingPeriod = PostingStatus.IsPeriodeClosed(pp.PayDate ?? DateTime.Now);
                    if (isClosingPeriod)
                    {
                        msg = "Financial statements for period: " +
                                           string.Format("{0:MMMM-yyyy}", pp.PayDate ?? DateTime.Now) +
                                           " have been closed. Please contact the authorities.";
                    }
                }

                if (msg == string.Empty)
                {
                    x.IsClosed = true;
                    x.LastUpdateByUserID = AppSession.UserLogin.UserID;
                    x.LastUpdateDateTime = DateTime.Now;
                    x.Save();

                    //int tax = ClosingWageTransaction.ProcessWageTax(x.PayrollPeriodID.ToInt(), 1, AppSession.UserLogin.UserID, -1);

                    if (isCreateJurnal)
                    {
                        int? journalId = JournalTransactions.AddNewPayrollJournal(x.PayrollPeriodID.ToInt(), AppSession.UserLogin.UserID, -1);
                    }

                    pnlInfo.Visible = false;
                }
                else
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = msg;
                }
                
                grdList.Rebind();
            }
        }
    }
}
