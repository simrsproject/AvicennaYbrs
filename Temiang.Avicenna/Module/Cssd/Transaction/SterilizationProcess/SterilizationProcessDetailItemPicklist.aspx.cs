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
    public partial class SterilizationProcessDetailItemPicklist : BasePageDialog
    {
        private string IsDtt
        {
            get
            {
                return Request.QueryString["dtt"];
            }
        }

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
            if (ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ReceivedNo"].Equals(dataItem.GetDataKeyValue("ReceivedNo").ToString()) && row["ReceivedSeqNo"].Equals(dataItem.GetDataKeyValue("ReceivedSeqNo").ToString()))
                    {
                        row["Qty"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value ?? 0;
                        row["Weight"] = ((RadNumericTextBox)dataItem.FindControl("txtWeight")).Value ?? 0;
                        break;
                    }
                }

                ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            DataTable dtb;
            if (!txtReceivedDate.IsEmpty)
                dtb = (new CssdSterilizationProcessItemCollection()).ItemOutstandingWithReceivedDate(
                            txtReceivedDate.SelectedDate.Value.Date, cboFromServiceUnitID.SelectedValue, cboSenderByID.SelectedValue, IsDtt == "1", 
                            AppSession.Application.IsMenuCssdPackagingActive, AppSession.Application.IsMenuCssdFeasibilityTestActive, AppSession.Application.IsMenuCssdDecontaminationActive);
            else
            {
                dtb = (new CssdSterilizationProcessItemCollection()).ItemOutstandingWithoutReceivedDate(
                            cboFromServiceUnitID.SelectedValue, cboSenderByID.SelectedValue, IsDtt == "1", 
                            AppSession.Application.IsMenuCssdPackagingActive, AppSession.Application.IsMenuCssdFeasibilityTestActive, AppSession.Application.IsMenuCssdDecontaminationActive);
            }

            foreach (DataRow row in dtb.Rows)
            {
                if (Convert.ToBoolean(row["IsNeedUltrasound"]))
                {
                    var uscoll = new CssdSterileItemsUltrasoundItemCollection();
                    var usdt = new CssdSterileItemsUltrasoundItemQuery("a");
                    var ushd = new CssdSterileItemsUltrasoundQuery("b");
                    usdt.InnerJoin(ushd).On(usdt.TransactionNo == ushd.TransactionNo && ushd.IsApproved == true);
                    usdt.Where(usdt.ReceivedNo == row["ReceivedNo"].ToString(),
                               usdt.ReceivedSeqNo == row["ReceivedSeqNo"].ToString());
                    uscoll.Load(usdt);
                    if (uscoll.Count == 0)
                        row.Delete();
                }
            }

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private CssdSterilizationProcessItem FindItemTransactionItem(string receivedNo, string receivedSeqNo)
        {
            var coll = (CssdSterilizationProcessItemCollection)Session["collCssdSterilizationProcessItem" + Request.UserHostName];
            foreach (CssdSterilizationProcessItem entity in coll)
            {
                if (entity.ReceivedNo == receivedNo && entity.ReceivedSeqNo == receivedSeqNo)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (CssdSterilizationProcessItemCollection)Session["collCssdSterilizationProcessItem" + Request.UserHostName];
            string seqNo = coll.HasData ? coll[coll.Count - 1].ProcessSeqNo : "000";

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyOutstanding = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyOutstanding")).Value);
                decimal qtyProcess = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value);
                decimal weight = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtWeight")).Value);
                string itemName = dataItem["ItemName"].Text;

                if (qtyProcess <= 0) continue;

                if (qtyProcess > qtyOutstanding)
                {
                    ShowMessage(string.Format("Qty Processed for item {0} can not be greather than {1}", itemName, qtyOutstanding.ToString()));
                    return false;
                }

                CssdSterilizationProcessItem entity = FindItemTransactionItem(dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.ProcessSeqNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.ProcessSeqNo;
                }
                entity.ReceivedNo = dataItem["ReceivedNo"].Text;
                entity.ReceivedSeqNo = dataItem["ReceivedSeqNo"].Text;
                entity.Qty = qtyProcess;
                entity.Weight = weight;

                entity.ItemNo = dataItem["ItemNo"].Text;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.CssdItemUnit = dataItem["CssdItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;
                entity.CostAmount = 0;
            }

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName] = null;
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
