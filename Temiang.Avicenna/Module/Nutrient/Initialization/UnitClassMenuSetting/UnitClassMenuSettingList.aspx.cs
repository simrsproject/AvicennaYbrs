using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.Nutrient.Initialization
{
    public partial class UnitClassMenuSettingList : BasePageList
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ToolBarMenuQuickSearch.Enabled = true;
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WindowSearch.Height = 140;

            UrlPageSearch = "UnitClassMenuSettingSearch.aspx";
            UrlPageDetail = "UnitClassMenuSettingDetail.aspx";

            ProgramID = AppConstant.Program.UnitClassMenuSetting;
        }

        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string modus)
        {
            string id = dataItem.GetDataKeyValue(ServiceUnitMetadata.ColumnNames.ServiceUnitID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, modus, id);
            Page.Response.Redirect(url, true);
        }

        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = ServiceUnits;
        }

        private DataTable ServiceUnits
        {
            get
            {
                object obj = Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                ServiceUnitQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (ServiceUnitQuery)Session[SessionNameForQuery];
                else
                {
                    query = new ServiceUnitQuery();
                    query.Select
                        (
                            query.ServiceUnitID,
                            query.ServiceUnitName
                        );
                    query.Where(query.IsActive == true,
                                query.SRRegistrationType == AppConstant.RegistrationType.InPatient);
                    query.OrderBy(query.ServiceUnitID.Ascending);
                }

                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                Session[SessionNameForList] = dtb;

                return dtb;
            }
        }
    }
}
