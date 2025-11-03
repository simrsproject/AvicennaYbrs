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
    public partial class DecontaminationDetailItemPicklist : BasePageDialog
    {
        private string DPhase
        {
            get
            {
                return Request.QueryString["p"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_" + DPhase] != null)
                grdList.DataSource = ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_" + DPhase];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_" + DPhase];
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

                ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_" + DPhase] = dtb;
            }
        }

        private void InitializeData()
        {
            DataTable dtb;
            if (!txtReceivedDate.IsEmpty)
                dtb = (new CssdDecontaminationItemCollection()).ItemOutstandingWithReceivedDate(txtReceivedDate.SelectedDate.Value.Date, cboFromServiceUnitID.SelectedValue, DPhase);
            else
                dtb = (new CssdDecontaminationItemCollection()).ItemOutstandingWithoutReceivedDate(cboFromServiceUnitID.SelectedValue, DPhase);

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_" + DPhase] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private CssdDecontaminationItem FindItemTransactionItem(string receivedNo, string receivedSeqNo)
        {
            var coll = (CssdDecontaminationItemCollection)Session["collCssdDecontaminationItem" + Request.UserHostName + "_" + DPhase];
            foreach (CssdDecontaminationItem entity in coll)
            {
                if (entity.ReceivedNo == receivedNo && entity.ReceivedSeqNo == receivedSeqNo)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (CssdDecontaminationItemCollection)Session["collCssdDecontaminationItem" + Request.UserHostName + "_" + DPhase];
            string seqNo = coll.HasData ? coll[coll.Count - 1].DecontaminationSeqNo : "000";

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyProcess = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyProcessed")).Value);
                string itemName = dataItem["ItemName"].Text;

                CssdDecontaminationItem entity = FindItemTransactionItem(dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.DecontaminationSeqNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.DecontaminationSeqNo;
                }
                entity.ReceivedNo = dataItem["ReceivedNo"].Text;
                entity.ReceivedSeqNo = dataItem["ReceivedSeqNo"].Text;
                entity.ItemNo = dataItem["ItemNo"].Text;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.Qty = qtyProcess;
                entity.CssdItemUnit = dataItem["CssdItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;
            }

            ViewState["CssdSterileItemsReceivedItems" + Request.UserHostName + "_" + DPhase] = null;
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