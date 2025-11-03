using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class PtkpList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "PtkpSearch.aspx";
            UrlPageDetail = "PtkpDetail.aspx";

            ProgramID = AppConstant.Program.Ptkp; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PtkpMetadata.ColumnNames.PtkpID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = PTKPs;
        }

        private DataTable PTKPs
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PtkpQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PtkpQuery)Session[SessionNameForQuery];
                }
                else
                {
                    AppStandardReferenceItemQuery tax = new AppStandardReferenceItemQuery("b"); 
                    query = new PtkpQuery("a");

                    query.Select(
                                query.PtkpID,
                                query.ValidFrom,
                                query.ValidTo,
                                query.SRTaxStatus,
                                tax.ItemName.As("TaxStatusName"),
                                query.Amount,
                                query.LastUpdateDateTime,
                                query.LastUpdateByUserID
                            );
                    query.InnerJoin(tax).On
                            (
                                query.SRTaxStatus == tax.ItemID &
                                tax.StandardReferenceID == "TaxStatus"
                            );

                    query.OrderBy(query.ValidFrom.Descending, query.PtkpID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

