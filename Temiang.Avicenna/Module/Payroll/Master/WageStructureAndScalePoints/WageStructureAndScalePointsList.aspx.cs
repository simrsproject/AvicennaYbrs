using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Payroll.Master
{
    public partial class WageStructureAndScalePointsList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "WageStructureAndScalePointsSearch.aspx";
            UrlPageDetail = "WageStructureAndScalePointsDetail.aspx";

            ProgramID = AppConstant.Program.WageStructureAndScalePoints; //TODO: Isi ProgramID
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
            //TODO: Betulkan parameter nya
            string id = dataItem.GetDataKeyValue(WageStructureAndScaleMetadata.ColumnNames.WageStructureAndScaleID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
            Page.Response.Redirect(url, true);
        }
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = WageStructureAndScales;
        }

        private DataTable WageStructureAndScales
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }

                WageStructureAndScaleQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (WageStructureAndScaleQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new WageStructureAndScaleQuery("a");
                    var type = new AppStandardReferenceItemQuery("b");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query.WageStructureAndScaleID,
                        query.SRWageStructureAndScaleType,
                        type.ItemName.As("WageStructureAndScaleTypeName"),
                        query.WageStructureAndScaleCode,
                        query.WageStructureAndScaleName,
                        query.LastUpdateDateTime,
                        query.LastUpdateByUserID
                        );
                    query.InnerJoin(type).On(type.StandardReferenceID == AppEnum.StandardReference.WageStructureAndScaleType && type.ItemID == query.SRWageStructureAndScaleType);
                    query.OrderBy(query.SRWageStructureAndScaleType.Ascending, query.WageStructureAndScaleCode.Ascending);
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }
    }
}