using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.BloodBank.Stock
{
    public partial class StockReceivedList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "StockReceivedSearch.aspx";
            UrlPageDetail = "StockReceivedDetail.aspx";

            ProgramID = AppConstant.Program.BloodStockReceived;

            this.WindowSearch.Height = 400;

            UrlPageDetailImport = "openWinImport('" + ProgramID + "');";
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
            string id = dataItem.GetDataKeyValue(BloodReceivedMetadata.ColumnNames.TransactionNo).ToString();
            string url = string.Format("StockReceivedDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
                grdList.DataSource = BloodReceiveds;
        }

        private DataTable BloodReceiveds
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                BloodReceivedQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (BloodReceivedQuery)Session[SessionNameForQuery];
                else
                {
                    query = new BloodReceivedQuery("a");
                    var bs = new AppStandardReferenceItemQuery("b");
                    
                    query.Select
                        (
                            query.TransactionNo,
                            query.TransactionDate,
                            bs.ItemName.As("BloodSource"),
                            query.Notes,
                            query.IsApproved,
                            query.IsVoid,
                            "<'StockReceivedDetail.aspx?md=view&id='+a.TransactionNo AS RUrl>"
                        );

                    query.InnerJoin(bs).On(bs.ItemID == query.SRBloodSource && bs.StandardReferenceID == AppEnum.StandardReference.BloodSource);

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
            var query = new BloodReceivedItemQuery("a");
            var bt = new AppStandardReferenceItemQuery("b");
            var bg = new AppStandardReferenceItemQuery("c");

            query.Select
                (
                    query.TransactionNo,
                    query.BagNo,
                    bt.ItemName.As("BloodType"),
                    query.BloodRhesus,
                    bg.ItemName.As("BloodGroup"),
                    query.VolumeBag.Coalesce("0"),
                    query.ExpiredDateTime
                );
            query.InnerJoin(bt).On(bt.ItemID == query.SRBloodType && bt.StandardReferenceID == AppEnum.StandardReference.BloodType);
            query.InnerJoin(bg).On(bg.ItemID == query.SRBloodGroup && bg.StandardReferenceID == AppEnum.StandardReference.BloodGroup);
            query.Where(query.TransactionNo == e.DetailTableView.ParentItem.GetDataKeyValue("TransactionNo").ToString());
            query.OrderBy(query.BagNo.Ascending);

            //Apply
            e.DetailTableView.DataSource = query.LoadDataTable();
        }

        public override void OnMenuExportToExcelClick(ValidateArgs args)
        {
            try
            {
                var table = GetDataGridDataTable();
                if (table.Rows.Count > 0)
                {
                    var fileName = "STOCK_RECEIVED_" + DateTime.Now.Date.ToString("yyyyMMdd");

                    Common.CreateExcelFile.CreateExcelDocument(table, fileName.Replace('/', '_').Replace(".", "_").Replace(" ", "_") + AppSession.Parameter.ExcelFileExtension, this.Response);
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw new Exception(error);
            }

            args.IsCancel = true;
        }

        private DataTable GetDataGridDataTable()
        {
            var query = new BloodReceivedQuery("a");
            query.es.Top = 1;
            query.Select
                (
                @"<'' AS TransactionNo>",
                @"<'' AS TransactionDate>",
                @"<'' AS SRBloodSource>",
                @"<'' AS SRBloodSourceFrom>",
                @"<'' AS BagNo>",
                @"<'' AS SRBloodType>",
                @"<'' AS SRBloodGroup>",
                @"<'' AS BloodRhesus>",
                @"<'' AS VolumeBag>",
                @"<'' AS ExpiredDateTime>",
                @"<0 AS isApproved>",
                @"<0 AS isVoid>"
                );

            DataTable dtb = query.LoadDataTable();
            return dtb;
        }
    }
}
