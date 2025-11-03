using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class SterilizationProcessList : BasePageList
    {
        private string IsDtt
        {
            get
            {
                return Request.QueryString["dtt"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SterilizationProcessSearch.aspx?dtt=" + IsDtt;
            UrlPageDetail = "SterilizationProcessDetail.aspx?dtt=" + IsDtt;

            ProgramID = IsDtt == "0" ? AppConstant.Program.CssdSterilizationProcess : AppConstant.Program.CssdDttProcess;

            this.WindowSearch.Height = 400;
         
            if (!IsPostBack)
            {
                grdList.Columns[4].Visible = IsDtt == "0";
                grdList.Columns[5].Visible = IsDtt == "0";
                grdList.Columns[7].Visible = IsDtt == "0";
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", IsDtt);
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
            string id = dataItem.GetDataKeyValue(CssdSterilizationProcessMetadata.ColumnNames.ProcessNo).ToString();
            string url = string.Format("SterilizationProcessDetail.aspx?md={0}&id={1}&dtt={2}", mode, id, IsDtt);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdSterilizationProcesss;
        }

        private DataTable CssdSterilizationProcesss
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdSterilizationProcessQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdSterilizationProcessQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdSterilizationProcessQuery("a");
                    var machine = new CssdMachineQuery("b");
                    var ptype = new AppStandardReferenceItemQuery("c");
                    var usr = new AppUserQuery("d");

                    query.Select
                        (
                            query.ProcessNo,
                            query.ProcessDate,
                            query.ProcessStartTime,
                            query.ProcessEndTime,
                            query.MachineID,
                            machine.MachineName,
                            query.SRCssdProcessType,
                            ptype.ItemName.As("CssdProcessTypeName"),
                            query.OperatorByUserID,
                            usr.UserName.As("OperatorBy"),
                            @"<CASE WHEN ISNULL(a.ProcessTo, '') = '' THEN '' ELSE SUBSTRING(a.ProcessTo, 8, 4) END AS ProcessTo>",
                            query.IsApproved,
                            query.IsVoid
                        );

                    query.LeftJoin(machine).On(machine.MachineID == query.MachineID);
                    query.LeftJoin(ptype).On(ptype.ItemID == query.SRCssdProcessType &&
                                              ptype.StandardReferenceID == AppEnum.StandardReference.CssdProcessType);
                    query.InnerJoin(usr).On(usr.UserID == query.OperatorByUserID);
                    if (IsDtt == "0")
                    {
                        query.Select("<'SterilizationProcessDetail.aspx?md=view&id='+a.ProcessNo+'&dtt=0' AS PUrl>");
                        query.Where(query.IsDtt == false);
                    }
                    else
                    {
                        query.Select("<'SterilizationProcessDetail.aspx?md=view&id='+a.ProcessNo+'&dtt=1' AS PUrl>");
                        query.Where(query.IsDtt == true);
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
            var query = new CssdSterilizationProcessItemQuery("a");
            var received = new CssdSterileItemsReceivedItemQuery("b");
            var iq = new ItemQuery("c");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.ProcessNo,
                    query.ProcessSeqNo,
                    query.ReceivedNo,
                    query.ReceivedSeqNo,
                    query.Qty,
                    query.Weight,

                    received.CssdItemNo,
                    @"<CAST((CAST(b.CssdItemNo AS INT)) AS VARCHAR) AS 'ItemNo'>",

                    received.ItemID,
                    iq.ItemName.As("ItemName"),

                    received.SRCssdItemUnit,
                    unitq.ItemName.As("CssdItemUnit"),
                    received.Notes
                );
            query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
            query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.ProcessNo == e.DetailTableView.ParentItem.GetDataKeyValue("ProcessNo").ToString());
            query.OrderBy(received.CssdItemNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}
