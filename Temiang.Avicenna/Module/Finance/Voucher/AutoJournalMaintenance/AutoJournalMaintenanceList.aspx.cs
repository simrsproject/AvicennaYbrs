using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using Temiang.Dal.Interfaces;
using Temiang.Avicenna.BusinessObject.Reference;

namespace Temiang.Avicenna.Module.Finance.Voucher.AutoJournalMaintenance
{
    public partial class AutoJournalMaintenanceList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            ProgramID = AppConstant.Program.VOUCHER_AUTO_MAINTENANCE;
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!txtDateFrom.SelectedDate.HasValue)
            {
                txtDateFrom.SelectedDate = DateTime.Now.Date;
            }
            if (!txtDateTo.SelectedDate.HasValue)
            {
                txtDateTo.SelectedDate = DateTime.Now.Date;
            }

            grdList.DataSource = UnJournal;
        }

        private DataTable UnJournal
        {
            get
            {
                var j = new JournalTransactionsCollection();
                return j.GetUnJournal(string.Empty, txtDateFrom.SelectedDate.Value, txtDateTo.SelectedDate.Value);
            }
        }

        private DataTable UnCashEntriedDownPayment
        {
            get
            {
                var j = new JournalTransactionsCollection();
                return j.GetUnCashEntriedDownPayment();
            }
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            foreach (CheckBox chkBox in grdList.MasterTableView.Items.Cast<GridDataItem>().Select(dataItem => (CheckBox)dataItem.FindControl("detailChkbox")).Where(chkBox => chkBox.Visible))
            {
                chkBox.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            pnlInfo.Visible = false;

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument == "rejournal")
            {
                if (PostingStatus.IsPeriodeClosed(txtDateFrom.SelectedDate.Value, txtDateTo.SelectedDate.Value))
                {
                    pnlInfo.Visible = true;
                    lblInfo.Text = "Financial statement for selected period has been closed";
                    return;
                }

                var items = grdList.MasterTableView.Items.Cast<GridDataItem>().Where(dataItem => ((CheckBox)dataItem.FindControl("detailChkbox")).Checked)
                    .Select(dataItem => new
                    {
                        TransactionNo = dataItem["TransactionNo"].Text,
                        JournalCode = dataItem["JournalCode"].Text.Replace("&nbsp;", string.Empty),
                        RefferenceNumber = dataItem["RefferenceNumber"].Text,
                        IsReverse = System.Convert.ToBoolean(System.Convert.ToInt32(dataItem["jReverse"].Text.Replace("&nbsp;", string.Empty)))
                    });
                foreach (var item in items)
                {
                    if (item.JournalCode == "DPCashEntry")
                    {
                        var JournalID = Int32.Parse(item.RefferenceNumber);
                        VoucherEntry.VoucherEntryDetail.DownPaymentAddCashEntry(item.TransactionNo, JournalID);
                    }
                    else if (item.JournalCode == "DPCashEntryReturn")
                    {
                        var JournalID = Int32.Parse(item.RefferenceNumber);
                        VoucherEntry.VoucherEntryDetail.DownPaymentReturnAddCashEntry(item.TransactionNo, JournalID);
                    }
                    else
                    {
                        // hanya untuk jurnal yang BELUM PERNAH tercreate
                        var jColl = new JournalTransactionsCollection();
                        jColl.Query.Where(jColl.Query.RefferenceNumber == item.TransactionNo);
                        if (item.IsReverse)
                        {
                            jColl.Query.Where(jColl.Query.JournalIdRefference.IsNotNull());
                        }
                        else
                        {
                            jColl.Query.Where(jColl.Query.JournalIdRefference.IsNull());
                        }
                        if (!jColl.LoadAll())
                        {
                            VoucherEntry.VoucherRejournalDialog.ReJournal(0, item.TransactionNo, item.JournalCode, item.IsReverse);
                        }
                    }
                }
            }

            grdList.Rebind();
        }
        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            grdList.Rebind();
        }
    }
}