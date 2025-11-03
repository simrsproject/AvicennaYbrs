using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Web.UI.WebControls;
using System.Collections;
using System.Linq;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaunderedProcessDetailItemPicklist : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtReceivedDateFrom.SelectedDate = DateTime.Now.AddDays(-7);
                txtReceivedDateTo.SelectedDate = DateTime.Now;

                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["LaundryReceivedItems" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["LaundryReceivedItems" + Request.UserHostName];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["LaundryReceivedItems" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ReceivedNo"].Equals(dataItem.GetDataKeyValue("ReceivedNo").ToString()) && row["ReceivedSeqNo"].Equals(dataItem.GetDataKeyValue("ReceivedSeqNo").ToString()))
                    {
                        row["Qty"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value ?? 0;
                        
                        break;
                    }
                }

                ViewState["LaundryReceivedItems" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            DataTable dtb;
            if (!txtReceivedDateFrom.IsEmpty && !txtReceivedDateTo.IsEmpty)
                dtb =
                    (new LaunderedProcessItemCollection()).ItemOutstandingWithReceivedDate(
                        txtReceivedDateFrom.SelectedDate.Value.Date, txtReceivedDateTo.SelectedDate.Value.Date, cboFromServiceUnitID.SelectedValue);
            else
            {
                dtb =
                    (new LaunderedProcessItemCollection()).ItemOutstandingWithoutReceivedDate(
                        cboFromServiceUnitID.SelectedValue);
            }

            ViewState["LaundryReceivedItems" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private LaunderedProcessItem FindItemTransactionItem(string receivedNo, string receivedSeqNo)
        {
            var coll = (LaunderedProcessItemCollection)Session["collLaunderedProcessItem" + Request.UserHostName];
            foreach (LaunderedProcessItem entity in coll)
            {
                if (entity.ReceivedNo == receivedNo && entity.ReceivedSeqNo == receivedSeqNo)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (LaunderedProcessItemCollection)Session["collLaunderedProcessItem" + Request.UserHostName];
            string seqNo = coll.HasData ? coll[coll.Count - 1].ProcessSeqNo : "000";

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyOutstanding = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyOutstanding")).Value);
                decimal qtyProcess = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value);
                string itemName = dataItem["ItemName"].Text;

                if (qtyProcess <= 0) continue;

                if (qtyProcess > qtyOutstanding)
                {
                    ShowMessage(string.Format("Qty Processed for item {0} can not be greather than {1}", itemName, qtyOutstanding.ToString()));
                    return false;
                }

                LaunderedProcessItem entity = FindItemTransactionItem(dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.ProcessSeqNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.ProcessSeqNo;
                }
                entity.ReceivedNo = dataItem["ReceivedNo"].Text;
                entity.ReceivedSeqNo = dataItem["ReceivedSeqNo"].Text;
                entity.Qty = qtyProcess;
                entity.ConversionFactor = 1;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.SRItemUnit = dataItem["SRItemUnit"].Text;
                entity.ItemUnit = dataItem["ItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;
            }

            ViewState["LaundryReceivedItems" + Request.UserHostName] = null;
            return true;
        }

        public override string OnGetAdditionalJavaScriptCloseAndApply()
        {
            return "oWnd.argument.command = 'rebind'";
        }

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            bool selected = ((CheckBox)sender).Checked;

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                ((CheckBox)dataItem.FindControl("detailChkbox")).Checked = selected;
            }
        }

        protected void cboFromServiceUnitID_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            ComboBox.ServiceUnitItemsRequested((RadComboBox)sender, e.Text);
        }

        protected void cboFromServiceUnitID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ServiceUnitName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ServiceUnitID"].ToString();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            InitializeData();
            grdList.Rebind();
        }
    }
}
