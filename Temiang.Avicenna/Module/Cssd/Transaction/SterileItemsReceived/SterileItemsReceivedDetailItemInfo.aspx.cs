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
    public partial class SterileItemsReceivedDetailItemInfo : BasePageDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtItemID.Text = Request.QueryString["itemid"];
                txtItemName.Text = Request.QueryString["itemname"];
                txtQty.Value = Convert.ToDouble(Request.QueryString["qty"]);
                txtSRItemUnit.Text = Request.QueryString["unit"];

                if (Request.QueryString["type"] == "edit")
                {
                    grdList.Columns.FindByUniqueName("QtyReceived").Visible = false;
                    if (Request.QueryString["from"] == "fts")
                    {
                        grdList.Columns.FindByUniqueName("TxtQtyReceived").Visible = false;

                        grdList.Columns.FindByUniqueName("QtyReplacements").Visible = false;
                        grdList.Columns.FindByUniqueName("IsBrokenInstrument").Visible = false;
                    }
                    else
                    {
                        grdList.Columns.FindByUniqueName("QtyReceived").Visible = false;

                        grdList.Columns.FindByUniqueName("IsBrokenInstrument").Visible = false;
                        grdList.Columns.FindByUniqueName("chkIsBrokenInstrument").Visible = false;
                        grdList.Columns.FindByUniqueName("QtyReplacements").Visible = false;
                        grdList.Columns.FindByUniqueName("TxtQtyReplacements").Visible = false;
                    }
                }
                else
                {
                    if (Request.QueryString["type"] == "info" && Request.QueryString["from"] == "ret")
                    {
                        var ret = new CssdSterileItemsReturnedItem();
                        if (ret.LoadByPrimaryKey(Request.QueryString["rno"], Request.QueryString["seq"]))
                        {
                            var process = new CssdSterilizationProcessItem();
                            if (process.LoadByPrimaryKey(ret.ProcessNo, ret.ProcessSeqNo))
                            {
                                var receive = new CssdSterileItemsReceivedItem();
                                if (receive.LoadByPrimaryKey(process.ReceivedNo, process.ReceivedSeqNo))
                                {
                                    txtQty.Value = Convert.ToDouble(receive.Qty);
                                }
                            }
                        }
                    }

                    (Helper.FindControlRecursive(this, "btnOk") as Button).Visible = false;
                    if (Request.QueryString["from"] == "fts")
                    {
                        grdList.Columns.FindByUniqueName("TxtQtyReceived").Visible = false;

                        grdList.Columns.FindByUniqueName("TxtQtyReplacements").Visible = false;
                        grdList.Columns.FindByUniqueName("chkIsBrokenInstrument").Visible = false;
                    }
                    else
                    {
                        grdList.Columns.FindByUniqueName("TxtQtyReceived").Visible = false;

                        grdList.Columns.FindByUniqueName("IsBrokenInstrument").Visible = false;
                        grdList.Columns.FindByUniqueName("chkIsBrokenInstrument").Visible = false;
                        grdList.Columns.FindByUniqueName("QtyReplacements").Visible = false;
                        grdList.Columns.FindByUniqueName("TxtQtyReplacements").Visible = false;
                        
                        if (Request.QueryString["from"] == "dst")
                        {
                            grdList.Columns.FindByUniqueName("QtyReceived").Visible = false;
                            this.Title = "Items Package";
                        }
                    }
                }
            }
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Request.QueryString["type"] == "info")
            {
                grdList.DataSource = CssdItemDetails;
            }
            else
            {
                if (Request.QueryString["from"] == "fts")
                {
                    var list = ((CssdSterileItemsReceivedItemDetailCollection)Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName + "_fts"]).Where(i => i.ReceivedNo == Request.QueryString["rno"] &&
                                                                                                             i.ReceivedSeqNo == Request.QueryString["seq"]);
                    grdList.DataSource = list;
                }
                else
                {
                    var list = ((CssdSterileItemsReceivedItemDetailCollection)Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName]).Where(i => i.ReceivedNo == Request.QueryString["rno"] &&
                                                                                                             i.ReceivedSeqNo == Request.QueryString["seq"]);
                    grdList.DataSource = list;
                }
            }
        }

        public override bool OnButtonOkClicked()
        {
            foreach (GridDataItem dataItem in grdList.MasterTableView.Items)
            {
                if (Request.QueryString["from"] == "fts")
                {
                    var entity = ((CssdSterileItemsReceivedItemDetailCollection)Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName + "_fts"]).FindByPrimaryKey(
                    dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text, dataItem["ItemID"].Text, dataItem["ItemDetailID"].Text);
                    if (entity != null)
                    {
                        entity.IsBrokenInstrument = (dataItem.FindControl("chkIsBrokenInstrument") as CheckBox).Checked;
                        entity.QtyReplacements = Convert.ToDecimal((dataItem.FindControl("txtQtyReplacements") as RadNumericTextBox).Value ?? 0);

                        entity.IsBrokenInstrumentX = entity.IsBrokenInstrument ?? false;
                        entity.QtyReplacementsX = entity.QtyReplacements ?? 0;
                    }
                }
                else
                {
                    var entity = ((CssdSterileItemsReceivedItemDetailCollection)Session["collCssdSterileItemsReceivedItemDetail" + Request.UserHostName]).FindByPrimaryKey(
                    dataItem["ReceivedNo"].Text, dataItem["ReceivedSeqNo"].Text, dataItem["ItemID"].Text, dataItem["ItemDetailID"].Text);
                    if (entity != null)
                    {
                        entity.QtyReceived = Convert.ToDecimal((dataItem.FindControl("txtQtyReceived") as RadNumericTextBox).Value ?? 0);
                    }
                }
            }

            return true;
        }

        private DataTable CssdItemDetails
        {
            get
            {
                DataTable dtb;

                if (Request.QueryString["from"] == "ret")
                {
                    var ret = new CssdSterileItemsReturnedItemQuery("ret");
                    var process = new CssdSterilizationProcessItemQuery("prs");
                    var query = new CssdSterileItemsReceivedItemDetailQuery("a");
                    var item = new ItemQuery("b");
                    var itemDetail = new VwItemProductMedicNonMedicQuery("c");

                    ret.Select(
                        query,
                        @"<ISNULL(a.IsBrokenInstrument, 0) AS 'IsBrokenInstrumentX'>",
                        @"<ISNULL(a.QtyReplacements, 0) AS 'QtyReplacementsX'>",
                        item.ItemName.As("ItemName"),
                        itemDetail.SRItemUnit.As("SRItemUnit")
                        );
                    ret.InnerJoin(process).On(process.ProcessNo == ret.ProcessNo && process.ProcessSeqNo == ret.ProcessSeqNo);
                    ret.InnerJoin(query).On(query.ReceivedNo == process.ReceivedNo && query.ReceivedSeqNo == process.ReceivedSeqNo);
                    ret.InnerJoin(item).On(query.ItemDetailID == item.ItemID);
                    ret.InnerJoin(itemDetail).On(query.ItemDetailID == itemDetail.ItemID);

                    ret.Where(ret.ReturnNo == Request.QueryString["rno"], ret.ReturnSeqNo == Request.QueryString["seq"]);

                    dtb = ret.LoadDataTable();
                }
                else if (Request.QueryString["from"] == "dst")
                {
                    var query = new CssdItemDetailQuery("a");
                    var item = new ItemQuery("b");
                    var itemDetail = new VwItemProductMedicNonMedicQuery("c");

                    query.Select(
                        @"<'' AS 'ReceivedNo'>",
                        @"<'' AS 'ReceivedSeqNo'>",
                        query.ItemID,
                        query.ItemDetailID,
                        query.Qty,
                        @"<a.Qty AS 'QtyReceived'>",
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID,
                        @"<CAST(0 AS BIT) AS 'IsBrokenInstrument'>",
                        @"<0 AS 'QtyReplacements'>",

                        @"<CAST(0 AS BIT) AS 'IsBrokenInstrumentX'>",
                        @"<0 AS 'QtyReplacementsX'>",
                        item.ItemName.As("ItemName"),
                        itemDetail.SRItemUnit.As("SRItemUnit")
                        );
                    query.InnerJoin(item).On(query.ItemDetailID == item.ItemID);
                    query.InnerJoin(itemDetail).On(query.ItemDetailID == itemDetail.ItemID);

                    query.Where(query.ItemID == Request.QueryString["itemid"]);

                    dtb = query.LoadDataTable();
                }
                else // rec,dec,fts
                {
                    var query = new CssdSterileItemsReceivedItemDetailQuery("a");
                    var item = new ItemQuery("b");
                    var itemDetail = new VwItemProductMedicNonMedicQuery("c");

                    query.Select(
                        query,
                        @"<ISNULL(a.IsBrokenInstrument, 0) AS 'IsBrokenInstrumentX'>",
                        @"<ISNULL(a.QtyReplacements, 0) AS 'QtyReplacementsX'>",
                        item.ItemName.As("ItemName"),
                        itemDetail.SRItemUnit.As("SRItemUnit")
                        );
                    query.InnerJoin(item).On(query.ItemDetailID == item.ItemID);
                    query.InnerJoin(itemDetail).On(query.ItemDetailID == itemDetail.ItemID);

                    query.Where(query.ReceivedNo == Request.QueryString["rno"], query.ReceivedSeqNo == Request.QueryString["seq"]);

                    dtb = query.LoadDataTable();
                }

                return dtb;
            }
        }
    }
}
