using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Finance.Voucher.Closing
{
    public partial class ClosingDetail : BasePageDetail
    {
        protected int PostingId
        {
            get
            {
                string tmpVal = this.lblPostingId.Text;
                int ret = 0;
                int.TryParse(tmpVal, out ret);
                return ret;
            }
            set
            {
                this.lblPostingId.Text = value.ToString();
            }
        }


        protected override void OnMenuEditClick()
        {
            if (chkIsPostingFinal.Checked)
                chkIsPostingFinal.Enabled = (UserAccess.IsUnApprovalAble);
        }

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            ToolBarMenuSearch.Enabled = false;
            UrlPageSearch = "ClosingSearch.aspx";
            UrlPageList = "ClosingList.aspx";

            ProgramID = AppConstant.Program.CLOSING_PERIODE;

            //StandardReference Initialize
            if (!IsPostBack)
            {
                
                //var users = new JournalGroupUserCollection();
                //users.Query.Where(users.Query.UserID == AppSession.UserLogin.UserID);
                //if (users.Query.Load())
                //{
                var groups = new JournalGroupCollection();
                //groups.Query.Where(groups.Query.JournalGroupID.In(users.Select(u => u.JournalGroupID)), groups.Query.IsActive == true);
                groups.Query.Where(groups.Query.IsActive == true);
                groups.Query.OrderBy(groups.Query.JournalGroupID.Ascending);
                groups.Query.Load();

                foreach (var group in groups)
                {
                    cboJournalGroup.Items.Add(new RadComboBoxItem(group.JournalGroupName, group.JournalGroupID.ToString()));
                }
                //}
            }

            //PopUp Search
            if (!IsCallback)
            {
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string q = Request.QueryString["ivd"];
                int val = 0;
                if (int.TryParse(q, out val))
                    this.PostingId = val;

                for (int i = 1; i < 13; i++)
                    ddlMonth.Items.Add(new RadComboBoxItem(Helper.MonthName(i), String.Format("{0:00}", i)));
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            ToolBarMenuApproval.Visible = false;
            ToolBarMenuUnApproval.Visible = false;
        }

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new Temiang.Avicenna.BusinessObject.PostingStatus());
        }

        protected override void OnMenuDeleteClick(ValidateArgs args)
        {
        }

        protected override void OnMenuSaveNewClick(ValidateArgs args)
        {
            ProcessClosing(args);
        }

        private void ProcessClosing(ValidateArgs args)
        {
            string selectedMonth = ddlMonth.SelectedValue;
            string selectedYear = txtYear.Text.Trim();

            int selectedYearINT = 0;
            int selectedMonthINT = 0;
            int selectedDayINT = 0;

            if (this.PostingId != 0)
            {
                PostingStatus entity = PostingStatus.Get(this.PostingId);
                if (entity == null)
                {
                    args.MessageText = AppConstant.Message.RecordNotExist;
                    args.IsCancel = true;
                    return;
                }
                else
                {
                    selectedMonth = entity.Month;
                    selectedYear = entity.Year;

                    int.TryParse(selectedMonth, out selectedMonthINT);
                    int.TryParse(selectedYear, out selectedYearINT);

                    if (entity.PostingUntilDate.HasValue)
                    {
                        selectedDayINT = entity.PostingUntilDate.Value.Day;
                    }
                    else
                    {
                        var tmp = new DateTime(selectedYearINT, selectedMonthINT, 1).AddMonths(1).AddDays(-1);
                        selectedDayINT = tmp.Day;
                    }
                }
            }
            else // user key-in the periode manually
            {
                if (selectedYear.Length != 4 || !int.TryParse(selectedYear, out selectedYearINT))
                {
                    args.IsCancel = true;
                    return;
                }

                int.TryParse(selectedMonth, out selectedMonthINT);
                int.TryParse(selectedYear, out selectedYearINT);

                if (this.txtPostingUntilDate.SelectedDate.HasValue)
                {
                    selectedDayINT = this.txtPostingUntilDate.SelectedDate.Value.Day;
                }
                else
                {
                    var tmp = new DateTime(selectedYearINT, selectedMonthINT, 1).AddMonths(1).AddDays(-1);
                    selectedDayINT = tmp.Day;
                }

                var psColl = new PostingStatusCollection();
                psColl.Query.Where(psColl.Query.Month == selectedMonth, psColl.Query.Year == selectedYear);
                psColl.LoadAll();
                if (psColl.Count > 0)
                {
                    args.MessageText = "Closing period already exists.";
                    args.IsCancel = true;
                    return;
                }
            }


            var flag = new AppParameter();
            // if( flag.LoadByPrimaryKey("ClosingJournalWithoutAllApproved") == false )
            if (AppParameter.GetParameterValue(AppParameter.ParameterItem.ClosingJournalWithoutAllApproved) == "0")
            {
                int totalUnposted = JournalTransactions.CountUnPosted(selectedDayINT, selectedMonthINT, selectedYearINT, cboJournalGroup.SelectedValue.ToInt());
                if (totalUnposted > 0)
                {
                    args.MessageText = string.Format("There are {0} unapproved transaction(s). All journal transaction must be approved first before you can close a periode.", totalUnposted);
                    args.IsCancel = true;
                    return;
                }

            }


            int result = JournalTransactions.ProcessClosing(selectedDayINT, selectedMonth, selectedYear, chkIsFiscalYear.Checked, chkIsPostingFinal.Checked, AppSession.UserLogin.UserID, cboJournalGroup.SelectedValue.ToInt());
            if (result != 0)
            {
                if (result == -1)
                    args.MessageText = "Closing failed please try again or contact your administrator.";
                else if (result == -2)
                    args.MessageText = "all transaction must be posted first";
                else if (result == -3)
                    args.MessageText = "this periode has been closed";


                args.IsCancel = true;
                return;
            }

            //Response.Redirect("ClosingList.aspx");
            RadScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                "window.location='ClosingList.aspx';", true
            );
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (this.PostingId != 0)
            {
                PostingStatus entity = PostingStatus.Get(this.PostingId);
                if (entity.IsEnabled.Value)
                {
                    args.MessageText = "This periode has been closed";
                    args.IsCancel = true;
                    return;
                }
                entity.PostingUntilDate = this.txtPostingUntilDate.SelectedDate;
                entity.Save();

                ProcessClosing(args);
            }
        }

        protected override void OnMenuMoveNextClick(ValidateArgs args)
        {
            MoveRecord(true);
        }

        protected override void OnMenuMovePrevClick(ValidateArgs args)
        {
            MoveRecord(false);
        }

        protected override void OnMenuAuditLogClick(AuditLogFilter auditLogFilter)
        {
            //TODO: Betulkan PrimaryKeyData nya
            auditLogFilter.PrimaryKeyData = string.Format("PostingId='{0}'", this.PostingId);
            auditLogFilter.TableName = "PostingStatus";
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            //this.ddlMonth.Enabled = (newVal == DataMode.New);
            //this.txtYear.Enabled = (newVal == DataMode.New);
            //this.chkIsPostingFinal.Enabled = true;
            //this.chkIsFiscalYear.Enabled = true;
            chkIsPostingFinal.Enabled = base.IsUserApproveAble;
            chkIsFiscalYear.Enabled = chkIsPostingFinal.Enabled;

            ToolBarMenuApproval.Visible = false;
            ToolBarMenuUnApproval.Visible = false;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            PostingStatus entity = new PostingStatus();
            if (parameters.Length > 0)
            {
                string id = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity = PostingStatus.Get(Convert.ToInt32(id));
            }
            else
            {
                entity = PostingStatus.Get(this.PostingId);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity e)
        {
            if (this.PostingId != 0)
            {
                PostingStatus entity = (PostingStatus)e;
                ddlMonth.SelectedValue = entity.Month;
                txtYear.Text = entity.Year;

                chkIsPostingFinal.Checked = entity.IsEnabled ?? false;

                chkIsFiscalYear.Checked = entity.IsFiscalYear ?? false;
                cboJournalGroup.SelectedValue = Convert.ToString(entity.JournalGroupID ?? 0);

                if (entity.IsUncompleteAppr == true) txtMessage.Text = "There are unapproved transaction(s) in this period";
                try
                {
                    this.txtPostingUntilDate.MinDate = new DateTime(Convert.ToInt32(entity.Year), Convert.ToInt32(entity.Month), 1);
                    this.txtPostingUntilDate.MaxDate = new DateTime(Convert.ToInt32(entity.Year), Convert.ToInt32(entity.Month), 1).AddMonths(1).AddDays(-1);

                    txtPostingUntilDate.SelectedDate = entity.PostingUntilDate;


                }
                catch
                {
                }
            }
        }

        #endregion

        #region Private Method Standard
        private void SetEntityValue(ChartOfAccounts entity)
        {
            //entity.ChartOfAccountCode = txtChartOfAccountCode.Text;
            //entity.ChartOfAccountName = txtAccountName.Text;
            //entity.IsDetail = chkIsDetail.Checked;
            //entity.AccountGroup = int.Parse(cboAccountGroup.SelectedValue);
            //entity.AccountLevel = int.Parse(cboSRAcctLevel.SelectedValue);
            //entity.GeneralAccount = txtAccountGroup.Text;
            //entity.NormalBalance = cboNormalBalance.SelectedValue;
            //int subLedgerId = 0;
            //int.TryParse(cboSubLedger.SelectedValue, out subLedgerId);
            //entity.SubLedgerId = subLedgerId;
            //entity.LastUpdateDateTime = DateTime.Now;
            //entity.LastUpdateByUserID = AppSession.UserLogin.UserID;

            //entity.IsDocumenNumberEnabled = false; //not used
            //entity.TreeCode = string.Empty;

            //if (entity.es.IsAdded)
            //{
            //    entity.DateCreated = DateTime.Now;
            //    entity.CreatedBy = entity.LastUpdateByUserID;
            //}
        }

        private void SaveEntity(ChartOfAccounts entity)
        {
            using (esTransactionScope trans = new esTransactionScope())
            {
                entity.Save();
                //Commit if success, Rollback if failed
                trans.Complete();
            }
        }

        private void MoveRecord(bool isNextRecord)
        {
            //ChartOfAccountsQuery que = new ChartOfAccountsQuery();
            //que.es.Top = 1; // SELECT TOP 1 ..
            //if (isNextRecord)
            //{
            //    que.Where(que.ChartOfAccountCode > txtChartOfAccountCode.Text);
            //    que.OrderBy(que.ChartOfAccountCode.Ascending);
            //}
            //else
            //{
            //    que.Where(que.ChartOfAccountCode < txtChartOfAccountCode.Text);
            //    que.OrderBy(que.ChartOfAccountCode.Descending);
            //}
            //ChartOfAccounts entity = new ChartOfAccounts();
            //entity.Load(que);
            //OnPopulateEntryControl(entity);
        }
        #endregion
    }
}