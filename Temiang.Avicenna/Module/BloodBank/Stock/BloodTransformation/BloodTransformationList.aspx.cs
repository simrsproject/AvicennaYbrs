using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class BloodTransformationList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "BloodTransformationSearch.aspx";
            UrlPageDetail = "BloodTransformationDetail.aspx";

            ProgramID = AppConstant.Program.BloodStockTransformation;

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
            string id = dataItem.GetDataKeyValue(BloodTransformationMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("BloodTransformationDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = BloodTransformations;
        }

        private DataTable BloodTransformations
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                BloodTransformationQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BloodTransformationQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BloodTransformationQuery("a");
                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid,
                            "<'BloodTransformationDetail.aspx?md=view&id='+a.TransactionNo AS RUrl>"
                        );

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
            var query = new BloodTransformationItemQuery("a");
            var bn = new BloodBagNoQuery("b");
            var bt = new AppStandardReferenceItemQuery("c");
            var bgf = new AppStandardReferenceItemQuery("d");
            var bgt = new AppStandardReferenceItemQuery("e");

            query.Select
                (
                    query.TransactionNo,
                    query.BagNo,
                    bt.ItemName.As("BloodType"),
                    bn.BloodRhesus,
                    bgf.ItemName.As("BloodGroupFrom"),
                    bgt.ItemName.As("BloodGroupTo"),
                    query.VolumeBag.Coalesce("0"),
                    query.ExpiredDateTime

                );
            query.InnerJoin(bn).On(bn.BagNo == query.BagNo);
            query.InnerJoin(bt).On(bt.StandardReferenceID == AppEnum.StandardReference.BloodType && bt.ItemID == bn.SRBloodType);
            query.InnerJoin(bgf).On(bgf.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgf.ItemID == query.SRBloodGroupFrom);
            query.InnerJoin(bgt).On(bgt.StandardReferenceID == AppEnum.StandardReference.BloodGroup && bgt.ItemID == query.SRBloodGroupTo);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.BagNo.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }
    }
}
