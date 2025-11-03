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
    public partial class SterileItemsReturnedDetailItemPickList : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateRoomList(Request.QueryString["tounit"]);
                StandardReference.InitializeIncludeSpace(cboSenderByID, AppEnum.StandardReference.CssdSender);
                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["CssdSterilizationProcessItems" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["CssdSterilizationProcessItems" + Request.UserHostName];
        }

        private void InitializeData()
        {
            string toUnit = Request.QueryString["tounit"];

            DataTable dtb;
            if (!txtProcessDate.IsEmpty)
                dtb = (new CssdSterileItemsReturnedItemCollection()).ItemOutstandingWithReceivedDate(
                            txtProcessDate.SelectedDate.Value.Date, toUnit, cboFromRoomID.SelectedValue, cboSenderByID.SelectedValue, AppSession.Application.IsMenuCssdPackagingActive);
            else
            {
                dtb = (new CssdSterileItemsReturnedItemCollection()).ItemOutstandingWithoutReceivedDate(
                            toUnit, cboFromRoomID.SelectedValue, cboSenderByID.SelectedValue, AppSession.Application.IsMenuCssdPackagingActive);
            }

            ViewState["CssdSterilizationProcessItems" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private CssdSterileItemsReturnedItem FindItemTransactionItem(string processNo, string processSeqNo)
        {
            var coll = (CssdSterileItemsReturnedItemCollection)Session["collCssdSterileItemsReturnedItem" + Request.UserHostName];
            foreach (CssdSterileItemsReturnedItem entity in coll)
            {
                if (entity.ProcessNo == processNo && entity.ProcessSeqNo == processSeqNo)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            //UpdateDataSource();

            DataTable tbl = (DataTable)ViewState["CssdSterilizationProcessItems" + Request.UserHostName];

            var coll = (CssdSterileItemsReturnedItemCollection)Session["collCssdSterileItemsReturnedItem" + Request.UserHostName];
            string seqNo = coll.HasData ? coll[coll.Count - 1].ReturnSeqNo : "000";

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyOutstanding = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyOutstanding")).Value);
                decimal qtyReturn = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyReturn")).Value);
                decimal weight = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtWeight")).Value);
                string itemName = dataItem["ItemName"].Text;

                if (qtyReturn <= 0) continue;

                if (qtyReturn > qtyOutstanding)
                {
                    ShowMessage(string.Format("Qty Return for item {0} can not be greather than {1}", itemName, qtyOutstanding.ToString()));
                    return false;
                }

                CssdSterileItemsReturnedItem entity = FindItemTransactionItem(dataItem["ProcessNo"].Text, dataItem["ProcessSeqNo"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();

                    entity.ReturnSeqNo = string.Format("{0:000}", int.Parse(seqNo) + 1);
                    seqNo = entity.ReturnSeqNo;
                }
                entity.ProcessNo = dataItem["ProcessNo"].Text;
                entity.ProcessSeqNo = dataItem["ProcessSeqNo"].Text;
                entity.ReceivedNo = dataItem["ReceivedNo"].Text;
                entity.ReceivedSeqNo = dataItem["ReceivedSeqNo"].Text;
                entity.Qty = qtyReturn;
                entity.Weight = weight;

                entity.ItemNo = dataItem["ItemNo"].Text;
                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.CssdItemUnit = dataItem["CssdItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;
            }

            ViewState["CssdSterilizationProcessItems" + Request.UserHostName] = null;
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

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            InitializeData();
            grdList.Rebind();
        }

        private void PopulateRoomList(string unitId)
        {
            cboFromRoomID.Items.Clear();

            var sr = new ServiceRoomCollection();
            var srQ = new ServiceRoomQuery("a");

            srQ.Select(srQ.RoomID, srQ.RoomName);
            srQ.Where(srQ.ServiceUnitID == unitId, srQ.IsActive == true);
            srQ.OrderBy(srQ.RoomID.Ascending);
            srQ.es.Distinct = true;

            sr.Load(srQ);

            cboFromRoomID.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
            foreach (ServiceRoom entity in sr)
            {
                cboFromRoomID.Items.Add(new RadComboBoxItem(entity.RoomName, entity.RoomID));
            }
        }
    }
}
