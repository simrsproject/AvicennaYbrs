using System;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;

namespace Temiang.Avicenna.Module.Laundry.Master
{
    public partial class WashingProgramTypeList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            UrlPageSearch = "WashingProgramTypeSearch.aspx";
            UrlPageDetail = "WashingProgramTypeDetail.aspx";

            this.WindowSearch.Height = 400;

            ProgramID = AppConstant.Program.LaundryWashingProgramType;
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
            string id = dataItem.GetDataKeyValue(LaundryWashingProgramTypeMetadata.ColumnNames.LaundryProgramTypeID).ToString();
            Page.Response.Redirect("WashingProgramTypeDetail.aspx?md=" + mode + "&id=" + id, true);
        }

        protected void grdList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = LaundryWashingProgramTypes;
        }

        private DataTable LaundryWashingProgramTypes
        {
            get
            {
                object obj = this.Session[SessionNameForList];
                if (obj != null)
                    return ((DataTable)(obj));

                LaundryWashingProgramTypeQuery query;

                if (Session[SessionNameForQuery] != null)
                    query = (LaundryWashingProgramTypeQuery)Session[SessionNameForQuery];
                else
                {
                    query = new LaundryWashingProgramTypeQuery("a");
                    var pt = new AppStandardReferenceItemQuery("b");
                    var p = new AppStandardReferenceItemQuery("c");
                    var t = new AppStandardReferenceItemQuery("d");
                    
                    query.InnerJoin(pt).On(pt.StandardReferenceID == "LaundryProcessType" && pt.ItemID == query.SRLaundryProcessType);
                    query.InnerJoin(p).On(p.StandardReferenceID == "LaundryProgram" && p.ItemID == query.SRLaundryProgram);
                    query.InnerJoin(t).On(t.StandardReferenceID == "LaundryType" && t.ItemID == query.SRLaundryType);

                    query.es.Top = AppSession.Parameter.MaxResultRecord;
                    query.Select(
                        query.LaundryProgramTypeID,
                        query.SRLaundryProcessType,
                        pt.ItemName.As("LaundryProcessTypeName"),
                        query.SRLaundryProgram,
                        p.ItemName.As("LaundryProgramName"),
                        query.SRLaundryType,
                        t.ItemName.As("LaundryTypeName"),
                        query.Weight);
                    query.OrderBy(query.SRLaundryProcessType.Ascending, query.SRLaundryProgram.Ascending, query.SRLaundryType.Ascending);
                }

                DataTable dtb = query.LoadDataTable();
                this.Session[SessionNameForList] = dtb;
                return dtb;
            }
        }
    }
}