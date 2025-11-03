using System;
using System.Linq;
using Temiang.Avicenna.BusinessObject;
using Temiang.Avicenna.Common;
using System.Data;
using Telerik.Web.UI;
using Temiang.Dal.DynamicQuery;
using System.Web.UI;

namespace Temiang.Avicenna.Module.RADT.Master
{
    public partial class NumberOfBedList : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            ProgramID = AppConstant.Program.NumberOfBed;
        }

        protected void grdList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdList.DataSource = NumberOfBeds;
        }

        private DataTable NumberOfBeds
        {
            get
            {
                var query = new NumberOfBedQuery("a");
                query.es.Distinct = true;
                query.Select
                    (
                        query.StartingDate
                    );

                query.OrderBy(query.StartingDate.Descending);

                return query.LoadDataTable();
            }
        }

        protected void btnFilter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            grdList.Rebind();
        }

        protected void grdList_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "detail":
                    var query = new NumberOfBedQuery("a");
                    var classq = new ClassQuery("b");

                    query.InnerJoin(classq).On(query.ClassID == classq.ClassID);
                    query.Select
                    (
                        query.StartingDate,
                        query.ClassID,
                        classq.ClassName
                    );

                    query.Where
                    (
                        query.StartingDate.Date() == Convert.ToDateTime(e.DetailTableView.ParentItem.GetDataKeyValue("StartingDate"))
                    );
                    query.es.Distinct = true;
                    query.OrderBy(query.ClassID.Ascending);
                    DataTable dtb = query.LoadDataTable();
                    e.DetailTableView.DataSource = dtb;

                    break;
            }
        }

        protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
        {
            base.RaisePostBackEvent(sourceControl, eventArgument);

            if (string.IsNullOrEmpty(eventArgument) || !(sourceControl is RadGrid))
                return;

            if (eventArgument == "new")
            {
                string url = string.Format("NumberOfBedDetail.aspx?md={0}", eventArgument);
                Page.Response.Redirect(url, true);
            }
        }
    }
}
