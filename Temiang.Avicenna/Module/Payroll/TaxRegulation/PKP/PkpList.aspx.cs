using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.TaxRegulation
{
    public partial class PkpList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "";
            UrlPageDetail = "PkpDetail.aspx";
			
			ProgramID = AppConstant.Program.Pkp; //TODO: Isi ProgramID
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
            string id = dataItem.GetDataKeyValue(PkpMetadata.ColumnNames.PkpID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = Pkps;
        }

        private DataTable Pkps
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				PkpQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (PkpQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new PkpQuery();
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				query.Select(
                				query.PkpID,
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

