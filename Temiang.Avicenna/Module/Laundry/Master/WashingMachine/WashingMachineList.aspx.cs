using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Master.WashingMachine
{
    public partial class WashingMachineList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "WashingMachineSearch.aspx";
            UrlPageDetail = "WashingMachineDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.WashingMachine;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }

        public override void OnMenuEditClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "edit");
        }

        public override void OnMenuViewClick(Telerik.Web.UI.GridDataItem[] dataItems)
        {
            RedirectToPageDetail(dataItems[0], "view");
        }

        private void RedirectToPageDetail(GridDataItem dataItem, string mode)
        {
            string id = dataItem.GetDataKeyValue(LaundryWashingMachineMetadata.ColumnNames.MachineID).ToString();
            Page.Response.Redirect("WashingMachineDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = LaundryWashingMachines;
        }

        private DataTable LaundryWashingMachines
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundryWashingMachineQuery query;

                if (Session[SessionNameForQuery] != null)
                    query = (LaundryWashingMachineQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundryWashingMachineQuery("a");
                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query.MachineID,
                        query.MachineName,
                        query.StartUsingDate,
                        query.Volume,
                        query.Notes,
                        query.IsActive);
                    query.OrderBy(query.MachineID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "MachineName", "MachineID");
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}