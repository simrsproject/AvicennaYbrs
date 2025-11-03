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
    public partial class UltrasoundProcessDetailItemPicklist : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StandardReference.InitializeIncludeSpace(cboSenderByID, AppEnum.StandardReference.CssdSender);
                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["CssdSterileItemsReceivedItemForUltrasounds" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["CssdSterileItemsReceivedItemForUltrasounds" + Request.UserHostName];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["CssdSterileItemsReceivedItemForUltrasounds" + Request.UserHostName];
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

                ViewState["CssdSterileItemsReceivedItemForUltrasounds" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            DataTable dtb;
            if (!txtReceivedDate.IsEmpty)
            {
                dtb = (new CssdSterileItemsUltrasoundItemCollection()).ItemOutstandingWithReceivedDate(
                            txtReceivedDate.SelectedDate.Value.Date, cboFromServiceUnitID.SelectedValue, cboSenderByID.SelectedValue, 
                            AppSession.Application.IsMenuCssdPackagingActive, AppSession.Application.IsMenuCssdFeasibilityTestActive, AppSession.Application.IsMenuCssdDecontaminationActive);
            }
            else
            {
                dtb = (new CssdSterileItemsUltrasoundItemCollection()).ItemOutstandingWithoutReceivedDate(
                            cboFromServiceUnitID.SelectedValue, cboSenderByID.SelectedValue, 
                            AppSession.Application.IsMenuCssdPackagingActive, AppSession.Application.IsMenuCssdFeasibilityTestActive, AppSession.Application.IsMenuCssdDecontaminationActive);
            }

            ViewState["CssdSterileItemsReceivedItemForUltrasounds" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private CssdSterileItemsUltrasoundItem FindItemTransactionItem(string receivedNo, string receivedSeqNo)
        {
            var coll = (CssdSterileItemsUltrasoundItemCollection)Session["collCssdSterileItemsUltrasoundItem" + Request.UserHostName];
            foreach (CssdSterileItemsUltrasoundItem entity in coll)
            {
                if (entity.ReceivedNo == receivedNo && entity.ReceivedSeqNo == receivedSeqNo)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (CssdSterileItemsUltrasoundItemCollection)Session["collCssdSterileItemsUltrasoundItem" + Request.UserHostName];
            string seqNo = coll.HasData ? coll[coll.Count - 1].TransactionSeqNo : "000";

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyProcess = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value);

                CssdSterileItemsUltrasoundItem entity = FindItemTransactionItem(dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.TransactionSeqNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.TransactionSeqNo;
                }
                entity.ReceivedNo = dataItem["ReceivedNo"].Text;
                entity.ReceivedSeqNo = dataItem["ReceivedSeqNo"].Text;
                entity.Qty = qtyProcess;
                entity.ItemNo = dataItem["ItemNo"].Text;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.CssdItemUnit = dataItem["CssdItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;
            }

            ViewState["CssdSterileItemsReceivedItemForUltrasounds" + Request.UserHostName] = null;
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
