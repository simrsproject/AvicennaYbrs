using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Cssd.Transaction
{
    public partial class DecontaminationList : BasePageList
    {
        private string DPhase
        {
            get
            {
                return Request.QueryString["p"];
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "DecontaminationSearch.aspx?p=" + DPhase;
            UrlPageDetail = "DecontaminationDetail.aspx?p=" + DPhase;

            ProgramID = DPhase == "1" ? AppConstant.Program.CssdDecontaminationImmersion : (DPhase == "2" ? AppConstant.Program.CssdDecontaminationAbstersion : AppConstant.Program.CssdDecontaminationDrying);

            this.WindowSearch.Height = 400;

            if (!IsPostBack)
            {
                grdList.Columns.FindByUniqueName("AbstersionType").Visible = (DPhase == "2");
            }
        }

        public override string OnGetScriptToolBarNewClicking()
        {
            string script = string.Format(@"gotoAddUrl('{0}'); args.set_cancel(true);", DPhase);
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
            string id = dataItem.GetDataKeyValue(CssdDecontaminationMetadata.ColumnNames.DecontaminationNo).ToString();
            string url = string.Format("DecontaminationDetail.aspx?md={0}&id={1}&p={2}", mode, id, DPhase);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = CssdDecontaminations;
        }

        private DataTable CssdDecontaminations
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                CssdDecontaminationQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (CssdDecontaminationQuery)Session[SessionNameForQuery];
                else
                {
                    query = new CssdDecontaminationQuery("a");
                    var phaseq = new AppStandardReferenceItemQuery("b");
                    var typeq = new AppStandardReferenceItemQuery("c");
                    query.LeftJoin(phaseq).On(phaseq.StandardReferenceID == AppEnum.StandardReference.DecontaminationPhase.ToString() && phaseq.ItemID == query.SRDecontaminationPhase);
                    query.LeftJoin(typeq).On(typeq.StandardReferenceID == AppEnum.StandardReference.AbstersionType.ToString() && typeq.ItemID == query.SRAbstersionType);

                    query.Select
                        (
                            query.DecontaminationNo,
                            query.DecontaminationDate,
                            query.DecontaminationTime,
                            phaseq.ItemName.As("DecontaminationPhase"),
                            typeq.ItemName.As("AbstersionType"),
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid
                        );
                    query.Where(query.SRDecontaminationPhase == DPhase);

                    query.OrderBy(query.DecontaminationDate.Descending, query.DecontaminationNo.Descending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;

                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            var query = new CssdDecontaminationItemQuery("a");
            var received = new CssdSterileItemsReceivedItemQuery("b");
            var iq = new ItemQuery("c");
            var unitq = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.DecontaminationNo,
                    query.DecontaminationSeqNo,
                    query.ReceivedNo,
                    query.ReceivedSeqNo,

                    received.CssdItemNo,
                    @"<CAST((CAST(b.CssdItemNo AS INT)) AS VARCHAR) AS 'ItemNo'>",

                    received.ItemID,
                    iq.ItemName.As("ItemName"),
                    received.Qty,
                    received.SRCssdItemUnit,
                    unitq.ItemName.As("CssdItemUnit"),
                    received.Notes
                );
            query.InnerJoin(received).On(received.ReceivedNo == query.ReceivedNo &&
                                         received.ReceivedSeqNo == query.ReceivedSeqNo);
            query.InnerJoin(iq).On(iq.ItemID == received.ItemID);
            query.InnerJoin(unitq).On(unitq.ItemID == received.SRCssdItemUnit &&
                                      unitq.StandardReferenceID == AppEnum.StandardReference.ItemUnit);
            query.Where(query.DecontaminationNo == e.DetailTableView.ParentItem.GetDataKeyValue("DecontaminationNo").ToString());
            query.OrderBy(received.CssdItemNo.Ascending);

            DataTable dtb = query.LoadDataTable();

            //Apply
            e.DetailTableView.DataSource = dtb;
        }
    }
}