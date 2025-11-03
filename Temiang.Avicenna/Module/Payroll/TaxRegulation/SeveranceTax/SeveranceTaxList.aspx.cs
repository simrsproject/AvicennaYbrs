using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class SeveranceTaxList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "SeveranceTaxSearch.aspx";
            UrlPageDetail = "SeveranceTaxDetail.aspx";

            ProgramID = AppConstant.Program.SeveranceTax; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(SeveranceTaxMetadata.ColumnNames.SeveranceTaxID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = SeveranceTaxs;
        }

        private DataTable SeveranceTaxs
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				SeveranceTaxQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (SeveranceTaxQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new SeveranceTaxQuery();
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.SeveranceTaxID,
                				query.ValidFrom,
                                query.IsNPWP,
                				query.LowerLimit,
                				query.UpperLimit,
                				query.TaxRate,
                				query.TaxAmount,
                				query.AmountOfDeduction,
                				query.LastUpdateDateTime,
                				query.LastUpdateByUserID
							);
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

