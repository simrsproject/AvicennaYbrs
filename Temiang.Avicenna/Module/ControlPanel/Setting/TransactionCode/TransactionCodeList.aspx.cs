using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.ControlPanel.Setting
{
    public partial class TransactionCodeList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "TransactionCodeSearch.aspx";
            UrlPageDetail = "TransactionCodeDetail.aspx";
			
			ProgramID = AppConstant.Program.TransactionCodeNumbering;
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
            string id = dataItem.GetDataKeyValue(AppAutoNumberTransactionCodeMetadata.ColumnNames.SRTransactionCode).ToString();
            string url = string.Format("TransactionCodeDetail.aspx?md={0}&id={1}", mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AppAutoNumberTransactionCodes;
        }

        private DataTable AppAutoNumberTransactionCodes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                AppAutoNumberTransactionCodeQuery query;
                AppStandardReferenceItemQuery sr = new AppStandardReferenceItemQuery("b");

                if (Session[SessionNameForQuery] != null)
                {
                    query = (AppAutoNumberTransactionCodeQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new AppAutoNumberTransactionCodeQuery("a");
                    query.InnerJoin(sr).On(query.SRTransactionCode == sr.ItemID & sr.StandardReferenceID == "TransactionCode");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                query.Select(query.SRTransactionCode, sr.ItemName.As("refToAppStandardReferenceItem_ItemName"), query.SRAutoNumber);

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}
