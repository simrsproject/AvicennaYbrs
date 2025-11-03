using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Finance.Master
{
    public partial class ProductAccountList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "ProductAccountSearch.aspx";
            UrlPageDetail = "ProductAccountDetailWithGuarantorIncomeGroup.aspx";
            //UrlPageDetail = "ProductAccountDetail.aspx";

            ProgramID = AppConstant.Program.PRODUCTACCOUNT; //TODO: Isi ProgramID
            WindowSearch.Height = 300;

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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(ProductAccountMetadata.ColumnNames.ProductAccountID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ProductAccounts;
        }

        private DataTable ProductAccounts
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				ProductAccountQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (ProductAccountQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new ProductAccountQuery("a");
                    var itype = new AppStandardReferenceItemQuery("b");
                    query.InnerJoin(itype).On(query.SRItemType == itype.ItemID &
                                              itype.StandardReferenceID == AppEnum.StandardReference.ItemType);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                                    query.ProductAccountID,
                                    query.ProductAccountName,
                                    itype.ItemName.As("ItemTypeName"),
                                    query.IsActive,
                                    query.IsPpnOpr,
                                    query.IsPpnEmr
                                );

                    //Quick Search
                    ApplyQuickSearch(query, "ProductAccountName", "ProductAccountID");
                }
				
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

