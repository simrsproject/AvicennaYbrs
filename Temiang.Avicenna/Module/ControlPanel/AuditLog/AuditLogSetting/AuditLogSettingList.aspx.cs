using System;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;

namespace Temiang.Avicenna.Module.ControlPanel
{
    public partial class AuditLogSettingList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            UrlPageSearch = "AuditLogSettingSearch.aspx";
            UrlPageDetail = "AuditLogSettingDetail.aspx";

            ProgramID = AppConstant.Program.AuditLogSetting;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0],"view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(AuditLogSettingMetadata.ColumnNames.TableName).ToString();
            Page.Response.Redirect("AuditLogSettingDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = AuditLogSettings;
        }

        private DataTable AuditLogSettings
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                AuditLogSettingQuery query;
                if (Session[SessionNameForQuery] != null)
                    query = (AuditLogSettingQuery)Session[SessionNameForQuery];
                else
                    query = new AuditLogSettingQuery();
                query.es.Top = AppSession.Parameter.MaxResultRecord;

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}

