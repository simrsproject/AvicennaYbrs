using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Transaction
{
    public partial class LaunderedProcessList : BasePageList
    {
        private string getPageID
        {
            get
            {
                return string.IsNullOrEmpty(Request.QueryString["type"]) ? string.Empty : Request.QueryString["type"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "LaunderedProcessSearch.aspx?type=" + getPageID;
            UrlPageDetail = "LaunderedProcessDetail.aspx?type=" + getPageID;

            ProgramID = (getPageID == "" || getPageID == "n") ? AppConstant.Program.LaundererProcess : (getPageID == "i" ? AppConstant.Program.LaundererProcessInfectious : AppConstant.Program.LaundererProcessRewashing);

            this.WindowSearch.Height = 400;
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", getPageID);
            return script;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(LaunderedProcessMetadata.ColumnNames.ProcessNo).ToString();
            Page.Response.Redirect("LaunderedProcessDetail.aspx?md=" + mode + "&id=" + id + "&type=" + getPageID, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = LaunderedProcesss;
        }

        private DataTable LaunderedProcesss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaunderedProcessQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (LaunderedProcessQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaunderedProcessQuery("a");
                    var m = new LaundryWashingMachineQuery("b");
                    var p = new AppStandardReferenceItemQuery("c");
                    var t = new AppStandardReferenceItemQuery("d");

                    query.LeftJoin(m).On(m.MachineID == query.MachineID);
                    query.LeftJoin(p).On(p.StandardReferenceID == "LaundryProgram" && p.ItemID == query.SRLaundryProgram);
                    query.LeftJoin(t).On(t.StandardReferenceID == "LaundryType" && t.ItemID == query.SRLaundryType);

                    query.Select
                        (
                            query.ProcessNo,
                            query.ProcessDate,
                            query.ProcessTime,
                            m.MachineName,
                            p.ItemName.As("LaundryProgramName"),
                            t.ItemName.As("LaundryTypeName"),
                            query.IsApproved,
                            query.IsVoid
                        );

                    if (getPageID == "")
                    {
                        query.Select(@"<'LaunderedProcessDetail.aspx?md=view&id='+a.ProcessNo+'&type=' AS PUrl>");
                        query.Where(query.SRLaundryProcessType == "01");
                    }
                    else if (getPageID == "n")
                    {
                        query.Select(@"<'LaunderedProcessDetail.aspx?md=view&id='+a.ProcessNo+'&type=n' AS PUrl>");
                        query.Where(query.SRLaundryProcessType == "01");
                    }
                    else if (getPageID == "i")
                    {
                        query.Select(@"<'LaunderedProcessDetail.aspx?md=view&id='+a.ProcessNo+'&type=i' AS PUrl>");
                        query.Where(query.SRLaundryProcessType == "02");
                    }
                    else
                    {
                        query.Select(@"<'LaunderedProcessDetail.aspx?md=view&id='+a.ProcessNo+'&type=r' AS PUrl>");
                        query.Where(query.SRLaundryProcessType == "03");
                    }

                    query.OrderBy(query.ProcessDate.Descending, query.ProcessNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            //Load record
            if (AppSession.Parameter.IsCentralizedLaundrie)
            {
                var query = new LaunderedProcessItemCentralizationQuery("a");
                var iq = new ItemQuery("b");
                var nmq = new ItemProductNonMedicQuery("c");
                var unitq = new AppStandardReferenceItemQuery("d");

                query.Select
                    (
                        query.ProcessNo,
                        @"<'000' AS ProcessSeqNo>",
                        @"<'-' AS ReceivedNo>",
                        @"<'000' AS ReceivedSeqNo>",
                        query.Qty,

                        query.ItemID,
                        iq.ItemName.As("ItemName"),

                        nmq.SRItemUnit,
                        unitq.ItemName.As("ItemUnit"),
                        @"<'' AS Notes>"
                    );
                query.InnerJoin(iq).On(iq.ItemID == query.ItemID);
                query.InnerJoin(nmq).On(nmq.ItemID == query.ItemID);
                query.InnerJoin(unitq).On(unitq.ItemID == nmq.SRItemUnit &&
                                          unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                query.Where(query.ProcessNo == e.DetailTableView.ParentItem.GetDataKeyValue("ProcessNo").ToString());
                query.OrderBy(query.ItemID.Ascending);

                DataTable dtb = query.LoadDataTable();

                //Apply
                e.DetailTableView.DataSource = dtb;
            }
            else
            {
                if (getPageID == "i")
                {
                    var query = new LaunderedProcessItemInfectiousQuery("a");
                    var received = new LaundryReceivedItemInfectiousQuery("b");
                    var iq = new ItemLinenQuery("c");
                    var unitq = new AppStandardReferenceItemQuery("d");

                    query.Select
                        (
                            query.ProcessNo,
                            query.ProcessSeqNo,
                            query.ReceivedNo,
                            query.ReceivedSeqNo,
                            query.Qty,

                            received.ItemID,
                            iq.ItemName.As("ItemName"),

                            received.SRItemUnit,
                            unitq.ItemName.As("ItemUnit"),
                            received.Notes
                        );
                    query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                                 received.ReceivedSeqNo == query.ReceivedSeqNo);
                    query.LeftJoin(iq).On(iq.ItemID == received.ItemID);
                    query.LeftJoin(unitq).On(unitq.ItemID == received.SRItemUnit &&
                                              unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                    query.Where(query.ProcessNo == e.DetailTableView.ParentItem.GetDataKeyValue("ProcessNo").ToString());
                    query.OrderBy(received.ReceivedNo.Ascending, received.ReceivedSeqNo.Ascending);

                    DataTable dtb = query.LoadDataTable();

                    //Apply
                    e.DetailTableView.DataSource = dtb;
                }
                else
                {
                    var query = new LaunderedProcessItemQuery("a");
                    var received = new LaundryReceivedItemQuery("b");
                    var iq = new ItemQuery("c");
                    var unitq = new AppStandardReferenceItemQuery("d");

                    query.Select
                        (
                            query.ProcessNo,
                            query.ProcessSeqNo,
                            query.ReceivedNo,
                            query.ReceivedSeqNo,
                            query.Qty,

                            received.ItemID,
                            iq.ItemName.As("ItemName"),

                            received.SRItemUnit,
                            unitq.ItemName.As("ItemUnit"),
                            received.Notes
                        );
                    query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                                 received.ReceivedSeqNo == query.ReceivedSeqNo);
                    query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
                    query.InnerJoin(unitq).On(unitq.ItemID == received.SRItemUnit &&
                                              unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
                    query.Where(query.ProcessNo == e.DetailTableView.ParentItem.GetDataKeyValue("ProcessNo").ToString());
                    query.OrderBy(received.ReceivedNo.Ascending, received.ReceivedSeqNo.Ascending);

                    DataTable dtb = query.LoadDataTable();

                    //Apply
                    e.DetailTableView.DataSource = dtb;
                }
                
            }
        }
    }
}
