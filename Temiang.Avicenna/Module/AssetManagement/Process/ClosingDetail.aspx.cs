using System;
using System.Collections.Generic;
using System.Linq;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.AssetManagement.Process
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

        #region Page Event & Initialize
        protected void Page_Init(object sender, EventArgs e)
        {
            // Url Search & List
            ToolBarMenuSearch.Enabled = false;
            UrlPageSearch = "ClosingSearch.aspx";
            UrlPageList = "ClosingList.aspx";

            ProgramID = AppConstant.Program.ASSET_CLOSING;

            //StandardReference Initialize
            if (!IsPostBack)
            {
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

        protected override void OnInitializeAjaxManagerSettingsCollection(AjaxSettingsCollection ajax)
        {
        }
        #endregion

        #region Toolbar Menu Event
        protected override void OnMenuNewClick()
        {
            OnPopulateEntryControl(new AssetPostingStatus());
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

            if (this.PostingId != 0)
            {
                AssetPostingStatus entity = AssetPostingStatus.Get(this.PostingId);
                if (entity == null)
                {
                    args.MessageText = AppConstant.Message.RecordNotExist;
                    args.IsCancel = true;
                    return;
                }

                selectedMonth = entity.Month;
                selectedYear = entity.Year;

                int.TryParse(selectedMonth, out selectedMonthINT);
                int.TryParse(selectedYear, out selectedYearINT);
            }
            else // user key-in the periode manually
            {
                if (selectedYear.Length != 4 || !int.TryParse(selectedYear, out selectedYearINT))
                {
                    args.MessageText = "Invalid Year.";
                    args.IsCancel = true;
                    return;
                }

                if (selectedMonthINT == 0)
                {
                    if (!int.TryParse(selectedMonth, out selectedMonthINT))
                    {
                        args.MessageText = "Invalid Month.";
                        args.IsCancel = true;
                        return;
                    }
                }

                var apsColl = new AssetPostingStatusCollection();
                apsColl.Query.Where(apsColl.Query.Month == selectedMonth, apsColl.Query.Year == selectedYear);
                apsColl.LoadAll();
                if (apsColl.Count > 0)
                {
                    args.MessageText = "The automatic journal with this period already exists.";
                    args.IsCancel = true;
                    return;
                }
            }

            var postingDate = new DateTime(selectedYearINT, selectedMonthINT, 1).AddMonths(1).AddDays(-1);

            var isClosingPeriod = PostingStatus.IsPeriodeClosed(postingDate);
            if (isClosingPeriod)
            {
                args.MessageText = "Financial statements for period: " +
                                   string.Format("{0:MMMM-yyyy}", postingDate) +
                                   " have been closed. Please contact the authorities.";
                args.IsCancel = true;
                return;
            }

            int result = AssetDepreciationPost.GenerateDepreciationJournal(this.PostingId, this.chkIsPostingFinal.Checked, postingDate, AppSession.UserLogin.UserID);
            if (result != 0)
            {
                if (result == -1)
                    args.MessageText = "Closing failed please try again or contact your administrator.";
                else if (result == -2)
                    args.MessageText = "All transaction must be posted first.";
                else if (result == -3)
                    args.MessageText = "This periode has been mark as posted.";


                args.IsCancel = true;
                return;
            }

            Response.Redirect("ClosingList.aspx");
        }
        protected override void OnMenuSaveEditClick(ValidateArgs args)
        {
            if (this.PostingId != 0)
            {
                AssetPostingStatus entity = AssetPostingStatus.Get(this.PostingId);

                //db:05-10-2023 status close dihilangkan, tetap bisa diproses selama belum tutup periode buku
                //if (!entity.IsEnabled.Value)
                {
                    ProcessClosing(args);
                }
                //else
                //{
                //    args.MessageText = "This periode has been mark as posted.";
                //    args.IsCancel = true;
                //    return;
                //}
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
            auditLogFilter.TableName = "AssetPostingStatus";
        }

        protected override void OnBeforeMenuEditClick(ValidateArgs args)
        {
            if (this.PostingId != 0)
            {
                AssetPostingStatus entity = AssetPostingStatus.Get(this.PostingId);
                if (entity.IsEnabled.Value)
                {
                    args.MessageText = "This periode has been mark as posted.";
                    args.IsCancel = true;
                    return;
                }
            }
        }
        #endregion

        #region ToolBar Menu Support
        protected override void OnDataModeChanged(AppEnum.DataMode oldVal, AppEnum.DataMode newVal)
        {
            //TODO: Set status entry control
            this.ddlMonth.Enabled = (newVal == AppEnum.DataMode.New);
            this.txtYear.Enabled = (newVal == AppEnum.DataMode.New);
            //this.chkIsPostingFinal.Enabled = true;
        }

        protected override void OnPopulateEntryControl(params string[] parameters)
        {
            AssetPostingStatus entity = new AssetPostingStatus();
            if (parameters.Length > 0)
            {
                string id = parameters[0];

                if (!parameters[0].Equals(string.Empty))
                    entity = AssetPostingStatus.Get(Convert.ToInt32(id));
            }
            else
            {
                entity = AssetPostingStatus.Get(this.PostingId);
            }
            OnPopulateEntryControl(entity);
        }

        protected override void OnPopulateEntryControl(esEntity e)
        {
            if (this.PostingId != 0)
            {
                AssetPostingStatus entity = (AssetPostingStatus)e;
                ddlMonth.SelectedValue = entity.Month;
                txtYear.Text = entity.Year;
                chkIsPostingFinal.Checked = entity.IsEnabled ?? false;
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