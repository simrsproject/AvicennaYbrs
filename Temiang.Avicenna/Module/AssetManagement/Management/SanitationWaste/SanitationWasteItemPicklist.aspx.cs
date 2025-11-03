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

namespace Temiang.Avicenna.Module.AssetManagement.Management
{
    public partial class SanitationWasteItemPicklist : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFromTransactionDate.SelectedDate = DateTime.Now;
                txtToTransactionDate.SelectedDate = DateTime.Now;
                InitializeData();
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (ViewState["SanitationWasteTransItemReceiveds" + Request.UserHostName] != null)
                grdList.DataSource = ViewState["SanitationWasteTransItemReceiveds" + Request.UserHostName];
        }

        protected void grdList_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dtb = (DataTable)ViewState["SanitationWasteTransItemReceiveds" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                foreach (DataRow row in dtb.Rows)
                {
                    if (row["TransactionNo"].Equals(dataItem.GetDataKeyValue("TransactionNo").ToString()) && row["SRWasteType"].Equals(dataItem.GetDataKeyValue("SRWasteType").ToString()))
                    {
                        row["Qty"] = ((RadNumericTextBox)dataItem.FindControl("txtQtyDisposal")).Value ?? 0;

                        break;
                    }
                }

                ViewState["SanitationWasteTransItemReceiveds" + Request.UserHostName] = dtb;
            }
        }

        private void InitializeData()
        {
            var dtb = (new SanitationWasteTransItemCollection()).GetSanitationWasteTransItemReceived(txtFromTransactionDate.SelectedDate, txtToTransactionDate.SelectedDate);

            ViewState["SanitationWasteTransItemReceiveds" + Request.UserHostName] = dtb;

            grdList.DataSource = dtb;
            grdList.DataBind();
        }

        private SanitationWasteTransItem FindItem(string refNo, string itemId)
        {
            var coll = (SanitationWasteTransItemCollection)Session["collSanitationWasteTransItem" + Request.UserHostName];
            foreach (SanitationWasteTransItem entity in coll)
            {
                if (entity.ReferenceNo == refNo && entity.SRWasteType == itemId)
                    return entity;
            }
            return null;
        }

        public override bool OnButtonOkClicked()
        {
            var coll = (SanitationWasteTransItemCollection)Session["collSanitationWasteTransItem" + Request.UserHostName];
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items.Cast<GridDataItem>().Where(g => ((CheckBox)g.FindControl("detailChkbox")).Checked))
            {
                decimal qtyDisposal = Convert.ToDecimal(((RadNumericTextBox)dataItem.FindControl("txtQtyDisposal")).Value);

                if (qtyDisposal <= 0) continue;

                SanitationWasteTransItem entity = FindItem(dataItem["TransactionNo"].Text, dataItem["SRWasteType"].Text);
                if (entity == null)
                {
                    entity = coll.AddNew();
                }
                entity.TransactionNo = Request.QueryString["tno"].ToString();
                entity.SRWasteType = dataItem["SRWasteType"].Text;
                entity.WasteTypeName = dataItem["WasteTypeName"].Text;
                entity.ReferenceNo = dataItem["TransactionNo"].Text;
                entity.Qty = qtyDisposal;
            }

            ViewState["SanitationWasteTransItemReceiveds" + Request.UserHostName] = null;
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