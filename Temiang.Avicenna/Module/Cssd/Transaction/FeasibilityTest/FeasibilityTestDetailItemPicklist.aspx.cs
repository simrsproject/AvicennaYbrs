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

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class FeasibilityTestDetailItemPicklist : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_fts"] != null)
                grdList.DataSource = ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_fts"];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_fts"];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ReceivedNo"].Equals(dataItem.GetDataKeyValue("ReceivedNo").ToString()) && row["ReceivedSeqNo"].Equals(dataItem.GetDataKeyValue("ReceivedSeqNo").ToString()))
                    {
                        row["IsFeasibilityTestPassed"] = (dataItem.FindControl("chkIsFeasibilityTestPassed") as CheckBox).Checked;
                        row["IsBrokenInstrument"] = (dataItem.FindControl("chkIsBrokenInstrument") as CheckBox).Checked;
                        row["QtyReplacements"] = Convert.ToDecimal((dataItem.FindControl("txtQtyReplacements") as RadNumericTextBox).Value ?? 0);

                        break;
                    }
                }

                ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_fts"] = dtb;
            }
        }

        private void InitializeData()
        {
            DataTable dtb;
            if (!txtReceivedDate.IsEmpty)
                dtb = (new CssdFeasibilityTestItemCollection()).ItemOutstandingWithReceivedDate(txtReceivedDate.SelectedDate.Value.Date, cboFromServiceUnitID.SelectedValue);
            else
                dtb = (new CssdFeasibilityTestItemCollection()).ItemOutstandingWithoutReceivedDate(cboFromServiceUnitID.SelectedValue);

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_fts"] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private CssdFeasibilityTestItem FindItemTransactionItem(string receivedNo, string receivedSeqNo)
        {
            var coll = (CssdFeasibilityTestItemCollection)Session["collCssdFeasibilityTestItem" + Request.UserHostName];
            foreach (CssdFeasibilityTestItem entity in coll)
            {
                if (entity.ReceivedNo == receivedNo && entity.ReceivedSeqNo == receivedSeqNo)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (CssdFeasibilityTestItemCollection)Session["collCssdFeasibilityTestItem" + Request.UserHostName];
            string seqNo = coll.HasData ? coll[coll.Count - 1].FeasibilityTestSeqNo : "000";

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyProcess = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value);
                bool isFeasibilityTestPassed = ((CheckBox)dataItem.FindControl("chkIsFeasibilityTestPassed")).Checked;
                bool isBrokenInstrument = ((CheckBox)dataItem.FindControl("chkIsBrokenInstrument")).Checked;
                decimal qtyReplacements = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyReplacements")).Value);
                string itemName = dataItem["ItemName"].Text;

                if (qtyReplacements > 0 && !isBrokenInstrument)
                {
                    ShowMessage(string.Format("Broken Instrument for item {0} must be checked", itemName));
                    return false;
                }

                CssdFeasibilityTestItem entity = FindItemTransactionItem(dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.FeasibilityTestSeqNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.FeasibilityTestSeqNo;
                }
                entity.ReceivedNo = dataItem["ReceivedNo"].Text;
                entity.ReceivedSeqNo = dataItem["ReceivedSeqNo"].Text;
                entity.ItemNo = dataItem["ItemNo"].Text;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.Qty = qtyProcess;
                entity.CssdItemUnit = dataItem["CssdItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;

                entity.IsFeasibilityTestPassed = isFeasibilityTestPassed;
                entity.IsBrokenInstrument = isBrokenInstrument;
                entity.QtyReplacements = qtyReplacements;

                entity.IsBrokenInstrumentDetail = false;
                entity.QtyReplacementsDetail = 0;
            }

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_fts"] = null;
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