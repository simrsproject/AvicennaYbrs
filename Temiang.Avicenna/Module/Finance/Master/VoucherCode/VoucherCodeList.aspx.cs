using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class VoucherCodeList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "VoucherCodeSearch.aspx";
            UrlPageDetail = "VoucherCodeDetail.aspx";
			
			ProgramID = AppConstant.Program.VOUCHER_CODE;

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
            string vt = dataItem.GetDataKeyValue(JournalCodesMetadata.ColumnNames.JournalCodeId).ToString();
            string vc = dataItem.GetDataKeyValue(JournalCodesMetadata.ColumnNames.JournalCode).ToString();
            string url = string.Format("{0}?md={1}&vt={2}&vc={3}", UrlPageDetail, mode, vt,vc);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = VoucherCodes;
        }

        private DataTable VoucherCodes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                JournalCodesQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (JournalCodesQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new JournalCodesQuery("a");
                    var b = new BankQuery("b");
                    var c = new AppStandardReferenceItemQuery("c");
                    query.Select
                        (
                            query.JournalCodeId,
                            query.JournalCode,
                            query.Description,
                            query.CurrentNumber,
                            query.NumberFormat,
                            query.NumberSeed,
                            query.IsEnabled,
                            query.IsAutoNumber,
                            query.BankID,
                            query.CashType,
                            b.BankName,
                            c.ItemName,
                            query.IsBku                            
                        );
                    query.LeftJoin(b).On(query.BankID == b.BankID);
                    query.LeftJoin(c).On(c.StandardReferenceID == "CashManagementType" & query.CashType == c.ItemID);
                    query.Where(query.IsVisible == true);

                    //Quick Search
                    ApplyQuickSearch(query, "Description", "JournalCode");
                }
                query.es.Top = AppSession.Parameter.MaxResultRecord;
                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}