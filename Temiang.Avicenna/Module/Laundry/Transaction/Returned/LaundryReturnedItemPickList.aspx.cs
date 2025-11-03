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
    public partial class LaundryReturnedItemPickList : BasePageDialog
    {
        private string getPageID
        {
            get
            {
                var retval = string.Empty;
                var p = new AppProgram();
                if (p.LoadByPrimaryKey("15.01.01"))
                {
                    string lastOne = p.NavigateUrl.Substring(p.NavigateUrl.Length - 1);
                    if (lastOne == "n")
                        retval = "n";
                }

                return retval;
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
            if (ViewState["LaunderedProcessItems" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["LaunderedProcessItems" + Request.UserHostName];
        }

        private void InitializeData()
        {
            string toUnit = Request.QueryString["tounit"];

            DataTable dtb;
            if (!txtProcessDate.IsEmpty)
            {
                if (rblTypes.SelectedIndex == 0)
                {
                    if (getPageID == "")
                        dtb = (new LaundryReturnedItemCollection()).ItemOutstandingWithReceivedDate(txtProcessDate.SelectedDate.Value.Date, toUnit, string.Empty);
                    else
                        dtb = (new LaundryReturnedItemCollection()).ItemOutstandingInfectiousWithReceivedDate(txtProcessDate.SelectedDate.Value.Date, toUnit, string.Empty, "01");
                }
                else
                {
                    dtb = (new LaundryReturnedItemCollection()).ItemOutstandingInfectiousWithReceivedDate(txtProcessDate.SelectedDate.Value.Date, toUnit, string.Empty, "02");
                }
            }
            else
            {
                if (rblTypes.SelectedIndex == 0)
                {
                    if (getPageID == "")
                        dtb = (new LaundryReturnedItemCollection()).ItemOutstandingWithoutReceivedDate(toUnit, string.Empty);
                    else
                        dtb = (new LaundryReturnedItemCollection()).ItemOutstandingInfectiousWithoutReceivedDate(toUnit, string.Empty, "01");
                }
                else
                {
                    dtb = (new LaundryReturnedItemCollection()).ItemOutstandingInfectiousWithoutReceivedDate(toUnit, string.Empty, "02");
                }
            }

            ViewState["LaunderedProcessItems" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private LaundryReturnedItem FindItemTransactionItem(string processNo, string processSeqNo, string itemId)
        {
            var coll = (LaundryReturnedItemCollection)Session["collLaundryReturnedItem" + Request.UserHostName];
            foreach (LaundryReturnedItem entity in coll)
            {
                if (entity.ProcessNo == processNo && entity.ProcessSeqNo == processSeqNo && entity.ItemID == itemId)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            DataTable tbl = (DataTable)ViewState["LaunderedProcessItems" + Request.UserHostName];

            var coll = (LaundryReturnedItemCollection)Session["collLaundryReturnedItem" + Request.UserHostName];
            string seqNo;
            if (coll.HasData)
            {
                var sequenceNo = (coll.OrderByDescending(c => c.ReturnSeqNo).Select(c => c.ReturnSeqNo)).Take(1);
                int seqNo1 = int.Parse(sequenceNo.Single());
                seqNo = string.Format("{0:000}", seqNo1);
            }
            else
            {
                seqNo = "000";
            }

            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyOutstanding = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyOutstanding")).Value);
                decimal qtyReturn = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyReturn")).Value);
                string itemId = dataItem["ItemID"].Text;
                string itemName = dataItem["ItemName"].Text;
                bool isInfectious = rblTypes.SelectedIndex == 1; //((CheckBox)dataItem.FindControl("chkIsInfectious")).Checked;

                if (qtyReturn <= 0) continue;

                if (qtyReturn > qtyOutstanding)
                {
                    ShowMessage(string.Format("Qty Return for item {0} can not be greather than {1}", itemName, qtyOutstanding.ToString()));
                    return false;
                }

                LaundryReturnedItem entity = FindItemTransactionItem(dataItem["ProcessNo"].Text, dataItem["ProcessSeqNo"].Text, itemId);
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

                entity.ItemID = dataItem["ItemID"].Text;
                entity.ItemName = dataItem["ItemName"].Text;
                entity.ItemUnit = dataItem["ItemUnit"].Text;
                entity.Notes = dataItem["Notes"].Text;
                entity.IsInfectious = isInfectious;
            }

            ViewState["LaunderedProcessItems" + Request.UserHostName] = null;
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
    }
}