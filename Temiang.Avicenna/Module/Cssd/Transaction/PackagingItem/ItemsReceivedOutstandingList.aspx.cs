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
    public partial class ItemsReceivedOutstandingList : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdList.Columns.FindByUniqueName("chkIsDtt").Visible = AppSession.Parameter.IsCssdUsingDttTerm;
                grdList.Columns.FindByUniqueName("cboIsDtt").Visible = !AppSession.Parameter.IsCssdUsingDttTerm;

                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_pck"] != null)
                grdList.DataSource = ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_pck"];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_pck"];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                string receivedNo = dataItem.GetDataKeyValue("ReceivedNo").ToString();
                string receivedSeqNo = dataItem.GetDataKeyValue("ReceivedSeqNo").ToString();

                DateTime? expiredDate = ((RadDatePicker)dataItem.FindControl("txtExpiredDate")).SelectedDate;
                double reuseTo = ((RadNumericTextBox)dataItem.FindControl("txtReuseTo")).Value ?? 0;
                bool isNeedUltrasound = ((CheckBox)dataItem.FindControl("chkIsNeedUltrasound")).Checked;
                bool isDtt = ((CheckBox)dataItem.FindControl("chkIsDtt")).Checked;
                string isDttText = ((RadComboBox)dataItem.FindControl("cboIsDtt")).SelectedValue;
                string isDttTextName = ((RadComboBox)dataItem.FindControl("cboIsDtt")).Text;

                if (AppSession.Parameter.IsCssdUsingDttTerm)
                    isDttTextName = isDtt ? "High" : "Low";
                else
                    isDtt = (isDttText == "1");

                foreach (DataRow row in dtb.Rows)
                {
                    if (row["ReceivedNo"].Equals(receivedNo) && row["ReceivedSeqNo"].Equals(receivedSeqNo))
                    {
                        if (expiredDate != null)
                        {
                            row["ExpiredDate"] = expiredDate;
                        }
                        row["ReuseTo"] = reuseTo;
                        row["IsNeedUltrasound"] = isNeedUltrasound;
                        row["IsDtt"] = isDtt;
                        row["DttDescription"] = isDttTextName;

                        break;
                    }
                }

                ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_pck"] = dtb;
            }
        }

        private void InitializeData()
        {
            DataTable dtb;
            var phase = string.Empty;
            if (AppSession.Application.IsMenuCssdFeasibilityTestActive)
                phase = "fts";
            else if (AppSession.Application.IsMenuCssdDecontaminationActive && AppSession.Parameter.IsCentralizedCssd)
                phase = "dec";

            if (!txtReceivedDate.IsEmpty)
                dtb = (new CssdPackagingItemCollection()).ItemOutstandingWithReceivedDate(txtReceivedDate.SelectedDate.Value.Date, cboFromServiceUnitID.SelectedValue, phase);
            else
                dtb = (new CssdPackagingItemCollection()).ItemOutstandingWithoutReceivedDate(cboFromServiceUnitID.SelectedValue, phase);

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_pck"] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private CssdPackagingItem FindItemTransactionItem(string receivedNo, string receivedSeqNo)
        {
            var coll = (CssdPackagingItemCollection)Session["collCssdPackagingItem" + Request.UserHostName];
            foreach (CssdPackagingItem entity in coll)
            {
                if (entity.ReceivedNo == receivedNo && entity.ReceivedSeqNo == receivedSeqNo)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (CssdPackagingItemCollection)Session["collCssdPackagingItem" + Request.UserHostName];
            string seqNo = coll.HasData ? coll[coll.Count - 1].SeqNo : "000";

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyProcess = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value);
                string itemId = dataItem["ItemID"].Text;
                string itemName = dataItem["ItemName"].Text;

                DateTime? expiredDate = ((RadDatePicker)dataItem.FindControl("txtExpiredDate")).SelectedDate;
                double reuseTo = ((RadNumericTextBox)dataItem.FindControl("txtReuseTo")).Value ?? 0;
                bool isNeedUltrasound = ((CheckBox)dataItem.FindControl("chkIsNeedUltrasound")).Checked;
                bool isDtt = ((CheckBox)dataItem.FindControl("chkIsDtt")).Checked;
                string isDttText = ((RadComboBox)dataItem.FindControl("cboIsDtt")).SelectedValue;
                string isDttTextName = ((RadComboBox)dataItem.FindControl("cboIsDtt")).Text;

                if (expiredDate == null)
                {
                    ShowMessage(string.Format("Expired Date for item {0} is required.", itemId + " - " + itemName));
                    return false;
                }

                if (AppSession.Parameter.IsCssdUsingDttTerm)
                    isDttTextName = isDtt ? "High" : "Low";
                else
                {
                    if (isDttText == "")
                    {
                        ShowMessage(string.Format("Temperature for item {0} is required.", itemId + " - " + itemName));
                        return false;
                    }
                    isDtt = (isDttText == "1");
                }
                    
                CssdPackagingItem entity = FindItemTransactionItem(dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.SeqNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.SeqNo;
                }
                entity.ReceivedNo = dataItem["ReceivedNo"].Text;
                entity.ReceivedSeqNo = dataItem["ReceivedSeqNo"].Text;
                entity.ItemNo = dataItem["ItemNo"].Text;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.Qty = qtyProcess;
                entity.CssdItemUnit = dataItem["CssdItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;

                if (expiredDate != null)
                    entity.ExpiredDate = expiredDate;
                else
                    entity.str.ExpiredDate = string.Empty;
                entity.ReuseTo = Convert.ToInt16(reuseTo);
                entity.IsNeedUltrasound = isNeedUltrasound;
                entity.IsDtt = isDtt;
                entity.DttDescription = isDttTextName;
            }

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_pck"] = null;
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

        protected void cboIsDtt_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            e.Item.Text = ((DataRowView)e.Item.DataItem)["ItemName"].ToString();
            e.Item.Value = ((DataRowView)e.Item.DataItem)["ItemID"].ToString();
        }

        protected void cboIsDtt_ItemsRequested(object o, RadComboBoxItemsRequestedEventArgs e)
        {
            var query = new AppStandardReferenceItemQuery("a");
            query.Where(
                query.StandardReferenceID == "DttText");
            query.Select(query.ItemID, query.ItemName);
            var combo = o as RadComboBox;

            combo.DataSource = query.LoadDataTable();
            combo.DataBind();
        }
    }
}