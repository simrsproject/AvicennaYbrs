using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.Interfaces;
using System.Web.UI;

namespace Temiang.Avicenna.Module.ControlPanel.PrinterManagement
{
    public partial class PrinterList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PrinterSearch.aspx";
            UrlPageDetail = "PrinterDetail.aspx";

            ProgramID = AppConstant.Program.PrinterLocation;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
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
            string id = dataItem.GetDataKeyValue(PrinterMetadata.ColumnNames.PrinterID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Printers;
        }

        private DataTable Printers
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                PrinterQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PrinterQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PrinterQuery();

                    //Quick Search
                    ApplyQuickSearch(query);
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(
                                query.PrinterID,
                                query.PrinterName,
                                query.PrinterLocationHost,
                                query.PrinterManagerHost,
                                query.Notes
                            );
                query.OrderBy(query.PrinterID.Descending);

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument))
                return;

            if (!(sourceControl is RadGrid))
                return;

            if (eventArgument.Contains("|"))
            {
                var param = eventArgument.Split('|');

                using (var trans = new esTransactionScope())
                {
                    var pjlcoll = new PrintJobLogCollection();
                    pjlcoll.Query.Where(pjlcoll.Query.PrinterID == param[1]);
                    pjlcoll.LoadAll();

                    foreach (var pjl in pjlcoll)
                    {
                        var pjplcoll = new PrintJobParameterLogCollection();
                        pjplcoll.Query.Where(pjplcoll.Query.PrintNo == pjl.PrintNo);
                        pjplcoll.LoadAll();

                        pjplcoll.MarkAllAsDeleted();
                        pjplcoll.Save();
                    }
                    pjlcoll.MarkAllAsDeleted();
                    pjlcoll.Save();

                    var pjcoll = new PrintJobCollection();
                    pjcoll.Query.Where(pjcoll.Query.PrinterID == param[1]);
                    pjcoll.LoadAll();

                    foreach (var pj in pjcoll)
                    {
                        var pjpcoll = new PrintJobParameterCollection();
                        pjpcoll.Query.Where(pjpcoll.Query.PrintNo == pj.PrintNo);
                        pjpcoll.LoadAll();

                        pjpcoll.MarkAllAsDeleted();
                        pjpcoll.Save();
                    }
                    pjcoll.MarkAllAsDeleted();
                    pjcoll.Save();

                    trans.Complete();
                }

                grdList.Rebind();
            }
        }
    }
}

