using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class BloodExterminationList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "BloodExterminationSearch.aspx";
            UrlPageDetail = "BloodExterminationDetail.aspx";

            ProgramID = AppConstant.Program.BloodStockExtermination;

            this.WindowSearch.Height = 400;
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
            string id = dataItem.GetDataKeyValue(BloodExterminationMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("BloodExterminationDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = BloodExterminations;
        }

        private DataTable BloodExterminations
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                BloodExterminationQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BloodExterminationQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BloodExterminationQuery("a");
                    var bs = new AppStandardReferenceItemQuery("b");
                    var usr = new AppUserQuery("c");

                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            bs.ItemName.As("ReasonsForExtermination"),
                            query.Weight,
                            usr.UserName.As("BloodBankOfficer"),
                            query.IncineratorOperator,
                            query.KnownBy,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid,
                            "<'BloodExterminationDetail.aspx?md=view&id='+a.TransactionNo AS RUrl>"
                        );

                    query.InnerJoin(bs).On(bs.StandardReferenceID == AppEnum.StandardReference.ReasonsForExtermination && bs.ItemID == query.SRReasonsForExtermination);
                    query.InnerJoin(usr).On(usr.UserID == query.BloodBankOfficerByUserID);
                    query.OrderBy(query.TransactionDate.Descending, query.TransactionNo.Descending);
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
            var query = new BloodExterminationItemQuery("a");
            var bn = new BloodBagNoQuery("b");
            var bt = new AppStandardReferenceItemQuery("c");
            var bg = new AppStandardReferenceItemQuery("d");

            query.Select
                (
                    query.TransactionNo,
                    query.BagNo,
                    bt.ItemName.As("BloodType"),
                    bn.BloodRhesus,
                    bg.ItemName.As("BloodGroup"),
                    bn.ExpiredDateTime
                );
            query.InnerJoin(bn).On(bn.BagNo == query.BagNo);
            query.InnerJoin(bt).On(bt.StandardReferenceID == AppEnum.StandardReference.BloodType && bt.ItemID == bn.SRBloodType);
            query.InnerJoin(bg).On(bg.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bg.ItemID == bn.SRBloodGroup);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.BagNo.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
