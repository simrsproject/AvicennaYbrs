using System;
using System.Data;
using Telerik.Web.UI;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class EDCMachineList : BasePageList
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UrlPageSearch = "EDCMachineSearch.aspx";
            UrlPageDetail = "EDCMachineDetail.aspx";

            ProgramID = AppConstant.Program.EdcMachine;

            // Quick Search
            ToolBarMenuQuickSearch.Visible = true;
        }	
        public override void OnMenuEditClick(GridDataItem[] dataItems)
        {
			RedirectToPageDetail(dataItems[0],"edit");
        }
        public override void OnMenuViewClick(GridDataItem[] dataItems)
        {
			RedirectToPageDetail(dataItems[0],"view");
        }		
        private void RedirectToPageDetail(GridDataItem dataItem,string mode)
        {
            string id = dataItem.GetDataKeyValue(EDCMachineMetadata.ColumnNames.EDCMachineID).ToString();
            string url = string.Format("{0}?md={1}&id={2}", UrlPageDetail, mode, id);
			Page.Response.Redirect(url, true);
        }	
        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = EDCMachines;
        }

        private DataTable EDCMachines
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                {
                    return ((DataTable)(obj));
                }
				
				EDCMachineQuery query;
                if (Session[SessionNameForQuery] != null)
                {
                    query = (EDCMachineQuery)Session[SessionNameForQuery];
                }
                else
                {
                    query = new EDCMachineQuery("a");
                    AppStandardReferenceItemQuery qRef = new AppStandardReferenceItemQuery("b");
                    query.InnerJoin(qRef).On(
                        query.SRCardProvider == qRef.ItemID & qRef.StandardReferenceID == "CardProvider");
                    query.Select(query.EDCMachineID, query.EDCMachineName, qRef.ItemName.As("CardProviderName"), query.IsActive);
                    query.OrderBy(query.EDCMachineID.Ascending);

                    //Quick Search
                    ApplyQuickSearch(query, "EDCMachineName", "EDCMachineID");
                }
				query.es.Top = AppSession.Parameter.MaxResultRecord;
				DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }

        }

    }
}

