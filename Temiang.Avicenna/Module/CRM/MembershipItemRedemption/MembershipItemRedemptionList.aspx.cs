using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.CRM
{
    public partial class MembershipItemRedemptionList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            WindowSearch.Height = 300;
            UrlPageSearch = "MembershipItemRedemptionSearch.aspx";
            UrlPageDetail = "MembershipItemRedemptionDetail.aspx";

            ProgramID = AppConstant.Program.MembershipItemRedemption;

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
            string id = dataItem.GetDataKeyValue(MembershipItemRedemptionMetadata.ColumnNames.TransactionNo).ToString();
            Page.Response.Redirect("MembershipItemRedemptionDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = MembershipItemRedemptions;
        }

        private DataTable MembershipItemRedemptions
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                MembershipItemRedemptionQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (MembershipItemRedemptionQuery)Session[SessionNameForQuery];
                else
                {
                    query = new MembershipItemRedemptionQuery("a");
                    var mem = new MembershipQuery("b");
                    var pat = new PatientQuery("c");
                    var pat2 = new PatientQuery("d");
                    var sal = new AppStandardReferenceItemQuery("e");
                    query.InnerJoin(mem).On(mem.MembershipNo == query.MembershipNo);
                    query.InnerJoin(pat).On(pat.PatientID == query.PatientID);
                    query.InnerJoin(pat2).On(pat2.PatientID == mem.PatientID);
                    query.LeftJoin(sal).On(sal.StandardReferenceID == AppEnum.StandardReference.Salutation && sal.ItemID == pat.SRSalutation);
                    query.Select(
                                    query.TransactionNo,
                                    query.TransactionDate,
                                    query.MembershipNo,
                                    mem.JoinDate,
                                    pat2.PatientName,
                                    pat2.Address,
                                    pat2.PhoneNo,
                                    pat.PatientName.As("RedeemedBy"),
                                    query.IsApproved,
                                    query.IsVoid
                                );
                    query.OrderBy(query.TransactionNo.Descending);

                    //Quick Search
                    ApplyQuickSearch(query, "TransactionNo", "TransactionNo");
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;
                return dtb;
            }
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;
            string id = dataItem.GetDataKeyValue("TransactionNo").ToString();

            if (e.DetailTableView.Name.Equals("grdListDetail"))
            {
                var query = new MembershipItemRedemptionDetailQuery("a");
                var mem = new MembershipDetailQuery("b");
                query.InnerJoin(mem).On(mem.MembershipDetailID == query.MembershipDetailID);
                
                query.Select(query,
                    mem.StartDate,
                    mem.EndDate
                    );

                query.Where(query.TransactionNo == id);
                query.OrderBy(mem.StartDate.Ascending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
            else
            {
                var query = new MembershipItemRedemptionItemQuery("a");
                var itm = new MembershipItemRedeemQuery("b");
                var itmgr = new AppStandardReferenceItemQuery("c");
                query.InnerJoin(itm).On(itm.ItemReedemID == query.ItemReedemID);
                query.InnerJoin(itmgr).On(itmgr.StandardReferenceID == AppEnum.StandardReference.ItemReedemGroup && itmgr.ItemID == itm.SRItemReedemGroup);

                query.Select(query,
                    itm.ItemReedemName,
                    itmgr.ItemName.As("ItemReedemGroup")
                    );

                query.Where(query.TransactionNo == id);
                query.OrderBy(itm.SRItemReedemGroup.Ascending, query.ItemReedemID.Ascending);

                e.DetailTableView.DataSource = query.LoadDataTable();
            }
        }
    }
}